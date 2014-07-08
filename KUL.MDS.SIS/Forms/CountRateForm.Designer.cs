namespace KUL.MDS.SIS.Forms
{
	partial class CountRateForm
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
			this.pnlCountRate = new System.Windows.Forms.Panel();
			this.pnlCountRateButtons = new System.Windows.Forms.Panel();
			this.btnCountRateClose = new System.Windows.Forms.Button();
			this.pnlCountRateMeter = new System.Windows.Forms.Panel();
			this.btnCoutRateMeterAPD1 = new System.Windows.Forms.Button();
			this.pnlCountRate.SuspendLayout();
			this.pnlCountRateButtons.SuspendLayout();
			this.pnlCountRateMeter.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlCountRate
			// 
			this.pnlCountRate.Controls.Add(this.pnlCountRateButtons);
			this.pnlCountRate.Controls.Add(this.pnlCountRateMeter);
			this.pnlCountRate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCountRate.Location = new System.Drawing.Point(0, 0);
			this.pnlCountRate.Name = "pnlCountRate";
			this.pnlCountRate.Size = new System.Drawing.Size(784, 301);
			this.pnlCountRate.TabIndex = 0;
			// 
			// pnlCountRateButtons
			// 
			this.pnlCountRateButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlCountRateButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlCountRateButtons.Controls.Add(this.btnCountRateClose);
			this.pnlCountRateButtons.Location = new System.Drawing.Point(12, 219);
			this.pnlCountRateButtons.Name = "pnlCountRateButtons";
			this.pnlCountRateButtons.Size = new System.Drawing.Size(760, 79);
			this.pnlCountRateButtons.TabIndex = 1;
			// 
			// btnCountRateClose
			// 
			this.btnCountRateClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnCountRateClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCountRateClose.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCountRateClose.Location = new System.Drawing.Point(308, 12);
			this.btnCountRateClose.Name = "btnCountRateClose";
			this.btnCountRateClose.Size = new System.Drawing.Size(144, 55);
			this.btnCountRateClose.TabIndex = 0;
			this.btnCountRateClose.Text = "STOP";
			this.btnCountRateClose.UseVisualStyleBackColor = true;
			this.btnCountRateClose.Click += new System.EventHandler(this.btnCountRateSTOP_Click);
			// 
			// pnlCountRateMeter
			// 
			this.pnlCountRateMeter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlCountRateMeter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pnlCountRateMeter.BackColor = System.Drawing.SystemColors.Control;
			this.pnlCountRateMeter.Controls.Add(this.btnCoutRateMeterAPD1);
			this.pnlCountRateMeter.Location = new System.Drawing.Point(12, 12);
			this.pnlCountRateMeter.Name = "pnlCountRateMeter";
			this.pnlCountRateMeter.Size = new System.Drawing.Size(760, 201);
			this.pnlCountRateMeter.TabIndex = 0;
			// 
			// btnCoutRateMeterAPD1
			// 
			this.btnCoutRateMeterAPD1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnCoutRateMeterAPD1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.btnCoutRateMeterAPD1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCoutRateMeterAPD1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnCoutRateMeterAPD1.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCoutRateMeterAPD1.ForeColor = System.Drawing.Color.Blue;
			this.btnCoutRateMeterAPD1.Location = new System.Drawing.Point(0, 0);
			this.btnCoutRateMeterAPD1.Name = "btnCoutRateMeterAPD1";
			this.btnCoutRateMeterAPD1.Size = new System.Drawing.Size(760, 201);
			this.btnCoutRateMeterAPD1.TabIndex = 0;
			this.btnCoutRateMeterAPD1.TabStop = false;
			this.btnCoutRateMeterAPD1.Text = "---";
			this.btnCoutRateMeterAPD1.UseVisualStyleBackColor = false;
			// 
			// CountRateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(784, 301);
			this.Controls.Add(this.pnlCountRate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "CountRateForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Count Rate";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CountRateForm_FormClosing);
			this.pnlCountRate.ResumeLayout(false);
			this.pnlCountRateButtons.ResumeLayout(false);
			this.pnlCountRateMeter.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlCountRate;
		private System.Windows.Forms.Panel pnlCountRateMeter;
		private System.Windows.Forms.Panel pnlCountRateButtons;
		private System.Windows.Forms.Button btnCountRateClose;
		private System.Windows.Forms.Button btnCoutRateMeterAPD1;
	}
}