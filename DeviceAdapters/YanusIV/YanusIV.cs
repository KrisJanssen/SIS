// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YanusIV.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Class that represents YanusIV galvo scanner
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    using log4net;

    using SIS.Hardware.ComPort;
    using SIS.Library;
    using SIS.ScanModes.Core;

    /// <summary>
    /// Class that represents YanusIV galvo scanner
    /// </summary>
    public class YanusIV : IPiezoStage
    {
        /* 
         * The galvo system supports a full mechanical angle range of +/- 15 degrees or +/- 0.261799388 rad.
         * This corresponds to an optical range (the one important to us) of +/- 30 degrees or +/- 0.523598776 rad.
         * This optical range is NOT EXACT. It is subject to proper calibration of the specific device.
         * This range is digitized in 16 bits (signed), i.e. -32768 to +32767 or 65536 discrete values.
         * The angle resolution is thus: 2 * 0.523598776 / 65536 = 1.597896655 E-05 rad/step. Again, this is NOT EXACT and subject to proper calibration.
         * Multiplication of +32767L by 1048576L shifts this 16 bit number left to be 36 bit upon conversion to binary before sending to controller.
         * 
         * To fully define the system we need three values:
         *    1) The absolute max angle (rad)
         *    2) The resolution of the controller, i.e. rad/int
         *    3) An angle range limiter for safety
         *    
         * We will define these now.
         * Controller specific numbers CAN NOT be anywhere else in the code but here! This could lead to errors.
        */
        #region Constants

        /// <summary>
        /// The galvo controller cycle time.
        /// </summary>
        private const double CYCLE_TIME = 10.0;

        /// <summary>
        /// Cycle time of the galvo controller in microseconds.
        /// </summary>
        private const double CYCLE_TIME_us = 10.0;

        /// <summary>
        /// Use this fractional parameter to limit the maximum settable galvo angle.
        /// </summary>
        private const double GALVO_ANGLE_LIMIT_PCT = 0.8;

        /// <summary>
        /// The galvo maximum optical angle. This value needs to be determined exactly using a calibration routine.
        /// </summary>
        private const double GALVO_MAX_ANGLE_ABS_RAD = (Math.PI * 30) / 180;

        /// <summary>
        /// The hard maximum on the int values that can be sent to the galvo controller.
        /// </summary>
        private const long GALVO_MAX_INT = 1048576L * 32767L;

        /// <summary>
        /// The hard minimum of the int values that can be sent to the galvo controller.
        /// </summary>
        private const long GALVO_MIN_INT = 1048576L * -32768L;

        /// <summary>
        /// The amount of radian displacement per integral controller value.
        /// </summary>
        private const double GALVO_RAD_PER_INT = 2 * GALVO_MAX_ANGLE_ABS_RAD / 65536.0;

        /// <summary>
        /// The number of times we should retry when DSC programming fails.
        /// </summary>
        private const int MAX_DSP_RETRY_COUNT = 10;

        /// <summary>
        /// The magic value used to shift 16 bit numbers by 20 bits for the 36 bit controller.
        /// This value can be cast into other types when needed.
        /// </summary>
        private const long SHIFT_BY_20_BITS = 1048576L;

        #endregion

        #region Static Fields

        /// <summary>
        /// The Logger.
        /// </summary>
        private static readonly ILog Logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        // Create variables to keep track of the currently set voltage to the Piezo stage.
        #region Fields

        /// <summary>
        /// The m_d magnification objective.
        /// </summary>
        private readonly double m_dMagnificationObjective; // the magnification of the objective

        /// <summary>
        /// The m_d scan lens focal length.
        /// </summary>
        private readonly double m_dScanLensFocalLength; // the focal length of the scan lens in [mm]

        /// <summary>
        /// The m_b is initialized.
        /// </summary>
        private bool m_bIsInitialized = false;

        /// <summary>
        /// The m_d freq.
        /// </summary>
        private double m_dFreq;

        /// <summary>
        /// The m_d x pos current.
        /// </summary>
        private double m_dXPosCurrent;

        /// <summary>
        /// The m_d y pos current.
        /// </summary>
        private double m_dYPosCurrent;

        /// <summary>
        /// The m_d z pos current.
        /// </summary>
        private double m_dZPosCurrent;

        /// <summary>
        /// The m_err current error.
        /// </summary>
        private DspError m_errCurrentError;

        /// <summary>
        /// The m_i controller id.
        /// </summary>
        private int m_iControllerID;

        // the line synchronization marker that the galvo rises upon a beginning of a line

        /// <summary>
        /// The m_i samples to stage current.
        /// </summary>
        private int m_iSamplesToStageCurrent;

        /// <summary>
        /// The m_i steps.
        /// </summary>
        private int m_iSteps;

        /// <summary>
        /// The m_i type of scan.
        /// </summary>
        private int m_iTypeOfScan;

        // set the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)

        /// <summary>
        /// The m_l cmd list.
        /// </summary>
        private List<DscCommand> m_lCmdList = new List<DscCommand>();

        // Status of the stage

        // We need a ComPort...
        /// <summary>
        /// The m_prt comm.
        /// </summary>
        private CommPort m_prtComm;

        /// <summary>
        /// The m_s axes.
        /// </summary>
        private string m_sAxes;

        /// <summary>
        /// The m_s idn.
        /// </summary>
        private string m_sIDN;

        // Place to store partial output...
        /// <summary>
        /// The m_s partial response.
        /// </summary>
        private string m_sPartialResponse = null;

        /// <summary>
        /// The m_s serial port name.
        /// </summary>
        private string m_sSerialPortName; // the name of the serial port where the galvo is connected to

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="YanusIV"/> class. 
        /// A hardware object Constructor that creates YanusIV galvo scanner object.
        /// </summary>
        /// <param name="serialPortName">
        /// The serial port name to connect to YanusIV
        /// </param>
        /// <param name="magnificationObjective">
        /// The magnification of the objective
        /// </param>
        /// <param name="scanLensFocalLength">
        /// The focal length of the scan lens in [mm]
        /// </param>
        public YanusIV(string serialPortName, double magnificationObjective, double scanLensFocalLength)
        {
            // Check if we are already initialized - only set up values if we are not initialized
            if (!this.m_bIsInitialized)
            {
                this.m_sSerialPortName = serialPortName;
                this.m_dMagnificationObjective = magnificationObjective;
                this.m_dScanLensFocalLength = scanLensFocalLength;
            }
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The engaged changed.
        /// </summary>
        public event EventHandler EngagedChanged; // Event thrown whenever the stage is switched ON, or OFF.

        /// <summary>
        /// The error occurred.
        /// </summary>
        public event EventHandler ErrorOccurred; // Event thrown whenever the hardware generated an error.

        /// <summary>
        /// The position changed.
        /// </summary>
        public event EventHandler PositionChanged; // Event thrown whenever the stage changed position.

        #endregion

        // List that contains the DSC protocol commands - e.g. it may contain commands that define a rectangular area to scan
        #region Public Properties

        /// <summary>
        /// Gets the magnification objective.
        /// </summary>
        public double MagnificationObjective
        {
            // the magnification of the objective
            get
            {
                return this.m_dMagnificationObjective;
            }
        }

        /// <summary>
        /// Gets the range angle degrees.
        /// </summary>
        public double RangeAngleDegrees
        {
            // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            get
            {
                return 0d; // this.m_dRangeAngleDegrees;
            }
        }

        /// <summary>
        /// Gets the range angle int.
        /// </summary>
        public double RangeAngleInt
        {
            // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
            get
            {
                return 0d; // this.m_dRangeAngleInt;
            }
        }

        /// <summary>
        /// Gets the scan lens focal length.
        /// </summary>
        public double ScanLensFocalLength
        {
            // the focal length of the scan lens in [mm]
            get
            {
                return this.m_dScanLensFocalLength;
            }
        }

        /// <summary>
        /// Gets the serial port name.
        /// </summary>
        public string SerialPortName
        {
            // the name of the serial port where the galvo is connected to
            get
            {
                return this.m_sSerialPortName;
            }
        }

        #endregion

        #region Explicit Interface Properties

        /// <summary>
        /// Gets the current error.
        /// </summary>
        string IPiezoStage.CurrentError
        {
            get
            {
                return this.m_errCurrentError.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether is initialized.
        /// </summary>
        bool IPiezoStage.IsInitialized
        {
            get
            {
                return this.m_bIsInitialized;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is moving.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        bool IPiezoStage.IsMoving
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the samples written.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        int IPiezoStage.SamplesWritten
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the x position.
        /// </summary>
        double IPiezoStage.XPosition
        {
            get
            {
                return this.m_dXPosCurrent;
            }
        }

        /// <summary>
        /// Gets the y position.
        /// </summary>
        double IPiezoStage.YPosition
        {
            get
            {
                return this.m_dXPosCurrent;
            }
        }

        /// <summary>
        /// Gets the z position.
        /// </summary>
        double IPiezoStage.ZPosition
        {
            get
            {
                return -1.0;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Configure the stage to perform scans at a set rate.
        /// </summary>
        /// <param name="__dCycleTimeMilisec">
        /// The amount of time in ms between subsequent position updates.
        /// </param>
        /// <param name="__iSteps">
        /// The amount of pixels in a scan.
        /// </param>
        public void Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Move galvo axes to home position.
        /// </summary>
        public void Home()
        {
            this.MoveAbs(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// Initialize YanusIV - try to open serial COM port to YanusiV in order to control it
        /// </summary>
        public void Initialize()
        {
            // Initialize only if it is in an initialized state
            if (!this.m_bIsInitialized)
            {
                this.m_prtComm = CommPort.Instance; // get an instance to the COM (serial) port
                CommSettings.SetupSettings(this.m_sSerialPortName); // set COM port settings

                // Register for COM port events which YanusIV must subscribe to
                this.m_prtComm.DataReceived += this.OnDataReceived;
                this.m_prtComm.StatusChanged += this.OnStatusChanged;

                // Try to open YanusIV COM (serial) port with the respective COM settings
                Logger.Debug("YanusIV: starting initialization...");
                this.m_prtComm.Open();

                // Check if COM port is active and then if YanusIV is ON
                this.m_errCurrentError = DspError.SCAN_CMD_COM_GALVO_IS_OFF;

                // as long as we do not have response from YanusIV, we assume it is OFF
                if (this.m_prtComm.IsOpen)
                {
                    // Probe if YanusIV galvo scanner is ON - we send a command to get an info string and if we cannot get in time we assume the galvo is OFF
                    this.m_prtComm.Send(((char)DspCommand.DSP_CMD_RETURN_INFO).ToString());

                    // on initialize get info from YanusIV by the DSP_CMD_RETURN_INFO command
                }
                else
                {
                    this.m_errCurrentError = DspError.SCAN_CMD_COM_PORT_CLOSED; // case if port is closed or not active
                }

                // Wait some time to let the galvo respond to the probe command we have just sent to it (make sure the sleep time here is at least 2x bigger than the COM port read time out)
                Thread.Sleep(4 * this.m_prtComm.ReadTimeOut);

                // Check for errors and decide if the initialization was successful
                if (this.m_errCurrentError == DspError.SCAN_CMD_NO_ERROR
                    || this.m_errCurrentError == DspError.SCAN_CMD_RUN_ABORTED)
                {
                    Logger.Debug("YanusIV: engage seems to have worked!");
                    this.m_bIsInitialized = true;
                }
                else
                {
                    Logger.Debug("YanusIV: something went wrong initializing YanusIV!");
                    Logger.Debug("YanusIV: error is --> " + this.m_errCurrentError.ToString());

                    if (this.m_prtComm.IsOpen)
                    {
                        this.m_prtComm.Close();
                    }

                    this.m_prtComm.DataReceived -= this.OnDataReceived;
                    this.m_prtComm.StatusChanged -= this.OnStatusChanged;

                    // Set the initialization to not successful
                    this.m_bIsInitialized = false;
                    this.m_errCurrentError = DspError.SCAN_CMD_NO_ERROR; // reset error state to no error
                }
            }
        }

        /// <summary>
        /// Move galvo scan axes absolute.
        /// </summary>
        /// <param name="__dXPosNm">
        /// The new X position in [nm].
        /// </param>
        /// <param name="__dYPosNm">
        /// The new Y position in [nm].
        /// </param>
        /// <param name="__dZPosNm">
        /// The new Z position in [nm].
        /// </param>
        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            // Moving axis...
            string _sCmdX = this.QuickValueCmd(DscChannel.X, this.ConvertMeterToAngle(__dXPosNm));
            string _sCmdY = this.QuickValueCmd(DscChannel.Y, this.ConvertMeterToAngle(__dYPosNm));

            // Debug
            Logger.Debug("YanusIV: trying to send MoveAbs X --> " + _sCmdX);
            Logger.Debug("YanusIV: trying to send MoveAbs Y --> " + _sCmdY);

            // Try to send the DSP scan command to YanusIV!
            this.SendIfAvailable(_sCmdX);
            this.SendIfAvailable(_sCmdY);

            // Keep track of the current position
            this.m_dXPosCurrent = __dXPosNm;
            this.m_dYPosCurrent = __dYPosNm;

            // Raise an event PositionChanged
            if (this.PositionChanged != null)
            {
                this.PositionChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Move galvo axis relative.
        /// </summary>
        /// <param name="__dXPosNm">
        /// The relative change of X position in [nm].
        /// </param>
        /// <param name="__dYPosNm">
        /// The relative change of Y position in [nm].
        /// </param>
        /// <param name="__dZPosNm">
        /// The relative change of Z position in [nm].
        /// </param>
        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Release the occupied resources and return the galvo axes to home position
        /// </summary>
        public void Release()
        {
            // Clear the current DSP protocol representation stored in YanusIV and in this class
            this.Clear();

            if (this.m_bIsInitialized)
            {
                if (this.m_prtComm.IsOpen)
                {
                    this.Home(); // move galvo axes to home position (X=0,Y=0,Z=0)

                    this.m_prtComm.Close(); // close COM port
                    this.m_bIsInitialized = false;
                }
                else
                {
                    this.m_bIsInitialized = false;
                }

                // Unsubscribe from the Comm events
                this.m_prtComm.DataReceived -= this.OnDataReceived;
                this.m_prtComm.StatusChanged -= this.OnStatusChanged;

                // Reset error state to default, i.e. no error
                this.m_errCurrentError = DspError.SCAN_CMD_NO_ERROR;

                // Update engage event state
                if (this.EngagedChanged != null)
                {
                    this.EngagedChanged(this, new EventArgs());
                }

                Logger.Info("YanusIV: now galvo is OFF!");
            }
            else
            {
                Logger.Info("YanusIV: doing nothing, galvo is already OFF!");
            }
        }

        /// <summary>
        /// Start scanning - execute the DSP protocol loaded in YanusIV.
        /// </summary>
        /// <param name="__scmScanMode">
        /// The type of scan.
        /// </param>
        /// <param name="__bResend">
        /// Load/Send or not a DSP protocol.
        /// </param>
        public void Scan(Scanmode __scmScanMode, bool __bResend)
        {
            // Info
            Logger.Info("Starting Scan ...");

            // Check if we create and execute a new DSP protocol
            if (__bResend)
            {
                // if true then creates and executes a new DSP protocol
                // Clear the existing DSP protocol
                this.Clear();

                // Add a rectangle to the DSP protocol
                int _iTypeOfScan = this.m_iTypeOfScan;

                // the type of scan mode (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
                long _int64ScanCommandLoopCount = 1L; // number of times to repeat the rectangular scan

                this.AddRectangle(
                    this.m_lCmdList,
                    new CoordinateD{X = __scmScanMode.InitialX, Y = __scmScanMode.InitialY},
                    new SizeD{Height = __scmScanMode.YScanSizeNm, Width = __scmScanMode.XScanSizeNm},
                    0d,
                    __scmScanMode.ImageWidthPx,
                    __scmScanMode.ImageHeightPx,
                    __scmScanMode.TimePPixel / 1000,
                    1);

                // Load/Send the new/updated DSP protocol to YanusIV
                this.Load();

                // Execute the currently loaded DSP protocol - tells YanusIV to start the actual scanning
                this.Start();
            }
            else
            {
                // Check if we have at least one DSP protocol command line to execute, if not create a rectangular DSP protocol
                if (this.m_lCmdList.Count < 1)
                {
                    // Clear the existing DSP protocol
                    this.Clear();

                    // Add a rectangle to the DSP protocol
                    int _iTypeOfScan = this.m_iTypeOfScan;

                    // the type of scan mode (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
                    long _int64ScanCommandLoopCount = 1L; // number of times to repeat the rectangular scan

                    // this.AddRectangle(
                    // this, 
                    // _iTypeOfScan, 
                    // _int64ScanCommandLoopCount, 
                    // __scmScanMode.InitialX, 
                    // __scmScanMode.InitialY, 
                    // __scmScanMode.ImageWidthPx, 
                    // __scmScanMode.ImageHeightPx, 
                    // __scmScanMode.XScanSizeNm, 
                    // __scmScanMode.YScanSizeNm, 
                    // __scmScanMode.TimePPixel);

                    // Load/Send the new/updated DSP protocol to YanusIV
                    this.Load();
                }

                // Execute the currently loaded DSP protocol - tells YanusIV to start the actual scanning
                this.Start();
            }
        }

        /// <summary>
        /// Setup YanusIV - pass few variables to YanusIV prior to starting the scanning
        /// </summary>
        /// <param name="__iTypeOfScan">
        /// The type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
        /// </param>
        /// <param name="__iFrameMarker">
        /// The frame synchronization marker that the galvo rises upon a beginning of a frame
        /// </param>
        /// <param name="__iLineMarker">
        /// The line synchronization marker that the galvo rises upon a beginning of a line
        /// </param>
        public void Setup(int __iTypeOfScan, int __iFrameMarker, int __iLineMarker)
        {
            this.m_iTypeOfScan = __iTypeOfScan;

            // this.m_iFrameMarker = __iFrameMarker;
            // this.m_iLineMarker = __iLineMarker;
        }

        /// <summary>
        /// Stop the current DSP scan protocol execution and return axes to home position.
        /// </summary>
        public void Stop()
        {
            // Assign the command that breaks the current protocol execution
            string _sCmd = ((char)DspCommand.DSP_CMD_DO_NOTHING).ToString();

            // the DSP command "do nothing" breaks/stops the current protocol execution

            // Info
            Logger.Info("YanusIV: scan finished/stopped!");

            // Send the Stop protocol execution command
            this.SendIfAvailable(_sCmd);

            Thread.Sleep(1000);

            // Move axes to home position
            this.Home();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The delta coordinate.
        /// </summary>
        /// <param name="firsCoordinate">
        /// The firs coordinate.
        /// </param>
        /// <param name="secondCoordinate">
        /// The second coordinate.
        /// </param>
        /// <returns>
        /// The calculated X and Y distance between both input <see cref="CoordinateD"/>s.
        /// </returns>
        private static SizeD DeltaCoordinate(CoordinateD firsCoordinate, CoordinateD secondCoordinate)
        {
            var deltaCoordinate = new SizeD
                                      {
                                          Width = secondCoordinate.X - firsCoordinate.X, 
                                          Height = secondCoordinate.Y - firsCoordinate.X
                                      };

            return deltaCoordinate;
        }

        /// <summary>
        /// Rotate a given coordinate around a center by a given angle.
        /// </summary>
        /// <param name="inCoordinate">
        /// The coordinate to be rotated.
        /// </param>
        /// <param name="centerCoordinate">
        /// The center coordinate.
        /// </param>
        /// <param name="angle">
        /// The angle or rotation.
        /// </param>
        /// <returns>
        /// The rotated <see cref="CoordinateD"/>.
        /// </returns>
        private static CoordinateD RotateCoordinate(
            CoordinateD inCoordinate, 
            CoordinateD centerCoordinate, 
            double angle)
        {
            var rotCoordinate = new CoordinateD
                                    {
                                        X =
                                            Math.Cos(angle) * (inCoordinate.X - centerCoordinate.X)
                                            - Math.Sin(angle) * (inCoordinate.Y - centerCoordinate.Y)
                                            + centerCoordinate.X, 
                                        Y =
                                            Math.Sin(angle) * (inCoordinate.X - centerCoordinate.X)
                                            + Math.Cos(angle) * (inCoordinate.Y - centerCoordinate.Y)
                                            + centerCoordinate.Y
                                    };

            return rotCoordinate;
        }

        /// <summary>
        /// Add/Create a rectangle in the format suitable to send as DSP scanning protocol that scans a rectangular area.
        /// </summary>
        /// <param name="protocol">
        /// The protocol list of the controller instance.
        /// </param>
        /// <param name="start">
        /// The physical coordinate of the frame origin.
        /// </param>
        /// <param name="frameSize">
        /// The physical width and height of the frame.
        /// </param>
        /// <param name="rotation">
        /// The desired scan rotation.
        /// </param>
        /// <param name="pixelsX">
        /// The number of pixels in X.
        /// </param>
        /// <param name="pixelsY">
        /// The number of pixels in Y.
        /// </param>
        /// <param name="timePerPixel">
        /// The time Per Pixel in seconds.
        /// </param>
        /// <param name="loops">
        /// The number of times the frame should be scanned.
        /// </param>
        private void AddRectangle(
            List<DscCommand> protocol, 
            CoordinateD start, 
            SizeD frameSize, 
            double rotation, 
            int pixelsX, 
            int pixelsY, 
            double timePerPixel, 
            int loops)
        {
            // Get the center of the rectangle.
            CoordinateD centerCoordinate = new CoordinateD(start.X + (frameSize.Width / 2), start.Y + (frameSize.Height / 2));
            CoordinateD topRight = new CoordinateD { X = start.X + frameSize.Width, Y = start.Y };
            CoordinateD bottomRight = new CoordinateD { X = start.X + frameSize.Width, Y = start.Y + frameSize.Height };

            // Perform a rotation if necessary.
            CoordinateD startRotatedD = RotateCoordinate(start, centerCoordinate, rotation);
            CoordinateD topRightRotatedD = RotateCoordinate(topRight, centerCoordinate, rotation);
            CoordinateD bottomRightRotatedD = RotateCoordinate(bottomRight, centerCoordinate, rotation);

            // Calculate X and Y deltas between the coordinates marking our frame. 
            SizeD lineDeltaD = DeltaCoordinate(topRightRotatedD, startRotatedD);
            SizeD frameDeltaD = DeltaCoordinate(bottomRightRotatedD, startRotatedD);

            CoordinateL startRotatedL = new CoordinateL
                                            {
                                                X = this.ConvertMeterToAngle(startRotatedD.X), 
                                                Y = this.ConvertMeterToAngle(startRotatedD.Y)
                                            };

            CoordinateL topRightRotatedL = new CoordinateL
                                               {
                                                   X = this.ConvertMeterToAngle(topRightRotatedD.X), 
                                                   Y = this.ConvertMeterToAngle(topRightRotatedD.Y)
                                               };

            CoordinateL bottomRightRotatedL = new CoordinateL
                                                  {
                                                      X = this.ConvertMeterToAngle(bottomRightRotatedD.X), 
                                                      Y = this.ConvertMeterToAngle(bottomRightRotatedD.Y)
                                                  };

            SizeL lineDeltaL = new SizeL
                                   {
                                       Height = this.ConvertMeterToAngle(lineDeltaD.Height), 
                                       Width = this.ConvertMeterToAngle(lineDeltaD.Width)
                                   };

            SizeL frameDeltaL = new SizeL
                                    {
                                        Height = this.ConvertMeterToAngle(frameDeltaD.Height), 
                                        Width = this.ConvertMeterToAngle(frameDeltaD.Width)
                                    };

            // CONSIDER THIS:
            // long _int64StartXAngleInt = this.ConvertMeterToAngle(__dInitialXnm) - _int64XScanAngleInt / 2L;

            // X offset of the scanning range in terms of an integer (angle) - note that the initial angle is divided by two because (X=0,Y=0) is the middle of the scan area
            // long _int64StartYAngleInt = this.ConvertMeterToAngle(__dInitialYnm) + _int64YScanAngleInt / 2L;            

            // Calculate the number of cycles per pixel (one cycle equals 10.0us)
            double cyclesPerPixel = (timePerPixel * 1E6) / CYCLE_TIME;

            // time per pixel in terms of YanusIV controller cycles

            // Increment/decrement and angles in terms of cycles
            ulong _ui64XAngleCycles = (UInt64)(pixelsX * cyclesPerPixel);

            // the image width in terms of cycles of the YanusIV controller
            ulong _ui64YAngleCycles = (UInt64)pixelsY;

            // the image height in terms of cycles of the YanusIV controller. Note that it differs than the image width in terms of cycles, because here we directly go to the new line.
            long _int64XAngleIntPerCycle = lineDeltaL.Width / (Int64)_ui64XAngleCycles;

            // the angle per cycle in order to cover the width of the image
            long _int64YAngleIntPerCycle = frameDeltaL.Height / (Int64)_ui64YAngleCycles;

            // The scan type is unidirectional scan
            // Start DSP loop 1
            this.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_loop_start, 0UL, DscChannel.None, loops));
                
                // add start DSP loop

            // Go to top initial position from where we start to scan the rectangle at cycle 0
            // move X to the left end of the rectangle
            this.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.X, startRotatedL.X));

            // move Y to the top end of the rectangle
            this.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.Y, startRotatedL.Y));

            // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DscChannel.X, _int64XAngleIntPerCycle));
                
                // set the increment along X so that first we scan along X

            // set the increment along Y to zero (first we scan a line along X)
            this.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DscChannel.Y, 0L));

            // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV).
            // Note that we need a frame marker to extract an image from the raw Time Harp data stream.
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.DO, (long)ScanMarkers.FrameMarker));

            // raise the frame marker
            this.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 1UL, DscChannel.DO, 0L));

            // set down the frame marker (it is enough to raise the marker for one cycle only)

            // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_loop_start, _ui64XAngleCycles, DscChannel.None, pixelsY));

            // add start DSP loop - the number of loops equals the number of lines to scan, i.e. the height of the image

            // Go to top initial X position from where we start to scan a new line
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles, DscChannel.X, startRotatedL.X));
                
                // move X to the left end of the rectangle

            // Set the Y axis decrement so that we go to the next line
            this.m_lCmdList.Add(
                new DscCommand(
                    ScanCommand.scan_cmd_set_increment_1, 
                    _ui64XAngleCycles, 
                    DscChannel.Y, 
                    -_int64YAngleIntPerCycle));

            // set the decrement along Y so that we go to the next line along Y (top down scanning)

            // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
            this.m_lCmdList.Add(
                new DscCommand(
                    ScanCommand.scan_cmd_set_value, 
                    _ui64XAngleCycles, 
                    DscChannel.DO, 
                    (long)ScanMarkers.LineMarker)); // raise the line marker
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DscChannel.DO, 0L));

            // set down the line marker (it is enough to raise the marker for one cycle only)

            // Set the Y axis decrement to zero so that we stay on the new line
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles + 1UL, DscChannel.Y, 0L));

            // End DSP loop 2
            this.m_lCmdList.Add(
                new DscCommand(ScanCommand.scan_cmd_loop_end, 2L * _ui64XAngleCycles, DscChannel.None, 0L));

            // add end DSP loop - note that we multiply by factor of 2 because the first line was already scanned before entering the second loop.

            // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
            this.m_lCmdList.Add(
                new DscCommand(
                    ScanCommand.scan_cmd_set_increment_1, 
                    ((UInt64)pixelsY) * _ui64XAngleCycles + _ui64XAngleCycles, 
                    DscChannel.X, 
                    0L));

            // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
            this.m_lCmdList.Add(
                new DscCommand(
                    ScanCommand.scan_cmd_set_increment_1, 
                    ((UInt64)pixelsY) * _ui64XAngleCycles + _ui64XAngleCycles, 
                    DscChannel.Y, 
                    0L));

            // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

            // End DSP loop 1
            this.m_lCmdList.Add(
                new DscCommand(
                    ScanCommand.scan_cmd_loop_end, 
                    ((UInt64)pixelsY) * _ui64XAngleCycles + _ui64XAngleCycles, 
                    DscChannel.None, 
                    0L)); // add end DSP loop
        }

        /// <summary>
        /// Clear the current DSP scan protocol.
        /// </summary>
        private void Clear()
        {
            // Clear the current DSP protocol representation stored in this class
            this.m_lCmdList.Clear();

            // Assign the command that clears the current DSP protocol in YanusIV
            string _sCmd = ((char)DspCommand.DSP_CMD_CLEAR_PROT).ToString(); // clears the currently loaded DSP protocol

            // Debug
            Logger.Debug("YanusIV: DSP protocol cleared!");

            // Send the Clear protocol command to YanusIV as well
            this.SendIfAvailable(_sCmd);
        }

        /// <summary>
        /// Convert physical coordinates in the image to angles supported by the galvo hardware.
        /// This method is also responsible for checking the validity of the requested angles.
        /// This is the single point through which all galvo programming MUST pass.
        /// </summary>
        /// <param name="value">
        /// The requested physical displacement in meters (SI).
        /// </param>
        /// <returns>
        /// A <see cref="long"/> value representing the corresponding angle.
        /// </returns>
        private long ConvertMeterToAngle(double value)
        {
            // The angle value, expressed as a 36 bit number but stored in long representation.
            long calculatedAngle = 0L;

            // 2^20 = 0b100000000000000000000, i.e. 21 bits.
            // Knowing that for example 0b1011 * 0b100 (3 bits) = 0b101100, multiplication by 2^n actually pads a number with (n-1) bits on the right.
            // Therefore, multiplying a 16 bit number with 2^20 = 1048576.0 effectively converts it into a 36 bit number.
            // We have to do this since the angle resolution of Yanus is 16 bit but the controller has a 36 bit bus system.
            const double ShiftBy20 = (double)SHIFT_BY_20_BITS;

            // For a simple lens, h = focal length * tan(angle of incidence), 
            // i.e. the hight of the image in the image plane relates to the angle of the incoming ray and focal length.
            // h is then further scaled by the magnification of the objective.
            // Remember also that (2 * Pi * deg) / 360 = radian.
            double angleRad = Math.Atan((value * this.m_dMagnificationObjective) / this.m_dScanLensFocalLength);

            /* Now that we have the desired angle, check if it is in range.
             * 
             * This first check is only a soft check to make sure the requested values are not out of range such that optical quality will diminish.
             * Responsibility for this check will ultimately be moved to the UI.
             * For the moment we can leave it here.
             * 
             * Note that the user is not informed of this check/any subsequent modification to the requested value.
             */
            if (GALVO_ANGLE_LIMIT_PCT * -GALVO_MAX_ANGLE_ABS_RAD > angleRad)
            {
                // Too large negative angle
                angleRad = -GALVO_MAX_ANGLE_ABS_RAD;
            }

            if (GALVO_ANGLE_LIMIT_PCT * GALVO_MAX_ANGLE_ABS_RAD < angleRad)
            {
                // Too large positive angle
                angleRad = GALVO_MAX_ANGLE_ABS_RAD;
            }

            // We now are sure that we have a safe angle so we convert it to a 36 bit value.        
            calculatedAngle = Convert.ToInt64(Math.Round(angleRad / GALVO_RAD_PER_INT, 2) * ShiftBy20);

            /* Just to be extra safe, check the hard limit.
             * 
             * Failure here is fatal.
             */
            if ((calculatedAngle < GALVO_MIN_INT) || (calculatedAngle > GALVO_MAX_INT))
            {
                // If this is true we cannot continue.
                throw new Exception("Calculated angle out of range!");
            }

            // All done, return the value.
            return calculatedAngle;
        }

        /// <summary>
        /// Load/Send a DSP scan protocol to YanusIV.
        /// </summary>
        private void Load()
        {
            // Disable parallel serial port reading (this a thread that reads the in and out data in background) while we are
            // trying to load the DSP protocol - this measure is necessary in order to treat a connection problems in the Serial Port
            // that causes weird behavior (wrong characters send). At the end of this method we enable parallel serial port reading again.
            this.m_prtComm.StopReading();
            bool _bDSPCommandLoaded = false;

            // true = DSP command loaded correctly; false = DSP command not loaded correctly
            int _intDSPFailedLoadsCount = 0; // counts how many times the load of the DSP failed

            // Debug
            Logger.Debug("YanusIV: trying to load the DSP protocol...");

            // Strings to represent the DSP protocol to send
            StringBuilder _sbDSPString = new StringBuilder();
            string _sDSPString = string.Empty;
            string _sLineEnding = this.m_prtComm.GetLineEnding();

            while (!_bDSPCommandLoaded && _intDSPFailedLoadsCount < MAX_DSP_RETRY_COUNT)
            {
                // Discard data that may still be in YanusIV buffer - we do not want to mix this data with the future YabusIV responses
                this.m_prtComm.DiscardInBufferData();

                // Format a DSCComand command as a string so that it is suitable to send to the YanusIV galvo scanner
                foreach (DscCommand _dscCmd in this.m_lCmdList)
                {
                    _sbDSPString.Clear();

                    _sbDSPString.Append((char)DspCommand.DSP_CMD_ADD_PROT_CMD);

                    // add protocol command - adds to the DSP protocol the commands that follows
                    _sbDSPString.Append((char)_dscCmd.ScanCmd); // the protocol command
                    _sbDSPString.Append(","); // the parameters list's separator symbol
                    _sbDSPString.Append(_dscCmd.Cycle); // the cycle it will be executed
                    _sbDSPString.Append(","); // the parameters list's separator symbol
                    _sbDSPString.Append((int)_dscCmd.Channel); // channel the command will act on
                    _sbDSPString.Append(","); // the parameters list's separator symbol
                    _sbDSPString.Append(_dscCmd.Value); // the value to be set on the given channel
                    _sbDSPString.Append(_sLineEnding); // terminate the line

                    _sDSPString = _sbDSPString.ToString();

                    // Send the DSP command string to the serial port
                    if (this.m_prtComm != null && this.m_prtComm.IsOpen)
                    {
                        Logger.Debug("YanusIV: send DSP command --> " + _sDSPString);
                        this.m_prtComm.Write(_sDSPString);
                    }
                    else
                    {
                        Logger.Error("YanusIV: check COM Port - port seems closed or not active!");
                    }

                    // Read back the answer of YanusIV and check for errors - if such we have to load the whole DSP protocol again
                    string _strDSPResponse = this.m_prtComm.Read(10);

                    // contains both the command itself and the numeric error code
                    string _strDSPCommand = _sDSPString + ((int)DspError.SCAN_CMD_NO_ERROR).ToString() + _sLineEnding;

                    // compose the string we expect YanusIV will return at a successful load           
                    if (_strDSPCommand != _strDSPResponse)
                    {
                        // if the response is not as expected we must try to reload the whole DSP protocol again
                        // Debug
                        Logger.Debug("YanusIV: DSP protocol failed to load! Trying again...");

                        _bDSPCommandLoaded = false;
                        _intDSPFailedLoadsCount++;

                        // Assign the command that clears the current DSP protocol in YanusIV
                        string _sCmd = ((char)DspCommand.DSP_CMD_CLEAR_PROT).ToString();

                        // clears the currently loaded DSP protocol

                        // Send the Clear protocol command to YanusIV as well
                        this.SendIfAvailable(_sCmd);
                        this.SendIfAvailable(_sCmd);

                        // sometimes we need to send a command second time - so to make sure we really cleared the DSP in YanusIV memory, we send a second time

                        // Sleep for a while
                        Thread.Sleep(100);

                        break; // causes to exit the foreach-loop and tries to load the whole DSP protocol again
                    }
                    else
                    {
                        _bDSPCommandLoaded = true;
                    }
                }
            }

            //// Convert to a string that represents the DSP protocol 
            // _sDSPString = _sbDSPString.ToString();

            //// Send the DSP protocol string
            // if (this.m_prtComm != null && this.m_prtComm.IsOpen)
            // {
            // _logger.Debug("YanusIV: send DSP protocol --> " + _sDSPString);
            // this.m_prtComm.Write(_sDSPString);
            // }
            // else
            // {
            // _logger.Error("YanusIV: check COM Port - port seems closed or not active!");
            // }

            // Discard data that may still be in YanusIV buffer - we do not want to mix this data with the future YabusIV responses
            this.m_prtComm.DiscardInBufferData();

            // Show info to the user if there was/were DSP protocol reloads
            if (_intDSPFailedLoadsCount > 0)
            {
                Logger.Info("YanusIV: DSP protocol reloaded " + (_intDSPFailedLoadsCount + 1).ToString() + "x!");

                // the number of reloads is the number of failed attempts + the successful one
            }

            // Enable parallel reading of the serial port again
            this.m_prtComm.StartReading();
        }

        /// <summary>
        /// Process the event DataReceived.
        /// </summary>
        /// <param name="param">
        /// A string that shows the received from YanusIV string data.
        /// </param>
        private void OnDataReceived(string param)
        {
            // if we detect a line terminator, add line to output
            int index;

            while (param.Length > 0 && ((index = param.IndexOf("\r")) != -1 || (index = param.IndexOf("\n")) != -1))
            {
                this.m_sPartialResponse += param.Substring(0, index);
                param = param.Remove(0, index + 1);
                Logger.Debug("YanusIV: response says --> " + this.m_sPartialResponse);

                if (this.m_sPartialResponse != null)
                {
                    this.ParseResponse(this.m_sPartialResponse);
                }
            }

            // if we have data remaining, add a partial line
            if (param.Length > 0)
            {
                this.m_sPartialResponse += param;
            }
        }

        /// <summary>
        /// Process the event StatusChange.
        /// </summary>
        /// <param name="param">
        /// A string that shows the current YanusIV status.
        /// </param>
        private void OnStatusChanged(string param)
        {
            Logger.Info("YanusIV: status says --> " + param);
        }

        /// <summary>
        /// Process YanusIV response - if error code returned throw an error event.
        /// </summary>
        /// <param name="__sResponse">
        /// A string - the response from the serial COM port.
        /// </param>
        private void ParseResponse(string __sResponse)
        {
            // YanusIV echoes the command it received. Local echo and actual error code might arrive at the same time,
            // We have to rely on the fact that all YanusIV communication is terminated by \r, hence, we need to split the responses at \r,,,
            string[] _sSplitResponse = __sResponse.Split('\r');

            // All possible errors are in the DSPError enum.
            // We will get them and check the incoming data against all of them.
            var _vErros = EnumUtil.GetValues<DspError>();

            this.m_errCurrentError = DspError.SCAN_CMD_NO_ERROR; // assume we start with no error

            // Run through the response and check against all errors.
            foreach (string s in _sSplitResponse)
            {
                foreach (DspError e in _vErros)
                {
                    // If YanusIV response equals an error we will throw an error event!
                    if (s == ((int)e).ToString() && e != DspError.SCAN_CMD_NO_ERROR
                        && e != DspError.SCAN_CMD_RUN_ABORTED)
                    {
                        this.m_errCurrentError = e;

                        // Throw an ErrorOccurred event to inform the user.
                        if (this.ErrorOccurred != null)
                        {
                            Logger.Debug(string.Format("YanusIV: returned a DSPError = {0}!", e.ToString()));
                            this.ErrorOccurred(this, new EventArgs());
                        }
                    }
                }
            }

            this.m_sPartialResponse = null;
        }

        /// <summary>
        /// Directly sets value of YanusIV scan channel (axis) without modifying the current DSP protocol.
        /// 
        /// Command syntax is: "V channel, value"
        /// V = DSP_CMD_SET_VALUE (indicates that we will set a value of the channel)
        /// channel = integer number
        /// value = long integer number
        /// 
        /// Example (type the quoted expression in the terminal and press enter to send the command to YanusIV): 
        /// 
        /// "V 3,0" - this sets channel 3 to 0, which means X axis (X = channel 3) goes to zero position (value = 0).
        /// </summary>
        /// <param name="__dscChan">
        /// The channel to act on.
        /// </param>
        /// <param name="__lVal">
        /// The value of the channel.
        /// </param>
        /// <returns>
        /// a string that represents the DSP scan command to execute.
        /// </returns>
        private string QuickValueCmd(DscChannel __dscChan, long __lVal)
        {
            string _sCmd = ((char)DspCommand.DSP_CMD_SET_VALUE).ToString() + " " + ((int)__dscChan).ToString() + ","
                           + __lVal.ToString();

            return _sCmd;
        }

        /// <summary>
        /// Send DSP command to YanusIV.
        /// </summary>
        /// <param name="__sCmd">
        /// A string - the DSP command.
        /// </param>
        private void SendIfAvailable(string __sCmd)
        {
            if (this.m_prtComm != null && this.m_prtComm.IsOpen)
            {
                Logger.Debug("YanusIV: send DSP command --> " + __sCmd);
                this.m_prtComm.Send(__sCmd);
            }
            else
            {
                Logger.Error("YanusIV: check COM Port - port seems closed or not active!");
            }
        }

        /// <summary>
        /// Start/Execute the current DSP scan protocol.
        /// </summary>
        private void Start()
        {
            // Assign the command that starts/executes the current DSP protocol
            string _sCmd = ((char)DspCommand.DSP_CMD_EXECUTE_PROT).ToString();

            // starts/executes the currently loaded DSP protocol

            // Debug
            Logger.Debug("YanusIV: scan started!");

            // Send the Start/Execute protocol command
            this.SendIfAvailable(_sCmd);
        }

        #endregion
    }
}