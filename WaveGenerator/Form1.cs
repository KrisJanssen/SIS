using SIS.Forms;
using SIS.ScanModes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveGenerator
{
    public partial class Form1 : Form
    {
        private SIS.Hardware.IPiezoStage m_Stage;
        private SIS.Forms.TrajectoryPlotForm m_frmTrajectoryForm = new TrajectoryPlotForm();

        // Delegate involved in handling cross thread passing of data from Hardware to UI.
        private delegate void UIUpdateDelegate();

        public Form1()
        {
            InitializeComponent();

            // Create a suitable stage.
            this.m_Stage = SIS.Hardware.NILineScanner.Instance;

            // Hook up EventHandler methods to the events of the stage.
            this.m_Stage.PositionChanged += new EventHandler(m_Stage_PositionChanged);
            this.m_Stage.ErrorOccurred += new EventHandler(m_Stage_ErrorOccurred);
            this.m_Stage.EngagedChanged += new EventHandler(m_Stage_EngagedChanged);
        }

        void m_Stage_EngagedChanged(object sender, EventArgs e)
        {
            // The text indicating stage status should always be updated.
            this.lblSTATUS.Text = this.m_Stage.IsInitialized.ToString();
        }

        void m_Stage_ErrorOccurred(object __oSender, EventArgs __evargsE)
        {
            MessageBox.Show(m_Stage.CurrentError);
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

        private void UpdateUI()
        {
            // Update the UI with the current voltage to stage.
            this.lblPOS.Text = this.m_Stage.XPosition.ToString();

            // Process any events that might be waiting.
            Application.DoEvents();
        }

        // Called to re-enable controls after a scan.
        private void EnableCtrls()
        {
            // Disable the Scan button because validation is always necessary before scanning can start.
            this.btnMOVE.Enabled = true;
            this.btnOFF.Enabled = true;
            this.btnON.Enabled = true;
            this.btnSTOP.Enabled = true;
            this.btnSTART.Enabled = true;
        }

        // Called to enable controls during a scan in progress.
        private void DisableCtrls()
        {
            // Disable the Scan button.
            this.btnMOVE.Enabled = false;
            this.btnOFF.Enabled = false;
            this.btnON.Enabled = false;
            this.btnSTOP.Enabled = true;
            this.btnSTART.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnON_Click(object sender, EventArgs e)
        {
            // Connect to the controller hardware and initialize it.
            this.m_Stage.Initialize();

            // Initialize stage control and update status indicator only if INIT worked.
            if (this.m_Stage.IsInitialized)
            {
                // We cannot turn the stage on twice!
                this.btnON.Enabled = false;
                // Feedback to UI.
                this.lblSTATUS.ForeColor = Color.FromKnownColor(KnownColor.Lime);
                // We might want to turn it off.
                this.btnOFF.Enabled = true;
            }
            else
            {
                this.lblSTATUS.ForeColor = Color.FromKnownColor(KnownColor.Red);
            }

            // The text indicating stage status should always be updated.
            this.lblSTATUS.Text = this.m_Stage.IsInitialized.ToString();

            // Update the UI.
            this.UpdateUI();
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            // Disconnect from the stage hardware.
            this.m_Stage.Release();

            // Release stage control and update status.
            if (!this.m_Stage.IsInitialized)
            {
                this.btnOFF.Enabled = false;
                this.lblSTATUS.ForeColor = Color.FromKnownColor(KnownColor.Red);
                this.lblSTATUS.Text = m_Stage.IsInitialized.ToString();
                this.btnON.Enabled = true;
            }

            // Update the UI.
            UpdateUI();
        }

        private void btnMOVE_Click(object sender, EventArgs e)
        {
            double[] _dXYCoordinates = new double[3];
            _dXYCoordinates[0] = Convert.ToDouble(this.txtMOVE.Text);
            _dXYCoordinates[1] = 0.0;
            _dXYCoordinates[2] = 0.0;
            this.workerMove.RunWorkerAsync(_dXYCoordinates);
        }

        private void workerMove_DoWork(object sender, DoWorkEventArgs e)
        {
            double[] _dXYCoordinates = (double[])e.Argument;
            this.m_Stage.MoveAbs(_dXYCoordinates[0], _dXYCoordinates[1], _dXYCoordinates[2]);
        }

        private void btnSTART_Click(object sender, EventArgs e)
        {
            Scanmode Scan = new UniDirXYScan(
                Convert.ToUInt16(this.txtPIXELS.Text),
                Convert.ToUInt16(this.txtPIXELS.Text),
                0,
                0,
                0,
                0,
                1000.0,
                1000.0,
                1000.0,
                25,
                1,
                1,
                0.2);

            this.m_frmTrajectoryForm.Visible = true;
            this.m_frmTrajectoryForm.NMCoordinates = Scan.ScanCoordinates;

            PrepnRun(Scan);
        }

        private void btnSTOP_Click(object sender, EventArgs e)
        {
            // Cancel de backgroundworker.
            workerScan.CancelAsync();

            // Enable all controls again.
            EnableCtrls();

            // Disable the Stop button again.
            this.btnSTOP.Enabled = false;
            this.btnSTART.Enabled = true;
        }

        private void PrepnRun(Scanmode __scnmScan)
        {
            // Disable the controls so the user cannot interfere with the scan. Only stopping the scan will be allowed.
            DisableCtrls();
            
            // Check if the stage is definitely engaged and ready.... if not all other operations would be useless!
            if (m_Stage.IsInitialized)
            {
                // Make sure the Stop button works.
                this.btnSTOP.Enabled = true;

                // Run the actual measurement in a separate thread to the UI thread. This will prevent the UI from blocking and it will
                // enable continuous updates of the UI with scan data.
                workerScan.RunWorkerAsync(__scnmScan);
            }

            // Update the UI.
            UpdateUI();
        }

        private void workerScan_DoWork(object sender, DoWorkEventArgs e)
        {
            // Boolean value to indicate wheter or not the running scan should be stopped.
            bool _bStop = false;

            // Assign the values to be written. They were passed as an event argument to the DoWork event for the background worker.
            Scanmode _Scan = (Scanmode)e.Argument;

            // Initiate stage scan movement.
            this.m_Stage.Scan(
                _Scan,
                Convert.ToDouble(this.txtTPL.Text) / Convert.ToDouble(this.txtPIXELS.Text),
                true,
                0.0,
                0,
                false,
                0.0,
                false);

            while (!_bStop)
            {
                // Update the UI every 0.1 seconds, more than fast enough.
                Thread.Sleep(100);

                Application.DoEvents();

                // Update the UI.
                if (InvokeRequired)
                {
                    // Update the rest of the UI.
                    Invoke(new UIUpdateDelegate(UpdateUI));
                }

                // Check if the worker was not cancelled.
                if (workerScan.CancellationPending)
                {
                    e.Cancel = true;
                    _bStop = true;
                }
            }

            // Update the UI.
            if (InvokeRequired)
            {
                // Update the rest of the UI.
                Invoke(new UIUpdateDelegate(UpdateUI));
            }
        }

        private void workerScan_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Actually stop the stage from scanning.
            this.m_Stage.Stop();

            // Wait a bit.
            Thread.Sleep(500);

            if (e.Cancelled)
            {
                // Inform the user.
                //MessageBox.Show("Scan Cancelled, press OK to zero stage.");
            }
            else
            {
                //MessageBox.Show("Scan Completed, press OK to zero stage.");
            }

            this.m_Stage.MoveAbs(
                Convert.ToDouble(this.txtMOVE.Text),
                0.0,
                0.0);

            this.EnableCtrls();
        }
    }
}
