//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace SIS.ScanModes
//{
//    public class UniDirDigScanmode : Scanmode
//    {
//        public override void PreCalculatedScan(
//            int __iImageWidthPx, 
//            int __iOverScanPx, 
//            double __dInitXPos, 
//            double __dInitYPos, 
//            double __dScanSizeNm)
//        {
//            // Init some variables.
//            double _dCurrentXPos = __dInitXPos;
//            double _dCurrentYPos = __dInitYPos;
//            double _dMinXPos = __dInitXPos;
//            double _dMinYPos = __dInitYPos;
//            double _dMaxXPos = __dInitXPos + __dScanSizeNm;
//            double _dMaxYPos = __dInitYPos + __dScanSizeNm;

//            // Calculate the voltage resolution.
//            double _dXRes = (_dMaxXPos - _dMinXPos) / __iImageWidthPx;
//            double _dYRes = (_dMaxYPos - _dMinYPos) / __iImageWidthPx;

//            // Array to store the voltages for the entire move operation.
//            double[,] _dMovement = new double[2, ((__iImageWidthPx + __iOverScanPx) * __iImageWidthPx)];

//            // Calculate the actual voltages for the intended movement.
//            for (int _iI = 0; _iI < __iImageWidthPx; _iI++)
//            {
//                // Increment Y voltage.
//                // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//                _dCurrentYPos = Math.Round((_dMinYPos + _dYRes * _iI), 4);

//                for (int _iJ = 0; _iJ < (__iImageWidthPx + __iOverScanPx); _iJ++)
//                {
//                    // In case we are on a "left to right" scanline.
//                    // Other scanmodes can also be implemented (eg. unidirectional scanning).
//                    if (_iI % 2 == 0)
//                    {
//                        // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//                        _dCurrentXPos = Math.Round((_dMinXPos + _dXRes * _iJ), 4);
//                        //_intscandir = 1;
//                    }
//                    // in case we are on a "right to left" scanline.
//                    else
//                    {
//                        // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//                        _dCurrentXPos = Math.Round((_dMaxXPos - _dXRes * (_iJ + 1 - __iOverScanPx)), 4);
//                        //_intscandir = -1;
//                    }

//                    // Write voltage for X.
//                    _dMovement[0, _iI * (__iImageWidthPx + __iOverScanPx) + _iJ] = _dCurrentXPos;

//                    // Write voltage for Y.
//                    _dMovement[1, _iI * (__iImageWidthPx + __iOverScanPx) + _iJ] = _dCurrentYPos;
//                }
//            }

//            this.m_dScanCoordinates = _dMovement;
//        }

//        // As stated in the comments that accompany the routines in PaintToScreen, the scan data in file is always presented as follows:
//        //
//        // The physical Image:       The array holding the data with corresponding bitmap coordinates:
//        // 
//        // -------------             [p00 p01 p02 p10 p11 p12 p20 p21 p22]
//        // |p20 p21 p22|               |   |   |   |   |   |   |   |   |
//        // |p10 p11 p12|              b02 b12 b22 b01 b11 b21 b00 b10 b20
//        // |p00 p01 p02|              Count down in the bimap (b coordinates) for Y and count up for X!!!
//        // -------------
//        //
//        // The bitmap:
//        //
//        //         X
//        // (b00)--(b10)--(b20)
//        //   |      |      |
//        // (b01)--(b11)--(b21)Y
//        //   |      |      |
//        // (b02)--(b12)--(b22)
//        //
//        //
//        // During the scan data always get stored in an array in the order they come in. This way, data will be in an array in the right way if
//        // 1D, left to right scanning is used. Should one use 1D right to left scanning or 2 directional scanning then the acquired array of
//        // intensities needs to be post processed.
//        // 1D right to left scanning is useless given the nature of the stage, we will only handle 2D scanning:
//        //
//        // 2D scanning data basically means that every odd numbered line needs to be mirrored (switch left and right). Therefore, we check if
//        // index _iI (row, Y coord of bitmap) is divisible by two or not. If it is then nothing needs to be done, hence we copy values to
//        // identical array positions [_iI * __iImagewidth + _iJ].
//        // If it is not, then we have an odd row and we need to flip values left to right, so if original value indexes go from 0 to ImageWidth 
//        // then the processed value index should go from ImageWidth to 0, hence we substitue index _iJ (column, X coord of bitmap) with 
//        // ImageWidth - _iJ - 1. 
//        public override UInt32[] PostProcessData(UInt32[] __ui32Rawdata, int __iImageWidthPx, int __iOverScanPx)
//        {
//            UInt32[] _ui32Processed = new UInt32[__ui32Rawdata.Length];

//            for (int _iI = 0; _iI < __iImageWidthPx; _iI++)
//            {
//                for (int _iJ = 0; _iJ < (__iImageWidthPx + __iOverScanPx); _iJ++)
//                {
//                    if (_iI % 2 == 0)
//                    {
//                        // Store the measured value 1 to 1 in the internal data structure.
//                        _ui32Processed[_iI * (__iImageWidthPx + __iOverScanPx) + _iJ] = __ui32Rawdata[_iI * (__iImageWidthPx + __iOverScanPx) + _iJ];
//                    }
//                    else
//                    {
//                        // Flip values and store the measured value in the internal data structure.
//                        _ui32Processed[_iI * (__iImageWidthPx + __iOverScanPx) + __iImageWidthPx + __iOverScanPx - _iJ - 1] = __ui32Rawdata[_iI * (__iImageWidthPx + __iOverScanPx) + _iJ];
//                    }
//                }
//            }

//            // Finally we return the processed data.
//            return _ui32Processed;
//        }
//    }
//}
