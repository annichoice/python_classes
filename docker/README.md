# CloudFlare Scanner
This script scans Millions of Cloudflare IP addresses and generates a result file containing the IPs which are work with CDN.

This script uses v2ray+vmess+websocket+tls by default and if you want to use it behind your Cloudflare proxy then you have to set up a vmess account, otherwise, it will use the default configuration.

Docker repository page [HERE](https://hub.docker.com/r/bashsiz/cfscanner "HERE")

## How to run
### 1. pull image

```shell
[~]>$ sudo docker pull bashsiz/cfscanner:latest
```

### 2. make directories

```shell
[~]>$ mkdir -p /tmp/cfscanner/config /tmp/cfscanner/result
[~]>$ cd /tmp/cfscanner/
[/tmp/cfscanner]>$
```

### 3. Run docker image

```shell
[/tmp/cfscanner]>$ sudo docker r