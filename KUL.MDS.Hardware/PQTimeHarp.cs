//for basic functions and data types			
//for StringBuilder 
//for File operations
//for images
//for images
//for Marshalling data and other functions for managed <-> unmanaged code interactions
//for performance testing

//for multi-threading
//using System.Threading.Tasks;  //for threading with Tasks and async/await asynchronous operations


namespace SIS.Hardware
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    using SIS.Hardware.TimeHarp;

    /// <summary>
    /// PQTimeHarp class provides support for Time Harp board.
    /// Note: Only one object instance is allowed to exist at a time from this class because the Time Harp DLL
    /// is not re-entrant (otherwise you can crash the PC).        
    /// </summary>
    public class PQTimeHarp : TimeHarpDLL
    {

		// For logging
		private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		// Events used within the class
		#region Events

		public event EventHandler PositionChanged;  // Event thrown whenever the position changed.
		public event EventHandler ErrorOccurred;  // Event thrown whenever the hardware generated an error.
		public event EventHandler EngagedChanged;  // Event thrown whenever the hardware is switched ON or OFF.

		#endregion Events


		// This class operates according to a singleton pattern. Having multiple PQTimeHarp could be dangerous because the hardware
        // could be left in an unknown state.
        //private static volatile PQTimeHarp m_instance;
        //private static object m_syncRoot = new object();

		//public static PQTimeHarp Instance
        //{
        //    get
        //    {
        //        if (m_instance == null)
        //        {
        //            lock (m_syncRoot)
        //            {
        //                if (m_instance == null)
        //                {
		//                    m_instance = new PQTimeHarp();
        //                }
        //            }
        //        }

        //        return m_instance;
        //    }
        //}


        
        #region Initialization and common functions

        /////////////////////////////////////////////////////////////////////////////
        // Initialization and common functions:
        
        /// <summary>
        /// Function that handles common exceptions.  
		/// Note: we override the inherited implementation since we want to add support for events, and handle errors through events.
        /// </summary>
        /// <param name="__iErrorCode">The error code, an integer value.</param>  
        /// <param name="__sAppendErrorMessage">Additional error string to be appended to the error message.</param> 
        protected override void CheckForException(int __iErrorCode, string __sAppendErrorMessage)
        {

            if (__iErrorCode < 0)  //check the success of the previous operation
            {                
                this.m_sbCurrentError = new StringBuilder("Time Harp: reporting an error --> " + __sAppendErrorMessage);
                this.m_sbCurrentError.Append(this.GetCurrentError(__iErrorCode));
                
                // Throw an ErrorOccurred event to inform the user.
                if (this.ErrorOccurred != null)
                {
                    _logger.Debug(this.m_sbCurrentError.ToString());
                    this.ErrorOccurred(this, new EventArgs());
                }
            }
            else
            {
                this.m_sbCurrentError = this.GetCurrentError(__iErrorCode);
            }            
        }


		/// <summary>
		/// Returns the current value of the count rate in [Hz] taken from Time Harp.
		/// </summary>
		public override int CountRate  //define property CountRate
		{
			get
			{
				// Set the correct variables to get the count rate
				int _iMeasurementMode = 0;  // measurement mode must be 0 - one-time histogramming and TTTR modes
				int _iAcquisitionTime = 1000;  // acquisition time in [ms]
				int _iCountRate = -1;  // set to -1 to indicate unsuccessful attempt to get the count rate

				// Check if there is some measurement running, if not return the count rate
				if (!this.m_IsMeasurementRunning)
				{
					_iCountRate = this.GetCountRate(_iMeasurementMode, _iAcquisitionTime);
					this.m_iCountRate = _iCountRate;  //keeps track of the last probed count rate value
				}

				return _iCountRate;
			}
		}


		/// <summary>
		/// Status of Time Harp board.
		/// </summary>
		private bool m_bIsInitialized = false;  // default is false, i.e. not initialized

		/// <summary>
		/// Returns the status of Time Harp board.
		/// </summary>
		public bool IsInitialized
		{
			get
			{
				return this.m_bIsInitialized;
			}
		}
				      

        /// <summary>
        /// Set the Time Harp  status as released.
        /// </summary>
        public void Release()
        {
            if (this.m_bIsInitialized)
            {
                this.m_bIsInitialized = false;

                // Reset error state to default, i.e. no error present
				this.m_iErrorCode = ERR_CODE_DEFAULT;
                
                if (this.EngagedChanged != null)
                {
                    this.EngagedChanged(this, new EventArgs());
                }

                _logger.Info("Time Harp: now Time Harp is OFF!"); 
            }
            else
            {
                _logger.Info("Time Harp: doing nothing, Time Harp is already OFF!");
            }
        }

		        
        /// <summary>
        /// Property - allows to stop Time Harp acquisition by setting its value to True.       
        /// </summary>
        private volatile bool m_IsMesurementToBeStopped = false;  //allows to stop Time Harp acquisition by setting its value to true

        /// <summary>
        /// Allows to stop Time Harp acquisition by setting IsMesurementToBeStop = True.        
        /// </summary>   
        private void RequestMeasurementStop()  //stop Time Harp acquisition by setting IsMesurementToBeStop = True
        {
            this.m_IsMesurementToBeStopped = true;  //allows to stop Time Harp acquisition by setting its value to true
        }


        /// <summary>
        /// The Time Harp acquisition status (if it is still measuring or not).        
        /// </summary>
        private volatile bool m_IsMeasurementRunning = false;  //keep the status of Time Harp acquisition (if it is still measuring or not)

        /// <summary>
        /// Property - the Time Harp acquisition status (if it is still measuring or not).        
        /// </summary>   
        public bool IsMeasurementRunning  //set or get property value
        {
            get { return this.m_IsMeasurementRunning; }
            //set { this.m_IsMeasurementRunning = IsRunning(); }
        }
				

		/// <summary>
		/// Returns the total pixels read from Time Harp buffer.
		/// </summary>
		public int TotalSamplesAcquired
		{
			get { return ScanStatus.TotalPixelsRead; }
			//private set { }
		}


		/// <summary>
		/// Stores the global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
		/// </summary>
		private static int m_iGlobalTTTRBufferSize = 100;  //define property

		/// <summary>
		/// Validate and then return the global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
		/// </summary>
		/// <param name="__iMarkerEdge">Global TTTR buffer size value to assign.</param>        
		/// <return>Returns the size of the global TTTR buffer after checking if it is within the allowed range.</return>
		private int GetGlobalTTTRBufferSize(int __iGlobalTTTRBufferSize)
		{
			// Check if __iGlobalTTTRBufferSize has a correct value
			if (!(__iGlobalTTTRBufferSize < 1 || __iGlobalTTTRBufferSize > 1000))
			{
				__iGlobalTTTRBufferSize = 100;  //set default to 100
			}

			return __iGlobalTTTRBufferSize;
		}

		/// <summary>
		/// Property - stores the global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)        
		/// </summary>
		public int GlobalTTTRBufferSize  //set or get property value
		{
			get { return m_iGlobalTTTRBufferSize; }
			set { m_iGlobalTTTRBufferSize = this.GetGlobalTTTRBufferSize(value); }
		}


		/// <summary>
		/// Stores the Line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
		/// </summary>
		private static int m_iLinePTTTRBufferSize = 50;  //define property

		/// <summary>
		/// Validate and then return the Line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
		/// </summary>
		/// <param name="__iLinePTTTRBufferSize">Line PTTTR buffer size value to assign.</param>        
		/// <return>Returns the size of the Line PTTTR buffer after checking if it is within the allowed range.</return>
		private int GetLinePTTTRBufferSize(int __iLinePTTTRBufferSize)
		{
			// Check if __iGlobalTTTRBufferSize has a correct value
			if (!(__iLinePTTTRBufferSize < 1 || __iLinePTTTRBufferSize > 1000))
			{
				__iLinePTTTRBufferSize = 50;  //set default to 50
			}

			return __iLinePTTTRBufferSize;
		}

		/// <summary>
		/// Property - stores the Line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)        
		/// </summary>
		public int LinePTTTRBufferSize  //set or get property value
		{
			get { return m_iLinePTTTRBufferSize; }
			set { m_iLinePTTTRBufferSize = this.GetLinePTTTRBufferSize(value); }
		}
		  		       	                	

        // END OF Initialization and common functions
        /////////////////////////////////////////////////////////////////////////////

        #endregion Initialization and common functions
				
		
		
		

		#region Special functions for TTTR file handling

        /////////////////////////////////////////////////////////////////////////////
        // Special functions for TTTR file handling

        /// <summary>
        /// Structure that keeps track of important files parameters and operations with files.
        /// </summary>            
        public struct Files
        {
            public const int MAXIMUM_TTTR_RECORDS = TimeHarpDefinitions.MAXIMUM_TTTR_RECORDS_PER_FILE;  //maximum TTTR records per file allowed
            public static string TTTRFileName = "";  //file name of the output data file
            public static string ASCIIFileName = "";  //file name of the output data file

            public static int FileCounter = 0;  // counts how many output files we have written to hard disk so far
            public static int RecordsCount = 0;  // counts how many records we have written to a TTTR binary file so far

            public static bool isTTTRFileFirstWrite = true;  //keep track of the TTTR file state - also it indicates if we have started to record the TTTR data or not
            public static Encoding ASCIIEncode = Encoding.ASCII;  //the Encoding class represents the char set encoding (.Net uses Unicode, i.e. UTF-16). For proper writing/reading from a TTTR file we need to use the methods of this class to convert between a .Net 16-bit char (Unicode) and 8-bit char (ASCII) and vice versa.

            public static TimeHarpDefinitions.StructTTTRFileHeader TTTRFileHeader;  //the header structure of Time Harp TTTR file 
        }


        /// <summary>
        /// Write the TTTR records (UInt32 numbers) to file.
        /// </summary>
        /// <param name="__TTTRBuffer">The TTTR buffer. It is an array of UInt32 numbers - represents the raw Time Harp data records.</param>
        /// <param name="__indexLowerTTTRBuffer">The lower index of the TTTR data buffer from where we start the saving of data.</param>                        
        /// <param name="__indexUpperTTTRBuffer">The upper index of the TTTR data buffer up to which we save data.</param> 
        /// <param name="__TTTRBuffer">The TTTR buffer. It is an array of UInt32 numbers - represents the raw Time Harp data records.</param>
        /// <param name="__sOutputFile">The name of the output TTTR file.</param>
        public void WriteTTTRDataToFile(UInt32[] __TTTRBuffer, int __indexLowerTTTRBuffer, int __indexUpperTTTRBuffer, string __sOutputFile)
        {
            // Write accumulated so far data to file
            FileStream _fsOutputFileStream = null;  //declare file stream
            BinaryWriter _bwOutputFile = null;  //declare binary writer 
            string _strCurrentFileName = "";

            if (Files.isTTTRFileFirstWrite)  //if it is the first time we store a buffer to a file, reset some parameters and create the TTTR file and write TTTR header to file
            {

                Files.RecordsCount = 0;  //reset TTTR records count
                Files.FileCounter = 0;  //reset file counter

                try
                {
                    _strCurrentFileName = Path.ChangeExtension(__sOutputFile, "." + Files.FileCounter.ToString() + ".t3r");  // set the current output file path and name - it adds a number so that we split the data to separate files if it exceeds the given limit
                    _fsOutputFileStream = File.Open(_strCurrentFileName, FileMode.Create, FileAccess.Write, FileShare.None); //create and open the output file for writing                   
                    _bwOutputFile = new BinaryWriter(_fsOutputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive

					this.WriteTTTRHeaderToFile(_bwOutputFile, ref Files.TTTRFileHeader);  //write TTTR header to file
                    WriteTTTRBufferToFile(_bwOutputFile, ref Files.RecordsCount, __indexLowerTTTRBuffer, __indexUpperTTTRBuffer, __TTTRBuffer); // TTTR buffer to file
                    WriteTTTRNumberOfRecordsToFile(_bwOutputFile, TimeHarpDefinitions.SizeTTTRDefaultFileHeader - 8, Files.RecordsCount);  //update the number of records written to the TTTR file so far                    
                }
                catch (Exception ex)
                {
                    //Shutdown();  //close Time harp 200 device
					//throw new Exception("KUL.MDS.Hardware.PQTimeHarp. Exception - " + "The file: " + _strCurrentFileName + " could not be created by WriteTTTRDataToFile() function!");

                    // Throw an ErrorOccurred event to inform the user.
                    if (this.ErrorOccurred != null)
                    {
						_logger.Error( "Time Harp: reporting error --> the file " + _strCurrentFileName + " could not be created by WriteTTTRDataToFile() function!" + " Exception Message: " + ex.Message );
                        this.ErrorOccurred(this, new EventArgs());
                    } 
                }

                Files.isTTTRFileFirstWrite = false;  //mark that the first write happened               
            }
            else  //if we write not for first time, it means we have to only append the binary data to the TTTR file
            {
                try  //create and open the file for writing
                {
                    if (Files.RecordsCount > Files.MAXIMUM_TTTR_RECORDS)  //if the number of TTTR records becomes bigger than Files.MAXIMUM_TTTR_RECORDS, create and write the buffer data to a new file
                    {
                        Files.RecordsCount = 0;  //reset records count to the proper value (= __indexGlobalTTTRBuffer)
                        Files.FileCounter++;  //count the number of output files. Note that we split the data acquired to many output files with certain size

                        _strCurrentFileName = Path.ChangeExtension(__sOutputFile, "." + Files.FileCounter.ToString() + ".t3r");  // set the current output file path and name - it adds a number so that we split the data to separate files if it exceeds the given limit
                        _fsOutputFileStream = File.Open(_strCurrentFileName, FileMode.Create, FileAccess.Write, FileShare.None); //create and open the output file for writing                        
                        _bwOutputFile = new BinaryWriter(_fsOutputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive

						this.WriteTTTRHeaderToFile(_bwOutputFile, ref Files.TTTRFileHeader);  //write TTTR header to file
                    }
                    else  //open the previous file and append the buffer data to it
                    {
                        _strCurrentFileName = Path.ChangeExtension(__sOutputFile, "." + Files.FileCounter.ToString() + ".t3r");  // set the current output file path and name - it adds a number so that we split the data to separate files if it exceeds the given limit
                        _fsOutputFileStream = File.Open(_strCurrentFileName, FileMode.Open, FileAccess.Write, FileShare.None);  //create and open the output file for writing                        
                        _bwOutputFile = new BinaryWriter(_fsOutputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive
                        _bwOutputFile.Seek(0, SeekOrigin.End);  //go to the end of file so that we can add the binary data to it
                    }

                    WriteTTTRBufferToFile(_bwOutputFile, ref Files.RecordsCount, __indexLowerTTTRBuffer, __indexUpperTTTRBuffer, __TTTRBuffer); // TTTR buffer to file
                    WriteTTTRNumberOfRecordsToFile(_bwOutputFile, TimeHarpDefinitions.SizeTTTRDefaultFileHeader - 8, Files.RecordsCount);  //update the number of records written to the TTTR file so far
                }
                catch (Exception ex)
                {
                    //Shutdown();  //close Time harp 200 device
					//throw new Exception("KUL.MDS.Hardware.PQTimeHarp. Exception - " + "The file: " + _strCurrentFileName + " could not be open in append mode by WriteTTTRDataToFile() function!");
                    
                    // Throw an ErrorOccurred event to inform the user.
                    if (this.ErrorOccurred != null)
                    {
						_logger.Error( "Time Harp: reporting error --> the file " + _strCurrentFileName + " could not be created by WriteTTTRDataToFile() function!" + " Exception Message: " + ex.Message );
                        this.ErrorOccurred(this, new EventArgs());
                    }
                }

            }


            // Close files
            _bwOutputFile.Close();
            _fsOutputFileStream.Close();
        }


        /// <summary>
        /// Write the Time Harp records (UInt32 numbers) from the global TTTR buffer to file.
        /// </summary> 
        /// <param name="__bwOutputFile">The binary writer stream that takes care to write the binary data to file.</param> 
        /// <param name="__iRecordsCount">The current number of TTTR data records.</param> 
        /// <param name="__indexLowerTTTRBuffer">The lower index of the TTTR data buffer from where we start the saving of data.</param>                        
        /// <param name="__indexUpperTTTRBuffer">The upper index of the TTTR data buffer up to which we save data.</param>                      
        /// <param name="__TTTRBuffer">The TTTR buffer. It is an array of UInt32 numbers - represents the raw Time Harp data records.</param>
        private static void WriteTTTRBufferToFile(BinaryWriter __bwOutputFile, ref int __iRecordsCount, int __indexLowerTTTRBuffer, int __indexUpperTTTRBuffer, UInt32[] __TTTRBuffer)
        {
            // TTTR buffer to file
            for (int i = __indexLowerTTTRBuffer; i < __indexUpperTTTRBuffer; i++)  //write data to file and reset to zero the data array elements
            {
                __bwOutputFile.Write(__TTTRBuffer[i]);  //write array elements to file                
            }

            __iRecordsCount += __indexUpperTTTRBuffer - __indexLowerTTTRBuffer;  //update number the records that have been written to file so far            
        }


        /// <summary>
        /// Returns a char array with predefined/fixed size - it is necessary for the TTTR file header to be written correctly.
        /// </summary>
        /// <param name="__InputString">The input string which will be converted to array of chars</param>
        /// <param name="__iCharLength">The length of the returned array of chars.</param>
        /// <returns>char[]: returns an array of chars with a length defined by __iCharLength</returns>
        private static char[] StringToCharArray(string __InputString, int __iCharLength)
        {
            char[] _charData = new char[__iCharLength];  //allocate char array of the given fixed size
            char[] _charBuffer = __InputString.ToCharArray();  //get the string into the char[] buffer

            int _iCharBufferLength = _charBuffer.Length;

            if (_iCharBufferLength > __iCharLength)  //the char buffer size must be smaller
            {
                _iCharBufferLength = __iCharLength;  //keep the maximum chars to be copied from buffer equal to the given __iCharLength
            }

            for (int i = 0; i < _iCharBufferLength; i++)  //copy chars to the array which will be returned
            {
                _charData[i] = _charBuffer[i];
            }

            for (int i = _iCharBufferLength; i < __iCharLength; i++)  //fill the rest with null character
            {
                _charData[i] = '\0';
            }

            return _charData;  //return the char array with the predefined size
        }


        /// <summary>
        /// Write the Time Harp header info to the TTTR file.
        /// </summary> 
        /// <param name="__bwOutputFile">The binary writer stream that takes care to write the binary data to file.</param>
		/// /// <param name="__TTTRFileHeader">A TTTR file header structure where the info from TTTR file header will be stored.</param>		
        public void WriteTTTRHeaderToFile(BinaryWriter __bwOutputFile, ref TimeHarpDefinitions.StructTTTRFileHeader __TTTRFileHeader)
        {
            // Write Text header to the TTTR file (header length is 328 bytes)            
			__TTTRFileHeader.TextHeader.Ident = StringToCharArray(TimeHarpDefinitions.IdentString, 16);
			__bwOutputFile.Write(__TTTRFileHeader.TextHeader.Ident);  //write char[16] to file

			__TTTRFileHeader.TextHeader.FormatVersion = StringToCharArray(TimeHarpDefinitions.FormatVersion, 6);
			__bwOutputFile.Write(__TTTRFileHeader.TextHeader.FormatVersion);  //write char[6] to file

			__TTTRFileHeader.TextHeader.CreatorName = StringToCharArray(TimeHarpDefinitions.CreatorName, 18);
			__bwOutputFile.Write(__TTTRFileHeader.TextHeader.CreatorName);  //write char[18] to file

			__TTTRFileHeader.TextHeader.CreatorVersion = StringToCharArray(TimeHarpDefinitions.CreatorVersion, 12);
            __bwOutputFile.Write(__TTTRFileHeader.TextHeader.CreatorVersion);  //write char[12] to file

            __TTTRFileHeader.TextHeader.FileTime = StringToCharArray(System.DateTime.Now.ToString("yyyy.MM.dd HH:mm"), 18);
            __bwOutputFile.Write(__TTTRFileHeader.TextHeader.FileTime);  //write char[18] to file

			__TTTRFileHeader.TextHeader.CRLF = StringToCharArray(TimeHarpDefinitions.CRLF, 2);
            __bwOutputFile.Write(__TTTRFileHeader.TextHeader.CRLF);  //write char[2] to file

            __TTTRFileHeader.TextHeader.CommentField = StringToCharArray(TimeHarpDefinitions.CommentField, 256);
            __bwOutputFile.Write(__TTTRFileHeader.TextHeader.CommentField);  //write char[256] to file


            // Write Binary header to the TTTR file (header length is 76 + 64 + 36 + 36 = 212 bytes)
            __TTTRFileHeader.BinaryHeader.NumberOfChannels = TimeHarpDefinitions.BLOCKSIZE;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.NumberOfChannels);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.NumberOfCurves = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.NumberOfCurves);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.BitsPerChannel = 32;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.BitsPerChannel);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.RoutingChannels = 1;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RoutingChannels);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.NumberOfBoards = 1;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.NumberOfBoards);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.ActiveCurve = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.ActiveCurve);  //write Int32 to file

			__TTTRFileHeader.BinaryHeader.MeasurementMode = this.m_iMeasurementMode + 2;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.MeasurementMode);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.SubMode = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.SubMode);  //write Int32 to file

			__TTTRFileHeader.BinaryHeader.RangeNo = this.m_iRangeCode;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RangeNo);  //write Int32 to file

			__TTTRFileHeader.BinaryHeader.Offset = this.m_iOffset;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.Offset);  //write Int32 to file

			__TTTRFileHeader.BinaryHeader.AcquisitionTime = this.m_iAcquisitionTime;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.AcquisitionTime);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.StopAt = TimeHarpDefinitions.OVERFLOWMAX;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.StopAt);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.StopOnOvfl = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.StopOnOvfl);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.Restart = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.Restart);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.DispayLinLog = 1;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DispayLinLog);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.DisplayTimeAxisFrom = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayTimeAxisFrom);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.DisplayTimeAxisTo = 160;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayTimeAxisTo);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.DisplayCountsAxisFrom = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayCountsAxisFrom);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.DisplayCountsAxisTo = 1000;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayCountsAxisTo);  //write Int32 to file


			if (__TTTRFileHeader.BinaryHeader.DisplayCurves == null)
			{
				__TTTRFileHeader.BinaryHeader.DisplayCurves = new TimeHarpDefinitions.tCurveMapping[8];  //allocate int struct array
			}
			
            for (int i = 0; i < 8; i++)
            {
				__TTTRFileHeader.BinaryHeader.DisplayCurves[i].MapTo = 0;
				__TTTRFileHeader.BinaryHeader.DisplayCurves[i].Show = 0;
				__bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayCurves[i].MapTo);  //write Int32 to file
				__bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.DisplayCurves[i].Show);  //write Int32 to file							
            }
			
		
			if (__TTTRFileHeader.BinaryHeader.Params == null)
			{
				__TTTRFileHeader.BinaryHeader.Params = new TimeHarpDefinitions.tParamStruct[3];  //allocate float struct array
			}

            for (int i = 0; i < 3; i++)
            {
				__TTTRFileHeader.BinaryHeader.Params[i].Start = 0.0f;
				__TTTRFileHeader.BinaryHeader.Params[i].Step = 0.0f;
				__TTTRFileHeader.BinaryHeader.Params[i].End = 0.0f;
				__bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.Params[i].Start);  //write float to file
				__bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.Params[i].Step);  //write float to file
				__bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.Params[i].End);  //write float to file							
            }

            __TTTRFileHeader.BinaryHeader.RepeatMode = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RepeatMode);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.RepeatsPerCurve = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RepeatsPerCurve);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.RepeatTime = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RepeatTime);  //write Int32 to file

            __TTTRFileHeader.BinaryHeader.RepeatWaitTime = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.RepeatWaitTime);  //write Int32 to file

			__TTTRFileHeader.BinaryHeader.ScriptName = StringToCharArray(TimeHarpDefinitions.ScriptName, 20);
            __bwOutputFile.Write(__TTTRFileHeader.BinaryHeader.ScriptName);  //write char[20] to file


            // Write BoardHeader header to the TTTR file (header length is 48 bytes)
			__TTTRFileHeader.BoardHeader.HardwareIdent = StringToCharArray(TimeHarpDefinitions.IdentString, 16);
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.HardwareIdent);  //write char[16] to file

			__TTTRFileHeader.BoardHeader.HardwareVersion = StringToCharArray(this.m_sHardwareVersion, 8);
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.HardwareVersion);  //write char[8] to file

			__TTTRFileHeader.BoardHeader.BoardSerial = Convert.ToInt32(this.m_sSerialNumber);
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.BoardSerial);  //write Int32 to file

			__TTTRFileHeader.BoardHeader.CFDZeroCross = this.m_iCFDZeroCross;
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.CFDZeroCross);  //write Int32 to file

			__TTTRFileHeader.BoardHeader.CFDDiscrMin = this.m_iCFDDiscrMin;
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.CFDDiscrMin);  //write Int32 to file

			__TTTRFileHeader.BoardHeader.SyncLevel = this.m_iSyncLevel;
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.SyncLevel);  //write Int32 to file

            __TTTRFileHeader.BoardHeader.CurveOffset = 0;
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.CurveOffset);  //write Int32 to file

			__TTTRFileHeader.BoardHeader.Resolution = this.m_fResolution;
            __bwOutputFile.Write(__TTTRFileHeader.BoardHeader.Resolution);  //write float to file


            // Write TTTR header to the TTTR file (header length is 52 bytes)
            __TTTRFileHeader.TTTRHeader.TTTRGlobclock = 100;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.TTTRGlobclock);  //write Int32 to file   

            __TTTRFileHeader.TTTRHeader.ExtDevices = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.ExtDevices);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.Reserved1 = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.Reserved1);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.Reserved2 = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.Reserved2);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.Reserved3 = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.Reserved3);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.Reserved4 = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.Reserved4);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.Reserved5 = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.Reserved5);  //write Int32 to file

            //Console.WriteLine("WriteTTTRHeaderToFile(): TTTRHeader.SyncRate = {0}, this.m_iSyncRate = {1}", __TTTRFileHeader.TTTRHeader.SyncRate, this.m_iSyncRate);  //for debugging purposes
			__TTTRFileHeader.TTTRHeader.SyncRate = this.m_iSyncRate;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.SyncRate);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.AverageCFDRate = this.m_iCountRate;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.AverageCFDRate);  //write Int32 to file

			__TTTRFileHeader.TTTRHeader.StopAfter = this.m_iAcquisitionTime;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.StopAfter);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.StopReason = 0;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.StopReason);  //write Int32 to file

			__TTTRFileHeader.TTTRHeader.NumberOfRecords = Files.RecordsCount;
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.NumberOfRecords);  //write Int32 to file

            __TTTRFileHeader.TTTRHeader.SpecialHeaderLength = TimeHarpDefinitions.SizeTTTRCustomSISFileHeader / 4;  // the length of the special header in 4 bytes portions (int type)
            __bwOutputFile.Write(__TTTRFileHeader.TTTRHeader.SpecialHeaderLength);  //write Int32 to file

            // Write CustomSISHeader to the TTTR file (header length is 140 bytes) - we use the special header to write info for our custom scan settings - we will call it custom SIS header
			__TTTRFileHeader.CustomSISHeader.Version = StringToCharArray(TimeHarpDefinitions.VersionTTTRCustomSISFileHeader, 8);
			__bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.Version);  //write char[8] to file

            __TTTRFileHeader.CustomSISHeader.TimePPixel = ScanStatus.TimePPixelMillisec;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.TimePPixel);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.InitXNm = ScanStatus.InitXNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.InitXNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.InitYNm = ScanStatus.InitYNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.InitYNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.InitZNm = ScanStatus.InitZNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.InitZNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.XScanSizeNm = ScanStatus.XScanSizeNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.XScanSizeNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.YScanSizeNm = ScanStatus.YScanSizeNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.YScanSizeNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.ZScanSizeNm = ScanStatus.ZScanSizeNm;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.ZScanSizeNm);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.ImageWidthPx = ScanStatus.XImageWidthPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.ImageWidthPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.ImageHeightPx = ScanStatus.YImageHeightPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.ImageHeightPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.ImageDepthPx = ScanStatus.ZImageDepthPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.ImageDepthPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.XOverScanPx = ScanStatus.XOverScanPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.XOverScanPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.YOverScanPx = ScanStatus.YOverScanPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.YOverScanPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.ZOverScanPx = ScanStatus.ZOverScanPx;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.ZOverScanPx);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.SISChannels = ScanStatus.SISChannels;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.SISChannels);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.TypeOfScan = ScanStatus.TypeOfScan;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.TypeOfScan);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.FrameTimeOut = ScanStatus.FrameTimeOut;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.FrameTimeOut);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.FiFoTimeOut = ScanStatus.FiFoTimeOut;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.FiFoTimeOut);  //write Int32 to file

            __TTTRFileHeader.CustomSISHeader.StackMarker = ScanStatus.StackMarker;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.StackMarker);  //write Byte to file

            __TTTRFileHeader.CustomSISHeader.FrameMarker = ScanStatus.FrameMarker;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.FrameMarker);  //write Byte to file

            __TTTRFileHeader.CustomSISHeader.LineMarker = ScanStatus.LineMarker;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.LineMarker);  //write Byte to file

            __TTTRFileHeader.CustomSISHeader.PixelMarker = ScanStatus.PixelMarker;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.PixelMarker);  //write Byte to file

            __TTTRFileHeader.CustomSISHeader.GalvoMagnificationObjective = ScanStatus.GalvoMagnificationObjective;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.GalvoMagnificationObjective);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.GalvoScanLensFocalLength = ScanStatus.GalvoScanLensFocalLength;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.GalvoScanLensFocalLength);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.GalvoRangeAngleDegrees = ScanStatus.GalvoRangeAngleDegrees;
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.GalvoRangeAngleDegrees);  //write Double to file

            __TTTRFileHeader.CustomSISHeader.GalvoRangeAngleInt = ScanStatus.GalvoRangeAngleInt;                       
            __bwOutputFile.Write(__TTTRFileHeader.CustomSISHeader.GalvoRangeAngleInt);  //write Double to file
        }


        /// <summary>
        /// Update the number of records in the current Time Harp TTTR header. 
        /// <param name="__bwOutputFile">The binary writer stream that takes care to write the binary data to file.</param>
        /// <param name="__iFileOffsetForNumberRecords">The byte offset relative to the origin of the TTTR file.</param>
        /// <param name="__iRecordsCount">The current number of records that have been written to a TTTR file.</param> 
        /// </summary>
        public static void WriteTTTRNumberOfRecordsToFile(BinaryWriter __bwOutputFile, int __iFileOffsetForNumberRecords, int __iRecordsCount)
        {
            __bwOutputFile.Seek(__iFileOffsetForNumberRecords, SeekOrigin.Begin);  //move the file pointer with __iFileOffsetForNumberRecords with respect to the file origin
            __bwOutputFile.Write(__iRecordsCount);  //write Int32 to file
        }


        /// <summary>
        /// Read the Time Harp header info from a TTTR file.
        /// </summary> 
        /// <param name="__sInputFile">The name of the input TTTR file to be read.</param>
        /// <param name="__TTTRFileHeader">A TTTR file header structure where the info from TTTR file header will be stored.</param>
        public static void ReadTTTRHeaderFromFile(string __sInputFile, ref TimeHarpDefinitions.StructTTTRFileHeader __TTTRFileHeader)
        {
            // Declare file stream variables
            FileStream _fsInputFileStream;  //declare file stream
            BinaryReader _brInputFile;  //declare the binary reader

            try  //open the TTTR file for reading
            {
                _fsInputFileStream = File.Open(__sInputFile, FileMode.Open, FileAccess.Read, FileShare.Read); //open the input file for reading                        
                _brInputFile = new BinaryReader(_fsInputFileStream);  //allocate the binary reader - makes the actual reading of the binary data from hard drive
            }
            catch (Exception ex)
            {
                throw new Exception("TimeHarpLib: exception --> " + "the file: " + __sInputFile + " could not be opened by ReadTTTRHeaderFromFile() function!" + "Exception Message: " + ex.Message);
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
        /// Reads TTTR file and convert it to a readable ASCII file.
        /// </summary> 
        /// <param name="__sInputFile">The name of the input TTTR file.</param>  
        /// <param name="__sOutputFile">The name of the output ASCII file.</param>
        public static void ConvertTTTRFileToASCIIFile(string __sInputFile, string __sOutputFile)
        {
            // First read TTTR header
            ReadTTTRHeaderFromFile(__sInputFile, ref Files.TTTRFileHeader);  //read TTTR header and store the parameters into Files.TTTRFileHeader structure

            // Declare corresponding file streams
            FileStream _fsInputFileStream = null;  //declare file stream - read TTTR file
            BinaryReader _brInputFile = null;  //declare binary reader - read TTTR File 

            FileStream _fsOutputFileStream = null;  //declare file stream - write to ASCII file 
            StreamWriter _swOutputFile = null;  //declare stream writer - write to ASCII file            


            try  //open input TTTR file for reading
            {
                _fsInputFileStream = File.Open(__sInputFile, FileMode.Open, FileAccess.Read, FileShare.Read); //open the input file for reading
                _brInputFile = new BinaryReader(_fsInputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive
                
				// Skip the file header
				long _lBytesOffset = TimeHarpDefinitions.SizeTTTRDefaultFileHeader + TimeHarpDefinitions.SizeTTTRCustomSISFileHeader;  // calc and set TTTR header size in bytes                
				_brInputFile.BaseStream.Seek(_lBytesOffset, SeekOrigin.Begin);  //move the file pointer by the specified bytes with respect to the file origin            
            }
            catch (Exception ex)
            {
                throw new Exception("TimeHarpLib: exception --> " + "the file: " + __sInputFile + " could not be opened by ConvertTTTRFileToASCIIFile() function!" + " Exception Message: " + ex.Message);
            }


            try  //create and open the output ASCII file for writing
            {
                _fsOutputFileStream = File.Open(__sOutputFile, FileMode.Create, FileAccess.Write, FileShare.None); //open the output file for writing                                
                _swOutputFile = new StreamWriter(_fsOutputFileStream);  //allocate the stream writer - makes the actual writing of the text file on the hard drive
            }
            catch (Exception ex)
            {
				throw new Exception("TimeHarpLib: exception --> " + "the file: " + __sOutputFile + " could not be created and opened by ConvertTTTRFileToASCIIFile() function!" + " Exception Message: " + ex.Message);
            }


            // Write the TTTR TextHeader to the ASCII file
            _swOutputFile.WriteLine("Identifier       : {0}", (new String(Files.TTTRFileHeader.TextHeader.Ident)).Trim('\0'));
            _swOutputFile.WriteLine("Format Version   : {0}", (new String(Files.TTTRFileHeader.TextHeader.FormatVersion)).Trim('\0'));
            _swOutputFile.WriteLine("Creator Name     : {0}", (new String(Files.TTTRFileHeader.TextHeader.CreatorName)).Trim('\0'));
            _swOutputFile.WriteLine("Creator Version  : {0}", (new String(Files.TTTRFileHeader.TextHeader.CreatorVersion)).Trim('\0'));
            _swOutputFile.WriteLine("Time of Creation : {0}", (new String(Files.TTTRFileHeader.TextHeader.FileTime)).Trim('\0'));
            _swOutputFile.WriteLine("File Comment     : {0}", (new String(Files.TTTRFileHeader.TextHeader.CommentField)).Trim('\0'));

            // Write the TTTR BinaryHeader to the ASCII file
            _swOutputFile.WriteLine("No of Channels   : {0}", Files.TTTRFileHeader.BinaryHeader.NumberOfChannels);
            _swOutputFile.WriteLine("No of Curves     : {0}", Files.TTTRFileHeader.BinaryHeader.NumberOfCurves);
            _swOutputFile.WriteLine("Bits per Channel : {0}", Files.TTTRFileHeader.BinaryHeader.BitsPerChannel);
            _swOutputFile.WriteLine("Routing Channels : {0}", Files.TTTRFileHeader.BinaryHeader.RoutingChannels);
            _swOutputFile.WriteLine("No of Boards     : {0}", Files.TTTRFileHeader.BinaryHeader.NumberOfBoards);
            _swOutputFile.WriteLine("Active Curve     : {0}", Files.TTTRFileHeader.BinaryHeader.ActiveCurve);
            _swOutputFile.WriteLine("Measurement Mode : {0}", Files.TTTRFileHeader.BinaryHeader.MeasurementMode);
            _swOutputFile.WriteLine("Measurem.SubMode : {0}", Files.TTTRFileHeader.BinaryHeader.SubMode);
            _swOutputFile.WriteLine("Range No         : {0}", Files.TTTRFileHeader.BinaryHeader.RangeNo);
            _swOutputFile.WriteLine("Offset           : {0} [ns]", Files.TTTRFileHeader.BinaryHeader.Offset);
            _swOutputFile.WriteLine("AcquisitionTime  : {0} [ms]", Files.TTTRFileHeader.BinaryHeader.AcquisitionTime);
            _swOutputFile.WriteLine("Stop at          : {0}", Files.TTTRFileHeader.BinaryHeader.StopAt);
            _swOutputFile.WriteLine("Stop on Ovfl.    : {0}", Files.TTTRFileHeader.BinaryHeader.StopOnOvfl);
            _swOutputFile.WriteLine("Restart          : {0}", Files.TTTRFileHeader.BinaryHeader.Restart);
            _swOutputFile.WriteLine("DispLinLog       : {0}", Files.TTTRFileHeader.BinaryHeader.DispayLinLog);
            _swOutputFile.WriteLine("DispTimeAxisFrom : {0}", Files.TTTRFileHeader.BinaryHeader.DisplayTimeAxisFrom);
            _swOutputFile.WriteLine("DispTimeAxisTo   : {0}", Files.TTTRFileHeader.BinaryHeader.DisplayTimeAxisTo);
            _swOutputFile.WriteLine("DispCountAxisFrom: {0}", Files.TTTRFileHeader.BinaryHeader.DisplayCountsAxisFrom);
            _swOutputFile.WriteLine("DispCountAxisTo  : {0}", Files.TTTRFileHeader.BinaryHeader.DisplayCountsAxisTo);

            // Write the TTTR BoardHeader to the ASCII file
            _swOutputFile.WriteLine("HardwareIdent    : {0}", (new String(Files.TTTRFileHeader.BoardHeader.HardwareIdent)).Trim('\0'));
            _swOutputFile.WriteLine("HardwareVersion  : {0}", (new String(Files.TTTRFileHeader.BoardHeader.HardwareVersion)).Trim('\0'));
            _swOutputFile.WriteLine("Board Serial     : {0}", Files.TTTRFileHeader.BoardHeader.BoardSerial);
            _swOutputFile.WriteLine("CFDZeroCross     : {0} [mV]", Files.TTTRFileHeader.BoardHeader.CFDZeroCross);
            _swOutputFile.WriteLine("CFDDiscriminMin  : {0} [mV]", Files.TTTRFileHeader.BoardHeader.CFDDiscrMin);
            _swOutputFile.WriteLine("SYNCLevel        : {0} [mV]", Files.TTTRFileHeader.BoardHeader.SyncLevel);
            _swOutputFile.WriteLine("Curve Offset     : {0}", Files.TTTRFileHeader.BoardHeader.CurveOffset);
            _swOutputFile.WriteLine("Resolution       : {0} [ns]", Files.TTTRFileHeader.BoardHeader.Resolution);

            // Write the TTTR TTTRHeader to the ASCII file
            _swOutputFile.WriteLine("Glob Clock       : {0} [ns]", Files.TTTRFileHeader.TTTRHeader.TTTRGlobclock);
            _swOutputFile.WriteLine("Ext Devices      : {0}", Files.TTTRFileHeader.TTTRHeader.ExtDevices);
            _swOutputFile.WriteLine("Reserved1        : {0}", Files.TTTRFileHeader.TTTRHeader.Reserved1);
            _swOutputFile.WriteLine("Reserved2        : {0}", Files.TTTRFileHeader.TTTRHeader.Reserved2);
            _swOutputFile.WriteLine("Reserved3        : {0}", Files.TTTRFileHeader.TTTRHeader.Reserved3);
            _swOutputFile.WriteLine("Reserved4        : {0}", Files.TTTRFileHeader.TTTRHeader.Reserved4);
            _swOutputFile.WriteLine("Reserved5        : {0}", Files.TTTRFileHeader.TTTRHeader.Reserved5);
            _swOutputFile.WriteLine("Sync Rate        : {0} [Hz]", Files.TTTRFileHeader.TTTRHeader.SyncRate);
            _swOutputFile.WriteLine("Average CFD Rate : {0} [Cps]", Files.TTTRFileHeader.TTTRHeader.AverageCFDRate);
            _swOutputFile.WriteLine("Stop After       : {0}", Files.TTTRFileHeader.TTTRHeader.StopAfter);
            _swOutputFile.WriteLine("Stop Reason      : {0}", Files.TTTRFileHeader.TTTRHeader.StopReason);
            _swOutputFile.WriteLine("No of Records    : {0}", Files.TTTRFileHeader.TTTRHeader.NumberOfRecords);
            _swOutputFile.WriteLine("Special Hdr Size : {0}", Files.TTTRFileHeader.TTTRHeader.SpecialHeaderLength);

            // Write CustomSISHeader to the ASCI file - if the the length of the special header matches then it means it is our custom SIS header (otherwise we skip it)
            if (Files.TTTRFileHeader.TTTRHeader.SpecialHeaderLength * 4 == TimeHarpDefinitions.SizeTTTRCustomSISFileHeader)
            {
                _swOutputFile.WriteLine("");
				_swOutputFile.WriteLine("Custom SIS header (version = {0}):", (new String(Files.TTTRFileHeader.CustomSISHeader.Version)).Trim('\0'));				
                _swOutputFile.WriteLine("Time Per Pixel   : {0} [ms]", Files.TTTRFileHeader.CustomSISHeader.TimePPixel);
                _swOutputFile.WriteLine("Initial X offset : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.InitXNm);
                _swOutputFile.WriteLine("Initial Y offset : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.InitYNm);
                _swOutputFile.WriteLine("Initial Z offset : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.InitZNm);
                _swOutputFile.WriteLine("X Scan Size      : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.XScanSizeNm);
                _swOutputFile.WriteLine("Y Scan Size      : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.YScanSizeNm);
                _swOutputFile.WriteLine("Z Scan Size      : {0} [nm]", Files.TTTRFileHeader.CustomSISHeader.ZScanSizeNm);
                _swOutputFile.WriteLine("X Image Width    : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.ImageWidthPx);
                _swOutputFile.WriteLine("Y Image Height   : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.ImageHeightPx);
                _swOutputFile.WriteLine("Z Image Depth    : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.ImageDepthPx);
                _swOutputFile.WriteLine("X Over Scan Size : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.XOverScanPx);
                _swOutputFile.WriteLine("Y Over Scan Size : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.YOverScanPx);
                _swOutputFile.WriteLine("Z Over Scan Size : {0} [px]", Files.TTTRFileHeader.CustomSISHeader.ZOverScanPx);
                _swOutputFile.WriteLine("SIS Channels     : {0}", Files.TTTRFileHeader.CustomSISHeader.SISChannels);        
                _swOutputFile.WriteLine("Type Of Scan     : {0}", Files.TTTRFileHeader.CustomSISHeader.TypeOfScan);
                _swOutputFile.WriteLine("Frame Time Out   : {0} [ms]", Files.TTTRFileHeader.CustomSISHeader.FrameTimeOut);
                _swOutputFile.WriteLine("FiFo Time Out    : {0} [ms]", Files.TTTRFileHeader.CustomSISHeader.FiFoTimeOut);
                _swOutputFile.WriteLine("Stack Marker     : {0}", Files.TTTRFileHeader.CustomSISHeader.StackMarker);
                _swOutputFile.WriteLine("Frame Marker     : {0}", Files.TTTRFileHeader.CustomSISHeader.FrameMarker);
                _swOutputFile.WriteLine("Line Marker      : {0}", Files.TTTRFileHeader.CustomSISHeader.LineMarker);
                _swOutputFile.WriteLine("Pixel Marker     : {0}", Files.TTTRFileHeader.CustomSISHeader.PixelMarker);
                _swOutputFile.WriteLine("Galvo Magnification Objective: {0}", Files.TTTRFileHeader.CustomSISHeader.GalvoMagnificationObjective);
                _swOutputFile.WriteLine("Galvo Scan Lens Focal Length : {0} [mm]", Files.TTTRFileHeader.CustomSISHeader.GalvoScanLensFocalLength);
                _swOutputFile.WriteLine("Galvo Max Range Angle        : {0} [degrees]", Files.TTTRFileHeader.CustomSISHeader.GalvoRangeAngleDegrees);
                _swOutputFile.WriteLine("Galvo Max Range Angle        : {0} [integer]", Files.TTTRFileHeader.CustomSISHeader.GalvoRangeAngleInt);
            }

            // Write the TTTR records to the ASCII file
            _swOutputFile.WriteLine("");
            _swOutputFile.WriteLine(" RawRec#   RawHex       #   timetag    time[s]     channel[#]    route");
            _swOutputFile.WriteLine("");

            UInt32 _ui32TTTRSingleRecord = 0U;  //the TTTR record is actually a unsigned 32bit integer
            ProcessedTTTRecord _PTTTRecord = new ProcessedTTTRecord();  //the processed single TTTR record
            long _lTrueTimeTag = 0;  //keeps track of the true time tag
            int _iPhotonCounter = 0;  //counts the photon events
            int _iOverflow = 0;  //counts the overflow events

            for (int i = 0; i < Files.TTTRFileHeader.TTTRHeader.NumberOfRecords; i++) //TTTR records to ASCII file
            {
                _ui32TTTRSingleRecord = _brInputFile.ReadUInt32();  //read a single record from the input binary TTTR file
                ConvertSingleTTTRtoPTTTR(_ui32TTTRSingleRecord, ref _PTTTRecord);  //convert single data record to a processed TTTR record in order to facilitate the extraction of the data fields from the record

                switch (_PTTTRecord.Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
                {
                    case 0: //Valid = 0 (external trigger or overflow event occurred)
                        {
                            if (_PTTTRecord.DataMarker > 0)  //if DataMarker it means we have found an external marker
                            {
                                _lTrueTimeTag = _PTTTRecord.TimeTag + 65536L * _iOverflow;  //calculate the true Time Tag from the TimeTag and the overflow status
                                _swOutputFile.WriteLine("{0,9} {1,9} {2,9} {3,6} {4,12}   Marker={5}", i, _ui32TTTRSingleRecord.ToString("X8"), " ", _PTTTRecord.TimeTag, _lTrueTimeTag * 100e-9, _PTTTRecord.DataMarker);

                            }
                            
                            if (_PTTTRecord.DataOverflow == 1)  //if DataOverflow = 0 (no overflow of _PTTTRBuffer[i].TimeTag), DataOverflow = 1 (overflow of _PTTTRBuffer[i].TimeTag occurred)
                            {
                                _lTrueTimeTag = _PTTTRecord.TimeTag + 65536L * _iOverflow;  //calculate the true Time Tag from the TimeTag and the overflow status
                                _swOutputFile.WriteLine("{0,9} {1,9} {2,9} {3,6} {4,12} {5,10}", i, _ui32TTTRSingleRecord.ToString("X8"), " ", _PTTTRecord.TimeTag, _lTrueTimeTag * 100e-9, "Overflow");

                                _iOverflow++;  //at overflow event, increase the overflow flag
                            }
                            
                            //else
                            //{
                            //    _swOutputFile.WriteLine("{0,9} {1,9} {2,9} {3,6} {4,12} {5,10} {6,4}", i, _ui32TTTRSingleRecord.ToString("X8"), _iPhotonCounter, _PTTTRRecord.TimeTag, _lTrueTimeTag * 100e-9, _PTTTRRecord.Channel, "InvalidTTTRRecord");
							//    //throw new Exception("KUL.MDS.Hardware.PQTimeHarp. Exception - " + "Current TTTR record is invalid by ConvertTTTRFileToASCIIFile() function!");
                            //}

                            break;
                        }
                    case 1:  //Valid = 1 (photon arrival event occurred)
                        {
                            _lTrueTimeTag = _PTTTRecord.TimeTag + 65536L * _iOverflow;  //calculate the true Time Tag from the TimeTag and the overflow status
                            _swOutputFile.WriteLine("{0,9} {1,9} {2,9} {3,6} {4,12} {5,10} {6,4}", i, _ui32TTTRSingleRecord.ToString("X8"), _iPhotonCounter, _PTTTRecord.TimeTag, _lTrueTimeTag * 100e-9, _PTTTRecord.Channel, _PTTTRecord.Route);
                            _iPhotonCounter++;

                            break;
                        }
                }

            }


            // Close input TTTR file
            _brInputFile.Close();
            _fsInputFileStream.Close();

            // Close output ASCII file
            _swOutputFile.Close();
            _fsOutputFileStream.Close();
        }


        /// <summary>
        /// Reads TTTR file and extracts a single image (as frame pixel buffer) and returns the current
        /// TTTR record up to which a TTTR file was processed.
        /// </summary> 
        /// <param name="__bIsToApplyTimeGating">Indicates whether we want to do time gating or not (true = do gating; false = do not perform gating)</param>
        /// <param name="__dGatingTimeMinMillisec">The minimum gating time in milliseconds</param>
        /// <param name="__dGatingTimeMaxMillisec">The maximum gating time in milliseconds (only photons between min and max gating time will pass the gate, i.e. will have contribution to the image)</param>
        /// <param name="__sInputFile">The name of the input TTTR file</param>  
        /// <param name="__iRecordsOffset">The specific TTTR records number from where we start reading</param>
        /// <param name="__iXImageWidth">We return here the width in pixels of the extracted image</param>
        /// <param name="__iYImageHeight">We return here the height in pixels of the extracted image</param>
        /// <param name="__dXScanSizeNm">We return here the width in nm of the extracted image</param>
        /// <param name="__dYScanSizeNm">We return here the height in nm of the extracted image</param>
        /// <param name="__ui32SingleFramePixelBuffer">The extracted image will be stored and returned in this buffer</param>
        public static void ExtractImageFromTTTRFile(bool __bIsToApplyTimeGating, double __dGatingTimeMinMillisec, double __dGatingTimeMaxMillisec, 
			string __sInputFile, ref int __iRecordsOffset, ref double __dXScanSizeNm, ref double __dYScanSizeNm, ref int __iXImageWidth, ref int __iYImageHeight, 
			ref uint[] __ui32SingleFramePixelBuffer)
        {
            // First read TTTR header
            ReadTTTRHeaderFromFile(__sInputFile, ref Files.TTTRFileHeader);  //read TTTR header

            // Declare corresponding file streams
            FileStream _fsInputFileStream;  //declare file stream - read TTTR file
            BinaryReader _brInputFile;  //declare binary reader - read TTTR File

            try  //open input TTTR file for reading
            {
                _fsInputFileStream = File.Open(__sInputFile, FileMode.Open, FileAccess.Read, FileShare.Read); //open the input file for reading                
                _brInputFile = new BinaryReader(_fsInputFileStream);  //allocate the binary writer - makes the actual writing of the binary data to hard drive

                // Skip the file header and if requested start reading TTTR records from a specific records number
				long _lBytesOffset = TimeHarpDefinitions.SizeTTTRDefaultFileHeader + TimeHarpDefinitions.SizeTTTRCustomSISFileHeader + __iRecordsOffset * sizeof(UInt32);  // calc TTTR header and TTTR records offset                
                _brInputFile.BaseStream.Seek(_lBytesOffset, SeekOrigin.Begin);  //move the file pointer by the specified bytes with respect to the file origin - in this way we can read the given chunk of TTTR records                
            }
            catch (Exception ex)
            {
				throw new Exception("TimeHarpLib: exception --> " + "the file: " + __sInputFile + " could not be opened by ConvertTTTRFileToBitmapImages() function!" + " Exception Message: " + ex.Message);
            }

            ExternalMarkers.FrameMarker.Found = false;  //reset frame marker
            ExternalMarkers.LineMarker.Found = false;  //reset line marker

            ExternalMarkers.LineCount = 0;  //start counting of the number of line markers within a frame to have as an info status (frame marker is beginning of line, so count it)
            ExternalMarkers.FrameCount = 0;  //count the number of frame markers to have as an info status

            ScanStatus.XScanSizeNm = Files.TTTRFileHeader.CustomSISHeader.XScanSizeNm;  // the width of the image in [nm]
            ScanStatus.YScanSizeNm = Files.TTTRFileHeader.CustomSISHeader.YScanSizeNm;  // the height of the image in [nm]

            ScanStatus.XImageWidthPx = Files.TTTRFileHeader.CustomSISHeader.ImageWidthPx;
            ScanStatus.YImageHeightPx = Files.TTTRFileHeader.CustomSISHeader.ImageHeightPx;
            ScanStatus.PixelCount = ScanStatus.XImageWidthPx * ScanStatus.YImageHeightPx;
            ScanStatus.PixelsCounter = 0;  //currently processed pixels
            ScanStatus.Overflow = 0;  //number of current overflow markers

            ScanStatus.TypeOfScan = Files.TTTRFileHeader.CustomSISHeader.TypeOfScan;  //get the type of scanning (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)            
            ScanStatus.SyncTimeChannel = (int)((1.0 / Files.TTTRFileHeader.TTTRHeader.SyncRate) / (Files.TTTRFileHeader.BoardHeader.Resolution * 1e-9));  //convert SYNC time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm

            ScanStatus.IsToApplyTimeGating = __bIsToApplyTimeGating;  // do or not time gating of the binned photons            
            ScanStatus.GatingTimeMinChannel = (int)((__dGatingTimeMinMillisec * 1e-3) / (Files.TTTRFileHeader.BoardHeader.Resolution * 1e-9));  //convert min gating time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm            
            ScanStatus.GatingTimeMinReverseChannel = (ushort)(ScanStatus.SyncTimeChannel - ScanStatus.GatingTimeMinChannel);  //convert min gating channel to the reverse one so that we comply with the reverse start-stop photon time tag - it is then easier to compare and discard/gate photons 
            ScanStatus.GatingTimeMaxChannel = (int)((__dGatingTimeMaxMillisec * 1e-3) / (Files.TTTRFileHeader.BoardHeader.Resolution * 1e-9));  //convert max gating time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm            
            ScanStatus.GatingTimeMaxReverseChannel = (ushort)(ScanStatus.SyncTimeChannel - ScanStatus.GatingTimeMaxChannel);  //convert max gating channel to the reverse one so that we comply with the reverse start-stop photon time tag - it is then easier to compare and discard/gate photons 

            ScanStatus.LinePixelBuffer = new uint[ScanStatus.XImageWidthPx];  //the line buffer with the processed pixels      
            //ScanStatus.FramePixelBuffer = new int[ScanStatus.PixelCount];  //allocate memory for the frame pixel buffer
            uint[][] _ui32FramePixelBuffer = new uint[1][];  //an array of arrays of pixels from the data buffer, each array of pixels represent a frame, returned by this function
            _ui32FramePixelBuffer[0] = new uint[ScanStatus.PixelCount];  //allocate memory for the frame pixel buffer
            UInt32[] _ui32TTTRBuffer = new UInt32[TimeHarpDefinitions.DMABLOCKSZ];  //Time Harp FIFO data records will be stored here, the max size must be TimeHarpDefinitions.DMABLOCKSZ

            ScanStatus.LinePTTTRBufferIndex = 0;  //reset to zero the LinePTTTRBuffer[] index (because we are about to start)

            bool _bFrameReady = false;
            //int _iFrameCounter = 0;            
            int _iNumberOfRecords = 0;

            int _indexTTTRBufferLowerBound = 0;
            int _indexTTTRBufferUpperBound = 0;


            // Read TTTR records from the binary TTTR file, extract image from records and save them to bitmap image file(s). Note that we read the data in chunks of (Files.TTTRFileHeader.TTTRHeader.NumberOfRecords / TimeHarpDefinitions.DMABLOCKSZ) size 
            for (int i = 0; i < (Files.TTTRFileHeader.TTTRHeader.NumberOfRecords / TimeHarpDefinitions.DMABLOCKSZ); i++) //Read TTTR records in multiple of TimeHarpDefinitions.DMABLOCKSZ
            {
                // Get records (in TimeHarpDefinitions.DMABLOCKSZ size) into the TTTR buffer                
                for (int j = 0; j < TimeHarpDefinitions.DMABLOCKSZ; j++)  //fill the _ui32TTTRBuffer[] buffer array with records
                {
                    _ui32TTTRBuffer[j] = _brInputFile.ReadUInt32();  //read a single record from the input binary TTTR file
                }

                _indexTTTRBufferLowerBound = 0;  //must be zero in the present algorithm
                _indexTTTRBufferUpperBound = TimeHarpDefinitions.DMABLOCKSZ;  //get the current number of records read from TTTR file                                

                // Process TTTR buffer and extract pixels from photon events
                while (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)  //loop until we process the entire TTTR buffer
                {
                    // Process TTTR buffer and extract pixels from photon events                    
                    ExtractPhotonEventsFromTTTRBuffer(_ui32TTTRBuffer, ref _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, ref _bFrameReady, _ui32FramePixelBuffer[0]);

                    _iNumberOfRecords += _indexTTTRBufferLowerBound;  // update the number of TTTR records read so far

                    // If a frame is ready exit the while-loop - the purpose of this function is to process the frames one by one
                    if (_bFrameReady)
                    {
                        break;
                    }

                }  //END while (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)

                // Save the state of processing so that next time we start from the appropriate place                    
                //_indexTTTRBufferLowerBound = (_indexTTTRBufferLowerBound >= _indexTTTRBufferUpperBound) ? 0 : _indexTTTRBufferLowerBound;  //check if we have processed the entire global TTTR buffer (if so reset the variable to zero so that it is ready for the next round of processing)                                    

                // If a frame is ready exit the for-loop - the purpose of this function is to process the frames one by one
                if (_bFrameReady)
                {
                    break;
                }

            }  //END for-loop


            // Process the last chunk of TTTR records and try to extract pixels. Note that the size of the last chunk is (Files.TTTRFileHeader.TTTRHeader.NumberOfRecords % TimeHarpDefinitions.DMABLOCKSZ)                       
            if (!_bFrameReady && (Files.TTTRFileHeader.TTTRHeader.NumberOfRecords % TimeHarpDefinitions.DMABLOCKSZ > 1))
            {
                for (int j = 0; j < (Files.TTTRFileHeader.TTTRHeader.NumberOfRecords % TimeHarpDefinitions.DMABLOCKSZ); j++)  //fill the _ui32TTTRBuffer[] buffer array with records
                {
                    _ui32TTTRBuffer[j] = _brInputFile.ReadUInt32();  //read a single record from the input binary TTTR file
                }

                _indexTTTRBufferLowerBound = 0;  //must be zero in the present algorithm
                _indexTTTRBufferUpperBound = Files.TTTRFileHeader.TTTRHeader.NumberOfRecords % TimeHarpDefinitions.DMABLOCKSZ;  //get the number of records left to be read from TTTR file

                // Process TTTR buffer and extract pixels from photon events
                while (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)  //loop until we process the entire TTTR buffer
                {
                    // Process TTTR buffer and extract pixels from photon events                    
                    ExtractPhotonEventsFromTTTRBuffer(_ui32TTTRBuffer, ref _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, ref _bFrameReady, _ui32FramePixelBuffer[0]);

                    _iNumberOfRecords += _indexTTTRBufferLowerBound;  // update the number of TTTR records read so far

                    // If a frame is ready exit the while-loop - the purpose of this function is to process the frames one by one
                    if (_bFrameReady)
                    {
                        break;
                    }

                }  //END while (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)
            }


            // Close input TTTR file
            _brInputFile.Close();
            _fsInputFileStream.Close();


            // Update values for the pixel buffer variables (by checking their values the caller can judge if the extraction was successful) 
            if (_bFrameReady || ExternalMarkers.FrameCount > 0)  // the case of complete or incomplete frame - note that when the frame is incomplete we nevertheless return what's left in pixel buffer (due to possible FiFo buffer overrun, which may wipe reference line markers)
            {
                __iRecordsOffset += _iNumberOfRecords; // update the number of records
                __dXScanSizeNm = ScanStatus.XScanSizeNm;  // update the width of the image in [nm]
                __dYScanSizeNm = ScanStatus.YScanSizeNm;  // update the height of the image in [nm]
                __iXImageWidth = ScanStatus.XImageWidthPx;  // update the width of the image in [px]
                __iYImageHeight = ScanStatus.YImageHeightPx;  // update the height of the image in [px]
                __ui32SingleFramePixelBuffer = _ui32FramePixelBuffer[0];  // get the extracted image                 
            }
            else  //case no frame or frame marker found - we return zeros and nulls
            {
                __dXScanSizeNm = 0.0;
                __dYScanSizeNm = 0.0; 
                __iXImageWidth = 0;
                __iYImageHeight = 0;
                __ui32SingleFramePixelBuffer = null;
            }

        }

		       

        // END OF Special functions for TTTR file handling
        /////////////////////////////////////////////////////////////////////////////

        #endregion Special functions for TTTR file handling



        #region Special functions for TTTR buffer handling and TTTR to pixels extraction

        /////////////////////////////////////////////////////////////////////////////
        // Functions for TTTR buffer handling and TTTR to pixels extraction

        /// <summary>
        /// Prepares the APD hardware for a specific image acquisition.
        /// </summary>
        /// <param name="__iFrameMarker">External frame marker that marks a beginning of frame in the Time Harp data stream</param>
        /// <param name="__iLineMarker">External line marker that marks a beginning of line in the Time Harp data stream</param>
        /// <param name="__dTimePPixelMillisec">The time per pixel in [ms] - we use it just for info (the real pix time is calculated from the line markers)</param>        
        /// <param name="__iXImageWidthPx">The number of pixels per line</param>
        /// <param name="__iYImageHeightPx">The number of lines per frame</param>  
        /// <param name="__iZImageDepthPx">The number of images per stack</param> 
        /// <param name="__dXScanSizeNm">The physical size of the scan along X in[nm]</param>
        /// <param name="__dYScanSizeNm">The physical size of the scan along Y in[nm]</param>
        /// <param name="__dZScanSizeNm">The physical size of the scan along Z in[nm]</param>
        /// <param name="__dInitXNm">The initial scan offset along X in [nm]</param>
        /// <param name="__dInitYNm">The initial scan offset along Y in [nm]</param>
        /// <param name="__dInitZNm">The initial scan offset along Z in [nm]</param>
        /// <param name="__iXOverScanPx">Overscan size in [px] along X</param>
        /// <param name="__iYOverScanPx">Overscan size in [px] along Y</param>
        /// <param name="__iZOverScanPx">Overscan size in [px] along Z</param>
        /// <param name="__iSISChannels">The number of channels that SIS shows/processes at the same time</param>
        /// <param name="__dMagnificationObjective">The magnification of the objective</param>
        /// <param name="__dScanLensFocalLength">The focal length of the scan lens</param>
        /// <param name="__dRangeAngleDegrees">The +/- of the maximum useful angle in [degrees] the galvo can scan</param>
        /// <param name="__dRangeAngleInt">The +/- of the maximum useful angle in [terms of integer] the galvo can scan - this is the value that the galvo's controller takes for an angle</param>
        /// <param name="__bIsToApplyTimeGating">Indicates whether we want to do time gating or not (true = do gating; false = do not perform gating)</param>
        /// <param name="__dGatingTimeMinMillisec">The  min gating time in milliseconds</param>
        /// <param name="__dGatingTimeMaxMillisec">The max gating time in milliseconds</param>        
        /// <param name="__iTypeOfScan">The type of scan mode (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)</param> 
        /// <param name="__iFrameTimeOut">The frame time out - the time after which a frame a part of the already processed frame will be returned</param>
        /// <param name="__iFiFoTimeOut">The Time Harp's FiFo time out - the time after which the FiFo will be read no matter if it is half full or not</param>
        /// <param name="__iMeasurementMode">The Time Harp measurement/acquisition mode. There are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode</param>
        /// <param name="__iAcquisitionTime">The acquisition time in milliseconds (min value = TimeHarpDefinitions.ACQTMIN; max value = TimeHarpDefinitions.ACQTMAX)</param>                            
        /// <param name="__bSaveTTTRData">Indicates if we want to save the TTTR data to file or not (true = save it, false = do not save)</param>
        /// <param name="__sTTTRFile">The name of the binary TTTR file where the acquired records will be stored</param>
        public void SetupAPDCountAndTiming(
            int __iFrameMarker,
            int __iLineMarker,

            double __dTimePPixelMillisec,

            int __iXImageWidthPx,
            int __iYImageHeightPx,
            int __iZImageDepthPx,

            double __dXScanSizeNm,
            double __dYScanSizeNm,
            double __dZScanSizeNm,

            double __dInitXNm,
            double __dInitYNm,
            double __dInitZNm,

            int __iXOverScanPx,
            int __iYOverScanPx,
            int __iZOverScanPx,

            int __iSISChannels,

            double __dMagnificationObjective,
            double __dScanLensFocalLength,
            double __dRangeAngleDegrees,
            double __dRangeAngleInt, 
            
            bool __bIsToApplyTimeGating,
            double __dGatingTimeMinMillisec,
            double __dGatingTimeMaxMillisec,

            int __iTypeOfScan,
            int __iFrameTimeOut,
            int __iFiFoTimeOut,

            int __iMeasurementMode, 
            int __iAcquisitionTime, 
            bool __bSaveTTTRData, 
            string __sTTTRFile)
        {
            // External markers settings
            //ScanStatus.StackMarker = (byte) __iStackMarker;  //value of external marker interpreted as a Stack marker
            ScanStatus.FrameMarker = (byte)__iFrameMarker;  //value of external marker interpreted as a Frame marker (note that Frame marker is also a line marker)
            ScanStatus.LineMarker = (byte)__iLineMarker;  //value of external marker interpreted as a Line marker
            //ScanStatus.PixelMarker = (byte) __iPixelMarker;  //value of external marker interpreted as a Pixel marker

            // Image acquisition default settings
            ScanStatus.TimePPixelMillisec = __dTimePPixelMillisec;  //the time per pixel in [ms] as set in SIS scan settings (not used in the pixel extraction)
            ScanStatus.TimePPixelChannel = 0;  //pixel time in channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm
                       
            ScanStatus.XImageWidthPx = __iXImageWidthPx;  //number of pixels per line
            ScanStatus.YImageHeightPx = __iYImageHeightPx;  //number of lines per frame
            ScanStatus.ZImageDepthPx = __iZImageDepthPx;  //number of images in the stack
            ScanStatus.PixelCount = ScanStatus.XImageWidthPx * ScanStatus.YImageHeightPx;  //number of pixels in one frame
            ScanStatus.PixelsCounter = 0;  //processed pixels counter - counts the pixels that have been already processed

            ScanStatus.XScanSizeNm = __dXScanSizeNm;  //the scan size along X in [nm]
            ScanStatus.YScanSizeNm = __dYScanSizeNm;  //the scan size along Y in [nm]
            ScanStatus.ZScanSizeNm = __dZScanSizeNm;  //the scan size along Z in [nm]

            ScanStatus.InitXNm = __dInitXNm;  //initial offset along X in [nm]
            ScanStatus.InitYNm = __dInitYNm;  //initial offset along Y in [nm]
            ScanStatus.InitZNm = __dInitZNm;  //initial offset along Z in [nm]

            ScanStatus.XOverScanPx = __iXOverScanPx;  //over scan area along X in [px]
            ScanStatus.YOverScanPx = __iYOverScanPx;  //over scan area along Y in [px]
            ScanStatus.ZOverScanPx = __iZOverScanPx;  //over scan area along Z in [px]

            // SIS channel settings
            ScanStatus.SISChannels = __iSISChannels;  // number of SIS channels

            // YanusIV settings
            ScanStatus.GalvoMagnificationObjective = __dMagnificationObjective;  //the magnification of the objective
            ScanStatus.GalvoScanLensFocalLength = __dScanLensFocalLength;  //the focal length of the scan lens in [mm]
            ScanStatus.GalvoRangeAngleDegrees = __dRangeAngleDegrees;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            ScanStatus.GalvoRangeAngleInt = __dRangeAngleInt;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           

            // Sync (laser timing) pulse settings
			int _iSyncRate = this.GetSyncRate();  //get SYNC rate in Hz
			float _fResolution = this.GetResolution();  //get resolution in [ns]
			ScanStatus.SyncTimeMillisec = 1000.0 / ((double)_iSyncRate);  //get SYNC time in [ms] (sync time = interval between the laser pulses)
            ScanStatus.SyncTimeChannel = (int)((ScanStatus.SyncTimeMillisec * 1e-3) / (_fResolution * 1e-9));  //convert SYNC time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm

            // Gating time settings - note that only photons between the min and max gating time will be allowed to pass the gate (i.e. will be counted)
            ScanStatus.IsToApplyTimeGating = __bIsToApplyTimeGating;  // do or not time gating of the binned photons
            
            ScanStatus.GatingTimeMinMillisec = __dGatingTimeMinMillisec;  //set the maximum (the upper bound of the) gating time in [ms]
            ScanStatus.GatingTimeMinChannel = (int)((__dGatingTimeMinMillisec * 1e-3) / (_fResolution * 1e-9));  //convert gating time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm
            ScanStatus.GatingTimeMinReverseChannel = (ushort)(ScanStatus.SyncTimeChannel - ScanStatus.GatingTimeMinChannel);  //convert gating channel to the reverse one so that we comply with the reverse start-stop photon time tag - it is then easier to compare and discard/gate photons 

            ScanStatus.GatingTimeMaxMillisec = __dGatingTimeMaxMillisec;  //the maximum (the upper bound of the) gating time in [ms]
            ScanStatus.GatingTimeMaxChannel = (int)((__dGatingTimeMaxMillisec * 1e-3) / (_fResolution * 1e-9));  //convert gating time to channel number, in the Time Harp buffer the time tags are stored in terms of integer numbers, so translate to this notation for better algorithm
            ScanStatus.GatingTimeMaxReverseChannel = (ushort)(ScanStatus.SyncTimeChannel - ScanStatus.GatingTimeMaxChannel);  //convert gating channel to the reverse one so that we comply with the reverse start-stop photon time tag - it is then easier to compare and discard/gate photons

            // Other default settings           
            // Set scan status parameters and allocate the pixel buffer
            ScanStatus.TypeOfScan = __iTypeOfScan;  //set used scan mode, (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)
            ScanStatus.Overflow = 0;  //reset overflow flag - keeps track of the number of times an overflow flag occurred in the Time Harp data stream (the Time Harp buffer)
            ScanStatus.FrameTimeOut = __iFrameTimeOut;  //set frame time out in [ms]
            ScanStatus.FiFoTimeOut = __iFiFoTimeOut;  //set Time Harp's FiFo time out in [ms] - the time period after which the FiFo will be fetched and read although it may not be Half Full (we do not want to wait too long when the count rate is low and filling FiFo slow, respectively). Note that it is good if FiFo time out is smaller or equal to the frame time out (thus we get and show the pixels synchronized with the frame). 
                        
            // Buffers settings           
            ScanStatus.FramePixelBuffer = new uint[1][];  //the frame buffer with the processed pixels - currently we allocate space for a single frame. If more frames are acquired (continuous scanning), we swap to the beginning of the buffer after each frame get completed (i.e. we use the same buffer to store the pixels for the next frame).
			ScanStatus.FramePixelBuffer[0] = new uint[ScanStatus.PixelCount];  //the frame pixel buffer with the processed pixels			
            ScanStatus.LinePixelBuffer = new uint[ScanStatus.XImageWidthPx];  //the line pixel buffer with the processed pixels

            // File settings
            Files.TTTRFileName = __sTTTRFile;  //the file path and name of the binary TTTR file where the raw Time harp 200 records will be stored
            Files.FileCounter = 0; //counts how many output TTTR files we have written to hard disk so far
            Files.isTTTRFileFirstWrite = true;   //keep track of the TTTR file state - also it indicates if we have started to record the TTTR data or not
            Files.RecordsCount = 0;  //counts how many records we have written to a file so far

            // Threads settings
            ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = 0;  //reset the index - note that we start storage from the beginning of the buffer
            ThreadsStatus.APDSaveThread.IsToRun = __bSaveTTTRData;  //set to true if you want to save TTTR data to file, otherwise false
            ThreadsStatus.APDReadThread.IsFiFoOverun = false;  //keeps track of if Time Harp overrun  
            ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound = 0;  //reset the index - note that we start storage from the beginning of the buffer
            ThreadsStatus.BuildImageThread.IsFrameReady = false;  //reset the frame ready status to false (no frame yet)
            
            // Buffers settings
            ScanStatus.SizeGlobalTTTRBuffer = 0;  //reset the initial size of the buffer to zero - note that we start storage from the beginning of the buffer 
            //ScanStatus.SizeGlobalPTTTRBuffer = 0;  //reset the initial size of the buffer to zero - note that we start storage from the beginning of the buffer 
            ScanStatus.LinePTTTRBufferIndex = 0;  //the index of the last available processed TTTR record in the given buffer (it must not exceed the length of the buffer, if it happens it wraps to the beginning of the buffer)
            ScanStatus.LinePTTTRBufferTimeTag1 = -1;  //time tag of the line marker 1, it marks the beginning of the buffer
            ScanStatus.LinePTTTRBufferTimeTag2 = -1;  //time tag of the line marker 2, it marks the end of the buffer 
                                
            // Reset frame marker
            ExternalMarkers.FrameMarker.Found = false;
            ExternalMarkers.FrameMarker.Index = -1;
            ExternalMarkers.FrameMarker.TimeTag = -1;
            ExternalMarkers.FrameCount = 0;  //counts the number of frame markers to have as an info status
            
            // Reset line marker
            ExternalMarkers.LineMarker.Found = false;
            ExternalMarkers.LineMarker.Index = -1;
            ExternalMarkers.LineMarker.TimeTag = -1;
            ExternalMarkers.LineCount = 0;  //counts the number of line markers within a frame to have as an info status (note that a frame marker is beginning of line, so we count it as line marker as well)
                                    
            // Reset the total amount of pixels read
			ScanStatus.TotalPixelsRead = 0;
            
            // Prepare Time Harp for measurement - set measurement mode and the TTL active edge			
            this.SetMeasurementMode(__iMeasurementMode, __iAcquisitionTime);  //set measurement mode to TTTR/one-time_histogramming mode, plus set the acquisition time                        
        }

    
        /// <summary>
        /// Start measurement/acquisition (non-blocking the calling thread). Starts the APD reading - note that it spawns two threads,
        /// one for fetching Time Harp's FiFo buffer and one for storing the fetched buffer data to a TTTR format binary file.
        /// </summary>  
        public void StartAPDAcquisition()
        {
            // Instruct Time Harp to start acquisition
            this.StartMeausurement();  //start Time Harp measurement
            
            this.m_IsMesurementToBeStopped = false;  //because we are about to start a measurement we do not want to stop it (so set it to False)
            //this.m_IsMeasurementRunning = true;  //because we are about to start a measurement we do not want to stop it (so set it to True)

            // Create and spawn two threads which will be responsible for reading and storing the Time Harp FiFo buffer
            ThreadsStatus.APDReadThread.Thread = new Thread(this.Read);  //execute Read() function in a separate thread. Note that the thread status is kept in ThreadsStatus structure (see APDReadThread variable in there)
            ThreadsStatus.APDReadThread.Thread.Name = "APD_Read()";  //set the name of the thread
            ThreadsStatus.APDReadThread.Thread.IsBackground = true;  //set the thread as a background thread
            ThreadsStatus.APDReadThread.Thread.Priority = ThreadPriority.Highest;  //set the thread priority to above normal in order to assure we collect all buffer events from FiFo
            
            ThreadsStatus.APDSaveThread.Thread = new Thread(this.Save);  //execute Save() function in a separate thread. Note that the thread status is kept in ThreadsStatus structure (see APDSaveThread variable in there)
            ThreadsStatus.APDSaveThread.Thread.Name = "APD_Save()";  //set the name of the thread
            ThreadsStatus.APDSaveThread.Thread.IsBackground = true;  //set the thread as a background thread
            ThreadsStatus.APDSaveThread.Thread.Priority = ThreadPriority.Normal;  //set the thread priority to above normal

			ThreadsStatus.BuildImageThread.Thread = new Thread(this.BuildImage);  //execute BuildImage() function in a separate thread. Note that the thread status is kept in ThreadsStatus structure (see BuildImageThread variable in there)
			ThreadsStatus.BuildImageThread.Thread.Name = "APD_BuildImage()";  //set the name of the thread
			ThreadsStatus.BuildImageThread.Thread.IsBackground = true;  //set the thread as a background thread
			ThreadsStatus.BuildImageThread.Thread.Priority = ThreadPriority.Normal;  //set the thread priority to normal
			
            // Start the created threads
            ThreadsStatus.APDReadThread.Thread.Start();  //start the thread               
            while (!ThreadsStatus.APDReadThread.Thread.IsAlive)  //wait until the thread gets alive
            {
                Thread.Sleep(10);  // sleep for a while  
            }

			if (ThreadsStatus.APDSaveThread.IsToRun) ThreadsStatus.APDSaveThread.Thread.Start();  //start the thread if we have chosen to save the binary data         
			if (ThreadsStatus.BuildImageThread.IsToRun) ThreadsStatus.BuildImageThread.Thread.Start();  //start the thread if we have chosen to get image(s) from the binary data stream
        }


        /// <summary>
        /// Stop measurement/acquisition.
        /// </summary>  
        public void StopAPDAcquisition()
        {
            this.RequestMeasurementStop();  //tell all threads engaged in the APD acquisition to stop
        }


        /// <summary>
        /// Start measurement/acquisition - it makes the same as StartAPDAcquisition() function but blocks the calling thread until it returns.
        /// Starts the APD reading - note that it spawns two threads, one for fetching Time Harp's FiFo buffer and one for storing the fetched 
        /// buffer data to a TTTR format binary file. Use this function if you only want to acquire and store TTTR raw data in sequential manner.
        /// </summary>  
        public void RunAPDAcquisition()
        {
            this.StartAPDAcquisition();  //start APD acquisition and storing raw TTTR data

            // Join the created for the acquisition and storing threads
            if (ThreadsStatus.APDReadThread.Thread.IsAlive) ThreadsStatus.APDReadThread.Thread.Join();  //wait for the thread to finish
            if (ThreadsStatus.APDSaveThread.Thread.IsAlive) ThreadsStatus.APDSaveThread.Thread.Join();  //wait for the thread to finish          
        }
        
        
        /// <summary>
        /// Reads Time Harp raw data buffer. 
		/// Note: it is run in a separate thread.
		/// Note: do not spawn more than one thread of this method, otherwise the program will crash.		
        /// </summary>
        private void Read()
        {
            Thread.BeginThreadAffinity();  //set thread affinity so that it pins to a given CPU core

            //Reset hardware FiFo before we start acquisition            
            ThreadsStatus.APDReadThread.IsFiFoOverun = false;  //it signals to the other threads for FiFo overrun
            ThreadsStatus.APDReadThread.FiFoOverrunCount = 0;  //it counts how many times FiFo overrun happened
            
            // Allocate local variables used within the given thread
            UInt32[] _ui32TTTRBuffer = null;  //Time Harp FIFO data records will be stored here, the max size must be TimeHarpDefinitions.DMABLOCKSZ. Note that this variable will point to _ui32TTTRBuffer1[] or _ui32TTTRBuffer2[] (helps to flip between the two buffers)
            UInt32[] _ui32TTTRBuffer1 = new UInt32[TimeHarpDefinitions.DMABLOCKSZ];  //Time Harp FIFO data records will be stored here, the max size must be TimeHarpDefinitions.DMABLOCKSZ
            UInt32[] _ui32TTTRBuffer2 = new UInt32[TimeHarpDefinitions.DMABLOCKSZ];  //Time Harp FIFO data records will be stored here, the max size must be TimeHarpDefinitions.DMABLOCKSZ
            int _iNumberTTTRBufferToProcess = 0; //indicates the number of the TTTR buffer to be processed (i.e. the one ready to be processed). Note the: 0 - means no buffer ready; 1 - means _ui32TTTRBuffer1[] is ready to be processed; 2 - means _ui32TTTRBuffer2[] is ready to be processed

            int _iStatusFlags = 0;
            bool _bFiFoFull = false;
            bool _bFiFoHalfFull = false;
            bool _bFiFoEmpty = true;
            bool _bMeasurementRunning = true;  // true because we want to try to get data at least once
            bool _bMesurementToBeStop = false;  // false because we do not want to prevent the thread from trying to get data at least once

            bool _bFiFoTimeOut = false;
            int _iFiFoTimeOut = ScanStatus.FiFoTimeOut;  //time out period in [ms] after which the FIFO buffer will be read
            const int _iTHREAD_SLEEP_TIME = 1;  //1ms, default thread sleep time
            int _iMeasurementTimeCounter = 0;  //in [ms] - used to measure the elapsed time and eventually
            int _iAcquisitionTime = this.AcquisitionTime;  //get the original acquisition time
            int _iTimeCounter = 0;  //in [ms] - used to measure the FiFo timeout  
            int _iTimeCounterStart = 0;  //in [ms] - used to measure the FiFo timeout  
            int _iTimeCounterStop = 0;  //in [ms] - used to measure the FiFo timeout   

            int _iNumberOfRecords = 0;  //this variable will hold the number of records _iNumberOfRecords1 or _iNumberOfRecords1
            int _iNumberOfRecords1 = 0;  //the number of records from _ui32TTTRBuffer1[] buffer
            int _iNumberOfRecords2 = 0;  //the number of records from _ui32TTTRBuffer2[] buffer
            //int _iRecord = 0;

            //Console.WriteLine("Read() Started...: SyncRate = {0} [Hz]\n\r", this.m_iSyncRate);  //for debugging purposes
            //Stopwatch sw = new Stopwatch();  //for performance testing and debugging purposes
            //long _lElapsedMilliseconds = 0;  //for performance testing and debugging purposes 
            //long _lElapsedTicks = 0;  //for performance testing and debugging purposes 
            //int _iPoint = 0;  //the processing point, e.g. -1 = if(_bFiFoFull), 0 = no if(), 1 = if(!_bMeasurementRunning), 2 = if(_bFiFoHalfFull), 3 = if(_bFiFoTimeOut)
            //int _iIterationNumber = 0;  //for performance testing and debugging purposes 


            // Loop to read the Time Harp buffer and convert the photon events to pixels data
            while (_bMeasurementRunning && !_bMesurementToBeStop)  //acquire data until the end of the measurement, until the FIFO buffer gets overrun or the frame gets ready
            {
                //sw.Start();  //for performance tests and debugging
                //_iIterationNumber++;  //for performance testing and debugging purposes 

                _iTimeCounter += _iTimeCounterStop - _iTimeCounterStart;  //calculate the FiFo timeout
                _iMeasurementTimeCounter += _iTimeCounterStop - _iTimeCounterStart;  //measure elapsed time since the beginning of the current measurement
                _iTimeCounterStart = System.Environment.TickCount;  //get the TickCount - used to check for FiFo timeout                
                _bFiFoTimeOut = (_iTimeCounter > _iFiFoTimeOut) ? true : false;  //set FiFo time out to true in case the FiFo buffer filling takes too long time

                System.Threading.Thread.Sleep(_iTHREAD_SLEEP_TIME);  //put the thread to sleep for about _iTHREAD_SLEEP_TIME [ms], thus let some time to the buffer to fill with data records                
                
                // Get the state of various status flags. It shows the status of Time Harp hardware
                _iStatusFlags = this.GetFlags();  //get status flags
				_bFiFoFull = this.IsFIFOFull(_iStatusFlags);  //check the FIFO state, if FIFO is full
				_bFiFoHalfFull = this.IsFIFOHalfFull(_iStatusFlags);  //check the FIFO state, if FIFO is half full
				_bFiFoEmpty = this.IsFIFOEmpty(_iStatusFlags);  //check the FIFO state, if FIFO is empty
				_bMeasurementRunning = this.IsRunning();  //check if measurement is running, _bMeasurementRunning = true (means measurement is still running)
				this.m_IsMeasurementRunning = _bMeasurementRunning;  //keeps track if the measurement is still running so other methods can get the respective measurement state
                _bMesurementToBeStop = this.m_IsMesurementToBeStopped;  // check if we must stop measurement on the next loop

                // Check and get buffer data
                if (_bFiFoFull)  // Handle the FIFO overrun case - basically reset FiFo and tries to continue
                {
                    ThreadsStatus.APDReadThread.IsFiFoOverun = true;  //signal the other threads FiFo overrun
                    ThreadsStatus.APDReadThread.FiFoOverrunCount++;  //count how many times FiFo overrun happened
                    //ThreadsStatus.BuildImageThread.IsFiFoOverun = true;  //signal BuildImageThread (i.e. BuildImage() function) that FiFo overrun

                    // Try to continue measurement
					this.StopMeasurement();  //reset FiFo buffer
                    int _iAcquisitionTimeNew = _iAcquisitionTime - _iMeasurementTimeCounter;  //calculate the new acquisition time so that we continue to measure up to the original acquisition time
                    if (_iAcquisitionTimeNew > 100)  //only continue if the time left is bigger than 100ms
                    {
						this.SetMeasurementMode(0, _iAcquisitionTimeNew);  //set measurement mode to TTTR/one-time_histogramming mode, plus set the acquisition time
						this.StartMeausurement();  //continue measurement                        
                    }
                    else  //it means measurement done, so try to exit
                    {
                        this.m_IsMesurementToBeStopped = true;  //causes to stop processing this thread, Save() function and if running BuildImage() function
                        _bMesurementToBeStop = this.m_IsMesurementToBeStopped;  // stop this thread on the next loop
                    }

                    ThreadsStatus.APDReadThread.IsFiFoOverun = false;  //signal the other threads FiFo cleaned
                    
                    // if we have got the FiFo buffer _bFiFoTimeOut = false
                    _bFiFoTimeOut = false;  //reset FiFo time out state
                    _iTimeCounter = 0;  //reset counting for the FiFo time out 
                    
                    //_iPoint = -1;  //for debugging purposes

					//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - Time Harp FIFO overrun by function Read()");  //print exception if the FiFo gets overrun                    
                }
                else if (!_bMeasurementRunning)  // Handle the end of measurement case - basically here we finalize the acquiring of the data
                {
                    for (int i = 0; i < (TimeHarpDefinitions.FIFOSIZE / TimeHarpDefinitions.DMABLOCKSZ); i++)  //max one FIFO full
                    {
                        // Fetch data records from FIFO                    
                        this.StartDMATransfer(ref _ui32TTTRBuffer, _ui32TTTRBuffer1, _ui32TTTRBuffer2, _iNumberTTTRBufferToProcess, ref _iNumberOfRecords, _iNumberOfRecords1, _iNumberOfRecords2);
                    
                        // Process any TTTR buffer which is ready to be processed
                        if (_iNumberTTTRBufferToProcess > 0)  //check if we really have a buffer to process (must be 1 or 2 for a ready to process buffer)
                        {
                            // Before copy the fetched FiFo to the global buffer, check if we potentially are going to overwrite an area of the global buffer still in processing in some of the other threads (see Save() and BuildImage() functions)
                            this.CheckGlobalTTTROverwrite(_iNumberOfRecords);

                            // Copy the fetched FIFO array to the global FIFO buffer array                                                                                                               
                            this.CopyFiFoToArray(_ui32TTTRBuffer, _iNumberOfRecords);
                        }

                        // Complete DMA transfer and assign few other variables
						_iNumberOfRecords = this.T3RCompleteDMA();  //end fetching FIFO
                        if (_iNumberOfRecords < 0)  //if _iNumberOfRecord is less than zero, then an error occurred
                        {
                            //Shutdown();  //close Time Harp device
							//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - Time Harp DMA buffer error by function Read()");

                            // Throw an ErrorOccurred event to inform the user.
                            if (this.ErrorOccurred != null)
                            {
                                _logger.Debug("Time Harp: reporting error --> DMA buffer error by function Read()!");
                                this.ErrorOccurred(this, new EventArgs());
                            }
                        }

                        if (_iNumberOfRecords != TimeHarpDefinitions.DMABLOCKSZ)  //check if the DMA transfer was successful (i.e. without errors)
                        {
                            _iStatusFlags = this.GetFlags();  //get Time Harp status flags
							if (!this.IsFIFOEmpty(_iStatusFlags))  //in this case it is a DMA error (because the buffer is not empty)
                            {
                                //Shutdown();  //close Time Harp device
								//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - Time Harp DMA buffer error by function Read()");

                                // Throw an ErrorOccurred event to inform the user.
                                if (this.ErrorOccurred != null)
                                {
                                    _logger.Debug("Time Harp: reporting error --> DMA buffer error by function Read()!");
                                    this.ErrorOccurred(this, new EventArgs());
                                }
                            }
                        }

                        // Flip indexes so that they point to the next TTTR buffer that is in a state to be processed
                        this.AssignIndexesForNextTTTRBuffer(ref _iNumberTTTRBufferToProcess, _iNumberOfRecords, ref _iNumberOfRecords1, ref _iNumberOfRecords2);

                    }  //END for-loop (int i = 0; i < (TimeHarpDefinitions.FIFOSIZE / TimeHarpDefinitions.DMABLOCKSZ); i++)


                    // Process the last chunk of buffer data (if any)                                      
                    switch (_iNumberTTTRBufferToProcess)
                    {
                        case 0: //_iNumberTTTRBufferToProcess = 0, i.e. the first TTTR acquisition
                            {
                                //T3RStartDMA(_ui32TTTRBuffer1, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                                break;  //exit switch statement
                            }
                        case 1: //_iNumberTTTRBufferToProcess = 1, i.e. acquire TTTR buffer into _ui32TTTRBuffer2[] and meanwhile process _ui32TTTRBuffer1[]
                            {
                                //T3RStartDMA(_ui32TTTRBuffer2, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                                _ui32TTTRBuffer = _ui32TTTRBuffer1;  //assigns the TTTR _ui32TTTRBuffer1[] buffer to be processed while waiting for DMA transfer of _ui32TTTRBuffer2[]
                                _iNumberOfRecords = _iNumberOfRecords1;  //number of records in _ui32TTTRBuffer1[] buffer                                
                                break;  //exit switch statement
                            }
                        case 2: //_iNumberTTTRBufferToProcess = 2, i.e. acquire TTTR buffer into _ui32TTTRBuffer1[] and meanwhile process _ui32TTTRBuffer2[]
                            {
                                //T3RStartDMA(_ui32TTTRBuffer1, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                                _ui32TTTRBuffer = _ui32TTTRBuffer2;  //assigns the TTTR _ui32TTTRBuffer2[] buffer to be processed while waiting for DMA transfer of _ui32TTTRBuffer1[]
                                _iNumberOfRecords = _iNumberOfRecords2;  //number of records in _ui32TTTRBuffer2[] buffer  
                                break;  //exit switch statement
                            }
                    }

                    // Before copy the fetched FiFo to the global buffer, check if we potentially are going to overwrite an area of the global buffer still in processing in some of the other threads (see Save() and BuildImage() functions)
                    this.CheckGlobalTTTROverwrite(_iNumberOfRecords);

                    // Copy the fetched FIFO array to the global FIFO buffer array                                      
                    this.CopyFiFoToArray(_ui32TTTRBuffer, _iNumberOfRecords);

                    // if we have got the FiFo buffer _bFiFoTimeOut = false
                    _bFiFoTimeOut = false;  //reset FiFo time out state
                    _iTimeCounter = 0;  //reset counting for the FiFo time out

                    //_iPoint = 1;  //for performance testing and debugging purposes 
                }
                else if (_bFiFoHalfFull)  // Handle the FIFO half full case - basically here we acquire the data
                {
                    // Fetch data records from FIFO by using two buffers - one of the buffers will get the data while the other will be processed, this should increase performance
                    //Console.WriteLine();
                    //Console.WriteLine();
                    //sw.Start();
                    this.StartDMATransfer(ref _ui32TTTRBuffer, _ui32TTTRBuffer1, _ui32TTTRBuffer2, _iNumberTTTRBufferToProcess, ref _iNumberOfRecords, _iNumberOfRecords1, _iNumberOfRecords2);
                    
                    // Process any TTTR buffer which is ready to be processed
                    if (_iNumberTTTRBufferToProcess > 0)  //check if we really have a buffer to process (must be 1 or 2 for a ready to process buffer)
                    {
                        // Before copy the fetched FiFo to the global buffer, check if we potentially are going to overwrite an area of the global buffer still in processing in some of the other threads (see Save() and BuildImage() functions)
                        this.CheckGlobalTTTROverwrite(_iNumberOfRecords);

                        // Copy the fetched FIFO array to the global FIFO buffer array
                        //sw.Start();                  
                        this.CopyFiFoToArray(_ui32TTTRBuffer, _iNumberOfRecords);
                        //sw.Stop();
                        //Console.WriteLine("Read(): _bFiFoHalfFull: Array.Copy(): {0}[ms], {1}[ts]", sw.ElapsedMilliseconds, sw.ElapsedTicks);  //for debugging purposes
                        //sw.Reset();
                        //sw.Start();
                    }

                    // Complete DMA transfer and assign few other variables                    
					_iNumberOfRecords = this.T3RCompleteDMA();  //end fetching FIFO. Note if no error, _iNumberOfRecords = TimeHarpDefinitions.DMABLOCKSZ
                    if (_iNumberOfRecords != TimeHarpDefinitions.DMABLOCKSZ)  //if _iNumberOfRecord1 is not equal to TimeHarpDefinitions.DMABLOCKSZ, then an error occurred
                    {
                        //Shutdown();  //close Time Harp device
						//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - Time Harp DMA buffer error by function Read()");

                        // Throw an ErrorOccurred event to inform the user.
                        if (this.ErrorOccurred != null)
                        {
                            _logger.Debug("Time Harp: reporting error --> DMA buffer error by function Read()!");
                            this.ErrorOccurred(this, new EventArgs());
                        }
                    }

                    // Flip indexes so that they point to the next TTTR buffer that is in a state to be processed
                    this.AssignIndexesForNextTTTRBuffer(ref _iNumberTTTRBufferToProcess, _iNumberOfRecords, ref _iNumberOfRecords1, ref _iNumberOfRecords2);

                    //sw.Stop();
                    //Console.WriteLine("Read(): _bFiFoHalfFull: StartDMA()-ArrayCopy()-CompleteDMA(): {0}[ms], {1}[ts]", sw.ElapsedMilliseconds, sw.ElapsedTicks);  //for debugging purposes
                    //sw.Reset();

                    // if we have got the FiFo buffer _bFiFoTimeOut = false
                    _bFiFoTimeOut = false;  //rest FiFo time out state
                    _iTimeCounter = 0;  //reset counting for the FiFo time out

                    //_iPoint = 2;  //for performance testing and debugging purposes              
                }
                else if (_bFiFoTimeOut && !_bFiFoEmpty)  // Handle the case of slow counts rate - process buffer data before FIFO gets half full. Buffer must be not empty in order to process, i.e. _bFiFoEmpty = false
                {
                    // Fetch data records from FIFO                    
                    this.StartDMATransfer(ref _ui32TTTRBuffer, _ui32TTTRBuffer1, _ui32TTTRBuffer2, _iNumberTTTRBufferToProcess, ref _iNumberOfRecords, _iNumberOfRecords1, _iNumberOfRecords2);
                    
                    // Process any TTTR buffer which is ready to be processed
                    if (_iNumberTTTRBufferToProcess > 0)  //check if we really have a buffer to process (must be 1 or 2 for a ready to process buffer)
                    {
                        // Before copy the fetched FiFo to the global buffer, check if we potentially are going to overwrite an area of the global buffer still in processing in some of the other threads (see Save() and BuildImage() functions)
                        this.CheckGlobalTTTROverwrite(_iNumberOfRecords);

                        // Copy the fetched FIFO array to the global FIFO buffer array                                         
                        this.CopyFiFoToArray(_ui32TTTRBuffer, _iNumberOfRecords);
                    }

                    // Complete DMA transfer and assign few other variables
					_iNumberOfRecords = this.T3RCompleteDMA();  //end fetching FIFO
                    if (_iNumberOfRecords < 0)  //if _iNumberOfRecord is less than zero, then an error occurred
                    {
                        //Shutdown();  //close Time Harp device
						//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - Time Harp DMA buffer error by function Read()");

                        // Throw an ErrorOccurred event to inform the user.
                        if (this.ErrorOccurred != null)
                        {
                            _logger.Debug("Time Harp: reporting error --> DMA buffer error by function Read()!");
                            this.ErrorOccurred(this, new EventArgs());
                        }
                    }

                    // Flip indexes so that they point to the next TTTR buffer that is in a state to be processed
                    this.AssignIndexesForNextTTTRBuffer(ref _iNumberTTTRBufferToProcess, _iNumberOfRecords, ref _iNumberOfRecords1, ref _iNumberOfRecords2);

                    // if we have got the FiFo buffer _bFiFoTimeOut = false
                    _bFiFoTimeOut = false;  //rest FiFo time out state
                    _iTimeCounter = 0;  //reset counting for the FiFo time out  

                    //_iPoint = 3;  //for performance testing and debugging purposes 
                }


                // Get TickCount - used to check for FiFo timeout
                _iTimeCounterStop = System.Environment.TickCount;

                //sw.Stop();
                //if (sw.ElapsedMilliseconds > 25 || _bFiFoFull)  //show only if the elapsed time is bigger than given value or when FiFo overrun occured
                //{
                //    Console.WriteLine("Read(): {0}[pt]: {1}[ms], {2}[ts], {3}[itr]", _iPoint, sw.ElapsedMilliseconds, sw.ElapsedTicks, _iIterationNumber);  //for debugging purposes              
                //    _iPoint = 0;  //indicates no entering any if() above
                //}
                //sw.Reset();

            }  //END  while-loop (_bMeasurementRunning && !this.m_IsMesurementToBeStopped)


            ////////////////////////////////////////////////////////////////////////
            // Final processing before exiting this function:
			this.StopMeasurement();  //resets the hardware FiFo - stops acquisition in case it is still going on
			this.m_IsMeasurementRunning = false;  // make sure the measurement running status is up-to-date

            //Console.WriteLine("Read(): ElapsedTime = {0}[ms], FiFoOverrunCount = {1}", _iMeasurementTimeCounter, ThreadsStatus.APDReadThread.FiFoOverrunCount);  //for debugging purposes                                                
            //Console.WriteLine("Read(): Performance measured... END! StopwatchFrequrncy = {0} [Hz]", Stopwatch.Frequency);  //for performance testing and debugging purposes 
                                       
            _logger.Info("Time Harp: FiFo overrun occurred " + ThreadsStatus.APDReadThread.FiFoOverrunCount.ToString() + "x" + " during scanning!");              
            
            Thread.EndThreadAffinity();  //ends thread affinity so that it unpins from a given CPU core          
        }


        /// <summary>
        /// Start the current DMA transfer and assign the current TTTR buffer to be processed.
        /// </summary> 
        /// <param name="__ui32TTTRBuffer">Point to the current TTTR buffer to be processed.</param>
        /// <param name="__ui32TTTRBuffer1">The TTTR buffer 1 - records from DMA come here or in TTTR buffer 2.</param>
        /// <param name="__ui32TTTRBuffer2">The TTTR buffer 2 - records from DMA come here or in TTTR buffer 1.</param>        
        /// <param name="__iNumberTTTRBufferToProcess">The number of the next TTTR buffer that is in a state to be processed.</param>
        /// <param name="__iNumberOfRecords">Number of records obtained from the Time Harp TTTR buffer.</param>
        /// <param name="__iNumberOfRecords1">Number of records obtained from the Time Harp TTTR buffer 1.</param>
        /// <param name="__iNumberOfRecords2">Number of records obtained from the Time Harp TTTR buffer 2.</param>
        private void StartDMATransfer(ref UInt32[] __ui32TTTRBuffer, UInt32[] __ui32TTTRBuffer1, UInt32[] __ui32TTTRBuffer2, int __iNumberTTTRBufferToProcess, ref int __iNumberOfRecords, int __iNumberOfRecords1, int __iNumberOfRecords2)
        {
            switch (__iNumberTTTRBufferToProcess)
            {
                case 0: //_iNumberTTTRBufferToProcess = 0, i.e. the first TTTR acquisition
                    {
                        this.T3RStartDMA(__ui32TTTRBuffer1, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                        break;  //exit switch statement
                    }
                case 1: //_iNumberTTTRBufferToProcess = 1, i.e. acquire TTTR buffer into _ui32TTTRBuffer2[] and meanwhile process _ui32TTTRBuffer1[]
                    {
						this.T3RStartDMA(__ui32TTTRBuffer2, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                        __ui32TTTRBuffer = __ui32TTTRBuffer1;  //assigns the TTTR _ui32TTTRBuffer1[] buffer to be processed while waiting for DMA transfer of _ui32TTTRBuffer2[]
                        __iNumberOfRecords = __iNumberOfRecords1;  //number of records in _ui32TTTRBuffer1[] buffer                                
                        break;  //exit switch statement
                    }
                case 2: //_iNumberTTTRBufferToProcess = 2, i.e. acquire TTTR buffer into _ui32TTTRBuffer1[] and meanwhile process _ui32TTTRBuffer2[]
                    {
						this.T3RStartDMA(__ui32TTTRBuffer1, TimeHarpDefinitions.DMABLOCKSZ);  //start fetching FIFO
                        __ui32TTTRBuffer = __ui32TTTRBuffer2;  //assigns the TTTR _ui32TTTRBuffer2[] buffer to be processed while waiting for DMA transfer of _ui32TTTRBuffer1[]
                        __iNumberOfRecords = __iNumberOfRecords2;  //number of records in _ui32TTTRBuffer2[] buffer  
                        break;  //exit switch statement
                    }
            }
        }


        /// <summary>
        /// Assign the number of the next TTTR buffer to be processed.
        /// </summary> 
        /// <param name="__iNumberTTTRBufferToProcess">The number of the next TTTR buffer that is in a state to be processed.</param>
        /// <param name="__iNumberOfRecords">Number of records obtained from the Time Harp TTTR buffer.</param>
        /// <param name="__iNumberOfRecords1">Number of records obtained from the Time Harp TTTR buffer 1.</param>
        /// <param name="__iNumberOfRecords2">Number of records obtained from the Time Harp TTTR buffer 2.</param>
        private void AssignIndexesForNextTTTRBuffer(ref int __iNumberTTTRBufferToProcess, int __iNumberOfRecords, ref int __iNumberOfRecords1, ref int __iNumberOfRecords2)
        {
            switch (__iNumberTTTRBufferToProcess)  //assign few variables so that we flip the TTTR buffers
            {
                case 0: //_iNumberTTTRBufferToProcess = 0, i.e. complete the first TTTR buffer acquisition that goes into _ui32TTTRBuffer1[] buffer
                    {
                        __iNumberOfRecords1 = __iNumberOfRecords;
                        __iNumberTTTRBufferToProcess = 1;  //indicates that next time we are going to process _ui32TTTRBuffer1[] buffer
                        break;  //exit switch statement
                    }
                case 1: //_iNumberTTTRBufferToProcess = 1, i.e. complete the TTTR buffer acquisition that goes into _ui32TTTRBuffer2[] buffer
                    {
                        __iNumberOfRecords2 = __iNumberOfRecords;
                        __iNumberTTTRBufferToProcess = 2;  //indicates that next time we are going to process _ui32TTTRBuffer1[] buffer                              
                        break;  //exit switch statement
                    }
                case 2: //_iNumberTTTRBufferToProcess = 2, i.e. complete the TTTR buffer acquisition that goes into _ui32TTTRBuffer1[] buffer
                    {
                        __iNumberOfRecords1 = __iNumberOfRecords;
                        __iNumberTTTRBufferToProcess = 1;  //indicates that next time we are going to process _ui32TTTRBuffer1[] buffer
                        break;  //exit switch statement
                    }
            }
        }


        /// <summary>
        /// Before copy the fetched FiFo to the global buffer, check if we potentially are going to overwrite an 
        /// area of the global buffer still in processing in some of the other threads (see Save() and BuildImage() functions).
        /// </summary> 
        /// <param name="__iNumberOfRecords">Number of records obtained from the Time Harp TTTR buffer.</param>        
        private void CheckGlobalTTTROverwrite(int __iNumberOfRecords)
        {
            if (ThreadsStatus.APDSaveThread.Thread.IsAlive)  //check first if the Save() thread is running
            {
                if ((ScanStatus.SizeGlobalTTTRBuffer < ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound) && ((ScanStatus.SizeGlobalTTTRBuffer + __iNumberOfRecords) > ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound))  //check if there is danger to overwrite an area of the global buffer still in processing by Save() function
                {
					//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - In the next step Read() function will overwrite, a still in processing by Save() function, area of the ScanStatus.GlobalTTTRBuffer[]!");  // throw exception to indicate to potential danger
					_logger.Warn("Time Harp: reporting a warning --> in the next step Read() function will overwrite, a still in processing by Save() function, area of the ScanStatus.GlobalTTTRBuffer[]!");                    
                }
            }

			if (ThreadsStatus.BuildImageThread.Thread.IsAlive)  //check first if the BuildImage() thread is running
            {
                if ((ScanStatus.SizeGlobalTTTRBuffer < ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound) && ((ScanStatus.SizeGlobalTTTRBuffer + __iNumberOfRecords) > ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound))  //check if there is danger to overwrite an area of the global buffer still in processing by Save() function
                {
					//throw new Exception("KUL.MDS.Hardware.PQTimeHarp Exception - In the next step Read() function will overwrite, a still in processing by BuildImage() function, area of the ScanStatus.GlobalTTTRBuffer[]!");  // throw exception to indicate to potential danger
                    _logger.Warn("Time Harp: reporting a warning --> in the next step Read() function will overwrite, a still in processing by BuildImage() function, area of the ScanStatus.GlobalTTTRBuffer[]!");                                            
                }
            }
        }


        /// <summary>
        /// Copy the fetched Time Harp raw TTTR buffer to the global TTTR buffer.
        /// </summary> 
        /// <param name="__ui32TTTRBuffer">Array of raw TTTR records from Time Harp TTTR buffer.</param>
        /// <param name="__iNumberOfRecords">Number of records obtained from the Time Harp TTTR buffer.</param>
        private void CopyFiFoToArray(UInt32[] __ui32TTTRBuffer, int __iNumberOfRecords)
        {
            if ((ScanStatus.SizeGlobalTTTRBuffer + __iNumberOfRecords - 1) < ScanStatus.GlobalTTTRBuffer.Length)  //check if the buffer is full
            {
                Array.Copy(__ui32TTTRBuffer, 0, ScanStatus.GlobalTTTRBuffer, ScanStatus.SizeGlobalTTTRBuffer, __iNumberOfRecords);  //copy TTTR buffer to the Global TTTR buffer
                ScanStatus.SizeGlobalTTTRBuffer += __iNumberOfRecords;
            }
            else //we have almost reached the end of the global buffer - so store the buffer and restart the processing from the beginning of the buffer
            {
                // First we copy so that fill the global buffer up to the end
                int _iNumberFreeCells = ScanStatus.GlobalTTTRBuffer.Length - ScanStatus.SizeGlobalTTTRBuffer;  //the number of cells that are still free to store records                                                    
                Array.Copy(__ui32TTTRBuffer, 0, ScanStatus.GlobalTTTRBuffer, ScanStatus.SizeGlobalTTTRBuffer, _iNumberFreeCells);  //copy TTTR buffer to the Global TTTR buffer

                // Second we wrap around and start coping from the beginning of the global buffer
                ScanStatus.SizeGlobalTTTRBuffer = 0;  //reset index                        
                Array.Copy(__ui32TTTRBuffer, _iNumberFreeCells, ScanStatus.GlobalTTTRBuffer, ScanStatus.SizeGlobalTTTRBuffer, __iNumberOfRecords - _iNumberFreeCells);  //copy TTTR buffer to the Global TTTR buffer
                ScanStatus.SizeGlobalTTTRBuffer = __iNumberOfRecords - _iNumberFreeCells;
            }
        }


        /// <summary>
        /// Saves the fetched Time Harp raw TTTR records. 
		/// Note: it is run in a separate thread.
		/// Note: do not spawn more than one thread of this method, otherwise the program will misbehave.
        /// </summary>        
        private void Save()
        {            
            //Stopwatch sw = new Stopwatch();  //performance testing

            const int _iTHREAD_SLEEP_TIME = 5;  //default thread sleep time
            int _indexTTTRBufferLowerBound = 0;  //the lower bound for the index of the global buffer
            int _indexTTTRBufferUpperBound = 0;  //the upper bound for the index of the global buffer            

            bool _bMeasurementRunning = true;   // true because we want to probe for raw TTTR data and try to save it 
            bool _bMesurementToBeStop = false;  // false because we want to probe for raw TTTR data and try to save it

            //Console.WriteLine("Save() Started...: SyncRate = {0} [Hz]", this.m_iSyncRate);  //for debugging purposes


            // Loop to read the Time Harp buffer and convert the photon events to pixels data
            while (_bMeasurementRunning && !_bMesurementToBeStop)  //acquire data until the end of the measurement, until the FIFO buffer gets overrun or the frame gets ready
            {
                _bMeasurementRunning = this.m_IsMeasurementRunning;  // check if we are still running, if not at the beginning of the next loop we stop and exit the loop
                _bMesurementToBeStop = this.m_IsMesurementToBeStopped;  // check if we are still running, if not at the beginning of the next loop we stop and exit the loop

                System.Threading.Thread.Sleep(_iTHREAD_SLEEP_TIME);  //put the thread to sleep for about _iTHREAD_SLEEP_TIME [ms], thus let some time to the buffer to fill with data records

                _indexTTTRBufferLowerBound = ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound;  //get the index position of the last saved chunk of data
                _indexTTTRBufferUpperBound = ScanStatus.SizeGlobalTTTRBuffer;  //get the index position of the currently available to be saved chunk of data

                // Save and raw buffer data from the global buffer to file
                //sw.Start();
                if (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)  //check if there is something to be saved in the given buffer
                {
                    this.WriteTTTRDataToFile(ScanStatus.GlobalTTTRBuffer, _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, Files.TTTRFileName);  //write the current TTTR buffer to file
                    ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = _indexTTTRBufferUpperBound;
                }
                else if (_indexTTTRBufferLowerBound > _indexTTTRBufferUpperBound)  //it means the filling of the GlobalTTTRBuffer[] buffer started again so save the chunk up to the end of the buffer and prepare it for the next round of storage iteration
                {
                    this.WriteTTTRDataToFile(ScanStatus.GlobalTTTRBuffer, _indexTTTRBufferLowerBound, ScanStatus.GlobalTTTRBuffer.Length, Files.TTTRFileName);  //write the current TTTR buffer to file
                    ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = 0;  //set the index to zero so that next time we start to save from the beginning of the global buffer                    
                }
                //sw.Stop();
                //if (sw.ElapsedMilliseconds > 0)  //print only when it takes longer than 1[ms] to save/process
                //{
                //    Console.WriteLine("Save(): WriteTTTRDataToFile(): {0}[ms], {1}[ts]", sw.ElapsedMilliseconds, sw.ElapsedTicks);  //for debugging purposes
                //}
                //sw.Reset();
            }


            ////////////////////////////////////////////////////////////////////////
            // Final processing before exiting this function:

            while (ThreadsStatus.APDReadThread.Thread.IsAlive)  //if Read() thread is still running, wait some time so that it terminates (otherwise we may lose TTTR records)
            {
                System.Threading.Thread.Sleep(100);  //wait some time so that Read() thread can finish first (thus we can save all records)
                //Console.WriteLine("Save(): Read() is still alive so wait...");  //for debugging purpose          
            }

            // Save the last chunk of raw TTTR data from the global FIFO buffer
            _indexTTTRBufferLowerBound = ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound;  //get the index position of the last saved chunk of data
            _indexTTTRBufferUpperBound = ScanStatus.SizeGlobalTTTRBuffer;  //get the index position of the currently available to be saved chunk of data

            if (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)  //check if there is something to be saved in the given buffer
            {
                this.WriteTTTRDataToFile(ScanStatus.GlobalTTTRBuffer, _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, Files.TTTRFileName);  //write the current TTTR buffer to file
                ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = _indexTTTRBufferUpperBound;
            }
            else if (_indexTTTRBufferLowerBound > _indexTTTRBufferUpperBound)  //it means the filling of the GlobalTTTRBuffer[] buffer started again so save the chunk up to the end of the buffer and prepare it for the next round of storage iteration
            {
                this.WriteTTTRDataToFile(ScanStatus.GlobalTTTRBuffer, _indexTTTRBufferLowerBound, ScanStatus.GlobalTTTRBuffer.Length, Files.TTTRFileName);  //write the current TTTR buffer to file
                ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = 0;  //set the index to zero so that next time we start to save from the beginning of the global buffer                    

                // Save the last chunk of data (in case the global buffer was being filled from the beginning)
                _indexTTTRBufferLowerBound = ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound;
                this.WriteTTTRDataToFile(ScanStatus.GlobalTTTRBuffer, _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, Files.TTTRFileName);  //write the current TTTR buffer to file
                ThreadsStatus.APDSaveThread.IndexTTTRBufferLowerBound = _indexTTTRBufferUpperBound;
            }

            //Console.WriteLine("Save(): Performance measured... END! StopwatchFrequrncy = {0} [Hz]", Stopwatch.Frequency);           
        }


        /// <summary>
        /// Returns an array of 2D images with the pixels read from the data buffer (Time Harp data buffer).
		/// Note: it is run in a separate thread.
        /// </summary>
        /// <returns>uint[][] _ui32PixelBuffer: An array of arrays of pixels (= 2D image) that holds found and converted images due to the processed pixels read from the Time Harp buffer</returns>
        private void BuildImage()
        {
			// Reset some variables so that they are ready for the processing of a new frame                
            ThreadsStatus.BuildImageThread.IsFrameReady = false;

            // Reset to zero the frame pixel buffer - currently the buffer has a single frame pixels buffer within it
			for (int i = 0; i < ScanStatus.FramePixelBuffer.Length; i++)
			{
				Array.Clear(ScanStatus.FramePixelBuffer[i], 0, ScanStatus.FramePixelBuffer[i].Length);  //reset frame pixel buffer to zero value elements
			}
			                        

            //Stopwatch sw = new Stopwatch();  //performance testing

            const int _iTHREAD_SLEEP_TIME = 20;  //default thread sleep time
               
            int _indexTTTRBufferLowerBound = 0;  //the lower bound for the index of the global buffer
            int _indexTTTRBufferUpperBound = 0;  //the upper bound for the index of the global buffer            

            bool _bFrameReady = ThreadsStatus.BuildImageThread.IsFrameReady;                       
            bool _bMeasurementRunning = true;   // true because we want to probe for raw TTTR data and try to extract pixels from it 
            bool _bMesurementToBeStop = false;  // false because we want to probe for raw TTTR data and try to extract pixels from it 

            uint[][] _ui32FramePixelBuffer = ScanStatus.FramePixelBuffer;  //frame pixel buffer (currently holds space for one image) - note that the memory is allocated in SetupAPDCountAndTiming() function
            
            //Console.WriteLine("BuildImage() Started...: SyncRate = {0} [Hz]", this.m_iSyncRate);  //for debugging purpose


            // Loop to read the Time Harp buffer and convert the photon events to pixels data
            while (_bMeasurementRunning && !_bMesurementToBeStop)  //acquire data until the end of the measurement, until the FIFO buffer gets overrun or the frame gets ready
            {
                //sw.Start();  //for performance/debugging measurements

                _bMeasurementRunning = this.m_IsMeasurementRunning;  // check if we are still running, if not at the beginning of the next loop we stop and exit the loop
                _bMesurementToBeStop = this.m_IsMesurementToBeStopped;  // check if we are still running, if not at the beginning of the next loop we stop and exit the loop
				                
                Thread.Sleep(_iTHREAD_SLEEP_TIME);  //put the thread to sleep for about _iThreadSleepTime [ms], thus let some time to the buffer to fill with data records

                _indexTTTRBufferLowerBound = ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound;  //get the index position of the last saved chunk of data
                _indexTTTRBufferUpperBound = ScanStatus.SizeGlobalTTTRBuffer;  //get the index position of the currently available to be saved chunk of data
                //_bFiFoOverrun = ThreadsStatus.BuildImageThread.IsFiFoOverun;  //get the FiFo overrun status induced to BuildImage() from the Read() thread
				
                // Extract image from TTTR records                
                if (_indexTTTRBufferLowerBound < _indexTTTRBufferUpperBound)  //check if there is something to be saved in the given buffer
                {                            
                    // Process TTTR buffer and extract pixels from photon events                    
                    ExtractPhotonEventsFromTTTRBuffer(ScanStatus.GlobalTTTRBuffer, ref _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, ref _bFrameReady, _ui32FramePixelBuffer[0]);
                   
                    // Save the state of processing so that next time we start from the appropriate place
                    ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound = _indexTTTRBufferLowerBound;  //track/save the current state of the processing with respect to the Global TTTR buffer (necessary in order to continue from here next time we enter BuildImage() function)
                }
                else if (_indexTTTRBufferLowerBound > _indexTTTRBufferUpperBound)  //it means the filling of the GlobalTTTRBuffer[] buffer started again so process the chunk up to the end of the buffer and prepare it for the next round of processing iteration
                {
                    // Extract pixels from photon counts
                    _indexTTTRBufferUpperBound = ScanStatus.GlobalTTTRBuffer.Length;  //in case (_indexLowerBound > _indexUpperBound), we just try to process the buffer up to its end

                    // Process TTTR buffer and extract pixels from photon events                    
                    ExtractPhotonEventsFromTTTRBuffer(ScanStatus.GlobalTTTRBuffer, ref _indexTTTRBufferLowerBound, _indexTTTRBufferUpperBound, ref _bFrameReady, _ui32FramePixelBuffer[0]);
                                        
                    // Save the state of processing so that next time we start from the appropriate place
                    ThreadsStatus.BuildImageThread.IndexTTTRBufferLowerBound = (_indexTTTRBufferLowerBound >= _indexTTTRBufferUpperBound) ? 0 : _indexTTTRBufferLowerBound;  //check if we have processed the entire global TTTR buffer (if so reset the variable to zero so that it is ready for the next round of processing)                                    
                }

				ThreadsStatus.BuildImageThread.IsFrameReady = _bFrameReady;  //update frame status
                
                //sw.Stop();
                //if (sw.ElapsedMilliseconds > 5)  //show only if elapsed time is bigger than the given time
                //{
                //    Console.WriteLine("BuildImage(): while-loop: {0}[ms], {1}[ts]", sw.ElapsedMilliseconds, sw.ElapsedTicks);  //for debugging purposes
                //}
                //sw.Reset(); 

			}  //END while(_bMeasurementRunning && !_bMesurementToBeStop)


            ////////////////////////////////////////////////////////////////////////
            // Final processing before exiting this function:
            
            //Console.WriteLine("BuildImage(): Performance measured... END! StopwatchFrequrncy = {0} [Hz]", Stopwatch.Frequency);  //for debugging purposes            
        }


        /// <summary>
        /// Binning of photons - process the photon events and convert them to pixel data. Store pixels in the respective frame pixel buffer.
        /// </summary> 
        /// <param name="__ui32TTTRBuffer">An array of UInt32 numbers - the raw TTTR records.</param>
        /// <param name="__indexTTTRBufferLowerBound">The index of the TTTR buffer from which we start to process the buffer.</param>
        /// <param name="__indexTTTRBufferUpperBound">The maximum index up to which we can process the current TTTR buffer.</param> 
        /// <param name="_bFrameReady">Indicates if the frame is ready - i.e. all photons within a frame are converted into pixels.</param>        
        /// <param name="__ui32FramePixelBuffer">The pixel buffer - the binned photon events go in this array (the buffer represent a scanned frame or part of a scanned frame).</param>            
        public static void ExtractPhotonEventsFromTTTRBuffer(UInt32[] __ui32TTTRBuffer, ref int __indexTTTRBufferLowerBound, int __indexTTTRBufferUpperBound, ref bool _bFrameReady, uint[] __ui32FramePixelBuffer)
        {
            // Assign scanning parameters
            //int _iTimePPixel = ScanStatus.TimePPixelChannel;
            int _iXWidth = ScanStatus.XImageWidthPx;
            int _iYHeight = ScanStatus.YImageHeightPx;
            int _iPixelCount = ScanStatus.PixelCount;
            int _iPixelCounter = ScanStatus.PixelsCounter;
            int _iOverflow = ScanStatus.Overflow;

            //Allocate a single Processed TTTRecord - we convert on the fly a TTTR record to a processed TTTR record for easier processing
            ProcessedTTTRecord _PTTTRecord = new ProcessedTTTRecord(); 

            // Assign the line pixel buffer (we extract the frame from TTTR records line by line)
            uint[] _ui32LinePixelBuffer = ScanStatus.LinePixelBuffer;  //line pixel buffer - note that the memory is allocated in SetupAPDCountAndTiming() function
            //bool _bLinePixelBufferReady = false;  //indicate if a scanned line with pixels is ready and thus can be copied to the frame pixel buffer


            // Process TTTR buffer and extract pixels from photon events
            while (__indexTTTRBufferLowerBound < __indexTTTRBufferUpperBound)  //loop until we process the entire TTTR buffer
            {
                // Convert raw TTTR records to processed PTTTR records
                ConvertSingleTTTRtoTrueTimeTagPTTTR(__ui32TTTRBuffer[__indexTTTRBufferLowerBound], ref _iOverflow, ref _PTTTRecord);  // extract information form a single 32 bits integer data record 

                // Search for the reference frame/line external markers, then fill the raw photon counts into the line PTTTR buffer and calculate time per pixel. Note that time per pixel may not be calculate on this run - we need a full line (two line markers) to be able to calculate it
                ExternalMarkers.LineMarker.Found = false;  //reset line marker state, necessary in order to find the next line marker and fill the line PTTTR buffer 

                // Check for FiFO buffer overrun by searching for frame marker which comes too early
                if (ExternalMarkers.FrameMarker.Found)  //handles FiFo buffer overrun by resetting frame pixel buffer
                {
                    switch (_PTTTRecord.Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
                    {
                        case 0: //Valid = 0 (external trigger or overflow event occurred)
                            {
                                if (_PTTTRecord.DataMarker == ScanStatus.FrameMarker && ExternalMarkers.LineCount < _iYHeight)  //ScanStatus.FrameMarker it means we have found a line trigger
                                {
                                    _iPixelCounter = _iPixelCount;  //causes the frame to be finished by PhotonEventsToPixelBuffer() function                                        
                                }

                                break;
                            }
                    }
                }

                // Find the reference frame marker (note that if we are processing frame this will be ignored because we are looking for line marker now)
                if (!ExternalMarkers.FrameMarker.Found)  //find the reference marker for frame
                {
                    switch (_PTTTRecord.Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
                    {
                        case 0: //Valid = 0 (external trigger or overflow event occurred)
                            {
                                if (_PTTTRecord.DataMarker == ScanStatus.FrameMarker)  //if DataMarker = ScanStatus.FrameMarker it means we have found a frame trigger
                                {
                                    ExternalMarkers.LineCount = 1;  //start counting of the number of line markers within a frame to have as an info status (frame marker is beginning of line, so count it)
                                    ExternalMarkers.FrameCount++;  //count the number of frame markers to have as an info status
                                    ExternalMarkers.FrameMarker.Found = true;  //the reference frame marker (beginning of frame) has been found
                                    ExternalMarkers.FrameMarker.Index = __indexTTTRBufferLowerBound;  //get the frame marker index from the current TTTR buffer
                                    ExternalMarkers.FrameMarker.TimeTag = _PTTTRecord.TimeTag;  //get the frame marker time tag from the current TTTR buffer

                                    ScanStatus.LinePTTTRBufferTimeTag1 = ExternalMarkers.FrameMarker.TimeTag;
                                    //ExternalMarkers.FrameCount++;  //count the number of frame markers in the current TTTR buffer

                                    _logger.Debug("Time Harp: frame marker found --> overall number of detected frames# " + ExternalMarkers.FrameCount.ToString() + "x...");  // log debug info if a frame marker is found
                                }

                                break;
                            }
                        //case 1:  //Valid = 1 (photon arrival event occurred)
                        //    {
                        //        break;
                        //    }                        
                    }
                }
                else if (ExternalMarkers.FrameMarker.Found && !ExternalMarkers.LineMarker.Found)  //find the reference marker for next line so that we can calculate the time per pixel
                {
                    // Find the reference line marker (note that every photon event between two line markers or a frame and line marker will be pushed in the line PTTTR buffer)
                    switch (_PTTTRecord.Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
                    {
                        case 0: //Valid = 0 (external trigger or overflow event occurred)
                            {
                                if (_PTTTRecord.DataMarker == ScanStatus.LineMarker || _PTTTRecord.DataMarker == ScanStatus.FrameMarker)  //if DataMarker = ScanStatus.LineMarker or ScanStatus.FrameMarker it means we have found a line trigger
                                //if (_PTTTRecord.DataMarker == ScanStatus.LineMarker)  //if DataMarker = ScanStatus.LineMarker it means we have found a line trigger                                        
                                {
                                    ExternalMarkers.LineCount++;  //count the number of line markers to have as an info status
                                    ExternalMarkers.LineMarker.Found = true;  //the reference line marker (beginning of new line) has been found
                                    ExternalMarkers.LineMarker.Index = __indexTTTRBufferLowerBound;  //get the line marker index from the current TTTR buffer
                                    ExternalMarkers.LineMarker.TimeTag = _PTTTRecord.TimeTag;  //get the line marker time tag from the current TTTR buffer

                                    ScanStatus.LinePTTTRBufferTimeTag2 = ExternalMarkers.LineMarker.TimeTag;
                                }

                                break;
                            }
                        case 1:  //Valid = 1 (photon arrival event occurred)
                            {
                                if (ScanStatus.LinePTTTRBufferIndex < ScanStatus.LinePTTTRBuffer.Length)  //check before you transfer photon events to line buffer
                                {
                                    ScanStatus.LinePTTTRBuffer[ScanStatus.LinePTTTRBufferIndex] = _PTTTRecord;  //copy photon events between two lines
                                    ScanStatus.LinePTTTRBufferIndex++;  //shift to the next free array cell
                                }
                                else  //line TTTR buffer overrun - it happens if we have too many photon events
                                {
                                    ScanStatus.LinePTTTRBufferIndex = 0;  //reset index, due to buffer overrun
                                    //ScanStatus.LinePTTTRBufferTimeTag1 = -1;  //reset index, due to buffer overrun
                                    //ScanStatus.LinePTTTRBufferTimeTag2 = -1;  //reset index, due to buffer overrun
                                    _logger.Debug("Time Harp: exception, ScanStatus.LinePTTTRBuffer overrun by function ExtractPhotonEventsFromTTTRBuffer(): to fix the problem edit the source code and increase the size of this buffer!");
                                
                                }


                                break;
                            }
                    }
                }

                // If we have a reference frame marker found as well as line marker found, we can calculate the time per pixel and later on bin photon events to pixels
                if (ExternalMarkers.FrameMarker.Found && ExternalMarkers.LineMarker.Found)  // Convert the line PTTTR buffer to pixels array - the conversion is line by line, i.e. first we get/fill one line and then we convert it to pixels  
                {
                    // Calculate the time per pixel value for the current line
                    //ExternalMarkers.IsNewFrame = false;
                    int _iTimePPixel = ScanStatus.TimePPixelChannel = ((int)(ScanStatus.LinePTTTRBufferTimeTag2 - ScanStatus.LinePTTTRBufferTimeTag1)) / _iXWidth;  //time per pixel in channel number (the channel width is 100e-9[s])
                    ScanStatus.TimePPixelMillisec = ScanStatus.TimePPixelChannel * 100e-9 * 1000;  //time per pixel in milliseconds
                    //ScanStatus.FrameTimeOut = (int)(ScanStatus.TimePPixelMillisec * ScanStatus.PixelCount) / 10;  //frame time out is approx. 10% of the overall time per frame

                    // Bin photons into the line pixel buffer
                    PhotonEventsToPixelBuffer(_iTimePPixel, _iXWidth, _iPixelCount, ref _iPixelCounter, ref _bFrameReady, _ui32LinePixelBuffer, __ui32FramePixelBuffer);  //photon counts to pixels

                    // If a frame is ready exit the while-loop and return the pixel frame buffer
                    if (_bFrameReady)
                    {    
                        break;  //exit the while-loop (__indexLowerBound < __indexUpperBound)
                    }

                }  //END if (ExternalMarkers.FrameMarker.Found && ExternalMarkers.LineMarker.Found)

                // Go to the next TTTR record
                __indexTTTRBufferLowerBound++;

            }  //END while (__indexLowerBound < __indexUpperBound)


            // Keep track of the scanning state so that at the next ExtractPhotonEventsFromTTTRBuffer() function call we use the correct values            
            ScanStatus.PixelsCounter = _iPixelCounter;  //current position of the pixel counter (next time we enter this function we start from this position)
            ScanStatus.Overflow = _iOverflow;  //current overflow flag value
        }


		/// <summary>
		/// Save the frame pixel buffer to a bitmap image file.
		/// </summary>         
		/// <param name="__sOutputFile">The name of the output image file(s).</param>
		/// <param name="__iXWidth">The number of pixels per line.</param>
		/// <param name="__iYHeight">The number of lines per frame.</param>
		/// <param name="__iPixelCount">.</param> 
		/// <param name="__ui32FramePixelBuffer">The pixel buffer - the binned photon events go in this array (the buffer represent a scanned frame or part of a scanned frame).</param>        
		/// <param name="__iFrameCounter">The starting number of the saved bitmap file (in case you want to save more then one file with the same name, use this to distinguish between images).</param>        
		public static void SaveFramePixelBufferAsBitmapImage(string __sOutputFile, int __iXWidth, int __iYHeight, uint[] __ui32FramePixelBuffer, int __iFrameCounter)
		{
			int _iPixelCount = __iXWidth * __iYHeight;  //The capacity of the pixel buffer (usually equals to the overall pixel counts)

			// Allocate temp frame pixel buffer in order to convert to signed integer pixel values
			int[] _i32FramePixelBuffer = new int[_iPixelCount];

			for (int i = 0; i < _iPixelCount; i++)
			{
				_i32FramePixelBuffer[i] = (int)__ui32FramePixelBuffer[i];  // convert the values of the frame pixel buffer from uint to int
			}

			// Create the bitmap
			Bitmap bitmapImage2D = new Bitmap(__iXWidth, __iYHeight, PixelFormat.Format32bppRgb);

			// Lock bitmap and return bitmap data                    
			BitmapData bitmapData = bitmapImage2D.LockBits(new System.Drawing.Rectangle(0, 0, __iXWidth, __iYHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

			// Byte per byte copy of the frame pixel buffer array to location given by the pointer bitmapData.Scan0 (basically to the bitmapImage2D)
			Marshal.Copy(_i32FramePixelBuffer, 0, bitmapData.Scan0, _iPixelCount);

			// Unlock bitmap
			bitmapImage2D.UnlockBits(bitmapData);

			// Save bitmap image to file
			try
			{
				bitmapImage2D.Save(__iFrameCounter.ToString() + "." + __sOutputFile, ImageFormat.Bmp);  //save the image as ".bmp" to file
			}
			catch (Exception ex)
			{
				throw new Exception("Time Harp: exception --> " + "the bitmap image file: " + __iFrameCounter.ToString() + "." + __sOutputFile + " could not be saved by ConvertTTTRFileToBitmapImages() function!" + " Exception Message: " + ex.Message);
			}
		}

                
        // END OF Functions for TTTR buffer handling and TTTR to pixels extraction
        /////////////////////////////////////////////////////////////////////////////

        #endregion Special functions for TTTR buffer handling and TTTR to pixels extraction



        #region Scanning Status and Various Data Processing Routines


        /////////////////////////////////////////////////////////////////////////////
        // BEGIN OF Functions for Scanning Status and various data processing routines
        
        /// <summary>
        /// Structure that keeps track of important scanning parameters.
        /// </summary>            
        private struct ScanStatus
        {
            // External Markers default settings
            // IMPORTANT NOTE FOR THE MARKERS (Markers = synchronization signals send by external harder to the data stream of Time Harp): 
            // The external hardware YanusIV (a galvo scanner) can output three marker bit0, bit1, bit2, since bit0 comes with a ~1us delay Time Harp 
            // detect it as a separate bit - e.g. if you assign to YanusiV a FrameMarker = 7 (111 in binary, bit2=1 bit1=1 bit0=1), it will
            // be detected by Time Harp as two markers, the first one 6 (110 in binary, bit2=1 bit1=1 bit0=0) and the second one 1 (001 in binary, bit2=0 bit1=0 bit0=1).
            // This also means that all odd numbers will be detected in this manner (e.g. if FrameMarker = 5 --> it will be detected as 4 and 1). Therefore, except for one
            // the other markers must be even numbers.
            public static byte StackMarker = 1;  //value of external marker interpreted as a Stack marker
            public static byte FrameMarker = 2;  //value of external marker interpreted as a Frame marker (note that Frame marker is also a line marker)
            public static byte LineMarker = 4;  //value of external marker interpreted as a Line marker
            public static byte PixelMarker = 6;  //value of external marker interpreted as a Pixel marker

            // Image acquisition default settings
            public static double TimePPixelMillisec = 0.0;  //the time per pixel in [ms]
            public static int TimePPixelChannel = 0;  //time per pixel in channel number (then the true time is 100ns * TimePPixel). 100ns is the resolution of the time tag.

            public static int PixelCount = 0;  //number of pixels in one frame
            public static int PixelsCounter = 0;  //processed pixels counter - counts the pixels that have been already processed
			public static volatile int TotalPixelsRead;  // total pixels read from Time Harp buffer - basically coincides with PixelsCounter, but needed in order to expose this value properly to outside world.

            public static int XImageWidthPx = 0;  //number of pixels per line
            public static int YImageHeightPx = 0;  //number of lines to scan
            public static int ZImageDepthPx = 0;  //number of images in the stack

            public static double XScanSizeNm = 0.0;  //the scan size along X in [nm]
            public static double YScanSizeNm = 0.0;  //the scan size along Y in [nm]
            public static double ZScanSizeNm = 0.0;  //the scan size along Z in [nm]

            public static double InitXNm = 0.0;  //initial offset along X in [nm]
            public static double InitYNm = 0.0;  //initial offset along Y in [nm]
            public static double InitZNm = 0.0;  //initial offset along Z in [nm]

            public static int XOverScanPx = 0;  //over scan area along X in [px]
            public static int YOverScanPx = 0;  //over scan area along Y in [px]
            public static int ZOverScanPx = 0;  //over scan area along Z in [px]

            // SIS channel default settings
            public static int SISChannels = 2;  // number of SIS channels

            // Galvo default settings
            public static double GalvoMagnificationObjective = 100.0;  //the magnification of the objective
            public static double GalvoScanLensFocalLength = 40.0;  //the focal length of the scan lens in [mm]
            public static double GalvoRangeAngleDegrees = 4.125;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            public static double GalvoRangeAngleInt = 4096.0;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           

            // Sync (laser timing) pulse default settings
            public static double SyncTimeMillisec = 0.0;  //the SYNC time in [ms] - i.e. the time interval between the laser pulses. Useful for the time gating feature.
            public static int SyncTimeChannel = 0;  //the SYNC time in channel number (then the true time is this.m_fResolution * SyncTimeChannel). this.m_fResolution is the resolution of the time tag in [ns].

            // Gating time default settings - note that only photons between the min and max gating time will be allowed to pass the gate (i.e. will be counted)
            public static bool IsToApplyTimeGating = false;  // to do time gating or not (default is is false, i.e. no time gating)
            
            public static double GatingTimeMinMillisec = 0.0;  //the minimum (the lower bound of the) gating time in [ms]
            public static int GatingTimeMinChannel = 0;  //the minimum (the lower bound of the) gating time in channel number (then the true time is this.m_fResolution * GatingTimeMinChannel). this.m_fResolution is the resolution of the time tag in [ns].
            public static ushort GatingTimeMinReverseChannel = 0;  //the minimum (the lower bound of the) gating time in terms of reverse start-stop channel number (then the true time is SyncTimeChannel - GatingMinTimeChannel). Thus all photons with a channel number bigger than GatingTimeMinReverseChannel will be discarded/gated.

            public static double GatingTimeMaxMillisec = Double.MaxValue;  //the maximum (the upper bound of the) gating time in [ms]
            public static int GatingTimeMaxChannel = Int32.MaxValue;  //the maximum (the upper bound of the) gating time in channel number (then the true time is this.m_fResolution * GatingTimeMaxChannel). this.m_fResolution is the resolution of the time tag in [ns].
            public static ushort GatingTimeMaxReverseChannel = 0;  //the maximum (the upper bound of the) gating time in terms of reverse start-stop channel number (then the true time is SyncTimeChannel - GatingMaxTimeChannel). Thus all photons with a channel number bigger than GatingTimeMaxReverseChannel will be discarded/gated.

            // Other default settings
            public static int TypeOfScan = 0;  //the type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan), the default is 0 - unidirectional frame scan
            public static int Overflow = 0;  //overflow flag - keeps track of the number of times an overflow flag occurred in the Time Harp data stream
            public static int FrameTimeOut = 200;  //default frame time out in [ms] after which all pixels in the buffer will be processed and shown

            public static int FiFoTimeOut = 200;  //default Time Harp's FiFo time out in [ms] - the time period after which the FiFo will be fetched and read although it may not be Half Full (we do not want to wait too long when the count rate is low and filling FiFo slow, respectively). Note that it is good if FiFo time out is smaller or equal to the frame time out (thus we get and show the pixels synchronized with the frame). 

            // Various buffers' allocation and declaration settings
            public static uint[][] FramePixelBuffer;  //frame buffer with the processed pixels - the allocate frame buffer size will be for two frames             
            public static uint[] LinePixelBuffer;  //line buffer with the processed pixels

            public static volatile UInt32[] GlobalTTTRBuffer = new UInt32[m_iGlobalTTTRBufferSize * TimeHarpDefinitions.DMABLOCKSZ];  //the global TTTR buffer - a pool for the Time Harp FiFo buffers. The size is 100x the size of the 1/2 Time Harp FiFo buffer. Note that it needs to be "volatile" so that every thread gets the most up to date value.
            public static volatile int SizeGlobalTTTRBuffer = 0;  //the current size up to which the buffer is filled with values. Note that it needs to be "volatile" so that every thread gets the most up to date value.

            //public static ProcessedTTTRecord[] GlobalPTTTRBuffer = new ProcessedTTTRecord[100 * TimeHarpDefinitions.DMABLOCKSZ];  //the Processed TTTR buffer. The size is 100x the size of the 1/2 Time Harp FiFo buffer
            //public static int SizeGlobalPTTTRBuffer = 0;  //the index of the last available processed TTTR record in the given buffer (it must not exceed the length of the buffer)

            public static ProcessedTTTRecord[] LinePTTTRBuffer = new ProcessedTTTRecord[m_iLinePTTTRBufferSize * TimeHarpDefinitions.DMABLOCKSZ];  //the line Processed TTTR buffer. The size is 100x the size of the 1/2 Time Harp FiFo buffer
            public static volatile int LinePTTTRBufferIndex = 0;  //the index of the last available processed TTTR record in the given buffer (it must not exceed the length of the buffer, if it happens it wraps to the beginning of the buffer)
            public static long LinePTTTRBufferTimeTag1 = -1;  //time tag of the line marker 1, it marks the beginning of the buffer
            public static long LinePTTTRBufferTimeTag2 = -1;  //time tag of the line marker 2, it marks the end of the buffer 
        }


        /// <summary>
        /// Structure to keep the threads' data/status/info. This is used to interact with the created threads.
        /// </summary>             
        private struct ThreadsStatus
        {
            public struct APDReadThread  //keeps track of the thread that spawns Read() function (that fetches the Time Harp FiFo buffer)
            {
                public static Thread Thread;  //refer to the thread that spawns Read() function (that fetches the Time Harp FiFo buffer)
                //public static volatile bool IsRunning = false;  //keep track if the thread is still running
                public static volatile int IndexTTTRBufferLowerBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                public static volatile int IndexTTTRBufferUpperBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                public static volatile bool IsFiFoOverun = false;  //keeps track of if Time Harp overrun
                public static volatile int FiFoOverrunCount = 0;  //keeps track of the number of times Time Harp FiFo buffer overrun
            }

            public struct APDSaveThread  //keeps track of the thread that spawns Save() function (that saves the GlobalTTTRBuffer[] buffer to file)                
            {
                public static Thread Thread;  //refer to the thread that spawns Save() function
                //public static volatile bool IsRunning = false;  //keep track if the thread is still running
                public static volatile bool IsToRun = true;  //if you want to save the acquired TTTR binary data to file choose true otherwise false
                public static volatile int IndexTTTRBufferLowerBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                public static volatile int IndexTTTRBufferUpperBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                //public static volatile bool IsFiFoOverun = false;  //keeps track of if Time Harp overrun
            }

            public struct BuildImageThread  //keeps track of the main thread that executes BuildImage() function (that extract pixels from the global buffer)
            {
                public static Thread Thread;  //refer to the thread that spawns BuildImage() function
                //public static volatile bool IsRunning = false;  //keep track if the thread is still running
				public static volatile bool IsToRun = true;  //if you want to extract an image from the acquired TTTR binary data choose true otherwise false
                public static volatile int IndexTTTRBufferLowerBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                public static volatile int IndexTTTRBufferUpperBound = 0;  //keeps track of the processing of the ScanStatus.GlobalTTTRBuffer[] buffer
                //public static volatile bool IsFiFoOverun = false;  //keeps track of if Time Harp overrun
                public static volatile bool IsFrameReady = false;  //keep track if there is a ready frame
            }
        }


        /// <summary>
        /// Structure to get the index and status of the external markers. This info is later used to extract the pixels from the buffer.
        /// </summary>             
        private struct ExternalMarkers
        {
            //public static int Count = 0;  //number of external markers in the currently processed TTTR buffer
            //public static int PixelCount = 0;  //number of pixel markers in the currently processed TTTR buffer
            public static int LineCount = 0;  //number of line markers in the currently processed TTTR buffer
            public static int FrameCount = 0;  //number of frame markers in the currently processed TTTR buffer
            //public static int StackCount = 0;  //number of stack markers in the currently processed TTTR buffer
            //public static bool IsNewFrame = true;  //true if we are about to process new frame; false if we are processing an existing  frame                

            public struct FrameMarker  //Frame maker, stores index and time tag of the a frame in the currently processed TTTR buffer
            {
                public static bool Found = false;
                public static int Index = -1;
                public static long TimeTag = -1;
            }

            public struct LineMarker  //Line maker, stores index and time tag of the a line in the currently processed TTTR buffer
            {
                public static bool Found = false;
                public static int Index = -1;
                public static long TimeTag = -1;
            }
        }


        /// <summary>
        /// Structure that uses hexadecimal masks to help extraction of a TTTR data record - necessary
        /// to read a single data record (which is unsigned 32 bits integer).
        /// </summary>            
        private struct TTTRMasks
        {
            public const uint MASK_TIME_TAG = 0xFFFF;  //= 65535 = 2^16 - 1; This bit mask is intended to extract the TimeTag from the data record
            public const uint MASK_CHANNEL = 0x0FFF;  //= 4095 = 2^12 - 1; This bit mask is intended to extract the Data/Channel field from the data record
            public const uint MASK_ROUTE = 0x0003;  //= 3 = 2^2 - 1; This bit mask is intended to extract the Route field from the data record
            public const uint MASK_VALID = 0x0001;  //= 1 = 2^1 - 1; This bit mask is intended to extract the Valid field from the data record
            public const uint MASK_RESERVED = 0x0001;  //= 1 = 2^1 - 1; This bit mask is intended to extract the Reserved field from the data record

            public const uint MASK_DATA_MARKER = 0x0007;  //= 7; This bit mask is intended to extract the DataMarker field from the TTTRrecord.Data
            public const uint MASK_DATA_RESERVED = 0x00FF;  //= 255 = 2^8 - 1; This bit mask is intended to extract the DataReserved field from the TTTRrecord.Data
            public const uint MASK_DATA_OVERFLOW = 0x0001;  //= 1; This bit mask is intended to extract the DataOverflow field from the TTTRrecord.Data                          
        }


        /// <summary>
        /// Structure that represents the internal structure of a single data record - necessary
        /// to read a single data record (which is unsigned 32 bits integer).
        /// </summary>            
        public struct RawTTTRrecord
        {
            public ushort TimeTag;  //TimeTag[16bits]. 100ns accuracy time tag of photon arrival
            public ushort Data;  //Data[12bits]. Channel (reversed start-stop time) or Overflow[1bit]-Reserved[8bits]-Marker[3bits]
            public byte Route;  //Route[2bits]. Router Channel, max 4 channels
            public byte Valid;  //Valid[1bit]. Valid = 0 (means overflow or external trigger event), Valid = 1 (means photon arrival event)
            public byte Reserved;  //Reserved[1bit]. Picoquant reserves this field for future use

            public byte DataMarker;  //DataMarker[3bits]. Each combination of bits represents a marker state, e.g. DataMarker = 101 -> DataMarker1 = 1, DataMarker2 = 0, DataMarker3 = 1
            public byte DataReserved;  //DataReserved[8bits]. Picoquant reserves this field for future use
            public byte DataOverflow;  //DataOverflow[1bit]. If DataOverflow = 1 it marks overflow of TimeTag variable. Then add 2^16 to recover the true time tag                
        }


        /// <summary>
        /// Structure that is more convenient representation of a TTTR record of a single 
        /// data record - used to process and analyze TTTR records.
        /// </summary>             
        public struct ProcessedTTTRecord
        {
            public long TimeTag;  //time tag of photon arrival in channels number, the true time tag is then TrueTimeTag[s] = TimeTag * 100[ns] * 1e-9
            public ushort Channel;  //Channel (reversed start-stop time)
            public byte Route;  //Router Channel, max 4 channels            
            public byte Valid;  //Valid = 0 (means overflow or external trigger event), Valid = 1 (means photon arrival event)
            //public byte Reserved;  //Reserved[1bit]. Picoquant reserves this field for future use

            public byte DataOverflow;  //Overflow = 0 (means no overflow), Overflow = 1 (means overflow)
            //public byte DataReserved;  //Reserved[8bits]. Picoquant reserves this field for future use
            public byte DataMarker;  //DataMarker[3bits]. Each combination of bits represents the markers' state, e.g. DataMarker = 101 -> DataMarker1 = 1, DataMarker2 = 0, DataMarker3 = 1
        }


        /// <summary>
        /// Convert a single raw TTTR record from the buffer to a Processed TTTR (PTTTR), as it populates the necessary data 
        /// fields - this information can be then used in any kind of data processing.
        /// </summary> 
        /// <param name="__ui32TTTRValue">32 bit unsigned integer - a single data record from Time Harp buffer.</param>
        /// <param name="__PTTTRRecord">a single ProcessedTTTRecord structure.</param>                       
        public static void ConvertSingleTTTRtoPTTTR(UInt32 __ui32TTTRValue, ref ProcessedTTTRecord __PTTTRRecord)
        {
            __PTTTRRecord.TimeTag = (long)(__ui32TTTRValue & TTTRMasks.MASK_TIME_TAG);  //extract the time tag from the data record (32 bits unsigned integer)
            __PTTTRRecord.Channel = (ushort)((__ui32TTTRValue >> 16) & TTTRMasks.MASK_CHANNEL);  //extract the Data/Channel field from the data record (32 bits unsigned integer)
            __PTTTRRecord.Route = (byte)((__ui32TTTRValue >> 28) & TTTRMasks.MASK_ROUTE);  //extract the Route field from the data record (32 bits unsigned integer)
            __PTTTRRecord.Valid = (byte)((__ui32TTTRValue >> 30) & TTTRMasks.MASK_VALID);  //extract the Valid field from the data record (32 bits unsigned integer)
            //__PTTTRRecord.Reserved = (byte)((__ui32TTTRValue >> 31) & TTTRMasks.MASK_RESERVED);  //extract the Reserved field from the data record (32 bits unsigned integer)


            // Check if special event occurred and then if Yes processed it
            switch (__PTTTRRecord.Valid)  //if Valid = 0 (special event occurred), otherwise if Valid = 1 (photon arrival event occurred)
            {
                case 0:  //case 0 - special event occurred (non-photon arrival event), i.e. overflow or/and external trigger (marker). Then we need to process __PTTTRRecord.Data differently 
                    {
                        __PTTTRRecord.DataMarker = (byte)(__PTTTRRecord.Channel & TTTRMasks.MASK_DATA_MARKER);  //extract the DataMarker field
                        //__PTTTRRecord.DataReserved = (byte)((__PTTTRRecord.Data >> 3) & TTTRMasks.MASK_DATA_RESERVED);  //extract the DataReserved field from the __PTTTRRecord.Data
                        __PTTTRRecord.DataOverflow = (byte)((__PTTTRRecord.Channel >> 11) & TTTRMasks.MASK_DATA_OVERFLOW);  //extract the DataOverflow field from the __PTTTRRecord.Data

                        break;
                    }
                case 1: //it makes sure to set the correct values of some variables on photon arrival
                    {
                        __PTTTRRecord.DataMarker = 0;  //set the DataMarker field to zero
                        //__PTTTRRecord.DataReserved = 0;  //set the DataReserved field to zero
                        __PTTTRRecord.DataOverflow = 0;  //set the DataOverflow field to zero

                        break;
                    }
            }
        }

            
        /// <summary>
        /// Convert a single raw TTTR record from the buffer to a Processed TTTR (PTTTR) with true time tag, and populate the necessary data 
        /// fields - this information can be then used in any kind of data processing.
        /// </summary> 
        /// <param name="__ui32TTTRValue">32 bit unsigned integer - a single data record from Time Harp buffer.</param>
        /// <param name="__iOverflow">Overflow flag. Keeps track of the number of overflow events in the Time Harp data buffer.</param>
        /// <param name="__PTTTRRecord">a single ProcessedTTTRecord structure.</param>                
        public static void ConvertSingleTTTRtoTrueTimeTagPTTTR(UInt32 __ui32TTTRValue, ref int __iOverflow, ref ProcessedTTTRecord __PTTTRRecord)
        {
            // Convert raw TTTR records to processed PTTTR records with true time tag
            ConvertSingleTTTRtoPTTTR(__ui32TTTRValue, ref __PTTTRRecord);  // extract information form a single 32 bits integer data record 

            switch (__PTTTRRecord.Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
            {
                case 0: //Valid = 0 (external trigger or overflow event occurred)
                    {
                        if (__PTTTRRecord.DataOverflow == 1)  //if DataOverflow = 0 (no overflow of __PTTTRRecord.TimeTag), DataOverflow = 1 (overflow of __PTTTRRecord.TimeTag occurred)
                        {
                            __iOverflow++;  //at overflow event, increase the overflow flag   
                        }

                        break;
                    }
                //case 1:  //Valid = 1 (photon arrival event occurred)
                //    {
                //        break;
                //    }                        
            }

            __PTTTRRecord.TimeTag += 65536L * __iOverflow;  //calculate the true Time Tag from the TimeTag and the overflow status
        }
        
        
        /// <summary>
        /// Convert the Time Harp TTTR buffer in more appropriate ProcessedTTTRecord (PTTTR) type data buffer.
        /// Thus all parameters from the Time Harp buffer are easily accessible for further analysis.
        /// </summary> 
        /// <param name="__indexTTTRBufferLowerBound">the lower bound for the global TTTR buffer (from where the conversion of TTTR into PTTTR starts).</param>
        /// <param name="__indexTTTRBufferUpperBound">the upper bound for the global TTTR buffer (up to where the conversion of TTTR into PTTTR finishes).</param>
        /// <param name="__ui32TTTRBuffer">Array of TTTR raw records with the same format as in the Time Harp FiFo buffer.</param>            
        /// <param name="__iOverflow">Overflow flag. Keeps track of the number of overflow events in the Time Harp data buffer.</param>
        /// <param name="__iPTTTRBufferOffset">Offset for PTTTR buffer - we may have records from previous processing.</param>
        /// <param name="__PTTTRBuffer">Array that will hold the processed TTTR records.</param>        
        public static void ConvertTTTRBufferToPTTTRBuffer(int __indexTTTRBufferLowerBound, int __indexTTTRBufferUpperBound, UInt32[] __ui32TTTRBuffer, ref int __iOverflow, int __iPTTTRBufferOffset, ProcessedTTTRecord[] __PTTTRBuffer)
        {
            // Loop to read and translate the Time Harp buffer records to the new ProcessedTTTRecord type buffer records
            for (int i = __indexTTTRBufferLowerBound; i < __indexTTTRBufferUpperBound; i++)
            {
                int j = __iPTTTRBufferOffset + (i - __indexTTTRBufferLowerBound);  //get the correct index for __PTTTRBuffer[] (index starts from zero)
                ConvertSingleTTTRtoPTTTR(__ui32TTTRBuffer[i], ref __PTTTRBuffer[j]);  // extract information form a single 32 bits integer data record 

                switch (__PTTTRBuffer[j].Valid) //Valid = 0 (external trigger or overflow event), Valid = 1 (means photon arrival event)
                {
                    case 0: //Valid = 0 (external trigger or overflow event occurred)
                        {
                            if (__PTTTRBuffer[j].DataOverflow == 1)  //if DataOverflow = 0 (no overflow of _PTTTRBuffer[j].TimeTag), DataOverflow = 1 (overflow of _PTTTRBuffer[j].TimeTag occurred)
                            {
                                __iOverflow++;  //at overflow event, increase the overflow flag   
                            }

                            break;
                        }
                    //case 1:  //Valid = 1 (photon arrival event occurred)
                    //    {
                    //        break;
                    //    }                        
                }

                __PTTTRBuffer[j].TimeTag += 65536L * __iOverflow;  //calculate the true Time Tag from the TimeTag and the overflow status
            }

        }
		

        /// <summary>
        /// Binning of photons - process the photon events and convert them to pixel data. Store pixels in the respective frame pixel buffer.
        /// </summary> 
        /// <param name="__iTimePPixel">The time per pixel in terms of channel number (channel width is 100e-9[s]).</param>
        /// <param name="__iXWidth">The number of pixels per line.</param>
        /// <param name="__iPixelCount">The capacity of the pixel buffer (usually equals to the overall pixel counts).</param> 
        /// <param name="__iPixelCounter">The pixels counter. Keeps track of the current pixel to process.</param>        
        /// <param name="__ui32LinePixelBuffer">The pixel buffer - the binned photon events go in this array (the buffer represent a scanned frame or part of the scanned frame).</param>            
        /// <param name="__ui32FramePixelBuffer">The pixel buffer - the binned photon events go in this array (the buffer represent a scanned frame or part of the scanned frame).</param>                            
        public static void PhotonEventsToPixelBuffer(int __iTimePPixel, int __iXWidth, int __iPixelCount, ref int __iPixelCounter, ref bool __bFrameReady, uint[] __ui32LinePixelBuffer, uint[] __ui32FramePixelBuffer)
        {
            long _lCurrentTimeTag;  //the time tag of the current event
            long _lReferenceTimeTag = ScanStatus.LinePTTTRBufferTimeTag1;  //the time tag of the line/frame marker of the beginning of the line TTTR buffer            
            int _iTempPixelCounter = __iPixelCounter;  //store temporary the number of currently processed pixels
           
            if (__iPixelCounter < __iPixelCount)  //check if in the current frame we still have pixels that must be processed and filled with photons
            {
                int i = 0;  //loop counter
                int _iLinePixelCounter = 0;  //count the current pixels for the processed line. Note that it is necessary for the proper binning of the photons
                
                // Applying time gating or not to the processed photon events:
                switch(ScanStatus.IsToApplyTimeGating)
                {
                    case true:  // the case of time gating
                        {
                            while (i < ScanStatus.LinePTTTRBufferIndex)  //convert the line PTTTR buffer into pixels
                            {
                                _lCurrentTimeTag = ScanStatus.LinePTTTRBuffer[i].TimeTag;  //current time tag of the event
                                int _iTimeChannel = (int)(_lCurrentTimeTag - (_lReferenceTimeTag + (long)(__iTimePPixel * _iLinePixelCounter)));  //calculate the size of the channel for the current photon

                                if (_iTimeChannel <= __iTimePPixel)  //check if the current photon event is within the given pixel time bin
                                {
                                    // Do binning and gating of the detected photons photons (here the gating is in terms of the reverse start-stop time) - note that only photons between the min and max gating value will have contribution to the image
                                    //if (ScanStatus.LinePTTTRBuffer[i].Channel < ScanStatus.GatingTimeMinReverseChannel && ScanStatus.LinePTTTRBuffer[i].Channel > ScanStatus.GatingTimeMaxReverseChannel)  //check if the photon passes the gate
                                    //{
                                    //    __ui32LinePixelBuffer[_iLinePixelCounter] += 1U;  //bin photons
                                    //}

                                    // NOTE: this below is temporal gating algorithm - due to misbehavior of Time Harp the sync time is not reliable
                                    // Do binning and gating of the detected photons photons - note that only photons between the min and max gating value will have contribution to the image
                                    if (ScanStatus.LinePTTTRBuffer[i].Channel < ScanStatus.GatingTimeMinChannel && ScanStatus.LinePTTTRBuffer[i].Channel > ScanStatus.GatingTimeMaxChannel)  //check if the photon passes the gate
                                    {
                                        __ui32LinePixelBuffer[_iLinePixelCounter] += 1U;  //bin photons
                                    }
                                    
                                    i++;  //go to the next photon event
                                }
                                else
                                {
                                    _iLinePixelCounter++;  //move the line pixel counter to the next pixel
                                    if (_iLinePixelCounter >= __iXWidth)  //this condition will discard photons which may exceed the the channel number of the current line (this is necessary due to rounding down to nearest integer when calculating Time Per Pixel)
                                    {
                                        break;  //exits the while-loop
                                    }

                                    __iPixelCounter++;  //increase pixel number, i.e. go to next pixel
                                }
                            }

                            break;  //exit case statement
                        }
                    case false:  // the case no time gating
                        {
                            while (i < ScanStatus.LinePTTTRBufferIndex)  //convert the line PTTTR buffer into pixels
                            {
                                _lCurrentTimeTag = ScanStatus.LinePTTTRBuffer[i].TimeTag;  //current time tag of the event
                                int _iTimeChannel = (int)(_lCurrentTimeTag - (_lReferenceTimeTag + (long)(__iTimePPixel * _iLinePixelCounter)));  //calculate the size of the channel for the current photon

                                if (_iTimeChannel <= __iTimePPixel)  //check if the current photon event is within the given pixel time bin
                                {                                    
                                    __ui32LinePixelBuffer[_iLinePixelCounter] += 1U;  //bin photons
                                    
                                    i++;  //go to the next photon event
                                }
                                else
                                {
                                    _iLinePixelCounter++;  //move the line pixel counter to the next pixel
                                    if (_iLinePixelCounter >= __iXWidth)  //this condition will discard photons which may exceed the the channel number of the current line (this is necessary due to rounding down to nearest integer when calculating Time Per Pixel)
                                    {
                                        break;  //exits the while-loop
                                    }

                                    __iPixelCounter++;  //increase pixel number, i.e. go to next pixel
                                }
                            }

                            break;  //exit case statement
                        }
                }  // end switch statement

                __iPixelCounter = _iTempPixelCounter + __iXWidth;  //update the number of currently processed pixels to the correct one. Note that this equals __iPixelCounter prior to the line processing + the number of pixels in a single line (given by __iXWidth)

                ScanStatus.LinePTTTRBufferIndex = 0;  //reset index, because buffer already processed
                ScanStatus.LinePTTTRBufferTimeTag1 = ScanStatus.LinePTTTRBufferTimeTag2;  //assign the new reference time tag (which is the last event from the current buffer) for the next time we process photon events
                ScanStatus.LinePTTTRBufferTimeTag2 = -1;  //upper bound of the buffer time tag is now -1 (because we have already processed the buffer)

				// Transfer line pixel buffer to the frame pixel buffer
				switch (ScanStatus.TypeOfScan)
				{
					case 0:  //case of unidirectional scanning
						{
							int _iPixels = __iPixelCounter - __iXWidth;  //assign the starting pixel index

							for (int indexPixel = 0; indexPixel < __iXWidth; indexPixel++)  //loop through all pixels - bidirectional
							{
								__ui32FramePixelBuffer[_iPixels + indexPixel] = __ui32LinePixelBuffer[indexPixel];  //transfer pixels to the frame buffer
								__ui32LinePixelBuffer[indexPixel] = 0U;  //reset line pixel buffer values to zero so that it is ready for the next pixel extraction
							}

							break;
						}
					case 1:  //case of bidirectional scanning
						{
							int _iPixels = __iPixelCounter - __iXWidth;  //assign the starting pixel index
							int _iResultIntDiv = _iPixels / __iXWidth;
							int _iResultIntModulo = _iResultIntDiv % 2;

							if (_iResultIntModulo == 0)  //even pixels line
							{
								for (int indexPixel = 0; indexPixel < __iXWidth; indexPixel++)  //loop through all pixels - bidirectional
								{
									__ui32FramePixelBuffer[_iPixels + indexPixel] = __ui32LinePixelBuffer[indexPixel];  //transfer pixels to the frame buffer
									__ui32LinePixelBuffer[indexPixel] = 0U;  //reset line pixel buffer values to zero so that it is ready for the next pixel extraction
								}
							}
							else  //odd pixels line
							{
								for (int indexPixel = 0; indexPixel < __iXWidth; indexPixel++)  //loop through all pixels - bidirectional
								{
									int j = (__iXWidth - 1) - indexPixel;  //calculate the correct index 'j' for the line pixel buffer so that we flip the pixels and thus get the right pixels order in the frame pixel buffer
									__ui32FramePixelBuffer[_iPixels + indexPixel] = __ui32LinePixelBuffer[j];  //transfer pixels to the frame buffer
									__ui32LinePixelBuffer[j] = 0U;  //reset line pixel buffer values to zero so that it is ready for the next pixel extraction
								}
							}

							break;
						}
					default:
						{
							//
							break;
						}
				}
            }
			

            // Check if we have a frame ready
            if (__iPixelCounter >= __iPixelCount)  //the case when the frame is ready
            {
                // Reset some variables so that they are ready for the processing of a new frame
                //ExternalMarkers.IsNewFrame = true;
                __bFrameReady = true;
                ThreadsStatus.BuildImageThread.IsFrameReady = __bFrameReady;

                ExternalMarkers.FrameMarker.Found = false;
                ExternalMarkers.FrameMarker.Index = -1;
                ExternalMarkers.FrameMarker.TimeTag = -1;

                ExternalMarkers.LineMarker.Found = false;
                ExternalMarkers.LineMarker.Index = -1;
                ExternalMarkers.LineMarker.TimeTag = -1;
                _logger.Debug("Time Harp: number scanned lines per frame found # " + ExternalMarkers.LineCount.ToString());

                ScanStatus.TimePPixelChannel = 0;
                ScanStatus.TimePPixelMillisec = 0.0;

				ScanStatus.TotalPixelsRead = __iPixelCounter;  // get the total amount of pixels read.

                __iPixelCounter = 0;
                ScanStatus.PixelsCounter = __iPixelCounter;
            }
            else
            {
				ScanStatus.TotalPixelsRead = __iPixelCounter;  // get the total amount of pixels read.

                __bFrameReady = false;  //set to also to indicate we are still processing a frame
                ThreadsStatus.BuildImageThread.IsFrameReady = __bFrameReady;
            }
        }


		/// <summary>
		/// Returns the current frame pixel buffer. Note that the image/frame itself is extracted in another thread called BuildImage() function.
		/// So the purpose of this function is give access to this frame from the outside world.
		/// </summary>
		/// <return>Returns one dimensional array with the processed pixels, which represent an image/frame as extracted from the TTTR binary data stream.</return>
		public uint[] GetImage()
		{
			return ScanStatus.FramePixelBuffer[0];
		}


        // END OF Functions for Scanning Status and various data processing routines
        /////////////////////////////////////////////////////////////////////////////

        #endregion Scanning Status and Various Data Processing Routines




		#region PQTimeHarp Class Constructor and Initialize Methods

		// Default constructor - obviously it needs to be private to prevent normal instantiation.
        /// <summary>
        /// Constructor. Private because it is part of a Singleton pattern.
        /// </summary>
		//private PQTimeHarp()
        //{
        //    // The Time Harp object should be instantiated in an uninitialized state.
        //    this.m_bIsInitialized = false;
        //}


        // Constructor - it gets the Time Harp settings
        /// <summary>
		/// An PQTimeHarp hardware object Constructor that creates Time Harp counting board object.
        /// </summary>
        /// <param name="__iMeasurementMode">The Time Harp measurement/acquisition mode. There are two possible modes: There are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode</param>
        /// <param name="__iCFDDiscrMin">The CFD level in [mV] of Time Harp (min value = TimeHarpDefinitions.DISCRMIN; max value = TimeHarpDefinitions.DISCRMAX)</param>
        /// <param name="__iCFDZeroCross">The CFD zero cross level in [mV] of Time Harp (min value = TimeHarpDefinitions.ZCMIN; max value = TimeHarpDefinitions.ZCMAX)</param>
        /// <param name="__iSyncLevel">The Sync level in [mV] of Time Harp (min value = TimeHarpDefinitions.SYNCMIN; max value = TimeHarpDefinitions.SYNCMAX)</param>
        /// <param name="__iRangeCode">The range code, used in order to set up the time resolution of Time Harp (min value = 0; max value = TimeHarpDefinitions.RANGES - 1)</param>
        /// <param name="__iOffset">The Time Harp board offset value in [ns], value is approximation of the desired offset (in steps of 2.5ns); (min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX)</param>
        /// <param name="__iMarkerEdge">Marker 0, set the active TTL edge: 0 - falling edge or 1 - rising edge</param>         
        /// <param name="__iGlobalTTTRBufferSize">global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)</param>  
        /// <param name="__iLinePTTTRBufferSize">line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)</param>  
        public PQTimeHarp(
            int __iMeasurementMode,
            int __iCFDDiscrMin,
            int __iCFDZeroCross,
            int __iSyncLevel,
            int __iRangeCode,
            int __iOffset,
            int __iMarkerEdge,
            int __iGlobalTTTRBufferSize,
            int __iLinePTTTRBufferSize
            )
        {
            // Check if we are already initialized - only set up values if we are not initialized
            if (!this.m_bIsInitialized)
            {
                this.CFDDiscrMin = __iCFDDiscrMin; //validate and set the CFD level of Time Harp
				this.CFDZeroCross = __iCFDZeroCross; //validate and set the CFD zero cross level of Time Harp
				this.SyncLevel = __iSyncLevel;  //validate and set the Sync level of Time Harp          
				this.RangeCode = __iRangeCode; //validate and set the Range (time resolution) of Time Harp
				this.Offset = __iOffset; // validate and set the offset for Time Harp
				this.MarkerEdge = __iMarkerEdge;  //get the active TTL edge (0 - falling edge, 1 - rising edge)


                // Get the Time Harp measurement mode (0 - histogramming or TTTR mode; 1 - continuous mode).
                // Note that when __iMeasurementMode is used in InitializeTimeHarp() function then 0 - histogramming, and 1 - TTTR mode.
                // So in InitializeTimeHarp() function we takes care of this and convert the value to match the suitable mode.
                this.MeasurementMode = __iMeasurementMode; // get the Time Harp measurement/acquisition mode

                // Allocate structure for the TTTR header file
                //Files.TTTRFileHeader.TextHeader = new TimeHarpDefinitions.TextHeader();  //the following represents the readable ASCII file header portion (header length is 328 bytes)
                //Files.TTTRFileHeader.BinaryHeader = new TimeHarpDefinitions.BinaryHeader();  //the following is binary header information (header length is 212 bytes)
                //Files.TTTRFileHeader.BoardHeader = new TimeHarpDefinitions.BoardHeader();  //the following is board header information (header length is 48 bytes)
                //Files.TTTRFileHeader.TTTRHeader = new TimeHarpDefinitions.TTTRHeader();  //the following is TTTR header information (header length is 52 bytes)            
                //Files.TTTRFileHeader.BinaryHeader.DisplayCurves = new TimeHarpDefinitions.tCurveMapping[8];
                //Files.TTTRFileHeader.BinaryHeader.Params = new TimeHarpDefinitions.tParamStruct[3];

                m_iGlobalTTTRBufferSize = this.GetGlobalTTTRBufferSize(__iGlobalTTTRBufferSize);  // validate and set the size of the global TTTR buffer in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
                m_iLinePTTTRBufferSize = this.GetLinePTTTRBufferSize(__iLinePTTTRBufferSize);  // validate and set the size of the global TTTR buffer in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)            
            }
        }


        // Initializer - to transfer Time Harp settings to the Time Harp hardware
        /// <summary>
		/// An PQTimeHarp hardware object Initializer that initialize Time Harp counting board.
        /// </summary>
        public void Initialize()
        {
            // Initialize only if it is in a uninitialized state
            if (!this.m_bIsInitialized)
            {
                _logger.Info("Time Harp: starting initialization..."); 

                // Get the Time Harp DLL library version
				string _sLibraryVersion = this.GetLibraryVersion();
                _logger.Info("TimeHarp: THLib version = " + _sLibraryVersion);
                if (_sLibraryVersion != TimeHarpDefinitions.TargetLibVersion)  //check if we match the installed DLL version and the supported in SIS one
                {
                    string _strErrorString = "Time Harp: reporting error --> Time Harp DLL version mismatch: ";
                
                    // Throw an ErrorOccurred event to inform the user.
                    if (this.ErrorOccurred != null)
                    {
                        _logger.Debug(_strErrorString + this.m_sbCurrentError.ToString());
                        this.ErrorOccurred(this, new EventArgs());
                    }                
                }


                // INITIALIZE Time Harp itself with the given measurement mode (0 - histogramming or TTTR mode; 1 - continuous mode).
                // Note that when __iMeasurementMode is used in InitializeTimeHarp() function then 0 - histogramming, and 1 - TTTR mode.
                // So in InitializeTimeHarp() function we takes care of this and convert the value to match the suitable mode.
				this.InitializeTimeHarp(); // initialize Time Harp and set measurement/acquisition mode - IMPORTANT NOTE: We must call this function prior to any other Time Harp function, this function initializes Time Harp itself as well


                // Calibrate Time Harp board - necessary prior to the start of a measurement session
				this.Calibrate();

                // Get the Time Harp hardware version
				string _sHardwareVersion = this.GetHardwareVersion();
                _logger.Info("TimeHarp: found hardware version = " + _sHardwareVersion);

                // Get the Time Harp board serial number
                string _sSerialNumber = this.GetSerialNumber();
				_logger.Info("TimeHarp: serial number S/N = " + _sSerialNumber);

                // Assign and set up the Time Harp CFD level in [mV], (min level = TimeHarpDefinitions.DISCRMIN; max level = TimeHarpDefinitions.DISCRMAX)            
				this.SetCFDDiscrMin(); //set the CFD level of Time Harp

                // Assign and set up the Time Harp CFD zero cross level in [mV], (min level = TimeHarpDefinitions.ZCMIN; max level = TimeHarpDefinitions.ZCMAX)            
				this.SetCFDZeroCross(); //set the CFD zero cross level of Time Harp

                // Assign and set up the Time Harp Sync level in [mV], (min level = TimeHarpDefinitions.SYNCMIN; max level = TimeHarpDefinitions.SYNCMAX)
				this.SetSyncLevel();  //set the Sync level of Time Harp

                // Assign and set up the Time Harp range, (min value = 0; max value = TimeHarpDefinitions.RANGES - 1)
				this.SetRange(); //set the Range (time resolution) of Time Harp

                // Assign and set up the Time Harp offset value in [ns], value is approximation of the desired offset (in steps of 2.5ns); (min value = TimeHarpDefinitions.OFFSETMIN; max value = TimeHarpDefinitions.OFFSETMAX)            
				this.SetOffset(); //set the offset for Time Harp

                //Assign the type of active TTL edge for the external markers (0 - falling edge, 1 - rising edge)
				this.T3RSetMarkerEdges();  //set the active TTL edge (0 - falling edge, 1 - rising edge)

                // Get time resolution of Time Harp board in [ns].
				float _fResolution = this.GetResolution(); //get the time resolution of Time Harp in [ns]
				_logger.Info("TimeHarp: timing resolution = " + _fResolution * 1000 + " [ps]");

                
                // Update the status of Time Hard - set initialize to True if no errors occurred during initialization process            
                if (this.m_iErrorCode >= (int) TimeHarpDefinitions.THError.ERROR_NONE)
                {
                    // The case of no error - then error code is equal or bigger than 0
                    _logger.Debug("Time Harp: engage seems to have worked!");                
                    this.m_bIsInitialized = true;

                    // Allocate buffers
                    ScanStatus.GlobalTTTRBuffer = new UInt32[m_iGlobalTTTRBufferSize * TimeHarpDefinitions.DMABLOCKSZ];   //the global TTTR buffer - a pool for the Time Harp FiFo buffers. The size is multiple of the size of the 1/2 Time Harp FiFo buffer
                    ScanStatus.SizeGlobalTTTRBuffer = 0;  //the current size up to which the buffer is filled with values. Note that it needs to be "volatile" so that every thread gets the most up to date value.

                    ScanStatus.LinePTTTRBuffer = new ProcessedTTTRecord[m_iLinePTTTRBufferSize * TimeHarpDefinitions.DMABLOCKSZ];  //the line Processed TTTR buffer. The size is multiple of the size of the 1/2 Time Harp FiFo buffer
                    ScanStatus.LinePTTTRBufferIndex = 0;  //the index of the last available processed TTTR record in the given buffer (it must not exceed the length of the buffer, if it happens it wraps to the beginning of the buffer)
                    ScanStatus.LinePTTTRBufferTimeTag1 = -1;  //time tag of the line marker 1, it marks the beginning of the buffer
                    ScanStatus.LinePTTTRBufferTimeTag2 = -1;  //time tag of the line marker 2, it marks the end of the buffer 
                    
                    // Get sync rate as well as count rate
					int _iSyncRate = this.GetSyncRate();  //get the number of laser pulses from the SYNC channel
					_logger.Info(String.Format("Time Harp: sync rate = {0} Hz ({1} MHz)", _iSyncRate, ((double)_iSyncRate) / 1e6));

					int _iCountRate = this.CountRate;  //get the count rate in counts/sec
					_logger.Info(String.Format("Time Harp: count rate = {0} Cps ({1} Mcps)", _iCountRate, ((double)_iCountRate) / 1e6));
                }
                else
                {
                    // The case of error - then error code is smaller than 0              
                    this.m_bIsInitialized = false;               

                    // Throw an ErrorOccurred event to inform the user.
                    if (this.ErrorOccurred != null)
                    {
                        _logger.Debug("Time Harp: something went wrong initializing Time Harp!");
                        _logger.Debug("Time Harp: reporting problem --> " + this.m_sbCurrentError.ToString());
                        this.ErrorOccurred(this, new EventArgs());
                    } 
                }

            }

		}


		#endregion PQTimeHarp Class Constructor and Initialize Methods



	}



    
}