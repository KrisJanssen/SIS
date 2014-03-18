using System;

namespace SIS.OpticalSystem
{
    public class Microscope
    {
        public Microscope(int __iLambdaEx, int __iLambdaEm, double __dn, double __dNA)
        {
        }

        public static int CFCriticalDistanceXY(int __iLambdaEx, double __dn, double __dAlpha)
        {
            return Convert.ToInt32(Math.Round(__iLambdaEx / (8 * __dn * Math.Sin(__dAlpha))));
        }

        public static int CFCriticalDistanceZ(int __iLambdaEx, double __dn, double __dAlpha)
        {
            return Convert.ToInt32(Math.Round(__iLambdaEx / (4 * __dn * (1 - Math.Cos(__dAlpha)))));

        }

        public static int WFCriticalDistanceXY(int __iLambdaEm, double __dn, double __dAlpha)
        {
            return Convert.ToInt32(Math.Round(__iLambdaEm / (4 * __dn * Math.Sin(__dAlpha))));
        }

        public static int WFCriticalDistanceZ(int __iLambdaEm, double __dn, double __dAlpha)
        {
            return Convert.ToInt32(Math.Round(__iLambdaEm / (2 * __dn * (1 - Math.Cos(__dAlpha)))));
        }

        public static double NA(double __dn, double __dAlpha)
        {
            return __dn * Math.Sin(__dAlpha);
        }

        public static double Alpha(double __dn, double __dNA)
        {
            return Math.Asin(__dNA / __dn);
        }


    }
}
