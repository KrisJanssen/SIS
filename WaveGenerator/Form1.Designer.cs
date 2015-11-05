namespace WaveGenerator
{
    partial class Form1
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
            this.btnON = new System.Windows.Forms.Button();
            this.btnOFF = new System.Windows.Forms.Button();
            this.lblSTATUS = new System.Windows.Forms.Label();
            this.btnSTART = new System.Windows.Forms.Button();
            this.btnSTOP = new System.Windows.Forms.Button();
            this.btnMOVE = new System.Windows.Forms.Button();
            this.txtMOVE = new System.Windows.Forms.TextBox();
            this.lblPOS = new System.Windows.Forms.Label();
            this.txtPIXELS = new System.Windows.Forms.TextBox();
            this.txtTPL = new System.Windows.Forms.TextBox();
            this.lblPIXELS = new System.Windows.Forms.Label();
            this.lblTPL = new System.Windows.Forms.Label();
            this.workerMove = new System.ComponentModel.BackgroundWorker();
            this.workerScan = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnON
            // 
            this.btnON.Location = new System.Drawing.Point(13, 13);
            this.btnON.Name = "btnON";
            this.btnON.Size = new System.Drawing.Size(75, 23);
            this.btnON.TabIndex = 0;
            this.btnON.Text = "ON";
            this.btnON.UseVisualStyleBackColor = true;
            this.btnON.Click += new System.EventHandler(this.btnON_Click);
            // 
            // btnOFF
            // 
            this.btnOFF.Location = new System.Drawing.Point(13, 43);
            this.btnOFF.Name = "btnOFF";
            this.btnOFF.Size = new System.Drawing.Size(75, 23);
            this.btnOFF.TabIndex = 1;
            this.btnOFF.Text = "OFF";
            this.btnOFF.UseVisualStyleBackColor = true;
            this.btnOFF.Click += new System.EventHandler(this.btnOFF_Click);
            // 
            // lblSTATUS
            // 
            this.lblSTATUS.AutoSize = true;
            this.lblSTATUS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTATUS.ForeColor = System.Drawing.Color.Red;
            this.lblSTATUS.Location = new System.Drawing.Point(117, 26);
            this.lblSTATUS.Name = "lblSTATUS";
            this.lblSTATUS.Size = new System.Drawing.Size(57, 25);
            this.lblSTATUS.TabIndex = 2;
            this.lblSTATUS.Text = "OFF";
            // 
            // btnSTART
            // 
            this.btnSTART.Location = new System.Drawing.Point(197, 13);
            this.btnSTART.Name = "btnSTART";
            this.btnSTART.Size = new System.Drawing.Size(75, 23);
            this.btnSTART.TabIndex = 3;
            this.btnSTART.Text = "START";
            this.btnSTART.UseVisualStyleBackColor = true;
            this.btnSTART.Click += new System.EventHandler(this.btnSTART_Click);
            // 
            // btnSTOP
            // 
            this.btnSTOP.Location = new System.Drawing.Point(197, 43);
            this.btnSTOP.Name = "btnSTOP";
            this.btnSTOP.Size = new System.Drawing.Size(75, 23);
            this.btnSTOP.TabIndex = 4;
            this.btnSTOP.Text = "STOP";
            this.btnSTOP.UseVisualStyleBackColor = true;
            this.btnSTOP.Click += new System.EventHandler(this.btnSTOP_Click);
            // 
            // btnMOVE
            // 
            this.btnMOVE.Location = new System.Drawing.Point(13, 114);
            this.btnMOVE.Name = "btnMOVE";
            this.btnMOVE.Size = new System.Drawing.Size(75, 23);
            this.btnMOVE.TabIndex = 5;
            this.btnMOVE.Text = "Move";
            this.btnMOVE.UseVisualStyleBackColor = true;
            this.btnMOVE.Click += new System.EventHandler(this.btnMOVE_Click);
            // 
            // txtMOVE
            // 
            this.txtMOVE.Location = new System.Drawing.Point(172, 116);
            this.txtMOVE.Name = "txtMOVE";
            this.txtMOVE.Size = new System.Drawing.Size(100, 20);
            this.txtMOVE.TabIndex = 6;
            this.txtMOVE.Text = "0.0";
            // 
            // lblPOS
            // 
            this.lblPOS.AutoSize = true;
            this.lblPOS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPOS.Location = new System.Drawing.Point(133, 77);
            this.lblPOS.Name = "lblPOS";
            this.lblPOS.Size = new System.Drawing.Size(25, 25);
            this.lblPOS.TabIndex = 7;
            this.lblPOS.Text = "0";
            // 
            // txtPIXELS
            // 
            this.txtPIXELS.Location = new System.Drawing.Point(172, 143);
            this.txtPIXELS.Name = "txtPIXELS";
            this.txtPIXELS.Size = new System.Drawing.Size(100, 20);
            this.txtPIXELS.TabIndex = 8;
            this.txtPIXELS.Text = "1000";
            // 
            // txtTPL
            // 
            this.txtTPL.Location = new System.Drawing.Point(172, 170);
            this.txtTPL.Name = "txtTPL";
            this.txtTPL.Size = new System.Drawing.Size(100, 20);
            this.txtTPL.TabIndex = 9;
            this.txtTPL.Text = "2.0";
            // 
            // lblPIXELS
            // 
            this.lblPIXELS.AutoSize = true;
            this.lblPIXELS.Location = new System.Drawing.Point(82, 146);
            this.lblPIXELS.Name = "lblPIXELS";
            this.lblPIXELS.Size = new System.Drawing.Size(84, 13);
            this.lblPIXELS.TabIndex = 10;
            this.lblPIXELS.Text = "Samples per line";
            this.lblPIXELS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTPL
            // 
            this.lblTPL.AutoSize = true;
            this.lblTPL.Location = new System.Drawing.Point(77, 173);
            this.lblTPL.Name = "lblTPL";
            this.lblTPL.Size = new System.Drawing.Size(89, 13);
            this.lblTPL.TabIndex = 11;
            this.lblTPL.Text = "Time per line (ms)";
            this.lblTPL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // workerMove
            // 
            this.workerMove.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerMove_DoWork);
            // 
            // workerScan
            // 
            this.workerScan.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerScan_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblTPL);
            this.Controls.Add(this.lblPIXELS);
            this.Controls.Add(this.txtTPL);
            this.Controls.Add(this.txtPIXELS);
            this.Controls.Add(this.lblPOS);
            this.Controls.Add(this.txtMOVE);
            this.Controls.Add(this.btnMOVE);
            this.Controls.Add(this.btnSTOP);
            this.Controls.Add(this.btnSTART);
            this.Controls.Add(this.lblSTATUS);
            this.Controls.Add(this.btnOFF);
            this.Controls.Add(this.btnON);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnON;
        private System.Windows.Forms.Button btnOFF;
        private System.Windows.Forms.Label lblSTATUS;
        private System.Windows.Forms.Button btnSTART;
        private System.Windows.Forms.Button btnSTOP;
        private System.Windows.Forms.Button btnMOVE;
        private System.Windows.Forms.TextBox txtMOVE;
        private System.Windows.Forms.Label lblPOS;
        private System.Windows.Forms.TextBox txtPIXELS;
        private System.Windows.Forms.TextBox txtTPL;
        private System.Windows.Forms.Label lblPIXELS;
        private System.Windows.Forms.Label lblTPL;
        private System.ComponentModel.BackgroundWorker workerMove;
        private System.ComponentModel.BackgroundWorker workerScan;
    }
}

