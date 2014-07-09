/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

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
    public sealed class NullGraphics
        : IDisposable
    {
        private IntPtr hdc = IntPtr.Zero;
        private Graphics graphics = null;
        private bool disposed = false;

        public Graphics Graphics
        {
            get
            {
                return this.graphics;
            }
        }

        public NullGraphics()
        {
            this.hdc = SafeNativeMethods.CreateCompatibleDC(IntPtr.Zero);

            if (this.hdc == IntPtr.Zero)
            {
                NativeMethods.ThrowOnWin32Error("CreateCompatibleDC returned NULL");
            }

            this.graphics = Graphics.FromHdc(this.hdc);
        }

        ~NullGraphics()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

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
    }
}
