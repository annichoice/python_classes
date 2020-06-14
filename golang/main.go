package main

import (
	configuration "CFScanner/configuration"
	scan "CFScanner/scanner"
	utils "CFScanner/utils"
	"bufio"
	"fmt"
	"log"
	"os"
	"runtime"
	"strings"

	"github.com/spf13/cobra"
)

var config = configuration.ConfigStruct{
	Local_port:           0,
	Address_port:         "0",
	User_id:              "",
	Ws_header_host:       "",
	Ws_header_path:       "",
	Sni:                  "",
	Do_upload_test:       false,
	Do_fronting_test:     false,
	Min_dl_speed:         50.0,
	Min_ul_speed:         50.0,
	Max_dl_time:          -2.0,
	Max_ul_time:          -2.0,
	Max_dl_latency:       -1.0,
	Max_ul_latency:       -1.0,
	Fronting_timeout:     -1.0,
	Startprocess_timeout: -1.0,
	N_tries:              -1,
	Vpn:                  false,
}

// Program Info
var (
	version  = "1.0"
	build    = "Custom"
	codename = "CFScanner , CloudFlare Scanner."
)

func Version() string {
	return version
}

// VersionStatement returns a list of strings representing the full version info.
func VersionStatement() string {
	return strings.Join([]string{
		"CFScanner ", Version(), " (", codename, ") ", build, " (", runtime.Version(), " ", runtime.GOOS, "/", runtime.GOARCH, ")",
	}, "")
}

func main() {
	var threads int
	var configPath string
	v