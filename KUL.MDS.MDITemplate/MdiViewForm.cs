// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MdiViewForm.cs" company="">
//   
// </copyright>
// <summary>
//   The mdi view form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate
{
    using System;
    using System.ComponentModel;

    // public class MdiViewForm : System.Windows.Forms.Form
    /// <summary>
    /// The mdi view form.
    /// </summary>
    public class MdiViewForm : BaseForm
    {
        #region Fields

        /// <summary>
        /// The components.
        /// </summary>
        private Container components = null;

        /// <summary>
        /// The m_document.
        /// </summary>
        private MdiDocument m_document = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MdiViewForm"/> class.
        /// </summary>
        public MdiViewForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        public MdiDocument Document
        {
            get
            {
                return this.m_document;
            }

            set
            {
                this.m_document = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The initial update.
        /// </summary>
        public void InitialUpdate()
        {
            this.Text = this.Document.FileName;
            this.OnInitialUpdate();
        }

        /// <summary>
        /// The update document.
        /// </summary>
        public void UpdateDocument()
        {
            this.OnUpdateDocument();
        }

        /// <summary>
        /// The update view.
        /// </summary>
        /// <param name="update">
        /// The update.
        /// </param>
        public void UpdateView(object update)
        {
            this.OnUpdateView(update);
        }

        /// <summary>
        /// The update view title.
        /// </summary>
        public void UpdateViewTitle()
        {
            if (this.Document.Modified)
            {
                this.Text = string.Format("{0}*", this.Document.FileName);
            }
            else
            {
                this.Text = this.Document.FileName;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// The on activated.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnActivated(EventArgs e)
        {
            MdiDocument.ActiveDocument = this.Document;

            base.OnActivated(e);
        }

        /// <summary>
        /// The on closed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (MdiDocument.ActiveDocument == this.Document)
            {
                MdiDocument.ActiveDocument = null;
            }
        }

        /// <summary>
        /// The on initial update.
        /// </summary>
        protected virtual void OnInitialUpdate()
        {
        }

        /// <summary>
        /// The on update document.
        /// </summary>
        protected virtual void OnUpdateDocument()
        {
        }

        /// <summary>
        /// The on update view.
        /// </summary>
        /// <param name="update">
        /// The update.
        /// </param>
        protected virtual void OnUpdateView(object update)
        {
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // MdiViewForm
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.DoubleBuffered = true;
            this.Name = "MdiViewForm";
            this.ShowInTaskbar = false;
            this.Text = "MdiViewForm";
            this.ResumeLayout(false);
        }

        #endregion
    }
}