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

        void IPiezoStage.Initialize()
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
                this.m_bIsInitialized = true;
            }
        }

        private void OnStatusChanged(string param)
        {
            _logger.Info("YanusIV says: " + param);
        }

        private void OnDataReceived(string param)
        {
            _logger.Debug("YanusIV says: " + param);
            this.ParseResponse(param);
        }

        // This probably needs improving...
        /// <summary>
        ///
        /// </summary>
        /// <param name="__sResponse"></param>
        private void ParseResponse(string __sResponse)
        {
            // All possible errors are in the Error enum.
            // We will get them and check the incoming data against all of them.
            var _vErros = EnumUtil.GetValues<Error>();

            foreach (var e in _vErros)
            {
                // If our response equals an error we will throw an error event!
                if (((Error)e).ToString() == __sResponse)
                {
                    this.m_errCurrentError = ((Error)e);

                    // Throw an ErrorOccurred event to inform the user.
                    if (ErrorOccurred != null || (Error)e != Error.SCAN_CMD_NO_ERROR)
                    {
                        ErrorOccurred(this, new EventArgs());
                    }
                }
            }
        }

        void IPiezoStage.Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Release()
        {
            if (this.m_prtComm.IsOpen)
            {
                this.m_prtComm.Close();
            }
        }

        void IPiezoStage.Home()
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Scan(ScanModes.Scanmode __scmScanMode, bool __bResend)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Stop()
        {
            throw new NotImplementedException();
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
