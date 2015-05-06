using Microsoft.Win32;
//using PaintDotNet.Threading;
using SIS.SystemLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SIS.Library
{
    /// <summary>
    /// Defines miscellaneous constants and static functions.
    /// </summary>
    /// // TODO: refactor into mini static classes
    public sealed partial class Utility
    {
        public static Bitmap RainbowColorBar(
            int __iImageWidth,
            UInt32 __ui32MinIntensity,
            UInt32 __ui32MaxIntensity,
            bool __bNormalizedFullColor,
            bool __bNormalizedRed,
            bool __bNormalizedGreen)
        {
            // The bitmap that will hold the color scale.
            Bitmap _bmpBar = new Bitmap(20, __iImageWidth);

            // An instance of the Fast Bitmap class for unsafe, fast pixel assignment.
            FastBitmap _fbmpFastBitmap = new FastBitmap(_bmpBar);

            // TODO: Make this a bit cleaner.
            for (int _iI = 0; _iI < __iImageWidth; _iI++)
            {
                for (int _iJ = 0; _iJ < 20; _iJ++)
                {
                    int _CurrentColor = (int)__ui32MinIntensity + _iI * ((int)__ui32MaxIntensity - (int)__ui32MinIntensity) / __iImageWidth;
                    _fbmpFastBitmap.SetPixel(_iJ, (__iImageWidth - 1) - _iI, Utility.ColorPicker(_CurrentColor, (int)__ui32MaxIntensity, __bNormalizedFullColor, __bNormalizedRed, __bNormalizedGreen));
                }
            }

            // Assign the newly created Bitmap.
            _bmpBar = _fbmpFastBitmap.Bitmap;

            // We don't need the Fast Bitmap anymore.
            _fbmpFastBitmap.Release();

            // Return the newly created Bitmap.
            return _bmpBar;
        }

        public static Color ColorPicker(
            int __iIntensity,
            int __iMaxIntensity,
            bool __bNormalizedFullColor,
            bool __bNormalizedRed,
            bool __bNormalizedGreen)
        {
            // The RGB colorspace holds 1792 discrete colors...
            // Uint32 intensity values are translated into RGB color values.
            // The RGB palette is traversed from 0-255 0 0 / 255 0-255 0 / 255-0 255 0 / 0 255 0-255 / 0 255-0 255 / 0-255 0 255 / 255 0-255 255
            // depending on the value of the original Uint32 value.
            // Doing it like this will get you from Black, Yellow, Red, Green, Magenta, Blue, Violet to white in 1792 discrete steps.
            int _intMaxColor = 1792;

            // We might need to perform normalisation of the intensities to fit them inside the available color palette.
            // _dblIntensityCorr will be used for this.
            double _dblIntensityCorr = 1.0;

            // Instantiate the color that will be returned.
            Color _colPickedColor = Color.Empty;

            // Create a working copy of the passed intensity value.
            int _iProcessedIntensity = __iIntensity;

            // Do we want a normalized image in the full colorspace?
            if (__bNormalizedFullColor)
            {
                // Account for the special case where the image might be empty, e.g. when creating a new image.
                if (__iMaxIntensity > 0)
                {
                    _dblIntensityCorr = (double)_intMaxColor / (double)__iMaxIntensity;
                    _iProcessedIntensity = Convert.ToInt32(Math.Round(__iIntensity * _dblIntensityCorr));
                }
            }

            // If we don't want Red or Green channel images we can pick from the full colorspace.
            if (!__bNormalizedRed & !__bNormalizedGreen)
            {
                if (_iProcessedIntensity >= 1792)
                {
                    _colPickedColor = Color.FromArgb(255, 255, 255, 255);
                }

                else
                {
                    switch (_iProcessedIntensity / 256)
                    {
                        case 0: _colPickedColor = Color.FromArgb(_iProcessedIntensity % 256, 0, 0);
                            break;
                        case 1: _colPickedColor = Color.FromArgb(255, _iProcessedIntensity % 256, 0);
                            break;
                        case 2: _colPickedColor = Color.FromArgb(255 - (_iProcessedIntensity % 256), 255, 0);
                            break;
                        case 3: _colPickedColor = Color.FromArgb(0, 255, _iProcessedIntensity % 256);
                            break;
                        case 4: _colPickedColor = Color.FromArgb(0, 255 - (_iProcessedIntensity % 256), 255);
                            break;
                        case 5: _colPickedColor = Color.FromArgb(_iProcessedIntensity % 256, 0, 255);
                            break;
                        default: _colPickedColor = Color.FromArgb(255, _iProcessedIntensity % 256, 255);
                            break;
                    }

                }
            }

            // Pick only from the Red colorspace.
            if (__bNormalizedRed)
            {
                // Account for the special case where the image might be empty, e.g. when creating a new image.
                if (__iMaxIntensity > 0)
                {
                    _dblIntensityCorr = (double)255 / (double)__iMaxIntensity;
                    _iProcessedIntensity = Convert.ToInt32(Math.Round(__iIntensity * _dblIntensityCorr));
                    _colPickedColor = Color.FromArgb(_iProcessedIntensity % 256, 0, 0);
                }
            }

            // Pick only from the Green colorspace.
            if (__bNormalizedGreen)
            {
                // Account for the special case where the image might be empty, e.g. when creating a new image.
                if (__iMaxIntensity > 0)
                {
                    _dblIntensityCorr = (double)255 / (double)__iMaxIntensity;
                    _iProcessedIntensity = Convert.ToInt32(Math.Round(__iIntensity * _dblIntensityCorr));
                    _colPickedColor = Color.FromArgb(0, _iProcessedIntensity % 256, 0);
                }
            }

            // Finally we return the chosen color.
            return _colPickedColor;
        }

        public static Bitmap DrawScanToBmp(
            UInt32[] __ui32Intensities,
            UInt32 __ui32MaxIntensity,
            UInt32 __ui32MinIntensity,
            int __iImageWidth,
            int __iImageHeight,
            int __iXOverScanPx,
            int __iYOverScanPx,
            bool __bCorrected,
            bool __bNormalized,
            bool __bRed,
            bool __bGreen)
        {
            // Max and Min intensity are ALWAYS recalculated on the most current data.
            UInt32 _ui32MaxIntensity = 0;
            UInt32 _ui32MinIntensity = 2147483647;

            Bitmap _bmpImage;

            // The new bitmap that holds the image.
            if (__bCorrected)
            {
                _bmpImage = new Bitmap(__iImageWidth, __iImageHeight);
            }
            else
            {
                _bmpImage = new Bitmap(__iImageWidth + __iXOverScanPx, __iImageHeight + __iYOverScanPx);
            }

            // Initialize a FastBitmap object for unsafe writing using the new (empty) bitmap as the image source.
            FastBitmap _fbmpFastBitmap = new FastBitmap(_bmpImage);

            // Get Min and Max intensity value.
            _ui32MaxIntensity = __ui32MaxIntensity;
            _ui32MinIntensity = __ui32MinIntensity;

            #region Comment on coordinate translations
            // The general convention on bitmaps is that coordinate systems extend from top to bottom and left to right (row-column convention).
            // The first pixel, (0,0) is thus located in the left topmost corner of the bitmap. This is important to know if one wants to relate 
            // the bitmap image to the actual positioning of features on the physical sample.
            //         X
            // (b00)--(b10)--(b20)
            //   |      |      |
            // (b01)--(b11)--(b21)Y
            //   |      |      |
            // (b02)--(b12)--(b22)
            //
            // Image data is always supplied with the first value as the Bottom Left pixel of the physical sample. The first scanline will proceed 
            // from left to right and consecutive scanlines will move up:
            //
            // The physical Image:       The array holding the data with corresponding bitmap coordinates:
            // 
            // -------------             [p00 p01 p02 p10 p11 p12 p20 p21 p22]
            // |p20 p21 p22|               |   |   |   |   |   |   |   |   |
            // |p10 p11 p12|              b02 b12 b22 b01 b11 b21 b00 b10 b20
            // |p00 p01 p02|              Count down in the bimap (b coordinates) for Y and count up for X!!!
            // -------------
            //
            // So if we cycle through the array using I and J where I represents rows (Y) in the image and J represents columns (X) we need to
            // Count from 0 to ImageWidth for J (columns, X) and from Imagewidth to 0 for I (rows, Y) to assign consecutive elements of the 
            // array to the image. You basically fill the bitmap from the bottom up...
            //
            // Processing on the data during aqcuisition will ensure that the data array is ALWAYS supplied in the same layout!
            #endregion

            for (int _intI = 0; _intI < __iImageWidth; _intI++)
            {
                for (int _intJ = 0; _intJ < __iImageWidth + __iXOverScanPx; _intJ++)
                {
                    if (__bCorrected)
                    {
                        if (_intI % 2 == 0)
                        {
                            if (_intJ >= __iXOverScanPx)
                            {
                                _fbmpFastBitmap.SetPixel(_intJ - __iXOverScanPx, __iImageHeight - _intI - 1, Utility.ColorPicker((int)__ui32Intensities[_intI * (__iImageWidth + __iXOverScanPx) + _intJ], (int)_ui32MaxIntensity, __bNormalized, __bRed, __bGreen));
                            }
                        }
                        else
                        {
                            if (_intJ < __iImageWidth)
                            {
                                _fbmpFastBitmap.SetPixel(_intJ, __iImageHeight - _intI - 1, Utility.ColorPicker((int)__ui32Intensities[_intI * (__iImageWidth + __iXOverScanPx) + _intJ], (int)_ui32MaxIntensity, __bNormalized, __bRed, __bGreen));
                            }
                        }
                    }
                    else
                    {
                        _fbmpFastBitmap.SetPixel(_intJ, (__iImageHeight + __iYOverScanPx) - _intI - 1, Utility.ColorPicker((int)__ui32Intensities[_intI * (__iImageWidth + __iXOverScanPx) + _intJ], (int)_ui32MaxIntensity, __bNormalized, __bRed, __bGreen));
                    }
                }
            }

            // Pass the updated bitmap back.
            _bmpImage = _fbmpFastBitmap.Bitmap;

            // Dispose of the unsafe bitmap object.
            _fbmpFastBitmap.Release();

            // Return the bitmap.
            return _bmpImage;
        }
    }
}