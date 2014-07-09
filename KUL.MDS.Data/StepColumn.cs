namespace SIS.Data
{
    using System.Windows.Forms;

    // Represents a column of fixed-step data.
    public class StepColumn : Column
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
            return this.m_iCount;
        }

        public override double GetValue(int __iN)
        {
            try
            {
                if (this.m_iCount != -1 && (__iN < 0 || __iN >= this.m_iCount))
                    throw new ColumnIndexOutOfRangeException("point index out of range");
                return this.m_dStart + base.Step * __iN;
            }
            catch (ColumnIndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1.0f;
            }
        }

        public override double GetMin()
        {
            return this.m_dStart;
        }

        public override double GetMax(int __iPointCount)
        {
            // TODO: Re-implement this assertiion.
            // assert(point_count != 0 || count != -1);
            int n = (this.m_iCount == -1 ? __iPointCount : this.m_iCount);
            return this.GetValue(n - 1);
        }
    }
}
