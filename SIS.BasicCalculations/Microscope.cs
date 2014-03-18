using System;

namespace SIS.OpticalSystem
{
    public class Microscope
    {
        private double m_dn;
        private double m_dNA;

        private int m_iLambdaEm;
        private int m_iLambdaEx;

        public Microscope(int __iLambdaEx, int __iLambdaEm, double __dn, double __dNA)
        {
            this.m_iLambdaEx = __iLambdaEx;
            this.m_iLambdaEm = __iLambdaEm;
            this.m_dn = __dn;
            this.m_dNA = __dNA;
        }

        public int CFCriticalDistanceXY
        {
            get
            {
                return Convert.ToInt32(Math.Round(m_iLambdaEx / (8 * m_dn * Math.Sin(Alpha))));
            }
        }

        public int CFCriticalDistanceZ
        {
            get
            {
                return Convert.ToInt32(Math.Round(m_iLambdaEx / (4 * m_dn * (1 - Math.Cos(Alpha)))));
            }

        }

        public int WFCriticalDistanceXY
        {
            get
            {
                return Convert.ToInt32(Math.Round(m_iLambdaEm / (4 * m_dn * Math.Sin(Alpha))));
            }
        }

        public int WFCriticalDistanceZ
        {
            get
            {
                return Convert.ToInt32(Math.Round(m_iLambdaEm / (2 * m_dn * (1 - Math.Cos(Alpha)))));
            }
        }

        public double NA
        {
            get
            {
                return this.m_dNA;
            }
        }

        public double Alpha
        {
            get
            {
                return Math.Asin(this.m_dNA / this.m_dn);
            }
        }


    }
}
