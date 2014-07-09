// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="CountRateForm.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Count Rate Form - displays the current count rate as measured from the APD source
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Forms
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    using SIS.Hardware;

    /// <summary>
    /// Count Rate Form - displays the current count rate as measured from the APD source
    /// </summary>
    public partial class CountRateForm : Form
    {
        #region Fields

        /// <summary>
        /// The m_apd ap d 1.
        /// </summary>
        private PQTimeHarp m_apdAPD1 = null; // points to the currently running APD1

        /// <summary>
        /// The m_b is button stop clicked.
        /// </summary>
        private volatile bool m_bIsButtonSTOPClicked = false;

        // loop until we click the STOP button or close the Count Rate form

        /// <summary>
        /// The m_thread start count rate meter.
        /// </summary>
        private Thread m_threadStartCountRateMeter = null; // run the Count Rate Meter on a separate thread

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CountRateForm"/> class.
        /// </summary>
        /// <param name="__apdAPD">
        /// The __apd apd.
        /// </param>
        public CountRateForm(PQTimeHarp __apdAPD)
        {
            this.InitializeComponent();
            this.m_apdAPD1 = __apdAPD; // get the APD				
        }

        #endregion

        // Delegate involved in handling cross thread passing of data
        #region Delegates

        /// <summary>
        /// The ui update button delegate.
        /// </summary>
        /// <param name="__btnControl">
        /// The __btn control.
        /// </param>
        /// <param name="__sString">
        /// The __s string.
        /// </param>
        private delegate void UIUpdateButtonDelegate(Button __btnControl, string __sString);

        /// <summary>
        /// The ui update delegate.
        /// </summary>
        private delegate void UIUpdateDelegate();

        #endregion

        // Indicate if Count Rate Meter is running or not
        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (this.m_threadStartCountRateMeter != null)
                {
                    return !this.m_threadStartCountRateMeter.IsAlive;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        // Count Rate Form Constructor

        // Start the Count Rate Meter in a separate threat
        #region Public Methods and Operators

        /// <summary>
        /// The run count rate meter.
        /// </summary>
        public void RunCountRateMeter()
        {
            this.m_threadStartCountRateMeter = new Thread(this.StartCountRateMeter);
            this.m_threadStartCountRateMeter.Name = "StartCountRateMeter()"; // set the name of the thread
            this.m_threadStartCountRateMeter.IsBackground = true; // set the thread as a background thread
            this.m_threadStartCountRateMeter.Priority = ThreadPriority.Normal;

            // set the thread priority to normal            
            this.m_threadStartCountRateMeter.Start(); // start the thread			
        }

        #endregion

        // Start the count rate meter and show the result on the screen
        #region Methods

        /// <summary>
        /// The control set text.
        /// </summary>
        /// <param name="__btnControl">
        /// The __btn control.
        /// </param>
        /// <param name="__sString">
        /// The __s string.
        /// </param>
        private void ControlSetText(Button __btnControl, string __sString)
        {
            __btnControl.Text = __sString; // update control text			
        }

        /// <summary>
        /// The count rate form_ form closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CountRateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopCountRateMeter(); // stop count rate		
        }

        /// <summary>
        /// The start count rate meter.
        /// </summary>
        private void StartCountRateMeter()
        {
            // Update UI
            this.UpdateUI();

            this.m_bIsButtonSTOPClicked = false; // false because we want to probe the count rate at least once
            int _iCountRate = 0;
            double _dCountRate = 0.0;
            string _sFormatedCountRate = string.Empty;

            // Loop in order to show continuously the count rate (the count rate is probed every 600ms are longer)
            while (!this.m_bIsButtonSTOPClicked && this.m_apdAPD1 != null)
            {
                _iCountRate = this.m_apdAPD1.CountRate; // get the count rate
                _dCountRate = Convert.ToDouble(_iCountRate);

                // convert the count rate in order to recalc it in units of Cps/Kcps/Mcps

                // Check if we really get a valid count rate
                if (_iCountRate >= 0)
                {
                    // Format the output digits to fit within the window
                    if (_iCountRate == 0)
                    {
                        // case 0Hz
                        _sFormatedCountRate = string.Format("{0:0.000} Hz", _dCountRate);
                    }
                    else if (_iCountRate > 0 && _iCountRate < 10000)
                    {
                        // case below 1Hz - 10kHz
                        _sFormatedCountRate = string.Format("{0:####} Hz", _dCountRate);
                    }
                    else if (_iCountRate >= 10000 && _iCountRate < 100000)
                    {
                        // case 10kHz - 100kHz
                        _dCountRate /= 1000.0;
                        _sFormatedCountRate = string.Format("{0:##.000} kHz", _dCountRate);
                    }
                    else if (_iCountRate >= 100000 && _iCountRate < 1000000)
                    {
                        // case 100kHz - 1000kHz
                        _dCountRate /= 1000.0;
                        _sFormatedCountRate = string.Format("{0:###.00} kHz", _dCountRate);

                        // show current count rate to the user						
                    }
                    else
                    {
                        // case above 1MHz
                        _dCountRate /= 1000000.0;
                        _sFormatedCountRate = string.Format("{0:##.000} MHz", _dCountRate);
                    }
                }
                else
                {
                    _sFormatedCountRate = string.Format("{0}", "APD busy!");
                    this.m_bIsButtonSTOPClicked = true;

                    // causes to exit the loop because count rate cannot be measured - device seems busy
                }

                // Update UI
                this.UIUdateControl(this.btnCoutRateMeterAPD1, _sFormatedCountRate);

                // show count rate info or warning (in case is APD busy with another type of measurement)
                this.UpdateUI();
            }

            // Free the pointer to the APD
            this.m_apdAPD1 = null;
        }

        /// <summary>
        /// The stop count rate meter.
        /// </summary>
        private void StopCountRateMeter()
        {
            this.m_bIsButtonSTOPClicked = true;
            if (this.m_threadStartCountRateMeter.IsAlive)
            {
                this.m_threadStartCountRateMeter.Join(); // wait for the StartCountRate() thread to complete				
            }
        }

        /// <summary>
        /// The ui udate control.
        /// </summary>
        /// <param name="__btnControl">
        /// The __btn control.
        /// </param>
        /// <param name="__sString">
        /// The __s string.
        /// </param>
        private void UIUdateControl(Button __btnControl, string __sString)
        {
            if (!__btnControl.IsDisposed)
            {
                if (__btnControl.InvokeRequired)
                {
                    this.BeginInvoke(
                        new UIUpdateButtonDelegate(this.ControlSetText), 
                        new object[] { __btnControl, __sString });

                    // update control text asynchronously (asynchronous Invoke necessary for the proper behavior when terminating the Count Rate Form)
                }
                else
                {
                    __btnControl.Text = __sString; // update control text
                }
            }
        }

        /// <summary>
        /// The update ui.
        /// </summary>
        private void UpdateUI()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIUpdateDelegate(this.Refresh));

                // update GUI asynchronously (asynchronous Invoke necessary for the proper behavior when terminating the Count Rate Form)
            }
            else
            {
                this.Refresh();
            }

            // Process any events that might be waiting.
            Application.DoEvents();
        }

        // Stop the count rate meter

        /// <summary>
        /// The btn count rate sto p_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnCountRateSTOP_Click(object sender, EventArgs e)
        {
            this.StopCountRateMeter(); // stop count rate
            this.Close(); // close the form						
        }

        #endregion
    }
}