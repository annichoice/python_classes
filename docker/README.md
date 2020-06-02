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
[/tmp/cfscanner]>$ sudo docker run -v /tmp/cfscanner/config:/CFSCANNER/CFScanner/config -v /tmp/cfscanner/result:/CFSCANNER/CFScanner/result -it bashsiz/cfscanner:latest bash
root@1b2f73d5988c:/CFSCANNER# ls
CFScanner
root@1b2f73d5988c:/CFSCANNER/CFScanner# git pull
root@1b2f73d5988c:/CFSCANNER/CFScanner# cd bash/
root@1b2f73d5988c:/CFSCANNER/CFScanner/bash# ls
cf.local.iplist  cfScanner.sh  config.json.temp  config.script	custom.ips  custom.subnets
root@1b2f73d5988c:/CFSCANNER/CFScanner/bash#
```

### 4. Execute it

This step is same as before, you can see in the main [README](https://github.com/MortezaBashsiz/CFScanner/tree/main/bash/README.md "README"). of project

### 5. Result

You can find the result and config files in the directories that you mounted as volume to the container

```shell
[/tmp/cfscanner]>$ ls *
config:
conf