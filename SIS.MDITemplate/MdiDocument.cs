// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MdiDocument.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The mdi document.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// The mdi document.
    /// </summary>
    public abstract class MdiDocument
    {
        #region Static Fields

        /// <summary>
        /// The m_a documents.
        /// </summary>
        private static ArrayList m_aDocuments = null;

        /// <summary>
        /// The m_document active.
        /// </summary>
        private static MdiDocument m_documentActive = null;

        #endregion

        #region Fields

        /// <summary>
        /// The m_a views.
        /// </summary>
        private ArrayList m_aViews = null;

        /// <summary>
        /// The m_f modified.
        /// </summary>
        private bool m_fModified = false;

        /// <summary>
        /// The m_s file path.
        /// </summary>
        private string m_sFilePath = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MdiDocument"/> class.
        /// </summary>
        public MdiDocument()
        {
            this.m_aViews = new ArrayList();

            if (m_aDocuments == null)
            {
                m_aDocuments = new ArrayList();
            }

            m_aDocuments.Add(this);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the active document.
        /// </summary>
        public static MdiDocument ActiveDocument
        {
            get
            {
                return m_documentActive;
            }

            set
            {
                m_documentActive = value;
            }
        }

        /// <summary>
        /// Gets the documents.
        /// </summary>
        public static MdiDocument[] Documents
        {
            get
            {
                if (m_aDocuments == null)
                {
                    return new MdiDocument[0];
                }
                else
                {
                    return m_aDocuments.ToArray(typeof(MdiDocument)) as MdiDocument[];
                }
            }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName
        {
            get
            {
                if (this.m_sFilePath == null)
                {
                    return string.Empty;
                }
                else
                {
                    return FileNameHelpers.GetFileName(this.m_sFilePath);
                }
            }
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        public string FilePath
        {
            get
            {
                if (this.m_sFilePath == null)
                {
                    return string.Empty;
                }
                else
                {
                    return this.m_sFilePath;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether modified.
        /// </summary>
        public bool Modified
        {
            get
            {
                return this.m_fModified;
            }

            set
            {
                this.m_fModified = value;
                this.UpdateViewTitles();
            }
        }

        /// <summary>
        /// Gets the views.
        /// </summary>
        public MdiViewForm[] Views
        {
            get
            {
                return this.m_aViews.ToArray(typeof(MdiViewForm)) as MdiViewForm[];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add view.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        public void AddView(MdiViewForm view)
        {
            this.m_aViews.Add(view);

            view.Closing += new System.ComponentModel.CancelEventHandler(this.view_Closing);
        }

        /// <summary>
        /// The create view.
        /// </summary>
        /// <returns>
        /// The <see cref="MdiViewForm"/>.
        /// </returns>
        public MdiViewForm CreateView()
        {
            MdiViewForm view = this.OnCreateView();

            if (view == null)
            {
                return null;
            }
            else
            {
                this.AddView(view);
                return view;
            }
        }

        /// <summary>
        /// The load document.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <param name="fAddToRecentFiles">
        /// The f add to recent files.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool LoadDocument(string sFilePath, bool fAddToRecentFiles)
        {
            this.m_sFilePath = sFilePath;

            if (fAddToRecentFiles)
            {
                RecentFilesList.Get().Add(sFilePath);
            }

            return this.OnLoadDocument(sFilePath);
        }

        /// <summary>
        /// The save document.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool SaveDocument(string sFilePath)
        {
            this.UpdateDocument();
            this.Modified = false;
            if (this.OnSaveDocument(sFilePath))
            {
                this.m_sFilePath = sFilePath;
                this.UpdateViewTitles();

                RecentFilesList.Get().Add(sFilePath);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The update all views.
        /// </summary>
        /// <param name="formFrom">
        /// The form from.
        /// </param>
        /// <param name="update">
        /// The update.
        /// </param>
        public void UpdateAllViews(MdiViewForm formFrom, object update)
        {
            foreach (MdiViewForm view in this.m_aViews)
            {
                if (view != formFrom)
                {
                    view.UpdateView(update);
                }
            }
        }

        /// <summary>
        /// The update document.
        /// </summary>
        public void UpdateDocument()
        {
            foreach (MdiViewForm view in this.m_aViews)
            {
                view.UpdateDocument();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on create view.
        /// </summary>
        /// <returns>
        /// The <see cref="MdiViewForm"/>.
        /// </returns>
        protected abstract MdiViewForm OnCreateView();

        /// <summary>
        /// The on load document.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected abstract bool OnLoadDocument(string sFilePath);

        /// <summary>
        /// The on save document.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected abstract bool OnSaveDocument(string sFilePath);

        /// <summary>
        /// The update view titles.
        /// </summary>
        private void UpdateViewTitles()
        {
            foreach (MdiViewForm view in this.m_aViews)
            {
                view.UpdateViewTitle();
            }
        }

        /// <summary>
        /// The view_ closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void view_Closing(object sender, CancelEventArgs e)
        {
            if (this.m_aViews.Count == 1 && this.m_fModified)
            {
                switch (
                    MessageBox.Show(
                        "This document has been modified. Do you want to save ?", 
                        this.FileName, 
                        MessageBoxButtons.YesNoCancel, 
                        MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        this.SaveDocument(this.m_sFilePath);
                        m_aDocuments.Remove(this);
                        break;
                    case DialogResult.No:
                        m_aDocuments.Remove(this);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
            else
            {
                m_aDocuments.Remove(this);
            }
        }

        #endregion
    }
}