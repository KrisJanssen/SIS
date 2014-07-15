// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoordinateL.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The point l.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime;

    /// <summary>
    /// The point l.
    /// </summary>
    public struct CoordinateL
    {
        #region Static Fields

        /// <summary>Represents a <see cref="T:SIS.Hardware.PointL" /> that has <see cref="P:SIS.Hardware.PointL.X" /> and <see cref="P:SIS.Hardware.PointL.Y" /> values set to zero. </summary>
        /// <filterpriority>1</filterpriority>
        public static readonly CoordinateL Empty;

        #endregion

        #region Fields

        /// <summary>
        /// The x.
        /// </summary>
        private long x;

        /// <summary>
        /// The y.
        /// </summary>
        private long y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="CoordinateL"/> struct.
        /// </summary>
        static CoordinateL()
        {
            CoordinateL.Empty = new CoordinateL();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateL"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.PointL"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">
        /// The horizontal position of the point. 
        /// </param>
        /// <param name="y">
        /// The vertical position of the point. 
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CoordinateL(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateL"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.PointL"/> class from a <see cref="T:SIS.Hardware.SizeL"/>.
        /// </summary>
        /// <param name="sz">
        /// A <see cref="T:SIS.Hardware.SizeL"/> that specifies the coordinates for the new <see cref="T:SIS.Hardware.PointL"/>. 
        /// </param>
        public CoordinateL(SizeL sz)
        {
            this.x = sz.Width;
            this.y = sz.Height;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets a value indicating whether this <see cref="T:SIS.Hardware.PointL" /> is empty.</summary>
        /// <returns>true if both <see cref="P:SIS.Hardware.PointL.X" /> and <see cref="P:SIS.Hardware.PointL.Y" /> are 0; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (this.x != 0)
                {
                    return false;
                }

                return this.y == 0;
            }
        }

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:SIS.Hardware.PointL" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:SIS.Hardware.PointL" />.</returns>
        /// <filterpriority>1</filterpriority>
        public long X
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.x;
            }

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.x = value;
            }
        }

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:SIS.Hardware.PointL" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:SIS.Hardware.PointL" />.</returns>
        /// <filterpriority>1</filterpriority>
        public long Y
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.y;
            }

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.y = value;
            }
        }

        #endregion

        ///// <summary>Initializes a new instance of the <see cref="T:SIS.Hardware.PointL" /> class using coordinates specified by an integer value.</summary>
        ///// <param name="dw">A 32-bit integer that specifies the coordinates for the new <see cref="T:SIS.Hardware.PointL" />. </param>
        // public PointL(int dw)
        // {
        // this.x = (short)PointL.LOWORD(dw);
        // this.y = (short)PointL.HIWORD(dw);
        // }
        #region Public Methods and Operators

        /// <summary>
        /// Adds the specified <see cref="T:SIS.Hardware.SizeL"/> to the specified <see cref="T:SIS.Hardware.PointL"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:SIS.Hardware.PointL"/> that is the result of the addition operation.
        /// </returns>
        /// <param name="pt">
        /// The <see cref="T:SIS.Hardware.PointL"/> to add.
        /// </param>
        /// <param name="sz">
        /// The <see cref="T:SIS.Hardware.SizeL"/> to add
        /// </param>
        public static CoordinateL Add(CoordinateL pt, SizeL sz)
        {
            return new CoordinateL(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>
        /// Returns the result of subtracting specified <see cref="T:SIS.Hardware.SizeL"/> from the specified <see cref="T:SIS.Hardware.PointL"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:SIS.Hardware.PointL"/> that is the result of the subtraction operation.
        /// </returns>
        /// <param name="pt">
        /// The <see cref="T:SIS.Hardware.PointL"/> to be subtracted from. 
        /// </param>
        /// <param name="sz">
        /// The <see cref="T:SIS.Hardware.SizeL"/> to subtract from the <see cref="T:SIS.Hardware.PointL"/>.
        /// </param>
        public static CoordinateL Subtract(CoordinateL pt, SizeL sz)
        {
            return new CoordinateL(pt.X - sz.Width, pt.Y - sz.Height);
        }

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.PointLF" /> to a <see cref="T:SIS.Hardware.PointL" /> by rounding the values of the <see cref="T:SIS.Hardware.PointLF" /> to the next higher integer values.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.PointL" /> this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.PointLF" /> to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static PointL Ceiling(PointF value)
        // {
        // return new PointL((int)Math.Ceiling((double)value.X), (int)Math.Ceiling((double)value.Y));
        // }

        /// <summary>Translates a <see cref="T:SIS.Hardware.PointL" /> by a given <see cref="T:SIS.Hardware.SizeL" />.</summary>
        /// <returns>The translated <see cref="T:SIS.Hardware.PointL" />.</returns>
        /// <param name="pt">The <see cref="T:SIS.Hardware.PointL" /> to translate. </param>
        /// <param name="sz">A <see cref="T:SIS.Hardware.SizeL" /> that specifies the pair of numbers to add to the coordinates of <paramref name="pt" />. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static CoordinateL operator +(CoordinateL pt, SizeL sz)
        {
            return CoordinateL.Add(pt, sz);
        }

        /// <summary>Compares two <see cref="T:SIS.Hardware.PointL" /> objects. The result specifies whether the values of the <see cref="P:SIS.Hardware.PointL.X" /> and <see cref="P:SIS.Hardware.PointL.Y" /> properties of the two <see cref="T:SIS.Hardware.PointL" /> objects are equal.</summary>
        /// <returns>true if the <see cref="P:SIS.Hardware.PointL.X" /> and <see cref="P:SIS.Hardware.PointL.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
        /// <param name="left">A <see cref="T:SIS.Hardware.PointL" /> to compare. </param>
        /// <param name="right">A <see cref="T:SIS.Hardware.PointL" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(CoordinateL left, CoordinateL right)
        {
            if (left.X != right.X)
            {
                return false;
            }

            return left.Y == right.Y;
        }

        /// <summary>
        /// The op_ explicit.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// </returns>
        public static explicit operator SizeL(CoordinateL p)
        {
            return new SizeL(p.X, p.Y);
        }

        // public static implicit operator PointF(PointL p)
        // {
        // return new PointF((float)p.X, (float)p.Y);
        // }

        /// <summary>Compares two <see cref="T:SIS.Hardware.PointL" /> objects. The result specifies whether the values of the <see cref="P:SIS.Hardware.PointL.X" /> or <see cref="P:SIS.Hardware.PointL.Y" /> properties of the two <see cref="T:SIS.Hardware.PointL" /> objects are unequal.</summary>
        /// <returns>true if the values of either the <see cref="P:SIS.Hardware.PointL.X" /> properties or the <see cref="P:SIS.Hardware.PointL.Y" /> properties of <paramref name="left" /> and <paramref name="right" /> differ; otherwise, false.</returns>
        /// <param name="left">A <see cref="T:SIS.Hardware.PointL" /> to compare. </param>
        /// <param name="right">A <see cref="T:SIS.Hardware.PointL" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static bool operator !=(CoordinateL left, CoordinateL right)
        {
            return !(left == right);
        }

        /// <summary>Translates a <see cref="T:SIS.Hardware.PointL" /> by the negative of a given <see cref="T:SIS.Hardware.SizeL" />.</summary>
        /// <returns>A <see cref="T:SIS.Hardware.PointL" /> structure that is translated by the negative of a given <see cref="T:SIS.Hardware.SizeL" /> structure.</returns>
        /// <param name="pt">The <see cref="T:SIS.Hardware.PointL" /> to translate. </param>
        /// <param name="sz">A <see cref="T:SIS.Hardware.SizeL" /> that specifies the pair of numbers to subtract from the coordinates of <paramref name="pt" />. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static CoordinateL operator -(CoordinateL pt, SizeL sz)
        {
            return CoordinateL.Subtract(pt, sz);
        }

        /// <summary>
        /// Specifies whether this <see cref="T:SIS.Hardware.PointL"/> contains the same coordinates as the specified <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> is a <see cref="T:SIS.Hardware.PointL"/> and has the same coordinates as this <see cref="T:SIS.Hardware.PointL"/>.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to test. 
        /// </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is CoordinateL))
            {
                return false;
            }

            CoordinateL point = (CoordinateL)obj;
            if (point.X != this.X)
            {
                return false;
            }

            return point.Y == this.Y;
        }

        ///// <summary>Returns a hash code for this <see cref="T:SIS.Hardware.PointL" />.</summary>
        ///// <returns>An integer value that specifies a hash value for this <see cref="T:SIS.Hardware.PointL" />.</returns>
        ///// <filterpriority>1</filterpriority>
        // public override int GetHashCode()
        // {
        // return this.x ^ this.y;
        // }

        // private static int HIWORD(int n)
        // {
        // return n >> 16 & 65535;
        // }

        // private static int LOWORD(int n)
        // {
        // return n & 65535;
        // }

        /// <summary>
        /// Translates this <see cref="T:SIS.Hardware.PointL"/> by the specified amount.
        /// </summary>
        /// <param name="dx">
        /// The amount to offset the x-coordinate. 
        /// </param>
        /// <param name="dy">
        /// The amount to offset the y-coordinate. 
        /// </param>
        /// <filterpriority>1</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public void Offset(long dx, long dy)
        {
            CoordinateL x = this;
            x.X = x.X + dx;
            CoordinateL y = this;
            y.Y = y.Y + dy;
        }

        /// <summary>
        /// Translates this <see cref="T:SIS.Hardware.PointL"/> by the specified <see cref="T:SIS.Hardware.PointL"/>.
        /// </summary>
        /// <param name="p">
        /// The <see cref="T:SIS.Hardware.PointL"/> used offset this <see cref="T:SIS.Hardware.PointL"/>.
        /// </param>
        public void Offset(CoordinateL p)
        {
            this.Offset(p.X, p.Y);
        }

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.PointF" /> to a <see cref="T:SIS.Hardware.PointL" /> object by rounding the <see cref="T:SIS.Hardware.PointL" /> values to the nearest integer.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.PointL" /> this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.PointLF" /> to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static PointL Round(PointF value)
        // {
        // return new PointL((int)Math.Round((double)value.X), (int)Math.Round((double)value.Y));
        // }

        /// <summary>Converts this <see cref="T:SIS.Hardware.PointL" /> to a human-readable string.</summary>
        /// <returns>A string that represents this <see cref="T:SIS.Hardware.PointL" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            string[] str = new[] { "{X=", null, null, null, null };
            long x = this.X;
            str[1] = x.ToString(CultureInfo.CurrentCulture);
            str[2] = ",Y=";
            long y = this.Y;
            str[3] = y.ToString(CultureInfo.CurrentCulture);
            str[4] = "}";
            return string.Concat(str);
        }

        #endregion

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.PointLF" /> to a <see cref="T:SIS.Hardware.PointL" /> by truncating the values of the <see cref="T:SIS.Hardware.PointL" />.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.PointL" /> this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.PointLF" /> to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static PointL Truncate(PointF value)
        // {
        // return new PointL((int)value.X, (int)value.Y);
        // }
    }
}