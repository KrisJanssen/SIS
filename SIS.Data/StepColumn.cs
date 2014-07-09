// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StepColumn.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The step column.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System.Windows.Forms;

    // Represents a column of fixed-step data.
    /// <summary>
    /// The step column.
    /// </summary>
    public class StepColumn : Column
    {
        #region Fields

        /// <summary>
        /// The m_d start.
        /// </summary>
        private double m_dStart;

        // -1 means unlimited column length.
        /// <summary>
        /// The m_i count.
        /// </summary>
        private int m_iCount;

        #endregion

        // Constructors.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StepColumn"/> class.
        /// </summary>
        /// <param name="__dStart">
        /// The __d start.
        /// </param>
        public StepColumn(double __dStart)
            : base()
        {
            this.m_dStart = __dStart;
            this.m_iCount = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepColumn"/> class.
        /// </summary>
        /// <param name="__dStart">
        /// The __d start.
        /// </param>
        /// <param name="__dStep">
        /// The __d step.
        /// </param>
        public StepColumn(double __dStart, double __dStep)
            : base(__dStep)
        {
            this.m_dStart = __dStart;
            this.m_iCount = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepColumn"/> class.
        /// </summary>
        /// <param name="__dStart">
        /// The __d start.
        /// </param>
        /// <param name="__dStep">
        /// The __d step.
        /// </param>
        /// <param name="__iCount_">
        /// The __i count_.
        /// </param>
        public StepColumn(double __dStart, double __dStep, int __iCount_)
            : base(__dStep)
        {
            this.m_dStart = __dStart;
            this.m_iCount = __iCount_;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.m_iCount;
            }

            set
            {
                this.m_iCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        public double Start
        {
            get
            {
                return this.m_dStart;
            }

            set
            {
                this.m_dStart = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get max.
        /// </summary>
        /// <param name="__iPointCount">
        /// The __i point count.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public override double GetMax(int __iPointCount)
        {
            // TODO: Re-implement this assertiion.
            // assert(point_count != 0 || count != -1);
            int n = this.m_iCount == -1 ? __iPointCount : this.m_iCount;
            return this.GetValue(n - 1);
        }

        /// <summary>
        /// The get min.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public override double GetMin()
        {
            return this.m_dStart;
        }

        /// <summary>
        /// The get point count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetPointCount()
        {
            return this.m_iCount;
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="__iN">
        /// The __i n.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        /// <exception cref="ColumnIndexOutOfRangeException">
        /// </exception>
        public override double GetValue(int __iN)
        {
            try
            {
                if (this.m_iCount != -1 && (__iN < 0 || __iN >= this.m_iCount))
                {
                    throw new ColumnIndexOutOfRangeException("point index out of range");
                }

                return this.m_dStart + base.Step * __iN;
            }
            catch (ColumnIndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1.0f;
            }
        }

        #endregion
    }
}