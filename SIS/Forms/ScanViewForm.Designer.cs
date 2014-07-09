namespace SIS.Forms
{
    using global::SIS.Library.ImageControl;
    using global::SIS.WPFControls.CCDControl;

    using SIS.Controls;

    partial class ScanViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanViewForm));
			KUL.MDS.Validation.ValidationRule validationRule1 = new KUL.MDS.Validation.ValidationRule();
			KUL.MDS.Validation.ValidationRule validationRule2 = new KUL.MDS.Validation.ValidationRule();
			KUL.MDS.Validation.ValidationRule validationRule3 = new KUL.MDS.Validation.ValidationRule();
			KUL.MDS.Validation.ValidationRule validationRule4 = new KUL.MDS.Validation.ValidationRule();
			this.valprovSISValidationProvider = new KUL.MDS.Validation.ValidationProvider(this.components);
			this.txtboxTimeGatingMinAPD2 = new System.Windows.Forms.TextBox();
			this.txtboxTimeGatingMaxAPD2 = new System.Windows.Forms.TextBox();
			this.txtboxTimeGatingMinAPD1 = new System.Windows.Forms.TextBox();
			this.txtboxTimeGatingMaxAPD1 = new System.Windows.Forms.TextBox();
			this.bckgwrkPerformScan = new System.ComponentModel.BackgroundWorker();
			this.bckgwrkPerformMove = new System.ComponentModel.BackgroundWorker();
			this.m_TabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.btnNoGateAPD2 = new System.Windows.Forms.Button();
			this.btnApplyGateAPD2 = new System.Windows.Forms.Button();
			this.btnNoGateAPD1 = new System.Windows.Forms.Button();
			this.btnApplyGateAPD1 = new System.Windows.Forms.Button();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.m_checkbxApplyTimeGatingWhileScanning = new System.Windows.Forms.CheckBox();
			this.lblTimeGating = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtbxCurrZPos = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.scanModeComboBox1 = new ScanModeComboBox();
			this.btnScanStart = new System.Windows.Forms.Button();
			this.btnAPDOnOff = new System.Windows.Forms.Button();
			this.txtbxCurrYPos = new System.Windows.Forms.TextBox();
			this.txtbxCurrXPos = new System.Windows.Forms.TextBox();
			this.btnStageOnOff = new System.Windows.Forms.Button();
			this.lblStageCurrYPos = new System.Windows.Forms.Label();
			this.lblStageCurrXPos = new System.Windows.Forms.Label();
			this.btnScanStop = new System.Windows.Forms.Button();
			this.btnValidateInput = new System.Windows.Forms.Button();
			this.btnScanSettings = new System.Windows.Forms.Button();
			this.m_checkbxContinuousScan = new System.Windows.Forms.CheckBox();
			this.m_checkbxAutoIncrement = new System.Windows.Forms.CheckBox();
			this.btnAPDCountRate = new System.Windows.Forms.Button();
			this.m_checkbxBidirScan = new System.Windows.Forms.CheckBox();
			this.m_checkbxSaveTTTRData = new System.Windows.Forms.CheckBox();
			this.m_checkbxResend = new System.Windows.Forms.CheckBox();
			this.m_nupdFilenameCount = new System.Windows.Forms.NumericUpDown();
			this.m_chkbxAutosave = new System.Windows.Forms.CheckBox();
			this.m_checkbxDMA = new System.Windows.Forms.CheckBox();
			this.m_txtbxGoToZ = new System.Windows.Forms.TextBox();
			this.m_txtbxGoToY = new System.Windows.Forms.TextBox();
			this.m_txtbxGoToX = new System.Windows.Forms.TextBox();
			this.m_lblGoToZ = new System.Windows.Forms.Label();
			this.m_lblGoToY = new System.Windows.Forms.Label();
			this.m_lblGoToX = new System.Windows.Forms.Label();
			this.btnMoveAbs = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.lblColorBarMaxInt1 = new System.Windows.Forms.Label();
			this.chkbxNormalized = new System.Windows.Forms.CheckBox();
			this.lblColorBarMinInt1 = new System.Windows.Forms.Label();
			this.chkbxCorrectedImage = new System.Windows.Forms.CheckBox();
			this.scanImageControl1 = new ImageControl();
			this.scanImageControl2 = new ImageControl();
			this.drwcnvColorBar2 = new DrawCanvas();
			this.drwcnvColorBar1 = new DrawCanvas();
			this.lblColorBarMaxInt2 = new System.Windows.Forms.Label();
			this.lblColorBarMinInt2 = new System.Windows.Forms.Label();
			this.btnImageFit = new System.Windows.Forms.Button();
			this.buttonExp = new System.Windows.Forms.Button();
			this.btnLoadPreviousFile = new System.Windows.Forms.Button();
			this.btnLoadNextFile = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.m_txtbxScanPropertiesFromFile = new System.Windows.Forms.TextBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
			this.ccdControl1 = new CCDControl();
			this.m_TabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_nupdFilenameCount)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// valprovSISValidationProvider
			// 
			this.valprovSISValidationProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("valprovSISValidationProvider.Icon")));
			// 
			// txtboxTimeGatingMinAPD2
			// 
			this.txtboxTimeGatingMinAPD2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtboxTimeGatingMinAPD2.Location = new System.Drawing.Point(139, 171);
			this.txtboxTimeGatingMinAPD2.Name = "txtboxTimeGatingMinAPD2";
			this.txtboxTimeGatingMinAPD2.Size = new System.Drawing.Size(50, 20);
			this.txtboxTimeGatingMinAPD2.TabIndex = 61;
			this.txtboxTimeGatingMinAPD2.Text = "0";
			this.txtboxTimeGatingMinAPD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			validationRule1.DataType = KUL.MDS.Validation.ValidationDataType.Double;
			validationRule1.MaximumValue = "1000";
			validationRule1.MinimumValue = "0";
			this.valprovSISValidationProvider.SetValidationRule(this.txtboxTimeGatingMinAPD2, validationRule1);
			// 
			// txtboxTimeGatingMaxAPD2
			// 
			this.txtboxTimeGatingMaxAPD2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtboxTimeGatingMaxAPD2.Location = new System.Drawing.Point(204, 171);
			this.txtboxTimeGatingMaxAPD2.Name = "txtboxTimeGatingMaxAPD2";
			this.txtboxTimeGatingMaxAPD2.Size = new System.Drawing.Size(50, 20);
			this.txtboxTimeGatingMaxAPD2.TabIndex = 62;
			this.txtboxTimeGatingMaxAPD2.Text = "50";
			this.txtboxTimeGatingMaxAPD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			validationRule2.DataType = KUL.MDS.Validation.ValidationDataType.Double;
			validationRule2.MaximumValue = "1000";
			validationRule2.MinimumValue = "0";
			this.valprovSISValidationProvider.SetValidationRule(this.txtboxTimeGatingMaxAPD2, validationRule2);
			// 
			// txtboxTimeGatingMinAPD1
			// 
			this.txtboxTimeGatingMinAPD1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtboxTimeGatingMinAPD1.Location = new System.Drawing.Point(139, 67);
			this.txtboxTimeGatingMinAPD1.Name = "txtboxTimeGatingMinAPD1";
			this.txtboxTimeGatingMinAPD1.Size = new System.Drawing.Size(50, 20);
			this.txtboxTimeGatingMinAPD1.TabIndex = 54;
			this.txtboxTimeGatingMinAPD1.Text = "0";
			this.txtboxTimeGatingMinAPD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			validationRule3.DataType = KUL.MDS.Validation.ValidationDataType.Double;
			validationRule3.MaximumValue = "1000";
			validationRule3.MinimumValue = "0";
			this.valprovSISValidationProvider.SetValidationRule(this.txtboxTimeGatingMinAPD1, validationRule3);
			// 
			// txtboxTimeGatingMaxAPD1
			// 
			this.txtboxTimeGatingMaxAPD1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtboxTimeGatingMaxAPD1.Location = new System.Drawing.Point(204, 67);
			this.txtboxTimeGatingMaxAPD1.Name = "txtboxTimeGatingMaxAPD1";
			this.txtboxTimeGatingMaxAPD1.Size = new System.Drawing.Size(50, 20);
			this.txtboxTimeGatingMaxAPD1.TabIndex = 55;
			this.txtboxTimeGatingMaxAPD1.Text = "50";
			this.txtboxTimeGatingMaxAPD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			validationRule4.DataType = KUL.MDS.Validation.ValidationDataType.Double;
			validationRule4.MaximumValue = "1000";
			validationRule4.MinimumValue = "0";
			this.valprovSISValidationProvider.SetValidationRule(this.txtboxTimeGatingMaxAPD1, validationRule4);
			// 
			// bckgwrkPerformScan
			// 
			this.bckgwrkPerformScan.WorkerSupportsCancellation = true;
			this.bckgwrkPerformScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgwrkPerformScan_DoWork);
			this.bckgwrkPerformScan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgwrkPerformScan_RunWorkerCompleted);
			// 
			// bckgwrkPerformMove
			// 
			this.bckgwrkPerformMove.WorkerReportsProgress = true;
			this.bckgwrkPerformMove.WorkerSupportsCancellation = true;
			this.bckgwrkPerformMove.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgwrkPerformMove_DoWork);
			// 
			// m_TabControl
			// 
			this.m_TabControl.Controls.Add(this.tabPage1);
			this.m_TabControl.Controls.Add(this.tabPage3);
			this.m_TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_TabControl.Location = new System.Drawing.Point(0, 0);
			this.m_TabControl.Name = "m_TabControl";
			this.m_TabControl.SelectedIndex = 0;
			this.m_TabControl.Size = new System.Drawing.Size(1512, 897);
			this.m_TabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.Transparent;
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(1504, 871);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Scan View";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.btnNoGateAPD2);
			this.groupBox4.Controls.Add(this.btnApplyGateAPD2);
			this.groupBox4.Controls.Add(this.btnNoGateAPD1);
			this.groupBox4.Controls.Add(this.btnApplyGateAPD1);
			this.groupBox4.Controls.Add(this.label9);
			this.groupBox4.Controls.Add(this.label10);
			this.groupBox4.Controls.Add(this.label11);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.txtboxTimeGatingMinAPD2);
			this.groupBox4.Controls.Add(this.txtboxTimeGatingMaxAPD2);
			this.groupBox4.Controls.Add(this.label8);
			this.groupBox4.Controls.Add(this.label7);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.m_checkbxApplyTimeGatingWhileScanning);
			this.groupBox4.Controls.Add(this.lblTimeGating);
			this.groupBox4.Controls.Add(this.txtboxTimeGatingMinAPD1);
			this.groupBox4.Controls.Add(this.txtboxTimeGatingMaxAPD1);
			this.groupBox4.Location = new System.Drawing.Point(558, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(276, 296);
			this.groupBox4.TabIndex = 41;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Time Gating Window";
			// 
			// btnNoGateAPD2
			// 
			this.btnNoGateAPD2.Location = new System.Drawing.Point(30, 207);
			this.btnNoGateAPD2.Name = "btnNoGateAPD2";
			this.btnNoGateAPD2.Size = new System.Drawing.Size(100, 30);
			this.btnNoGateAPD2.TabIndex = 63;
			this.btnNoGateAPD2.TabStop = false;
			this.btnNoGateAPD2.Text = "No Gate";
			this.btnNoGateAPD2.UseVisualStyleBackColor = true;
			this.btnNoGateAPD2.Click += new System.EventHandler(this.btnNoGateAPD2_Click);
			// 
			// btnApplyGateAPD2
			// 
			this.btnApplyGateAPD2.Location = new System.Drawing.Point(140, 207);
			this.btnApplyGateAPD2.Name = "btnApplyGateAPD2";
			this.btnApplyGateAPD2.Size = new System.Drawing.Size(100, 30);
			this.btnApplyGateAPD2.TabIndex = 64;
			this.btnApplyGateAPD2.TabStop = false;
			this.btnApplyGateAPD2.Text = "Apply Gate";
			this.btnApplyGateAPD2.UseVisualStyleBackColor = true;
			this.btnApplyGateAPD2.Click += new System.EventHandler(this.btnApplyGateAPD2_Click);
			// 
			// btnNoGateAPD1
			// 
			this.btnNoGateAPD1.Location = new System.Drawing.Point(30, 106);
			this.btnNoGateAPD1.Name = "btnNoGateAPD1";
			this.btnNoGateAPD1.Size = new System.Drawing.Size(100, 30);
			this.btnNoGateAPD1.TabIndex = 56;
			this.btnNoGateAPD1.TabStop = false;
			this.btnNoGateAPD1.Text = "No Gate";
			this.btnNoGateAPD1.UseVisualStyleBackColor = true;
			this.btnNoGateAPD1.Click += new System.EventHandler(this.btnNoGateAPD1_Click);
			// 
			// btnApplyGateAPD1
			// 
			this.btnApplyGateAPD1.Location = new System.Drawing.Point(140, 106);
			this.btnApplyGateAPD1.Name = "btnApplyGateAPD1";
			this.btnApplyGateAPD1.Size = new System.Drawing.Size(100, 30);
			this.btnApplyGateAPD1.TabIndex = 57;
			this.btnApplyGateAPD1.TabStop = false;
			this.btnApplyGateAPD1.Text = "Apply Gate";
			this.btnApplyGateAPD1.UseVisualStyleBackColor = true;
			this.btnApplyGateAPD1.Click += new System.EventHandler(this.btnApplyGateAPD1_Click);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(212, 152);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(27, 13);
			this.label9.TabIndex = 59;
			this.label9.Text = "Max";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(150, 152);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(24, 13);
			this.label10.TabIndex = 58;
			this.label10.Text = "Min";
			// 
			// label11
			// 
			this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(191, 174);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(10, 13);
			this.label11.TabIndex = 62;
			this.label11.Text = "-";
			// 
			// label12
			// 
			this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(8, 174);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(122, 13);
			this.label12.TabIndex = 60;
			this.label12.Text = "Time Gating APD2* [ns]:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 273);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(177, 13);
			this.label8.TabIndex = 66;
			this.label8.Text = "gating time will be binned into pixels.";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(4, 257);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(262, 13);
			this.label7.TabIndex = 65;
			this.label7.Text = "*Note: only photons with arrival time between Min-Max";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(212, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(27, 13);
			this.label6.TabIndex = 52;
			this.label6.Text = "Max";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(150, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 13);
			this.label5.TabIndex = 51;
			this.label5.Text = "Min";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(191, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(10, 13);
			this.label4.TabIndex = 54;
			this.label4.Text = "-";
			// 
			// m_checkbxApplyTimeGatingWhileScanning
			// 
			this.m_checkbxApplyTimeGatingWhileScanning.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.m_checkbxApplyTimeGatingWhileScanning.AutoSize = true;
			this.m_checkbxApplyTimeGatingWhileScanning.Location = new System.Drawing.Point(10, 22);
			this.m_checkbxApplyTimeGatingWhileScanning.Name = "m_checkbxApplyTimeGatingWhileScanning";
			this.m_checkbxApplyTimeGatingWhileScanning.Size = new System.Drawing.Size(265, 17);
			this.m_checkbxApplyTimeGatingWhileScanning.TabIndex = 50;
			this.m_checkbxApplyTimeGatingWhileScanning.Text = "Apply Time Gating While Scanning (by APD1 gate)";
			this.m_checkbxApplyTimeGatingWhileScanning.UseVisualStyleBackColor = true;
			// 
			// lblTimeGating
			// 
			this.lblTimeGating.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblTimeGating.AutoSize = true;
			this.lblTimeGating.Location = new System.Drawing.Point(8, 70);
			this.lblTimeGating.Name = "lblTimeGating";
			this.lblTimeGating.Size = new System.Drawing.Size(122, 13);
			this.lblTimeGating.TabIndex = 53;
			this.lblTimeGating.Text = "Time Gating APD1* [ns]:";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.tableLayoutPanel1);
			this.groupBox3.Location = new System.Drawing.Point(0, 6);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(552, 290);
			this.groupBox3.TabIndex = 40;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Experiment Control";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Controls.Add(this.txtbxCurrZPos, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.scanModeComboBox1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnScanStart, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.btnAPDOnOff, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtbxCurrYPos, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtbxCurrXPos, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnStageOnOff, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblStageCurrYPos, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblStageCurrXPos, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnScanStop, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.btnValidateInput, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnScanSettings, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxContinuousScan, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxAutoIncrement, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.btnAPDCountRate, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxBidirScan, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxSaveTTTRData, 3, 7);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxResend, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.m_nupdFilenameCount, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this.m_chkbxAutosave, 3, 6);
			this.tableLayoutPanel1.Controls.Add(this.m_checkbxDMA, 4, 6);
			this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToZ, 4, 3);
			this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToY, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToX, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.m_lblGoToZ, 4, 2);
			this.tableLayoutPanel1.Controls.Add(this.m_lblGoToY, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.m_lblGoToX, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.btnMoveAbs, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 271);
			this.tableLayoutPanel1.TabIndex = 34;
			// 
			// txtbxCurrZPos
			// 
			this.txtbxCurrZPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxCurrZPos.Location = new System.Drawing.Point(439, 36);
			this.txtbxCurrZPos.Name = "txtbxCurrZPos";
			this.txtbxCurrZPos.ReadOnly = true;
			this.txtbxCurrZPos.Size = new System.Drawing.Size(104, 20);
			this.txtbxCurrZPos.TabIndex = 76;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label3.Location = new System.Drawing.Point(439, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 33);
			this.label3.TabIndex = 75;
			this.label3.Text = "Z Position (nm):";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// scanModeComboBox1
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.scanModeComboBox1, 2);
			this.scanModeComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scanModeComboBox1.FormattingEnabled = true;
			this.scanModeComboBox1.Location = new System.Drawing.Point(3, 69);
			this.scanModeComboBox1.Name = "scanModeComboBox1";
			this.scanModeComboBox1.Size = new System.Drawing.Size(212, 21);
			this.scanModeComboBox1.TabIndex = 60;
			// 
			// btnScanStart
			// 
			this.btnScanStart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnScanStart.Location = new System.Drawing.Point(3, 102);
			this.btnScanStart.Name = "btnScanStart";
			this.btnScanStart.Size = new System.Drawing.Size(103, 27);
			this.btnScanStart.TabIndex = 50;
			this.btnScanStart.Text = "Scan";
			this.btnScanStart.UseVisualStyleBackColor = true;
			this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
			// 
			// btnAPDOnOff
			// 
			this.btnAPDOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAPDOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAPDOnOff.ForeColor = System.Drawing.Color.Red;
			this.btnAPDOnOff.Location = new System.Drawing.Point(112, 36);
			this.btnAPDOnOff.Name = "btnAPDOnOff";
			this.btnAPDOnOff.Size = new System.Drawing.Size(103, 27);
			this.btnAPDOnOff.TabIndex = 58;
			this.btnAPDOnOff.Text = "APD: OFF";
			this.btnAPDOnOff.UseVisualStyleBackColor = true;
			this.btnAPDOnOff.Click += new System.EventHandler(this.btnAPDOnOff_Click);
			// 
			// txtbxCurrYPos
			// 
			this.txtbxCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxCurrYPos.Location = new System.Drawing.Point(330, 36);
			this.txtbxCurrYPos.Name = "txtbxCurrYPos";
			this.txtbxCurrYPos.ReadOnly = true;
			this.txtbxCurrYPos.Size = new System.Drawing.Size(103, 20);
			this.txtbxCurrYPos.TabIndex = 54;
			// 
			// txtbxCurrXPos
			// 
			this.txtbxCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtbxCurrXPos.Location = new System.Drawing.Point(221, 36);
			this.txtbxCurrXPos.Name = "txtbxCurrXPos";
			this.txtbxCurrXPos.ReadOnly = true;
			this.txtbxCurrXPos.Size = new System.Drawing.Size(103, 20);
			this.txtbxCurrXPos.TabIndex = 53;
			// 
			// btnStageOnOff
			// 
			this.btnStageOnOff.BackColor = System.Drawing.Color.Transparent;
			this.btnStageOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnStageOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStageOnOff.ForeColor = System.Drawing.Color.Red;
			this.btnStageOnOff.Location = new System.Drawing.Point(112, 3);
			this.btnStageOnOff.Name = "btnStageOnOff";
			this.btnStageOnOff.Size = new System.Drawing.Size(103, 27);
			this.btnStageOnOff.TabIndex = 56;
			this.btnStageOnOff.Text = "STAGE: OFF";
			this.btnStageOnOff.UseVisualStyleBackColor = false;
			this.btnStageOnOff.Click += new System.EventHandler(this.btnStageOnOff_Click);
			// 
			// lblStageCurrYPos
			// 
			this.lblStageCurrYPos.AutoSize = true;
			this.lblStageCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStageCurrYPos.Location = new System.Drawing.Point(221, 0);
			this.lblStageCurrYPos.Name = "lblStageCurrYPos";
			this.lblStageCurrYPos.Size = new System.Drawing.Size(103, 33);
			this.lblStageCurrYPos.TabIndex = 52;
			this.lblStageCurrYPos.Text = "X Position (nm):";
			this.lblStageCurrYPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblStageCurrXPos
			// 
			this.lblStageCurrXPos.AutoSize = true;
			this.lblStageCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblStageCurrXPos.Location = new System.Drawing.Point(330, 0);
			this.lblStageCurrXPos.Name = "lblStageCurrXPos";
			this.lblStageCurrXPos.Size = new System.Drawing.Size(103, 33);
			this.lblStageCurrXPos.TabIndex = 51;
			this.lblStageCurrXPos.Text = "Y Position (nm):";
			this.lblStageCurrXPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnScanStop
			// 
			this.btnScanStop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnScanStop.Location = new System.Drawing.Point(3, 135);
			this.btnScanStop.Name = "btnScanStop";
			this.btnScanStop.Size = new System.Drawing.Size(103, 27);
			this.btnScanStop.TabIndex = 57;
			this.btnScanStop.Text = "STOP";
			this.btnScanStop.UseVisualStyleBackColor = true;
			this.btnScanStop.Click += new System.EventHandler(this.btnScanStop_Click);
			// 
			// btnValidateInput
			// 
			this.btnValidateInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnValidateInput.Location = new System.Drawing.Point(3, 36);
			this.btnValidateInput.Name = "btnValidateInput";
			this.btnValidateInput.Size = new System.Drawing.Size(103, 27);
			this.btnValidateInput.TabIndex = 34;
			this.btnValidateInput.Text = "Validate";
			this.btnValidateInput.UseVisualStyleBackColor = true;
			// 
			// btnScanSettings
			// 
			this.btnScanSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnScanSettings.Location = new System.Drawing.Point(3, 3);
			this.btnScanSettings.Name = "btnScanSettings";
			this.btnScanSettings.Size = new System.Drawing.Size(103, 27);
			this.btnScanSettings.TabIndex = 61;
			this.btnScanSettings.Text = "Scan Settings";
			this.btnScanSettings.UseVisualStyleBackColor = true;
			this.btnScanSettings.Click += new System.EventHandler(this.btnScanSettings_Click);
			// 
			// m_checkbxContinuousScan
			// 
			this.m_checkbxContinuousScan.AutoSize = true;
			this.m_checkbxContinuousScan.Location = new System.Drawing.Point(3, 234);
			this.m_checkbxContinuousScan.Name = "m_checkbxContinuousScan";
			this.m_checkbxContinuousScan.Size = new System.Drawing.Size(85, 17);
			this.m_checkbxContinuousScan.TabIndex = 74;
			this.m_checkbxContinuousScan.Text = "Continuous?";
			this.m_checkbxContinuousScan.UseVisualStyleBackColor = true;
			// 
			// m_checkbxAutoIncrement
			// 
			this.m_checkbxAutoIncrement.AutoSize = true;
			this.m_checkbxAutoIncrement.Location = new System.Drawing.Point(3, 201);
			this.m_checkbxAutoIncrement.Name = "m_checkbxAutoIncrement";
			this.m_checkbxAutoIncrement.Size = new System.Drawing.Size(98, 17);
			this.m_checkbxAutoIncrement.TabIndex = 73;
			this.m_checkbxAutoIncrement.Text = "Auto-Increment";
			this.m_checkbxAutoIncrement.UseVisualStyleBackColor = true;
			// 
			// btnAPDCountRate
			// 
			this.btnAPDCountRate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnAPDCountRate.Location = new System.Drawing.Point(112, 135);
			this.btnAPDCountRate.Name = "btnAPDCountRate";
			this.btnAPDCountRate.Size = new System.Drawing.Size(103, 27);
			this.btnAPDCountRate.TabIndex = 80;
			this.btnAPDCountRate.Text = "Count Rate";
			this.btnAPDCountRate.UseVisualStyleBackColor = true;
			this.btnAPDCountRate.Click += new System.EventHandler(this.btnAPDCountRate_Click);
			// 
			// m_checkbxBidirScan
			// 
			this.m_checkbxBidirScan.AutoSize = true;
			this.m_checkbxBidirScan.Location = new System.Drawing.Point(112, 234);
			this.m_checkbxBidirScan.Name = "m_checkbxBidirScan";
			this.m_checkbxBidirScan.Size = new System.Drawing.Size(74, 17);
			this.m_checkbxBidirScan.TabIndex = 78;
			this.m_checkbxBidirScan.Text = "Bidir Scan";
			this.m_checkbxBidirScan.UseVisualStyleBackColor = true;
			// 
			// m_checkbxSaveTTTRData
			// 
			this.m_checkbxSaveTTTRData.AutoSize = true;
			this.m_checkbxSaveTTTRData.Checked = true;
			this.m_checkbxSaveTTTRData.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel1.SetColumnSpan(this.m_checkbxSaveTTTRData, 2);
			this.m_checkbxSaveTTTRData.Location = new System.Drawing.Point(330, 234);
			this.m_checkbxSaveTTTRData.Name = "m_checkbxSaveTTTRData";
			this.m_checkbxSaveTTTRData.Size = new System.Drawing.Size(179, 17);
			this.m_checkbxSaveTTTRData.TabIndex = 79;
			this.m_checkbxSaveTTTRData.Text = "Save raw Time Harp TTTR data";
			this.m_checkbxSaveTTTRData.UseVisualStyleBackColor = true;
			// 
			// m_checkbxResend
			// 
			this.m_checkbxResend.AutoSize = true;
			this.m_checkbxResend.Checked = true;
			this.m_checkbxResend.CheckState = System.Windows.Forms.CheckState.Checked;
			this.m_checkbxResend.Location = new System.Drawing.Point(112, 201);
			this.m_checkbxResend.Name = "m_checkbxResend";
			this.m_checkbxResend.Size = new System.Drawing.Size(68, 17);
			this.m_checkbxResend.TabIndex = 72;
			this.m_checkbxResend.Text = "Re-Send";
			this.m_checkbxResend.UseVisualStyleBackColor = true;
			// 
			// m_nupdFilenameCount
			// 
			this.m_nupdFilenameCount.Location = new System.Drawing.Point(221, 201);
			this.m_nupdFilenameCount.Name = "m_nupdFilenameCount";
			this.m_nupdFilenameCount.Size = new System.Drawing.Size(103, 20);
			this.m_nupdFilenameCount.TabIndex = 70;
			// 
			// m_chkbxAutosave
			// 
			this.m_chkbxAutosave.AutoSize = true;
			this.m_chkbxAutosave.Location = new System.Drawing.Point(330, 201);
			this.m_chkbxAutosave.Name = "m_chkbxAutosave";
			this.m_chkbxAutosave.Size = new System.Drawing.Size(74, 17);
			this.m_chkbxAutosave.TabIndex = 71;
			this.m_chkbxAutosave.Text = "Auto-save";
			this.m_chkbxAutosave.UseVisualStyleBackColor = true;
			// 
			// m_checkbxDMA
			// 
			this.m_checkbxDMA.AutoSize = true;
			this.m_checkbxDMA.Location = new System.Drawing.Point(439, 201);
			this.m_checkbxDMA.Name = "m_checkbxDMA";
			this.m_checkbxDMA.Size = new System.Drawing.Size(78, 17);
			this.m_checkbxDMA.TabIndex = 77;
			this.m_checkbxDMA.Text = "Use DMA?";
			this.m_checkbxDMA.UseVisualStyleBackColor = true;
			// 
			// m_txtbxGoToZ
			// 
			this.m_txtbxGoToZ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtbxGoToZ.Location = new System.Drawing.Point(439, 102);
			this.m_txtbxGoToZ.Name = "m_txtbxGoToZ";
			this.m_txtbxGoToZ.Size = new System.Drawing.Size(104, 20);
			this.m_txtbxGoToZ.TabIndex = 67;
			this.m_txtbxGoToZ.Text = "0";
			// 
			// m_txtbxGoToY
			// 
			this.m_txtbxGoToY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_txtbxGoToY.Location = new System.Drawing.Point(330, 102);
			this.m_txtbxGoToY.Name = "m_txtbxGoToY";
			this.m_txtbxGoToY.Size = new System.Drawing.Size(103, 20);
			this.m_txtbxGoToY.TabIndex = 66;
			this.m_txtbxGoToY.Text = "0";
			// 
			// m_txtbxGoToX
			// 
			this.m_txtbxGoToX.Location = new System.Drawing.Point(221, 102);
			this.m_txtbxGoToX.Name = "m_txtbxGoToX";
			this.m_txtbxGoToX.Size = new System.Drawing.Size(103, 20);
			this.m_txtbxGoToX.TabIndex = 65;
			this.m_txtbxGoToX.Text = "0";
			// 
			// m_lblGoToZ
			// 
			this.m_lblGoToZ.AutoSize = true;
			this.m_lblGoToZ.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lblGoToZ.Location = new System.Drawing.Point(439, 66);
			this.m_lblGoToZ.Name = "m_lblGoToZ";
			this.m_lblGoToZ.Size = new System.Drawing.Size(104, 33);
			this.m_lblGoToZ.TabIndex = 64;
			this.m_lblGoToZ.Text = "GotoZ";
			this.m_lblGoToZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_lblGoToY
			// 
			this.m_lblGoToY.AutoSize = true;
			this.m_lblGoToY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lblGoToY.Location = new System.Drawing.Point(330, 66);
			this.m_lblGoToY.Name = "m_lblGoToY";
			this.m_lblGoToY.Size = new System.Drawing.Size(103, 33);
			this.m_lblGoToY.TabIndex = 63;
			this.m_lblGoToY.Text = "GotoY";
			this.m_lblGoToY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// m_lblGoToX
			// 
			this.m_lblGoToX.AutoSize = true;
			this.m_lblGoToX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_lblGoToX.Location = new System.Drawing.Point(221, 66);
			this.m_lblGoToX.Name = "m_lblGoToX";
			this.m_lblGoToX.Size = new System.Drawing.Size(103, 33);
			this.m_lblGoToX.TabIndex = 62;
			this.m_lblGoToX.Text = "GotoX";
			this.m_lblGoToX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnMoveAbs
			// 
			this.btnMoveAbs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnMoveAbs.Location = new System.Drawing.Point(112, 102);
			this.btnMoveAbs.Name = "btnMoveAbs";
			this.btnMoveAbs.Size = new System.Drawing.Size(103, 27);
			this.btnMoveAbs.TabIndex = 59;
			this.btnMoveAbs.Text = "Move Abs";
			this.btnMoveAbs.UseVisualStyleBackColor = true;
			this.btnMoveAbs.Click += new System.EventHandler(this.btnMoveAbs_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tableLayoutPanel3);
			this.groupBox2.Location = new System.Drawing.Point(3, 299);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(1498, 606);
			this.groupBox2.TabIndex = 39;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Images";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 8;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.checkBox9, 7, 5);
			this.tableLayoutPanel3.Controls.Add(this.checkBox8, 0, 5);
			this.tableLayoutPanel3.Controls.Add(this.checkBox7, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.checkBox6, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.checkBox5, 7, 4);
			this.tableLayoutPanel3.Controls.Add(this.checkBox4, 7, 3);
			this.tableLayoutPanel3.Controls.Add(this.textBox4, 7, 6);
			this.tableLayoutPanel3.Controls.Add(this.textBox3, 0, 6);
			this.tableLayoutPanel3.Controls.Add(this.label2, 0, 9);
			this.tableLayoutPanel3.Controls.Add(this.label1, 2, 7);
			this.tableLayoutPanel3.Controls.Add(this.textBox2, 0, 10);
			this.tableLayoutPanel3.Controls.Add(this.textBox1, 3, 8);
			this.tableLayoutPanel3.Controls.Add(this.lblColorBarMaxInt1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.chkbxNormalized, 3, 4);
			this.tableLayoutPanel3.Controls.Add(this.lblColorBarMinInt1, 0, 13);
			this.tableLayoutPanel3.Controls.Add(this.chkbxCorrectedImage, 3, 3);
			this.tableLayoutPanel3.Controls.Add(this.scanImageControl1, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.scanImageControl2, 5, 0);
			this.tableLayoutPanel3.Controls.Add(this.drwcnvColorBar2, 6, 0);
			this.tableLayoutPanel3.Controls.Add(this.drwcnvColorBar1, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblColorBarMaxInt2, 7, 0);
			this.tableLayoutPanel3.Controls.Add(this.lblColorBarMinInt2, 7, 13);
			this.tableLayoutPanel3.Controls.Add(this.btnImageFit, 3, 0);
			this.tableLayoutPanel3.Controls.Add(this.buttonExp, 3, 12);
			this.tableLayoutPanel3.Controls.Add(this.btnLoadPreviousFile, 3, 2);
			this.tableLayoutPanel3.Controls.Add(this.btnLoadNextFile, 4, 2);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 16;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(1492, 587);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// checkBox9
			// 
			this.checkBox9.AutoSize = true;
			this.checkBox9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox9.Location = new System.Drawing.Point(1364, 193);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(125, 34);
			this.checkBox9.TabIndex = 48;
			this.checkBox9.Text = "Overlay";
			this.checkBox9.UseVisualStyleBackColor = true;
			this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox8.Location = new System.Drawing.Point(3, 193);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(115, 34);
			this.checkBox8.TabIndex = 47;
			this.checkBox8.Text = "Overlay";
			this.checkBox8.UseVisualStyleBackColor = true;
			this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox7.Location = new System.Drawing.Point(3, 153);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(115, 34);
			this.checkBox7.TabIndex = 46;
			this.checkBox7.Text = "Green";
			this.checkBox7.UseVisualStyleBackColor = true;
			this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox6.Location = new System.Drawing.Point(3, 113);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(115, 34);
			this.checkBox6.TabIndex = 45;
			this.checkBox6.Text = "Red";
			this.checkBox6.UseVisualStyleBackColor = true;
			this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox5.Location = new System.Drawing.Point(1364, 153);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(125, 34);
			this.checkBox5.TabIndex = 44;
			this.checkBox5.Text = "Green";
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.checkBox4.Location = new System.Drawing.Point(1364, 113);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(125, 34);
			this.checkBox4.TabIndex = 43;
			this.checkBox4.Text = "Red";
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
			// 
			// textBox4
			// 
			this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox4.Location = new System.Drawing.Point(1364, 233);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(125, 20);
			this.textBox4.TabIndex = 42;
			// 
			// textBox3
			// 
			this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox3.Location = new System.Drawing.Point(3, 233);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(115, 20);
			this.textBox3.TabIndex = 41;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.label2, 2);
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(684, 350);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 40);
			this.label2.TabIndex = 39;
			this.label2.Text = "Samples APD2";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.label1, 2);
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(684, 270);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(114, 40);
			this.label1.TabIndex = 38;
			this.label1.Text = "Samples APD1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// textBox2
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.textBox2, 2);
			this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox2.Location = new System.Drawing.Point(684, 393);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(114, 20);
			this.textBox2.TabIndex = 37;
			// 
			// textBox1
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.textBox1, 2);
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox1.Location = new System.Drawing.Point(684, 313);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(114, 20);
			this.textBox1.TabIndex = 36;
			// 
			// lblColorBarMaxInt1
			// 
			this.lblColorBarMaxInt1.AutoSize = true;
			this.lblColorBarMaxInt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblColorBarMaxInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColorBarMaxInt1.Location = new System.Drawing.Point(3, 0);
			this.lblColorBarMaxInt1.Name = "lblColorBarMaxInt1";
			this.lblColorBarMaxInt1.Size = new System.Drawing.Size(115, 30);
			this.lblColorBarMaxInt1.TabIndex = 30;
			this.lblColorBarMaxInt1.Text = "Max";
			this.lblColorBarMaxInt1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// chkbxNormalized
			// 
			this.chkbxNormalized.AutoSize = true;
			this.chkbxNormalized.Checked = true;
			this.chkbxNormalized.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tableLayoutPanel3.SetColumnSpan(this.chkbxNormalized, 2);
			this.chkbxNormalized.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkbxNormalized.Location = new System.Drawing.Point(684, 153);
			this.chkbxNormalized.Name = "chkbxNormalized";
			this.chkbxNormalized.Size = new System.Drawing.Size(114, 34);
			this.chkbxNormalized.TabIndex = 35;
			this.chkbxNormalized.Text = "Normalized";
			this.chkbxNormalized.UseVisualStyleBackColor = true;
			this.chkbxNormalized.CheckedChanged += new System.EventHandler(this.chkbxNormalized_CheckedChanged);
			// 
			// lblColorBarMinInt1
			// 
			this.lblColorBarMinInt1.AutoSize = true;
			this.lblColorBarMinInt1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblColorBarMinInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColorBarMinInt1.Location = new System.Drawing.Point(3, 510);
			this.lblColorBarMinInt1.Name = "lblColorBarMinInt1";
			this.lblColorBarMinInt1.Size = new System.Drawing.Size(115, 30);
			this.lblColorBarMinInt1.TabIndex = 31;
			this.lblColorBarMinInt1.Text = "Min";
			this.lblColorBarMinInt1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// chkbxCorrectedImage
			// 
			this.chkbxCorrectedImage.AutoSize = true;
			this.tableLayoutPanel3.SetColumnSpan(this.chkbxCorrectedImage, 2);
			this.chkbxCorrectedImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chkbxCorrectedImage.Location = new System.Drawing.Point(684, 113);
			this.chkbxCorrectedImage.Name = "chkbxCorrectedImage";
			this.chkbxCorrectedImage.Size = new System.Drawing.Size(114, 34);
			this.chkbxCorrectedImage.TabIndex = 34;
			this.chkbxCorrectedImage.Text = "Corrected Image";
			this.chkbxCorrectedImage.UseVisualStyleBackColor = true;
			this.chkbxCorrectedImage.CheckedChanged += new System.EventHandler(this.chkbxCorrectedImage_CheckedChanged);
			// 
			// scanImageControl1
			// 
			this.scanImageControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.scanImageControl1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.scanImageControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scanImageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scanImageControl1.Image = null;
			this.scanImageControl1.ImageText = "APD1";
			this.scanImageControl1.Location = new System.Drawing.Point(141, 0);
			this.scanImageControl1.Margin = new System.Windows.Forms.Padding(0);
			this.scanImageControl1.Name = "scanImageControl1";
			this.scanImageControl1.Origin = new System.Drawing.Point(0, 0);
			this.scanImageControl1.PanButton = System.Windows.Forms.MouseButtons.Left;
			this.scanImageControl1.PanMode = true;
			this.tableLayoutPanel3.SetRowSpan(this.scanImageControl1, 14);
			this.scanImageControl1.ScrollbarsVisible = true;
			this.scanImageControl1.Size = new System.Drawing.Size(540, 540);
			this.scanImageControl1.StretchImageToFit = false;
			this.scanImageControl1.TabIndex = 32;
			this.scanImageControl1.ZoomFactor = 1D;
			this.scanImageControl1.ZoomOnMouseWheel = true;
			this.scanImageControl1.OnPositionSelected += new System.EventHandler(this.scanImageControl1_OnPositionSelected);
			// 
			// scanImageControl2
			// 
			this.scanImageControl2.AutoSize = true;
			this.scanImageControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.scanImageControl2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.scanImageControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.scanImageControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scanImageControl2.Image = null;
			this.scanImageControl2.ImageText = "APD2";
			this.scanImageControl2.Location = new System.Drawing.Point(801, 0);
			this.scanImageControl2.Margin = new System.Windows.Forms.Padding(0);
			this.scanImageControl2.Name = "scanImageControl2";
			this.scanImageControl2.Origin = new System.Drawing.Point(0, 0);
			this.scanImageControl2.PanButton = System.Windows.Forms.MouseButtons.Left;
			this.scanImageControl2.PanMode = true;
			this.tableLayoutPanel3.SetRowSpan(this.scanImageControl2, 14);
			this.scanImageControl2.ScrollbarsVisible = true;
			this.scanImageControl2.Size = new System.Drawing.Size(540, 540);
			this.scanImageControl2.StretchImageToFit = false;
			this.scanImageControl2.TabIndex = 28;
			this.scanImageControl2.ZoomFactor = 1D;
			this.scanImageControl2.ZoomOnMouseWheel = true;
			this.scanImageControl2.OnPositionSelected += new System.EventHandler(this.scanImageControl2_OnPositionSelected);
			// 
			// drwcnvColorBar2
			// 
			this.drwcnvColorBar2.AutoSize = true;
			this.drwcnvColorBar2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.drwcnvColorBar2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drwcnvColorBar2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.drwcnvColorBar2.Image = null;
			this.drwcnvColorBar2.ImageText = null;
			this.drwcnvColorBar2.Location = new System.Drawing.Point(1344, 3);
			this.drwcnvColorBar2.Name = "drwcnvColorBar2";
			this.drwcnvColorBar2.Origin = new System.Drawing.Point(0, 0);
			this.drwcnvColorBar2.PanButton = System.Windows.Forms.MouseButtons.Left;
			this.drwcnvColorBar2.PanMode = true;
			this.tableLayoutPanel3.SetRowSpan(this.drwcnvColorBar2, 14);
			this.drwcnvColorBar2.Size = new System.Drawing.Size(14, 534);
			this.drwcnvColorBar2.StretchImageToFit = true;
			this.drwcnvColorBar2.TabIndex = 29;
			this.drwcnvColorBar2.ZoomFactor = 1D;
			this.drwcnvColorBar2.ZoomOnMouseWheel = false;
			// 
			// drwcnvColorBar1
			// 
			this.drwcnvColorBar1.AutoSize = true;
			this.drwcnvColorBar1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.drwcnvColorBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.drwcnvColorBar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.drwcnvColorBar1.Image = null;
			this.drwcnvColorBar1.ImageText = null;
			this.drwcnvColorBar1.Location = new System.Drawing.Point(124, 3);
			this.drwcnvColorBar1.Name = "drwcnvColorBar1";
			this.drwcnvColorBar1.Origin = new System.Drawing.Point(0, 0);
			this.drwcnvColorBar1.PanButton = System.Windows.Forms.MouseButtons.Left;
			this.drwcnvColorBar1.PanMode = true;
			this.tableLayoutPanel3.SetRowSpan(this.drwcnvColorBar1, 14);
			this.drwcnvColorBar1.Size = new System.Drawing.Size(14, 534);
			this.drwcnvColorBar1.StretchImageToFit = true;
			this.drwcnvColorBar1.TabIndex = 33;
			this.drwcnvColorBar1.ZoomFactor = 1D;
			this.drwcnvColorBar1.ZoomOnMouseWheel = false;
			// 
			// lblColorBarMaxInt2
			// 
			this.lblColorBarMaxInt2.AutoSize = true;
			this.lblColorBarMaxInt2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblColorBarMaxInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColorBarMaxInt2.Location = new System.Drawing.Point(1364, 0);
			this.lblColorBarMaxInt2.Name = "lblColorBarMaxInt2";
			this.lblColorBarMaxInt2.Size = new System.Drawing.Size(125, 30);
			this.lblColorBarMaxInt2.TabIndex = 18;
			this.lblColorBarMaxInt2.Text = "Max";
			// 
			// lblColorBarMinInt2
			// 
			this.lblColorBarMinInt2.AutoSize = true;
			this.lblColorBarMinInt2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblColorBarMinInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColorBarMinInt2.Location = new System.Drawing.Point(1364, 510);
			this.lblColorBarMinInt2.Name = "lblColorBarMinInt2";
			this.lblColorBarMinInt2.Size = new System.Drawing.Size(125, 30);
			this.lblColorBarMinInt2.TabIndex = 19;
			this.lblColorBarMinInt2.Text = "Min";
			this.lblColorBarMinInt2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// btnImageFit
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.btnImageFit, 2);
			this.btnImageFit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnImageFit.Location = new System.Drawing.Point(684, 3);
			this.btnImageFit.Name = "btnImageFit";
			this.tableLayoutPanel3.SetRowSpan(this.btnImageFit, 2);
			this.btnImageFit.Size = new System.Drawing.Size(114, 64);
			this.btnImageFit.TabIndex = 33;
			this.btnImageFit.Text = "Full Image";
			this.btnImageFit.UseVisualStyleBackColor = true;
			this.btnImageFit.Click += new System.EventHandler(this.btnImageFit_Click);
			// 
			// buttonExp
			// 
			this.tableLayoutPanel3.SetColumnSpan(this.buttonExp, 2);
			this.buttonExp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buttonExp.Location = new System.Drawing.Point(684, 473);
			this.buttonExp.Name = "buttonExp";
			this.tableLayoutPanel3.SetRowSpan(this.buttonExp, 2);
			this.buttonExp.Size = new System.Drawing.Size(114, 64);
			this.buttonExp.TabIndex = 49;
			this.buttonExp.Text = "EXPORT";
			this.buttonExp.UseVisualStyleBackColor = true;
			this.buttonExp.Click += new System.EventHandler(this.buttonExp_Click);
			// 
			// btnLoadPreviousFile
			// 
			this.btnLoadPreviousFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoadPreviousFile.Location = new System.Drawing.Point(684, 73);
			this.btnLoadPreviousFile.Name = "btnLoadPreviousFile";
			this.btnLoadPreviousFile.Size = new System.Drawing.Size(54, 34);
			this.btnLoadPreviousFile.TabIndex = 50;
			this.btnLoadPreviousFile.Text = "<";
			this.btnLoadPreviousFile.UseVisualStyleBackColor = true;
			this.btnLoadPreviousFile.Click += new System.EventHandler(this.btnLoadPreviousFile_Click);
			// 
			// btnLoadNextFile
			// 
			this.btnLoadNextFile.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnLoadNextFile.Location = new System.Drawing.Point(744, 73);
			this.btnLoadNextFile.Name = "btnLoadNextFile";
			this.btnLoadNextFile.Size = new System.Drawing.Size(54, 34);
			this.btnLoadNextFile.TabIndex = 51;
			this.btnLoadNextFile.Text = ">";
			this.btnLoadNextFile.UseVisualStyleBackColor = true;
			this.btnLoadNextFile.Click += new System.EventHandler(this.btnLoadNextFile_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.richTextBox1);
			this.groupBox1.Controls.Add(this.m_txtbxScanPropertiesFromFile);
			this.groupBox1.Location = new System.Drawing.Point(840, -1);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(658, 297);
			this.groupBox1.TabIndex = 38;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Experiment Properties";
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(244, 16);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richTextBox1.Size = new System.Drawing.Size(404, 275);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			this.richTextBox1.WordWrap = false;
			// 
			// m_txtbxScanPropertiesFromFile
			// 
			this.m_txtbxScanPropertiesFromFile.Location = new System.Drawing.Point(7, 16);
			this.m_txtbxScanPropertiesFromFile.Multiline = true;
			this.m_txtbxScanPropertiesFromFile.Name = "m_txtbxScanPropertiesFromFile";
			this.m_txtbxScanPropertiesFromFile.ReadOnly = true;
			this.m_txtbxScanPropertiesFromFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.m_txtbxScanPropertiesFromFile.Size = new System.Drawing.Size(231, 275);
			this.m_txtbxScanPropertiesFromFile.TabIndex = 0;
			this.m_txtbxScanPropertiesFromFile.WordWrap = false;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.elementHost2);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1504, 871);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "CCD WPF";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// elementHost2
			// 
			this.elementHost2.Location = new System.Drawing.Point(82, 14);
			this.elementHost2.Name = "elementHost2";
			this.elementHost2.Size = new System.Drawing.Size(1030, 674);
			this.elementHost2.TabIndex = 1;
			this.elementHost2.Text = "elementHost2";
			this.elementHost2.Child = this.ccdControl1;
			// 
			// ScanViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1512, 897);
			this.Controls.Add(this.m_TabControl);
			this.DoubleBuffered = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(0, 0);
			this.MaximizeBox = false;
			this.Name = "ScanViewForm";
			this.Text = "Scan View Form";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanViewForm_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScanViewForm_FormClosed);
			this.Controls.SetChildIndex(this.m_TabControl, 0);
			this.m_TabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_nupdFilenameCount)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private KUL.MDS.Validation.ValidationProvider valprovSISValidationProvider;
        private System.ComponentModel.BackgroundWorker bckgwrkPerformScan;
        private System.ComponentModel.BackgroundWorker bckgwrkPerformMove;
        private System.Windows.Forms.TabControl m_TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblColorBarMaxInt1;
        private System.Windows.Forms.CheckBox chkbxNormalized;
        private System.Windows.Forms.Label lblColorBarMinInt1;
        private System.Windows.Forms.CheckBox chkbxCorrectedImage;
        private ImageControl scanImageControl2;
        public DrawCanvas drwcnvColorBar2;
        public DrawCanvas drwcnvColorBar1;
        private System.Windows.Forms.Label lblColorBarMaxInt2;
        private System.Windows.Forms.Label lblColorBarMinInt2;
        private System.Windows.Forms.Button btnImageFit;
        private System.Windows.Forms.GroupBox groupBox1;
        private ImageControl scanImageControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button btnValidateInput;
        private System.Windows.Forms.Button btnAPDOnOff;
        private System.Windows.Forms.TextBox txtbxCurrYPos;
        private System.Windows.Forms.TextBox txtbxCurrXPos;
        private System.Windows.Forms.Button btnStageOnOff;
        private System.Windows.Forms.Label lblStageCurrYPos;
        private System.Windows.Forms.Label lblStageCurrXPos;
        private System.Windows.Forms.Button btnMoveAbs;
        private System.Windows.Forms.Button btnScanStart;
        private System.Windows.Forms.Button btnScanStop;
        private ScanModeComboBox scanModeComboBox1;
        private System.Windows.Forms.Button btnScanSettings;
        private System.Windows.Forms.Label m_lblGoToX;
        private System.Windows.Forms.Label m_lblGoToY;
        private System.Windows.Forms.Label m_lblGoToZ;
        private System.Windows.Forms.TextBox m_txtbxGoToX;
        private System.Windows.Forms.TextBox m_txtbxGoToY;
        private System.Windows.Forms.TextBox m_txtbxGoToZ;
        private System.Windows.Forms.TextBox m_txtbxScanPropertiesFromFile;
        private System.Windows.Forms.NumericUpDown m_nupdFilenameCount;
        private System.Windows.Forms.CheckBox m_chkbxAutosave;
        private System.Windows.Forms.CheckBox m_checkbxAutoIncrement;
        private System.Windows.Forms.CheckBox m_checkbxResend;
        private System.Windows.Forms.CheckBox m_checkbxContinuousScan;
        private System.Windows.Forms.TextBox txtbxCurrZPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private CCDControl ccdControl1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox m_checkbxDMA;
        private System.Windows.Forms.Button buttonExp;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox m_checkbxBidirScan;
        private System.Windows.Forms.CheckBox m_checkbxSaveTTTRData;
        private System.Windows.Forms.Button btnAPDCountRate;
        private System.Windows.Forms.CheckBox m_checkbxApplyTimeGatingWhileScanning;
        private System.Windows.Forms.TextBox txtboxTimeGatingMaxAPD1;
        private System.Windows.Forms.Label lblTimeGating;
        private System.Windows.Forms.TextBox txtboxTimeGatingMinAPD1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnNoGateAPD1;
        private System.Windows.Forms.Button btnApplyGateAPD1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtboxTimeGatingMinAPD2;
        private System.Windows.Forms.TextBox txtboxTimeGatingMaxAPD2;
        private System.Windows.Forms.Button btnNoGateAPD2;
        private System.Windows.Forms.Button btnApplyGateAPD2;
		private System.Windows.Forms.Button btnLoadPreviousFile;
		private System.Windows.Forms.Button btnLoadNextFile;

    }
}