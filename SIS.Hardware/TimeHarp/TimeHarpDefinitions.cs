// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="TimeHarpDefinitions.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This class provides the DLL functions for interacting with Time Harp 200 board.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.TimeHarp
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// This class provides the DLL functions for interacting with Time Harp 200 board.
    /// </summary>
    public static class TimeHarpDefinitions
    {
        ///////////////////////////////////////////
        // TimeHarp DLL functions and constants. //
        ///////////////////////////////////////////
        #region Constants

        /// <summary>
        /// The acqtmax.
        /// </summary>
        public const int ACQTMAX = 36000000; // maximum Acquisition time in [ms] = 10*60*60*1000ms = 10h

        /// <summary>
        /// The acqtmin.
        /// </summary>
        public const int ACQTMIN = 1; // minimum Acquisition time in [ms]

        /// <summary>
        /// The acq t_ default.
        /// </summary>
        public const int ACQT_DEFAULT = 1000; // default acquisition time in [ms]

        /// <summary>
        /// The banksize.
        /// </summary>
        public const int BANKSIZE = CURVES * BLOCKSIZE; // max. histograms 

        /// <summary>
        /// The blocksize.
        /// </summary>
        public const int BLOCKSIZE = 4096; // max. histograms per block

        /// <summary>
        /// The crlf.
        /// </summary>
        public const string CRLF = "\r\n"; // new line character

        /// <summary>
        /// The curves.
        /// </summary>
        public const int CURVES = 32; // max. histogram blocks

        /// <summary>
        /// The comment field.
        /// </summary>
        public const string CommentField = "SIS software created the TTTR file";

        // comment field - now we just note that SIS software was used to create the file

        /// <summary>
        /// The creator name.
        /// </summary>
        public const string CreatorName = "TimeHarp Software"; // software creator name string

        /// <summary>
        /// The creator version.
        /// </summary>
        public const string CreatorVersion = "6.1.0.0"; // software creator version string

        /// <summary>
        /// The discrmax.
        /// </summary>
        public const int DISCRMAX = 400; // maximum discriminator level in [mV]

        /// <summary>
        /// The discrmin.
        /// </summary>
        public const int DISCRMIN = 0; // minimum discriminator level in [mV]

        /// <summary>
        /// Additional constants:
        /// </summary>        
        public const int DISCR_DEFAULT = 50; // default discriminator level in [mV]

        /// <summary>
        /// The dmablocksz.
        /// </summary>
        public const int DMABLOCKSZ = FIFOSIZE / 2; // DMA block size = 2^16 = 65536  

        /// <summary>
        /// The following constants are taken from thlib.defin.
        /// </summary>             
        public const int FIFOSIZE = 131072; // Fifo buffer size = 128K = 2*2^16 event records

        /// <summary>
        /// The fla g_ fifoempty.
        /// </summary>
        public const int FLAG_FIFOEMPTY = 0x2000; // =8192, the bit pattern for FiFo Empty state

        /// <summary>
        /// The fla g_ fifofull.
        /// </summary>
        public const int FLAG_FIFOFULL = 0x0800; // =2048, the bit pattern for FifoFull state

        /// <summary>
        /// The fla g_ fifohalffull.
        /// </summary>
        public const int FLAG_FIFOHALFFULL = 0x1000; // =4096, the bit pattern for FifoHalfFull state

        /// <summary>
        /// The fla g_ overflow.
        /// </summary>
        public const int FLAG_OVERFLOW = 0x0200; // =512, the bit pattern for Overflow state

        /// <summary>
        /// The fla g_ ramready.
        /// </summary>
        public const int FLAG_RAMREADY = 0x0400; // =1024, the bit pattern for RAM Ready state

        /// <summary>
        /// The fla g_ scanactive.
        /// </summary>
        public const int FLAG_SCANACTIVE = 0x4000; // =16384, the bit pattern for Scan Active state

        /// <summary>
        /// Status flags - a particular flag can be extracted by bitwise AND between 
        /// the complete status flag and the respective flag constant below.
        /// </summary>        
        public const int FLAG_SYSERR = 0x0100; // =256, the bit pattern for System Error

        /// <summary>
        /// The format version.
        /// </summary>
        public const string FormatVersion = "6.0"; // format version string

        /// <summary>
        /// The ident string.
        /// </summary>
        public const string IdentString = "TimeHarp 200"; // board identification string

        /// <summary>
        /// The maximu m_ ttt r_ record s_ pe r_ file.
        /// </summary>
        public const int MAXIMUM_TTTR_RECORDS_PER_FILE = 268435456;

        // =2^28 (= 268 435 456) maximum records per TTTR file allowed

        /// <summary>
        /// The offsetmax.
        /// </summary>
        public const int OFFSETMAX = 2000; // maximum START-STOP time offset in [ns]

        /// <summary>
        /// The offsetmin.
        /// </summary>
        public const int OFFSETMIN = 0; // minimum START-STOP time offset in [ns]

        /// <summary>
        /// The offse t_ default.
        /// </summary>
        public const int OFFSET_DEFAULT = 0; // default offset value in [ns]

        /// <summary>
        /// The overflowmax.
        /// </summary>
        public const int OVERFLOWMAX = 65535; // maximum overflow level

        /// <summary>
        /// The overflowmin.
        /// </summary>
        public const int OVERFLOWMIN = 0; // minimum overflow level

        /// <summary>
        /// The ranges.
        /// </summary>
        public const int RANGES = 6;

        // Number of ranges in terms of an integer number (the real time resolution is then 2^range number multiplied by Time Harp base time resolution)

        /// <summary>
        /// The range s_ default.
        /// </summary>
        public const int RANGES_DEFAULT = 0; // default Range value (min = 0, max = 5)

        /// <summary>
        /// The sc x_ pausemax.
        /// </summary>
        public const int SCX_PAUSEMAX = 15; // pixel durations

        /// <summary>
        /// The sc x_ pausemin.
        /// </summary>
        public const int SCX_PAUSEMIN = 0; // pixel durations

        /// <summary>
        /// The sc x_ shutdelmax.
        /// </summary>
        public const int SCX_SHUTDELMAX = 1000; // ms

        /// <summary>
        /// The sc x_ shutdelmin.
        /// </summary>
        public const int SCX_SHUTDELMIN = 0; // ms

        /// <summary>
        /// The sc x_ tpixmax.
        /// </summary>
        public const int SCX_TPIXMAX = 3200; // us

        /// <summary>
        /// The sc x_ tpixmin.
        /// </summary>
        public const int SCX_TPIXMIN = 100; // us

        /// <summary>
        /// The sc x_ xmax.
        /// </summary>
        public const int SCX_XMAX = 4095; // Piezo stage (Must have SCX Controller) max. X pixels

        /// <summary>
        /// The sc x_ xmin.
        /// </summary>
        public const int SCX_XMIN = 0; // Piezo stage (Must have SCX Controller) min. X pixels

        /// <summary>
        /// The sc x_ ymax.
        /// </summary>
        public const int SCX_YMAX = 4095; // Piezo stage (Must have SCX Controller) max. X pixels

        /// <summary>
        /// The sc x_ ymin.
        /// </summary>
        public const int SCX_YMIN = 0; // Piezo stage (Must have SCX Controller) min. Y pixels

        /// <summary>
        /// The syncmax.
        /// </summary>
        public const int SYNCMAX = 400; // maximum SYNC level in [mV]

        /// <summary>
        /// The syncmin.
        /// </summary>
        public const int SYNCMIN = -1300; // minimum SYNC level in [mV]

        /// <summary>
        /// The syn c_ default.
        /// </summary>
        public const int SYNC_DEFAULT = -50; // default SYNC level in [mV]

        /// <summary>
        /// The script name.
        /// </summary>
        public const string ScriptName = "ScriptName"; // script name - senseless for us

        /// <summary>
        /// The size tttr custom sis file header.
        /// </summary>
        public const int SizeTTTRCustomSISFileHeader = 140; // the size of the TTTR custom SIS header in bytes

        /// <summary>
        /// The size tttr default file header.
        /// </summary>
        public const int SizeTTTRDefaultFileHeader = 640; // the size of the default TTTR header in bytes

        /// <summary>
        /// The size tttr file header.
        /// </summary>
        public const int SizeTTTRFileHeader = 780; // the size of the TTTR header in bytes

        /// <summary>
        /// The th lib name.
        /// </summary>
        public const string THLibName = "THLib.dll"; // the DLL library

        /// <summary>
        /// The target lib version.
        /// </summary>
        public const string TargetLibVersion = "6.1"; // this is what this program was written for

        /// <summary>
        /// The version tttr custom sis file header.
        /// </summary>
        public const string VersionTTTRCustomSISFileHeader = "0.1"; // the version of the TTTR custom SIS header

        /// <summary>
        /// The zcmax.
        /// </summary>
        public const int ZCMAX = 40; // maximum zero level in [mV]

        /// <summary>
        /// The zcmin.
        /// </summary>
        public const int ZCMIN = 0; // minimum zero level in [mV]

        /// <summary>
        /// The z c_ default.
        /// </summary>
        public const int ZC_DEFAULT = 20; // default zero level in [mV]

        #endregion

        #region Enums

        /// <summary>
        /// Error codes returned by TimeHarp THLIB v6.1, February 2009
        /// </summary>   
        public enum THError
        {
            /// <summary>
            /// The erro r_ none.
            /// </summary>
            ERROR_NONE = 0, 

            /// <summary>
            /// The erro r_ devic e_ ope n_ fail.
            /// </summary>
            ERROR_DEVICE_OPEN_FAIL = -1, 

            /// <summary>
            /// The erro r_ devic e_ busy.
            /// </summary>
            ERROR_DEVICE_BUSY = -2, 

            /// <summary>
            /// The erro r_ devic e_ heven t_ fail.
            /// </summary>
            ERROR_DEVICE_HEVENT_FAIL = -3, 

            /// <summary>
            /// The erro r_ devic e_ callbse t_ fail.
            /// </summary>
            ERROR_DEVICE_CALLBSET_FAIL = -4, 

            /// <summary>
            /// The erro r_ devic e_ barma p_ fail.
            /// </summary>
            ERROR_DEVICE_BARMAP_FAIL = -5, 

            /// <summary>
            /// The erro r_ devic e_ clos e_ fail.
            /// </summary>
            ERROR_DEVICE_CLOSE_FAIL = -6, 

            /// <summary>
            /// The erro r_ devic e_ rese t_ fail.
            /// </summary>
            ERROR_DEVICE_RESET_FAIL = -7, 

            /// <summary>
            /// The erro r_ devic e_ versio n_ fail.
            /// </summary>
            ERROR_DEVICE_VERSION_FAIL = -8, 

            /// <summary>
            /// The erro r_ devic e_ versio n_ mismatch.
            /// </summary>
            ERROR_DEVICE_VERSION_MISMATCH = -9, 

            /// <summary>
            /// The erro r_ instanc e_ running.
            /// </summary>
            ERROR_INSTANCE_RUNNING = -16, 

            /// <summary>
            /// The erro r_ invali d_ argument.
            /// </summary>
            ERROR_INVALID_ARGUMENT = -17, 

            /// <summary>
            /// The erro r_ invali d_ mode.
            /// </summary>
            ERROR_INVALID_MODE = -18, 

            /// <summary>
            /// The erro r_ invali d_ option.
            /// </summary>
            ERROR_INVALID_OPTION = -19, 

            /// <summary>
            /// The erro r_ invali d_ memory.
            /// </summary>
            ERROR_INVALID_MEMORY = -20, 

            /// <summary>
            /// The erro r_ invali d_ rdata.
            /// </summary>
            ERROR_INVALID_RDATA = -21, 

            /// <summary>
            /// The erro r_ no t_ initialized.
            /// </summary>
            ERROR_NOT_INITIALIZED = -22, 

            /// <summary>
            /// The erro r_ no t_ calibrated.
            /// </summary>
            ERROR_NOT_CALIBRATED = -23, 

            /// <summary>
            /// The erro r_ dm a_ fail.
            /// </summary>
            ERROR_DMA_FAIL = -24, 

            /// <summary>
            /// The erro r_ xtdevic e_ fail.
            /// </summary>
            ERROR_XTDEVICE_FAIL = -25, 

            /// <summary>
            /// The erro r_ cali b_ maxre t_ fail.
            /// </summary>
            ERROR_CALIB_MAXRET_FAIL = -32, 

            /// <summary>
            /// The erro r_ cali b_ regini t_ fail.
            /// </summary>
            ERROR_CALIB_REGINIT_FAIL = -33, 

            /// <summary>
            /// The erro r_ cali b_ cvalerrbit.
            /// </summary>
            ERROR_CALIB_CVALERRBIT = -34, 

            /// <summary>
            /// The erro r_ cali b_ mvalerrbit.
            /// </summary>
            ERROR_CALIB_MVALERRBIT = -35, 

            /// <summary>
            /// The erro r_ cali b_ mfi t_ fail.
            /// </summary>
            ERROR_CALIB_MFIT_FAIL = -36, 

            /// <summary>
            /// The erro r_ cali b_ minvalid.
            /// </summary>
            ERROR_CALIB_MINVALID = -37, 

            /// <summary>
            /// The erro r_ sc x_ no t_ present.
            /// </summary>
            ERROR_SCX_NOT_PRESENT = -48, 

            /// <summary>
            /// The erro r_ sc x_ no t_ enabled.
            /// </summary>
            ERROR_SCX_NOT_ENABLED = -49, 

            /// <summary>
            /// The erro r_ sc x_ rese t_ fail.
            /// </summary>
            ERROR_SCX_RESET_FAIL = -50, 

            /// <summary>
            /// The erro r_ sc x_ no t_ active.
            /// </summary>
            ERROR_SCX_NOT_ACTIVE = -51, 

            /// <summary>
            /// The erro r_ sc x_ stil l_ active.
            /// </summary>
            ERROR_SCX_STILL_ACTIVE = -52, 

            /// <summary>
            /// The erro r_ sc x_ gotox y_ fail.
            /// </summary>
            ERROR_SCX_GOTOXY_FAIL = -53, 

            /// <summary>
            /// The erro r_ sc x_ scpro g_ fail.
            /// </summary>
            ERROR_SCX_SCPROG_FAIL = -54, 

            /// <summary>
            /// The erro r_ hardwar e_ f 01.
            /// </summary>
            ERROR_HARDWARE_F01 = -64, 

            /// <summary>
            /// The erro r_ hardwar e_ f 02.
            /// </summary>
            ERROR_HARDWARE_F02 = -65, 

            /// <summary>
            /// The erro r_ hardwar e_ f 03.
            /// </summary>
            ERROR_HARDWARE_F03 = -66, 

            /// <summary>
            /// The erro r_ hardwar e_ f 04.
            /// </summary>
            ERROR_HARDWARE_F04 = -67, 

            /// <summary>
            /// The erro r_ hardwar e_ f 05.
            /// </summary>
            ERROR_HARDWARE_F05 = -68, 

            /// <summary>
            /// The erro r_ hardwar e_ f 06.
            /// </summary>
            ERROR_HARDWARE_F06 = -69, 

            /// <summary>
            /// The erro r_ hardwar e_ f 07.
            /// </summary>
            ERROR_HARDWARE_F07 = -70, 

            /// <summary>
            /// The erro r_ hardwar e_ f 08.
            /// </summary>
            ERROR_HARDWARE_F08 = -71, 

            /// <summary>
            /// The erro r_ hardwar e_ f 09.
            /// </summary>
            ERROR_HARDWARE_F09 = -72, 

            /// <summary>
            /// The erro r_ hardwar e_ f 10.
            /// </summary>
            ERROR_HARDWARE_F10 = -73, 

            /// <summary>
            /// The erro r_ hardwar e_ f 11.
            /// </summary>
            ERROR_HARDWARE_F11 = -74, 

            /// <summary>
            /// The erro r_ hardwar e_ f 12.
            /// </summary>
            ERROR_HARDWARE_F12 = -75, 

            /// <summary>
            /// The erro r_ hardwar e_ f 13.
            /// </summary>
            ERROR_HARDWARE_F13 = -76, 

            /// <summary>
            /// The erro r_ hardwar e_ f 14.
            /// </summary>
            ERROR_HARDWARE_F14 = -77, 

            /// <summary>
            /// The erro r_ hardwar e_ f 15.
            /// </summary>
            ERROR_HARDWARE_F15 = -78, 
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The t h_ ctc status.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_CTCStatus")]
        public static extern int TH_CTCStatus();

        /// <summary>
        /// The t h_ calibrate.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_Calibrate")]
        public static extern int TH_Calibrate();

        /// <summary>
        /// The t h_ clear hist mem.
        /// </summary>
        /// <param name="block">
        /// The block.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ClearHistMem")]
        public static extern int TH_ClearHistMem(int block);

        /// <summary>
        /// The t h_ enable routing.
        /// </summary>
        /// <param name="enable">
        /// The enable.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_EnableRouting")]
        public static extern int TH_EnableRouting(int enable);

        /// <summary>
        /// The t h_ get bank.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="chanfrom">
        /// The chanfrom.
        /// </param>
        /// <param name="chanto">
        /// The chanto.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBank")]
        public static extern int TH_GetBank(ushort[] buffer, int chanfrom, int chanto);

        /// <summary>
        /// The t h_ get base resolution.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBaseResolution")]
        public static extern int TH_GetBaseResolution();

        /// <summary>
        /// The t h_ get block.
        /// </summary>
        /// <param name="chcount">
        /// The chcount.
        /// </param>
        /// <param name="block">
        /// The block.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetBlock")]
        public static extern int TH_GetBlock(uint[] chcount, int block);

        /// <summary>
        /// The t h_ get count rate.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetCountRate")]
        public static extern int TH_GetCountRate();

        /// <summary>
        /// THLib function "TH_GetElapsedMeasTime" - returns the elapsed measurement time in ms (not for continuous mode).
        /// </summary> 
        /// <returns>int: returns the elapsed measurement time in ms (not for continuous mode)</returns>
        [DllImport(THLibName, EntryPoint = "TH_GetElapsedMeasTime")]
        public static extern int TH_GetElapsedMeasTime();

        /// <summary>
        /// The t h_ get error string.
        /// </summary>
        /// <param name="errstring">
        /// The errstring.
        /// </param>
        /// <param name="errcode">
        /// The errcode.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetErrorString")]
        public static extern int TH_GetErrorString(StringBuilder errstring, int errcode);

        /// <summary>
        /// THLib function "TH_GetFlags" - returns the current status flags (a bit pattern).
        /// Use the predefined flags (e.g. FLAG_OVERFLOW) and bitwise AND to extract individual bits (you can call this function anytime during a measurement but not during DMA).
        /// </summary> 
        /// <returns>int: returns current status flags (a bit pattern)</returns>  
        [DllImport(THLibName, EntryPoint = "TH_GetFlags")]
        public static extern int TH_GetFlags();

        /// <summary>
        /// The t h_ get hardware version.
        /// </summary>
        /// <param name="vers">
        /// The vers.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetHardwareVersion")]
        public static extern int TH_GetHardwareVersion(StringBuilder vers);

        /// <summary>
        /// The t h_ get library version.
        /// </summary>
        /// <param name="vers">
        /// The vers.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetLibraryVersion")]
        public static extern int TH_GetLibraryVersion(StringBuilder vers);

        /// <summary>
        /// The t h_ get lost blocks.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetLostBlocks")]
        public static extern int TH_GetLostBlocks();

        /// <summary>
        /// The t h_ get resolution.
        /// </summary>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetResolution")]
        public static extern float TH_GetResolution();

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for TTTR mode (Time Harp must have TTTR option activated/purchased)

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for ROUTING (Must have PRT/NRT 400 Router)

        /// <summary>
        /// The t h_ get routing channels.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetRoutingChannels")]
        public static extern int TH_GetRoutingChannels();

        /// <summary>
        /// The t h_ get serial number.
        /// </summary>
        /// <param name="serial">
        /// The serial.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_GetSerialNumber")]
        public static extern int TH_GetSerialNumber(StringBuilder serial);

        /// <summary>
        /// The t h_ initialize.
        /// </summary>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_Initialize")]
        public static extern int TH_Initialize(int mode);

        /// <summary>
        /// The t h_ next offset.
        /// </summary>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_NextOffset")]
        public static extern int TH_NextOffset(int direction);

        /// <summary>
        /// The t h_ scan done.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ScanDone")]
        public static extern int TH_ScanDone();

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for SCANNING with Piezo stage (Must have SCX Controller)

        /// <summary>
        /// The t h_ scan enable.
        /// </summary>
        /// <param name="enable">
        /// The enable.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ScanEnable")]
        public static extern int TH_ScanEnable(int enable);

        /// <summary>
        /// The t h_ scan goto xy.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="shutter">
        /// The shutter.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ScanGotoXY")]
        public static extern int TH_ScanGotoXY(int x, int y, int shutter);

        /// <summary>
        /// The t h_ scan reset.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ScanReset")]
        public static extern int TH_ScanReset();

        /// <summary>
        /// The t h_ scan setup.
        /// </summary>
        /// <param name="startx">
        /// The startx.
        /// </param>
        /// <param name="starty">
        /// The starty.
        /// </param>
        /// <param name="widthx">
        /// The widthx.
        /// </param>
        /// <param name="widthy">
        /// The widthy.
        /// </param>
        /// <param name="pixtime">
        /// The pixtime.
        /// </param>
        /// <param name="pause">
        /// The pause.
        /// </param>
        /// <param name="shutterdel">
        /// The shutterdel.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_ScanSetup")]
        public static extern int TH_ScanSetup(
            int startx, 
            int starty, 
            int widthx, 
            int widthy, 
            int pixtime, 
            int pause, 
            int shutterdel);

        /// <summary>
        /// The t h_ set cfd discr min.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetCFDDiscrMin")]
        public static extern int TH_SetCFDDiscrMin(int value);

        /// <summary>
        /// The t h_ set cfd zero cross.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetCFDZeroCross")]
        public static extern int TH_SetCFDZeroCross(int value);

        /// <summary>
        /// The t h_ set m mode.
        /// </summary>
        /// <param name="mmode">
        /// The mmode.
        /// </param>
        /// <param name="tacq">
        /// The tacq.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetMMode")]
        public static extern int TH_SetMMode(int mmode, int tacq);

        /// <summary>
        /// THLib function "TH_SetNRT400CFD" - (Note: Must have NRT Router) set NRT 400 CFD level.
        /// </summary>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <param name="zerocross">
        /// The zerocross.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetNRT400CFD")]
        public static extern int TH_SetNRT400CFD(int channel, int level, int zerocross);

        /// <summary>
        /// The t h_ set offset.
        /// </summary>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetOffset")]
        public static extern int TH_SetOffset(int offset);

        /// <summary>
        /// The t h_ set range.
        /// </summary>
        /// <param name="range">
        /// The range.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetRange")]
        public static extern int TH_SetRange(int range);

        /// <summary>
        /// The t h_ set stop overflow.
        /// </summary>
        /// <param name="StopOnOfl">
        /// The stop on ofl.
        /// </param>
        /// <param name="StopLevel">
        /// The stop level.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetStopOverflow")]
        public static extern int TH_SetStopOverflow(int StopOnOfl, int StopLevel);

        /// <summary>
        /// The t h_ set sync level.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetSyncLevel")]
        public static extern int TH_SetSyncLevel(int value);

        /// <summary>
        /// The t h_ set sync mode.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_SetSyncMode")]
        public static extern int TH_SetSyncMode();

        /// <summary>
        /// The t h_ shutdown.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_Shutdown")]
        public static extern int TH_Shutdown();

        /// <summary>
        /// The t h_ start meas.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_StartMeas")]
        public static extern int TH_StartMeas();

        /// <summary>
        /// The t h_ stop meas.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_StopMeas")]
        public static extern int TH_StopMeas();

        /// <summary>
        /// The t h_ t 3 r complete dma.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RCompleteDMA")]
        public static extern int TH_T3RCompleteDMA();

        /// <summary>
        /// The t h_ t 3 r do dma.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RDoDMA")]
        public static extern int TH_T3RDoDMA(uint[] buffer, uint count);

        /// <summary>
        /// THLib function "TH_T3RSetMarkerEdges" - it sets the active edges of the TTL signals that will appear as markers in TTTR data stream.
        /// Three markers available, me0/me1/me2 = 0 (falling edge) or 1 (rising edge).
        /// </summary>
        /// <param name="me0">
        /// Marker 0, set the active TTL edge: 0 - falling edge or 1 - rising edge.
        /// </param>
        /// <param name="me1">
        /// Marker 1, set the active TTL edge: 0 - falling edge or 1 - rising edge.
        /// </param>
        /// <param name="me2">
        /// Marker 2, set the active TTL edge: 0 - falling edge or 1 - rising edge.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RSetMarkerEdges")]
        public static extern int TH_T3RSetMarkerEdges(int me0, int me1, int me2);

        /// <summary>
        /// The t h_ t 3 r start dma.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport(THLibName, EntryPoint = "TH_T3RStartDMA")]
        public static extern int TH_T3RStartDMA(uint[] buffer, uint count);

        #endregion

        /// <summary>
        /// The binary header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BinaryHeader
        {
            // the following is binary header information (header length is 212 bytes)
            /// <summary>
            /// The number of channels.
            /// </summary>
            public int NumberOfChannels;

            // number of time channels (normally 4096). Note that it may be needed to read this value before further processing a TTTR file

            /// <summary>
            /// The number of curves.
            /// </summary>
            public int NumberOfCurves;

            // meaningless in TTTR file. Note that it may be needed to read this value before further processing a TTTR file

            /// <summary>
            /// The bits per channel.
            /// </summary>
            public int BitsPerChannel;

            // meaningless in TTTR file. Note that it may be needed to read this value before further processing a TTTR file

            /// <summary>
            /// The routing channels.
            /// </summary>
            public int RoutingChannels; // 1 to 4

            /// <summary>
            /// The number of boards.
            /// </summary>
            public int NumberOfBoards;

            // reserved, now 1. Note that it may be needed to read this value before further processing a TTTR file

            /// <summary>
            /// The active curve.
            /// </summary>
            public int ActiveCurve; // meaningless in TTTR file

            /// <summary>
            /// The measurement mode.
            /// </summary>
            public int MeasurementMode; // 2=TTTR

            /// <summary>
            /// The sub mode.
            /// </summary>
            public int SubMode; // 0=Standard (Timed), 1 or 2 = reserved, 3=Image

            /// <summary>
            /// The range no.
            /// </summary>
            public int RangeNo; // 0=base resolution, 1=x2, 2=x4, 3=x8, 4=x16, 5=32

            /// <summary>
            /// The offset.
            /// </summary>
            public int Offset; // offset (approx.) in [ns]

            /// <summary>
            /// The acquisition time.
            /// </summary>
            public int AcquisitionTime; // acquisition time in [ms]

            /// <summary>
            /// The stop at.
            /// </summary>
            public int StopAt; // meaningless in TTTR file

            /// <summary>
            /// The stop on ovfl.
            /// </summary>
            public int StopOnOvfl; // meaningless in TTTR file

            /// <summary>
            /// The restart.
            /// </summary>
            public int Restart; // meaningless in TTTR file

            /// <summary>
            /// The dispay lin log.
            /// </summary>
            public int DispayLinLog; // lin=0, log=1

            /// <summary>
            /// The display time axis from.
            /// </summary>
            public int DisplayTimeAxisFrom; // lower time axis bound for display in [ns]

            /// <summary>
            /// The display time axis to.
            /// </summary>
            public int DisplayTimeAxisTo; // upper time axis bound for display in [ns]

            /// <summary>
            /// The display counts axis from.
            /// </summary>
            public int DisplayCountsAxisFrom; // lower count axis bound for display

            /// <summary>
            /// The display counts axis to.
            /// </summary>
            public int DisplayCountsAxisTo; // upper count axis bound for display

            /// <summary>
            /// The display curves.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 8)]
            public tCurveMapping[] DisplayCurves; // tCurveMapping[8], meaningless in TTTR file

            /// <summary>
            /// The params.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 3)]
            public tParamStruct[] Params; // tParamStruct[3], reserved for automated measurements

            /// <summary>
            /// The repeat mode.
            /// </summary>
            public int RepeatMode; // reserved for automated measurements

            /// <summary>
            /// The repeats per curve.
            /// </summary>
            public int RepeatsPerCurve; // reserved for automated measurements

            /// <summary>
            /// The repeat time.
            /// </summary>
            public int RepeatTime; // reserved for automated measurements

            /// <summary>
            /// The repeat wait time.
            /// </summary>
            public int RepeatWaitTime; // reserved for automated measurements

            /// <summary>
            /// The script name.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public char[] ScriptName; // char[20], reserved for automated measurements
        }

        /// <summary>
        /// The board header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct BoardHeader
        {
            // the following is board header information (header length is 48 bytes)
            /// <summary>
            /// The hardware ident.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char[] HardwareIdent; // char[16], 'Time Harp'

            /// <summary>
            /// The hardware version.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public char[] HardwareVersion; // char[8], currently '2.4' or '2.5'

            /// <summary>
            /// The board serial.
            /// </summary>
            public int BoardSerial; // Board serial number

            /// <summary>
            /// The cfd zero cross.
            /// </summary>
            public int CFDZeroCross; // CFD zero cross level in [mV]

            /// <summary>
            /// The cfd discr min.
            /// </summary>
            public int CFDDiscrMin; // CFD min discriminator level in [mV]

            /// <summary>
            /// The sync level.
            /// </summary>
            public int SyncLevel; // SYNC trigger level in [mV]

            /// <summary>
            /// The curve offset.
            /// </summary>
            public int CurveOffset; // reserved, now 0             

            /// <summary>
            /// The resolution.
            /// </summary>
            public float Resolution; // resolution in [ns]
        }

        /// <summary>
        /// The custom sis header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct CustomSISHeader
        {
            // the following is the custom SIS header information (header length is 140 bytes) - we place this in the special header
            /// <summary>
            /// The version.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
            public char[] Version; // char[8], version of the SIS header, currently "0.1"

            /// <summary>
            /// The time p pixel.
            /// </summary>
            public double TimePPixel; // time per pixel in [ms]

            /// <summary>
            /// The init x nm.
            /// </summary>
            public double InitXNm; // initial offset along X axis in [nm]

            /// <summary>
            /// The init y nm.
            /// </summary>
            public double InitYNm; // initial offset along Y axis in [nm]

            /// <summary>
            /// The init z nm.
            /// </summary>
            public double InitZNm; // initial offset along Z axis in [nm]

            /// <summary>
            /// The x scan size nm.
            /// </summary>
            public double XScanSizeNm; // scan size along X axis in [nm] 

            /// <summary>
            /// The y scan size nm.
            /// </summary>
            public double YScanSizeNm; // scan size along Y axis in [nm] 

            /// <summary>
            /// The z scan size nm.
            /// </summary>
            public double ZScanSizeNm; // scan size along Z axis in [nm] 

            /// <summary>
            /// The image width px.
            /// </summary>
            public int ImageWidthPx; // image size along X axis in [px] 

            /// <summary>
            /// The image height px.
            /// </summary>
            public int ImageHeightPx; // image size along Y axis in [px] 

            /// <summary>
            /// The image depth px.
            /// </summary>
            public int ImageDepthPx; // image size along Z axis in [px] 

            /// <summary>
            /// The x over scan px.
            /// </summary>
            public int XOverScanPx; // over scan image size along X axis in [px] 

            /// <summary>
            /// The y over scan px.
            /// </summary>
            public int YOverScanPx; // over scan image size along Y axis in [px] 

            /// <summary>
            /// The z over scan px.
            /// </summary>
            public int ZOverScanPx; // over scan image size along Z axis in [px] 

            /// <summary>
            /// The sis channels.
            /// </summary>
            public int SISChannels; // number of channels that SIS can show/process at the same time

            /// <summary>
            /// The type of scan.
            /// </summary>
            public int TypeOfScan;

            // the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)

            /// <summary>
            /// The frame time out.
            /// </summary>
            public int FrameTimeOut;

            // frame time out - the period after which we force the processing of the raw counts and extract pixels

            /// <summary>
            /// The fi fo time out.
            /// </summary>
            public int FiFoTimeOut;

            // Time Harp's FiFo time out - the period after which we force the fetching of the raw events in the FiFo buffer

            /// <summary>
            /// The stack marker.
            /// </summary>
            public byte StackMarker; // value of external marker interpreted as a Stack marker

            /// <summary>
            /// The frame marker.
            /// </summary>
            public byte FrameMarker;

            // value of external marker interpreted as a Frame marker (note that Frame marker is also a line marker)

            /// <summary>
            /// The line marker.
            /// </summary>
            public byte LineMarker; // value of external marker interpreted as a Line marker

            /// <summary>
            /// The pixel marker.
            /// </summary>
            public byte PixelMarker; // value of external marker interpreted as a Pixel marker

            /// <summary>
            /// The galvo magnification objective.
            /// </summary>
            public double GalvoMagnificationObjective; // the magnification of the objective

            /// <summary>
            /// The galvo scan lens focal length.
            /// </summary>
            public double GalvoScanLensFocalLength; // the focal length of the scan lens in [mm]            

            /// <summary>
            /// The galvo range angle degrees.
            /// </summary>
            public double GalvoRangeAngleDegrees;

            // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)

            /// <summary>
            /// The galvo range angle int.
            /// </summary>
            public double GalvoRangeAngleInt;

            // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
        }

        /// <summary>
        /// The following structures are used to hold the TimeHarp file data
        /// They reflect the file Header structure.          
        /// </summary>            
        public struct StructTTTRFileHeader
        {
            // (header length is 640+140 bytes)
            // Default TTTR header part
            #region Fields

            /// <summary>
            /// The binary header.
            /// </summary>
            public BinaryHeader BinaryHeader; // the following is binary header information (header length is 212 bytes)

            /// <summary>
            /// The board header.
            /// </summary>
            public BoardHeader BoardHeader; // the following is board header information (header length is 48 bytes)

            // CUstom TTTR header part
            /// <summary>
            /// The custom sis header.
            /// </summary>
            public CustomSISHeader CustomSISHeader;

            // the following is SIS header information (header length is 140 bytes)  

            /// <summary>
            /// The tttr header.
            /// </summary>
            public TTTRHeader TTTRHeader; // the following is TTTR header information (header length is 52 bytes)

            /// <summary>
            /// The text header.
            /// </summary>
            public TextHeader TextHeader;

            #endregion

            // the following represents the readable ASCII file header portion (header length is 328 bytes)
        }

        /// <summary>
        /// The tttr header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TTTRHeader
        {
            // the following is TTTR header information (header length is 52 bytes) 
            /// <summary>
            /// The tttr globclock.
            /// </summary>
            public int TTTRGlobclock; // clock in [ns], normally 100

            /// <summary>
            /// The ext devices.
            /// </summary>
            public int ExtDevices; // 1 = PRT 400, 2 = NRT 400

            /// <summary>
            /// The reserved 1.
            /// </summary>
            public int Reserved1; // reserved field for future or custom use

            /// <summary>
            /// The reserved 2.
            /// </summary>
            public int Reserved2; // reserved field for future or custom use

            /// <summary>
            /// The reserved 3.
            /// </summary>
            public int Reserved3; // reserved field for future or custom use

            /// <summary>
            /// The reserved 4.
            /// </summary>
            public int Reserved4; // reserved field for future or custom use

            /// <summary>
            /// The reserved 5.
            /// </summary>
            public int Reserved5; // reserved field for future or custom use

            /// <summary>
            /// The sync rate.
            /// </summary>
            public int SyncRate; // SYNC rate as displayed in meter

            /// <summary>
            /// The average cfd rate.
            /// </summary>
            public int AverageCFDRate; // Average CFD rate as displayed in meter

            /// <summary>
            /// The stop after.
            /// </summary>
            public int StopAfter; // Stopped after this many ms

            /// <summary>
            /// The stop reason.
            /// </summary>
            public int StopReason; // 0=TimeOver, 1=Manual, 2=Overflow, 3=Error

            /// <summary>
            /// The number of records.
            /// </summary>
            public int NumberOfRecords;

            // Number of TTTR records. Note that it may be needed to read this value before further processing a TTTR file

            /// <summary>
            /// The special header length.
            /// </summary>
            public int SpecialHeaderLength;

            // Length of special header to follow (in 4 bytes portion - -> currently we predefine it and write there the custom SIS header). Note that you must skip this header length to get to the TTTR records
        }

        /// <summary>
        /// The text header.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct TextHeader
        {
            // the following represents the readable ASCII file header portion (header length is 328 bytes)
            /// <summary>
            /// The ident.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char[] Ident; // char[16], the string 'Time Harp'

            /// <summary>
            /// The format version.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
            public char[] FormatVersion; // char[6], currently '6.0'

            /// <summary>
            /// The creator name.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public char[] CreatorName; // char[18], 'TimeHarp Software' - use your own if you create such file

            /// <summary>
            /// The creator version.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
            public char[] CreatorVersion; // char[12], currently '6.1.0.0' - use your own if you create such file

            /// <summary>
            /// The file time.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
            public char[] FileTime; // char[18], the file creation date and time

            /// <summary>
            /// The crlf.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
            public char[] CRLF; // char[2], 0x0D, 0x0A carriage return, line feed

            /// <summary>
            /// The comment field.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public char[] CommentField; // char[256], any ASCII string
        }

        /// <summary>
        /// The t curve mapping.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct tCurveMapping
        {
            /// <summary>
            /// The map to.
            /// </summary>
            public int MapTo;

            /// <summary>
            /// The show.
            /// </summary>
            public int Show;
        }

        /// <summary>
        /// The following structures are used to hold the Time Harp header file data
        /// They reflect the TTTR file Header structure.          
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct tParamStruct
        {
            /// <summary>
            /// The start.
            /// </summary>
            public float Start;

            /// <summary>
            /// The step.
            /// </summary>
            public float Step;

            /// <summary>
            /// The end.
            /// </summary>
            public float End;
        }
    }
}