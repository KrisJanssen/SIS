// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Column.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The column.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    /// <summary>
    /// The column.
    /// </summary>
    public abstract class Column
    {
        // Members.

        // step, 0.0f means step is not fixed.
        #region Fields

        /// <summary>
        /// The m_d step.
        /// </summary>
        private double m_dStep;

        /// <summary>
        /// The m_str name.
        /// </summary>
        private string m_strName;

        #endregion

        // Fields.

        // Constructor.
        // The default step is 0.0f, which means the stepsize is not fixed.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        public Column()
        {
            this.m_strName = string.Empty;
            this.m_dStep = 0.0f;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Column"/> class.
        /// </summary>
        /// <param name="__dStep">
        /// The __d step.
        /// </param>
        public Column(double __dStep)
        {
            this.m_dStep = __dStep;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_strName;
            }

            set
            {
                this.m_strName = value;
            }
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        public double Step
        {
            get
            {
                return this.m_dStep;
            }

            set
            {
                this.m_dStep = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get max.
        /// </summary>
        /// <param name="__iPointCount">
        /// The __i Point Count.
        /// </param>
        /// get maximum value in column;
        /// point_count must be specified if column has "unlimited" length, it is
        /// ignored otherwise
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public abstract double GetMax(int __iPointCount);

        /// <summary>
        /// The get min.
        /// </summary>
        /// get minimum value in column
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public abstract double GetMin();

        // Methods.
        // Return number of points or -1 for "unlimited" number of points.
        /// <summary>
        /// The get point count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public abstract int GetPointCount();

        // Return value of n'th point (starting from 0-th)
        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public abstract double GetValue(int n);

        #endregion
    }
}