// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoordinateD.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan coordinate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime;

    /// <summary>
    /// The coordinate.
    /// </summary>
    public struct CoordinateD
    {
        #region Static Fields

        /// <summary>Represents a new instance of the <see cref="T:SIS.Hardware.Coordinate" /> class with member data left uninitialized.</summary>
        /// <filterpriority>1</filterpriority>
        public static readonly CoordinateD Empty;

        #endregion

        #region Fields

        /// <summary>
        /// The x.
        /// </summary>
        private double x;

        /// <summary>
        /// The y.
        /// </summary>
        private double y;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="CoordinateD"/> struct.
        /// </summary>
        static CoordinateD()
        {
            CoordinateD.Empty = new CoordinateD();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateD"/> struct. 
        /// Initializes a new instance of the <see cref="T:SIS.Hardware.Coordinate"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">
        /// The horizontal position of the point. 
        /// </param>
        /// <param name="y">
        /// The vertical position of the point. 
        /// </param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public CoordinateD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets a value indicating whether this <see cref="T:SIS.Hardware.Coordinate" /> is empty.</summary>
        /// <returns>true if both <see cref="P:SIS.Hardware.Coordinate.X" /> and <see cref="P:SIS.Hardware.Coordinate.Y" /> are 0; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                if (this.x != 0d)
                {
                    return false;
                }

                return this.y == 0d;
            }
        }

        /// <summary>Gets or sets the x-coordinate of this <see cref="T:SIS.Hardware.Coordinate" />.</summary>
        /// <returns>The x-coordinate of this <see cref="T:SIS.Hardware.Coordinate" />.</returns>
        /// <filterpriority>1</filterpriority>
        public double X
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

        /// <summary>Gets or sets the y-coordinate of this <see cref="T:SIS.Hardware.Coordinate" />.</summary>
        /// <returns>The y-coordinate of this <see cref="T:SIS.Hardware.Coordinate" />.</returns>
        /// <filterpriority>1</filterpriority>
        public double Y
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

        #region Public Methods and Operators

        /// <summary>
        /// Translates a given <see cref="T:SIS.Hardware.Coordinate"/> by a specified <see cref="T:System.Drawing.SizeF"/>.
        /// </summary>
        /// <returns>
        /// The translated <see cref="T:SIS.Hardware.Coordinate"/>.
        /// </returns>
        /// <param name="pt">
        /// The <see cref="T:SIS.Hardware.Coordinate"/> to translate.
        /// </param>
        /// <param name="sz">
        /// The <see cref="T:System.Drawing.SizeF"/> that specifies the numbers to add to the coordinates of <paramref name="pt"/>.
        /// </param>
        public static CoordinateD Add(CoordinateD pt, SizeD sz)
        {
            return new CoordinateD(pt.X + sz.Width, pt.Y + sz.Height);
        }

        /// <summary>
        /// Translates a <see cref="T:SIS.Hardware.Coordinate"/> by the negative of a specified size.
        /// </summary>
        /// <returns>
        /// The translated <see cref="T:SIS.Hardware.Coordinate"/>.
        /// </returns>
        /// <param name="pt">
        /// The <see cref="T:SIS.Hardware.Coordinate"/> to translate.
        /// </param>
        /// <param name="sz">
        /// The <see cref="T:System.Drawing.SizeF"/> that specifies the numbers to subtract from the coordinates of <paramref name="pt"/>.
        /// </param>
        public static CoordinateD Subtract(CoordinateD pt, SizeD sz)
        {
            return new CoordinateD(pt.X - sz.Width, pt.Y - sz.Height);
        }

        /// <summary>Translates the <see cref="T:SIS.Hardware.Coordinate" /> by the specified <see cref="T:System.Drawing.SizeF" />.</summary>
        /// <returns>The translated <see cref="T:SIS.Hardware.Coordinate" />.</returns>
        /// <param name="pt">The <see cref="T:SIS.Hardware.Coordinate" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to add to the x- and y-coordinates of the <see cref="T:SIS.Hardware.Coordinate" />.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static CoordinateD operator +(CoordinateD pt, SizeD sz)
        {
            return CoordinateD.Add(pt, sz);
        }

        /// <summary>Compares two <see cref="T:SIS.Hardware.Coordinate" /> structures. The result specifies whether the values of the <see cref="P:SIS.Hardware.Coordinate.X" /> and <see cref="P:SIS.Hardware.Coordinate.Y" /> properties of the two <see cref="T:SIS.Hardware.Coordinate" /> structures are equal.</summary>
        /// <returns>true if the <see cref="P:SIS.Hardware.Coordinate.X" /> and <see cref="P:SIS.Hardware.Coordinate.Y" /> values of the left and right <see cref="T:SIS.Hardware.Coordinate" /> structures are equal; otherwise, false.</returns>
        /// <param name="left">A <see cref="T:SIS.Hardware.Coordinate" /> to compare. </param>
        /// <param name="right">A <see cref="T:SIS.Hardware.Coordinate" /> to compare. </param>
        /// <filterpriority>3</filterpriority>
        public static bool operator ==(CoordinateD left, CoordinateD right)
        {
            if (left.X != right.X)
            {
                return false;
            }

            return left.Y == right.Y;
        }

        /// <summary>Determines whether the coordinates of the specified points are not equal.</summary>
        /// <returns>true to indicate the <see cref="P:SIS.Hardware.Coordinate.X" /> and <see cref="P:SIS.Hardware.Coordinate.Y" /> values of <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false. </returns>
        /// <param name="left">A <see cref="T:SIS.Hardware.Coordinate" /> to compare.</param>
        /// <param name="right">A <see cref="T:SIS.Hardware.Coordinate" /> to compare.</param>
        /// <filterpriority>3</filterpriority>
        public static bool operator !=(CoordinateD left, CoordinateD right)
        {
            return !(left == right);
        }

        /// <summary>Translates a <see cref="T:SIS.Hardware.Coordinate" /> by the negative of a specified <see cref="T:System.Drawing.SizeF" />. </summary>
        /// <returns>The translated <see cref="T:SIS.Hardware.Coordinate" />.</returns>
        /// <param name="pt">The <see cref="T:SIS.Hardware.Coordinate" /> to translate.</param>
        /// <param name="sz">The <see cref="T:System.Drawing.SizeF" /> that specifies the numbers to subtract from the coordinates of <paramref name="pt" />.</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static CoordinateD operator -(CoordinateD pt, SizeD sz)
        {
            return CoordinateD.Subtract(pt, sz);
        }

        /// <summary>
        /// Specifies whether this <see cref="T:SIS.Hardware.Coordinate"/> contains the same coordinates as the specified <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// This method returns true if <paramref name="obj"/> is a <see cref="T:SIS.Hardware.Coordinate"/> and has the same coordinates as this <see cref="T:System.Drawing.Point"/>.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="T:System.Object"/> to test. 
        /// </param>
        /// <filterpriority>1</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is CoordinateD))
            {
                return false;
            }

            CoordinateD pointF = (CoordinateD)obj;
            if (pointF.X != this.X || pointF.Y != this.Y)
            {
                return false;
            }

            return pointF.GetType().Equals(this.GetType());
        }

        /// <summary>Returns a hash code for this <see cref="T:SIS.Hardware.Coordinate" /> structure.</summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="T:SIS.Hardware.Coordinate" /> structure.</returns>
        /// <filterpriority>1</filterpriority>
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        /// <summary>Converts this <see cref="T:SIS.Hardware.Coordinate" /> to a human readable string.</summary>
        /// <returns>A string that represents this <see cref="T:SIS.Hardware.Coordinate" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            object[] objArray = new object[] { this.x, this.y };
            return string.Format(currentCulture, "{{X={0}, Y={1}}}", objArray);
        }

        #endregion
    }
}