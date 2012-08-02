///////////////////////////////////////////////////////////////////////////////
// CapDevice v1.1
//
// This software is released into the public domain.  You are free to use it
// in any way you like, except that you may not sell this source code.
//
// This software is provided "as is" with no expressed or implied warranty.
// I accept no liability for any damage or loss of business that this software
// may cause.
// 
// This source code is originally written by Tamir Khason (see http://blogs.microsoft.co.il/blogs/tamir
// or http://www.codeplex.com/wpfcap).
// 
// Modifications are made by Geert van Horrik (CatenaLogic, see http://blog.catenalogic.com) 
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.ComponentModel;

namespace KUL.MDS.WPFControls.CCDControl.Device
{
    public class CCDDevice : DependencyObject, IDisposable
    {
        #region Win32
        static readonly Guid FilterGraph = new Guid(0xE436EBB3, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

        static readonly Guid SampleGrabber = new Guid(0xC1F400A0, 0x3F08, 0x11D3, 0x9F, 0x0B, 0x00, 0x60, 0x08, 0x03, 0x9E, 0x37);

        public static readonly Guid SystemDeviceEnum = new Guid(0x62BE5D10, 0x60EB, 0x11D0, 0xBD, 0x3B, 0x00, 0xA0, 0xC9, 0x11, 0xCE, 0x86);

        public static readonly Guid VideoInputDevice = new Guid(0x860BB310, 0x5D01, 0x11D0, 0xBD, 0x3B, 0x00, 0xA0, 0xC9, 0x11, 0xCE, 0x86);

        [ComVisible(false)]
        internal class MediaTypes
        {
            public static readonly Guid Video = new Guid(0x73646976, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid Interleaved = new Guid(0x73766169, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid Audio = new Guid(0x73647561, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid Text = new Guid(0x73747874, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid Stream = new Guid(0xE436EB83, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);
        }

        [ComVisible(false)]
        internal class MediaSubTypes
        {
            public static readonly Guid YUYV = new Guid(0x56595559, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid IYUV = new Guid(0x56555949, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid DVSD = new Guid(0x44535644, 0x0000, 0x0010, 0x80, 0x00, 0x00, 0xAA, 0x00, 0x38, 0x9B, 0x71);

            public static readonly Guid RGB1 = new Guid(0xE436EB78, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB4 = new Guid(0xE436EB79, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB8 = new Guid(0xE436EB7A, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB565 = new Guid(0xE436EB7B, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB555 = new Guid(0xE436EB7C, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB24 = new Guid(0xE436Eb7D, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid RGB32 = new Guid(0xE436EB7E, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid Avi = new Guid(0xE436EB88, 0x524F, 0x11CE, 0x9F, 0x53, 0x00, 0x20, 0xAF, 0x0B, 0xA7, 0x70);

            public static readonly Guid Asf = new Guid(0x3DB80F90, 0x9412, 0x11D1, 0xAD, 0xED, 0x00, 0x00, 0xF8, 0x75, 0x4B, 0x99);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);
        #endregion

        #region Variables

        private ManualResetEvent m_rstevStopSignal = null;
        private Thread m_thrdWorker = null;
        private IGraphBuilder m_igrphbldGraph = null;
        private ISampleGrabber m_isplGrabber = null;
        private IBaseFilter m_sourceObject = null;
        private IBaseFilter m_grabberObject = null;
        private IMediaControl m_imedctrlControl = null;
        private CCDGrabber m_grbrCapGrabber = null;
        private IntPtr m_iptrMap = IntPtr.Zero;
        private IntPtr m_iptrSection = IntPtr.Zero;

        private System.Diagnostics.Stopwatch m_stpwtchTimer = System.Diagnostics.Stopwatch.StartNew();
        private double m_dFrames = 0.0;
        private string m_sMonikerString = "";

        #endregion

        #region Constructor & destructor

        /// <summary>
        /// Initializes the default capture device
        /// </summary>
        public CCDDevice()
            : this("")
        { }

        /// <summary>
        /// Initializes a specific capture device
        /// </summary>
        /// <param name="moniker">Moniker string that represents a specific device</param>
        public CCDDevice(string _sMoniker)
        {
            // Store moniker string
            this.MonikerString = _sMoniker;
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            // Stop
            this.Stop();
        }

        #endregion

        #region Events

        /// <summary>
        /// Event that is invoked when a new bitmap is ready
        /// </summary>
        public event EventHandler NewBitmapReady;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the device monikers
        /// </summary>
        public static FilterInfo[] DeviceMonikers
        {
            get
            {
                List<FilterInfo> filters = new List<FilterInfo>();
                IMoniker[] ms = new IMoniker[1];
                ICreateDevEnum enumD = Activator.CreateInstance(Type.GetTypeFromCLSID(SystemDeviceEnum)) as ICreateDevEnum;
                IEnumMoniker moniker;
                Guid g = VideoInputDevice;
                if (enumD.CreateClassEnumerator(ref g, out moniker, 0) == 0)
                {
                    while (true)
                    {
                        int r = moniker.Next(1, ms, IntPtr.Zero);
                        if (r != 0 || ms[0] == null)
                            break;
                        filters.Add(new FilterInfo(ms[0]));
                        Marshal.ReleaseComObject(ms[0]);
                        ms[0] = null;
                    }
                }

                return filters.ToArray();
            }
        }

        /// <summary>
        /// Gets the available devices
        /// </summary>
        public static CCDDevice[] Devices
        {
            get
            {
                // Declare variables
                List<CCDDevice> devices = new List<CCDDevice>();

                // Loop all monikers
                foreach (FilterInfo moniker in DeviceMonikers)
                {
                    devices.Add(new CCDDevice(moniker.MonikerString));
                }

                // Return result
                return devices.ToArray();
            }
        }

        /// <summary>
        /// Wrapper for the BitmapSource dependency property
        /// </summary>
        public InteropBitmap BitmapSource
        {
            get { return (InteropBitmap)GetValue(BitmapSourceProperty); }
            private set { SetValue(BitmapSourcePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey BitmapSourcePropertyKey =
            DependencyProperty.RegisterReadOnly("BitmapSource", typeof(InteropBitmap), typeof(CCDDevice), new UIPropertyMetadata(default(InteropBitmap)));

        public static readonly DependencyProperty BitmapSourceProperty = BitmapSourcePropertyKey.DependencyProperty;

        /// <summary>
        /// Wrapper for the Name dependency property
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(CCDDevice), new UIPropertyMetadata(""));

        /// <summary>
        /// Wrapper for the MonikerString dependency property
        /// </summary>
        public string MonikerString
        {
            get { return (string)GetValue(MonikerStringProperty); }
            set { SetValue(MonikerStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MonikerString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MonikerStringProperty =
            DependencyProperty.Register("MonikerString", typeof(string), typeof(CCDDevice), new UIPropertyMetadata("", new PropertyChangedCallback(MonikerString_Changed)));

        /// <summary>
        /// Wrapper for the Framerate dependency property
        /// </summary>
        public float Framerate
        {
            get { return (float)GetValue(FramerateProperty); }
            set { SetValue(FramerateProperty, value); }
        }

        public static readonly DependencyProperty FramerateProperty =
            DependencyProperty.Register("Framerate", typeof(float), typeof(CCDDevice), new UIPropertyMetadata(default(float)));

        /// <summary>
        /// Gets whether the capture device is currently running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                // Check if we have a worker thread
                if (m_thrdWorker == null) return false;

                return m_thrdWorker.IsAlive;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when a new frame arrived
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void CCDGrabber_NewFrameArrived(object sender, EventArgs e)
        {
            // Make sure to be thread safe
            if (Dispatcher != null)
            {
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, (SendOrPostCallback)delegate
                {
                    if (BitmapSource != null)
                    {
                        BitmapSource.Invalidate();
                        UpdateFramerate();
                    }
                }, null);
            }
        }

        /// <summary>
        /// Invoked when the MonikerString dependency property has changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private static void MonikerString_Changed(DependencyObject __depoSender, DependencyPropertyChangedEventArgs __deppropcheaE)
        {
            // Get typed sender
            CCDDevice _CCDSender = __depoSender as CCDDevice;

            if (_CCDSender != null)
            {
                // Always stop the device
                _CCDSender.Stop();

                // Get the new value
                string _sNewMonikerString = __deppropcheaE.NewValue as string;

                // Initialize device
                _CCDSender.InitializeDeviceForMoniker(_sNewMonikerString);

                // Check if we have a valid moniker string
                if (!string.IsNullOrEmpty(_sNewMonikerString))
                {
                    // Initialize device
                    _CCDSender.InitializeDeviceForMoniker(_sNewMonikerString);

                    // Start
                    _CCDSender.Start();
                }
            }
        }

        /// <summary>
        /// Invoked when a property of the CapGrabber object has changed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        private void CCDGrabber_PropertyChanged(object __oSender, PropertyChangedEventArgs __propcheaE)
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.DataBind, (SendOrPostCallback)delegate
            {
                try
                {
                    if ((m_grbrCapGrabber.Width != default(int)) && (m_grbrCapGrabber.Height != default(int)))
                    {
                        // Get the pixel count
                        uint pcount = (uint)(m_grbrCapGrabber.Width * m_grbrCapGrabber.Height * PixelFormats.Bgr32.BitsPerPixel / 8);

                        // Create a file mapping
                        m_iptrSection = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, pcount, null);
                        m_iptrMap = MapViewOfFile(m_iptrSection, 0xF001F, 0, 0, pcount);

                        // Get the bitmap
                        BitmapSource = (InteropBitmap)Imaging.CreateBitmapSourceFromMemorySection(
                            m_iptrSection, 
                            m_grbrCapGrabber.Width,
                            m_grbrCapGrabber.Height, 
                            PixelFormats.Bgr32, 
                            m_grbrCapGrabber.Width * PixelFormats.Bgr32.BitsPerPixel / 8, 0);

                        m_grbrCapGrabber.Map = m_iptrMap;

                        // Invoke event
                        if (NewBitmapReady != null)
                        {
                            NewBitmapReady(this, null);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trace
                    Trace.TraceError(ex.Message);
                }
            }, null);
        }

        /// <summary>
        /// Updates the framerate
        /// </summary>
        private void UpdateFramerate()
        {
            // Increase the frames
            m_dFrames++;

            // Check the timer
            if (m_stpwtchTimer.ElapsedMilliseconds >= 1000)
            {
                // Set the framerate
                Framerate = (float)Math.Round(m_dFrames * 1000 / m_stpwtchTimer.ElapsedMilliseconds);

                // Reset the timer again so we can count the framerate again
                m_stpwtchTimer.Reset();
                m_stpwtchTimer.Start();
                m_dFrames = 0;
            }
        }

        /// <summary>
        /// Initialize the device for a specific moniker
        /// </summary>
        /// <param name="moniker">Moniker to initialize the device for</param>
        private void InitializeDeviceForMoniker(string __sMoniker)
        {
            // Store moniker (since dependency properties are not thread-safe, store it locally as well)
            m_sMonikerString = __sMoniker;

            // Find the name
            foreach (FilterInfo filterInfo in DeviceMonikers)
            {
                if (filterInfo.MonikerString == __sMoniker)
                {
                    Name = filterInfo.Name;
                    break;
                }
            }
        }

        /// <summary>;
        /// Starts grabbing images from the capture device
        /// </summary>
        public void Start()
        {
            // First check if we have a valid moniker string
            if (string.IsNullOrEmpty(m_sMonikerString)) return;

            // Check if we are already running
            if (this.IsRunning)
            {
                // Yes, stop it first
                this.Stop();
            }

            // Create new grabber
            m_grbrCapGrabber = new CCDGrabber();
            m_grbrCapGrabber.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(CCDGrabber_PropertyChanged);
            m_grbrCapGrabber.NewFrameArrived += new EventHandler(CCDGrabber_NewFrameArrived);

            // Create manual reset event
            m_rstevStopSignal = new ManualResetEvent(false);

            // Start the thread
            m_thrdWorker = new Thread(RunWorker);
            m_thrdWorker.Start();
        }

        /// <summary>
        /// Stops grabbing images from the capture device
        /// </summary>
        public void Stop()
        {
            try
            {
                // Check if the capture device is even running
                if (this.IsRunning)
                {
                    // Abort the thread
                    this.Release();
                }
            }
            catch (Exception ex)
            {
                // Trace
                Trace.TraceError(ex.Message);

                // Release
                this.Release();
            }
        }

        /// <summary>
        /// Releases the capture device
        /// </summary>
        private void Release()
        {
            if (m_thrdWorker != null)
            {
                // Yes, stop via the event
                m_rstevStopSignal.Set();

                Thread.Sleep(100);

                // Join the worker thread so we only continue when it exits.
                //_worker.Join();

                // Stop the thread.
                m_thrdWorker.Abort();

                // Dispose of the thread.
                m_thrdWorker = null;
            }

            // Clear the event
            if (m_rstevStopSignal != null)
            {
                m_rstevStopSignal.Close();
                m_rstevStopSignal = null;
            }

            // Clean up
            m_igrphbldGraph = null;
            m_sourceObject = null;
            m_grabberObject = null;
            m_isplGrabber = null;
            m_grbrCapGrabber = null;
            m_imedctrlControl = null;
        }

        /// <summary>
        /// Worker thread that captures the images
        /// </summary>
        private void RunWorker()
        {
            try
            {
                // Create the main graph
                m_igrphbldGraph = Activator.CreateInstance(Type.GetTypeFromCLSID(FilterGraph)) as IGraphBuilder;

                // Create the webcam source
                m_sourceObject = FilterInfo.CreateFilter(m_sMonikerString);

                // Create the grabber
                m_isplGrabber = Activator.CreateInstance(Type.GetTypeFromCLSID(SampleGrabber)) as ISampleGrabber;
                m_grabberObject = m_isplGrabber as IBaseFilter;

                // Add the source and grabber to the main graph
                m_igrphbldGraph.AddFilter(m_sourceObject, "source");
                m_igrphbldGraph.AddFilter(m_grabberObject, "grabber");

                using (AMMediaType mediaType = new AMMediaType())
                {
                    mediaType.MajorType = MediaTypes.Video;
                    mediaType.SubType = MediaSubTypes.RGB32;
                    m_isplGrabber.SetMediaType(mediaType);

                    if (m_igrphbldGraph.Connect(m_sourceObject.GetPin(PinDirection.Output, 0), m_grabberObject.GetPin(PinDirection.Input, 0)) >= 0)
                    {
                        if (m_isplGrabber.GetConnectedMediaType(mediaType) == 0)
                        {
                            // During startup, this code can be too fast, so try at least 3 times
                            int retryCount = 0;
                            bool succeeded = false;
                            while ((retryCount < 3) && !succeeded)
                            {
                                // Tried again
                                retryCount++;

                                try
                                {
                                    // Retrieve the grabber information
                                    VideoInfoHeader header = (VideoInfoHeader)Marshal.PtrToStructure(mediaType.FormatPtr, typeof(VideoInfoHeader));
                                    m_grbrCapGrabber.Width = header.BmiHeader.Width;
                                    m_grbrCapGrabber.Height = header.BmiHeader.Height;

                                    // Succeeded
                                    succeeded = true;
                                }
                                catch (Exception retryException)
                                {
                                    // Trace
                                    Trace.TraceInformation("Failed to retrieve the grabber information, tried {0} time(s)", retryCount);

                                    // Sleep
                                    Thread.Sleep(50);
                                }
                            }
                        }
                    }
                    m_igrphbldGraph.Render(m_grabberObject.GetPin(PinDirection.Output, 0));
                    m_isplGrabber.SetBufferSamples(false);
                    m_isplGrabber.SetOneShot(false);
                    m_isplGrabber.SetCallback(m_grbrCapGrabber, 1);

                    // Get the video window
                    IVideoWindow wnd = (IVideoWindow)m_igrphbldGraph;
                    wnd.put_AutoShow(false);
                    wnd = null;

                    // Create the control and run
                    m_imedctrlControl = (IMediaControl)m_igrphbldGraph;
                    m_imedctrlControl.Run();

                    // Wait for the stop signal
                    while (!m_rstevStopSignal.WaitOne(0, true))
                    {
                        Thread.Sleep(10);
                    }

                    // Stop when ready
                    // _control.StopWhenReady();
                    m_imedctrlControl.Stop();

                    // Wait a bit... It apparently takes some time to stop IMediaControl
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                // Trace
                Trace.WriteLine(ex);
            }
            finally
            {
                // Clean up
                this.Release();
            }
        }

        #endregion
    }
}
