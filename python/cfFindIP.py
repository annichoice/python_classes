
#!/usr/bin/env python

import argparse
import ipaddress
import json
import multiprocessing
import os
import platform
import re
import signal
import socket
import socketserver
import statistics
import subprocess
import sys
import time
import traceback
from datetime import datetime
from functools import partial
from typing import Tuple

import requests
from clog import CLogger

log = CLogger("CFScanner-python")


SCRIPTDIR = os.path.dirname(os.path.realpath(__file__))
BINDIR = f"{SCRIPTDIR}/../bin"
CONFIGDIR = f"{SCRIPTDIR}/../config"
RESULTDIR = f"{SCRIPTDIR}/../result"
START_DT_STR = datetime.now().strftime(r"%Y%m%d_%H%M%S")
INTERIM_RESULTS_PATH = os.path.join(RESULTDIR, f'{START_DT_STR}_result.csv')


class TestConfig:
    local_port = 0
    address_port = 0
    user_id = ""
    ws_header_host = ""
    ws_header_path = ""
    sni = ""
    do_upload_test = False
    min_dl_speed = 99999.0  # kilobytes per second
    min_ul_speed = 99999.0  # kilobytes per second
    max_dl_time = -2.0  # seconds
    max_ul_time = -2.0  # seconds
    max_dl_latency = -1.0  # seconds
    max_ul_latency = -1.0  # seconds
    fronting_timeout = -1.0  # seconds
    startprocess_timeout = -1.0  # seconds
    n_tries = -1
    no_vpn = False
    use_xray = False
    proxy_config_template = ""


class _COLORS:
    OKBLUE = '\033[94m'
    OKGREEN = '\033[92m'
    WARNING = '\033[93m'
    FAIL = '\033[91m'
    ENDC = '\033[0m'


def get_free_port():
    with socketserver.TCPServer(("localhost", 0), None) as s:
        free_port = s.server_address[1]
    return free_port


def create_proxy_config(
    edge_ip,
    test_config: TestConfig
) -> str:
    """creates proxy (v2ray/xray) config json file based on ``TestConfig`` instance

    Args:
        edge_ip (str): Cloudflare edge ip to use for the config
        test_config (TestConfig): instance of ``TestConfig`` containing information about the setting of the test

    Returns:
        config_path (str): the path to the json file created
    """
    test_config.local_port = get_free_port()
    local_port_str = str(test_config.local_port)
    config = test_config.proxy_config_template.replace(
        "PORTPORT", local_port_str)
    config = config.replace("IP.IP.IP.IP", edge_ip)
    config = config.replace("CFPORTCFPORT", str(test_config.address_port))
    config = config.replace("IDID", test_config.user_id)
    config = config.replace("HOSTHOST", test_config.ws_header_host)
    config = config.replace("ENDPOINTENDPOINT", test_config.ws_header_path)
    config = config.replace("RANDOMHOST", test_config.sni)

    config_path = f"{CONFIGDIR}/config-{edge_ip}.json"
    with open(config_path, "w") as configFile:
        configFile.write(config)

    return config_path


def wait_for_port(
    port: int,
    host: str = 'localhost',
    timeout: float = 5.0
) -> None:
    """Wait until a port starts accepting TCP connections.
    Args:
        port: Port number.
        host: Host address on which the port should exist.
        timeout: In seconds. How long to wait before raising errors.
    Raises:
        TimeoutError: The port isn't accepting connection after time specified in `timeout`.
    """
    start_time = time.perf_counter()
    while True:
        try:
            with socket.create_connection((host, port), timeout=timeout):
                break
        except OSError as ex:
            time.sleep(0.01)
            if time.perf_counter() - start_time >= timeout:
                raise TimeoutError(
                    f'Timeout exceeded for the port {port} on host {host} to start accepting connections.'
                ) from ex


def fronting_test(
    ip: str,
    timeout: float
) -> bool:
    """conducts a fronting test on an ip and return true if status 200 is received

    Args:
        ip (str): ip for testing
        timeout (float): the timeout to wait for ``requests.get`` result

    Returns:
        bool: True if ``status_code`` is 200, False otherwise
    """
    s = requests.Session()
    s.get_adapter(
        'https://').poolmanager.connection_pool_kw['server_hostname'] = "speed.cloudflare.com"
    s.get_adapter(
        'https://').poolmanager.connection_pool_kw['assert_hostname'] = "speed.cloudflare.com"

    success = False
    try:
        r = s.get(
            f"https://{ip}",
            timeout=timeout,
            headers={"Host": "speed.cloudflare.com"}
        )
        if r.status_code != 200:
            print(
                f"{_COLORS.FAIL}NO {_COLORS.WARNING}{ip:15s} fronting error {r.status_code} {_COLORS.ENDC}")
        else:
            success = True
    except requests.exceptions.ConnectTimeout as e:
        print(
            f"{_COLORS.FAIL}NO {_COLORS.WARNING}{ip:15s} fronting connect timeout{_COLORS.ENDC}"
        )
    except requests.exceptions.ReadTimeout as e:
        print(
            f"{_COLORS.FAIL}NO {_COLORS.WARNING}{ip:15s} fronting read timeout{_COLORS.ENDC}"
        )
    except requests.exceptions.ConnectionError as e:
        print(
            f"{_COLORS.FAIL}NO {_COLORS.WARNING}{ip:15s} fronting connection error{_COLORS.ENDC}"
        )
    except Exception as e:
        f"{_COLORS.FAIL}NO {_COLORS.WARNING}{ip:15s}fronting Unknown error{_COLORS.ENDC}"
        log.error(f"Fronting test Unknown error {ip:15}")
        log.exception(e)

    return success


def current_platform() -> str:
    """Get current platform name by short string."""
    if sys.platform.startswith('linux'):
        return 'linux'
    elif sys.platform.startswith('darwin'):
        if "arm" in platform.processor().lower():
            return 'mac-arm'
        else:
            return 'mac'
    elif (
        sys.platform.startswith('win')
        or sys.platform.startswith('msys')
        or sys.platform.startswith('cyg')
    ):
        if sys.maxsize > 2 ** 31 - 1:
            return 'win64'
        return 'win32'
    raise OSError('Unsupported platform: ' + sys.platform)


def start_proxy_service(
    proxy_conf_path: str,
    timeout=5,
    use_xray=False
) -> Tuple[subprocess.Popen, dict]:
    """starts the proxy (v2ray/xray) service and waits for the respective port to open

    Args:
        proxy_conf_path (str): the path to the proxy (v2ray or xray) config json file
        timeout (int, optional): total time in seconds to wait for the proxy service to start. Defaults to 5.
        use_xray (bool, optional): if true, xray will be used, otherwise v2ray

    Returns:
        Tuple[subprocess.Popen, dict]: the v2 ray process object and a dictionary containing the proxies to use with ``requests.get`` 