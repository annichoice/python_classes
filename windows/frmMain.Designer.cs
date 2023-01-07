
﻿namespace WinCFScan
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            txtLog = new TextBox();
            timerBase = new System.Windows.Forms.Timer(components);
            labelLastIPChecked = new Label();
            lblLastIPRange = new Label();
            timerProgress = new System.Windows.Forms.Timer(components);
            groupBox1 = new GroupBox();
            lblTempInfo = new Label();
            toolStrip1 = new ToolStrip();
            btnStart = new ToolStripSplitButton();
            mnuPauseScan = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            comboConcurrent = new ToolStripComboBox();
            lblConcurrent = new ToolStripLabel();
            comboTargetSpeed = new ToolStripComboBox();
            lblTargetSpeed = new ToolStripLabel();
            comboConfigs = new ToolStripComboBox();
            toolStripLabel3 = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            prgOveral = new ToolStripProgressBar();
            toolStripLabel1 = new ToolStripLabel();
            btnSkipCurRange = new ToolStripSplitButton();
            mnuSkipAfterFoundIPs = new ToolStripMenuItem();
            mnuSkipAfterAWhile = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            mnuSkipAfter10Percent = new ToolStripMenuItem();
            mnuSkipAfter30Percent = new ToolStripMenuItem();
            mnuSkipAfter50Percent = new ToolStripMenuItem();
            prgCurRange = new ToolStripProgressBar();
            toolStripLabel2 = new ToolStripLabel();
            lblDebugMode = new Label();
            btnCopyFastestIP = new Button();
            txtFastestIP = new TextBox();
            lblTotalWorkingIPs = new Label();
            linkGithub = new ToolStripLabel();
            toolTip1 = new ToolTip(components);
            comboResults = new ComboBox();
            btnScanInPrevResults = new Button();
            btnLoadIPRanges = new Button();
            listResults = new ListView();
            hdrDelay = new ColumnHeader();
            hdrIP = new ColumnHeader();
            mnuListView = new ContextMenuStrip(components);
            mnuListViewCopyIP = new ToolStripMenuItem();
            mnuTestThisIP = new ToolStripMenuItem();
            mnuListViewTestThisIPAddress = new ToolStripMenuItem();
            mnuTesIP2Times = new ToolStripMenuItem();
            mnuTesIP3Times = new ToolStripMenuItem();
            mnuTesIP5Times = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPageCFRanges = new TabPage();
            listCFIPList = new ListView();
            headIPRange = new ColumnHeader();
            headTotalIPs = new ColumnHeader();
            lblCFIPListStatus = new Label();
            btnSelectNoneIPRanges = new Button();
            btnSelectAllIPRanges = new Button();
            tabPageResults = new TabPage();
            btnStopAvgTest = new Button();
            lblPrevListTotalIPs = new Label();
            lblPrevResults = new Label();
            btnResultsActions = new Button();
            mnuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadCustomIPRangesToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            importScanResultsToolStripMenuItem = new ToolStripMenuItem();
            exportScanResultsToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            scanASingleIPAddressToolStripMenuItem = new ToolStripMenuItem();
            addCustomV2rayConfigToolStripMenuItem = new ToolStripMenuItem();
            downloadTimeoutToolStripMenuItem = new ToolStripMenuItem();
            comboDownloadTimeout = new ToolStripComboBox();
            toolStripSeparator5 = new ToolStripSeparator();
            checkForUpdateToolStripMenuItem = new ToolStripMenuItem();
            updateClientConfigCloudflareSubnetsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            mnuHelpCustomConfig = new ToolStripMenuItem();
            mnuHelpOurGitHub = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            mnuResultsActions = new ContextMenuStrip(components);
            exportResultsToolStripMenuItem = new ToolStripMenuItem();
            importResultsToolStripMenuItem = new ToolStripMenuItem();
            deleteResultsToolStripMenuItem = new ToolStripMenuItem();
            toolStripBottom = new ToolStrip();
            btnFrontingErrors = new ToolStripSplitButton();
            mnuCopyFrontingErrors = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            btnDownloadErrors = new ToolStripSplitButton();
            mnuCopyDownloadErrors = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            lblAutoSkipStatus = new ToolStripLabel();
            seperatorAutoSkip = new ToolStripSeparator();
            lblRunningWorkers = new ToolStripLabel();
            linkBuyMeCoffee = new ToolStripLabel();
            groupBox1.SuspendLayout();
            toolStrip1.SuspendLayout();
            mnuListView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageCFRanges.SuspendLayout();
            tabPageResults.SuspendLayout();
            mnuMain.SuspendLayout();
            mnuResultsActions.SuspendLayout();
            toolStripBottom.SuspendLayout();
            SuspendLayout();
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.FromArgb(21, 23, 24);
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtLog.ForeColor = Color.PaleTurquoise;
            txtLog.Location = new Point(0, 0);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(852, 182);
            txtLog.TabIndex = 1;
            txtLog.Text = "Welcome to Cloudflare IP Scanner.\r\n";
            // 
            // timerBase
            // 
            timerBase.Enabled = true;
            timerBase.Interval = 1500;
            timerBase.Tick += timerBase_Tick;
            // 
            // labelLastIPChecked
            // 
            labelLastIPChecked.AutoSize = true;
            labelLastIPChecked.Location = new Point(11, 55);
            labelLastIPChecked.Name = "labelLastIPChecked";
            labelLastIPChecked.Size = new Size(91, 15);
            labelLastIPChecked.TabIndex = 1;
            labelLastIPChecked.Text = "Last checked IP:";
            // 
            // lblLastIPRange
            // 
            lblLastIPRange.AutoSize = true;
            lblLastIPRange.Location = new Point(11, 76);
            lblLastIPRange.Name = "lblLastIPRange";
            lblLastIPRange.Size = new Size(99, 15);
            lblLastIPRange.TabIndex = 0;
            lblLastIPRange.Text = "Current IP Range:";
            // 
            // timerProgress
            // 
            timerProgress.Interval = 300;
            timerProgress.Tick += timerProgress_Tick;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(lblTempInfo);
            groupBox1.Controls.Add(toolStrip1);
            groupBox1.Controls.Add(lblDebugMode);
            groupBox1.Controls.Add(btnCopyFastestIP);
            groupBox1.Controls.Add(txtFastestIP);
            groupBox1.Controls.Add(lblTotalWorkingIPs);
            groupBox1.Controls.Add(labelLastIPChecked);
            groupBox1.Controls.Add(lblLastIPRange);
            groupBox1.Location = new Point(12, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 0, 3, 3);
            groupBox1.Size = new Size(852, 120);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // lblTempInfo
            // 
            lblTempInfo.BackColor = Color.LightGray;
            lblTempInfo.ForeColor = SystemColors.ControlText;
            lblTempInfo.Location = new Point(267, 57);
            lblTempInfo.Name = "lblTempInfo";
            lblTempInfo.Size = new Size(201, 63);
            lblTempInfo.TabIndex = 17;
            lblTempInfo.Text = "bebug";
            lblTempInfo.Visible = false;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnStart, toolStripSeparator2, comboConcurrent, lblConcurrent, comboTargetSpeed, lblTargetSpeed, comboConfigs, toolStripLabel3, toolStripSeparator1, prgOveral, toolStripLabel1, btnSkipCurRange, prgCurRange, toolStripLabel2 });
            toolStrip1.Location = new Point(3, 16);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RightToLeft = RightToLeft.Yes;
            toolStrip1.Size = new Size(846, 33);
            toolStrip1.TabIndex = 16;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnStart
            // 
            btnStart.AutoSize = false;
            btnStart.BackColor = SystemColors.Control;
            btnStart.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnStart.DropDownItems.AddRange(new ToolStripItem[] { mnuPauseScan });
            btnStart.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = SystemColors.ControlText;
            btnStart.Image = (Image)resources.GetObject("btnStart.Image");
            btnStart.ImageTransparentColor = Color.Magenta;
            btnStart.Name = "btnStart";
            btnStart.RightToLeft = RightToLeft.No;
            btnStart.Size = new Size(95, 30);
            btnStart.Text = "Start Scan";
            btnStart.ToolTipText = "Scan in selected IP ranges of Cloudflare";
            btnStart.ButtonClick += btnStart_ButtonClick;
            btnStart.Click += btnStart_Click;
            // 
            // mnuPauseScan
            // 
            mnuPauseScan.Name = "mnuPauseScan";
            mnuPauseScan.Size = new Size(134, 22);
            mnuPauseScan.Text = "Pause Scan";
            mnuPauseScan.Visible = false;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 33);
            // 
            // comboConcurrent
            // 
            comboConcurrent.AutoSize = false;
            comboConcurrent.DropDownWidth = 50;
            comboConcurrent.FlatStyle = FlatStyle.System;
            comboConcurrent.Items.AddRange(new object[] { "1", "2", "4", "8", "16", "32" });
            comboConcurrent.Name = "comboConcurrent";
            comboConcurrent.RightToLeft = RightToLeft.No;
            comboConcurrent.Size = new Size(50, 23);
            comboConcurrent.Text = "4";
            comboConcurrent.ToolTipText = "Number of parallel scan processes";
            comboConcurrent.TextChanged += comboConcurrent_TextChanged;
            // 
            // lblConcurrent
            // 
            lblConcurrent.Name = "lblConcurrent";
            lblConcurrent.Size = new Size(67, 30);
            lblConcurrent.Text = "Concurrent";
            // 
            // comboTargetSpeed
            // 
            comboTargetSpeed.AutoSize = false;
            comboTargetSpeed.DropDownStyle = ComboBoxStyle.DropDownList;
            comboTargetSpeed.FlatStyle = FlatStyle.System;
            comboTargetSpeed.Items.AddRange(new object[] { "20 KB/s", "50 KB/s", "100 KB/s", "200 KB/s", "500 KB/s" });
            comboTargetSpeed.Name = "comboTargetSpeed";
            comboTargetSpeed.RightToLeft = RightToLeft.No;
            comboTargetSpeed.Size = new Size(70, 23);
            comboTargetSpeed.ToolTipText = "Target Speed";
            // 
            // lblTargetSpeed
            // 
            lblTargetSpeed.Name = "lblTargetSpeed";
            lblTargetSpeed.Size = new Size(74, 30);
            lblTargetSpeed.Text = "Target Speed";
            // 
            // comboConfigs
            // 
            comboConfigs.AutoSize = false;
            comboConfigs.DropDownStyle = ComboBoxStyle.DropDownList;
            comboConfigs.DropDownWidth = 130;
            comboConfigs.FlatStyle = FlatStyle.System;
            comboConfigs.Items.AddRange(new object[] { "Default" });
            comboConfigs.Name = "comboConfigs";
            comboConfigs.RightToLeft = RightToLeft.No;
            comboConfigs.Size = new Size(90, 23);
            comboConfigs.ToolTipText = "v2ray configs";
            comboConfigs.SelectedIndexChanged += comboConfigs_SelectedIndexChanged;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(43, 30);
            toolStripLabel3.Text = "Config";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 33);
            // 
            // prgOveral
            // 
            prgOveral.AutoSize = false;
            prgOveral.Name = "prgOveral";
            prgOveral.Size = new Size(90, 25);
            prgOveral.ToolTipText = "Overal progress";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(41, 30);
            toolStripLabel1.Text = "Overal";
            // 
            // btnSkipCurRange
            // 
            btnSkipCurRange.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSkipCurRange.DropDownItems.AddRange(new ToolStripItem[] { mnuSkipAfterFoundIPs, mnuSkipAfterAWhile, toolStripSeparator6, mnuSkipAfter10Percent, mnuSkipAfter30Percent, mnuSkipAfter50Percent });
            btnSkipCurRange.Image = (Image)resources.GetObject("btnSkipCurRange.Image");
            btnSkipCurRange.ImageTransparentColor = Color.Magenta;
            btnSkipCurRange.Margin = new Padding(2, 1, 0, 2);
            btnSkipCurRange.Name = "btnSkipCurRange";
            btnSkipCurRange.Padding = new Padding(2, 0, 0, 0);
            btnSkipCurRange.RightToLeft = RightToLeft.No;
            btnSkipCurRange.Size = new Size(47, 30);
            btnSkipCurRange.Text = "Skip";
            btnSkipCurRange.ToolTipText = "Skip curent IP range";
            btnSkipCurRange.ButtonClick += btnSkipCurRange_ButtonClick;
            // 
            // mnuSkipAfterFoundIPs
            // 
            mnuSkipAfterFoundIPs.CheckOnClick = true;
            mnuSkipAfterFoundIPs.Name = "mnuSkipAfterFoundIPs";
            mnuSkipAfterFoundIPs.Size = new Size(287, 22);
            mnuSkipAfterFoundIPs.Text = "Auto skip after found 5 working IPs";
            mnuSkipAfterFoundIPs.Click += mnuSkipAfterFoundIPs_Click;
            // 
            // mnuSkipAfterAWhile
            // 
            mnuSkipAfterAWhile.CheckOnClick = true;
            mnuSkipAfterAWhile.Name = "mnuSkipAfterAWhile";
            mnuSkipAfterAWhile.Size = new Size(287, 22);
            mnuSkipAfterAWhile.Text = "Auto skip after 3 minutes of scanning";
            mnuSkipAfterAWhile.Click += mnuSkipAfterAWhile_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(284, 6);
            // 
            // mnuSkipAfter10Percent
            // 
            mnuSkipAfter10Percent.CheckOnClick = true;
            mnuSkipAfter10Percent.Name = "mnuSkipAfter10Percent";
            mnuSkipAfter10Percent.Size = new Size(287, 22);
            mnuSkipAfter10Percent.Tag = "10";
            mnuSkipAfter10Percent.Text = "Auto skip after scanning 10% of IP range";
            mnuSkipAfter10Percent.Click += mnuSkipAfter10Percent_Click;
            // 
            // mnuSkipAfter30Percent
            // 
            mnuSkipAfter30Percent.CheckOnClick = true;
            mnuSkipAfter30Percent.Name = "mnuSkipAfter30Percent";
            mnuSkipAfter30Percent.Size = new Size(287, 22);
            mnuSkipAfter30Percent.Tag = "30";
            mnuSkipAfter30Percent.Text = "Auto skip after scanning 30% of IP range";
            mnuSkipAfter30Percent.Click += mnuSkipAfter30Percent_Click;
            // 
            // mnuSkipAfter50Percent
            // 
            mnuSkipAfter50Percent.CheckOnClick = true;
            mnuSkipAfter50Percent.Name = "mnuSkipAfter50Percent";
            mnuSkipAfter50Percent.Size = new Size(287, 22);
            mnuSkipAfter50Percent.Tag = "50";
            mnuSkipAfter50Percent.Text = "Auto skip after scanning 50% of IP range";
            mnuSkipAfter50Percent.Click += mnuSkipAfter50Percent_Click;
            // 
            // prgCurRange
            // 
            prgCurRange.AutoSize = false;
            prgCurRange.Name = "prgCurRange";
            prgCurRange.Size = new Size(69, 19);
            prgCurRange.ToolTipText = "Current IP range progress";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(47, 30);
            toolStripLabel2.Text = "Current";
            // 
            // lblDebugMode
            // 
            lblDebugMode.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDebugMode.AutoSize = true;
            lblDebugMode.BackColor = SystemColors.Control;
            lblDebugMode.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDebugMode.ForeColor = Color.Red;
            lblDebugMode.Location = new Point(699, 94);
            lblDebugMode.Name = "lblDebugMode";
            lblDebugMode.Size = new Size(143, 15);
            lblDebugMode.TabIndex = 13;
            lblDebugMode.Text = "DEBUG MODE is Enabled";
            toolTip1.SetToolTip(lblDebugMode, "To exit debug mode delete 'enable-debug' file in the app directory.\r\nIn debug mode you can not set concurrent processes.");
            lblDebugMode.Visible = false;
            // 
            // btnCopyFastestIP
            // 
            btnCopyFastestIP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyFastestIP.Location = new Point(695, 55);
            btnCopyFastestIP.Name = "btnCopyFastestIP";
            btnCopyFastestIP.Size = new Size(151, 25);
            btnCopyFastestIP.TabIndex = 10;
            btnCopyFastestIP.Text = "Copy fastest IP";
            btnCopyFastestIP.UseVisualStyleBackColor = true;
            btnCopyFastestIP.Click += btnCopyFastestIP_Click;
            // 
            // txtFastestIP
            // 
            txtFastestIP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFastestIP.BackColor = Color.White;
            txtFastestIP.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtFastestIP.ForeColor = Color.Green;
            txtFastestIP.Location = new Point(486, 57);
            txtFastestIP.Name = "txtFastestIP";
            txtFastestIP.PlaceholderText = "Fastest IP";
            txtFastestIP.ReadOnly = true;
            txtFastestIP.Size = new Size(203, 23);
            txtFastestIP.TabIndex = 11;
            // 
            // lblTotalWorkingIPs
            // 
            lblTotalWorkingIPs.AutoSize = true;
            lblTotalWorkingIPs.Location = new Point(11, 97);
            lblTotalWorkingIPs.Name = "lblTotalWorkingIPs";
            lblTotalWorkingIPs.Size = new Size(143, 15);
            lblTotalWorkingIPs.TabIndex = 6;
            lblTotalWorkingIPs.Text = "Total working IPs found: 0";
            // 
            // linkGithub
            // 
            linkGithub.Alignment = ToolStripItemAlignment.Right;
            linkGithub.AutoSize = false;
            linkGithub.Image = Properties.Resources.github_mark24;
            linkGithub.ImageAlign = ContentAlignment.MiddleLeft;
            linkGithub.ImageScaling = ToolStripItemImageScaling.None;
            linkGithub.IsLink = true;
            linkGithub.Margin = new Padding(0, 1, 5, 2);
            linkGithub.Name = "linkGithub";
            linkGithub.Size = new Size(71, 23);
            linkGithub.Text = "GitHub";
            linkGithub.TextAlign = ContentAlignment.MiddleRight;
            linkGithub.ToolTipText = "Visit us on the Github";
            linkGithub.Click += linkGithub_Click;
            // 
            // comboResults
            // 
            comboResults.DropDownStyle = ComboBoxStyle.DropDownList;
            comboResults.FormattingEnabled = true;
            comboResults.Location = new Point(140, 10);
            comboResults.Name = "comboResults";
            comboResults.Size = new Size(220, 23);
            comboResults.TabIndex = 5;
            toolTip1.SetToolTip(comboResults, "List of last scan results");
            comboResults.SelectedIndexChanged += comboResults_SelectedIndexChanged;
            // 
            // btnScanInPrevResults
            // 
            btnScanInPrevResults.Location = new Point(419, 10);
            btnScanInPrevResults.Name = "btnScanInPrevResults";
            btnScanInPrevResults.Size = new Size(115, 24);
            btnScanInPrevResults.TabIndex = 6;
            btnScanInPrevResults.Text = "Scan in results";
            toolTip1.SetToolTip(btnScanInPrevResults, "Scan again in this ip results");
            btnScanInPrevResults.UseVisualStyleBackColor = true;
            btnScanInPrevResults.Click += btnScanInPrevResults_Click;
            // 
            // btnLoadIPRanges
            // 
            btnLoadIPRanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoadIPRanges.Location = new Point(542, 8);
            btnLoadIPRanges.Name = "btnLoadIPRanges";
            btnLoadIPRanges.Size = new Size(104, 23);
            btnLoadIPRanges.TabIndex = 4;
            btnLoadIPRanges.Text = "Load IP ranges";
            toolTip1.SetToolTip(btnLoadIPRanges, "Load your custom IP ranges");
            btnLoadIPRanges.UseVisualStyleBackColor = true;
            btnLoadIPRanges.Click += btnLoadIPRanges_Click;
            // 
            // listResults
            // 
            listResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listResults.Columns.AddRange(new ColumnHeader[] { hdrDelay, hdrIP });
            listResults.FullRowSelect = true;
            listResults.GridLines = true;
            listResults.Location = new Point(0, 40);
            listResults.Name = "listResults";
            listResults.Size = new Size(844, 220);
            listResults.TabIndex = 4;
            listResults.UseCompatibleStateImageBehavior = false;
            listResults.View = View.Details;
            listResults.ColumnClick += listResults_ColumnClick;
            listResults.MouseClick += listResults_MouseClick;
            listResults.MouseDoubleClick += listResults_MouseDoubleClick;
            // 
            // hdrDelay
            // 
            hdrDelay.Text = "Delay";
            hdrDelay.Width = 90;
            // 
            // hdrIP
            // 
            hdrIP.Text = "IP Address";
            hdrIP.Width = 140;
            // 
            // mnuListView
            // 
            mnuListView.ImageScalingSize = new Size(20, 20);
            mnuListView.Items.AddRange(new ToolStripItem[] { mnuListViewCopyIP, mnuTestThisIP, mnuListViewTestThisIPAddress });
            mnuListView.Name = "mnuListView";
            mnuListView.Size = new Size(253, 70);
            // 
            // mnuListViewCopyIP
            // 
            mnuListViewCopyIP.Name = "mnuListViewCopyIP";
            mnuListViewCopyIP.Size = new Size(252, 22);
            mnuListViewCopyIP.Text = "Copy IP address";
            mnuListViewCopyIP.Click += mnuListViewCopyIP_Click;
            // 
            // mnuTestThisIP
            // 
            mnuTestThisIP.Name = "mnuTestThisIP";
            mnuTestThisIP.Size = new Size(252, 22);
            mnuTestThisIP.Text = "Test this IP address";
            mnuTestThisIP.Click += testThisIPAddressToolStripMenuItem_Click;
            // 
            // mnuListViewTestThisIPAddress
            // 
            mnuListViewTestThisIPAddress.DropDownItems.AddRange(new ToolStripItem[] { mnuTesIP2Times, mnuTesIP3Times, mnuTesIP5Times });
            mnuListViewTestThisIPAddress.Name = "mnuListViewTestThisIPAddress";
            mnuListViewTestThisIPAddress.Size = new Size(252, 22);
            mnuListViewTestThisIPAddress.Text = "Average test selected IP addresses";
            // 
            // mnuTesIP2Times
            // 
            mnuTesIP2Times.Name = "mnuTesIP2Times";
            mnuTesIP2Times.Size = new Size(135, 22);
            mnuTesIP2Times.Text = "Test 2 times";
            mnuTesIP2Times.Click += mnuTesIP2Times_Click;
            // 
            // mnuTesIP3Times
            // 
            mnuTesIP3Times.Name = "mnuTesIP3Times";
            mnuTesIP3Times.Size = new Size(135, 22);
            mnuTesIP3Times.Text = "Test 3 times";
            mnuTesIP3Times.Click += mnuTesIP3Times_Click;
            // 
            // mnuTesIP5Times
            // 
            mnuTesIP5Times.Name = "mnuTesIP5Times";
            mnuTesIP5Times.Size = new Size(135, 22);
            mnuTesIP5Times.Text = "Test 5 times";
            mnuTesIP5Times.Click += mnuTesIP5Times_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 147);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(txtLog);
            splitContainer1.Size = new Size(852, 480);
            splitContainer1.SplitterDistance = 294;
            splitContainer1.TabIndex = 7;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageCFRanges);
            tabControl1.Controls.Add(tabPageResults);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(852, 294);