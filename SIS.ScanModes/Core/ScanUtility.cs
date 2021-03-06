﻿using System;

namespace SIS.ScanModes
{
    /// <summary>
    /// A set of supporting methods used in ScanMode generation.
    /// </summary>
    public static class ScanUtility
    {
        /// <summary>
        /// Builds an array of coordinates that describe a linear segment of motion according to a set of parameters.
        /// </summary>
        /// <param name="__iEquilibrationPts">The Amount of points used for waiting before the linear motion.</param>
        /// <param name="__iSpeedupSlowdownPts">The amount of points used for speeding up to- and slowing down from maximum speed.</param>
        /// <param name="__iCurvePts">The amount of points in the acual linear motion added with the speedup and slowdown points.</param>
        /// <param name="__dOffset">The starting offset of the linear motion in micrometer.</param>
        /// <param name="__dAmplitude">The amplitude of the linear motion in micrometer.</param>
        /// <returns></returns>
        public static double[] LinSegment(
            int __iEquilibrationPts, 
            int __iSpeedupSlowdownPts, 
            int __iCurvePts, 
            int __iSegmentPts,
            double __dOffset, 
            double __dAmplitude)
        {
            double[] _dMovement = new double[__iSegmentPts];

            for (int _iI = 0; _iI < __iEquilibrationPts; _iI++)
            {
                _dMovement[_iI] = __dOffset;
            }

            for (int _iI = 0; _iI < __iSpeedupSlowdownPts; _iI++)
            {
                double _dA = (__dAmplitude / (((double)__iCurvePts / (double)__iSpeedupSlowdownPts) - 1));
                double _dB = ((double)_iI / (2 * (double)__iSpeedupSlowdownPts));
                double _dC = (1 / (2 * Math.PI));
                double _dD = Math.Sin((_iI * Math.PI) / (double)__iSpeedupSlowdownPts);
                _dMovement[__iEquilibrationPts + _iI] = Math.Round(__dOffset + _dA * (_dB - (_dC * _dD)), 4);

            }

            for (int _iI = 0; _iI < __iCurvePts - (2 * __iSpeedupSlowdownPts); _iI++)
            {
                double _dA = (__dAmplitude / (((double)__iCurvePts / (double)__iSpeedupSlowdownPts) - 1));
                double _dB = (0.5 + ((double)_iI / (double)__iSpeedupSlowdownPts));
                _dMovement[__iEquilibrationPts + __iSpeedupSlowdownPts + _iI] = Math.Round(__dOffset + _dA * _dB, 4);
            }

            for (int _iI = 0; _iI < __iSpeedupSlowdownPts; _iI++)
            {
                double _dA = (__dAmplitude / (((double)__iCurvePts / (double)__iSpeedupSlowdownPts) - 1));
                double _dB = (((double)__iCurvePts - 2 * (double)__iSpeedupSlowdownPts) / (double)__iSpeedupSlowdownPts);
                double _dC = (((double)_iI + (double)__iSpeedupSlowdownPts) / (2 * (double)__iSpeedupSlowdownPts));
                double _dD = (1 / (2 * Math.PI));
                double _dE = Math.Sin(((double)_iI + (double)__iSpeedupSlowdownPts) * (Math.PI / (double)__iSpeedupSlowdownPts));
                _dMovement[__iEquilibrationPts + __iCurvePts - __iSpeedupSlowdownPts + _iI] = Math.Round(__dOffset + (_dA * (_dB + _dC - (_dD * _dE))), 4);
            }

            for (int _iI = __iEquilibrationPts + __iCurvePts; _iI < __iSegmentPts; _iI++)
            {
                _dMovement[_iI] = Math.Round(__dOffset + __dAmplitude, 4);
            }

            return _dMovement;
        }
    }
}
