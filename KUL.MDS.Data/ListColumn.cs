namespace SIS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    // column uses vector<double> to represent the data.
    public class ListColumn : Column
    {
        private double m_dMinVal;
        private double m_dMaxVal;
        private List<double> m_lData;

        // Constructor.
        public ListColumn()
        {
            this.m_dMinVal = 0;
            this.m_dMaxVal = 0;
            this.m_lData = new List<double>();
        }

        // Implementation of the base interface 
        public override int GetPointCount()
        {
            return this.m_lData.Count;
        }

        public override double GetValue(int n)
        {
            try
            {
                if (n < 0 || n >= this.GetPointCount())
                    throw new ColumnIndexOutOfRangeException("index out of range in VecColumn");
                return this.m_lData[n];
            }
            catch (ColumnIndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1.0f;
            }
        }

        public void AddValue(double __dValue)
        {
            this.m_lData.Add(__dValue);
        }

        // get all numbers in the first legal line
        // __cSep is _optional_ separator that can be used in addition to white space
        // TODO: Implement proper error handling.
        public void AddValuesFromString(string __strString, char __cSep)
        {
            string[] _sSubStrings = __strString.Split(null);
            
            foreach (string _sSubString in _sSubStrings)
            {
                this.AddValue(Double.Parse(_sSubString));
            }
        }

        public override double GetMin()
        {
            this.m_dMinVal = this.FindMin(this.m_lData);
            return this.m_dMinVal;
        }
        
        public override double GetMax(int __iI)
        {
            this.m_dMaxVal = this.FindMax(this.m_lData);
            return this.m_dMaxVal ;
        }

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
    }
}
