// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanUtility.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   A set of supporting methods used in ScanMode generation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.ScanModes.Core
{
    using System;

    /// <summary>
    /// A set of supporting methods used in ScanMode generation.
    /// </summary>
    public static class ScanUtility
    {
        #region Public Methods and Operators

        /// <summary>
        /// Builds an array of coordinates that describe a linear segment of motion according to a set of parameters.
        /// </summary>
        /// <param name="__iEquilibrationPts">
        /// The Amount of points used for waiting before and after the linear motion.
        /// </param>
        /// <param name="__iSpeedupSlowdownPts">
        /// The amount of points used for speeding up to- and slowing down from maximum speed.
        /// </param>
        /// <param name="__iCurvePoints">
        /// The amount of points in the acual linear motion added with the speedup and slowdown points.
        /// </param>
        /// <param name="__dOffset">
        /// The starting offset of the linear motion in micrometer.
        /// </param>
        /// <param name="__dAmplitude">
        /// The amplitude of the linear motion in micrometer.
        /// </param>
        /// <returns>
        /// The <see cref="double[]"/>.
        /// </returns>
        public static double[] LinSegment(
            int __iEquilibrationPts, 
            int __iSpeedupSlowdownPts, 
            int __iCurvePoints, 
            double __dOffset, 
            double __dAmplitude)
        {
            int _iTotalPoints = __iEquilibrationPts * 2 + __iCurvePoints;

            double[] _dMovement = new double[_iTotalPoints];

            for (int _iI = 0; _iI < __iEquilibrationPts; _iI++)
            {
                _dMovement[_iI] = __dOffset;
            }

            for (int _iI = 0; _iI < __iSpeedupSlowdownPts; _iI++)
            {
                double _dA = __dAmplitude / (((double)__iCurvePoints / (double)__iSpeedupSlowdownPts) - 1);
                double _dB = (double)_iI / (2 * (double)__iSpeedupSlowdownPts);
                double _dC = 1 / (2 * Math.PI);
                double _dD = Math.Sin((_iI * Math.PI) / (double)__iSpeedupSlowdownPts);
                _dMovement[__iEquilibrationPts + _iI] = Math.Round(__dOffset + _dA * (_dB - (_dC * _dD)), 4);
            }

            for (int _iI = 0; _iI < __iCurvePoints - (2 * __iSpeedupSlowdownPts); _iI++)
            {
                double _dA = __dAmplitude / (((double)__iCurvePoints / (double)__iSpeedupSlowdownPts) - 1);
                double _dB = 0.5 + ((double)_iI / (double)__iSpeedupSlowdownPts);
                _dMovement[__iEquilibrationPts + __iSpeedupSlowdownPts + _iI] = Math.Round(__dOffset + _dA * _dB, 4);
            }

            for (int _iI = 0; _iI < __iSpeedupSlowdownPts; _iI++)
            {
                double _dA = __dAmplitude / (((double)__iCurvePoints / (double)__iSpeedupSlowdownPts) - 1);
                double _dB = ((double)__iCurvePoints - 2 * (double)__iSpeedupSlowdownPts)
                             / (double)__iSpeedupSlowdownPts;
                double _dC = ((double)_iI + (double)__iSpeedupSlowdownPts) / (2 * (double)__iSpeedupSlowdownPts);
                double _dD = 1 / (2 * Math.PI);
                double _dE =
                    Math.Sin(((double)_iI + (double)__iSpeedupSlowdownPts) * (Math.PI / (double)__iSpeedupSlowdownPts));
                _dMovement[__iEquilibrationPts + __iCurvePoints - __iSpeedupSlowdownPts + _iI] =
                    Math.Round(__dOffset + (_dA * (_dB + _dC - (_dD * _dE))), 4);
            }

            for (int _iI = __iEquilibrationPts + __iCurvePoints; _iI < _iTotalPoints; _iI++)
            {
                _dMovement[_iI] = Math.Round(__dOffset + __dAmplitude, 4);
            }

            return _dMovement;
        }

        #endregion
    }
}