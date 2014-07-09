using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.Documents
{
    public struct ScanSettings
    {
        private UInt16 m_iImageWidthPx;
        private UInt16 m_iImageHeightPx;
        private UInt16 m_iImageDepthPx;
        private UInt16 m_iXOverScanPx;
        private UInt16 m_iYOverScanPx;
        private UInt16 m_iZOverScanPx;
        private double m_dTimePPixel;
        private double m_dXScanSizeNm;
        private double m_dYScanSizeNm;
        private double m_dZScanSizeNm;
        private double m_dInitXNm;
        private double m_dInitYNm;
        private double m_dInitZNm;
        private int m_iChannels;
        private string m_sAnnotation;

        public UInt16 ImageWidthPx
        {
            get
            {
                return this.m_iImageWidthPx;
            }
            set
            {
                this.m_iImageWidthPx = value;
            }
        }

        public UInt16 ImageHeightPx
        {
            get
            {
                return this.m_iImageHeightPx;
            }
            set
            {
                this.m_iImageHeightPx = value;
            }
        }

        public UInt16 ImageDepthPx
        {
            get
            {
                return this.m_iImageDepthPx;
            }
            set
            {
                this.m_iImageDepthPx = value;
            }
        }

        public UInt16 XOverScanPx
        {
            get
            {
                return this.m_iXOverScanPx;
            }
            set
            {
                this.m_iXOverScanPx = value;
            }
        }

        public UInt16 YOverScanPx
        {
            get
            {
                return this.m_iYOverScanPx;
            }
            set
            {
                this.m_iYOverScanPx = value;
            }
        }

        public UInt16 ZOverScanPx
        {
            get
            {
                return this.m_iZOverScanPx;
            }
            set
            {
                this.m_iZOverScanPx = value;
            }
        }

        public double TimePPixel
        {
            get
            {
                return this.m_dTimePPixel;
            }
            set
            {
                this.m_dTimePPixel = value;
            }
        }

        public double XScanSizeNm
        {
            get
            {
                return this.m_dXScanSizeNm;
            }
            set
            {
                this.m_dXScanSizeNm = value;
            }
        }

        public double YScanSizeNm
        {
            get
            {
                return this.m_dYScanSizeNm;
            }
            set
            {
                this.m_dYScanSizeNm = value;
            }
        }

        public double ZScanSizeNm
        {
            get
            {
                return this.m_dZScanSizeNm;
            }
            set
            {
                this.m_dZScanSizeNm = value;
            }
        }

        public double InitXNm
        {
            get
            {
                return this.m_dInitXNm;
            }
            set
            {
                this.m_dInitXNm = value;
            }
        }

        public double InitYNm
        {
            get
            {
                return this.m_dInitYNm;
            }
            set
            {
                this.m_dInitYNm = value;
            }
        }

        public double InitZNm
        {
            get
            {
                return this.m_dInitZNm;
            }
            set
            {
                this.m_dInitZNm = value;
            }
        }

        public int Channels
        {
            get
            {
                return this.m_iChannels;
            }
            set
            {
                this.m_iChannels = value;
            }
        }

        public string Annotation
        {
            get
            {
                return this.m_sAnnotation;
            }
            set
            {
                this.m_sAnnotation = value;
            }
        }
    }
}
