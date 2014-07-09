using System;
using System.Collections.Generic;
using System.Text;
using SIS.SystemLayer;


namespace SIS.ScanModes
{
    /// <summary>
    /// Object supplies the necessary coordinates to a stage based upon the physical parameters that fully describe a unidirectional STED scan
    /// </summary>
    [ScanMode("STED Unidirectional XZ Scan")]
    public class STEDUniDirXZScan : Scanmode
    {
        #region Methods.

        /// <summary>
        /// StedUniDirScan constructor
        /// </summary>
        /// <param name="__iImageWidthPx">The width (X-dimension) of the image to acquire in pixels</param>
        /// <param name="__iImageHeightPx">The height (Y-dimension) of the image to acquire in pixels</param>
        /// <param name="__iXOverScanPx">The amount of extra pixels to scan in the X Dimension</param>
        /// <param name="__iYOverScanPx">The amount of extra pixels to scan in the Y Dimension</param>
        /// <param name="__dInitXPos">The physical start X-position for the scan in nm</param>
        /// <param name="__dInitYPos">The physical start Y-position for the scan in nm</param>
        /// <param name="__dXScanSizeNm">The physical width of the scan in nm</param>
        /// <param name="__dYScanSizeNm">The physical height of the scan in nm</param>
        /// <param name="__dMaxSpeed">This parameter is RESERVED for future use</param>
        /// <param name="__dCycleTime">This parameter is RESERVED for future use</param>
        public STEDUniDirXZScan(
            int __iImageWidthPx,
            int __iImageHeightPx,
            int __iImageDepthPx,
            int __iXOverScanPx,
            int __iYOverScanPx,
            int __iZOverScanPx,
            double __dInitXPos,
            double __dInitYPos,
            double __dInitZPos,
            double __dXScanSizeNm,
            double __dYScanSizeNm,
            double __dZScanSizeNm,
            int __iSpeedupFactor,
            int __iReturnSpeedFactor,
            double __dMaxSpeed,
            double __dCycleTime)
            : base(__iImageWidthPx,
                __iImageHeightPx,
                __iImageDepthPx,
                __iXOverScanPx,
                __iYOverScanPx,
                __iZOverScanPx,
                __dInitXPos,
                __dInitYPos,
                __dInitZPos,
                __dXScanSizeNm,
                __dYScanSizeNm,
                __dZScanSizeNm,
                __iSpeedupFactor,
                __iReturnSpeedFactor,
                __dMaxSpeed,
                __dCycleTime)
        {
            // Set the scan axes.
            this.m_iScanAxes = (int)ScanAxesTypes.XZ;

            // X-Movement forward.
            // Speed up points are 10% of the total pixels of the imagewidth.
            this.m_iXPSfwd = Convert.ToInt32(Math.Round(this.m_iImageWidthPx * this.m_dSpeedupPct, 0));
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iXPAfwd = this.m_iXPSfwd * 1;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iXPEfwd = this.m_iXPAfwd;
            // The Curve points are the points that will actually be measured.
            this.m_iXCPfwd = this.m_iImageWidthPx + this.m_iXOverScanPx + 2 * this.m_iXPSfwd;
            // The Segment points are all points including curve
            this.m_iXPTfwd = this.m_iXPAfwd + this.m_iXCPfwd + this.m_iXPEfwd;
            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
            this.m_dXGLfwd = (((double)this.m_dXScanSizeNm / (double)(this.m_iImageWidthPx)) * (double)(this.m_iImageWidthPx + this.m_iXOverScanPx)) * ((double)(this.m_iXCPfwd - this.m_iXPSfwd) / (double)(this.m_iXCPfwd - (2 * this.m_iXPSfwd)));

            // X-Movement backward.
            // Speed up points are 10% of the total pixels of the imagewidth.
            this.m_iXPSbckwd = this.m_iXPSfwd;
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iXPAbckwd = this.m_iXPAfwd;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iXPEbckwd = this.m_iXPEfwd;
            // The Curve points are the points that will actually be measured.
            this.m_iXCPbckwd = Convert.ToInt32(Math.Round((double)this.m_iXCPfwd / this.m_iReturnSpeedFactor, 0));
            //this.m_iXCPbckwd = this.m_iXCPfwd;
            // The Segment points are all points including curve
            this.m_iXPTbckwd = this.m_iXPAbckwd + this.m_iXCPbckwd + this.m_iXPEbckwd;
            //this.m_iXPTbckwd = this.m_iXPTfwd;
            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
            this.m_dXGLbckwd = -this.m_dXGLfwd;

            // The total amount of points can be assigned.
            this.m_iPtsPerScanline = this.m_iXPTfwd * 2 + this.m_iXPTbckwd * 2;

            // Z-Movement (Z only advances forward in this ScanMode).
            // X and Z movement need to be in phase. Therefore int _iZSegmentPts = int _iXSegmentPts * 2
            this.m_iZPTfwd = this.m_iPtsPerScanline;
            // The Z axis will move after the X axis has moved and before it is about to move back.
            this.m_iZCPfwd = this.m_iXPAbckwd + this.m_iXPEfwd;
            // Speed up points are 10% of the total Z Curve Points.
            this.m_iZPSfwd = Convert.ToInt32(Math.Round(this.m_iZCPfwd * 0.10, 0));
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iZPAfwd = this.m_iXPTfwd * 2 + this.m_iXPTbckwd - this.m_iXPEfwd;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iZPEfwd = this.m_iZPTfwd - this.m_iZCPfwd - this.m_iZPAfwd;
            // The Curve points are the points that will actually be measured.
            this.m_dZGLfwd = (this.m_dZScanSizeNm / (this.m_iImageHeightPx));

            // Some Debug checks.
            Tracing.Ping("Total points X: " + (this.m_iXPTfwd * 2 + this.m_iXPTbckwd * 2).ToString());
            Tracing.Ping("Total points Z: " + this.m_iZPTfwd.ToString());

            // Calculate the width of the speedup border (points that will not be visible in the scan image).
            this.m_dBorderWidthX = this.m_dXGLfwd / (2 * ((this.m_iXCPfwd / this.m_iXPSfwd) - 1));
            
            // Trigger line one should be active during the first rising on X.
            // Set the points that delimit the trig1 points for scanning.
            this.m_bTrig1Set = false;
            this.m_bTrig12Set = false;
            this.m_bTrig13Set = true;
            this.m_bTrig14Set = false;
            this.m_iTrig1Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig1Start = this.m_iXPAfwd + this.m_iXPSfwd + 1;
            this.m_iTrig1End = this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;

            // Trigger line two should be active during the second rising on X.
            // Set the points that delimit the trig2 points for scanning.
            this.m_bTrig2Set = true;
            this.m_bTrig23Set = false;
            this.m_bTrig24Set = false;
            this.m_iTrig2Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig2Start = this.m_iXPTfwd + this.m_iXPTbckwd + this.m_iXPAfwd + this.m_iXPSfwd + 1;
            this.m_iTrig2End = this.m_iXPTfwd + this.m_iXPTbckwd + this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;

            // Trigger line three does not matter.
            // Set the points that delimit the trig3 points for scanning.
            this.m_bTrig3Set = false;
            this.m_bTrig34Set = false;
            this.m_iTrig3Type = (int)TriggerType.LevelTrigger;
            //this.m_iTrig3Start = this.m_iXPAfwd + this.m_iXPSfwd + 1;
            //this.m_iTrig3End = this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;
            this.m_iTrig3Start = 0; // This is intended to make switching faster
            this.m_iTrig3End = this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;

            // Trigger line four does not matter.
            // Set the points that delimit the trig4 points for scanning.
            this.m_bTrig4Set = false;
            this.m_iTrig4Type = (int)TriggerType.LevelTrigger;
            this.m_iTrig4Start = 0;
            this.m_iTrig4End = 0;

            // Set the amount of scanlines necessary.
            this.m_iRepeatNumber = this.m_iImageDepthPx + this.m_iZOverScanPx;
        }

        // TODO: Implement.
        protected override void CalculateAnalogScanCoordinates()
        {
            //this.m_dScanCoordinates = _dMovement;
        }

        // This method calculates XZ scan coordinates expressed in nm for general use.
        protected override void CalculateNMScanCoordinates()
        {
            // The 2D array that will store all scan coordinates for both X and Y and that will be returned.
            double[,] _dMovement = new double[3, this.m_iPtsPerScanline];

            // A List<double> that is used to build the full X scanline out of individual segments.
            List<double> _ldCoordinateBuilder = new List<double>(this.m_iPtsPerScanline);

            // A working array to store the current segment being generated.
            double[] _dCurrentSegmentfwd;
            double[] _dCurrentSegmentbckwd;
            
            // Generate the Forward segment for X and append it to the full list of coordinates, it will be repeated twice.
            _dCurrentSegmentfwd = ScanUtility.LinSegment(
                this.m_iXPAfwd, 
                this.m_iXPSfwd, 
                this.m_iXCPfwd, 
                0.0, 
                this.m_dXGLfwd);

            // Generate the Backward segment for X and append it to the full list of coordinates, it will be repeated twice.
            _dCurrentSegmentbckwd = ScanUtility.LinSegment(
                this.m_iXPAbckwd, this.m_iXPSbckwd, 
                this.m_iXCPbckwd, 
                0.0 + this.m_dXGLfwd, 
                this.m_dXGLbckwd);

            // The X motion will go Forward/Backward/Forward/Backward so we add segments accordingly.
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentfwd);
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentbckwd);
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentfwd);
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentbckwd);

            // Transfer the List<double> to an array.
            double[] _dX = _ldCoordinateBuilder.ToArray();

            // Generate the Z coordinates. Z movement contains only one segment here so we do not need a List<double>.
            double[] _dZ = ScanUtility.LinSegment(
                this.m_iZPAfwd, 
                this.m_iZPSfwd, 
                this.m_iZCPfwd, 
                0.0, 
                this.m_dZGLfwd);

            // Fill the 2D array containing all coordinates for both X and Z.
            for (int _iI = 0; _iI < this.m_iPtsPerScanline; _iI++)
            {
                _dMovement[0, _iI] = _dX[_iI];
                _dMovement[1, _iI] = this.m_dInitYPosNm;
                //_dMovement[1, _iI] = 0;
                _dMovement[2, _iI] = _dZ[_iI];
            } 

            // Assign the coordinates.
            this.m_dNMScanCoordinates = _dMovement;
        }

        // The physical Image:       The array holding the data with corresponding bitmap coordinates:
        // 
        // -------------             [p00 p01 p02 p10 p11 p12 p20 p21 p22]
        // |p20 p21 p22|               |   |   |   |   |   |   |   |   |
        // |p10 p11 p12|              b02 b12 b22 b01 b11 b21 b00 b10 b20
        // |p00 p01 p02|              Count down in the bitmap (b coordinates) for Y and count up for X!!!
        // -------------
        //
        // The bitmap:
        //
        //         X
        // (b00)--(b10)--(b20)
        //   |      |      |
        // (b01)--(b11)--(b21)Y
        //   |      |      |
        // (b02)--(b12)--(b22)
        //
        //
        // During the scan data always get stored in an array in the order they come in. This way, data will be in an array in the right way if
        // unidirectional, left to right scanning is used. Should one use unidirectional RIGHT to LEFT scanning or 2 directional scanning then 
        // the acquired array of intensities needs to be post processed. This is not the case here however.
        //
        public override UInt32[] PostProcessData(
            UInt32[] __ui32Rawdata)
        {
            // Finally we return the processed data.
            // In this case, no processing is necessary, data are already in the correct order.
            return __ui32Rawdata;
        }

        #endregion
    }
}
