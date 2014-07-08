﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using KUL.MDS.Library;

namespace KUL.MDS.Hardware
{
    /// <summary>
    /// Class that represents YanusIV galvo scanner
    /// </summary>
    public class YanusIV : IPiezoStage
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region Constants

        private const long MAX_YANUS_GALVO_RANGE_ANGLE = 1048576L * 32768L;  // +/- 1048576L * 32768L is the true maximum range a galvo axis can go in terms of an integer value
        private const long MAX_SETUP_GALVO_RANGE_ANGLE = 1048576L * 4096L;  // +/- 1048576L * 4096L is the current useful (due to limits of the microscope setup) range a galvo axis can go in terms of an integer value
        private const double SINGLE_CYCLE_LENGTH_us = 10.0;  // the length of one cycle of YanusIV controller
        private const int MAX_DSP_FAILED_LOADS = 10;  // the number of times to try the reload the DSP protocol (this deals with a bug in .Net Serial Port lib)

        // IMPORTANT NOTE FOR THE MARKERS (Markers = synchronization signals send by external harder to the data stream of Time Harp): 
        // The external hardware YanusIV (a galvo scanner) can output three marker bit0, bit1, bit2, since bit0 comes with a ~1us delay Time Harp 
        // detect it as a separate bit - e.g. if you assign to YanusiV a FrameMarker = 7 (111 in binary, bit2=1 bit1=1 bit0=1), it will
        // be detected by Time Harp as two markers, the first one 6 (110 in binary, bit2=1 bit1=1 bit0=0) and the second one 1 (001 in binary, bit2=0 bit1=0 bit0=1).
        // This also means that all odd numbers will be detected in this manner (e.g. if FrameMarker = 5 --> it will be detected as 4 and 1). Therefore, except for one
        // the other markers must be even numbers.
        private const byte FRAME_MARKER = 2;  // the default frame marker (YanusIV can output a bit pattern for beginning of frame on the digital outputs)
        private const byte LINE_MARKER = 4;  // the default line marker (YanusIV can output a bit pattern for beginning of line on the digital outputs)
        
        #endregion Constants


        #region Members

        // Create variables to keep track of the currently set voltage to the Piezo stage.
        private string m_sSerialPortName;  // the name of the serial port where the galvo is connected to
        private int m_iFrameMarker = FRAME_MARKER;  // the frame synchronization marker that the galvo rises upon a beginning of a frame
        private int m_iLineMarker = LINE_MARKER;  // the line synchronization marker that the galvo rises upon a beginning of a line
        private double m_dMagnificationObjective;  //the magnification of the objective
        private double m_dScanLensFocalLength;  //the focal length of the scan lens in [mm]
        private double m_dRangeAngleDegrees;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
        private double m_dRangeAngleInt;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           

        private int m_iTypeOfScan;  // set the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)

        private double m_dXPosCurrent;
        private double m_dYPosCurrent;
        private double m_dZPosCurrent;
        private int m_iSamplesToStageCurrent;

        // Some properties of the stage.
        private int m_iControllerID;
        private string m_sIDN;
        private DSPError m_errCurrentError;
        private string m_sAxes;
        double m_dFreq;
        int m_iSteps;

        // Status of the stage
        private bool m_bIsInitialized = false;

        // We need a ComPort...
        private CommPort m_prtComm;

        // Place to store partial output...
        private string m_sPartialResponse = null;

        // List that contains the DSC protocol commands - e.g. it may contain commands that define a rectangular area to scan
        private List<DSCCommand> m_lCmdList = new List<DSCCommand>();       

        #endregion Members



        #region Properties

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

        public int TypeOfScan  // the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
        {
            get { return this.m_iTypeOfScan; }
        }
       
        public string SerialPortName  // the name of the serial port where the galvo is connected to
        {
            get { return this.m_sSerialPortName; }            
        }

        public int FrameMarker
        {
            get { return this.m_iFrameMarker; }            
        }

        public int LineMarker
        {
            get { return this.m_iLineMarker; }            
        }

        public double MagnificationObjective  //the magnification of the objective
        {
            get { return this.m_dMagnificationObjective; }            
        }

        public double ScanLensFocalLength  //the focal length of the scan lens in [mm]
        {
            get { return this.m_dScanLensFocalLength; }            
        }

        public double RangeAngleDegrees  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
        {
            get { return this.m_dRangeAngleDegrees; }            
        }

        public double RangeAngleInt  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
        {
            get { return this.m_dRangeAngleInt; }            
        }

        #endregion Properties



        #region Events

        public event EventHandler PositionChanged;  // Event thrown whenever the stage changed position.
        public event EventHandler ErrorOccurred;  // Event thrown whenever the hardware generated an error.
        public event EventHandler EngagedChanged;  // Event thrown whenever the stage is switched on, or off.

        #endregion Events



        #region Singleton Pattern

        // This class operates according to a singleton pattern. Having multiple PIAnalogStage could be dangerous because the hardware
        // could be left in an unknown state.
        //private static volatile YanusIV m_instance;
        //private static object m_syncRoot = new object();

        //public static YanusIV Instance
        //{
        //    get
        //    {
        //        if (m_instance == null)
        //        {
        //            lock (m_syncRoot)
        //            {
        //                if (m_instance == null)
        //                {
        //                    m_instance = new YanusIV();
        //                }
        //            }
        //        }

        //        return m_instance;
        //    }
        //}

        #endregion Singleton Pattern



        #region Methods

        /// <summary>
        /// A hardware object Constructor that creates YanusIV galvo scanner object.
        /// </summary>
        /// <param name="__sSerialPortName">The serial port name to connect to YanusIV</param>
        /// <param name="__dMagnificationObjective">The magnification of the objective</param>
        /// <param name="__dScanLensFocalLength">The focal length of the scan lens in [mm]</param>
        /// <param name="__dGalvoRangeAngleDegrees">+/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)</param>
        /// <param name="__dRangeAngleInt">+/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)</param>
        public YanusIV(string __sSerialPortName, double __dMagnificationObjective, double __dScanLensFocalLength, double __dGalvoRangeAngleDegrees, double __dRangeAngleInt)
        {
            // Check if we are already initialized - only set up values if we are not initialized
            if (!this.m_bIsInitialized)
            {
                this.m_sSerialPortName = __sSerialPortName;
                this.m_dMagnificationObjective = __dMagnificationObjective;
                this.m_dScanLensFocalLength = __dScanLensFocalLength;
                this.m_dRangeAngleDegrees = __dGalvoRangeAngleDegrees;
                this.m_dRangeAngleInt = __dRangeAngleInt;                                              
            }           
        }


        /// <summary>
        /// Initialize YanusIV - try to open serial COM port to YanusiV in order to control it
        /// </summary>
        public void Initialize()
        {
            // Initialize only if it is in an initialized state
            if (!this.m_bIsInitialized)
            {				
                this.m_prtComm = CommPort.Instance;  // get an instance to the COM (serial) port
                CommSettings.SetupSettings(m_sSerialPortName);  // set COM port settings
				
                // Register for COM port events which YanusIV must subscribe to
                this.m_prtComm.DataReceived += OnDataReceived;
                this.m_prtComm.StatusChanged += OnStatusChanged;

                // Try to open YanusIV COM (serial) port with the respective COM settings
                _logger.Debug("YanusIV: starting initialization...");
                this.m_prtComm.Open();

				// Check if COM port is active and then if YanusIV is ON
				this.m_errCurrentError = DSPError.SCAN_CMD_COM_GALVO_IS_OFF;  // as long as we do not have response from YanusIV, we assume it is OFF

                if (this.m_prtComm.IsOpen)
                {
					// Probe if YanusIV galvo scanner is ON - we send a command to get an info string and if we cannot get in time we assume the galvo is OFF
                    this.m_prtComm.Send(((char)DSPCommand.DSP_CMD_RETURN_INFO).ToString());  // on initialize get info from YanusIV by the DSP_CMD_RETURN_INFO command
                }
                else
                {
                    this.m_errCurrentError = DSPError.SCAN_CMD_COM_PORT_CLOSED;  // case if port is closed or not active
                }

				// Wait some time to let the galvo respond to the probe command we have just sent to it (make sure the sleep time here is at least 2x bigger than the COM port read time out)
				Thread.Sleep(4 * this.m_prtComm.ReadTimeOut);  

				// Check for errors and decide if the initialization was successful
                if (this.m_errCurrentError == DSPError.SCAN_CMD_NO_ERROR || this.m_errCurrentError == DSPError.SCAN_CMD_RUN_ABORTED)
                {
                    _logger.Debug("YanusIV: engage seems to have worked!");                    
                    this.m_bIsInitialized = true;
                }
                else
                {
                    _logger.Debug("YanusIV: something went wrong initializing YanusIV!");
                    _logger.Debug("YanusIV: error is --> " + this.m_errCurrentError.ToString());
                    
                    if (this.m_prtComm.IsOpen)
                    {
                        this.m_prtComm.Close();
                    }

					this.m_prtComm.DataReceived -= OnDataReceived;
					this.m_prtComm.StatusChanged -= OnStatusChanged;

					// Set the initialization to not successful
					this.m_bIsInitialized = false;
					this.m_errCurrentError = DSPError.SCAN_CMD_NO_ERROR;  // reset error state to no error
                }
                
            }

        }


        /// <summary>
        /// Setup YanusIV - pass few variables to YanusIV prior to starting the scanning
        /// <param name="__iTypeOfScan">The type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)</param>
        /// <param name="__iFrameMarker">The frame synchronization marker that the galvo rises upon a beginning of a frame</param>
        /// <param name="__iLineMarker">The line synchronization marker that the galvo rises upon a beginning of a line</param>
        /// </summary>
        public void Setup(int __iTypeOfScan, int __iFrameMarker, int __iLineMarker)
        {
            m_iTypeOfScan = __iTypeOfScan;
            m_iFrameMarker = __iFrameMarker;
            m_iLineMarker = __iLineMarker; 
        }
        
        
        /// <summary>
        /// Process the event StatusChange.
        /// </summary>
        /// <param name="param">A string that shows the current YanusIV status.</param>
        private void OnStatusChanged(string param)
        {
            _logger.Info("YanusIV: status says --> " + param);
        }


        /// <summary>
        /// Process the event DataReceived.
        /// </summary>
        /// <param name="param">A string that shows the received from YanusIV string data.</param>
        private void OnDataReceived(string param)
        {
            // if we detect a line terminator, add line to output
            int index;

            while (param.Length > 0 && ((index = param.IndexOf("\r")) != -1 || (index = param.IndexOf("\n")) != -1))
            {
                m_sPartialResponse += param.Substring(0, index);
                param = param.Remove(0, index + 1);
                _logger.Debug("YanusIV: response says --> " + m_sPartialResponse);

                if (m_sPartialResponse != null)
                {
                    this.ParseResponse(m_sPartialResponse);
                }
            }

            // if we have data remaining, add a partial line
            if (param.Length > 0)
            {
                m_sPartialResponse += param;
            }
        }


        /// <summary>
        /// Converts the nanometers into an angle (the angle is in terms of a signed integer).
        /// YanusIV uses an angle in terms of a signed integer to move the XY axes.
        /// Note that the max optical angle = +/- 33 degrees, the focal length of the scan lens = 40mm,
        /// max galvo range in terms of controller's integer value = (-32768 to 32767). We calculate the 
        /// correspondence nanometers <--> angles by using the max useful angle for the current setup, i.e.
        /// we have 4.125 [degrees] <--> 4096 [integer]. The magnification of the objective will define the
        /// physical size of the scan area.
        /// </summary>
        /// <param name="_dVal">The input in nanometers</param>
        /// <returns>angle in terms of a signed integer value</returns>
        private long ConvertNanometersToAngle(double _dVal)
        {
            long _lAngleVal = 0L;
            const double _dDSPControllerLowest20bits = 1048576.0;  // = 2^20 - represents the value corresponding to the lowest 20 bits of the DSP controller. Note that it uses 36 bits, and sends the highest 16 bits as axis position

            double _dMagnificationObjective = m_dMagnificationObjective;  //the magnification of the objective
            double _dScanLensFocalLength = m_dScanLensFocalLength;  //the focal length of the scan lens in [mm]            
            double _dRangeAngleDegrees = m_dRangeAngleDegrees;//3.75;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            double _dRangeAngleInt = m_dRangeAngleInt;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)

            double _dRangeNm = 1e+6 * ( ( _dScanLensFocalLength * Math.Tan(_dRangeAngleDegrees * ((2.0 * Math.PI) / 360.0)) ) / _dMagnificationObjective );  // +/- of the setup's max range a galvo axis can reach in [nm]
            double _dAngleIntPerNm = _dRangeAngleInt / _dRangeNm;  //calc the correspondence between angle in integers and the size in nanometers         
            double _dAngleInt = Math.Round(_dVal * _dAngleIntPerNm, 2);  // calc the correspondence between the DSP angle bits and the input nanometers value

            _lAngleVal = Convert.ToInt64(_dAngleInt * _dDSPControllerLowest20bits);  //convert to an nteger value and multiply by the lowest 20 bits so that the value is translated to the YanusIV controller value

            // Check if we do not exceed the max galvo range
            if (_lAngleVal > 2L*MAX_SETUP_GALVO_RANGE_ANGLE)
            {
                _lAngleVal = 2L*MAX_SETUP_GALVO_RANGE_ANGLE;  // if we exceed the max galvo range set a default value (in this case max angle range)
            }
            else if (_lAngleVal < - 2L*MAX_SETUP_GALVO_RANGE_ANGLE)
            {
                _lAngleVal = - 2L*MAX_SETUP_GALVO_RANGE_ANGLE;  // if we exceed the max galvo range set a default value (in this case max angle range)
            }

            return _lAngleVal;
        }


        /// <summary>
        /// Send DSP command to YanusIV.
        /// </summary>
        /// <param name="__sCmd">A string - the DSP command.</param>
        private void SendIfAvailable(string __sCmd)
        {
            if (this.m_prtComm != null && this.m_prtComm.IsOpen)
            {
                _logger.Debug("YanusIV: send DSP command --> " + __sCmd);
                this.m_prtComm.Send(__sCmd);
            }
            else
            {
                _logger.Error("YanusIV: check COM Port - port seems closed or not active!");
            }
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
        /// <param name="__dscChan">The channel to act on.</param>
        /// <param name="__lVal">The value of the channel.</param>
        /// <returns>a string that represents the DSP scan command to execute.</returns>
        private string QuickValueCmd(DSCChannel __dscChan, long __lVal)
        {
            string _sCmd = ((char)DSPCommand.DSP_CMD_SET_VALUE).ToString() + " " + ((int)__dscChan).ToString() + "," + __lVal.ToString();

            return _sCmd;
        }


        // This probably needs improving...
        /// <summary>
        /// Process YanusIV response - if error code returned throw an error event.
        /// </summary>
        /// <param name="__sResponse">A string - the response from the serial COM port.</param>
        private void ParseResponse(string __sResponse)
        {
            // YanusIV echoes the command it received. Local echo and actual error code might arrive at the same time,
            // We have to rely on the fact that all YanusIV communication is terminated by \r, hence, we need to split the responses at \r,,,
            string[] _sSplitResponse = __sResponse.Split('\r');

            // All possible errors are in the DSPError enum.
            // We will get them and check the incoming data against all of them.
            var _vErros = EnumUtil.GetValues<DSPError>();

			this.m_errCurrentError = DSPError.SCAN_CMD_NO_ERROR;  // assume we start with no error

            // Run through the response and check against all errors.
            foreach (string s in _sSplitResponse)
            {
                foreach (DSPError e in _vErros)
                {
                    // If YanusIV response equals an error we will throw an error event!
                    if (s == ((int)e).ToString() && e != DSPError.SCAN_CMD_NO_ERROR && e != DSPError.SCAN_CMD_RUN_ABORTED)
                    {
                        this.m_errCurrentError = e;

                        // Throw an ErrorOccurred event to inform the user.
                        if (ErrorOccurred != null)
                        {
                            _logger.Debug(String.Format("YanusIV: returned a DSPError = {0}!", e.ToString()));
                            ErrorOccurred(this, new EventArgs());
                        }
                    }                    
                }
            }

            m_sPartialResponse = null;
        }


        /// <summary>
        /// Configure the stage to perform scans at a set rate.
        /// </summary>
        /// <param name="__dCycleTimeMilisec">The amount of time in ms between subsequent position updates.</param>
        /// <param name="__iSteps">The amount of pixels in a scan.</param>
        public void Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            throw new NotImplementedException();
        }
        

        /// <summary>
        /// Move galvo scan axes absolute.
        /// </summary>
        /// <param name="__dXPosNm">The new X position in [nm].</param>
        /// <param name="__dYPosNm">The new Y position in [nm].</param>
        /// <param name="__dZPosNm">The new Z position in [nm].</param>
        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            // Moving axis...
            string _sCmdX = QuickValueCmd(DSCChannel.X, ConvertNanometersToAngle(__dXPosNm));
            string _sCmdY = QuickValueCmd(DSCChannel.Y, ConvertNanometersToAngle(__dYPosNm));

            // Debug
            _logger.Debug("YanusIV: trying to send MoveAbs X --> " + _sCmdX);
            _logger.Debug("YanusIV: trying to send MoveAbs Y --> " + _sCmdY);

            // Try to send the DSP scan command to YanusIV!
            this.SendIfAvailable(_sCmdX);
            this.SendIfAvailable(_sCmdY);

            // Keep track of the current position
            this.m_dXPosCurrent = __dXPosNm;
            this.m_dYPosCurrent = __dYPosNm;

            // Raise an event PositionChanged
            if (PositionChanged != null)
            {
                PositionChanged(this, new EventArgs());
            }
        }


        /// <summary>
        /// Move galvo axis relative.
        /// </summary>
        /// <param name="__dXPosNm">The relative change of X position in [nm].</param>
        /// <param name="__dYPosNm">The relative change of Y position in [nm].</param>
        /// <param name="__dZPosNm">The relative change of Z position in [nm].</param>
        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Start scanning - execute the DSP protocol loaded in YanusIV.
        /// </summary>
        /// <param name="__scmScanMode">The type of scan.</param>
        /// <param name="__bResend">Load/Send or not a DSP protocol.</param>
        public void Scan(ScanModes.Scanmode __scmScanMode, bool __bResend)
        {
            // Info
            _logger.Info("Starting Scan ...");

            // Check if we create and execute a new DSP protocol
            if (__bResend)  // if true then creates and executes a new DSP protocol
            {
                // Clear the existing DSP protocol
                this.Clear();  

                // Add a rectangle to the DSP protocol
                int _iTypeOfScan = m_iTypeOfScan;  // the type of scan mode (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
                Int64 _int64ScanCommandLoopCount = 1L;  // number of times to repeat the rectangular scan

                this.AddRectangle(_iTypeOfScan, _int64ScanCommandLoopCount,
                                  __scmScanMode.InitialX, __scmScanMode.InitialY, 
                                  __scmScanMode.ImageWidthPx, __scmScanMode.ImageHeightPx,
                                  __scmScanMode.XScanSizeNm, __scmScanMode.YScanSizeNm,
                                  __scmScanMode.TimePPixel);

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
                    int _iTypeOfScan = m_iTypeOfScan;  // the type of scan mode (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
                    Int64 _int64ScanCommandLoopCount = 1L;  // number of times to repeat the rectangular scan

                    this.AddRectangle(_iTypeOfScan, _int64ScanCommandLoopCount,
                                      __scmScanMode.InitialX, __scmScanMode.InitialY,
                                      __scmScanMode.ImageWidthPx, __scmScanMode.ImageHeightPx,
                                      __scmScanMode.XScanSizeNm, __scmScanMode.YScanSizeNm,
                                      __scmScanMode.TimePPixel);

                    // Load/Send the new/updated DSP protocol to YanusIV
                    this.Load();
                }

                // Execute the currently loaded DSP protocol - tells YanusIV to start the actual scanning
                this.Start(); 
            }                     
        }


        /// <summary>
        /// Start/Execute the current DSP scan protocol.
        /// </summary>
        private void Start()
        {
            // Assign the command that starts/executes the current DSP protocol
            string _sCmd = ((char)DSPCommand.DSP_CMD_EXECUTE_PROT).ToString();  // starts/executes the currently loaded DSP protocol

            // Debug
            _logger.Debug("YanusIV: scan started!");

            // Send the Start/Execute protocol command
            this.SendIfAvailable(_sCmd);
        }


        /// <summary>
        /// Stop the current DSP scan protocol execution and return axes to home position.
        /// </summary>
        public void Stop()
        {
            // Assign the command that breaks the current protocol execution
            string _sCmd = ((char)DSPCommand.DSP_CMD_DO_NOTHING).ToString();  // the DSP command "do nothing" breaks/stops the current protocol execution

            // Info
            _logger.Info("YanusIV: scan finished/stopped!");

            // Send the Stop protocol execution command
            this.SendIfAvailable(_sCmd);

            Thread.Sleep(1000);

            // Move axes to home position
            this.Home();
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
            bool _bDSPCommandLoaded = false;  // true = DSP command loaded correctly; false = DSP command not loaded correctly
            int _intDSPFailedLoadsCount = 0;  // counts how many times the load of the DSP failed
            
            // Debug
            _logger.Debug("YanusIV: trying to load the DSP protocol...");

            // Strings to represent the DSP protocol to send
            StringBuilder _sbDSPString = new StringBuilder();
            string _sDSPString = "";
            string _sLineEnding = this.m_prtComm.GetLineEnding();
            
            while (!_bDSPCommandLoaded && _intDSPFailedLoadsCount < MAX_DSP_FAILED_LOADS)
            {
                // Discard data that may still be in YanusIV buffer - we do not want to mix this data with the future YabusIV responses
                this.m_prtComm.DiscardInBufferData();

                // Format a DSCComand command as a string so that it is suitable to send to the YanusIV galvo scanner
                foreach (DSCCommand _dscCmd in this.m_lCmdList)
                {
                    _sbDSPString.Clear();

                    _sbDSPString.Append((char)DSPCommand.DSP_CMD_ADD_PROT_CMD);  // add protocol command - adds to the DSP protocol the commands that follows
                    _sbDSPString.Append((char)_dscCmd.ScanCmd);  // the protocol command
                    _sbDSPString.Append(",");  // the parameters list's separator symbol
                    _sbDSPString.Append(_dscCmd.Cycle);  // the cycle it will be executed
                    _sbDSPString.Append(",");  // the parameters list's separator symbol
                    _sbDSPString.Append((int)_dscCmd.Channel);  // channel the command will act on
                    _sbDSPString.Append(",");  // the parameters list's separator symbol
                    _sbDSPString.Append(_dscCmd.Value);  // the value to be set on the given channel
                    _sbDSPString.Append(_sLineEnding);  // terminate the line
                    
                    _sDSPString = _sbDSPString.ToString();

                    // Send the DSP command string to the serial port
                    if (this.m_prtComm != null && this.m_prtComm.IsOpen)
                    {
                        _logger.Debug("YanusIV: send DSP command --> " + _sDSPString);
                        this.m_prtComm.Write(_sDSPString);
                    }
                    else
                    {
                        _logger.Error("YanusIV: check COM Port - port seems closed or not active!");
                    }

                
                    // Read back the answer of YanusIV and check for errors - if such we have to load the whole DSP protocol again
                    string _strDSPResponse = this.m_prtComm.Read(10);  // contains both the command itself and the numeric error code
                    string _strDSPCommand = _sDSPString + ((int)DSPError.SCAN_CMD_NO_ERROR).ToString() + _sLineEnding;  //compose the string we expect YanusIV will return at a successful load           

                    if (_strDSPCommand != _strDSPResponse)  // if the response is not as expected we must try to reload the whole DSP protocol again
                    {
                        // Debug
                        _logger.Debug("YanusIV: DSP protocol failed to load! Trying again...");

                        _bDSPCommandLoaded = false;
                        _intDSPFailedLoadsCount++;

                        // Assign the command that clears the current DSP protocol in YanusIV
                        string _sCmd = ((char)DSPCommand.DSP_CMD_CLEAR_PROT).ToString();  // clears the currently loaded DSP protocol
                        
                        // Send the Clear protocol command to YanusIV as well
                        this.SendIfAvailable(_sCmd);
                        this.SendIfAvailable(_sCmd);  //sometimes we need to send a command second time - so to make sure we really cleared the DSP in YanusIV memory, we send a second time

                        // Sleep for a while
                        Thread.Sleep(100);

                        break;  // causes to exit the foreach-loop and tries to load the whole DSP protocol again
                    }
                    else
                    {
                        _bDSPCommandLoaded = true;                        
                    }
                }
        }

            //// Convert to a string that represents the DSP protocol 
            //_sDSPString = _sbDSPString.ToString();

            //// Send the DSP protocol string
            //if (this.m_prtComm != null && this.m_prtComm.IsOpen)
            //{
            //    _logger.Debug("YanusIV: send DSP protocol --> " + _sDSPString);
            //    this.m_prtComm.Write(_sDSPString);
            //}
            //else
            //{
            //    _logger.Error("YanusIV: check COM Port - port seems closed or not active!");
            //}

            // Discard data that may still be in YanusIV buffer - we do not want to mix this data with the future YabusIV responses
            this.m_prtComm.DiscardInBufferData();

            // Show info to the user if there was/were DSP protocol reloads
            if (_intDSPFailedLoadsCount > 0)
            {
                _logger.Info("YanusIV: DSP protocol reloaded " + (_intDSPFailedLoadsCount + 1).ToString() + "x!");  // the number of reloads is the number of failed attempts + the successful one
            }

            // Enable parallel reading of the serial port again
            this.m_prtComm.StartReading();
        }


        /// <summary>
        /// Clear the current DSP scan protocol.
        /// </summary>
        private void Clear()
        {
            // Clear the current DSP protocol representation stored in this class
            this.m_lCmdList.Clear();

            // Assign the command that clears the current DSP protocol in YanusIV
            string _sCmd = ((char)DSPCommand.DSP_CMD_CLEAR_PROT).ToString();  // clears the currently loaded DSP protocol

            // Debug
            _logger.Debug("YanusIV: DSP protocol cleared!");

            // Send the Clear protocol command to YanusIV as well
            this.SendIfAvailable(_sCmd);
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
                    this.Home();  // move galvo axes to home position (X=0,Y=0,Z=0)

                    this.m_prtComm.Close();  // close COM port
                    this.m_bIsInitialized = false;
                }
                else
                {
                    this.m_bIsInitialized = false;
                }

				// Unsubscribe from the Comm events
				this.m_prtComm.DataReceived -= OnDataReceived;
				this.m_prtComm.StatusChanged -= OnStatusChanged;

                // Reset error state to default, i.e. no error
                this.m_errCurrentError = DSPError.SCAN_CMD_NO_ERROR;

                //Update engage event state
                if (EngagedChanged != null)
                {
                    EngagedChanged(this, new EventArgs());
                }

                _logger.Info("YanusIV: now galvo is OFF!");
            }
            else
            {
                _logger.Info("YanusIV: doing nothing, galvo is already OFF!");
            }
        }


        /// <summary> 
        /// Move galvo axes to home position.
        /// </summary>
        public void Home()
        {
            this.MoveAbs(0.0, 0.0, 0.0);
        }
        

        /// <summary>
        /// Add/Create a rectangle in the format suitable to send as DSP scanning protocol that scans a rectangular area.
        /// </summary>    
        /// <param name="__iTypeOfScan">Defines the type of scan - bidirectional (if true) or unidirectional (if false)</param>
        /// <param name="__int64ScanCommandLoopCount">The number of times to repeat the scan of the rectangle</param>
        /// <param name="__dInitialXnm">The X offset in nm</param>
        /// <param name="__dInitialYnm">The Y offset in nm</param>
        /// <param name="__intImageWidthPx">The image width in pixels</param>
        /// <param name="__intImageHeightPx">The image height in pixels</param>
        /// <param name="__dXScanSizeNm">The image width in nm</param>
        /// <param name="__dYScanSizeNm">The image height in nm</param>
        /// <param name="__dTimePPixel">The time per pixel</param>
        private void AddRectangle(int __iTypeOfScan, Int64 __int64ScanCommandLoopCount, double __dInitialXnm, double __dInitialYnm, int __intImageWidthPx, int __intImageHeightPx, double __dXScanSizeNm, double __dYScanSizeNm, double __dTimePPixel)
        {
            // Debug
            _logger.Debug("YanusIV: create a DSP protocol to scan rectangle...");

            // Set and check the scanning range in terms of angles values - starting angle position and angle scan range.
            Int64 _int64XScanAngleInt = this.ConvertNanometersToAngle(__dXScanSizeNm);  // the width of the image in terms of an integer (angle)
            Int64 _int64YScanAngleInt = this.ConvertNanometersToAngle(__dYScanSizeNm);  // the height of the image in terms of an integer (angle)
            Int64 _int64StartXAngleInt = this.ConvertNanometersToAngle(__dInitialXnm) - _int64XScanAngleInt / 2L;  // X offset of the scanning range in terms of an integer (angle) - note that the initial angle is divided by two because (X=0,Y=0) is the middle of the scan area
            Int64 _int64StartYAngleInt = this.ConvertNanometersToAngle(__dInitialYnm) + _int64YScanAngleInt / 2L;  // Y offset of the scanning range in terms of an integer (angle) - note that the initial angle is divided by two because (X=0,Y=0) is the middle of the scan area

            // Check if we do not exceed the max galvo range along X
            if (_int64StartXAngleInt < -MAX_SETUP_GALVO_RANGE_ANGLE || _int64StartXAngleInt > MAX_SETUP_GALVO_RANGE_ANGLE)
            {
                _int64StartXAngleInt = -_int64XScanAngleInt / 2L;  // if we exceed the max galvo range set a default value (in this case no offset applied)
            }

            // Check if we do not exceed the max galvo range along Y
            if (_int64StartYAngleInt < -MAX_SETUP_GALVO_RANGE_ANGLE || _int64StartYAngleInt > MAX_SETUP_GALVO_RANGE_ANGLE)
            {
                _int64StartYAngleInt = +_int64YScanAngleInt / 2L;  // if we exceed the max galvo range set a default value (in this case no offset applied)
            }

            // Get frame/line markers
            Int64 _int64FrameMarker = (Int64) m_iFrameMarker;  // the value of the frame marker to be raised by YanusIV in the beginning of each frame
            Int64 _int64LineMarker = (Int64) m_iLineMarker;  // the value of the line marker to be raised by YanusIV in the beginning of each line            
            
            // Calc time per pixel in terms of cycles (one cycle equals 10.0us)
            double _dTimePPixelUs = __dTimePPixel * 1000.0;  // time per pixel in [us]
            double _dTimePPixelCycles = _dTimePPixelUs / SINGLE_CYCLE_LENGTH_us;  // time per pixel in terms of YanusIV controller cycles

            // Increment/decrement and angles in terms of cycles
            UInt64 _ui64XAngleCycles = (UInt64)(__intImageWidthPx * _dTimePPixelCycles);  // the image width in terms of cycles of the YanusIV controller
            UInt64 _ui64YAngleCycles = (UInt64) (__intImageHeightPx);  // the image height in terms of cycles of the YanusIV controller. Note that it differs than the image width in terms of cycles, because here we directly go to the new line.
            
            Int64 _int64XAngleIntPerCycle = _int64XScanAngleInt / (Int64) _ui64XAngleCycles;  // the angle per cycle in order to cover the width of the image
            Int64 _int64YAngleIntPerCycle = _int64YScanAngleInt / (Int64) _ui64YAngleCycles;  // the angle per cycle in order to cover the height of the image


            // Create the DSP rectangle
            // Create the DSP rectangle
            switch (__iTypeOfScan)
            {
                case 0:  // The scan type is unidirectional scan
                    {
                        // Start DSP loop 1
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_start, 0UL, DSCChannel.None, __int64ScanCommandLoopCount));  // add start DSP loop

                        // Go to top initial position from where we start to scan the rectangle
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.X, _int64StartXAngleInt));  // move X to the left end of the rectangle
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.Y, _int64StartYAngleInt));  // move Y to the top end of the rectangle

                        // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DSCChannel.X, _int64XAngleIntPerCycle));  // set the increment along X so that first we scan along X
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DSCChannel.Y, 0L));  // set the increment along Y to zero (first we scan a line along X)

                        // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV). Note that we need a frame marker to extract an image from the raw Time Harp data stream.
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.DO, _int64FrameMarker));  // raise the frame marker
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 1UL, DSCChannel.DO, 0L));  // set down the frame marker (it is enough to raise the marker for one cycle only)

                        // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_start, _ui64XAngleCycles, DSCChannel.None, __intImageHeightPx));  // add start DSP loop - the number of loops equals the number of lines to scan, i.e. the height of the image

                        // Go to top initial X position from where we start to scan a new line
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles, DSCChannel.X, _int64StartXAngleInt));  // move X to the left end of the rectangle

                        // Set the Y axis decrement so that we go to the next line
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles, DSCChannel.Y, -_int64YAngleIntPerCycle));  // set the decrement along Y so that we go to the next line along Y (top down scanning)

                        // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles, DSCChannel.DO, _int64LineMarker));  // raise the line marker
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DSCChannel.DO, 0L));  // set down the line marker (it is enough to raise the marker for one cycle only)

                        // Set the Y axis decrement to zero so that we stay on the new line
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles + 1UL, DSCChannel.Y, 0L));

                        // End DSP loop 2
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_end, 2L * _ui64XAngleCycles, DSCChannel.None, 0L));  // add end DSP loop - note that we multiply by factor of 2 because the first line was already scanned before entering the second loop.

                        // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.X, 0L));  // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.Y, 0L));  // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

                        // End DSP loop 1
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_end, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.None, 0L));  // add end DSP loop

                        break;
                    }

                case 1:  // The scan type is bidirectional scan
                    {
                
                        // The scan type is bidirectional scan

                        // Start DSP loop 1
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_start, 0UL, DSCChannel.None, __int64ScanCommandLoopCount));  // add start DSP loop

                            // Go forward direction (left to right)

                            // Go to top initial position from where we start to scan the rectangle
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.X, _int64StartXAngleInt));  // move X to the left end of the rectangle
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.Y, _int64StartYAngleInt));  // move Y to the top end of the rectangle

                            // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DSCChannel.X, _int64XAngleIntPerCycle));  // set the increment along X so that first we scan along X
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DSCChannel.Y, 0L));  // set the increment along Y to zero (first we scan a line along X)

                            // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV). Note that we need a frame marker to extract an image from the raw Time Harp data stream.
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 0UL, DSCChannel.DO, _int64FrameMarker));  // raise the frame marker
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 1UL, DSCChannel.DO, 0L));  // set down the frame marker (it is enough to raise the marker for one cycle only)

                            // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_start, _ui64XAngleCycles, DSCChannel.None, __intImageHeightPx / 2));  // add start DSP loop - the number of loops equals the number of lines to scan over 2, i.e. the height of the image over 2 (because it is a bidirectional scan)

                                // Go backward direction (right to left)

                                // Set the X axis decrement so that galvo scan X axis in backwards direction (because the scan is bidirectional)
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles, DSCChannel.X, -_int64XAngleIntPerCycle));  // set the decrement along X so that we go to backwards direction along X (bidirectional scanning)

                                // Set the Y axis decrement so that we go to the next line
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles, DSCChannel.Y, -_int64YAngleIntPerCycle));  // set the decrement along Y so that we go to the next line along Y (top down scanning)

                                // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles, DSCChannel.DO, _int64LineMarker));  // raise the line marker
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DSCChannel.DO, 0L));  // set down the line marker (it is enough to raise the marker for one cycle only)

                                // Set the Y axis decrement to zero so that we stay on the new line
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, _ui64XAngleCycles + 1UL, DSCChannel.Y, 0L));

                                // Go forward direction (left to right)

                                // Set the X axis increment so that galvo scan X axis in forward direction (note that now we reached the beginning of the line, so we have to tell the galvo to scan in forward direction, i.e. left to right)
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 2UL * _ui64XAngleCycles, DSCChannel.X, _int64XAngleIntPerCycle));  // set the increment along X so that we go to forward direction along X

                                // Set the Y axis decrement so that we go to the next line
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 2UL * _ui64XAngleCycles, DSCChannel.Y, -_int64YAngleIntPerCycle));  // set the decrement along Y so that we go to the next line along Y (top down scanning)

                                // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 2UL * _ui64XAngleCycles, DSCChannel.DO, _int64LineMarker));  // raise the line marker
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_value, 2UL * _ui64XAngleCycles + 1UL, DSCChannel.DO, 0L));  // set down the line marker (it is enough to raise the marker for one cycle only)

                                // Set the Y axis decrement to zero so that we stay on the new line
                                this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, 2UL * _ui64XAngleCycles + 1UL, DSCChannel.Y, 0L));
                
                            // End DSP loop 2
                            this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_end, 3UL * _ui64XAngleCycles, DSCChannel.None, 0L));  // add end DSP loop - note that we multiply by factor of 3 because the first line was already scanned before entering the second loop (and inside this loop we scan two lines per loop iteration).

                        // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.X, 0L));  // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_set_increment_1, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.Y, 0L));  // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

                        // End DSP loop 1
                        this.m_lCmdList.Add(new DSCCommand(ScanCommand.scan_cmd_loop_end, ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, DSCChannel.None, 0L));  // add end DSP loop

                        break;
                    }

                default:
                    {
                        //
                        break;
                    }
            }
            
        }


        #endregion Methods

    }
}
