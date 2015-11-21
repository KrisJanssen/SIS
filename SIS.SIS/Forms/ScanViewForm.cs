using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SIS.Library;
using SIS.Documents;
using SIS.ScanModes;
using SIS.WPFControls;
using System.Collections.Generic;
using SIS.SystemLayer;
using System.IO;
using log4net;
using log4net.Layout;
using DevDefined.Common.Appenders;
using System.Linq;

namespace SIS.Forms
{
    public partial class ScanViewForm : SIS.MDITemplate.MdiViewForm
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Member Variables

        // The various essential objects representing hardware.
        // We need one or more APDs, a Piezo and a timing clock to sync everything.
        private SIS.Hardware.APD m_apdAPD1;
        private SIS.Hardware.APD m_apdAPD2;
        private SIS.Hardware.IPiezoStage m_Stage;
        private SIS.Hardware.NISampleClock m_clckGlobalSync;


        // A progress bar that we can use to indicate... progress of various tasks that are handled.
        private SIS.Forms.ProgressBarForm m_frmPBar = new ProgressBarForm();
        private SIS.Forms.TrajectoryPlotForm m_frmTrajectoryForm = new TrajectoryPlotForm();
        private SIS.Forms.ScanSettingsForm m_frmScanSettingsForm;
        private SIS.Forms.CountRateForm m_frmCountRate;

        // Object to keep track of the current Scan Settings.
        private ScanSettings m_scnstSettings;

        // Arrays that will hold displays of the scans.
        private Bitmap[] m_bmpBitmapsAPD1 = new Bitmap[4];
        private Bitmap[] m_bmpBitmapsAPD2 = new Bitmap[4];

        // NEW STUFF probably needs cleaning...
        private double m_dXSel;
        private double m_dYSel;
        private AForge.Imaging.Filters.Add m_filter = new AForge.Imaging.Filters.Add();

        // Informing the user of Debug output.
        private ColoredRichTextBoxAppender m_ColRTApp;

        #endregion


        #region Delegates

        // Delegate involved in handling cross thread passing of data from Hardware to UI.
        private delegate void UIUpdateDelegate();

        #endregion


        #region Interface Related Methods

        // Scanviewform constructor. Initialization of some important stuff is done here.
        public ScanViewForm()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // Initialize form buttons.
            InitInterface();

            // This version of the imaging suite operates with 2 APDs
            // Parameters needed are:
            // 1) Board ID
            // 2) Counter to generate a pulse with duration of bintime
            // 3) Timebase for bintime pulse
            // 4) APD data collection trigger
            // 5) Counter that counts TTLs from physical APD
            // 6) Input terminal carrying TTLs from physical APD
            //
            // TODO: Put this stuff in some sort of config file/the Windows registry.
            //this.m_apdAPD1 = new SIS.Hardware.APD("Dev1", "Ctr1", 100, "Ctr2InternalOutput", "Ctr0", "PFI8", true);
            this.m_apdAPD1 = new SIS.Hardware.APD("Dev1", "Ctr0", 100, "PFI7", "Ctr1", "PFI15", true);
            //this.m_apdAPD2 = new SIS.Hardware.APD("Dev1", "Ctr3", 20, "PFI31", "Ctr2", "PFI35", true);
            //this.m_pdPhotoDiode = new SIS.Hardware.PhotoDiode("Dev2", "Ctr0", "80MHzTimebase", "RTSI0", "ai0");

            // Create a new ColoredRichTextBoxAppender and give it a standard layout.
            this.m_ColRTApp = new DevDefined.Common.Appenders.ColoredRichTextBoxAppender(this.richTextBox1, 1000, 500);
            this.m_ColRTApp.Layout = new PatternLayout();

            // Add the appender to the log4net root. 
            ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetLoggerRepository()).Root.AddAppender(this.m_ColRTApp);

            // For Analog stage controllers we need a global timing source.
            //m_clckGlobalSync = new TimingClock();

            #region Piezo

            // The piezo stage is the most critical hardware resource. To prevent conflicts it is created as a singleton instance.
            //this.m_Stage = SIS.Hardware.PIDigitalStage.Instance;
            this.m_Stage = SIS.Hardware.NIAnalogStage.Instance;


            // Hook up EventHandler methods to the events of the stage.
            this.m_Stage.PositionChanged += new EventHandler(m_Stage_PositionChanged);
            this.m_Stage.ErrorOccurred += new EventHandler(m_Stage_ErrorOccurred);
            this.m_Stage.EngagedChanged += new EventHandler(m_Stage_EngagedChanged);

            this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();

            #endregion
        }

        void m_Stage_EngagedChanged(object sender, EventArgs e)
        {
            // The text indicating stage status should always be updated.
            this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();
        }

        void m_frmScanSettingsForm_UpdateParameters(object sender, EventArgs e)
        {
            // Get to the information passed along in the EventArgs.
            this.m_scnstSettings = ((NotifyEventArgs)e).Settings;

            // Make sure the changes to the settings are visible to the user.
            ScanPropertiesToScreen();
        }

        /// <summary>
        /// Initializes all buttons on the form when the form is created.
        /// </summary>
        private void InitInterface()
        {
            // Make sure the progress bar is hidden.
            m_frmPBar.Visible = false;
            m_frmTrajectoryForm.Visible = false;

            //this.btnScanStart.Enabled = false;
            //this.btnStageOFF.Enabled = false;
            this.btnCountRate.Enabled = true;
            //this.btnStop.Enabled = false;

            // Indicate that the stage is not yet brought online.
            lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
            lblStageVoltageEngaged.Text = "OFFLINE";

            // Specify how the input validation should notify the user of invalid input.
            this.valprovSISValidationProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        // Display all properties of the scan.
        private void ScanPropertiesToScreen()
        {
            ScanDocument _docDocument = this.Document as ScanDocument;
            this.m_txtbxScanPropertiesFromFile.Text = string.Empty;

            this.m_txtbxScanPropertiesFromFile.Text =
                "Exp. Date:       " + _docDocument.Modified.ToString() + "\r\n" +
                "Scan Duration:   " + _docDocument.ScanDuration.ToString() + "\r\n" +
                "Scan Axes:       " + _docDocument.ScanAxes.ToString() + "\r\n" +
                "----------------------------------------------\r\n" +
                "Image Width Px:  " + _docDocument.ImageWidthPx.ToString() + "\r\n" +
                "Image Heigth Px: " + _docDocument.ImageHeightPx.ToString() + "\r\n" +
                "Image Depth Px:  " + _docDocument.ImageDepthPx.ToString() + "\r\n" +

                "X Over Scan px:  " + _docDocument.XOverScanPx.ToString() + "\r\n" +
                "Y Over Scan px:  " + _docDocument.YOverScanPx.ToString() + "\r\n" +
                "Z Over Scan px:  " + _docDocument.ZOverScanPx.ToString() + "\r\n" +

                "Initial X nm:  " + _docDocument.InitialX.ToString() + "\r\n" +
                "Initial Y nm: " + _docDocument.InitialY.ToString() + "\r\n" +
                "Initial Z nm:  " + _docDocument.InitialZ.ToString() + "\r\n" +

                "Image Width nm:  " + _docDocument.XScanSizeNm.ToString() + "\r\n" +
                "Image Heigth nm: " + _docDocument.YScanSizeNm.ToString() + "\r\n" +
                "Image Depth nm:  " + _docDocument.ZScanSizeNm.ToString() + "\r\n";
        }

        // Called to update the data display area of the UI.
        private void UpdateUI()
        {
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Update the UI with the current voltage to stage.
            txtbxCurrXPos.Text = this.m_Stage.XPosition.ToString();
            txtbxCurrYPos.Text = this.m_Stage.YPosition.ToString();
            txtbxCurrZPos.Text = this.m_Stage.ZPosition.ToString();
            textBox1.Text = this.m_apdAPD1.TotalSamplesAcuired.ToString();

            // Get the in memory bitmaps to the screen.
            PaintToScreen();

            // Process any events that might be waiting.
            Application.DoEvents();
        }

        // Called to re-enable controls after a scan.
        private void EnableCtrls()
        {
            // Disable the Scan button because validation is always necessary before scanning can start.
            this.btnFrameStart.Enabled = true;
            this.btnCountRate.Enabled = true;
            this.btnStop.Enabled = true;
        }

        // Called to enable controls during a scan in progress.
        private void DisableCtrls()
        {
            // Disable the Scan button.
            this.btnFrameStart.Enabled = false;
            this.btnCountRate.Enabled = false;
            this.btnStop.Enabled = true;
        }

        #region Form Eventhandlers

        // Handle closing of the form.
        private void ScanViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_Stage.IsInitialized)
                {
                    throw new SIS.Hardware.StageNotReleasedException("The stage was not released! Please use stage control to turn it off!");
                }
                this.m_Stage.PositionChanged -= new EventHandler(m_Stage_PositionChanged);
                this.m_Stage.ErrorOccurred -= new EventHandler(m_Stage_ErrorOccurred);
                this.m_Stage.EngagedChanged -= new EventHandler(m_Stage_EngagedChanged);
            }

            catch (SIS.Hardware.StageNotReleasedException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void btnCountRate_Click(object sender, EventArgs e)
        {
            if (this.m_frmCountRate == null)
            {
                this.m_frmCountRate = new CountRateForm();
            }

            this.m_frmCountRate.ShowDialog();
        }

        // Restore the full image after a Zoom operation.
        private void btnImageFit_Click(object sender, EventArgs e)
        {
            this.scanImageControl2.FitToScreen();

            // Update the UI.
            UpdateUI();
        }

        private void m_btnScanSettings_Click(object sender, EventArgs e)
        {
            ScanDocument _docDocument = this.Document as ScanDocument;

            this.m_frmScanSettingsForm = new ScanSettingsForm(this.m_scnstSettings);

            this.m_frmScanSettingsForm.UpdateParameters += new EventHandler(m_frmScanSettingsForm_UpdateParameters);

            this.m_frmScanSettingsForm.Visible = true;
        }

        #endregion

        private void UpdateProgress(int __iProgress)
        {
            m_frmPBar.Progress = __iProgress;
        }

        private void ShowProgress()
        {
            m_frmPBar.Visible = true;
        }

        private void HideProgress()
        {
            m_frmPBar.Visible = false;
            m_frmPBar.Progress = 0;
        }

        #endregion


        #region Document Related Methods

        protected override void OnUpdateDocument()
        {
            ScanPropertiesToScreen();
            PaintToScreen();
        }

        protected override void OnUpdateView(object update)
        {
            ScanPropertiesToScreen();
            PaintToScreen();
        }

        protected override void OnInitialUpdate()
        {
            this.m_scnstSettings = ((ScanDocument)this.Document).Settings;
            ScanPropertiesToScreen();
            PaintToScreen();
        }

        #endregion


        #region Scan Drawing

        public static uint[] PostProcessData(
            UInt32[] __ui32Rawdata, int width, int height)
        {
            int szUint = sizeof(uint);
            // Finally we return the processed data.
            // In this case, no processing is necessary, data are already in the correct order.
            UInt32[] buffer = new UInt32[width];
            UInt32[] processed = __ui32Rawdata;
            for (int i = 1; i < height; i += 2)
            {
                Buffer.BlockCopy(__ui32Rawdata, i * width * szUint, buffer, 0,width * szUint);
                buffer.Reverse();
                Buffer.BlockCopy(buffer, 0, processed, i * width * szUint, width * szUint);
            }
            return processed;
        }

        private void PaintToScreen()
        {
            ScanDocument _docDocument = this.Document as ScanDocument;
            Bitmap _bmpTemp;
            // Create two bitmaps, one for the scan, one for the colorbar.
            this.scanImageControl1.ImageHeight = _docDocument.XScanSizeNm;
            this.scanImageControl1.ImageWidth = _docDocument.YScanSizeNm;
            this.scanImageControl1.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            this.scanImageControl1.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
            //    _docDocument.MaxIntensity[0],
            //    _docDocument.MinIntensity[0],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    false,
            //    false);

            _bmpTemp = Utility.DrawScanToBmp(PostProcessData(_docDocument.GetChannelData(0), _docDocument.ImageWidthPx, _docDocument.ImageHeightPx),
                _docDocument.MaxIntensity[0],
                _docDocument.MinIntensity[0],
                _docDocument.ImageWidthPx,
                _docDocument.ImageHeightPx,
                _docDocument.XOverScanPx,
                _docDocument.YOverScanPx,
                this.chkbxCorrectedImage.Checked,
                true,
                false,
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD1[0] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
            //    _docDocument.MaxIntensity[0],
            //    _docDocument.MinIntensity[0],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    true,
            //    false,
            //    false);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD1[1] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
            //    _docDocument.MaxIntensity[0],
            //    _docDocument.MinIntensity[0],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    true,
            //    false);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD1[2] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
            //    _docDocument.MaxIntensity[0],
            //    _docDocument.MinIntensity[0],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    false,
            //    true);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD1[3] = _bmpTemp;

            //this.scanImageControl2.ImageHeight = _docDocument.XScanSizeNm;
            //this.scanImageControl2.ImageWidth = _docDocument.YScanSizeNm;
            //this.scanImageControl2.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            //this.scanImageControl2.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
            //    _docDocument.MaxIntensity[1],
            //    _docDocument.MinIntensity[1],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    false,
            //    false);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD2[0] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
            //    _docDocument.MaxIntensity[1],
            //    _docDocument.MinIntensity[1],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    true,
            //    false,
            //    false);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD2[1] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
            //    _docDocument.MaxIntensity[1],
            //    _docDocument.MinIntensity[1],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    true,
            //    false);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD2[2] = _bmpTemp;

            //_bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
            //    _docDocument.MaxIntensity[1],
            //    _docDocument.MinIntensity[1],
            //    _docDocument.ImageWidthPx,
            //    _docDocument.ImageHeightPx,
            //    _docDocument.XOverScanPx,
            //    _docDocument.YOverScanPx,
            //    this.chkbxCorrectedImage.Checked,
            //    false,
            //    false,
            //    true);

            //_bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            //m_bmpBitmapsAPD2[3] = _bmpTemp;

            //if (this.checkBox8.Checked)
            //{
            //    this.m_filter.OverlayImage = m_bmpBitmapsAPD2[3];
            //    System.Drawing.Bitmap newImage = this.m_filter.Apply(m_bmpBitmapsAPD1[2]);
            //    this.scanImageControl1.Image = newImage;
            //    this.scanImageControl2.Image = m_bmpBitmapsAPD2[1];
            //}

            //else if (this.checkBox9.Checked)
            //{
            //    this.m_filter.OverlayImage = m_bmpBitmapsAPD1[3];
            //    System.Drawing.Bitmap newImage = this.m_filter.Apply(m_bmpBitmapsAPD2[2]);
            //    this.scanImageControl2.Image = newImage;
            //    this.scanImageControl1.Image = m_bmpBitmapsAPD1[1];
            //}

            //else
            //{
            //    if (this.checkBox4.Checked)
            //    {
            //        this.scanImageControl2.Image = m_bmpBitmapsAPD2[2];
            //    }
            //    if (this.checkBox5.Checked)
            //    {
            //        this.scanImageControl2.Image = m_bmpBitmapsAPD2[3];
            //    }
            //    if (!this.checkBox4.Checked & !this.checkBox5.Checked)
            //    {

            //        this.scanImageControl2.Image = m_bmpBitmapsAPD2[1];
            //    }

            //    if (this.checkBox6.Checked)
            //    {
            //        this.scanImageControl1.Image = m_bmpBitmapsAPD1[2];
            //    }
            //    if (this.checkBox7.Checked)
            //    {
            //        this.scanImageControl1.Image = m_bmpBitmapsAPD1[3];
            //    }
            //    if (!this.checkBox6.Checked & !this.checkBox7.Checked)
            //    {
            //        this.scanImageControl1.Image = m_bmpBitmapsAPD1[1];
            //    }
            //}

            this.scanImageControl1.Image = _bmpTemp;
            this.scanImageControl2.Image = _bmpTemp;

            this.drwcnvColorBar1.Image = Utility.RainbowColorBar(_docDocument.ImageWidthPx, _docDocument.MinIntensity[0], _docDocument.MaxIntensity[0], this.chkbxNormalized.Checked, this.checkBox6.Checked, this.checkBox7.Checked);

            this.lblColorBarMaxInt1.Text = _docDocument.MaxIntensity[0].ToString();
            this.lblColorBarMinInt1.Text = _docDocument.MinIntensity[0].ToString();

            // Paint the bitmaps to screen.
            this.scanImageControl1.FitToScreen();

            this.drwcnvColorBar2.Image = Utility.RainbowColorBar(_docDocument.ImageWidthPx, _docDocument.MinIntensity[1], _docDocument.MaxIntensity[1], this.chkbxNormalized.Checked, this.checkBox4.Checked, this.checkBox5.Checked);

            this.lblColorBarMaxInt2.Text = _docDocument.MaxIntensity[1].ToString();
            this.lblColorBarMinInt2.Text = _docDocument.MinIntensity[1].ToString();

            // Paint the bitmaps to screen.
            this.scanImageControl2.FitToScreen();
        }

        // When we select a different display mode for the scan images we have to re-paint
        private void chkbxCorrectedImage_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
        }

        // When we select a different display mode for the scan images we have to re-paint
        private void chkbxNormalized_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();

            this.checkBox5.Checked = false;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
            this.checkBox4.Checked = false;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
            this.checkBox7.Checked = false;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
            this.checkBox6.Checked = false;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
            this.checkBox9.Checked = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            PaintToScreen();
            this.checkBox8.Checked = false;
        }

        // Handles coordinate selection by double click on the scan image for APD1
        private void scanImageControl1_OnPositionSelected(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            this.m_dXSel = ((this.scanImageControl1.XPositionSelected / (double)_docDocument.ImageWidthPx) * _docDocument.XScanSizeNm) + _docDocument.InitialX + _docDocument.XBorderWidth;
            this.m_dYSel = ((this.scanImageControl1.YPositionSelected / (double)_docDocument.ImageHeightPx) * _docDocument.YScanSizeNm) + _docDocument.InitialY;

            MessageBox.Show("Selected Physical X: " + this.m_dXSel.ToString() + "\r\n"
                + "Selected Physical Y: " + this.m_dYSel.ToString() + "\r\n"
                + "\r\n"
                + "These take InitalX and Y and Border X into account!\r\n"
                + "InitX: " + _docDocument.InitialX.ToString() + "\r\n"
                + "InitY: " + _docDocument.InitialY.ToString() + "\r\n"
                + "BorderX: " + _docDocument.XBorderWidth.ToString());

            if (this.m_dXSel - _docDocument.XBorderWidth < 0)
            {
                this.m_txtbxGoToX.Text = "0";
            }
            else
            {
                this.m_txtbxGoToX.Text = (this.m_dXSel - _docDocument.XBorderWidth).ToString();
            }
            this.m_txtbxGoToX.Text = (this.m_dXSel - _docDocument.XBorderWidth).ToString();
            this.m_txtbxGoToY.Text = this.m_dYSel.ToString();
        }

        // Handles coordinate selection by double click on the scan image for APD2
        private void scanImageControl2_OnPositionSelected(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            this.m_dXSel = ((this.scanImageControl2.XPositionSelected / (double)_docDocument.ImageWidthPx) * _docDocument.XScanSizeNm) + _docDocument.InitialX + _docDocument.XBorderWidth;
            this.m_dYSel = ((this.scanImageControl2.YPositionSelected / (double)_docDocument.ImageHeightPx) * _docDocument.YScanSizeNm) + _docDocument.InitialY;

            MessageBox.Show("Selected Physical X: " + this.m_dXSel.ToString() + "\r\n"
                + "Selected Physical Y: " + this.m_dYSel.ToString() + "\r\n"
                + "\r\n"
                + "These take InitalX and Y and Border X into account!\r\n"
                + "InitX: " + _docDocument.InitialX.ToString() + "\r\n"
                + "InitY: " + _docDocument.InitialY.ToString() + "\r\n"
                + "BorderX: " + _docDocument.XBorderWidth.ToString());

            if (this.m_dXSel - _docDocument.XBorderWidth < 0)
            {
                this.m_txtbxGoToX.Text = "0";
            }
            else
            {
                this.m_txtbxGoToX.Text = (this.m_dXSel - _docDocument.XBorderWidth).ToString();
            }
            this.m_txtbxGoToX.Text = (this.m_dXSel - _docDocument.XBorderWidth).ToString();
            this.m_txtbxGoToY.Text = this.m_dYSel.ToString();
        }

        #endregion


        #region Stage Control

        private void btnStageON_Click(object __oSender, EventArgs __evargsE)
        {
            // Connect to the controller hardware and initialize it.
            this.m_Stage.Initialize();

            // Initialize stage control and update status indicator only if INIT worked.
            if (this.m_Stage.IsInitialized)
            {
                // We cannot turn the stage on twice!
                this.btnStageON.Enabled = false;
                // Feedback to UI.
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Lime);
                // We might want to turn it off.
                this.btnStageOFF.Enabled = true;
            }
            else
            {
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }

            // The text indicating stage status should always be updated.
            this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();

            // Update the UI.
            this.UpdateUI();
        }

        private void btnStageOFF_Click(object __oSender, EventArgs __evargsE)
        {
            // Disconnect from the stage hardware.
            this.m_Stage.Release();

            // Release stage control and update status.
            if (!this.m_Stage.IsInitialized)
            {
                this.btnStageOFF.Enabled = false;
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
                this.lblStageVoltageEngaged.Text = m_Stage.IsInitialized.ToString();
                this.btnStageON.Enabled = true;
            }

            // Update the UI.
            UpdateUI();
        }

        private void btnZeroStage_Click(object __oSender, EventArgs __evargsE)
        {
            double[] _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = 0.0;
            _dXYCoordinates[1] = 0.0;
            _dXYCoordinates[2] = 0.0;
            this.bckgwrkPerformMove.RunWorkerAsync(_dXYCoordinates);
        }

        private void btnMoveAbs_Click(object __oSender, EventArgs __evargsE)
        {
            double[] _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = Convert.ToDouble(this.m_txtbxGoToX.Text);
            _dXYCoordinates[1] = Convert.ToDouble(this.m_txtbxGoToY.Text);
            _dXYCoordinates[2] = Convert.ToDouble(this.m_txtbxGoToZ.Text);
            this.bckgwrkPerformMove.RunWorkerAsync(_dXYCoordinates);
        }

        private void bckgwrkPerformMove_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
        {
            double[] _dXYCoordinates = (double[])__evargsE.Argument;
            this.m_Stage.MoveAbs(_dXYCoordinates[0], _dXYCoordinates[1], _dXYCoordinates[2]);
        }

        void m_Stage_PositionChanged(object __oSender, EventArgs __evargsE)
        {
            if (InvokeRequired)
            {
                Invoke(new UIUpdateDelegate(this.UpdateUI));
            }
            else
            {
                this.UpdateUI();
            }
        }

        void m_Stage_ErrorOccurred(object __oSender, EventArgs __evargsE)
        {
            MessageBox.Show(m_Stage.CurrentError);
        }

        #endregion


        #region Scan/Experimental Control

        private void btnFrameStart_Click(object __oSender, EventArgs __evargsE)
        {
            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            if (this.checkBox2.Checked)
            {
                int _iIndex = 0;
                string _sTemp = string.Empty;
                string _sTempPath = _docDocument.FilePath;

                if (this.m_nupdFilenameCount.Value == 0)
                {
                    _sTempPath = _sTempPath.Replace(".dat", "000.dat");
                }

                if (this.m_nupdFilenameCount.Value < 100)
                {
                    _sTemp = "0" + this.m_nupdFilenameCount.Value.ToString();
                }
                if (this.m_nupdFilenameCount.Value < 10)
                {
                    _sTemp = "00" + this.m_nupdFilenameCount.Value.ToString();
                }

                _iIndex = _sTempPath.LastIndexOf(".");
                _sTempPath = _sTempPath.Remove(_iIndex - 3, 3);
                _sTempPath = _sTempPath.Replace(".dat", _sTemp + ".dat");

                _docDocument.SaveDocument(_sTempPath);
            }

            this.m_scnstSettings.InitXNm = this.m_Stage.XPosition;
            this.m_scnstSettings.InitYNm = this.m_Stage.YPosition;
            this.m_scnstSettings.InitZNm = this.m_Stage.ZPosition;


            // Store the settings to the document.
            _docDocument.AllocateData(this.m_scnstSettings);

            // Generate the requested Scanmode. The settings can now be fetched from the document itself.
            // Currently the MaxSpeed and CycleTime are not used in the Scanmode object.
            SIS.Library.ComboBoxItem<Type> item = (SIS.Library.ComboBoxItem<Type>)this.scanModeComboBox1.SelectedItem;

            object[] _oScanParameters = {
                                 _docDocument.ImageWidthPx,
                                 _docDocument.ImageHeightPx,
                                 _docDocument.XOverScanPx,
                                 _docDocument.YOverScanPx,
                                 _docDocument.XScanSizeNm,
                                 _docDocument.YScanSizeNm,
                                 10,
                                 2};

            ConstructorInfo[] ci = item.Value.GetConstructors();
            Scanmode m_BiScan = (Scanmode)ci[0].Invoke(_oScanParameters);

            _docDocument.ScanAxes = 1;
            _docDocument.XBorderWidth = m_BiScan.BorderWidthX;
            

            // Indicate that the document data was modified by the scan operation just performed.
            // This will prompt a notification when the user tries to exit without saving.
            _docDocument.Modified = true;

            this.m_frmTrajectoryForm.Visible = true;
            this.m_frmTrajectoryForm.NMCoordinates = m_BiScan.ScanCoordinates;

            // Continue with prepping and eventually running the scan.
            PrepnRunScan(m_BiScan);


        }

        private void PrepnRunScan(Scanmode __scnmScan)
        {
            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Disable the controls so the user cannot interfere with the scan. Only stopping the scan will be allowed.
            DisableCtrls();

            // Get the new experimental settings to screen.
            this.ScanPropertiesToScreen();

            // Check if the stage is definitely engaged and ready.... if not all other operations would be useless!
            if (m_Stage.IsInitialized)
            {
                // Make sure the Stop button works.
                this.btnStop.Enabled = true;

                this.m_apdAPD1.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                //this.m_apdAPD2.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);

                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                bckgwrkPerformScan.RunWorkerAsync(__scnmScan);
                wrkUpdate.RunWorkerAsync();
            }

            // Update the UI.
            //UpdateUI();
        }

        private void bckgwrkPerformScan_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
        {
            int szUint32 = sizeof(UInt32);

            // Boolean value to indicate wheter or not the running scan should be stopped.
            bool _bStop = false;

            // Access the document object that holds all the data.
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            Scanmode _Scan = (Scanmode)__evargsE.Argument;

            // This int keeps track of the total number of samples already acquired. It is obviously zero at the beginning of measurement.
            int _readsamples1 = 0;
            int _readsamples2 = 0;

            // The array that will be assigned the current photon counts in the buffer.
            UInt32[] _ui32SingleReadValues1;
            UInt32[] _ui32SingleReadValues2;

            // The array that will hold the total samples already acquired. It is used as a temporary store for the measurement data
            // because the measured data needs to be processed before it can be assigned to the actual document object.
            UInt32[] _ui32AllReadValues1 = new UInt32[_docDocument.PixelCount];
            UInt32[] _ui32AllReadValues2 = new UInt32[_docDocument.PixelCount];

            if (!this.checkBoxStack.Checked)
            {
                this.m_Stage.MoveAbs(
                    Convert.ToDouble(this.m_txtbxGoToX.Text),
                    Convert.ToDouble(this.m_txtbxGoToY.Text),
                    Convert.ToDouble(this.m_txtbxGoToZ.Text));
            }
            else
            {
                this.m_Stage.MoveAbs(
                    Convert.ToDouble(this.m_txtbxGoToX.Text),
                    Convert.ToDouble(this.m_txtbxGoToY.Text),
                    Convert.ToDouble(this.m_txtbxStackMin.Text));
            }

            double stackPos = Convert.ToDouble(this.m_txtbxStackMin.Text);
            double stackInc = Convert.ToDouble(this.m_txtbxStackInc.Text);
            double stackEnd = Convert.ToDouble(this.m_txtbxStackMax.Text);

            // Start the APD. It will now count photons every time it is triggered by either a clock or a digital controller.
            this.m_apdAPD1.StartAPDAcquisition();

            // Initiate stage scan movement.
            this.m_Stage.Scan(
                _Scan,
                _docDocument.TimePPixel,
                this.checkBox1.Checked,
                Convert.ToDouble(this.textBox5.Text),
                Convert.ToInt32(this.txtDelay.Text),
                this.checkBoxWobble.Checked,
                Convert.ToDouble(this.txtWobbleAmp.Text),
                this.checkBoxXY.Checked);

            while (_bStop != true)
            {
                // Update the UI every 0.1 seconds, more than fast enough.
                //Thread.Sleep(1);

                // Perform a read of all samples currently in the buffer.
                if (_readsamples1 < _docDocument.PixelCount)
                {
                    if (this.m_apdAPD1.IsRunning)
                    {
                        _ui32SingleReadValues1 = this.m_apdAPD1.Read(5000);

                        Buffer.BlockCopy(_ui32SingleReadValues1, 0, _ui32AllReadValues1, _readsamples1 * szUint32, 2000 * szUint32 );
                        _readsamples1 = _readsamples1 + _ui32SingleReadValues1.Length;

                        //if (_ui32SingleReadValues1.Length >= _docDocument.PixelCount - _readsamples1)
                        //{
                        //    // Add the read samples to the previously read samples in memory.
                        //    for (int _i = 0; _i < _docDocument.PixelCount - _readsamples1; _i++)
                        //    {
                        //        _ui32AllReadValues1[_readsamples1 + _i] = _ui32SingleReadValues1[_i];
                        //    }

                        //    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                        //    _readsamples1 = _docDocument.PixelCount;
                        //}
                        //else
                        //{
                        //    // Add the read samples to the previously read samples in memory.
                        //    for (int _i = 0; _i < _ui32SingleReadValues1.Length; _i++)
                        //    {
                        //        _ui32AllReadValues1[_readsamples1 + _i] = _ui32SingleReadValues1[_i];
                        //    }

                        //    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                        //    _readsamples1 = _readsamples1 + _ui32SingleReadValues1.Length;
                        //}
                    }

                }

                _docDocument.StoreChannelData(0, _ui32AllReadValues1);
                _docDocument.StoreChannelData(1, _ui32AllReadValues1);

                _logger.Info(_readsamples1.ToString());

                if ((_readsamples1 == _docDocument.PixelCount))
                {
                    _readsamples1 = 0;
                    _readsamples2 = 0;

                    //if (this.checkBoxStack.Checked)
                    //{
                    //    stackPos = stackPos + stackInc;
                    //}

                    //if (!(this.checkBoxCont.Checked || this.checkBoxStack.Checked))
                    //{
                    //    _bStop = true;
                    //}
                    //else
                    //{
                    //    _bStop = false;
                    //    this.m_Stage.Stop();

                    //    Thread.Sleep(10);

                    //    this.m_apdAPD1.StopAPDAcquisition();
                    //    //this.m_apdAPD2.StopAPDAcquisition();

                    //    if (!this.checkBoxStack.Checked)
                    //    {
                    //        this.m_Stage.MoveAbs(
                    //            Convert.ToDouble(this.m_txtbxGoToX.Text),
                    //            Convert.ToDouble(this.m_txtbxGoToY.Text),
                    //            Convert.ToDouble(this.m_txtbxGoToZ.Text));
                    //    }
                    //    else
                    //    {
                    //        if (stackPos < stackEnd)
                    //        {
                    //            this.m_Stage.MoveAbs(
                    //                Convert.ToDouble(this.m_txtbxGoToX.Text),
                    //                Convert.ToDouble(this.m_txtbxGoToY.Text),
                    //                Convert.ToDouble(stackPos));
                    //        }
                    //        else
                    //        {
                    //            _bStop = true;
                    //        }
                    //    }

                    //    Thread.Sleep(10);

                    //    this.m_apdAPD1.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                    //    //this.m_apdAPD2.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);

                    //    if (!_bStop)
                    //    {
                    //        //Thread.Sleep(10);

                    //        //this.m_apdAPD1.StartAPDAcquisition();
                    //        ////this.m_apdAPD2.StartAPDAcquisition();

                    //        //if (!this.checkBoxStack.Checked)
                    //        //{
                    //        //    this.m_Stage.Scan(
                    //        //        _Scan,
                    //        //        _docDocument.TimePPixel,
                    //        //        false,
                    //        //        Convert.ToDouble(this.textBox5.Text),
                    //        //        Convert.ToInt32(this.txtDelay.Text),
                    //        //        this.checkBoxWobble.Checked,
                    //        //        Convert.ToDouble(this.txtWobbleAmp.Text),
                    //        //        this.checkBoxXY.Checked);
                    //        //}
                    //        //else
                    //        //{
                    //        //    this.m_Stage.Scan(
                    //        //        _Scan,
                    //        //        _docDocument.TimePPixel,
                    //        //        true,
                    //        //        Convert.ToDouble(this.textBox5.Text),
                    //        //        Convert.ToInt32(this.txtDelay.Text),
                    //        //        this.checkBoxWobble.Checked,
                    //        //        Convert.ToDouble(this.txtWobbleAmp.Text),
                    //        //        this.checkBoxXY.Checked);
                    //        //}

                    //        //_readsamples1 = 0;
                    //        //_readsamples2 = 0;
                    //    }

                    //}
                }

                // Update the UI.
                //if (InvokeRequired)
                //{
                //    // Update the rest of the UI.
                //    Invoke(new UIUpdateDelegate(UpdateUI));
                //}

                // Check if the worker was not cancelled.
                if (bckgwrkPerformScan.CancellationPending)
                {
                    __evargsE.Cancel = true;
                    _bStop = true;
                }
            }

            // Update the UI.
            if (InvokeRequired)
            {
                // Update the rest of the UI.
                Invoke(new UIUpdateDelegate(UpdateUI));
            }
            Thread.Sleep(100);

            // Stop the move task for the stage.
            this.m_apdAPD1.StopAPDAcquisition();
            //this.m_apdAPD2.StopAPDAcquisition();

        }

        private void btnStop_Click(object __oSender, EventArgs __evargsE)
        {
            // Cancel de backgroundworker.
            bckgwrkPerformScan.CancelAsync();
            wrkUpdate.CancelAsync();

            // Enable all controls again.
            EnableCtrls();

            // Disable the Stop button again.
            this.btnStop.Enabled = false;
            this.btnFrameStart.Enabled = true;
        }

        private void bckgwrkPerformScan_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs __evargsE)
        {

            // Actually stop the stage from scanning.
            this.m_Stage.Stop();

            // Wait a bit.
            Thread.Sleep(500);

            if (__evargsE.Cancelled)
            {
                // Inform the user.
                //MessageBox.Show("Scan Cancelled, press OK to zero stage.");
            }
            else
            {
                //MessageBox.Show("Scan Completed, press OK to zero stage.");
            }

            // Handle auto-save.
            if (this.m_chkbxAutosave.Checked)
            {
                // Save the document.
                this.Document.SaveDocument(this.Document.FilePath);

                // Increment the filename counter.
                this.m_nupdFilenameCount.Value = this.m_nupdFilenameCount.Value + 1;
            }

            this.m_Stage.MoveAbs(
                Convert.ToDouble(this.m_txtbxGoToX.Text),
                Convert.ToDouble(this.m_txtbxGoToY.Text),
                Convert.ToDouble(this.m_txtbxGoToZ.Text));

            this.EnableCtrls();
        }

        #endregion

        private void buttonExp_Click(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            uint[] _uintChannelData1 = _docDocument.GetChannelData(0);
            uint[] _uintChannelData2 = _docDocument.GetChannelData(1);
            int _iImageHeight = _docDocument.ImageHeightPx;
            int _iImageWidth = _docDocument.ImageWidthPx;

            int _iXOverScanPx = _docDocument.XOverScanPx;

            string _strData1 =
                "Exp. Date:       " + _docDocument.Modified.ToString() + "\r\n" +
                "Scan Duration:   " + _docDocument.ScanDuration.ToString() + "\r\n" +
                "Scan Axes:       " + _docDocument.ScanAxes.ToString() + "\r\n" +
                "----------------------------------------------\r\n" +
                "Image Width Px:  " + _docDocument.ImageWidthPx.ToString() + "\r\n" +
                "Image Heigth Px: " + _docDocument.ImageHeightPx.ToString() + "\r\n" +
                "Image Depth Px:  " + _docDocument.ImageDepthPx.ToString() + "\r\n" +

                "X Over Scan px:  " + _docDocument.XOverScanPx.ToString() + "\r\n" +
                "Y Over Scan px:  " + _docDocument.YOverScanPx.ToString() + "\r\n" +
                "Z Over Scan px:  " + _docDocument.ZOverScanPx.ToString() + "\r\n" +

                "Initial X nm:  " + _docDocument.InitialX.ToString() + "\r\n" +
                "Initial Y nm: " + _docDocument.InitialY.ToString() + "\r\n" +
                "Initial Z nm:  " + _docDocument.InitialZ.ToString() + "\r\n" +

                "Image Width nm:  " + _docDocument.XScanSizeNm.ToString() + "\r\n" +
                "Image Heigth nm: " + _docDocument.YScanSizeNm.ToString() + "\r\n" +
                "Image Depth nm:  " + _docDocument.ZScanSizeNm.ToString() + "\r\n";

            for (int _intI = 0; _intI < _iImageHeight; _intI++)
            {
                for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                {
                    _strData1 = _strData1 + _uintChannelData1[_intI * (_iImageWidth + _iXOverScanPx) + _intJ].ToString() + "\t";
                }
                _strData1 = _strData1 + "\r\n";
            }

            string _strData2 =
                "Exp. Date:       " + _docDocument.Modified.ToString() + "\r\n" +
                "Scan Duration:   " + _docDocument.ScanDuration.ToString() + "\r\n" +
                "Scan Axes:       " + _docDocument.ScanAxes.ToString() + "\r\n" +
                "----------------------------------------------\r\n" +
                "Image Width Px:  " + _docDocument.ImageWidthPx.ToString() + "\r\n" +
                "Image Heigth Px: " + _docDocument.ImageHeightPx.ToString() + "\r\n" +
                "Image Depth Px:  " + _docDocument.ImageDepthPx.ToString() + "\r\n" +

                "X Over Scan px:  " + _docDocument.XOverScanPx.ToString() + "\r\n" +
                "Y Over Scan px:  " + _docDocument.YOverScanPx.ToString() + "\r\n" +
                "Z Over Scan px:  " + _docDocument.ZOverScanPx.ToString() + "\r\n" +

                "Initial X nm:  " + _docDocument.InitialX.ToString() + "\r\n" +
                "Initial Y nm: " + _docDocument.InitialY.ToString() + "\r\n" +
                "Initial Z nm:  " + _docDocument.InitialZ.ToString() + "\r\n" +

                "Image Width nm:  " + _docDocument.XScanSizeNm.ToString() + "\r\n" +
                "Image Heigth nm: " + _docDocument.YScanSizeNm.ToString() + "\r\n" +
                "Image Depth nm:  " + _docDocument.ZScanSizeNm.ToString() + "\r\n";

            for (int _intI = 0; _intI < _iImageHeight; _intI++)
            {
                for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                {
                    _strData2 = _strData1 + _uintChannelData2[_intI * (_iImageWidth + _iXOverScanPx) + _intJ].ToString() + "\t";
                }
                _strData2 = _strData1 + "\r\n";
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH1.txt")))
                {
                    sr.Write(_strData1);
                    sr.Close();
                }
                using (StreamWriter sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH2.txt")))
                {
                    sr.Write(_strData2);
                    sr.Close();
                }

            }

        }

        private void wrkUpdate_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool _bStop = false;

            while(!_bStop)
            {
                // Update the UI.
                if (InvokeRequired)
                {
                    // Update the rest of the UI.
                    Invoke(new UIUpdateDelegate(UpdateUI));
                }
                else
                {
                    UpdateUI();
                }

                Thread.Sleep(20);
            }
            if (wrkUpdate.CancellationPending)
            {
                _bStop = true;
            }
        }
    }
}