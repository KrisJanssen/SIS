using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KUL.MDS.Library;

namespace KUL.MDS.Hardware
{
    public class YanusIV : IPiezoStage
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Members.

        // Create variables to keep track of the currently set voltage to the Piezo stage.
        private double m_dXPosCurrent;
        private double m_dYPosCurrent;
        private double m_dZPosCurrent;
        private int m_iSamplesToStageCurrent;

        // Some properties of the stage.
        private int m_iControllerID;
        private string m_sIDN;
        private Error m_errCurrentError;
        private string m_sAxes;
        double m_dFreq;
        int m_iSteps;

        // Status of the stage
        private bool m_bIsInitialized;

        // We need a ComPort...
        private CommPort m_prtComm;

        #endregion

        #region Properties.

        string IPiezoStage.CurrentError
        {
            get 
            {
                return this.m_errCurrentError.ToString();
            }
        }

        double IPiezoStage.XPosition
        {
            get 
            {
                return this.m_dXPosCurrent;
            }
        }

        double IPiezoStage.YPosition
        {
            get
            {
                return this.m_dXPosCurrent;
            }
        }

        double IPiezoStage.ZPosition
        {
            get
            {
                return -1.0;
            }
        }

        int IPiezoStage.SamplesWritten
        {
            get { throw new NotImplementedException(); }
        }

        bool IPiezoStage.IsInitialized
        {
            get 
            {
                return this.m_bIsInitialized;
            }
        }

        bool IPiezoStage.IsMoving
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Events.

        public event EventHandler PositionChanged;

        public event EventHandler ErrorOccurred;

        public event EventHandler EngagedChanged;

        #endregion

        #region Singleton Pattern.

        // This class operates according to a singleton pattern. Having multiple PIAnalogStage could be dangerous because the hardware
        // could be left in an unknown state.
        private static volatile YanusIV m_instance;
        private static object m_syncRoot = new object();

        public static YanusIV Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new YanusIV();
                        }
                    }
                }

                return m_instance;
            }
        }
        #endregion

        #region Methods.

        // The constructor obviously needs to be private to prevent normal instantiation.
        /// <summary>
        /// Constructor. Private because it is part of a Singleton pattern.
        /// </summary>
        private YanusIV()
        {
            // The YanusIV object should be instantiated in an uninitialized state.
            this.m_bIsInitialized = false;
        }

        public void Initialize()
        {
            this.m_prtComm = CommPort.Instance;
            CommSettings.Read();

            this.m_prtComm.DataReceived += OnDataReceived;
            this.m_prtComm.StatusChanged += OnStatusChanged;

            _logger.Debug("Trying to open YanusIV COM");
            this.m_prtComm.Open();

            if (this.m_prtComm.IsOpen)
            {
                this.m_prtComm.Send("R");
            }
            else
            {
                this.m_errCurrentError = Error.SCAN_CMD_UNKONWN_COMMAND;
            }

            Thread.Sleep(1000);

            if (this.m_errCurrentError == Error.SCAN_CMD_NO_ERROR)
            {
                _logger.Debug("Engage seems to have worked");
                _logger.Debug(this.m_errCurrentError.ToString());
                this.m_bIsInitialized = true;
            }
            else
            {
                this.m_prtComm.DataReceived -= OnDataReceived;
                this.m_prtComm.StatusChanged -= OnStatusChanged;
            }
        }

        private void OnStatusChanged(string param)
        {
            _logger.Info("YanusIV Status says: " + param);
        }

        private void OnDataReceived(string param)
        {
            _logger.Debug("YanusIV Response says: " + param);
            this.ParseResponse(param);
        }

        private long NmtoAngle(double _dVal)
        {
            long _lAngleVal = 0;

            long _lRangenm = 220000;

            double _dRangeAngle = 12.5;

            double _dDegpernm = _dRangeAngle / _lRangenm;

            double _dDegperint16 = 15.0 / Int16.MaxValue;

            double _dAngleInt = Math.Round((_dVal * _dDegpernm) / _dDegperint16, 2);

            _lAngleVal = Convert.ToInt64(_dAngleInt * 1048576);

            return _lAngleVal;
        }

        private void SendIfAvailable(string __sCmd)
        {
            if (this.m_prtComm != null && this.m_prtComm.IsOpen)
            {
                _logger.Debug(__sCmd);
                this.m_prtComm.Send(__sCmd);
            }
            else
            {
                _logger.Error("Check Comm!");
            }
        }

        // This probably needs improving...
        /// <summary>
        ///
        /// </summary>
        /// <param name="__sResponse"></param>
        private void ParseResponse(string __sResponse)
        {
            // YanusIV echoes the command it received. Local echo and actual error code might arrive at the same time,
            // We have to rely on the fact that all YanusIV communication is terminated by \r, hence, we need to split the responses at \r,,,
            string[] _sSplitResponse = __sResponse.Split('\r');
            
            // All possible errors are in the Error enum.
            // We will get them and check the incoming data against all of them.
            var _vErros = EnumUtil.GetValues<Error>();

            // Run through the response and check against all errors.
            foreach (string s in _sSplitResponse)
            {
                foreach (Error e in _vErros)
                {
                    // If our response equals an error we will throw an error event!
                    if (((int)e).ToString() == s)
                    {
                        this.m_errCurrentError = ((Error)e);

                        // Throw an ErrorOccurred event to inform the user.
                        if (ErrorOccurred != null && (Error)e != Error.SCAN_CMD_NO_ERROR)
                        {
                            _logger.Debug("Throwing Error!");
                            ErrorOccurred(this, new EventArgs());
                        }
                    }
                }
            }
        }

        public void Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }

        public void Release()
        {
            if (this.m_prtComm.IsOpen)
            {
                this.m_prtComm.Close();
                this.m_bIsInitialized = false;
            }
            else
            {
                this.m_bIsInitialized = false;
            }
        }

        public void Home()
        {
            this.MoveAbs(0.0, 0.0, 0.0);
        }

        private string QuickValueCmd(DSCChannel __dscChan, long __lVal)
        {
            string _sCmd = string.Empty;

            _sCmd += ((char)DSPCommand.DSP_CMD_SET_VALUE).ToString() + " " + ((int)__dscChan).ToString() + "," + __lVal.ToString();

            return _sCmd;
        }

        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            // Moving axis...
            string _sCmdX = QuickValueCmd(DSCChannel.X, NmtoAngle(__dXPosNm));
            string _sCmdY = QuickValueCmd(DSCChannel.Y, NmtoAngle(__dYPosNm));

            // Debug
            _logger.Debug("YIV send: " + _sCmdX);
            _logger.Debug("YIV send: " + _sCmdY);

            //  Send it!
            this.SendIfAvailable(_sCmdX);
            this.SendIfAvailable(_sCmdY);
        }

        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        public void Scan(ScanModes.Scanmode __scmScanMode, bool __bResend)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            string _sCmd = ((char)DSPCommand.DSP_CMD_DO_NOTHING).ToString();
        }

        #endregion

        //Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.

        /// <summary>
        /// An enumeration of all error codes that can be returned by the YanusIV DSC.
        /// </summary>
        public enum Error
        {
            // Nothing wrong here!
            SCAN_CMD_NO_ERROR = 0,
            // Calculation took too long for 10 us frame
            SCAN_CMD_RUN_TIMEOUT = 1,
            // Run aborted by sending character on RS232
            SCAN_CMD_RUN_ABORTED = 2,
            // Trying to run an empty command list
            SCAN_CMD_LIST_EMPTY = 3,
            // Trying to run a command list with unclosed loops.
            SCAN_CMD_LIST_NOT_CLOSED = 4,
            // Tried to add command to a command-list that has // already maximum length.
            SCAN_CMD_LIST_OVERFLOW = 10,
            // Cycle time of newly added command is impossible.
            SCAN_CMD_LIST_DISORDER = 11,
            // Command is specified with non-existing channel number.
            SCAN_CMD_INVALID_CHANNEL = 12,
            // Too many nested loops.
            SCAN_CMD_LOOP_OVERFLOW = 13,
            // Invalid number of loop, iterations (<0)
            SCAN_CMD_INVALID_ITERATIONS = 14,
            // Trying to close a loop, which is already closed.
            SCAN_CMD_LOOP_IS_CLOSED = 15,
            // Unknown command
            SCAN_CMD_UNKONWN_COMMAND = 16,
            // No debug buffer available for the selected channel
            SCAN_CMD_NO_BUFFER = 17,
            // Invalid syntax when adding a scan command:
            // not enough parameters or too many parameters.
            SCAN_CMD_INVALID_SYNTAX = 18
        }
    }
}
