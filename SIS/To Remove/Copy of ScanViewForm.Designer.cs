//namespace KUL.MDS.RIS.Forms
//{
//    partial class ScanViewForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            KUL.MDS.Validation.ValidationRule validationRule21 = new KUL.MDS.Validation.ValidationRule();
//            KUL.MDS.Validation.ValidationRule validationRule22 = new KUL.MDS.Validation.ValidationRule();
//            KUL.MDS.Validation.ValidationRule validationRule23 = new KUL.MDS.Validation.ValidationRule();
//            KUL.MDS.Validation.ValidationRule validationRule24 = new KUL.MDS.Validation.ValidationRule();
//            KUL.MDS.Validation.ValidationRule validationRule25 = new KUL.MDS.Validation.ValidationRule();
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanViewForm));
//            this.lblDateTime = new System.Windows.Forms.Label();
//            this.lblImageSize = new System.Windows.Forms.Label();
//            this.txtbxPortStatus = new System.Windows.Forms.TextBox();
//            this.grpbxLsrComms = new System.Windows.Forms.GroupBox();
//            this.btnLsrCW = new System.Windows.Forms.Button();
//            this.btnLsrPow = new System.Windows.Forms.Button();
//            this.btnLsrOFF = new System.Windows.Forms.Button();
//            this.btnLsrON = new System.Windows.Forms.Button();
//            this.txtbxLsrReply = new System.Windows.Forms.TextBox();
//            this.label2 = new System.Windows.Forms.Label();
//            this.label1 = new System.Windows.Forms.Label();
//            this.btnLsrClose = new System.Windows.Forms.Button();
//            this.btnLsrOpen = new System.Windows.Forms.Button();
//            this.grpbxExpCtrl = new System.Windows.Forms.GroupBox();
//            this.btnScanM1DX = new System.Windows.Forms.Button();
//            this.btnScan1DX = new System.Windows.Forms.Button();
//            this.txtbxOverScanPx = new System.Windows.Forms.TextBox();
//            this.label5 = new System.Windows.Forms.Label();
//            this.txtbxGoToY = new System.Windows.Forms.TextBox();
//            this.label4 = new System.Windows.Forms.Label();
//            this.txtbxGoToX = new System.Windows.Forms.TextBox();
//            this.label3 = new System.Windows.Forms.Label();
//            this.btnMoveAbs = new System.Windows.Forms.Button();
//            this.txtbxSampleDelta = new System.Windows.Forms.TextBox();
//            this.lblSampleDelta = new System.Windows.Forms.Label();
//            this.txtbxSamplesFromAPD = new System.Windows.Forms.TextBox();
//            this.lblSamplesFromAPD = new System.Windows.Forms.Label();
//            this.txtbxSamplesToStage = new System.Windows.Forms.TextBox();
//            this.lblSamplesToStage = new System.Windows.Forms.Label();
//            this.btnStop = new System.Windows.Forms.Button();
//            this.btnZeroStage = new System.Windows.Forms.Button();
//            this.btnValidateInput = new System.Windows.Forms.Button();
//            this.txtbxSetTimePPixel = new System.Windows.Forms.TextBox();
//            this.lblSetTimePPixel = new System.Windows.Forms.Label();
//            this.txtbxSetImageWidthnm = new System.Windows.Forms.TextBox();
//            this.lblSetImageWidthInnm = new System.Windows.Forms.Label();
//            this.txtbxSetInitY = new System.Windows.Forms.TextBox();
//            this.lblSetInitY = new System.Windows.Forms.Label();
//            this.txtbxSetInitX = new System.Windows.Forms.TextBox();
//            this.lblSetInitX = new System.Windows.Forms.Label();
//            this.txtbxSetImageWidth = new System.Windows.Forms.TextBox();
//            this.lblSetImageWidth = new System.Windows.Forms.Label();
//            this.btnScanStart = new System.Windows.Forms.Button();
//            this.btnImageFit = new System.Windows.Forms.Button();
//            this.groupBox4 = new System.Windows.Forms.GroupBox();
//            this.txtbxScanTime = new System.Windows.Forms.TextBox();
//            this.txtbxOverScan = new System.Windows.Forms.TextBox();
//            this.txtbxInitY = new System.Windows.Forms.TextBox();
//            this.txtbxInitX = new System.Windows.Forms.TextBox();
//            this.txtbxTimePPixel = new System.Windows.Forms.TextBox();
//            this.txtbxImageWidth = new System.Windows.Forms.TextBox();
//            this.lblScanTime = new System.Windows.Forms.Label();
//            this.lblOverScan = new System.Windows.Forms.Label();
//            this.lblInitY = new System.Windows.Forms.Label();
//            this.lblInitX = new System.Windows.Forms.Label();
//            this.lblImageWidth = new System.Windows.Forms.Label();
//            this.lblTimePPixel = new System.Windows.Forms.Label();
//            this.txtbxImageSize = new System.Windows.Forms.TextBox();
//            this.txtbxDateTime = new System.Windows.Forms.TextBox();
//            this.lblColorBarMaxInt = new System.Windows.Forms.Label();
//            this.lblColorBarMinInt = new System.Windows.Forms.Label();
//            this.lblYAxisMax = new System.Windows.Forms.Label();
//            this.lblYAxisMin = new System.Windows.Forms.Label();
//            this.lblXAxisMax = new System.Windows.Forms.Label();
//            this.lblXAxisMin = new System.Windows.Forms.Label();
//            this.lblXAxis = new System.Windows.Forms.Label();
//            this.lblYAxis = new System.Windows.Forms.Label();
//            this.grpbxStageStatus = new System.Windows.Forms.GroupBox();
//            this.btnStageOFF = new System.Windows.Forms.Button();
//            this.btnStageON = new System.Windows.Forms.Button();
//            this.lblStageVoltageEngaged = new System.Windows.Forms.Label();
//            this.txtbxYVoltCurr = new System.Windows.Forms.TextBox();
//            this.txtbxXVoltCurr = new System.Windows.Forms.TextBox();
//            this.lblStageCurrYVoltage = new System.Windows.Forms.Label();
//            this.lblStageCurrXVoltage = new System.Windows.Forms.Label();
//            this.valprovRISValidationProvider = new KUL.MDS.Validation.ValidationProvider(this.components);
//            this.groupBox5 = new System.Windows.Forms.GroupBox();
//            this.chkbxNormalized = new System.Windows.Forms.CheckBox();
//            this.chkbxCorrectedImage = new System.Windows.Forms.CheckBox();
//            this.bckgwrkPerformScan = new System.ComponentModel.BackgroundWorker();
//            this.drwcnvColorBar = new KUL.MDS.Library.PanZoomImageControl();
//            this.drwcnvScanImage = new KUL.MDS.Library.PanZoomImageControl();
//            this.bckgwrkPerformMove = new System.ComponentModel.BackgroundWorker();
//            this.grpbxLsrComms.SuspendLayout();
//            this.grpbxExpCtrl.SuspendLayout();
//            this.groupBox4.SuspendLayout();
//            this.grpbxStageStatus.SuspendLayout();
//            this.groupBox5.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // lblDateTime
//            // 
//            this.lblDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblDateTime.AutoSize = true;
//            this.lblDateTime.Location = new System.Drawing.Point(6, 24);
//            this.lblDateTime.Name = "lblDateTime";
//            this.lblDateTime.Size = new System.Drawing.Size(57, 13);
//            this.lblDateTime.TabIndex = 1;
//            this.lblDateTime.Text = "Exp. Date:";
//            // 
//            // lblImageSize
//            // 
//            this.lblImageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblImageSize.AutoSize = true;
//            this.lblImageSize.Location = new System.Drawing.Point(6, 56);
//            this.lblImageSize.Name = "lblImageSize";
//            this.lblImageSize.Size = new System.Drawing.Size(50, 13);
//            this.lblImageSize.TabIndex = 2;
//            this.lblImageSize.Text = "Img Size:";
//            // 
//            // txtbxPortStatus
//            // 
//            this.txtbxPortStatus.Location = new System.Drawing.Point(72, 21);
//            this.txtbxPortStatus.Name = "txtbxPortStatus";
//            this.txtbxPortStatus.ReadOnly = true;
//            this.txtbxPortStatus.Size = new System.Drawing.Size(408, 20);
//            this.txtbxPortStatus.TabIndex = 4;
//            // 
//            // grpbxLsrComms
//            // 
//            this.grpbxLsrComms.Controls.Add(this.btnLsrCW);
//            this.grpbxLsrComms.Controls.Add(this.btnLsrPow);
//            this.grpbxLsrComms.Controls.Add(this.btnLsrOFF);
//            this.grpbxLsrComms.Controls.Add(this.btnLsrON);
//            this.grpbxLsrComms.Controls.Add(this.txtbxLsrReply);
//            this.grpbxLsrComms.Controls.Add(this.label2);
//            this.grpbxLsrComms.Controls.Add(this.label1);
//            this.grpbxLsrComms.Controls.Add(this.btnLsrClose);
//            this.grpbxLsrComms.Controls.Add(this.btnLsrOpen);
//            this.grpbxLsrComms.Controls.Add(this.txtbxPortStatus);
//            this.grpbxLsrComms.Location = new System.Drawing.Point(752, 8);
//            this.grpbxLsrComms.Name = "grpbxLsrComms";
//            this.grpbxLsrComms.Size = new System.Drawing.Size(488, 128);
//            this.grpbxLsrComms.TabIndex = 6;
//            this.grpbxLsrComms.TabStop = false;
//            this.grpbxLsrComms.Text = "Laser Comms";
//            // 
//            // btnLsrCW
//            // 
//            this.btnLsrCW.Location = new System.Drawing.Point(89, 88);
//            this.btnLsrCW.Name = "btnLsrCW";
//            this.btnLsrCW.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrCW.TabIndex = 13;
//            this.btnLsrCW.Text = "LSR CW";
//            this.btnLsrCW.UseVisualStyleBackColor = true;
//            this.btnLsrCW.Click += new System.EventHandler(this.btnLsrCW_Click);
//            // 
//            // btnLsrPow
//            // 
//            this.btnLsrPow.Location = new System.Drawing.Point(169, 88);
//            this.btnLsrPow.Name = "btnLsrPow";
//            this.btnLsrPow.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrPow.TabIndex = 12;
//            this.btnLsrPow.Text = "LSR Pow";
//            this.btnLsrPow.UseVisualStyleBackColor = true;
//            this.btnLsrPow.Click += new System.EventHandler(this.btnLsrPow_Click);
//            // 
//            // btnLsrOFF
//            // 
//            this.btnLsrOFF.Location = new System.Drawing.Point(249, 88);
//            this.btnLsrOFF.Name = "btnLsrOFF";
//            this.btnLsrOFF.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrOFF.TabIndex = 11;
//            this.btnLsrOFF.Text = "LSR OFF";
//            this.btnLsrOFF.UseVisualStyleBackColor = true;
//            this.btnLsrOFF.Click += new System.EventHandler(this.btnLsrOFF_Click);
//            // 
//            // btnLsrON
//            // 
//            this.btnLsrON.Location = new System.Drawing.Point(409, 88);
//            this.btnLsrON.Name = "btnLsrON";
//            this.btnLsrON.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrON.TabIndex = 10;
//            this.btnLsrON.Text = "LSR ON";
//            this.btnLsrON.UseVisualStyleBackColor = true;
//            this.btnLsrON.Click += new System.EventHandler(this.btnLsrON_Click);
//            // 
//            // txtbxLsrReply
//            // 
//            this.txtbxLsrReply.Location = new System.Drawing.Point(72, 52);
//            this.txtbxLsrReply.Name = "txtbxLsrReply";
//            this.txtbxLsrReply.ReadOnly = true;
//            this.txtbxLsrReply.Size = new System.Drawing.Size(408, 20);
//            this.txtbxLsrReply.TabIndex = 9;
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(6, 55);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(66, 13);
//            this.label2.TabIndex = 8;
//            this.label2.Text = "Laser Reply:";
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(6, 24);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(62, 13);
//            this.label1.TabIndex = 7;
//            this.label1.Text = "Port Status:";
//            // 
//            // btnLsrClose
//            // 
//            this.btnLsrClose.Location = new System.Drawing.Point(329, 88);
//            this.btnLsrClose.Name = "btnLsrClose";
//            this.btnLsrClose.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrClose.TabIndex = 6;
//            this.btnLsrClose.Text = "LSR Close";
//            this.btnLsrClose.UseVisualStyleBackColor = true;
//            this.btnLsrClose.Click += new System.EventHandler(this.btnLsrClose_Click);
//            // 
//            // btnLsrOpen
//            // 
//            this.btnLsrOpen.Location = new System.Drawing.Point(9, 88);
//            this.btnLsrOpen.Name = "btnLsrOpen";
//            this.btnLsrOpen.Size = new System.Drawing.Size(70, 31);
//            this.btnLsrOpen.TabIndex = 5;
//            this.btnLsrOpen.Text = "LSR Open";
//            this.btnLsrOpen.UseVisualStyleBackColor = true;
//            this.btnLsrOpen.Click += new System.EventHandler(this.btnLsrOpen_Click);
//            // 
//            // grpbxExpCtrl
//            // 
//            this.grpbxExpCtrl.Controls.Add(this.btnScanM1DX);
//            this.grpbxExpCtrl.Controls.Add(this.btnScan1DX);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxOverScanPx);
//            this.grpbxExpCtrl.Controls.Add(this.label5);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxGoToY);
//            this.grpbxExpCtrl.Controls.Add(this.label4);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxGoToX);
//            this.grpbxExpCtrl.Controls.Add(this.label3);
//            this.grpbxExpCtrl.Controls.Add(this.btnMoveAbs);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSampleDelta);
//            this.grpbxExpCtrl.Controls.Add(this.lblSampleDelta);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSamplesFromAPD);
//            this.grpbxExpCtrl.Controls.Add(this.lblSamplesFromAPD);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSamplesToStage);
//            this.grpbxExpCtrl.Controls.Add(this.lblSamplesToStage);
//            this.grpbxExpCtrl.Controls.Add(this.btnStop);
//            this.grpbxExpCtrl.Controls.Add(this.btnZeroStage);
//            this.grpbxExpCtrl.Controls.Add(this.btnValidateInput);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSetTimePPixel);
//            this.grpbxExpCtrl.Controls.Add(this.lblSetTimePPixel);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSetImageWidthnm);
//            this.grpbxExpCtrl.Controls.Add(this.lblSetImageWidthInnm);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSetInitY);
//            this.grpbxExpCtrl.Controls.Add(this.lblSetInitY);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSetInitX);
//            this.grpbxExpCtrl.Controls.Add(this.lblSetInitX);
//            this.grpbxExpCtrl.Controls.Add(this.txtbxSetImageWidth);
//            this.grpbxExpCtrl.Controls.Add(this.lblSetImageWidth);
//            this.grpbxExpCtrl.Controls.Add(this.btnScanStart);
//            this.grpbxExpCtrl.Location = new System.Drawing.Point(752, 280);
//            this.grpbxExpCtrl.Name = "grpbxExpCtrl";
//            this.grpbxExpCtrl.Size = new System.Drawing.Size(488, 176);
//            this.grpbxExpCtrl.TabIndex = 14;
//            this.grpbxExpCtrl.TabStop = false;
//            this.grpbxExpCtrl.Text = "Experiment Control";
//            // 
//            // btnScanM1DX
//            // 
//            this.btnScanM1DX.Location = new System.Drawing.Point(232, 96);
//            this.btnScanM1DX.Name = "btnScanM1DX";
//            this.btnScanM1DX.Size = new System.Drawing.Size(70, 31);
//            this.btnScanM1DX.TabIndex = 50;
//            this.btnScanM1DX.Text = "ScanM1DX";
//            this.btnScanM1DX.UseVisualStyleBackColor = true;
//            this.btnScanM1DX.Click += new System.EventHandler(this.btnScanM1DX_Click);
//            // 
//            // btnScan1DX
//            // 
//            this.btnScan1DX.Location = new System.Drawing.Point(232, 56);
//            this.btnScan1DX.Name = "btnScan1DX";
//            this.btnScan1DX.Size = new System.Drawing.Size(70, 31);
//            this.btnScan1DX.TabIndex = 49;
//            this.btnScan1DX.Text = "Scan1DX";
//            this.btnScan1DX.UseVisualStyleBackColor = true;
//            this.btnScan1DX.Click += new System.EventHandler(this.btnScan1DX_Click);
//            // 
//            // txtbxOverScanPx
//            // 
//            this.txtbxOverScanPx.Location = new System.Drawing.Point(416, 160);
//            this.txtbxOverScanPx.Name = "txtbxOverScanPx";
//            this.txtbxOverScanPx.Size = new System.Drawing.Size(59, 20);
//            this.txtbxOverScanPx.TabIndex = 48;
//            this.txtbxOverScanPx.Text = "0";
//            // 
//            // label5
//            // 
//            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.label5.AutoSize = true;
//            this.label5.Location = new System.Drawing.Point(312, 164);
//            this.label5.Name = "label5";
//            this.label5.Size = new System.Drawing.Size(65, 13);
//            this.label5.TabIndex = 47;
//            this.label5.Text = "OverscanPx";
//            // 
//            // txtbxGoToY
//            // 
//            this.txtbxGoToY.Location = new System.Drawing.Point(416, 133);
//            this.txtbxGoToY.Name = "txtbxGoToY";
//            this.txtbxGoToY.Size = new System.Drawing.Size(59, 20);
//            this.txtbxGoToY.TabIndex = 46;
//            this.txtbxGoToY.Text = "0";
//            // 
//            // label4
//            // 
//            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.label4.AutoSize = true;
//            this.label4.Location = new System.Drawing.Point(312, 137);
//            this.label4.Name = "label4";
//            this.label4.Size = new System.Drawing.Size(37, 13);
//            this.label4.TabIndex = 45;
//            this.label4.Text = "GotoY";
//            // 
//            // txtbxGoToX
//            // 
//            this.txtbxGoToX.Location = new System.Drawing.Point(416, 107);
//            this.txtbxGoToX.Name = "txtbxGoToX";
//            this.txtbxGoToX.Size = new System.Drawing.Size(59, 20);
//            this.txtbxGoToX.TabIndex = 44;
//            this.txtbxGoToX.Text = "0";
//            // 
//            // label3
//            // 
//            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(312, 111);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(37, 13);
//            this.label3.TabIndex = 43;
//            this.label3.Text = "GotoX";
//            // 
//            // btnMoveAbs
//            // 
//            this.btnMoveAbs.Location = new System.Drawing.Point(232, 16);
//            this.btnMoveAbs.Name = "btnMoveAbs";
//            this.btnMoveAbs.Size = new System.Drawing.Size(70, 31);
//            this.btnMoveAbs.TabIndex = 42;
//            this.btnMoveAbs.Text = "Move Abs";
//            this.btnMoveAbs.UseVisualStyleBackColor = true;
//            this.btnMoveAbs.Click += new System.EventHandler(this.btnMoveAbs_Click);
//            // 
//            // txtbxSampleDelta
//            // 
//            this.txtbxSampleDelta.Location = new System.Drawing.Point(416, 78);
//            this.txtbxSampleDelta.Name = "txtbxSampleDelta";
//            this.txtbxSampleDelta.ReadOnly = true;
//            this.txtbxSampleDelta.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSampleDelta.TabIndex = 41;
//            this.txtbxSampleDelta.Text = "0";
//            // 
//            // lblSampleDelta
//            // 
//            this.lblSampleDelta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSampleDelta.AutoSize = true;
//            this.lblSampleDelta.Location = new System.Drawing.Point(312, 82);
//            this.lblSampleDelta.Name = "lblSampleDelta";
//            this.lblSampleDelta.Size = new System.Drawing.Size(70, 13);
//            this.lblSampleDelta.TabIndex = 40;
//            this.lblSampleDelta.Text = "Sample Delta";
//            // 
//            // txtbxSamplesFromAPD
//            // 
//            this.txtbxSamplesFromAPD.Location = new System.Drawing.Point(416, 52);
//            this.txtbxSamplesFromAPD.Name = "txtbxSamplesFromAPD";
//            this.txtbxSamplesFromAPD.ReadOnly = true;
//            this.txtbxSamplesFromAPD.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSamplesFromAPD.TabIndex = 39;
//            this.txtbxSamplesFromAPD.Text = "0";
//            // 
//            // lblSamplesFromAPD
//            // 
//            this.lblSamplesFromAPD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSamplesFromAPD.AutoSize = true;
//            this.lblSamplesFromAPD.Location = new System.Drawing.Point(312, 56);
//            this.lblSamplesFromAPD.Name = "lblSamplesFromAPD";
//            this.lblSamplesFromAPD.Size = new System.Drawing.Size(98, 13);
//            this.lblSamplesFromAPD.TabIndex = 38;
//            this.lblSamplesFromAPD.Text = "Samples from APD:";
//            // 
//            // txtbxSamplesToStage
//            // 
//            this.txtbxSamplesToStage.Location = new System.Drawing.Point(416, 20);
//            this.txtbxSamplesToStage.Name = "txtbxSamplesToStage";
//            this.txtbxSamplesToStage.ReadOnly = true;
//            this.txtbxSamplesToStage.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSamplesToStage.TabIndex = 37;
//            this.txtbxSamplesToStage.Text = "0";
//            // 
//            // lblSamplesToStage
//            // 
//            this.lblSamplesToStage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSamplesToStage.AutoSize = true;
//            this.lblSamplesToStage.Location = new System.Drawing.Point(312, 24);
//            this.lblSamplesToStage.Name = "lblSamplesToStage";
//            this.lblSamplesToStage.Size = new System.Drawing.Size(97, 13);
//            this.lblSamplesToStage.TabIndex = 36;
//            this.lblSamplesToStage.Text = "Samples To Stage:";
//            // 
//            // btnStop
//            // 
//            this.btnStop.Location = new System.Drawing.Point(160, 96);
//            this.btnStop.Name = "btnStop";
//            this.btnStop.Size = new System.Drawing.Size(70, 31);
//            this.btnStop.TabIndex = 35;
//            this.btnStop.Text = "STOP";
//            this.btnStop.UseVisualStyleBackColor = true;
//            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
//            // 
//            // btnZeroStage
//            // 
//            this.btnZeroStage.Location = new System.Drawing.Point(161, 136);
//            this.btnZeroStage.Name = "btnZeroStage";
//            this.btnZeroStage.Size = new System.Drawing.Size(70, 31);
//            this.btnZeroStage.TabIndex = 34;
//            this.btnZeroStage.Text = "Zero Stage";
//            this.btnZeroStage.UseVisualStyleBackColor = true;
//            this.btnZeroStage.Click += new System.EventHandler(this.btnZeroStage_Click);
//            // 
//            // btnValidateInput
//            // 
//            this.btnValidateInput.Location = new System.Drawing.Point(160, 16);
//            this.btnValidateInput.Name = "btnValidateInput";
//            this.btnValidateInput.Size = new System.Drawing.Size(70, 31);
//            this.btnValidateInput.TabIndex = 33;
//            this.btnValidateInput.Text = "Validate";
//            this.btnValidateInput.UseVisualStyleBackColor = true;
//            this.btnValidateInput.Click += new System.EventHandler(this.btnValidateInput_Click);
//            // 
//            // txtbxSetTimePPixel
//            // 
//            this.txtbxSetTimePPixel.Location = new System.Drawing.Point(96, 148);
//            this.txtbxSetTimePPixel.Name = "txtbxSetTimePPixel";
//            this.txtbxSetTimePPixel.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSetTimePPixel.TabIndex = 32;
//            this.txtbxSetTimePPixel.Text = "0";
//            validationRule21.DataType = KUL.MDS.Validation.ValidationDataType.Integer;
//            validationRule21.InitialValue = "0";
//            validationRule21.IsCaseSensitive = false;
//            validationRule21.IsRequired = true;
//            validationRule21.MaximumValue = "100";
//            validationRule21.MinimumValue = "1";
//            validationRule21.Operator = KUL.MDS.Validation.ValidationCompareOperator.GreaterThan;
//            validationRule21.ValueToCompare = "0";
//            this.valprovRISValidationProvider.SetValidationRule(this.txtbxSetTimePPixel, validationRule21);
//            // 
//            // lblSetTimePPixel
//            // 
//            this.lblSetTimePPixel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSetTimePPixel.AutoSize = true;
//            this.lblSetTimePPixel.Location = new System.Drawing.Point(8, 152);
//            this.lblSetTimePPixel.Name = "lblSetTimePPixel";
//            this.lblSetTimePPixel.Size = new System.Drawing.Size(82, 13);
//            this.lblSetTimePPixel.TabIndex = 31;
//            this.lblSetTimePPixel.Text = "Time/Pixel (ms):";
//            // 
//            // txtbxSetImageWidthnm
//            // 
//            this.txtbxSetImageWidthnm.Location = new System.Drawing.Point(96, 116);
//            this.txtbxSetImageWidthnm.Name = "txtbxSetImageWidthnm";
//            this.txtbxSetImageWidthnm.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSetImageWidthnm.TabIndex = 30;
//            this.txtbxSetImageWidthnm.Text = "0";
//            validationRule22.DataType = KUL.MDS.Validation.ValidationDataType.Integer;
//            validationRule22.InitialValue = "0";
//            validationRule22.IsCaseSensitive = false;
//            validationRule22.MaximumValue = "90000";
//            validationRule22.MinimumValue = "10";
//            validationRule22.Operator = KUL.MDS.Validation.ValidationCompareOperator.GreaterThan;
//            validationRule22.ValueToCompare = "9";
//            this.valprovRISValidationProvider.SetValidationRule(this.txtbxSetImageWidthnm, validationRule22);
//            // 
//            // lblSetImageWidthInnm
//            // 
//            this.lblSetImageWidthInnm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSetImageWidthInnm.AutoSize = true;
//            this.lblSetImageWidthInnm.Location = new System.Drawing.Point(8, 120);
//            this.lblSetImageWidthInnm.Name = "lblSetImageWidthInnm";
//            this.lblSetImageWidthInnm.Size = new System.Drawing.Size(81, 13);
//            this.lblSetImageWidthInnm.TabIndex = 29;
//            this.lblSetImageWidthInnm.Text = "Img Width (nm):";
//            // 
//            // txtbxSetInitY
//            // 
//            this.txtbxSetInitY.Location = new System.Drawing.Point(96, 84);
//            this.txtbxSetInitY.Name = "txtbxSetInitY";
//            this.txtbxSetInitY.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSetInitY.TabIndex = 28;
//            this.txtbxSetInitY.Text = "0";
//            validationRule23.DataType = KUL.MDS.Validation.ValidationDataType.Integer;
//            validationRule23.InitialValue = "0";
//            validationRule23.IsCaseSensitive = false;
//            validationRule23.MaximumValue = "90000";
//            validationRule23.MinimumValue = "0";
//            validationRule23.Operator = KUL.MDS.Validation.ValidationCompareOperator.GreaterThanEqual;
//            validationRule23.ValueToCompare = "0";
//            this.valprovRISValidationProvider.SetValidationRule(this.txtbxSetInitY, validationRule23);
//            // 
//            // lblSetInitY
//            // 
//            this.lblSetInitY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSetInitY.AutoSize = true;
//            this.lblSetInitY.Location = new System.Drawing.Point(8, 88);
//            this.lblSetInitY.Name = "lblSetInitY";
//            this.lblSetInitY.Size = new System.Drawing.Size(67, 13);
//            this.lblSetInitY.TabIndex = 27;
//            this.lblSetInitY.Text = "Initial Y (nm):";
//            // 
//            // txtbxSetInitX
//            // 
//            this.txtbxSetInitX.Location = new System.Drawing.Point(96, 52);
//            this.txtbxSetInitX.Name = "txtbxSetInitX";
//            this.txtbxSetInitX.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSetInitX.TabIndex = 26;
//            this.txtbxSetInitX.Text = "0";
//            validationRule24.DataType = KUL.MDS.Validation.ValidationDataType.Integer;
//            validationRule24.InitialValue = "0";
//            validationRule24.IsCaseSensitive = false;
//            validationRule24.MaximumValue = "90000";
//            validationRule24.MinimumValue = "0";
//            validationRule24.Operator = KUL.MDS.Validation.ValidationCompareOperator.GreaterThanEqual;
//            validationRule24.ValueToCompare = "0";
//            this.valprovRISValidationProvider.SetValidationRule(this.txtbxSetInitX, validationRule24);
//            // 
//            // lblSetInitX
//            // 
//            this.lblSetInitX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSetInitX.AutoSize = true;
//            this.lblSetInitX.Location = new System.Drawing.Point(8, 56);
//            this.lblSetInitX.Name = "lblSetInitX";
//            this.lblSetInitX.Size = new System.Drawing.Size(67, 13);
//            this.lblSetInitX.TabIndex = 25;
//            this.lblSetInitX.Text = "Initial X (nm):";
//            // 
//            // txtbxSetImageWidth
//            // 
//            this.txtbxSetImageWidth.Location = new System.Drawing.Point(96, 20);
//            this.txtbxSetImageWidth.Name = "txtbxSetImageWidth";
//            this.txtbxSetImageWidth.Size = new System.Drawing.Size(59, 20);
//            this.txtbxSetImageWidth.TabIndex = 24;
//            this.txtbxSetImageWidth.Text = "0";
//            validationRule25.DataType = KUL.MDS.Validation.ValidationDataType.Integer;
//            validationRule25.InitialValue = "0";
//            validationRule25.IsCaseSensitive = false;
//            validationRule25.IsRequired = true;
//            validationRule25.MaximumValue = "512";
//            validationRule25.MinimumValue = "1";
//            validationRule25.Operator = KUL.MDS.Validation.ValidationCompareOperator.GreaterThan;
//            validationRule25.ValueToCompare = "0";
//            this.valprovRISValidationProvider.SetValidationRule(this.txtbxSetImageWidth, validationRule25);
//            // 
//            // lblSetImageWidth
//            // 
//            this.lblSetImageWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblSetImageWidth.AutoSize = true;
//            this.lblSetImageWidth.Location = new System.Drawing.Point(8, 24);
//            this.lblSetImageWidth.Name = "lblSetImageWidth";
//            this.lblSetImageWidth.Size = new System.Drawing.Size(78, 13);
//            this.lblSetImageWidth.TabIndex = 23;
//            this.lblSetImageWidth.Text = "Img Width (px):";
//            // 
//            // btnScanStart
//            // 
//            this.btnScanStart.Location = new System.Drawing.Point(160, 56);
//            this.btnScanStart.Name = "btnScanStart";
//            this.btnScanStart.Size = new System.Drawing.Size(70, 31);
//            this.btnScanStart.TabIndex = 14;
//            this.btnScanStart.Text = "Scan";
//            this.btnScanStart.UseVisualStyleBackColor = true;
//            this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
//            // 
//            // btnImageFit
//            // 
//            this.btnImageFit.Location = new System.Drawing.Point(8, 24);
//            this.btnImageFit.Name = "btnImageFit";
//            this.btnImageFit.Size = new System.Drawing.Size(70, 31);
//            this.btnImageFit.TabIndex = 33;
//            this.btnImageFit.Text = "Full Image";
//            this.btnImageFit.UseVisualStyleBackColor = true;
//            this.btnImageFit.Click += new System.EventHandler(this.btnImageFit_Click);
//            // 
//            // groupBox4
//            // 
//            this.groupBox4.Controls.Add(this.txtbxScanTime);
//            this.groupBox4.Controls.Add(this.txtbxOverScan);
//            this.groupBox4.Controls.Add(this.txtbxInitY);
//            this.groupBox4.Controls.Add(this.txtbxInitX);
//            this.groupBox4.Controls.Add(this.txtbxTimePPixel);
//            this.groupBox4.Controls.Add(this.txtbxImageWidth);
//            this.groupBox4.Controls.Add(this.lblScanTime);
//            this.groupBox4.Controls.Add(this.lblOverScan);
//            this.groupBox4.Controls.Add(this.lblInitY);
//            this.groupBox4.Controls.Add(this.lblInitX);
//            this.groupBox4.Controls.Add(this.lblImageWidth);
//            this.groupBox4.Controls.Add(this.lblTimePPixel);
//            this.groupBox4.Controls.Add(this.txtbxImageSize);
//            this.groupBox4.Controls.Add(this.txtbxDateTime);
//            this.groupBox4.Controls.Add(this.lblImageSize);
//            this.groupBox4.Controls.Add(this.lblDateTime);
//            this.groupBox4.Location = new System.Drawing.Point(752, 464);
//            this.groupBox4.Name = "groupBox4";
//            this.groupBox4.Size = new System.Drawing.Size(488, 160);
//            this.groupBox4.TabIndex = 15;
//            this.groupBox4.TabStop = false;
//            this.groupBox4.Text = "Experiment Properties";
//            // 
//            // txtbxScanTime
//            // 
//            this.txtbxScanTime.Location = new System.Drawing.Point(231, 116);
//            this.txtbxScanTime.Name = "txtbxScanTime";
//            this.txtbxScanTime.ReadOnly = true;
//            this.txtbxScanTime.Size = new System.Drawing.Size(78, 20);
//            this.txtbxScanTime.TabIndex = 27;
//            // 
//            // txtbxOverScan
//            // 
//            this.txtbxOverScan.Location = new System.Drawing.Point(231, 84);
//            this.txtbxOverScan.Name = "txtbxOverScan";
//            this.txtbxOverScan.ReadOnly = true;
//            this.txtbxOverScan.Size = new System.Drawing.Size(78, 20);
//            this.txtbxOverScan.TabIndex = 26;
//            // 
//            // txtbxInitY
//            // 
//            this.txtbxInitY.Location = new System.Drawing.Point(231, 52);
//            this.txtbxInitY.Name = "txtbxInitY";
//            this.txtbxInitY.ReadOnly = true;
//            this.txtbxInitY.Size = new System.Drawing.Size(78, 20);
//            this.txtbxInitY.TabIndex = 25;
//            // 
//            // txtbxInitX
//            // 
//            this.txtbxInitX.Location = new System.Drawing.Point(231, 20);
//            this.txtbxInitX.Name = "txtbxInitX";
//            this.txtbxInitX.ReadOnly = true;
//            this.txtbxInitX.Size = new System.Drawing.Size(78, 20);
//            this.txtbxInitX.TabIndex = 24;
//            // 
//            // txtbxTimePPixel
//            // 
//            this.txtbxTimePPixel.Location = new System.Drawing.Point(74, 116);
//            this.txtbxTimePPixel.Name = "txtbxTimePPixel";
//            this.txtbxTimePPixel.ReadOnly = true;
//            this.txtbxTimePPixel.Size = new System.Drawing.Size(78, 20);
//            this.txtbxTimePPixel.TabIndex = 23;
//            // 
//            // txtbxImageWidth
//            // 
//            this.txtbxImageWidth.Location = new System.Drawing.Point(74, 84);
//            this.txtbxImageWidth.Name = "txtbxImageWidth";
//            this.txtbxImageWidth.ReadOnly = true;
//            this.txtbxImageWidth.Size = new System.Drawing.Size(78, 20);
//            this.txtbxImageWidth.TabIndex = 22;
//            // 
//            // lblScanTime
//            // 
//            this.lblScanTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblScanTime.AutoSize = true;
//            this.lblScanTime.Location = new System.Drawing.Point(158, 120);
//            this.lblScanTime.Name = "lblScanTime";
//            this.lblScanTime.Size = new System.Drawing.Size(61, 13);
//            this.lblScanTime.TabIndex = 21;
//            this.lblScanTime.Text = "Scan Time:";
//            // 
//            // lblOverScan
//            // 
//            this.lblOverScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblOverScan.AutoSize = true;
//            this.lblOverScan.Location = new System.Drawing.Point(158, 88);
//            this.lblOverScan.Name = "lblOverScan";
//            this.lblOverScan.Size = new System.Drawing.Size(56, 13);
//            this.lblOverScan.TabIndex = 20;
//            this.lblOverScan.Text = "Overscan:";
//            // 
//            // lblInitY
//            // 
//            this.lblInitY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblInitY.AutoSize = true;
//            this.lblInitY.Location = new System.Drawing.Point(158, 56);
//            this.lblInitY.Name = "lblInitY";
//            this.lblInitY.Size = new System.Drawing.Size(34, 13);
//            this.lblInitY.TabIndex = 19;
//            this.lblInitY.Text = "Init Y:";
//            // 
//            // lblInitX
//            // 
//            this.lblInitX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblInitX.AutoSize = true;
//            this.lblInitX.Location = new System.Drawing.Point(158, 24);
//            this.lblInitX.Name = "lblInitX";
//            this.lblInitX.Size = new System.Drawing.Size(34, 13);
//            this.lblInitX.TabIndex = 18;
//            this.lblInitX.Text = "Init X:";
//            // 
//            // lblImageWidth
//            // 
//            this.lblImageWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblImageWidth.AutoSize = true;
//            this.lblImageWidth.Location = new System.Drawing.Point(6, 88);
//            this.lblImageWidth.Name = "lblImageWidth";
//            this.lblImageWidth.Size = new System.Drawing.Size(58, 13);
//            this.lblImageWidth.TabIndex = 17;
//            this.lblImageWidth.Text = "Img Width:";
//            // 
//            // lblTimePPixel
//            // 
//            this.lblTimePPixel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblTimePPixel.AutoSize = true;
//            this.lblTimePPixel.Location = new System.Drawing.Point(6, 120);
//            this.lblTimePPixel.Name = "lblTimePPixel";
//            this.lblTimePPixel.Size = new System.Drawing.Size(60, 13);
//            this.lblTimePPixel.TabIndex = 16;
//            this.lblTimePPixel.Text = "Time/Pixel:";
//            // 
//            // txtbxImageSize
//            // 
//            this.txtbxImageSize.Location = new System.Drawing.Point(74, 52);
//            this.txtbxImageSize.Name = "txtbxImageSize";
//            this.txtbxImageSize.ReadOnly = true;
//            this.txtbxImageSize.Size = new System.Drawing.Size(78, 20);
//            this.txtbxImageSize.TabIndex = 15;
//            // 
//            // txtbxDateTime
//            // 
//            this.txtbxDateTime.Location = new System.Drawing.Point(74, 20);
//            this.txtbxDateTime.Name = "txtbxDateTime";
//            this.txtbxDateTime.ReadOnly = true;
//            this.txtbxDateTime.Size = new System.Drawing.Size(78, 20);
//            this.txtbxDateTime.TabIndex = 14;
//            // 
//            // lblColorBarMaxInt
//            // 
//            this.lblColorBarMaxInt.AutoSize = true;
//            this.lblColorBarMaxInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblColorBarMaxInt.Location = new System.Drawing.Point(48, 8);
//            this.lblColorBarMaxInt.Name = "lblColorBarMaxInt";
//            this.lblColorBarMaxInt.Size = new System.Drawing.Size(38, 20);
//            this.lblColorBarMaxInt.TabIndex = 18;
//            this.lblColorBarMaxInt.Text = "Max";
//            // 
//            // lblColorBarMinInt
//            // 
//            this.lblColorBarMinInt.AutoSize = true;
//            this.lblColorBarMinInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblColorBarMinInt.Location = new System.Drawing.Point(48, 496);
//            this.lblColorBarMinInt.Name = "lblColorBarMinInt";
//            this.lblColorBarMinInt.Size = new System.Drawing.Size(34, 20);
//            this.lblColorBarMinInt.TabIndex = 19;
//            this.lblColorBarMinInt.Text = "Min";
//            // 
//            // lblYAxisMax
//            // 
//            this.lblYAxisMax.AutoSize = true;
//            this.lblYAxisMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblYAxisMax.Location = new System.Drawing.Point(672, 496);
//            this.lblYAxisMax.Name = "lblYAxisMax";
//            this.lblYAxisMax.Size = new System.Drawing.Size(49, 20);
//            this.lblYAxisMax.TabIndex = 20;
//            this.lblYAxisMax.Text = "YMax";
//            this.lblYAxisMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblYAxisMin
//            // 
//            this.lblYAxisMin.AutoSize = true;
//            this.lblYAxisMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblYAxisMin.Location = new System.Drawing.Point(672, 8);
//            this.lblYAxisMin.Name = "lblYAxisMin";
//            this.lblYAxisMin.Size = new System.Drawing.Size(45, 20);
//            this.lblYAxisMin.TabIndex = 21;
//            this.lblYAxisMin.Text = "YMin";
//            this.lblYAxisMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblXAxisMax
//            // 
//            this.lblXAxisMax.AutoSize = true;
//            this.lblXAxisMax.BackColor = System.Drawing.SystemColors.Control;
//            this.lblXAxisMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblXAxisMax.Location = new System.Drawing.Point(672, 528);
//            this.lblXAxisMax.Name = "lblXAxisMax";
//            this.lblXAxisMax.Size = new System.Drawing.Size(49, 20);
//            this.lblXAxisMax.TabIndex = 22;
//            this.lblXAxisMax.Text = "XMax";
//            this.lblXAxisMax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblXAxisMin
//            // 
//            this.lblXAxisMin.AutoSize = true;
//            this.lblXAxisMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblXAxisMin.Location = new System.Drawing.Point(152, 528);
//            this.lblXAxisMin.Name = "lblXAxisMin";
//            this.lblXAxisMin.Size = new System.Drawing.Size(45, 20);
//            this.lblXAxisMin.TabIndex = 23;
//            this.lblXAxisMin.Text = "XMin";
//            this.lblXAxisMin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblXAxis
//            // 
//            this.lblXAxis.AutoSize = true;
//            this.lblXAxis.BackColor = System.Drawing.SystemColors.Control;
//            this.lblXAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblXAxis.Location = new System.Drawing.Point(400, 528);
//            this.lblXAxis.Name = "lblXAxis";
//            this.lblXAxis.Size = new System.Drawing.Size(21, 20);
//            this.lblXAxis.TabIndex = 24;
//            this.lblXAxis.Text = "X";
//            this.lblXAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // lblYAxis
//            // 
//            this.lblYAxis.AutoSize = true;
//            this.lblYAxis.BackColor = System.Drawing.SystemColors.Control;
//            this.lblYAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblYAxis.Location = new System.Drawing.Point(672, 256);
//            this.lblYAxis.Name = "lblYAxis";
//            this.lblYAxis.Size = new System.Drawing.Size(21, 20);
//            this.lblYAxis.TabIndex = 25;
//            this.lblYAxis.Text = "Y";
//            this.lblYAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // grpbxStageStatus
//            // 
//            this.grpbxStageStatus.Controls.Add(this.btnStageOFF);
//            this.grpbxStageStatus.Controls.Add(this.btnStageON);
//            this.grpbxStageStatus.Controls.Add(this.lblStageVoltageEngaged);
//            this.grpbxStageStatus.Controls.Add(this.txtbxYVoltCurr);
//            this.grpbxStageStatus.Controls.Add(this.txtbxXVoltCurr);
//            this.grpbxStageStatus.Controls.Add(this.lblStageCurrYVoltage);
//            this.grpbxStageStatus.Controls.Add(this.lblStageCurrXVoltage);
//            this.grpbxStageStatus.Location = new System.Drawing.Point(752, 144);
//            this.grpbxStageStatus.Name = "grpbxStageStatus";
//            this.grpbxStageStatus.Size = new System.Drawing.Size(488, 128);
//            this.grpbxStageStatus.TabIndex = 26;
//            this.grpbxStageStatus.TabStop = false;
//            this.grpbxStageStatus.Text = "Stage Status";
//            // 
//            // btnStageOFF
//            // 
//            this.btnStageOFF.Location = new System.Drawing.Point(8, 88);
//            this.btnStageOFF.Name = "btnStageOFF";
//            this.btnStageOFF.Size = new System.Drawing.Size(70, 31);
//            this.btnStageOFF.TabIndex = 35;
//            this.btnStageOFF.Text = "OFF";
//            this.btnStageOFF.UseVisualStyleBackColor = true;
//            this.btnStageOFF.Click += new System.EventHandler(this.btnStageOFF_Click);
//            // 
//            // btnStageON
//            // 
//            this.btnStageON.Location = new System.Drawing.Point(8, 40);
//            this.btnStageON.Name = "btnStageON";
//            this.btnStageON.Size = new System.Drawing.Size(70, 31);
//            this.btnStageON.TabIndex = 34;
//            this.btnStageON.Text = "ON";
//            this.btnStageON.UseVisualStyleBackColor = true;
//            this.btnStageON.Click += new System.EventHandler(this.btnStageON_Click);
//            // 
//            // lblStageVoltageEngaged
//            // 
//            this.lblStageVoltageEngaged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.lblStageVoltageEngaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.lblStageVoltageEngaged.ForeColor = System.Drawing.Color.Red;
//            this.lblStageVoltageEngaged.Location = new System.Drawing.Point(80, 40);
//            this.lblStageVoltageEngaged.Name = "lblStageVoltageEngaged";
//            this.lblStageVoltageEngaged.Size = new System.Drawing.Size(400, 80);
//            this.lblStageVoltageEngaged.TabIndex = 26;
//            this.lblStageVoltageEngaged.Text = "STAGE";
//            this.lblStageVoltageEngaged.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // txtbxYVoltCurr
//            // 
//            this.txtbxYVoltCurr.Location = new System.Drawing.Point(322, 16);
//            this.txtbxYVoltCurr.Name = "txtbxYVoltCurr";
//            this.txtbxYVoltCurr.ReadOnly = true;
//            this.txtbxYVoltCurr.Size = new System.Drawing.Size(158, 20);
//            this.txtbxYVoltCurr.TabIndex = 25;
//            // 
//            // txtbxXVoltCurr
//            // 
//            this.txtbxXVoltCurr.Location = new System.Drawing.Point(80, 16);
//            this.txtbxXVoltCurr.Name = "txtbxXVoltCurr";
//            this.txtbxXVoltCurr.ReadOnly = true;
//            this.txtbxXVoltCurr.Size = new System.Drawing.Size(158, 20);
//            this.txtbxXVoltCurr.TabIndex = 24;
//            // 
//            // lblStageCurrYVoltage
//            // 
//            this.lblStageCurrYVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
//            this.lblStageCurrYVoltage.AutoSize = true;
//            this.lblStageCurrYVoltage.Location = new System.Drawing.Point(248, 20);
//            this.lblStageCurrYVoltage.Name = "lblStageCurrYVoltage";
//            this.lblStageCurrYVoltage.Size = new System.Drawing.Size(72, 13);
//            this.lblStageCurrYVoltage.TabIndex = 19;
//            this.lblStageCurrYVoltage.Text = "Y Voltage (V):";
//            // 
//            // lblStageCurrXVoltage
//            // 
//            this.lblStageCurrXVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
//            this.lblStageCurrXVoltage.AutoSize = true;
//            this.lblStageCurrXVoltage.Location = new System.Drawing.Point(8, 20);
//            this.lblStageCurrXVoltage.Name = "lblStageCurrXVoltage";
//            this.lblStageCurrXVoltage.Size = new System.Drawing.Size(72, 13);
//            this.lblStageCurrXVoltage.TabIndex = 18;
//            this.lblStageCurrXVoltage.Text = "X Voltage (V):";
//            // 
//            // valprovRISValidationProvider
//            // 
//            this.valprovRISValidationProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.BlinkIfDifferentError;
//            this.valprovRISValidationProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("valprovRISValidationProvider.Icon")));
//            // 
//            // groupBox5
//            // 
//            this.groupBox5.Controls.Add(this.chkbxNormalized);
//            this.groupBox5.Controls.Add(this.chkbxCorrectedImage);
//            this.groupBox5.Controls.Add(this.btnImageFit);
//            this.groupBox5.Location = new System.Drawing.Point(152, 552);
//            this.groupBox5.Name = "groupBox5";
//            this.groupBox5.Size = new System.Drawing.Size(512, 72);
//            this.groupBox5.TabIndex = 27;
//            this.groupBox5.TabStop = false;
//            this.groupBox5.Text = "Image Manipulation";
//            // 
//            // chkbxNormalized
//            // 
//            this.chkbxNormalized.AutoSize = true;
//            this.chkbxNormalized.Location = new System.Drawing.Point(200, 32);
//            this.chkbxNormalized.Name = "chkbxNormalized";
//            this.chkbxNormalized.Size = new System.Drawing.Size(128, 17);
//            this.chkbxNormalized.TabIndex = 35;
//            this.chkbxNormalized.Text = "Normalized Intensities";
//            this.chkbxNormalized.UseVisualStyleBackColor = true;
//            this.chkbxNormalized.CheckedChanged += new System.EventHandler(this.chkbxNormalized_CheckedChanged);
//            // 
//            // chkbxCorrectedImage
//            // 
//            this.chkbxCorrectedImage.AutoSize = true;
//            this.chkbxCorrectedImage.Location = new System.Drawing.Point(88, 32);
//            this.chkbxCorrectedImage.Name = "chkbxCorrectedImage";
//            this.chkbxCorrectedImage.Size = new System.Drawing.Size(104, 17);
//            this.chkbxCorrectedImage.TabIndex = 34;
//            this.chkbxCorrectedImage.Text = "Corrected Image";
//            this.chkbxCorrectedImage.UseVisualStyleBackColor = true;
//            this.chkbxCorrectedImage.CheckedChanged += new System.EventHandler(this.chkbxCorrectedImage_CheckedChanged);
//            // 
//            // bckgwrkPerformScan
//            // 
//            this.bckgwrkPerformScan.WorkerSupportsCancellation = true;
//            this.bckgwrkPerformScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgwrkPerformScan_DoWork);
//            this.bckgwrkPerformScan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgwrkPerformScan_RunWorkerCompleted);
//            // 
//            // drwcnvColorBar
//            // 
//            this.drwcnvColorBar.BackColor = System.Drawing.Color.Gray;
//            this.drwcnvColorBar.Location = new System.Drawing.Point(8, 8);
//            this.drwcnvColorBar.Name = "drwcnvColorBar";
//            this.drwcnvColorBar.Origin = new System.Drawing.Point(0, 0);
//            this.drwcnvColorBar.PanButton = System.Windows.Forms.MouseButtons.Left;
//            this.drwcnvColorBar.PanMode = true;
//            this.drwcnvColorBar.Size = new System.Drawing.Size(30, 512);
//            this.drwcnvColorBar.StretchImageToFit = true;
//            this.drwcnvColorBar.TabIndex = 17;
//            this.drwcnvColorBar.ZoomFactor = 1;
//            this.drwcnvColorBar.ZoomOnMouseWheel = false;
//            // 
//            // drwcnvScanImage
//            // 
//            this.drwcnvScanImage.BackColor = System.Drawing.Color.Gray;
//            this.drwcnvScanImage.Location = new System.Drawing.Point(152, 8);
//            this.drwcnvScanImage.Name = "drwcnvScanImage";
//            this.drwcnvScanImage.Origin = new System.Drawing.Point(0, 0);
//            this.drwcnvScanImage.PanButton = System.Windows.Forms.MouseButtons.Left;
//            this.drwcnvScanImage.PanMode = true;
//            this.drwcnvScanImage.Size = new System.Drawing.Size(512, 512);
//            this.drwcnvScanImage.StretchImageToFit = false;
//            this.drwcnvScanImage.TabIndex = 16;
//            this.drwcnvScanImage.ZoomFactor = 1;
//            this.drwcnvScanImage.ZoomOnMouseWheel = true;
//            this.drwcnvScanImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drwcnvScanImage_MouseMove);
//            // 
//            // bckgwrkPerformMove
//            // 
//            this.bckgwrkPerformMove.WorkerReportsProgress = true;
//            this.bckgwrkPerformMove.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgwrkPerformMove_DoWork);
//            // 
//            // ScanViewForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1250, 633);
//            this.Controls.Add(this.groupBox5);
//            this.Controls.Add(this.grpbxStageStatus);
//            this.Controls.Add(this.lblYAxis);
//            this.Controls.Add(this.lblXAxis);
//            this.Controls.Add(this.lblXAxisMin);
//            this.Controls.Add(this.lblXAxisMax);
//            this.Controls.Add(this.lblYAxisMin);
//            this.Controls.Add(this.lblYAxisMax);
//            this.Controls.Add(this.lblColorBarMinInt);
//            this.Controls.Add(this.groupBox4);
//            this.Controls.Add(this.lblColorBarMaxInt);
//            this.Controls.Add(this.drwcnvColorBar);
//            this.Controls.Add(this.drwcnvScanImage);
//            this.Controls.Add(this.grpbxExpCtrl);
//            this.Controls.Add(this.grpbxLsrComms);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
//            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
//            this.MaximizeBox = false;
//            this.Name = "ScanViewForm";
//            this.Text = "ScanViewForm";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanViewForm_FormClosing);
//            this.grpbxLsrComms.ResumeLayout(false);
//            this.grpbxLsrComms.PerformLayout();
//            this.grpbxExpCtrl.ResumeLayout(false);
//            this.grpbxExpCtrl.PerformLayout();
//            this.groupBox4.ResumeLayout(false);
//            this.groupBox4.PerformLayout();
//            this.grpbxStageStatus.ResumeLayout(false);
//            this.grpbxStageStatus.PerformLayout();
//            this.groupBox5.ResumeLayout(false);
//            this.groupBox5.PerformLayout();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.Label lblDateTime;
//        private System.Windows.Forms.Label lblImageSize;
//        private System.Windows.Forms.TextBox txtbxPortStatus;
//        private System.Windows.Forms.GroupBox grpbxLsrComms;
//        private System.Windows.Forms.Button btnLsrClose;
//        private System.Windows.Forms.Button btnLsrOpen;
//        private System.Windows.Forms.Button btnLsrCW;
//        private System.Windows.Forms.Button btnLsrPow;
//        private System.Windows.Forms.Button btnLsrOFF;
//        private System.Windows.Forms.Button btnLsrON;
//        private System.Windows.Forms.TextBox txtbxLsrReply;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.GroupBox grpbxExpCtrl;
//        private System.Windows.Forms.GroupBox groupBox4;
//        private System.Windows.Forms.Label lblTimePPixel;
//        private System.Windows.Forms.TextBox txtbxImageSize;
//        private System.Windows.Forms.TextBox txtbxDateTime;
//        private System.Windows.Forms.TextBox txtbxScanTime;
//        private System.Windows.Forms.TextBox txtbxOverScan;
//        private System.Windows.Forms.TextBox txtbxInitY;
//        private System.Windows.Forms.TextBox txtbxInitX;
//        private System.Windows.Forms.TextBox txtbxTimePPixel;
//        private System.Windows.Forms.TextBox txtbxImageWidth;
//        private System.Windows.Forms.Label lblScanTime;
//        private System.Windows.Forms.Label lblOverScan;
//        private System.Windows.Forms.Label lblInitY;
//        private System.Windows.Forms.Label lblInitX;
//        private System.Windows.Forms.Label lblImageWidth;
//        private System.Windows.Forms.Button btnScanStart;
//        private System.Windows.Forms.TextBox txtbxSetImageWidth;
//        private System.Windows.Forms.Label lblSetImageWidth;
//        private KUL.MDS.Library.PanZoomImageControl drwcnvScanImage;
//        private System.Windows.Forms.TextBox txtbxSetTimePPixel;
//        private System.Windows.Forms.Label lblSetTimePPixel;
//        private System.Windows.Forms.TextBox txtbxSetImageWidthnm;
//        private System.Windows.Forms.Label lblSetImageWidthInnm;
//        private System.Windows.Forms.TextBox txtbxSetInitY;
//        private System.Windows.Forms.Label lblSetInitY;
//        private System.Windows.Forms.TextBox txtbxSetInitX;
//        private System.Windows.Forms.Label lblSetInitX;
//        private KUL.MDS.Library.PanZoomImageControl drwcnvColorBar;
//        private System.Windows.Forms.Label lblColorBarMaxInt;
//        private System.Windows.Forms.Label lblColorBarMinInt;
//        private System.Windows.Forms.Button btnImageFit;
//        private System.Windows.Forms.Label lblYAxisMax;
//        private System.Windows.Forms.Label lblYAxisMin;
//        private System.Windows.Forms.Label lblXAxisMax;
//        private System.Windows.Forms.Label lblXAxisMin;
//        private System.Windows.Forms.Label lblXAxis;
//        private System.Windows.Forms.Label lblYAxis;
//        private System.Windows.Forms.GroupBox grpbxStageStatus;
//        private System.Windows.Forms.TextBox txtbxYVoltCurr;
//        private System.Windows.Forms.TextBox txtbxXVoltCurr;
//        private System.Windows.Forms.Label lblStageCurrYVoltage;
//        private System.Windows.Forms.Label lblStageCurrXVoltage;
//        private KUL.MDS.Validation.ValidationProvider valprovRISValidationProvider;
//        private System.Windows.Forms.Label lblStageVoltageEngaged;
//        private System.Windows.Forms.Button btnValidateInput;
//        private System.Windows.Forms.GroupBox groupBox5;
//        private System.Windows.Forms.Button btnStageOFF;
//        private System.Windows.Forms.Button btnStageON;
//        private System.ComponentModel.BackgroundWorker bckgwrkPerformScan;
//        private System.Windows.Forms.Button btnZeroStage;
//        private System.ComponentModel.BackgroundWorker bckgwrkPerformMove;
//        private System.Windows.Forms.Button btnStop;
//        private System.Windows.Forms.TextBox txtbxSamplesFromAPD;
//        private System.Windows.Forms.Label lblSamplesFromAPD;
//        private System.Windows.Forms.TextBox txtbxSamplesToStage;
//        private System.Windows.Forms.Label lblSamplesToStage;
//        private System.Windows.Forms.TextBox txtbxSampleDelta;
//        private System.Windows.Forms.Label lblSampleDelta;
//        private System.Windows.Forms.Button btnMoveAbs;
//        private System.Windows.Forms.TextBox txtbxGoToX;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.TextBox txtbxGoToY;
//        private System.Windows.Forms.Label label4;
//        private System.Windows.Forms.TextBox txtbxOverScanPx;
//        private System.Windows.Forms.Label label5;
//        private System.Windows.Forms.Button btnScan1DX;
//        private System.Windows.Forms.Button btnScanM1DX;
//        private System.Windows.Forms.CheckBox chkbxCorrectedImage;
//        private System.Windows.Forms.CheckBox chkbxNormalized;
//    }
//}