using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIS.Hardware;
using System.Threading;
using System.Globalization;

namespace SIS.Forms
{
    public partial class CountRateForm : Form
    {
        private NISampleClock m_clock;
        private APD m_APD;

        public CountRateForm()
        {
            InitializeComponent();
        }

        private void CountRateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.backgroundWorker1.CancelAsync();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bool _bStop = false;

            this.m_clock = new NISampleClock("Dev1", "Ctr2");

            this.m_APD = new SIS.Hardware.APD("Dev1", "Ctr0", 100, "Ctr2InternalOutput", "Ctr1", "PFI15", false);

            this.m_APD.SetupAPDCountAndTiming(100.0, 100000);
            this.m_APD.StartAPDAcquisition();

            this.m_clock.Start(0.1);

            uint[] data;

            while (!_bStop)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    _bStop = true;
                }
                else
                {
                    data = this.m_APD.Read();

                    foreach (uint sample in data)
                    {
                        if (!this.Disposing && InvokeRequired)
                        {
                            Invoke((MethodInvoker)delegate { this.label1.Text = (sample * 10).ToString("E3"); });
                        }
                    }
                }

                Thread.Sleep(50);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.m_APD.StopAPDAcquisition();
            this.m_clock.Stop();
            this.Hide();
        }

        private void CountRateForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.backgroundWorker1.IsBusy)
            {
                this.backgroundWorker1.RunWorkerAsync();
            }
            
        }
    }
}
