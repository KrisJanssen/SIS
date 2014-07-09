/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

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
        private int denominator;
        private int numerator;

        public int Denominator
        {
            get
            {
                return this.denominator;
            }
        }

        public int Numerator
        {
            get
            {
                return this.numerator;
            }
        }

        public double Ratio
        {
            get
            {
                return (double)this.numerator / (double)this.denominator;
            }
        }

        public static readonly ScaleFactor OneToOne = new ScaleFactor(1, 1);
        public static readonly ScaleFactor MinValue = new ScaleFactor(1, 100);
        public static readonly ScaleFactor MaxValue = new ScaleFactor(100, 1);

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

        public static ScaleFactor Min(int n1, int d1, int n2, int d2, ScaleFactor lastResort)
        {
            ScaleFactor a = UseIfValid(n1, d1, lastResort);
            ScaleFactor b = UseIfValid(n2, d2, lastResort);
            return ScaleFactor.Min(a, b);
        }

        public static ScaleFactor Max(int n1, int d1, int n2, int d2, ScaleFactor lastResort)
        {
            ScaleFactor a = UseIfValid(n1, d1, lastResort);
            ScaleFactor b = UseIfValid(n2, d2, lastResort);
            return ScaleFactor.Max(a, b);
        }

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

        public static bool operator ==(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) == (rhs.numerator * lhs.denominator);
        }

        public static bool operator !=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) < (rhs.numerator * lhs.denominator);
        }

        public static bool operator <=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) <= (rhs.numerator * lhs.denominator);
        }

        public static bool operator >(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) > (rhs.numerator * lhs.denominator);
        }

        public static bool operator >=(ScaleFactor lhs, ScaleFactor rhs)
        {
            return (lhs.numerator * rhs.denominator) >= (rhs.numerator * lhs.denominator);
        }

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

        public override int GetHashCode()
        {
            return this.numerator.GetHashCode() ^ this.denominator.GetHashCode();
        }

        //private static string percentageFormat = SISResources.GetString("ScaleFactor.Percentage.Format");
        private static string percentageFormat = "ScaleFactor.Percentage.Format";
        public override string ToString()
        {
            return string.Format(percentageFormat, Math.Round(100 * this.Ratio));
        }

        public int ScaleScalar(int x)
        {
            return (int)(((long)x * this.numerator) / this.denominator);
        }

        public int UnscaleScalar(int x)
        {
            return (int)(((long)x * this.denominator) / this.numerator);
        }

        public float ScaleScalar(float x)
        {
            return (x * (float)this.numerator) / (float)this.denominator;
        }

        public float UnscaleScalar(float x)
        {
            return (x * (float)this.denominator) / (float)this.numerator;
        }

        public double ScaleScalar(double x)
        {
            return (x * (double)this.numerator) / (double)this.denominator;
        }

        public double UnscaleScalar(double x)
        {
            return (x * (double)this.denominator) / (double)this.numerator;
        }

        public Point ScalePoint(Point p)
        {
            return new Point(this.ScaleScalar(p.X), this.ScaleScalar(p.Y));
        }

        public PointF ScalePoint(PointF p)
        {
            return new PointF(this.ScaleScalar(p.X), this.ScaleScalar(p.Y));
        }

        public PointF ScalePointJustX(PointF p)
        {
            return new PointF(this.ScaleScalar(p.X), p.Y);
        }

        public PointF ScalePointJustY(PointF p)
        {
            return new PointF(p.X, this.ScaleScalar(p.Y));
        }

        public PointF UnscalePoint(PointF p)
        {
            return new PointF(this.UnscaleScalar(p.X), this.UnscaleScalar(p.Y));
        }

        public PointF UnscalePointJustX(PointF p)
        {
            return new PointF(this.UnscaleScalar(p.X), p.Y);
        }

        public PointF UnscalePointJustY(PointF p)
        {
            return new PointF(p.X, this.UnscaleScalar(p.Y));
        }

        public Point ScalePointJustX(Point p)
        {
            return new Point(this.ScaleScalar(p.X), p.Y);
        }

        public Point ScalePointJustY(Point p)
        {
            return new Point(p.X, this.ScaleScalar(p.Y));
        }

        public Point UnscalePoint(Point p)
        {
            return new Point(this.UnscaleScalar(p.X), this.UnscaleScalar(p.Y));
        }

        public Point UnscalePointJustX(Point p)
        {
            return new Point(this.UnscaleScalar(p.X), p.Y);
        }

        public Point UnscalePointJustY(Point p)
        {
            return new Point(p.X, this.UnscaleScalar(p.Y));
        }

        public SizeF ScaleSize(SizeF s)
        {
            return new SizeF(this.ScaleScalar(s.Width), this.ScaleScalar(s.Height));
        }

        public SizeF UnscaleSize(SizeF s)
        {
            return new SizeF(this.UnscaleScalar(s.Width), this.UnscaleScalar(s.Height));
        }

        public Size ScaleSize(Size s)
        {
            return new Size(this.ScaleScalar(s.Width), this.ScaleScalar(s.Height));
        }

        public Size UnscaleSize(Size s)
        {
            return new Size(this.UnscaleScalar(s.Width), this.UnscaleScalar(s.Height));
        }

        public RectangleF ScaleRectangle(RectangleF rectF)
        {
            return new RectangleF(this.ScalePoint(rectF.Location), this.ScaleSize(rectF.Size));
        }

        public RectangleF UnscaleRectangle(RectangleF rectF)
        {
            return new RectangleF(this.UnscalePoint(rectF.Location), this.UnscaleSize(rectF.Size));
        }

        public Rectangle ScaleRectangle(Rectangle rect)
        {
            return new Rectangle(this.ScalePoint(rect.Location), this.ScaleSize(rect.Size));
        }

        public Rectangle UnscaleRectangle(Rectangle rect)
        {
            return new Rectangle(this.UnscalePoint(rect.Location), this.UnscaleSize(rect.Size));
        }

        private static readonly double[] scales = 
            { 
                0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.08, 0.12, 0.16, 0.25, 0.33, 0.50, 0.66, 1,
                2, 3, 4, 5, 6, 7, 8, 12, 16, 24, 32
            };

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
        /// Rounds the current scaling factor up to the next power of two.
        /// </summary>
        /// <returns>The new ScaleFactor value.</returns>
        public ScaleFactor GetNextLarger()
        {
            double ratio = this.Ratio + 0.005;

            int index = Array.FindIndex(
                scales,
                delegate(double scale)
                {
                    return ratio <= scale;
                });

            if (index == -1)
            {
                index = scales.Length;
            }

            index = Math.Min(index, scales.Length - 1);

            return ScaleFactor.FromDouble(scales[index]);
        }

        public ScaleFactor GetNextSmaller()
        {
            double ratio = this.Ratio - 0.005;

            int index = Array.FindIndex(
                scales,
                delegate(double scale)
                {
                    return ratio <= scale;
                });

            --index;

            if (index == -1)
            {
                index = 0;
            }

            index = Math.Max(index, 0);

            return ScaleFactor.FromDouble(scales[index]);
        }

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

        public static ScaleFactor FromDouble(double scalar)
        {
            int numerator = (int)(Math.Floor(scalar * 1000.0));
            int denominator = 1000;
            return Reduce(numerator, denominator);
        }

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
    }
}
