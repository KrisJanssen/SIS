// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Watermarker.cs" company="">
//   
// </copyright>
// <summary>
//   Watermarker - a class which can be used for watermarking an image.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Tools.Images
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    /// <summary>
    /// Watermarker - a class which can be used for watermarking an image.
    /// </summary>
    /// <remarks>
    /// Used in some older projects, should probably be deprecated.
    /// </remarks>
    public class Watermarker
    {
        #region Public Methods and Operators

        /// <summary>
        /// The draw string.
        /// </summary>
        /// <param name="grPhoto">
        /// The gr photo.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="font">
        /// The font.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="brush">
        /// The brush.
        /// </param>
        public void DrawString(Graphics grPhoto, string text, Font font, int x, int y, Brush brush)
        {
            // Define the text layout by setting the text alignment to centered
            var StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Near;

            // Draw the Copyright string
            grPhoto.DrawString(text, font, brush, new PointF(x, y), StrFormat);
        }

        /// <summary>
        /// The watermark file with text.
        /// </summary>
        /// <param name="inputFile">
        /// The input file.
        /// </param>
        /// <param name="outputFile">
        /// The output file.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="font">
        /// The font.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="renderOver">
        /// The render over.
        /// </param>
        /// <param name="under">
        /// The under.
        /// </param>
        /// <param name="over">
        /// The over.
        /// </param>
        /// <param name="xAlignment">
        /// The x alignment.
        /// </param>
        /// <param name="yAlignment">
        /// The y alignment.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public void WatermarkFileWithText(
            string inputFile, 
            string outputFile, 
            string text, 
            Font font, 
            int x, 
            int y, 
            bool renderOver, 
            Brush under, 
            Brush over, 
            StringAlignment xAlignment, 
            StringAlignment yAlignment)
        {
            Image imgPhoto = null;
            Image outputPhoto = null;
            try
            {
                try
                {
                    imgPhoto = Image.FromFile(inputFile);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to open file \"" + inputFile + "\"", ex);
                }

                this.WatermarkImageWithText(
                    imgPhoto, 
                    ref outputPhoto, 
                    SmoothingMode.AntiAlias, 
                    text, 
                    font, 
                    x, 
                    y, 
                    renderOver, 
                    under, 
                    over, 
                    xAlignment, 
                    yAlignment);

                imgPhoto.Dispose();
                imgPhoto = null;

                outputPhoto.Save(outputFile, ImageFormat.Jpeg);
            }
            finally
            {
                if (imgPhoto != null)
                {
                    imgPhoto.Dispose();
                }

                if (outputPhoto != null)
                {
                    outputPhoto.Dispose();
                }
            }
        }

        /// <summary>
        /// The watermark image with text.
        /// </summary>
        /// <param name="inputImage">
        /// The input image.
        /// </param>
        /// <param name="outputImage">
        /// The output image.
        /// </param>
        /// <param name="smoothingMode">
        /// The smoothing mode.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="font">
        /// The font.
        /// </param>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="renderOver">
        /// The render over.
        /// </param>
        /// <param name="under">
        /// The under.
        /// </param>
        /// <param name="over">
        /// The over.
        /// </param>
        /// <param name="xAlignment">
        /// The x alignment.
        /// </param>
        /// <param name="yAlignment">
        /// The y alignment.
        /// </param>
        public void WatermarkImageWithText(
            Image inputImage, 
            ref Image outputImage, 
            SmoothingMode smoothingMode, 
            string text, 
            Font font, 
            int x, 
            int y, 
            bool renderOver, 
            Brush under, 
            Brush over, 
            StringAlignment xAlignment, 
            StringAlignment yAlignment)
        {
            int phWidth = inputImage.Width;
            int phHeight = inputImage.Height;

            // create a Bitmap the Size of the original photograph
            var bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            Graphics grPhoto = null;

            try
            {
                // load the Bitmap into a Graphics object 
                grPhoto = Graphics.FromImage(bmPhoto);

                // Set the rendering quality for this Graphics object
                grPhoto.SmoothingMode = smoothingMode;

                // Draws the photo Image object at original size to the graphics object.
                grPhoto.DrawImage(
                    inputImage, 
                    // Photo Image object
                    new Rectangle(0, 0, phWidth, phHeight), 
                    // Rectangle structure
                    0, 
                    // x-coordinate of the portion of the source image to draw. 
                    0, 
                    // y-coordinate of the portion of the source image to draw. 
                    phWidth, 
                    // Width of the portion of the source image to draw. 
                    phHeight, 
                    // Height of the portion of the source image to draw. 
                    GraphicsUnit.Pixel); // Units of measure

                // calculate the text size
                SizeF textSize = grPhoto.MeasureString(text, font);
                int textX = CalculatePosition(x, phWidth, (int)textSize.Width, xAlignment);
                int textY = CalculatePosition(y, phHeight, (int)textSize.Height, yAlignment);

                if (renderOver)
                {
                    this.DrawString(grPhoto, text, font, textX + 1, textY + 1, over);
                }

                this.DrawString(grPhoto, text, font, textX, textY, under);

                outputImage = bmPhoto;
                bmPhoto = null;
            }
            finally
            {
                if (grPhoto != null)
                {
                    grPhoto.Dispose();
                }

                if (bmPhoto != null)
                {
                    bmPhoto.Dispose();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The calculate position.
        /// </summary>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="canvasSize">
        /// The canvas size.
        /// </param>
        /// <param name="textSize">
        /// The text size.
        /// </param>
        /// <param name="alignment">
        /// The alignment.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int CalculatePosition(int offset, int canvasSize, int textSize, StringAlignment alignment)
        {
            switch (alignment)
            {
                case StringAlignment.Center:
                    return ((canvasSize / 2) - (textSize / 2)) + offset;
                case StringAlignment.Far:
                    return (canvasSize - textSize) - offset;
                default:
                    return offset;
            }
        }

        #endregion
    }
}