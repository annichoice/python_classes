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
                    addTextLog(configManager.errorMessage);
                }

                isAppCongigValid = false;
            }

            scanEngine = new ScanEngine();

            loadLastResultsComboList();
            comboTargetSpeed.SelectedIndex = 2; // 100kb/s
            comboDownloadTimeout.SelectedIndex = 0; //2 seconds timeout
            loadCustomConfigsComboList();

            appVersion = AppUpdateChecker.getCurrentVersion();

            DateTime buildDate = new DateTime(2000, 1, 1)
                                    .AddDays(appVersion.Build).AddSeconds(appVersion.Revision * 2);
            string displayableVersion = $" - {appVersion}";
            this.Text += displayableVersion;

            appUpdateChecker = new AppUpdateChecker();

            // is debug mode enable? this line should be at bottom line
            checkEnableDebugMode();
        }

        private void loadCustomConfigsComboList(string selectedConfigFileName = "")
        {
            if (!configManager.isConfigValid() || configManager.customConfigs.customConfigInfos.Count == 0)
            {
                comboConfigs.SelectedIndex = 0;
                return;
            }

            comboConfigs.Items.Clear();
            comboConfigs.Items.Add(new CustomConfigInfo("Default", "Default"));

            foreach (var customConfig in configManager.customConfigs.customConfigInfos)
            {
                comboConfigs.Items.Add(customConfig);
            }

            int selectedIndex = 0;
            if (selectedConfigFileName != "")
            {
                selectedIndex = comboConfigs.FindStringExact(selectedConfigFileName);
            }

            comboConfigs.SelectedIndex = selectedIndex;
        }

        public CustomConfigInfo getSelectedV2rayConfig()
        {
            if (comboConfigs.SelectedItem is not null and not (object)"Default")
            {
                return (CustomConfigInfo)comboConfigs.SelectedItem;
            }

            // return defualt config if nothing is selected
            return new CustomConfigInfo("Default", "Default");
        }

        private void checkEnableDebugMode()
        {
            if (configManager.enableDebug)
            {
                comboConcurrent.Text = "1";
                comboConcurrent.Enabled = false;
                lblDebugMode.Visible = true;
                addTextLog("Debug mode is enabled. In this mode concurrent process is set to 1 and you can see scan debug data in 'debug.txt' file in the app directory.");
                addTextLog("To exit debug mode delete 'enable-debug' file from the app directory and then re-open the app.");

                string systemInfo = $"OS: {System.Runtime.InteropServices.RuntimeInformation.OSDescription} {System.Runtime.InteropServices.RuntimeInformation.OSArchitecture}, " +
                    $"Cpu Arch: {System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture}, Framework: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}";
                Tools.logStep($"\n\nApp started. Version: {appVersion}\n{systemInfo}");
            }
        }

        // add text log to log textbox
        delegate void SetTextCallback(string log);
        public void addTextLog(string log)
        {
            try
            {
                if (this.txtLog.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(addTextLog);
                    this.Invoke(d, new object[] { log });
                }
                else
                {
                    txtLog.AppendText(log + Environment.NewLine);
                }
            }
            catch (Exception)
            {
            }
        }


        private void btnScanInPrevResults_Click(object sender, EventArgs e)
        {
            startStopScan(true);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void startStopScan(bool inPrevResult = false)
        {
            if (!isAppCongigValid)
            {
                showCanNotContinueMessage();
                return;
            }

            if (isManualTesting)
            {
                addTextLog($"Can not start while app is scanning.");
                return;
            }

            if (isScanRunning())
            {
                // stop scan
                btnStart.Enabled = false;
                waitUntilScannerStoped();
                updateUIControlls(false);
            }
            else
            {   // start scan
                if (inPrevResult)
                {
                    if (currentScanResults.Count == 0)
                    {
                        addTextLog("Current result list is empty!");
                        return;
                    }
                    addTextLog($"Start scanning {currentScanResults.Count} IPs in previous results...");
                }
                else
                {
                    // set cf ip list to scan engine
                    string[] ipRanges = getCheckedCFIPList();
                    if (ipRanges.Length == 0)
                    {
                        tabControl1.SelectTab(0);
                        MessageBox.Show($"No Cloudflare IP ranges are selected. Please select some IP ranges.",
                             "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    currentScanResults = new();
                    scanEngine.setCFIPRangeList(ipRanges);
                    addTextLog($"Start scanning {ipRanges.Length} Cloudflare IP ranges...");
                }

                updateUIControlls(true);
                scanEngine.workingIPsFromPrevScan = inPrevResult ? currentScanResults : null;
                scanEngine.targetSpeed = getTargetSpeed(); // set target speed
                scanEngine.scanConfig = getSelectedV2rayConfig(); // set scan config
                scanEngine.downloadTimeout = getDownloadTimeout(); // set download timeout
                string scanConfigContent = scanEngine.scanConfig.content;

                Tools.logStep($"Starting scan engine with target speed: {scanEngine.targetSpeed.getTargetSpeed()}, dl timeout: {scanEngine.downloadTimeout}, " +
                    $"config: '{scanEngine.scanConfig}' => " +
                    $"{scanConfigContent.Substring(0, Math.Min(150, scanConfigContent.Length))}...");

                var scanType = inPrevResult ? ScanType.SCAN_IN_PERV_RESULTS : ScanType.SCAN_CLOUDFLARE_IPS;

                // start scan job in new thread
                Task.Factory.StartNew(() => scanEngine.start(scanType))
                    .ContinueWith(done =>
                    {
                        scanFinshed = true;
                        this.currentScanResults = scanEngine.progressInfo.scanResults.workingIPs;
                        addTextLog($"{scanEngine.progressInfo.totalCheckedIP:n0} IPs tested and found {scanEngine.progressInfo.scanResults.totalFoundWorkingIPs:n0} working IPs.");
                    });

                tabControl1.SelectTab(1);
            }
        }

        private int getDownloadTimeout()
        {
            string timeoutStr = comboDownloadTimeout.Selecte