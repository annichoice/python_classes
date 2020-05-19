## Requirements
You have to install the following packages:

[getopt](https://linux.die.net/man/3/getopt)<br>
[jq](https://stedolan.github.io/jq/)<br>
[git](https://git-scm.com/)<br>
[tput](https://command-not-found.com/tput)<br>
[bc](https://www.gnu.org/software/bc/)<br>
[curl](https://curl.se/download.html)<br>
[parallel (version > 20220515)](https://www.gnu.org/software/parallel/)

## How to run
### 1. clone

```shell
[~]>$ git clone https://github.com/MortezaBashsiz/CFScanner.git
```

### 2. Change directory and make them executable

```shell
[~]>$ cd CFScanner/bash
[~/CFScanner/bash]> chmod +x ../bin/*
```

### 3. Get config.real

```shell
[~/CFScanner/bash]>$ curl -s https://raw.githubusercontent.com/MortezaBashsiz/CFScanner/main/bash/ClientConfig.json -o config.real
```

In the config file the variables are
```shell
{
	"id": "User's UUID",
	"Host": "Host address which is behind Cloudflare",
	"Port": "Port which you are using behind Cloudflare on your origin server",
	"path": "Websocket endpoint like api20",
	"serverName": "SNI",
   	"subnetsList": "https://raw.githubusercontent.com/MortezaBashsiz/CFScanner/main/bash/cf.local.iplist"
}
```

NOTE: If you want to use your custom config DO NOT use it as config.real since script will update this file. Store your config in another file and pass it as an argument to script instead of config.real

### 4. Execute it



#### in Linux:

At following command pay attention to the arguments **vpn** **SUBNET or IP** **DOWN or UP or BOTH**, **threads**, **tryCount**, **speed** and **Custom Subnet File**.
You have following switches to define the arguments 



-vpn: YES or NO, you are able to define for script to test with your vmess or not

-m: SUBNET or IP, Choose one of them for scanning subnets or single IPs

-t: DOWN or UP or BOTH, Choos one of them for download and upload test

-thr: This is an integer number that defines the parallel threads count

-try: This is an integer to define how many times you like to check an IP

-s: This is the filter that you can define to list the IPs based on download speed. The value is in KBPS (Kilo Bytes Per Se