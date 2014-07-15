// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SizeD.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The size.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime;

    /// <summary>
    /// The size.
    /// </summary>
    public struct SizeD
    {
        #region Static Fields

        /// <summary>Gets a <see cref="T:SIS.Hardware.Size" /> structure that has a <see cref="P:SIS.Hardware.Size.Height" /> and <see cref="P:SIS.Hardware.Size.Width" /> value of 0. </summary>
        /// <returns>A <see cref="T:SIS.Hardware.Size" /> structure that has a <see cref="P:SIS.Hardware.Size.Height" /> and <see cref="P:SIS.Hardware.Size.Width" /> value of 0.</returns>
        /// <filterpriority>1</filterpriority>
        public static readonly SizeD Empty;

        #endregion

        #region Fields

        /// <summary>
        /// The height.
        /// </summary>
        private double height;

        /// <summary>
        /// The width.
        /// </summary>
        private double width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="SizeD"/> struct.
        /// </summary>
        static SizeD()
        {
            SizeD.Empty = new SizeD();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeD"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.Size"/> structure from the specified existing <see cref="T:SIS.Hardware.Size"/> structure.
        /// </summary>
        /// <param name="size">
        /// The <see cref="T:SIS.Hardware.Size"/> structure from which to create the new <see cref="T:SIS.Hardware.Size"/> structure. 
        /// </param>
        public SizeD(SizeD size)
        {
            this.width = size.width;
            this.height = size.height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeD"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.Size"/> structure from the specified <see cref="T:System.Drawing.Coordinate"/> structure.
        /// </summary>
        /// <param name="pt">
        /// The <see cref="T:System.Drawing.Coordinate"/> structure from which to initialize this <see cref="T:SIS.Hardware.Size"/> structure. 
        /// </param>
        public SizeD(CoordinateD pt)
        {
            this.width = pt.X;
            this.height = pt.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeD"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.Size"/> structure from the specified dimensions.
        /// </summary>
        /// <param name="width">
        /// The width component of the new <see cref="T:SIS.Hardware.Size"/> structure. 
        /// </param>
        /// <param name="height">
        /// The height component of the new <see cref="T:SIS.Hardware.Size"/> structure. 
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public SizeD(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the vertical component of this <see cref="T:SIS.Hardware.Size" /> structure.</summary>
        /// <returns>The vertical component of this <see cref="T:SIS.Hardware.Size" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public double Height
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
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

        /// <summary>Gets a value that indicates whether this <see cref="T:SIS.Hardware.Size" /> structure has zero width and height.</summary>
        /// <returns>This property returns true when this <see cref="T:SIS.Hardware.Size" /> structure has both a width and height of zero; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                if (this.width != 0d)
                {
                    return false;
                }

                return this.height == 0d;
            }
        }

        /// <summary>Gets or sets the horizontal component of this <see cref="T:SIS.Hardware.Size" /> structure.</summary>
        /// <returns>The horizontal component of this <see cref="T:SIS.Hardware.Size" /> structure, typically measured in pixels.</returns>
        /// <filterpriority>1</filterpriority>
        public double Width
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
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
        /// Adds the width and height of one <see cref="T:SIS.Hardware.Size"/> structure to the width and height of another <see cref="T:SIS.Hardware.Size"/> structure.
        /// </summary>
        /// <returns>
        /// A <see cref="T:SIS.Hardware.Size"/> structure that is the result of the addition operation.
        /// </returns>
        /// <param name="sz1">
        /// The first <see cref="T:SIS.Hardware.Size"/> structure to add.
        /// </param>
        /// <param name="sz2">
        /// The second <see cref="T:SIS.Hardware.Size"/> structure to add.
        /// </param>
        public static SizeD Add(SizeD sz1, SizeD sz2)
        {
            return new SizeD(sz1.Width + sz2.Width, sz1.Height + sz2.Height);
        }

        /// <summary>
        /// Subtracts the width and height of one <see cref="T:SIS.Hardware.Size"/> structure from the width and height of another <see cref="T:SIS.Hardware.Size"/> structure.
        /// </summary>
        /// <returns>
        /// A <see cref="T:SIS.Hardware.Size"/> structure that is a result of the subtraction operation.
        /// </returns>
        /// <param name="sz1">
        /// The <see cref="T:SIS.Hardware.Size"/> structure on the left side of the subtraction operator. 
        /// </param>
        /// <param name="sz2">
        /// The <see cref="T:SIS.Hardware.Size"/> structure on the right side of the subtraction operator. 
        /// </param>
        public static SizeD Subtract(SizeD sz1, SizeD sz2)
        {
            return new SizeD(sz1.Width - sz2.Width, sz1.Height - sz2.Height);
        }

        /// <summary>Adds the width and height of one <see cref="T:SIS.Hardware.Size" /> structure to the width and height of another <see cref="T:SIS.Hardware.Size" /> structure.</summary>
        /// <returns>A <see cref="T:System.Drawing.Size" /> structure that is the result of the addition operation.</returns>
        /// <param name="sz1">The first <see cref="T:SIS.Hardware.Size" /> structure to add. </param>
        /// <param name="sz2">The second <see cref="T:SIS.Hardware.Size" /> structure to add. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static SizeD operator +(SizeD sz1, SizeD sz2)
        {
            return SizeD.Add(sz1, sz2);
        }

        /// <summary>Tests whether two <see cref="T:SIS.Hardware.Size" /> structures are equal.</summary>
        /// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> have equal width and height; otherwise, false.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.Size" /> structure on the left side of the equality operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.Size" /> structure on the right of the equality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(SizeD sz1, SizeD sz2)
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
        public static explicit operator CoordinateD(SizeD size)
        {
            return new CoordinateD(size.Width, size.Height);
        }

        /// <summary>Tests whether two <see cref="T:SIS.Hardware.Size" /> structures are different.</summary>
        /// <returns>This operator returns true if <paramref name="sz1" /> and <paramref name="sz2" /> differ either in width or height; false if <paramref name="sz1" /> and <paramref name="sz2" /> are equal.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.Size" /> structure on the left of the inequality operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.Size" /> structure on the right of the inequality operator. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(SizeD sz1, SizeD sz2)
        {
            return !(sz1 == sz2);
        }

        /// <summary>Subtracts the width and height of one <see cref="T:SIS.Hardware.Size" /> structure from the width and height of another <see cref="T:SIS.Hardware.Size" /> structure.</summary>
        /// <returns>A <see cref="T:SIS.Hardware.Size" /> that is the result of the subtraction operation.</returns>
        /// <param name="sz1">The <see cref="T:SIS.Hardware.Size" /> structure on the left side of the subtraction operator. </param>
        /// <param name="sz2">The <see cref="T:SIS.Hardware.Size" /> structure on the right side of the subtraction operator. </param>
        /// <filterpriority>3</filterpriority>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static SizeD operator -(SizeD sz1, SizeD sz2)
        {
            return SizeD.Subtract(sz1, sz2);
        }

        /// <summary>
        /// Tests to see whether the specified object is a <see cref="T:SIS.Hardware.Size"/> structure with the same dimensions as this <see cref="T:SIS.Hardware.Size"/> structure.
        /// </summary>
        /// <returns>
        /// This method returns true if <paramref name="obj"/> is a <see cref="T:SIS.Hardware.Size"/> and has the same width and height as this <see cref="T:SIS.Hardware.Size"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to test. 
        /// </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is SizeD))
            {
                return false;
            }

            SizeD sizeF = (SizeD)obj;
            if (sizeF.Width != this.Width || sizeF.Height != this.Height)
            {
                return false;
            }

            return sizeF.GetType().Equals(this.GetType());
        }

        /// <summary>Returns a hash code for this <see cref="T:System.Drawing.Size" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:System.Drawing.Size" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        /// <summary>Converts a <see cref="T:SIS.Hardware.Size" /> structure to a <see cref="T:System.Drawing.Coordinate" /> structure.</summary>
        /// <returns>Returns a <see cref="T:System.Drawing.Coordinate" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public CoordinateD ToCoordinate()
        {
            return (CoordinateD)this;
        }

        /// <summary>Creates a human-readable string that represents this <see cref="T:SIS.Hardware.Size" /> structure.</summary>
        /// <returns>A string that represents this <see cref="T:SIS.Hardware.Size" /> structure.</returns>
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
    }
}