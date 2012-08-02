namespace PI_Digital_Stage_Test_Framework
{
    partial class MainForm
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
            this.grpbxExpCtrl = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnMoveAbs = new System.Windows.Forms.Button();
            this.txtbxGoToY = new System.Windows.Forms.TextBox();
            this.lblStageVoltageEngaged = new System.Windows.Forms.Label();
            this.btnStageOFF = new System.Windows.Forms.Button();
            this.txtbxCurrYPos = new System.Windows.Forms.TextBox();
            this.txtbxOverScanPx = new System.Windows.Forms.TextBox();
            this.txtbxCurrXPos = new System.Windows.Forms.TextBox();
            this.btnStageON = new System.Windows.Forms.Button();
            this.lblStageCurrYPos = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStageCurrXPos = new System.Windows.Forms.Label();
            this.txtbxSetImageWidth = new System.Windows.Forms.TextBox();
            this.txtbxSetInitX = new System.Windows.Forms.TextBox();
            this.txtbxSetInitY = new System.Windows.Forms.TextBox();
            this.txtbxGoToX = new System.Windows.Forms.TextBox();
            this.txtbxSetImageWidthnm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbxSetTimePPixel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSetImageWidth = new System.Windows.Forms.Label();
            this.lblSetInitX = new System.Windows.Forms.Label();
            this.lblSetInitY = new System.Windows.Forms.Label();
            this.lblSetImageWidthInnm = new System.Windows.Forms.Label();
            this.lblSetTimePPixel = new System.Windows.Forms.Label();
            this.btnScanStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.bckgwrkPerformScan = new System.ComponentModel.BackgroundWorker();
            this.grpbxExpCtrl.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbxExpCtrl
            // 
            this.grpbxExpCtrl.Controls.Add(this.tableLayoutPanel1);
            this.grpbxExpCtrl.Location = new System.Drawing.Point(12, 12);
            this.grpbxExpCtrl.Name = "grpbxExpCtrl";
            this.grpbxExpCtrl.Size = new System.Drawing.Size(552, 246);
            this.grpbxExpCtrl.TabIndex = 38;
            this.grpbxExpCtrl.TabStop = false;
            this.grpbxExpCtrl.Text = "Experiment Control";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.btnMoveAbs, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtbxGoToY, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblStageVoltageEngaged, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStageOFF, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtbxCurrYPos, 3, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtbxOverScanPx, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtbxCurrXPos, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnStageON, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStageCurrYPos, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblStageCurrXPos, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSetImageWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSetInitX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSetInitY, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtbxGoToX, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSetImageWidthnm, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.txtbxSetTimePPixel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblSetImageWidth, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblSetInitX, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSetInitY, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblSetImageWidthInnm, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblSetTimePPixel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnScanStart, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnStop, 2, 4);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 227);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // btnMoveAbs
            // 
            this.btnMoveAbs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMoveAbs.Location = new System.Drawing.Point(275, 171);
            this.btnMoveAbs.Name = "btnMoveAbs";
            this.tableLayoutPanel1.SetRowSpan(this.btnMoveAbs, 2);
            this.btnMoveAbs.Size = new System.Drawing.Size(130, 53);
            this.btnMoveAbs.TabIndex = 51;
            this.btnMoveAbs.Text = "Move Abs";
            this.btnMoveAbs.UseVisualStyleBackColor = true;
            this.btnMoveAbs.Click += new System.EventHandler(this.btnMoveAbs_Click);
            // 
            // txtbxGoToY
            // 
            this.txtbxGoToY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxGoToY.Location = new System.Drawing.Point(139, 199);
            this.txtbxGoToY.Name = "txtbxGoToY";
            this.txtbxGoToY.Size = new System.Drawing.Size(130, 20);
            this.txtbxGoToY.TabIndex = 8;
            this.txtbxGoToY.Text = "0";
            // 
            // lblStageVoltageEngaged
            // 
            this.lblStageVoltageEngaged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStageVoltageEngaged.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStageVoltageEngaged.ForeColor = System.Drawing.Color.Red;
            this.lblStageVoltageEngaged.Location = new System.Drawing.Point(411, 56);
            this.lblStageVoltageEngaged.Name = "lblStageVoltageEngaged";
            this.lblStageVoltageEngaged.Size = new System.Drawing.Size(132, 28);
            this.lblStageVoltageEngaged.TabIndex = 26;
            this.lblStageVoltageEngaged.Text = "STAGE";
            this.lblStageVoltageEngaged.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStageOFF
            // 
            this.btnStageOFF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStageOFF.Location = new System.Drawing.Point(275, 31);
            this.btnStageOFF.Name = "btnStageOFF";
            this.btnStageOFF.Size = new System.Drawing.Size(130, 22);
            this.btnStageOFF.TabIndex = 35;
            this.btnStageOFF.Text = "OFF";
            this.btnStageOFF.UseVisualStyleBackColor = true;
            this.btnStageOFF.Click += new System.EventHandler(this.btnStageOFF_Click);
            // 
            // txtbxCurrYPos
            // 
            this.txtbxCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxCurrYPos.Location = new System.Drawing.Point(411, 171);
            this.txtbxCurrYPos.Name = "txtbxCurrYPos";
            this.txtbxCurrYPos.ReadOnly = true;
            this.txtbxCurrYPos.Size = new System.Drawing.Size(132, 20);
            this.txtbxCurrYPos.TabIndex = 25;
            // 
            // txtbxOverScanPx
            // 
            this.txtbxOverScanPx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxOverScanPx.Location = new System.Drawing.Point(139, 143);
            this.txtbxOverScanPx.Name = "txtbxOverScanPx";
            this.txtbxOverScanPx.Size = new System.Drawing.Size(130, 20);
            this.txtbxOverScanPx.TabIndex = 6;
            this.txtbxOverScanPx.Text = "0";
            // 
            // txtbxCurrXPos
            // 
            this.txtbxCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxCurrXPos.Location = new System.Drawing.Point(411, 115);
            this.txtbxCurrXPos.Name = "txtbxCurrXPos";
            this.txtbxCurrXPos.ReadOnly = true;
            this.txtbxCurrXPos.Size = new System.Drawing.Size(132, 20);
            this.txtbxCurrXPos.TabIndex = 24;
            // 
            // btnStageON
            // 
            this.btnStageON.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStageON.Location = new System.Drawing.Point(275, 3);
            this.btnStageON.Name = "btnStageON";
            this.btnStageON.Size = new System.Drawing.Size(130, 22);
            this.btnStageON.TabIndex = 34;
            this.btnStageON.Text = "ON";
            this.btnStageON.UseVisualStyleBackColor = true;
            this.btnStageON.Click += new System.EventHandler(this.btnStageON_Click);
            // 
            // lblStageCurrYPos
            // 
            this.lblStageCurrYPos.AutoSize = true;
            this.lblStageCurrYPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageCurrYPos.Location = new System.Drawing.Point(411, 140);
            this.lblStageCurrYPos.Name = "lblStageCurrYPos";
            this.lblStageCurrYPos.Size = new System.Drawing.Size(132, 28);
            this.lblStageCurrYPos.TabIndex = 19;
            this.lblStageCurrYPos.Text = "Y Position (nm):";
            this.lblStageCurrYPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 28);
            this.label5.TabIndex = 47;
            this.label5.Text = "OverscanPx";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStageCurrXPos
            // 
            this.lblStageCurrXPos.AutoSize = true;
            this.lblStageCurrXPos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStageCurrXPos.Location = new System.Drawing.Point(411, 84);
            this.lblStageCurrXPos.Name = "lblStageCurrXPos";
            this.lblStageCurrXPos.Size = new System.Drawing.Size(132, 28);
            this.lblStageCurrXPos.TabIndex = 18;
            this.lblStageCurrXPos.Text = "X Position (nm):";
            this.lblStageCurrXPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtbxSetImageWidth
            // 
            this.txtbxSetImageWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSetImageWidth.Location = new System.Drawing.Point(139, 3);
            this.txtbxSetImageWidth.Name = "txtbxSetImageWidth";
            this.txtbxSetImageWidth.Size = new System.Drawing.Size(130, 20);
            this.txtbxSetImageWidth.TabIndex = 0;
            this.txtbxSetImageWidth.Text = "256";
            // 
            // txtbxSetInitX
            // 
            this.txtbxSetInitX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSetInitX.Location = new System.Drawing.Point(139, 31);
            this.txtbxSetInitX.Name = "txtbxSetInitX";
            this.txtbxSetInitX.Size = new System.Drawing.Size(130, 20);
            this.txtbxSetInitX.TabIndex = 1;
            this.txtbxSetInitX.Text = "0";
            // 
            // txtbxSetInitY
            // 
            this.txtbxSetInitY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSetInitY.Location = new System.Drawing.Point(139, 59);
            this.txtbxSetInitY.Name = "txtbxSetInitY";
            this.txtbxSetInitY.Size = new System.Drawing.Size(130, 20);
            this.txtbxSetInitY.TabIndex = 3;
            this.txtbxSetInitY.Text = "0";
            // 
            // txtbxGoToX
            // 
            this.txtbxGoToX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxGoToX.Location = new System.Drawing.Point(139, 171);
            this.txtbxGoToX.Name = "txtbxGoToX";
            this.txtbxGoToX.Size = new System.Drawing.Size(130, 20);
            this.txtbxGoToX.TabIndex = 7;
            this.txtbxGoToX.Text = "0";
            // 
            // txtbxSetImageWidthnm
            // 
            this.txtbxSetImageWidthnm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSetImageWidthnm.Location = new System.Drawing.Point(139, 87);
            this.txtbxSetImageWidthnm.Name = "txtbxSetImageWidthnm";
            this.txtbxSetImageWidthnm.Size = new System.Drawing.Size(130, 20);
            this.txtbxSetImageWidthnm.TabIndex = 4;
            this.txtbxSetImageWidthnm.Text = "5000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 31);
            this.label4.TabIndex = 45;
            this.label4.Text = "GotoY";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtbxSetTimePPixel
            // 
            this.txtbxSetTimePPixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbxSetTimePPixel.Location = new System.Drawing.Point(139, 115);
            this.txtbxSetTimePPixel.Name = "txtbxSetTimePPixel";
            this.txtbxSetTimePPixel.Size = new System.Drawing.Size(130, 20);
            this.txtbxSetTimePPixel.TabIndex = 5;
            this.txtbxSetTimePPixel.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 28);
            this.label3.TabIndex = 43;
            this.label3.Text = "GotoX";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetImageWidth
            // 
            this.lblSetImageWidth.AutoSize = true;
            this.lblSetImageWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetImageWidth.Location = new System.Drawing.Point(3, 0);
            this.lblSetImageWidth.Name = "lblSetImageWidth";
            this.lblSetImageWidth.Size = new System.Drawing.Size(130, 28);
            this.lblSetImageWidth.TabIndex = 23;
            this.lblSetImageWidth.Text = "Img Width (px):";
            this.lblSetImageWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetInitX
            // 
            this.lblSetInitX.AutoSize = true;
            this.lblSetInitX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetInitX.Location = new System.Drawing.Point(3, 28);
            this.lblSetInitX.Name = "lblSetInitX";
            this.lblSetInitX.Size = new System.Drawing.Size(130, 28);
            this.lblSetInitX.TabIndex = 25;
            this.lblSetInitX.Text = "Initial X (nm):";
            this.lblSetInitX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetInitY
            // 
            this.lblSetInitY.AutoSize = true;
            this.lblSetInitY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetInitY.Location = new System.Drawing.Point(3, 56);
            this.lblSetInitY.Name = "lblSetInitY";
            this.lblSetInitY.Size = new System.Drawing.Size(130, 28);
            this.lblSetInitY.TabIndex = 27;
            this.lblSetInitY.Text = "Initial Y (nm):";
            this.lblSetInitY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetImageWidthInnm
            // 
            this.lblSetImageWidthInnm.AutoSize = true;
            this.lblSetImageWidthInnm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetImageWidthInnm.Location = new System.Drawing.Point(3, 84);
            this.lblSetImageWidthInnm.Name = "lblSetImageWidthInnm";
            this.lblSetImageWidthInnm.Size = new System.Drawing.Size(130, 28);
            this.lblSetImageWidthInnm.TabIndex = 29;
            this.lblSetImageWidthInnm.Text = "Img Width (nm):";
            this.lblSetImageWidthInnm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSetTimePPixel
            // 
            this.lblSetTimePPixel.AutoSize = true;
            this.lblSetTimePPixel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSetTimePPixel.Location = new System.Drawing.Point(3, 112);
            this.lblSetTimePPixel.Name = "lblSetTimePPixel";
            this.lblSetTimePPixel.Size = new System.Drawing.Size(130, 28);
            this.lblSetTimePPixel.TabIndex = 31;
            this.lblSetTimePPixel.Text = "Time/Pixel (ms):";
            this.lblSetTimePPixel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnScanStart
            // 
            this.btnScanStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnScanStart.Location = new System.Drawing.Point(275, 59);
            this.btnScanStart.Name = "btnScanStart";
            this.tableLayoutPanel1.SetRowSpan(this.btnScanStart, 2);
            this.btnScanStart.Size = new System.Drawing.Size(130, 50);
            this.btnScanStart.TabIndex = 14;
            this.btnScanStart.Text = "Scan";
            this.btnScanStart.UseVisualStyleBackColor = true;
            this.btnScanStart.Click += new System.EventHandler(this.btnScanStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStop.Location = new System.Drawing.Point(275, 115);
            this.btnStop.Name = "btnStop";
            this.tableLayoutPanel1.SetRowSpan(this.btnStop, 2);
            this.btnStop.Size = new System.Drawing.Size(130, 50);
            this.btnStop.TabIndex = 35;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // bckgwrkPerformScan
            // 
            this.bckgwrkPerformScan.WorkerSupportsCancellation = true;
            this.bckgwrkPerformScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bckgwrkPerformScan_DoWork);
            this.bckgwrkPerformScan.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bckgwrkPerformScan_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 271);
            this.Controls.Add(this.grpbxExpCtrl);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.grpbxExpCtrl.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbxExpCtrl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtbxGoToY;
        private System.Windows.Forms.Label lblStageVoltageEngaged;
        private System.Windows.Forms.Button btnStageOFF;
        private System.Windows.Forms.TextBox txtbxCurrYPos;
        private System.Windows.Forms.TextBox txtbxOverScanPx;
        private System.Windows.Forms.TextBox txtbxCurrXPos;
        private System.Windows.Forms.Button btnStageON;
        private System.Windows.Forms.Label lblStageCurrYPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStageCurrXPos;
        private System.Windows.Forms.TextBox txtbxSetImageWidth;
        private System.Windows.Forms.TextBox txtbxSetInitX;
        private System.Windows.Forms.TextBox txtbxSetInitY;
        private System.Windows.Forms.TextBox txtbxGoToX;
        private System.Windows.Forms.TextBox txtbxSetImageWidthnm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbxSetTimePPixel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSetImageWidth;
        private System.Windows.Forms.Label lblSetInitX;
        private System.Windows.Forms.Label lblSetInitY;
        private System.Windows.Forms.Label lblSetImageWidthInnm;
        private System.Windows.Forms.Label lblSetTimePPixel;
        private System.Windows.Forms.Button btnScanStart;
        private System.Windows.Forms.Button btnStop;
        private System.ComponentModel.BackgroundWorker bckgwrkPerformScan;
        private System.Windows.Forms.Button btnMoveAbs;
    }
}

