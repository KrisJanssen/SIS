using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIS.Library;
using SIS.Documents;
using SIS.SerialTerminal;
using SIS.AppResources;
using System.Threading;
using AutoUpdaterDotNET;

namespace SIS
{
	public class MainForm : SIS.MDITemplate.MdiParentForm
    {
        #region TO BE REMOVED

        //private SIS.MDITemplate.MdiMenuItem m_menuLaserComms;
        //private SIS.MDITemplate.MdiMenuItem m_menuSetupChannel;
        //private SerialTerminalMainForm m_serfrmSettings;

        #endregion

        private System.ComponentModel.IContainer m_icontComponents = null;

        public MainForm()
            : this(new string[0])
        {
        }

        public MainForm(string[] __sArgs)
            : base(__sArgs)
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

            if (!this.DesignMode)
            {
                this.Icon = Info.AppIcon;
            }
            
            //this.m_menuMain.MenuItems.Add(this.m_menuLaserComms);
            //this.m_menuMain.MenuItems.Add(this.menuItem2); 

            // Display a warning for the Development version.
            // This will be removed in the final builds...
            Utility.InfoBox(null, Resources.GetString("Startup.MDIwarning"));

            base.SplashForm = new SISSplashForm();
            base.SplashForm.TopMost = true;
            base.SplashForm.Show();
            base.SplashForm.Update();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool __bDisposing)
		{
			if(__bDisposing)
			{
				if (m_icontComponents != null) 
				{
					m_icontComponents.Dispose();
				}
			}
			base.Dispose(__bDisposing);
		}

		#region Designer generated code

		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // m_statusBar
            // 
            this.m_statusBar.Location = new System.Drawing.Point(0, 46);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(672, 68);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

		}

		#endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://dl.dropboxusercontent.com/u/17174999/SIS/SIS.xml");
        }

        #region TO BE REMOVED

        //private void m_menuSetupChannel_Click(object sender, EventArgs e)
        //{
        //    this.m_serfrmSettings = new SerialTerminalMainForm();
        //    this.m_serfrmSettings.Visible = true;
        //}

        #endregion
    }
}

