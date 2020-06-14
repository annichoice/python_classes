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
	Sni:                 