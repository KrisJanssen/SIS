// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeHarpDLL.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This class provides a wrapper for the DLL functions to interact with Time Harp 200 board.
//   Note: Only one object instance is allowed to exist at a time from this class because the Time Harp 200 DLL
//   is not re-entrant (otherwise you can crash the PC).
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.TimeHarp
{
    using System.Text;

    /// <summary>
    /// This class provides a wrapper for the DLL functions to interact with Time Harp 200 board.
    /// Note: Only one object instance is allowed to exist at a time from this class because the Time Harp 200 DLL
    /// is not re-entrant (otherwise you can crash the PC).
    /// </summary>
    public class TimeHarpDLL
    {
        ///////////////////////////////////////////////////////////////////////////////////
        // A wrapper for the TimeHarp DLL functions defined in TimeHarpDefinitions.cs //
        ///////////////////////////////////////////////////////////////////////////////////
        #region Constants

        /// <summary>
        /// The er r_ cod e_ default.
        /// </summary>
        protected const int ERR_CODE_DEFAULT = (int)TimeHarpDefinitions.THError.ERROR_NONE;

        // the default error code value; 0 or >0 - means success

        /// <summary>
        /// Constants used within the class:        
        /// </summary>         
        protected const int ERR_STRING_CAPACITY = 40;

        /// <summary>
        /// The histogra m_ channelmax.
        /// </summary>
        protected const int HISTOGRAM_CHANNELMAX = 4095; // the maximum histogram channel

        /// <summary>
        /// The histogra m_ channelmin.
        /// </summary>
        protected const int HISTOGRAM_CHANNELMIN = 0; // the minimum histogram channel

        /// <summary>
        /// The measurmen t_ mod e_ default.
        /// </summary>
        protected const int MEASURMENT_MODE_DEFAULT = 0;

        // the default measurement mode of Time Harp (0 - standard histogramming or TTTR mode; 1 - continuous mode)

        /// <summary>
        /// The strin g 1_ capacity.
        /// </summary>
        protected const int STRING1_CAPACITY = 8;

        /// <summary>
        /// The threa d_ slee p_ time.
        /// </summary>
        protected const int THREAD_SLEEP_TIME = 1000;

        #endregion

        // the default waiting time before the current thread continue execution. Note that some of the Time Harp DLL functions require at least 600ms waiting prior to execution
        #region Fields

        /// <summary>
        /// The last probed time resolution value of Time Harp in [ns].
        /// </summary>
        protected float m_fResolution; // define property m_fResolution

        /// <summary>
        /// The last probed value of the acquisition time in [ms].
        /// </summary>
        protected int m_iAcquisitionTime; // the acquisition time in [ms]

        /// <summary>
        /// CFD level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.DISCRMIN; max value = TimeHarpDefinitions.DISCRMAX.
        /// </summary>        
        protected int m_iCFDDiscrMin;

        // define property m_iCFDDiscrMin = TimeHarpDefinitions.DISCRMIN..TimeHarpDefinitions.DISCRMAX

        /// <summary>
        /// CFD zero cross level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.ZCMIN; max value = TimeHarpDefinitions.ZCMAX.
        /// </summary>        
        protected int m_iCFDZeroCross;

        // define property m_iCFDZeroCross = TimeHarpDefinitions.ZCMIN..TimeHarpDefinitions.ZCMAX

        /// <summary>
        /// The last probed value of the count rate in [Hz] taken from Time Harp.
        /// </summary>
        protected int m_iCountRate; // define property m_iCountRate

        /////////////////////////////////////////////////////////////////////////////
        // Initialization and common functions:

        /// <summary>
        /// The error code eventually returned from executing a Time Harp DLL function.        
        /// </summary>
        protected int m_iErrorCode = ERR_CODE_DEFAULT; // set default value to zero (no error)

        /// <summary>
        /// Stores the active edges of the TTL signals that will appear as markers in TTTR data stream. Relevant in TTTR mode.
        /// </summary>
        protected int m_iMarkerEdge; // define property MarkerEdge = 0 or 1

        /// <summary>
        /// Stores the measurement mode (0 - standard histogramming or TTTR mode; 1 - continuous mode).
        /// Note that if you set the measurement mode through InitializeTimeHarp() function then, 
        /// 0 - one-time histogramming mode, and 1 - TTTR mode. Complain to Picoquant for this confusing decision. 
        /// </summary>
        protected int m_iMeasurementMode; // define property m_iMeasurementMode = 0 or 1

        /// <summary>
        /// The time offset value in [ns] of Time Harp.
        /// min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX.
        /// </summary>          
        protected int m_iOffset;

        // define property m_iOffset = TimeHarpDefinitions.OFFSETMIN..TimeHarpDefinitions.OFFSETCMAX

        /// <summary>
        /// The range code.
        /// m_iRangeCode = 0 (= 1x base resolution), = 1 (2x base resolution) ... up to 
        /// range = TimeHarpDefinitions.RANGES - 1 (largest resolution in multiple of the 2^m_iRangeCode multiplied by the base time resolution).
        /// </summary>         
        protected int m_iRangeCode; // define property m_iRangeCode = 0..(TimeHarpDefinitions.RANGES - 1)

        /// <summary>
        /// Sync level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.SYNCMIN; max value = TimeHarpDefinitions.SYNCMAX.
        /// </summary>        
        protected int m_iSyncLevel;

        // define property m_iSyncLevel = TimeHarpDefinitions.SYNCMIN..TimeHarpDefinitions.SYNCMAX

        /// <summary>
        /// The last probed value of the sync rate in [Hz] taken from Time Harp.
        /// </summary>
        protected int m_iSyncRate; // define property m_iSyncRate

        /// <summary>
        /// Stores the last probed hardware version of Time Harp.        
        /// </summary>
        protected string m_sHardwareVersion;

        /// <summary>
        /// Stores the last probed DLL library version of Time Harp.        
        /// </summary>
        protected string m_sLibraryVersion;

        /// <summary>
        /// Stores the last probed serial number of Time Harp.        
        /// </summary>
        protected string m_sSerialNumber;

        /// <summary>
        /// The error code string corresponding to the returned error code (see m_iErrorCode).        
        /// </summary>
        protected StringBuilder m_sbCurrentError = new StringBuilder(ERR_STRING_CAPACITY);

        #endregion

        #region Public Properties

        /// <summary>
        /// Property - stores the the acquisition time in [ms].
        /// </summary>
        public int AcquisitionTime
        {
            // the acquisition time in [ms]
            get
            {
                return this.m_iAcquisitionTime;
            }
        }

        /// <summary>
        /// Property - stores CFD level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.DISCRMIN; max value = TimeHarpDefinitions.DISCRMAX.
        /// </summary> 
        public int CFDDiscrMin
        {
            // set or get property value in [mV]
            get
            {
                return this.m_iCFDDiscrMin;
            }

            set
            {
                this.m_iCFDDiscrMin = this.GetCFDDiscrMin(value);
            }
        }

        /// <summary>
        /// Property - stores CFD zero cross level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.ZCMIN; max value = TimeHarpDefinitions.ZCMAX.
        /// </summary> 
        public int CFDZeroCross
        {
            // set or get property value in [mV]
            get
            {
                return this.m_iCFDZeroCross;
            }

            set
            {
                this.m_iCFDZeroCross = this.GetCFDZeroCross(value);
            }
        }

        /// <summary>
        /// Returns the current value of the count rate in [Hz] taken from Time Harp.
        /// </summary>
        public virtual int CountRate
        {
            // define property CountRate
            get
            {
                // Set the correct variables to get the count rate
                int _iMeasurementMode = 0; // measurement mode must be 0 - one-time histogramming and TTTR modes
                int _iAcquisitionTime = 1000; // acquisition time in [ms]
                int _iCountRate = this.GetCountRate(_iMeasurementMode, _iAcquisitionTime);

                // set to -1 to indicate unsuccessful attempt to get the count rate
                this.m_iCountRate = _iCountRate; // keeps track of the last probed count rate value

                return _iCountRate;
            }
        }

        /// <summary>
        /// Property that stores the error code string corresponding to the returned error code (see m_iErrorCode).        
        /// </summary>
        public string CurrentError
        {
            // set or get property value
            get
            {
                return this.m_sbCurrentError.ToString();
            }
        }

        /// <summary>
        /// Property that stores the error code eventually returned from executing a Time Harp DLL function.        
        /// </summary>
        public int ErrorCode
        {
            // set or get property value
            get
            {
                return this.m_iErrorCode;
            }
        }

        /// <summary>
        /// Property - stores the hardware version of Time Harp.        
        /// </summary>
        public string HardwareVersion
        {
            // set or get property value
            get
            {
                return this.m_sHardwareVersion;
            }
        }

        /// <summary>
        /// Property - stores the DLL library version of Time Harp.        
        /// </summary>
        public string LibraryVersion
        {
            // set or get property value
            get
            {
                return this.m_sLibraryVersion;
            }
        }

        /// <summary>
        /// Property - stores the active edges of the TTL signals that will appear as markers in TTTR data stream. Relevant in TTTR mode.        
        /// </summary>
        public int MarkerEdge
        {
            // set or get property value
            get
            {
                return this.m_iMarkerEdge;
            }

            set
            {
                this.m_iMarkerEdge = this.GetMarkerEdge(value);
            }
        }

        /// <summary>
        /// Property - stores the measurement mode (0 - standard histogramming; 1 - TTTR).        
        /// </summary>
        public int MeasurementMode
        {
            // set or get property value
            get
            {
                return this.m_iMeasurementMode;
            }

            set
            {
                this.m_iMeasurementMode = this.GetMeasurementMode(value);
            }
        }

        /// <summary>
        /// Property - stores the time offset value in [ns] of Time Harp.
        /// min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX.
        /// </summary>   
        public int Offset
        {
            // set or get property value
            get
            {
                return this.m_iOffset;
            }

            set
            {
                this.m_iOffset = this.GetOffset(value);
            }
        }

        /// <summary>
        /// Property - stores the range code.
        /// m_iRangeCode = 0 (= 1x base resolution), = 1 (2x base resolution) ... up to 
        /// range = TimeHarpDefinitions.RANGES - 1 (largest resolution in multiple of the 2^m_iRangeCode multiplied by the base time resolution).
        /// </summary>  
        public int RangeCode
        {
            // set or get property value
            get
            {
                return this.m_iRangeCode;
            }

            set
            {
                this.m_iRangeCode = this.GetRangeCode(value);
            }
        }

        /// <summary>
        /// Property - stores the time resolution value of Time Harp in [ns].
        /// </summary>
        public float Resolution
        {
            // set or get property value
            get
            {
                return this.m_fResolution;
            }
        }

        /// <summary>
        /// Property - stores the serial number of the installed Time Harp board.        
        /// </summary>
        public string SerialNumber
        {
            // set or get property value
            get
            {
                return this.m_sSerialNumber;
            }
        }

        /// <summary>
        /// Property - stores Sync level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.SYNCMIN; max value = TimeHarpDefinitions.SYNCMAX.
        /// </summary>
        public int SyncLevel
        {
            // set or get property value
            get
            {
                return this.m_iSyncLevel;
            }

            set
            {
                this.m_iSyncLevel = this.GetSyncLevel(value);
            }
        }

        /// <summary>
        /// Property - stores the value of the sync rate in [Hz] taken from Time Harp.
        /// </summary>
        public int SyncRate
        {
            // set or get property value
            get
            {
                return this.m_iSyncRate;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Calibrate Time Harp board - necessary prior to the start of a measurement session.       
        /// </summary>  
        public void Calibrate()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_Calibrate();

            // calibrate Time Harp, this performs internal calibration and self test procedure
            this.CheckForException(this.m_iErrorCode, "cannot calibrate Time Harp board by Calibrate() function: ");
        }

        /// <summary>
        /// Enable or disable routing - 0 (disable routing) or 1 (enable routing)       
        /// </summary>
        /// <param name="__iEnable">
        /// Enable or disable routing = 0 (disable routing) or 1 (enable routing).
        /// </param>
        public void EnableRouting(int __iEnable)
        {
            // Check if __iMarkerEdge0 has a correct value
            if (!(__iEnable == 0 || __iEnable == 1))
            {
                __iEnable = 0; // set default to 0 (disable routing)
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_EnableRouting(__iEnable);

            // enable or disable routing for PRT/NRT 400 Router connected to Time Harp          
            this.CheckForException(this.m_iErrorCode, "Time Harp cannot find router by EnableRouting() function: ");
        }

        /// <summary>
        /// Get the Time Harp board approximated time resolution in the current range in [ns].
        /// Use this function before calibration. For exact values use GetResolution(), but first 
        /// you must calibrate by Calibrate() function.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetBaseResolution()
        {
            int __iResolutionOrErrorCode = TimeHarpDefinitions.TH_GetBaseResolution();

            // get approximated time resolution value of Time Harp in [ns]
            if (__iResolutionOrErrorCode < 0.0)
            {
                // checked if there was an error returned
                this.m_iErrorCode = __iResolutionOrErrorCode;

                // convert the returned error code to integer in order to handle the error properly
                this.CheckForException(
                    this.m_iErrorCode, 
                    "cannot get the Time Harp resolution value by GetBaseResolution() function: ");
            }
            else
            {
                // the case of no error, assign default values
                this.m_iErrorCode = 0;
                this.CheckForException(
                    this.m_iErrorCode, 
                    "cannot get the Time Harp resolution value by GetResolution() function: ");
            }

            return __iResolutionOrErrorCode;
        }

        /// <summary>
        /// Get the current count rate (Note: if __iAcquisitionTime = 1000 then it returns the counts/sec).
        /// </summary>
        /// <param name="__iMeasurementMode">
        /// The Time Harp measurement/acquisition mode. There are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode.
        /// </param>
        /// <param name="__iAcquisitionTime">
        /// The acquisition time in milliseconds (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX).
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCountRate(int __iMeasurementMode, int __iAcquisitionTime)
        {
            this.SetMeasurementMode(__iMeasurementMode, __iAcquisitionTime);

            // needs to call this function prior to getting the count rate
            System.Threading.Thread.Sleep(THREAD_SLEEP_TIME); // allow at least 600ms to get a stable rate meter reading
            System.Threading.Thread.Sleep(THREAD_SLEEP_TIME); // allow at least 600ms to get a stable rate meter reading

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetCountRate();

            // get the count rate of Time Harp, the count_rate_per_second = GetCountRate() / __iAcquisitionTime
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the Time Harp count rate by GetCountRate() function: ");

            this.m_iCountRate = this.m_iErrorCode;

            // if the execution reaches this point it means this will return the count rate
            return this.m_iCountRate; // return the count rate
        }

        /// <summary>
        /// Return the elapsed measurement time in [ms] (not for continuous mode).
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetElapsedMeasurementTime()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_GetElapsedMeasTime();

            // get the elapsed measurement time from Time Harp device
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot get the elapsed measurement time by GetElapsedMeasurementTime() function: ");

            int _iElapsedMeasurementTime = this.m_iErrorCode;

            // if the execution reaches this point, it means the elapsed time in [ms]
            return _iElapsedMeasurementTime; // return the elapsed measurement time
        }

        /// <summary>
        /// Returns the current status flags (a bit pattern) - use the predefined flags (e.g. FLAG_OVERFLOW) and bitwise AND to extract individual bits.
        /// Note: you can call this function anytime during a measurement but not during DMA.
        /// </summary>
        /// <returns>int: returns the current status flags (a bit pattern)</returns>
        public int GetFlags()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_GetFlags();
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the acquisition status of Time Harp by GetFlags() function: ");

            int _iFlags = this.m_iErrorCode; // if the execution reaches this point it means the flags bit pattern
            return _iFlags; // return the current status flags (a bit pattern)
        }

        /// <summary>
        /// Returns the hardware version of Time Harp.        
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHardwareVersion()
        {
            // return the Time Harp hardware version
            StringBuilder _sbHardwareVersion = new StringBuilder(STRING1_CAPACITY);
            this.m_iErrorCode = TimeHarpDefinitions.TH_GetHardwareVersion(_sbHardwareVersion);

            // get the hardware version of Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the Time Harp hardware version by GetHardwareVersion() function: ");

            this.m_sHardwareVersion = _sbHardwareVersion.ToString();
            return this.m_sHardwareVersion;
        }

        /// <summary>
        /// Returns the DLL library version of Time Harp.        
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetLibraryVersion()
        {
            // return the Time Harp DLL library version
            StringBuilder _sbLibraryVersion = new StringBuilder(STRING1_CAPACITY);
            this.m_iErrorCode = TimeHarpDefinitions.TH_GetLibraryVersion(_sbLibraryVersion);

            // get the library version of Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the Time Harp DLL library version by GetLibraryVersion() function: ");

            this.m_sLibraryVersion = _sbLibraryVersion.ToString();
            return this.m_sLibraryVersion;
        }

        /// <summary>
        /// Get the Time Harp board time resolution in the current range in [ns].
        /// </summary>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public float GetResolution()
        {
            float _fResolutionOrErrorCode = TimeHarpDefinitions.TH_GetResolution();

            // get time resolution value of Time Harp in [ns]
            if (_fResolutionOrErrorCode < 0.0)
            {
                // checked if there was an error returned
                this.m_iErrorCode = (int)_fResolutionOrErrorCode;

                // convert the returned error code to integer in order to handle the error properly
                this.CheckForException(
                    this.m_iErrorCode, 
                    "cannot get the Time Harp resolution value by GetResolution() function: ");
            }
            else
            {
                // the case of no error, assign default values
                this.m_iErrorCode = 0;
                this.CheckForException(
                    this.m_iErrorCode, 
                    "cannot get the Time Harp resolution value by GetResolution() function: ");
            }

            this.m_fResolution = _fResolutionOrErrorCode;
            return this.m_fResolution;
        }

        /// <summary>
        /// Returns the number of routing channels available.        
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetRoutingChannels()
        {
            int _iNumberRoutingChannels = 1;

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetRoutingChannels();

            // get number of routing channels of PRT/NRT 400 Router connected to Time Harp          
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot get the number of routing channels by GetRoutingChannels() function: ");

            _iNumberRoutingChannels = this.m_iErrorCode;

            // if the execution reaches this point it means m_iErrorCode = Number of Routing Channels
            return _iNumberRoutingChannels; // return the number of routing channels
        }

        /// <summary>
        /// Returns the Time Harp serial number.        
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSerialNumber()
        {
            // return the Time Harp serial number
            StringBuilder _sbSerialNumber = new StringBuilder(STRING1_CAPACITY);
            this.m_iErrorCode = TimeHarpDefinitions.TH_GetSerialNumber(_sbSerialNumber);

            // get the serial number of Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the Time Harp serial number by GetSerialNumber() function: ");

            this.m_sSerialNumber = _sbSerialNumber.ToString();
            return this.m_sSerialNumber;
        }

        /// <summary>
        /// Get the current sync rate (reads the rate of laser pulses from the SYNC input).
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetSyncRate()
        {
            this.SetSyncMode(); // needs to call this function prior to getting the sync rate

            System.Threading.Thread.Sleep(THREAD_SLEEP_TIME); // allow at least 600ms to get a stable rate meter reading
            System.Threading.Thread.Sleep(THREAD_SLEEP_TIME); // allow at least 600ms to get a stable rate meter reading

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetCountRate(); // get the sync rate of Time Harp,
            this.CheckForException(this.m_iErrorCode, "cannot get the Time Harp sync rate by GetSyncRate(): ");

            this.m_iSyncRate = this.m_iErrorCode;

            // if the execution reaches this point it means this will return the sync rate

            // Console.WriteLine("GetSyncRate(): TTTRHeader.SyncRate = {0}, m_iSyncRate = {1}", Files.TTTRFileHeader.TTTRHeader.SyncRate, m_iSyncRate);  //for debugging purposes
            return this.m_iSyncRate; // return sync rate
        }

        /// <summary>
        /// Initialize Time Harp itself and set up its measurement/acquisition mode.
        /// Here we interpret the measurement mode as follows: _iMeasurementMode = 0 (histogramming) or 1 (TTTR mode).
        /// </summary>        
        public void InitializeTimeHarp()
        {
            int _iMeasurementMode = this.m_iMeasurementMode;

            // Convert the measurement mode passed to InitializeTimeHarp() to match its expected mode value.
            // Note that the corresponding measurement mode in InitializeTimeHarp() function differs compare SetMeasurementMode() function.
            // For example, in SetMeasurementMode() we have: 0 - standard histogramming or TTTR, and 1 - continuous mode. While in
            // InitializeTimeHarp() we have: 0 - histogramming, and 1 - TTTR mode. In the class we use the convention that SetMeasurementMode() expects.
            // Thus to assign the proper measurement mode here we need to convert _iMeasurementMode to the appropriate value. 
            // This is realized in the below if-statement. It takes care to match both cases so that we do not need to change things anywhere else.            
            if (_iMeasurementMode == 0)
            {
                // convert to 1 - TTTR mode according InitializeTimeHarp() convention
                _iMeasurementMode = 1; // (0 - histogramming, 1 - TTTR mode)               
            }
            else if (_iMeasurementMode == 1)
            {
                // convert to 0 - histogramming mode according InitializeTimeHarp() convention
                _iMeasurementMode = 0; // (0 - histogramming, 1 - TTTR mode)               
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_Initialize(_iMeasurementMode);

            // set up the Time Harp measurement mode (0 - histogramming, 1 - TTTR mode) 
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot initialize and set the Time Harp measurement mode by InitializeTimeHarp() function: ");
        }

        /// <summary>
        /// FIFO empty status - return True if Time Harp has reported FIFO is empty.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFIFOEmpty(int _iFlags)
        {
            bool _bIsFIFOEmpty = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_FIFOEMPTY); // extract flag status
            return _bIsFIFOEmpty; // true = FiFo is empty, false = FiFo is not empty
        }

        /// <summary>
        /// FIFO status - return True if Time Harp has reported FIFO is full.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFIFOFull(int _iFlags)
        {
            bool _bIsFIFOFull = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_FIFOFULL); // extract flag status
            return _bIsFIFOFull; // true = FiFo full, false = FiFo not full
        }

        /// <summary>
        /// FIFO half status - return True if Time Harp has reported FIFO is half full.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFIFOHalfFull(int _iFlags)
        {
            bool _bIsFIFOHalfFull = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_FIFOHALFFULL);

            // extract flag status
            return _bIsFIFOHalfFull; // true = FiFo is half full, false = FiFo is not half full
        }

        /// <summary>
        /// Overflow error status - return True if Time Harp has reported overflow error.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsOverflow(int _iFlags)
        {
            bool _bIsOverflowError = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_OVERFLOW);

            // extract flag status
            return _bIsOverflowError; // true = error, false = no error
        }

        /// <summary>
        /// RAM status - return True if Time Harp has reported RAM is ready.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRAMReady(int _iFlags)
        {
            bool _bIsRAMReady = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_RAMREADY); // extract flag status
            return _bIsRAMReady; // true = ready, false = not ready
        }

        /// <summary>
        /// Acquisition time status - return True if measurement is running.
        /// </summary>
        /// <returns>bool: returns the current running status of Time Harp</returns>
        public bool IsRunning()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_CTCStatus(); // get the acquisition time status
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot get the acquisition status of Time Harp by IsRunning() function: ");

            int _iAcquisitionTimeStatus = this.m_iErrorCode;

            // 0 - acquisition time still running; >0 - acquisition time has ended
            bool _bIsRunning = false;
            if (_iAcquisitionTimeStatus == 0)
            {
                // check if measurement is running
                _bIsRunning = true;
            }
            else if (_iAcquisitionTimeStatus > 0)
            {
                _bIsRunning = false;
            }

            return _bIsRunning;
        }

        /// <summary>
        /// Scan active status - return True if Time Harp has reported scan is 
        /// active (use only if you connect SCX controller to Time Harp).
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsScanActive(int _iFlags)
        {
            bool _bIsScanActive = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_SCANACTIVE);

            // extract flag status
            return _bIsScanActive; // true = scan is active, false = scan is not active
        }

        /// <summary>
        /// System error status - return True if Time Harp has reported system error.
        /// Prior to using this function call GetFlags() function to get the status flags.
        /// </summary>
        /// <param name="_iFlags">
        /// The status flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSystemError(int _iFlags)
        {
            bool _bIsSystemError = this.GetStatusFlag(_iFlags, TimeHarpDefinitions.FLAG_SYSERR); // extract flag status
            return _bIsSystemError; // true = error, false = no error
        }

        /// <summary>
        /// Set up the Time Harp board CFD level, value is in [mV].
        /// min value = TimeHarpDefinitions.DISCRMIN; max value = TimeHarpDefinitions.DISCRMAX.
        /// </summary>       
        public void SetCFDDiscrMin()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetCFDDiscrMin(this.m_iCFDDiscrMin);

            // set up CFD level of Time Harp in [mV]
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp CFD level by SetCFDDiscrMin() function: ");
        }

        /// <summary>
        /// Set up the Time Harp board CFD zero cross level in [mV].
        /// min value = TimeHarpDefinitions.ZCMIN, max value = TimeHarpDefinitions.ZCMAXmin value = TimeHarpDefinitions.SYNCMIN.
        /// </summary>         
        public void SetCFDZeroCross()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetCFDZeroCross(this.m_iCFDZeroCross);

            // set up CFD zero cross level of Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp CFD zero cross level by SetCFDZeroCross() function: ");
        }

        /// <summary>
        /// Set the measurement mode.
        /// </summary>
        /// <param name="__iMeasurementMode">
        /// The Time Harp measurement/acquisition mode. There are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode.
        /// </param>
        /// <param name="__iAcquisitionTime">
        /// The acquisition time in milliseconds (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX).
        /// </param>
        public void SetMeasurementMode(int __iMeasurementMode, int __iAcquisitionTime)
        {
            if (!(__iMeasurementMode == 0 || __iMeasurementMode == 1))
            {
                // check if __iMeasurementMode has the correct value, if not set it to 0 (one-time histogramming and TTTR mode as default value)
                __iMeasurementMode = 0; // assign the default Measurement Mode value               
            }

            if (__iAcquisitionTime == 0)
            {
                // check if __iAcquisitionTime has the correct value, (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX)
                __iAcquisitionTime = TimeHarpDefinitions.ACQTMAX;

                // in case __iAcquisitionTime = 0 we assign the maximum acquisition time               
            }
            else if (__iAcquisitionTime < TimeHarpDefinitions.ACQTMIN)
            {
                // check if __iAcquisitionTime has the correct value, (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX)
                __iAcquisitionTime = TimeHarpDefinitions.ACQT_DEFAULT;

                // assign the default acquisition time               
            }
            else if (__iAcquisitionTime > TimeHarpDefinitions.ACQTMAX)
            {
                // check if __iAcquisitionTime has the correct value, (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX)
                __iAcquisitionTime = TimeHarpDefinitions.ACQTMAX;

                // assign the maximum acquisition time             
            }

            this.m_iAcquisitionTime = __iAcquisitionTime; // keep track of the acquisition time in [ms]

            this.m_iErrorCode = TimeHarpDefinitions.TH_SetMMode(__iMeasurementMode, __iAcquisitionTime);

            // set the Time Harp measurement mode and acquisition time         
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp measurement mode by SetMeasurementMode() function: ");
        }

        /// <summary>
        /// (Note: Must have NRT Router) Set NRT 400 CFD level       
        /// </summary>
        /// <param name="__iChannel">
        /// Channel number = 0..3.
        /// </param>
        /// <param name="__iLevel">
        /// Discrimination level = 0..400 [mV].
        /// </param>
        /// <param name="__iZerocross">
        /// Zero cross voltage = 0..40 [mV].
        /// </param>
        public void SetNRT400CFD(int __iChannel, int __iLevel, int __iZerocross)
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetNRT400CFD(__iChannel, __iLevel, __iZerocross);

            // Set NRT 400 CFD level for NRT 400 Router connected to Time Harp          
            this.CheckForException(this.m_iErrorCode, "Time Harp cannot find router by SetNRT400CFD() function: ");
        }

        /// <summary>
        /// Set up the Time Harp board offset value in [ns], value is approximation of the desired offset (in steps of 2.5ns).
        /// min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX.
        /// </summary>        
        public void SetOffset()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetOffset(this.m_iOffset);

            // set up Offset value of Time Harp in [ns]            
            this.CheckForException(this.m_iErrorCode, "cannot set the Time Harp offset value by SetOffset() function: ");
        }

        /// <summary>
        /// Set up the Time Harp board range in multiple of the base time resolution 
        /// max value = TimeHarpDefinitions.RANGES - 1. 
        /// </summary>        
        public void SetRange()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetRange(this.m_iRangeCode); // set up Range of Time Harp
            this.CheckForException(this.m_iErrorCode, "cannot set the Time Harp range by SetRange() function: ");
        }

        /// <summary>
        /// Set up the Time Harp board Sync level, value is in [mV].
        /// min value = TimeHarpDefinitions.SYNCMIN; max value = TimeHarpDefinitions.SYNCMAX.
        /// </summary>        
        public void SetSyncLevel()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetSyncLevel(this.m_iSyncLevel);

            // set up Sync level of Time Harp in [mV]
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp Sync level by SetSyncLevel() function: ");
        }

        /// <summary>
        /// Start Time Harp measurement.
        /// </summary>  
        public void StartMeausurement()
        {
            // Instruct Time Harp to start measurement
            this.m_iErrorCode = TimeHarpDefinitions.TH_StartMeas();
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot start measurement by StartMeasurement() function: ");
        }

        /// <summary>
        /// Stop measurement. Note that in TTTR mode this rests the hardware FIFO.
        /// </summary>  
        public void StopMeasurement()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_StopMeas(); // stop Time Harp measurement/acquisition
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot stop measurement/acquisition by StopMeasurement() function: ");
        }

        /// <summary>
        /// Returns the number of transferred records.
        /// The function waits for DMA to complete. CPU time during wait for DMA completion will be yielded to other processes/threads.
        /// __uiBufferData[] past in T3RStartDMA() must not be accessed until T3RCompleteDMA() returns.
        /// </summary>
        /// <returns>int: returns the number of obtained records</returns>
        public int T3RCompleteDMA()
        {
            int _iNumberRecords = 0;

            this.m_iErrorCode = TimeHarpDefinitions.TH_T3RCompleteDMA();

            // initiate and perform DMA to get TTTR data          
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot complete the TTTR data by T3RCompleteDMA() function: ");

            _iNumberRecords = this.m_iErrorCode;

            // if the execution reaches this point it means m_iErrorCode = Number of Transferred Records
            return _iNumberRecords; // return the number of transferred records
        }

        /// <summary>
        /// Set the active edges of the TTL signals that will appear as markers in TTTR data stream. Relevant in TTTR mode.
        /// Three markers available - __iMarkerEdge0, __iMarkerEdge1, __iMarkerEdge2.
        /// m_MarkerEdge = 0 (falling edge) or 1 (rising edge)
        /// </summary>                
        public void T3RSetMarkerEdges()
        {
            // Set up __iMarkerEdge0, __iMarkerEdge1, and __iMarkerEdge2
            int __iMarkerEdge0 = this.m_iMarkerEdge;
            int __iMarkerEdge1 = this.m_iMarkerEdge;
            int __iMarkerEdge2 = this.m_iMarkerEdge;

            this.m_iErrorCode = TimeHarpDefinitions.TH_T3RSetMarkerEdges(__iMarkerEdge0, __iMarkerEdge1, __iMarkerEdge2);

            // set the active edges of the TTL signals
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot set the active edges of the TTL signals by T3RSetMarkerEdges() function: ");
        }

        /// <summary>
        /// The t 3 r start dma.
        /// </summary>
        /// <param name="__uiBufferData">
        /// The __ui buffer data.
        /// </param>
        /// <param name="__iRecordsCount">
        /// The __i records count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int T3RStartDMA(uint[] __uiBufferData, uint __iRecordsCount)
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_T3RStartDMA(__uiBufferData, __iRecordsCount);

            // initiate and perform DMA to get TTTR data          

            // CheckForException(m_iErrorCode, "Time Harp cannot get the TTTR data by T3RStartDMA() function: ");  //I commented this because we must not call other functions from THlib until T3RCompleteDMA() returns (see the function below)
            return this.m_iErrorCode; // return the error code, if return value =0 (success), if <0 (error).
        }

        #endregion

        #region Methods

        /// <summary>
        /// Function that handles common exceptions.   
        /// </summary>
        /// <param name="__iErrorCode">
        /// The error code, an integer value.
        /// </param>
        /// <param name="__sAppendErrorMessage">
        /// Additional error string to be appended to the error message.
        /// </param>
        protected virtual void CheckForException(int __iErrorCode, string __sAppendErrorMessage)
        {
            if (__iErrorCode < 0)
            {
                // check the success of the previous operation
                this.m_sbCurrentError = new StringBuilder("Time Harp: reporting an error --> " + __sAppendErrorMessage);
                this.m_sbCurrentError.Append(this.GetCurrentError(__iErrorCode));
            }
            else
            {
                this.m_sbCurrentError = this.GetCurrentError(__iErrorCode);
            }
        }

        /// <summary>
        /// Clear histogram memory. Relevant in histogramming mode.
        /// </summary>
        /// <param name="__iNumberBlocks">
        /// Number of blocks to clear (always 0 if not routing).
        /// </param>
        protected void ClearHistogramMemory(int __iNumberBlocks)
        {
            // Check if the __iNumberBlocks has a correct value (minimum = 0, maximum = 3)
            if (__iNumberBlocks < 0)
            {
                __iNumberBlocks = 0; // set default to 0
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_ClearHistMem(__iNumberBlocks);

            // clear histogram memory of Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot clear the Time Harp histogram memory by ClearHistogramMemory() function: ");
        }

        // END OF Initialization and common functions
        /////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for CONTINUOUS mode

        /// <summary>
        /// The get bank.
        /// </summary>
        /// <param name="__usChannelCounts">
        /// The __us channel counts.
        /// </param>
        /// <param name="__iChannelFrom">
        /// The __i channel from.
        /// </param>
        /// <param name="__iChannelTo">
        /// The __i channel to.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetBank(ushort[] __usChannelCounts, int __iChannelFrom, int __iChannelTo)
        {
            int _iTotalHistogramCounts = 0;

            // Check if __iChannelFrom is within the correct range
            if ((__iChannelFrom < HISTOGRAM_CHANNELMIN) || (__iChannelFrom > HISTOGRAM_CHANNELMAX))
            {
                __iChannelFrom = HISTOGRAM_CHANNELMIN; // set default to HISTOGRAM_CHANNELMIN                
            }

            // Check if __iChannelTo is within the correct range
            if ((__iChannelTo < HISTOGRAM_CHANNELMIN) || (__iChannelTo > HISTOGRAM_CHANNELMAX))
            {
                __iChannelTo = HISTOGRAM_CHANNELMAX; // set default to HISTOGRAM_CHANNELMAX                
            }

            // Check and make sure that __iChannelFrom <= __iChannelTo
            if (!(__iChannelFrom <= __iChannelTo))
            {
                __iChannelFrom = HISTOGRAM_CHANNELMIN; // set default to HISTOGRAM_CHANNELMIN
                __iChannelTo = HISTOGRAM_CHANNELMAX; // set default to HISTOGRAM_CHANNELMAX
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetBank(__usChannelCounts, __iChannelFrom, __iChannelTo);

            // get histograms and all channels total counts            
            this.CheckForException(this.m_iErrorCode, "Time Harp cannot get the histogram data by GetBank() function: ");

            _iTotalHistogramCounts = this.m_iErrorCode;

            // if the execution reaches this point it means m_iErrorCode = Total Number of Counts
            return _iTotalHistogramCounts; // return the Total Number of Counts in the histograms
        }

        /// <summary>
        /// Returns the total number of counts in the histogram. Relevant in histogramming mode.
        /// The histogram itself is stored in the following array: uint[] __uiChannelCounts = new uint[TimeHarpDefinitions.BLOCKSIZE].
        /// The current version counts only up to 65535 (16 bits).
        /// </summary>
        /// <param name="__uiChannelCounts">
        /// an array of unsigned integer words (32bit) of TimeHarpDefinitions.BLOCKSIZE where the histogram can be stored.
        /// </param>
        /// <param name="__iNumberBlocks">
        /// Number of blocks (0..3) to fetch (always 0 if not routing).
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetBlock(uint[] __uiChannelCounts, int __iNumberBlocks)
        {
            int _iTotalHistogramCounts = 0;

            // Check if the __iNumberBlocks has a correct value (minimum = 0, maximum = 3)
            if (__iNumberBlocks < 0 || __iNumberBlocks > 3)
            {
                __iNumberBlocks = 0; // set default to 0
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetBlock(__uiChannelCounts, __iNumberBlocks);

            // get histogram from Time Harp
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot get the histogram data by GetBlock() function: ");

            _iTotalHistogramCounts = this.m_iErrorCode;

            // if the execution reaches this point it means m_iErrorCode = Total Number of Counts
            return _iTotalHistogramCounts; // return the Total Number of Counts in the histogram
        }

        /// <summary>
        /// Validate CFD level for correctness.        
        /// </summary>
        /// <param name="__iValueMilliVolts">
        /// The level value in [mV].
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetCFDDiscrMin(int __iValueMilliVolts)
        {
            // validate m_iCFDDiscrMin for correctness
            if ((__iValueMilliVolts < TimeHarpDefinitions.DISCRMIN)
                || (__iValueMilliVolts > TimeHarpDefinitions.DISCRMAX))
            {
                __iValueMilliVolts = TimeHarpDefinitions.DISCR_DEFAULT; // assign the default CFD level in [mV]
            }

            return __iValueMilliVolts;
        }

        /// <summary>
        /// The get cfd zero cross.
        /// </summary>
        /// <param name="__iValueMilliVolts">
        /// The __i value milli volts.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetCFDZeroCross(int __iValueMilliVolts)
        {
            // validate m_iCFDZeroCross for correctness
            if ((__iValueMilliVolts < TimeHarpDefinitions.ZCMIN) || (__iValueMilliVolts > TimeHarpDefinitions.ZCMAX))
            {
                __iValueMilliVolts = TimeHarpDefinitions.ZC_DEFAULT; // assign the default CFD level in [mV]
            }

            return __iValueMilliVolts;
        }

        /// <summary>
        /// Returns the corresponding to the error code error string (see m_iErrorCode).        
        /// </summary>
        /// <param name="__iErrorCode">
        /// The __i Error Code.
        /// </param>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        protected StringBuilder GetCurrentError(int __iErrorCode)
        {
            // return the corresponding to the error code error string
            StringBuilder _sbErrorString = new StringBuilder(ERR_STRING_CAPACITY);
            TimeHarpDefinitions.TH_GetErrorString(_sbErrorString, __iErrorCode);

            // get the corresponding to the error code strings            
            return _sbErrorString;
        }

        /// <summary>
        /// The get lost blocks.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetLostBlocks()
        {
            int _iNumberLostBlocks = 0;

            this.m_iErrorCode = TimeHarpDefinitions.TH_GetLostBlocks(); // get the number of lost blocks            
            this.CheckForException(
                this.m_iErrorCode, 
                "Time Harp cannot get the number of lost blocks by GetLostBlocks() function: ");

            _iNumberLostBlocks = this.m_iErrorCode;

            // if the execution reaches this point it means m_iErrorCode = Number of Lost Blocks
            return _iNumberLostBlocks; // return the number of lost blocks in the histogram
        }

        /// <summary>
        /// Validate and then return the active edges of the TTL signals that will appear as markers in TTTR data stream. Relevant in TTTR mode.
        /// Three markers available - note that they are controlled by the same value, i.e. __iMarkerEdge = 0 (falling edge) or 1 (rising edge)
        /// </summary>
        /// <param name="__iMarkerEdge">
        /// Marker 0, set the active TTL edge: 0 - falling edge or 1 - rising edge.
        /// </param>
        /// <return>Returns the marker value, i.e. the active TTL edge (after validation): 0 - falling edge or 1 - rising edge.</return>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetMarkerEdge(int __iMarkerEdge)
        {
            // Check if __iMarkerEdge has a correct value
            if (!(__iMarkerEdge == 0 || __iMarkerEdge == 1))
            {
                __iMarkerEdge = 1; // set default to 1 (rising edge)
            }

            return __iMarkerEdge;
        }

        /// <summary>
        /// Validate and then return the measurement mode (0 - standard histogramming or TTTR mode; 1 - continuous mode).        
        /// </summary>
        /// <param name="__iMeasurementMode">
        /// The measurement mode (0 - standard histogramming or TTTR mode; 1 - continuous mode).
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetMeasurementMode(int __iMeasurementMode)
        {
            // validate m_iMeasurementMode for correctness
            // Check if __iMeasurementMode has the correct value, if not set it to 0 (TTTR mode as default value)
            if (!(__iMeasurementMode == 0 || __iMeasurementMode == 1))
            {
                this.m_iMeasurementMode = MEASURMENT_MODE_DEFAULT; // assign default measurement mode              
            }
            else
            {
                this.m_iMeasurementMode = __iMeasurementMode; // assign the given measurement mode
            }

            __iMeasurementMode = this.m_iMeasurementMode; // reassign the proper measurement mode to __iMeasurementMode

            return __iMeasurementMode;
        }

        /// <summary>
        /// Validate the time offset value of Time Harp.
        /// min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX.
        /// </summary>
        /// <param name="__iOffset">
        /// The offset value in [ns].
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetOffset(int __iOffset)
        {
            // validate m_iOffset for correctness
            if ((__iOffset < TimeHarpDefinitions.OFFSETMIN) || (__iOffset > TimeHarpDefinitions.OFFSETMAX))
            {
                __iOffset = TimeHarpDefinitions.OFFSET_DEFAULT; // assign the default Offset in [ns]
            }

            return __iOffset;
        }

        /// <summary>
        /// Validate range code for correctness
        /// </summary>
        /// <param name="__iRangeCode">
        /// The range code value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetRangeCode(int __iRangeCode)
        {
            // validate m_iRangeCode for correctness
            if ((__iRangeCode < 0) || (__iRangeCode > (TimeHarpDefinitions.RANGES - 1)))
            {
                __iRangeCode = TimeHarpDefinitions.RANGES_DEFAULT; // assign the default Range code
            }

            return __iRangeCode;
        }

        /// <summary>
        /// Returns the status of a particular flag - True or False (e.g. it uses FLAG_OVERFLOW etc and bitwise AND to extract the bit pattern of a flag).
        /// </summary>
        /// <param name="__iFlags">
        /// The __i Flags.
        /// </param>
        /// <param name="__iFlagBitPattern">
        /// The __i Flag Bit Pattern.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool GetStatusFlag(int __iFlags, int __iFlagBitPattern)
        {
            bool _bFlagStatus = false;

            int _iErrorFlag = __iFlags & __iFlagBitPattern; // get the binary number related to the error status
            if (_iErrorFlag > 0)
            {
                // check the flag status
                _bFlagStatus = true; // flag is true
            }
            else
            {
                _bFlagStatus = false; // flag is false
            }

            return _bFlagStatus; // return flag status
        }

        /// <summary>
        /// Validate Sync level in [mV] of Time Harp.
        /// min value = TimeHarpDefinitions.SYNCMIN; max value = TimeHarpDefinitions.SYNCMAX.
        /// </summary>
        /// <param name="__iValueMilliVolts">
        /// The level value in [mV].
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int GetSyncLevel(int __iValueMilliVolts)
        {
            // validate m_iSyncLevel for correctness
            if ((__iValueMilliVolts < TimeHarpDefinitions.SYNCMIN) || (__iValueMilliVolts > TimeHarpDefinitions.SYNCMAX))
            {
                __iValueMilliVolts = TimeHarpDefinitions.SYNC_DEFAULT; // assign the default Sync level in [mV]
            }

            return __iValueMilliVolts;
        }

        /// <summary>
        /// Change offset - the function returns the new offset. Relevant in histogramming mode.
        /// </summary>
        /// <param name="__iDirectionOffset">
        /// minimum = -1 (down), maximum = +1 (up). The offset changes at a step size of approximately 2.5ns.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected int NextOffset(int __iDirectionOffset)
        {
            int _iOffset = this.m_iOffset;
            const int _iSetpOffset = 3; // the approximate step size of the offset, see the DLL manual for more details

            // Check if the __iDirectionOffset has a correct value, minimum = -1 (down), maximum = +1 (up)
            if (!(__iDirectionOffset == -1 || __iDirectionOffset == 0 || __iDirectionOffset == +1))
            {
                __iDirectionOffset = 0; // set default to 0 (no offset)
            }

            // Check if the __iOffset will have a correct value, we do not want to go out of range
            if (((_iOffset + __iDirectionOffset * _iSetpOffset) < TimeHarpDefinitions.OFFSETMIN)
                || ((_iOffset + __iDirectionOffset * _iSetpOffset) > TimeHarpDefinitions.OFFSETMAX))
            {
                __iDirectionOffset = 0; // assign the default direction offset value
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_NextOffset(__iDirectionOffset);

            // set up next offset of Time Harp            
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp next offset value by NextOffset() function: ");

            _iOffset = this.m_iErrorCode;

            // if the executions comes here it means, the m_iErrorCode means the new offset
            this.m_iOffset = _iOffset; // assign the new offset
            return this.m_iOffset; // return the new offset
        }

        /// <summary>
        /// Set if measurement will stop if any channel reaches stop level.
        /// This function is relevant in histogramming mode.
        /// </summary>
        /// <param name="__iStopOnOverflow">
        /// 0 - do not stop, 1 - stop on overflow.
        /// </param>
        /// <param name="__iStopLevel">
        /// Count level at which to stop (min value = TimeHarpDefinitions.OVERFLOWMIN; max value = TimeHarpDefinitions.OVERFLOWMAX).
        /// </param>
        protected void SetStopOverflow(int __iStopOnOverflow, int __iStopLevel)
        {
            if (!(__iStopOnOverflow == 0 || __iStopOnOverflow == 1))
            {
                // check if we have the correct input
                __iStopOnOverflow = 0; // in case of incorrect input set default to 0 (do not stop);
            }

            if ((__iStopLevel < TimeHarpDefinitions.OVERFLOWMIN) || (__iStopLevel > TimeHarpDefinitions.OVERFLOWMAX))
            {
                // check if we have the correct input
                __iStopLevel = TimeHarpDefinitions.OVERFLOWMAX;

                // in case of incorrect input set default to TimeHarpDefinitions.OVERFLOWMAX value;
            }

            this.m_iErrorCode = TimeHarpDefinitions.TH_SetStopOverflow(__iStopOnOverflow, __iStopLevel);

            // set on/off the Time Harp overflow as well as the overflow stop level
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp overflow values by SetStopOverflow() function: ");
        }

        /// <summary>
        /// Set Time Harp to sync mode (related to the SYNC input of Time Harp).
        /// </summary>  
        protected void SetSyncMode()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_SetSyncMode(); // set the Time Harp to sync mode
            this.CheckForException(
                this.m_iErrorCode, 
                "cannot set the Time Harp to SYNC mode by SetSyncMode() function: ");
        }

        /// <summary>
        /// Close the Time Harp device - closes the handle to the device driver so other
        /// programs can use Time Harp (note: for me it produces unhandled exception when called).
        /// </summary>  
        protected void Shutdown()
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_Shutdown(); // close Time Harp device
            this.CheckForException(this.m_iErrorCode, "cannot close Time Harp device by Shutdown() function: ");
        }

        // END OF Special functions for CONTINUOUS mode
        /////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for TTTR mode (Time Harp must have TTTR option activated/purchased)

        /// <summary>
        /// Initiate and perform DMA in a single function call (timeout = 300ms). Relevant in TTTR mode.
        /// CPU time during wait for DMA completion will be yielded to other processes/threads. 
        /// </summary>
        /// <param name="__uiDataBuffer">
        /// an array of unsigned integers (32bit) of at least 1/2 FIFOSIZE where the TTTR data can be stored (must not be accessed until the function returns).
        /// </param>
        /// <param name="__iRecordsCount">
        /// Number of TTTR records to be fetched (max 1/2 FIFOSIZE, or not larger than the __uiBufferData[] size).
        /// </param>
        /// <returns>
        /// int: returns the number of obtained records
        /// </returns>
        protected int T3RDoDMA(uint[] __uiDataBuffer, uint __iRecordsCount)
        {
            this.m_iErrorCode = TimeHarpDefinitions.TH_T3RDoDMA(__uiDataBuffer, __iRecordsCount);

            // initiate and perform DMA to get TTTR data          
            this.CheckForException(this.m_iErrorCode, "Time Harp cannot get the TTTR data by T3RDoDMA() function: ");

            return this.m_iErrorCode;

            // return the error code, if return value =0 (success), if <0 (error). If positive it means also the number of obtained records
        }

        #endregion

        // END OF Special functions for ROUTING (Must have PRT/NRT 400 Router)
        /////////////////////////////////////////////////////////////////////////////
    }
}