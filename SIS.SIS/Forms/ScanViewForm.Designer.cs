namespace SIS.Forms
{
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.valprovSISValidationProvider = new SIS.Validation.ValidationProvider(this.components);
            this.bckgwrkPerformScan = new System.ComponentModel.BackgroundWorker();
            this.bckgwrkPerformMove = new System.ComponentModel.BackgroundWorker();
            this.m_TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtbxCurrZPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxCont = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.m_txtbxGoToZ = new System.Windows.Forms.TextBox();
            this.m_txtbxGoToY = new System.Windows.Forms.TextBox();
            this.m_txtbxGoToX = new System.Windows.Forms.TextBox();
            this.m_lblGoToZ = new System.Windows.Forms.Label();
            this.m_lblGoToY = new System.Windows.Forms.Label();
            this.m_lblGoToX = new System.Windows.Forms.Label();
            this.scanModeComboBox1 = new SIS.Controls.ScanModeComboBox();
            this.btnScanStart = new System.Windows.Forms.Button();
            this.lblStageVoltageEngaged = new System.Windows.Forms.Label();
            this.btnStageOFF = new System.Windows.Forms.Button();
            this.txtbxCurrYPos = new System.Windows.Forms.TextBox();
            this.txtbxCurrXPos = new System.Windows.Forms.TextBox();
            this.btnStageON = new System.Windows.Forms.Button();
            this.lblStageCurrYPos = new System.Windows.Forms.Label();
            this.lblStageCurrXPos = new System.Windows.Forms.Label();
            this.btnMoveAbs = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnValidateInput = new System.Windows.Forms.Button();
            this.m_btnScanSettings = new System.Windows.Forms.Button();
            this.m_nupdFilenameCount = new System.Windows.Forms.NumericUpDown();
            this.m_chkbxAutosave = new System.Windows.Forms.CheckBox();
            this.checkBoxDMA = new System.Windows.Forms.CheckBox();
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
            this.scanImageControl1 = new SIS.Library.ImageControl();
            this.scanImageControl2 = new SIS.Library.ImageControl();
            this.drwcnvColorBar2 = new SIS.Library.DrawCanvas();
            this.drwcnvColorBar1 = new SIS.Library.DrawCanvas();
            this.lblColorBarMaxInt2 = new System.Windows.Forms.Label();
            this.lblColorBarMinInt2 = new System.Windows.Forms.Label();
            this.btnImageFit = new System.Windows.Forms.Button();
            this.buttonExp = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.m_txtbxScanPropertiesFromFile = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.ccdControl1 = new SIS.WPFControls.CCDControl.UI.CCDControl();
            this.m_TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.m_TabControl.Size = new System.Drawing.Size(1194, 722);
            this.m_TabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1186, 696);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scan View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(12, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(552, 246);
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
            this.tableLayoutPanel1.Controls.Add(this.txtbxCurrZPos, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxCont, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.checkBox2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToZ, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToY, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_txtbxGoToX, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_lblGoToZ, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_lblGoToY, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_lblGoToX, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.scanModeComboBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnScanStart, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblStageVoltageEngaged, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnStageOFF, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtbxCurrYPos, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtbxCurrXPos, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStageON, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStageCurrYPos, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStageCurrXPos, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnMoveAbs, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnValidateInput, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_btnScanSettings, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_nupdFilenameCount, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_chkbxAutosave, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxDMA, 4, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 227);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // txtbxCurrZPos
            // 
            this.txtbxCurrZPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxCurrZPos.Location = new System.Drawing.Point(439, 71);
            this.txtbxCurrZPos.Name = "txtbxCurrZPos";
            this.txtbxCurrZPos.ReadOnly = true;
            this.txtbxCurrZPos.Size = new System.Drawing.Size(104, 20);
            this.txtbxCurrZPos.TabIndex = 76;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(439, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 34);
            this.label3.TabIndex = 75;
            this.label3.Text = "Z Position (nm):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxCont
            // 
            this.checkBoxCont.AutoSize = true;
            this.checkBoxCont.Location = new System.Drawing.Point(3, 207);
            this.checkBoxCont.Name = "checkBoxCont";
            this.checkBoxCont.Size = new System.Drawing.Size(85, 17);
            this.checkBoxCont.TabIndex = 74;
            this.checkBoxCont.Text = "Continuous?";
            this.checkBoxCont.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(3, 173);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 17);
            this.checkBox2.TabIndex = 73;
            this.checkBox2.Text = "Auto-Increment";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(112, 173);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 72;
            this.checkBox1.Text = "Re-Send";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // m_txtbxGoToZ
            // 
            this.m_txtbxGoToZ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtbxGoToZ.Location = new System.Drawing.Point(330, 139);
            this.m_txtbxGoToZ.Name = "m_txtbxGoToZ";
            this.m_txtbxGoToZ.Size = new System.Drawing.Size(103, 20);
            this.m_txtbxGoToZ.TabIndex = 67;
            this.m_txtbxGoToZ.Text = "0";
            // 
            // m_txtbxGoToY
            // 
            this.m_txtbxGoToY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtbxGoToY.Location = new System.Drawing.Point(221, 139);
            this.m_txtbxGoToY.Name = "m_txtbxGoToY";
            this.m_txtbxGoToY.Size = new System.Drawing.Size(103, 20);
            this.m_txtbxGoToY.TabIndex = 66;
            this.m_txtbxGoToY.Text = "0";
            // 
            // m_txtbxGoToX
            // 
            this.m_txtbxGoToX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_txtbxGoToX.Location = new System.Drawing.Point(112, 139);
            this.m_txtbxGoToX.Name = "m_txtbxGoToX";
            this.m_txtbxGoToX.Size = new System.Drawing.Size(103, 20);
            this.m_txtbxGoToX.TabIndex = 65;
            this.m_txtbxGoToX.Text = "0";
            // 
            // m_lblGoToZ
            // 
            this.m_lblGoToZ.AutoSize = true;
            this.m_lblGoToZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblGoToZ.Location = new System.Drawing.Point(330, 102);
            this.m_lblGoToZ.Name = "m_lblGoToZ";
            this.m_lblGoToZ.Size = new System.Drawing.Size(103, 34);
            this.m_lblGoToZ.TabIndex = 64;
            this.m_lblGoToZ.Text = "GotoZ";
            this.m_lblGoToZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblGoToY
            // 
            this.m_lblGoToY.AutoSize = true;
            this.m_lblGoToY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblGoToY.Location = new System.Drawing.Point(221, 102);
            this.m_lblGoToY.Name = "m_lblGoToY";
            this.m_lblGoToY.Size = new System.Drawing.Size(103, 34);
            this.m_lblGoToY.TabIndex = 63;
            this.m_lblGoToY.Text = "GotoY";
            this.m_lblGoToY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // m_lblGoToX
            // 
            this.m_lblGoToX.AutoSize = true;
            this.m_lblGoToX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_lblGoToX.Location = new System.Drawing.Point(112, 102);
            this.m_lblGoToX.Name = "m_lblGoToX";
            this.m_lblGoToX.Size = new System.Drawing.Size(103, 34);
            this.m_lblGoToX.TabIndex = 62;
            this.m_lblGoToX.Text = "GotoX";
            this.m_lblGoToX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // scanModeComboBox1
            // 
            this.scanModeComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scanModeComboBox1.FormattingEnabled = true;
            this.scanModeComboBox1.Location = new System.Drawing.Point(3, 71);
            this.scanModeComboBox1.Name = "scanModeComboBox1";
            this.scanModeComboBox1.Size = new System.Drawing.Size(103, 21);
            this.scanModeComboBox1.TabIndex = 60;
            // 
            // btnScanStart
            // 
            this.btnScanStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScanStart.Location = new System.Drawing.Point(3, 105);
            this.btnScanStart.Name = "btnScanStart";
            this.btnScanStart.Size = new System.Drawing.Size(103, 28);
            this.btnScanStart.TabIndex = 50;
            this.btnScanStart.Text = "Scan";
            this.btnScanStart.UseVisualStyleBackColor = true;
            this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
            // 
            // lblStageVoltageEngaged
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.lblStageVoltageEngaged, 2);
            this.lblStageVoltageEngaged.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageVoltageEngaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStageVoltageEngaged.ForeColor = System.Drawing.Color.Red;
            this.lblStageVoltageEngaged.Location = new System.Drawing.Point(221, 0);
            this.lblStageVoltageEngaged.Name = "lblStageVoltageEngaged";
            this.lblStageVoltageEngaged.Size = new System.Drawing.Size(212, 34);
            this.lblStageVoltageEngaged.TabIndex = 55;
            this.lblStageVoltageEngaged.Text = "STAGE";
            this.lblStageVoltageEngaged.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStageOFF
            // 
            this.btnStageOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStageOFF.Location = new System.Drawing.Point(112, 37);
            this.btnStageOFF.Name = "btnStageOFF";
            this.btnStageOFF.Size = new System.Drawing.Size(103, 28);
            this.btnStageOFF.TabIndex = 58;
            this.btnStageOFF.Text = "OFF";
            this.btnStageOFF.UseVisualStyleBackColor = true;
            this.btnStageOFF.Click += new System.EventHandler(this.btnStageOFF_Click);
            // 
            // txtbxCurrYPos
            // 
            this.txtbxCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxCurrYPos.Location = new System.Drawing.Point(330, 71);
            this.txtbxCurrYPos.Name = "txtbxCurrYPos";
            this.txtbxCurrYPos.ReadOnly = true;
            this.txtbxCurrYPos.Size = new System.Drawing.Size(103, 20);
            this.txtbxCurrYPos.TabIndex = 54;
            // 
            // txtbxCurrXPos
            // 
            this.txtbxCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxCurrXPos.Location = new System.Drawing.Point(221, 71);
            this.txtbxCurrXPos.Name = "txtbxCurrXPos";
            this.txtbxCurrXPos.ReadOnly = true;
            this.txtbxCurrXPos.Size = new System.Drawing.Size(103, 20);
            this.txtbxCurrXPos.TabIndex = 53;
            // 
            // btnStageON
            // 
            this.btnStageON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStageON.Location = new System.Drawing.Point(112, 3);
            this.btnStageON.Name = "btnStageON";
            this.btnStageON.Size = new System.Drawing.Size(103, 28);
            this.btnStageON.TabIndex = 56;
            this.btnStageON.Text = "ON";
            this.btnStageON.UseVisualStyleBackColor = true;
            this.btnStageON.Click += new System.EventHandler(this.btnStageON_Click);
            // 
            // lblStageCurrYPos
            // 
            this.lblStageCurrYPos.AutoSize = true;
            this.lblStageCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageCurrYPos.Location = new System.Drawing.Point(221, 34);
            this.lblStageCurrYPos.Name = "lblStageCurrYPos";
            this.lblStageCurrYPos.Size = new System.Drawing.Size(103, 34);
            this.lblStageCurrYPos.TabIndex = 52;
            this.lblStageCurrYPos.Text = "X Position (nm):";
            this.lblStageCurrYPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStageCurrXPos
            // 
            this.lblStageCurrXPos.AutoSize = true;
            this.lblStageCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageCurrXPos.Location = new System.Drawing.Point(330, 34);
            this.lblStageCurrXPos.Name = "lblStageCurrXPos";
            this.lblStageCurrXPos.Size = new System.Drawing.Size(103, 34);
            this.lblStageCurrXPos.TabIndex = 51;
            this.lblStageCurrXPos.Text = "Y Position (nm):";
            this.lblStageCurrXPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMoveAbs
            // 
            this.btnMoveAbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveAbs.Location = new System.Drawing.Point(112, 71);
            this.btnMoveAbs.Name = "btnMoveAbs";
            this.btnMoveAbs.Size = new System.Drawing.Size(103, 28);
            this.btnMoveAbs.TabIndex = 59;
            this.btnMoveAbs.Text = "Move Abs";
            this.btnMoveAbs.UseVisualStyleBackColor = true;
            this.btnMoveAbs.Click += new System.EventHandler(this.btnMoveAbs_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(3, 139);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(103, 28);
            this.btnStop.TabIndex = 57;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnValidateInput
            // 
            this.btnValidateInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnValidateInput.Location = new System.Drawing.Point(3, 37);
            this.btnValidateInput.Name = "btnValidateInput";
            this.btnValidateInput.Size = new System.Drawing.Size(103, 28);
            this.btnValidateInput.TabIndex = 34;
            this.btnValidateInput.Text = "Validate";
            this.btnValidateInput.UseVisualStyleBackColor = true;
            // 
            // m_btnScanSettings
            // 
            this.m_btnScanSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_btnScanSettings.Location = new System.Drawing.Point(3, 3);
            this.m_btnScanSettings.Name = "m_btnScanSettings";
            this.m_btnScanSettings.Size = new System.Drawing.Size(103, 28);
            this.m_btnScanSettings.TabIndex = 61;
            this.m_btnScanSettings.Text = "Scan Settings";
            this.m_btnScanSettings.UseVisualStyleBackColor = true;
            this.m_btnScanSettings.Click += new System.EventHandler(this.m_btnScanSettings_Click);
            // 
            // m_nupdFilenameCount
            // 
            this.m_nupdFilenameCount.Location = new System.Drawing.Point(221, 173);
            this.m_nupdFilenameCount.Name = "m_nupdFilenameCount";
            this.m_nupdFilenameCount.Size = new System.Drawing.Size(103, 20);
            this.m_nupdFilenameCount.TabIndex = 70;
            // 
            // m_chkbxAutosave
            // 
            this.m_chkbxAutosave.AutoSize = true;
            this.m_chkbxAutosave.Checked = true;
            this.m_chkbxAutosave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_chkbxAutosave.Location = new System.Drawing.Point(330, 173);
            this.m_chkbxAutosave.Name = "m_chkbxAutosave";
            this.m_chkbxAutosave.Size = new System.Drawing.Size(74, 17);
            this.m_chkbxAutosave.TabIndex = 71;
            this.m_chkbxAutosave.Text = "Auto-save";
            this.m_chkbxAutosave.UseVisualStyleBackColor = true;
            // 
            // checkBoxDMA
            // 
            this.checkBoxDMA.AutoSize = true;
            this.checkBoxDMA.Location = new System.Drawing.Point(439, 173);
            this.checkBoxDMA.Name = "checkBoxDMA";
            this.checkBoxDMA.Size = new System.Drawing.Size(78, 17);
            this.checkBoxDMA.TabIndex = 77;
            this.checkBoxDMA.Text = "Use DMA?";
            this.checkBoxDMA.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Location = new System.Drawing.Point(9, 262);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1167, 426);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Images";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 8;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142859F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.85714F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.142859F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.checkBox9, 4, 3);
            this.tableLayoutPanel3.Controls.Add(this.checkBox8, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.checkBox7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.checkBox6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.checkBox5, 7, 2);
            this.tableLayoutPanel3.Controls.Add(this.checkBox4, 7, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBox4, 7, 4);
            this.tableLayoutPanel3.Controls.Add(this.textBox3, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.label1, 2, 4);
            this.tableLayoutPanel3.Controls.Add(this.textBox2, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.textBox1, 3, 5);
            this.tableLayoutPanel3.Controls.Add(this.lblColorBarMaxInt1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkbxNormalized, 3, 3);
            this.tableLayoutPanel3.Controls.Add(this.lblColorBarMinInt1, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.chkbxCorrectedImage, 3, 2);
            this.tableLayoutPanel3.Controls.Add(this.scanImageControl1, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.scanImageControl2, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.drwcnvColorBar2, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.drwcnvColorBar1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblColorBarMaxInt2, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblColorBarMinInt2, 7, 9);
            this.tableLayoutPanel3.Controls.Add(this.btnImageFit, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonExp, 3, 8);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 11;
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
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1161, 407);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox9.Location = new System.Drawing.Point(1071, 123);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(87, 34);
            this.checkBox9.TabIndex = 48;
            this.checkBox9.Text = "Overlay";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox9_CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox8.Location = new System.Drawing.Point(3, 123);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(84, 34);
            this.checkBox8.TabIndex = 47;
            this.checkBox8.Text = "Overlay";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox8_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox7.Location = new System.Drawing.Point(3, 83);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(84, 34);
            this.checkBox7.TabIndex = 46;
            this.checkBox7.Text = "Green";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox6.Location = new System.Drawing.Point(3, 43);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(84, 34);
            this.checkBox6.TabIndex = 45;
            this.checkBox6.Text = "Red";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox5.Location = new System.Drawing.Point(1071, 83);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(87, 34);
            this.checkBox5.TabIndex = 44;
            this.checkBox5.Text = "Green";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox4.Location = new System.Drawing.Point(1071, 43);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(87, 34);
            this.checkBox4.TabIndex = 43;
            this.checkBox4.Text = "Red";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(1071, 163);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(87, 20);
            this.textBox4.TabIndex = 42;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(3, 163);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(84, 20);
            this.textBox3.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(518, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 40);
            this.label2.TabIndex = 39;
            this.label2.Text = "Samples APD2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(518, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 40);
            this.label1.TabIndex = 38;
            this.label1.Text = "Samples APD1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.textBox2, 2);
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(518, 283);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(122, 20);
            this.textBox2.TabIndex = 37;
            // 
            // textBox1
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(518, 203);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 36;
            // 
            // lblColorBarMaxInt1
            // 
            this.lblColorBarMaxInt1.AutoSize = true;
            this.lblColorBarMaxInt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBarMaxInt1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorBarMaxInt1.Location = new System.Drawing.Point(3, 0);
            this.lblColorBarMaxInt1.Name = "lblColorBarMaxInt1";
            this.lblColorBarMaxInt1.Size = new System.Drawing.Size(84, 40);
            this.lblColorBarMaxInt1.TabIndex = 30;
            this.lblColorBarMaxInt1.Text = "Max";
            // 
            // chkbxNormalized
            // 
            this.chkbxNormalized.AutoSize = true;
            this.chkbxNormalized.Checked = true;
            this.chkbxNormalized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tableLayoutPanel3.SetColumnSpan(this.chkbxNormalized, 2);
            this.chkbxNormalized.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbxNormalized.Location = new System.Drawing.Point(518, 123);
            this.chkbxNormalized.Name = "chkbxNormalized";
            this.chkbxNormalized.Size = new System.Drawing.Size(122, 34);
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
            this.lblColorBarMinInt1.Location = new System.Drawing.Point(3, 360);
            this.lblColorBarMinInt1.Name = "lblColorBarMinInt1";
            this.lblColorBarMinInt1.Size = new System.Drawing.Size(84, 40);
            this.lblColorBarMinInt1.TabIndex = 31;
            this.lblColorBarMinInt1.Text = "Min";
            this.lblColorBarMinInt1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // chkbxCorrectedImage
            // 
            this.chkbxCorrectedImage.AutoSize = true;
            this.tableLayoutPanel3.SetColumnSpan(this.chkbxCorrectedImage, 2);
            this.chkbxCorrectedImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbxCorrectedImage.Location = new System.Drawing.Point(518, 83);
            this.chkbxCorrectedImage.Name = "chkbxCorrectedImage";
            this.chkbxCorrectedImage.Size = new System.Drawing.Size(122, 34);
            this.chkbxCorrectedImage.TabIndex = 34;
            this.chkbxCorrectedImage.Text = "Corrected Image";
            this.chkbxCorrectedImage.UseVisualStyleBackColor = true;
            this.chkbxCorrectedImage.CheckedChanged += new System.EventHandler(this.chkbxCorrectedImage_CheckedChanged);
            // 
            // scanImageControl1
            // 
            this.scanImageControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.scanImageControl1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scanImageControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanImageControl1.Image = null;
            this.scanImageControl1.ImageText = "APD1";
            this.scanImageControl1.Location = new System.Drawing.Point(115, 0);
            this.scanImageControl1.Margin = new System.Windows.Forms.Padding(0);
            this.scanImageControl1.Name = "scanImageControl1";
            this.scanImageControl1.Origin = new System.Drawing.Point(0, 0);
            this.scanImageControl1.PanButton = System.Windows.Forms.MouseButtons.Left;
            this.scanImageControl1.PanMode = true;
            this.tableLayoutPanel3.SetRowSpan(this.scanImageControl1, 10);
            this.scanImageControl1.ScrollbarsVisible = true;
            this.scanImageControl1.Size = new System.Drawing.Size(400, 400);
            this.scanImageControl1.StretchImageToFit = true;
            this.scanImageControl1.TabIndex = 32;
            this.scanImageControl1.ZoomFactor = 1D;
            this.scanImageControl1.ZoomOnMouseWheel = true;
            this.scanImageControl1.OnPositionSelected += new System.EventHandler(this.scanImageControl1_OnPositionSelected);
            // 
            // scanImageControl2
            // 
            this.scanImageControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.scanImageControl2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.scanImageControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scanImageControl2.Image = null;
            this.scanImageControl2.ImageText = "APD2";
            this.scanImageControl2.Location = new System.Drawing.Point(643, 0);
            this.scanImageControl2.Margin = new System.Windows.Forms.Padding(0);
            this.scanImageControl2.Name = "scanImageControl2";
            this.scanImageControl2.Origin = new System.Drawing.Point(0, 0);
            this.scanImageControl2.PanButton = System.Windows.Forms.MouseButtons.Left;
            this.scanImageControl2.PanMode = true;
            this.tableLayoutPanel3.SetRowSpan(this.scanImageControl2, 10);
            this.scanImageControl2.ScrollbarsVisible = true;
            this.scanImageControl2.Size = new System.Drawing.Size(400, 400);
            this.scanImageControl2.StretchImageToFit = true;
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
            this.drwcnvColorBar2.Location = new System.Drawing.Point(1046, 3);
            this.drwcnvColorBar2.Name = "drwcnvColorBar2";
            this.drwcnvColorBar2.Origin = new System.Drawing.Point(0, 0);
            this.drwcnvColorBar2.PanButton = System.Windows.Forms.MouseButtons.Left;
            this.drwcnvColorBar2.PanMode = true;
            this.tableLayoutPanel3.SetRowSpan(this.drwcnvColorBar2, 10);
            this.drwcnvColorBar2.Size = new System.Drawing.Size(19, 394);
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
            this.drwcnvColorBar1.Location = new System.Drawing.Point(93, 3);
            this.drwcnvColorBar1.Name = "drwcnvColorBar1";
            this.drwcnvColorBar1.Origin = new System.Drawing.Point(0, 0);
            this.drwcnvColorBar1.PanButton = System.Windows.Forms.MouseButtons.Left;
            this.drwcnvColorBar1.PanMode = true;
            this.tableLayoutPanel3.SetRowSpan(this.drwcnvColorBar1, 10);
            this.drwcnvColorBar1.Size = new System.Drawing.Size(19, 394);
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
            this.lblColorBarMaxInt2.Location = new System.Drawing.Point(1071, 0);
            this.lblColorBarMaxInt2.Name = "lblColorBarMaxInt2";
            this.lblColorBarMaxInt2.Size = new System.Drawing.Size(87, 40);
            this.lblColorBarMaxInt2.TabIndex = 18;
            this.lblColorBarMaxInt2.Text = "Max";
            this.lblColorBarMaxInt2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblColorBarMinInt2
            // 
            this.lblColorBarMinInt2.AutoSize = true;
            this.lblColorBarMinInt2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBarMinInt2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorBarMinInt2.Location = new System.Drawing.Point(1071, 360);
            this.lblColorBarMinInt2.Name = "lblColorBarMinInt2";
            this.lblColorBarMinInt2.Size = new System.Drawing.Size(87, 40);
            this.lblColorBarMinInt2.TabIndex = 19;
            this.lblColorBarMinInt2.Text = "Min";
            this.lblColorBarMinInt2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // btnImageFit
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.btnImageFit, 2);
            this.btnImageFit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImageFit.Location = new System.Drawing.Point(518, 3);
            this.btnImageFit.Name = "btnImageFit";
            this.tableLayoutPanel3.SetRowSpan(this.btnImageFit, 2);
            this.btnImageFit.Size = new System.Drawing.Size(122, 74);
            this.btnImageFit.TabIndex = 33;
            this.btnImageFit.Text = "Full Image";
            this.btnImageFit.UseVisualStyleBackColor = true;
            this.btnImageFit.Click += new System.EventHandler(this.btnImageFit_Click);
            // 
            // buttonExp
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.buttonExp, 2);
            this.buttonExp.Location = new System.Drawing.Point(518, 323);
            this.buttonExp.Name = "buttonExp";
            this.tableLayoutPanel3.SetRowSpan(this.buttonExp, 2);
            this.buttonExp.Size = new System.Drawing.Size(122, 74);
            this.buttonExp.TabIndex = 49;
            this.buttonExp.Text = "EXPORT";
            this.buttonExp.UseVisualStyleBackColor = true;
            this.buttonExp.Click += new System.EventHandler(this.buttonExp_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.m_txtbxScanPropertiesFromFile);
            this.groupBox1.Location = new System.Drawing.Point(570, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 246);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Experiment Properties";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(155, 16);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(445, 224);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.WordWrap = false;
            // 
            // m_txtbxScanPropertiesFromFile
            // 
            this.m_txtbxScanPropertiesFromFile.Location = new System.Drawing.Point(3, 16);
            this.m_txtbxScanPropertiesFromFile.Multiline = true;
            this.m_txtbxScanPropertiesFromFile.Name = "m_txtbxScanPropertiesFromFile";
            this.m_txtbxScanPropertiesFromFile.ReadOnly = true;
            this.m_txtbxScanPropertiesFromFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_txtbxScanPropertiesFromFile.Size = new System.Drawing.Size(146, 227);
            this.m_txtbxScanPropertiesFromFile.TabIndex = 0;
            this.m_txtbxScanPropertiesFromFile.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.elementHost2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1186, 696);
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
            this.ClientSize = new System.Drawing.Size(1194, 722);
            this.Controls.Add(this.m_TabControl);
            this.DoubleBuffered = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.Name = "ScanViewForm";
            this.Text = "ScanViewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanViewForm_FormClosing);
            this.Controls.SetChildIndex(this.m_TabControl, 0);
            this.m_TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_nupdFilenameCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SIS.Validation.ValidationProvider valprovSISValidationProvider;
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
        private SIS.Library.ImageControl scanImageControl2;
        public SIS.Library.DrawCanvas drwcnvColorBar2;
        public SIS.Library.DrawCanvas drwcnvColorBar1;
        private System.Windows.Forms.Label lblColorBarMaxInt2;
        private System.Windows.Forms.Label lblColorBarMinInt2;
        private System.Windows.Forms.Button btnImageFit;
        private System.Windows.Forms.GroupBox groupBox1;
        private SIS.Library.ImageControl scanImageControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnValidateInput;
        private System.Windows.Forms.Label lblStageVoltageEngaged;
        private System.Windows.Forms.Button btnStageOFF;
        private System.Windows.Forms.TextBox txtbxCurrYPos;
        private System.Windows.Forms.TextBox txtbxCurrXPos;
        private System.Windows.Forms.Button btnStageON;
        private System.Windows.Forms.Label lblStageCurrYPos;
        private System.Windows.Forms.Label lblStageCurrXPos;
        private System.Windows.Forms.Button btnMoveAbs;
        private System.Windows.Forms.Button btnScanStart;
        private System.Windows.Forms.Button btnStop;
        private SIS.Controls.ScanModeComboBox scanModeComboBox1;
        private System.Windows.Forms.Button m_btnScanSettings;
        private System.Windows.Forms.Label m_lblGoToX;
        private System.Windows.Forms.Label m_lblGoToY;
        private System.Windows.Forms.Label m_lblGoToZ;
        private System.Windows.Forms.TextBox m_txtbxGoToX;
        private System.Windows.Forms.TextBox m_txtbxGoToY;
        private System.Windows.Forms.TextBox m_txtbxGoToZ;
        private System.Windows.Forms.TextBox m_txtbxScanPropertiesFromFile;
        private System.Windows.Forms.NumericUpDown m_nupdFilenameCount;
        private System.Windows.Forms.CheckBox m_chkbxAutosave;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxCont;
        private System.Windows.Forms.TextBox txtbxCurrZPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private SIS.WPFControls.CCDControl.UI.CCDControl ccdControl1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBoxDMA;
        private System.Windows.Forms.Button buttonExp;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}