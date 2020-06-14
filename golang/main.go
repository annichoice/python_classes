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
	var Vpn bool
	var subnets string
	var doUploadTest bool
	var nTries int
	var minDLSpeed float64
	var minULSpeed float64
	var maxDLTime float64
	var maxULTime float64

	var startProcessTimeout float64
	var frontingTimeout float64
	var maxDLLatency float64
	var maxULLatency float64
	var fronting bool
	var v2raypath string

	var bigIPList []string

	rootCmd := &cobra.Command{
		Use:   os.Args[0],
		Short: codename,
		Run: func(cmd *cobra.Command, args []string) {
			fmt.Println(VersionStatement())
			if v2raypath != "" {
				configuration.BINDIR = v2raypath
			}
			if !Vpn {
				utils.CreateDir(configuration.CONFIGDIR)
			}
			utils.CreateDir(configuration.RESULTDIR)
			if err := configuration.CreateInterimResultsFile(configuration.INTERIM_RESULTS_PATH, nTries); err != nil {
				fmt.Printf("Error creating interim results file: %v\n", err)
			}
			// number of threads for scanning
			threadsCount := threads

			// lists of ip for scanning proccess
			var IPLIST []string

			file, _ := utils.Exists(subnets)
			if file {
				if subnets != "" {
					subnetFilePath := subnets
					subnetFile, err := os.Open(subnetFilePath)
					if err != nil {
						log.Fatal(err)
					}
					defer subnetFile.Close()

					scanner := bufio.NewScanner(subnetFile)
					for scanner.Scan() {
						IPLIST = append(IPLIST, strings.TrimSpace(scanner.Text()))
					}
					if err := scanner.Err(); err != nil {
						log.Fatal(err)
					}
				}
			} else {
				// Parsing cidr input
				if strings.Contains(subnets, "/") {
					var subnetips []string
					subnetips = append(subnetips, subnets)

					ips := utils.IPParser(subnetips)

					IPLIST = append(IPLIST, ips...)
				} else {
					// P