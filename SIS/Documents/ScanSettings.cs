// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanSettings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Documents
{
    /// <summary>
    /// The scan settings.
    /// </summary>
    public struct ScanSettings
    {
        // Members of Scan Settings Section:
        #region Fields

        /// <summary>
        /// The m_d galvo magnification objective.
        /// </summary>
        private double m_dGalvoMagnificationObjective; // the magnification of the objective

        /// <summary>
        /// The m_d galvo range angle degrees.
        /// </summary>
        private double m_dGalvoRangeAngleDegrees;

        // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)

        /// <summary>
        /// The m_d galvo range angle int.
        /// </summary>
        private double m_dGalvoRangeAngleInt;

        // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           

        /// <summary>
        /// The m_d galvo scan lens focal length.
        /// </summary>
        private double m_dGalvoScanLensFocalLength; // the focal length of the scan lens in [mm]

        /// <summary>
        /// The m_d init x nm.
        /// </summary>
        private double m_dInitXNm;

        /// <summary>
        /// The m_d init y nm.
        /// </summary>
        private double m_dInitYNm;

        /// <summary>
        /// The m_d init z nm.
        /// </summary>
        private double m_dInitZNm;

        /// <summary>
        /// The m_d time p pixel.
        /// </summary>
        private double m_dTimePPixel;

        /// <summary>
        /// The m_d x scan size nm.
        /// </summary>
        private double m_dXScanSizeNm;

        /// <summary>
        /// The m_d y scan size nm.
        /// </summary>
        private double m_dYScanSizeNm;

        /// <summary>
        /// The m_d z scan size nm.
        /// </summary>
        private double m_dZScanSizeNm;

        /// <summary>
        /// The m_i channels.
        /// </summary>
        private int m_iChannels;

        /// <summary>
        /// The m_i galvo frame marker.
        /// </summary>
        private int m_iGalvoFrameMarker;

        // the frame synchronization marker that the galvo rises upon a beginning of a frame

        /// <summary>
        /// The m_i galvo line marker.
        /// </summary>
        private int m_iGalvoLineMarker;

        // the line synchronization marker that the galvo rises upon a beginning of a line

        /// <summary>
        /// The m_i image depth px.
        /// </summary>
        private ushort m_iImageDepthPx;

        /// <summary>
        /// The m_i image height px.
        /// </summary>
        private ushort m_iImageHeightPx;

        /// <summary>
        /// The m_i image width px.
        /// </summary>
        private ushort m_iImageWidthPx;

        /// <summary>
        /// The m_i time harp cfd min.
        /// </summary>
        private int m_iTimeHarpCFDMin; // CFD discrimination voltage level in [mV]

        /// <summary>
        /// The m_i time harp cfd zero cross.
        /// </summary>
        private int m_iTimeHarpCFDZeroCross; // CFD zero cross voltage level in [mV]

        /// <summary>
        /// The m_i time harp fi fo time out.
        /// </summary>
        private int m_iTimeHarpFiFoTimeOut;

        // the max time period after which the recorded raw Time Harp events will be read from the Time Harp FiFo buffer

        // Members of Time Harp Settings Section:
        /// <summary>
        /// The m_i time harp frame marker.
        /// </summary>
        private int m_iTimeHarpFrameMarker;

        // tells Time Harp the value of the frame synchronization marker that the galvo rises upon a beginning of a frame

        /// <summary>
        /// The m_i time harp frame time out.
        /// </summary>
        private int m_iTimeHarpFrameTimeOut;

        // the max time period after which the processed pixels so far will be returned as a frame

        /// <summary>
        /// The m_i time harp global tttr buffer size.
        /// </summary>
        private int m_iTimeHarpGlobalTTTRBufferSize;

        // global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)

        /// <summary>
        /// The m_i time harp line marker.
        /// </summary>
        private int m_iTimeHarpLineMarker;

        // tells Time Harp the value of the line synchronization marker that the galvo rises upon a beginning of a line

        /// <summary>
        /// The m_i time harp line ptttr buffer size.
        /// </summary>
        private int m_iTimeHarpLinePTTTRBufferSize;

        // line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)

        /// <summary>
        /// The m_i time harp marker edge.
        /// </summary>
        private int m_iTimeHarpMarkerEdge;

        // set the active TTL edge (0 - falling edge, 1 - rising edge). Note that this defines the type of edge used from the frame/line marker TTL pulse (so it must be the same as the outputed TTL from YanusIV).

        /// <summary>
        /// The m_i time harp measurement mode.
        /// </summary>
        private int m_iTimeHarpMeasurementMode;

        // there are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode. Note that we need mode 0 in order to get raw photon data (arrival time) and build an image.

        /// <summary>
        /// The m_i time harp offset.
        /// </summary>
        private int m_iTimeHarpOffset; // set offset

        /// <summary>
        /// The m_i time harp range code.
        /// </summary>
        private int m_iTimeHarpRangeCode;

        // set the timing resolution of Time Harp, range (0..5). Note that then the timing resolution is base_timing_resolution*2^(_iRangeCode); base_timing_resolution is the time resolution of Time Harp (~30ps for Time Harp 200)

        /// <summary>
        /// The m_i time harp sync level.
        /// </summary>
        private int m_iTimeHarpSyncLevel; // Sync voltage level [mV]

        /// <summary>
        /// The m_i x over scan px.
        /// </summary>
        private ushort m_iXOverScanPx;

        /// <summary>
        /// The m_i y over scan px.
        /// </summary>
        private ushort m_iYOverScanPx;

        /// <summary>
        /// The m_i z over scan px.
        /// </summary>
        private ushort m_iZOverScanPx;

        /// <summary>
        /// The m_s annotation.
        /// </summary>
        private string m_sAnnotation;

        // Members of Galvo Settings Section:
        /// <summary>
        /// The m_s galvo serial port name.
        /// </summary>
        private string m_sGalvoSerialPortName; // the name of the serial port where the galvo is connected to

        /// <summary>
        /// The m_s time harp name tttr file.
        /// </summary>
        private string m_sTimeHarpNameTTTRFile;

        #endregion

        // the name (without the path and extension) of the data file with raw photon data (the TTTR binary file)

        // Properties (get/set) of Scan Settings Section
        #region Public Properties

        /// <summary>
        /// Gets or sets the annotation.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
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

        // Properties (get/set) of Galvo Settings Section:

        /// <summary>
        /// Gets or sets the galvo frame marker.
        /// </summary>
        public int GalvoFrameMarker
        {
            // the frame synchronization marker that the galvo rises upon a beginning of a frame
            get
            {
                return this.m_iGalvoFrameMarker;
            }

            set
            {
                this.m_iGalvoFrameMarker = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo line marker.
        /// </summary>
        public int GalvoLineMarker
        {
            // the line synchronization marker that the galvo rises upon a beginning of a line
            get
            {
                return this.m_iGalvoLineMarker;
            }

            set
            {
                this.m_iGalvoLineMarker = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo magnification objective.
        /// </summary>
        public double GalvoMagnificationObjective
        {
            // the magnification of the objective
            get
            {
                return this.m_dGalvoMagnificationObjective;
            }

            set
            {
                this.m_dGalvoMagnificationObjective = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo range angle degrees.
        /// </summary>
        public double GalvoRangeAngleDegrees
        {
            // +/- of the max range a galvo axis can reach in degrees (this is the angle after the scan lens, which is useful in the current microscopy setup)
            get
            {
                return this.m_dGalvoRangeAngleDegrees;
            }

            set
            {
                this.m_dGalvoRangeAngleDegrees = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo range angle int.
        /// </summary>
        public double GalvoRangeAngleInt
        {
            // +/- of the max range a galvo axis can reach in integers (this is the angle after the scan lens, which is useful in the current microscopy setup)           
            get
            {
                return this.m_dGalvoRangeAngleInt;
            }

            set
            {
                this.m_dGalvoRangeAngleInt = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo scan lens focal length.
        /// </summary>
        public double GalvoScanLensFocalLength
        {
            // the focal length of the scan lens in [mm]
            get
            {
                return this.m_dGalvoScanLensFocalLength;
            }

            set
            {
                this.m_dGalvoScanLensFocalLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the galvo serial port name.
        /// </summary>
        public string GalvoSerialPortName
        {
            // the name of the serial port where the galvo is connected to
            get
            {
                return this.m_sGalvoSerialPortName;
            }

            set
            {
                this.m_sGalvoSerialPortName = value;
            }
        }

        /// <summary>
        /// Gets or sets the image depth px.
        /// </summary>
        public ushort ImageDepthPx
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

        /// <summary>
        /// Gets or sets the image height px.
        /// </summary>
        public ushort ImageHeightPx
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

        /// <summary>
        /// Gets or sets the image width px.
        /// </summary>
        public ushort ImageWidthPx
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

        /// <summary>
        /// Gets or sets the init x nm.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the init y nm.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the init z nm.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the time harp cfd min.
        /// </summary>
        public int TimeHarpCFDMin
        {
            // CFD discrimination voltage level in [mV]
            get
            {
                return this.m_iTimeHarpCFDMin;
            }

            set
            {
                this.m_iTimeHarpCFDMin = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp cfd zero cross.
        /// </summary>
        public int TimeHarpCFDZeroCross
        {
            // CFD zero cross voltage level in [mV]
            get
            {
                return this.m_iTimeHarpCFDZeroCross;
            }

            set
            {
                this.m_iTimeHarpCFDZeroCross = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp fi fo time out.
        /// </summary>
        public int TimeHarpFiFoTimeOut
        {
            // the max time period after which the recorded raw Time Harp events will be read from the Time Harp FiFo buffer
            get
            {
                return this.m_iTimeHarpFiFoTimeOut;
            }

            set
            {
                this.m_iTimeHarpFiFoTimeOut = value;
            }
        }

        // Properties (get/set) of Time Harp Settings Section:
        /// <summary>
        /// Gets or sets the time harp frame marker.
        /// </summary>
        public int TimeHarpFrameMarker
        {
            // tells Time Harp the value of the frame synchronization marker that the galvo rises upon a beginning of a frame
            get
            {
                return this.m_iTimeHarpFrameMarker;
            }

            set
            {
                this.m_iTimeHarpFrameMarker = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp frame time out.
        /// </summary>
        public int TimeHarpFrameTimeOut
        {
            // the max time period after which the processed pixels so far will be returned as a frame
            get
            {
                return this.m_iTimeHarpFrameTimeOut;
            }

            set
            {
                this.m_iTimeHarpFrameTimeOut = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp global tttr buffer size.
        /// </summary>
        public int TimeHarpGlobalTTTRBufferSize
        {
            // global TTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
            get
            {
                return this.m_iTimeHarpGlobalTTTRBufferSize;
            }

            set
            {
                this.m_iTimeHarpGlobalTTTRBufferSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp line marker.
        /// </summary>
        public int TimeHarpLineMarker
        {
            // tells Time Harp the value of the line synchronization marker that the galvo rises upon a beginning of a line
            get
            {
                return this.m_iTimeHarpLineMarker;
            }

            set
            {
                this.m_iTimeHarpLineMarker = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp line ptttr buffer size.
        /// </summary>
        public int TimeHarpLinePTTTRBufferSize
        {
            // line PTTTR buffer size in multiples of Time Harp's Half FiFo Size (see TimeHarpDefinitions.DMABLOCKSZ)
            get
            {
                return this.m_iTimeHarpLinePTTTRBufferSize;
            }

            set
            {
                this.m_iTimeHarpLinePTTTRBufferSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp marker edge.
        /// </summary>
        public int TimeHarpMarkerEdge
        {
            // set the active TTL edge (0 - falling edge, 1 - rising edge). Note that this defines the type of edge used from the frame/line marker TTL pulse (so it must be the same as the outputed TTL from YanusIV).
            get
            {
                return this.m_iTimeHarpMarkerEdge;
            }

            set
            {
                this.m_iTimeHarpMarkerEdge = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp measurement mode.
        /// </summary>
        public int TimeHarpMeasurementMode
        {
            // there are two possible modes: 0 - one-time histogramming and TTTR modes; 1 - continuous mode. Note that we need mode 0 in order to get raw photon data (arrival time) and build an image.
            get
            {
                return this.m_iTimeHarpMeasurementMode;
            }

            set
            {
                this.m_iTimeHarpMeasurementMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp name tttr file.
        /// </summary>
        public string TimeHarpNameTTTRFile
        {
            // the name (without the path and extension) of the data file with raw photon data (the TTTR binary file)
            get
            {
                return this.m_sTimeHarpNameTTTRFile;
            }

            set
            {
                this.m_sTimeHarpNameTTTRFile = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp offset.
        /// </summary>
        public int TimeHarpOffset
        {
            // set offset
            get
            {
                return this.m_iTimeHarpOffset;
            }

            set
            {
                this.m_iTimeHarpOffset = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp range code.
        /// </summary>
        public int TimeHarpRangeCode
        {
            // set the timing resolution of Time Harp, range (0..5). Note that then the timing resolution is base_timing_resolution*2^(_iRangeCode); base_timing_resolution is the time resolution of Time Harp (~30ps for Time Harp 200)
            get
            {
                return this.m_iTimeHarpRangeCode;
            }

            set
            {
                this.m_iTimeHarpRangeCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the time harp sync level.
        /// </summary>
        public int TimeHarpSyncLevel
        {
            // Sync voltage level [mV]
            get
            {
                return this.m_iTimeHarpSyncLevel;
            }

            set
            {
                this.m_iTimeHarpSyncLevel = value;
            }
        }

        /// <summary>
        /// Gets or sets the time p pixel.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the x over scan px.
        /// </summary>
        public ushort XOverScanPx
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

        /// <summary>
        /// Gets or sets the x scan size nm.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the y over scan px.
        /// </summary>
        public ushort YOverScanPx
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

        /// <summary>
        /// Gets or sets the y scan size nm.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the z over scan px.
        /// </summary>
        public ushort ZOverScanPx
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

        /// <summary>
        /// Gets or sets the z scan size nm.
        /// </summary>
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

        #endregion
    }
}