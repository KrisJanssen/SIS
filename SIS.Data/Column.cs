using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Data
{
    public abstract class Column
    {
        // Members.
        private string m_strName;

        // step, 0.0f means step is not fixed.
        private double m_dStep;

        // Fields.
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

        // Constructor.
        // The default step is 0.0f, which means the stepsize is not fixed.
        public Column()
        {
            m_strName = string.Empty;
            m_dStep = 0.0f;
        }

        public Column(double __dStep)
        {
            m_dStep = __dStep;
        }

        // Methods.
        // Return number of points or -1 for "unlimited" number of points.
        public abstract int GetPointCount();

        // Return value of n'th point (starting from 0-th)
        public abstract double GetValue(int n);

        /// get minimum value in column
        public abstract double GetMin();

        /// get maximum value in column;
        /// point_count must be specified if column has "unlimited" length, it is
        /// ignored otherwise 
        public abstract double GetMax(int __iPointCount);
    }
}
