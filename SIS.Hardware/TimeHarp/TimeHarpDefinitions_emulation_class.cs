using System;  //for basic functions and data types	
using System.IO; //for File operations
using System.Runtime.InteropServices;  //for DllImport and Marshalling data
using System.Text;  //for StringBuilder 
using System.Threading;  //for multi-threading


namespace KUL.MDS.Hardware
{
	/// <summary>
	/// This class provides EMULATION of the DLL functions for interacting with Time Harp 200 board.
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

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public struct CustomSISHeader  //the following is the custom SIS header information (header length is 140 bytes) - we place this in the special header
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
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



		#region Emulation of the Time Harp 200 THLib DLL functions

		////////////////////////////////////////////////////////////////////////////
		////////////// BEGIN Emulation of the Time Harp 200 THLib DLL functions

		/// <summary>
		/// Emulation of the Time Harp THLib DLL functions.
		/// It allows testing of Time Harp 200 related code
		/// without having installed the Time Harp device.
		/// Note that not all functions are emulated but just
		/// the most important ones.
		/// 
		/// In order to use the emulation functions replace the DLL functions class with this one.
		/// Also if it is necessary you can readjust here some of the parameters and file path to TTTR files.
		/// </summary>

		private const string TIME_HARP_EMULATION_TTTR_FILE = "tttr_binary_file(time_harp_emulation)_-_one_frame.t3r";  //file name of the TTTR binary file used to supply raw records for the emulation of Time Harp
		private const int THREAD_SLEEP_TIME = 5;  //put the thread to sleep for about THREAD_SLEEP_TIME [ms] (used in GetDataIntoFiFo() function)
		private const int FIFO_SIZE_RECORDS_BLOCK = 4096;  //block of records to read at once
		private const bool STOP_ON_NUMBER_OF_RECORDS = true;  //whether or not to stop if we reach the end of TTTR file (this will cause one-to-one emulation with the respective TTTR emulation file)

		private static volatile bool m_IsMesurementToBeStop = false;  //allows to stop filling FiFo by setting its value to true
		private static volatile bool m_bMeasurementRunning = false;  //measurement running status (initially set to not running)

		private static int m_iMeasurementTimeCounter = 0;  //in [ms] - used to measure the elapsed time
		private static int m_iAcquisitionTime = 0;  //acquisition time in [ms] (initially zero)
		//private static int m_iTimeCounter = 0;  //in [ms] - used to measure to introduce timeout  
		private static int m_iTimeCounterStart = 0;  //in [ms] - used to measure time 
		private static int m_iTimeCounterStop = 0;  //in [ms] - used to measure time  

		private const int TIME_HARP_BASE_RESOLUTION = 30;  //base resolution in [ps] of Time Harp board (before calibration)
		//private const float TIME_HARP_RESOLUTION = 0.030f;  //Time Harp resolution after calibration        

		private static bool m_IsCalibrated = false;  //calibration status of Time Harp (initially indicates it is not yet calibrated)
		//private static bool m_IsInitialized = false;  //initialization status of Time Harp (initially indicates it is not yet initialized)
		private static bool m_IsSyncMode = false;  //indicates if we have set to sync mode or not (necessary if you want to get the sync rate)
		private static bool m_bIsBidirectionalScan = false;  //indicates if we have bidirectional scan (=true) or unidirectional scan (=false)

		private static int m_iNumberOfRecords = 0;  //number of records in the TTTR file used for emulation of Time Harp

		private static string m_sHardwareVersion = "";  //Time Harp hardware version
		private static string m_sSerialNumber = "";  //Time Harp current serial number
		private static float m_fResolution = 0;  //resolution of Time Harp after calibration (initially zero)
		private static int m_iOffset = 0;  //offset
		private static int m_iBlocksToClear = 0;  //number of blocks to clear from histogram memory (equals zero if not routing)
		private static int m_iCountRate = 0;  //count rate (initially zero)
		private static int m_iSyncRate = 0;  //sync rate (initially zero)

		private static int m_iCFDDiscrMin = 0;  //CFD discrimination voltage level in [mV] (initially zero)
		private static int m_iCFDZeroCross = 0;  //CFD zero cross voltage level in [mV] (initially zero)
		private static int m_iSyncLevel = 0;  //Sync voltage level in [mV] (initially zero)
		private static int m_iStopOnOfl = 0;  //Stop or not on overflow (StopOnOfl = 0 (do not stop); StopOnOfl = 1 (stop on overflow)
		private static int m_iStopLevel = 0;  //Stop level - count level at which to stop (min 0, max 65535)

		private static int m_iRange = 0;  //range = 0 (base resolution), range = 1 (2x base resolution) etc. up to range = RANGES - 1 (largest resolution in multiple of the base resolution)
		private static int m_iMeasurementMode = -1;  //0 - one-time histogramming mode, and 1 - TTTR mode

		private static double m_dTimePPixel = 0.0;  //time per pixel in [ms]
		private static int m_iXWidth = 0;  //number of pixels per line
		private static int m_iYHeight = 0;  //number of lines to scan
		private static int m_iGatingTimeChannel = 0;  //the gating time in channel number (then the true time is m_fResolution * GatingTimeChannel). m_fResolution is the resolution of the time tag in [ns].



		/// <summary>
		/// Structure that keeps track of important files parameters and operations with files.
		/// </summary>            
		private struct Files
		{
			public const int MAXIMUM_TTTR_RECORDS = MAXIMUM_TTTR_RECORDS_PER_FILE;  //maximum records per TTTR file allowed
			public static string TTTRFileName = "";  //file name of the TTTR binary file
			public static string ASCIIFileName = "";  //file name of the output data file

			public static int FileCounter = 0;  // counts how many output files we have written to hard disk so far
			public static int RecordsCount = 0;  // counts how many records we have written to a file so far

			public static bool isTTTRFileFirstWrite = true;  //keep track of the TTTR file state - also it indicates if we have started to record the TTTR data or not
			public static Encoding ASCIIEncode = Encoding.ASCII;  //the Encoding class represents the char set encoding (.Net uses Unicode, i.e. UTF-16). For proper writing/reading from a TTTR file we need to use the methods of this class to convert between a .Net 16-bit char (Unicode) and 8-bit char (ASCII) and vice versa.

			public static StructTTTRFileHeader TTTRFileHeader;  //the header structure of Time Harp TTTR file
		}


		/// <summary>
		/// Structure to keep the threads' data/status/info. This is used to interact with the created threads.
		/// </summary>             
		private struct ThreadsStatus
		{
			public static readonly object ThreadLocker = new object();  //used to lock shared variables (for synchronization between threads)

			public struct GetFiFoThread  //keeps track of the thread that spawns GetDataIntoFiFo() function (that fetches the TTTRrecords from file into FiFo buffer)
			{
				public static Thread Thread;  //refer to the thread that spawns GetDataIntoFiFo() function (that fetches the TTTRrecords from file into FiFo buffer)
				//public static volatile bool IsRunning = false;  //keep track if the thread is still running
				public static volatile UInt32[] ui32FiFoBuffer = new UInt32[2 * DMABLOCKSZ];  //Time Harp FIFO data records (full buffer length)
				public static readonly object ThreadLockerFiFoBuffer = new object();  //used to lock FiFo buffer (for synchronization between threads)
				public static volatile int IndexFiFoBufferLowerBound = 0;  //keeps track of the processing of the buffer
				public static volatile int IndexFiFoBufferUpperBound = 0;  //keeps track of the processing of the buffer          
				public static volatile int FiFoBufferNumberOfRecords = 0;  //keeps track of the number of records to be transferred from the FiFo buffer to a supplied buffer

				public static readonly object ThreadLockerStatusFlags = new object();  //used to lock status flags variables (for synchronization between threads)
				public static volatile int StatusFlags = 0;  //the returned integer that represents the status of FiFo                
			}

			public struct DMAReadThread  //keeps track of the thread that spawns TH_T3RStartDMA() function (that gets the ui32TTTRBuffer[] buffer into the user supplied buffer)                
			{
				public static Thread Thread;  //refer to the thread that spawns TH_T3RStartDMA() function
				//public static volatile bool IsRunning = false;  //keep track if the thread is still running                                              
			}
		}


		/// <summary>
		/// Read the Time Harp 200 header info from a TTTR file.
		/// </summary> 
		/// <param name="__sInputFile">The name of the input TTTR file to be read.</param>
		/// <param name="__TTTRFileHeader">A TTTR file header structure where the info from TTTR file header will be stored.</param>
		private static void ReadTTTRHeaderFromFile(string __sInputFile, ref TimeHarpDefinitions.StructTTTRFileHeader __TTTRFileHeader)
		{
			// Declare file stream variables
			FileStream _fsInputFileStream;  //declare file stream
			BinaryReader _brInputFile;  //declare the binary reader

			try  //open the TTTR file for reading
			{
				_fsInputFileStream = File.Open(__sInputFile, FileMode.Open, FileAccess.Read, FileShare.Read); //open the input file for reading                        
				_brInputFile = new BinaryReader(_fsInputFileStream);  //allocate the binary reader - makes the actual reading of the binary data from hard drive
			}
			catch (Exception)
			{
				throw new Exception("KUL.MDS.Hardware.PQTimeHarp. Exception - " + "The file: " + __sInputFile + " could not be opened by ReadTTTRHeaderFromFile() function!");
			}


			// Read Text header from the TTTR file (header length is 328 bytes)            
			__TTTRFileHeader.TextHeader.Ident = _brInputFile.ReadChars(16);  //read char[16] from file
			__TTTRFileHeader.TextHeader.FormatVersion = _brInputFile.ReadChars(6);  //read char[6] from file
			__TTTRFileHeader.TextHeader.CreatorName = _brInputFile.ReadChars(18);  //read char[18] from file
			__TTTRFileHeader.TextHeader.CreatorVersion = _brInputFile.ReadChars(12);  //read char[12] from file
			__TTTRFileHeader.TextHeader.FileTime = _brInputFile.ReadChars(18);  //read char[18] from file
			__TTTRFileHeader.TextHeader.CRLF = _brInputFile.ReadChars(2);  //read char[2] from file
			__TTTRFileHeader.TextHeader.CommentField = _brInputFile.ReadChars(256);  //read char[256] from file

			// Read Binary header to the TTTR file (header length is 76 + 64 + 36 + 36 = 212 bytes)
			__TTTRFileHeader.BinaryHeader.NumberOfChannels = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.NumberOfCurves = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.BitsPerChannel = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.RoutingChannels = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.NumberOfBoards = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.ActiveCurve = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.MeasurementMode = _brInputFile.ReadInt32();  //read Int32 from file            
			__TTTRFileHeader.BinaryHeader.SubMode = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.RangeNo = _brInputFile.ReadInt32();  //read Int32 from file            
			__TTTRFileHeader.BinaryHeader.Offset = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.AcquisitionTime = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.StopAt = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.StopOnOvfl = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.Restart = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.DispayLinLog = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.DisplayTimeAxisFrom = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.DisplayTimeAxisTo = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.DisplayCountsAxisFrom = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.DisplayCountsAxisTo = _brInputFile.ReadInt32();  //read Int32 from file

			if (__TTTRFileHeader.BinaryHeader.DisplayCurves == null)
			{
				__TTTRFileHeader.BinaryHeader.DisplayCurves = new TimeHarpDefinitions.tCurveMapping[8];  //allocate int struct array
			}

			for (int i = 0; i < 8; i++)
			{
				__TTTRFileHeader.BinaryHeader.DisplayCurves[i].MapTo = _brInputFile.ReadInt32();  //read Int32 from file
				__TTTRFileHeader.BinaryHeader.DisplayCurves[i].Show = _brInputFile.ReadInt32();  //read Int32 from file 							 
			}

			if (__TTTRFileHeader.BinaryHeader.Params == null)
			{
				__TTTRFileHeader.BinaryHeader.Params = new TimeHarpDefinitions.tParamStruct[3];  //allocate float struct array
			}

			for (int i = 0; i < 3; i++)
			{
				__TTTRFileHeader.BinaryHeader.Params[i].Start = _brInputFile.ReadSingle();  //read float from file
				__TTTRFileHeader.BinaryHeader.Params[i].Step = _brInputFile.ReadSingle();  //read float from file
				__TTTRFileHeader.BinaryHeader.Params[i].End = _brInputFile.ReadSingle();  //read float from file  				
			}

			__TTTRFileHeader.BinaryHeader.RepeatMode = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.RepeatsPerCurve = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.RepeatTime = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.RepeatWaitTime = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BinaryHeader.ScriptName = _brInputFile.ReadChars(20);  //read char[20] from file

			// Read BoardHeader header to the TTTR file (header length is 48 bytes)
			__TTTRFileHeader.BoardHeader.HardwareIdent = _brInputFile.ReadChars(16);  //read char[16] from file
			__TTTRFileHeader.BoardHeader.HardwareVersion = _brInputFile.ReadChars(8);  //read char[8] from file
			__TTTRFileHeader.BoardHeader.BoardSerial = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BoardHeader.CFDZeroCross = _brInputFile.ReadInt32();  //read Int32 from file           
			__TTTRFileHeader.BoardHeader.CFDDiscrMin = _brInputFile.ReadInt32();  //read Int32 from file            
			__TTTRFileHeader.BoardHeader.SyncLevel = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BoardHeader.CurveOffset = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.BoardHeader.Resolution = _brInputFile.ReadSingle();  //read float from file

			// Read TTTR header to the TTTR file (header length is 52 bytes)
			__TTTRFileHeader.TTTRHeader.TTTRGlobclock = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.ExtDevices = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.Reserved1 = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.Reserved2 = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.Reserved3 = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.Reserved4 = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.Reserved5 = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.SyncRate = _brInputFile.ReadInt32();  //read Int32 from file            
			__TTTRFileHeader.TTTRHeader.AverageCFDRate = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.StopAfter = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.StopReason = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.NumberOfRecords = _brInputFile.ReadInt32();  //read Int32 from file
			__TTTRFileHeader.TTTRHeader.SpecialHeaderLength = _brInputFile.ReadInt32();  //read Int32 from file

			// Read CustomSISHeader from the TTTR file (header length is 140 bytes) - if the the length of the special header matches then it means it is our custom SIS header (otherwise we skip it)
			if (__TTTRFileHeader.TTTRHeader.SpecialHeaderLength * 4 == TimeHarpDefinitions.SizeTTTRCustomSISFileHeader)
			{
				__TTTRFileHeader.CustomSISHeader.Version = _brInputFile.ReadChars(8);  //read char[8] from file
				__TTTRFileHeader.CustomSISHeader.TimePPixel = _brInputFile.ReadDouble();  //read Double from file
				__TTTRFileHeader.CustomSISHeader.InitXNm = _brInputFile.ReadDouble();  //read Double from file
				__TTTRFileHeader.CustomSISHeader.InitYNm = _brInputFile.ReadDouble();  //read Double from file
				__TTTRFileHeader.CustomSISHeader.InitZNm = _brInputFile.ReadDouble();  //read Double from file
				__TTTRFileHeader.CustomSISHeader.XScanSizeNm = _brInputFile.ReadDouble();  //read Double from file           
				__TTTRFileHeader.CustomSISHeader.YScanSizeNm = _brInputFile.ReadDouble();  //read Double from file           
				__TTTRFileHeader.CustomSISHeader.ZScanSizeNm = _brInputFile.ReadDouble();  //read Double from file
				__TTTRFileHeader.CustomSISHeader.ImageWidthPx = _brInputFile.ReadInt32();  //read Int32 from file           
				__TTTRFileHeader.CustomSISHeader.ImageHeightPx = _brInputFile.ReadInt32();  //read Int32 from file            
				__TTTRFileHeader.CustomSISHeader.ImageDepthPx = _brInputFile.ReadInt32();  //read Int32 from file         
				__TTTRFileHeader.CustomSISHeader.XOverScanPx = _brInputFile.ReadInt32();  //read Int32 from file         
				__TTTRFileHeader.CustomSISHeader.YOverScanPx = _brInputFile.ReadInt32();  //read Int32 from file         
				__TTTRFileHeader.CustomSISHeader.ZOverScanPx = _brInputFile.ReadInt32();  //read Int32 from file           
				__TTTRFileHeader.CustomSISHeader.SISChannels = _brInputFile.ReadInt32();  //read Int32 from file          
				__TTTRFileHeader.CustomSISHeader.TypeOfScan = _brInputFile.ReadInt32();  //read Int32 from file          
				__TTTRFileHeader.CustomSISHeader.FrameTimeOut = _brInputFile.ReadInt32();  //read Int32 from file         
				__TTTRFileHeader.CustomSISHeader.FiFoTimeOut = _brInputFile.ReadInt32();  //read Int32 from file          
				__TTTRFileHeader.CustomSISHeader.StackMarker = _brInputFile.ReadByte();  //read Byte from file         
				__TTTRFileHeader.CustomSISHeader.FrameMarker = _brInputFile.ReadByte();  //read Byte from file       
				__TTTRFileHeader.CustomSISHeader.LineMarker = _brInputFile.ReadByte();  //read Byte from file          
				__TTTRFileHeader.CustomSISHeader.PixelMarker = _brInputFile.ReadByte();  //read Byte from file          
				__TTTRFileHeader.CustomSISHeader.GalvoMagnificationObjective = _brInputFile.ReadDouble();  //read Double from file          
				__TTTRFileHeader.CustomSISHeader.GalvoScanLensFocalLength = _brInputFile.ReadDouble();  //read Double from file         
				__TTTRFileHeader.CustomSISHeader.GalvoRangeAngleDegrees = _brInputFile.ReadDouble();  //read Double from file         
				__TTTRFileHeader.CustomSISHeader.GalvoRangeAngleInt = _brInputFile.ReadDouble();  //read Double from file
			}

			// Close file
			_brInputFile.Close();
			_fsInputFileStream.Close();
		}


		/// <summary>
		/// Get TTTR data from TTTR file into the dedicated buffer - this function is threaded by TH_StartMeas() function.
		/// </summary> 
		/// <param name="__sInputFile">The name of the input TTTR file to be read.</param>        
		private static void GetDataIntoFiFo(string __sInputFile)
		{
			// Declare corresponding file streams
			FileStream _fsInputFileStream;  //declare file stream - read TTTR file
			BinaryReader _brInputFile;  //declare binary reader - read TTTR File

			try  //open input TTTR file for reading
			{
				_fsInputFileStream = File.Open(__sInputFile, FileMode.Open, FileAccess.Read, FileShare.Read); //open the input file for reading
				_brInputFile = new BinaryReader(_fsInputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive
				_brInputFile.ReadBytes(SizeTTTRFileHeader);  //move the file pointer by SizeTTTRFileHeader bytes with respect to the file origin - in this way we can read the TTTR records
			}
			catch (Exception)
			{
				throw new Exception("KUL.MDS.Hardware.TimeHarp200DLL.Exception - " + "The file: " + __sInputFile + " could not be opened by GetDataIntoFiFo() function!");
			}


			// Fill FiFo buffer with records from binary TTTR file
			int i = 0;
			//int k = 0;  //for debugging purposes          
			ThreadsStatus.GetFiFoThread.IndexFiFoBufferLowerBound = 0;  //reset index
			ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound = 0;  //reset index

			while (i < m_iNumberOfRecords && IsMeasuremetRunning() && !m_IsMesurementToBeStop)  //fill the ui32TTTRBuffer[] buffer array with records
			{
				lock (ThreadsStatus.GetFiFoThread.ThreadLockerFiFoBuffer)  //lock FiFo buffer to make sure we correctly handle FiFo buffer (note: we also use this FiFo buffer in CopyFiFoToBuffer() function)
				{
					// Get data into the FiFo buffer in blocks of FIFO_SIZE_RECORDS_BLOCK size
					int j = 0;
					while (j < FIFO_SIZE_RECORDS_BLOCK && ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound < FIFOSIZE)
					{
						ThreadsStatus.GetFiFoThread.ui32FiFoBuffer[ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound] = _brInputFile.ReadUInt32();  //read a single record from the input binary TTTR file
						ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound++;  //get the current FiFo size (used in the DMA transfer to derive the number of records to transfer)

						i++;  //count the overall number of records read so far
						j++;  //count the number of records within a block read

						if (i >= m_iNumberOfRecords)  //case when we reach the end of TTTR emulation file
						{
							if (!STOP_ON_NUMBER_OF_RECORDS)  //if true reset i to start from the beginning - in this way we can emulate endless measurement time
							{
								i = 0;
								_fsInputFileStream.Position = _fsInputFileStream.Position - m_iNumberOfRecords * sizeof(UInt32);  //move the file pointer to the beginning of the records part of the TTTR file
							}

							break;  //exit the for-loop because we have reached the end of the file
						}
					}

					// Update status flags
					UpdateStatusFlags();

					//k++;  //for debugging purposes
					//Console.WriteLine("GetDataIntoFiFo(): k={0}, IndexFiFoBufferUpperBound = {1}, StatusFlags = {2}", k, ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound, ThreadsStatus.GetFiFoThread.StatusFlags);  //for debugging purposes
				}


				System.Threading.Thread.Sleep(THREAD_SLEEP_TIME); //sleep to simulate Time Harp FiFo filling with records
				UpdateElapsedTime();  //update elapsed time                                
			}


			// Set measurement to not running
			m_bMeasurementRunning = false;  //we want to stop measurement, so set it status to show acquisition has ended


			// Close open files
			_fsInputFileStream.Close();  //close file
			_brInputFile.Close();  //close file
		}


		/// <summary>
		/// Copy FiFo to a supplied buffer - used in the TH_T3RStartDMA() function.        
		/// </summary>
		/// <param name="buffer">The buffer which the FiFo will be copied to.</param> 
		private static void CopyFiFoToBuffer(uint[] buffer)
		{
			lock (ThreadsStatus.GetFiFoThread.ThreadLockerFiFoBuffer)  //lock FiFo buffer to make sure we correctly handle FiFo buffer (note: we also use this FiFo buffer in GetDataIntoFiFo() function)
			{
				if (ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound <= DMABLOCKSZ)  //the case when FiFo half full or FiFo time out (e.g. TH_T3RStartDMA() was called due to FiFo time out - in this case most likely number of records is smaller than DMABLOCKSZ)
				{
					ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords = ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound;  //calculate the number of records to be transferred                    
					Array.Copy(ThreadsStatus.GetFiFoThread.ui32FiFoBuffer, 0, buffer, 0, ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords);  //copy FiFo buffer to the TTTR buffer supplied to TH_T3RStartDMA() function                    
					ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound = 0;  //reset FiFo (we have got all records, so go to the beginning of the FiFo)
				}
				else
				{
					ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords = DMABLOCKSZ;  //calculate the number of records to be transferred
					Array.Copy(ThreadsStatus.GetFiFoThread.ui32FiFoBuffer, 0, buffer, 0, ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords);  //copy FiFo buffer to the TTTR buffer supplied to TH_T3RStartDMA() function
					ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound -= ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords;

					// Move the rest of the records of FiFo buffer to its beginning - it is actually a copy and due to recalculation of the index IndexFiFoBufferUpperBound of the FiFo, it will ignore the rest of the elements beyond this index
					Array.Copy(ThreadsStatus.GetFiFoThread.ui32FiFoBuffer, DMABLOCKSZ, ThreadsStatus.GetFiFoThread.ui32FiFoBuffer, 0, ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound);  //copy records to beginning of FiFo buffer
				}

				// Update status flags
				UpdateStatusFlags();
			}
		}


		/// <summary>
		/// Update status flags.        
		/// </summary>
		private static void UpdateStatusFlags()
		{
			// Set FiFo empty flag
			if (ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound >= FIFO_SIZE_RECORDS_BLOCK)  //check if FiFo is empty or not (if we have more than 16384 records we set FiFo to not empty state)
			{
				// Set that FiFo is not empty (i.e. set its bit to zero) - for this we use XOR operation so that we preserve the other flags                            
				if ((ThreadsStatus.GetFiFoThread.StatusFlags & FLAG_FIFOEMPTY) > 0)  //if FiFo is set to empty (its bit is 1) then set it to not empty (its bit to 0)
				{
					ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags ^ FLAG_FIFOEMPTY;  //set FiFo empty flag to zero (which means the buffer is not empty)
				}
			}
			else  // Set that FiFo is empty (i.e. set its bit to 1) - for this we use OR operation so that we preserve the other flags
			{
				ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags | FLAG_FIFOEMPTY;  //set FiFo empty flag to 1 (which means the buffer is empty)                                                        
			}

			// Set FiFo Half Full flag
			if (ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound >= DMABLOCKSZ)  //check for FiFo half full
			{
				// Set the FiFo half full flag to 1 (means FiFo Half Full) - for this we use OR operation so that we preserve the other flags                   
				ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags | FLAG_FIFOHALFFULL;  //set FiFo Half Full flag to 1 (which means the buffer is half full)
			}
			else  // Set FiFo half full flag to zero (means FiFo is not half full)
			{
				if ((ThreadsStatus.GetFiFoThread.StatusFlags & FLAG_FIFOHALFFULL) > 0)  //if FiFo Half Full is set to 1 then we use XOR to set it to 0 (check it flag is 1 in order to make sure XOR will set the flag to 0 correctly)
				{
					ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags ^ FLAG_FIFOHALFFULL;  //set FiFo Half Full flag to 0 (which means the buffer is not half full)
				}
			}

			// Set FiFo Full flag
			if (ThreadsStatus.GetFiFoThread.IndexFiFoBufferUpperBound >= FIFOSIZE)  //check for FiFo full
			{
				// Set the FiFo full flag to 1 (means FiFo Full) - for this we use OR operation so that we preserve the other flags                   
				ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags | FLAG_FIFOFULL;  //set FiFo Full flag to 1 (which means the buffer is full)                                                      
			}
			else  // Set FiFo full flag to zero (means FiFo is not full)
			{
				if ((ThreadsStatus.GetFiFoThread.StatusFlags & FLAG_FIFOFULL) > 0)  //if FiFo Full is set to 1 then we use XOR to set it to 0 (check it flag is 1 in order to make sure XOR will set the flag to 0 correctly)
				{
					ThreadsStatus.GetFiFoThread.StatusFlags = ThreadsStatus.GetFiFoThread.StatusFlags ^ FLAG_FIFOFULL;  //set FiFo Full flag to 0 (which means the buffer is not full)
				}
			}
		}


		/// <summary>
		/// Allows to stop filling FiFo by setting IsMesurementToBeStop = True.        
		/// </summary>   
		private static void RequestStop()  //stop filling FiFo by setting IsMesurementToBeStop = True
		{
			m_IsMesurementToBeStop = true;  //allows to stop filling FiFo by setting its value to true
			//System.Threading.Thread.Sleep(500);  //sleep for a while
		}


		/// <summary>
		/// Count time and checks if the measurement must continue running.        
		/// </summary> 
		private static bool IsMeasuremetRunning()
		{
			if (m_iMeasurementTimeCounter < m_iAcquisitionTime)
			{
				m_bMeasurementRunning = true;
			}
			else
			{
				m_bMeasurementRunning = false;
			}

			return m_bMeasurementRunning;
		}


		/// <summary>
		/// Update elapsed time.        
		/// </summary>
		private static void UpdateElapsedTime()
		{
			m_iTimeCounterStop = System.Environment.TickCount;  //get TickCount in order to keep track of the elapsed time            
			m_iMeasurementTimeCounter = m_iTimeCounterStop - m_iTimeCounterStart;  //calculate elapsed time
		}



		/////////////////////////////////////////////////////////////////
		//// BEGIN The Emulated DLL functions

		/// <summary>
		/// Emulation of THLib function "TH_GetErrorString" - it converts error code (errcode) into meaningful error string (errstring).
		/// </summary>
		/// <param name="errstring">The returned error string.</param>  
		/// <param name="errcode">Error code.</param> 
		/// <returns>int: if return value >0 (success), if <0 (error)</returns>   
		public static int TH_GetErrorString(StringBuilder errstring, int errcode)
		{
			//Error codes TimeHarp THLIB v6.1, February 2009
			int _iErrorCode = 1;  //initialize with positive integer, so no error
			errstring.Clear();  //clear string (if any) before trying to append an error string

			switch (errcode)
			{
				case 0: { errstring.Append("ERROR_NONE"); break; }
				case -1: { errstring.Append("ERROR_DEVICE_OPEN_FAIL"); break; }
				case -2: { errstring.Append("ERROR_DEVICE_BUSY"); break; }
				case -3: { errstring.Append("ERROR_DEVICE_HEVENT_FAIL"); break; }
				case -4: { errstring.Append("ERROR_DEVICE_CALLBSET_FAIL"); break; }
				case -5: { errstring.Append("ERROR_DEVICE_BARMAP_FAIL"); break; }
				case -6: { errstring.Append("ERROR_DEVICE_CLOSE_FAIL"); break; }
				case -7: { errstring.Append("ERROR_DEVICE_RESET_FAIL"); break; }
				case -8: { errstring.Append("ERROR_DEVICE_VERSION_FAIL"); break; }
				case -9: { errstring.Append("ERROR_DEVICE_VERSION_MISMATCH"); break; }

				case -16: { errstring.Append("ERROR_INSTANCE_RUNNING"); break; }
				case -17: { errstring.Append("ERROR_INVALID_ARGUMENT"); break; }
				case -18: { errstring.Append("ERROR_INVALID_MODE"); break; }
				case -19: { errstring.Append("ERROR_INVALID_OPTION"); break; }
				case -20: { errstring.Append("ERROR_INVALID_MEMORY"); break; }
				case -21: { errstring.Append("ERROR_INVALID_RDATA"); break; }
				case -22: { errstring.Append("ERROR_NOT_INITIALIZED"); break; }
				case -23: { errstring.Append("ERROR_NOT_CALIBRATED"); break; }
				case -24: { errstring.Append("ERROR_DMA_FAIL"); break; }
				case -25: { errstring.Append("ERROR_XTDEVICE_FAIL"); break; }

				case -32: { errstring.Append("ERROR_CALIB_MAXRET_FAIL"); break; }
				case -33: { errstring.Append("ERROR_CALIB_REGINIT_FAIL"); break; }
				case -34: { errstring.Append("ERROR_CALIB_CVALERRBIT"); break; }
				case -35: { errstring.Append("ERROR_CALIB_MVALERRBIT"); break; }
				case -36: { errstring.Append("ERROR_CALIB_MFIT_FAIL"); break; }
				case -37: { errstring.Append("ERROR_CALIB_MINVALID"); break; }

				case -48: { errstring.Append("ERROR_SCX_NOT_PRESENT"); break; }
				case -49: { errstring.Append("ERROR_SCX_NOT_ENABLED"); break; }
				case -50: { errstring.Append("ERROR_SCX_RESET_FAIL"); break; }
				case -51: { errstring.Append("ERROR_SCX_NOT_ACTIVE"); break; }
				case -52: { errstring.Append("ERROR_SCX_STILL_ACTIVE"); break; }
				case -53: { errstring.Append("ERROR_SCX_GOTOXY_FAIL"); break; }
				case -54: { errstring.Append("ERROR_SCX_SCPROG_FAIL"); break; }

				case -64: { errstring.Append("ERROR_HARDWARE_F01"); break; }
				case -65: { errstring.Append("ERROR_HARDWARE_F02"); break; }
				case -66: { errstring.Append("ERROR_HARDWARE_F03"); break; }
				case -67: { errstring.Append("ERROR_HARDWARE_F04"); break; }
				case -68: { errstring.Append("ERROR_HARDWARE_F05"); break; }
				case -69: { errstring.Append("ERROR_HARDWARE_F06"); break; }
				case -70: { errstring.Append("ERROR_HARDWARE_F07"); break; }
				case -71: { errstring.Append("ERROR_HARDWARE_F08"); break; }
				case -72: { errstring.Append("ERROR_HARDWARE_F09"); break; }
				case -73: { errstring.Append("ERROR_HARDWARE_F10"); break; }
				case -74: { errstring.Append("ERROR_HARDWARE_F11"); break; }
				case -75: { errstring.Append("ERROR_HARDWARE_F12"); break; }
				case -76: { errstring.Append("ERROR_HARDWARE_F13"); break; }
				case -77: { errstring.Append("ERROR_HARDWARE_F14"); break; }
				case -78: { errstring.Append("ERROR_HARDWARE_F15"); break; }

				default: { _iErrorCode = -1; break; }  //will return indicating error   
			}


			return _iErrorCode;  //return error code
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetLibraryVersion" - it returns the library version into vers variable.
		/// </summary>
		/// <param name="vers">The returned library version as a string.</param>
		/// <returns>return value = integer; if return value =0 (success), if <0 (error)</returns>
		public static int TH_GetLibraryVersion(StringBuilder vers)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error
			vers.Clear();  //clear string (if any) before trying to append a new one

			try
			{
				vers.Append(TargetLibVersion);
			}
			catch (Exception)
			{
				_iErrorCode = -1;  //if exception, return its error code
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_Initialize" - it sets up the Time Harp 200 acquisition mode.       
		/// </summary>
		/// <param name="mode">mode = 0 or 1 (0 - standard histogramming; 1 - TTTR mode).</param>        
		/// <returns>int: if return value = 0 (success), if <0 (error)</returns>
		public static int TH_Initialize(int mode)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (mode == 0 || mode == 1)  //check if mode has correct value
			{
				m_iMeasurementMode = mode;  //set measurement mode
				//m_IsInitialized = true;  //indicate successful initialization              
			}
			else
			{
				_iErrorCode = -18;  //if invalid mode, return its error code (-18 = ERROR_INVALID_MODE)
				//m_IsInitialized = false;  //indicate unsuccessful initialization
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetHardwareVersion" - it returns the Hardware (Time Harp) version into vers variable.
		/// </summary>
		/// <param name="vers">the Hardware (Time Harp) version.</param>        
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>    
		public static int TH_GetHardwareVersion(StringBuilder vers)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error
			vers.Clear();  //clear contents

			try
			{
				vers.Append(m_sHardwareVersion);
			}
			catch (Exception)
			{
				_iErrorCode = -8;  //if exception, return error code (-8 = ERROR_DEVICE_VERSION_FAIL)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetSerialNumber" - it returns the serial number of Time Harp 200 into serial variable.
		/// </summary>
		/// <param name="serial">serial number of Time Harp 200.</param>        
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>        
		public static int TH_GetSerialNumber(StringBuilder serial)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error
			serial.Clear();  //clear contents before adding new one

			try
			{
				serial.Append(m_sSerialNumber);
			}
			catch (Exception)
			{
				_iErrorCode = -1;  //if exception, return error code (-1 = ERROR_DEVICE_OPEN_FAIL)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetBaseResolution" - it returns the approximated value of the Time Harp 200 time resolution (used before calibration).
		/// </summary>        
		/// <returns>int: if return value >0 (the approximated value of the time resolution), if <0 (error)</returns>
		public static int TH_GetBaseResolution()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			_iErrorCode = TIME_HARP_BASE_RESOLUTION;  //set Time Harp base resolution


			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_Calibrate" - calibrate Time Harp 200 (always must be done before the start of a measurement session).
		/// </summary>     
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_Calibrate()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			System.Threading.Thread.Sleep(2000);  //put the thread to sleep for about _iTHREAD_SLEEP_TIME [ms]

			// Assign the TTTR file for the emulation of Time Harp
			Files.TTTRFileName = TIME_HARP_EMULATION_TTTR_FILE;  //assign the file which will be used for Time Harp emulation

			// Read TTTR file header in order to get the parameters of the emulation
			ReadTTTRHeaderFromFile(Files.TTTRFileName, ref Files.TTTRFileHeader);

			// Initialize parameters using the info from TTTR file
			m_iMeasurementMode = Files.TTTRFileHeader.BinaryHeader.MeasurementMode;
			m_iRange = Files.TTTRFileHeader.BinaryHeader.RangeNo;
			m_iOffset = Files.TTTRFileHeader.BinaryHeader.Offset;
			m_iAcquisitionTime = Files.TTTRFileHeader.BinaryHeader.AcquisitionTime;

			m_sHardwareVersion = (new String(Files.TTTRFileHeader.BoardHeader.HardwareVersion)).Trim('\0');
			m_sSerialNumber = Files.TTTRFileHeader.BoardHeader.BoardSerial.ToString();
			m_iCFDZeroCross = Files.TTTRFileHeader.BoardHeader.CFDZeroCross;
			m_iCFDDiscrMin = Files.TTTRFileHeader.BoardHeader.CFDDiscrMin;
			m_iSyncLevel = Files.TTTRFileHeader.BoardHeader.SyncLevel;
			m_fResolution = Files.TTTRFileHeader.BoardHeader.Resolution;

			m_dTimePPixel = Files.TTTRFileHeader.CustomSISHeader.TimePPixel;
			m_iXWidth = Files.TTTRFileHeader.CustomSISHeader.ImageWidthPx;
			m_iYHeight = Files.TTTRFileHeader.CustomSISHeader.ImageHeightPx;
			m_bIsBidirectionalScan = (Files.TTTRFileHeader.CustomSISHeader.TypeOfScan == 1) ? true : false;  //get the type of scanning (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
			m_iSyncRate = Files.TTTRFileHeader.TTTRHeader.SyncRate;
			m_iCountRate = (Files.TTTRFileHeader.TTTRHeader.AverageCFDRate >= 0) ? Files.TTTRFileHeader.TTTRHeader.AverageCFDRate : 0;  //temporally we add check to handle no count rate written to file
			m_iNumberOfRecords = Files.TTTRFileHeader.TTTRHeader.NumberOfRecords;

			// Print parameters read from the TTTR file            
			Console.WriteLine();
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("EMULATION - Time Harp Emulation!");
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("BEGIN - Parameters from TTTR emulation file:");
			Console.WriteLine("TTTR emulation file: {0}", Files.TTTRFileName);
			Console.WriteLine("m_iMeasurementMode = {0}", m_iMeasurementMode);
			Console.WriteLine("m_iRange = {0}", m_iRange);
			Console.WriteLine("m_iOffset = {0} [ns]", m_iOffset);
			Console.WriteLine("m_iAcquisitionTime = {0} [ms]", m_iAcquisitionTime);
			Console.WriteLine("m_fResolution = {0} [ns]", m_fResolution);
			Console.WriteLine("m_dTimePPixel = {0} [ms]", m_dTimePPixel);
			Console.WriteLine("m_iXWidth = {0}", m_iXWidth);
			Console.WriteLine("m_iYHeight = {0}", m_iYHeight);
			Console.WriteLine("m_bIsBidirectionalScan = {0}", m_bIsBidirectionalScan);
			Console.WriteLine("m_iNumberOfRecords = {0}", m_iNumberOfRecords);
			Console.WriteLine("END - Parameters from TTTR emulation file.");
			Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine();

			System.Threading.Thread.Sleep(2000);  //put the thread to sleep for about _iTHREAD_SLEEP_TIME [ms]

			// Initialize status flags to default value (so that its state is properly set)
			ThreadsStatus.GetFiFoThread.StatusFlags = FLAG_RAMREADY + FLAG_FIFOEMPTY;  //set all flags to zero except FLAG_RAMREADY (=0x0400) and FLAG_FIFOEMPTY (=0x2000) - first RAM is set to be ready and second the FIFO is also empty, so we just indicate this

			// Indicate that calibration is done
			m_IsCalibrated = true;  //emulates calibration of Time Harp

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetCFDDiscrMin" - it sets CFD level, value is in millivolts; value (minimum) = DISCRMIN, value (maximum) = DISCRMAX.
		/// </summary>
		/// <param name="value">CFD level, value is in [mV]; value (minimum) = DISCRMIN, value (maximum) = DISCRMAX.</param>        
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_SetCFDDiscrMin(int value)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (value >= DISCRMIN && value <= DISCRMAX)  //check if we have the correct value, if not return error
			{
				m_iCFDDiscrMin = value;
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetCFDZeroCross" - it sets CFD zero cross level, value is in millivolts; value (minimum) = ZCMIN, value (maximum) = ZCMAX.
		/// </summary>
		/// <param name="value"> CFD zero cross level, value is in [mV]; value (minimum) = ZCMIN, value (maximum) = ZCMAX.</param>        
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_SetCFDZeroCross(int value)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (value >= ZCMIN && value <= ZCMAX)  //check if we have the correct value, if not return error
			{
				m_iCFDZeroCross = value;
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetSyncLevel" - sets sync level, value in millivolts; value (minimum) = SYNCMIN, value (maximum) = SYNCMAX.
		/// </summary>
		/// <param name="value">sync level value in [mV], value (minimum) = SYNCMIN, value (maximum) = SYNCMAX</param>        
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_SetSyncLevel(int value)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (value >= SYNCMIN && value <= SYNCMAX)  //check if we have the correct value, if not return error
			{
				m_iSyncLevel = value;
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetStopOverflow" - the arguments determine if a measurement run will stop if any channel reaches the
		/// stop level (StopOnOfl = 0 (do not stop); StopOnOfl = 1 (stop on overflow); StopLevel - count level at which to stop (min 0, max 65535).
		/// </summary>
		/// <param name="StopOnOfl">controls to stop or not if overflow occurs, (StopOnOfl = 0 (do not stop), StopOnOfl = 1 (stop on overflow).</param>        
		/// <param name="StopLevel">count level at which to stop (min 0, max 65535).</param> 
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_SetStopOverflow(int StopOnOfl, int StopLevel)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (StopOnOfl == 0 || StopOnOfl == 1)  //check if we have the correct value, if not return error
			{
				m_iStopOnOfl = StopOnOfl;  //(StopOnOfl = 0 (do not stop); StopOnOfl = 1 (stop on overflow)
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			if (StopLevel >= 0 && StopLevel <= 65535)  //check if we have the correct value, if not return error
			{
				m_iStopLevel = StopLevel;  //count level at which to stop (min 0, max 65535)
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetRange" - it sets measurement range.
		/// </summary>
		/// <param name="range">range = 0 (base resolution), range = 1 (2x base resolution) etc. up to range = RANGES - 1 (largest resolution in multiple of the base resolution).</param>
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_SetRange(int range)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (range >= 0 && range <= (RANGES - 1))  //check if we have the correct value, if not return error
			{
				if (range == m_iRange)
				{
					m_iRange = range;  //set new range
				}
				else
				{
					Console.WriteLine("Wrong value for m_iRange = {0}, the value from TTTR file is = {1}. Try again!", range, m_iRange);
					_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)                    
				}
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
				m_iRange = 0;  //set to default value
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetOffset" - it sets new offset in [ns]. 
		/// The new offset is approximation of the desired offset (the typical step size is around 2.5ns).
		/// </summary> 
		/// <param name="offset">offset (minimum) = OFFSETMIN, offset (maximum) = OFFSETMAX.</param>
		/// <returns>int: if return value >=0 (new offset), if <0 (error)</returns>      
		public static int TH_SetOffset(int offset)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (offset >= OFFSETMIN && offset <= OFFSETMAX)  //check if we have the correct value, if not return error
			{
				if (offset == m_iOffset)
				{
					m_iOffset = offset;  //set up the new offset in steps of 2.5ns
					_iErrorCode = m_iOffset;
				}
				else
				{
					Console.WriteLine("Wrong value for m_iOffset = {0}, the value from TTTR file is = {1}. Try again!", offset, m_iOffset);
					_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)                    
				}
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_NextOffset" - it changes offset. The offset changes at a step size of approximately 2.5ns.
		/// </summary> 
		/// <param name="direction">direction (minimum) = -1 (down), direction (maximum) = +1 (up).</param>
		/// <returns>int: if return value >=0 (new offset), if <0 (error)</returns>
		public static int TH_NextOffset(int direction)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (direction == -1 || direction == +1)  //check if we have the correct value, if not return error
			{
				m_iOffset = m_iOffset + direction;  //set up next offset
				_iErrorCode = m_iOffset;
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_ClearHistMem" - it clears histogram memory.
		/// </summary> 
		/// <param name="block">Number of blocks to clear (always 0 if not routing).</param>
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_ClearHistMem(int block)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (block > 0)
			{
				m_iBlocksToClear = block;
			}
			else
			{
				_iErrorCode = -17;  //if invalid value, return its error code (-17 = ERROR_INVALID_ARGUMENT)
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetMMode" - it sets measurement mode.        
		/// </summary>
		/// <param name="mmode">Measurement mode, mmode = 0 (one-time histogramming and TTTR modes), mmode = 1 (continuous mode).</param>  
		/// <param name="tacq">Acquisition time in milliseconds (set this to 0 for continuous mode with external clock).</param> 
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>  
		public static int TH_SetMMode(int mmode, int tacq)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error
			m_IsSyncMode = false;  //indicates that we want to measure count rate (if we immediately call GetCountRate() after calling the current function)

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (mmode == 0 || mmode == 1)  //check if mode has correct value
			{
				m_iMeasurementMode = mmode;  //set measurement mode                
			}
			else
			{
				return (_iErrorCode = -18);  //if invalid mode, return its error code (-18 = ERROR_INVALID_MODE)                
			}

			if (tacq >= ACQTMIN && tacq <= ACQTMAX)  //check if we have the correct value
			{
				m_iAcquisitionTime = tacq;  //set acquisition time                
			}
			else
			{
				return (_iErrorCode = -17);  //if invalid value, return its error code (-18 = ERROR_INVALID_VALUE)                
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_StartMeas" - it starts measurement/data acquisition.
		/// </summary>     
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_StartMeas()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			//Start time counter to measure elapsed time
			m_iTimeCounterStart = System.Environment.TickCount;  //get TickCount in order to keep track of the elapsed time
			m_iTimeCounterStop = m_iTimeCounterStart;  //reset stop counter
			m_iMeasurementTimeCounter = 0;  //reset measurement counter
			m_bMeasurementRunning = true;  //indicates that measurement is running

			// We do not want to stop the measurement in the beginning, so indicate this
			m_IsMesurementToBeStop = false;  //measurement has just started, so set to "false"

			// Reset current number of records
			ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords = 0;

			// Create and spawn a thread which will be responsible for reading and getting the FiFo buffer from TTTR binary file
			ThreadsStatus.GetFiFoThread.Thread = new Thread(() => GetDataIntoFiFo(Files.TTTRFileName));  //execute GetDataIntoFiFo() function in a separate thread. Note that the thread status is kept in ThreadsStatus structure (see GetFiFoThread variable in there)
			ThreadsStatus.GetFiFoThread.Thread.Name = "GetDataIntoFiFo";  //set the name of the thread
			ThreadsStatus.GetFiFoThread.Thread.IsBackground = true;  //set the thread as a background thread
			ThreadsStatus.GetFiFoThread.Thread.Priority = ThreadPriority.Normal;  //set the thread priority to above normal in order to assure we collect all buffer events from FiFo
			ThreadsStatus.GetFiFoThread.Thread.Start();  //start the thread            


			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_StopMeas" - it stops measurement/data acquisition. 
		/// Note that in TTTR mode this also rests the hardware FIFO.
		/// </summary>
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>
		public static int TH_StopMeas()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}


			// Stop getting data into FiFo buffer
			RequestStop();

			// Indicates that measurement is stopped
			m_bMeasurementRunning = false;


			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_CTCStatus" - it returns the acquisition time status.
		/// </summary>       
		/// <returns>int: if return value =0 (acquisition time still running), if >0 (acquisition time has ended), if <0 (error)</returns>       
		public static int TH_CTCStatus()
		{
			int _iErrorCode = 1;  //initialize with zero, so acquisition time has ended

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			// Check the elapsed time to see if we are still running/reading from file            
			if (m_bMeasurementRunning)
			{
				_iErrorCode = 0;  //acquisition time still running
			}
			else
			{
				_iErrorCode = 1;  //acquisition time has ended
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetSyncMode" - it sets the SYNC mode. 
		/// Note that this function must be called before GetCountRate() if the sync rate is to
		/// be measured. Allow at least 600ms after this call to get a stable sync rate reading.
		/// </summary>       
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>       
		public static int TH_SetSyncMode()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			m_IsSyncMode = true;  //set sync mode to true so that when you call GetCountRate() you get the sync rate

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetBlock" - returns the total number of counts in this histogram.
		/// The current version counts only up to 65535 (16 bits).
		/// </summary> 
		/// <param name="chcount[]">An array of unsigned integers (32bit) of BLOCKSIZE where the histogram can be stored.</param>
		/// <param name="block">Block number (0..3) to fetch (always 0 if not routing).</param>        
		/// <returns>int: if return value >=0 (total number of counts in this histogram), if <0 (error)</returns>
		public static int TH_GetBlock(uint[] chcount, int block)
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			//if (!m_IsCalibrated)  //we need calibration to get the count rate
			//{
			//    return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			//}

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetResolution" - it returns the channel width in current range (from last calibration) in [ns].
		/// </summary>           
		/// <returns>if return value >0 (the resolution), if <0 (error)</returns>    
		public static float TH_GetResolution()
		{
			float _iErrorCode = 0.0f;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23.0f;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			_iErrorCode = m_fResolution;

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetCountRate" - returns the current count rate or sync rate. 
		/// The function returns either the current count rate if previously TH_SetMMode() was called or 
		/// the current sync rate if previously TH_SetSyncMode() was called. Allow at least 600ms to get a
		/// stable rate meter reading in both cases.
		/// </summary> 
		/// <returns>int: if return value >=0 (current count rate or sync rate, see notes above), if <0 (error)</returns>        
		public static int TH_GetCountRate()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (m_IsSyncMode)
			{
				_iErrorCode = m_iSyncRate;
			}
			else
			{
				_iErrorCode = m_iCountRate;
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetFlags" - returns the current status flags (a bit pattern).
		/// Use the predefined flags (e.g. FLAG_OVERFLOW) and bitwise AND to extract individual bits (you can call this function anytime during a measurement but not during DMA).
		/// </summary> 
		/// <returns>int: returns current status flags (a bit pattern)</returns>  
		public static int TH_GetFlags()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			// Get status flags from a bit pattern
			lock (ThreadsStatus.GetFiFoThread.ThreadLockerFiFoBuffer)  //lock status flags to make sure proper handling of  the flags
			{
				_iErrorCode = ThreadsStatus.GetFiFoThread.StatusFlags;
			}

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetElapsedMeasTime" - returns the elapsed measurement time in ms (not for continuous mode).
		/// </summary> 
		/// <returns>int: returns the elapsed measurement time in ms (not for continuous mode)</returns>
		public static int TH_GetElapsedMeasTime()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			// Check the elapsed time to see if we are still running/reading from file
			_iErrorCode = m_iMeasurementTimeCounter;  //get elapsed time from m_iMeasurementTimeCounter

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_Shutdown" - it closes the device (closes the handle to Time Harp device driver).
		/// </summary> 
		/// <returns>int: if return value >0 (success), if <0 (error)</returns>                 
		public static int TH_Shutdown()
		{
			int _iErrorCode = 1;  //initialize with one, so no error

			return _iErrorCode;
		}


		/////////////////////////////////////////////////////////////////////////////
		// Special functions for CONTINUOUS mode

		/// <summary>
		/// Emulation of THLib function "TH_GetBank" - it returns the sum of all counts in the channels fetched.
		/// </summary>
		/// <param name="buffer[]">An array of words (16bit) of BANKSIZE where the histogram data can be stored</param>
		/// <param name="chanfrom">Lower limit of the channels to be fetched per histogram (min 0, max 4095, and chanfrom<=chanto)</param>
		/// <param name="chanto">Upper limit of the channels to be fetched per histogram (min 0, max 4095, and chanfrom<=chanto)</param>
		/// <returns>int: if return value >0 (sum of all counts in the channels fetched), if <0 (error)</returns>
		public static int TH_GetBank(ushort[] buffer, int chanfrom, int chanto)
		{
			//int _iErrorCode = 1;  //initialize with zero, so no error

			//if (!m_IsCalibrated)  //we need calibration to get the count rate
			//{
			//    return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			//}


			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_GetLostBlocks" - it returns either the number of lost blocks (if >0) in continuous mode run or an error (if <0).
		/// </summary> 
		/// <returns>int: returns either the number of lost blocks (if >0) in continuous mode run or an error (if <0)</returns>
		public static int TH_GetLostBlocks()
		{
			//int _iErrorCode = 1;  //initialize with zero, so no error

			//if (!m_IsCalibrated)  //we need calibration to get the count rate
			//{
			//    return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			//}

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/////////////////////////////////////////////////////////////////////////////
		// Special functions for TTTR mode (Time Harp 200 must have TTTR option activated/purchased)

		/// <summary>
		/// Emulation of THLib function "TH_T3RDoDMA" - it initiates and performs DMA in a single function call (timeout = 300ms). 
		/// CPU time during wait for DMA completion will be yielded to other processes/threads.
		/// </summary> 
		/// <param name="buffer[]">an array of dwords (32bit) of at least 1/2 FIFOSIZE where the TTTR data can be stored (buffer[] must not be accessed until the function returns).</param>
		/// <param name="count">number of TTTR records to be fetched (max 1/2 FIFOSIZE), count must not be larger than the buffer[] size.</param>
		/// <returns>int: if return value >=0 (success and equals the number of records obtain), if <0 (error)</returns>
		public static int TH_T3RDoDMA(uint[] buffer, uint count)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			// Start DMA transfer
			_iErrorCode = TH_T3RStartDMA(buffer, count);

			if (_iErrorCode != 0)  //check if start DMA was successful
			{
				return _iErrorCode = -20;  //due to the failure of DMA return error code (-20 = ERROR_INVALID_MEMORY)            
			}

			// Wait for DMA completion and get the number of records obtained from FiFo buffer
			_iErrorCode = TH_T3RCompleteDMA();

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_T3RStartDMA" - it initiates DMA but returns before completion.
		/// No other calls to THLib are allowed between this call and the return of a subsequent call to T3RCompleteDMA().
		/// </summary> 
		/// <param name="buffer[]">an array of dwords (32bit) of at least 1/2 FIFOSIZE where the TTTR data can be stored (buffer[] must not be accessed until the function returns).</param>
		/// <param name="count">number of TTTR records to be fetched (max 1/2 FIFOSIZE), count must not be larger than the buffer[] size.</param>
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>        
		public static int TH_T3RStartDMA(uint[] buffer, uint count)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			if (count < DMABLOCKSZ)  //check if we have the right buffer[] size (must be at least DMABLOCKSZ or bigger)
			{
				return _iErrorCode = -24;  //due to wrong buffer size return error code (-24 = ERROR_DMA_FAIL)
			}


			// Copy FiFo to a supplied buffer (it basically makes/emulates the DMA transfer)
			ThreadsStatus.DMAReadThread.Thread = new Thread(() => CopyFiFoToBuffer(buffer));  //execute function in a separate thread. Note that the thread status is kept in ThreadsStatus structure (see DMAReadThread variable in there)
			ThreadsStatus.DMAReadThread.Thread.Name = "CopyFiFoToBuffer";  //set the name of the thread
			ThreadsStatus.DMAReadThread.Thread.IsBackground = true;  //set the thread as a background thread
			ThreadsStatus.DMAReadThread.Thread.Priority = ThreadPriority.Normal;  //set the thread priority to above normal in order to assure we collect all buffer events from FiFo
			ThreadsStatus.DMAReadThread.Thread.Start();  //start the thread            


			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_T3RCompleteDMA" - it returns the number of transferred records (if >=0) or error (if <0). 
		/// The function waits for DMA to complete. CPU time during wait for DMA completion will be yielded to other processes/threads.
		/// Function returns after timeout period of 300ms, even if not all data could be fetched. buffer[] past in T3RStartDMA() must 
		/// not be accessed until T3RCompleteDMA().
		/// </summary> 
		/// <returns>int: if return value >=0 (success, the number of records obtained), if <0 (error)</returns>
		public static int TH_T3RCompleteDMA()
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			//while (ThreadsStatus.DMAReadThread.Thread.IsAlive)  //wait until TH_T3RStartDMA() thread finishes DMA transfer
			//{
			//    System.Threading.Thread.Sleep(1);  //put the thread to sleep for about 1ms
			//}

			ThreadsStatus.DMAReadThread.Thread.Join();  //wait until TH_T3RStartDMA() thread finishes DMA transfer

			_iErrorCode = ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords;  //number of records transferred from FiFo to a supplied buffer
			ThreadsStatus.GetFiFoThread.FiFoBufferNumberOfRecords = 0;

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_T3RSetMarkerEdges" - it sets the active edges of the TTL signals that will appear as markers in TTTR data stream.
		/// Three markers available, me0/me1/me2 = 0 (falling edge) or 1 (rising edge).
		/// </summary> 
		/// <param name="me0">Marker 0, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param>        
		/// <param name="me1">Marker 1, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param>
		/// <param name="me2">Marker 2, set the active TTL edge: 0 - falling edge or 1 - rising edge.</param> 
		/// <returns>int: if return value =0 (success), if <0 (error)</returns>         
		public static int TH_T3RSetMarkerEdges(int me0, int me1, int me2)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			if (!m_IsCalibrated)  //we need calibration to get the count rate
			{
				return _iErrorCode = -23;  //due to the lack of calibration return error code (-23 = ERROR_NOT_CALIBRATED)            
			}

			return _iErrorCode;
		}


		/////////////////////////////////////////////////////////////////////////////
		// Special functions for ROUTING (Must have PRT/NRT 400 Router)

		/// <summary>
		/// Emulation of THLib function "TH_GetRoutingChannels" - it returns the number of routing channels available (if >0) or error (if <0).
		/// </summary>     
		/// <returns>int: returns the number of routing channels available (if >0, 1 = no router) or error (if <0)</returns>         
		public static int TH_GetRoutingChannels()
		{
			int _iErrorCode = 1;  //initialize with one, so no error

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_EnableRouting" - it returns the number of routing channels available (if >0) or error (if <0).
		/// </summary> 
		/// <param name="enable">enable = 0 (disable routing) or 1 (enable routing).</param>        
		/// <returns>int: if return value =0 (success), if <0 (most likely: no router found)</returns>         
		public static int TH_EnableRouting(int enable)
		{
			int _iErrorCode = 0;  //initialize with zero, so no error

			return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_SetNRT400CFD" - (Note: Must have NRT Router) set NRT 400 CFD level.
		/// </summary> 
		/// <param name="channel">channel = 0..3.</param>        
		/// <param name="level">level (discrimination level) = 0..400 (mV).</param>     
		/// <param name="zerocross">zerocross (zero cross voltage) = 0..40 (mV).</param>     
		/// <returns>int: if return value = 0 (success), if <0 (most likely: no router found)</returns>         
		public static int TH_SetNRT400CFD(int channel, int level, int zerocross)
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/////////////////////////////////////////////////////////////////////////////
		// Special functions for SCANNING with Piezo stage (Must have SCX Controller)

		/// <summary>
		/// Emulation of THLib function "TH_ScanEnable" - disable (enable=0) or enable (enable=1) scan.
		/// </summary> 
		/// <param name="enable">disable (enable=0) or enable (enable=1) scan</param>        
		/// <returns>int: if return value = 0 (success), if <0 (most likely: no scan controller found)</returns>         
		public static int TH_ScanEnable(int enable)
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_ScanGotoXY" - go to position (x=0..4095, y=0..4095) with shutter = 1/0 (TTL shutter output high/low).
		/// </summary> 
		/// <param name="x">position x=0..4095.</param>
		/// <param name="y">position y=0..4095.</param>
		/// <param name="shutter">shutter = 1/0 (TTL shutter output high/low).</param> 
		/// <returns>int: if return value = 0 (success), if <0 (error)</returns>         
		public static int TH_ScanGotoXY(int x, int y, int shutter)
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_ScanDone" - check the scan status.
		/// </summary>        
		/// <returns>int: if return value = 0 (success -> done), if <0 (error -> not finished)</returns>         
		public static int TH_ScanDone()
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_ScanSetup" - setup scan frame (scan is performed the next run of TTTR mode).
		/// </summary> 
		/// <param name="startx">starting position x=0..4095.</param>
		/// <param name="starty">starting position y=0..4095.</param>
		/// <param name="widthx">scan region width x=2..4096.</param>
		/// <param name="widthy">scan region width y=2..4096.</param>
		/// <param name="pixtime">pixel dwell time in us (100..3200).</param>
		/// <param name="pause">pause at turning position of x axis (0..15).</param>
		/// <param name="shutterdel">shutter delay to start of scan in ms (0..1000).</param> 
		/// <returns>int: if return value = 0 (success), if <0 (error)</returns>         
		public static int TH_ScanSetup(int startx, int starty, int widthx, int widthy, int pixtime, int pause, int shutterdel)
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}


		/// <summary>
		/// Emulation of THLib function "TH_ScanReset" - it resets scanner.
		/// This call is only required at program start or recover from an unexpected error or crash.
		/// </summary>        
		/// <returns>int: if return value = 0 (success), if <0 (error)</returns>         
		public static int TH_ScanReset()
		{
			//int _iErrorCode = 0;  //initialize with zero, so no error

			// Not developed yet. 
			throw new NotImplementedException();

			//return _iErrorCode;
		}



		//// END The Emulated DLL functions
		/////////////////////////////////////////////////////////////////


		////////////// END Emulation of the Time Harp 200 THLib DLL functions
		////////////////////////////////////////////////////////////////////////////

		#endregion Emulation of the Time Harp 200 THLib DLL functions
	}





}
