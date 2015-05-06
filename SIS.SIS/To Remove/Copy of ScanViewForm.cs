//using System;
//using System.Drawing;
//using System.Windows.Forms;
//using KUL.MDS.RIS.Documents;
//using KUL.MDS.RIS.Tools;
//using KUL.MDS.SerialTerminal;
//using KUL.MDS.RIS.Exceptions;
//using NationalInstruments.DAQmx;
//using System.Threading;
//using KUL.MDS.ScanModes;

//namespace KUL.MDS.RIS.Forms
//{
//    public partial class ScanViewForm : KUL.MDS.MDITemplate.MdiViewForm
//    {
//        #region Member Variables

//        private KUL.MDS.RIS.Forms.ProgressBarForm m_frmPBar = new ProgressBarForm();
//        private KUL.MDS.ScanModes.Scanmode m_BiScan;

//        // The Various NI-Daqmx tasks.
//        private Task m_daqtskAPDCount;
//        private Task m_daqtskTimingPulse;
//        private Task m_daqtskMoveStage;
//        private Task m_daqtskGlobalSync;

//        // Constant properties of the stage. These will be used in input validation and safe speed calculation for stage movement.
//        private double m_dNmPVolt = 10000.0;
//        private int m_iMaxPosition = 90000;

//        // Set global range for the Voltage outputs as an additional safety.
//        private double m_dVoltageMax = 10.0;
//        private double m_dVoltageMin = 0.0;

//        //private AnalogSingleChannelReader reader;
//        private AnalogMultiChannelWriter m_wrtrVoltageWriter;
//        private CounterReader m_rdrCountReader;

//        // Create variables to keep track of the currently set voltage to the Piezo stage.
//        private double m_dVoltageXCurrent;
//        private double m_dVoltageYCurrent;

//        // The UI will display data on the acquisition progress during the scan.
//        // More specifically, total samples currently sent to stage and total samples taken from APD.
//        private int m_iSamplesToStageCurrent;
//        private int m_iSamplesFromAPD;
//        private int m_iSampleDelta;

//        // Message strings
//        private string m_strInvalidScanSettingsMsg1 = "Danger, Will Robinson!! You provided illegal settings for the scan!\r\nMake sure you select:\r\n\r\nImage width: 1-512 px\r\nImage Size: 10-90000 nm\r\nTime per pixel: 1-100 ms";
//        private string m_strInvalidScanSettingsMsg2 = "Make sure that the startpositions for the scan, together with the image width do not exceed the movement range of the stage!\r\n\r\nThe stage maximum range is (nm): ";

//        #endregion


//        #region Delegates

//        private delegate void UIUpdateDelegate();
//        private delegate void ProgressUpdate(int _iProgress);

//        #endregion


//        #region Interface Related Methods

//        // Scanviewform constructor. Initialization of some important stuff is done here.
//        public ScanViewForm()
//        {
//            m_frmPBar.Visible = false;

//            // This call is required by the Windows Form Designer.
//            InitializeComponent();

//            // Initialize form buttons.
//            InitButtons();

//            // Setup Comms to the Laser Device.
//            // The Comms object will be available globally.
//            SetupComms();

//            // Indicate that the stage is not yet brought online.
//            lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
//            lblStageVoltageEngaged.Text = "OFFLINE";

//            // Specify how the input validation should notify the user of invalid input.
//            this.valprovRISValidationProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

//            // The stage should be at 0,0 voltage at program start.
//            m_dVoltageXCurrent = 0.0;
//            m_dVoltageYCurrent = 0.0;

//            m_iSamplesToStageCurrent = 0;
//            m_iSamplesFromAPD = 0;
//            m_iSampleDelta = 0;
//        }

//        // Method to setup Comms.
//        /// <summary>
//        /// Setup Comms to the Laser Device.
//        /// The Comms object will be available globally.
//        /// </summary>
//        private void SetupComms()
//        {
//            Settings.Read();
//            CommPort _comprtCom = CommPort.Instance;
//            _comprtCom.StatusChanged += OnStatusChanged;
//            _comprtCom.DataReceived += OnDataReceived;
//            _comprtCom.Open();
//        }

//        // Method to close Comms.
//        /// <summary>
//        /// Closes the Comms.
//        /// </summary>
//        private void CloseComms()
//        {
//            CommPort _comprtCom = CommPort.Instance;
//            if (_comprtCom.IsOpen)
//            {
//                _comprtCom.Send("L=0");
//                _comprtCom.Close();
//            }
//        }

//        // Method to init buttons.
//        /// <summary>
//        /// Initializes all buttons on the form when the form is created.
//        /// </summary>
//        private void InitButtons()
//        {
//            this.btnScanStart.Enabled = false;
//            this.btnStageOFF.Enabled = false;
//            this.btnValidateInput.Enabled = true;
//            this.btnZeroStage.Enabled = true;
//            this.btnStop.Enabled = false;
//        }

//        // Handle closing of the form.
//        private void ScanViewForm_FormClosing(object sender, FormClosingEventArgs e)
//        {
//            try
//            {
//                if (m_daqtskMoveStage != null)
//                {
//                    throw new StageNotReleasedException("The stage was not released! Please use stage control to turn it off!");
//                }

//                // Close Comms.
//                CloseComms();
//            }

//            catch (StageNotReleasedException ex)
//            {
//                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                e.Cancel = true;
//            }
//        }

//        // Moving the mouse with left button down influences the displayed area, thus labels should be updated.
//        private void drwcnvScanImage_MouseMove(object sender, MouseEventArgs e)
//        {
//            // Update the UI.
//            UpdateUI();
//        }

//        // Zooming influences the displayed area, thus labels should be updated.
//        protected override void OnMouseWheel(MouseEventArgs e)
//        {
//            // Update the UI.
//            UpdateUI();
//        }

//        // Validate user input for scan settings.
//        // Valid ranges for the different input controls are set up through the designer by selecting the control,
//        // Going to the properties pane and setting "Validationrule on validationprovider" to the desired values.
//        private void btnValidateInput_Click(object sender, EventArgs e)
//        {
//            bool _boolPrimaryValidationPassed = false;
//            bool _boolSecondaryValidationPassed = false;

//            _boolPrimaryValidationPassed = this.valprovRISValidationProvider.Validate();
//            this.valprovRISValidationProvider.ValidationMessages(!_boolPrimaryValidationPassed);

//            if (_boolPrimaryValidationPassed == false)
//            {
//                MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            }
//            else
//            {
//                if (((Convert.ToUInt32(this.txtbxSetInitX.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000) &&
//                    ((Convert.ToUInt32(this.txtbxSetInitY.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000))
//                    _boolSecondaryValidationPassed = true;
//                else
//                    MessageBox.Show(m_strInvalidScanSettingsMsg2 + m_iMaxPosition.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//            }

//            if ((_boolPrimaryValidationPassed == true) && (_boolSecondaryValidationPassed == true))
//                btnScanStart.Enabled = true;
//            else
//                btnScanStart.Enabled = false;
//        }

//        // Restore the full image after a Zoom operation.
//        private void btnImageFit_Click(object sender, EventArgs e)
//        {
//            this.drwcnvScanImage.FitToScreen();
//            // Update the UI.
//            UpdateUI();
//        }

//        // Display all properties of the scan.
//        private void ScanPropertiesToScreen()
//        {
//            // All experimental data are stored in a document object that is being passed to this form.
//            ScanDocument document = this.Document as ScanDocument;

//            txtbxImageSize.Text = document.ScanSize.ToString();
//            txtbxImageWidth.Text = document.ImageWidth.ToString();
//            txtbxTimePPixel.Text = document.TimePPixel.ToString();
//            txtbxInitX.Text = document.InitialX.ToString();
//            txtbxInitY.Text = document.InitialY.ToString();
//            txtbxOverScan.Text = document.OverScanPixels.ToString();
//            txtbxScanTime.Text = document.ScanDuration.ToString();
//        }

//        // Called to update the data display area of the UI.
//        private void UpdateUI()
//        {
//            ScanDocument _docDocument = this.Document as ScanDocument;

//            // Set the scales on the scan image.
//            double _XMin = Math.Round((_docDocument.ScanSize / _docDocument.ImageWidth) * this.drwcnvScanImage.Origin.X);
//            double _YMin = Math.Round((_docDocument.ScanSize / _docDocument.ImageWidth) * this.drwcnvScanImage.Origin.Y);

//            lblXAxisMin.Text = _XMin.ToString();
//            lblYAxisMin.Text = _YMin.ToString();

//            double _XMax = Math.Round((this.drwcnvScanImage.Origin.X + _docDocument.ImageWidth / (this.drwcnvScanImage.ZoomFactor / (512 / _docDocument.ImageWidth))) * (_docDocument.ScanSize / _docDocument.ImageWidth));
//            double _YMax = Math.Round((this.drwcnvScanImage.Origin.Y + _docDocument.ImageWidth / (this.drwcnvScanImage.ZoomFactor / (512 / _docDocument.ImageWidth))) * (_docDocument.ScanSize / _docDocument.ImageWidth));

//            lblXAxisMax.Text = _XMax.ToString() + " nm";
//            lblYAxisMax.Text = _YMax.ToString() + " nm";

//            // Update the UI with the current voltage to stage.
//            txtbxXVoltCurr.Text = m_dVoltageXCurrent.ToString();
//            txtbxYVoltCurr.Text = m_dVoltageYCurrent.ToString();

//            // Update UI with information on scan progress.
//            txtbxSamplesToStage.Text = m_iSamplesToStageCurrent.ToString();
//            txtbxSamplesFromAPD.Text = m_iSamplesFromAPD.ToString();
//            txtbxSampleDelta.Text = m_iSampleDelta.ToString();

//            // Get the in memory bitmap to the screen.
//            //PaintToScreen();

//            // Process any events that might be waiting.
//            Application.DoEvents();
//        }

//        // Called to enable controls during a scan in progress.
//        private void DisableCtrls()
//        {
//            this.grpbxLsrComms.Enabled = false;
//            this.grpbxStageStatus.Enabled = false;

//            // Disable the Scan button.
//            this.btnScanStart.Enabled = false;
//            this.btnValidateInput.Enabled = false;
//            this.btnZeroStage.Enabled = false;
//            this.btnStop.Enabled = true;
//        }

//        // Called to re-enable controls after a scan.
//        private void EnableCtrls()
//        {
//            this.grpbxLsrComms.Enabled = true;
//            this.grpbxStageStatus.Enabled = true;

//            // Disable the Scan button because validation is always necessary before scanning can start.
//            this.btnScanStart.Enabled = false;
//            this.btnValidateInput.Enabled = true;
//            this.btnZeroStage.Enabled = true;
//            this.btnStop.Enabled = false;
//        }

//        private void UpdateProgress(int __iProgress)
//        {
//            m_frmPBar.Progress = __iProgress;
//        }

//        private void ShowProgress()
//        {
//            m_frmPBar.Visible = true;
//        }

//        private void HideProgress()
//        {
//            m_frmPBar.Visible = false;
//            m_frmPBar.Progress = 0;
//        }

//        #endregion


//        #region Document Related Methods

//        protected override void OnUpdateDocument()
//        {
//            // This method is currently a stub. It needs to be here.
//        }

//        protected override void OnUpdateView(object update)
//        {
//            // This method is currently a stub. It needs to be here.
//        }

//        protected override void OnInitialUpdate()
//        {
//            ScanPropertiesToScreen();
//            PaintToScreen();
//        }

//        private void StoreExpSettingsToDocument(ScanDocument __docDocument)
//        {
//            // Write all user settings to the in memory ScanDocument
//            __docDocument.ImageWidth = Convert.ToInt32(this.txtbxSetImageWidth.Text.Trim());
//            __docDocument.InitialX = NmToVoltage(Convert.ToInt32(this.txtbxSetInitX.Text.Trim()));
//            __docDocument.InitialY = NmToVoltage(Convert.ToInt32(this.txtbxSetInitY.Text.Trim()));
//            __docDocument.ScanSize = Convert.ToDouble(this.txtbxSetImageWidthnm.Text.Trim());
//            __docDocument.TimePPixel = Convert.ToInt32(this.txtbxSetTimePPixel.Text.Trim());
//            __docDocument.OverScanPixels = Convert.ToInt32(this.txtbxOverScanPx.Text.Trim());
//        }

//        #endregion


//        #region Scan Drawing


//        // Uint32 intensity values are translated into RGB color values.
//        // The RGB palette is traversed from 0-255 0 0 / 255 0-255 0 / 255-0 255 0 / 0 255 0-255 / 0 255-0 255 / 0-255 0 255 / 255 0-255 255
//        // depending on the value of the original Uint32 value.
//        // Doing it like this will get you from Black, Yellow, Red, Green, Magenta, Blue, Violet to white in 1792 discrete steps.
//        private Color ColorPicker(int __iIntensity)
//        {
//            Color ColorPicker = new Color();
//            if (__iIntensity >= 1792)
//            {
//                ColorPicker = Color.FromArgb(255, 255, 255, 255);
//            }

//            else
//            {
//                switch (__iIntensity / 256)
//                {
//                    case 0: ColorPicker = Color.FromArgb(__iIntensity % 256, 0, 0);
//                        break;
//                    case 1: ColorPicker = Color.FromArgb(255, __iIntensity % 256, 0);
//                        break;
//                    case 2: ColorPicker = Color.FromArgb(255 - (__iIntensity % 256), 255, 0);
//                        break;
//                    case 3: ColorPicker = Color.FromArgb(0, 255, __iIntensity % 256);
//                        break;
//                    case 4: ColorPicker = Color.FromArgb(0, 255 - (__iIntensity % 256), 255);
//                        break;
//                    case 5: ColorPicker = Color.FromArgb(__iIntensity % 256, 0, 255);
//                        break;
//                    default: ColorPicker = Color.FromArgb(255, __iIntensity % 256, 255);
//                        break;
//                }

//            }
//            return ColorPicker;
//        }

//        private Bitmap DrawScanToBmp(UInt32[] __ui32Intensities, int __intImagewidth, int __iOverScanPx, bool __bCorrected, bool __bNormalized)
//        {
//            ScanDocument _docDocument = this.Document as ScanDocument;

//            double _dblIntensityCorr = 1.0;

//            // Max and Min intensity are ALWAYS recalculated on the most current data.
//            UInt32 _ui32MaxIntensity = 0;
//            UInt32 _ui32MinIntensity = 2147483647;
//            //UInt32 m_ui32OnScreenMax = 100;

//            Bitmap _bmpImage;

//            // See the Color ColorPicker method implementation for more details on the value of _intMaxColor.
//            int _intMaxColor = 1792;

//            // The new bitmap that holds the image.
//            if (__bCorrected)
//            {
//                _bmpImage = new Bitmap(__intImagewidth, __intImagewidth);
//            }
//            else
//            {
//                _bmpImage = new Bitmap(__intImagewidth + __iOverScanPx, __intImagewidth + __iOverScanPx);
//            }

//            // Initialize a FastBitmap object for unsafe writing using the new (empty) bitmap as the image source.
//            FastBitmap _fbmpFastBitmap = new FastBitmap(_bmpImage);

//            // Get Min and Max intensity value.
//            _ui32MaxIntensity = _docDocument.MaxIntensity;
//            _ui32MinIntensity = _docDocument.MinIntensity;


//            if (_ui32MaxIntensity > 1)
//            {
//                _dblIntensityCorr = (double)_intMaxColor / (double)_ui32MaxIntensity;
//            }

//            #region Comment on coordinate translations
//            // The general convention on bitmaps is that coordinate systems extend from top to bottom and left to right (row-column convention).
//            // The first pixel, (0,0) is thus located in the left topmost corner of the bitmap. This is important to know if one wants to relate 
//            // the bitmap image to the actual positioning of features on the physical sample.
//            //         X
//            // (b00)--(b10)--(b20)
//            //   |      |      |
//            // (b01)--(b11)--(b21)Y
//            //   |      |      |
//            // (b02)--(b12)--(b22)
//            //
//            // Image data is always supplied with the first value as the Bottom Left pixel of the physical sample. The first scanline will proceed 
//            // from left to right and consecutive scanlines will move up:
//            //
//            // The physical Image:       The array holding the data with corresponding bitmap coordinates:
//            // 
//            // -------------             [p00 p01 p02 p10 p11 p12 p20 p21 p22]
//            // |p20 p21 p22|               |   |   |   |   |   |   |   |   |
//            // |p10 p11 p12|              b02 b12 b22 b01 b11 b21 b00 b10 b20
//            // |p00 p01 p02|              Count down in the bimap (b coordinates) for Y and count up for X!!!
//            // -------------
//            //
//            // So if we cycle through the array using I and J where I represents rows (Y) in the image and J represents columns (X) we need to
//            // Count from 0 to ImageWidth for J (columns, X) and from Imagewidth to 0 for I (rows, Y) to assign consecutive elements of the 
//            // array to the image. You basically fill the bitmap from the bottom up...
//            //
//            // Processing on the data during aqcuisition will ensure that the data array is ALWAYS supplied in the same layout!
//            #endregion

//            if (__bNormalized)
//            {
//                for (int _intI = 0; _intI < __intImagewidth; _intI++)
//                {
//                    for (int _intJ = 0; _intJ < __intImagewidth + __iOverScanPx; _intJ++)
//                    {
//                        if (__bCorrected)
//                        {
//                            if (_intI % 2 == 0)
//                            {
//                                if (_intJ >= __iOverScanPx)
//                                {
//                                    _fbmpFastBitmap.SetPixel(_intJ - __iOverScanPx, __intImagewidth - _intI - 1, ColorPicker(Convert.ToInt16(Math.Round(_dblIntensityCorr * __ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]))));
//                                }
//                            }
//                            else
//                            {
//                                if (_intJ < __intImagewidth)
//                                {
//                                    _fbmpFastBitmap.SetPixel(_intJ, __intImagewidth - _intI - 1, ColorPicker(Convert.ToInt16(Math.Round(_dblIntensityCorr * __ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]))));
//                                }
//                            }
//                        }
//                        else
//                        {
//                            _fbmpFastBitmap.SetPixel(_intJ, (__intImagewidth + __iOverScanPx) - _intI - 1, ColorPicker(Convert.ToInt16(Math.Round(_dblIntensityCorr * __ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]))));
//                        }
//                    }
//                }
//            }
//            else
//            {
//                for (int _intI = 0; _intI < __intImagewidth; _intI++)
//                {
//                    for (int _intJ = 0; _intJ < __intImagewidth + __iOverScanPx; _intJ++)
//                    {
//                        if (__bCorrected)
//                        {
//                            if (_intI % 2 == 0)
//                            {
//                                if (_intJ >= __iOverScanPx)
//                                {
//                                    _fbmpFastBitmap.SetPixel(_intJ - __iOverScanPx, __intImagewidth - _intI - 1, ColorPicker((int)__ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]));
//                                }
//                            }
//                            else
//                            {
//                                if (_intJ < __intImagewidth)
//                                {
//                                    _fbmpFastBitmap.SetPixel(_intJ, __intImagewidth - _intI - 1, ColorPicker((int)__ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]));
//                                }
//                            }
//                        }
//                        else
//                        {
//                            _fbmpFastBitmap.SetPixel(_intJ, (__intImagewidth + __iOverScanPx) - _intI - 1, ColorPicker((int)__ui32Intensities[_intI * (__intImagewidth + __iOverScanPx) + _intJ]));
//                        }
//                    }
//                }
//            }



//            // Pass the updated bitmap back.
//            _bmpImage = _fbmpFastBitmap.Bitmap;

//            // Dispose of the unsafe bitmap object.
//            _fbmpFastBitmap.Release();

//            // Return the bitmap.
//            return _bmpImage;
//        }

//        private Bitmap RainbowColorBar(int __iImageWidth, UInt32 __ui32MinIntensity, UInt32 __ui32MaxIntensity, bool __bNormalized)
//        {
//            // The bitmap that will hold the color scale.
//            Bitmap _bmpBar = new Bitmap(20, __iImageWidth);

//            // An instance of the Fast Bitmap class for unsafe, fast pixel assignment.
//            FastBitmap _fbmpFastBitmap = new FastBitmap(_bmpBar);

//            // The maximum amount of discrete colors that we can generate.
//            int _intMaxColor = 1792;

//            // We might want to do intensity normalization on the scan image.
//            double _dblIntensityCorr = 1.0;

//            // Only calculate normalization if necessary.
//            if (__bNormalized)
//            {
//                _dblIntensityCorr = (double)_intMaxColor / (double)__ui32MaxIntensity;
//            }

//            //TODO: Remove this after testing.
//            //if (__ui32MaxIntensity > 1)
//            //{
//            //    _dblIntensityCorr = (double)_intMaxColor / (double)__ui32MaxIntensity;
//            //}


//            // TODO: Make this a bit cleaner.
//            for (int _iI = 0; _iI < __iImageWidth; _iI++)
//            {
//                for (int _iJ = 0; _iJ < 20; _iJ++)
//                {
//                    int _CurrentColor = (int)__ui32MinIntensity + _iI * ((int)__ui32MaxIntensity - (int)__ui32MinIntensity) / __iImageWidth;
//                    _fbmpFastBitmap.SetPixel(_iJ, (__iImageWidth - 1) - _iI, ColorPicker(Convert.ToInt16(Math.Round(_dblIntensityCorr * _CurrentColor))));
//                }
//            }

//            // Assign the newly created Bitmap.
//            _bmpBar = _fbmpFastBitmap.Bitmap;

//            // We don't need the Fast Bitmap anymore.
//            _fbmpFastBitmap.Release();

//            // Return the newly created Bitmap.
//            return _bmpBar;
//        }

//        private void PaintToScreen()
//        {
//            ScanDocument _docDocument = this.Document as ScanDocument;
//            // Create two bitmaps, one for the scan, one for the colorbar.
//            this.drwcnvScanImage.Image = DrawScanToBmp(_docDocument.Intensities, _docDocument.ImageWidth, _docDocument.OverScanPixels, this.chkbxCorrectedImage.Checked, this.chkbxNormalized.Checked);
//            this.drwcnvColorBar.Image = RainbowColorBar(_docDocument.ImageWidth, _docDocument.MinIntensity, _docDocument.MaxIntensity, this.chkbxNormalized.Checked);
//            this.lblColorBarMaxInt.Text = _docDocument.MaxIntensity.ToString();
//            this.lblColorBarMinInt.Text = _docDocument.MinIntensity.ToString();

//            // Paint the bitmaps to screen.
//            this.drwcnvScanImage.FitToScreen();
//            this.drwcnvScanImage.Invalidate();
//            this.drwcnvColorBar.Invalidate();
//        }

//        private void chkbxCorrectedImage_CheckedChanged(object sender, EventArgs e)
//        {
//            PaintToScreen();
//        }

//        private void chkbxNormalized_CheckedChanged(object sender, EventArgs e)
//        {
//            PaintToScreen();
//        }


//        #endregion


//        #region Synchronization

//        /// <summary>
//        /// Creates a global timing pulse train that will be made available on RTSI0. The Pulse train always has a duty cycle of .5
//        /// </summary>
//        /// <param name="__daqtskGlobalSync">The task object that will hold the task</param>
//        /// <param name="__dFreq">The frequency in Hz at which all timed tasks will run</param>
//        private void SetupGlobalSync(ref Task __daqtskGlobalSync, double __dFreq)
//        {
//            // Create a new task instance that will be passed to the globally available task.
//            Task _daqtskTask = new Task();

//            try
//            {
//                // Setup Global Sync Clock (GSC).
//                // Commit before start to speed things up later on when the task needs to be started.
//                _daqtskTask.COChannels.CreatePulseChannelFrequency("/Dev1/Ctr0", "Sync", COPulseFrequencyUnits.Hertz, COPulseIdleState.Low, 0.0, __dFreq, 0.1);
//                _daqtskTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);
//                _daqtskTask.Control(TaskAction.Verify);
//                _daqtskTask.Control(TaskAction.Commit);

//                // Be sure to route the timing pulse to the RTSI line to make it available on all the installed DAQ boards of the system.
//                DaqSystem.Local.ConnectTerminals("/Dev1/Ctr0InternalOutput", "/Dev1/RTSI0");
//                // Finally pass the task.
//                __daqtskGlobalSync = _daqtskTask;
//            }

//            catch (DaqException ex)
//            {
//                _daqtskTask.Dispose();
//                __daqtskGlobalSync = null;

//                // Inform the user about the error.
//                MessageBox.Show(ex.Message, "Error creating the GSC!");
//            }
//        }

//        /// <summary>
//        /// Properly disposes of the global timing pulse train after measurements of moves
//        /// </summary>
//        /// <param name="__daqtskGlobalSync">The task object that will hold the task</param>
//        private void DisposeGlobalSync(ref Task __daqtskGlobalSync)
//        {
//            try
//            {
//                __daqtskGlobalSync.Stop();
//                __daqtskGlobalSync.Dispose();
//                __daqtskGlobalSync = null;
//            }

//            catch (DaqException ex)
//            {
//                __daqtskGlobalSync = null;
//                // Inform the user about the error.
//                MessageBox.Show(ex.Message, "Error disposing the GSC!");
//            }
//        }

//        /// <summary>
//        /// Given a binning time for photon counting, a suitable frequency will be calculated to sync all tasks properly. 
//        /// See Raman Imaging Timing Implementation.pptx in /Documentation/ folder for more details.
//        /// </summary>
//        /// <param name="__iBinTime">The photon binning time in ms</param>
//        /// <returns></returns>
//        private double Frequency(double __dBinTime, double __dPadTime)
//        {
//            // All operations in the program will be synchronized to one single clock.
//            // An Edge will fire every x miliseconds. The edge timing depends on the time to scan every px plus a safety.
//            // It is calculated here.
//            double _dFreq = 1000 * (1 / (__dBinTime + __dPadTime));

//            return _dFreq;
//        }

//        #endregion


//        #region Validation

//        private double FindMin(double[,] __dArray, int __iRow, int __iCol)
//        {
//            double _dMin = __dArray[0, 0];

//            for (int _iR = 0; _iR < __iRow; _iR++)
//            {
//                for (int _iC = 0; _iC < __iCol; _iC++)
//                {
//                    if (__dArray[_iR, _iC] < _dMin)
//                    {
//                        _dMin = __dArray[_iR, _iC];
//                    }
//                }
//            }
//            return _dMin;
//        }

//        private double FindMax(double[,] __dArray, int __iRow, int __iCol)
//        {
//            double _dMax = __dArray[0, 0];

//            for (int _iR = 0; _iR < __iRow; _iR++)
//            {
//                for (int _iC = 0; _iC < __iCol; _iC++)
//                {
//                    if (__dArray[_iR, _iC] > _dMax)
//                    {
//                        _dMax = __dArray[_iR, _iC];
//                    }
//                }
//            }
//            return _dMax;
//        }

//        #endregion


//        #region Stage Control

//        private void btnStageON_Click(object __oSender, EventArgs __evargsE)
//        {
//            this.btnStageON.Enabled = false;

//            // Initialize stage control and update status indicator.
//            lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Lime);
//            lblStageVoltageEngaged.Text = PreInitStageControl(ref m_daqtskMoveStage);
//            PreInitStageControl(ref m_daqtskMoveStage);

//            this.btnStageOFF.Enabled = true;

//            // Update the UI.
//            UpdateUI();
//        }

//        private void btnStageOFF_Click(object __oSender, EventArgs __evargsE)
//        {
//            this.btnStageOFF.Enabled = false;

//            // Release stage control and update status.
//            lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
//            lblStageVoltageEngaged.Text = ReleaseStageControl(ref m_daqtskMoveStage);
//            this.btnStageON.Enabled = true;

//            // Update the UI.
//            UpdateUI();
//        }

//        private string PreInitStageControl(ref Task __daqtskStageMove)
//        {
//            string _strStatus;
//            Task _daqtskTask = new Task();

//            try
//            {
//                // Setup an Analog Out task to move the Piezo stage along X and Y.
//                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao0", "aoChannelX", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
//                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao1", "aoChannelY", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
//                _daqtskTask.Control(TaskAction.Verify);

//                // Assign the task.
//                __daqtskStageMove = _daqtskTask;

//                // Return a status indication for the stage.
//                _strStatus = "ONLINE";
//                return _strStatus;
//            }

//            catch (DaqException exception)
//            {
//                _daqtskTask.Dispose();
//                MessageBox.Show(exception.Message);

//                _strStatus = "INIT ERROR";
//                return _strStatus;
//            }
//        }

//        private void ConfigureStageControl(ref Task __daqtskStageMove, double __dFreq, int __iSteps)
//        {
//            try
//            {
//                __daqtskStageMove.Timing.ConfigureSampleClock("/Dev1/RTSI0", __dFreq, SampleClockActiveEdge.Rising, SampleQuantityMode.FiniteSamples, __iSteps);
//                __daqtskStageMove.Control(TaskAction.Verify);
//                __daqtskStageMove.Control(TaskAction.Commit);
//            }

//            catch (DaqException ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private string ReleaseStageControl(ref Task __daqtskStageMove)
//        {
//            string _strStatus;

//            MoveStageAbs(0, 0);

//            try
//            {
//                // Properly dispose of the AO tasks that control the piezo stage.
//                __daqtskStageMove.Stop();
//                __daqtskStageMove.Dispose();
//                __daqtskStageMove = null;

//                // Return a status indication for the stage.
//                _strStatus = "OFFLINE";
//                return _strStatus;
//            }

//            catch (DaqException ex)
//            {
//                MessageBox.Show(ex.Message);
//                _strStatus = "ERROR";
//                return _strStatus;
//            }
//        }

//        private double[,] PrecalculatedAbsMove(double __dInitVoltageX, double __dInitVoltageY, double __dFinVoltageX, double __dFinVoltageY, int __iSteps)
//        {
//            // Init some variables.
//            double _dCurrentVoltageX = __dInitVoltageX;
//            double _dCurrentVoltageY = __dInitVoltageY;

//            // Calculate the voltage resolution.
//            double _dVoltageResX = (__dFinVoltageX - __dInitVoltageX) / __iSteps;
//            double _dVoltageResY = (__dFinVoltageY - __dInitVoltageY) / __iSteps;

//            // Array to store the voltages for the entire move operation.
//            double[,] _dMovement = new double[2, __iSteps * 2];

//            // Calculate the actual voltages for the intended movement on X.
//            // Movement will be one axis at a time.
//            for (int _iI = 0; _iI < __iSteps; _iI++)
//            {
//                // Increment voltage. 
//                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
//                _dCurrentVoltageX = Math.Round((__dInitVoltageX + _dVoltageResX * (_iI + 1)), 4);

//                // Write voltage for X.
//                _dMovement[0, _iI] = _dCurrentVoltageX;

//                // Write voltage for Y.
//                _dMovement[1, _iI] = _dCurrentVoltageY;
//            }

//            // Calculate the actual voltages for the intended movement on X.
//            // Movement will be one axis at a time.
//            for (int _iI = 0; _iI < __iSteps; _iI++)
//            {
//                // Increment voltage. 
//                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
//                _dCurrentVoltageY = Math.Round((__dInitVoltageY + _dVoltageResY * (_iI + 1)), 4);

//                // Write voltage for X.
//                _dMovement[0, _iI + __iSteps] = _dCurrentVoltageX;

//                // Write voltage for Y.
//                _dMovement[1, _iI + __iSteps] = _dCurrentVoltageY;
//            }

//            return _dMovement;
//        }

//        private void MoveStageAbs(double __dXVolt, double __dYVolt)
//        {
//            try
//            {
//                // Local array that keeps the voltages for the current scan operation.
//                double[,] _dScanVoltages;

//                // Disable the Scan button.
//                this.btnScanStart.Enabled = false;

//                DisableCtrls();

//                // Calculate the voltages that make up the full scan.
//                _dScanVoltages = PrecalculatedAbsMove(m_dVoltageXCurrent, m_dVoltageYCurrent, __dXVolt, __dYVolt, 1000);

//                double _dMinV = FindMin(_dScanVoltages, 2, _dScanVoltages.Length / 2);
//                double _dMaxV = FindMax(_dScanVoltages, 2, _dScanVoltages.Length / 2);

//                if (_dMinV < m_dVoltageMin)
//                {
//                    throw new MinVoltageExceededException("The move you wish to execute will cause a voltage too LOW condition!" +
//                        "\r\n\r\nVoltage Value: " + _dMinV.ToString());
//                }

//                if (_dMaxV > m_dVoltageMax)
//                {
//                    throw new MinVoltageExceededException("The move you wish to execute will cause a voltage too HIGH condition!" +
//                        "\r\n\r\nVoltage Value: " + _dMaxV.ToString());
//                }

//                // At the end of the move the voltage applied to the stage will be the last value written to it.
//                // This value is kept in a global variable which will be used as a reference for future moves.
//                m_dVoltageXCurrent = _dScanVoltages[0, (_dScanVoltages.Length / 2) - 1];
//                this.txtbxXVoltCurr.Text = m_dVoltageXCurrent.ToString();
//                m_dVoltageYCurrent = _dScanVoltages[1, (_dScanVoltages.Length / 2) - 1];
//                this.txtbxYVoltCurr.Text = m_dVoltageYCurrent.ToString();

//                if (m_daqtskMoveStage != null)
//                {
//                    SetupGlobalSync(ref m_daqtskGlobalSync, 500.0);

//                    // Prepare the stage control task for writing as many samples as necessary to complete the scan.
//                    ConfigureStageControl(ref m_daqtskMoveStage, 500.0, _dScanVoltages.Length / 2);

//                    // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
//                    // enable continuous updates of the UI with scan data.
//                    bckgwrkPerformMove.RunWorkerAsync(_dScanVoltages);

//                    while (bckgwrkPerformMove.IsBusy)
//                    {
//                        Thread.Sleep(100);
//                        m_frmPBar.UpdateProgress();
//                        Application.DoEvents();
//                    }
//                }
//                else
//                {
//                    EnableCtrls();
//                    throw new StageNotEngagedException("The Piezo stage was not engaged properly, please use stage control to engage it!");
//                }
//            }

//            catch (MinVoltageExceededException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (MaxVoltageExceededException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (StageNotEngagedException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (DaqException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private void btnZeroStage_Click(object sender, EventArgs __evargsE)
//        {
//            MoveStageAbs(0, 0);
//        }

//        private void bckgwrkPerformMove_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
//        {
//            double _dProgress = 0.0;

//            if (InvokeRequired)
//            {
//                Invoke(new UIUpdateDelegate(ShowProgress));
//            }

//            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
//            double[,] _dCoordinates = (double[,])__evargsE.Argument;

//            // Objects to perform reads and writes on our DAQ tasks.
//            m_wrtrVoltageWriter = new AnalogMultiChannelWriter(m_daqtskMoveStage.Stream);

//            // Perform the actual AO write.
//            m_wrtrVoltageWriter.WriteMultiSample(false, (double[,])__evargsE.Argument);

//            // Start all four tasks in the correct order. Global sync should be last.
//            m_daqtskMoveStage.Start();
//            m_daqtskGlobalSync.Start();

//            while (m_daqtskMoveStage.IsDone != true)
//            {
//                // Update the UI every 0.1 seconds, more than fast enough.
//                Thread.Sleep(100);

//                // Update the voltages.
//                m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//                m_dVoltageXCurrent = _dCoordinates[0, m_iSamplesToStageCurrent - 1];
//                m_dVoltageYCurrent = _dCoordinates[1, m_iSamplesToStageCurrent - 1];

//                _dProgress = ((double)m_iSamplesToStageCurrent / (_dCoordinates.Length / 2)) * 100;

//                if (InvokeRequired)
//                {
//                    // Update the rest of the UI.
//                    Invoke(new UIUpdateDelegate(UpdateUI));
//                }

//                if (InvokeRequired)
//                {
//                    // Update the rest of the UI.
//                    Invoke(new ProgressUpdate(UpdateProgress), Convert.ToInt16(Math.Round(_dProgress)));
//                }
//            }

//            // Stop the globalsync and dispose of it.
//            m_daqtskGlobalSync.Stop();
//            m_daqtskGlobalSync.Dispose();

//            // Update the voltages.
//            m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//            m_dVoltageXCurrent = _dCoordinates[0, m_iSamplesToStageCurrent - 1];
//            m_dVoltageYCurrent = _dCoordinates[1, m_iSamplesToStageCurrent - 1];

//            _dProgress = ((double)m_iSamplesToStageCurrent / (_dCoordinates.Length / 2)) * 100;

//            // Stop the move task for the stage.
//            m_daqtskMoveStage.Stop();

//            // Dispose the writer and reader.
//            m_wrtrVoltageWriter = null;
//            m_rdrCountReader = null;

//            if (InvokeRequired)
//            {
//                // Update the rest of the UI.
//                Invoke(new UIUpdateDelegate(UpdateUI));
//            }

//            if (InvokeRequired)
//            {
//                // Update the rest of the UI.
//                Invoke(new ProgressUpdate(UpdateProgress), Convert.ToInt16(Math.Round(_dProgress)));
//            }

//            // Enable the UI controls again.
//            if (InvokeRequired)
//            {
//                Invoke(new UIUpdateDelegate(EnableCtrls));
//            }

//            if (InvokeRequired)
//            {
//                Invoke(new UIUpdateDelegate(HideProgress));
//            }
//        }

//        private void btnMoveAbs_Click(object sender, EventArgs e)
//        {
//            MoveStageAbs(NmToVoltage(Convert.ToInt16(this.txtbxGoToX.Text)), NmToVoltage(Convert.ToInt16(this.txtbxGoToY.Text)));
//        }

//        private double NmToVoltage(int __iNmCoordinate)
//        {
//            double _dVoltage = __iNmCoordinate / m_dNmPVolt;
//            return _dVoltage;
//        }

//        #endregion


//        #region Scan/Experimental Control

//        // Overview of the necessary measurement steps:
//        // --------------------------------------------
//        //
//        // TODO: Update this comment.

//        private void SetupAPDCountAndTiming(ref Task __daqtskTimingPulse, ref Task __daqtskAPDCount, double __dBinTime, int __iSteps)
//        {
//            try
//            {
//                // Calculate how many ticks the photon counting should take and how much it should be delayed to allow stage movement.
//                int _iBinTicks = Convert.ToInt32(__dBinTime * 80000);

//                // Create new task instances that will be passed to the globally available tasks.
//                Task _daqtskTiming = new Task();
//                Task _daqtskAPD = new Task();

//                // Setup a pulsechannel that will determine the bin time for photon counts.
//                // This channel will create a single delayed edge upon triggering by the global sync pulsetrain. High time of the pulse determines bin time.
//                _daqtskTiming.COChannels.CreatePulseChannelTicks("/Dev2/Ctr1", "TimedPulse", "/Dev2/80MHzTimebase", COPulseIdleState.Low, 80, 80, _iBinTicks);

//                // We want to sync voltage out and measurement without software intervention.
//                // Therefore we tap into the global sync pulsetrain which is available from the RTSI cable to sync photon counting with movement.
//                // For each pixel a single pulse with a high duration equal to the photon binning time will be generated.
//                _daqtskTiming.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev2/RTSI0", DigitalEdgeStartTriggerEdge.Rising);
//                _daqtskTiming.Triggers.StartTrigger.Retriggerable = true;
//                _daqtskTiming.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
//                _daqtskTiming.Control(TaskAction.Verify);
//                _daqtskTiming.Control(TaskAction.Commit);

//                // Setup countertask for the actual timed APD counting.
//                // We will actually measure the width of the counting timing pulse in # of ticks of the APD, thus effectively counting photons.
//                _daqtskAPD.CIChannels.CreatePulseWidthChannel("/Dev2/Ctr0", "CountAPD", 0.0, 1000000, CIPulseWidthStartingEdge.Rising, CIPulseWidthUnits.Ticks);
//                _daqtskAPD.CIChannels.All.PulseWidthTerminal = "/Dev2/Ctr1InternalOutput";
//                _daqtskAPD.CIChannels.All.CounterTimebaseSource = "/Dev2/PFI39";
//                // TODO: Evaluate the behavior of this setting. It might be necessary to deal with zero counts correctly although this is not likeley
//                // because an APD probably has non-zero dark count!
//                _daqtskAPD.CIChannels.All.DuplicateCountPrevention = true;

//                // We only want to collect as many counts as there are pixels or "steps" in the image.
//                // Every time we read from the buffer we will read all samples that are there at once.
//                _daqtskAPD.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, __iSteps);
//                _daqtskAPD.Stream.ReadAllAvailableSamples = true;

//                // Commit before start to speed things up.
//                _daqtskAPD.Control(TaskAction.Verify);
//                _daqtskAPD.Control(TaskAction.Commit);

//                // Finally pass the tasks.
//                __daqtskTimingPulse = _daqtskTiming;
//                __daqtskAPDCount = _daqtskAPD;
//            }

//            catch (DaqException ex)
//            {
//                __daqtskTimingPulse = null;
//                __daqtskAPDCount = null;

//                // Inform the user about the error.
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private void btnScanStart_Click(object __oSender, EventArgs __evargsE)
//        {
//            m_BiScan = new BidirScanMode();
//            // Continue with prepping and eventually running the scan.
//            PrepnRunScan(m_BiScan);
//        }

//        private void btnScan1DX_Click(object sender, EventArgs e)
//        {
//            m_BiScan = new XLineScanMode();
//            // Continue with prepping and eventually running the scan.
//            PrepnRunScan(m_BiScan);
//        }

//        private void btnScanM1DX_Click(object sender, EventArgs e)
//        {

//            m_BiScan = new XLineMultiScanMode();
//            // Continue with prepping and eventually running the scan.
//            PrepnRunScan(m_BiScan);
//        }

//        private void PrepnRunScan(Scanmode __scnmScan)
//        {
//            try
//            {
//                // Acces the ScanDocument object related to this form.
//                ScanDocument _docDocument = this.Document as ScanDocument;
//                //EnableCtrls();
//                // Disable the controls so the user cannot interfere with the scan. Only stopping the scan will be allowed.
//                //DisableCtrls();

//                // Update the in-memory document object with the new scan settings.
//                // From now on we can get all necessary parameters from the document itself.
//                StoreExpSettingsToDocument(_docDocument);

//                // Get the new experimental settings to screen.
//                ScanPropertiesToScreen();

//                // Calculate the voltages that make up the full scan.
//                // Local array that keeps the voltages for the current scan operation.
//                __scnmScan.PreCalculatedScan(_docDocument.ImageWidth,
//                    _docDocument.OverScanPixels,
//                    _docDocument.InitialX,
//                    _docDocument.InitialY,
//                    _docDocument.ScanSize,
//                    m_dNmPVolt);

//                MoveStageAbs(_docDocument.InitialX, _docDocument.InitialY);

//                Thread.Sleep(2000);
//                this.btnStop.Enabled = true;
//                double _dMinV = FindMin(__scnmScan.Coordinates, 2, __scnmScan.Coordinates.Length / 2);
//                double _dMaxV = FindMax(__scnmScan.Coordinates, 2, __scnmScan.Coordinates.Length / 2);

//                if (_dMinV < m_dVoltageMin)
//                {
//                    throw new MinVoltageExceededException(
//                        "The scan you wish to perform will cause a voltage too LOW condition!\r\n\r\n"
//                        + "Voltage Value: " + _dMinV.ToString());
//                }

//                if (_dMaxV > m_dVoltageMax)
//                {
//                    throw new MinVoltageExceededException(
//                        "The scan you wish to perform will cause a voltage too HIGH condition!\r\n\r\n"
//                        + "Voltage Value: " + _dMaxV.ToString());
//                }

//                if (m_daqtskMoveStage != null)
//                {
//                    SetupGlobalSync(ref m_daqtskGlobalSync, Frequency(_docDocument.TimePPixel, 0.1));
//                    SetupAPDCountAndTiming(ref m_daqtskTimingPulse, ref m_daqtskAPDCount, _docDocument.TimePPixel, __scnmScan.Coordinates.Length / 2);

//                    // Prepare the stage control task for writing as many samples as necessary to complete the scan.
//                    ConfigureStageControl(ref m_daqtskMoveStage, Frequency(_docDocument.TimePPixel, 0.1), __scnmScan.Coordinates.Length / 2);

//                    // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
//                    // enable continuous updates of the UI with scan data.
//                    bckgwrkPerformScan.RunWorkerAsync(__scnmScan);

//                    // Indicate that the document data was modified by the scan operation just performed.
//                    // This will prompt a notification when the user tries to exit without saving.
//                    _docDocument.Modified = true;
//                }
//                else
//                {
//                    EnableCtrls();
//                    throw new StageNotEngagedException("The Piezo stage was not engaged properly, please use stage control to engage it!");
//                }

//                // At the end of the move the voltage applied to the stage will be the last value written to it.
//                // This value is kept in a global variable which will be used as a reference for future moves.
//                m_dVoltageXCurrent = __scnmScan.Coordinates[0, (__scnmScan.Coordinates.Length / 2) - 1];
//                m_dVoltageYCurrent = __scnmScan.Coordinates[1, (__scnmScan.Coordinates.Length / 2) - 1];

//                // Update the UI.
//                UpdateUI();
//            }

//            catch (MinVoltageExceededException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (MaxVoltageExceededException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (StageNotEngagedException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }

//            catch (DaqException ex)
//            {
//                EnableCtrls();
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private void bckgwrkPerformScan_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
//        {
//            // Bool
//            bool _bStop = false;

//            // Access the document object that holds all the data.
//            ScanDocument _docDocument = this.Document as ScanDocument;

//            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
//            Scanmode _Scan = (Scanmode)__evargsE.Argument;

//            // This int keeps track of the total number of samples already acquired. It is obviously zero at the beginning of measurement.
//            int _readsamples = 0;

//            // The array that will be assigned the current photon counts in the buffer.
//            UInt32[] _ui32SingleReadValues;

//            // The array that will hold the total samples already acquired. It is used as a temporary store for the measurement data
//            // because the measured data needs to be processed before it can be assigned to the actual document object.
//            UInt32[] _ui32AllReadValues = new UInt32[_docDocument.Intensities.Length];

//            // Objects to perform reads and writes on our DAQ tasks.
//            m_wrtrVoltageWriter = new AnalogMultiChannelWriter(m_daqtskMoveStage.Stream);
//            m_rdrCountReader = new CounterReader(m_daqtskAPDCount.Stream);

//            // Perform the actual AO write. But do not start it yet.
//            m_wrtrVoltageWriter.WriteMultiSample(false, _Scan.Coordinates);

//            // For debug purposes.
//            //Random RandomClass = new Random();

//            // Start all four tasks in the correct order. Global sync should be last.
//            m_daqtskAPDCount.Start();
//            m_daqtskTimingPulse.Start();
//            m_daqtskMoveStage.Start();

//            // Until GlobalSync gets started the other tasks will wait. So GlobalSync should be last in line to get started.
//            m_daqtskGlobalSync.Start();

//            // Reset the document object to zero for all intensities.
//            for (int _i = 0; _i < _docDocument.Intensities.Length; _i++)
//            {
//                _docDocument.Intensities[_i] = 0;
//            }

//            //while ((m_daqtskMoveStage.IsDone != true) && (m_daqtskAPDCount.IsDone != true))
//            while ((_readsamples < _Scan.Coordinates.Length / 2) & (_bStop != true))
//            {
//                // Update the UI every 0.1 seconds, more than fast enough.
//                Thread.Sleep(100);

//                // Perform a read of all samples currently in the buffer.
//                _ui32SingleReadValues = m_rdrCountReader.ReadMultiSampleUInt32(-1);

//                // Add the read samples to the previously read samples in memory.
//                for (int _i = 0; _i < _ui32SingleReadValues.Length; _i++)
//                {
//                    _ui32AllReadValues[_readsamples + _i] = _ui32SingleReadValues[_i];
//                    //_ui32AllReadValues[_readsamples + _i] = (UInt32)RandomClass.Next(1, 1600);
//                }

//                // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
//                _readsamples = _readsamples + _ui32SingleReadValues.Length;

//                // Assign processed data to the actual document opject. This should only be done in the case of bidirectional scanning.
//                _docDocument.Intensities = _Scan.PostProcessData(_ui32AllReadValues, _docDocument.ImageWidth, _docDocument.OverScanPixels);

//                // Update the voltages.
//                m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//                m_dVoltageXCurrent = _Scan.Coordinates[0, m_iSamplesToStageCurrent - 1];
//                m_dVoltageYCurrent = _Scan.Coordinates[1, m_iSamplesToStageCurrent - 1];
//                m_iSamplesFromAPD = (int)m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;
//                m_iSampleDelta = m_iSamplesToStageCurrent - m_iSamplesFromAPD;

//                // Update the UI.
//                if (InvokeRequired)
//                {
//                    // Get the in memory bitmap to the screen.
//                    Invoke(new UIUpdateDelegate(PaintToScreen));
//                    // Update the rest of the UI.
//                    Invoke(new UIUpdateDelegate(UpdateUI));
//                }

//                // Check if the worker was not cancelled.
//                if (bckgwrkPerformScan.CancellationPending)
//                {
//                    __evargsE.Cancel = true;
//                    _bStop = true;
//                }
//            }

//            // Stop the globalsync and dispose of it.
//            m_daqtskGlobalSync.Stop();
//            m_daqtskGlobalSync.Dispose();

//            // Update the voltages.
//            m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//            m_dVoltageXCurrent = _Scan.Coordinates[0, m_iSamplesToStageCurrent - 1];
//            m_dVoltageYCurrent = _Scan.Coordinates[1, m_iSamplesToStageCurrent - 1];
//            m_iSamplesFromAPD = (int)m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;
//            m_iSampleDelta = m_iSamplesToStageCurrent - m_iSamplesFromAPD;

//            // Update the UI.
//            if (InvokeRequired)
//            {
//                // Get the in memory bitmap to the screen.
//                Invoke(new UIUpdateDelegate(PaintToScreen));
//                // Update the rest of the UI.
//                Invoke(new UIUpdateDelegate(UpdateUI));
//            }

//            // At the end of the scan, confirm the total amount of acquired samples to the user.
//            MessageBox.Show("Total samples written to stage: " +
//                   m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel.ToString() +
//                   "\r\n\r\n Voltage X: " + m_dVoltageXCurrent +
//                   "\r\n Voltage Y: " + m_dVoltageYCurrent +
//                   "\r\n\r\n Samples read from HW Buffer: " + m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel.ToString() +
//                   "\r\n Samples stored to document: " + _readsamples.ToString());

//            // Stop the move task for the stage.
//            m_daqtskTimingPulse.Stop();
//            m_daqtskMoveStage.Stop();
//            m_daqtskAPDCount.Stop();

//            // Free the resources used
//            m_daqtskAPDCount.Control(TaskAction.Unreserve);
//            m_daqtskTimingPulse.Control(TaskAction.Unreserve);

//            // m_daqtskMoveStage = null;
//            m_daqtskAPDCount = null;
//            m_daqtskTimingPulse = null;

//            // Dispose the writer and reader.
//            m_wrtrVoltageWriter = null;
//            m_rdrCountReader = null;

//            // Enable the UI controls again.
//            if (InvokeRequired)
//            {
//                Invoke(new UIUpdateDelegate(EnableCtrls));
//            }
//        }

//        private void bckgwrkPerformScan_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs __evargsE)
//        {
//            if (__evargsE.Cancelled)
//            {
//                MessageBox.Show("Scan Cancelled, press OK to zero stage.");
//            }

//            else
//            {
//                MessageBox.Show("Scan Completed, press OK to zero stage.");
//            }

//            MoveStageAbs(0, 0);
//        }

//        private void btnStop_Click(object sender, EventArgs __evargsE)
//        {
//            bckgwrkPerformScan.CancelAsync();
//        }

//        #endregion


//        #region Communication with laser

//        internal delegate void StringDelegate(string data);

//        /// <summary>
//        /// Handle data received event from serial port.
//        /// </summary>
//        /// <param name="data">incoming data</param>
//        public void OnDataReceived(string dataIn)
//        {
//            // Handle multi-threading.
//            if (InvokeRequired)
//            {
//                Invoke(new StringDelegate(OnDataReceived), new object[] { dataIn });
//                return;
//            }

//            // Send the reply to the form.
//            this.txtbxLsrReply.Text = dataIn;
//        }

//        /// <summary>
//        /// Update the connection status
//        /// </summary>
//        public void OnStatusChanged(string status)
//        {
//            // Handle multi-threading.
//            if (InvokeRequired)
//            {
//                Invoke(new StringDelegate(OnStatusChanged), new object[] { status });
//                return;
//            }

//            txtbxPortStatus.Text = status;
//        }

//        private void btnLsrOpen_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (!com.IsOpen)
//            {
//                com.Open();
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is already open, try doing something else!!");
//            }
//        }

//        private void btnLsrClose_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (com.IsOpen)
//            {
//                com.Send("L=0");
//                com.Close();
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is already closed, you can reopen it!!");
//            }
//        }

//        private void btnLsrON_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (com.IsOpen)
//            {
//                com.Send("L=1");
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is closed, try opening it first!!");
//            }
//        }

//        private void btnLsrOFF_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (com.IsOpen)
//            {
//                com.Send("L=0");
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is closed, try opening it first!!");
//            }
//        }

//        private void btnLsrCW_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (com.IsOpen)
//            {
//                //com.Send("?STA");
//                com.Send("?CW");
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is closed, try opening it first!!");
//            }
//        }

//        private void btnLsrPow_Click(object sender, EventArgs e)
//        {
//            CommPort com = CommPort.Instance;
//            if (com.IsOpen)
//            {
//                //com.Send("?STA");
//                com.Send("?P");
//            }
//            else
//            {
//                MessageBox.Show("The COM Port is closed, try opening it first!!");
//            }
//        }

//        #endregion

//    }
//}