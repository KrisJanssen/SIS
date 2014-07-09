using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Data
{
    [Document("WinSpec Data", ".spe")]
    class WinspecSPEDataSet : DataSet
    {
        private enum HeaderSize
        {
            SPE_HEADER_SIZE = 4100, // Fixed binary header size
        }

        // Datatypes of DATA point in spe_file.
        private enum DataType
        {
            SPE_DATA_FLOAT = 0,     // size: 4 bytes
            SPE_DATA_LONG = 1,      // size: 4 bytes
            SPE_DATA_INT = 2,       // size: 2 bytes
            SPE_DATA_UINT = 3,      // size: 2 bytes
        }

        // Calibration structure in SPE format.
        // NOTE: fields that we don't care have been commented out
        private struct SpeCalib
        {
            public SpeCalib(int calib, int order)
            {
                Offset = 0;
                Factor = 0;
                current_unit = (char)0;
                CalibValid = (char)calib;
                PolynomOrder = (char)order;
                PolynomCoeff = new double[6];
                LaserPosition = 0;
            }

            public double Offset;                    // +0 offset for absolute data scaling
            public double Factor;                    // +8 factor for absolute data scaling 
            public char current_unit;                // +16 selected scaling unit 
            //    char reserved1;                    // +17 reserved 
            //    char string[40];                   // +18 special string for scaling 
            //    char reserved2[40];                // +58 reserved
            public char CalibValid;                  // +98 flag of whether calibration is valid
            //    char input_unit;                   // +99 current input units for  "calib-value" 
            //    char polynom_unit;                 // +100 linear UNIT and used 
            // in the "polynom-coeff" 
            public char PolynomOrder;                // +101 ORDER of calibration POLYNOM 
            //    char calib_count;                  // +102 valid calibration data pairs 
            //    double pixel_position[10];         // +103 pixel pos. of calibration data 
            //    double calib_value[10];            // +183 calibration VALUE at above pos 
            public double[] PolynomCoeff;            // +263 polynom COEFFICIENTS 
            public double LaserPosition;             // +311 laser wavenumber for relativ WN 
            //    char reserved3;                    // +319 reserved 
            //    unsigned char new_calib_flag;      // +320 If set to 200, valid label below 
            //    char calib_label[81];              // +321 Calibration label (NULL term'd) 
            //    char expansion[87];                // +402 Calibration Expansion area 
        };

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

        public override bool IsCorrectFormat(System.IO.FileStream fs)
        {
            System.IO.BinaryReader _brdrReader = new System.IO.BinaryReader(fs);
            
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

        public override void LoadData(System.IO.FileStream __fsFileStream) 
        {
            System.IO.BinaryReader _brdrReader = new System.IO.BinaryReader(__fsFileStream);

            // only read necessary params from file header
            __fsFileStream.Seek(42, System.IO.SeekOrigin.Begin);
            int _iXDimension = (int)_brdrReader.ReadInt16();
            __fsFileStream.Seek(108, System.IO.SeekOrigin.Begin);
            DataType _dtDataType = (DataType)_brdrReader.ReadInt16();
            __fsFileStream.Seek(656, System.IO.SeekOrigin.Begin);
            int _iYDimension = (int)_brdrReader.ReadInt16();
            __fsFileStream.Seek(1446, System.IO.SeekOrigin.Begin);
            UInt32 numframes = (UInt32)_brdrReader.ReadInt32();

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
            __fsFileStream.Seek(3489, System.IO.SeekOrigin.Begin);   // move ptr to x_calib start
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
            
            __fsFileStream.Seek(4100, System.IO.SeekOrigin.Begin);      // move ptr to frames-start
            for (int frm = 0; frm < (int)numframes; frm++) 
            {
                Block _blkBlock = new Block();
                
                Column _colXCol = this.GetCalibColumn(_calCurrCalib, _iDimension);
                _blkBlock.AddColumn(_colXCol, "", true);
                
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
                _blkBlock.AddColumn(_colYCol, "", true);
                this.m_blcklstBlocks.Add(_blkBlock);
            }
        }

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
    }
}
