// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scanmode.cs" company="">
//   
// </copyright>
// <summary>
//   ScanMode objects derived the abstract ScanMode supply the necessary coordinates to a stage based upon the physical parameters that fully describe a specific scantype
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.ScanModes.Core
{
    using System;
    using System.Collections;

    using SIS.ScanModes.Enums;

    /// <summary>
    /// ScanMode objects derived the abstract ScanMode supply the necessary coordinates to a stage based upon the physical parameters that fully describe a specific scantype
    /// </summary>
    public abstract class Scanmode
    {
        // Scan Axes.
        #region Fields

        /// <summary>
        /// The m_ ht.
        /// </summary>
        protected Hashtable m_HT;

        /// <summary>
        /// The m_b trig 12 set.
        /// </summary>
        protected bool m_bTrig12Set;

        /// <summary>
        /// The m_b trig 13 set.
        /// </summary>
        protected bool m_bTrig13Set;

        /// <summary>
        /// The m_b trig 14 set.
        /// </summary>
        protected bool m_bTrig14Set;

        /// <summary>
        /// The m_b trig 1 set.
        /// </summary>
        protected bool m_bTrig1Set;

        /// <summary>
        /// The m_b trig 23 set.
        /// </summary>
        protected bool m_bTrig23Set;

        /// <summary>
        /// The m_b trig 24 set.
        /// </summary>
        protected bool m_bTrig24Set;

        /// <summary>
        /// The m_b trig 2 set.
        /// </summary>
        protected bool m_bTrig2Set;

        /// <summary>
        /// The m_b trig 34 set.
        /// </summary>
        protected bool m_bTrig34Set;

        /// <summary>
        /// The m_b trig 3 set.
        /// </summary>
        protected bool m_bTrig3Set;

        /// <summary>
        /// The m_b trig 4 set.
        /// </summary>
        protected bool m_bTrig4Set;

        /// <summary>
        /// The m_d analog scan coordinates.
        /// </summary>
        protected double[,] m_dAnalogScanCoordinates;

        /// <summary>
        /// The m_d border width x.
        /// </summary>
        protected double m_dBorderWidthX;

        /// <summary>
        /// The m_d cycle time.
        /// </summary>
        protected double m_dCycleTime;

        /// <summary>
        /// The m_d init x pos nm.
        /// </summary>
        protected double m_dInitXPosNm;

        /// <summary>
        /// The m_d init y pos nm.
        /// </summary>
        protected double m_dInitYPosNm;

        /// <summary>
        /// The m_d init z pos nm.
        /// </summary>
        protected double m_dInitZPosNm;

        /// <summary>
        /// The m_d max speed.
        /// </summary>
        protected double m_dMaxSpeed;

        /// <summary>
        /// The m_d nm scan coordinates.
        /// </summary>
        protected double[,] m_dNMScanCoordinates;

        /// <summary>
        /// The m_d speedup pct.
        /// </summary>
        protected double m_dSpeedupPct;

        /// <summary>
        /// The m_d time p pixel.
        /// </summary>
        protected double m_dTimePPixel;

        /// <summary>
        /// The m_d xg lbckwd.
        /// </summary>
        protected double m_dXGLbckwd;

        /// <summary>
        /// The m_d xg lfwd.
        /// </summary>
        protected double m_dXGLfwd;

        /// <summary>
        /// The m_d x scan size nm.
        /// </summary>
        protected double m_dXScanSizeNm;

        /// <summary>
        /// The m_d yg lbckwd.
        /// </summary>
        protected double m_dYGLbckwd;

        /// <summary>
        /// The m_d yg lfwd.
        /// </summary>
        protected double m_dYGLfwd;

        /// <summary>
        /// The m_d y scan size nm.
        /// </summary>
        protected double m_dYScanSizeNm;

        /// <summary>
        /// The m_d zg lbckwd.
        /// </summary>
        protected double m_dZGLbckwd;

        /// <summary>
        /// The m_d zg lfwd.
        /// </summary>
        protected double m_dZGLfwd;

        /// <summary>
        /// The m_d z scan size nm.
        /// </summary>
        protected double m_dZScanSizeNm;

        /// <summary>
        /// The m_i image depth px.
        /// </summary>
        protected int m_iImageDepthPx;

        /// <summary>
        /// The m_i image height px.
        /// </summary>
        protected int m_iImageHeightPx;

        /// <summary>
        /// The m_i image width px.
        /// </summary>
        protected int m_iImageWidthPx;

        /// <summary>
        /// The m_i pts per scanline.
        /// </summary>
        protected int m_iPtsPerScanline;

        /// <summary>
        /// The m_i repeat number.
        /// </summary>
        protected int m_iRepeatNumber;

        /// <summary>
        /// The m_i return speed factor.
        /// </summary>
        protected int m_iReturnSpeedFactor;

        /// <summary>
        /// The m_i scan axes.
        /// </summary>
        protected int m_iScanAxes;

        /// <summary>
        /// The m_i trig 1 end.
        /// </summary>
        protected int m_iTrig1End;

        /// <summary>
        /// The m_i trig 1 start.
        /// </summary>
        protected int m_iTrig1Start;

        /// <summary>
        /// The m_i trig 1 type.
        /// </summary>
        protected int m_iTrig1Type;

        /// <summary>
        /// The m_i trig 2 end.
        /// </summary>
        protected int m_iTrig2End;

        /// <summary>
        /// The m_i trig 2 start.
        /// </summary>
        protected int m_iTrig2Start;

        /// <summary>
        /// The m_i trig 2 type.
        /// </summary>
        protected int m_iTrig2Type;

        /// <summary>
        /// The m_i trig 3 end.
        /// </summary>
        protected int m_iTrig3End;

        /// <summary>
        /// The m_i trig 3 start.
        /// </summary>
        protected int m_iTrig3Start;

        /// <summary>
        /// The m_i trig 3 type.
        /// </summary>
        protected int m_iTrig3Type;

        /// <summary>
        /// The m_i trig 4 end.
        /// </summary>
        protected int m_iTrig4End;

        /// <summary>
        /// The m_i trig 4 start.
        /// </summary>
        protected int m_iTrig4Start;

        /// <summary>
        /// The m_i trig 4 type.
        /// </summary>
        protected int m_iTrig4Type;

        /// <summary>
        /// The m_i xc pbckwd.
        /// </summary>
        protected int m_iXCPbckwd;

        /// <summary>
        /// The m_i xc pfwd.
        /// </summary>
        protected int m_iXCPfwd;

        /// <summary>
        /// The m_i x over scan px.
        /// </summary>
        protected int m_iXOverScanPx;

        /// <summary>
        /// The m_i xp abckwd.
        /// </summary>
        protected int m_iXPAbckwd;

        /// <summary>
        /// The m_i xp afwd.
        /// </summary>
        protected int m_iXPAfwd;

        /// <summary>
        /// The m_i xp ebckwd.
        /// </summary>
        protected int m_iXPEbckwd;

        /// <summary>
        /// The m_i xp efwd.
        /// </summary>
        protected int m_iXPEfwd;

        /// <summary>
        /// The m_i xp sbckwd.
        /// </summary>
        protected int m_iXPSbckwd;

        /// <summary>
        /// The m_i xp sfwd.
        /// </summary>
        protected int m_iXPSfwd;

        /// <summary>
        /// The m_i xp tbckwd.
        /// </summary>
        protected int m_iXPTbckwd;

        /// <summary>
        /// The m_i xp tfwd.
        /// </summary>
        protected int m_iXPTfwd;

        /// <summary>
        /// The m_i yc pbckwd.
        /// </summary>
        protected int m_iYCPbckwd;

        /// <summary>
        /// The m_i yc pfwd.
        /// </summary>
        protected int m_iYCPfwd;

        /// <summary>
        /// The m_i y over scan px.
        /// </summary>
        protected int m_iYOverScanPx;

        /// <summary>
        /// The m_i yp abckwd.
        /// </summary>
        protected int m_iYPAbckwd;

        /// <summary>
        /// The m_i yp afwd.
        /// </summary>
        protected int m_iYPAfwd;

        /// <summary>
        /// The m_i yp ebckwd.
        /// </summary>
        protected int m_iYPEbckwd;

        /// <summary>
        /// The m_i yp efwd.
        /// </summary>
        protected int m_iYPEfwd;

        /// <summary>
        /// The m_i yp sbckwd.
        /// </summary>
        protected int m_iYPSbckwd;

        /// <summary>
        /// The m_i yp sfwd.
        /// </summary>
        protected int m_iYPSfwd;

        /// <summary>
        /// The m_i yp tbckwd.
        /// </summary>
        protected int m_iYPTbckwd;

        /// <summary>
        /// The m_i yp tfwd.
        /// </summary>
        protected int m_iYPTfwd;

        /// <summary>
        /// The m_i zc pbckwd.
        /// </summary>
        protected int m_iZCPbckwd;

        /// <summary>
        /// The m_i zc pfwd.
        /// </summary>
        protected int m_iZCPfwd;

        /// <summary>
        /// The m_i z over scan px.
        /// </summary>
        protected int m_iZOverScanPx;

        /// <summary>
        /// The m_i zp abckwd.
        /// </summary>
        protected int m_iZPAbckwd;

        /// <summary>
        /// The m_i zp afwd.
        /// </summary>
        protected int m_iZPAfwd;

        /// <summary>
        /// The m_i zp ebckwd.
        /// </summary>
        protected int m_iZPEbckwd;

        /// <summary>
        /// The m_i zp efwd.
        /// </summary>
        protected int m_iZPEfwd;

        /// <summary>
        /// The m_i zp sbckwd.
        /// </summary>
        protected int m_iZPSbckwd;

        /// <summary>
        /// The m_i zp sfwd.
        /// </summary>
        protected int m_iZPSfwd;

        /// <summary>
        /// The m_i zp tbckwd.
        /// </summary>
        protected int m_iZPTbckwd;

        /// <summary>
        /// The m_i zp tfwd.
        /// </summary>
        protected int m_iZPTfwd;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scanmode"/> class. 
        /// ScanMode constructor
        /// </summary>
        /// <param name="__iImageWidthPx">
        /// The width (X-dimension) of the image to acquire in pixels
        /// </param>
        /// <param name="__iImageHeightPx">
        /// The height (Y-dimension) of the image to acquire in pixels
        /// </param>
        /// <param name="__iImageDepthPx">
        /// The height (Z-dimension) of the image to acquire in pixels
        /// </param>
        /// <param name="__iXOverScanPx">
        /// The amount of extra pixels to scan in the X Dimension
        /// </param>
        /// <param name="__iYOverScanPx">
        /// The amount of extra pixels to scan in the Y Dimension
        /// </param>
        /// <param name="__iZOverScanPx">
        /// The amount of extra pixels to scan in the Z Dimension
        /// </param>
        /// <param name="__dInitXPosNm">
        /// The __d Init X Pos Nm.
        /// </param>
        /// <param name="__dInitYPosNm">
        /// The __d Init Y Pos Nm.
        /// </param>
        /// <param name="__dInitZPosNm">
        /// The __d Init Z Pos Nm.
        /// </param>
        /// <param name="__dXScanSizeNm">
        /// The width (X-dimension) of the image to acquire in nm
        /// </param>
        /// <param name="__dYScanSizeNm">
        /// The height (Y-dimension) of the image to acquire in nm
        /// </param>
        /// <param name="__dZScanSizeNm">
        /// The depth (Z-dimension) of the image to acquire in nm
        /// </param>
        /// <param name="__dTimePPixel">
        /// The time per pixel in ms
        /// </param>
        /// <param name="__iSpeedupPct">
        /// Value between 0 and 1 to indicate amount of speedup points in relation to image pixels
        /// </param>
        /// <param name="__iReturnSpeedFactor">
        /// Value to indicate how much faster return speed is relative to forward speed
        /// </param>
        /// <param name="__dMaxSpeed">
        /// This parameter is RESERVED for future use
        /// </param>
        /// <param name="__dCycleTime">
        /// This parameter is RESERVED for future use
        /// </param>
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
            double __dTimePPixel, 
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
            this.m_dTimePPixel = __dTimePPixel;
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

            this.m_iTrig1Start = 0;
            this.m_iTrig2Start = 0;
            this.m_iTrig3Start = 0;
            this.m_iTrig4Start = 0;

            this.m_iTrig1End = 0;
            this.m_iTrig2End = 0;
            this.m_iTrig3End = 0;
            this.m_iTrig4End = 0;

            this.m_bTrig1Set = false;
            this.m_bTrig2Set = false;
            this.m_bTrig3Set = false;
            this.m_bTrig4Set = false;

            this.m_bTrig12Set = false;
            this.m_bTrig13Set = false;
            this.m_bTrig14Set = false;
            this.m_bTrig23Set = false;
            this.m_bTrig24Set = false;
            this.m_bTrig34Set = false;

            this.m_iTrig1Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig2Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig3Type = (int)TriggerType.PulseTrigger;
            this.m_iTrig4Type = (int)TriggerType.PulseTrigger;

            this.CalculateAnalogScanCoordinates();
        }

        #endregion

        #region Public Properties

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

                return this.m_dAnalogScanCoordinates;
            }
        }

        /// <summary>
        /// Gets the border width x.
        /// </summary>
        public double BorderWidthX
        {
            get
            {
                return this.m_dBorderWidthX;
            }
        }

        /// <summary>
        /// Gets the image depth px.
        /// </summary>
        public int ImageDepthPx
        {
            get
            {
                return this.m_iImageDepthPx;
            }
        }

        /// <summary>
        /// Gets the image height px.
        /// </summary>
        public int ImageHeightPx
        {
            get
            {
                return this.m_iImageHeightPx;
            }
        }

        /// <summary>
        /// Gets the image width px.
        /// </summary>
        public int ImageWidthPx
        {
            get
            {
                return this.m_iImageWidthPx;
            }
        }

        /// <summary>
        /// Gets the initial x.
        /// </summary>
        public double InitialX
        {
            get
            {
                return this.m_dInitXPosNm;
            }
        }

        /// <summary>
        /// Gets the initial y.
        /// </summary>
        public double InitialY
        {
            get
            {
                return this.m_dInitYPosNm;
            }
        }

        /// <summary>
        /// Gets the initial z.
        /// </summary>
        public double InitialZ
        {
            get
            {
                return this.m_dInitZPosNm;
            }
        }

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
        public int RepeatNumber
        {
            get
            {
                return this.m_iRepeatNumber;
            }
        }

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
        /// Gets the time p pixel.
        /// </summary>
        public double TimePPixel
        {
            get
            {
                return this.m_dTimePPixel;
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
        public int Trig4Type
        {
            get
            {
                return this.m_iTrig4Type;
            }
        }

        /// <summary>
        /// Gets the x amplitude.
        /// </summary>
        public double XAmplitude
        {
            get
            {
                return this.m_dXGLfwd;
            }
        }

        /// <summary>
        /// Gets the x scan size nm.
        /// </summary>
        public double XScanSizeNm
        {
            get
            {
                return this.m_dXScanSizeNm;
            }
        }

        /// <summary>
        /// Gets the y amplitude.
        /// </summary>
        public double YAmplitude
        {
            get
            {
                return this.m_dYGLfwd;
            }
        }

        /// <summary>
        /// Gets the y scan size nm.
        /// </summary>
        public double YScanSizeNm
        {
            get
            {
                return this.m_dYScanSizeNm;
            }
        }

        /// <summary>
        /// Gets the z amplitude.
        /// </summary>
        public double ZAmplitude
        {
            get
            {
                return this.m_dZGLfwd;
            }
        }

        /// <summary>
        /// Gets the z scan size nm.
        /// </summary>
        public double ZScanSizeNm
        {
            get
            {
                return this.m_dZScanSizeNm;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        /// <param name="__uint32Rawdata">
        /// </param>
        /// <returns>
        /// The <see cref="uint[]"/>.
        /// </returns>
        public abstract uint[] PostProcessData(uint[] __uint32Rawdata);

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CalculateAnalogScanCoordinates();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CalculateNMScanCoordinates();

        #endregion
    }
}