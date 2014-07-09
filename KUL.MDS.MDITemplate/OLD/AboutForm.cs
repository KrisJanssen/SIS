namespace SIS.MDITemplate.OLD
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox m_pictureBoxAppIcon = null;
		private System.Windows.Forms.Label m_labelCopyright = null;
		private System.Windows.Forms.Label m_labelVersion = null;
		private System.ComponentModel.Container components = null;
		private const int m_nBorder = 8;

		public AboutForm()
		{
			this.InitializeComponent();

			if (!this.DesignMode)
			{
				this.InitialiseText();
				this.InitialiseSize();
			}
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.m_pictureBoxAppIcon = new System.Windows.Forms.PictureBox();
			this.m_labelCopyright = new System.Windows.Forms.Label();
			this.m_labelVersion = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// m_pictureBoxAppIcon
			// 
			this.m_pictureBoxAppIcon.Location = new System.Drawing.Point(14, 8);
			this.m_pictureBoxAppIcon.Name = "m_pictureBoxAppIcon";
			this.m_pictureBoxAppIcon.Size = new System.Drawing.Size(32, 32);
			this.m_pictureBoxAppIcon.TabIndex = 0;
			this.m_pictureBoxAppIcon.TabStop = false;
			// 
			// m_labelCopyright
			// 
			this.m_labelCopyright.Location = new System.Drawing.Point(56, 32);
			this.m_labelCopyright.Name = "m_labelCopyright";
			this.m_labelCopyright.Size = new System.Drawing.Size(192, 16);
			this.m_labelCopyright.TabIndex = 1;
			this.m_labelCopyright.Text = "`";
			// 
			// m_labelVersion
			// 
			this.m_labelVersion.Location = new System.Drawing.Point(56, 8);
			this.m_labelVersion.Name = "m_labelVersion";
			this.m_labelVersion.Size = new System.Drawing.Size(192, 16);
			this.m_labelVersion.TabIndex = 1;
			this.m_labelVersion.Text = "`";
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(258, 55);
			this.Controls.Add(this.m_pictureBoxAppIcon);
			this.Controls.Add(this.m_labelCopyright);
			this.Controls.Add(this.m_labelVersion);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AboutForm";
			this.ResumeLayout(false);

		}
		#endregion

		public Image ApplicationIcon
		{
			get
			{
				return this.m_pictureBoxAppIcon.Image;
			}

			set
			{
				this.m_pictureBoxAppIcon.Image = value;
			}
		}

		private void InitialiseText()
		{
			this.Text = string.Format("About {0}", Application.ProductName);
			this.m_labelVersion.Text = string.Format("Version {0}", Application.ProductVersion);
			this.m_labelCopyright.Text = string.Format("Copyright (c) {0} {1}", Application.CompanyName, DateTime.Now.Year);
		}

		private void InitialiseSize()
		{
			int nVersionWidth = this.GetLabelWidth(this.m_labelVersion);
			int nCopyrightWidth = this.GetLabelWidth(this.m_labelCopyright);

			int nLabelWidth = Math.Max(nVersionWidth, nCopyrightWidth);

			this.m_labelVersion.Width = nLabelWidth;
			this.m_labelCopyright.Width = nLabelWidth;

			int nRight = this.m_labelVersion.Left + this.m_labelVersion.Width + m_nBorder;

			this.Width = nRight;
		}

		private int GetLabelWidth(System.Windows.Forms.Label label)
		{
			Graphics graphics = label.CreateGraphics();
			SizeF sizeLabel = graphics.MeasureString(label.Text, label.Font);
			graphics.Dispose();

			return (int)sizeLabel.Width + (m_nBorder << 2);
		}
	}
}
