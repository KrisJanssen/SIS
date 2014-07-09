// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FastBitmap.cs" company="">
//   
// </copyright>
// <summary>
//   The fast bitmap.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Library
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    /// <summary>
    /// The fast bitmap.
    /// </summary>
    public unsafe class FastBitmap
    {
        #region Fields

        /// <summary>
        /// The subject.
        /// </summary>
        private Bitmap Subject;

        /// <summary>
        /// The subject width.
        /// </summary>
        private int SubjectWidth;

        /// <summary>
        /// The bitmap data.
        /// </summary>
        private BitmapData bitmapData = null;

        /// <summary>
        /// The p base.
        /// </summary>
        private byte* pBase = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FastBitmap"/> class.
        /// </summary>
        /// <param name="SubjectBitmap">
        /// The subject bitmap.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public FastBitmap(Bitmap SubjectBitmap)
        {
            this.Subject = SubjectBitmap;

            try
            {
                this.LockBitmap();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the bitmap.
        /// </summary>
        public Bitmap Bitmap
        {
            get
            {
                return this.Subject;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get pixel.
        /// </summary>
        /// <param name="X">
        /// The x.
        /// </param>
        /// <param name="Y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="Color"/>.
        /// </returns>
        /// <exception cref="AccessViolationException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public Color GetPixel(int X, int Y)
        {
            try
            {
                PixelData* p = this.PixelAt(X, Y);
                return Color.FromArgb((int)p->red, (int)p->green, (int)p->blue);
            }
            catch (AccessViolationException ave)
            {
                throw ave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The release.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        public void Release()
        {
            try
            {
                this.UnlockBitmap();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The set pixel.
        /// </summary>
        /// <param name="X">
        /// The x.
        /// </param>
        /// <param name="Y">
        /// The y.
        /// </param>
        /// <param name="Colour">
        /// The colour.
        /// </param>
        /// <exception cref="AccessViolationException">
        /// </exception>
        /// <exception cref="Exception">
        /// </exception>
        public void SetPixel(int X, int Y, Color Colour)
        {
            try
            {
                PixelData* p = this.PixelAt(X, Y);
                p->red = Colour.R;
                p->green = Colour.G;
                p->blue = Colour.B;
            }
            catch (AccessViolationException ave)
            {
                throw ave;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The lock bitmap.
        /// </summary>
        private void LockBitmap()
        {
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = this.Subject.GetBounds(ref unit);
            Rectangle bounds = new Rectangle((int)boundsF.X, (int)boundsF.Y, (int)boundsF.Width, (int)boundsF.Height);
            this.SubjectWidth = (int)boundsF.Width * sizeof(PixelData);

            if (this.SubjectWidth % 4 != 0)
            {
                this.SubjectWidth = 4 * (this.SubjectWidth / 4 + 1);
            }

            this.bitmapData = this.Subject.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            this.pBase = (Byte*)this.bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        /// The pixel at.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="PixelData*"/>.
        /// </returns>
        private PixelData* PixelAt(int x, int y)
        {
            return (PixelData*)(this.pBase + y * this.SubjectWidth + x * sizeof(PixelData));
        }

        /// <summary>
        /// The unlock bitmap.
        /// </summary>
        private void UnlockBitmap()
        {
            this.Subject.UnlockBits(this.bitmapData);
            this.bitmapData = null;
            this.pBase = null;
        }

        #endregion

        /// <summary>
        /// The pixel data.
        /// </summary>
        public struct PixelData
        {
            #region Fields

            /// <summary>
            /// The blue.
            /// </summary>
            public byte blue;

            /// <summary>
            /// The green.
            /// </summary>
            public byte green;

            /// <summary>
            /// The red.
            /// </summary>
            public byte red;

            #endregion
        }
    }
}