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
            // Update Scan Settings Section
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
            this.m_txtbxAnnotations.Text = this.m_docScanDocument.Annotation;

            // Update Galvo Settings Section
            this.m_cmbbxGalvoSerialPort.Text = this.m_docScanDocument.GalvoSerialPortName;
            this.m_cmbbxGalvoFrameMarker.Text = this.m_docScanDocument.GalvoFrameMarker.ToString();
            this.m_cmbbxGalvoLineMarker.Text = this.m_docScanDocument.GalvoLineMarker.ToString();
            this.m_txtbxGalvoMagnificationObjective.Text = this.m_docScanDocument.GalvoMagnificationObjective.ToString();
            this.m_txtbxGalvoScanLensFocalLength.Text = this.m_docScanDocument.GalvoScanLensFocalLength.ToString();
            this.m_txtbxGalvoRangeAngleDegrees.Text = this.m_docScanDocument.GalvoRangeAngleDegrees.ToString();
            this.m_txtbxGalvoRangeAngleInt.Text = this.m_docScanDocument.GalvoRangeAngleInt.ToString();

            // Update Time Harp Settings Section
            this.m_cmbbxTimeHarpFrameMarker.Text = this.m_docScanDocument.TimeHarpFrameMarker.ToString();
            this.m_cmbbxTimeHarpLineMarker.Text = this.m_docScanDocument.TimeHarpLineMarker.ToString();
            this.m_cmbbxTimeHarpMarkerEdge.Text = (this.m_docScanDocument.TimeHarpMarkerEdge == 1)
                                                      ? "rising edge"
                                                      : "falling edge";
            this.m_cmbbxTimeHarpMeasurementMode.Text = this.m_docScanDocument.TimeHarpMeasurementMode.ToString();
            this.m_cmbbxTimeHarpRangeCode.Text = this.m_docScanDocument.TimeHarpRangeCode.ToString();
            this.m_txtbxTimeHarpOffset.Text = this.m_docScanDocument.TimeHarpOffset.ToString();
            this.m_txtbxTimeHarpCFDZeroCross.Text = this.m_docScanDocument.TimeHarpCFDZeroCross.ToString();
            this.m_txtbxTimeHarpCFDMin.Text = this.m_docScanDocument.TimeHarpCFDMin.ToString();
            this.m_txtbxTimeHarpSyncLevel.Text = this.m_docScanDocument.TimeHarpSyncLevel.ToString();
            this.m_txtbxTimeHarpLinePTTTRBufferSize.Text = this.m_docScanDocument.TimeHarpLinePTTTRBufferSize.ToString();
            this.m_txtbxTimeHarpGlobalTTTRBufferSize.Text =
                this.m_docScanDocument.TimeHarpGlobalTTTRBufferSize.ToString();
            this.m_txtbxTimeHarpFrameTimeOut.Text = this.m_docScanDocument.TimeHarpFrameTimeOut.ToString();
            this.m_txtbxTimeHarpFiFoTimeOut.Text = this.m_docScanDocument.TimeHarpFiFoTimeOut.ToString();
            this.m_txtbxTimeHarpNameTTTRFile.Text = this.m_docScanDocument.TimeHarpNameTTTRFile;
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
            // Accept Settings from Scan Section
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
            this.m_docScanDocument.Annotation = this.m_txtbxAnnotations.Text.Trim();

            // Accept Settings from Galvo Section
            this.m_docScanDocument.GalvoSerialPortName = this.m_cmbbxGalvoSerialPort.Text.Trim();
            this.m_docScanDocument.GalvoFrameMarker = Convert.ToInt32(this.m_cmbbxGalvoFrameMarker.Text.Trim());
            this.m_docScanDocument.GalvoLineMarker = Convert.ToInt32(this.m_cmbbxGalvoLineMarker.Text.Trim());
            this.m_docScanDocument.GalvoMagnificationObjective =
                Convert.ToDouble(this.m_txtbxGalvoMagnificationObjective.Text.Trim());
            this.m_docScanDocument.GalvoScanLensFocalLength =
                Convert.ToDouble(this.m_txtbxGalvoScanLensFocalLength.Text.Trim());
            this.m_docScanDocument.GalvoRangeAngleDegrees =
                Convert.ToDouble(this.m_txtbxGalvoRangeAngleDegrees.Text.Trim());
            this.m_docScanDocument.GalvoRangeAngleInt = Convert.ToDouble(this.m_txtbxGalvoRangeAngleInt.Text.Trim());

            // Accept Settings from Time Harp Section
            this.m_docScanDocument.TimeHarpFrameMarker = Convert.ToInt32(this.m_cmbbxTimeHarpFrameMarker.Text.Trim());
            this.m_docScanDocument.TimeHarpLineMarker = Convert.ToInt32(this.m_cmbbxTimeHarpLineMarker.Text.Trim());
            this.m_docScanDocument.TimeHarpMarkerEdge = (this.m_cmbbxTimeHarpMarkerEdge.Text.Trim() == "rising edge")
                                                            ? 1
                                                            : 0;
            this.m_docScanDocument.TimeHarpMeasurementMode =
                Convert.ToInt32(this.m_cmbbxTimeHarpMeasurementMode.Text.Trim());
            this.m_docScanDocument.TimeHarpRangeCode = Convert.ToInt32(this.m_cmbbxTimeHarpRangeCode.Text.Trim());
            this.m_docScanDocument.TimeHarpOffset = Convert.ToInt32(this.m_txtbxTimeHarpOffset.Text.Trim());
            this.m_docScanDocument.TimeHarpCFDZeroCross = Convert.ToInt32(this.m_txtbxTimeHarpCFDZeroCross.Text.Trim());
            this.m_docScanDocument.TimeHarpCFDMin = Convert.ToInt32(this.m_txtbxTimeHarpCFDMin.Text.Trim());
            this.m_docScanDocument.TimeHarpSyncLevel = Convert.ToInt32(this.m_txtbxTimeHarpSyncLevel.Text.Trim());
            this.m_docScanDocument.TimeHarpLinePTTTRBufferSize =
                Convert.ToInt32(this.m_txtbxTimeHarpLinePTTTRBufferSize.Text.Trim());
            this.m_docScanDocument.TimeHarpGlobalTTTRBufferSize =
                Convert.ToInt32(this.m_txtbxTimeHarpGlobalTTTRBufferSize.Text.Trim());
            this.m_docScanDocument.TimeHarpFrameTimeOut = Convert.ToInt32(this.m_txtbxTimeHarpFrameTimeOut.Text.Trim());
            this.m_docScanDocument.TimeHarpFiFoTimeOut = Convert.ToInt32(this.m_txtbxTimeHarpFiFoTimeOut.Text.Trim());
            this.m_docScanDocument.TimeHarpNameTTTRFile = this.m_txtbxTimeHarpNameTTTRFile.Text.Trim();

            bool _boolPrimaryValidationPassed = false;

            _boolPrimaryValidationPassed = this.validationProvider1.Validate();
            this.validationProvider1.ValidationMessages(!_boolPrimaryValidationPassed);

            if (_boolPrimaryValidationPassed == false)
            {
                // MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (_boolPrimaryValidationPassed == true)
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