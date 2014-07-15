// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SizeL.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The size l.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime;

    /// <summary>
    /// The size l.
    /// </summary>
    public struct SizeL
    {
        #region Static Fields

        /// <summary>Gets a <see cref="T:SIS.Hardware.SizeL" /> structure that has a <see cref="P:SIS.Hardware.SizeL.Height" /> and <see cref="P:SIS.Hardware.SizeL.Width" /> value of 0. </summary>
        /// <returns>A <see cref="T:SIS.Hardware.SizeL" /> that has a <see cref="P:SIS.Hardware.SizeL.Height" /> and <see cref="P:SIS.Hardware.SizeL.Width" /> value of 0.</returns>
        /// <filterpriority>1</filterpriority>
        public static readonly SizeL Empty;

        #endregion

        #region Fields

        /// <summary>
        /// The height.
        /// </summary>
        private long height;

        /// <summary>
        /// The width.
        /// </summary>
        private long width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="SizeL"/> struct.
        /// </summary>
        static SizeL()
        {
            SizeL.Empty = new SizeL();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.SizeL"/> structure from the specified <see cref="T:System.Drawing.Point"/> structure.
        /// </summary>
        /// <param name="pt">
        /// The <see cref="T:System.Drawing.Point"/> structure from which to initialize this <see cref="T:SIS.Hardware.SizeL"/> structure. 
        /// </param>
        public SizeL(CoordinateL pt)
        {
            this.width = pt.X;
            this.height = pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.SizeL"/> structure from the specified dimensions.
        /// </summary>
        /// <param name="width">
        /// The width component of the new <see cref="T:SIS.Hardware.SizeL"/>. 
        /// </param>
        /// <param name="height">
        /// The height component of the new <see cref="T:SIS.Hardware.SizeL"/>. 
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SizeL(long width, long height)
        {
            this.width = width;
            this.height = height;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the vertical component of this <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:SIS.Hardware.SizeL" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public long Height
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return this.height;
            }

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.height = value;
            }
        }

        /// <summary>Tests whether this <see cref="T:SIS.Hardware.SizeL" /> structure has width and height of 0.</summary>
        /// <returns>This property returns true when this <see cref="T:SIS.Hardware.SizeL" /> structure has both a width and height of 0; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                if (this.width != 0L)
                {
                    return false;
                }

                return this.height == 0L;
            }
        }

        /// <summary>Gets or sets the horizontal component of this <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="T:SIS.Hardware.SizeL" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public long Width
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return this.width;
            }

            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.width = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds the width and height of one <see cref="T:SIS.Hardware.SizeL"/> structure to the width and height of another <see cref="T:SIS.Hardware.SizeL"/> structure.
        /// </summary>
        /// <returns>
        /// A <see cref="T:SIS.Hardware.SizeL"/> structure that is the result of the addition operation.
        /// </returns>
        /// <param name="sz1">
        /// The first <see cref="T:SIS.Hardware.SizeL"/> structure to add.
        /// </param>
        /// <param name="sz2">
        /// The second <see cref="T:SIS.Hardware.SizeL"/> structure to add.
        /// </param>
        public static SizeL Add(SizeL sz1, SizeL sz2)
        {
            return new SizeL(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>
        /// Subtracts the width and height of one <see cref="T:SIS.Hardware.SizeL"/> structure from the width and height of another <see cref="T:SIS.Hardware.SizeL"/> structure.
        /// </summary>
        /// <returns>
        /// A <see cref="T:SIS.Hardware.SizeL"/> structure that is a result of the subtraction operation.
        /// </returns>
        /// <param name="sz1">
        /// The <see cref="T:SIS.Hardware.SizeL"/> structure on the left side of the subtraction operator. 
        /// </param>
        /// <param name="sz2">
        /// The <see cref="T:SIS.Hardware.SizeL"/> structure on the right side of the subtraction operator. 
        /// </param>
        public static SizeL Subtract(SizeL sz1, SizeL sz2)
        {
            return new SizeL(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.SizeLF" /> structure to a <see cref="T:SIS.Hardware.SizeL" /> structure by rounding the values of the <see cref="T:SIS.Hardware.SizeL" /> structure to the next higher integer values.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.SizeL" /> structure this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.SizeLF" /> structure to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static SizeL Ceiling(SizeD value)
        // {
        // return new SizeL((int)Math.Ceiling((double)value.Width), (int)Math.Ceiling((double)value.Height));
        // }

        /// <summary>Adds the width and height of one <see cref="T:SIS.Hardware.SizeL" /> structure to the width and height of another <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        /// <returns>A <see cref="T:SIS.Hardware.SizeL" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:SIS.Hardware.SizeL" /> to add. </param>
        /// <param name="sz2">The second <see cref="T:SIS.Hardware.SizeL" /> to add. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static SizeL operator +(SizeL sz1, SizeL sz2)
        {
            return SizeL.Add(sz1, sz2);
        }

        /// <summary>Tests whether two <see cref="T:SIS.Hardware.SizeL" /> structures are equal.</summary>
        /// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.SizeL" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.SizeL" /> structure on the right of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(SizeL sz1, SizeL sz2)
        {
            if (sz1.Width != sz2.Width)
            {
                return false;
            }

            return sz1.Height == sz2.Height;
        }

        /// <summary>
        /// The op_ explicit.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <returns>
        /// </returns>
        public static explicit operator CoordinateL(SizeL size)
        {
            return new CoordinateL(size.Width, size.Height);
        }

        /// <summary>
        /// The op_ implicit.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// </returns>
        public static implicit operator SizeD(SizeL p)
        {
            return new SizeD((float)p.Width, (float)p.Height);
        }

        /// <summary>Tests whether two <see cref="T:SIS.Hardware.SizeL" /> structures are different.</summary>
        /// <returns>true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.SizeL" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.SizeL" /> structure on the right of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public static bool operator !=(SizeL sz1, SizeL sz2)
        {
            return !(sz1 == sz2);
        }

        /// <summary>Subtracts the width and height of one <see cref="T:SIS.Hardware.SizeL" /> structure from the width and height of another <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        /// <returns>A <see cref="T:SIS.Hardware.SizeL" /> structure that is the result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.SizeL" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.SizeL" /> structure on the right side of the subtraction operator. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static SizeL operator -(SizeL sz1, SizeL sz2)
        {
            return SizeL.Subtract(sz1, sz2);
        }

        /// <summary>
        /// Tests to see whether the specified object is a <see cref="T:SIS.Hardware.SizeL"/> structure with the same dimensions as this <see cref="T:SIS.Hardware.SizeL"/> structure.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> is a <see cref="T:SIS.Hardware.SizeL"/> and has the same width and height as this <see cref="T:SIS.Hardware.SizeL"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to test. 
        /// </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is SizeL))
            {
                return false;
            }

            SizeL size = (SizeL)obj;
            if (size.width != this.width)
            {
                return false;
            }

            return size.height == this.height;
        }

        ///// <summary>Returns a hash code for this <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        ///// <returns>An integer value that specifies a hash value for this <see cref="T:SIS.Hardware.SizeL" /> structure.</returns>
        ///// <filterpriority>1</filterpriority>
        // public override int GetHashCode()
        // {
        // return this.width ^ this.height;
        // }

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.SizeLF" /> structure to a <see cref="T:SIS.Hardware.SizeL" /> structure by rounding the values of the <see cref="T:SIS.Hardware.SizeLF" /> structure to the nearest integer values.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.SizeL" /> structure this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.SizeLF" /> structure to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static SizeL Round(SizeD value)
        // {
        // return new SizeL((int)Math.Round((double)value.Width), (int)Math.Round((double)value.Height));
        // }

        /// <summary>Creates a human-readable string that represents this <see cref="T:SIS.Hardware.SizeL" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:SIS.Hardware.SizeL" />.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        public override string ToString()
        {
            string[] str = new[]
                               {
                                   "{Width=", this.width.ToString(CultureInfo.CurrentCulture), ", Height=", 
                                   this.height.ToString(CultureInfo.CurrentCulture), "}"
                               };
            return string.Concat(str);
        }

        #endregion

        ///// <summary>Converts the specified <see cref="T:SIS.Hardware.SizeLF" /> structure to a <see cref="T:SIS.Hardware.SizeL" /> structure by truncating the values of the <see cref="T:SIS.Hardware.SizeLF" /> structure to the next lower integer values.</summary>
        ///// <returns>The <see cref="T:SIS.Hardware.SizeL" /> structure this method converts to.</returns>
        ///// <param name="value">The <see cref="T:SIS.Hardware.SizeLF" /> structure to convert. </param>
        ///// <filterpriority>1</filterpriority>
        // public static Size Truncate(SizeD value)
        // {
        // return new Size((int)value.Width, (int)value.Height);
        // }
    }
}