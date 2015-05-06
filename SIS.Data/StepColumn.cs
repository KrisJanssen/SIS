using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace SIS.Data
{
    // Represents a column of fixed-step data.
    public class StepColumn : SIS.Data.Column
    {
        private double m_dStart;
        
        // -1 means unlimited column length.
        private int m_iCount;

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

        // Constructors.
        public StepColumn(double __dStart)
            : base()
        {
            this.m_dStart = __dStart;
            this.m_iCount = -1;
        }

        public StepColumn(double __dStart, double __dStep)
            : base(__dStep)
        {
            this.m_dStart = __dStart;
            this.m_iCount = -1;
        }

        public StepColumn(double __dStart, double __dStep, int __iCount_)
            : base(__dStep)
        {
            this.m_dStart = __dStart;
            this.m_iCount = __iCount_;
        }

        public override int GetPointCount()
        {
            return m_iCount;
        }

        public override double GetValue(int __iN)
        {
            try
            {
                if (m_iCount != -1 && (__iN < 0 || __iN >= m_iCount))
                    throw new ColumnIndexOutOfRangeException("point index out of range");
                return m_dStart + base.Step * __iN;
            }
            catch (ColumnIndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1.0f;
            }
        }

        public override double GetMin()
        {
            return m_dStart;
        }

        public override double GetMax(int __iPointCount)
        {
            // TODO: Re-implement this assertiion.
            // assert(point_count != 0 || count != -1);
            int n = (m_iCount == -1 ? __iPointCount : m_iCount);
            return this.GetValue(n - 1);
        }
    }
}
