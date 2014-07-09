// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ScrollRange.cs">
//   
// </copyright>
// <summary>
//   A simple struct to store minimum and maximum <see cref="double" /> type
//   values for the scroll range
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    /// <summary>
    /// A simple struct to store minimum and maximum <see cref="double" /> type
    /// values for the scroll range
    /// </summary>
    public struct ScrollRange
    {
        #region Fields

        /// <summary>
        /// The _is scrollable.
        /// </summary>
        private bool _isScrollable;

        /// <summary>
        /// The _max.
        /// </summary>
        private double _max;

        /// <summary>
        /// The _min.
        /// </summary>
        private double _min;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollRange"/> struct. 
        /// Construct a <see cref="ScrollRange"/> object given the specified data values.
        /// </summary>
        /// <param name="min">
        /// The minimum axis value limit for the scroll bar
        /// </param>
        /// <param name="max">
        /// The maximum axis value limit for the scroll bar
        /// </param>
        /// <param name="isScrollable">
        /// true to make this item scrollable, false otherwise
        /// </param>
        public ScrollRange(double min, double max, bool isScrollable)
        {
            this._min = min;
            this._max = max;
            this._isScrollable = isScrollable;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollRange"/> struct. 
        /// Sets the scroll range to default values of zero, and sets the <see cref="IsScrollable"/>
        /// property as specified.
        /// </summary>
        /// <param name="isScrollable">
        /// true to make this item scrollable, false otherwise
        /// </param>
        public ScrollRange(bool isScrollable)
        {
            this._min = 0.0;
            this._max = 0.0;
            this._isScrollable = isScrollable;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollRange"/> struct. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ScrollRange"/> object from which to copy
        /// </param>
        public ScrollRange(ScrollRange rhs)
        {
            this._min = rhs._min;
            this._max = rhs._max;
            this._isScrollable = rhs._isScrollable;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a property that determines if the <see cref="Axis" /> corresponding to
        /// this <see cref="ScrollRange" /> object can be scrolled.
        /// </summary>
        public bool IsScrollable
        {
            get
            {
                return this._isScrollable;
            }

            set
            {
                this._isScrollable = value;
            }
        }

        /// <summary>
        /// The maximum axis value limit for the scroll bar.
        /// </summary>
        public double Max
        {
            get
            {
                return this._max;
            }

            set
            {
                this._max = value;
            }
        }

        /// <summary>
        /// The minimum axis value limit for the scroll bar.
        /// </summary>
        public double Min
        {
            get
            {
                return this._min;
            }

            set
            {
                this._min = value;
            }
        }

        #endregion
    }
}