using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using KUL.MDS.Hardware;
using KUL.MDS.ScanModes;
using KUL.MDS.SystemLayer;
using System.IO;
using KUL.MDS.Library;

namespace PI_Digital_Stage_Test_Framework
{
    public partial class MainForm : Form
    {
        // The stage hardware object is obviously a member of the form so it is always accessible.
        private KUL.MDS.Hardware.IPiezoStage m_Stage;

        private Logger m_logPositionLogger;

        // Delegate involved in handling cross thread passing of data from Hardware to UI.
        private delegate void UIUpdateDelegate();

        #region FORM RELATED CODE

        /// <summary>
        /// The Main Form Constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // The piezo stage is the most critical hardware resource. To prevent conflicts it is created as a singleton instance.
            this.m_Stage = KUL.MDS.Hardware.PIDigitalStage.Instance;

            // Hook up EventHandler methods to the events of the stage.
            // Find the event handler methods in the coderegion HARDWARE EVENT HANDLERS
            // When the stage moves it can raise a PositionChanged event and then the position indicators on the form need to update.
            this.m_Stage.PositionChanged += new EventHandler(m_Stage_PositionChanged);
            // When one of the E7XX DLL calls generates an error the user needs to be informed.
            this.m_Stage.ErrorOccurred += new EventHandler(m_Stage_ErrorOccurred);      
        }

        /// <summary>
        /// Called to update the data display area of the UI, namely the stage position.
        /// </summary>
        private void UpdateUI()
        {
            // Update the UI with the position of the stage.
            txtbxCurrXPos.Text = this.m_Stage.XPosition.ToString();
            txtbxCurrYPos.Text = this.m_Stage.YPosition.ToString();

            // Send record of XY positions to debug output.
            //Tracing.Ping("Position X: " + txtbxCurrXPos.Text + " -- Position Y: " + txtbxCurrYPos.Text);
            if (m_logPositionLogger != null)
            {
                m_logPositionLogger.WriteLine(LogType.Info, "Position X: " + txtbxCurrXPos.Text + " -- Position Y: " + txtbxCurrYPos.Text);
            }

            // Process any events that might be waiting for Form1.
            Application.DoEvents();
        }

        #endregion

        #region BUTTON EVENT HANDLERS

        /// <summary>
        /// The event handler for the StageOn Button.
        /// </summary>
        /// <param name="__oSender">The Sender (Button)</param>
        /// <param name="__evargsE">The Event Arguments for the sending Button</param>
        private void btnStageON_Click(object __oSender, EventArgs __evargsE)
        {
            // Connect to the controller hardware and initialize it.
            this.m_Stage.Initialize();

            // Initialize stage control and update status indicator only if INIT worked.
            if (this.m_Stage.IsInitialized)
            {
                // Feedback to UI.
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Lime);
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
        /// The event handler for the StageOff Button.
        /// </summary>
        /// <param name="__oSender">The Sender (Button)</param>
        /// <param name="__evargsE">The Event Arguments for the sending Button</param>
        private void btnStageOFF_Click(object __oSender, EventArgs __evargsE)
        {
            // Disconnect from the stage hardware.
            this.m_Stage.Release();

            // Release stage control and update status.
            if (!this.m_Stage.IsInitialized)
            {
                this.lblStageVoltageEngaged.ForeColor = Color.FromKnownColor(KnownColor.Red);
                this.lblStageVoltageEngaged.Text = m_Stage.IsInitialized.ToString();
            }

            // Update the UI.
            UpdateUI();
        }

        /// <summary>
        /// The event handler for the ScanStart Button.
        /// </summary>
        /// <param name="__oSender">The Sender (Button)</param>
        /// <param name="__evargsE">The Event Arguments for the sending Button</param>
        private void btnScanStart_Click(object __oSender, EventArgs __evargsE)
        {
            // A ScanMode object holds all the information that defines a certain scan behavior of the stage.
            // It exposes the scan to the user as a set of nm coordinate, a Native PI command set or a set of voltages that can be sent to
            // analog stage controllers.
            // All scans, be it unidirectional of bidirectional XY scans are defined by a startpoint a physical width and height and 
            // a pixelwidth and pixelheight of the final image.
            // The parameters ScanSpeed and Bintime (the final two) ARE NOT YET USED.
            // The ScanMode will eventualy be passed to the stage Scan method to run the scan.
            Scanmode m_BiScan = new UniDirXYScan(
                System.Convert.ToInt32(this.txtbxSetImageWidth.Text),
                System.Convert.ToInt32(this.txtbxSetImageWidth.Text),
                0,
                System.Convert.ToInt32(this.txtbxOverScanPx.Text),
                System.Convert.ToInt32(this.txtbxOverScanPx.Text),
                0,
                System.Convert.ToDouble(this.txtbxSetInitX.Text),
                System.Convert.ToDouble(this.txtbxSetInitY.Text),
                0.0d,
                System.Convert.ToDouble(this.txtbxSetImageWidthnm.Text),
                System.Convert.ToDouble(this.txtbxSetImageWidthnm.Text),
                0.0d,
                10,
                2,
                1.0,
                0.2);

            // Continue with prepping and eventually running the scan.
            PrepnRunScan(m_BiScan);
        }

        /// <summary>
        /// The event handler for the Stop Button.
        /// </summary>
        /// <param name="__oSender">The Sender (Button)</param>
        /// <param name="__evargsE">The Event Arguments for the sending Button</param>
        private void btnStop_Click(object sender, EventArgs __evargsE)
        {
            // Cancel de backgroundworker.
            bckgwrkPerformScan.CancelAsync();


            // Disable the Stop button again.
            this.btnStop.Enabled = false;
        }

        /// <summary>
        /// The event handler for the ZeroStage Button.
        /// </summary>
        /// <param name="__oSender">The Sender (Button)</param>
        /// <param name="__evargsE">The Event Arguments for the sending Button</param>
        private void btnMoveAbs_Click(object __oSender, EventArgs __evargsE)
        {
            double[] _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = Convert.ToDouble(this.txtbxGoToX.Text);
            _dXYCoordinates[1] = Convert.ToDouble(this.txtbxGoToY.Text);
            _dXYCoordinates[2] = 0.0;
            this.m_Stage.MoveAbs(_dXYCoordinates[0], _dXYCoordinates[1], _dXYCoordinates[2]);

            // Wait a bit.
            Thread.Sleep(2000);
        }

        #endregion

        #region SCAN SETUP AND RUN

        /// <summary>
        /// Do everything necessary for actually running the scan.
        /// </summary>
        /// <param name="__scnmScan">The defenition of the scantype that will be used to tell the stage how to scan.</param>
        private void PrepnRunScan(Scanmode __scnmScan)
        {
            // Check if the stage is definitely ready.
            if (m_Stage.IsInitialized)
            {
                // Move the stage to the starting position.
                this.m_Stage.MoveAbs(System.Convert.ToDouble(this.txtbxSetInitX.Text), System.Convert.ToDouble(this.txtbxSetInitY.Text), 0.0);

                // Wait for the stage to settle.
                Thread.Sleep(2000);

                // Make sure the Stop button works.
                this.btnStop.Enabled = true;

                // Prepare the stage control task for writing as many samples as necessary to complete the scan.
                // Note that the second parameter is only there for compatibility with other hardware implementations,
                // IT IS NOT used for the PIDigitalStage!!!!
                // The first parameter defines the table rate of the DigitalStage.
                this.m_Stage.Configure(System.Convert.ToDouble(this.txtbxSetTimePPixel.Text), 1);

                // Debug Writing of coordinates to File.
                string _sLogFile = Logger.GetNewLogFilename("SIS Position Log");
                Logger m_logPositionLogger = new Logger(_sLogFile);
                m_logPositionLogger.WriteLine(LogType.Info, "New scan started\r\n----------------------");
                m_logPositionLogger.WriteLine(LogType.Info, "Axes: " + __scnmScan.ScanAxes.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "ImageWidthPx: " + __scnmScan.ImageWidthPx.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "ImageHeightPx: " + __scnmScan.ImageHeightPx.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "ImageDepthPx: " + __scnmScan.ImageDepthPx.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "XScanSizeNm: " + __scnmScan.XScanSizeNm.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "YScanSizeNm: " + __scnmScan.YScanSizeNm.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "ZScanSizeNm: " + __scnmScan.ZScanSizeNm.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "InitX: " + __scnmScan.InitialX.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "InitY: " + __scnmScan.InitialY.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "InitZ: " + __scnmScan.InitialZ.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "XAmp: " + __scnmScan.XAmplitude.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "YAmp: " + __scnmScan.YAmplitude.ToString());
                m_logPositionLogger.WriteLine(LogType.Info, "ZAmp: " + __scnmScan.ZAmplitude.ToString()); 
                

                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                bckgwrkPerformScan.RunWorkerAsync(__scnmScan);
            }

            // Update the UI.
            UpdateUI();
        }

        /// <summary>
        /// The things that will happen during the time in which the scan runs.
        /// </summary>
        /// <param name="__oSender"></param>
        /// <param name="__evargsE"></param>
        private void bckgwrkPerformScan_DoWork(object __oSender, System.ComponentModel.DoWorkEventArgs __evargsE)
        {
            // Boolean value to indicate wheter or not the running scan should be stopped.
            bool _bStop = false;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            Scanmode _Scan = (Scanmode)__evargsE.Argument;

            // Initiate stage scan movement.
            this.m_Stage.Scan(_Scan, true);
            
            while (_bStop != true)
            {
                // Update the UI every 0.xx seconds, more than fast enough.
                Thread.Sleep(100);

                // Update the UI.
                if (InvokeRequired)
                {
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

            // Update the UI. This is a cross thread operation.
            if (InvokeRequired)
            {
                // Update the rest of the UI.
                Invoke(new UIUpdateDelegate(UpdateUI));
            }

            Thread.Sleep(1000);
        }

        /// <summary>
        /// Finish up after the scan is complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="__evargsE"></param>
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

            this.m_Stage.MoveAbs(0.0, 0.0, 0.0);

            // Wait a bit.
            Thread.Sleep(2000);

            // Update the UI.
            UpdateUI();

            m_logPositionLogger.WriteLine(LogType.Info, "Scan Finished\r\n----------------------");

            // Get rid of the logger.
            m_logPositionLogger = null;
        }

        

        #endregion

        #region HARDWARE EVENT HANDLERS

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
    }
}
