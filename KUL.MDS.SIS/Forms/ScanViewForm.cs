﻿using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using KUL.MDS.Library;
using KUL.MDS.SIS.Documents;
using KUL.MDS.ScanModes;
using KUL.MDS.WPFControls;
using System.Collections.Generic;
using KUL.MDS.SystemLayer;

namespace KUL.MDS.SIS.Forms
{
    public partial class ScanViewForm : KUL.MDS.MDITemplate.MdiViewForm
    {

        #region Member Variables

        // The various essential objects representing hardware.
        // We need one or more APDs, a Piezo and a timing clock to sync everything.
        private KUL.MDS.Hardware.APD m_apdAPD1;
        private KUL.MDS.Hardware.APD m_apdAPD2;
        private KUL.MDS.Hardware.IPiezoStage m_Stage;
        private KUL.MDS.Hardware.PhotoDiode m_pdPhotoDiode;
        private KUL.MDS.Hardware.TimingClock m_clckGlobalSync;


        // A progress bar that we can use to indicate... progress of various tasks that are handled.
        private KUL.MDS.SIS.Forms.ProgressBarForm m_frmPBar = new ProgressBarForm();
        private KUL.MDS.SIS.Forms.TrajectoryPlotForm m_frmTrajectoryForm = new TrajectoryPlotForm();
        private KUL.MDS.SIS.Forms.ScanSettingsForm m_frmScanSettingsForm;

        // Object to keep track of the current Scan Settings.
        private ScanSettings m_scnstSettings;

        // Arrays that will hold displays of the scans.
        private Bitmap[] m_bmpBitmapsAPD1 = new Bitmap[4];
        private Bitmap[] m_bmpBitmapsAPD2 = new Bitmap[4];

        // NEW STUFF probably needs cleaning...
        private double m_dXSel;
        private double m_dYSel;
        private AForge.Imaging.Filters.Add m_filter = new AForge.Imaging.Filters.Add();

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
            this.m_apdAPD1 = new KUL.MDS.Hardware.APD("Dev1", "Ctr1", "80MHzTimebase", "PFI27", "Ctr0", "PFI39");
            this.m_apdAPD2 = new KUL.MDS.Hardware.APD("Dev1", "Ctr3", "80MHzTimebase", "PFI31", "Ctr2", "PFI35");
            this.m_pdPhotoDiode = new KUL.MDS.Hardware.PhotoDiode("Dev2", "Ctr0", "80MHzTimebase", "RTSI0", "ai0");

            // For Analog stage controllers we need a global timing source.
            //m_clckGlobalSync = new TimingClock();

            #region Piezo

            // The piezo stage is the most critical hardware resource. To prevent conflicts it is created as a singleton instance.
            this.m_Stage = KUL.MDS.Hardware.PIDigitalStage.Instance;

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
            this.btnValidateInput.Enabled = true;
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
            textBox2.Text = this.m_apdAPD2.TotalSamplesAcuired.ToString();
            //if (this.m_apdAPD1.TotalSamplesAcuired > 0)
            //{
            //    textBox3.Text = _docDocument.GetChannelData(0)[this.m_apdAPD1.TotalSamplesAcuired - 1].ToString();
            //}
            //if (this.m_apdAPD1.TotalSamplesAcuired > 0)
            //{
            //    textBox4.Text = _docDocument.GetChannelData(1)[this.m_apdAPD2.TotalSamplesAcuired - 1].ToString();
            //}
            // Get the in memory bitmaps to the screen.
            PaintToScreen();

            // Process any events that might be waiting.
            Application.DoEvents();
        }

        // Called to re-enable controls after a scan.
        private void EnableCtrls()
        {
            // Disable the Scan button because validation is always necessary before scanning can start.
            this.btnScanStart.Enabled = true;
            this.btnValidateInput.Enabled = true;
            this.btnStop.Enabled = true;
        }

        // Called to enable controls during a scan in progress.
        private void DisableCtrls()
        {
            // Disable the Scan button.
            this.btnScanStart.Enabled = false;
            this.btnValidateInput.Enabled = false;
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
                    throw new KUL.MDS.Hardware.StageNotReleasedException("The stage was not released! Please use stage control to turn it off!");
                }
                this.m_Stage.PositionChanged -= new EventHandler(m_Stage_PositionChanged);
                this.m_Stage.ErrorOccurred -= new EventHandler(m_Stage_ErrorOccurred);
                this.m_Stage.EngagedChanged -= new EventHandler(m_Stage_EngagedChanged);
            }

            catch (KUL.MDS.Hardware.StageNotReleasedException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        // Validate user input for scan settings.
        // Valid ranges for the different input controls are set up through the designer by selecting the control,
        // Going to the properties pane and setting "Validationrule on validationprovider" to the desired values.
        private void btnValidateInput_Click(object sender, EventArgs e)
        {
            //bool _boolPrimaryValidationPassed = false;
            //bool _boolSecondaryValidationPassed = false;

            //_boolPrimaryValidationPassed = this.valprovSISValidationProvider.Validate();
            //this.valprovSISValidationProvider.ValidationMessages(!_boolPrimaryValidationPassed);

            //if (_boolPrimaryValidationPassed == false)
            //{
            //    MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else
            //{
            //    if (((Convert.ToUInt32(this.txtbxSetInitX.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000) &&
            //        ((Convert.ToUInt32(this.txtbxSetInitY.Text.Trim()) + Convert.ToUInt32(this.txtbxSetImageWidthnm.Text.Trim())) <= 90000))
            //        _boolSecondaryValidationPassed = true;
            //    //else
            //    //MessageBox.Show(m_strInvalidScanSettingsMsg2 + m_iMaxPosition.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

            //if ((_boolPrimaryValidationPassed == true) && (_boolSecondaryValidationPassed == true))
            //    btnScanStart.Enabled = true;
            //else
            //    btnScanStart.Enabled = false;
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

            //this.m_frmScanSettingsForm = new ScanSettingsForm(_docDocument.Settings);
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

        private void PaintToScreen()
        {
            ScanDocument _docDocument = this.Document as ScanDocument;
            Bitmap _bmpTemp;
            // Create two bitmaps, one for the scan, one for the colorbar.
            this.scanImageControl1.ImageHeight = _docDocument.XScanSizeNm;
            this.scanImageControl1.ImageWidth = _docDocument.YScanSizeNm;
            this.scanImageControl1.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            this.scanImageControl1.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD1[0] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
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

            m_bmpBitmapsAPD1[1] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD1[2] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(0),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD1[3] = _bmpTemp;

            this.scanImageControl2.ImageHeight = _docDocument.XScanSizeNm;
            this.scanImageControl2.ImageWidth = _docDocument.YScanSizeNm;
            this.scanImageControl2.XDpu = _docDocument.ImageWidthPx / _docDocument.XScanSizeNm;
            this.scanImageControl2.YDpu = _docDocument.ImageHeightPx / _docDocument.YScanSizeNm;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD2[0] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD2[1] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD2[2] = _bmpTemp;

            _bmpTemp = Utility.DrawScanToBmp(_docDocument.GetChannelData(1),
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
            AForge.Imaging.Image.FormatImage(ref _bmpTemp);

            m_bmpBitmapsAPD2[3] = _bmpTemp;

            if (this.checkBox8.Checked)
            {
                this.m_filter.OverlayImage = m_bmpBitmapsAPD2[3];
                System.Drawing.Bitmap newImage = this.m_filter.Apply(m_bmpBitmapsAPD1[2]);
                this.scanImageControl1.Image = newImage;
                this.scanImageControl2.Image = m_bmpBitmapsAPD2[1];
            }

            else if (this.checkBox9.Checked)
            {
                this.m_filter.OverlayImage = m_bmpBitmapsAPD1[3];
                System.Drawing.Bitmap newImage = this.m_filter.Apply(m_bmpBitmapsAPD2[2]);
                this.scanImageControl2.Image = newImage;
                this.scanImageControl1.Image = m_bmpBitmapsAPD1[1];
            }

            else
            {
                if (this.checkBox4.Checked)
                {
                    this.scanImageControl2.Image = m_bmpBitmapsAPD2[2];
                }
                if (this.checkBox5.Checked)
                {
                    this.scanImageControl2.Image = m_bmpBitmapsAPD2[3];
                }
                if (!this.checkBox4.Checked & !this.checkBox5.Checked)
                {

                    this.scanImageControl2.Image = m_bmpBitmapsAPD2[1];
                }

                if (this.checkBox6.Checked)
                {
                    this.scanImageControl1.Image = m_bmpBitmapsAPD1[2];
                }
                if (this.checkBox7.Checked)
                {
                    this.scanImageControl1.Image = m_bmpBitmapsAPD1[3];
                }
                if (!this.checkBox6.Checked & !this.checkBox7.Checked)
                {
                    this.scanImageControl1.Image = m_bmpBitmapsAPD1[1];
                }
            }

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

        private void btnScanStart_Click(object __oSender, EventArgs __evargsE)
        {
            #region Filename Increment

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

            #endregion

            // Store the settings to the document.
            _docDocument.AllocateData(this.m_scnstSettings);

            // Generate the requested Scanmode. The settings can now be fetched from the document itself.
            // Currently the MaxSpeed and CycleTime are not used in the Scanmode object.
            KUL.MDS.Library.ComboBoxItem<Type> item = (KUL.MDS.Library.ComboBoxItem<Type>)this.scanModeComboBox1.SelectedItem;

            object[] _oScanParameters = {
                                 _docDocument.ImageWidthPx,
                                 _docDocument.ImageHeightPx,
                                 _docDocument.ImageDepthPx,
                                 _docDocument.XOverScanPx,
                                 _docDocument.YOverScanPx,
                                 _docDocument.ZOverScanPx,
                                 _docDocument.InitialX,
                                 _docDocument.InitialY,
                                 _docDocument.InitialZ,
                                 _docDocument.XScanSizeNm,
                                 _docDocument.YScanSizeNm,
                                 _docDocument.ZScanSizeNm,
                                 10,
                                 2,
                                 1,
                                 0.2 };

            ConstructorInfo[] ci = item.Value.GetConstructors();
            Scanmode m_BiScan = (Scanmode)ci[0].Invoke(_oScanParameters);

            _docDocument.ScanAxes = (UInt16)m_BiScan.ScanAxes;
            _docDocument.XBorderWidth = m_BiScan.BorderWidthX;

            // Indicate that the document data was modified by the scan operation just performed.
            // This will prompt a notification when the user tries to exit without saving.
            _docDocument.Modified = true;

            this.m_frmTrajectoryForm.Visible = true;
            this.m_frmTrajectoryForm.NMCoordinates = m_BiScan.NMScanCoordinates;

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

                // this.m_clckGlobalSync.SetupClock(this.m_clckGlobalSync.Frequency(_docDocument.TimePPixel, 0.1F));
                this.m_apdAPD1.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                this.m_apdAPD2.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);
                //this.m_pdPhotoDiode.SetupAPDCountAndTiming(_docDocument.TimePPixel, _docDocument.PixelCount);

                // Prepare the stage control task for writing as many samples as necessary to complete the scan.
                this.m_Stage.Configure(_docDocument.TimePPixel * 2, _docDocument.PixelCount);

                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                bckgwrkPerformScan.RunWorkerAsync(__scnmScan);
            }

            // Update the UI.
            UpdateUI();
        }

        private void bckgwrkPerformScan_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
        {
            // Boolean value to indicate wheter or not the running scan should be stopped.
            bool _bStop = false;

            // Access the document object that holds all the data.
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            Scanmode _Scan = (Scanmode)__evargsE.Argument;

            // This int keeps track of the total number of samples already acquired. It is obviously zero at the beginning of measurement.
            int _readsamples1 = 0;
            int _readsamples2 = 0;
            //int _readsamplesa = 0;

            // The array that will be assigned the current photon counts in the buffer.
            UInt32[] _ui32SingleReadValues1;
            UInt32[] _ui32SingleReadValues2;
            //Double[] _dSingleReadValuesa;

            // The array that will hold the total samples already acquired. It is used as a temporary store for the measurement data
            // because the measured data needs to be processed before it can be assigned to the actual document object.
            UInt32[] _ui32AllReadValues1 = new UInt32[_docDocument.PixelCount];
            UInt32[] _ui32AllReadValues2 = new UInt32[_docDocument.PixelCount];
            //Double[] _dAllreadValuesa = new Double[_docDocument.PixelCount * 100];

            //List<UInt32> _lui32AllReadValues1 = new List<UInt32>(_docDocument.PixelCount);
            //List<UInt32> _lui32AllReadValues2 = new List<UInt32>(_docDocument.PixelCount);

            // Start the APD. It will now count photons every time it is triggered by either a clock or a digital controller.
            this.m_apdAPD1.StartAPDAcquisition();
            this.m_apdAPD2.StartAPDAcquisition();
            //this.m_pdPhotoDiode.StartAPDAcquisition();

            // Initiate stage scan movement.
            //this.m_Stage.Scan(_Scan, this.checkBox1.Checked);
            this.m_Stage.Scan(_Scan, this.checkBox1.Checked);

            //while ((_readsamples1 < _docDocument.PixelCount) & (_bStop != true))
            //while ((_readsamples1 < _docDocument.PixelCount) & (_readsamples2 < _docDocument.PixelCount) & (_bStop != true))
            //while ((_readsamples2 < _docDocument.PixelCount) & (_bStop != true))
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
                        //_ui32AllReadValues1[_readsamples1 + _i] = (UInt32)RandomClass.Next(1, 1600);
                    }
                    //_lui32AllReadValues1.AddRange(_ui32SingleReadValues1);

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
                        //_ui32AllReadValues2[_readsamples2 + _i] = (UInt32)RandomClass.Next(1, 1600);
                    }
                    //_lui32AllReadValues2.AddRange(_ui32SingleReadValues2);

                    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                    _readsamples2 = _readsamples2 + _ui32SingleReadValues2.Length;
                }
                //if (_readsamplesa < _docDocument.PixelCount * 100)
                //{
                //    _dSingleReadValuesa = this.m_pdPhotoDiode.Read();

                //    for (int _i = 0; _i < _dSingleReadValuesa.Length; _i++)
                //    {
                //        _dAllreadValuesa[_readsamplesa + _i] = _dSingleReadValuesa[_i];

                //        // For debug purposes.
                //        //_ui32AllReadValues2[_readsamples2 + _i] = (UInt32)RandomClass.Next(1, 1600);
                //    }
                //    //_lui32AllReadValues2.AddRange(_ui32SingleReadValues2);

                //    // Increment the total number of acquired samples AFTER this number has been used to store values in the array!!
                //    _readsamplesa = _readsamplesa + _dSingleReadValuesa.Length;
                //    Tracing.Ping("Analog Samples Read: " + _readsamplesa.ToString());
                //}

                // Assign processed data to the actual document opject. This should only be done in the case of bidirectional scanning.
                _docDocument.StoreChannelData(0, _Scan.PostProcessData(_ui32AllReadValues1));
                _docDocument.StoreChannelData(1, _Scan.PostProcessData(_ui32AllReadValues2));
                //_docDocument.StoreChannelData(0, _Scan.PostProcessData(_lui32AllReadValues1.ToArray()));
                //_docDocument.StoreChannelData(1, _Scan.PostProcessData(_lui32AllReadValues2.ToArray()));

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
                if (InvokeRequired)
                {
                    // Get the in memory bitmap to the screen.
                    Invoke(new UIUpdateDelegate(PaintToScreen));
                    // Update the rest of the UI.
                    Invoke(new UIUpdateDelegate(UpdateUI));
                }

                // Check if the worker was not cancelled.
                if (bckgwrkPerformScan.CancellationPending)
                {
                    __evargsE.Cancel = true;
                    _bStop = true;
                }
            }

            // Stop the globalsync and dispose of it.
            //m_daqtskGlobalSync.Stop();
            //m_daqtskGlobalSync.Dispose();

            // Update the UI.
            if (InvokeRequired)
            {
                // Get the in memory bitmap to the screen.
                Invoke(new UIUpdateDelegate(PaintToScreen));
                // Update the rest of the UI.
                Invoke(new UIUpdateDelegate(UpdateUI));
            }
            Thread.Sleep(1000);

            // At the end of the scan, confirm the total amount of acquired samples to the user.
            MessageBox.Show(
                "\r\n\r\n X Position: " + this.m_Stage.XPosition.ToString() +
                "\r\n Y Position: " + this.m_Stage.YPosition.ToString() +
                "\r\n\r\n Samples read from APD1 Buffer: " + this.m_apdAPD1.TotalSamplesAcuired.ToString() +
                "\r\n\r\n Samples read from APD2 Buffer: " + this.m_apdAPD2.TotalSamplesAcuired.ToString() +
                "\r\n Samples stored to document for APD1: " + _readsamples1.ToString() +
                "\r\n Samples stored to document for APD2: " + _readsamples2.ToString());

            // Stop the move task for the stage.
            //m_daqtskTimingPulse.Stop();
            this.m_apdAPD1.StopAPDAcquisition();
            this.m_apdAPD2.StopAPDAcquisition();
            //this.m_pdPhotoDiode.StopAPDAcquisition();
        }

        private void btnStop_Click(object __oSender, EventArgs __evargsE)
        {
            // Cancel de backgroundworker.
            bckgwrkPerformScan.CancelAsync();

            // Enable all controls again.
            EnableCtrls();

            // Disable the Stop button again.
            this.btnStop.Enabled = false;
            this.btnScanStart.Enabled = true;
        }

        private void bckgwrkPerformScan_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs __evargsE)
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

        #endregion

    }
}