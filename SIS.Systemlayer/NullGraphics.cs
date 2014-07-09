// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullGraphics.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Sometimes you need a Graphics instance when you don't really have access to one.
//   Example situations include retrieving the bounds or scanlines of a Region.
//   So use this to create a 'null' Graphics instance that effectively eats all
//   rendering calls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Sometimes you need a Graphics instance when you don't really have access to one.
    /// Example situations include retrieving the bounds or scanlines of a Region.
    /// So use this to create a 'null' Graphics instance that effectively eats all
    /// rendering calls.
    /// </summary>
    public sealed class NullGraphics : IDisposable
    {
        #region Fields

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// The graphics.
        /// </summary>
        private Graphics graphics = null;

        /// <summary>
        /// The hdc.
        /// </summary>
        private IntPtr hdc = IntPtr.Zero;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NullGraphics"/> class.
        /// </summary>
        public NullGraphics()
        {
            this.hdc = SafeNativeMethods.CreateCompatibleDC(IntPtr.Zero);

            if (this.hdc == IntPtr.Zero)
            {
                NativeMethods.ThrowOnWin32Error("CreateCompatibleDC returned NULL");
            }

            this.graphics = Graphics.FromHdc(this.hdc);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="NullGraphics"/> class. 
        /// </summary>
        ~NullGraphics()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.graphics.Dispose();
                    this.graphics = null;
                }

                SafeNativeMethods.DeleteDC(this.hdc);
                this.disposed = true;
            }
        }

        #endregion
    }
}