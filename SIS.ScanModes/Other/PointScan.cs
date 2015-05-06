using System;
using System.Collections.Generic;
using System.Text;
using SIS.SystemLayer;


namespace SIS.ScanModes
{
    /// <summary>
    /// Object supplies the necessary coordinates to a stage based upon the physical parameters that fully describe a unidirectional scan
    /// </summary>
    [ScanMode("Point Scan")]
    public class PointScan : Scanmode
    {
        #region Methods.

        /// <summary>
        /// PointScan constructor
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
        public PointScan(
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
            this.m_iScanAxes = (int)ScanAxesTypes.XY;

            // X-Movement forward.
            // Speed up points are 10% of the total pixels of the imagewidth.
            this.m_iXPSfwd = 0;
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iXPAfwd = 0;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iXPEfwd = 0;
            // The Curve points are the points that will actually be measured.
            this.m_iXCPfwd = this.m_iImageWidthPx;
            // The Segment points are all points including curve
            this.m_iXPTfwd = this.m_iXPAfwd + this.m_iXCPfwd + this.m_iXPEfwd;
            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
            this.m_dXGLfwd = (((double)this.m_dXScanSizeNm / (double)(this.m_iImageWidthPx)) * (double)(this.m_iImageWidthPx + this.m_iXOverScanPx)) * ((double)(this.m_iXCPfwd - this.m_iXPSfwd) / (double)(this.m_iXCPfwd - (2 * this.m_iXPSfwd)));

            // X-Movement backward.
            // Speed up points are 10% of the total pixels of the imagewidth.
            this.m_iXPSbckwd = 0;
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iXPAbckwd = 0;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iXPEbckwd = 0;
            // The Curve points are the points that will actually be measured.
            this.m_iXCPbckwd = 0;
            //this.m_iXCPbckwd = this.m_iXCPfwd;
            // The Segment points are all points including curve
            this.m_iXPTbckwd = this.m_iXPAbckwd + this.m_iXCPbckwd + this.m_iXPEbckwd;
            //this.m_iXPTbckwd = this.m_iXPTfwd;
            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
            this.m_dXGLbckwd = -this.m_dXGLfwd;

            // The total amount of points can be assigned.
            this.m_iPtsPerScanline = this.m_iXPTfwd + this.m_iXPTbckwd;

            // Y-Movement (Y only advances forward in this ScanMode).
            // X and Y movement need to be in phase. Therefore int _iYSegmentPts = int _iXSegmentPts * 2
            this.m_iYPTfwd = this.m_iPtsPerScanline;
            // The Y axis will move after the X axis has moved and before it is about to move back.
            this.m_iYCPfwd = this.m_iXPAbckwd + this.m_iXPEfwd;
            // Speed up points are 10% of the total Y Curve Points.
            this.m_iYPSfwd = Convert.ToInt32(Math.Round(this.m_iYCPfwd * 0.10, 0));
            // Dead time where the piezo is allowed to settle before a movement.
            this.m_iYPAfwd = this.m_iXPTfwd - this.m_iXPEfwd;
            // Dead time where the piezo is allowed to settle after a movement.
            this.m_iYPEfwd = this.m_iYPTfwd - this.m_iYCPfwd - this.m_iYPAfwd;
            // The Curve points are the points that will actually be measured.
            this.m_dYGLfwd = (this.m_dYScanSizeNm / (this.m_iImageHeightPx));

            // Some Debug checks.
            Tracing.Ping("Total points X: " + (this.m_iXPTfwd * 2 + this.m_iXPTbckwd * 2).ToString());
            Tracing.Ping("Total points Y: " + this.m_iYPTfwd.ToString());

            // Calculate the width of the speedup border (points that will not be visible in the scan image).
            //this.m_dBorderWidthX = this.m_dXGLfwd / (2 * ((this.m_iXCPfwd / this.m_iXPSfwd) - 1));
            this.m_dBorderWidthX = 0;

            // Trigger line one should be active during the first rising on X.
            // Set the points that delimit the trig1 points for scanning.
            // XPAfwd and XPSfwd should be zero anyway, so the start trigger is at the first pixel.
            // The trigger should end after the width of the image in px, all other parameters are zero.
            // Therefore, in total there are ImafeWidthPx trigger pulses.
            this.m_bTrig1Set = false;
            this.m_bTrig12Set = true;
            this.m_bTrig13Set = false;
            this.m_bTrig14Set = false;
            this.m_iTrig1Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig1Start = this.m_iXPAfwd + this.m_iXPSfwd + 1;
            this.m_iTrig1End = this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;

            // Trigger line two does not matter.
            this.m_bTrig2Set = false;
            this.m_bTrig23Set = false;
            this.m_bTrig24Set = false;
            this.m_iTrig2Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig2Start = this.m_iXPAfwd + this.m_iXPSfwd + 1;
            this.m_iTrig2End = this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx;

            // Trigger line three does not matter.
            this.m_bTrig3Set = false;
            this.m_bTrig34Set = false;
            this.m_iTrig3Type = (int)TriggerType.LevelTrigger;
            this.m_iTrig3Start = 0;
            this.m_iTrig3End = 0;

            // Trigger line four does not matter.
            // Set the points that delimit the trig4 points for scanning.
            this.m_bTrig4Set = false;
            this.m_iTrig4Type = (int)TriggerType.LevelTrigger;
            this.m_iTrig4Start = 0;
            this.m_iTrig4End = 0;

            // Set the amount of scanlines necessary.
            // Since we are doing a point scan, there is no movement of the stage, hence no "special"  points.
            //this.m_iRepeatNumber = this.m_iImageHeightPx + this.m_iYOverScanPx;
            this.m_iRepeatNumber = this.m_iImageHeightPx;
        }

        // TODO: Implement.
        protected override void CalculateAnalogScanCoordinates()
        {
            //this.m_dScanCoordinates = _dMovement;
        }

        // This method calculates XY scan coordinates expressed in nm for general use.
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

            // Transfer the List<double> to an array.
            double[] _dX = _ldCoordinateBuilder.ToArray();

            // Generate the Y coordinates. Y movement contains only one segment here so we do not need a List<double>.
            double[] _dY = ScanUtility.LinSegment(
                this.m_iYPAfwd,
                this.m_iYPSfwd,
                this.m_iYCPfwd,
                0.0,
                this.m_dYGLfwd);

            // Fill the 2D array containing all coordinates for both X and Y.
            for (int _iI = 0; _iI < this.m_iPtsPerScanline; _iI++)
            {
                // Bugfix. All wave generator movement is RELATIVE to positions set via absolute movements or analog operation. 
                // We should therefore never take the initial position into account. It is handled outside of the scan definition.
                _dMovement[0, _iI] = 0;
                _dMovement[1, _iI] = 0;
                _dMovement[2, _iI] = 0;
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
