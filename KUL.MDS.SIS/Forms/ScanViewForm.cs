using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using KUL.MDS.Library;
using KUL.MDS.MDITemplate;
using KUL.MDS.SIS.Documents;
using KUL.MDS.ScanModes;
using KUL.MDS.WPFControls;
using System.Collections.Generic;
using KUL.MDS.SystemLayer;
using System.IO;
using log4net;
using log4net.Layout;
using DevDefined.Common.Appenders;

namespace KUL.MDS.SIS.Forms
{
    public partial class ScanViewForm : KUL.MDS.MDITemplate.MdiViewForm
    {

        #region Member Variables

        // The various essential objects representing hardware.
        // We need one or more APDs, a Piezo and a timing clock to sync everything.
        //private KUL.MDS.Hardware.APD m_apdAPD1;
        //private KUL.MDS.Hardware.APD m_apdAPD2;
        private KUL.MDS.Hardware.PQTimeHarp m_apdAPD;
        private KUL.MDS.Hardware.IPiezoStage m_Stage;
        //private KUL.MDS.Hardware.PhotoDiode m_pdPhotoDiode;
        //private KUL.MDS.Hardware.TimingClock m_clckGlobalSync;


        // A progress bar that we can use to indicate... progress of various tasks that are handled.
        private KUL.MDS.SIS.Forms.ProgressBarForm m_frmPBar = new ProgressBarForm();
        private KUL.MDS.SIS.Forms.TrajectoryPlotForm m_frmTrajectoryForm = new TrajectoryPlotForm();
        private KUL.MDS.SIS.Forms.ScanSettingsForm m_frmScanSettingsForm;
		private KUL.MDS.SIS.Forms.CountRateForm m_frmCountRateForm;

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

		// Strings that define the ON/OFF state of stage/galvo and APD (and are used to control these states)
		private const string str_STAGE_BUTTON_TEXT_OFF = "STAGE: OFF";
		private const string str_STAGE_BUTTON_TEXT_ON = "STAGE: ON";
		private const string str_APD_BUTTON_TEXT_OFF = "APD: OFF";
		private const string str_APD_BUTTON_TEXT_ON = "APD: ON";

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

			// Create a new ColoredRichTextBoxAppender and give it a standard layout.
			if (this.m_ColRTApp == null)
			{
				this.m_ColRTApp = new DevDefined.Common.Appenders.ColoredRichTextBoxAppender(this.richTextBox1, 1000, 500);
				this.m_ColRTApp.Layout = new PatternLayout();
			}

			// Add the appender to the log4net root.			
			((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.AddAppender(this.m_ColRTApp);          
        }

        void m_Stage_EngagedChanged(object sender, EventArgs e)
        {
            // The text indicating stage status should always be updated.			
			if (this.m_Stage != null && this.m_Stage.IsInitialized)
			{
				// Set correct state of stage button - note that we use the same button as ON/OFF button
				this.btnStageOnOff.Text = str_STAGE_BUTTON_TEXT_ON;
				this.btnStageOnOff.ForeColor = Color.FromKnownColor(KnownColor.Green);
			}
			else
			{
				// Set correct state of stage button - note that we use the same button as ON/OFF button
				this.btnStageOnOff.Text = str_STAGE_BUTTON_TEXT_OFF;
				this.btnStageOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			}
        }

        void m_APD_EngagedChanged(object sender, EventArgs e)
        {
            // The text indicating stage status should always be updated.			
			if (this.m_apdAPD != null && this.m_apdAPD.IsInitialized)
			{
				// Set correct state of APD button - note that we use the same button as ON/OFF button
				this.btnAPDOnOff.Text = str_APD_BUTTON_TEXT_ON;
				this.btnAPDOnOff.ForeColor = Color.FromKnownColor(KnownColor.Green);
			}
			else
			{
				// Set correct state of APD button - note that we use the same button as ON/OFF button
				this.btnAPDOnOff.Text = str_APD_BUTTON_TEXT_OFF;
				this.btnAPDOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			}
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

			// Set correct Validate button states
			this.btnValidateInput.Enabled = true;
                       
			// Set correct ON/OFF button states
			this.btnAPDOnOff.Enabled = true;
			this.btnStageOnOff.Enabled = true;

			// Set correct scan button state
			this.btnScanStart.Enabled = false; //disable scan start button
			this.btnScanStop.Enabled = false; //disable scan stop button  

            // Indicate that the stage and APD are not yet brought online.			
			this.btnStageOnOff.Text = str_STAGE_BUTTON_TEXT_OFF;
			this.btnStageOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			this.btnAPDOnOff.Text = str_APD_BUTTON_TEXT_OFF;
			this.btnAPDOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			
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
                "Image Depth nm:  " + _docDocument.ZScanSizeNm.ToString() + "\r\n" +
                "Time Per Pixel ms: " + _docDocument.TimePPixel.ToString() + "\r\n";			
        }

        // Called to update the data display area of the UI.
        private void UpdateUI()
        {
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Update the UI with the pixels read.
            if (this.m_apdAPD != null)
            {
                if (this.m_apdAPD.IsInitialized)
                {
                    int _TotalPixelsAcquired = this.m_apdAPD.TotalSamplesAcquired;  // the number of pixels acquired so far
                    int _TotalPixelsAcquiredPercentage = (int)(100.0 * ((double)_TotalPixelsAcquired / (double)(_docDocument.ImageWidthPx * _docDocument.ImageHeightPx)));  // the number of pixels in [%] acquired so far
                    textBox1.Text = _TotalPixelsAcquired.ToString() + " (" + _TotalPixelsAcquiredPercentage.ToString() + "%)";
                    textBox2.Text = _TotalPixelsAcquired.ToString() + " (" + _TotalPixelsAcquiredPercentage.ToString() + "%)";
                }
            }            

            // Update the UI with the current voltage to stage.
            if (this.m_Stage != null)
            {
                if (this.m_Stage.IsInitialized)
                {
                    txtbxCurrXPos.Text = this.m_Stage.XPosition.ToString();
                    txtbxCurrYPos.Text = this.m_Stage.YPosition.ToString();
                    txtbxCurrZPos.Text = this.m_Stage.ZPosition.ToString();
                }
            }            
            
            //if (this.m_apdAPD1.TotalSamplesAcquired > 0)
            //{
            //    textBox3.Text = _docDocument.GetChannelData(0)[this.m_apdAPD1.TotalSamplesAcquired - 1].ToString();
            //}
            //if (this.m_apdAPD1.TotalSamplesAcquired > 0)
            //{
            //    textBox4.Text = _docDocument.GetChannelData(1)[this.m_apdAPD2.TotalSamplesAcquired - 1].ToString();
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
            this.btnScanStop.Enabled = true;
        }

        // Called to enable controls during a scan in progress.
        private void DisableCtrls()
        {
            // Disable the Scan button.
            this.btnScanStart.Enabled = false;
            this.btnValidateInput.Enabled = false;
            this.btnScanStop.Enabled = true;
        }

        #region Form Eventhandlers

        // Handle closing of the form.
        private void ScanViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.btnStageOnOff.Text == str_STAGE_BUTTON_TEXT_ON)  // do not close the form if the stage is still ON
                {                    
                    throw new KUL.MDS.Hardware.StageNotReleasedException("The stage was not released! Please use stage control to turn it off!");                    
                }                
            }
            catch (KUL.MDS.Hardware.StageNotReleasedException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }

            try
            {
				if (this.btnAPDOnOff.Text == str_APD_BUTTON_TEXT_ON)  // do not close the form if the APD is still ON
                {                    
                    throw new KUL.MDS.Hardware.StageNotReleasedException("The APD was not released! Please use APD control to turn it off!");                    
                }                
            }
            catch (KUL.MDS.Hardware.StageNotReleasedException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
		
		private void ScanViewForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// Remove the appender from the log4net root.			
			((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root.RemoveAppender(this.m_ColRTApp);
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

        private void btnScanSettings_Click(object sender, EventArgs e)
        {
            ScanDocument _docDocument = this.Document as ScanDocument;

            //this.m_frmScanSettingsForm = new ScanSettingsForm(_docDocument.Settings);
            this.m_frmScanSettingsForm = new ScanSettingsForm(this.m_scnstSettings);

            this.m_frmScanSettingsForm.UpdateParameters += m_frmScanSettingsForm_UpdateParameters;

            this.m_frmScanSettingsForm.Visible = true;
        }

		private void btnAPDCountRate_Click(object sender, EventArgs e)
		{
			if (this.m_apdAPD != null)
			{
				if (m_frmCountRateForm == null)  // case the form does not exists
				{
					this.m_frmCountRateForm = new CountRateForm(this.m_apdAPD);
					this.m_frmCountRateForm.Visible = true;
					this.m_frmCountRateForm.Focus();
					this.m_frmCountRateForm.RunCountRateMeter();  // get count rate and show it to the user (it runs the method on separate thread and continue)
				}
				else  // case the form exists
				{
					if (this.m_frmCountRateForm.IsDisposed)  // case we are already running
					{
						this.m_frmCountRateForm = new CountRateForm(this.m_apdAPD);
						this.m_frmCountRateForm.Visible = true;
						this.m_frmCountRateForm.Focus();
						this.m_frmCountRateForm.RunCountRateMeter();  // get count rate and show it to the user (it runs the method on separate thread and continue)
					}
					else
					{
						this.m_frmCountRateForm.Visible = true;
						this.m_frmCountRateForm.Focus();
					}
				}
			}
			else
			{
				string _sMessage = "APD is OFF! APD must be ON when you want to get the Count Rate!";
				MessageBox.Show(_sMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

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

        
		private void btnStageOnOff_Click(object __oSender, EventArgs __evargsE)
		{
			// Access the ScanDocument object related to this form
			ScanDocument _docDocument = this.Document as ScanDocument;
						
			// Turn ON/OFF stage/galvo
			if (this.btnStageOnOff.Text == str_STAGE_BUTTON_TEXT_OFF) // turn ON stage/galvo only if it is already OFF
			{
				// First allocate stage/galvo in case it is not		
				if (this.m_Stage == null)
				{
					// Create and setup a Galvo scanner with the given settings
					this.m_Stage = new KUL.MDS.Hardware.YanusIV(
						_docDocument.Settings.GalvoSerialPortName,
						_docDocument.Settings.GalvoMagnificationObjective, _docDocument.Settings.GalvoScanLensFocalLength,
						_docDocument.Settings.GalvoRangeAngleDegrees, _docDocument.Settings.GalvoRangeAngleInt);

					// Hook up EventHandler methods to the events of the stage
					this.m_Stage.PositionChanged += m_Stage_PositionChanged;
					this.m_Stage.ErrorOccurred += m_Stage_ErrorOccurred;
					this.m_Stage.EngagedChanged += m_Stage_EngagedChanged;
				}

				// Second connect to the controller hardware and initialize it
				if (this.m_Stage != null)
				{
					this.m_Stage.Initialize();  // initialize stage	

					if (!this.m_Stage.IsInitialized)  // if initialization is not successful release resources	
					{
						// Release methods from the events of the stage
						this.m_Stage.PositionChanged -= m_Stage_PositionChanged;
						this.m_Stage.ErrorOccurred -= m_Stage_ErrorOccurred;
						this.m_Stage.EngagedChanged -= m_Stage_EngagedChanged;

						// Release stage/galvo object
						this.m_Stage = null;  			
					}
				}
			}
			else  // Turn OFF stage/galvo if it is ON
			{
				// Stage/galvo is ON so release it, i.e. turn it OFF
				if (this.m_Stage != null)
				{
					// Disconnect from hardware
					this.m_Stage.Release();
															
					// Release stage events
					this.m_Stage.PositionChanged -=	m_Stage_PositionChanged;
					this.m_Stage.ErrorOccurred -= m_Stage_ErrorOccurred;
					this.m_Stage.EngagedChanged -= m_Stage_EngagedChanged;

					// Release stage object
					this.m_Stage = null;  					
				}
			}
			

			// Feedback to UI - update stage/galvo button status
			if (this.m_Stage != null && this.m_Stage.IsInitialized)
			{
				// Set correct state of stage button - note that we use the same button as ON/OFF button
				this.btnStageOnOff.Text = str_STAGE_BUTTON_TEXT_ON;  
				this.btnStageOnOff.ForeColor = Color.FromKnownColor(KnownColor.Green); 
			}
			else
			{
				// Set correct state of stage button - note that we use the same button as ON/OFF button
				this.btnStageOnOff.Text = str_STAGE_BUTTON_TEXT_OFF;
				this.btnStageOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			}


			// Set scan start/stop button state
			if (this.btnAPDOnOff.Text == str_APD_BUTTON_TEXT_ON && this.btnStageOnOff.Text == str_STAGE_BUTTON_TEXT_ON)
			{
				// Enable scan button because stage and APD are ON
				this.btnScanStart.Enabled = true;
				this.btnScanStop.Enabled = true;
			}
			else
			{
				// Disable scan button because stage or/and APD are OFF
				this.btnScanStart.Enabled = false;
				this.btnScanStop.Enabled = false;
			}


			// Update the UI
			this.UpdateUI();
		}


		private void btnAPDOnOff_Click(object __oSender, EventArgs __evargsE)
		{
			// Access the ScanDocument object related to this form
			ScanDocument _docDocument = this.Document as ScanDocument;
			
			// Turn ON/OFF APD
			if (this.btnAPDOnOff.Text == str_APD_BUTTON_TEXT_OFF) // turn ON APD only if it is already OFF
			{
				// First allocate APD in case it is not	
				if (this.m_apdAPD == null)
				{				
					// Create and setup a Time Harp counting board with the given settings
					this.m_apdAPD = new KUL.MDS.Hardware.PQTimeHarp(
						_docDocument.Settings.TimeHarpMeasurementMode, _docDocument.Settings.TimeHarpCFDMin, _docDocument.Settings.TimeHarpCFDZeroCross,
						_docDocument.Settings.TimeHarpSyncLevel, _docDocument.Settings.TimeHarpRangeCode, _docDocument.Settings.TimeHarpOffset,
						_docDocument.Settings.TimeHarpMarkerEdge, _docDocument.Settings.TimeHarpGlobalTTTRBufferSize, _docDocument.Settings.TimeHarpLinePTTTRBufferSize);

					// Hook up EventHandler methods to the events of the APD
					this.m_apdAPD.PositionChanged += m_APD_PositionChanged;
					this.m_apdAPD.ErrorOccurred += m_APD_ErrorOccurred;
					this.m_apdAPD.EngagedChanged += m_APD_EngagedChanged;
				}
						
				// Connect to the controller hardware and initialize it
				if (this.m_apdAPD != null)
				{				
					this.m_apdAPD.Initialize();  // initialize Time Harp counting board with the given settings

					if (!this.m_apdAPD.IsInitialized)  // if initialization is not successful release the APD
					{
						// Release APD events
						this.m_apdAPD.PositionChanged -= m_APD_PositionChanged;
						this.m_apdAPD.ErrorOccurred -= m_APD_ErrorOccurred;
						this.m_apdAPD.EngagedChanged -= m_APD_EngagedChanged;

						// Release APD object
						this.m_apdAPD = null;
					}
				}
			}
			else  // Turn OFF APD if it is ON
			{
				// APD is ON so release it, i.e. turn it OFF
				if (this.m_apdAPD != null)
				{
					// Disconnect from hardware				
					this.m_apdAPD.Release();

					// Release APD events
					this.m_apdAPD.PositionChanged -= m_APD_PositionChanged;
					this.m_apdAPD.ErrorOccurred -= m_APD_ErrorOccurred;
					this.m_apdAPD.EngagedChanged -= m_APD_EngagedChanged;

					// Release APD object
					this.m_apdAPD = null;				
				}
			}
			

			// Feedback to UI - update APD button status
			if (this.m_apdAPD != null && this.m_apdAPD.IsInitialized)
			{
				// Set correct state of APD button - note that we use the same button as ON/OFF button
				this.btnAPDOnOff.Text = str_APD_BUTTON_TEXT_ON;
				this.btnAPDOnOff.ForeColor = Color.FromKnownColor(KnownColor.Green); 
			}
			else
			{
				// Set correct state of APD button - note that we use the same button as ON/OFF button
				this.btnAPDOnOff.Text = str_APD_BUTTON_TEXT_OFF;
				this.btnAPDOnOff.ForeColor = Color.FromKnownColor(KnownColor.Red);
			}


			// Set scan start/stop button state
			if (this.btnAPDOnOff.Text == str_APD_BUTTON_TEXT_ON && this.btnStageOnOff.Text == str_STAGE_BUTTON_TEXT_ON)
			{
				// Enable scan button because stage and APD are ON
				this.btnScanStart.Enabled = true;
				this.btnScanStop.Enabled = true;
			}
			else
			{
				// Disable scan button because stage or/and APD are OFF
				this.btnScanStart.Enabled = false;
				this.btnScanStop.Enabled = false;
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

        void m_APD_PositionChanged(object __oSender, EventArgs __evargsE)
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


        void m_APD_ErrorOccurred(object __oSender, EventArgs __evargsE)
        {
            MessageBox.Show(m_apdAPD.CurrentError);
        }

        #endregion


        #region Scan/Experimental Control

        private void btnScanStart_Click(object __oSender, EventArgs __evargsE)
        {
            #region Filename Increment

            // Acces the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            if (this.m_checkbxAutoIncrement.Checked)
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
                                 _docDocument.TimePPixel,
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

            //this.m_frmTrajectoryForm.Visible = true;
            //this.m_frmTrajectoryForm.NMCoordinates = m_BiScan.NMScanCoordinates;

			// Indicate scanning by changing button text
			this.btnScanStart.Text = "Scanning...";
			this.btnScanStart.BackColor = Color.FromKnownColor(KnownColor.Lime);

            // Continue with prepping and eventually running the scan.
            PrepnRunScan(m_BiScan);
        }

        private void PrepnRunScan(Scanmode __scnmScan)
        {
            // Access the ScanDocument object related to this form.
            ScanDocument _docDocument = this.Document as ScanDocument;

            // Disable the controls so the user cannot interfere with the scan. Only stopping the scan will be allowed.
            DisableCtrls();

            // Get the new experimental settings to screen.
            this.ScanPropertiesToScreen();
               
            // Check if the stage and APD are definitely engaged and ready.... if not all other operations would be useless!
            if (m_Stage.IsInitialized && m_apdAPD.IsInitialized)
            {
                // Make sure the Stop button works.
                this.btnScanStop.Enabled = true;


                // Counting and timing settings for the APD                
                bool _bIsToApplyTimeGating = this.m_checkbxApplyTimeGatingWhileScanning.Checked;  // indicates whether we want to do time gating or not (true = do gating; false = do not perform gating)
                double _dGatingTimeMinMillisec = Convert.ToDouble(txtboxTimeGatingMinAPD1.Text.Trim()) * 1e-6;  // min gating time in [ms]
                double _dGatingTimeMaxMillisec = Convert.ToDouble(txtboxTimeGatingMaxAPD1.Text.Trim()) * 1e-6;  // max gating time in [ms]

                int _iTypeOfScan = (m_checkbxBidirScan.Checked) ? 1 : 0;  // set the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
                int _iAcquisitionTime = 0;  // acquisition time in [ms] - the amount of time that the APD will count photons. Note that zero here means the acquisition time will be set to its max value (~10 hours)                
                bool _bSaveTTTRData = m_checkbxSaveTTTRData.Checked;  //to save or not the raw photon data
                string _sTTTRFile = Path.GetDirectoryName(_docDocument.FilePath) + "\\" + _docDocument.Settings.TimeHarpNameTTTRFile + ".t3r";  // the path and name to the data file with raw photon data (the TTTR file)

                if (!this.m_checkbxContinuousScan.Checked)  // if we scan in a non-continuous mode we set some limit of the acquisition time based on the size of the image and the pixel time (note that later we have to set this acquisition time bigger because of some delays until the measurement starts and/or ends)
                {
                    // Set limit of the acquisition time plus +5000 ms delay to the expected acquisition time - we need this because if for some performance or hardware problem reasons we miss a frame/line marker the measurement in single scan mode will not stop automatically                                
                    _iAcquisitionTime = (int)(_docDocument.TimePPixel * _docDocument.ImageWidthPx * _docDocument.ImageHeightPx) + 5000;  // calc the acquisition time in [ms]                    
                }
                else
                {
                    _iAcquisitionTime = 0;  // acquisition time in [ms] - the amount of time that the APD will count photons. Note that zero here means the acquisition time will be set to its max value (~10 hours)                                
                }
                
                // Prepare the APD for measurement
                this.m_apdAPD.SetupAPDCountAndTiming(
                _docDocument.Settings.TimeHarpFrameMarker, _docDocument.Settings.TimeHarpLineMarker, 
                _docDocument.TimePPixel, _docDocument.ImageWidthPx, _docDocument.ImageHeightPx, _docDocument.ImageDepthPx,
                _docDocument.XScanSizeNm, _docDocument.YScanSizeNm, _docDocument.ZScanSizeNm, _docDocument.InitialX, _docDocument.InitialY, _docDocument.InitialZ,
                _docDocument.XOverScanPx, _docDocument.YOverScanPx, _docDocument.ZOverScanPx, _docDocument.Settings.Channels,
                _docDocument.Settings.GalvoMagnificationObjective, _docDocument.Settings.GalvoScanLensFocalLength, _docDocument.Settings.GalvoRangeAngleDegrees, _docDocument.Settings.GalvoRangeAngleInt,
                _bIsToApplyTimeGating, _dGatingTimeMinMillisec, _dGatingTimeMaxMillisec,
                _iTypeOfScan, _docDocument.Settings.TimeHarpFrameTimeOut, _docDocument.Settings.TimeHarpFiFoTimeOut,
                _docDocument.Settings.TimeHarpMeasurementMode, _iAcquisitionTime, 
                _bSaveTTTRData, _sTTTRFile);
                
                // Counting and timing settings for the stage/galvo                
                this.m_Stage.Setup(_iTypeOfScan, _docDocument.Settings.GalvoFrameMarker, _docDocument.Settings.GalvoLineMarker);

                
                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                bckgwrkPerformScan.RunWorkerAsync(__scnmScan);
            }

            // Update the UI.
            UpdateUI();
        }

        private void bckgwrkPerformScan_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
        {
            // Boolean value to indicate whether or not the running scan should be stopped.
            bool _bStop = false;

            // Indicates if measurement is running, _bMeasurementRunning = true (means measurement is still running)
            bool _bMeasurementRunning = true;   
            
            // Access the document object that holds all the data.
            ScanDocument _docDocument = this.Document as ScanDocument;
			int _iFrameRefreshRate = _docDocument.Settings.TimeHarpFrameTimeOut;  // the max time period in [ms] after which the processed pixels so far will be read
			int _iPixelCount = _docDocument.PixelCount;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            Scanmode _Scan = (Scanmode)__evargsE.Argument;

            // This int keeps track of the total number of samples already acquired. It is obviously zero at the beginning of measurement.
            int _readsamples1 = 0;
            int _readsamples2 = 0;
            //int _readsamplesa = 0;

            // The array that will be assigned the current photon counts in the buffer.
            //UInt32[] _ui32SingleReadValues1;
            //UInt32[] _ui32SingleReadValues2;
            //Double[] _dSingleReadValuesa;

            // The array that will hold the total samples already acquired. It is used as a temporary store for the measurement data
            // because the measured data needs to be processed before it can be assigned to the actual document object.
            UInt32[] _ui32AllReadValues1 = null;  // new UInt32[_docDocument.PixelCount];
            UInt32[] _ui32AllReadValues2 = null;  //new UInt32[_docDocument.PixelCount];
            //Double[] _dAllreadValuesa = new Double[_docDocument.PixelCount * 100];

            //List<UInt32> _lui32AllReadValues1 = new List<UInt32>(_docDocument.PixelCount);
            //List<UInt32> _lui32AllReadValues2 = new List<UInt32>(_docDocument.PixelCount);
            
            uint[] _ui32FramePixelBuffer = null;  // an array of arrays of pixels from the data buffer, each array of pixels represent a frame
			int _iPixelsReadNew = 0;  // the number of currently read pixels from the frame pixels buffer

            // Start the APD acquisition
            this.m_apdAPD.StartAPDAcquisition();  //it spawn a separate thread responsible for the acquisition (note that after spawn thread gets alive it returns and the execution continues in the current calling thread)
                 
            // Initiate stage/galvo scan movement.            
            this.m_Stage.Scan(_Scan, this.m_checkbxResend.Checked);


            // Loop that continuously acquires an image(s) and paint it on the screen until scan is running
            while (!_bStop && _bMeasurementRunning)
            {
				// The sleep time defines the rate of the screen frame refresh rate
				Thread.Sleep(_iFrameRefreshRate);

                // Perform a read of all samples currently in the buffer  
				_iPixelsReadNew = this.m_apdAPD.TotalSamplesAcquired;  // the total number of currently processed pixels
                _ui32FramePixelBuffer = this.m_apdAPD.GetImage();  // returns a pointer to the current image/frame buffer with the processed pixels so far
                
                _bMeasurementRunning = this.m_apdAPD.IsMeasurementRunning;  //indicates if measurement is running, _bMeasurementRunning = true (means measurement is still running) 
                
                // Copy frame pixel buffer from APD space to Scan document space
                _ui32AllReadValues1 = _ui32FramePixelBuffer;  //copy frame pixel buffer to another buffer (actually we get a reference to it)
                _ui32AllReadValues2 = _ui32FramePixelBuffer;  //copy frame pixel buffer to another buffer  (actually we get a reference to it)
				                                               
                // Assign processed data to the actual document object - transfers pixels data from APD space to the Scan document space.
				if (_iPixelsReadNew < _readsamples1)  //if true, means we started a new frame but we still need to get the last portion of pixels from the previous frame (note that this portion is kept at the end of the same frame buffer)
				{
					// Transfer pixels data from APD space to the Scan document space
					_docDocument.StoreChannelData(0, _ui32AllReadValues1, _readsamples1, _iPixelCount - _readsamples1);
					_docDocument.StoreChannelData(1, _ui32AllReadValues2, _readsamples2, _iPixelCount - _readsamples1);

					// Update the value for the number of the previously read pixels
					_readsamples1 = _iPixelCount;
					_readsamples2 = _iPixelCount;
				}
				else  //in this case we get the portion of the newly processed pixels not yet read
				{
					// Transfer pixels data from APD space to the Scan document space
					_docDocument.StoreChannelData(0, _ui32AllReadValues1, _readsamples1, _iPixelsReadNew - _readsamples1);
					_docDocument.StoreChannelData(1, _ui32AllReadValues2, _readsamples2, _iPixelsReadNew - _readsamples1);

					// Update the value for the number of the previously read pixels
					_readsamples1 = _iPixelsReadNew;
					_readsamples2 = _iPixelsReadNew;
				}				
                
                // Check if we continue the scanning or not                
                if ((_readsamples1 == _iPixelCount) && (_readsamples2 == _iPixelCount))
                {
                    if (this.m_checkbxContinuousScan.Checked)
                    {
                        _bStop = false;

                        //this.m_apdAPD.StopAPDAcquisition();
                        //this.m_apdAPD.SetupAPDCountAndTiming(...);  // prepare APD for measurement                                        
                        //this.m_apdAPD.StartAPDAcquisition();  

                        this.m_Stage.Scan(_Scan, false);  // this will repeat the scan with the last loaded settings

						_iPixelsReadNew = 0;
                        _readsamples1 = 0;
                        _readsamples2 = 0;
						_ui32FramePixelBuffer = null;
                    }
                    else
                    {
                        _bStop = true;  // this will stop the while-loop
                    }
                }
                
                // Update the UI.
                if (InvokeRequired)
                {
                    // Get the in memory bitmap to the screen.
                    //Invoke(new UIUpdateDelegate(PaintToScreen));
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

            // Stop the move task for the stage            
            this.m_apdAPD.StopAPDAcquisition();            
            this.m_Stage.Stop();

            // Update the UI.
            if (InvokeRequired)
            {
                // Get the in memory bitmap to the screen.
                //Invoke(new UIUpdateDelegate(PaintToScreen));
                // Update the rest of the UI.
                Invoke(new UIUpdateDelegate(UpdateUI));
            }                      
        }

        private void btnScanStop_Click(object __oSender, EventArgs __evargsE)
        {
            // Cancel de backgroundworker.
            bckgwrkPerformScan.CancelAsync();

            // Enable all controls again.
            EnableCtrls();

            // Disable the Stop button again.
            this.btnScanStop.Enabled = false;

			// Indicate scanning finished by changing button text
			this.btnScanStart.Text = "Scan";
			this.btnScanStart.BackColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        private void bckgwrkPerformScan_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs __evargsE)
        {
            if (__evargsE.Cancelled)
            {
                // Actually stop the hardware from scanning.
                if (this.m_apdAPD != null)
                {
                    this.m_apdAPD.StopAPDAcquisition();
                }

                if (this.m_Stage != null)
                {
                    this.m_Stage.Stop();
                }

                // Wait a bit.
                Thread.Sleep(2000);

                // Inform the user.
                //MessageBox.Show("Scan Cancelled, press OK to zero stage.");				
            }
            else
            {
				// Inform the user.
                //MessageBox.Show("Scan Completed, press OK to zero stage.");
            }

			// Inform the user - indicate scanning finished by changing button text
			this.btnScanStart.Text = "Scan";
			this.btnScanStart.BackColor = Color.FromKnownColor(KnownColor.Transparent);

            // Handle auto-save.
            if (this.m_chkbxAutosave.Checked)
            {
                // Save the document.
                this.Document.SaveDocument(this.Document.FilePath);

                // Increment the filename counter.
                this.m_nupdFilenameCount.Value = this.m_nupdFilenameCount.Value + 1;
            }

            this.EnableCtrls();
            //this.m_Stage.MoveAbs(0.0, 0.0, 0.0);
        }

        #endregion


		#region Processing data
		
		private void buttonExp_Click(object sender, EventArgs e)
        {
            // Access the ScanDocument object related to this form.            
            ScanDocument _docDocument = this.Document as ScanDocument;

            UInt32[] _uint32ChannelData1 = _docDocument.GetChannelData(0);
            UInt32[] _uint32ChannelData2 = _docDocument.GetChannelData(1);
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
                        

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH1.txt")))  //write Channel 1 picture to ASCII file
                {
                    sr.Write(_strData1);  //write header info to file

                    for (int _intI = 0; _intI < _iImageHeight; _intI++)  //write pixel values to file
                    {
                        for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                        {
                            sr.Write(_uint32ChannelData1[_intI * (_iImageWidth + _iXOverScanPx) + _intJ].ToString() + "\t");
                        }
                        sr.Write("\r\n");
                    }

                    sr.Close();
                }

                using (StreamWriter sr = new StreamWriter(dialog.FileName.Replace(".txt", "_CH2.txt")))  //write Channel 2 picture to ASCII file
                {
                    sr.Write(_strData2);  //write header info to file

                    for (int _intI = 0; _intI < _iImageHeight; _intI++)  //write pixel values to file
                    {
                        for (int _intJ = 0; _intJ < _iImageWidth + _iXOverScanPx; _intJ++)
                        {
                            sr.Write(_uint32ChannelData2[_intI * (_iImageWidth + _iXOverScanPx) + _intJ].ToString() + "\t");
                        }
                        sr.Write("\r\n");
                    }

                    sr.Close();
                }
            }

        }


		private void ApplyTimeGating( int __iChannel, bool __bIsToApplyTimeGating, double __dGatingTimeMinMillisec, double __dGatingTimeMaxMillisec )
		{
			// Access the ScanDocument object related to this form.            
			ScanDocument _docDocument = this.Document as ScanDocument;

			string _sInputFile = "";  // the path to the raw TTTR file with the recorded photon arrrival times - from this data we produce time gated image
			int _iRecordsOffset = 0;  // the TTTR record from which the reading of the TTTR records starts
			double _dXScanSizeNm = 0.0;  // the physical size of the image along X in [ns] as read from the TTTR header
			double _dYScanSizeNm = 0.0;  // the physical size of the image along Y in [ns] as read from the TTTR header
			int _iXImageWidth = 0;  // the size of the image along X in [px] as read from the TTTR header
			int _iYImageHeight = 0;  // the size of the image along Y in [px] as read from the TTTR header
			uint[] _ui32FramePixelBuffer = null;  // the extracted time gated image will be stored in this array

			// We probe to possible file paths for a valid TTTR file
			int _iTTTRFileCounter = KUL.MDS.Hardware.PQTimeHarp.Files.FileCounter;  // counts (starting from 0) how many TTTR files we have written to hard disk so far
			string _sInputFile1 = Path.ChangeExtension( _docDocument.FilePath, ".t3r");  // the path and name to the data file with raw photon data (the TTTR file) - first we assign that the name is the same as the saved SIS ".dat" file but with ".t3r" extension
			string _sInputFile2 = Path.GetDirectoryName( _docDocument.FilePath ) + "\\" + _docDocument.Settings.TimeHarpNameTTTRFile + "." + _iTTTRFileCounter.ToString() + ".t3r";  // the path and name to the data file with raw photon data (the TTTR file) - this is the supposed temp TTTR file

			// Check if we have a TTTR file
			if (File.Exists(_sInputFile1) && !_docDocument.Modified)
			{
				_sInputFile = _sInputFile1;  // first (if document not modified) we guess that the name is the same as the saved SIS ".dat" file but with extension ".t3r"
			}
			else if ( File.Exists( _sInputFile2 ) )
			{
				_sInputFile = _sInputFile2;  // if first file does not exists we guess that the name (without the extension) is the same as the temp TTTR file
			}
			else
			{
				_sInputFile = "";  // if a TTTR file with the above requirements does not exist then we assign empty string and will not process any file (because there is no such suitable file)
			}
			
			// If there is a TTTR file, extract image as a pixel buffer
			if ( _sInputFile != "" )
			{
				KUL.MDS.Hardware.PQTimeHarp.ExtractImageFromTTTRFile( __bIsToApplyTimeGating, __dGatingTimeMinMillisec, __dGatingTimeMaxMillisec, _sInputFile, ref _iRecordsOffset,ref _dXScanSizeNm, ref _dYScanSizeNm, ref _iXImageWidth, ref _iYImageHeight, ref _ui32FramePixelBuffer );

				// Check if we indeed have a valid image in the TTTR file and the size matches with those from the current settings - if yes then store it in the scan doc data structure 
				if ( _ui32FramePixelBuffer != null && _dXScanSizeNm == _docDocument.XScanSizeNm && _dXScanSizeNm == _docDocument.YScanSizeNm && _iXImageWidth == _docDocument.ImageWidthPx && _iYImageHeight == _docDocument.ImageHeightPx )
				{
					_docDocument.StoreChannelData(__iChannel, _ui32FramePixelBuffer, 0, _docDocument.PixelCount);  // store the processed image into the doc image field
				}
			}        
		}


		private void btnApplyGateAPD1_Click( object sender, EventArgs e )
		{
			// Get time gating settings
			int _iChannel = 0;  // channel is 0 because the button is assigned to APD1 and we show the result on the APD1's channel
			bool _bIsToApplyTimeGating = true;  // we want gating so set to true
			double _dGatingTimeMinMillisec = Convert.ToDouble( txtboxTimeGatingMinAPD1.Text.Trim() ) * 1e-6;  // the lower bound of the gating window (user input is in [ns], so we also convert it to [ms])
			double _dGatingTimeMaxMillisec = Convert.ToDouble( txtboxTimeGatingMaxAPD1.Text.Trim() ) * 1e-6;  // the upper bound of the gating window (user input is in [ns], so we also convert it to [ms])							
			
			// Apply gating with the given settings
			ApplyTimeGating( _iChannel, _bIsToApplyTimeGating, _dGatingTimeMinMillisec, _dGatingTimeMaxMillisec );

			// Update the GUI
			UpdateUI();
		}


		private void btnNoGateAPD1_Click( object sender, EventArgs e )
		{
			// Get time gating settings
			int _iChannel = 0;  // channel is 0 because the button is assigned to APD1 and we show the result on the APD1's channel
			bool _bIsToApplyTimeGating = false;  // we do not want gating so set to false
			double _dGatingTimeMinMillisec = 0.0;  // the lower bound of the gating window (user input is in [ns], so we also convert it to [ms])
			double _dGatingTimeMaxMillisec = 0.0;  // the upper bound of the gating window (user input is in [ns], so we also convert it to [ms])							

			// Apply gating with the given settings
			ApplyTimeGating( _iChannel, _bIsToApplyTimeGating, _dGatingTimeMinMillisec, _dGatingTimeMaxMillisec );

			// Update the GUI
			UpdateUI();
		}


		private void btnApplyGateAPD2_Click( object sender, EventArgs e )
		{
			// Get time gating settings
			int _iChannel = 1;  // channel is 1 because the button is assigned to APD2 and we show the result on the APD2's channel
			bool _bIsToApplyTimeGating = true;  // we want gating so set to true
			double _dGatingTimeMinMillisec = Convert.ToDouble( txtboxTimeGatingMinAPD2.Text.Trim() ) * 1e-6;  // the lower bound of the gating window (user input is in [ns], so we also convert it to [ms])
			double _dGatingTimeMaxMillisec = Convert.ToDouble( txtboxTimeGatingMaxAPD2.Text.Trim() ) * 1e-6;  // the upper bound of the gating window (user input is in [ns], so we also convert it to [ms])							

			// Apply gating with the given settings
			ApplyTimeGating( _iChannel, _bIsToApplyTimeGating, _dGatingTimeMinMillisec, _dGatingTimeMaxMillisec );

			// Update the GUI
			UpdateUI();
		}


		private void btnNoGateAPD2_Click( object sender, EventArgs e )
		{
			// Get time gating settings
			int _iChannel = 1;  // channel is 1 because the button is assigned to APD2 and we show the result on the APD2's channel
			bool _bIsToApplyTimeGating = false;  // we do not want gating so set to false
			double _dGatingTimeMinMillisec = 0.0;  // the lower bound of the gating window (user input is in [ns], so we also convert it to [ms])
			double _dGatingTimeMaxMillisec = 0.0;  // the upper bound of the gating window (user input is in [ns], so we also convert it to [ms])							

			// Apply gating with the given settings
			ApplyTimeGating( _iChannel, _bIsToApplyTimeGating, _dGatingTimeMinMillisec, _dGatingTimeMaxMillisec );

			// Update the GUI
			UpdateUI();
		}


		#endregion Processing data


		#region View next/previous data file

		private void btnLoadNextFile_Click(object sender, EventArgs e)
		{			
			// Access the ScanDocument object related to this form.            
			ScanDocument _docDocument = this.Document as ScanDocument;

			// In case the current document is not saved ask user what to do
			DialogResult _dialogResult = DialogResult.Yes;  // default is to continue to load the file (if exists)
			if (_docDocument.Modified)
			{
				string _sMessage = "Current file is not saved! Continue anyway?";
				_dialogResult = MessageBox.Show(_sMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			}

			// Check if we go to the next file
			if (_dialogResult == DialogResult.Yes)
			{
				string _sCurrentFilePath = _docDocument.FilePath;  // get the full path and name of the currently opened file
				string _sDirectoryPath = Path.GetDirectoryName(_docDocument.FilePath);  // get current directory
				string _sFileFilter = "*.dat";  // SIS file filter - we want to load only SIS files

				string[] _sFilePaths = Directory.GetFiles(_sDirectoryPath, _sFileFilter);  // get all files in the current directory
				int _iFilesCount = _sFilePaths.Length;  // get the number of files in the current directory

				Array.Sort(_sFilePaths);  // sort file names

				int _iIndexOfCurrentFile = Array.IndexOf(_sFilePaths, _sCurrentFilePath);  // get the index of the current file
				int _iIndexOfNextFile = _iIndexOfCurrentFile + 1;  // this is the index of the next file in the dir that we are going to load

				// Check if the index of the next file is a valid (must be smaller than the number of files in the directory)
				if (_iIndexOfNextFile > 0 && _iIndexOfNextFile < _iFilesCount)
				{
					// Get the file path of the next file
					string _sNextFilePath = _sFilePaths[_iIndexOfNextFile];

					// Check if the file exists - if yes, load it.
					if (File.Exists(_sNextFilePath))
					{
						_docDocument.LoadDocument(_sNextFilePath, false);  // load next available SIS file
					}
				}
				else if (_iIndexOfNextFile >= _iFilesCount) // we wrap around, i.e. current case start from the beginning
				{
					// Get the file path of the next file - we start from the beginning
					string _sNextFilePath = _sFilePaths[0];

					// Check if the file exists - if yes, load it.
					if (File.Exists(_sNextFilePath))
					{
						_docDocument.LoadDocument(_sNextFilePath, false);  // load next available SIS file					
					}
				}

				// Update screen values for the current document
				ScanPropertiesToScreen();
				this.Text = _docDocument.FileName;  //update document title
				_docDocument.Modified = false;  // update the modified state of the currently opened document (must be false, this a new document)
			}
			
			// Update the GUI			
			UpdateUI();
		}


		private void btnLoadPreviousFile_Click(object sender, EventArgs e)
		{			
			// Access the ScanDocument object related to this form.            
			ScanDocument _docDocument = this.Document as ScanDocument;

			// In case the current document is not saved as k user what to do
			DialogResult _dialogResult = DialogResult.Yes;  // default is to continue to load the file (if exists)
			if (_docDocument.Modified)
			{
				string _sMessage = "Current file is not saved! Continue anyway?";
				_dialogResult = MessageBox.Show(_sMessage, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
			}

			// Check if we go to previous file
			if (_dialogResult == DialogResult.Yes)
			{
				string _sCurrentFilePath = _docDocument.FilePath;  // get the full path and name of the currently opened file
				string _sDirectoryPath = Path.GetDirectoryName(_docDocument.FilePath);  // get current directory
				string _sFileFilter = "*.dat";  // SIS file filter - we want to load only SIS files

				string[] _sFilePaths = Directory.GetFiles(_sDirectoryPath, _sFileFilter);  // get all files in the current directory
				int _iFilesCount = _sFilePaths.Length;  // get the number of files in the current directory

				Array.Sort(_sFilePaths);  // sort file names

				int _iIndexOfCurrentFile = Array.IndexOf(_sFilePaths, _sCurrentFilePath);  // get the index of the current file
				int _iIndexOfPreviousFile = _iIndexOfCurrentFile - 1;  // this is the index of the previous file in the dir that we are going to load

				// Check if the index of the previous file is a valid (must be smaller than the number of files in the directory)
				if (_iIndexOfPreviousFile >= 0)
				{
					// Get the file path of the previous file
					string _sPreviousFilePath = _sFilePaths[_iIndexOfPreviousFile];

					// Check if the file exists - if yes, load it.
					if (File.Exists(_sPreviousFilePath))
					{
						_docDocument.LoadDocument(_sPreviousFilePath, false);  // load next available SIS file					
					}
				}
				else if (_iIndexOfPreviousFile < 0) // we wrap around, i.e. current case start from the beginning
				{
					// Get the file path of the previous file - we start from the end
					string _sPreviousFilePath = _sFilePaths[_iFilesCount - 1];

					// Check if the file exists - if yes, load it.
					if (File.Exists(_sPreviousFilePath))
					{
						_docDocument.LoadDocument(_sPreviousFilePath, false);  // load next available SIS file					
					}
				}

				// Update screen values for the current document
				ScanPropertiesToScreen();				
				this.Text = _docDocument.FileName;  //update document title
				_docDocument.Modified = false;  // update the modified state of the currently opened document (must be false, this is a new document)
			}
						
			// Update the GUI
			UpdateUI();
		}

		#endregion View next/previous data file
		

	}
}