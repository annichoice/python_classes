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
            string timeoutStr = comboDownloadTimeout.SelectedItem.ToString().Replace(" Seconds", "");

            if (int.TryParse(timeoutStr, out int downloadTimeout))
                return downloadTimeout;
            else
                return 2;
        }

        private void updateUIControlls(bool isStarting)
        {
            if (isStarting)
            {
                loadLastResultsComboList();
                listResults.Items.Clear();
                btnStart.Text = "Stop Scan";
                btnScanInPrevResults.Enabled = false;
                btnResultsActions.Enabled = false;
                comboConcurrent.Enabled = false;
                comboTargetSpeed.Enabled = false;
                comboConfigs.Enabled = false;
                timerProgress.Enabled = true;
                //btnSkipCurRange.Enabled = true;
                comboResults.Enabled = false;
                tabPageCFRanges.Enabled = false;
            }
            else
            {   // is stopping
                btnStart.Text = "Start Scan";
                btnStart.Enabled = true;
                btnScanInPrevResults.Enabled = true;
                btnResultsActions.Enabled = true;
                timerProgress.Enabled = false;
                lblRunningWorkers.Text = $"Threads: 0";

                //btnSkipCurRange.Enabled = false;
                comboResults.Enabled = true;
                if (!configManager.enableDebug)
                {
                    comboConcurrent.Enabled = true;
                }
                comboTargetSpeed.Enabled = true;
                comboConfigs.Enabled = true;
                tabPageCFRanges.Enabled = true;

                // save result file if found working IPs
                var scanResults = scanEngine.progressInfo.scanResults;
                if (scanResults.totalFoundWorkingIPs != 0)
                {
                    // save results into disk
                    if (!scanResults.save())
                    {
                        addTextLog($"Could not save scan result into the file: {scanResults.resultsFileName}");
                    }
                }
                else
                {
                    // delete result file if there is no woriking ip
                    scanResults.remove();
                }

                loadLastResultsComboList();

                // sort results list
                listResultsColumnSorter.Order = SortOrder.Ascending;
                listResultsColumnSorter.SortColumn = 0;
                listResults.Sort();
            }
        }

        private void timerBase_Tick(object sender, EventArgs e)
        {
            oneTimeChecks();
            if (scanFinshed)
            {
                scanFinshed = false;
                updateConrtolsProgress(true);
                updateUIControlls(false);
            }

            btnStopAvgTest.Visible = isManualTesting;
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            updateConrtolsProgress();
        }

        private void updateConrtolsProgress(bool forceUpdate = false)
        {
            var pInf = scanEngine.progressInfo;
            if (isScanRunning() || forceUpdate)
            {
                lblLastIPRange.Text = $"Current IP range: {pInf.currentIPRange} ({pInf.currentIPRangesNumber:n0}/{pInf.totalIPRanges:n0})";
                labelLastIPChecked.Text = $"Last checked IP:  {pInf.lastCheckedIP} ({pInf.totalCheckedIPInCurIPRange:n0}/{pInf.currentIPRangeTotalIPs:n0})";
                lblTotalWorkingIPs.Text = $"Total working IPs found:  {pInf.scanResults.totalFoundWorkingIPs:n0}";
                if (pInf.scanResults.fastestIP != null)
                {
                    txtFastestIP.Text = $"{pInf.scanResults.fastestIP.ip}  -  {pInf.scanResults.fastestIP.delay:n0} ms";
                }

                lblRunningWorkers.Text = $"Threads: {pInf.curentWorkingThreads}";

                prgOveral.Maximum = pInf.totalIPRanges;
                prgOveral.Value = pInf.currentIPRangesNumber;

                prgCurRange.Maximum = pInf.currentIPRangeTotalIPs;
                prgCurRange.Value = pInf.totalCheckedIPInCurIPRange;
                prgCurRange.ToolTipText = $"Current IP range progress: {pInf.getCurrentRangePercentIsDone():f1}%";

                fetchWorkingIPResults();
                pInf.scanResults.autoSave();

                fetchScanEngineLogMessages();

                // exception rate
                pInf.frontingExceptions.setControlColorStyles(btnFrontingErrors);
                pInf.downloadExceptions.setControlColorStyles(btnDownloadErrors);
                btnFrontingErrors.Text = $"Fronting Errors : {pInf.frontingExceptions.getErrorRate():f1}%";
                btnDownloadErrors.Text = $"Download Errors : {pInf.downloadExceptions.getErrorRate():f1}%";
                btnFrontingErrors.ToolTipText = $"Total errors: {pInf.downloadExceptions.getTotalErros()}";
                btnDownloadErrors.ToolTipText = $"Total errors: {pInf.frontingExceptions.getTotalErros()}";
            }
        }

        private void fetchScanEngineLogMessages()
        {
            var messages = scanEngine.fetchLogMessages();
            foreach (var message in messages)
            {
                addTextLog(message);
            }
        }

        // fetch new woriking ips and add to the list view while scanning
        private void fetchWorkingIPResults()
        {
            List<ResultItem> scanResults = scanEngine.progressInfo.scanResults.fetchWorkingIPs();
            addResulItemsToListView(scanResults);
        }

        private void loadLastResultsComboList()
        {
            comboResults.Items.Clear();
            comboResults.Items.Add("Current Scan Results");
            var resultFiles = Directory.GetFiles("results/", "*.json");
            foreach (var resultFile in resultFiles)
            {
                comboResults.Items.Add(resultFile);
            }
            comboResults.SelectedIndex = 0;
        }

        private void comboResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? filename = getSelectedScanResultFilename();
            if (filename != null)
            {
                fillResultsListView(filename);
            }
        }

        // get filename of selected scan result
        private string? getSelectedScanResultFilename()
        {
            string? filename = comboResults.SelectedItem.ToString();
            if (filename != null && File.Exists(filename) && filename != "Current Scan Results")
            {
                return filename;
            }

            return null;
        }

        // load previous results into list view
        private void fillResultsListView(string resultsFileName, bool plainFile = false)
        {
            var results = new ScanResults(resultsFileName);
            bool isLoaded = plainFile ? results.loadPlain() : results.load();
            if (isLoaded)
            {
                listResults.Items.Clear();
                var loadedResults = results.getLoadedInstance();
                this.currentScanResults = loadedResults.workingIPs;
                addResulItemsToListView(currentScanResults);

                addTextLog($"'{resultsFileName}' loaded with {loadedResults.totalFoundWorkingIPs:n0} working IPs, scan time: {loadedResults.startDate}");
            }
            else
            {
                addTextLog($"Could not load scan result file from disk: '{resultsFileName}'");
            }
        }

        private void addResulItemsToListView(List<ResultItem>? workingIPs)
        {
            if (workingIPs != null)
            {
                int index = 0;
                listResults.BeginUpdate();
                listResults.ListViewItemSorter = null;
                foreach (ResultItem resultItem in workingIPs)
                {
                    index++;
                    listResults.Items.Add(new ListViewItem(new string[] { resultItem.delay.ToString(), resultItem.ip }));
                }
                listResults.EndUpdate();
                listResults.ListViewItemSorter = listResultsColumnSorter;
                lblPrevListTotalIPs.Text = $"{listResults.Items.Count:n0} IPs";
            }
        }


        private void oneTimeChecks()
        {
            if (!oneTimeChecked && isAppCongigValid)
            {

                // check if client config file is exists and update
                if (configManager.getClientConfig() != null && configManager.getClientConfig().isClientConfigOld())
                {
                    remoteUpdateClientConfig();
                }

                //Load cf ip ranges
                loadCFIPListView();

                // check fronting domain
                Task.Factory.StartNew(() =>
                {
                    var checker = new CheckIPWorking();
                    if (!checker.checkFronting(false, 5))
                    {
                        addTextLog($"Fronting domain is not accessible! you might need to get new fronting url from our github or check your internet connection.");
                    }
                });

                // check for updates
                if (appUpdateChecker.shouldCheck())
                {
                    checkForUpdate();
                }

                oneTimeChecked = true;
            }
        }

        // update clinet config and cf ip list
        private bool remoteUpdateClientConfig()
        {
            addTextLog("Updating client config from remote...");
            bool result = configManager.getClientConfig().remoteUpdateClientConfig();
            if (result)
            {
                addTextLog("Client config and Cloudflare subnets are successfully updated.");
                if (!configManager.getClientConfig().isConfigValid())
                {
                    addTextLog("'client config' data is not valid!");
                }

                // reload cf subnet list
                scanEngine.loadCFIPList();
            }
            else
            {
                addTextLog("Failed to update client config. check your internet connection or maybe client config url is blocked by your ISP!");
            }

            return result;
        }

        // check for update
        private void checkForUpdate(bool logNoNewVersion = false)
        {
            Task.Factory.StartNew(() => { appUpdateChecker.check(); })
            .ContinueWith(done =>
            {
                if (appUpdateChecker.isFoundNewVersion())
     