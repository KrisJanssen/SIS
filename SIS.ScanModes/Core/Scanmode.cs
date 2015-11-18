using System;

namespace SIS.ScanModes
{
    /// <summary>
    /// ScanMode objects supply the necessary coordinates to a stage based upon physical parameters:
    /// * The desired physical size of the scan area.
    /// * The size of the image in pixels
    /// 
    /// Motion waveforms are calculated according to the the calculations provided by PI in the documentation of the GCS2 library (e.g. document SM151E).
    /// These calculations allow the defenition of continuous successions of motion regimes for a particular motion segment:
    /// * PA:                       # of points where the channel is at rest, i.e. static
    /// * PS:                       # of points where the channel is ramping up to desired speed
    /// * CP - 2 * PS:              # of points in the linear phase where the channel is moving at the final speed obtained during speed-up
    /// * PS:                       # of points where the channel speed is ramping down, this number of points is always identical to the speed-up points
    /// * PE:                       # of points where the channel is at rest at the end of the segment. PA and PE need not be identical
    /// * CP:                       # of points in the curve segment, exluding PA and PE
    /// * PT:                       # of points in the curve segment
    /// 
    /// * GL:                       The physical amplitude of the segment corresponding to CP.
    /// 
    /// A scan typically involves a forward and a backward motion on the fast axis (X by onvention) whereas the slow axis typically only progresses forward.
    /// PA, PS, PE and CP can nonetheless be defined for for forward and backward motion independently.
    /// 
    /// GL is shared for forward an backward motion. It's value is typically calculated: users supply the desired phycial size of the linear motion range.
    /// This range corresponds to CP - PA - PE - 2 * PS and this knowledge can be used to calculate GL such that is covers CP.
    /// 
    /// Scanmodes should further provide an array of up to 16 Trigger structures that can be used to synchronize events with motion.
    /// </summary>
    public abstract class Scanmode
    {
        #region Members.

        // Physical properties that define the scan area.
        protected int m_iImageWidthPx;
        protected int m_iImageHeightPx;
        protected int m_iXOverScanPx;
        protected int m_iYOverScanPx;
        
        protected double m_dXScanSizeNm;
        protected double m_dYScanSizeNm;

        protected double m_dBorderWidthX;
        protected double m_dMaxSpeed;
        protected double m_dCycleTime;

        // Curve characteristics for forward X movement.
        protected int m_iXPTfwd;
        protected int m_iXPAfwd;
        protected int m_iXPSfwd;
        protected int m_iXCPfwd;
        protected int m_iXPEfwd;

        // Curve characteristics for backward X movement.
        protected int m_iXPTbckwd;
        protected int m_iXPAbckwd;
        protected int m_iXPSbckwd;
        protected int m_iXCPbckwd;
        protected int m_iXPEbckwd;

        protected double m_dXGL;

        // Curve characteristics for forward Y movement.
        protected int m_iYPTfwd;
        protected int m_iYPAfwd;
        protected int m_iYPSfwd;
        protected int m_iYCPfwd;
        protected int m_iYPEfwd;

        // Curve characteristics for backward Y movement.
        protected int m_iYPTbckwd;
        protected int m_iYPAbckwd;
        protected int m_iYPSbckwd;
        protected int m_iYCPbckwd;
        protected int m_iYPEbckwd;

        protected double m_dYGL;

        // Total number of single points in a scanline. This number is the same for X and Y.
        protected int m_iPtsPerScanline;

        // Two numbers that help define scan speed.
        protected double m_dSpeedupPct;
        protected int m_iReturnSpeedFactor;

        protected int m_iRepeatNumber;

        protected double[,] m_dNMScanCoordinates;
        protected double[,] m_dAnalogScanCoordinates;

        protected Trigger[] m_Triggers;

        #endregion

        #region Properties.

        /// <summary>
        /// 
        /// </summary>
        public int RepeatNumber
        {
            get
            {
                return this.m_iRepeatNumber;
            }
        }

        public int ImageWidthPx
        {
            get
            {
                return this.m_iImageWidthPx;
            }
        }

        public int ImageHeightPx
        {
            get
            {
                return this.m_iImageHeightPx;
            }
        }

        public double XScanSizeNm
        {
            get
            {
                return this.m_dXScanSizeNm;
            }
        }

        public double YScanSizeNm
        {
            get
            {
                return this.m_dYScanSizeNm;
            }
        }

        public double XAmplitude
        {
            get
            {
                return this.m_dXGL;
            }
        }

        public double YAmplitude
        {
            get
            {
                return this.m_dYGL;
            }
        }

        public double BorderWidthX
        {
            get
            {
                return this.m_dBorderWidthX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double[,] ScanCoordinates
        {
            get
            {
                this.CalculateNMScanCoordinates();
                return this.m_dNMScanCoordinates;
            }
        }

        public Trigger[] Triggers
        {
            get
            {
                return this.Triggers;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// ScanMode constructor
        /// </summary>
        /// <param name="__iImageWidthPx">The width (X-dimension) of the image to acquire in pixels</param>
        /// <param name="__iImageHeightPx">The height (Y-dimension) of the image to acquire in pixels</param>
        /// <param name="__iXOverScanPx">The amount of extra pixels to scan in the X Dimension</param>
        /// <param name="__iYOverScanPx">The amount of extra pixels to scan in the Y Dimension</param>
        /// <param name="__dXScanSize">The width (X-dimension) of the image to acquire in nm</param>
        /// <param name="__dYScanSize">The height (Y-dimension) of the image to acquire in nm</param>
        /// <param name="__iSpeedupPct">Value between 0 and 1 to indicate amount of speedup points in relation to image pixels</param>
        /// <param name="__iReturnSpeedFactor">Value to indicate how much faster return speed is relative to forward speed</param>
        public Scanmode(
            int __iImageWidthPx,
            int __iImageHeightPx,
            int __iXOverScanPx,
            int __iYOverScanPx,
            double __dXScanSize,
            double __dYScanSize,
            int __iSpeedupPct,
            int __iReturnSpeedFactor
            )
        {
            this.m_iImageWidthPx = __iImageWidthPx;
            this.m_iImageHeightPx = __iImageHeightPx;
            this.m_iXOverScanPx = __iXOverScanPx;
            this.m_iYOverScanPx = __iYOverScanPx;
            this.m_dXScanSizeNm = __dXScanSize;
            this.m_dYScanSizeNm = __dYScanSize;
            this.m_dBorderWidthX = 0.0;

            if ((0 <= __iSpeedupPct) && (__iSpeedupPct <= 100))
            {
                this.m_dSpeedupPct = (double)__iSpeedupPct / 100;
            }
            else
            {
                this.m_dSpeedupPct = 0.1;
            }
            

            if ((0 < __iReturnSpeedFactor) && (__iReturnSpeedFactor < 5))
            {
                this.m_iReturnSpeedFactor = __iReturnSpeedFactor;
            }
            else
            {
                this.m_iReturnSpeedFactor = 1;
            }

            this.m_Triggers = new Trigger[16]
            {
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0),
                new Trigger(false, TriggerType.PulseTrigger, 0, 0)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CalculateNMScanCoordinates();

        #endregion
    }
}
