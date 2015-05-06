// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorBgra.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This is our pixel format that we will work with. It is always 32-bits / 4-bytes and is
//   always laid out in BGRA order.
//   Generally used with the Surface class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Reflection;
    using System.Runtime.InteropServices;

    using SIS.Library;

    /// <summary>
    ///     This is our pixel format that we will work with. It is always 32-bits / 4-bytes and is
    ///     always laid out in BGRA order.
    ///     Generally used with the Surface class.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct ColorBgra
    {
        /// <summary>
        /// The b.
        /// </summary>
        [FieldOffset(0)]
        public byte B;

        /// <summary>
        /// The g.
        /// </summary>
        [FieldOffset(1)]
        public byte G;

        /// <summary>
        /// The r.
        /// </summary>
        [FieldOffset(2)]
        public byte R;

        /// <summary>
        /// The a.
        /// </summary>
        [FieldOffset(3)]
        public byte A;

        /// <summary>
        ///     Lets you change B, G, R, and A at the same time.
        /// </summary>
        [NonSerialized]
        [FieldOffset(0)]
        public uint Bgra;

        /// <summary>
        /// The blue channel.
        /// </summary>
        public const int BlueChannel = 0;

        /// <summary>
        /// The green channel.
        /// </summary>
        public const int GreenChannel = 1;

        /// <summary>
        /// The red channel.
        /// </summary>
        public const int RedChannel = 2;

        /// <summary>
        /// The alpha channel.
        /// </summary>
        public const int AlphaChannel = 3;

        /// <summary>
        /// The size of.
        /// </summary>
        public const int SizeOf = 4;

        /// <summary>
        /// The parse hex string.
        /// </summary>
        /// <param name="hexString">
        /// The hex string.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra ParseHexString(string hexString)
        {
            uint value = Convert.ToUInt32(hexString, 16);
            return FromUInt32(value);
        }

        /// <summary>
        /// The to hex string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ToHexString()
        {
            int rgbNumber = (this.R << 16) | (this.G << 8) | this.B;
            string colorString = Convert.ToString(rgbNumber, 16);

            while (colorString.Length < 6)
            {
                colorString = "0" + colorString;
            }

            string alphaString = Convert.ToString(this.A, 16);

            while (alphaString.Length < 2)
            {
                alphaString = "0" + alphaString;
            }

            colorString = alphaString + colorString;

            return colorString.ToUpper();
        }

        /// <summary>
        /// Gets or sets the byte value of the specified color channel.
        /// </summary>
        /// <param name="channel">
        /// The channel.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public unsafe byte this[int channel]
        {
            get
            {
                if (channel < 0 || channel > 3)
                {
                    throw new ArgumentOutOfRangeException("channel", channel, "valid range is [0,3]");
                }

                fixed (byte* p = &this.B)
                {
                    return p[channel];
                }
            }

            set
            {
                if (channel < 0 || channel > 3)
                {
                    throw new ArgumentOutOfRangeException("channel", channel, "valid range is [0,3]");
                }

                fixed (byte* p = &this.B)
                {
                    p[channel] = value;
                }
            }
        }

        /// <summary>
        ///     Gets the luminance intensity of the pixel based on the values of the red, green, and blue components. Alpha is
        ///     ignored.
        /// </summary>
        /// <returns>A value in the range 0 to 1 inclusive.</returns>
        public double GetIntensity()
        {
            return ((0.114 * this.B) + (0.587 * this.G) + (0.299 * this.R)) / 255.0;
        }

        /// <summary>
        ///     Gets the luminance intensity of the pixel based on the values of the red, green, and blue components. Alpha is
        ///     ignored.
        /// </summary>
        /// <returns>A value in the range 0 to 255 inclusive.</returns>
        public byte GetIntensityByte()
        {
            return (byte)((7471 * this.B + 38470 * this.G + 19595 * this.R) >> 16);
        }

        /// <summary>
        /// Returns the maximum value out of the B, G, and R values. Alpha is ignored.
        /// </summary>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public byte GetMaxColorChannelValue()
        {
            return Math.Max(this.B, Math.Max(this.G, this.R));
        }

        /// <summary>
        /// Returns the average of the B, G, and R values. Alpha is ignored.
        /// </summary>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public byte GetAverageColorChannelValue()
        {
            return (byte)((this.B + this.G + this.R) / 3);
        }

        /// <summary>
        ///     Compares two ColorBgra instance to determine if they are equal.
        /// </summary>
        public static bool operator ==(ColorBgra lhs, ColorBgra rhs)
        {
            return lhs.Bgra == rhs.Bgra;
        }

        /// <summary>
        ///     Compares two ColorBgra instance to determine if they are not equal.
        /// </summary>
        public static bool operator !=(ColorBgra lhs, ColorBgra rhs)
        {
            return lhs.Bgra != rhs.Bgra;
        }

        /// <summary>
        /// Compares two ColorBgra instance to determine if they are equal.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is ColorBgra && ((ColorBgra)obj).Bgra == this.Bgra)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this color value.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (int)this.Bgra;
            }
        }

        /// <summary>
        ///     Gets the equivalent GDI+ PixelFormat.
        /// </summary>
        /// <remarks>
        ///     This property always returns PixelFormat.Format32bppArgb.
        /// </remarks>
        public static PixelFormat PixelFormat
        {
            get
            {
                return PixelFormat.Format32bppArgb;
            }
        }

        /// <summary>
        /// Returns a new ColorBgra with the same color values but with a new alpha component value.
        /// </summary>
        /// <param name="newA">
        /// The new A.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public ColorBgra NewAlpha(byte newA)
        {
            return FromBgra(this.B, this.G, this.R, newA);
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color and alpha values.
        /// </summary>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        [Obsolete("Use FromBgra() instead (make sure to swap the order of your b and r parameters)")]
        public static ColorBgra FromRgba(byte r, byte g, byte b, byte a)
        {
            return FromBgra(b, g, r, a);
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color values, and 255 for alpha.
        /// </summary>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        [Obsolete("Use FromBgr() instead (make sure to swap the order of your b and r parameters)")]
        public static ColorBgra FromRgb(byte r, byte g, byte b)
        {
            return FromBgr(b, g, r);
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color and alpha values.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromBgra(byte b, byte g, byte r, byte a)
        {
            var color = new ColorBgra();
            color.Bgra = BgraToUInt32(b, g, r, a);
            return color;
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color and alpha values.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromBgraClamped(int b, int g, int r, int a)
        {
            return FromBgra(
                Utility.ClampToByte(b), 
                Utility.ClampToByte(g), 
                Utility.ClampToByte(r), 
                Utility.ClampToByte(a));
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color and alpha values.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromBgraClamped(float b, float g, float r, float a)
        {
            return FromBgra(
                Utility.ClampToByte(b), 
                Utility.ClampToByte(g), 
                Utility.ClampToByte(r), 
                Utility.ClampToByte(a));
        }

        /// <summary>
        /// Packs color and alpha values into a 32-bit integer.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        public static uint BgraToUInt32(byte b, byte g, byte r, byte a)
        {
            return b + ((uint)g << 8) + ((uint)r << 16) + ((uint)a << 24);
        }

        /// <summary>
        /// Packs color and alpha values into a 32-bit integer.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        public static uint BgraToUInt32(int b, int g, int r, int a)
        {
            return (uint)b + ((uint)g << 8) + ((uint)r << 16) + ((uint)a << 24);
        }

        /// <summary>
        /// Creates a new ColorBgra instance with the given color values, and 255 for alpha.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="r">
        /// The r.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromBgr(byte b, byte g, byte r)
        {
            return FromBgra(b, g, r, 255);
        }

        /// <summary>
        /// Constructs a new ColorBgra instance with the given 32-bit value.
        /// </summary>
        /// <param name="bgra">
        /// The bgra.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromUInt32(uint bgra)
        {
            var color = new ColorBgra();
            color.Bgra = bgra;
            return color;
        }

        /// <summary>
        /// Constructs a new ColorBgra instance given a 32-bit signed integer that represents an R,G,B triple.
        ///     Alpha will be initialized to 255.
        /// </summary>
        /// <param name="bgr">
        /// The bgr.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromOpaqueInt32(int bgr)
        {
            if (bgr < 0 || bgr > 0xffffff)
            {
                throw new ArgumentOutOfRangeException("bgr", "must be in the range [0, 0xffffff]");
            }

            var color = new ColorBgra();
            color.Bgra = (uint)bgr;
            color.A = 255;

            return color;
        }

        /// <summary>
        /// The to opaque int 32.
        /// </summary>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static int ToOpaqueInt32(ColorBgra color)
        {
            if (color.A != 255)
            {
                throw new InvalidOperationException("Alpha value must be 255 for this to work");
            }

            return (int)(color.Bgra & 0xffffff);
        }

        /// <summary>
        /// Constructs a new ColorBgra instance from the values in the given Color instance.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra FromColor(Color c)
        {
            return FromBgra(c.B, c.G, c.R, c.A);
        }

        /// <summary>
        /// Converts this ColorBgra instance to a Color instance.
        /// </summary>
        /// <returns>
        /// The <see cref="Color"/>.
        /// </returns>
        public Color ToColor()
        {
            return Color.FromArgb(this.A, this.R, this.G, this.B);
        }

        /// <summary>
        /// Smoothly blends between two colors.
        /// </summary>
        /// <param name="ca">
        /// The ca.
        /// </param>
        /// <param name="cb">
        /// The cb.
        /// </param>
        /// <param name="cbAlpha">
        /// The cb Alpha.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra Blend(ColorBgra ca, ColorBgra cb, byte cbAlpha)
        {
            // uint caA = (uint)Utility.FastScaleByteByByte((byte)(255 - cbAlpha), ca.A);
            uint caA = 1;

            // uint cbA = (uint)Utility.FastScaleByteByByte(cbAlpha, cb.A);
            uint cbA = 2;
            uint cbAT = caA + cbA;

            uint r;
            uint g;
            uint b;

            if (cbAT == 0)
            {
                r = 0;
                g = 0;
                b = 0;
            }
            else
            {
                r = ((ca.R * caA) + (cb.R * cbA)) / cbAT;
                g = ((ca.G * caA) + (cb.G * cbA)) / cbAT;
                b = ((ca.B * caA) + (cb.B * cbA)) / cbAT;
            }

            return FromBgra((byte)b, (byte)g, (byte)r, (byte)cbAT);
        }

        /// <summary>
        /// Linearly interpolates between two color values.
        /// </summary>
        /// <param name="from">
        /// The color value that represents 0 on the lerp number line.
        /// </param>
        /// <param name="to">
        /// The color value that represents 1 on the lerp number line.
        /// </param>
        /// <param name="frac">
        /// A value in the range [0, 1].
        /// </param>
        /// <remarks>
        /// This method does a simple lerp on each color value and on the alpha channel. It does
        ///     not properly take into account the alpha channel's effect on color blending.
        /// </remarks>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra Lerp(ColorBgra from, ColorBgra to, float frac)
        {
            var ret = new ColorBgra();

            ret.B = Utility.ClampToByte(Utility.Lerp(@from.B, to.B, frac));
            ret.G = Utility.ClampToByte(Utility.Lerp(@from.G, to.G, frac));
            ret.R = Utility.ClampToByte(Utility.Lerp(@from.R, to.R, frac));
            ret.A = Utility.ClampToByte(Utility.Lerp(@from.A, to.A, frac));

            return ret;
        }

        /// <summary>
        /// Linearly interpolates between two color values.
        /// </summary>
        /// <param name="from">
        /// The color value that represents 0 on the lerp number line.
        /// </param>
        /// <param name="to">
        /// The color value that represents 1 on the lerp number line.
        /// </param>
        /// <param name="frac">
        /// A value in the range [0, 1].
        /// </param>
        /// <remarks>
        /// This method does a simple lerp on each color value and on the alpha channel. It does
        ///     not properly take into account the alpha channel's effect on color blending.
        /// </remarks>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra Lerp(ColorBgra from, ColorBgra to, double frac)
        {
            var ret = new ColorBgra();

            ret.B = Utility.ClampToByte(Utility.Lerp(@from.B, to.B, frac));
            ret.G = Utility.ClampToByte(Utility.Lerp(@from.G, to.G, frac));
            ret.R = Utility.ClampToByte(Utility.Lerp(@from.R, to.R, frac));
            ret.A = Utility.ClampToByte(Utility.Lerp(@from.A, to.A, frac));

            return ret;
        }

        /// <summary>
        /// Blends four colors together based on the given weight values.
        /// </summary>
        /// <param name="c1">
        /// The c 1.
        /// </param>
        /// <param name="w1">
        /// The w 1.
        /// </param>
        /// <param name="c2">
        /// The c 2.
        /// </param>
        /// <param name="w2">
        /// The w 2.
        /// </param>
        /// <param name="c3">
        /// The c 3.
        /// </param>
        /// <param name="w3">
        /// The w 3.
        /// </param>
        /// <param name="c4">
        /// The c 4.
        /// </param>
        /// <param name="w4">
        /// The w 4.
        /// </param>
        /// <returns>
        /// The blended color.
        /// </returns>
        /// <remarks>
        /// The weights should be 16-bit fixed point numbers that add up to 65536 ("1.0").
        ///     4W16IP means "4 colors, weights, 16-bit integer precision"
        /// </remarks>
        public static ColorBgra BlendColors4W16IP(
            ColorBgra c1, 
            uint w1, 
            ColorBgra c2, 
            uint w2, 
            ColorBgra c3, 
            uint w3, 
            ColorBgra c4, 
            uint w4)
        {
#if DEBUG

            /*
            if ((w1 + w2 + w3 + w4) != 65536)
            {
                throw new ArgumentOutOfRangeException("w1 + w2 + w3 + w4 must equal 65536!");
            }
             * */
#endif

            const uint ww = 32768;
            uint af = (c1.A * w1) + (c2.A * w2) + (c3.A * w3) + (c4.A * w4);
            uint a = (af + ww) >> 16;

            uint b;
            uint g;
            uint r;

            if (a == 0)
            {
                b = 0;
                g = 0;
                r = 0;
            }
            else
            {
                b =
                    (uint)
                    ((((long)c1.A * c1.B * w1) + ((long)c2.A * c2.B * w2) + ((long)c3.A * c3.B * w3)
                      + ((long)c4.A * c4.B * w4)) / af);
                g =
                    (uint)
                    ((((long)c1.A * c1.G * w1) + ((long)c2.A * c2.G * w2) + ((long)c3.A * c3.G * w3)
                      + ((long)c4.A * c4.G * w4)) / af);
                r =
                    (uint)
                    ((((long)c1.A * c1.R * w1) + ((long)c2.A * c2.R * w2) + ((long)c3.A * c3.R * w3)
                      + ((long)c4.A * c4.R * w4)) / af);
            }

            return FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
        }

        /// <summary>
        /// Blends the colors based on the given weight values.
        /// </summary>
        /// <param name="c">
        /// The array of color values.
        /// </param>
        /// <param name="w">
        /// The array of weight values.
        /// </param>
        /// <returns>
        /// The weights should be fixed point numbers.
        ///     The total summation of the weight values will be treated as "1.0".
        ///     Each color will be blended in proportionally to its weight value respective to
        ///     the total summation of the weight values.
        /// </returns>
        /// <remarks>
        /// "WAIP" stands for "weights, arbitrary integer precision"
        /// </remarks>
        public static ColorBgra BlendColorsWAIP(ColorBgra[] c, uint[] w)
        {
            if (c.Length != w.Length)
            {
                throw new ArgumentException("c.Length != w.Length");
            }

            if (c.Length == 0)
            {
                return FromUInt32(0);
            }

            long wsum = 0;
            long asum = 0;

            for (int i = 0; i < w.Length; ++i)
            {
                wsum += w[i];
                asum += c[i].A * w[i];
            }

            var a = (uint)((asum + (wsum >> 1)) / wsum);

            long b;
            long g;
            long r;

            if (a == 0)
            {
                b = 0;
                g = 0;
                r = 0;
            }
            else
            {
                b = 0;
                g = 0;
                r = 0;

                for (int i = 0; i < c.Length; ++i)
                {
                    b += (long)c[i].A * c[i].B * w[i];
                    g += (long)c[i].A * c[i].G * w[i];
                    r += (long)c[i].A * c[i].R * w[i];
                }

                b /= asum;
                g /= asum;
                r /= asum;
            }

            return FromUInt32((uint)b + ((uint)g << 8) + ((uint)r << 16) + (a << 24));
        }

        /// <summary>
        /// Blends the colors based on the given weight values.
        /// </summary>
        /// <param name="c">
        /// The array of color values.
        /// </param>
        /// <param name="w">
        /// The array of weight values.
        /// </param>
        /// <returns>
        /// Each color will be blended in proportionally to its weight value respective to
        ///     the total summation of the weight values.
        /// </returns>
        /// <remarks>
        /// "WAIP" stands for "weights, floating-point"
        /// </remarks>
        public static ColorBgra BlendColorsWFP(ColorBgra[] c, double[] w)
        {
            if (c.Length != w.Length)
            {
                throw new ArgumentException("c.Length != w.Length");
            }

            if (c.Length == 0)
            {
                return Transparent;
            }

            double wsum = 0;
            double asum = 0;

            for (int i = 0; i < w.Length; ++i)
            {
                wsum += w[i];
                asum += c[i].A * w[i];
            }

            double a = asum / wsum;
            double aMultWsum = a * wsum;

            double b;
            double g;
            double r;

            if (asum == 0)
            {
                b = 0;
                g = 0;
                r = 0;
            }
            else
            {
                b = 0;
                g = 0;
                r = 0;

                for (int i = 0; i < c.Length; ++i)
                {
                    b += (double)c[i].A * c[i].B * w[i];
                    g += (double)c[i].A * c[i].G * w[i];
                    r += (double)c[i].A * c[i].R * w[i];
                }

                b /= aMultWsum;
                g /= aMultWsum;
                r /= aMultWsum;
            }

            return FromBgra((byte)b, (byte)g, (byte)r, (byte)a);
        }

        /// <summary>
        /// The blend.
        /// </summary>
        /// <param name="colors">
        /// The colors.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static ColorBgra Blend(ColorBgra[] colors)
        {
            unsafe
            {
                fixed (ColorBgra* pColors = colors)
                {
                    return Blend(pColors, colors.Length);
                }
            }
        }

        /// <summary>
        /// Smoothly blends the given colors together, assuming equal weighting for each one.
        /// </summary>
        /// <param name="colors">
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="ColorBgra"/>.
        /// </returns>
        public static unsafe ColorBgra Blend(ColorBgra* colors, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count must be 0 or greater");
            }

            if (count == 0)
            {
                return Transparent;
            }

            ulong aSum = 0;

            for (int i = 0; i < count; ++i)
            {
                aSum += colors[i].A;
            }

            byte b = 0;
            byte g = 0;
            byte r = 0;
            var a = (byte)(aSum / (ulong)count);

            if (aSum != 0)
            {
                ulong bSum = 0;
                ulong gSum = 0;
                ulong rSum = 0;

                for (int i = 0; i < count; ++i)
                {
                    bSum += (ulong)(colors[i].A * colors[i].B);
                    gSum += (ulong)(colors[i].A * colors[i].G);
                    rSum += (ulong)(colors[i].A * colors[i].R);
                }

                b = (byte)(bSum / aSum);
                g = (byte)(gSum / aSum);
                r = (byte)(rSum / aSum);
            }

            return FromBgra(b, g, r, a);
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return "B: " + this.B + ", G: " + this.G + ", R: " + this.R + ", A: " + this.A;
        }

        /// <summary>
        ///     Casts a ColorBgra to a UInt32.
        /// </summary>
        public static explicit operator UInt32(ColorBgra color)
        {
            return color.Bgra;
        }

        /// <summary>
        ///     Casts a UInt32 to a ColorBgra.
        /// </summary>
        public static explicit operator ColorBgra(uint uint32)
        {
            return FromUInt32(uint32);
        }

        // Colors: copied from System.Drawing.Color's list (don't worry I didn't type it in 
        // manually, I used a code generator w/ reflection ...)

        /// <summary>
        /// Gets the transparent.
        /// </summary>
        public static ColorBgra Transparent
        {
            get
            {
                return FromBgra(255, 255, 255, 0);
            }
        }

        /// <summary>
        /// Gets the alice blue.
        /// </summary>
        public static ColorBgra AliceBlue
        {
            get
            {
                return FromBgra(255, 248, 240, 255);
            }
        }

        /// <summary>
        /// Gets the antique white.
        /// </summary>
        public static ColorBgra AntiqueWhite
        {
            get
            {
                return FromBgra(215, 235, 250, 255);
            }
        }

        /// <summary>
        /// Gets the aqua.
        /// </summary>
        public static ColorBgra Aqua
        {
            get
            {
                return FromBgra(255, 255, 0, 255);
            }
        }

        /// <summary>
        /// Gets the aquamarine.
        /// </summary>
        public static ColorBgra Aquamarine
        {
            get
            {
                return FromBgra(212, 255, 127, 255);
            }
        }

        /// <summary>
        /// Gets the azure.
        /// </summary>
        public static ColorBgra Azure
        {
            get
            {
                return FromBgra(255, 255, 240, 255);
            }
        }

        /// <summary>
        /// Gets the beige.
        /// </summary>
        public static ColorBgra Beige
        {
            get
            {
                return FromBgra(220, 245, 245, 255);
            }
        }

        /// <summary>
        /// Gets the bisque.
        /// </summary>
        public static ColorBgra Bisque
        {
            get
            {
                return FromBgra(196, 228, 255, 255);
            }
        }

        /// <summary>
        /// Gets the black.
        /// </summary>
        public static ColorBgra Black
        {
            get
            {
                return FromBgra(0, 0, 0, 255);
            }
        }

        /// <summary>
        /// Gets the blanched almond.
        /// </summary>
        public static ColorBgra BlanchedAlmond
        {
            get
            {
                return FromBgra(205, 235, 255, 255);
            }
        }

        /// <summary>
        /// Gets the blue.
        /// </summary>
        public static ColorBgra Blue
        {
            get
            {
                return FromBgra(255, 0, 0, 255);
            }
        }

        /// <summary>
        /// Gets the blue violet.
        /// </summary>
        public static ColorBgra BlueViolet
        {
            get
            {
                return FromBgra(226, 43, 138, 255);
            }
        }

        /// <summary>
        /// Gets the brown.
        /// </summary>
        public static ColorBgra Brown
        {
            get
            {
                return FromBgra(42, 42, 165, 255);
            }
        }

        /// <summary>
        /// Gets the burly wood.
        /// </summary>
        public static ColorBgra BurlyWood
        {
            get
            {
                return FromBgra(135, 184, 222, 255);
            }
        }

        /// <summary>
        /// Gets the cadet blue.
        /// </summary>
        public static ColorBgra CadetBlue
        {
            get
            {
                return FromBgra(160, 158, 95, 255);
            }
        }

        /// <summary>
        /// Gets the chartreuse.
        /// </summary>
        public static ColorBgra Chartreuse
        {
            get
            {
                return FromBgra(0, 255, 127, 255);
            }
        }

        /// <summary>
        /// Gets the chocolate.
        /// </summary>
        public static ColorBgra Chocolate
        {
            get
            {
                return FromBgra(30, 105, 210, 255);
            }
        }

        /// <summary>
        /// Gets the coral.
        /// </summary>
        public static ColorBgra Coral
        {
            get
            {
                return FromBgra(80, 127, 255, 255);
            }
        }

        /// <summary>
        /// Gets the cornflower blue.
        /// </summary>
        public static ColorBgra CornflowerBlue
        {
            get
            {
                return FromBgra(237, 149, 100, 255);
            }
        }

        /// <summary>
        /// Gets the cornsilk.
        /// </summary>
        public static ColorBgra Cornsilk
        {
            get
            {
                return FromBgra(220, 248, 255, 255);
            }
        }

        /// <summary>
        /// Gets the crimson.
        /// </summary>
        public static ColorBgra Crimson
        {
            get
            {
                return FromBgra(60, 20, 220, 255);
            }
        }

        /// <summary>
        /// Gets the cyan.
        /// </summary>
        public static ColorBgra Cyan
        {
            get
            {
                return FromBgra(255, 255, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dark blue.
        /// </summary>
        public static ColorBgra DarkBlue
        {
            get
            {
                return FromBgra(139, 0, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dark cyan.
        /// </summary>
        public static ColorBgra DarkCyan
        {
            get
            {
                return FromBgra(139, 139, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dark goldenrod.
        /// </summary>
        public static ColorBgra DarkGoldenrod
        {
            get
            {
                return FromBgra(11, 134, 184, 255);
            }
        }

        /// <summary>
        /// Gets the dark gray.
        /// </summary>
        public static ColorBgra DarkGray
        {
            get
            {
                return FromBgra(169, 169, 169, 255);
            }
        }

        /// <summary>
        /// Gets the dark green.
        /// </summary>
        public static ColorBgra DarkGreen
        {
            get
            {
                return FromBgra(0, 100, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dark khaki.
        /// </summary>
        public static ColorBgra DarkKhaki
        {
            get
            {
                return FromBgra(107, 183, 189, 255);
            }
        }

        /// <summary>
        /// Gets the dark magenta.
        /// </summary>
        public static ColorBgra DarkMagenta
        {
            get
            {
                return FromBgra(139, 0, 139, 255);
            }
        }

        /// <summary>
        /// Gets the dark olive green.
        /// </summary>
        public static ColorBgra DarkOliveGreen
        {
            get
            {
                return FromBgra(47, 107, 85, 255);
            }
        }

        /// <summary>
        /// Gets the dark orange.
        /// </summary>
        public static ColorBgra DarkOrange
        {
            get
            {
                return FromBgra(0, 140, 255, 255);
            }
        }

        /// <summary>
        /// Gets the dark orchid.
        /// </summary>
        public static ColorBgra DarkOrchid
        {
            get
            {
                return FromBgra(204, 50, 153, 255);
            }
        }

        /// <summary>
        /// Gets the dark red.
        /// </summary>
        public static ColorBgra DarkRed
        {
            get
            {
                return FromBgra(0, 0, 139, 255);
            }
        }

        /// <summary>
        /// Gets the dark salmon.
        /// </summary>
        public static ColorBgra DarkSalmon
        {
            get
            {
                return FromBgra(122, 150, 233, 255);
            }
        }

        /// <summary>
        /// Gets the dark sea green.
        /// </summary>
        public static ColorBgra DarkSeaGreen
        {
            get
            {
                return FromBgra(139, 188, 143, 255);
            }
        }

        /// <summary>
        /// Gets the dark slate blue.
        /// </summary>
        public static ColorBgra DarkSlateBlue
        {
            get
            {
                return FromBgra(139, 61, 72, 255);
            }
        }

        /// <summary>
        /// Gets the dark slate gray.
        /// </summary>
        public static ColorBgra DarkSlateGray
        {
            get
            {
                return FromBgra(79, 79, 47, 255);
            }
        }

        /// <summary>
        /// Gets the dark turquoise.
        /// </summary>
        public static ColorBgra DarkTurquoise
        {
            get
            {
                return FromBgra(209, 206, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dark violet.
        /// </summary>
        public static ColorBgra DarkViolet
        {
            get
            {
                return FromBgra(211, 0, 148, 255);
            }
        }

        /// <summary>
        /// Gets the deep pink.
        /// </summary>
        public static ColorBgra DeepPink
        {
            get
            {
                return FromBgra(147, 20, 255, 255);
            }
        }

        /// <summary>
        /// Gets the deep sky blue.
        /// </summary>
        public static ColorBgra DeepSkyBlue
        {
            get
            {
                return FromBgra(255, 191, 0, 255);
            }
        }

        /// <summary>
        /// Gets the dim gray.
        /// </summary>
        public static ColorBgra DimGray
        {
            get
            {
                return FromBgra(105, 105, 105, 255);
            }
        }

        /// <summary>
        /// Gets the dodger blue.
        /// </summary>
        public static ColorBgra DodgerBlue
        {
            get
            {
                return FromBgra(255, 144, 30, 255);
            }
        }

        /// <summary>
        /// Gets the firebrick.
        /// </summary>
        public static ColorBgra Firebrick
        {
            get
            {
                return FromBgra(34, 34, 178, 255);
            }
        }

        /// <summary>
        /// Gets the floral white.
        /// </summary>
        public static ColorBgra FloralWhite
        {
            get
            {
                return FromBgra(240, 250, 255, 255);
            }
        }

        /// <summary>
        /// Gets the forest green.
        /// </summary>
        public static ColorBgra ForestGreen
        {
            get
            {
                return FromBgra(34, 139, 34, 255);
            }
        }

        /// <summary>
        /// Gets the fuchsia.
        /// </summary>
        public static ColorBgra Fuchsia
        {
            get
            {
                return FromBgra(255, 0, 255, 255);
            }
        }

        /// <summary>
        /// Gets the gainsboro.
        /// </summary>
        public static ColorBgra Gainsboro
        {
            get
            {
                return FromBgra(220, 220, 220, 255);
            }
        }

        /// <summary>
        /// Gets the ghost white.
        /// </summary>
        public static ColorBgra GhostWhite
        {
            get
            {
                return FromBgra(255, 248, 248, 255);
            }
        }

        /// <summary>
        /// Gets the gold.
        /// </summary>
        public static ColorBgra Gold
        {
            get
            {
                return FromBgra(0, 215, 255, 255);
            }
        }

        /// <summary>
        /// Gets the goldenrod.
        /// </summary>
        public static ColorBgra Goldenrod
        {
            get
            {
                return FromBgra(32, 165, 218, 255);
            }
        }

        /// <summary>
        /// Gets the gray.
        /// </summary>
        public static ColorBgra Gray
        {
            get
            {
                return FromBgra(128, 128, 128, 255);
            }
        }

        /// <summary>
        /// Gets the green.
        /// </summary>
        public static ColorBgra Green
        {
            get
            {
                return FromBgra(0, 128, 0, 255);
            }
        }

        /// <summary>
        /// Gets the green yellow.
        /// </summary>
        public static ColorBgra GreenYellow
        {
            get
            {
                return FromBgra(47, 255, 173, 255);
            }
        }

        /// <summary>
        /// Gets the honeydew.
        /// </summary>
        public static ColorBgra Honeydew
        {
            get
            {
                return FromBgra(240, 255, 240, 255);
            }
        }

        /// <summary>
        /// Gets the hot pink.
        /// </summary>
        public static ColorBgra HotPink
        {
            get
            {
                return FromBgra(180, 105, 255, 255);
            }
        }

        /// <summary>
        /// Gets the indian red.
        /// </summary>
        public static ColorBgra IndianRed
        {
            get
            {
                return FromBgra(92, 92, 205, 255);
            }
        }

        /// <summary>
        /// Gets the indigo.
        /// </summary>
        public static ColorBgra Indigo
        {
            get
            {
                return FromBgra(130, 0, 75, 255);
            }
        }

        /// <summary>
        /// Gets the ivory.
        /// </summary>
        public static ColorBgra Ivory
        {
            get
            {
                return FromBgra(240, 255, 255, 255);
            }
        }

        /// <summary>
        /// Gets the khaki.
        /// </summary>
        public static ColorBgra Khaki
        {
            get
            {
                return FromBgra(140, 230, 240, 255);
            }
        }

        /// <summary>
        /// Gets the lavender.
        /// </summary>
        public static ColorBgra Lavender
        {
            get
            {
                return FromBgra(250, 230, 230, 255);
            }
        }

        /// <summary>
        /// Gets the lavender blush.
        /// </summary>
        public static ColorBgra LavenderBlush
        {
            get
            {
                return FromBgra(245, 240, 255, 255);
            }
        }

        /// <summary>
        /// Gets the lawn green.
        /// </summary>
        public static ColorBgra LawnGreen
        {
            get
            {
                return FromBgra(0, 252, 124, 255);
            }
        }

        /// <summary>
        /// Gets the lemon chiffon.
        /// </summary>
        public static ColorBgra LemonChiffon
        {
            get
            {
                return FromBgra(205, 250, 255, 255);
            }
        }

        /// <summary>
        /// Gets the light blue.
        /// </summary>
        public static ColorBgra LightBlue
        {
            get
            {
                return FromBgra(230, 216, 173, 255);
            }
        }

        /// <summary>
        /// Gets the light coral.
        /// </summary>
        public static ColorBgra LightCoral
        {
            get
            {
                return FromBgra(128, 128, 240, 255);
            }
        }

        /// <summary>
        /// Gets the light cyan.
        /// </summary>
        public static ColorBgra LightCyan
        {
            get
            {
                return FromBgra(255, 255, 224, 255);
            }
        }

        /// <summary>
        /// Gets the light goldenrod yellow.
        /// </summary>
        public static ColorBgra LightGoldenrodYellow
        {
            get
            {
                return FromBgra(210, 250, 250, 255);
            }
        }

        /// <summary>
        /// Gets the light green.
        /// </summary>
        public static ColorBgra LightGreen
        {
            get
            {
                return FromBgra(144, 238, 144, 255);
            }
        }

        /// <summary>
        /// Gets the light gray.
        /// </summary>
        public static ColorBgra LightGray
        {
            get
            {
                return FromBgra(211, 211, 211, 255);
            }
        }

        /// <summary>
        /// Gets the light pink.
        /// </summary>
        public static ColorBgra LightPink
        {
            get
            {
                return FromBgra(193, 182, 255, 255);
            }
        }

        /// <summary>
        /// Gets the light salmon.
        /// </summary>
        public static ColorBgra LightSalmon
        {
            get
            {
                return FromBgra(122, 160, 255, 255);
            }
        }

        /// <summary>
        /// Gets the light sea green.
        /// </summary>
        public static ColorBgra LightSeaGreen
        {
            get
            {
                return FromBgra(170, 178, 32, 255);
            }
        }

        /// <summary>
        /// Gets the light sky blue.
        /// </summary>
        public static ColorBgra LightSkyBlue
        {
            get
            {
                return FromBgra(250, 206, 135, 255);
            }
        }

        /// <summary>
        /// Gets the light slate gray.
        /// </summary>
        public static ColorBgra LightSlateGray
        {
            get
            {
                return FromBgra(153, 136, 119, 255);
            }
        }

        /// <summary>
        /// Gets the light steel blue.
        /// </summary>
        public static ColorBgra LightSteelBlue
        {
            get
            {
                return FromBgra(222, 196, 176, 255);
            }
        }

        /// <summary>
        /// Gets the light yellow.
        /// </summary>
        public static ColorBgra LightYellow
        {
            get
            {
                return FromBgra(224, 255, 255, 255);
            }
        }

        /// <summary>
        /// Gets the lime.
        /// </summary>
        public static ColorBgra Lime
        {
            get
            {
                return FromBgra(0, 255, 0, 255);
            }
        }

        /// <summary>
        /// Gets the lime green.
        /// </summary>
        public static ColorBgra LimeGreen
        {
            get
            {
                return FromBgra(50, 205, 50, 255);
            }
        }

        /// <summary>
        /// Gets the linen.
        /// </summary>
        public static ColorBgra Linen
        {
            get
            {
                return FromBgra(230, 240, 250, 255);
            }
        }

        /// <summary>
        /// Gets the magenta.
        /// </summary>
        public static ColorBgra Magenta
        {
            get
            {
                return FromBgra(255, 0, 255, 255);
            }
        }

        /// <summary>
        /// Gets the maroon.
        /// </summary>
        public static ColorBgra Maroon
        {
            get
            {
                return FromBgra(0, 0, 128, 255);
            }
        }

        /// <summary>
        /// Gets the medium aquamarine.
        /// </summary>
        public static ColorBgra MediumAquamarine
        {
            get
            {
                return FromBgra(170, 205, 102, 255);
            }
        }

        /// <summary>
        /// Gets the medium blue.
        /// </summary>
        public static ColorBgra MediumBlue
        {
            get
            {
                return FromBgra(205, 0, 0, 255);
            }
        }

        /// <summary>
        /// Gets the medium orchid.
        /// </summary>
        public static ColorBgra MediumOrchid
        {
            get
            {
                return FromBgra(211, 85, 186, 255);
            }
        }

        /// <summary>
        /// Gets the medium purple.
        /// </summary>
        public static ColorBgra MediumPurple
        {
            get
            {
                return FromBgra(219, 112, 147, 255);
            }
        }

        /// <summary>
        /// Gets the medium sea green.
        /// </summary>
        public static ColorBgra MediumSeaGreen
        {
            get
            {
                return FromBgra(113, 179, 60, 255);
            }
        }

        /// <summary>
        /// Gets the medium slate blue.
        /// </summary>
        public static ColorBgra MediumSlateBlue
        {
            get
            {
                return FromBgra(238, 104, 123, 255);
            }
        }

        /// <summary>
        /// Gets the medium spring green.
        /// </summary>
        public static ColorBgra MediumSpringGreen
        {
            get
            {
                return FromBgra(154, 250, 0, 255);
            }
        }

        /// <summary>
        /// Gets the medium turquoise.
        /// </summary>
        public static ColorBgra MediumTurquoise
        {
            get
            {
                return FromBgra(204, 209, 72, 255);
            }
        }

        /// <summary>
        /// Gets the medium violet red.
        /// </summary>
        public static ColorBgra MediumVioletRed
        {
            get
            {
                return FromBgra(133, 21, 199, 255);
            }
        }

        /// <summary>
        /// Gets the midnight blue.
        /// </summary>
        public static ColorBgra MidnightBlue
        {
            get
            {
                return FromBgra(112, 25, 25, 255);
            }
        }

        /// <summary>
        /// Gets the mint cream.
        /// </summary>
        public static ColorBgra MintCream
        {
            get
            {
                return FromBgra(250, 255, 245, 255);
            }
        }

        /// <summary>
        /// Gets the misty rose.
        /// </summary>
        public static ColorBgra MistyRose
        {
            get
            {
                return FromBgra(225, 228, 255, 255);
            }
        }

        /// <summary>
        /// Gets the moccasin.
        /// </summary>
        public static ColorBgra Moccasin
        {
            get
            {
                return FromBgra(181, 228, 255, 255);
            }
        }

        /// <summary>
        /// Gets the navajo white.
        /// </summary>
        public static ColorBgra NavajoWhite
        {
            get
            {
                return FromBgra(173, 222, 255, 255);
            }
        }

        /// <summary>
        /// Gets the navy.
        /// </summary>
        public static ColorBgra Navy
        {
            get
            {
                return FromBgra(128, 0, 0, 255);
            }
        }

        /// <summary>
        /// Gets the old lace.
        /// </summary>
        public static ColorBgra OldLace
        {
            get
            {
                return FromBgra(230, 245, 253, 255);
            }
        }

        /// <summary>
        /// Gets the olive.
        /// </summary>
        public static ColorBgra Olive
        {
            get
            {
                return FromBgra(0, 128, 128, 255);
            }
        }

        /// <summary>
        /// Gets the olive drab.
        /// </summary>
        public static ColorBgra OliveDrab
        {
            get
            {
                return FromBgra(35, 142, 107, 255);
            }
        }

        /// <summary>
        /// Gets the orange.
        /// </summary>
        public static ColorBgra Orange
        {
            get
            {
                return FromBgra(0, 165, 255, 255);
            }
        }

        /// <summary>
        /// Gets the orange red.
        /// </summary>
        public static ColorBgra OrangeRed
        {
            get
            {
                return FromBgra(0, 69, 255, 255);
            }
        }

        /// <summary>
        /// Gets the orchid.
        /// </summary>
        public static ColorBgra Orchid
        {
            get
            {
                return FromBgra(214, 112, 218, 255);
            }
        }

        /// <summary>
        /// Gets the pale goldenrod.
        /// </summary>
        public static ColorBgra PaleGoldenrod
        {
            get
            {
                return FromBgra(170, 232, 238, 255);
            }
        }

        /// <summary>
        /// Gets the pale green.
        /// </summary>
        public static ColorBgra PaleGreen
        {
            get
            {
                return FromBgra(152, 251, 152, 255);
            }
        }

        /// <summary>
        /// Gets the pale turquoise.
        /// </summary>
        public static ColorBgra PaleTurquoise
        {
            get
            {
                return FromBgra(238, 238, 175, 255);
            }
        }

        /// <summary>
        /// Gets the pale violet red.
        /// </summary>
        public static ColorBgra PaleVioletRed
        {
            get
            {
                return FromBgra(147, 112, 219, 255);
            }
        }

        /// <summary>
        /// Gets the papaya whip.
        /// </summary>
        public static ColorBgra PapayaWhip
        {
            get
            {
                return FromBgra(213, 239, 255, 255);
            }
        }

        /// <summary>
        /// Gets the peach puff.
        /// </summary>
        public static ColorBgra PeachPuff
        {
            get
            {
                return FromBgra(185, 218, 255, 255);
            }
        }

        /// <summary>
        /// Gets the peru.
        /// </summary>
        public static ColorBgra Peru
        {
            get
            {
                return FromBgra(63, 133, 205, 255);
            }
        }

        /// <summary>
        /// Gets the pink.
        /// </summary>
        public static ColorBgra Pink
        {
            get
            {
                return FromBgra(203, 192, 255, 255);
            }
        }

        /// <summary>
        /// Gets the plum.
        /// </summary>
        public static ColorBgra Plum
        {
            get
            {
                return FromBgra(221, 160, 221, 255);
            }
        }

        /// <summary>
        /// Gets the powder blue.
        /// </summary>
        public static ColorBgra PowderBlue
        {
            get
            {
                return FromBgra(230, 224, 176, 255);
            }
        }

        /// <summary>
        /// Gets the purple.
        /// </summary>
        public static ColorBgra Purple
        {
            get
            {
                return FromBgra(128, 0, 128, 255);
            }
        }

        /// <summary>
        /// Gets the red.
        /// </summary>
        public static ColorBgra Red
        {
            get
            {
                return FromBgra(0, 0, 255, 255);
            }
        }

        /// <summary>
        /// Gets the rosy brown.
        /// </summary>
        public static ColorBgra RosyBrown
        {
            get
            {
                return FromBgra(143, 143, 188, 255);
            }
        }

        /// <summary>
        /// Gets the royal blue.
        /// </summary>
        public static ColorBgra RoyalBlue
        {
            get
            {
                return FromBgra(225, 105, 65, 255);
            }
        }

        /// <summary>
        /// Gets the saddle brown.
        /// </summary>
        public static ColorBgra SaddleBrown
        {
            get
            {
                return FromBgra(19, 69, 139, 255);
            }
        }

        /// <summary>
        /// Gets the salmon.
        /// </summary>
        public static ColorBgra Salmon
        {
            get
            {
                return FromBgra(114, 128, 250, 255);
            }
        }

        /// <summary>
        /// Gets the sandy brown.
        /// </summary>
        public static ColorBgra SandyBrown
        {
            get
            {
                return FromBgra(96, 164, 244, 255);
            }
        }

        /// <summary>
        /// Gets the sea green.
        /// </summary>
        public static ColorBgra SeaGreen
        {
            get
            {
                return FromBgra(87, 139, 46, 255);
            }
        }

        /// <summary>
        /// Gets the sea shell.
        /// </summary>
        public static ColorBgra SeaShell
        {
            get
            {
                return FromBgra(238, 245, 255, 255);
            }
        }

        /// <summary>
        /// Gets the sienna.
        /// </summary>
        public static ColorBgra Sienna
        {
            get
            {
                return FromBgra(45, 82, 160, 255);
            }
        }

        /// <summary>
        /// Gets the silver.
        /// </summary>
        public static ColorBgra Silver
        {
            get
            {
                return FromBgra(192, 192, 192, 255);
            }
        }

        /// <summary>
        /// Gets the sky blue.
        /// </summary>
        public static ColorBgra SkyBlue
        {
            get
            {
                return FromBgra(235, 206, 135, 255);
            }
        }

        /// <summary>
        /// Gets the slate blue.
        /// </summary>
        public static ColorBgra SlateBlue
        {
            get
            {
                return FromBgra(205, 90, 106, 255);
            }
        }

        /// <summary>
        /// Gets the slate gray.
        /// </summary>
        public static ColorBgra SlateGray
        {
            get
            {
                return FromBgra(144, 128, 112, 255);
            }
        }

        /// <summary>
        /// Gets the snow.
        /// </summary>
        public static ColorBgra Snow
        {
            get
            {
                return FromBgra(250, 250, 255, 255);
            }
        }

        /// <summary>
        /// Gets the spring green.
        /// </summary>
        public static ColorBgra SpringGreen
        {
            get
            {
                return FromBgra(127, 255, 0, 255);
            }
        }

        /// <summary>
        /// Gets the steel blue.
        /// </summary>
        public static ColorBgra SteelBlue
        {
            get
            {
                return FromBgra(180, 130, 70, 255);
            }
        }

        /// <summary>
        /// Gets the tan.
        /// </summary>
        public static ColorBgra Tan
        {
            get
            {
                return FromBgra(140, 180, 210, 255);
            }
        }

        /// <summary>
        /// Gets the teal.
        /// </summary>
        public static ColorBgra Teal
        {
            get
            {
                return FromBgra(128, 128, 0, 255);
            }
        }

        /// <summary>
        /// Gets the thistle.
        /// </summary>
        public static ColorBgra Thistle
        {
            get
            {
                return FromBgra(216, 191, 216, 255);
            }
        }

        /// <summary>
        /// Gets the tomato.
        /// </summary>
        public static ColorBgra Tomato
        {
            get
            {
                return FromBgra(71, 99, 255, 255);
            }
        }

        /// <summary>
        /// Gets the turquoise.
        /// </summary>
        public static ColorBgra Turquoise
        {
            get
            {
                return FromBgra(208, 224, 64, 255);
            }
        }

        /// <summary>
        /// Gets the violet.
        /// </summary>
        public static ColorBgra Violet
        {
            get
            {
                return FromBgra(238, 130, 238, 255);
            }
        }

        /// <summary>
        /// Gets the wheat.
        /// </summary>
        public static ColorBgra Wheat
        {
            get
            {
                return FromBgra(179, 222, 245, 255);
            }
        }

        /// <summary>
        /// Gets the white.
        /// </summary>
        public static ColorBgra White
        {
            get
            {
                return FromBgra(255, 255, 255, 255);
            }
        }

        /// <summary>
        /// Gets the white smoke.
        /// </summary>
        public static ColorBgra WhiteSmoke
        {
            get
            {
                return FromBgra(245, 245, 245, 255);
            }
        }

        /// <summary>
        /// Gets the yellow.
        /// </summary>
        public static ColorBgra Yellow
        {
            get
            {
                return FromBgra(0, 255, 255, 255);
            }
        }

        /// <summary>
        /// Gets the yellow green.
        /// </summary>
        public static ColorBgra YellowGreen
        {
            get
            {
                return FromBgra(50, 205, 154, 255);
            }
        }

        /// <summary>
        /// Gets the zero.
        /// </summary>
        public static ColorBgra Zero
        {
            get
            {
                return (ColorBgra)0;
            }
        }

        /// <summary>
        /// The predefined colors.
        /// </summary>
        private static readonly Dictionary<string, ColorBgra> predefinedColors;

        /// <summary>
        ///     Gets a hashtable that contains a list of all the predefined colors.
        ///     These are the same color values that are defined as public static properties
        ///     in System.Drawing.Color. The hashtable uses strings for the keys, and
        ///     ColorBgras for the values.
        /// </summary>
        public static Dictionary<string, ColorBgra> PredefinedColors
        {
            get
            {
                if (predefinedColors != null)
                {
                    Type colorBgraType = typeof(ColorBgra);
                    PropertyInfo[] propInfos = colorBgraType.GetProperties(BindingFlags.Static | BindingFlags.Public);
                    var colors = new Hashtable();

                    foreach (PropertyInfo pi in propInfos)
                    {
                        if (pi.PropertyType == colorBgraType)
                        {
                            colors.Add(pi.Name, (ColorBgra)pi.GetValue(null, null));
                        }
                    }
                }

                return new Dictionary<string, ColorBgra>(predefinedColors);
            }
        }
    }
}