using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUL.MDS.SIS.Documents
{
    public struct ScanSettings
    {
        // Members of Scan Settings Section:
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

        // Members of Galvo Settings Section:
        private string m_sGalvoSerialPortName;  // the name of the serial port where the galvo is connected to
        private int m_iGalvoFrameMarker;  // the frame synchronization marker that the galvo rises upon a beginning of a frame
        private int m_iGalvoLineMarker;  // the line synchronization marker that the galvo rises upon a beginning of a line
        private double m_dGalvoMagnificationObjective;  //the magnification of the objective
        private double m_dGalvoScanLensFocalLength;  //the focal length of the scan lens in [mm]
        private double m_dGalvoRangeAngleDegrees;  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
        private double m_dGalvoRangeAngleInt;  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
        
        // Members of Time Harp Settings Section:
        private int m_iTimeHarpFrameMarker;  // tells Time Harp the value of the frame synchronization marker that the galvo rises upon a beginning of a frame
        private int m_iTimeHarpLineMarker;  // tells Time Harp the value of the line synchronization marker that the galvo rises upon a beginning of a line
        private int m_iTimeHarpMarkerEdge;  // set the active TTL edge (0 - falling edge, 1 - rising edge). Note that this defines the type of edge used from the frame/line marker TTL pulse (so it must be the same as the outputed TTL from YanusIV).
        private int m_iTimeHarpMeasurementMode;  // there are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode. Note that we need mode 0 in order to get raw photon data (arrival time) and build an image.
        private int m_iTimeHarpRangeCode;  // set the timing resolution of Time Harp, range (0..5). Note that then the timing resolution is base_timing_resolution*2^(_iRangeCode); base_timing_resolution is the time resolution of Time Harp (~30ps for Time Harp 200)
        private int m_iTimeHarpOffset;  //set offset
        private int m_iTimeHarpCFDZeroCross;  //CFD zero cross voltage level in [mV]
        private int m_iTimeHarpCFDMin;  //CFD discrimination voltage level in [mV]
        private int m_iTimeHarpSyncLevel;  //Sync voltage level [mV]
        
        private int m_iTimeHarpGlobalTTTRBufferSize;  // global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
        private int m_iTimeHarpLinePTTTRBufferSize;  // line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
        private int m_iTimeHarpFrameTimeOut;  // the max time period after which the processed pixels so far will be returned as a frame
        private int m_iTimeHarpFiFoTimeOut;  // the max time period after which the recorded raw Time Harp events will be read from the Time Harp FiFo buffer
        private string m_sTimeHarpNameTTTRFile;  // the name (without the path and extension) of the data file with raw photon data (the TTTR binary file)



        // Properties (get/set) of Scan Settings Section
        public UInt16 ImageWidthPx
        {
            get { return this.m_iImageWidthPx; }
            set { this.m_iImageWidthPx = value; }
        }

        public UInt16 ImageHeightPx
        {
            get { return this.m_iImageHeightPx; }
            set { this.m_iImageHeightPx = value; }
        }

        public UInt16 ImageDepthPx
        {
            get { return this.m_iImageDepthPx; }
            set { this.m_iImageDepthPx = value; }
        }

        public UInt16 XOverScanPx
        {
            get { return this.m_iXOverScanPx; }
            set { this.m_iXOverScanPx = value; }
        }

        public UInt16 YOverScanPx
        {
            get { return this.m_iYOverScanPx; }
            set { this.m_iYOverScanPx = value; }
        }

        public UInt16 ZOverScanPx
        {
            get { return this.m_iZOverScanPx; }
            set { this.m_iZOverScanPx = value; }
        }

        public double TimePPixel
        {
            get { return this.m_dTimePPixel; }
            set { this.m_dTimePPixel = value; }
        }

        public double XScanSizeNm
        {
            get { return this.m_dXScanSizeNm; }
            set { this.m_dXScanSizeNm = value; }
        }

        public double YScanSizeNm
        {
            get { return this.m_dYScanSizeNm; }
            set { this.m_dYScanSizeNm = value; }
        }

        public double ZScanSizeNm
        {
            get { return this.m_dZScanSizeNm; }
            set { this.m_dZScanSizeNm = value; }
        }

        public double InitXNm
        {
            get { return this.m_dInitXNm; }
            set { this.m_dInitXNm = value; }
        }

        public double InitYNm
        {
            get { return this.m_dInitYNm; }
            set { this.m_dInitYNm = value; }
        }

        public double InitZNm
        {
            get { return this.m_dInitZNm; }
            set { this.m_dInitZNm = value; }
        }

        public int Channels
        {
            get { return this.m_iChannels; }
            set { this.m_iChannels = value; }
        }

        public string Annotation
        {
            get { return this.m_sAnnotation; }
            set { this.m_sAnnotation = value; }
        }


        // Properties (get/set) of Galvo Settings Section:
        public string GalvoSerialPortName  // the name of the serial port where the galvo is connected to
        {
            get { return this.m_sGalvoSerialPortName; }
            set { this.m_sGalvoSerialPortName = value; }
        }

        public int GalvoFrameMarker  // the frame synchronization marker that the galvo rises upon a beginning of a frame
        {
            get { return this.m_iGalvoFrameMarker; }
            set { this.m_iGalvoFrameMarker = value; }
        }

        public int GalvoLineMarker  // the line synchronization marker that the galvo rises upon a beginning of a line
        {
            get { return this.m_iGalvoLineMarker; }
            set { this.m_iGalvoLineMarker = value; }
        }

        public double GalvoMagnificationObjective  //the magnification of the objective
        {
            get { return this.m_dGalvoMagnificationObjective; }
            set { this.m_dGalvoMagnificationObjective = value; }
        }

        public double GalvoScanLensFocalLength  //the focal length of the scan lens in [mm]
        {
            get { return this.m_dGalvoScanLensFocalLength; }
            set { this.m_dGalvoScanLensFocalLength = value; }
        }

        public double GalvoRangeAngleDegrees  // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
        {
            get { return this.m_dGalvoRangeAngleDegrees; }
            set { this.m_dGalvoRangeAngleDegrees = value; }
        }

        public double GalvoRangeAngleInt  // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
        {
            get { return this.m_dGalvoRangeAngleInt; }
            set { this.m_dGalvoRangeAngleInt = value; }
        }


        // Properties (get/set) of Time Harp Settings Section:
        public int TimeHarpFrameMarker  // tells Time Harp the value of the frame synchronization marker that the galvo rises upon a beginning of a frame
        {
            get { return this.m_iTimeHarpFrameMarker; }
            set { this.m_iTimeHarpFrameMarker = value; }
        }

        public int TimeHarpLineMarker  // tells Time Harp the value of the line synchronization marker that the galvo rises upon a beginning of a line
        {
            get { return this.m_iTimeHarpLineMarker; }
            set { this.m_iTimeHarpLineMarker = value; }
        }

        public int TimeHarpMarkerEdge  // set the active TTL edge (0 - falling edge, 1 - rising edge). Note that this defines the type of edge used from the frame/line marker TTL pulse (so it must be the same as the outputed TTL from YanusIV).
        {
            get { return this.m_iTimeHarpMarkerEdge; }
            set { this.m_iTimeHarpMarkerEdge = value; }
        }

        public int TimeHarpMeasurementMode  // there are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode. Note that we need mode 0 in order to get raw photon data (arrival time) and build an image.
        {
            get { return this.m_iTimeHarpMeasurementMode; }
            set { this.m_iTimeHarpMeasurementMode = value; }
        }

        public int TimeHarpRangeCode  // set the timing resolution of Time Harp, range (0..5). Note that then the timing resolution is base_timing_resolution*2^(_iRangeCode); base_timing_resolution is the time resolution of Time Harp (~30ps for Time Harp 200)
        {
            get { return this.m_iTimeHarpRangeCode; }
            set { this.m_iTimeHarpRangeCode = value; }
        }

        public int TimeHarpOffset  //set offset
        {
            get { return this.m_iTimeHarpOffset; }
            set { this.m_iTimeHarpOffset = value; }
        }

        public int TimeHarpCFDZeroCross  //CFD zero cross voltage level in [mV]
        {
            get { return this.m_iTimeHarpCFDZeroCross; }
            set { this.m_iTimeHarpCFDZeroCross = value; }
        }

        public int TimeHarpCFDMin  //CFD discrimination voltage level in [mV]
        {
            get { return this.m_iTimeHarpCFDMin; }
            set { this.m_iTimeHarpCFDMin = value; }
        }

        public int TimeHarpSyncLevel  //Sync voltage level [mV]
        {
            get { return this.m_iTimeHarpSyncLevel; }
            set { this.m_iTimeHarpSyncLevel = value; }
        }

        public int TimeHarpGlobalTTTRBufferSize  // global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
        {
            get { return this.m_iTimeHarpGlobalTTTRBufferSize; }
            set { this.m_iTimeHarpGlobalTTTRBufferSize = value; }
        }

        public int TimeHarpLinePTTTRBufferSize  // line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
        {
            get { return this.m_iTimeHarpLinePTTTRBufferSize; }
            set { this.m_iTimeHarpLinePTTTRBufferSize = value; }
        }

        public int TimeHarpFrameTimeOut  // the max time period after which the processed pixels so far will be returned as a frame
        {
            get { return this.m_iTimeHarpFrameTimeOut; }
            set { this.m_iTimeHarpFrameTimeOut = value; }
        }

        public int TimeHarpFiFoTimeOut  // the max time period after which the recorded raw Time Harp events will be read from the Time Harp FiFo buffer
        {
            get { return this.m_iTimeHarpFiFoTimeOut; }
            set { this.m_iTimeHarpFiFoTimeOut = value; }
        }

        public string TimeHarpNameTTTRFile  // the name (without the path and extension) of the data file with raw photon data (the TTTR binary file)
        {
            get { return this.m_sTimeHarpNameTTTRFile; }
            set { this.m_sTimeHarpNameTTTRFile = value; }
        }

    }
}
