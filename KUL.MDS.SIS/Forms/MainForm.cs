// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="">
//   
// </copyright>
// <summary>
//   The main form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Forms
{
    using System;
    using System.ComponentModel;

    using AutoUpdaterDotNET;

    using SIS.MDITemplate;
    using SIS.Resources;

    /// <summary>
    /// The main form.
    /// </summary>
    public class MainForm : MdiParentForm
    {
        // private KUL.MDS.MDITemplate.MdiMenuItem m_menuLaserComms;
        // private KUL.MDS.MDITemplate.MdiMenuItem m_menuSetupChannel;
        // private SerialTerminalMainForm m_serfrmSettings;
        #region Fields

        /// <summary>
        /// The m_icont components.
        /// </summary>
        private IContainer m_icontComponents = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
            : this(new string[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        /// <param name="__sArgs">
        /// The __s args.
        /// </param>
        public MainForm(string[] __sArgs)
            : base(__sArgs)
        {
            // This call is required by the Windows Form Designer.
            this.InitializeComponent();

            if (!this.DesignMode)
            {
                this.Icon = Info.AppIcon;
            }

            // this.m_menuMain.MenuItems.Add(this.m_menuLaserComms);
            // this.m_menuMain.MenuItems.Add(this.menuItem2); 

            // Display a warning for the Development version.
            // This will be removed in the final builds...
            // Utility.InfoBox(null, Resources.GetString("Startup.MDIwarning"));
            base.SplashForm = new SISSplashForm();
            base.SplashForm.TopMost = true;
            base.SplashForm.Show();
            base.SplashForm.Update();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="__bDisposing">
        /// The __b Disposing.
        /// </param>
        protected override void Dispose(bool __bDisposing)
        {
            if (__bDisposing)
            {
                if (this.m_icontComponents != null)
                {
                    this.m_icontComponents.Dispose();
                }
            }

            base.Dispose(__bDisposing);
        }

        /// <summary>
        /// The initialize component.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // m_statusBar
            this.m_statusBar.Location = new System.Drawing.Point(0, 46);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.ClientSize = new System.Drawing.Size(672, 68);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The main form_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://dl.dropboxusercontent.com/u/17174999/SIS/SIS.xml");
        }

        #endregion

        // private void m_menuSetupChannel_Click(object sender, EventArgs e)
        // {
        // this.m_serfrmSettings = new SerialTerminalMainForm();
        // this.m_serfrmSettings.Visible = true;
        // }
    }
}