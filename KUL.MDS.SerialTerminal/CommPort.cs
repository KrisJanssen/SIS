// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommPort.cs" company="">
//   
// </copyright>
// <summary>
//   CommPort class creates a singleton instance
//   of SerialPort (System.IO.Ports)
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.SerialTerminal
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
        // begin Singleton pattern
        #region Static Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly CommPort instance = new CommPort();

        #endregion

        #region Fields

        /// <summary>
        /// The data received.
        /// </summary>
        public EventHandler DataReceived;

        /// <summary>
        /// The status changed.
        /// </summary>
        public EventHandler StatusChanged;

        /// <summary>
        /// The _keep reading.
        /// </summary>
        private bool _keepReading;

        /// <summary>
        /// The _read thread.
        /// </summary>
        private Thread _readThread;

        /// <summary>
        /// The _serial port.
        /// </summary>
        private SerialPort _serialPort;

        #endregion

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="CommPort"/> class.
        /// </summary>
        static CommPort()
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CommPort"/> class from being created.
        /// </summary>
        private CommPort()
        {
            this._serialPort = new SerialPort();
            this._readThread = null;
            this._keepReading = false;
        }

        #endregion

        // end Singleton pattern

        // begin Observer pattern
        #region Delegates

        /// <summary>
        /// The event handler.
        /// </summary>
        /// <param name="param">
        /// The param.
        /// </param>
        public delegate void EventHandler(string param);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static CommPort Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary> Get the status of the serial port. </summary>
        public bool IsOpen
        {
            get
            {
                return this._serialPort.IsOpen;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary> Close the serial port. </summary>
        public void Close()
        {
            this.StopReading();
            this._serialPort.Close();
            this.StatusChanged("connection closed");
        }

        /// <summary>
        /// Get a list of the available ports. Already opened ports
        /// are not returend. 
        /// </summary>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public string[] GetAvailablePorts()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary> Open the serial port with current settings. </summary>
        public void Open()
        {
            this.Close();

            try
            {
                this._serialPort.PortName = Settings.Port.PortName;
                this._serialPort.BaudRate = Settings.Port.BaudRate;
                this._serialPort.Parity = Settings.Port.Parity;
                this._serialPort.DataBits = Settings.Port.DataBits;
                this._serialPort.StopBits = Settings.Port.StopBits;
                this._serialPort.Handshake = Settings.Port.Handshake;

                // Set the read/write timeouts
                this._serialPort.ReadTimeout = 50;
                this._serialPort.WriteTimeout = 50;

                this._serialPort.Open();
                this.StartReading();
            }
            catch (IOException)
            {
                this.StatusChanged(string.Format("{0} does not exist", Settings.Port.PortName));
            }
            catch (UnauthorizedAccessException)
            {
                this.StatusChanged(string.Format("{0} already in use", Settings.Port.PortName));
            }

            // Update the status
            if (this._serialPort.IsOpen)
            {
                string p = this._serialPort.Parity.ToString().Substring(0, 1); // First char
                string h = this._serialPort.Handshake.ToString();
                if (this._serialPort.Handshake == Handshake.None)
                {
                    h = "no handshake"; // more descriptive than "None"
                }

                this.StatusChanged(
                    string.Format(
                        "{0}: {1} bps, {2}{3}{4}, {5}", 
                        this._serialPort.PortName, 
                        this._serialPort.BaudRate, 
                        this._serialPort.DataBits, 
                        p, 
                        (int)this._serialPort.StopBits, 
                        h));
            }
            else
            {
                this.StatusChanged(string.Format("{0} already in use", Settings.Port.PortName));
            }
        }

        /// <summary>
        /// Send data to the serial port after appending line ending. 
        /// </summary>
        /// <param name="data">
        /// An string containing the data to send. 
        /// </param>
        public void Send(string data)
        {
            if (this.IsOpen)
            {
                string lineEnding = string.Empty;
                switch (Settings.Option.AppendToSend)
                {
                    case Settings.Option.AppendType.AppendCR:
                        lineEnding = "\r";
                        break;
                    case Settings.Option.AppendType.AppendLF:
                        lineEnding = "\n";
                        break;
                    case Settings.Option.AppendType.AppendCRLF:
                        lineEnding = "\r\n";
                        break;
                }

                this._serialPort.Write(data + lineEnding);
            }
        }

        #endregion

        #region Methods

        /// <summary> Get the data and pass it on. </summary>
        private void ReadPort()
        {
            while (this._keepReading)
            {
                if (this._serialPort.IsOpen)
                {
                    byte[] readBuffer = new byte[this._serialPort.ReadBufferSize + 1];
                    try
                    {
                        // If there are bytes available on the serial port,
                        // Read returns up to "count" bytes, but will not block (wait)
                        // for the remaining bytes. If there are no bytes available
                        // on the serial port, Read will block until at least one byte
                        // is available on the port, up until the ReadTimeout milliseconds
                        // have elapsed, at which time a TimeoutException will be thrown.
                        int count = this._serialPort.Read(readBuffer, 0, this._serialPort.ReadBufferSize);
                        string SerialIn = System.Text.Encoding.ASCII.GetString(readBuffer, 0, count);
                        this.DataReceived(SerialIn);
                    }
                    catch (TimeoutException)
                    {
                    }
                }
                else
                {
                    TimeSpan waitTime = new TimeSpan(0, 0, 0, 0, 50);
                    Thread.Sleep(waitTime);
                }
            }
        }

        /// <summary>
        /// The start reading.
        /// </summary>
        private void StartReading()
        {
            if (!this._keepReading)
            {
                this._keepReading = true;
                this._readThread = new Thread(this.ReadPort);
                this._readThread.Start();
            }
        }

        /// <summary>
        /// The stop reading.
        /// </summary>
        private void StopReading()
        {
            if (this._keepReading)
            {
                this._keepReading = false;
                this._readThread.Join(); // block until exits
                this._readThread = null;
            }
        }

        #endregion
    }
}