// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListColumn.cs" company="">
//   
// </copyright>
// <summary>
//   The list column.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    // column uses vector<double> to represent the data.
    /// <summary>
    /// The list column.
    /// </summary>
    public class ListColumn : Column
    {
        #region Fields

        /// <summary>
        /// The m_d max val.
        /// </summary>
        private double m_dMaxVal;

        /// <summary>
        /// The m_d min val.
        /// </summary>
        private double m_dMinVal;

        /// <summary>
        /// The m_l data.
        /// </summary>
        private List<double> m_lData;

        #endregion

        // Constructor.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ListColumn"/> class.
        /// </summary>
        public ListColumn()
        {
            this.m_dMinVal = 0;
            this.m_dMaxVal = 0;
            this.m_lData = new List<double>();
        }

        #endregion

        // Implementation of the base interface 
        #region Public Methods and Operators

        /// <summary>
        /// The add value.
        /// </summary>
        /// <param name="__dValue">
        /// The __d value.
        /// </param>
        public void AddValue(double __dValue)
        {
            this.m_lData.Add(__dValue);
        }

        // get all numbers in the first legal line
        // __cSep is _optional_ separator that can be used in addition to white space
        // TODO: Implement proper error handling.
        /// <summary>
        /// The add values from string.
        /// </summary>
        /// <param name="__strString">
        /// The __str string.
        /// </param>
        /// <param name="__cSep">
        /// The __c sep.
        /// </param>
        public void AddValuesFromString(string __strString, char __cSep)
        {
            string[] _sSubStrings = __strString.Split(null);

            foreach (string _sSubString in _sSubStrings)
            {
                this.AddValue(double.Parse(_sSubString));
            }
        }

        /// <summary>
        /// The get max.
        /// </summary>
        /// <param name="__iI">
        /// The __i i.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public override double GetMax(int __iI)
        {
            this.m_dMaxVal = this.FindMax(this.m_lData);
            return this.m_dMaxVal;
        }

        /// <summary>
        /// The get min.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public override double GetMin()
        {
            this.m_dMinVal = this.FindMin(this.m_lData);
            return this.m_dMinVal;
        }

        /// <summary>
        /// The get point count.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetPointCount()
        {
            return this.m_lData.Count;
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        /// <exception cref="ColumnIndexOutOfRangeException">
        /// </exception>
        public override double GetValue(int n)
        {
            try
            {
                if (n < 0 || n >= this.GetPointCount())
                {
                    throw new ColumnIndexOutOfRangeException("index out of range in VecColumn");
                }

                return this.m_lData[n];
            }
            catch (ColumnIndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1.0f;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The find max.
        /// </summary>
        /// <param name="__dArray">
        /// The __d array.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double FindMax(List<double> __dArray)
        {
            int _iLength = __dArray.Count;
            double _dMax = __dArray[0];

            for (int _iI = 0; _iI < _iLength; _iI++)
            {
                if (__dArray[_iI] > _dMax)
                {
                    _dMax = __dArray[_iI];
                }
            }

            return _dMax;
        }

        /// <summary>
        /// The find min.
        /// </summary>
        /// <param name="__dArray">
        /// The __d array.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double FindMin(List<double> __dArray)
        {
            int _iLength = __dArray.Count;
            double _dMin = __dArray[0];

            for (int _iI = 0; _iI < _iLength; _iI++)
            {
                if (__dArray[_iI] < _dMin)
                {
                    _dMin = __dArray[_iI];
                }
            }

            return _dMin;
        }

        #endregion
    }
}