//using System;
//using System.Collections.Generic;
//using System.Text;
//using System;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Text;
//using System.Web;

//namespace KUL.MDS.ScanModes
//{
//    class RasteredImageScanMode
//    {
//        public double[,] ConvertImage(string __sFile, int _iImageWidthPx, bool __bFast)
//        {
//            double[,] _dCoordinates = new double[3, ((__iImageWidthPx + __iOverScanPx) * __iImageWidthPx)];

//            Bitmap _image = new Bitmap(__sFile);

//            _image = MakeGrayscale(_image);

//            _image = new Bitmap(_image, _iImageWidthPx, _iImageWidthPx);


//            //clean up
//            _image.Dispose();
//        }

//        public static Bitmap MakeGrayscale(Bitmap original)
//        {
//            //create a blank bitmap the same size as original
//            Bitmap newBitmap =
//               new Bitmap(original.Width, original.Height);

//            //get a graphics object from the new image
//            Graphics g = Graphics.FromImage(newBitmap);

//            //create the grayscale ColorMatrix
//            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
//            {
//                new float[] {.3f, .3f, .3f, 0, 0},
//                new float[] {.59f, .59f, .59f, 0, 0},
//                new float[] {.11f, .11f, .11f, 0, 0},
//                new float[] {0, 0, 0, 1, 0},
//                new float[] {0, 0, 0, 0, 1}
//            });

//            //create some image attributes
//            ImageAttributes attributes = new ImageAttributes();

//            //set the color matrix attribute
//            attributes.SetColorMatrix(colorMatrix);

//            //draw the original image on the new image
//            //using the grayscale color matrix
//            g.DrawImage(original,
//               new Rectangle(0, 0, original.Width, original.Height),
//               0, 0, original.Width, original.Height,
//               GraphicsUnit.Pixel, attributes);

//            //dispose the Graphics object
//            g.Dispose();
//            return newBitmap;
//        }
//    }
//}
