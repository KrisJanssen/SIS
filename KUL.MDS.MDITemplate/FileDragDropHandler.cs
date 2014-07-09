// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileDragDropHandler.cs" company="">
//   
// </copyright>
// <summary>
//   The file drag drop handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// The file drag drop handler.
    /// </summary>
    public class FileDragDropHandler
    {
        #region Fields

        /// <summary>
        /// The m_map allowed extensions.
        /// </summary>
        private Hashtable m_mapAllowedExtensions = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDragDropHandler"/> class.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <param name="asExtensions">
        /// The as extensions.
        /// </param>
        public FileDragDropHandler(Form form, string[] asExtensions)
        {
            this.InitialiseEvents(form);
            this.InitialiseExtensionMap(asExtensions);
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The file drop handler.
        /// </summary>
        /// <param name="sFileName">
        /// The s file name.
        /// </param>
        public delegate void FileDropHandler(string sFileName);

        #endregion

        #region Public Events

        /// <summary>
        /// The file dropped.
        /// </summary>
        public event FileDropHandler FileDropped = null;

        #endregion

        #region Methods

        /// <summary>
        /// The initialise events.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        private void InitialiseEvents(Form form)
        {
            form.AllowDrop = true;
            form.DragEnter += new DragEventHandler(this.form_DragEnter);
            form.DragLeave += new EventHandler(this.form_DragLeave);
            form.DragDrop += new DragEventHandler(this.form_DragDrop);
        }

        /// <summary>
        /// The initialise extension map.
        /// </summary>
        /// <param name="asExtensions">
        /// The as extensions.
        /// </param>
        private void InitialiseExtensionMap(string[] asExtensions)
        {
            this.m_mapAllowedExtensions = new Hashtable();

            if (asExtensions != null)
            {
                foreach (string sExtension in asExtensions)
                {
                    this.m_mapAllowedExtensions.Add(sExtension, null);
                }
            }
        }

        /// <summary>
        /// The test extensions.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool TestExtensions(DragEventArgs e)
        {
            string[] asFiles = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (asFiles == null)
            {
                return false;
            }
            else
            {
                foreach (string sFileName in asFiles)
                {
                    string sExtension = FileNameHelpers.GetExtension(sFileName);

                    if (!this.m_mapAllowedExtensions.Contains(sExtension))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// The form_ drag drop.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void form_DragDrop(object sender, DragEventArgs e)
        {
            string[] asFiles = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (asFiles != null && this.FileDropped != null)
            {
                foreach (string sFileName in asFiles)
                {
                    this.FileDropped(sFileName);
                }
            }
        }

        /// <summary>
        /// The form_ drag enter.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (this.TestExtensions(e))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// The form_ drag leave.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void form_DragLeave(object sender, EventArgs e)
        {
        }

        #endregion
    }
}