// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="CCDGrabber.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The ccd grabber.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.WPFControls.CCDControl
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The ccd grabber.
    /// </summary>
    internal class CCDGrabber : ISampleGrabberCB, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The _height.
        /// </summary>
        private int _height = default(int);

        /// <summary>
        /// The _width.
        /// </summary>
        private int _width = default(int);

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CCDGrabber"/> class.
        /// </summary>
        public CCDGrabber()
        {
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The new frame arrived.
        /// </summary>
        public event EventHandler NewFrameArrived;

        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the height of the grabber
        /// </summary>
        public int Height
        {
            get
            {
                return this._height;
            }

            set
            {
                this._height = value;
                this.OnPropertyChanged("Height");
            }
        }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        public IntPtr Map { get; set; }

        /// <summary>
        /// Gets or sets the width of the grabber
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }

            set
            {
                this._width = value;
                this.OnPropertyChanged("Width");
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The buffer cb.
        /// </summary>
        /// <param name="sampleTime">
        /// The sample time.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="bufferLen">
        /// The buffer len.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int BufferCB(double sampleTime, IntPtr buffer, int bufferLen)
        {
            if (this.Map != IntPtr.Zero)
            {
                CopyMemory(this.Map, buffer, bufferLen);
                this.OnNewFrameArrived();
            }

            return 0;
        }

        /// <summary>
        /// The sample cb.
        /// </summary>
        /// <param name="sampleTime">
        /// The sample time.
        /// </param>
        /// <param name="sample">
        /// The sample.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int SampleCB(double sampleTime, IntPtr sample)
        {
            return 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The copy memory.
        /// </summary>
        /// <param name="Destination">
        /// The destination.
        /// </param>
        /// <param name="Source">
        /// The source.
        /// </param>
        /// <param name="Length">
        /// The length.
        /// </param>
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        /// <summary>
        /// The on new frame arrived.
        /// </summary>
        private void OnNewFrameArrived()
        {
            if (this.NewFrameArrived != null)
            {
                this.NewFrameArrived(this, null);
            }
        }

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }
}