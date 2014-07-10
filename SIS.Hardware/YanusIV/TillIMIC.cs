//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="TillIMIC.cs" company="Kris Janssen">
////   Copyright (c) 2014 Kris Janssen
//// </copyright>
//// <summary>
////   The till imic.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------
//namespace SIS.Hardware.YanusIV
//{
//    using System;

//    using log4net;

//    using SIS.Hardware.ComPort;
//    using SIS.ScanModes.Core;

//    /// <summary>
//    /// The till imic.
//    /// </summary>
//    public class TillIMIC : IPiezoStage
//    {
//        #region Static Fields

//        /// <summary>
//        /// The _logger.
//        /// </summary>
//        private static readonly ILog _logger =
//            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

//        /// <summary>
//        /// The m_instance.
//        /// </summary>
//        private static volatile TillIMIC m_instance;

//        /// <summary>
//        /// The m_sync root.
//        /// </summary>
//        private static object m_syncRoot = new object();

//        #endregion

//        #region Fields

//        /// <summary>
//        /// The m_b is initialized.
//        /// </summary>
//        private bool m_bIsInitialized;

//        /// <summary>
//        /// The m_d freq.
//        /// </summary>
//        private double m_dFreq;

//        // Create variables to keep track of the currently set voltage to the Piezo stage.
//        /// <summary>
//        /// The m_d x pos current.
//        /// </summary>
//        private double m_dXPosCurrent;

//        /// <summary>
//        /// The m_d y pos current.
//        /// </summary>
//        private double m_dYPosCurrent;

//        /// <summary>
//        /// The m_d z pos current.
//        /// </summary>
//        private double m_dZPosCurrent;

//        /// <summary>
//        /// The m_err current error.
//        /// </summary>
//        private TillLSMDevice.LSM_Error m_errCurrentError;

//        /// <summary>
//        /// The m_i samples to stage current.
//        /// </summary>
//        private int m_iSamplesToStageCurrent;

//        /// <summary>
//        /// The m_i steps.
//        /// </summary>
//        private int m_iSteps;

//        /// <summary>
//        /// The m_iptr controller id.
//        /// </summary>
//        private IntPtr m_iptrControllerID;

//        // Status of the stage

//        // We need a ComPort...
//        /// <summary>
//        /// The m_prt comm.
//        /// </summary>
//        private CommPort m_prtComm;

//        /// <summary>
//        /// The m_s axes.
//        /// </summary>
//        private string m_sAxes;

//        /// <summary>
//        /// The m_s idn.
//        /// </summary>
//        private string m_sIDN;

//        #endregion

//        #region Constructors and Destructors

//        /// <summary>
//        /// Prevents a default instance of the <see cref="TillIMIC"/> class from being created. 
//        /// Constructor. Private because it is part of a Singleton pattern.
//        /// </summary>
//        private TillIMIC()
//        {
//            // The YanusIV object should be instantiated in an uninitialized state.
//            this.m_bIsInitialized = false;
//        }

//        #endregion

//        #region Public Events

//        /// <summary>
//        /// The engaged changed.
//        /// </summary>
//        public event EventHandler EngagedChanged;

//        /// <summary>
//        /// The error occurred.
//        /// </summary>
//        public event EventHandler ErrorOccurred;

//        /// <summary>
//        /// The position changed.
//        /// </summary>
//        public event EventHandler PositionChanged;

//        #endregion

//        #region Public Properties

//        /// <summary>
//        /// Gets the instance.
//        /// </summary>
//        public static TillIMIC Instance
//        {
//            get
//            {
//                if (m_instance == null)
//                {
//                    lock (m_syncRoot)
//                    {
//                        if (m_instance == null)
//                        {
//                            m_instance = new TillIMIC();
//                        }
//                    }
//                }

//                return m_instance;
//            }
//        }

//        #endregion

//        #region Explicit Interface Properties

//        /// <summary>
//        /// Gets the current error.
//        /// </summary>
//        string IPiezoStage.CurrentError
//        {
//            get
//            {
//                return this.m_errCurrentError.ToString();
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether is initialized.
//        /// </summary>
//        bool IPiezoStage.IsInitialized
//        {
//            get
//            {
//                return this.m_bIsInitialized;
//            }
//        }

//        /// <summary>
//        /// Gets a value indicating whether is moving.
//        /// </summary>
//        /// <exception cref="NotImplementedException">
//        /// </exception>
//        bool IPiezoStage.IsMoving
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }

//        /// <summary>
//        /// Gets the samples written.
//        /// </summary>
//        /// <exception cref="NotImplementedException">
//        /// </exception>
//        int IPiezoStage.SamplesWritten
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }

//        /// <summary>
//        /// Gets the x position.
//        /// </summary>
//        double IPiezoStage.XPosition
//        {
//            get
//            {
//                return this.m_dXPosCurrent;
//            }
//        }

//        /// <summary>
//        /// Gets the y position.
//        /// </summary>
//        double IPiezoStage.YPosition
//        {
//            get
//            {
//                return this.m_dXPosCurrent;
//            }
//        }

//        /// <summary>
//        /// Gets the z position.
//        /// </summary>
//        double IPiezoStage.ZPosition
//        {
//            get
//            {
//                return -1.0;
//            }
//        }

//        #endregion

//        #region Explicit Interface Methods

//        /// <summary>
//        /// The configure.
//        /// </summary>
//        /// <param name="__dCycleTimeMilisec">
//        /// The __d cycle time milisec.
//        /// </param>
//        /// <param name="__iSteps">
//        /// The __i steps.
//        /// </param>
//        /// <exception cref="NotImplementedException">
//        /// </exception>
//        void IPiezoStage.Configure(double __dCycleTimeMilisec, int __iSteps)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// The home.
//        /// </summary>
//        void IPiezoStage.Home()
//        {
//            TillLSMDevice.LSM_Coordinate _cTarget = new TillLSMDevice.LSM_Coordinate(0.0, 0.0);

//            if (this.m_bIsInitialized)
//            {
//                if (this.IsError(TillLSMDevice.LSM_GetRestPosition(this.m_iptrControllerID, ref _cTarget)))
//                {
//                    _logger.Error("Error getting resting position");
//                }
//                else
//                {
//                    if (this.IsError(TillLSMDevice.LSM_SetGalvoRawPosition(this.m_iptrControllerID, _cTarget)))
//                    {
//                        _logger.Error("Error setting position in raw coordinates");
//                    }
//                    else
//                    {
//                        _logger.Info("Galvo home! @ pos: " + _cTarget.X.ToString() + "," + _cTarget.Y.ToString());
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// The initialize.
//        /// </summary>
//        void IPiezoStage.Initialize()
//        {
//            // Create a pointer to store the device handle.
//            this.m_iptrControllerID = new IntPtr();
//            int i = 1;

//            // if (!IsError(TillLSMDevice.LSM_Open(ConfigurationManager.AppSettings["PortName"], ref this.m_iptrControllerID)))
//            if (!this.IsError(TillLSMDevice.LSM_Open("COM1", ref this.m_iptrControllerID)))
//            {
//                _logger.Info("TillIMIC device handle created: " + this.m_iptrControllerID.ToString());
//                this.m_bIsInitialized = true;
//                double maxval = Convert.ToDouble(34359738368);
//                if (
//                    !this.IsError(
//                        TillLSMDevice.LSM_AddCalibrationPoint(
//                            this.m_iptrControllerID, 
//                            new TillLSMDevice.LSM_Coordinate(0.0, 0.0), 
//                            new TillLSMDevice.LSM_Coordinate(maxval, maxval))))
//                {
//                    _logger.Info("Added origin 0,0");
//                }

//                if (
//                    !this.IsError(
//                        TillLSMDevice.LSM_AddCalibrationPoint(
//                            this.m_iptrControllerID, 
//                            new TillLSMDevice.LSM_Coordinate(100.0, 0.0), 
//                            new TillLSMDevice.LSM_Coordinate(-maxval, maxval))))
//                {
//                    _logger.Info("Added origin 100,0");
//                }

//                if (
//                    !this.IsError(
//                        TillLSMDevice.LSM_AddCalibrationPoint(
//                            this.m_iptrControllerID, 
//                            new TillLSMDevice.LSM_Coordinate(0.0, 100.0), 
//                            new TillLSMDevice.LSM_Coordinate(maxval, -maxval))))
//                {
//                    _logger.Info("Added origin 0,100");
//                }

//                _logger.Debug(this.IsCalibrated().ToString());
//            }
//            else
//            {
//                this.m_bIsInitialized = false;
//            }
//        }

//        /// <summary>
//        /// The move abs.
//        /// </summary>
//        /// <param name="__dXPosNm">
//        /// The __d x pos nm.
//        /// </param>
//        /// <param name="__dYPosNm">
//        /// The __d y pos nm.
//        /// </param>
//        /// <param name="__dZPosNm">
//        /// The __d z pos nm.
//        /// </param>
//        void IPiezoStage.MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
//        {
//            TillLSMDevice.LSM_Coordinate _cTarget = new TillLSMDevice.LSM_Coordinate(__dXPosNm, __dYPosNm);

//            if (this.m_bIsInitialized & this.IsCalibrated())
//            {
//                if (this.IsError(TillLSMDevice.LSM_SetPoint(this.m_iptrControllerID, _cTarget)))
//                {
//                    _logger.Error("Error setting position in pixelcoordinates");
//                }
//                else
//                {
//                    _logger.Info("Pixel position changed: " + _cTarget.X.ToString() + "," + _cTarget.Y.ToString());
//                }
//            }
//            else
//            {
//                if (this.IsError(TillLSMDevice.LSM_SetGalvoRawPosition(this.m_iptrControllerID, _cTarget)))
//                {
//                    _logger.Error("Error setting position in raw coordinates");
//                }
//                else
//                {
//                    _logger.Info("Raw position changed: " + _cTarget.X.ToString() + "," + _cTarget.Y.ToString());
//                }
//            }
//        }

//        /// <summary>
//        /// The move rel.
//        /// </summary>
//        /// <param name="__dXPosNm">
//        /// The __d x pos nm.
//        /// </param>
//        /// <param name="__dYPosNm">
//        /// The __d y pos nm.
//        /// </param>
//        /// <param name="__dZPosNm">
//        /// The __d z pos nm.
//        /// </param>
//        /// <exception cref="NotImplementedException">
//        /// </exception>
//        void IPiezoStage.MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// The release.
//        /// </summary>
//        void IPiezoStage.Release()
//        {
//            if (this.m_bIsInitialized)
//            {
//                if (!this.IsError(TillLSMDevice.LSM_ResetCalibration(this.m_iptrControllerID)))
//                {
//                    _logger.Info("TillIMIC device calibration reset!");
//                }

//                if (this.IsError(TillLSMDevice.LSM_Close(this.m_iptrControllerID)))
//                {
//                    _logger.Error("Error while releasing TillIMIC device: " + this.m_errCurrentError.ToString());
//                }
//                else
//                {
//                    // TillLSMDevice.UnloadModule();
//                    this.m_bIsInitialized = false;
//                    _logger.Info("TillIMIC device released!");
//                }
//            }
//        }

//        /// <summary>
//        /// The scan.
//        /// </summary>
//        /// <param name="__scmScanMode">
//        /// The __scm scan mode.
//        /// </param>
//        /// <param name="__bResend">
//        /// The __b resend.
//        /// </param>
//        /// <exception cref="NotImplementedException">
//        /// </exception>
//        void IPiezoStage.Scan(Scanmode __scmScanMode, bool __bResend)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Setup stage - pass few variables to stage prior to starting the scanning
//        /// </summary>
//        /// <param name="__iTypeOfScan">
//        /// The type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
//        /// </param>
//        /// <param name="__iFrameMarker">
//        /// The frame synchronization marker that the galvo rises upon a beginning of a frame
//        /// </param>
//        /// <param name="__iLineMarker">
//        /// The line synchronization marker that the galvo rises upon a beginning of a line
//        /// </param>
//        void IPiezoStage.Setup(int __iTypeOfScan, int __iFrameMarker, int __iLineMarker)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// The stop.
//        /// </summary>
//        void IPiezoStage.Stop()
//        {
//            if (this.m_bIsInitialized)
//            {
//                if (this.IsError(TillLSMDevice.LSM_Abort(this.m_iptrControllerID)))
//                {
//                    _logger.Error("Error trying to abort!");
//                }
//                else
//                {
//                    _logger.Info("Scan aborted!");
//                }
//            }
//            else
//            {
//                _logger.Info("Was not running anyway...!");
//            }
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// The is calibrated.
//        /// </summary>
//        /// <returns>
//        /// The <see cref="bool"/>.
//        /// </returns>
//        private bool IsCalibrated()
//        {
//            bool _bIsCalibrated = false;

//            if (this.IsError(TillLSMDevice.LSM_IsCalibrated(this.m_iptrControllerID, ref _bIsCalibrated)))
//            {
//                _logger.Error("Error checking for TillIMIC calibration!");
//                _bIsCalibrated = false;
//            }

//            _logger.Debug("Calibration? : " + _bIsCalibrated.ToString());

//            return _bIsCalibrated;
//        }

//        /// <summary>
//        /// True if the API command that was checked returns an error. An ErrorOcurred event will be raised so that
//        /// the user can be informed of the controller error status.
//        /// </summary>
//        /// <param name="__iResult">
//        /// Integer indicating the error status of an API command.
//        /// </param>
//        /// <returns>
//        /// Boolean indicating the error status.
//        /// </returns>
//        private bool IsError(int __iResult)
//        {
//            // Always assume the worst...
//            bool _bIsError = true;

//            // Int that will hold the error code.
//            this.m_errCurrentError = (TillLSMDevice.LSM_Error)__iResult;

//            if (__iResult != 0)
//            {
//                // This is a hack... for some reason the controller insists on returning 1 (Unknown Error) instead of 0.
//                if (__iResult == 1)
//                {
//                    this.m_errCurrentError = TillLSMDevice.LSM_Error.None;
//                    _logger.Debug("TillIMIC returned 1!");
//                    _bIsError = false;
//                }
//                else
//                {
//                    // Throw an ErrorOccurred event to inform the user.
//                    if (this.ErrorOccurred != null)
//                    {
//                        this.ErrorOccurred(this, new EventArgs());
//                    }
//                }
//            }
//            else
//            {
//                this.m_errCurrentError = TillLSMDevice.LSM_Error.None;
//                _bIsError = false;
//            }

//            return _bIsError;
//        }

//        #endregion
//    }
//}