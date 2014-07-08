using System;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Threading;

namespace KUL.MDS.Hardware
{
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
            m_serialPort = new SerialPort();
            m_readThread = null;
            m_keepReading = false;
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
				return m_serialPort.IsOpen;
			}
		}

		/// <summary> Get/set serial port read time out</summary>
		public int ReadTimeOut
		{
			get
			{
				return m_SerailPortReadTimeOut;
			}

			set
			{
				if (value > 0)
				{
					m_SerailPortReadTimeOut = value;
				}
				else
				{
					m_SerailPortReadTimeOut = COM_PORT_DEFAULT_READ_TIME_OUT;
				}
			}
		}

		/// <summary> Get/set serial port write time out</summary>
		public int WriteTimeOut
		{
			get
			{
				return m_SerailPortWriteTimeOut;
			}

			set
			{
				if (value > 0)
				{
					m_SerailPortWriteTimeOut = value;
				}
				else
				{
					m_SerailPortWriteTimeOut = COM_PORT_DEFAULT_WRITE_TIME_OUT;
				}
			}
		}

		/// <summary> Start reading of the Serial port in a separate thread. </summary>
        public void StartReading()
        {
            if (!m_keepReading)
            {
                m_keepReading = true;
                m_readThread = new Thread(ReadPort);
                m_readThread.Start();
            }
        }

		/// <summary> Stop reading of the Serial port from the separate thread. </summary>
        public void StopReading()
        {
            if (m_keepReading)
            {
                m_keepReading = false;
                m_readThread.Join();	//block until exits
                m_readThread = null;
            }
        }
        
        /// <summary> Get the data and pass it on. </summary>
        private void ReadPort()
        {
            while (m_keepReading)
            {
                if (m_serialPort.IsOpen)
                {
                    byte[] readBuffer = new byte[m_serialPort.ReadBufferSize + 1];
                    try
                    {
                        // If there are bytes available on the serial port,
                        // Read returns up to "count" bytes, but will not block (wait)
                        // for the remaining bytes. If there are no bytes available
                        // on the serial port, Read will block until at least one byte
                        // is available on the port, up until the ReadTimeout milliseconds
                        // have elapsed, at which time a TimeoutException will be thrown.
                        int count = m_serialPort.Read(readBuffer, 0, m_serialPort.ReadBufferSize);                        
                        String SerialIn = m_serialPort.Encoding.GetString(readBuffer, 0, count);
						
                        DataReceived(SerialIn);
                    }
                    catch (TimeoutException) 
					{
						//StatusChanged("COM port reading time out reached...");
					}
                }
                else
                {
					//TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
					Thread.Sleep(m_SerailPortReadTimeOut);
                }
            }
        }
		
        /// <summary> Get the data directly and pass it on. </summary>
        public string Read(int __iWaitTime)
        {            
            string _strSerialIn = "";

            if (m_serialPort.IsOpen)
            {
                Thread.Sleep(__iWaitTime);

                byte[] _byteReadBuffer = new byte[m_serialPort.ReadBufferSize + 1];
                try
                {
                    // If there are bytes available on the serial port,
                    // Read returns up to "count" bytes, but will not block (wait)
                    // for the remaining bytes. If there are no bytes available
                    // on the serial port, Read will block until at least one byte
                    // is available on the port, up until the ReadTimeout milliseconds
                    // have elapsed, at which time a TimeoutException will be thrown.
                    int _iCount = m_serialPort.Read(_byteReadBuffer, 0, m_serialPort.ReadBufferSize);                    
                    _strSerialIn = m_serialPort.Encoding.GetString(_byteReadBuffer, 0, _iCount);
                }
                catch (TimeoutException)
				{
					StatusChanged("COM port reading time out reached...");
				}
            }

            return _strSerialIn;
        }

        /// <summary> Open the serial port with current settings. </summary>
        public void Open()
        {
            StatusChanged("trying to open COM port...");
            m_serialPort.Close();  // in case the port is already open try to close it first

            try
            {
                m_serialPort.PortName = CommSettings.Port.PortName;
                m_serialPort.BaudRate = CommSettings.Port.BaudRate;
                m_serialPort.Parity = CommSettings.Port.Parity;
                m_serialPort.DataBits = CommSettings.Port.DataBits;
                m_serialPort.StopBits = CommSettings.Port.StopBits;
                m_serialPort.Handshake = CommSettings.Port.Handshake;
                m_serialPort.Encoding = CommSettings.Port.Encoding;

                // Set the read/write timeouts
				m_serialPort.ReadTimeout = m_SerailPortReadTimeOut;
				m_serialPort.WriteTimeout = m_SerailPortWriteTimeOut;

                m_serialPort.Open();
                StartReading();
            }
            catch (IOException)
            {
                StatusChanged(String.Format("{0} does not exist", CommSettings.Port.PortName));
            }
            catch (UnauthorizedAccessException)
            {
                StatusChanged(String.Format("{0} already in use", CommSettings.Port.PortName));
            }
            catch (Exception ex)
            {
                StatusChanged(String.Format("{0}", ex.ToString()));
            }

            // Update the status
            if (m_serialPort.IsOpen)
            {
                string p = m_serialPort.Parity.ToString().Substring(0, 1);   //First char
                string h = m_serialPort.Handshake.ToString();
                if (m_serialPort.Handshake == Handshake.None)
                    h = "no handshake"; // more descriptive than "None"

                StatusChanged(String.Format("{0}: {1} bps, {2}{3}{4}, {5}, {6}",
                    m_serialPort.PortName, m_serialPort.BaudRate,
                    m_serialPort.DataBits, p, (int)m_serialPort.StopBits, h, m_serialPort.Encoding.EncodingName));
            }
            else
            {
                StatusChanged(String.Format("{0} already in use", CommSettings.Port.PortName));
            }
        }

        /// <summary> Close the serial port. </summary>
        public void Close()
        {
            StopReading();
            m_serialPort.Close();
            StatusChanged("connection closed");
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
            if (IsOpen)
            {
                string lineEnding = GetLineEnding();
                m_serialPort.Write(data + lineEnding);                
            }
        }


        /// <summary>Send directly (without appending of a line ending) data to the serial port. </summary>
        /// <param name="data">A string containing the data to send. </param>
        public void Write(string data)
        {
            if (IsOpen)
            {
                m_serialPort.Write(data);
            }
        }


        /// <summary> Discards data from the serial driver's receive buffer.. </summary>
        public void DiscardInBufferData()
        {
            if (IsOpen)
            {
                m_serialPort.DiscardInBuffer();
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