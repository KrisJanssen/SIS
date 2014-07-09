// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinspecSPEDataSet.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The winspec spe data set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The winspec spe data set.
    /// </summary>
    [Document("WinSpec Data", ".spe")]
    internal class WinspecSPEDataSet : DataSet
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WinspecSPEDataSet"/> class.
        /// </summary>
        public WinspecSPEDataSet()
            : base()
        {
            this.FormatInfo = new FormatInfo(
                "spe", 
                "Princeton Instruments WinSpec", 
                new List<string> { "spe" }, 
                true, 
                true);
        }

        #endregion

        #region Enums

        /// <summary>
        /// The data type.
        /// </summary>
        private enum DataType
        {
            /// <summary>
            /// The sp e_ dat a_ float.
            /// </summary>
            SPE_DATA_FLOAT = 0, // size: 4 bytes

            /// <summary>
            /// The sp e_ dat a_ long.
            /// </summary>
            SPE_DATA_LONG = 1, // size: 4 bytes

            /// <summary>
            /// The sp e_ dat a_ int.
            /// </summary>
            SPE_DATA_INT = 2, // size: 2 bytes

            /// <summary>
            /// The sp e_ dat a_ uint.
            /// </summary>
            SPE_DATA_UINT = 3, // size: 2 bytes
        }

        /// <summary>
        /// The header size.
        /// </summary>
        private enum HeaderSize
        {
            /// <summary>
            /// The sp e_ heade r_ size.
            /// </summary>
            SPE_HEADER_SIZE = 4100, // Fixed binary header size
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The is correct format.
        /// </summary>
        /// <param name="fs">
        /// The fs.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsCorrectFormat(FileStream fs)
        {
            BinaryReader _brdrReader = new System.IO.BinaryReader(fs);

            // Make sure file size > 4100 (data begins after a 4100-byte header).
            if (fs.Length <= 4100)
            {
                return false;
            }

            // Datatype field in header ONLY can be 0~3
            // We can finde the datatype at offset 108 according to the file spec.
            fs.Seek(108, System.IO.SeekOrigin.Begin);

            DataType _dtDataType = (DataType)_brdrReader.ReadInt16();
            if (_dtDataType < DataType.SPE_DATA_FLOAT || _dtDataType > DataType.SPE_DATA_UINT)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// The load data.
        /// </summary>
        /// <param name="__fsFileStream">
        /// The __fs file stream.
        /// </param>
        /// <exception cref="UnexpectedFormatException">
        /// </exception>
        public override void LoadData(FileStream __fsFileStream)
        {
            BinaryReader _brdrReader = new System.IO.BinaryReader(__fsFileStream);

            // only read necessary params from file header
            __fsFileStream.Seek(42, System.IO.SeekOrigin.Begin);
            int _iXDimension = (int)_brdrReader.ReadInt16();
            __fsFileStream.Seek(108, System.IO.SeekOrigin.Begin);
            DataType _dtDataType = (DataType)_brdrReader.ReadInt16();
            __fsFileStream.Seek(656, System.IO.SeekOrigin.Begin);
            int _iYDimension = (int)_brdrReader.ReadInt16();
            __fsFileStream.Seek(1446, System.IO.SeekOrigin.Begin);
            uint numframes = (UInt32)_brdrReader.ReadInt32();

            // Start reading the XCalibStruct.
            SpeCalib XCalib = new SpeCalib(0, 0);
            __fsFileStream.Seek(3000, System.IO.SeekOrigin.Begin);
            XCalib.Offset = (double)_brdrReader.ReadDouble();
            __fsFileStream.Seek(3008, System.IO.SeekOrigin.Begin);
            XCalib.Factor = (double)_brdrReader.ReadDouble();
            __fsFileStream.Seek(3016, System.IO.SeekOrigin.Begin);
            XCalib.current_unit = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3098, System.IO.SeekOrigin.Begin);
            XCalib.CalibValid = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3101, System.IO.SeekOrigin.Begin);
            XCalib.PolynomOrder = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3263, System.IO.SeekOrigin.Begin);

            XCalib.PolynomCoeff[0] = _brdrReader.ReadDouble();
            XCalib.PolynomCoeff[1] = _brdrReader.ReadDouble();
            XCalib.PolynomCoeff[2] = _brdrReader.ReadDouble();
            XCalib.PolynomCoeff[3] = _brdrReader.ReadDouble();
            XCalib.PolynomCoeff[4] = _brdrReader.ReadDouble();
            XCalib.PolynomCoeff[5] = _brdrReader.ReadDouble();

            __fsFileStream.Seek(3311, System.IO.SeekOrigin.Begin);
            XCalib.LaserPosition = (double)_brdrReader.ReadDouble();

            // Start reading the YCalibStruct.
            SpeCalib YCalib = new SpeCalib(0, 0);
            __fsFileStream.Seek(3489, System.IO.SeekOrigin.Begin); // move ptr to x_calib start
            YCalib.Offset = (double)_brdrReader.ReadDouble();
            __fsFileStream.Seek(3497, System.IO.SeekOrigin.Begin);
            YCalib.Factor = (double)_brdrReader.ReadDouble();
            __fsFileStream.Seek(3505, System.IO.SeekOrigin.Begin);
            YCalib.current_unit = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3587, System.IO.SeekOrigin.Begin);
            YCalib.CalibValid = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3590, System.IO.SeekOrigin.Begin);
            YCalib.PolynomOrder = (char)_brdrReader.ReadChar();
            __fsFileStream.Seek(3752, System.IO.SeekOrigin.Begin);

            YCalib.PolynomCoeff[0] = _brdrReader.ReadDouble();
            YCalib.PolynomCoeff[1] = _brdrReader.ReadDouble();
            YCalib.PolynomCoeff[2] = _brdrReader.ReadDouble();
            YCalib.PolynomCoeff[3] = _brdrReader.ReadDouble();
            YCalib.PolynomCoeff[4] = _brdrReader.ReadDouble();
            YCalib.PolynomCoeff[5] = _brdrReader.ReadDouble();

            __fsFileStream.Seek(3800, System.IO.SeekOrigin.Begin);
            YCalib.LaserPosition = (double)_brdrReader.ReadDouble();

            int _iDimension;
            SpeCalib _calCurrCalib;
            if (_iYDimension == 1)
            {
                _iDimension = _iXDimension;
                _calCurrCalib = XCalib;
            }
            else if (_iXDimension == 1)
            {
                _iDimension = _iYDimension;
                _calCurrCalib = YCalib;
            }
            else
            {
                throw new UnexpectedFormatException("xylib does not support 2-D images");
            }

            __fsFileStream.Seek(4100, System.IO.SeekOrigin.Begin); // move ptr to frames-start
            for (int frm = 0; frm < (int)numframes; frm++)
            {
                Block _blkBlock = new Block();

                Column _colXCol = this.GetCalibColumn(_calCurrCalib, _iDimension);
                _blkBlock.AddColumn(_colXCol, string.Empty, true);

                ListColumn _colYCol = new ListColumn();

                for (int i = 0; i < _iDimension; ++i)
                {
                    double _dYVal = 0;
                    switch (_dtDataType)
                    {
                        case DataType.SPE_DATA_FLOAT:
                            _dYVal = (double)_brdrReader.ReadSingle();
                            break;
                        case DataType.SPE_DATA_LONG:
                            _dYVal = (double)_brdrReader.ReadInt32();
                            break;
                        case DataType.SPE_DATA_INT:
                            _dYVal = (double)_brdrReader.ReadInt16();
                            break;
                        case DataType.SPE_DATA_UINT:
                            _dYVal = (double)_brdrReader.ReadUInt16();
                            break;
                        default:
                            break;
                    }

                    _colYCol.AddValue(_dYVal);
                }

                _blkBlock.AddColumn(_colYCol, string.Empty, true);
                this.m_blcklstBlocks.Add(_blkBlock);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get calib column.
        /// </summary>
        /// <param name="__calCalib">
        /// The __cal calib.
        /// </param>
        /// <param name="_iDimension">
        /// The _i dimension.
        /// </param>
        /// <returns>
        /// The <see cref="Column"/>.
        /// </returns>
        private Column GetCalibColumn(SpeCalib __calCalib, int _iDimension)
        {
            this.FormatAssert(__calCalib.PolynomOrder <= 6, "bad polynom header");

            if (__calCalib.CalibValid == 0)
            {
                return new StepColumn(0, 1);
            }
            else if (__calCalib.PolynomOrder == 1)
            {
                return new StepColumn(__calCalib.PolynomCoeff[0], __calCalib.PolynomCoeff[1]);
            }
            else
            {
                ListColumn _colXCol = new ListColumn();
                for (int _iI = 0; _iI < _iDimension; _iI++)
                {
                    double x = 0;
                    for (int _iJ = 0; _iJ <= __calCalib.PolynomOrder; _iJ++)
                    {
                        x += __calCalib.PolynomCoeff[_iJ] * Math.Pow(_iI + 1.0f, (double)_iJ);
                    }

                    _colXCol.AddValue(x);
                }

                return _colXCol;
            }
        }

        #endregion

        /// <summary>
        /// The spe calib.
        /// </summary>
        private struct SpeCalib
        {
            // char reserved1;                    // +17 reserved 
            // char string[40];                   // +18 special string for scaling 
            // char reserved2[40];                // +58 reserved
            #region Fields

            /// <summary>
            /// The calib valid.
            /// </summary>
            public char CalibValid; // +98 flag of whether calibration is valid

            /// <summary>
            /// The factor.
            /// </summary>
            public double Factor; // +8 factor for absolute data scaling 

            // char input_unit;                   // +99 current input units for  "calib-value" 
            // char polynom_unit;                 // +100 linear UNIT and used 
            // in the "polynom-coeff" 

            /// <summary>
            /// The laser position.
            /// </summary>
            public double LaserPosition; // +311 laser wavenumber for relativ WN 

            /// <summary>
            /// The offset.
            /// </summary>
            public double Offset; // +0 offset for absolute data scaling

            /// <summary>
            /// The polynom coeff.
            /// </summary>
            public double[] PolynomCoeff; // +263 polynom COEFFICIENTS 

            /// <summary>
            /// The polynom order.
            /// </summary>
            public char PolynomOrder; // +101 ORDER of calibration POLYNOM 

            /// <summary>
            /// The current_unit.
            /// </summary>
            public char current_unit; // +16 selected scaling unit 

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="SpeCalib"/> struct.
            /// </summary>
            /// <param name="calib">
            /// The calib.
            /// </param>
            /// <param name="order">
            /// The order.
            /// </param>
            public SpeCalib(int calib, int order)
            {
                this.Offset = 0;
                this.Factor = 0;
                this.current_unit = (char)0;
                this.CalibValid = (char)calib;
                this.PolynomOrder = (char)order;
                this.PolynomCoeff = new double[6];
                this.LaserPosition = 0;
            }

            #endregion

            // char reserved3;                    // +319 reserved 
            // unsigned char new_calib_flag;      // +320 If set to 200, valid label below 
            // char calib_label[81];              // +321 Calibration label (NULL term'd) 
            // char expansion[87];                // +402 Calibration Expansion area 
        };
    }
}