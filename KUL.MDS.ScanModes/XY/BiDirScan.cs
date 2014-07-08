//using System;
//using System.Collections.Generic;
//using System.Text;


//namespace KUL.MDS.ScanModes
//{
//    [ScanMode("Bidirectional Scan")]
//    public class BiDirScan : Scanmode
//    {
//        public BiDirScan(
//            int __iImageWidthPx,
//            int __iImageHeightPx,
//            int __iXOverScanPx,
//            int __iYOverScanPx,
//            double __dInitXPos,
//            double __dInitYPos,
//            double __dXScanSizeNm,
//            double __dYScanSizeNm,
//            double __dMaxSpeed,
//            double __dCycleTime)
//            : base(__iImageWidthPx,
//                __iImageHeightPx,
//                __iXOverScanPx,
//                __iYOverScanPx,
//                __dInitXPos,
//                __dInitYPos,
//                __dXScanSizeNm,
//                __dYScanSizeNm,
//                __dMaxSpeed,
//                __dCycleTime)
//        {
//            // X-Movement.
//            // Speed up points are 10% of the total pixels of the imagewidth.
//            this.m_iXPTfwd = Convert.ToInt32(Math.Round(this.m_iImageWidthPx * 0.10, 0));
//            // Dead time where the piezo is allowed to settle before a movement.
//            this.m_iXPAfwd = this.m_iXPTfwd * 100;
//            // Dead time where the piezo is allowed to settle after a movement.
//            this.m_iXPEfwd = this.m_iXPAfwd;
//            // The Curve points are the points that will actually be measured.
//            this.m_iXCPfwd = this.m_iImageWidthPx + this.m_iXOverScanPx + 2 * this.m_iXPTfwd;
//            // The Segment points are all points including curve
//            this.m_iXPTfwd = this.m_iXPAfwd + this.m_iXCPfwd + this.m_iXPEfwd;
//            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
//            this.m_dXGLfwd = (((double)this.m_dXScanSizeNm / (double)(1000.0 * this.m_iImageWidthPx)) * (double)(this.m_iImageWidthPx + this.m_iXOverScanPx)) * ((double)(this.m_iXCPfwd - this.m_iXPTfwd) / (double)(this.m_iXCPfwd - (2 * this.m_iXPTfwd)));

//            // Y-Movement.
//            // X and Y movement need to be in phase. Therefore int _iYSegmentPts = int _iXSegmentPts * 2
//            this.m_iYPTfwd = this.m_iXPTfwd * 2;
//            // The Y axis will move after the X axis has moved and before it is about to move back.
//            this.m_iYCPfwd = this.m_iXPAfwd + this.m_iXPEfwd;
//            // Speed up points are 10% of the total Y Curve Points.
//            this.m_iYPSfwd = Convert.ToInt32(Math.Round(this.m_iYCPfwd * 0.10, 0));
//            // Dead time where the piezo is allowed to settle before a movement.
//            this.m_iYPAfwd = (this.m_iYPTfwd - this.m_iYCPfwd) / 2;
//            // Dead time where the piezo is allowed to settle after a movement.
//            this.m_iYPEfwd = this.m_iYPAfwd;
//            // The Curve points are the points that will actually be measured.
//            this.m_dYGLfwd = (this.m_dYScanSizeNm / (1000.0 * this.m_iImageHeightPx));
//        }

//        protected override void CalculateAnalogScanCoordinates()
//        {
//            //// Init some variables.
//            //double _dCurrentXPos = this.m_dInitXPos;
//            //double _dCurrentYPos = this.m_dInitYPos;
//            //double _dMinXPos = this.m_dInitXPos;
//            //double _dMinYPos = this.m_dInitYPos;
//            //double _dMaxXPos = this.m_dInitXPos + this.m_dScanSizeNm;
//            //double _dMaxYPos = this.m_dInitYPos + this.m_dScanSizeNm;

//            //// Calculate the voltage resolution.
//            //double _dXRes = (_dMaxXPos - _dMinXPos) / this.m_iImageWidthPx;
//            //double _dYRes = (_dMaxYPos - _dMinYPos) / this.m_iImageWidthPx;

//            //// Array to store the voltages for the entire move operation.
//            //double[,] _dMovement = new double[2, ((this.m_iImageWidthPx + this.m_iOverScanPx) * this.m_iImageWidthPx)];

//            //// Calculate the actual voltages for the intended movement.
//            //for (int _iI = 0; _iI < this.m_iImageWidthPx; _iI++)
//            //{
//            //    // Increment Y voltage.
//            //    // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//            //    _dCurrentYPos = Math.Round((_dMinYPos + _dYRes * _iI), 4);

//            //    for (int _iJ = 0; _iJ < (this.m_iImageWidthPx + this.m_iOverScanPx); _iJ++)
//            //    {
//            //        // In case we are on a "left to right" scanline.
//            //        // Other scanmodes can also be implemented (eg. unidirectional scanning).
//            //        if (_iI % 2 == 0)
//            //        {
//            //            // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//            //            _dCurrentXPos = Math.Round((_dMinXPos + _dXRes * _iJ), 4);
//            //            //_intscandir = 1;
//            //        }
//            //        // in case we are on a "right to left" scanline.
//            //        else
//            //        {
//            //            // Voltage resolution of the DAQ board for AO is 305 microvolts, so we round to the 4th digit.
//            //            _dCurrentXPos = Math.Round((_dMaxXPos - _dXRes * (_iJ + 1 - this.m_iOverScanPx)), 4);
//            //            //_intscandir = -1;
//            //        }

//            //        // Write voltage for X.
//            //        _dMovement[0, _iI * (this.m_iImageWidthPx + this.m_iOverScanPx) + _iJ] = _dCurrentXPos;

//            //        // Write voltage for Y.
//            //        _dMovement[1, _iI * (this.m_iImageWidthPx + this.m_iOverScanPx) + _iJ] = _dCurrentYPos;
//            //    }
//            //}

//            //this.m_dScanCoordinates = _dMovement;
//        }

//        protected override void CalculateNMScanCoordinates()
//        {
//            double[,] _dMovement = new double[2, this.m_iXPTfwd * 2];

//            // Forward X scanline.
//            for (int _iI = 0; _iI < this.m_iXPAfwd; _iI++)
//            {
//                _dMovement[0, _iI] = 0.0;
//            }

//            for (int _iI = 0; _iI < this.m_iXPSfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPAfwd +_iI] = 
//                    0 + ((this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd)-1)) * ((_iI / (2 * this.m_iXPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin(_iI * (Math.PI / this.m_iXPSfwd)))));
//            }

//            for (int _iI = 0; _iI < this.m_iXCPfwd - (2 * this.m_iXPSfwd); _iI++)
//            {
//                _dMovement[0, this.m_iXPAfwd + this.m_iXPSfwd + _iI] = 
//                    0 + ((this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd) - 1)) * (0.5 + (_iI / this.m_iXPSfwd)));
//            }

//            for (int _iI = 0; _iI < this.m_iXPSfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPAfwd + this.m_iXCPfwd - this.m_iXPSfwd + _iI] = 0 + ((this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd) - 1)) * (((this.m_iXCPfwd - 2 * this.m_iXPSfwd) / this.m_iXPSfwd) + ((_iI + this.m_iXPSfwd) / (2 * this.m_iXPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin((_iI + this.m_iXPSfwd) * (Math.PI / this.m_iXPSfwd)))));
//            }

//            for (int _iI = this.m_iXPAfwd + this.m_iXCPfwd; _iI < this.m_iXPTfwd; _iI++)
//            {
//                _dMovement[0, _iI] = this.m_dXGLfwd;
//            }

//            // Backward X scanline.
//            for (int _iI = 0; _iI < this.m_iXPAfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPTfwd + _iI] = this.m_dXGLfwd;
//            }

//            for (int _iI = 0; _iI < this.m_iXPSfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPTfwd + this.m_iXPAfwd + _iI] =
//                    0 + ((-this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd) - 1)) * ((_iI / (2 * this.m_iXPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin(_iI * (Math.PI / this.m_iXPSfwd)))));
//            }

//            for (int _iI = 0; _iI < this.m_iXCPfwd - (2 * this.m_iXPSfwd); _iI++)
//            {
//                _dMovement[0, this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXPSfwd + _iI] =
//                    0 + ((-this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd) - 1)) * (0.5 + (_iI / this.m_iXPSfwd)));
//            }

//            for (int _iI = 0; _iI < this.m_iXPSfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXCPfwd - this.m_iXPSfwd + _iI] = 0 + ((-this.m_dXGLfwd / ((this.m_iXCPfwd / this.m_iXPSfwd) - 1)) * (((this.m_iXCPfwd - 2 * this.m_iXPSfwd) / this.m_iXPSfwd) + ((_iI + this.m_iXPSfwd) / (2 * this.m_iXPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin((_iI + this.m_iXPSfwd) * (Math.PI / this.m_iXPSfwd)))));
//            }

//            for (int _iI = this.m_iXPAfwd + this.m_iXCPfwd; _iI < this.m_iXPTfwd; _iI++)
//            {
//                _dMovement[0, this.m_iXPTfwd + _iI] = 0.0;
//            }

//            // Y Scanline.
//            for (int _iI = 0; _iI < this.m_iYPAfwd; _iI++)
//            {
//                _dMovement[0, this.m_iYPTfwd + _iI] = 0.0;
//            }

//            for (int _iI = 0; _iI < this.m_iYPSfwd; _iI++)
//            {
//                _dMovement[1, this.m_iYPAfwd + _iI] =
//                    0 + ((this.m_dYGLfwd / ((this.m_iYCPfwd / this.m_iYPSfwd) - 1)) * ((_iI / (2 * this.m_iYPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin(_iI * (Math.PI / this.m_iYPSfwd)))));
//            }

//            for (int _iI = 0; _iI < this.m_iYCPfwd - (2 * this.m_iYPSfwd); _iI++)
//            {
//                _dMovement[1, this.m_iYPAfwd + this.m_iYPSfwd + _iI] =
//                    0 + ((this.m_dYGLfwd / ((this.m_iYCPfwd / this.m_iYPSfwd) - 1)) * (0.5 + (_iI / this.m_iYPSfwd)));
//            }

//            for (int _iI = 0; _iI < this.m_iYPSfwd; _iI++)
//            {
//                _dMovement[1, this.m_iYPAfwd + this.m_iYCPfwd - this.m_iYPSfwd + _iI] = 0 + ((this.m_dYGLfwd / ((this.m_iYCPfwd / this.m_iYPSfwd) - 1)) * (((this.m_iYCPfwd - 2 * this.m_iYPSfwd) / this.m_iYPSfwd) + ((_iI + this.m_iYPSfwd) / (2 * this.m_iYPSfwd)) - ((1 / (2 * Math.PI)) * Math.Sin((_iI + this.m_iYPSfwd) * (Math.PI / this.m_iYPSfwd)))));
//            }

//            for (int _iI = this.m_iYPAfwd + this.m_iYCPfwd; _iI < this.m_iYPTfwd; _iI++)
//            {
//                _dMovement[1, _iI] = this.m_dYGLfwd;
//            }

//            this.m_dNMScanCoordinates = _dMovement;
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
//        public override UInt32[] PostProcessData(
//            UInt32[] __ui32Rawdata)
//        {
//            UInt32[] _ui32Processed = new UInt32[__ui32Rawdata.Length];

//            for (int _iI = 0; _iI < this.m_iImageHeightPx + this.m_iYOverScanPx; _iI++)
//            {
//                for (int _iJ = 0; _iJ < (this.m_iImageWidthPx + this.m_iXOverScanPx); _iJ++)
//                {
//                    if (_iI % 2 == 0)
//                    {
//                        // Store the measured value 1 to 1 in the internal data structure.
//                        _ui32Processed[_iI * (this.m_iImageWidthPx + this.m_iXOverScanPx) + _iJ] = __ui32Rawdata[_iI * (this.m_iImageWidthPx + this.m_iXOverScanPx) + _iJ];
//                    }
//                    else
//                    {
//                        // Flip values and store the measured value in the internal data structure.
//                        _ui32Processed[_iI * (this.m_iImageWidthPx + this.m_iXOverScanPx) + this.m_iImageWidthPx + this.m_iXOverScanPx - _iJ - 1] = __ui32Rawdata[_iI * (this.m_iImageWidthPx + this.m_iXOverScanPx) + _iJ];
//                    }
//                }
//            }

//            // Finally we return the processed data.
//            return _ui32Processed;
//        }
//    }
//}
