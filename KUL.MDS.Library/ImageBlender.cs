// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageBlender.cs" company="">
//   
// </copyright>
// <summary>
//   The image blender.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Library
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    /// <summary>
    /// The image blender.
    /// </summary>
    public class ImageBlender
    {
        #region Constants

        /// <summary>
        /// The b_ weight.
        /// </summary>
        public const float B_WEIGHT = 0.144f;

        /// <summary>
        /// The g_ weight.
        /// </summary>
        public const float G_WEIGHT = 0.587f;

        /// <summary>
        /// The hlsmax.
        /// </summary>
        public const ushort HLSMAX = 360;

        /// <summary>
        /// The hundefined.
        /// </summary>
        public const byte HUNDEFINED = 0;

        /// <summary>
        /// The rgbmax.
        /// </summary>
        public const byte RGBMAX = 255;

        /// <summary>
        /// The r_ weight.
        /// </summary>
        public const float R_WEIGHT = 0.299f;

        #endregion

        #region Delegates

        /// <summary>
        /// The per channel process delegate.
        /// </summary>
        /// <param name="nSrc">
        /// The n src.
        /// </param>
        /// <param name="nDst">
        /// The n dst.
        /// </param>
        private delegate byte PerChannelProcessDelegate(ref byte nSrc, ref byte nDst);

        /// <summary>
        /// The rgb process delegate.
        /// </summary>
        /// <param name="sR">
        /// The s r.
        /// </param>
        /// <param name="sG">
        /// The s g.
        /// </param>
        /// <param name="sB">
        /// The s b.
        /// </param>
        /// <param name="dR">
        /// The d r.
        /// </param>
        /// <param name="dG">
        /// The d g.
        /// </param>
        /// <param name="dB">
        /// The d b.
        /// </param>
        private delegate void RGBProcessDelegate(byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB);

        #endregion

        #region Enums

        /// <summary>
        /// The blend operation.
        /// </summary>
        public enum BlendOperation : int
        {
            /// <summary>
            /// The source copy.
            /// </summary>
            SourceCopy = 1, 

            /// <summary>
            /// The ro p_ merge paint.
            /// </summary>
            ROP_MergePaint, 

            /// <summary>
            /// The ro p_ not source erase.
            /// </summary>
            ROP_NOTSourceErase, 

            /// <summary>
            /// The ro p_ source and.
            /// </summary>
            ROP_SourceAND, 

            /// <summary>
            /// The ro p_ source erase.
            /// </summary>
            ROP_SourceErase, 

            /// <summary>
            /// The ro p_ source invert.
            /// </summary>
            ROP_SourceInvert, 

            /// <summary>
            /// The ro p_ source paint.
            /// </summary>
            ROP_SourcePaint, 

            /// <summary>
            /// The blend_ darken.
            /// </summary>
            Blend_Darken, 

            /// <summary>
            /// The blend_ multiply.
            /// </summary>
            Blend_Multiply, 

            /// <summary>
            /// The blend_ color burn.
            /// </summary>
            Blend_ColorBurn, 

            /// <summary>
            /// The blend_ lighten.
            /// </summary>
            Blend_Lighten, 

            /// <summary>
            /// The blend_ screen.
            /// </summary>
            Blend_Screen, 

            /// <summary>
            /// The blend_ color dodge.
            /// </summary>
            Blend_ColorDodge, 

            /// <summary>
            /// The blend_ overlay.
            /// </summary>
            Blend_Overlay, 

            /// <summary>
            /// The blend_ soft light.
            /// </summary>
            Blend_SoftLight, 

            /// <summary>
            /// The blend_ hard light.
            /// </summary>
            Blend_HardLight, 

            /// <summary>
            /// The blend_ pin light.
            /// </summary>
            Blend_PinLight, 

            /// <summary>
            /// The blend_ difference.
            /// </summary>
            Blend_Difference, 

            /// <summary>
            /// The blend_ exclusion.
            /// </summary>
            Blend_Exclusion, 

            /// <summary>
            /// The blend_ hue.
            /// </summary>
            Blend_Hue, 

            /// <summary>
            /// The blend_ saturation.
            /// </summary>
            Blend_Saturation, 

            /// <summary>
            /// The blend_ color.
            /// </summary>
            Blend_Color, 

            /// <summary>
            /// The blend_ luminosity.
            /// </summary>
            Blend_Luminosity
        }

        #endregion

        // Invert image

        // Adjustment values are between -1.0 and 1.0
        #region Public Methods and Operators

        /// <summary>
        /// The adjust brightness.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="adjValueR">
        /// The adj value r.
        /// </param>
        /// <param name="adjValueG">
        /// The adj value g.
        /// </param>
        /// <param name="adjValueB">
        /// The adj value b.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void AdjustBrightness(Image img, float adjValueR, float adjValueG, float adjValueB)
        {
            if (img == null)
            {
                throw new Exception("Image must be provided");
            }

            ColorMatrix cMatrix =
                new ColorMatrix(
                    new[]
                        {
                            new[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f }, new[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f }, 
                            new[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f }, new[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, 
                            new[] { adjValueR, adjValueG, adjValueB, 0.0f, 1.0f }
                        });
            this.ApplyColorMatrix(ref img, cMatrix);
        }

        // Adjustment values are between -1.0 and 1.0
        /// <summary>
        /// The adjust brightness.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="adjValue">
        /// The adj value.
        /// </param>
        public void AdjustBrightness(Image img, float adjValue)
        {
            this.AdjustBrightness(img, adjValue, adjValue, adjValue);
        }

        // Saturation. 0.0 = desaturate, 1.0 = identity, -1.0 = complementary colors
        /// <summary>
        /// The adjust saturation.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="sat">
        /// The sat.
        /// </param>
        /// <param name="rweight">
        /// The rweight.
        /// </param>
        /// <param name="gweight">
        /// The gweight.
        /// </param>
        /// <param name="bweight">
        /// The bweight.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void AdjustSaturation(Image img, float sat, float rweight, float gweight, float bweight)
        {
            if (img == null)
            {
                throw new Exception("Image must be provided");
            }

            ColorMatrix cMatrix =
                new ColorMatrix(
                    new[]
                        {
                            new[]
                                {
                                    (1.0f - sat) * rweight + sat, (1.0f - sat) * rweight, (1.0f - sat) * rweight, 0.0f, 
                                    0.0f
                                }, 
                            new[]
                                {
                                    (1.0f - sat) * gweight, (1.0f - sat) * gweight + sat, (1.0f - sat) * gweight, 0.0f, 
                                    0.0f
                                }, 
                            new[]
                                {
                                    (1.0f - sat) * bweight, (1.0f - sat) * bweight, (1.0f - sat) * bweight + sat, 0.0f, 
                                    0.0f
                                }, 
                            new[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, 
                            new[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
                        });
            this.ApplyColorMatrix(ref img, cMatrix);
        }

        // Saturation. 0.0 = desaturate, 1.0 = identity, -1.0 = complementary colors
        /// <summary>
        /// The adjust saturation.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="sat">
        /// The sat.
        /// </param>
        public void AdjustSaturation(Image img, float sat)
        {
            this.AdjustSaturation(img, sat, R_WEIGHT, G_WEIGHT, B_WEIGHT);
        }

        // Weights between 0.0 and 1.0

        /// <summary>
        /// The apply color matrix.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="colMatrix">
        /// The col matrix.
        /// </param>
        public void ApplyColorMatrix(ref Image img, ColorMatrix colMatrix)
        {
            Graphics gr = Graphics.FromImage(img);
            ImageAttributes attrs = new ImageAttributes();
            attrs.SetColorMatrix(colMatrix);
            gr.DrawImage(
                img, 
                new Rectangle(0, 0, img.Width, img.Height), 
                0, 
                0, 
                img.Width, 
                img.Height, 
                GraphicsUnit.Pixel, 
                attrs);
            gr.Dispose();
        }

        /* 
			destImage - image that will be used as background
			destX, destY - define position on destination image where to start applying blend operation
			destWidth, destHeight - width and height of the area to apply blending
			srcImage - image to use as foreground (source of blending)	
			srcX, srcY - starting position of the source image 	  
		*/

        /// <summary>
        /// The blend images.
        /// </summary>
        /// <param name="destImage">
        /// The dest image.
        /// </param>
        /// <param name="destX">
        /// The dest x.
        /// </param>
        /// <param name="destY">
        /// The dest y.
        /// </param>
        /// <param name="destWidth">
        /// The dest width.
        /// </param>
        /// <param name="destHeight">
        /// The dest height.
        /// </param>
        /// <param name="srcImage">
        /// The src image.
        /// </param>
        /// <param name="srcX">
        /// The src x.
        /// </param>
        /// <param name="srcY">
        /// The src y.
        /// </param>
        /// <param name="BlendOp">
        /// The blend op.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void BlendImages(
            Image destImage, 
            int destX, 
            int destY, 
            int destWidth, 
            int destHeight, 
            Image srcImage, 
            int srcX, 
            int srcY, 
            BlendOperation BlendOp)
        {
            if (destImage == null)
            {
                throw new Exception("Destination image must be provided");
            }

            if (destImage.Width < destX + destWidth || destImage.Height < destY + destHeight)
            {
                throw new Exception("Destination image is smaller than requested dimentions");
            }

            if (srcImage == null)
            {
                throw new Exception("Source image must be provided");
            }

            if (srcImage.Width < srcX + destWidth || srcImage.Height < srcY + destHeight)
            {
                throw new Exception("Source image is smaller than requested dimentions");
            }

            Bitmap tempBmp = null;
            Graphics gr = Graphics.FromImage(destImage);
            gr.CompositingMode = CompositingMode.SourceCopy;

            switch (BlendOp)
            {
                case BlendOperation.SourceCopy:
                    gr.DrawImage(
                        srcImage, 
                        new Rectangle(destX, destY, destWidth, destHeight), 
                        srcX, 
                        srcY, 
                        destWidth, 
                        destHeight, 
                        GraphicsUnit.Pixel);
                    break;

                case BlendOperation.ROP_MergePaint:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.MergePaint));
                    break;

                case BlendOperation.ROP_NOTSourceErase:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.NOTSourceErase));
                    break;

                case BlendOperation.ROP_SourceAND:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.SourceAND));
                    break;

                case BlendOperation.ROP_SourceErase:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.SourceErase));
                    break;

                case BlendOperation.ROP_SourceInvert:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.SourceInvert));
                    break;

                case BlendOperation.ROP_SourcePaint:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.SourcePaint));
                    break;

                case BlendOperation.Blend_Darken:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendDarken));
                    break;

                case BlendOperation.Blend_Multiply:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendMultiply));
                    break;

                case BlendOperation.Blend_Screen:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendScreen));
                    break;

                case BlendOperation.Blend_Lighten:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendLighten));
                    break;

                case BlendOperation.Blend_HardLight:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendHardLight));
                    break;

                case BlendOperation.Blend_Difference:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendDifference));
                    break;

                case BlendOperation.Blend_PinLight:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendPinLight));
                    break;

                case BlendOperation.Blend_Overlay:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendOverlay));
                    break;

                case BlendOperation.Blend_Exclusion:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendExclusion));
                    break;

                case BlendOperation.Blend_SoftLight:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendSoftLight));
                    break;

                case BlendOperation.Blend_ColorBurn:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendColorBurn));
                    break;

                case BlendOperation.Blend_ColorDodge:
                    tempBmp = this.PerChannelProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new PerChannelProcessDelegate(this.BlendColorDodge));
                    break;

                case BlendOperation.Blend_Hue:
                    tempBmp = this.RGBProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new RGBProcessDelegate(this.BlendHue));
                    break;

                case BlendOperation.Blend_Saturation:
                    tempBmp = this.RGBProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new RGBProcessDelegate(this.BlendSaturation));
                    break;

                case BlendOperation.Blend_Color:
                    tempBmp = this.RGBProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new RGBProcessDelegate(this.BlendColor));
                    break;

                case BlendOperation.Blend_Luminosity:
                    tempBmp = this.RGBProcess(
                        ref destImage, 
                        destX, 
                        destY, 
                        destWidth, 
                        destHeight, 
                        ref srcImage, 
                        srcX, 
                        srcY, 
                        new RGBProcessDelegate(this.BlendLuminosity));
                    break;
            }

            if (tempBmp != null)
            {
                gr.DrawImage(tempBmp, 0, 0, tempBmp.Width, tempBmp.Height);
                tempBmp.Dispose();
                tempBmp = null;
            }

            gr.Dispose();
            gr = null;
        }

        /// <summary>
        /// The blend images.
        /// </summary>
        /// <param name="destImage">
        /// The dest image.
        /// </param>
        /// <param name="srcImage">
        /// The src image.
        /// </param>
        /// <param name="BlendOp">
        /// The blend op.
        /// </param>
        public void BlendImages(Image destImage, Image srcImage, BlendOperation BlendOp)
        {
            this.BlendImages(destImage, 0, 0, destImage.Width, destImage.Height, srcImage, 0, 0, BlendOp);
        }

        /// <summary>
        /// The blend images.
        /// </summary>
        /// <param name="destImage">
        /// The dest image.
        /// </param>
        /// <param name="BlendOp">
        /// The blend op.
        /// </param>
        public void BlendImages(Image destImage, BlendOperation BlendOp)
        {
            this.BlendImages(destImage, 0, 0, destImage.Width, destImage.Height, null, 0, 0, BlendOp);
        }

        /// <summary>
        /// The blend images.
        /// </summary>
        /// <param name="destImage">
        /// The dest image.
        /// </param>
        /// <param name="destX">
        /// The dest x.
        /// </param>
        /// <param name="destY">
        /// The dest y.
        /// </param>
        /// <param name="BlendOp">
        /// The blend op.
        /// </param>
        public void BlendImages(Image destImage, int destX, int destY, BlendOperation BlendOp)
        {
            this.BlendImages(
                destImage, 
                destX, 
                destY, 
                destImage.Width - destX, 
                destImage.Height - destY, 
                null, 
                0, 
                0, 
                BlendOp);
        }

        /// <summary>
        /// The blend images.
        /// </summary>
        /// <param name="destImage">
        /// The dest image.
        /// </param>
        /// <param name="destX">
        /// The dest x.
        /// </param>
        /// <param name="destY">
        /// The dest y.
        /// </param>
        /// <param name="destWidth">
        /// The dest width.
        /// </param>
        /// <param name="destHeight">
        /// The dest height.
        /// </param>
        /// <param name="BlendOp">
        /// The blend op.
        /// </param>
        public void BlendImages(
            Image destImage, 
            int destX, 
            int destY, 
            int destWidth, 
            int destHeight, 
            BlendOperation BlendOp)
        {
            this.BlendImages(destImage, destX, destY, destWidth, destHeight, null, 0, 0, BlendOp);
        }

        /// <summary>
        /// The desaturate.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <param name="RWeight">
        /// The r weight.
        /// </param>
        /// <param name="GWeight">
        /// The g weight.
        /// </param>
        /// <param name="BWeight">
        /// The b weight.
        /// </param>
        public void Desaturate(Image img, float RWeight, float GWeight, float BWeight)
        {
            this.AdjustSaturation(img, 0.0f, RWeight, GWeight, BWeight);
        }

        // Desaturate using "default" NTSC defined color weights
        /// <summary>
        /// The desaturate.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        public void Desaturate(Image img)
        {
            this.AdjustSaturation(img, 0.0f, R_WEIGHT, G_WEIGHT, B_WEIGHT);
        }

        /// <summary>
        /// The hls to rgb.
        /// </summary>
        /// <param name="H">
        /// The h.
        /// </param>
        /// <param name="L">
        /// The l.
        /// </param>
        /// <param name="S">
        /// The s.
        /// </param>
        /// <param name="R">
        /// The r.
        /// </param>
        /// <param name="G">
        /// The g.
        /// </param>
        /// <param name="B">
        /// The b.
        /// </param>
        public void HLSToRGB(ushort H, ushort L, ushort S, out byte R, out byte G, out byte B)
        {
            float Magic1, Magic2; /* calculated magic numbers (really!) */

            if (S == 0)
            {
                /* achromatic case */
                R = G = B = (byte)((L * RGBMAX) / HLSMAX);
            }
            else
            {
                /* chromatic case */
                /* set up magic numbers */
                if (L <= (HLSMAX / 2))
                {
                    Magic2 = (float)((L * (HLSMAX + S) + (HLSMAX / 2)) / HLSMAX);
                }
                else
                {
                    Magic2 = (float)(L + S - ((L * S) + (HLSMAX / 2)) / HLSMAX);
                }

                Magic1 = (float)(2 * L - Magic2);

                /* get RGB, change units from HLSMAX to RGBMAX */
                R = (byte)((this.HueToRGB(Magic1, Magic2, H + (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                G = (byte)((this.HueToRGB(Magic1, Magic2, H) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
                B = (byte)((this.HueToRGB(Magic1, Magic2, H - (HLSMAX / 3)) * RGBMAX + (HLSMAX / 2)) / HLSMAX);
            }
        }

        /// <summary>
        /// The invert.
        /// </summary>
        /// <param name="img">
        /// The img.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Invert(Image img)
        {
            if (img == null)
            {
                throw new Exception("Image must be provided");
            }

            ColorMatrix cMatrix =
                new ColorMatrix(
                    new[]
                        {
                            new[] { -1.0f, 0.0f, 0.0f, 0.0f, 0.0f }, new[] { 0.0f, -1.0f, 0.0f, 0.0f, 0.0f }, 
                            new[] { 0.0f, 0.0f, -1.0f, 0.0f, 0.0f }, new[] { 0.0f, 0.0f, 0.0f, 1.0f, 0.0f }, 
                            new[] { 1.0f, 1.0f, 1.0f, 0.0f, 1.0f }
                        });
            this.ApplyColorMatrix(ref img, cMatrix);
        }

        /// <summary>
        /// The rgb to hls.
        /// </summary>
        /// <param name="R">
        /// The r.
        /// </param>
        /// <param name="G">
        /// The g.
        /// </param>
        /// <param name="B">
        /// The b.
        /// </param>
        /// <param name="H">
        /// The h.
        /// </param>
        /// <param name="L">
        /// The l.
        /// </param>
        /// <param name="S">
        /// The s.
        /// </param>
        public void RGBToHLS(byte R, byte G, byte B, out ushort H, out ushort L, out ushort S)
        {
            byte cMax, cMin; /* max and min RGB values */
            float Rdelta, Gdelta, Bdelta; /* intermediate value: % of spread from max */

            /* calculate lightness */
            cMax = Math.Max(Math.Max(R, G), B);
            cMin = Math.Min(Math.Min(R, G), B);
            L = (ushort)((((cMax + cMin) * HLSMAX) + RGBMAX) / (2 * RGBMAX));

            if (cMax == cMin)
            {
                /* r=g=b --> achromatic case */
                S = 0; /* saturation */
                H = HUNDEFINED; /* hue */
            }
            else
            {
                /* chromatic case */
                /* saturation */
                if (L <= (HLSMAX / 2))
                {
                    S = (ushort)((((cMax - cMin) * HLSMAX) + ((cMax + cMin) / 2)) / (cMax + cMin));
                }
                else
                {
                    S =
                        (ushort)
                        ((((cMax - cMin) * HLSMAX) + ((2 * RGBMAX - cMax - cMin) / 2)) / (2 * RGBMAX - cMax - cMin));
                }

                /* hue */
                Rdelta = (float)((((cMax - R) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Gdelta = (float)((((cMax - G) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));
                Bdelta = (float)((((cMax - B) * (HLSMAX / 6)) + ((cMax - cMin) / 2)) / (cMax - cMin));

                if (R == cMax)
                {
                    H = (ushort)(Bdelta - Gdelta);
                }
                else if (G == cMax)
                {
                    H = (ushort)((HLSMAX / 3) + Rdelta - Bdelta);
                }
                else /* B == cMax */
                {
                    H = (ushort)(((2 * HLSMAX) / 3) + Gdelta - Rdelta);
                }

                if (H < 0)
                {
                    H += HLSMAX;
                }

                if (H > HLSMAX)
                {
                    H -= HLSMAX;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The blend color.
        /// </summary>
        /// <param name="sR">
        /// The s r.
        /// </param>
        /// <param name="sG">
        /// The s g.
        /// </param>
        /// <param name="sB">
        /// The s b.
        /// </param>
        /// <param name="dR">
        /// The d r.
        /// </param>
        /// <param name="dG">
        /// The d g.
        /// </param>
        /// <param name="dB">
        /// The d b.
        /// </param>
        private void BlendColor(byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB)
        {
            ushort sH, sL, sS, dH, dL, dS;
            this.RGBToHLS(sR, sG, sB, out sH, out sL, out sS);
            this.RGBToHLS(dR, dG, dB, out dH, out dL, out dS);
            this.HLSToRGB(sH, dL, sS, out dR, out dG, out dB);
        }

        /// <summary>
        /// The blend color burn.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendColorBurn(ref byte Src, ref byte Dst)
        {
            return (Src == 0) ? (byte)0 : (byte)Math.Max(Math.Min(255 - (((255 - Dst) * 255) / Src), 255), 0);
        }

        // Color Dodge 
        /// <summary>
        /// The blend color dodge.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendColorDodge(ref byte Src, ref byte Dst)
        {
            return (Src == 255) ? (byte)255 : (byte)Math.Max(Math.Min((Dst * 255) / (255 - Src), 255), 0);
        }

        /// <summary>
        /// The blend darken.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendDarken(ref byte Src, ref byte Dst)
        {
            return (Src < Dst) ? Src : Dst;
        }

        /// <summary>
        /// The blend difference.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendDifference(ref byte Src, ref byte Dst)
        {
            return (byte)((Src > Dst) ? Src - Dst : Dst - Src);
        }

        /// <summary>
        /// The blend exclusion.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendExclusion(ref byte Src, ref byte Dst)
        {
            return (byte)(Src + Dst - 2 * (Dst * Src) / 255f);
        }

        /// <summary>
        /// The blend hard light.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendHardLight(ref byte Src, ref byte Dst)
        {
            return (Src < 128)
                        ? (byte)Math.Max(Math.Min((Src / 255.0f * Dst / 255.0f) * 255.0f * 2, 255), 0)
                        : (byte)
                          Math.Max(Math.Min(255 - ((255 - Src) / 255.0f * (255 - Dst) / 255.0f) * 255.0f * 2, 255), 0);
        }

        /// <summary>
        /// The blend hue.
        /// </summary>
        /// <param name="sR">
        /// The s r.
        /// </param>
        /// <param name="sG">
        /// The s g.
        /// </param>
        /// <param name="sB">
        /// The s b.
        /// </param>
        /// <param name="dR">
        /// The d r.
        /// </param>
        /// <param name="dG">
        /// The d g.
        /// </param>
        /// <param name="dB">
        /// The d b.
        /// </param>
        private void BlendHue(byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB)
        {
            ushort sH, sL, sS, dH, dL, dS;
            this.RGBToHLS(sR, sG, sB, out sH, out sL, out sS);
            this.RGBToHLS(dR, dG, dB, out dH, out dL, out dS);
            this.HLSToRGB(sH, dL, dS, out dR, out dG, out dB);
        }

        /// <summary>
        /// The blend lighten.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendLighten(ref byte Src, ref byte Dst)
        {
            return (Src > Dst) ? Src : Dst;
        }

        /// <summary>
        /// The blend luminosity.
        /// </summary>
        /// <param name="sR">
        /// The s r.
        /// </param>
        /// <param name="sG">
        /// The s g.
        /// </param>
        /// <param name="sB">
        /// The s b.
        /// </param>
        /// <param name="dR">
        /// The d r.
        /// </param>
        /// <param name="dG">
        /// The d g.
        /// </param>
        /// <param name="dB">
        /// The d b.
        /// </param>
        private void BlendLuminosity(byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB)
        {
            ushort sH, sL, sS, dH, dL, dS;
            this.RGBToHLS(sR, sG, sB, out sH, out sL, out sS);
            this.RGBToHLS(dR, dG, dB, out dH, out dL, out dS);
            this.HLSToRGB(dH, sL, dS, out dR, out dG, out dB);
        }

        /// <summary>
        /// The blend multiply.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendMultiply(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min((Src / 255.0f * Dst / 255.0f) * 255.0f, 255), 0);
        }

        /// <summary>
        /// The blend overlay.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendOverlay(ref byte Src, ref byte Dst)
        {
            return (Dst < 128)
                        ? (byte)Math.Max(Math.Min((Src / 255.0f * Dst / 255.0f) * 255.0f * 2, 255), 0)
                        : (byte)
                          Math.Max(Math.Min(255 - ((255 - Src) / 255.0f * (255 - Dst) / 255.0f) * 255.0f * 2, 255), 0);
        }

        /// <summary>
        /// The blend pin light.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendPinLight(ref byte Src, ref byte Dst)
        {
            return (Src < 128) ? ((Dst > Src) ? Src : Dst) : ((Dst < Src) ? Src : Dst);
        }

        /// <summary>
        /// The blend saturation.
        /// </summary>
        /// <param name="sR">
        /// The s r.
        /// </param>
        /// <param name="sG">
        /// The s g.
        /// </param>
        /// <param name="sB">
        /// The s b.
        /// </param>
        /// <param name="dR">
        /// The d r.
        /// </param>
        /// <param name="dG">
        /// The d g.
        /// </param>
        /// <param name="dB">
        /// The d b.
        /// </param>
        private void BlendSaturation(byte sR, byte sG, byte sB, ref byte dR, ref byte dG, ref byte dB)
        {
            ushort sH, sL, sS, dH, dL, dS;
            this.RGBToHLS(sR, sG, sB, out sH, out sL, out sS);
            this.RGBToHLS(dR, dG, dB, out dH, out dL, out dS);
            this.HLSToRGB(dH, dL, sS, out dR, out dG, out dB);
        }

        /// <summary>
        /// The blend screen.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendScreen(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(255 - ((255 - Src) / 255.0f * (255 - Dst) / 255.0f) * 255.0f, 255), 0);
        }

        /// <summary>
        /// The blend soft light.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte BlendSoftLight(ref byte Src, ref byte Dst)
        {
            return
                (byte)
                Math.Max(
                    Math.Min(
                        (Dst * Src / 255f)
                        + Dst * (255 - ((255 - Dst) * (255 - Src) / 255f) - (Dst * Src / 255f)) / 255f, 
                        255), 
                    0);
        }

        /* utility routine for HLStoRGB */

        /// <summary>
        /// The hue to rgb.
        /// </summary>
        /// <param name="n1">
        /// The n 1.
        /// </param>
        /// <param name="n2">
        /// The n 2.
        /// </param>
        /// <param name="hue">
        /// The hue.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        private float HueToRGB(float n1, float n2, float hue)
        {
            /* range check: note values passed add/subtract thirds of range */
            if (hue < 0)
            {
                hue += HLSMAX;
            }

            if (hue > HLSMAX)
            {
                hue -= HLSMAX;
            }

            /* return r,g, or b value from this tridrant */
            if (hue < (HLSMAX / 6))
            {
                return (float)(n1 + (((n2 - n1) * hue + (HLSMAX / 12)) / (HLSMAX / 6)));
            }

            if (hue < (HLSMAX / 2))
            {
                return (float)n2;
            }

            if (hue < ((HLSMAX * 2) / 3))
            {
                return (float)(n1 + (((n2 - n1) * (((HLSMAX * 2) / 3) - hue) + (HLSMAX / 12)) / (HLSMAX / 6)));
            }
            else
            {
                return (float)n1;
            }
        }

        // (NOT Source) OR Destination
        /// <summary>
        /// The merge paint.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte MergePaint(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min((255 - Src) | Dst, 255), 0);
        }

        // NOT (Source OR Destination)
        /// <summary>
        /// The not source erase.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte NOTSourceErase(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(255 - (Src | Dst), 255), 0);
        }

        /// <summary>
        /// The per channel process.
        /// </summary>
        /// <param name="destImg">
        /// The dest img.
        /// </param>
        /// <param name="destX">
        /// The dest x.
        /// </param>
        /// <param name="destY">
        /// The dest y.
        /// </param>
        /// <param name="destWidth">
        /// The dest width.
        /// </param>
        /// <param name="destHeight">
        /// The dest height.
        /// </param>
        /// <param name="srcImg">
        /// The src img.
        /// </param>
        /// <param name="srcX">
        /// The src x.
        /// </param>
        /// <param name="srcY">
        /// The src y.
        /// </param>
        /// <param name="ChannelProcessFunction">
        /// The channel process function.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        private Bitmap PerChannelProcess(
            ref Image destImg, 
            int destX, 
            int destY, 
            int destWidth, 
            int destHeight, 
            ref Image srcImg, 
            int srcX, 
            int srcY, 
            PerChannelProcessDelegate ChannelProcessFunction)
        {
            Bitmap dst = new Bitmap(destImg);
            Bitmap src = new Bitmap(srcImg);

            BitmapData dstBD = dst.LockBits(
                new Rectangle(destX, destY, destWidth, destHeight), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);
            BitmapData srcBD = src.LockBits(
                new Rectangle(srcX, srcY, destWidth, destHeight), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int dstStride = dstBD.Stride;
            int srcStride = srcBD.Stride;

            IntPtr dstScan0 = dstBD.Scan0;
            IntPtr srcScan0 = srcBD.Scan0;

            unsafe
            {
                byte* pDst = (byte*)(void*)dstScan0;
                byte* pSrc = (byte*)(void*)srcScan0;

                for (int y = 0; y < destHeight; y++)
                {
                    for (int x = 0; x < destWidth * 3; x++)
                    {
                        pDst[x + y * dstStride] = ChannelProcessFunction(
                            ref pSrc[x + y * srcStride], 
                            ref pDst[x + y * dstStride]);
                    }
                }
            }

            src.UnlockBits(srcBD);
            dst.UnlockBits(dstBD);

            src.Dispose();

            return dst;
        }

        /// <summary>
        /// The rgb process.
        /// </summary>
        /// <param name="destImg">
        /// The dest img.
        /// </param>
        /// <param name="destX">
        /// The dest x.
        /// </param>
        /// <param name="destY">
        /// The dest y.
        /// </param>
        /// <param name="destWidth">
        /// The dest width.
        /// </param>
        /// <param name="destHeight">
        /// The dest height.
        /// </param>
        /// <param name="srcImg">
        /// The src img.
        /// </param>
        /// <param name="srcX">
        /// The src x.
        /// </param>
        /// <param name="srcY">
        /// The src y.
        /// </param>
        /// <param name="RGBProcessFunction">
        /// The rgb process function.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        private Bitmap RGBProcess(
            ref Image destImg, 
            int destX, 
            int destY, 
            int destWidth, 
            int destHeight, 
            ref Image srcImg, 
            int srcX, 
            int srcY, 
            RGBProcessDelegate RGBProcessFunction)
        {
            Bitmap dst = new Bitmap(destImg);
            Bitmap src = new Bitmap(srcImg);

            BitmapData dstBD = dst.LockBits(
                new Rectangle(destX, destY, destWidth, destHeight), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);
            BitmapData srcBD = src.LockBits(
                new Rectangle(srcX, srcY, destWidth, destHeight), 
                ImageLockMode.ReadWrite, 
                PixelFormat.Format24bppRgb);

            int dstStride = dstBD.Stride;
            int srcStride = srcBD.Stride;

            IntPtr dstScan0 = dstBD.Scan0;
            IntPtr srcScan0 = srcBD.Scan0;

            unsafe
            {
                byte* pDst = (byte*)(void*)dstScan0;
                byte* pSrc = (byte*)(void*)srcScan0;

                for (int y = 0; y < destHeight; y++)
                {
                    for (int x = 0; x < destWidth; x++)
                    {
                        RGBProcessFunction(
                            pSrc[x * 3 + 2 + y * srcStride], 
                            pSrc[x * 3 + 1 + y * srcStride], 
                            pSrc[x * 3 + y * srcStride], 
                            ref pDst[x * 3 + 2 + y * dstStride], 
                            ref pDst[x * 3 + 1 + y * dstStride], 
                            ref pDst[x * 3 + y * dstStride]);
                    }
                }
            }

            src.UnlockBits(srcBD);
            dst.UnlockBits(dstBD);

            src.Dispose();

            return dst;
        }

        // Source AND Destination
        /// <summary>
        /// The source and.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte SourceAND(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(Src & Dst, 255), 0);
        }

        // Source AND (NOT Destination)
        /// <summary>
        /// The source erase.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte SourceErase(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(Src & (255 - Dst), 255), 0);
        }

        // Source XOR Destination
        /// <summary>
        /// The source invert.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte SourceInvert(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(Src ^ Dst, 255), 0);
        }

        // Source OR Destination
        /// <summary>
        /// The source paint.
        /// </summary>
        /// <param name="Src">
        /// The src.
        /// </param>
        /// <param name="Dst">
        /// The dst.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte SourcePaint(ref byte Src, ref byte Dst)
        {
            return (byte)Math.Max(Math.Min(Src | Dst, 255), 0);
        }

        #endregion

        // Choose darkest color 
    }
}