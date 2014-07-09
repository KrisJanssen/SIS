// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentFilesMenuItem.cs" company="">
//   
// </copyright>
// <summary>
//   The recent files menu item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// The recent files menu item.
    /// </summary>
    public class RecentFilesMenuItem : MdiMenuItem
    {
        #region Fields

        /// <summary>
        /// The m_map menu item to file.
        /// </summary>
        private Hashtable m_mapMenuItemToFile = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentFilesMenuItem"/> class.
        /// </summary>
        public RecentFilesMenuItem()
        {
            this.m_mapMenuItemToFile = new Hashtable();
            this.Text = "Recent files";

            this.Load();
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The open file handler.
        /// </summary>
        /// <param name="sFile">
        /// The s file.
        /// </param>
        public delegate void OpenFileHandler(string sFile);

        #endregion

        #region Public Events

        /// <summary>
        /// The open file.
        /// </summary>
        public event OpenFileHandler OpenFile = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether enabled.
        /// </summary>
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }

            set
            {
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The load.
        /// </summary>
        public void Load()
        {
            this.MenuItems.Clear();
            this.m_mapMenuItemToFile.Clear();

            foreach (string sFile in RecentFilesList.Get())
            {
                MenuItem itemFile = new MenuItem(FileNameHelpers.GetFileName(sFile));
                this.MenuItems.Add(itemFile);
                itemFile.Click += new EventHandler(this.MenuItem_Click);
                this.m_mapMenuItemToFile.Add(itemFile, sFile);
            }

            base.Enabled = this.m_mapMenuItemToFile.Count > 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on test enabled.
        /// </summary>
        protected override void OnTestEnabled()
        {
            this.Load();
        }

        /// <summary>
        /// The menu item_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            string sFile = this.m_mapMenuItemToFile[sender] as string;

            if (sFile != null)
            {
                if (this.OpenFile != null)
                {
                    this.OpenFile(sFile);
                }
            }

            StatusBarMessenger.SetMessage(StatusBarMessenger.DefaultMessage);
        }

        #endregion
    }
}