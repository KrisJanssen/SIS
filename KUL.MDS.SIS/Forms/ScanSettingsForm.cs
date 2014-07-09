// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanSettingsForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan settings form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Forms
{
    using System;
    using System.Windows.Forms;

    using SIS.Documents;

    /// <summary>
    /// The scan settings form.
    /// </summary>
    public partial class ScanSettingsForm : Form
    {
        #region Fields

        /// <summary>
        /// The m_doc scan document.
        /// </summary>
        private ScanSettings m_docScanDocument;

        /// <summary>
        /// The m_o parameters.
        /// </summary>
        private object[] m_oParameters;

        #endregion

        // declare the EventHandler
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanSettingsForm"/> class.
        /// </summary>
        /// <param name="__scnstSettings">
        /// The __scnst settings.
        /// </param>
        public ScanSettingsForm(ScanSettings __scnstSettings)
        {
            this.m_docScanDocument = __scnstSettings;
            this.InitializeComponent();
            this.UpdateThis();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The update parameters.
        /// </summary>
        public event EventHandler UpdateParameters;

        #endregion

        // Wire up the event
        #region Methods

        /// <summary>
        /// The on update parameters.
        /// </summary>
        protected void OnUpdateParameters()
        {
            if (this.UpdateParameters != null)
            {
                this.UpdateParameters(this, new NotifyEventArgs(this.m_docScanDocument));
            }
        }

        /// <summary>
        /// The update this.
        /// </summary>
        private void UpdateThis()
        {
            this.m_txtbxSetImageWidth.Text = this.m_docScanDocument.ImageWidthPx.ToString();
            this.m_txtbxSetImageHeight.Text = this.m_docScanDocument.ImageHeightPx.ToString();
            this.m_txtbxSetImageDepth.Text = this.m_docScanDocument.ImageDepthPx.ToString();
            this.m_txtbxSetOverScanPxX.Text = this.m_docScanDocument.XOverScanPx.ToString();
            this.m_txtbxSetOverScanPxY.Text = this.m_docScanDocument.YOverScanPx.ToString();
            this.m_txtbxSetOverScanPxZ.Text = this.m_docScanDocument.ZOverScanPx.ToString();
            this.m_txtbxSetTimePPixel.Text = this.m_docScanDocument.TimePPixel.ToString();
            this.m_txtbxSetImageWidthnm.Text = this.m_docScanDocument.XScanSizeNm.ToString();
            this.m_txtbxSetImageHeightnm.Text = this.m_docScanDocument.YScanSizeNm.ToString();
            this.m_txtbxSetImageDepthnm.Text = this.m_docScanDocument.ZScanSizeNm.ToString();
            this.m_txtbxSetInitX.Text = this.m_docScanDocument.InitXNm.ToString();
            this.m_txtbxSetInitY.Text = this.m_docScanDocument.InitYNm.ToString();
            this.m_txtbxSetInitZ.Text = this.m_docScanDocument.InitZNm.ToString();
            this.textBox1.Text = this.m_docScanDocument.Annotation;
        }

        /// <summary>
        /// The m_btn accept_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_btnAccept_Click(object sender, EventArgs e)
        {
            this.m_docScanDocument.ImageWidthPx = Convert.ToUInt16(this.m_txtbxSetImageWidth.Text.Trim());
            this.m_docScanDocument.ImageHeightPx = Convert.ToUInt16(this.m_txtbxSetImageHeight.Text.Trim());
            this.m_docScanDocument.ImageDepthPx = Convert.ToUInt16(this.m_txtbxSetImageDepth.Text.Trim());
            this.m_docScanDocument.XOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxX.Text.Trim());
            this.m_docScanDocument.YOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxY.Text.Trim());
            this.m_docScanDocument.ZOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxZ.Text.Trim());
            this.m_docScanDocument.TimePPixel = Convert.ToSingle(this.m_txtbxSetTimePPixel.Text.Trim());
            this.m_docScanDocument.XScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageWidthnm.Text.Trim());
            this.m_docScanDocument.YScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageHeightnm.Text.Trim());
            this.m_docScanDocument.ZScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageDepthnm.Text.Trim());
            this.m_docScanDocument.InitXNm = Convert.ToSingle(this.m_txtbxSetInitX.Text.Trim());
            this.m_docScanDocument.InitYNm = Convert.ToSingle(this.m_txtbxSetInitY.Text.Trim());
            this.m_docScanDocument.InitZNm = Convert.ToSingle(this.m_txtbxSetInitZ.Text.Trim());
            this.m_docScanDocument.Annotation = this.textBox1.Text.Trim();

            bool _boolPrimaryValidationPassed = false;

            _boolPrimaryValidationPassed = this.validationProvider1.Validate();
            this.validationProvider1.ValidationMessages(!_boolPrimaryValidationPassed);

            if (_boolPrimaryValidationPassed == false)
            {
                // MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (_boolPrimaryValidationPassed)
            {
                this.OnUpdateParameters();
                this.Visible = false;
            }
        }

        /// <summary>
        /// The m_btn cancel_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        /// <summary>
        /// The m_btn default_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_btnDefault_Click(object sender, EventArgs e)
        {
            this.m_docScanDocument = new ScanSettings();
            this.UpdateThis();
        }

        #endregion
    }
}