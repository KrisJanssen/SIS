// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MdiMenuItem.cs" company="">
//   
// </copyright>
// <summary>
//   The mdi menu item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The mdi menu item.
    /// </summary>
    public class MdiMenuItem : MenuItem, IStatusBarMessage
    {
        #region Fields

        /// <summary>
        /// The m_f needs document.
        /// </summary>
        private bool m_fNeedsDocument = false;

        /// <summary>
        /// The m_s status message.
        /// </summary>
        private string m_sStatusMessage = StatusBarMessenger.DefaultMessage;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MdiMenuItem"/> class.
        /// </summary>
        public MdiMenuItem()
        {
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The test enabled helper.
        /// </summary>
        /// <param name="menuItem">
        /// The menu item.
        /// </param>
        public delegate void TestEnabledHelper(MdiMenuItem menuItem);

        #endregion

        #region Public Events

        /// <summary>
        /// The test enabled.
        /// </summary>
        public event TestEnabledHelper TestEnabled = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether needs document.
        /// </summary>
        public bool NeedsDocument
        {
            get
            {
                return this.m_fNeedsDocument;
            }

            set
            {
                this.m_fNeedsDocument = value;
            }
        }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return this.m_sStatusMessage;
            }

            set
            {
                this.m_sStatusMessage = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The update menu item.
        /// </summary>
        public void UpdateMenuItem()
        {
            if (this.IsParent)
            {
                foreach (MenuItem item in this.MenuItems)
                {
                    if (item is MdiMenuItem)
                    {
                        MdiMenuItem itemStatusBar = item as MdiMenuItem;
                        itemStatusBar.OnTestEnabled();
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on click.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            StatusBarMessenger.SetMessage(StatusBarMessenger.DefaultMessage);
        }

        /// <summary>
        /// The on init menu popup.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnInitMenuPopup(EventArgs e)
        {
            base.OnInitMenuPopup(e);

            this.UpdateMenuItem();
        }

        /// <summary>
        /// The on select.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnSelect(EventArgs e)
        {
            base.OnSelect(e);

            StatusBarMessenger.SetMessage(this.m_sStatusMessage);
        }

        /// <summary>
        /// The on test enabled.
        /// </summary>
        protected virtual void OnTestEnabled()
        {
            if (this.NeedsDocument)
            {
                this.Enabled = MdiDocument.ActiveDocument != null;
            }

            if (this.TestEnabled != null)
            {
                this.TestEnabled(this);
            }
        }

        #endregion
    }
}