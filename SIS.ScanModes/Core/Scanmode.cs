using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace SIS.ScanModes
{
    /// <summary>
    /// ScanMode objects derived the abstract ScanMode supply the necessary coordinates to a stage based upon the physical parameters that fully describe a specific scantype
    /// </summary>
    public abstract class Scanmode
    {
        #region Members.

        // Scan Axes.
        protected int m_iScanAxes;

        // Physical properties that define the scan area.
        protected int m_iImageWidthPx;
        protected int m_iImageHeightPx;
        protected int m_iImageDepthPx;
        protected int m_iXOverScanPx;
        protected int m_iYOverScanPx;
        protected int m_iZOverScanPx;
        protected double m_dInitXPosNm;
        protected double m_dInitYPosNm;
        protected double m_dInitZPosNm;
        protected double m_dXScanSizeNm;
        protected double m_dYScanSizeNm;
        protected double m_dZScanSizeNm;
        protected double m_dBorderWidthX;
        protected double m_dMaxSpeed;
        protected double m_dCycleTime;

        // Curve characteristics for forward X movement.
        protected int m_iXPTfwd;
        protected int m_iXPAfwd;
        protected int m_iXPSfwd;
        protected int m_iXCPfwd;
        protected int m_iXPEfwd;
        protected double m_dXGLfwd;

        // Curve characteristics for backward X movement.
        protected int m_iXPTbckwd;
        protected int m_iXPAbckwd;
        protected int m_iXPSbckwd;
        protected int m_iXCPbckwd;
        protected int m_iXPEbckwd;
        protected double m_dXGLbckwd;

        // Curve characteristics for forward Y movement.
        protected int m_iYPTfwd;
        protected int m_iYPAfwd;
        protected int m_iYPSfwd;
        protected int m_iYCPfwd;
        protected int m_iYPEfwd;
        protected double m_dYGLfwd;

        // Curve characteristics for backward Y movement.
        protected int m_iYPTbckwd;
        protected int m_iYPAbckwd;
        protected int m_iYPSbckwd;
        protected int m_iYCPbckwd;
        protected int m_iYPEbckwd;
        protected double m_dYGLbckwd;

        // Curve characteristics for forward Z movement.
        protected int m_iZPTfwd;
        protected int m_iZPAfwd;
        protected int m_iZPSfwd;
        protected int m_iZCPfwd;
        protected int m_iZPEfwd;
        protected double m_dZGLfwd;

        // Curve characteristics for backward Z movement.
        protected int m_iZPTbckwd;
        protected int m_iZPAbckwd;
        protected int m_iZPSbckwd;
        protected int m_iZCPbckwd;
        protected int m_iZPEbckwd;
        protected double m_dZGLbckwd;

        // Total number of single points in a scanline. This number is the same for X and Y.
        protected int m_iPtsPerScanline;

        // Two numbers that help define scan speed.
        protected double m_dSpeedupPct;
        protected int m_iReturnSpeedFactor;

        // All triggers need startpoints.
        protected int m_iTrig1Start;
        protected int m_iTrig2Start;
        protected int m_iTrig3Start;
        protected int m_iTrig4Start;

        // All triggers need endpoints.
        protected int m_iTrig1End;
        protected int m_iTrig2End;
        protected int m_iTrig3End;
        protected int m_iTrig4End;

        // Booleans to indicate if triggers should be set.
        protected bool m_bTrig1Set;
        protected bool m_bTrig2Set;
        protected bool m_bTrig3Set;
        protected bool m_bTrig4Set;

        // Booleans to indicate if combined triggers should be set.
        protected bool m_bTrig12Set;
        protected bool m_bTrig13Set;
        protected bool m_bTrig14Set;
        protected bool m_bTrig23Set;
        protected bool m_bTrig24Set;
        protected bool m_bTrig34Set;

        // Int to indicate triggertype.
        protected int m_iTrig1Type;
        protected int m_iTrig2Type;
        protected int m_iTrig3Type;
        protected int m_iTrig4Type;

        // Hashtable
        protected Hashtable m_HT;

        protected int m_iRepeatNumber;

        protected double[,] m_dNMScanCoordinates;
        protected double[,] m_dAnalogScanCoordinates;

        #endregion

        #region Properties.

        /// <summary>
        /// 
        /// </summary>
        public int ScanAxes
        {
            get
            {
                return this.m_iScanAxes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig1Start
        {
            get
            {
                return this.m_iTrig1Start;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig2Start
        {
            get
            {
                return this.m_iTrig2Start;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig3Start
        {
            get
            {
                return this.m_iTrig3Start;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig4Start
        {
            get
            {
                return this.m_iTrig4Start;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig1End
        {
            get
            {
                return this.m_iTrig1End;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig2End
        {
            get
            {
                return this.m_iTrig2End;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig3End
        {
            get
            {
                return this.m_iTrig3End;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig4End
        {
            get
            {
                return this.m_iTrig4End;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig1Set
        {
            get
            {
                return this.m_bTrig1Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig2Set
        {
            get
            {
                return this.m_bTrig2Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig3Set
        {
            get
            {
                return this.m_bTrig3Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig4Set
        {
            get
            {
                return this.m_bTrig4Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig12Set
        {
            get
            {
                return this.m_bTrig12Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig13Set
        {
            get
            {
                return this.m_bTrig13Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig14Set
        {
            get
            {
                return this.m_bTrig14Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig23Set
        {
            get
            {
                return this.m_bTrig23Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig24Set
        {
            get
            {
                return this.m_bTrig24Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Trig34Set
        {
            get
            {
                return this.m_bTrig34Set;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig1Type
        {
            get
            {
                return this.m_iTrig1Type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig2Type
        {
            get
            {
                return this.m_iTrig2Type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig3Type
        {
            get
            {
                return this.m_iTrig3Type;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Trig4Type
        {
            get
            {
                return this.m_iTrig4Type;
            }
        }

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

        public int ImageDepthPx
        {
            get
            {
                return this.m_iImageDepthPx;
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

        public double ZScanSizeNm
        {
            get
            {
                return this.m_dZScanSizeNm;
            }
        }

        public double InitialX
        {
            get
            {
                return this.m_dInitXPosNm;
            }
        }

        public double InitialY
        {
            get
            {
                return this.m_dInitYPosNm;
            }
        }

        public double InitialZ
        {
            get
            {
                return this.m_dInitZPosNm;
            }
        }

        public double XAmplitude
        {
            get
            {
                return this.m_dXGLfwd;
            }
        }

        public double YAmplitude
        {
            get
            {
                return this.m_dYGLfwd;
            }
        }

        public double ZAmplitude
        {
            get
            {
                return this.m_dZGLfwd;
            }
        }

        public double BorderWidthX
        {
            get
            {
                return this.m_dBorderWidthX;
            }
        }

        //public double BorderWidthY
        //{
        //    get
        //    {
        //        return this.m_dRealInitYPosNm;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        public double[,] NMScanCoordinates
        {
            get
            {
                this.CalculateNMScanCoordinates();
                return this.m_dNMScanCoordinates;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double[,] AnalogScanCoordinates
        {
            get
            {
                if (this.m_dNMScanCoordinates != null)
                {
                    this.CalculateNMScanCoordinates();
                }

                this.CalculateAnalogScanCoordinates();

                return m_dAnalogScanCoordinates;
            }
        }

        #endregion

        # region Methods

        /// <summary>
        /// ScanMode constructor
        /// </summary>
        /// <param name="__iImageWidthPx">The width (X-dimension) of the image to acquire in pixels</param>
        /// <param name="__iImageHeightPx">The height (Y-dimension) of the image to acquire in pixels</param>
        /// <param name="__iImageDepthPx">The height (Z-dimension) of the image to acquire in pixels</param>
        /// <param name="__iXOverScanPx">The amount of extra pixels to scan in the X Dimension</param>
        /// <param name="__iYOverScanPx">The amount of extra pixels to scan in the Y Dimension</param>
        /// <param name="__iZOverScanPx">The amount of extra pixels to scan in the Z Dimension</param>
        /// <param name="__dInitXPos">The physical start X-position for the scan in nm</param>
        /// <param name="__dInitYPos">The physical start Y-position for the scan in nm</param>
        /// <param name="__dInitZPos">The physical start Z-position for the scan in nm</param>
        /// <param name="__dXScanSizeNm">The width (X-dimension) of the image to acquire in nm</param>
        /// <param name="__dYScanSizeNm">The height (Y-dimension) of the image to acquire in nm</param>
        /// <param name="__dZScanSizeNm">The depth (Z-dimension) of the image to acquire in nm</param>
        /// <param name="__iSpeedupPct">Value between 0 and 1 to indicate amount of speedup points in relation to image pixels</param>
        /// <param name="__iReturnSpeedFactor">Value to indicate how much faster return speed is relative to forward speed</param>
        /// <param name="__dMaxSpeed">This parameter is RESERVED for future use</param>
        /// <param name="__dCycleTime">This parameter is RESERVED for future use</param>
        public Scanmode(
            int __iImageWidthPx,
            int __iImageHeightPx,
            int __iImageDepthPx,
            int __iXOverScanPx,
            int __iYOverScanPx,
            int __iZOverScanPx,
            double __dInitXPosNm,
            double __dInitYPosNm,
            double __dInitZPosNm,
            double __dXScanSizeNm,
            double __dYScanSizeNm,
            double __dZScanSizeNm,
            int __iSpeedupPct,
            int __iReturnSpeedFactor,
            double __dMaxSpeed,
            double __dCycleTime)
        {
            this.m_iImageWidthPx = __iImageWidthPx;
            this.m_iImageHeightPx = __iImageHeightPx;
            this.m_iImageDepthPx = __iImageDepthPx;
            this.m_iXOverScanPx = __iXOverScanPx;
            this.m_iYOverScanPx = __iYOverScanPx;
            this.m_iZOverScanPx = __iZOverScanPx; 
            this.m_dInitXPosNm = __dInitXPosNm;
            this.m_dInitYPosNm = __dInitYPosNm;
            this.m_dInitZPosNm = __dInitZPosNm;
            this.m_dXScanSizeNm = __dXScanSizeNm;
            this.m_dYScanSizeNm = __dYScanSizeNm;
            this.m_dZScanSizeNm = __dZScanSizeNm;
            this.m_dSpeedupPct = (double)__iSpeedupPct / 100;

            if ((0 < __iReturnSpeedFactor) && (__iReturnSpeedFactor < 4))
            {
                this.m_iReturnSpeedFactor = __iReturnSpeedFactor;
            }
            else
            {
                this.m_iReturnSpeedFactor = 1;
            }

            this.m_dMaxSpeed = __dMaxSpeed;
            this.m_dCycleTime = __dCycleTime;

            m_iTrig1Start = 0;
            m_iTrig2Start = 0;
            m_iTrig3Start = 0;
            m_iTrig4Start = 0;

            m_iTrig1End = 0;
            m_iTrig2End = 0;
            m_iTrig3End = 0;
            m_iTrig4End = 0;

            m_bTrig1Set = false;
            m_bTrig2Set = false;
            m_bTrig3Set = false;
            m_bTrig4Set = false;

            m_bTrig12Set = false;
            m_bTrig13Set = false;
            m_bTrig14Set = false;
            m_bTrig23Set = false;
            m_bTrig24Set = false;
            m_bTrig34Set = false;

            m_iTrig1Type = (int)TriggerType.PulseTrigger;
            m_iTrig2Type = (int)TriggerType.PulseTrigger;
            m_iTrig3Type = (int)TriggerType.PulseTrigger;
            m_iTrig4Type = (int)TriggerType.PulseTrigger;

            this.CalculateAnalogScanCoordinates();
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CalculateAnalogScanCoordinates();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CalculateNMScanCoordinates();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="__ui32Rawdata"></param>
        /// <returns></returns>
        public abstract UInt32[] PostProcessData(
            UInt32[] __ui32Rawdata);

        #endregion
    }
}
