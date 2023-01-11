using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Windows.Forms;
using WinCFScan.Classes;
using WinCFScan.Classes.Config;
using WinCFScan.Classes.HTTPRequest;
using WinCFScan.Classes.IP;
using static System.Net.Mime.MediaTypeNames;

namespace WinCFScan
{
    public partial class frmMain : Form
    {
        private const string ourGitHubUrl = "https://github.com/MortezaBashsiz/CFScanner";
        private const string helpCustomConfigUrl = "https://github.com/MortezaBashsiz/CFScanner/discussions/210";
        private const string buyMeCoffeeUrl = "https://www.buymeacoffee.com/Bashsiz";
        ConfigManager configManager;
        bool oneTimeChecked = false; // config checked once?
        ScanEngine scanEngine;
        private List<ResultItem> currentScanResults = new();
        private bool scanFinshed = false;
        private bool isUpdatinglistCFIP;
        private bool isAppCongigValid = true;
        private bool isManualTesting = false; // is testing ips 
        private ListViewColumnSorter listResultsColumnSorter;
        private ListViewColumnSorter listCFIPsColumnSorter;
        private Version appVersion;
        private AppUpdateChecker appUpdateChecker;
        private bool stopAvgTetingIsRequested;

        public frmMain()
        {
            InitializeComponent();

            listResultsColumnSorter = new ListViewColumnSorter();
            this.listResults.ListViewItemSorter = listResultsColumnSorter;

            listCFIPsColumnSorter = new ListViewColumnSorter();
            this.listCFIPList.ListViewItemSorter = listCFIPsColumnSorter;

            // load configs
            configManager = new();
            if (!configManager.isConfigValid())
            {
                addTextLog("App config is not valid! we can not continue.");

                if (configManager.errorMessage != "")
                {
                    addTextL