// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanDocument.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This class provides an object for loading scan files. The object provides scan data as well as scan settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Documents
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using SIS.Forms;
    using SIS.MDITemplate;

    /// <summary>
    ///     This class provides an object for loading scan files. The object provides scan data as well as scan settings.
    /// </summary>
    [Document("Scan document files", ".dat")]
    public class ScanDocument : MdiDocument
    {
        // Members.
        #region Fields

        /// <summary>
        /// The m_s application name.
        /// </summary>
        private readonly string m_sApplicationName;

        /// <summary>
        /// The m_d border width x.
        /// </summary>
        private double m_dBorderWidthX;

        /// <summary>
        /// The m_i data type.
        /// </summary>
        private ushort m_iDataType;

        /// <summary>
        /// The m_i scan axes.
        /// </summary>
        private ushort m_iScanAxes;

        /// <summary>
        /// The m_s application version.
        /// </summary>
        private string m_sApplicationVersion;

        /// <summary>
        /// The m_scnst settings.
        /// </summary>
        private ScanSettings m_scnstSettings;

        /// <summary>
        /// The m_ui 32 pixels.
        /// </summary>
        private uint[] m_ui32Pixels;

        #endregion

        // NEW STUFF

        // Constructor.
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanDocument"/> class.
        /// </summary>
        public ScanDocument()
        {
            this.m_sApplicationName = null;
            this.m_sApplicationVersion = null;

            this.m_iScanAxes = 0;

            this.m_scnstSettings = new ScanSettings();

            this.m_scnstSettings.ImageWidthPx = 256;
            this.m_scnstSettings.ImageHeightPx = 256;
            this.m_scnstSettings.ImageDepthPx = 0;
            this.m_scnstSettings.XOverScanPx = 0;
            this.m_scnstSettings.YOverScanPx = 0;
            this.m_scnstSettings.ZOverScanPx = 0;
            this.m_scnstSettings.TimePPixel = 2;
            this.m_scnstSettings.XScanSizeNm = 1000;
            this.m_scnstSettings.YScanSizeNm = 1000;
            this.m_scnstSettings.ZScanSizeNm = 0;
            this.m_scnstSettings.InitXNm = 0;
            this.m_scnstSettings.InitYNm = 0;
            this.m_scnstSettings.InitZNm = 0;
            this.m_scnstSettings.Channels = 2;
            this.m_scnstSettings.Annotation = string.Empty;
            this.m_iDataType = 0;

            this.ScanDuration = 0;

            this.AllocatePixels();
        }

        #endregion

        // Property fields.
        #region Public Properties

        /// <summary>
        /// Gets the application name.
        /// </summary>
        public string ApplicationName
        {
            get
            {
                return this.m_sApplicationName;
            }
        }

        /// <summary>
        /// Gets the data type.
        /// </summary>
        public ushort DataType
        {
            get
            {
                return this.m_iDataType;
            }
        }

        /// <summary>
        /// Gets the image depth px.
        /// </summary>
        public ushort ImageDepthPx
        {
            get
            {
                return this.m_scnstSettings.ImageDepthPx;
            }
        }

        /// <summary>
        /// Gets the image height px.
        /// </summary>
        public ushort ImageHeightPx
        {
            get
            {
                return this.m_scnstSettings.ImageHeightPx;
            }
        }

        /// <summary>
        /// Gets the image width px.
        /// </summary>
        public ushort ImageWidthPx
        {
            get
            {
                return this.m_scnstSettings.ImageWidthPx;
            }
        }

        /// <summary>
        /// Gets the initial x.
        /// </summary>
        public double InitialX
        {
            get
            {
                return this.m_scnstSettings.InitXNm;
            }
        }

        /// <summary>
        /// Gets the initial y.
        /// </summary>
        public double InitialY
        {
            get
            {
                return this.m_scnstSettings.InitYNm;
            }
        }

        /// <summary>
        /// Gets the initial z.
        /// </summary>
        public double InitialZ
        {
            get
            {
                return this.m_scnstSettings.InitZNm;
            }
        }

        /// <summary>
        /// Gets the max intensity.
        /// </summary>
        public uint[] MaxIntensity
        {
            get
            {
                var _Max = new uint[this.m_scnstSettings.Channels];
                for (int _iI = 0; _iI < this.m_scnstSettings.Channels; _iI++)
                {
                    _Max[_iI] = this.FindMax(
                        this.m_ui32Pixels, 
                        _iI, 
                        this.m_ui32Pixels.Length / this.m_scnstSettings.Channels);
                }

                return _Max;
            }
        }

        /// <summary>
        /// Gets the min intensity.
        /// </summary>
        public uint[] MinIntensity
        {
            get
            {
                var _Min = new uint[this.m_scnstSettings.Channels];
                for (int _iI = 0; _iI < this.m_scnstSettings.Channels; _iI++)
                {
                    _Min[_iI] = this.FindMin(
                        this.m_ui32Pixels, 
                        _iI, 
                        this.m_ui32Pixels.Length / this.m_scnstSettings.Channels);
                }

                return _Min;
            }
        }

        /// <summary>
        /// Gets the pixel count.
        /// </summary>
        public int PixelCount
        {
            get
            {
                return this.m_ui32Pixels.Length / this.m_scnstSettings.Channels;
            }
        }

        /// <summary>
        /// Gets or sets the scan axes.
        /// </summary>
        public ushort ScanAxes
        {
            get
            {
                return this.m_iScanAxes;
            }

            set
            {
                this.m_iScanAxes = value;
            }
        }

        /// <summary>
        /// Gets or sets the scan duration.
        /// </summary>
        public double ScanDuration { get; set; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public ScanSettings Settings
        {
            get
            {
                return this.m_scnstSettings;
            }
        }

        /// <summary>
        /// Gets the time p pixel.
        /// </summary>
        public double TimePPixel
        {
            get
            {
                return this.m_scnstSettings.TimePPixel;
            }
        }

        /// <summary>
        /// Gets or sets the x border width.
        /// </summary>
        public double XBorderWidth
        {
            get
            {
                return this.m_dBorderWidthX;
            }

            set
            {
                this.m_dBorderWidthX = value;
            }
        }

        /// <summary>
        /// Gets the x over scan px.
        /// </summary>
        public ushort XOverScanPx
        {
            get
            {
                return this.m_scnstSettings.XOverScanPx;
            }
        }

        /// <summary>
        /// Gets the x scan size nm.
        /// </summary>
        public double XScanSizeNm
        {
            get
            {
                return this.m_scnstSettings.XScanSizeNm;
            }
        }

        /// <summary>
        /// Gets the y over scan px.
        /// </summary>
        public ushort YOverScanPx
        {
            get
            {
                return this.m_scnstSettings.YOverScanPx;
            }
        }

        /// <summary>
        /// Gets the y scan size nm.
        /// </summary>
        public double YScanSizeNm
        {
            get
            {
                return this.m_scnstSettings.YScanSizeNm;
            }
        }

        /// <summary>
        /// Gets the z over scan px.
        /// </summary>
        public ushort ZOverScanPx
        {
            get
            {
                return this.m_scnstSettings.ZOverScanPx;
            }
        }

        /// <summary>
        /// Gets the z scan size nm.
        /// </summary>
        public double ZScanSizeNm
        {
            get
            {
                return this.m_scnstSettings.ZScanSizeNm;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The allocate data.
        /// </summary>
        /// <param name="__scnstSettings">
        /// The __scnst settings.
        /// </param>
        public void AllocateData(ScanSettings __scnstSettings)
        {
            this.m_scnstSettings = __scnstSettings;

            this.AllocatePixels();
        }

        /// <summary>
        /// The get channel data.
        /// </summary>
        /// <param name="__iChannel">
        /// The __i channel.
        /// </param>
        /// <returns>
        /// The <see cref="uint[]"/>.
        /// </returns>
        public uint[] GetChannelData(int __iChannel)
        {
            int _iPixelCount = this.m_ui32Pixels.Length / this.m_scnstSettings.Channels;
            var _ui32ChannelData = new uint[_iPixelCount];

            int _iStart = __iChannel * _iPixelCount;

            for (int _iI = 0; _iI < _iPixelCount; _iI++)
            {
                _ui32ChannelData[_iI] = this.m_ui32Pixels[_iStart + _iI];
            }

            return _ui32ChannelData;
        }

        /// <summary>
        /// The store channel data.
        /// </summary>
        /// <param name="__iChannel">
        /// The __i channel.
        /// </param>
        /// <param name="__ui32Values">
        /// The __ui 32 values.
        /// </param>
        public void StoreChannelData(int __iChannel, uint[] __ui32Values)
        {
            int _iPixelCount = this.m_ui32Pixels.Length / this.m_scnstSettings.Channels;

            int _iStart = __iChannel * _iPixelCount;

            for (int _iI = 0; _iI < _iPixelCount; _iI++)
            {
                this.m_ui32Pixels[_iStart + _iI] = __ui32Values[_iI];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on create view.
        /// </summary>
        /// <returns>
        /// The <see cref="MdiViewForm"/>.
        /// </returns>
        protected override MdiViewForm OnCreateView()
        {
            return new ScanViewForm();
        }

        // See Documentation/SISHeader.txt for more info on the file format!
        /// <summary>
        /// The on load document.
        /// </summary>
        /// <param name="_sFilePath">
        /// The _s file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool OnLoadDocument(string _sFilePath)
        {
            bool _bResult = true;
            int _iI = 0;

            // int _iXCord = 0;
            // int _iYCord = 0;
            byte _bByte = 0;
            uint _ui32Data = 0;
            int _intLength = 0;

            // string _sSettingsPath = null;
            try
            {
                // Get the data from disk into memory using a filestream object.
                var _fsStream = new FileStream(_sFilePath, FileMode.Open);
                var _brdrReader = new BinaryReader(_fsStream);

                _fsStream.Seek(98, SeekOrigin.Begin);
                this.m_iScanAxes = _brdrReader.ReadUInt16();

                // _fsStream.Seek(100, SeekOrigin.Begin);
                this.m_scnstSettings.ImageWidthPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(102, SeekOrigin.Begin);
                this.m_scnstSettings.ImageHeightPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(104, SeekOrigin.Begin);
                this.m_scnstSettings.ImageDepthPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(106, SeekOrigin.Begin);
                this.m_scnstSettings.XOverScanPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(108, SeekOrigin.Begin);
                this.m_scnstSettings.YOverScanPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(110, SeekOrigin.Begin);
                this.m_scnstSettings.ZOverScanPx = _brdrReader.ReadUInt16();

                // _fsStream.Seek(112, SeekOrigin.Begin);
                this.m_scnstSettings.TimePPixel = _brdrReader.ReadDouble();

                // _fsStream.Seek(120, SeekOrigin.Begin);
                this.m_scnstSettings.XScanSizeNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(128, SeekOrigin.Begin);
                this.m_scnstSettings.YScanSizeNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(136, SeekOrigin.Begin);
                this.m_scnstSettings.ZScanSizeNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(144, SeekOrigin.Begin);
                this.m_scnstSettings.InitXNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(152, SeekOrigin.Begin);
                this.m_scnstSettings.InitYNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(160, SeekOrigin.Begin);
                this.m_scnstSettings.InitZNm = _brdrReader.ReadDouble();

                // _fsStream.Seek(168, SeekOrigin.Begin);
                this.m_iDataType = _brdrReader.ReadUInt16();

                // _fsStream.Seek(170, SeekOrigin.Begin);
                this.m_scnstSettings.Channels = _brdrReader.ReadUInt16();

                // _fsStream.Seek(172, SeekOrigin.Begin);
                this.m_dBorderWidthX = _brdrReader.ReadDouble();

                // long test = _brdrReader.BaseStream.Position;
                _fsStream.Seek(3099, SeekOrigin.Begin);

                // test = _brdrReader.BaseStream.Position;
                byte[] b1 = _brdrReader.ReadBytes(1000);

                this.m_scnstSettings.Annotation = Encoding.ASCII.GetString(b1).Trim();

                this.AllocatePixels();

                // Make sure we start reading at the correct offset for the data.
                _fsStream.Seek(4100, SeekOrigin.Begin);

                #region Restore UInt32 from 4 bytes

                // Read all the bytes in groups of 4.
                for (_iI = 0; _iI < this.m_ui32Pixels.Length; _iI++)
                {
                    _bByte = _brdrReader.ReadByte();
                    _ui32Data = _bByte;
                    _bByte = _brdrReader.ReadByte();
                    _ui32Data += (UInt32)_bByte * 256;
                    _bByte = _brdrReader.ReadByte();
                    _ui32Data += (UInt32)_bByte * 65536;
                    _bByte = _brdrReader.ReadByte();
                    _ui32Data += (UInt32)_bByte * 16777216;

                    this.m_ui32Pixels[_iI] = _ui32Data;
                }

                #endregion

                // You don't need the reader anymore.
                _brdrReader.Close();
                _fsStream.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                _bResult = false;
            }

            return _bResult;
        }

        // See Documentation/SISHeader.txt for more info on the file format!
        /// <summary>
        /// The on save document.
        /// </summary>
        /// <param name="_sFilePath">
        /// The _s file path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool OnSaveDocument(string _sFilePath)
        {
            bool fResult = true;

            const char _chNull = (char)0;

            // Will be assigned the raw data from the scan for further processing.
            long _lData = 0;

            // Will be assigned the actual byte values for storage.
            byte _bByte = 0;

            try
            {
                var _fsStream = new FileStream(_sFilePath, FileMode.Create);
                var _bwriWriter = new BinaryWriter(_fsStream);

                // AssemblyName _AppName =System.Reflection.Assembly.GetEntryAssembly().GetName();
                // m_sApplicationName = _AppName.Name;
                // m_sApplicationVersion = _AppName.Version.ToString();

                // char[] _chName = m_sApplicationName.ToCharArray();
                for (int _intI = 0; _intI < 98; _intI++)
                {
                    _bwriWriter.Write(_chNull);
                }

                _bwriWriter.Write(this.m_iScanAxes);
                _bwriWriter.Write(this.m_scnstSettings.ImageWidthPx);
                _bwriWriter.Write(this.m_scnstSettings.ImageHeightPx);
                _bwriWriter.Write(this.m_scnstSettings.ImageDepthPx);
                _bwriWriter.Write(this.m_scnstSettings.XOverScanPx);
                _bwriWriter.Write(this.m_scnstSettings.YOverScanPx);
                _bwriWriter.Write(this.m_scnstSettings.ZOverScanPx);
                _bwriWriter.Write(this.m_scnstSettings.TimePPixel);
                _bwriWriter.Write(this.m_scnstSettings.XScanSizeNm);
                _bwriWriter.Write(this.m_scnstSettings.YScanSizeNm);
                _bwriWriter.Write(this.m_scnstSettings.ZScanSizeNm);
                _bwriWriter.Write(this.m_scnstSettings.InitXNm);
                _bwriWriter.Write(this.m_scnstSettings.InitYNm);
                _bwriWriter.Write(this.m_scnstSettings.InitZNm);
                _bwriWriter.Write(this.m_iDataType);
                _bwriWriter.Write((UInt16)this.m_scnstSettings.Channels);

                _bwriWriter.Write(this.m_dBorderWidthX);

                // long test = _bwriWriter.BaseStream.Position;
                for (int _intI = 0; _intI < 2919; _intI++)
                {
                    _bwriWriter.Write(_chNull);
                }

                // test = _bwriWriter.BaseStream.Position;
                byte[] b1 = Encoding.ASCII.GetBytes(this.m_scnstSettings.Annotation);

                for (int _intI = 0; _intI < b1.Length; _intI++)
                {
                    _bwriWriter.Write(b1[_intI]);
                }

                for (int _intI = 0; _intI < (1000 - b1.Length); _intI++)
                {
                    _bwriWriter.Write(_chNull);
                }

                // test = _bwriWriter.BaseStream.Position;
                // Make sure we start writing data at the correct offset, even if we made a mistake writing the header.
                _bwriWriter.Seek(4100, SeekOrigin.Begin);

                for (int _intI = 0;
                     _intI
                     < ((this.m_scnstSettings.ImageWidthPx + this.m_scnstSettings.XOverScanPx)
                        * (this.m_scnstSettings.ImageHeightPx + this.m_scnstSettings.YOverScanPx)
                        * this.m_scnstSettings.Channels);
                     _intI++)
                {
                    _lData = this.m_ui32Pixels[_intI];

                    for (int _intJ = 0; _intJ < 4; _intJ++)
                    {
                        _bByte = (byte)(_lData % 256);
                        _lData = _lData / 256;
                        _bwriWriter.Write(_bByte);
                    }
                }

                _bwriWriter.Close();
                _fsStream.Close();
            }
            catch (IOException)
            {
                fResult = false;
            }

            return fResult;
        }

        /// <summary>
        /// The allocate pixels.
        /// </summary>
        private void AllocatePixels()
        {
            // Make room for this.m_iChannels times the amount of pixels in the image.
            this.m_ui32Pixels =
                new uint[
                    (this.m_scnstSettings.ImageWidthPx + this.m_scnstSettings.XOverScanPx)
                    * (this.m_scnstSettings.ImageHeightPx + this.m_scnstSettings.YOverScanPx)
                    * this.m_scnstSettings.Channels];

            // Set all pixels to 0 initially.
            for (int _iI = 0; _iI < this.m_ui32Pixels.Length; _iI++)
            {
                this.m_ui32Pixels[_iI] = 0;
            }
        }

        /// <summary>
        /// The find max.
        /// </summary>
        /// <param name="__dArray">
        /// The __d array.
        /// </param>
        /// <param name="__iChannel">
        /// The __i channel.
        /// </param>
        /// <param name="__iPixelCount">
        /// The __i pixel count.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        private uint FindMax(uint[] __dArray, int __iChannel, int __iPixelCount)
        {
            int _iStart = __iChannel * __iPixelCount;
            int _iStop = (__iChannel + 1) * __iPixelCount;
            uint _ui32Max = uint.MinValue;

            for (int _iI = _iStart; _iI < _iStop; _iI++)
            {
                if (__dArray[_iI] > _ui32Max)
                {
                    _ui32Max = __dArray[_iI];
                }
            }

            return _ui32Max;
        }

        /// <summary>
        /// The find min.
        /// </summary>
        /// <param name="__dArray">
        /// The __d array.
        /// </param>
        /// <param name="__iChannel">
        /// The __i channel.
        /// </param>
        /// <param name="__iPixelCount">
        /// The __i pixel count.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        private uint FindMin(uint[] __dArray, int __iChannel, int __iPixelCount)
        {
            int _iStart = __iChannel * __iPixelCount;
            int _iStop = (__iChannel + 1) * __iPixelCount;
            uint _ui32Min = uint.MaxValue;

            for (int _iI = _iStart; _iI < _iStop; _iI++)
            {
                if (__dArray[_iI] < _ui32Min)
                {
                    _ui32Min = __dArray[_iI];
                }
            }

            return _ui32Min;
        }

        #endregion

        // private T FindMin<T>(List<T> __lList) where T : IComparable
        // {
        // int _iLength = __lList.Count;
        // T _TMin = __lList[0];

        // for (int _iI = 0; _iI < _iLength; _iI++)
        // {
        // if (__lList[_iI].CompareTo(_TMin) < 0)
        // {
        // _TMin = __lList[_iI];
        // }
        // }
        // return _TMin;
        // }

        // private T FindMax<T>(List<T> __lList) where T : IComparable
        // {
        // int _iLength = __lList.Count;
        // T _TMax = __lList[0];

        // for (int _iI = 0; _iI < _iLength; _iI++)
        // {
        // if (__lList[_iI].CompareTo(_TMax) > 0)
        // {
        // _TMax = __lList[_iI];
        // }
        // }
        // return _TMax;
        // }
    }
}