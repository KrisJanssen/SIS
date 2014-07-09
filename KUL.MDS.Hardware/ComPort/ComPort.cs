namespace SIS.Hardware.ComPort
{
    using System;
    using System.IO;
    using System.IO.Ports;
    using System.Threading;

    /// <summary> CommPort class creates a singleton instance
    /// of SerialPort (System.IO.Ports) </summary>
    /// <remarks> When ready, you open the port.
    ///   <code>
    ///   CommPort com = CommPort.Instance;
    ///   com.StatusChanged += OnStatusChanged;
    ///   com.DataReceived += OnDataReceived;
    ///   com.Open();
    ///   </code>
    ///   Notice that delegates are used to handle status and data events.
    ///   When settings are changed, you close and reopen the port.
    ///   <code>
    ///   CommPort com = CommPort.Instance;
    ///   com.Close();
    ///   com.PortName = "COM4";
    ///   com.Open();
    ///   </code>
    /// </remarks>
    public sealed class CommPort
    {
		private const int COM_PORT_DEFAULT_READ_TIME_OUT = 50;  // default CommPort port read time out
		private const int COM_PORT_DEFAULT_WRITE_TIME_OUT = 50;  // default CommPort port write time out

        SerialPort m_serialPort;
        Thread m_readThread;
        volatile bool m_keepReading;
		int m_SerailPortReadTimeOut = COM_PORT_DEFAULT_READ_TIME_OUT;  // read time out in [ms]
		int m_SerailPortWriteTimeOut = COM_PORT_DEFAULT_WRITE_TIME_OUT;  // write time out in [ms]

        //begin Singleton pattern
        static readonly CommPort instance = new CommPort();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static CommPort()
        {
        }

        CommPort()
        {
            this.m_serialPort = new SerialPort();
            this.m_readThread = null;
            this.m_keepReading = false;
        }

        public static CommPort Instance
        {
            get
            {
                return instance;
            }
        }
        //end Singleton pattern

        //begin Observer pattern
        public delegate void EventHandler(string param);
        public EventHandler StatusChanged;
        public EventHandler DataReceived;
        //end Observer pattern

		/// <summary> Get the status of the serial port. </summary>
		public bool IsOpen
		{
			get
			{
				return this.m_serialPort.IsOpen;
			}
		}

		/// <summary> Get/set serial port read time out</summary>
		public int ReadTimeOut
		{
			get
			{
				return this.m_SerailPortReadTimeOut;
			}

			set
			{
				if (value > 0)
				{
					this.m_SerailPortReadTimeOut = value;
				}
				else
				{
					this.m_SerailPortReadTimeOut = COM_PORT_DEFAULT_READ_TIME_OUT;
				}
			}
		}

		/// <summary> Get/set serial port write time out</summary>
		public int WriteTimeOut
		{
			get
			{
				return this.m_SerailPortWriteTimeOut;
			}

			set
			{
				if (value > 0)
				{
					this.m_SerailPortWriteTimeOut = value;
				}
				else
				{
					this.m_SerailPortWriteTimeOut = COM_PORT_DEFAULT_WRITE_TIME_OUT;
				}
			}
		}

		/// <summary> Start reading of the Serial port in a separate thread. </summary>
        public void StartReading()
        {
            if (!this.m_keepReading)
            {
                this.m_keepReading = true;
                this.m_readThread = new Thread(this.ReadPort);
                this.m_readThread.Start();
            }
        }

		/// <summary> Stop reading of the Serial port from the separate thread. </summary>
        public void StopReading()
        {
            if (this.m_keepReading)
            {
                this.m_keepReading = false;
                this.m_readThread.Join();	//block until exits
                this.m_readThread = null;
            }
        }
        
        /// <summary> Get the data and pass it on. </summary>
        private void ReadPort()
        {
            while (this.m_keepReading)
            {
                if (this.m_serialPort.IsOpen)
                {
                    byte[] readBuffer = new byte[this.m_serialPort.ReadBufferSize + 1];
                    try
                    {
                        // If there are bytes available on the serial port,
                        // Read returns up to "count" bytes, but will not block (wait)
                        // for the remaining bytes. If there are no bytes available
                        // on the serial port, Read will block until at least one byte
                        // is available on the port, up until the ReadTimeout milliseconds
                        // have elapsed, at which time a TimeoutException will be thrown.
                        int count = this.m_serialPort.Read(readBuffer, 0, this.m_serialPort.ReadBufferSize);                        
                        String SerialIn = this.m_serialPort.Encoding.GetString(readBuffer, 0, count);
						
                        this.DataReceived(SerialIn);
                    }
                    catch (TimeoutException) 
					{
						//StatusChanged("COM port reading time out reached...");
					}
                }
                else
                {
					//TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
					Thread.Sleep(this.m_SerailPortReadTimeOut);
                }
            }
        }
		
        /// <summary> Get the data directly and pass it on. </summary>
        public string Read(int __iWaitTime)
        {            
            string _strSerialIn = "";

            if (this.m_serialPort.IsOpen)
            {
                Thread.Sleep(__iWaitTime);

                byte[] _byteReadBuffer = new byte[this.m_serialPort.ReadBufferSize + 1];
                try
                {
                    // If there are bytes available on the serial port,
                    // Read returns up to "count" bytes, but will not block (wait)
                    // for the remaining bytes. If there are no bytes available
                    // on the serial port, Read will block until at least one byte
                    // is available on the port, up until the ReadTimeout milliseconds
                    // have elapsed, at which time a TimeoutException will be thrown.
                    int _iCount = this.m_serialPort.Read(_byteReadBuffer, 0, this.m_serialPort.ReadBufferSize);                    
                    _strSerialIn = this.m_serialPort.Encoding.GetString(_byteReadBuffer, 0, _iCount);
                }
                catch (TimeoutException)
				{
					this.StatusChanged("COM port reading time out reached...");
				}
            }

            return _strSerialIn;
        }

        /// <summary> Open the serial port with current settings. </summary>
        public void Open()
        {
            this.StatusChanged("trying to open COM port...");
            this.m_serialPort.Close();  // in case the port is already open try to close it first

            try
            {
                this.m_serialPort.PortName = CommSettings.Port.PortName;
                this.m_serialPort.BaudRate = CommSettings.Port.BaudRate;
                this.m_serialPort.Parity = CommSettings.Port.Parity;
                this.m_serialPort.DataBits = CommSettings.Port.DataBits;
                this.m_serialPort.StopBits = CommSettings.Port.StopBits;
                this.m_serialPort.Handshake = CommSettings.Port.Handshake;
                this.m_serialPort.Encoding = CommSettings.Port.Encoding;

                // Set the read/write timeouts
				this.m_serialPort.ReadTimeout = this.m_SerailPortReadTimeOut;
				this.m_serialPort.WriteTimeout = this.m_SerailPortWriteTimeOut;

                this.m_serialPort.Open();
                this.StartReading();
            }
            catch (IOException)
            {
                this.StatusChanged(String.Format("{0} does not exist", CommSettings.Port.PortName));
            }
            catch (UnauthorizedAccessException)
            {
                this.StatusChanged(String.Format("{0} already in use", CommSettings.Port.PortName));
            }
            catch (Exception ex)
            {
                this.StatusChanged(String.Format("{0}", ex.ToString()));
            }

            // Update the status
            if (this.m_serialPort.IsOpen)
            {
                string p = this.m_serialPort.Parity.ToString().Substring(0, 1);   //First char
                string h = this.m_serialPort.Handshake.ToString();
                if (this.m_serialPort.Handshake == Handshake.None)
                    h = "no handshake"; // more descriptive than "None"

                this.StatusChanged(String.Format("{0}: {1} bps, {2}{3}{4}, {5}, {6}",
                    this.m_serialPort.PortName, this.m_serialPort.BaudRate,
                    this.m_serialPort.DataBits, p, (int)this.m_serialPort.StopBits, h, this.m_serialPort.Encoding.EncodingName));
            }
            else
            {
                this.StatusChanged(String.Format("{0} already in use", CommSettings.Port.PortName));
            }
        }

        /// <summary> Close the serial port. </summary>
        public void Close()
        {
            this.StopReading();
            this.m_serialPort.Close();
            this.StatusChanged("connection closed");
        }        

        /// <summary> Get a list of the available ports. Already opened ports
        /// are not returned. </summary>
        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>Send data to the serial port after appending line ending. </summary>
        /// <param name="data">A string containing the data to send - note it appends a predefined line ending. </param>
        public void Send(string data)
        {
            if (this.IsOpen)
            {
                string lineEnding = this.GetLineEnding();
                this.m_serialPort.Write(data + lineEnding);                
            }
        }


        /// <summary>Send directly (without appending of a line ending) data to the serial port. </summary>
        /// <param name="data">A string containing the data to send. </param>
        public void Write(string data)
        {
            if (this.IsOpen)
            {
                this.m_serialPort.Write(data);
            }
        }


        /// <summary> Discards data from the serial driver's receive buffer.. </summary>
        public void DiscardInBufferData()
        {
            if (this.IsOpen)
            {
                this.m_serialPort.DiscardInBuffer();
            }                        
        }


        /// <summary>
        /// Get the type of line ending (from the COM setting) that must be appended to a string. 
        /// </summary>        
        /// <returns>A string that represents the type of line ending.</returns>
        public string GetLineEnding()
        {            
            string lineEnding = "";

            switch (CommSettings.Option.AppendToSend)
            {
                case CommSettings.Option.AppendType.AppendNothing:
                    lineEnding = ""; break;
                case CommSettings.Option.AppendType.AppendCR:
                    lineEnding = "\r"; break;
                case CommSettings.Option.AppendType.AppendLF:
                    lineEnding = "\n"; break;
                case CommSettings.Option.AppendType.AppendCRLF:
                    lineEnding = "\r\n"; break;
                case CommSettings.Option.AppendType.AppendSC:
                    lineEnding = ";"; break;                
            }

            return lineEnding;            
        }
    }
}