// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanViewForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan view form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------



// for images

// for Marshalling data and other functions for managed <-> unmanaged code interactions
namespace SIS.Forms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    using AForge.Imaging.Filters;

    using DevDefined.Common.Appenders;

    using log4net;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;

    using SIS.Documents;
    using SIS.Hardware;
    using SIS.Library;
    using SIS.MDITemplate;
    using SIS.ScanModes;

    using Image = AForge.Imaging.Image;

    /// <summary>
    /// The scan view form.
    /// </summary>
    public partial class ScanViewForm : MdiViewForm
    {
        // The various essential objects representing hardware.
        // We need one or more APDs, a Piezo and a timing clock to sync everything.
        #region Fields

        /// <summary>
        /// The m_ stage.
        /// </summary>
        private readonly IPiezoStage m_Stage;

        /// <summary>
        /// The m_apd ap d 1.
        /// </summary>
        private readonly APD m_apdAPD1;

        /// <summary>
        /// The m_apd ap d 2.
        /// </summary>
        private readonly APD m_apdAPD2;

        // Arrays that will hold displays of the scans.
        /// <summary>
        /// The m_bmp bitmaps ap d 1.
        /// </summary>
        private readonly Bitmap[] m_bmpBitmapsAPD1 = new Bitmap[4];

        /// <summary>
        /// The m_bmp bitmaps ap d 2.
        /// </summary>
        private readonly Bitmap[] m_bmpBitmapsAPD2 = new Bitmap[4];

        // NEW STUFF probably needs cleaning...

        /// <summary>
        /// The m_filter.
        /// </summary>
        private readonly Add m_filter = new Add();

        /// <summary>
        /// The m_frm p bar.
        /// </summary>
        private readonly ProgressBarForm m_frmPBar = new ProgressBarForm();

        /// <summary>
        /// The m_frm trajectory form.
        /// </summary>
        private readonly TrajectoryPlotForm m_frmTrajectoryForm = new TrajectoryPlotForm();

        // Informing the user of Debug output.
        /// <summary>
        /// The m_ col rt app.
        /// </summary>
        private ColoredRichTextBoxAppender m_ColRTApp;

        /// <summary>
        /// The m_clck global sync.
        /// </summary>
        private TimingClock m_clckGlobalSync;

        /// <summary>
        /// The m_d x sel.
        /// </summary>
        private double m_dXSel;

        /// <summary>
        /// The m_d y sel.
        /// </summary>
        private double m_dYSel;

        /// <summary>
        /// The m_frm scan settings form.
        /// </summary>
        private ScanSettingsForm m_frmScanSettingsForm;

        /// <summary>
        /// The m_pd photo diode.
        /// </summary>
        private PhotoDiode m_pdPhotoDiode;

        /// <summary>
        /// The m_scnst settings.
        /// </summary>
        private ScanSettings m_scnstSettings;

        #endregion

        // Delegate involved in handling cross thread passing of data from Hardware to UI.

        // Scanviewform constructor. Initialization of some important stuff is done here.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanViewForm"/> class.
        /// </summary>
        public ScanViewForm()
        {
            // This call is required by the Windows Form Designer.
            this.InitializeComponent();

            // Initialize form buttons.
            this.InitInterface();

            // This version of the imaging suite operates with 2 APDs
            // Parameters needed are:
            // 1) Board ID
            // 2) Counter to generate a pulse with duration of bintime
            // 3) Timebase for bintime pulse
            // 4) APD data collection trigger
            // 5) Counter that counts TTLs from physical APD
            // 6) Input terminal carrying TTLs from physical APD
            // TODO: Put this stuff in some sort of config file/the Windows registry.
            this.m_apdAPD1 = new APD("Dev1", "Ctr1", 20, "PFI27", "Ctr0", "PFI39", this.checkBoxDMA.Checked);
            this.m_apdAPD2 = new APD("Dev1", "Ctr3", 20, "PFI31", "Ctr2", "PFI35", this.checkBoxDMA.Checked);

            // this.m_pdPhotoDiode = new SIS.Hardware.PhotoDiode("Dev2", "Ctr0", "80MHzTimebase", "RTSI0", "ai0");

            // Create a new ColoredRichTextBoxAppender and give it a standard layout.
            this.m_ColRTApp = new ColoredRichTextBoxAppender(this.richTextBox1, 1000, 500);
            this.m_ColRTApp.Layout = new PatternLayout();

            // Add the appender to the log4net root. 
            ((Hierarchy)LogManager.GetLoggerRepository()).Root.AddAppender(this.m_ColRTApp);

            // For Analog stage controllers we need a global timing source.
            // m_clckGlobalSync = new TimingClock();

            // The piezo stage is the most critical hardware resource. To prevent conflicts it is created as a singleton instance.
            this.m_Stage = PIDigitalStage.Instance;

            // Hook up EventHandler methods to the events of the stage.
            this.m_Stage.PositionChanged += this.m_Stage_PositionChanged;
            this.m_Stage.ErrorOccurred += this.m_Stage_ErrorOccurred;
            this.m_Stage.EngagedChanged += this.m_Stage_EngagedChanged;

            this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The ui update delegate.
        /// </summary>
        private delegate void UIUpdateDelegate();

        #endregion

        #region Methods

        /// <summary>
        /// The on initial update.
        /// </summary>
        protected override void OnInitialUpdate()
        {
            this.m_scnstSettings = ((ScanDocument)this.Document).Settings;
            this.ScanPropertiesToScreen();
            this.PaintToScreen();
        }

        /// <summary>
        /// The on update document.
        /// </summary>
        protected override void OnUpdateDocument()
        {
            this.ScanPropertiesToScreen();
            this.PaintToScreen();
        }

        /// <summary>
        /// The on update view.
        /// </summary>
        /// <param name="update">
        /// The update.
        /// </param>
        protected override void OnUpdateView(object update)
        {
            this.ScanPropertiesToScreen();
            this.PaintToScreen();
        }

        /// <summary>
        /// The disable ctrls.
        /// </summary>
        private void DisableCtrls()
        {
            // Disable the Scan button.
            this.btnScanStart.Enabled = false;
            this.btnValidateInput.Enabled = false;
            this.btnStop.Enabled = true;
        }

        /// <summary>
        /// The enable ctrls.
        /// </summary>
        private void EnableCtrls()
        {
            // Disable the Scan button because validation is always necessary before scanning can start.
            this.btnScanStart.Enabled = true;
            this.btnValidateInput.Enabled = true;
            this.btnStop.Enabled = true;
        }

        /// <summary>
        /// The hide progress.
        /// </summary>
        private void HideProgress()
        {
            this.m_frmPBar.Visible = false;
            this.m_frmPBar.Progress = 0;
        }

        /// <summary>
        ///     Initializes all buttons on the form when the form is created.
        /// </summary>
        private void InitInterface()
        {
            // Make sure the progress bar is hidden.
            this.m_frmPBar.Visible = false;
            this.m_frmTrajectoryForm.Visible = false;

            // this.btnScanStart.Enabled = false;
            // this.btnStageOFF.Enabled = false;
            this.btnValidateInput.Enabled = true;

            // this.btnStop.Enabled = false;

            // Indicate that the stage is not yet brought online.
            this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
            this.lblStageVoltageEngaged.Text = "OFFLINE";

            // Specify how the input validation should notify the user of invalid input.
            this.valprovSISValidationProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        /// <summary>
        /// The paint to screen.
        /// </summary>
        private void PaintToScreen()
        {
            var _docDocument = this.Document as ScanDocument;
            Bitmap _bmpTemp;

            // Create two bitmaps, one for the scan, one for the colorbar.
            this.scanImageControl1.ImageHeight = _docDocument.XScanSizeNm;
            this.scanImageControl1.ImageWidth = _docDocument.YScanSizeNm;
            this.scanImageControl1.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            this.scanImageControl1.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(0), 
                _docDocument.MaxIntensity[0], 
                _docDocument.MinIntensity[0], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                false, 
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD1[0] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(0), 
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
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD1[1] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(0), 
                _docDocument.MaxIntensity[0], 
                _docDocument.MinIntensity[0], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                true, 
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD1[2] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(0), 
                _docDocument.MaxIntensity[0], 
                _docDocument.MinIntensity[0], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                false, 
                true);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD1[3] = _bmpTemp;

            this.scanImageControl2.ImageHeight = _docDocument.XScanSizeNm;
            this.scanImageControl2.ImageWidth = _docDocument.YScanSizeNm;
            this.scanImageControl2.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            this.scanImageControl2.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(1), 
                _docDocument.MaxIntensity[1], 
                _docDocument.MinIntensity[1], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                false, 
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD2[0] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(1), 
                _docDocument.MaxIntensity[1], 
                _docDocument.MinIntensity[1], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                true, 
                false, 
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD2[1] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(1), 
                _docDocument.MaxIntensity[1], 
                _docDocument.MinIntensity[1], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                true, 
                false);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD2[2] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(
                _docDocument.GetChannelData(1), 
                _docDocument.MaxIntensity[1], 
                _docDocument.MinIntensity[1], 
                _docDocument.ImageWidthPx, 
                _docDocument.ImageHeightPx, 
                _docDocument.XOverScanPx, 
                _docDocument.YOverScanPx, 
                this.chkbxCorrectedImage.Checked, 
                false, 
                false, 
                true);

            _bmpTemp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            Image.FormatImage(ref _bmpTemp);

            this.m_bmpBitmapsAPD2[3] = _bmpTemp;

            if (this.checkBox8.Checked)
            {
                this.m_filter.OverlayImage = this.m_bmpBitmapsAPD2[3];
                Bitmap newImage = this.m_filter.Apply(this.m_bmpBitmapsAPD1[2]);
                this.scanImageControl1.Image = newImage;
                this.scanImageControl2.Image = this.m_bmpBitmapsAPD2[1];
            }
            else if (this.checkBox9.Checked)
            {
                this.m_filter.OverlayImage = this.m_bmpBitmapsAPD1[3];
                Bitmap newImage = this.m_filter.Apply(this.m_bmpBitmapsAPD2[2]);
                this.scanImageControl2.Image = newImage;
                this.scanImageControl1.Image = this.m_bmpBitmapsAPD1[1];
            }
            else
            {
                if (this.checkBox4.Checked)
                {
                    this.scanImageControl2.Image = this.m_bmpBitmapsAPD2[2];
                }

                if (this.checkBox5.Checked)
                {
                    this.scanImageControl2.Image = this.m_bmpBitmapsAPD2[3];
                }

                if (!this.checkBox4.Checked & !this.checkBox5.Checked)
                {
                    this.scanImageControl2.Image = this.m_bmpBitmapsAPD2[1];
                }

                if (this.checkBox6.Checked)
                {
                    this.scanImageControl1.Image = this.m_bmpBitmapsAPD1[2];
                }

                if (this.checkBox7.Checked)
                {
                    this.scanImageControl1.Image = this.m_bmpBitmapsAPD1[3];
                }

                if (!this.checkBox6.Checked & !this.checkBox7.Checked)
                {
                    this.scanImageControl1.Image = this.m_bmpBitmapsAPD1[1];
                }
            }

            this.drwcnvColorBar1.Image = Utility.RainbowColorBar(
                _docDocument.ImageWidthPx, 
                _docDocument.MinIntensity[0], 
                _docDocument.MaxIntensity[0], 
                this.chkbxNormalized.Checked, 
                this.checkBox6.Checked, 
                this.checkBox7.Checked);

            this.lblColorBarMaxInt1.Text = _docDocument.MaxIntensity[0].ToString();
            this.lblColorBarMinInt1.Text = _docDocument.MinIntensity[0].ToString();

            // Paint the bitmaps to screen.
            this.scanImageControl1.FitToScreen();

            this.drwcnvColorBar2.Image = Utility.RainbowColorBar(
                _docDocument.ImageWidthPx, 
                _docDocument.MinIntensity[1], 
                _docDocument.MaxIntensity[1], 
                this.chkbxNormalized.Checked, 
                this.checkBox4.Checked, 
                this.checkBox5.Checked);

            this.lblColorBarMaxInt2.Text = _docDocument.MaxIntensity[1].ToString();
            this.lblColorBarMinInt2.Text = _docDocument.MinIntensity[1].ToString();

            // Paint the bitmaps to screen.
            this.scanImageControl2.FitToScreen();
        }

        /// <summary>
        /// The prepn run scan.
        /// </summary>
        /// <param name="__scnmScan">
        /// The __scnm scan.
        /// </param>
        private void PrepnRunScan(Scanmode __scnmScan)
        {
            // Acces the ScanDocument object related to this form.
            var _docDocument = this.Document as ScanDocument;

            // Disable the controls so the user cannot interfere with the scan. Only stopping the scan will be allowed.
            this.DisableCtrls();

            // Get the new experimental settings to screen.
            this.ScanPropertiesToScreen();

            // Check if the stage is definitely engaged and ready.... if not all other operations would be useless!
            if (this.m_Stage.IsInitialized)
            {
                // Make sure the Stop button works.
                this.btnStop.Enabled = true;

                // this.m_clckGlobalSync.SetupClock(this.m_clckGlobalSync.Frequency(_docDocument.TimePPixel, 0.1F));
                this.m_apdAPD1.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                this.m_apdAPD2.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);

                // this.m_pdPhotoDiode.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);

                // Prepare the stage control task for writing as many samples as necessary to complete the scan.
                this.m_Stage.Configure(_docDocument.TimePPixel * 2, _docDocument.PixelCount);

                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                this.bckgwrkPerformScan.RunWorkerAsync(__scnmScan);
            }

            // Update the UI.
            this.UpdateUI();
        }

        // Display all properties of the scan.
        /// <summary>
        /// The scan properties to screen.
        /// </summary>
        private void ScanPropertiesToScreen()
        {
            var _docDocument = this.Document as ScanDocument;
            this.m_txtbxScanPropertiesFromFile.Text = string.Empty;

            this.m_txtbxScanPropertiesFromFile.Text = "Exp. Date:       " + _docDocument.Modified + "\r\n"
                                                      + "Scan Duration:   " + _docDocument.ScanDuration + "\r\n"
                                                      + "Scan Axes:       " + _docDocument.ScanAxes + "\r\n"
                                                      + "----------------------------------------------\r\n"
                                                      + "Image Width Px:  " + _docDocument.ImageWidthPx + "\r\n"
                                                      + "Image Heigth Px: " + _docDocument.ImageHeightPx + "\r\n"
                                                      + "Image Depth Px:  " + _docDocument.ImageDepthPx + "\r\n"
                                                      + "X Over Scan px:  " + _docDocument.XOverScanPx + "\r\n"
                                                      + "Y Over Scan px:  " + _docDocument.YOverScanPx + "\r\n"
                                                      + "Z Over Scan px:  " + _docDocument.ZOverScanPx + "\r\n"
                                                      + "Initial X nm:  " + _docDocument.InitialX + "\r\n"
                                                      + "Initial Y nm: " + _docDocument.InitialY + "\r\n"
                                                      + "Initial Z nm:  " + _docDocument.InitialZ + "\r\n"
                                                      + "Image Width nm:  " + _docDocument.XScanSizeNm + "\r\n"
                                                      + "Image Heigth nm: " + _docDocument.YScanSizeNm + "\r\n"
                                                      + "Image Depth nm:  " + _docDocument.ZScanSizeNm + "\r\n";
        }

        // Called to update the data display area of the UI.

        // Handle closing of the form.
        /// <summary>
        /// The scan view form_ form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <exception cref="StageNotReleasedException">
        /// </exception>
        private void ScanViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.m_Stage.IsInitialized)
                {
                    throw new StageNotReleasedException(
                        "The stage was not released! Please use stage control to turn it off!");
                }

                this.m_Stage.PositionChanged -= this.m_Stage_PositionChanged;
                this.m_Stage.ErrorOccurred -= this.m_Stage_ErrorOccurred;
                this.m_Stage.EngagedChanged -= this.m_Stage_EngagedChanged;
            }
            catch (StageNotReleasedException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        // Validate user input for scan settings.
        // Valid ranges for the different input controls are set up through the designer by selecting the control,
        // Going to the properties pane and setting "Validationrule on validationprovider" to the desired values.

        /// <summary>
        /// The show progress.
        /// </summary>
        private void ShowProgress()
        {
            this.m_frmPBar.Visible = true;
        }

        /// <summary>
        /// The update progress.
        /// </summary>
        /// <param name="__iProgress">
        /// The __i progress.
        /// </param>
        private void UpdateProgress(int __iProgress)
        {
            this.m_frmPBar.Progress = __iProgress;
        }

        /// <summary>
        /// The update ui.
        /// </summary>
        private void UpdateUI()
        {
            var _docDocument = this.Document as ScanDocument;

            // Update the UI with the current voltage to stage.
            this.txtbxCurrXPos.Text = this.m_Stage.XPosition.ToString();
            this.txtbxCurrYPos.Text = this.m_Stage.YPosition.ToString();
            this.txtbxCurrZPos.Text = this.m_Stage.ZPosition.ToString();
            this.textBox1.Text = this.m_apdAPD1.TotalSamplesAcuired.ToString();
            this.textBox2.Text = this.m_apdAPD2.TotalSamplesAcuired.ToString();

            // if (this.m_apdAPD1.TotalSamplesAcuired > 0)
            // {
            // textBox3.Text = _docDocument.GetChannelData(0)[this.m_apdAPD1.TotalSamplesAcuired - 1].ToString();
            // }
            // if (this.m_apdAPD1.TotalSamplesAcuired > 0)
            // {
            // textBox4.Text = _docDocument.GetChannelData(1)[this.m_apdAPD2.TotalSamplesAcuired - 1].ToString();
            // }
            // Get the in memory bitmaps to the screen.
            this.PaintToScreen();

            // Process any events that might be waiting.
            Application.DoEvents();
        }

        /// <summary>
        /// The bckgwrk perform move_ do work.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void bckgwrkPerformMove_DoWork(object __oSender, DoWorkEventArgs __evargsE)
        {
            var _dXYCoordinates = (double[])__evargsE.Argument;
            this.m_Stage.MoveAbs(_dXYCoordinates[0], _dXYCoordinates[1], _dXYCoordinates[2]);
        }

        /// <summary>
        /// The bckgwrk perform scan_ do work.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void bckgwrkPerformScan_DoWork(object __oSender, DoWorkEventArgs __evargsE)
        {
            // Boolean value to indicate wheter or not the running scan should be stopped.
            bool _bStop = false;

            // Access the document object that holds all the data.
            var _docDocument = this.Document as ScanDocument;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            var _Scan = (Scanmode)__evargsE.Argument;

            // This int keeps track of the total number of samples already acquired. It is obviously zero at the beginning of measurement.
            int _readsamples1 = 0;
            int _readsamples2 = 0;

            // int _readsamplesa = 0;

            // The array that will be assigned the current photon counts in the buffer.
            uint[] _ui32SingleReadValues1;
            uint[] _ui32SingleReadValues2;

            // Double[] _dSingleReadValuesa;

            // The array that will hold the total samples already acquired. It is used as a temporary store for the measurement data
            // because the measured data needs to be processed before it can be assigned to the actual document object.
            var _ui32AllReadValues1 = new uint[_docDocument.PixelCount];
            var _ui32AllReadValues2 = new uint[_docDocument.PixelCount];

            // Double[] _dAllreadValuesa = new Double[_docDocument.PixelCount * 100];

            // List<UInt32> _lui32AllReadValues1 = new List<UInt32>(_docDocument.PixelCount);
            // List<UInt32> _lui32AllReadValues2 = new List<UInt32>(_docDocument.PixelCount);

            // Start the APD. It will now count photons every time it is triggered by either a clock or a digital controller.
            this.m_apdAPD1.StartAPDAcquisition();
            this.m_apdAPD2.StartAPDAcquisition();

            // this.m_pdPhotoDiode.StartAPDAcquisition();

            // Initiate stage scan movement.
            // this.m_Stage.Scan(_Scan, this.checkBox1.Checked);
            this.m_Stage.Scan(_Scan, this.checkBox1.Checked);

            // while ((_readsamples1 < _docDocument.PixelCount) & (_bStop != true))
            // while ((_readsamples1 < _docDocument.PixelCount) & (_readsamples2 < _docDocument.PixelCount) & (_bStop != true))
            // while ((_readsamples2 < _docDocument.PixelCount) & (_bStop != true))
            while (_bStop != true)
            {
                // Update the UI every 0.1 seconds, more than fast enough.
                Thread.Sleep(100);

                // Perform a read of all samples currently in the buffer.
                if (_readsamples1 < _docDocument.PixelCount)
                {
                    _ui32SingleReadValues1 = this.m_apdAPD1.Read();

                    // Add the read samples to the previously read samples in memory.
                    for (int _i = 0; _i < _ui32SingleReadValues1.Length; _i++)
                    {
                        _ui32AllReadValues1[_readsamples1 + _i] = _ui32SingleReadValues1[_i];

                        // For debug purposes.
                        // _ui32AllReadValues1[_readsamples1 + _i] = (UInt32)RandomClass.Next(1, 1600);
                    }

                    // _lui32AllReadValues1.AddRange(_ui32SingleReadValues1);

                    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                    _readsamples1 = _readsamples1 + _ui32SingleReadValues1.Length;
                }

                if (_readsamples2 < _docDocument.PixelCount)
                {
                    _ui32SingleReadValues2 = this.m_apdAPD2.Read();

                    for (int _i = 0; _i < _ui32SingleReadValues2.Length; _i++)
                    {
                        _ui32AllReadValues2[_readsamples2 + _i] = _ui32SingleReadValues2[_i];

                        // For debug purposes.
                        // _ui32AllReadValues2[_readsamples2 + _i] = (UInt32)RandomClass.Next(1, 1600);
                    }

                    // _lui32AllReadValues2.AddRange(_ui32SingleReadValues2);

                    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                    _readsamples2 = _readsamples2 + _ui32SingleReadValues2.Length;
                }

                // if (_readsamplesa < _docDocument.PixelCount * 100)
                // {
                // _dSingleReadValuesa = this.m_pdPhotoDiode.Read();

                // for (int _i = 0; _i < _dSingleReadValuesa.Length; _i++)
                // {
                // _dAllreadValuesa[_readsamplesa + _i] = _dSingleReadValuesa[_i];

                // // For debug purposes.
                // //_ui32AllReadValues2[_readsamples2 + _i] = (UInt32)RandomClass.Next(1, 1600);
                // }
                // //_lui32AllReadValues2.AddRange(_ui32SingleReadValues2);

                // // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                // _readsamplesa = _readsamplesa + _dSingleReadValuesa.Length;
                // Tracing.Ping("Analog Samples Read: " + _readsamplesa.ToString());
                // }

                // Assign processed data to the actual document opject. This should only be done in the case of bidirectional scanning.
                _docDocument.StoreChannelData(0, _Scan.PostProcessData(_ui32AllReadValues1));
                _docDocument.StoreChannelData(1, _Scan.PostProcessData(_ui32AllReadValues2));

                // _docDocument.StoreChannelData(0, _Scan.PostProcessData(_lui32AllReadValues1.ToArray()));
                // _docDocument.StoreChannelData(1, _Scan.PostProcessData(_lui32AllReadValues2.ToArray()));
                if ((_readsamples1 == _docDocument.PixelCount) & (_readsamples2 == _docDocument.PixelCount))
                {
                    if (!this.checkBoxCont.Checked)
                    {
                        _bStop = true;
                    }

                    if (this.checkBoxCont.Checked)
                    {
                        _bStop = false;
                        this.m_Stage.MoveAbs(0.0, 0.0, 0.0);
                        this.m_apdAPD1.StopAPDAcquisition();
                        this.m_apdAPD2.StopAPDAcquisition();
                        this.m_apdAPD1.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                        this.m_apdAPD2.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                        this.m_apdAPD1.StartAPDAcquisition();
                        this.m_apdAPD2.StartAPDAcquisition();
                        this.m_Stage.Scan(_Scan, false);
                        _readsamples1 = 0;
                        _readsamples2 = 0;
                    }
                }

                // Update the UI.
                if (this.InvokeRequired)
                {
                    // Get the in memory bitmap to the screen.
                    this.Invoke(new UIUpdateDelegate(this.PaintToScreen));

                    // Update the rest of the UI.
                    this.Invoke(new UIUpdateDelegate(this.UpdateUI));
                }

                // Check if the worker was not cancelled.
                if (this.bckgwrkPerformScan.CancellationPending)
                {
                    __evargsE.Cancel = true;
                    _bStop = true;
                }
            }

            // Stop the globalsync and dispose of it.
            // m_daqtskGlobalSync.Stop();
            // m_daqtskGlobalSync.Dispose();

            // Update the UI.
            if (this.InvokeRequired)
            {
                // Get the in memory bitmap to the screen.
                this.Invoke(new UIUpdateDelegate(this.PaintToScreen));

                // Update the rest of the UI.
                this.Invoke(new UIUpdateDelegate(this.UpdateUI));
            }

            Thread.Sleep(1000);

            // At the end of the scan, confirm the total amount of acquired samples to the user.
            MessageBox.Show(
                "\r\n\r\n X Position: " + this.m_Stage.XPosition + "\r\n Y Position: " + this.m_Stage.YPosition
                + "\r\n\r\n Samples read from APD1 Buffer: " + this.m_apdAPD1.TotalSamplesAcuired
                + "\r\n\r\n Samples read from APD2 Buffer: " + this.m_apdAPD2.TotalSamplesAcuired
                + "\r\n Samples stored to document for APD1: " + _readsamples1
                + "\r\n Samples stored to document for APD2: " + _readsamples2);

            // Stop the move task for the stage.
            // m_daqtskTimingPulse.Stop();
            this.m_apdAPD1.StopAPDAcquisition();
            this.m_apdAPD2.StopAPDAcquisition();

            // this.m_pdPhotoDiode.StopAPDAcquisition();
        }

        /// <summary>
        /// The bckgwrk perform scan_ run worker completed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void bckgwrkPerformScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs __evargsE)
        {
            if (__evargsE.Cancelled)
            {
                // Actually stop the stage from scanning.
                this.m_Stage.Stop();

                // Wait a bit.
                Thread.Sleep(2000);

                // Inform the user.
                MessageBox.Show("Scan Cancelled, press OK to zero stage.");
            }
            else
            {
                MessageBox.Show("Scan Completed, press OK to zero stage.");
            }

            // Handle auto-save.
            if (this.m_chkbxAutosave.Checked)
            {
                // Save the document.
                this.Document.SaveDocument(this.Document.FilePath);

                // Increment the filename counter.
                this.m_nupdFilenameCount.Value = this.m_nupdFilenameCount.Value + 1;
            }

            this.EnableCtrls();
            this.m_Stage.MoveAbs(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// The btn image fit_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnImageFit_Click(object sender, EventArgs e)
        {
            this.scanImageControl2.FitToScreen();

            // Update the UI.
            this.UpdateUI();
        }

        /// <summary>
        /// The btn move abs_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void btnMoveAbs_Click(object __oSender, EventArgs __evargsE)
        {
            var _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = Convert.ToDouble(this.m_txtbxGoToX.Text);
            _dXYCoordinates[1] = Convert.ToDouble(this.m_txtbxGoToY.Text);
            _dXYCoordinates[2] = Convert.ToDouble(this.m_txtbxGoToZ.Text);
            this.bckgwrkPerformMove.RunWorkerAsync(_dXYCoordinates);
        }

        /// <summary>
        /// The btn scan start_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void btnScanStart_Click(object __oSender, EventArgs __evargsE)
        {
            

            // Acces the ScanDocument object related to this form.
            var _docDocument = this.Document as ScanDocument;

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
                    _sTemp = "0" + this.m_nupdFilenameCount.Value;
                }

                if (this.m_nupdFilenameCount.Value < 10)
                {
                    _sTemp = "00" + this.m_nupdFilenameCount.Value;
                }

                _iIndex = _sTempPath.LastIndexOf(".");
                _sTempPath = _sTempPath.Remove(_iIndex - 3, 3);
                _sTempPath = _sTempPath.Replace(".dat", _sTemp + ".dat");

                _docDocument.SaveDocument(_sTempPath);
            }

            

            // Store the settings to the document.
            _docDocument.AllocateData(this.m_scnstSettings);

            // Generate the requested Scanmode. The settings can now be fetched from the document itself.
            // Currently the MaxSpeed and CycleTime are not used in the Scanmode object.
            var item = (ComboBoxItem<Type>)this.scanModeComboBox1.SelectedItem;

            object[] _oScanParameters =
                {
                    _docDocument.ImageWidthPx, _docDocument.ImageHeightPx, 
                    _docDocument.ImageDepthPx, _docDocument.XOverScanPx, _docDocument.YOverScanPx, 
                    _docDocument.ZOverScanPx, _docDocument.InitialX, _docDocument.InitialY, 
                    _docDocument.InitialZ, _docDocument.XScanSizeNm, _docDocument.YScanSizeNm, 
                    _docDocument.ZScanSizeNm, 10, 2, 1, 0.2
                };

            ConstructorInfo[] ci = item.Value.GetConstructors();
            var m_BiScan = (Scanmode)ci[0].Invoke(_oScanParameters);

            _docDocument.ScanAxes = (UInt16)m_BiScan.ScanAxes;
            _docDocument.XBorderWidth = m_BiScan.BorderWidthX;

            // Indicate that the document data was modified by the scan operation just performed.
            // This will prompt a notification when the user tries to exit without saving.
            _docDocument.Modified = true;

            this.m_frmTrajectoryForm.Visible = true;
            this.m_frmTrajectoryForm.NMCoordinates = m_BiScan.NMScanCoordinates;

            // Continue with prepping and eventually running the scan.
            this.PrepnRunScan(m_BiScan);
        }

        /// <summary>
        /// The btn stage of f_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void btnStageOFF_Click(object __oSender, EventArgs __evargsE)
        {
            // Disconnect from the stage hardware.
            this.m_Stage.Release();

            // Release stage control and update status.
            if (!this.m_Stage.IsInitialized)
            {
                this.btnStageOFF.Enabled = false;
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
                this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();
                this.btnStageON.Enabled = true;
            }

            // Update the UI.
            this.UpdateUI();
        }

        /// <summary>
        /// The btn stage o n_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
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

        /// <summary>
        /// The btn stop_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void btnStop_Click(object __oSender, EventArgs __evargsE)
        {
            // Cancel de backgroundworker.
            this.bckgwrkPerformScan.CancelAsync();

            // Enable all controls again.
            this.EnableCtrls();

            // Disable the Stop button again.
            this.btnStop.Enabled = false;
            this.btnScanStart.Enabled = true;
        }

        /// <summary>
        /// The btn validate input_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnValidateInput_Click(object sender, EventArgs e)
        {
            // bool _boolPrimaryValidationPassed = false;
            // bool _boolSecondaryValidationPassed = false;

            // _boolPrimaryValidationPassed = this.valprovSISValidationProvider.Validate();
            // this.valprovSISValidationProvider.ValidationMessages(!_boolPrimaryValidationPassed);

            // if (_boolPrimaryValidationPassed == false)
            // {
            // MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            // }
            // else
            // {
            // if (((Convert.ToUInt32(this.txtbxSetInitX.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000) &&
            // ((Convert.ToUInt32(this.txtbxSetInitY.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000))
            // _boolSecondaryValidationPassed = true;
            // //else
            // //MessageBox.Show(m_strInvalidScanSettingsMsg2 + m_iMaxPosition.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            // }

            // if ((_boolPrimaryValidationPassed == true) && (_boolSecondaryValidationPassed == true))
            // btnScanStart.Enabled = true;
            // else
            // btnScanStart.Enabled = false;
        }

        /// <summary>
        /// The btn zero stage_ click.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void btnZeroStage_Click(object __oSender, EventArgs __evargsE)
        {
            var _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = 0.0;
            _dXYCoordinates[1] = 0.0;
            _dXYCoordinates[2] = 0.0;
            this.bckgwrkPerformMove.RunWorkerAsync(_dXYCoordinates);
        }

        /// <summary>
        /// The button exp_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void buttonExp_Click(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            var _docDocument = this.Document as ScanDocument;

            uint[] _uintChannelData1 = _docDocument.GetChannelData(0);
            uint[] _uintChannelData2 = _docDocument.GetChannelData(1);
            int _iImageHeight = _docDocument.ImageHeightPx;
            int _iImageWidth = _docDocument.ImageWidthPx;

            int _iXOverScanPx = _docDocument.XOverScanPx;

            string _strData1 = "Exp. Date:       " + _docDocument.Modified + "\r\n" + "Scan Duration:   "
                               + _docDocument.ScanDuration + "\r\n" + "Scan Axes:       " + _docDocument.ScanAxes
                               + "\r\n" + "----------------------------------------------\r\n" + "Image Width Px:  "
                               + _docDocument.ImageWidthPx + "\r\n" + "Image Heigth Px: " + _docDocument.ImageHeightPx
                               + "\r\n" + "Image Depth Px:  " + _docDocument.ImageDepthPx + "\r\n" + "X Over Scan px:  "
                               + _docDocument.XOverScanPx + "\r\n" + "Y Over Scan px:  " + _docDocument.YOverScanPx
                               + "\r\n" + "Z Over Scan px:  " + _docDocument.ZOverScanPx + "\r\n" + "Initial X nm:  "
                               + _docDocument.InitialX + "\r\n" + "Initial Y nm: " + _docDocument.InitialY + "\r\n"
                               + "Initial Z nm:  " + _docDocument.InitialZ + "\r\n" + "Image Width nm:  "
                               + _docDocument.XScanSizeNm + "\r\n" + "Image Heigth nm: " + _docDocument.YScanSizeNm
                               + "\r\n" + "Image Depth nm:  " + _docDocument.ZScanSizeNm + "\r\n";

            string _strData2 = "Exp. Date:       " + _docDocument.Modified + "\r\n" + "Scan Duration:   "
                               + _docDocument.ScanDuration + "\r\n" + "Scan Axes:       " + _docDocument.ScanAxes
                               + "\r\n" + "----------------------------------------------\r\n" + "Image Width Px:  "
                               + _docDocument.ImageWidthPx + "\r\n" + "Image Heigth Px: " + _docDocument.ImageHeightPx
                               + "\r\n" + "Image Depth Px:  " + _docDocument.ImageDepthPx + "\r\n" + "X Over Scan px:  "
                               + _docDocument.XOverScanPx + "\r\n" + "Y Over Scan px:  " + _docDocument.YOverScanPx
                               + "\r\n" + "Z Over Scan px:  " + _docDocument.ZOverScanPx + "\r\n" + "Initial X nm:  "
                               + _docDocument.InitialX + "\r\n" + "Initial Y nm: " + _docDocument.InitialY + "\r\n"
                               + "Initial Z nm:  " + _docDocument.InitialZ + "\r\n" + "Image Width nm:  "
                               + _docDocument.XScanSizeNm + "\r\n" + "Image Heigth nm: " + _docDocument.YScanSizeNm
                               + "\r\n" + "Image Depth nm:  " + _docDocument.ZScanSizeNm + "\r\n";

            var dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH1.txt")))
                {
                    // write Channel 1 picture to ASCII file
                    sr.Write(_strData1); // write header info to file

                    for (int _intI = 0; _intI < _iImageHeight; _intI++)
                    {
                        // write pixel values to file
                        for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                        {
                            sr.Write(_uintChannelData1[_intI * (_iImageWidth + _iXOverScanPx) + _intJ] + "\t");
                        }

                        sr.Write("\r\n");
                    }

                    sr.Close();
                }

                using (var sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH2.txt")))
                {
                    // write Channel 2 picture to ASCII file
                    sr.Write(_strData2); // write header info to file

                    for (int _intI = 0; _intI < _iImageHeight; _intI++)
                    {
                        // write pixel values to file
                        for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                        {
                            sr.Write(_uintChannelData2[_intI * (_iImageWidth + _iXOverScanPx) + _intJ] + "\t");
                        }

                        sr.Write("\r\n");
                    }

                    sr.Close();
                }

                //// Save pixel arrays corresponding to CH1 and CH2 as bitmap files
                // int _iPixelCount = (_iImageWidth + _iXOverScanPx) * _iImageHeight;  //The capacity of the pixel buffer (usually equals to the overall pixel counts)
                // string _filePathAndName = dialog.FileName.Replace(".txt", "");  //make the file path and name without file type extension

                //// Create the bitmap
                // Bitmap bitmapImage2D = new Bitmap((_iImageWidth + _iXOverScanPx), _iImageHeight, PixelFormat.Format32bppRgb);

                //// CH1 to bitmap:
                //// Lock bitmap and return bitmap data                    
                // BitmapData bitmapData = bitmapImage2D.LockBits(new System.Drawing.Rectangle(0, 0, (_iImageWidth + _iXOverScanPx), _iImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

                //// Byte per byte copy of _uintChannelData1[] array to location given by the pointer bitmapData.Scan0 (basically to the bitmapImage2D)
                // Marshal.Copy((int[])(object)_uintChannelData1, 0, bitmapData.Scan0, _iPixelCount);  //note that we need to convert _uintChannelData1 from an unsigned array to a signed array in order to make the Marshal.Copy() work

                //// Unlock bitmap
                // bitmapImage2D.UnlockBits(bitmapData);

                //// Save bitmap image of CH1 to file
                // try
                // {
                // bitmapImage2D.Save(_filePathAndName + "_CH1.bmp", ImageFormat.Bmp);  //save the image as ".bmp" to file
                // }
                // catch (Exception ex)
                // {
                // MessageBox.Show("Exception:" + ex + "\r\nThe bitmap image file: " + _filePathAndName + "_CH1.bmp" + " could not be saved!");
                // }

                //// CH2 to bitmap:
                //// Lock bitmap and return bitmap data                    
                // bitmapData = bitmapImage2D.LockBits(new System.Drawing.Rectangle(0, 0, (_iImageWidth + _iXOverScanPx), _iImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

                //// Byte per byte copy of _uintChannelData1[] array to location given by the pointer bitmapData.Scan0 (basically to the bitmapImage2D)
                // Marshal.Copy((int[])(object)_uintChannelData2, 0, bitmapData.Scan0, _iPixelCount);  //note that we need to convert _uintChannelData1 from an unsigned array to a signed array in order to make the Marshal.Copy() work

                //// Unlock bitmap
                // bitmapImage2D.UnlockBits(bitmapData);

                //// Save bitmap image of CH2 to file
                // try
                // {
                // bitmapImage2D.Save(_filePathAndName + "_CH2.bmp", ImageFormat.Bmp);  //save the image as ".bmp" to file
                // }
                // catch (Exception ex)
                // {
                // MessageBox.Show("Exception:" + ex + "\r\nThe bitmap image file: " + _filePathAndName + "_CH2.bmp" + " could not be saved!");
                // }
            }
        }

        /// <summary>
        /// The check box 4_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();

            this.checkBox5.Checked = false;
        }

        /// <summary>
        /// The check box 5_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
            this.checkBox4.Checked = false;
        }

        /// <summary>
        /// The check box 6_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
            this.checkBox7.Checked = false;
        }

        /// <summary>
        /// The check box 7_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
            this.checkBox6.Checked = false;
        }

        /// <summary>
        /// The check box 8_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
            this.checkBox9.Checked = false;
        }

        /// <summary>
        /// The check box 9_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
            this.checkBox8.Checked = false;
        }

        /// <summary>
        /// The chkbx corrected image_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkbxCorrectedImage_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
        }

        // When we select a different display mode for the scan images we have to re-paint
        /// <summary>
        /// The chkbx normalized_ checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void chkbxNormalized_CheckedChanged(object sender, EventArgs e)
        {
            this.PaintToScreen();
        }

        /// <summary>
        /// The m_ stage_ engaged changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_Stage_EngagedChanged(object sender, EventArgs e)
        {
            // The text indicating stage status should always be updated.
            this.lblStageVoltageEngaged.Text = this.m_Stage.IsInitialized.ToString();
        }

        /// <summary>
        /// The m_ stage_ error occurred.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void m_Stage_ErrorOccurred(object __oSender, EventArgs __evargsE)
        {
            MessageBox.Show(this.m_Stage.CurrentError);
        }

        /// <summary>
        /// The m_ stage_ position changed.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="__evargsE">
        /// The __evargs e.
        /// </param>
        private void m_Stage_PositionChanged(object __oSender, EventArgs __evargsE)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new UIUpdateDelegate(this.UpdateUI));
            }
            else
            {
                this.UpdateUI();
            }
        }

        /// <summary>
        /// The m_btn scan settings_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_btnScanSettings_Click(object sender, EventArgs e)
        {
            var _docDocument = this.Document as ScanDocument;

            // this.m_frmScanSettingsForm = new ScanSettingsForm(_docDocument.Settings);
            this.m_frmScanSettingsForm = new ScanSettingsForm(this.m_scnstSettings);

            this.m_frmScanSettingsForm.UpdateParameters += this.m_frmScanSettingsForm_UpdateParameters;

            this.m_frmScanSettingsForm.Visible = true;
        }

        /// <summary>
        /// The m_frm scan settings form_ update parameters.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void m_frmScanSettingsForm_UpdateParameters(object sender, EventArgs e)
        {
            // Get to the information passed along in the EventArgs.
            this.m_scnstSettings = ((NotifyEventArgs)e).Settings;

            // Make sure the changes to the settings are visible to the user.
            this.ScanPropertiesToScreen();
        }

        // Handles coordinate selection by double click on the scan image for APD1
        /// <summary>
        /// The scan image control 1_ on position selected.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void scanImageControl1_OnPositionSelected(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            var _docDocument = this.Document as ScanDocument;

            this.m_dXSel = ((this.scanImageControl1.XPositionSelected / _docDocument.ImageWidthPx)
                            * _docDocument.XScanSizeNm) + _docDocument.InitialX + _docDocument.XBorderWidth;
            this.m_dYSel = ((this.scanImageControl1.YPositionSelected / _docDocument.ImageHeightPx)
                            * _docDocument.YScanSizeNm) + _docDocument.InitialY;

            MessageBox.Show(
                "Selected Physical X: " + this.m_dXSel + "\r\n" + "Selected Physical Y: " + this.m_dYSel + "\r\n"
                + "\r\n" + "These take InitalX and Y and Border X into account!\r\n" + "InitX: " + _docDocument.InitialX
                + "\r\n" + "InitY: " + _docDocument.InitialY + "\r\n" + "BorderX: " + _docDocument.XBorderWidth);

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
        /// <summary>
        /// The scan image control 2_ on position selected.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void scanImageControl2_OnPositionSelected(object sender, EventArgs e)
        {
            // Acces the ScanDocument object related to this form.
            var _docDocument = this.Document as ScanDocument;

            this.m_dXSel = ((this.scanImageControl2.XPositionSelected / _docDocument.ImageWidthPx)
                            * _docDocument.XScanSizeNm) + _docDocument.InitialX + _docDocument.XBorderWidth;
            this.m_dYSel = ((this.scanImageControl2.YPositionSelected / _docDocument.ImageHeightPx)
                            * _docDocument.YScanSizeNm) + _docDocument.InitialY;

            MessageBox.Show(
                "Selected Physical X: " + this.m_dXSel + "\r\n" + "Selected Physical Y: " + this.m_dYSel + "\r\n"
                + "\r\n" + "These take InitalX and Y and Border X into account!\r\n" + "InitX: " + _docDocument.InitialX
                + "\r\n" + "InitY: " + _docDocument.InitialY + "\r\n" + "BorderX: " + _docDocument.XBorderWidth);

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
    }
}