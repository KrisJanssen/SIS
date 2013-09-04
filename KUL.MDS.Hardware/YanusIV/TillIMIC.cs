using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using KUL.MDS.Library;

namespace KUL.MDS.Hardware
{
    public class TillIMIC : IPiezoStage
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Members.

        // Create variables to keep track of the currently set voltage to the Piezo stage.
        private double m_dXPosCurrent;
        private double m_dYPosCurrent;
        private double m_dZPosCurrent;
        private int m_iSamplesToStageCurrent;

        // Some properties of the stage.
        private IntPtr m_iptrControllerID;
        private string m_sIDN;
        private TillLSMDevice.LSM_Error m_errCurrentError;
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
        private static volatile TillIMIC m_instance;
        private static object m_syncRoot = new object();

        public static TillIMIC Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new TillIMIC();
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
        private TillIMIC()
        {
            // The YanusIV object should be instantiated in an uninitialized state.
            this.m_bIsInitialized = false;
        }

        /// <summary>
        /// True if the API command that was checked returns an error. An ErrorOcurred event will be raised so that
        /// the user can be informed of the controller error status.
        /// </summary>
        /// <param name="__iResult">Integer indicating the error status of an API command.</param>
        /// <returns>Boolean indicating the error status.</returns>
        private bool IsError(int __iResult)
        {
            // Always assume the worst...
            bool _bIsError = true;

            if (__iResult != 0)
            {
                // Int that will hold the error code.
                this.m_errCurrentError = (TillLSMDevice.LSM_Error) __iResult;

                // Throw an ErrorOccurred event to inform the user.
                if (ErrorOccurred != null)
                {
                    ErrorOccurred(this, new EventArgs());
                }
            }
            else
            {
                this.m_errCurrentError = TillLSMDevice.LSM_Error.None;
                _bIsError = false;
            }

            return _bIsError;
        }

        void IPiezoStage.Initialize()
        {
            // Create a pointer to store the device handle.
            this.m_iptrControllerID = new IntPtr();

            if (!IsError(TillLSMDevice.LSM_Open(ConfigurationManager.AppSettings["PortName"], ref this.m_iptrControllerID)))
            {
                _logger.Info("TillIMIC device handle created: " + this.m_iptrControllerID.ToString()); 
                this.m_bIsInitialized = true;
            }
            else
            {
                this.m_bIsInitialized = false;
            }
        }

        void IPiezoStage.Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }

        void IPiezoStage.Release()
        {
            if (this.m_bIsInitialized)
            {
                if (IsError(TillLSMDevice.LSM_Close(this.m_iptrControllerID)))
                {
                    _logger.Error("Error while releasing TillIMIC device: " + this.m_errCurrentError.ToString());
                }
                else
                {
                    this.m_bIsInitialized = false;
                    _logger.Info("TillIMIC device released!");
                }
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

    }
}
