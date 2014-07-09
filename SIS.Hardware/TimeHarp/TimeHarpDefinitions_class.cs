using System;  //for basic functions and data types	
using System.Runtime.InteropServices;  //for DllImport and Marshalling data
using System.Text;  //for StringBuilder


namespace KUL.MDS.Hardware
{
    /// <summary>
    /// This class provides the DLL functions for interacting with Time Harp 200 board.
    /// </summary>
    public static class TimeHarpDefinitions
    {
        ///////////////////////////////////////////
        // TimeHarp DLL functions and constants. //
        ///////////////////////////////////////////

        public const string THLibName = "THLib.dll";  //the DLL library
        public const string TargetLibVersion = "6.1"; //this is what this program was written for

		public const string IdentString = "TimeHarp 200";  //board identification string
		public const string FormatVersion = "6.0";  //format version string
		public const string CreatorName = "TimeHarp Software";  //software creator name string
		public const string CreatorVersion = "6.1.0.0";  //software creator version string
		public const string CRLF = "\r\n";  //new line character
		public const string CommentField = "SIS software created the TTTR file";  //comment field - now we just note that SIS software was used to create the file
		public const string ScriptName = "ScriptName";  //script name - senseless for us

        /// <summary>
        /// The following constants are taken from thlib.defin.
        /// </summary>             
        public const int FIFOSIZE = 131072;  // Fifo buffer size = 128K = 2*2^16 event records
        public const int DMABLOCKSZ = (FIFOSIZE / 2);  // DMA block size = 2^16 = 65536  
        public const int BLOCKSIZE = 4096;  //max. histograms per block
        public const int CURVES = 32;  //max. histogram blocks
        public const int BANKSIZE = CURVES * BLOCKSIZE;  //max. histograms 

        public const int RANGES = 6;  //Number of ranges in terms of an integer number (the real time resolution is then 2^range number multiplied by Time Harp base time resolution)
        public const int DISCRMIN = 0;  //minimum discriminator level in [mV]
        public const int DISCRMAX = 400;  //maximum discriminator level in [mV]
        public const int ZCMIN = 0;  //minimum zero level in [mV]
        public const int ZCMAX = 40;  //maximum zero level in [mV]
        public const int SYNCMIN = -1300;  //minimum SYNC level in [mV]
        public const int SYNCMAX = 400;  //maximum SYNC level in [mV]
        public const int OFFSETMIN = 0;  //minimum START-STOP time offset in [ns]
        public const int OFFSETMAX = 2000;  //maximum START-STOP time offset in [ns]
        public const int ACQTMIN = 1; //minimum Acquisition time in [ms]
        public const int ACQTMAX = 36000000; //maximum Acquisition time in [ms] = 10*60*60*1000ms = 10h

        public const int SCX_XMIN = 0;  //Piezo stage (Must have SCX Controller) min. X pixels
        public const int SCX_XMAX = 4095;  //Piezo stage (Must have SCX Controller) max. X pixels
        public const int SCX_YMIN = 0;  //Piezo stage (Must have SCX Controller) min. Y pixels
        public const int SCX_YMAX = 4095;  //Piezo stage (Must have SCX Controller) max. X pixels
        public const int SCX_TPIXMIN = 100;  //us
        public const int SCX_TPIXMAX = 3200;  //us
        public const int SCX_PAUSEMIN = 0;  //pixel durations
        public const int SCX_PAUSEMAX = 15;  //pixel durations
        public const int SCX_SHUTDELMIN = 0;  //ms
        public const int SCX_SHUTDELMAX = 1000;  //ms


        /// <summary>
        /// Additional constants:
        /// </summary>        
        public const int DISCR_DEFAULT = 50;  //default discriminator level in [mV]
        public const int ZC_DEFAULT = 20;  //default zero level in [mV]
        public const int SYNC_DEFAULT = -50;  //default SYNC level in [mV]
        public const int RANGES_DEFAULT = 0;  //default Range value (min = 0, max = 5)
        public const int OFFSET_DEFAULT = 0; //default offset value in [ns]
        public const int ACQT_DEFAULT = 1000; //default acquisition time in [ms]
        public const int OVERFLOWMIN = 0;  //minimum overflow level
        public const int OVERFLOWMAX = 65535;  //maximum overflow level


        /// <summary>
        /// Status flags - a particular flag can be extracted by bitwise AND between 
        /// the complete status flag and the respective flag constant below.
        /// </summary>        
        public const int FLAG_SYSERR = 0x0100;  //=256, the bit pattern for System Error
        public const int FLAG_OVERFLOW = 0x0200;  //=512, the bit pattern for Overflow state
        public const int FLAG_RAMREADY = 0x0400;  //=1024, the bit pattern for RAM Ready state
        public const int FLAG_FIFOFULL = 0x0800;  //=2048, the bit pattern for FifoFull state
        public const int FLAG_FIFOHALFFULL = 0x1000;  //=4096, the bit pattern for FifoHalfFull state
        public const int FLAG_FIFOEMPTY = 0x2000;  //=8192, the bit pattern for FiFo Empty state
        public const int FLAG_SCANACTIVE = 0x4000;  //=16384, the bit pattern for Scan Active state


        /// <summary>
        /// Error codes returned by TimeHarp THLIB v6.1, February 2009
        /// </summary>   
        public enum THError
        {
            ERROR_NONE = 0,

            ERROR_DEVICE_OPEN_FAIL = -1,
            ERROR_DEVICE_BUSY = -2,
            ERROR_DEVICE_HEVENT_FAIL = -3,
            ERROR_DEVICE_CALLBSET_FAIL = -4,
            ERROR_DEVICE_BARMAP_FAIL = -5,
            ERROR_DEVICE_CLOSE_FAIL = -6,
            ERROR_DEVICE_RESET_FAIL = -7,
            ERROR_DEVICE_VERSION_FAIL = -8,
            ERROR_DEVICE_VERSION_MISMATCH = -9,

            ERROR_INSTANCE_RUNNING = -16,
            ERROR_INVALID_ARGUMENT = -17,
            ERROR_INVALID_MODE = -18,
            ERROR_INVALID_OPTION = -19,
            ERROR_INVALID_MEMORY = -20,
            ERROR_INVALID_RDATA = -21,
            ERROR_NOT_INITIALIZED = -22,
            ERROR_NOT_CALIBRATED = -23,
            ERROR_DMA_FAIL = -24,
            ERROR_XTDEVICE_FAIL = -25,

            ERROR_CALIB_MAXRET_FAIL = -32,
            ERROR_CALIB_REGINIT_FAIL = -33,
            ERROR_CALIB_CVALERRBIT = -34,
            ERROR_CALIB_MVALERRBIT = -35,
            ERROR_CALIB_MFIT_FAIL = -36,
            ERROR_CALIB_MINVALID = -37,

            ERROR_SCX_NOT_PRESENT = -48,
            ERROR_SCX_NOT_ENABLED = -49,
            ERROR_SCX_RESET_FAIL = -50,
            ERROR_SCX_NOT_ACTIVE = -51,
            ERROR_SCX_STILL_ACTIVE = -52,
            ERROR_SCX_GOTOXY_FAIL = -53,
            ERROR_SCX_SCPROG_FAIL = -54,

            ERROR_HARDWARE_F01 = -64,
            ERROR_HARDWARE_F02 = -65,
            ERROR_HARDWARE_F03 = -66,
            ERROR_HARDWARE_F04 = -67,
            ERROR_HARDWARE_F05 = -68,
            ERROR_HARDWARE_F06 = -69,
            ERROR_HARDWARE_F07 = -70,
            ERROR_HARDWARE_F08 = -71,
            ERROR_HARDWARE_F09 = -72,
            ERROR_HARDWARE_F10 = -73,
            ERROR_HARDWARE_F11 = -74,
            ERROR_HARDWARE_F12 = -75,
            ERROR_HARDWARE_F13 = -76,
            ERROR_HARDWARE_F14 = -77,
            ERROR_HARDWARE_F15 = -78,
        }


        /// <summary>
        /// The following structures are used to hold the Time Harp header file data
        /// They reflect the TTTR file Header structure.          
        /// </summary>

        [StructLayout(LayoutKind.Sequential)]
        public struct tParamStruct
        {
            public float Start;
            public float Step;
            public float End;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct tCurveMapping
        {
            public Int32 MapTo;
            public Int32 Show;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TextHeader  //the following represents the readable ASCII file header portion (header length is 328 bytes)
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char[] Ident;  //char[16], the string 'Time Harp'

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public char[] FormatVersion;  //char[6], currently '6.0'

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public char[] CreatorName;  //char[18], 'TimeHarp Software' - use your own if you create such file

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public char[] CreatorVersion;  //char[12], currently '6.1.0.0' - use your own if you create such file

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public char[] FileTime;  //char[18], the file creation date and time

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
            public char[] CRLF;  //char[2], 0x0D, 0x0A carriage return, line feed

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public char[] CommentField;  //char[256], any ASCII string
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BinaryHeader  //the following is binary header information (header length is 212 bytes)
        {
            public Int32 NumberOfChannels;  //number of time channels (normally 4096). Note that it may be needed to read this value before further processing a TTTR file
            public Int32 NumberOfCurves;  //meaningless in TTTR file. Note that it may be needed to read this value before further processing a TTTR file
            public Int32 BitsPerChannel;  //meaningless in TTTR file. Note that it may be needed to read this value before further processing a TTTR file
            public Int32 RoutingChannels;  //1 to 4
            public Int32 NumberOfBoards;  //reserved, now 1. Note that it may be needed to read this value before further processing a TTTR file
            public Int32 ActiveCurve;  //meaningless in TTTR file
            public Int32 MeasurementMode;  //2=TTTR
            public Int32 SubMode;  //0=Standard (Timed), 1 or 2 = reserved, 3=Image
            public Int32 RangeNo;  //0=base resolution, 1=x2, 2=x4, 3=x8, 4=x16, 5=32
            public Int32 Offset; //offset (approx.) in [ns]
            public Int32 AcquisitionTime;  //acquisition time in [ms]
            public Int32 StopAt;  //meaningless in TTTR file
            public Int32 StopOnOvfl;  //meaningless in TTTR file
            public Int32 Restart;  //meaningless in TTTR file
            public Int32 DispayLinLog;  //lin=0, log=1
            public Int32 DisplayTimeAxisFrom;  //lower time axis bound for display in [ns]
            public Int32 DisplayTimeAxisTo;  //upper time axis bound for display in [ns]
            public Int32 DisplayCountsAxisFrom;  //lower count axis bound for display
            public Int32 DisplayCountsAxisTo;  //upper count axis bound for display

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public tCurveMapping[] DisplayCurves;  //tCurveMapping[8], meaningless in TTTR file
			
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 3)]
            public tParamStruct[] Params;  //tParamStruct[3], reserved for automated measurements
			
            public Int32 RepeatMode;  //reserved for automated measurements
            public Int32 RepeatsPerCurve;  //reserved for automated measurements
            public Int32 RepeatTime;  //reserved for automated measurements
            public Int32 RepeatWaitTime;  //reserved for automated measurements

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public char[] ScriptName;  //char[20], reserved for automated measurements
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BoardHeader  //the following is board header information (header length is 48 bytes)
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char[] HardwareIdent;  //char[16], 'Time Harp'

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public char[] HardwareVersion;  //char[8], currently '2.4' or '2.5'

            public Int32 BoardSerial;  //Board serial number
            public Int32 CFDZeroCross;  //CFD zero cross level in [mV]
            public Int32 CFDDiscrMin;  //CFD min discriminator level in [mV]
            public Int32 SyncLevel;  //SYNC trigger level in [mV]
            public Int32 CurveOffset;  //reserved, now 0             
            public float Resolution;  //resolution in [ns]
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TTTRHeader  //the following is TTTR header information (header length is 52 bytes) 
        {
            public Int32 TTTRGlobclock;  //clock in [ns], normally 100
            public Int32 ExtDevices;  //1 = PRT 400, 2 = NRT 400
            public Int32 Reserved1;  //reserved field for future or custom use
            public Int32 Reserved2;  //reserved field for future or custom use
            public Int32 Reserved3;  //reserved field for future or custom use
            public Int32 Reserved4;  //reserved field for future or custom use
            public Int32 Reserved5;  //reserved field for future or custom use
            public Int32 SyncRate;  //SYNC rate as displayed in meter
            public Int32 AverageCFDRate;  //Average CFD rate as displayed in meter
            public Int32 StopAfter;  //Stopped after this many ms
            public Int32 StopReason;  //0=TimeOver, 1=Manual, 2=Overflow, 3=Error
            public Int32 NumberOfRecords;  //Number of TTTR records. Note that it may be needed to read this value before further processing a TTTR file
            public Int32 SpecialHeaderLength;  //Length of special header to follow (in 4 bytes portion - -> currently we predefine it and write there the custom SIS header). Note that you must skip this header length to get to the TTTR records
        }

		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Ansi )]
        public struct CustomSISHeader  //the following is the custom SIS header information (header length is 140 bytes) - we place this in the special header
        {
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 8 )]
			public char[] Version;  // char[8], version of the SIS header, currently "0.1"

            public double TimePPixel;  // time per pixel in [ms]

            public double InitXNm;  // initial offset along X axis in [nm]
            public double InitYNm;  // initial offset along Y axis in [nm]
            public double InitZNm;  // initial offset along Z axis in [nm]

            public double XScanSizeNm;  // scan size along X axis in [nm] 
            public double YScanSizeNm;  // scan size along Y axis in [nm] 
            public double ZScanSizeNm;  // scan size along Z axis in [nm] 

            public Int32 ImageWidthPx;  // image size along X axis in [px] 
            public Int32 ImageHeightPx;  // image size along Y axis in [px] 
            public Int32 ImageDepthPx;  // image size along Z axis in [px] 

            public Int32 XOverScanPx;  // over scan image size along X axis in [px] 
            public Int32 YOverScanPx;  // over scan image size along Y axis in [px] 
            public Int32 ZOverScanPx;  // over scan image size along Z axis in [px] 

            public Int32 SISChannels;  // number of channels that SIS can show/process at the same time

            public Int32 TypeOfScan;  //the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)

            public Int32 FrameTimeOut;  // frame time out - the period after which we force the processing of the raw counts and extract pixels
            public Int32 FiFoTimeOut;  // Time Harp's FiFo time out - the period after which we force the fetching of the raw events in the FiFo buffer

            public byte StackMarker;  //value of external marker interpreted as a Stack marker
            public byte FrameMarker;  //value of external marker interpreted as a Frame marker (note that Frame marker is also a line marker)
            public byte LineMarker;  //value of external marker interpreted as a Line marker
            public byte PixelMarker;  //value of external marker interpreted as a Pixel marker

            public double GalvoMagnificationObjective;  //the magnification of the objective
            public double GalvoScanLensFocalLength;  //the focal length of the scan lens in [mm]            
            public double GalvoRangeAngleDegrees;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            public double GalvoRangeAngleInt;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
        }

        /// <summary>
        /// The following structures are used to hold the TimeHarp file data
        /// They reflect the file Header structure.          
        /// </summary>            
        public struct StructTTTRFileHeader  //(header length is 640+140 bytes)
        {
			// Default TTTR header part
            public TextHeader TextHeader;  //the following represents the readable ASCII file header portion (header length is 328 bytes)
            public BinaryHeader BinaryHeader;  //the following is binary header information (header length is 212 bytes)
            public BoardHeader BoardHeader;  //the following is board header information (header length is 48 bytes)
            public TTTRHeader TTTRHeader;  //the following is TTTR header information (header length is 52 bytes)
 
			// CUstom TTTR header part
            public CustomSISHeader CustomSISHeader;  //the following is SIS header information (header length is 140 bytes)  
        }

		public const int SizeTTTRFileHeader = 780;  //the size of the TTTR header in bytes
        public const int SizeTTTRDefaultFileHeader = 640;  //the size of the default TTTR header in bytes
        public const int SizeTTTRCustomSISFileHeader = 140;  //the size of the TTTR custom SIS header in bytes
		public const string VersionTTTRCustomSISFileHeader = "0.1";  //the version of the TTTR custom SIS header
        public const int MAXIMUM_TTTR_RECORDS_PER_FILE = 268435456;  //=2^28 (= 268 435 456) maximum records per TTTR file allowed




        #region Time Harp THLib DLL functions

        /// <summary>
        /// DLL initialization and common functions:
        /// DLL initialization and common functions:
        /// DLL initialization and common functions:
        /// </summary> 


        /// <summary>
        /// THLib function "TH_GetErrorString" - it converts error code (errcode) into meaningful error string (errstring).
        /// </summary>
        /// <param name="errstring">The returned error string.</param>  
        /// <param name="errcode">Error code.</param> 
        /// <returns>int: if return value >0 (success), if <0 (error)</returns>   
        [DllImport(THLibName, EntryPoint = "TH_GetErrorString")]
        public static extern int TH_GetErrorString(StringBuilder errstring, int errcode);


        /// <summary>
        /// THLib function "TH_GetLibraryVersion" - it returns the library version into vers variable.
        /// </summary>
        /// <param name="vers">The returned library version as a string.</param>
        /// <returns>return value = integer; if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetLibraryVersion")]
        public static extern int TH_GetLibraryVersion(StringBuilder vers);


        /// <summary>
        /// THLib function "TH_Initialize" - it sets up the Time Harp acquisition mode.       
        /// </summary>
        /// <param name="mode">mode = 0 or 1 (0 - standard histogramming; 1 - TTTR mode).</param>        
        /// <returns>int: if return value = 0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_Initialize")]
        public static extern int TH_Initialize(int mode);


        /// <summary>
        /// THLib function "TH_GetHardwareVersion" - it returns the Hardware (Time Harp) version into vers variable.
        /// </summary>
        /// <param name="vers">the Hardware (Time Harp) version.</param>        
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>    
        [DllImport(THLibName, EntryPoint = "TH_GetHardwareVersion")]
        public static extern int TH_GetHardwareVersion(StringBuilder vers);


        /// <summary>
        /// THLib function "TH_GetSerialNumber" - it returns the serial number of Time Harp into serial variable.
        /// </summary>
        /// <param name="serial">serial number of Time Harp.</param>        
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetSerialNumber")]
        public static extern int TH_GetSerialNumber(StringBuilder serial);


        /// <summary>
        /// THLib function "TH_GetBaseResolution" - it returns the approximated value of the Time Harp time resolution (used before calibration).
        /// </summary>        
        /// <returns>int: if return value >0 (the approximated value of the time resolution), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBaseResolution")]
        public static extern int TH_GetBaseResolution();


        /// <summary>
        /// THLib function "TH_Calibrate" - calibrate Time Harp (always must be done before the start of a measurement session).
        /// </summary>     
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_Calibrate")]
        public static extern int TH_Calibrate();


        /// <summary>
        /// THLib function "TH_SetCFDDiscrMin" - it sets CFD level, value is in millivolts; value (minimum) = DISCRMIN, value (maximum) = DISCRMAX.
        /// </summary>
        /// <param name="value">CFD level, value is in [mV]; value (minimum) = DISCRMIN, value (maximum) = DISCRMAX.</param>        
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_SetCFDDiscrMin")]
        public static extern int TH_SetCFDDiscrMin(int value);


        /// <summary>
        /// THLib function "TH_SetCFDZeroCross" - it sets CFD zero cross level, value is in millivolts; value (minimum) = ZCMIN, value (maximum) = ZCMAX.
        /// </summary>
        /// <param name="value"> CFD zero cross level, value is in [mV]; value (minimum) = ZCMIN, value (maximum) = ZCMAX.</param>        
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_SetCFDZeroCross")]
        public static extern int TH_SetCFDZeroCross(int value);


        /// <summary>
        /// THLib function "TH_SetSyncLevel" - sets sync level, value in millivolts; value (minimum) = SYNCMIN, value (maximum) = SYNCMAX.
        /// </summary>
        /// <param name="value">sync level value in [mV], value (minimum) = SYNCMIN, value (maximum) = SYNCMAX</param>        
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_SetSyncLevel")]
        public static extern int TH_SetSyncLevel(int value);


        /// <summary>
        /// THLib function "TH_SetStopOverflow" - the arguments determine if a measurement run will stop if any channel reaches the
        /// stop level (StopOnOfl = 0 (do not stop); StopOnOfl = 1 (stop on overflow); StopLevel - count level at which to stop (min 0, max 65535)).
        /// </summary>
        /// <param name="StopOnOfl">controls to stop or not if overflow occurs, (StopOnOfl = 0 (do not stop), StopOnOfl = 1 (stop on overflow).</param>        
        /// <param name="StopLevel">count level at which to stop (min 0, max 65535).</param> 
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_SetStopOverflow")]
        public static extern int TH_SetStopOverflow(int StopOnOfl, int StopLevel);


        /// <summary>
        /// THLib function "TH_SetRange" - it sets measurement range.
        /// </summary>
        /// <param name="range">range = 0 (base resolution), range = 1 (2x base resolution) etc. up to range = RANGES - 1 (largest resolution in multiple of the base resolution).</param>
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_SetRange")]
        public static extern int TH_SetRange(int range);


        /// <summary>
        /// THLib function "TH_SetOffset" - it sets new offset. 
        /// The new offset is approximation of the desired offset (the typical step size is around 2.5ns).
        /// </summary> 
        /// <param name="offset">offset (minimum) = OFFSETMIN, offset (maximum) = OFFSETMAX.</param>
        /// <returns>int: if return value >=0 (new offset), if <0 (error)</returns> 
        [DllImport(THLibName, EntryPoint = "TH_SetOffset")]
        public static extern int TH_SetOffset(int offset);


        /// <summary>
        /// THLib function "TH_NextOffset" - it changes offset. The offset changes at a step size of approximately 2.5ns.
        /// </summary> 
        /// <param name="direction">direction (minimum) = -1 (down), direction (maximum) = +1 (up).</param>
        /// <returns>int: if return value >=0 (new offset), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_NextOffset")]
        public static extern int TH_NextOffset(int direction);


        /// <summary>
        /// THLib function "TH_ClearHistMem" - it clears histogram memory.
        /// </summary> 
        /// <param name="block">Number of blocks to clear (always 0 if not routing).</param>
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_ClearHistMem")]
        public static extern int TH_ClearHistMem(int block);


        /// <summary>
        /// THLib function "TH_SetMMode" - it sets measurement mode.        
        /// </summary>
        /// <param name="mmode">Measurement mode, mmode = 0 (one-time histogramming and TTTR modes), mmode = 1 (continuous mode).</param>  
        /// <param name="tacq">Acquisition time in milliseconds (set this to 0 for continuous mode with external clock).</param> 
        /// <returns>int: if return value =0 (success), if <0 (error)</returns> 
        [DllImport(THLibName, EntryPoint = "TH_SetMMode")]
        public static extern int TH_SetMMode(int mmode, int tacq);


        /// <summary>
        /// THLib function "TH_StartMeas" - it starts measurement/data acquisition.
        /// </summary>     
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_StartMeas")]
        public static extern int TH_StartMeas();


        /// <summary>
        /// THLib function "TH_StopMeas" - it stops measurement/data acquisition. 
        /// Note that in TTTR mode this also rests the hardware FIFO.
        /// </summary>
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_StopMeas")]
        public static extern int TH_StopMeas();


        /// <summary>
        /// THLib function "TH_CTCStatus" - it returns the acquisition time status.
        /// </summary>       
        /// <returns>int: if return value =0 (acquisition time still running), if >0 (acquisition time has ended), if <0 (error)</returns>   
        [DllImport(THLibName, EntryPoint = "TH_CTCStatus")]
        public static extern int TH_CTCStatus();


        /// <summary>
        /// THLib function "TH_SetSyncMode" - it sets the SYNC mode. 
        /// Note that this function must be called before GetCountRate() if the sync rate is to
        /// be measured. Allow at least 600ms after this call to get a stable sync rate reading.
        /// </summary>       
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>   
        [DllImport(THLibName, EntryPoint = "TH_SetSyncMode")]
        public static extern int TH_SetSyncMode();


        /// <summary>
        /// THLib function "TH_GetBlock" - returns the total number of counts in this histogram.
        /// The current version counts only up to 65535 (16 bits).
        /// </summary> 
        /// <param name="chcount[]">An array of unsigned integers (32bit) of BLOCKSIZE where the histogram can be stored.</param>
        /// <param name="block">Block number (0..3) to fetch (always 0 if not routing).</param>        
        /// <returns>int: if return value >=0 (total number of counts in this histogram), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBlock")]
        public static extern int TH_GetBlock(uint[] chcount, int block);


        /// <summary>
        /// THLib function "TH_GetResolution" - it returns the channel width in current range (from last calibration) in [ns].
        /// </summary>           
        /// <returns>if return value >0 (the resolution), if <0 (error)</returns> 
        [DllImport(THLibName, EntryPoint = "TH_GetResolution")]
        public static extern float TH_GetResolution();


        /// <summary>
        /// THLib function "TH_GetCountRate" - returns the current count rate or sync rate. 
        /// The function returns either the current count rate if previously TH_SetMMode() was called or 
        /// the current sync rate if previously TH_SetSyncMode() was called. Allow at least 600ms to get a
        /// stable rate meter reading in both cases.
        /// </summary> 
        /// <returns>int: if return value >=0 (current count rate or sync rate, see notes above), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetCountRate")]
        public static extern int TH_GetCountRate();


        /// <summary>
        /// THLib function "TH_GetFlags" - returns the current status flags (a bit pattern).
        /// Use the predefined flags (e.g. FLAG_OVERFLOW) and bitwise AND to extract individual bits (you can call this function anytime during a measurement but not during DMA).
        /// </summary> 
        /// <returns>int: returns current status flags (a bit pattern)</returns>  
        [DllImport(THLibName, EntryPoint = "TH_GetFlags")]
        public static extern int TH_GetFlags();


        /// <summary>
        /// THLib function "TH_GetElapsedMeasTime" - returns the elapsed measurement time in ms (not for continuous mode).
        /// </summary> 
        /// <returns>int: returns the elapsed measurement time in ms (not for continuous mode)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetElapsedMeasTime")]
        public static extern int TH_GetElapsedMeasTime();


        /// <summary>
        /// THLib function "TH_Shutdown" - it closes the device (closes the handle to Time Harp device driver).
        /// </summary> 
        /// <returns>int: if return value >0 (success), if <0 (error)</returns>    
        [DllImport(THLibName, EntryPoint = "TH_Shutdown")]
        public static extern int TH_Shutdown();


        /////////////////////////////////////////////////////////////////////////////
        // Special functions for CONTINUOUS mode

        /// <summary>
        /// THLib function "TH_GetBank" - it returns the sum of all counts in the channels fetched.
        /// </summary>
        /// <param name="buffer[]">An array of words (16bit) of BANKSIZE where the histogram data can be stored</param>
        /// <param name="chanfrom">Lower limit of the channels to be fetched per histogram (min 0, max 4095, and chanfrom<=chanto)</param>
        /// <param name="chanto">Upper limit of the channels to be fetched per histogram (min 0, max 4095, and chanfrom<=chanto)</param>
        /// <returns>int: if return value >0 (sum of all counts in the channels fetched), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBank")]
        public static extern int TH_GetBank(ushort[] buffer, int chanfrom, int chanto);


        /// <summary>
        /// THLib function "TH_GetLostBlocks" - it returns either the number of lost blocks (if >0) in continuous mode run or an error (if <0).
        /// </summary> 
        /// <returns>int: returns either the number of lost blocks (if >0) in continuous mode run or an error (if <0)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetLostBlocks")]
        public static extern int TH_GetLostBlocks();


        /////////////////////////////////////////////////////////////////////////////
        // Special functions for TTTR mode (Time Harp must have TTTR option activated/purchased)

        /// <summary>
        /// THLib function "TH_T3RDoDMA" - it initiates and performs DMA in a single function call (timeout = 300ms). 
        /// CPU time during wait for DMA completion will be yielded to other processes/threads.
        /// </summary> 
        /// <param name="buffer[]">an array of dwords (32bit) of at least 1/2 FIFOSIZE where the TTTR data can be stored (buffer[] must not be accessed until the function returns).</param>
        /// <param name="count">number of TTTR records to be fetched (max 1/2 FIFOSIZE), count must not be larger than the buffer[] size.</param>
        /// <returns>int: if return value >=0 (success and equals the number of records obtain), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RDoDMA")]
        public static extern int TH_T3RDoDMA(uint[] buffer, uint count);


        /// <summary>
        /// THLib function "TH_T3RStartDMA" - it initiates DMA but returns before completion.
        /// No other calls to THLib are allowed between this call and the return of a subsequent call to T3RCompleteDMA().
        /// </summary> 
        /// <param name="buffer[]">an array of dwords (32bit) of at least 1/2 FIFOSIZE where the TTTR data can be stored (buffer[] must not be accessed until the function returns).</param>
        /// <param name="count">number of TTTR records to be fetched (max 1/2 FIFOSIZE), count must not be larger than the buffer[] size.</param>
        /// <returns>int: if return value =0 (success), if <0 (error)</returns>   
        [DllImport(THLibName, EntryPoint = "TH_T3RStartDMA")]
        public static extern int TH_T3RStartDMA(uint[] buffer, uint count);


        /// <summary>
        /// THLib function "TH_T3RCompleteDMA" - it returns the number of transferred records (if >=0) or error (if <0). 
        /// The function waits for DMA to complete. CPU time during wait for DMA completion will be yielded to other processes/threads.
        /// Function returns after timeout period of 300ms, even if not all data could be fetched. buffer[] past in T3RStartDMA() must 
        /// not be accessed until T3RCompleteDMA().
        /// </summary> 
        /// <returns>int: if return value >=0 (success, the number of records obtained), if <0 (error)</returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RCompleteDMA")]
        public static extern int TH_T3RCompleteDMA();


        /// <summary>
        /// THLib function "TH_T3RSetMarkerEdges" - it sets the active edges of the TTL signals that will appear as markers in TTTR data stream.
        /// Three markers available, me0/me1/me2 = 0 (falling edge) or 1 (rising edge).
        /// </summary> 
        /// <param name="me0">Marker 0, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param>        
        /// <param name="me1">Marker 1, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param>
        /// <param name="me2">Marker 2, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param> 
        [DllImport(THLibName, EntryPoint = "TH_T3RSetMarkerEdges")]
        public static extern int TH_T3RSetMarkerEdges(int me0, int me1, int me2);


        /////////////////////////////////////////////////////////////////////////////
        // Special functions for ROUTING (Must have PRT/NRT 400 Router)

        /// <summary>
        /// THLib function "TH_GetRoutingChannels" - it returns the number of routing channels available (if >0) or error (if <0).
        /// </summary>      
        /// <returns>int: returns the number of routing channels available (if >0, 1 = no router) or error (if <0)</returns>   
        [DllImport(THLibName, EntryPoint = "TH_GetRoutingChannels")]
        public static extern int TH_GetRoutingChannels();


        /// <summary>
        /// THLib function "TH_EnableRouting" - it returns the number of routing channels available (if >0) or error (if <0).
        /// </summary> 
        /// <param name="enable">enable = 0 (disable routing) or 1 (enable routing).</param>        
        /// <returns>int: if return value =0 (success), if <0 (most likely: no router found)</returns> 
        [DllImport(THLibName, EntryPoint = "TH_EnableRouting")]
        public static extern int TH_EnableRouting(int enable);


        /// <summary>
        /// THLib function "TH_SetNRT400CFD" - (Note: Must have NRT Router) set NRT 400 CFD level.
        /// </summary> 
        [DllImport(THLibName, EntryPoint = "TH_SetNRT400CFD")]
        public static extern int TH_SetNRT400CFD(int channel, int level, int zerocross);


        /////////////////////////////////////////////////////////////////////////////
        // Special functions for SCANNING with Piezo stage (Must have SCX Controller)

        /// <summary>
        /// THLib function "TH_ScanEnable" - disable (enable=0) or enable (enable=1) scan.
        /// </summary> 
        /// <param name="enable">disable (enable=0) or enable (enable=1) scan</param>        
        /// <returns>int: if return value = 0 (success), if <0 (most likely: no scan controller found)</returns>    
        [DllImport(THLibName, EntryPoint = "TH_ScanEnable")]
        public static extern int TH_ScanEnable(int enable);


        /// <summary>
        /// THLib function "TH_ScanGotoXY" - go to position (x=0..4095, y=0..4095) with shutter = 1/0 (TTL shutter output high/low).
        /// </summary> 
        /// <param name="x">position x=0..4095.</param>
        /// <param name="y">position y=0..4095.</param>
        /// <param name="shutter">shutter = 1/0 (TTL shutter output high/low).</param> 
        /// <returns>int: if return value = 0 (success), if <0 (error)</returns>     
        [DllImport(THLibName, EntryPoint = "TH_ScanGotoXY")]
        public static extern int TH_ScanGotoXY(int x, int y, int shutter);


        /// <summary>
        /// THLib function "TH_ScanDone" - check the scan status.
        /// </summary>        
        /// <returns>int: if return value = 0 (success -> done), if <0 (error -> not finished)</returns>  
        [DllImport(THLibName, EntryPoint = "TH_ScanDone")]
        public static extern int TH_ScanDone();


        /// <summary>
        /// THLib function "TH_ScanSetup" - setup scan frame (scan is performed the next run of TTTR mode).
        /// </summary> 
        /// <param name="startx">starting position x=0..4095.</param>
        /// <param name="starty">starting position y=0..4095.</param>
        /// <param name="widthx">scan region width x=2..4096.</param>
        /// <param name="widthy">scan region width y=2..4096.</param>
        /// <param name="pixtime">pixel dwell time in us (100..3200).</param>
        /// <param name="pause">pause at turning position of x axis (0..15).</param>
        /// <param name="shutterdel">shutter delay to start of scan in ms (0..1000).</param> 
        /// <returns>int: if return value = 0 (success), if <0 (error)</returns>  
        [DllImport(THLibName, EntryPoint = "TH_ScanSetup")]
        public static extern int TH_ScanSetup(int startx, int starty, int widthx, int widthy, int pixtime, int pause, int shutterdel);


        /// <summary>
        /// THLib function "TH_ScanReset" - it resets scanner.
        /// This call is only required at program start or recover from an unexpected error or crash.
        /// </summary>        
        /// <returns>int: if return value = 0 (success), if <0 (error)</returns>     
        [DllImport(THLibName, EntryPoint = "TH_ScanReset")]
        public static extern int TH_ScanReset();


        #endregion Time Harp THLib DLL functions

    }





}
