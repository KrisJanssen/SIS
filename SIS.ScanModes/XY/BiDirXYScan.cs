using System;
using System.Collections.Generic;
using System.Text;
using SIS.SystemLayer;
using System.Linq;

namespace SIS.ScanModes
{
    /// <summary>
    /// Object supplies the necessary coordinates to a stage based upon the physical parameters that fully describe a unidirectional scan
    /// </summary>
    [ScanMode("Bidirectional XY Scan")]
    public class BiDirXYScan : Scanmode
    {
        #region Methods.

        /// <summary>
        /// UniDirXYScan constructor
        /// </summary>
        /// <param name="__iImageWidthPx">The width (X-dimension) of the image to acquire in pixels</param>
        /// <param name="__iImageHeightPx">The height (Y-dimension) of the image to acquire in pixels</param>
        /// <param name="__iXOverScanPx">The amount of extra pixels to scan in the X Dimension</param>
        /// <param name="__iYOverScanPx">The amount of extra pixels to scan in the Y Dimension</param>
        /// <param name="__dXScanSize">The physical width of the scan in nm</param>
        /// <param name="__dYScanSize">The physical height of the scan in nm</param>
        public BiDirXYScan(
            int __iImageWidthPx,
            int __iImageHeightPx,
            int __iXOverScanPx,
            int __iYOverScanPx,
            double __dXScanSize,
            double __dYScanSize,
            int __iSpeedupFactor,
            int __iReturnSpeedFactor)
            : base(__iImageWidthPx,
                __iImageHeightPx,
                __iXOverScanPx,
                __iYOverScanPx,
                __dXScanSize,
                __dYScanSize,
                __iSpeedupFactor,
                __iReturnSpeedFactor)
        {
            // X-Movement forward.
            this.m_iXPSfwd = Convert.ToInt32(Math.Round(this.m_iImageWidthPx * this.m_dSpeedupPct, 0));
            //this.m_iXPAfwd = this.m_iXPSfwd * 1;
            this.m_iXPAfwd = 0;
            //this.m_iXPEfwd = this.m_iXPAfwd;
            this.m_iXPEfwd = 0;
            this.m_iXCPfwd = this.m_iImageWidthPx + this.m_iXOverScanPx + 2 * this.m_iXPSfwd;
            this.m_iXPTfwd = this.m_iXPAfwd + this.m_iXCPfwd + this.m_iXPEfwd;

            // Calculate the required amplitude to set in order to have a real scanning distance as requested.
            this.m_dXGL = (((double)this.m_dXScanSizeNm / (double)(this.m_iImageWidthPx)) * (double)(this.m_iImageWidthPx + this.m_iXOverScanPx)) * ((double)(this.m_iXCPfwd - this.m_iXPSfwd) / (double)(this.m_iXCPfwd - (2 * this.m_iXPSfwd)));

            // X-Movement backward.
            this.m_iXPSbckwd = this.m_iXPSfwd;
            //this.m_iXPAbckwd = this.m_iXPAfwd;
            this.m_iXPAbckwd = this.m_iXPAfwd;
            this.m_iXPEbckwd = this.m_iXPEfwd;
            //this.m_iXCPbckwd = Convert.ToInt32(Math.Round((double)this.m_iXCPfwd / this.m_iReturnSpeedFactor, 0));
            this.m_iXCPbckwd = this.m_iXCPfwd;
            this.m_iXPTbckwd = this.m_iXPAbckwd + this.m_iXCPbckwd + this.m_iXPEbckwd;

            // The total amount of points can be assigned.
            this.m_iPtsPerScanline = 2 * this.m_iXPTfwd + this.m_iXPTbckwd;

            // Y-Movement (Y only advances forward in this ScanMode).
            /* 
                X and Y movement need to be in phase. Therefore int _iYSegmentPts = int _iXSegmentPts * 2
                The Y axis will move after the X axis has moved and before it is about to move back.
            */
            this.m_iYPSfwd = (this.m_iXPSfwd + this.m_iXPEfwd) / 2;
            this.m_iYCPfwd = 2 * this.m_iYPSfwd + (this.m_iXPSfwd + this.m_iXPEfwd) % 2;
            this.m_iYPAfwd = this.m_iXCPfwd - this.m_iXPSfwd;
            this.m_iYPEfwd = 0;
            this.m_iYPTfwd = this.m_iYPAfwd + this.m_iYCPfwd + this.m_iYPEfwd;
            this.m_dYGL = (this.m_dYScanSizeNm / (this.m_iImageHeightPx));

            // Calculate the width of the speedup border (points that will not be visible in the scan image).
            this.m_dBorderWidthX = this.m_dXGL / (2 * ((this.m_iXCPfwd / this.m_iXPSfwd) - 1));

            // Trigger line one should be active during the first rising on X.
            // Set the points that delimit the trig1 points for scanning.
            this.m_Triggers[0] = new Trigger(
                true,
                TriggerType.PulseTrigger,
                this.m_iXPAfwd + this.m_iXPSfwd + 1,
                this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx);

            this.m_Triggers[1] = new Trigger(
                true,
                TriggerType.PulseTrigger,
                this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXPSfwd + 1,
                this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx);

            this.m_Triggers[2] = new Trigger(
                true,
                TriggerType.PulseTrigger,
                2 * this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXPSfwd + 1,
                2 * this.m_iXPTfwd + this.m_iXPAfwd + this.m_iXPSfwd + this.m_iImageWidthPx + this.m_iXOverScanPx);

            // Set the amount of scanlines necessary.
            this.m_iRepeatNumber = (this.m_iImageHeightPx + this.m_iYOverScanPx) / 2 ;
        }

        // This method calculates XY scan coordinates expressed in nm for general use.
        protected override void CalculateNMScanCoordinates()
        {
            // The 2D array that will store all scan coordinates for both X and Y and that will be returned.
            double[,] _dMovement = new double[2, this.m_iPtsPerScanline];

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
                this.m_iXPTfwd,
                0.0,
                this.m_dXGL);

            // Generate the Backward segment for X and append it to the full list of coordinates, it will be repeated twice.
            _dCurrentSegmentbckwd = ScanUtility.LinSegment(
                this.m_iXPAbckwd, 
                this.m_iXPSbckwd,
                this.m_iXCPbckwd,
                this.m_iXCPbckwd,
                this.m_dXGL,
                -this.m_dXGL);

            // The X motion will go Forward/Backward/Forward/Backward so we add segments accordingly.
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentfwd);
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentbckwd);
            _ldCoordinateBuilder.AddRange(_dCurrentSegmentfwd);

            // Transfer the List<double> to an array.
            double[] _dX = _ldCoordinateBuilder.ToArray();

            _ldCoordinateBuilder.Clear();
            
            // Generate the Y coordinates. Y movement contains only one segment here so we do not need a List<double>.
            double[] _sectionOne = ScanUtility.LinSegment(
                this.m_iYPAfwd,
                this.m_iYPSfwd,
                this.m_iYCPfwd,
                this.m_iYPTfwd,
                0.0,
                this.m_dYGL);

            double[] _sectionTwo = ScanUtility.LinSegment(
                this.m_iYPAfwd,
                this.m_iYPSfwd,
                this.m_iYCPfwd,
                this.m_iYPTfwd + this.m_iYPTfwd,
                this.m_dYGL,
                this.m_dYGL);

            _ldCoordinateBuilder.AddRange(_sectionOne);
            _ldCoordinateBuilder.AddRange(_sectionTwo);

            double[] _dY = _ldCoordinateBuilder.ToArray();

            // Fill the 2D array containing all coordinates for both X and Y.
            for (int _iI = 0; _iI < this.m_iPtsPerScanline; _iI++)
            {
                // Bugfix. All wave generator movement is RELATIVE to positions set via absolute movements or analog operation. 
                // We should therefore never take the initial position into account. It is handled outside of the scan definition.
                _dMovement[0, _iI] = _dX[_iI];
                _dMovement[1, _iI] = _dY[_iI];
            }

            // Assign the coordinates.
            this.m_dNMScanCoordinates = _dMovement;
        }

        public override UInt32[] PostProcessData(
            UInt32[] __ui32Rawdata)
        {
            int szUint = sizeof(UInt32);
            // Finally we return the processed data.
            // In this case, no processing is necessary, data are already in the correct order.
            UInt32[] buffer = new UInt32[this.ImageWidthPx];
            UInt32[] processed = __ui32Rawdata;
            for (int i=1; i<this.ImageHeightPx; i+=2)
            {
                Buffer.BlockCopy(__ui32Rawdata, i * this.ImageWidthPx * szUint, buffer, 0, this.ImageWidthPx * szUint);
                buffer.Reverse();
                Buffer.BlockCopy(buffer, 0, processed, i * this.ImageWidthPx * szUint, this.ImageWidthPx * szUint);
            }
            return processed;
        }

        #endregion
    }
}
