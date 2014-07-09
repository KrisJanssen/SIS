// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScaleFactor.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Encapsulates functionality for zooming/scaling coordinates.
//   Includes methods for Size[F]'s, Point[F]'s, Rectangle[F]'s,
//   and various scalars
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Encapsulates functionality for zooming/scaling coordinates.
    /// Includes methods for Size[F]'s, Point[F]'s, Rectangle[F]'s,
    /// and various scalars
    /// </summary>
    public struct ScaleFactor
    {
        #region Static Fields

        /// <summary>
        /// The max value.
        /// </summary>
        public static readonly ScaleFactor MaxValue = new ScaleFactor(100, 1);

        /// <summary>
        /// The min value.
        /// </summary>
        public static readonly ScaleFactor MinValue = new ScaleFactor(1, 100);

        /// <summary>
        /// The one to one.
        /// </summary>
        public static readonly ScaleFactor OneToOne = new ScaleFactor(1, 1);

        /// <summary>
        /// The scales.
        /// </summary>
        private static readonly double[] scales =
            {
                0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.08, 0.12, 0.16, 0.25, 0.33, 
                0.50, 0.66, 1, 2, 3, 4, 5, 6, 7, 8, 12, 16, 24, 32
            };

        /// <summary>
        /// The percentage format.
        /// </summary>
        private static string percentageFormat = "ScaleFactor.Percentage.Format";

        #endregion

        #region Fields

        /// <summary>
        /// The denominator.
        /// </summary>
        private int denominator;

        /// <summary>
        /// The numerator.
        /// </summary>
        private int numerator;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleFactor"/> struct.
        /// </summary>
        /// <param name="numerator">
        /// The numerator.
        /// </param>
        /// <param name="denominator">
        /// The denominator.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        public ScaleFactor(int numerator, int denominator)
        {
            if (denominator <= 0)
            {
                throw new ArgumentOutOfRangeException("denominator", "must be greater than 0");
            }

            if (numerator < 0)
            {
                throw new ArgumentOutOfRangeException("numerator", "must be greater than 0");
            }

            this.numerator = numerator;
            this.denominator = denominator;
            this.Clamp();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a list of values that GetNextLarger() and GetNextSmaller() will cycle through.
        /// </summary>
        /// <remarks>
        /// 1.0 is guaranteed to be in the array returned by this property. This list is also
        /// sorted in ascending order.
        /// </remarks>
        public static double[] PresetValues
        {
            get
            {
                double[] returnValue = new double[scales.Length];
                scales.CopyTo(returnValue, 0);
                return returnValue;
            }
        }

        /// <summary>
        /// Gets the denominator.
        /// </summary>
        public int Denominator
        {
            get
            {
                return this.denominator;
            }
        }

        /// <summary>
        /// Gets the numerator.
        /// </summary>
        public int Numerator
        {
            get
            {
                return this.numerator;
            }
        }

        /// <summary>
        /// Gets the ratio.
        /// </summary>
        public double Ratio
        {
            get
            {
                return (double)this.numerator / (double)this.denominator;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The from double.
        /// </summary>
        /// <param name="scalar">
        /// The scalar.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor FromDouble(double scalar)
        {
            int numerator = (int)Math.Floor(scalar * 1000.0);
            int denominator = 1000;
            return Reduce(numerator, denominator);
        }

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="n1">
        /// The n 1.
        /// </param>
        /// <param name="d1">
        /// The d 1.
        /// </param>
        /// <param name="n2">
        /// The n 2.
        /// </param>
        /// <param name="d2">
        /// The d 2.
        /// </param>
        /// <param name="lastResort">
        /// The last resort.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor Max(int n1, int d1, int n2, int d2, ScaleFactor lastResort)
        {
            ScaleFactor a = UseIfValid(n1, d1, lastResort);
            ScaleFactor b = UseIfValid(n2, d2, lastResort);
            return ScaleFactor.Max(a, b);
        }

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor Max(ScaleFactor lhs, ScaleFactor rhs)
        {
            if (lhs > rhs)
            {
                return lhs;
            }
            else
            {
                return lhs;
            }
        }

        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="n1">
        /// The n 1.
        /// </param>
        /// <param name="d1">
        /// The d 1.
        /// </param>
        /// <param name="n2">
        /// The n 2.
        /// </param>
        /// <param name="d2">
        /// The d 2.
        /// </param>
        /// <param name="lastResort">
        /// The last resort.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor Min(int n1, int d1, int n2, int d2, ScaleFactor lastResort)
        {
            ScaleFactor a = UseIfValid(n1, d1, lastResort);
            ScaleFactor b = UseIfValid(n2, d2, lastResort);
            return ScaleFactor.Min(a, b);
        }

        /// <summary>
        /// The min.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor Min(ScaleFactor lhs, ScaleFactor rhs)
        {
            if (lhs < rhs)
            {
                return lhs;
            }
            else
            {
                return rhs;
            }
        }

        /// <summary>
        /// The use if valid.
        /// </summary>
        /// <param name="numerator">
        /// The numerator.
        /// </param>
        /// <param name="denominator">
        /// The denominator.
        /// </param>
        /// <param name="lastResort">
        /// The last resort.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public static ScaleFactor UseIfValid(int numerator, int denominator, ScaleFactor lastResort)
        {
            if (numerator <= 0 || denominator <= 0)
            {
                return lastResort;
            }
            else
            {
                return new ScaleFactor(numerator, denominator);
            }
        }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) == (rhs.numerator * lhs.denominator);
        }

        /// <summary>
        /// The &gt;.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator >(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) > (rhs.numerator * lhs.denominator);
        }

        /// <summary>
        /// The &gt;=.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator >=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) >= (rhs.numerator * lhs.denominator);
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// The &lt;.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator <(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) < (rhs.numerator * lhs.denominator);
        }

        /// <summary>
        /// The &lt;=.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator <=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) <= (rhs.numerator * lhs.denominator);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is ScaleFactor)
            {
                ScaleFactor rhs = (ScaleFactor)obj;
                return this == rhs;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return this.numerator.GetHashCode() ^ this.denominator.GetHashCode();
        }

        /// <summary>
        /// Rounds the current scaling factor up to the next power of two.
        /// </summary>
        /// <returns>The new ScaleFactor value.</returns>
        public ScaleFactor GetNextLarger()
        {
            double ratio = this.Ratio + 0.005;

            int index = Array.FindIndex(scales, delegate(double scale) { return ratio <= scale; });

            if (index == -1)
            {
                index = scales.Length;
            }

            index = Math.Min(index, scales.Length - 1);

            return ScaleFactor.FromDouble(scales[index]);
        }

        /// <summary>
        /// The get next smaller.
        /// </summary>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        public ScaleFactor GetNextSmaller()
        {
            double ratio = this.Ratio - 0.005;

            int index = Array.FindIndex(scales, delegate(double scale) { return ratio <= scale; });

            --index;

            if (index == -1)
            {
                index = 0;
            }

            index = Math.Max(index, 0);

            return ScaleFactor.FromDouble(scales[index]);
        }

        // private static string percentageFormat = SISResources.GetString("ScaleFactor.Percentage.Format");

        /// <summary>
        /// The scale point.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point ScalePoint(Point p)
        {
            return new Point(this.ScaleScalar(p.X), this.ScaleScalar(p.Y));
        }

        /// <summary>
        /// The scale point.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF ScalePoint(PointF p)
        {
            return new PointF(this.ScaleScalar(p.X), this.ScaleScalar(p.Y));
        }

        /// <summary>
        /// The scale point just x.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF ScalePointJustX(PointF p)
        {
            return new PointF(this.ScaleScalar(p.X), p.Y);
        }

        /// <summary>
        /// The scale point just x.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point ScalePointJustX(Point p)
        {
            return new Point(this.ScaleScalar(p.X), p.Y);
        }

        /// <summary>
        /// The scale point just y.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF ScalePointJustY(PointF p)
        {
            return new PointF(p.X, this.ScaleScalar(p.Y));
        }

        /// <summary>
        /// The scale point just y.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point ScalePointJustY(Point p)
        {
            return new Point(p.X, this.ScaleScalar(p.Y));
        }

        /// <summary>
        /// The scale rectangle.
        /// </summary>
        /// <param name="rectF">
        /// The rect f.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public RectangleF ScaleRectangle(RectangleF rectF)
        {
            return new RectangleF(this.ScalePoint(rectF.Location), this.ScaleSize(rectF.Size));
        }

        /// <summary>
        /// The scale rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public Rectangle ScaleRectangle(Rectangle rect)
        {
            return new Rectangle(this.ScalePoint(rect.Location), this.ScaleSize(rect.Size));
        }

        /// <summary>
        /// The scale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int ScaleScalar(int x)
        {
            return (int)(((long)x * this.numerator) / this.denominator);
        }

        /// <summary>
        /// The scale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public float ScaleScalar(float x)
        {
            return (x * (float)this.numerator) / (float)this.denominator;
        }

        /// <summary>
        /// The scale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double ScaleScalar(double x)
        {
            return (x * (double)this.numerator) / (double)this.denominator;
        }

        /// <summary>
        /// The scale size.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="SizeF"/>.
        /// </returns>
        public SizeF ScaleSize(SizeF s)
        {
            return new SizeF(this.ScaleScalar(s.Width), this.ScaleScalar(s.Height));
        }

        /// <summary>
        /// The scale size.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        public Size ScaleSize(Size s)
        {
            return new Size(this.ScaleScalar(s.Width), this.ScaleScalar(s.Height));
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format(percentageFormat, Math.Round(100 * this.Ratio));
        }

        /// <summary>
        /// The unscale point.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF UnscalePoint(PointF p)
        {
            return new PointF(this.UnscaleScalar(p.X), this.UnscaleScalar(p.Y));
        }

        /// <summary>
        /// The unscale point.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point UnscalePoint(Point p)
        {
            return new Point(this.UnscaleScalar(p.X), this.UnscaleScalar(p.Y));
        }

        /// <summary>
        /// The unscale point just x.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF UnscalePointJustX(PointF p)
        {
            return new PointF(this.UnscaleScalar(p.X), p.Y);
        }

        /// <summary>
        /// The unscale point just x.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point UnscalePointJustX(Point p)
        {
            return new Point(this.UnscaleScalar(p.X), p.Y);
        }

        /// <summary>
        /// The unscale point just y.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF UnscalePointJustY(PointF p)
        {
            return new PointF(p.X, this.UnscaleScalar(p.Y));
        }

        /// <summary>
        /// The unscale point just y.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public Point UnscalePointJustY(Point p)
        {
            return new Point(p.X, this.UnscaleScalar(p.Y));
        }

        /// <summary>
        /// The unscale rectangle.
        /// </summary>
        /// <param name="rectF">
        /// The rect f.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public RectangleF UnscaleRectangle(RectangleF rectF)
        {
            return new RectangleF(this.UnscalePoint(rectF.Location), this.UnscaleSize(rectF.Size));
        }

        /// <summary>
        /// The unscale rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public Rectangle UnscaleRectangle(Rectangle rect)
        {
            return new Rectangle(this.UnscalePoint(rect.Location), this.UnscaleSize(rect.Size));
        }

        /// <summary>
        /// The unscale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int UnscaleScalar(int x)
        {
            return (int)(((long)x * this.denominator) / this.numerator);
        }

        /// <summary>
        /// The unscale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public float UnscaleScalar(float x)
        {
            return (x * (float)this.denominator) / (float)this.numerator;
        }

        /// <summary>
        /// The unscale scalar.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double UnscaleScalar(double x)
        {
            return (x * (double)this.denominator) / (double)this.numerator;
        }

        /// <summary>
        /// The unscale size.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="SizeF"/>.
        /// </returns>
        public SizeF UnscaleSize(SizeF s)
        {
            return new SizeF(this.UnscaleScalar(s.Width), this.UnscaleScalar(s.Height));
        }

        /// <summary>
        /// The unscale size.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        public Size UnscaleSize(Size s)
        {
            return new Size(this.UnscaleScalar(s.Width), this.UnscaleScalar(s.Height));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The reduce.
        /// </summary>
        /// <param name="numerator">
        /// The numerator.
        /// </param>
        /// <param name="denominator">
        /// The denominator.
        /// </param>
        /// <returns>
        /// The <see cref="ScaleFactor"/>.
        /// </returns>
        private static ScaleFactor Reduce(int numerator, int denominator)
        {
            int factor = 2;

            while (factor < denominator && factor < numerator)
            {
                if ((numerator % factor) == 0 && (denominator % factor) == 0)
                {
                    numerator /= factor;
                    denominator /= factor;
                }
                else
                {
                    ++factor;
                }
            }

            return new ScaleFactor(numerator, denominator);
        }

        /// <summary>
        /// The clamp.
        /// </summary>
        private void Clamp()
        {
            if (this < MinValue)
            {
                this = MinValue;
            }
            else if (this > MaxValue)
            {
                this = MaxValue;
            }
        }

        #endregion
    }
}