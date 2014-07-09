// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="Utility.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Defines miscellaneous constants and static functions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;

    using SIS.Resources;

    /// <summary>
    /// Defines miscellaneous constants and static functions.
    /// </summary>
    /// // TODO: refactor into mini static classes
    public sealed partial class Utility
    {
        #region Static Fields

        /// <summary>
        /// The identity 5 x 5 f.
        /// </summary>
        public static readonly float[][] Identity5x5F = new[]
                                                            {
                                                                new float[] { 1, 0, 0, 0, 0 }, 
                                                                new float[] { 0, 1, 0, 0, 0 }, 
                                                                new float[] { 0, 0, 1, 0, 0 }, 
                                                                new float[] { 0, 0, 0, 1, 0 }, 
                                                                new float[] { 0, 0, 0, 0, 1 }
                                                            };

        /// <summary>
        /// The identity color matrix.
        /// </summary>
        public static readonly ColorMatrix IdentityColorMatrix = new ColorMatrix(Identity5x5F);

        /// <summary>
        /// The transparent key.
        /// </summary>
        public static readonly Color TransparentKey = Color.FromArgb(192, 192, 192);

        /// <summary>
        /// The mas table.
        /// </summary>
        private static readonly uint[] masTable =
            {
                0x00000000, 0x00000000, 0, // 0
                0x00000001, 0x00000000, 0, // 1
                0x00000001, 0x00000000, 1, // 2
                0xAAAAAAAB, 0x00000000, 33, // 3
                0x00000001, 0x00000000, 2, // 4
                0xCCCCCCCD, 0x00000000, 34, // 5
                0xAAAAAAAB, 0x00000000, 34, // 6
                0x49249249, 0x49249249, 33, // 7
                0x00000001, 0x00000000, 3, // 8
                0x38E38E39, 0x00000000, 33, // 9
                0xCCCCCCCD, 0x00000000, 35, // 10
                0xBA2E8BA3, 0x00000000, 35, // 11
                0xAAAAAAAB, 0x00000000, 35, // 12
                0x4EC4EC4F, 0x00000000, 34, // 13
                0x49249249, 0x49249249, 34, // 14
                0x88888889, 0x00000000, 35, // 15
                0x00000001, 0x00000000, 4, // 16
                0xF0F0F0F1, 0x00000000, 36, // 17
                0x38E38E39, 0x00000000, 34, // 18
                0xD79435E5, 0xD79435E5, 36, // 19
                0xCCCCCCCD, 0x00000000, 36, // 20
                0xC30C30C3, 0xC30C30C3, 36, // 21
                0xBA2E8BA3, 0x00000000, 36, // 22
                0xB21642C9, 0x00000000, 36, // 23
                0xAAAAAAAB, 0x00000000, 36, // 24
                0x51EB851F, 0x00000000, 35, // 25
                0x4EC4EC4F, 0x00000000, 35, // 26
                0x97B425ED, 0x97B425ED, 36, // 27
                0x49249249, 0x49249249, 35, // 28
                0x8D3DCB09, 0x00000000, 36, // 29
                0x88888889, 0x00000000, 36, // 30
                0x42108421, 0x42108421, 35, // 31
                0x00000001, 0x00000000, 5, // 32
                0x3E0F83E1, 0x00000000, 35, // 33
                0xF0F0F0F1, 0x00000000, 37, // 34
                0x75075075, 0x75075075, 36, // 35
                0x38E38E39, 0x00000000, 35, // 36
                0x6EB3E453, 0x6EB3E453, 36, // 37
                0xD79435E5, 0xD79435E5, 37, // 38
                0x69069069, 0x69069069, 36, // 39
                0xCCCCCCCD, 0x00000000, 37, // 40
                0xC7CE0C7D, 0x00000000, 37, // 41
                0xC30C30C3, 0xC30C30C3, 37, // 42
                0x2FA0BE83, 0x00000000, 35, // 43
                0xBA2E8BA3, 0x00000000, 37, // 44
                0x5B05B05B, 0x5B05B05B, 36, // 45
                0xB21642C9, 0x00000000, 37, // 46
                0xAE4C415D, 0x00000000, 37, // 47
                0xAAAAAAAB, 0x00000000, 37, // 48
                0x5397829D, 0x00000000, 36, // 49
                0x51EB851F, 0x00000000, 36, // 50
                0xA0A0A0A1, 0x00000000, 37, // 51
                0x4EC4EC4F, 0x00000000, 36, // 52
                0x9A90E7D9, 0x9A90E7D9, 37, // 53
                0x97B425ED, 0x97B425ED, 37, // 54
                0x94F2094F, 0x94F2094F, 37, // 55
                0x49249249, 0x49249249, 36, // 56
                0x47DC11F7, 0x47DC11F7, 36, // 57
                0x8D3DCB09, 0x00000000, 37, // 58
                0x22B63CBF, 0x00000000, 35, // 59
                0x88888889, 0x00000000, 37, // 60
                0x4325C53F, 0x00000000, 36, // 61
                0x42108421, 0x42108421, 36, // 62
                0x41041041, 0x41041041, 36, // 63
                0x00000001, 0x00000000, 6, // 64
                0xFC0FC0FD, 0x00000000, 38, // 65
                0x3E0F83E1, 0x00000000, 36, // 66
                0x07A44C6B, 0x00000000, 33, // 67
                0xF0F0F0F1, 0x00000000, 38, // 68
                0x76B981DB, 0x00000000, 37, // 69
                0x75075075, 0x75075075, 37, // 70
                0xE6C2B449, 0x00000000, 38, // 71
                0x38E38E39, 0x00000000, 36, // 72
                0x381C0E07, 0x381C0E07, 36, // 73
                0x6EB3E453, 0x6EB3E453, 37, // 74
                0x1B4E81B5, 0x00000000, 35, // 75
                0xD79435E5, 0xD79435E5, 38, // 76
                0x3531DEC1, 0x00000000, 36, // 77
                0x69069069, 0x69069069, 37, // 78
                0xCF6474A9, 0x00000000, 38, // 79
                0xCCCCCCCD, 0x00000000, 38, // 80
                0xCA4587E7, 0x00000000, 38, // 81
                0xC7CE0C7D, 0x00000000, 38, // 82
                0x3159721F, 0x00000000, 36, // 83
                0xC30C30C3, 0xC30C30C3, 38, // 84
                0xC0C0C0C1, 0x00000000, 38, // 85
                0x2FA0BE83, 0x00000000, 36, // 86
                0x2F149903, 0x00000000, 36, // 87
                0xBA2E8BA3, 0x00000000, 38, // 88
                0xB81702E1, 0x00000000, 38, // 89
                0x5B05B05B, 0x5B05B05B, 37, // 90
                0x2D02D02D, 0x2D02D02D, 36, // 91
                0xB21642C9, 0x00000000, 38, // 92
                0xB02C0B03, 0x00000000, 38, // 93
                0xAE4C415D, 0x00000000, 38, // 94
                0x2B1DA461, 0x2B1DA461, 36, // 95
                0xAAAAAAAB, 0x00000000, 38, // 96
                0xA8E83F57, 0xA8E83F57, 38, // 97
                0x5397829D, 0x00000000, 37, // 98
                0xA57EB503, 0x00000000, 38, // 99
                0x51EB851F, 0x00000000, 37, // 100
                0xA237C32B, 0xA237C32B, 38, // 101
                0xA0A0A0A1, 0x00000000, 38, // 102
                0x9F1165E7, 0x9F1165E7, 38, // 103
                0x4EC4EC4F, 0x00000000, 37, // 104
                0x27027027, 0x27027027, 36, // 105
                0x9A90E7D9, 0x9A90E7D9, 38, // 106
                0x991F1A51, 0x991F1A51, 38, // 107
                0x97B425ED, 0x97B425ED, 38, // 108
                0x2593F69B, 0x2593F69B, 36, // 109
                0x94F2094F, 0x94F2094F, 38, // 110
                0x24E6A171, 0x24E6A171, 36, // 111
                0x49249249, 0x49249249, 37, // 112
                0x90FDBC09, 0x90FDBC09, 38, // 113
                0x47DC11F7, 0x47DC11F7, 37, // 114
                0x8E78356D, 0x8E78356D, 38, // 115
                0x8D3DCB09, 0x00000000, 38, // 116
                0x23023023, 0x23023023, 36, // 117
                0x22B63CBF, 0x00000000, 36, // 118
                0x44D72045, 0x00000000, 37, // 119
                0x88888889, 0x00000000, 38, // 120
                0x8767AB5F, 0x8767AB5F, 38, // 121
                0x4325C53F, 0x00000000, 37, // 122
                0x85340853, 0x85340853, 38, // 123
                0x42108421, 0x42108421, 37, // 124
                0x10624DD3, 0x00000000, 35, // 125
                0x41041041, 0x41041041, 37, // 126
                0x10204081, 0x10204081, 35, // 127
                0x00000001, 0x00000000, 7, // 128
                0x0FE03F81, 0x00000000, 35, // 129
                0xFC0FC0FD, 0x00000000, 39, // 130
                0xFA232CF3, 0x00000000, 39, // 131
                0x3E0F83E1, 0x00000000, 37, // 132
                0xF6603D99, 0x00000000, 39, // 133
                0x07A44C6B, 0x00000000, 34, // 134
                0xF2B9D649, 0x00000000, 39, // 135
                0xF0F0F0F1, 0x00000000, 39, // 136
                0x077975B9, 0x00000000, 34, // 137
                0x76B981DB, 0x00000000, 38, // 138
                0x75DED953, 0x00000000, 38, // 139
                0x75075075, 0x75075075, 38, // 140
                0x3A196B1F, 0x00000000, 37, // 141
                0xE6C2B449, 0x00000000, 39, // 142
                0xE525982B, 0x00000000, 39, // 143
                0x38E38E39, 0x00000000, 37, // 144
                0xE1FC780F, 0x00000000, 39, // 145
                0x381C0E07, 0x381C0E07, 37, // 146
                0xDEE95C4D, 0x00000000, 39, // 147
                0x6EB3E453, 0x6EB3E453, 38, // 148
                0xDBEB61EF, 0x00000000, 39, // 149
                0x1B4E81B5, 0x00000000, 36, // 150
                0x36406C81, 0x00000000, 37, // 151
                0xD79435E5, 0xD79435E5, 39, // 152
                0xD62B80D7, 0x00000000, 39, // 153
                0x3531DEC1, 0x00000000, 37, // 154
                0xD3680D37, 0x00000000, 39, // 155
                0x69069069, 0x69069069, 38, // 156
                0x342DA7F3, 0x00000000, 37, // 157
                0xCF6474A9, 0x00000000, 39, // 158
                0xCE168A77, 0xCE168A77, 39, // 159
                0xCCCCCCCD, 0x00000000, 39, // 160
                0xCB8727C1, 0x00000000, 39, // 161
                0xCA4587E7, 0x00000000, 39, // 162
                0xC907DA4F, 0x00000000, 39, // 163
                0xC7CE0C7D, 0x00000000, 39, // 164
                0x634C0635, 0x00000000, 38, // 165
                0x3159721F, 0x00000000, 37, // 166
                0x621B97C3, 0x00000000, 38, // 167
                0xC30C30C3, 0xC30C30C3, 39, // 168
                0x60F25DEB, 0x00000000, 38, // 169
                0xC0C0C0C1, 0x00000000, 39, // 170
                0x17F405FD, 0x17F405FD, 36, // 171
                0x2FA0BE83, 0x00000000, 37, // 172
                0xBD691047, 0xBD691047, 39, // 173
                0x2F149903, 0x00000000, 37, // 174
                0x5D9F7391, 0x00000000, 38, // 175
                0xBA2E8BA3, 0x00000000, 39, // 176
                0x5C90A1FD, 0x5C90A1FD, 38, // 177
                0xB81702E1, 0x00000000, 39, // 178
                0x5B87DDAD, 0x5B87DDAD, 38, // 179
                0x5B05B05B, 0x5B05B05B, 38, // 180
                0xB509E68B, 0x00000000, 39, // 181
                0x2D02D02D, 0x2D02D02D, 37, // 182
                0xB30F6353, 0x00000000, 39, // 183
                0xB21642C9, 0x00000000, 39, // 184
                0x1623FA77, 0x1623FA77, 36, // 185
                0xB02C0B03, 0x00000000, 39, // 186
                0xAF3ADDC7, 0x00000000, 39, // 187
                0xAE4C415D, 0x00000000, 39, // 188
                0x15AC056B, 0x15AC056B, 36, // 189
                0x2B1DA461, 0x2B1DA461, 37, // 190
                0xAB8F69E3, 0x00000000, 39, // 191
                0xAAAAAAAB, 0x00000000, 39, // 192
                0x15390949, 0x00000000, 36, // 193
                0xA8E83F57, 0xA8E83F57, 39, // 194
                0x15015015, 0x15015015, 36, // 195
                0x5397829D, 0x00000000, 38, // 196
                0xA655C439, 0xA655C439, 39, // 197
                0xA57EB503, 0x00000000, 39, // 198
                0x5254E78F, 0x00000000, 38, // 199
                0x51EB851F, 0x00000000, 38, // 200
                0x028C1979, 0x00000000, 33, // 201
                0xA237C32B, 0xA237C32B, 39, // 202
                0xA16B312F, 0x00000000, 39, // 203
                0xA0A0A0A1, 0x00000000, 39, // 204
                0x4FEC04FF, 0x00000000, 38, // 205
                0x9F1165E7, 0x9F1165E7, 39, // 206
                0x27932B49, 0x00000000, 37, // 207
                0x4EC4EC4F, 0x00000000, 38, // 208
                0x9CC8E161, 0x00000000, 39, // 209
                0x27027027, 0x27027027, 37, // 210
                0x9B4C6F9F, 0x00000000, 39, // 211
                0x9A90E7D9, 0x9A90E7D9, 39, // 212
                0x99D722DB, 0x00000000, 39, // 213
                0x991F1A51, 0x991F1A51, 39, // 214
                0x4C346405, 0x00000000, 38, // 215
                0x97B425ED, 0x97B425ED, 39, // 216
                0x4B809701, 0x4B809701, 38, // 217
                0x2593F69B, 0x2593F69B, 37, // 218
                0x12B404AD, 0x12B404AD, 36, // 219
                0x94F2094F, 0x94F2094F, 39, // 220
                0x25116025, 0x25116025, 37, // 221
                0x24E6A171, 0x24E6A171, 37, // 222
                0x24BC44E1, 0x24BC44E1, 37, // 223
                0x49249249, 0x49249249, 38, // 224
                0x91A2B3C5, 0x00000000, 39, // 225
                0x90FDBC09, 0x90FDBC09, 39, // 226
                0x905A3863, 0x905A3863, 39, // 227
                0x47DC11F7, 0x47DC11F7, 38, // 228
                0x478BBCED, 0x00000000, 38, // 229
                0x8E78356D, 0x8E78356D, 39, // 230
                0x46ED2901, 0x46ED2901, 38, // 231
                0x8D3DCB09, 0x00000000, 39, // 232
                0x2328A701, 0x2328A701, 37, // 233
                0x23023023, 0x23023023, 37, // 234
                0x45B81A25, 0x45B81A25, 38, // 235
                0x22B63CBF, 0x00000000, 37, // 236
                0x08A42F87, 0x08A42F87, 35, // 237
                0x44D72045, 0x00000000, 38, // 238
                0x891AC73B, 0x00000000, 39, // 239
                0x88888889, 0x00000000, 39, // 240
                0x10FEF011, 0x00000000, 36, // 241
                0x8767AB5F, 0x8767AB5F, 39, // 242
                0x86D90545, 0x00000000, 39, // 243
                0x4325C53F, 0x00000000, 38, // 244
                0x85BF3761, 0x85BF3761, 39, // 245
                0x85340853, 0x85340853, 39, // 246
                0x10953F39, 0x10953F39, 36, // 247
                0x42108421, 0x42108421, 38, // 248
                0x41CC9829, 0x41CC9829, 38, // 249
                0x10624DD3, 0x00000000, 36, // 250
                0x828CBFBF, 0x00000000, 39, // 251
                0x41041041, 0x41041041, 38, // 252
                0x81848DA9, 0x00000000, 39, // 253
                0x10204081, 0x10204081, 36, // 254
                0x80808081, 0x00000000, 39 // 255
            };

        /// <summary>
        /// The default simplification factor.
        /// </summary>
        private static int defaultSimplificationFactor = 50;

        /// <summary>
        /// The identity matrix.
        /// </summary>
        [ThreadStatic]
        private static Matrix identityMatrix = null;

        /// <summary>
        /// The last time.
        /// </summary>
        private static DateTime lastTime = DateTime.Now;

        /// <summary>
        /// The start time.
        /// </summary>
        private static DateTime startTime = DateTime.Now;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Utility"/> class from being created.
        /// </summary>
        private Utility()
        {
        }

        #endregion

        #region Enums

        /// <summary>
        /// The rectangle edge.
        /// </summary>
        private enum RectangleEdge
        {
            /// <summary>
            /// The left.
            /// </summary>
            Left, 

            /// <summary>
            /// The right.
            /// </summary>
            Right, 

            /// <summary>
            /// The top.
            /// </summary>
            Top, 

            /// <summary>
            /// The bottom.
            /// </summary>
            Bottom
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the default simplification factor.
        /// </summary>
        public static int DefaultSimplificationFactor
        {
            get
            {
                return defaultSimplificationFactor;
            }

            set
            {
                defaultSimplificationFactor = value;
            }
        }

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        public static Matrix IdentityMatrix
        {
            get
            {
                if (identityMatrix == null)
                {
                    identityMatrix = new Matrix();
                    identityMatrix.Reset();
                }

                return identityMatrix;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add vectors.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF AddVectors(PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// The bitmap to icon.
        /// </summary>
        /// <param name="bitmap">
        /// The bitmap.
        /// </param>
        /// <param name="disposeBitmap">
        /// The dispose bitmap.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon BitmapToIcon(Bitmap bitmap, bool disposeBitmap)
        {
            Icon icon = Icon.FromHandle(bitmap.GetHicon());

            if (disposeBitmap)
            {
                bitmap.Dispose();
            }

            return icon;
        }

        /// <summary>
        /// The check numeric up down.
        /// </summary>
        /// <param name="upDown">
        /// The up down.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CheckNumericUpDown(NumericUpDown upDown)
        {
            int a;
            bool result = int.TryParse(upDown.Text, out a);

            if (result && (a <= (int)upDown.Maximum) && (a >= (int)upDown.Minimum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The clamp.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double Clamp(double x, double min, double max)
        {
            if (x < min)
            {
                return min;
            }
            else if (x > max)
            {
                return max;
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// The clamp.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float Clamp(float x, float min, float max)
        {
            if (x < min)
            {
                return min;
            }
            else if (x > max)
            {
                return max;
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// The clamp.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Clamp(int x, int min, int max)
        {
            if (x < min)
            {
                return min;
            }
            else if (x > max)
            {
                return max;
            }
            else
            {
                return x;
            }
        }

        /// <summary>
        /// The clamp to byte.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public static byte ClampToByte(double x)
        {
            if (x > 255)
            {
                return 255;
            }
            else if (x < 0)
            {
                return 0;
            }
            else
            {
                return (byte)x;
            }
        }

        /// <summary>
        /// The clamp to byte.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public static byte ClampToByte(float x)
        {
            if (x > 255)
            {
                return 255;
            }
            else if (x < 0)
            {
                return 0;
            }
            else
            {
                return (byte)x;
            }
        }

        /// <summary>
        /// The clamp to byte.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        public static byte ClampToByte(int x)
        {
            if (x > 255)
            {
                return 255;
            }
            else if (x < 0)
            {
                return 0;
            }
            else
            {
                return (byte)x;
            }
        }

        /// <summary>
        /// The clip numeric up down.
        /// </summary>
        /// <param name="upDown">
        /// The up down.
        /// </param>
        public static void ClipNumericUpDown(NumericUpDown upDown)
        {
            if (upDown.Value < upDown.Minimum)
            {
                upDown.Value = upDown.Minimum;
            }
            else if (upDown.Value > upDown.Maximum)
            {
                upDown.Value = upDown.Maximum;
            }
        }

        /// <summary>
        /// The compute thumbnail size.
        /// </summary>
        /// <param name="originalSize">
        /// The original size.
        /// </param>
        /// <param name="maxEdgeLength">
        /// The max edge length.
        /// </param>
        /// <returns>
        /// The <see cref="Size"/>.
        /// </returns>
        public static Size ComputeThumbnailSize(Size originalSize, int maxEdgeLength)
        {
            Size thumbSize;

            if (originalSize.Width > originalSize.Height)
            {
                int longSide = Math.Min(originalSize.Width, maxEdgeLength);
                thumbSize = new Size(longSide, Math.Max(1, (originalSize.Height * longSide) / originalSize.Width));
            }
            else if (originalSize.Height > originalSize.Width)
            {
                int longSide = Math.Min(originalSize.Height, maxEdgeLength);
                thumbSize = new Size(Math.Max(1, (originalSize.Width * longSide) / originalSize.Height), longSide);
            }
            else
            {
                // if (docSize.Width == docSize.Height)
                int longSide = Math.Min(originalSize.Width, maxEdgeLength);
                thumbSize = new Size(longSide, longSide);
            }

            return thumbSize;
        }

        /// <summary>
        /// The copy stream.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <param name="maxBytes">
        /// The max bytes.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long CopyStream(Stream input, Stream output, long maxBytes)
        {
            long bytesCopied = 0;
            byte[] buffer = new byte[4096];

            while (true)
            {
                int bytesRead = input.Read(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                {
                    break;
                }
                else
                {
                    int bytesToCopy;

                    if (maxBytes != -1 && (bytesCopied + bytesRead) > maxBytes)
                    {
                        bytesToCopy = (int)(maxBytes - bytesCopied);
                    }
                    else
                    {
                        bytesToCopy = bytesRead;
                    }

                    output.Write(buffer, 0, bytesRead);
                    bytesCopied += bytesToCopy;

                    if (bytesToCopy != bytesRead)
                    {
                        break;
                    }
                }
            }

            return bytesCopied;
        }

        /// <summary>
        /// The copy stream.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="output">
        /// The output.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long CopyStream(Stream input, Stream output)
        {
            return CopyStream(input, output, -1);
        }

        /// <summary>
        /// The create font.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <returns>
        /// The <see cref="Font"/>.
        /// </returns>
        public static Font CreateFont(string name, float size, FontStyle style)
        {
            Font returnFont;

            try
            {
                returnFont = new Font(name, size, style);
            }
            catch
            {
                returnFont = new Font(FontFamily.GenericSansSerif, size);
            }

            return returnFont;
        }

        /// <summary>
        /// The create font.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="backupName">
        /// The backup name.
        /// </param>
        /// <param name="backupSize">
        /// The backup size.
        /// </param>
        /// <param name="style">
        /// The style.
        /// </param>
        /// <returns>
        /// The <see cref="Font"/>.
        /// </returns>
        public static Font CreateFont(string name, float size, string backupName, float backupSize, FontStyle style)
        {
            Font returnFont;

            try
            {
                returnFont = new Font(name, size, style);
            }
            catch
            {
                returnFont = CreateFont(backupName, backupSize, style);
            }

            return returnFont;
        }

        /// <summary>
        /// The deserialize object from stream.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object DeserializeObjectFromStream(Stream stream)
        {
            return new BinaryFormatter().Deserialize(stream);
        }

        /// <summary>
        /// Disposes an object for you. This function is here just to keep code a little
        /// cleaner so you don't have to test an object for null every time you want to
        /// dispose it.
        /// </summary>
        /// <param name="obj">
        /// A reference to the object to dispose.
        /// </param>
        /// <returns>
        /// true is the object was disposed, false if it wasn't (if obj was null)
        /// </returns>
        [Obsolete]
        public static bool Dispose(IDisposable obj)
        {
            if (obj != null)
            {
                obj.Dispose();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the Distance between two points
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float Distance(PointF a, PointF b)
        {
            return Magnitude(new PointF(a.X - b.X, a.Y - b.Y));
        }

        /// <summary>
        /// The does control have mouse captured.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool DoesControlHaveMouseCaptured(Control control)
        {
            bool result = false;

            result |= control.Capture;

            foreach (Control c in control.Controls)
            {
                result |= DoesControlHaveMouseCaptured(c);
            }

            return result;
        }

        /// <summary>
        /// Calculates the dot product of two vectors.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float DotProduct(PointF lhs, PointF rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        /// <summary>
        /// The draw color rectangle.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="color">
        /// The color.
        /// </param>
        /// <param name="drawBorder">
        /// The draw border.
        /// </param>
        public static void DrawColorRectangle(Graphics g, Rectangle rect, Color color, bool drawBorder)
        {
            int inflateAmt = drawBorder ? -2 : 0;
            Rectangle colorRectangle = Rectangle.Inflate(rect, inflateAmt, inflateAmt);
            Brush colorBrush = new LinearGradientBrush(colorRectangle, Color.FromArgb(255, color), color, 90.0f, false);
            HatchBrush backgroundBrush = new HatchBrush(
                HatchStyle.LargeCheckerBoard, 
                Color.FromArgb(191, 191, 191), 
                Color.FromArgb(255, 255, 255));

            if (drawBorder)
            {
                g.DrawRectangle(Pens.Black, rect.Left, rect.Top, rect.Width - 1, rect.Height - 1);
                g.DrawRectangle(Pens.White, rect.Left + 1, rect.Top + 1, rect.Width - 3, rect.Height - 3);
            }

            PixelOffsetMode oldPOM = g.PixelOffsetMode;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.FillRectangle(backgroundBrush, colorRectangle);
            g.FillRectangle(colorBrush, colorRectangle);
            g.PixelOffsetMode = oldPOM;

            backgroundBrush.Dispose();
            colorBrush.Dispose();
        }

        /// <summary>
        /// The draw drop shadow 1 px.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        public static void DrawDropShadow1px(Graphics g, Rectangle rect)
        {
            Brush b0 = new SolidBrush(Color.FromArgb(15, Color.Black));
            Brush b1 = new SolidBrush(Color.FromArgb(47, Color.Black));
            Pen p2 = new Pen(Color.FromArgb(63, Color.Black));

            g.FillRectangle(b0, rect.Left, rect.Top, 1, 1);
            g.FillRectangle(b1, rect.Left + 1, rect.Top, 1, 1);
            g.FillRectangle(b1, rect.Left, rect.Top + 1, 1, 1);

            g.FillRectangle(b0, rect.Right - 1, rect.Top, 1, 1);
            g.FillRectangle(b1, rect.Right - 2, rect.Top, 1, 1);
            g.FillRectangle(b1, rect.Right - 1, rect.Top + 1, 1, 1);

            g.FillRectangle(b0, rect.Left, rect.Bottom - 1, 1, 1);
            g.FillRectangle(b1, rect.Left + 1, rect.Bottom - 1, 1, 1);
            g.FillRectangle(b1, rect.Left, rect.Bottom - 2, 1, 1);

            g.FillRectangle(b0, rect.Right - 1, rect.Bottom - 1, 1, 1);
            g.FillRectangle(b1, rect.Right - 2, rect.Bottom - 1, 1, 1);
            g.FillRectangle(b1, rect.Right - 1, rect.Bottom - 2, 1, 1);

            g.DrawLine(p2, rect.Left + 2, rect.Top, rect.Right - 3, rect.Top);
            g.DrawLine(p2, rect.Left, rect.Top + 2, rect.Left, rect.Bottom - 3);
            g.DrawLine(p2, rect.Left + 2, rect.Bottom - 1, rect.Right - 3, rect.Bottom - 1);
            g.DrawLine(p2, rect.Right - 1, rect.Top + 2, rect.Right - 1, rect.Bottom - 3);

            b0.Dispose();
            b0 = null;
            b1.Dispose();
            b1 = null;
            p2.Dispose();
            p2 = null;
        }

        /// <summary>
        /// Downloads a small file (max 8192 bytes) and returns it as a byte array.
        /// </summary>
        /// <param name="n">
        /// The n.
        /// </param>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <returns>
        /// The contents of the file if downloaded successfully.
        /// </returns>
        /// <summary>
        /// Download a file (max 128MB) and saves it to the given Stream.
        /// </summary>
        public static int FastDivideShortByByte(ushort n, byte d)
        {
            int i = d * 3;
            uint m = masTable[i];
            uint a = masTable[i + 1];
            uint s = masTable[i + 2];

            uint nTimesMPlusA = unchecked((n * m) + a);
            uint shifted = nTimesMPlusA >> (int)s;
            int r = (int)shifted;

            return r;
        }

        /// <summary>
        /// The find focus.
        /// </summary>
        /// <returns>
        /// The <see cref="Control"/>.
        /// </returns>
        public static Control FindFocus()
        {
            foreach (Form form in Application.OpenForms)
            {
                Control focused = FindFocus(form);

                if (focused != null)
                {
                    return focused;
                }
            }

            return null;
        }

        /// <summary>
        /// The full clone bitmap.
        /// </summary>
        /// <param name="cloneMe">
        /// The clone me.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/>.
        /// </returns>
        public static Bitmap FullCloneBitmap(Bitmap cloneMe)
        {
            Bitmap bitmap = new Bitmap(cloneMe.Width, cloneMe.Height, cloneMe.PixelFormat);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(cloneMe, 0, 0, cloneMe.Width, cloneMe.Height);
            }

            return bitmap;
        }

        /// <summary>
        /// The gc full collect.
        /// </summary>
        public static void GCFullCollect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// The get angle of transform.
        /// </summary>
        /// <param name="matrix">
        /// The matrix.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float GetAngleOfTransform(Matrix matrix)
        {
            PointF[] pts = new[] { new PointF(1.0f, 0.0f) };
            matrix.TransformVectors(pts);
            double atan2 = Math.Atan2(pts[0].Y, pts[0].X);
            double angle = atan2 * (180.0f / Math.PI);

            return (float)angle;
        }

        /// <summary>
        /// The get line points.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <returns>
        /// The <see cref="Point[]"/>.
        /// </returns>
        public static Point[] GetLinePoints(Point first, Point second)
        {
            Point[] coords = null;

            int x1 = first.X;
            int y1 = first.Y;
            int x2 = second.X;
            int y2 = second.Y;
            int dx = x2 - x1;
            int dy = y2 - y1;
            int dxabs = Math.Abs(dx);
            int dyabs = Math.Abs(dy);
            int px = x1;
            int py = y1;
            int sdx = Math.Sign(dx);
            int sdy = Math.Sign(dy);
            int x = 0;
            int y = 0;

            if (dxabs > dyabs)
            {
                coords = new Point[dxabs + 1];

                for (int i = 0; i <= dxabs; i++)
                {
                    y += dyabs;

                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }

                    coords[i] = new Point(px, py);
                    px += sdx;
                }
            }
            else
                
                // had to add in this cludge for slopes of 1 ... wasn't drawing half the line
                if (dxabs == dyabs)
                {
                    coords = new Point[dxabs + 1];

                    for (int i = 0; i <= dxabs; i++)
                    {
                        coords[i] = new Point(px, py);
                        px += sdx;
                        py += sdy;
                    }
                }
                else
                {
                    coords = new Point[dyabs + 1];

                    for (int i = 0; i <= dyabs; i++)
                    {
                        x += dxabs;

                        if (x >= dyabs)
                        {
                            x -= dyabs;
                            px += sdx;
                        }

                        coords[i] = new Point(px, py);
                        py += sdy;
                    }
                }

            return coords;
        }

        /// <summary>
        /// Calculates the orthogonal projection of y on to u.
        /// yhat = u * ((y dot u) / (u dot u))
        /// z = y - yhat
        /// Section 6.2 (pg. 381) of Linear Algebra and its Applications, Second Edition, by David C. Lay
        /// </summary>
        /// <param name="y">
        /// The vector to decompose
        /// </param>
        /// <param name="u">
        /// The non-zero vector to project y on to
        /// </param>
        /// <param name="yhat">
        /// The orthogonal projection of y onto u
        /// </param>
        /// <param name="yhatLen">
        /// The length of yhat such that yhat = yhatLen * u
        /// </param>
        /// <param name="z">
        /// The component of y orthogonal to u
        /// </param>
        /// <remarks>
        /// As a special case, if u=(0,0) the results are all zero.
        /// </remarks>
        public static void GetProjection(PointF y, PointF u, out PointF yhat, out float yhatLen, out PointF z)
        {
            if (u.X == 0 && u.Y == 0)
            {
                yhat = new PointF(0, 0);
                yhatLen = 0;
                z = new PointF(0, 0);
            }
            else
            {
                float yDotU = DotProduct(y, u);
                float uDotU = DotProduct(u, u);
                yhatLen = yDotU / uDotU;
                yhat = MultiplyVector(u, yhatLen);
                z = SubtractVectors(y, yhat);
            }
        }

        // public static Icon SurfaceToIcon(Surface surface, bool disposeSurface)
        // {
        // Bitmap bitmap = surface.CreateAliasedBitmap();
        // Icon icon = Icon.FromHandle(bitmap.GetHicon());

        // bitmap.Dispose();

        // if (disposeSurface)
        // {
        // surface.Dispose();
        // }

        // return icon;
        // }

        /// <summary>
        /// The get rectangle center.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        public static Point GetRectangleCenter(Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        /// <summary>
        /// The get rectangle center.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF GetRectangleCenter(RectangleF rect)
        {
            return new PointF((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        // public static Scanline[] GetRectangleScans(Rectangle rect)
        // {
        // Scanline[] scans = new Scanline[rect.Height];

        // for (int y = 0; y < rect.Height; ++y)
        // {
        // scans[y] = new Scanline(rect.X, rect.Y + y, rect.Width);
        // }

        // return scans;
        // }

        // public static Scanline[] GetRegionScans(Rectangle[] region)
        // {
        // int scanCount = 0;

        // for (int i = 0; i < region.Length; ++i)
        // {
        // scanCount += region[i].Height;
        // }

        // Scanline[] scans = new Scanline[scanCount];
        // int scanIndex = 0;

        // foreach (Rectangle rect in region)
        // {
        // for (int y = 0; y < rect.Height; ++y)
        // {
        // scans[scanIndex] = new Scanline(rect.X, rect.Y + y, rect.Width);
        // ++scanIndex;
        // }
        // }

        // return scans;
        // }

        // public static Rectangle[] ScanlinesToRectangles(Scanline[] scans)
        // {
        // return ScanlinesToRectangles(scans, 0, scans.Length);
        // }

        // public static Rectangle[] ScanlinesToRectangles(Scanline[] scans, int startIndex, int length)
        // {
        // Rectangle[] rects = new Rectangle[length];

        // for (int i = 0; i < length; ++i)
        // {
        // Scanline scan = scans[i + startIndex];
        // rects[i] = new Rectangle(scan.X, scan.Y, scan.Length, 1);
        // }

        // return rects;
        // }

        /// <summary>
        /// Found on Google Groups when searching for "Region.Union" while looking
        /// for bugs:
        /// ---
        /// Hello,
        /// 
        /// I did not run your code, but I know Region.Union is flawed in both 1.0 and
        /// 1.1, so I assume it is in the gdi+ unmanged code dll.  The best workaround,
        /// in terms of speed, is to use a PdnGraphicsPath, but it must be a path with
        /// FillMode = FillMode.Winding. You add the rectangles to the path, then you do
        /// union onto an empty region with the path. The important point is to do only
        /// one union call on a given empty region. We created a "super region" object
        /// to hide all these bugs and optimize clipping operations. In fact, it is much
        /// faster to use the path than to call Region.Union for each rectangle.
        /// 
        /// Too bad about Region.Union. A lot of people will hit this bug, as it is
        /// essential in high-performance animation.
        /// 
        /// Regards,
        /// Frank Hileman
        /// Prodige Software Corporation
        /// ---
        /// </summary>
        /// <param name="rectsF">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetRegionArea(RectangleF[] rectsF)
        {
            int area = 0;

            foreach (RectangleF rectF in rectsF)
            {
                Rectangle rect = Rectangle.Truncate(rectF);
                area += rect.Width * rect.Height;
            }

            return area;
        }

        /// <summary>
        /// Allows you to find the bounding box for a Region object without requiring
        /// the presence of a Graphics object.
        /// (Region.GetBounds takes a Graphics instance as its only parameter.)
        /// </summary>
        /// <returns>
        /// A RectangleF structure that surrounds the Region.
        /// </returns>
        /// <summary>
        /// Allows you to find the bounding box for a "region" that is described as an
        /// array of bounding boxes.
        /// </summary>
        /// <param name="rectsF">
        /// The "region" you want to find a bounding box for.
        /// </param>
        /// <param name="startIndex">
        /// The start Index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// A RectangleF structure that surrounds the Region.
        /// </returns>
        public static RectangleF GetRegionBounds(RectangleF[] rectsF, int startIndex, int length)
        {
            if (rectsF.Length == 0)
            {
                return RectangleF.Empty;
            }

            float left = rectsF[startIndex].Left;
            float top = rectsF[startIndex].Top;
            float right = rectsF[startIndex].Right;
            float bottom = rectsF[startIndex].Bottom;

            for (int i = startIndex + 1; i < startIndex + length; ++i)
            {
                RectangleF rectF = rectsF[i];

                if (rectF.Left < left)
                {
                    left = rectF.Left;
                }

                if (rectF.Top < top)
                {
                    top = rectF.Top;
                }

                if (rectF.Right > right)
                {
                    right = rectF.Right;
                }

                if (rectF.Bottom > bottom)
                {
                    bottom = rectF.Bottom;
                }
            }

            return RectangleF.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// Allows you to find the bounding box for a "region" that is described as an
        /// array of bounding boxes.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="startIndex">
        /// The start Index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// A RectangleF structure that surrounds the Region.
        /// </returns>
        public static Rectangle GetRegionBounds(Rectangle[] rects, int startIndex, int length)
        {
            if (rects.Length == 0)
            {
                return Rectangle.Empty;
            }

            int left = rects[startIndex].Left;
            int top = rects[startIndex].Top;
            int right = rects[startIndex].Right;
            int bottom = rects[startIndex].Bottom;

            for (int i = startIndex + 1; i < startIndex + length; ++i)
            {
                Rectangle rect = rects[i];

                if (rect.Left < left)
                {
                    left = rect.Left;
                }

                if (rect.Top < top)
                {
                    top = rect.Top;
                }

                if (rect.Right > right)
                {
                    right = rect.Right;
                }

                if (rect.Bottom > bottom)
                {
                    bottom = rect.Bottom;
                }
            }

            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// The get region bounds.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF GetRegionBounds(RectangleF[] rectsF)
        {
            return GetRegionBounds(rectsF, 0, rectsF.Length);
        }

        /// <summary>
        /// The get region bounds.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle GetRegionBounds(Rectangle[] rects)
        {
            return GetRegionBounds(rects, 0, rects.Length);
        }

        /// <summary>
        /// The get static name.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetStaticName(Type type)
        {
            PropertyInfo pi = type.GetProperty(
                "StaticName", 
                BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
            return (string)pi.GetValue(null, null);
        }

        /// <summary>
        /// The get time ms.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetTimeMs()
        {
            return Utility.TicksToMs(DateTime.Now.Ticks);
        }

        /// <summary>
        /// The get trace bounds.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF GetTraceBounds(PointF[] pointsF, int startIndex, int length)
        {
            if (pointsF.Length == 0)
            {
                return RectangleF.Empty;
            }

            float left = pointsF[startIndex].X;
            float top = pointsF[startIndex].Y;
            float right = 1 + pointsF[startIndex].X;
            float bottom = 1 + pointsF[startIndex].Y;

            for (int i = startIndex + 1; i < startIndex + length; ++i)
            {
                PointF pointF = pointsF[i];

                if (pointF.X < left)
                {
                    left = pointF.X;
                }

                if (pointF.Y < top)
                {
                    top = pointF.Y;
                }

                if (pointF.X > right)
                {
                    right = pointF.X;
                }

                if (pointF.Y > bottom)
                {
                    bottom = pointF.Y;
                }
            }

            return RectangleF.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// The get trace bounds.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle GetTraceBounds(Point[] points, int startIndex, int length)
        {
            if (points.Length == 0)
            {
                return Rectangle.Empty;
            }

            int left = points[startIndex].X;
            int top = points[startIndex].Y;
            int right = 1 + points[startIndex].X;
            int bottom = 1 + points[startIndex].Y;

            for (int i = startIndex + 1; i < startIndex + length; ++i)
            {
                Point point = points[i];

                if (point.X < left)
                {
                    left = point.X;
                }

                if (point.Y < top)
                {
                    top = point.Y;
                }

                if (point.X > right)
                {
                    right = point.X;
                }

                if (point.Y > bottom)
                {
                    bottom = point.Y;
                }
            }

            return Rectangle.FromLTRB(left, top, right, bottom);
        }

        /// <summary>
        /// The get up down value from text.
        /// </summary>
        /// <param name="nud">
        /// The nud.
        /// </param>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetUpDownValueFromText(NumericUpDown nud, out double val)
        {
            if (nud.Text == string.Empty)
            {
                val = 0;
                return false;
            }
            else
            {
                try
                {
                    if (nud.DecimalPlaces == 0)
                    {
                        val = (double)int.Parse(nud.Text);
                    }
                    else
                    {
                        val = double.Parse(nud.Text);
                    }
                }
                catch
                {
                    val = 0;
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// The greatest common divisor.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GreatestCommonDivisor(int a, int b)
        {
            int r;

            if (a < b)
            {
                r = a;
                a = b;
                b = r;
            }

            do
            {
                r = a % b;
                a = b;
                b = r;
            }
            while (r != 0);

            return a;
        }

        /// <summary>
        /// Converts an Image to an Icon.
        /// </summary>
        /// <param name="image">
        /// The Image to convert to an icon. Must be an appropriate icon size (32x32, 16x16, etc).
        /// </param>
        /// <param name="seeThru">
        /// The color that will be treated as transparent in the icon.
        /// </param>
        /// <param name="disposeImage">
        /// Whether or not to dispose the passed-in Image.
        /// </param>
        /// <returns>
        /// An Icon representation of the Image.
        /// </returns>
        public static Icon ImageToIcon(Image image, Color seeThru, bool disposeImage)
        {
            Bitmap bitmap = new Bitmap(image);

            for (int y = 0; y < bitmap.Height; ++y)
            {
                for (int x = 0; x < bitmap.Width; ++x)
                {
                    if (bitmap.GetPixel(x, y) == seeThru)
                    {
                        bitmap.SetPixel(x, y, Color.FromArgb(0));
                    }
                }
            }

            Icon icon = Icon.FromHandle(bitmap.GetHicon());
            bitmap.Dispose();

            if (disposeImage)
            {
                image.Dispose();
            }

            return icon;
        }

        /// <summary>
        /// The inflate rectangles.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] InflateRectangles(Rectangle[] rects, int amount)
        {
            Rectangle[] inflated = new Rectangle[rects.Length];

            for (int i = 0; i < rects.Length; ++i)
            {
                inflated[i] = Rectangle.Inflate(rects[i], amount, amount);
            }

            return inflated;
        }

        /// <summary>
        /// The inflate rectangles.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF[]"/>.
        /// </returns>
        public static RectangleF[] InflateRectangles(RectangleF[] rectsF, int amount)
        {
            RectangleF[] inflated = new RectangleF[rectsF.Length];

            for (int i = 0; i < rectsF.Length; ++i)
            {
                inflated[i] = RectangleF.Inflate(rectsF[i], amount, amount);
            }

            return inflated;
        }

        /// <summary>
        /// The inflate rectangles in place.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        public static void InflateRectanglesInPlace(Rectangle[] rects, int amount)
        {
            for (int i = 0; i < rects.Length; ++i)
            {
                rects[i].Inflate(amount, amount);
            }
        }

        /// <summary>
        /// The inflate rectangles in place.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        public static void InflateRectanglesInPlace(RectangleF[] rectsF, float amount)
        {
            for (int i = 0; i < rectsF.Length; ++i)
            {
                rectsF[i].Inflate(amount, amount);
            }
        }

        /// <summary>
        /// The is arrow key.
        /// </summary>
        /// <param name="keyData">
        /// The key data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsArrowKey(Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (key == Keys.Up || key == Keys.Down || key == Keys.Left || key == Keys.Right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The is clipboard image available.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsClipboardImageAvailable()
        {
            try
            {
                return Clipboard.ContainsImage();
            }
            catch (ExternalException)
            {
                return false;
            }
        }

        /// <summary>
        /// The is point in rectangle.
        /// </summary>
        /// <param name="pt">
        /// The pt.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [Obsolete("Use rect.Contains() instead", true)]
        public static bool IsPointInRectangle(Point pt, Rectangle rect)
        {
            return rect.Contains(pt);
        }

        /// <summary>
        /// The is point in rectangle.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [Obsolete("Use rect.Contains() instead", true)]
        public static bool IsPointInRectangle(int x, int y, Rectangle rect)
        {
            return rect.Contains(x, y);
        }

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <param name="frac">
        /// The frac.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float Lerp(float from, float to, float frac)
        {
            return from + frac * (to - from);
        }

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <param name="frac">
        /// The frac.
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public static double Lerp(double from, double to, double frac)
        {
            return from + frac * (to - from);
        }

        /// <summary>
        /// The lerp.
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="to">
        /// The to.
        /// </param>
        /// <param name="frac">
        /// The frac.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF Lerp(PointF from, PointF to, float frac)
        {
            return new PointF(Lerp(from.X, to.X, frac), Lerp(from.Y, to.Y, frac));
        }

        /// <summary>
        /// The letter or digit char to keys.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="Keys"/>.
        /// </returns>
        public static Keys LetterOrDigitCharToKeys(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return (Keys)((int)(c - 'a') + (int)Keys.A);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                return (Keys)((int)(c - 'A') + (int)Keys.A);
            }
            else if (c >= '0' && c <= '9')
            {
                return (Keys)((int)(c - '0') + (int)Keys.D0);
            }
            else
            {
                return Keys.None;
            }
        }

        /// <summary>
        /// Rounds an integer to the smallest power of 2 that is greater
        /// than or equal to it.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Log2RoundUp(int x)
        {
            if (x == 0)
            {
                return 1;
            }

            if (x == 1)
            {
                return 1;
            }

            return 1 << (1 + HighestBit(x - 1));
        }

        /// <summary>
        /// Returns the Magnitude (distance to origin) of a point
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        public static float Magnitude(PointF p)
        {
            return (float)Math.Sqrt(p.X * p.X + p.Y * p.Y);
        }

        /// <summary>
        /// The max.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Max(int[,] array)
        {
            int max = int.MinValue;

            for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); ++i)
            {
                for (int j = array.GetLowerBound(1); j <= array.GetUpperBound(1); ++j)
                {
                    if (array[i, j] > max)
                    {
                        max = array[i, j];
                    }
                }
            }

            return max;
        }

        /// <summary>
        /// The multiply vector.
        /// </summary>
        /// <param name="vecF">
        /// The vec f.
        /// </param>
        /// <param name="scalar">
        /// The scalar.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF MultiplyVector(PointF vecF, float scalar)
        {
            return new PointF(vecF.X * scalar, vecF.Y * scalar);
        }

        /// <summary>
        /// The negate vector.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF NegateVector(PointF v)
        {
            return new PointF(-v.X, -v.Y);
        }

        /// <summary>
        /// The point array to point f array.
        /// </summary>
        /// <param name="ptArray">
        /// The pt array.
        /// </param>
        /// <returns>
        /// The <see cref="PointF[]"/>.
        /// </returns>
        public static PointF[] PointArrayToPointFArray(Point[] ptArray)
        {
            PointF[] ret = new PointF[ptArray.Length];

            for (int i = 0; i < ret.Length; ++i)
            {
                ret[i] = (PointF)ptArray[i];
            }

            return ret;
        }

        /// <summary>
        /// The point list to point f list.
        /// </summary>
        /// <param name="ptList">
        /// The pt list.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<PointF> PointListToPointFList(List<Point> ptList)
        {
            List<PointF> ret = new List<PointF>(ptList.Count);

            for (int i = 0; i < ptList.Count; ++i)
            {
                ret.Add((PointF)ptList[i]);
            }

            return ret;
        }

        /// <summary>
        /// The points to constrained rectangle.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle PointsToConstrainedRectangle(Point a, Point b)
        {
            Rectangle rect = Utility.PointsToRectangle(a, b);
            int minWH = Math.Min(rect.Width, rect.Height);

            rect.Width = minWH;
            rect.Height = minWH;

            if (rect.Y != a.Y)
            {
                rect.Location = new Point(rect.X, a.Y - minWH);
            }

            if (rect.X != a.X)
            {
                rect.Location = new Point(a.X - minWH, rect.Y);
            }

            return rect;
        }

        /// <summary>
        /// The points to constrained rectangle.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF PointsToConstrainedRectangle(PointF a, PointF b)
        {
            RectangleF rect = Utility.PointsToRectangle(a, b);
            float minWH = Math.Min(rect.Width, rect.Height);

            rect.Width = minWH;
            rect.Height = minWH;

            if (rect.Y != a.Y)
            {
                rect.Location = new PointF(rect.X, a.Y - minWH);
            }

            if (rect.X != a.X)
            {
                rect.Location = new PointF(a.X - minWH, rect.Y);
            }

            return rect;
        }

        /// <summary>
        /// Takes two points and creates a bounding rectangle from them.
        /// </summary>
        /// <param name="a">
        /// One corner of the rectangle.
        /// </param>
        /// <param name="b">
        /// The other corner of the rectangle.
        /// </param>
        /// <returns>
        /// A Rectangle instance that bounds the two points.
        /// </returns>
        public static Rectangle PointsToRectangle(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X) + 1;
            int height = Math.Abs(a.Y - b.Y) + 1;

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// The points to rectangle.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF PointsToRectangle(PointF a, PointF b)
        {
            float x = Math.Min(a.X, b.X);
            float y = Math.Min(a.Y, b.Y);
            float width = Math.Abs(a.X - b.X) + 1;
            float height = Math.Abs(a.Y - b.Y) + 1;

            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// The points to rectangle exclusive.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle PointsToRectangleExclusive(Point a, Point b)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int width = Math.Abs(a.X - b.X);
            int height = Math.Abs(a.Y - b.Y);

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// The points to rectangle exclusive.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF PointsToRectangleExclusive(PointF a, PointF b)
        {
            float x = Math.Min(a.X, b.X);
            float y = Math.Min(a.Y, b.Y);
            float width = Math.Abs(a.X - b.X);
            float height = Math.Abs(a.Y - b.Y);

            return new RectangleF(x, y, width, height);
        }

        /// <summary>
        /// The points to rectangles.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF[]"/>.
        /// </returns>
        public static RectangleF[] PointsToRectangles(PointF[] pointsF)
        {
            if (pointsF.Length == 0)
            {
                return new RectangleF[] { };
            }

            if (pointsF.Length == 1)
            {
                return new[] { new RectangleF(pointsF[0].X, pointsF[0].Y, 1, 1) };
            }

            RectangleF[] rectsF = new RectangleF[pointsF.Length - 1];

            for (int i = 0; i < pointsF.Length - 1; ++i)
            {
                rectsF[i] = PointsToRectangle(pointsF[i], pointsF[i + 1]);
            }

            return rectsF;
        }

        /// <summary>
        /// The points to rectangles.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] PointsToRectangles(Point[] points)
        {
            if (points.Length == 0)
            {
                return new Rectangle[] { };
            }

            if (points.Length == 1)
            {
                return new[] { new Rectangle(points[0].X, points[0].Y, 1, 1) };
            }

            Rectangle[] rects = new Rectangle[points.Length - 1];

            for (int i = 0; i < points.Length - 1; ++i)
            {
                rects[i] = PointsToRectangle(points[i], points[i + 1]);
            }

            return rects;
        }

        /// <summary>
        /// The read from stream.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="IOException">
        /// </exception>
        public static int ReadFromStream(Stream input, byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = input.Read(buffer, offset + totalBytesRead, count - totalBytesRead);

                if (bytesRead == 0)
                {
                    throw new IOException("ran out of data");
                }

                totalBytesRead += bytesRead;
            }

            return totalBytesRead;
        }

        /// <summary>
        /// Reads a 16-bit unsigned integer from a Stream in little-endian format.
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <returns>
        /// -1 on failure, else the 16-bit unsigned integer that was read.
        /// </returns>
        public static int ReadUInt16(Stream stream)
        {
            int byte1 = stream.ReadByte();

            if (byte1 == -1)
            {
                return -1;
            }

            int byte2 = stream.ReadByte();

            if (byte2 == -1)
            {
                return -1;
            }

            return byte1 + (byte2 << 8);
        }

        /// <summary>
        /// Reads a 24-bit unsigned integer from a Stream in little-endian format.
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <returns>
        /// -1 on failure, else the 24-bit unsigned integer that was read.
        /// </returns>
        public static int ReadUInt24(Stream stream)
        {
            int byte1 = stream.ReadByte();

            if (byte1 == -1)
            {
                return -1;
            }

            int byte2 = stream.ReadByte();

            if (byte2 == -1)
            {
                return -1;
            }

            int byte3 = stream.ReadByte();

            if (byte3 == -1)
            {
                return -1;
            }

            return byte1 + (byte2 << 8) + (byte3 << 16);
        }

        /// <summary>
        /// Reads a 32-bit unsigned integer from a Stream in little-endian format.
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <returns>
        /// -1 on failure, else the 32-bit unsigned integer that was read.
        /// </returns>
        public static long ReadUInt32(Stream stream)
        {
            int byte1 = stream.ReadByte();

            if (byte1 == -1)
            {
                return -1;
            }

            int byte2 = stream.ReadByte();

            if (byte2 == -1)
            {
                return -1;
            }

            int byte3 = stream.ReadByte();

            if (byte3 == -1)
            {
                return -1;
            }

            int byte4 = stream.ReadByte();

            if (byte4 == -1)
            {
                return -1;
            }

            return unchecked((long)((uint)(byte1 + (byte2 << 8) + (byte3 << 16) + (byte4 << 24))));
        }

        /// <summary>
        /// The rectangle from center.
        /// </summary>
        /// <param name="center">
        /// The center.
        /// </param>
        /// <param name="halfSize">
        /// The half size.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF"/>.
        /// </returns>
        public static RectangleF RectangleFromCenter(PointF center, float halfSize)
        {
            RectangleF ret = new RectangleF(center.X, center.Y, 0, 0);
            ret.Inflate(halfSize, halfSize);
            return ret;
        }

        /// <summary>
        /// The remove spaces.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RemoveSpaces(string s)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// The repeat array.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="repeatCount">
        /// The repeat count.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T[]"/>.
        /// </returns>
        public static T[] RepeatArray<T>(T[] array, int repeatCount)
        {
            T[] returnArray = new T[repeatCount * array.Length];

            for (int i = 0; i < repeatCount; ++i)
            {
                for (int j = 0; j < array.Length; ++j)
                {
                    int index = (i * array.Length) + j;
                    returnArray[index] = array[j];
                }
            }

            return returnArray;
        }

        /// <summary>
        /// The reverse.
        /// </summary>
        /// <param name="reverseMe">
        /// The reverse me.
        /// </param>
        /// <returns>
        /// The <see cref="Stack"/>.
        /// </returns>
        public static Stack Reverse(Stack reverseMe)
        {
            Stack reversed = new Stack();

            foreach (object o in reverseMe)
            {
                reversed.Push(o);
            }

            return reversed;
        }

        /// <summary>
        /// The rotate vector.
        /// </summary>
        /// <param name="vecF">
        /// The vec f.
        /// </param>
        /// <param name="angleDelta">
        /// The angle delta.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF RotateVector(PointF vecF, float angleDelta)
        {
            angleDelta *= (float)(Math.PI / 180.0);
            float vecFLen = Magnitude(vecF);
            float vecFAngle = angleDelta + (float)Math.Atan2(vecF.Y, vecF.X);
            vecF.X = (float)Math.Cos(vecFAngle);
            vecF.Y = (float)Math.Sin(vecFAngle);
            return vecF;
        }

        /// <summary>
        /// The rotate vectors.
        /// </summary>
        /// <param name="vecFs">
        /// The vec fs.
        /// </param>
        /// <param name="angleDelta">
        /// The angle delta.
        /// </param>
        public static void RotateVectors(PointF[] vecFs, float angleDelta)
        {
            for (int i = 0; i < vecFs.Length; ++i)
            {
                vecFs[i] = RotateVector(vecFs[i], angleDelta);
            }
        }

        /// <summary>
        /// The round points.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <returns>
        /// The <see cref="Point[]"/>.
        /// </returns>
        public static Point[] RoundPoints(PointF[] pointsF)
        {
            Point[] points = new Point[pointsF.Length];

            for (int i = 0; i < pointsF.Length; ++i)
            {
                points[i] = Point.Round(pointsF[i]);
            }

            return points;
        }

        /// <summary>
        /// Converts a RectangleF to RectangleF by rounding down the Location and rounding
        /// up the Size.
        /// </summary>
        /// <param name="rectF">
        /// The rect F.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle"/>.
        /// </returns>
        public static Rectangle RoundRectangle(RectangleF rectF)
        {
            float left = (float)Math.Floor(rectF.Left);
            float top = (float)Math.Floor(rectF.Top);
            float right = (float)Math.Ceiling(rectF.Right);
            float bottom = (float)Math.Ceiling(rectF.Bottom);

            return Rectangle.Truncate(RectangleF.FromLTRB(left, top, right, bottom));
        }

        /// <summary>
        /// The serialize object to stream.
        /// </summary>
        /// <param name="graph">
        /// The graph.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public static void SerializeObjectToStream(object graph, Stream stream)
        {
            new BinaryFormatter().Serialize(stream, graph);
        }

        /// <summary>
        /// The set numeric up down value.
        /// </summary>
        /// <param name="upDown">
        /// The up down.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        public static void SetNumericUpDownValue(NumericUpDown upDown, decimal newValue)
        {
            if (upDown.Value != newValue)
            {
                upDown.Value = newValue;
            }
        }

        /// <summary>
        /// The set numeric up down value.
        /// </summary>
        /// <param name="upDown">
        /// The up down.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>
        public static void SetNumericUpDownValue(NumericUpDown upDown, int newValue)
        {
            SetNumericUpDownValue(upDown, (decimal)newValue);
        }

        /// <summary>
        /// The show help.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public static void ShowHelp(Control parent)
        {
            string helpFileUrlFormat = Resources.GetString("HelpFile.Url.Format");
            string baseSiteUrl = InvariantStrings.WebsiteUrl;
            string helpFileUrl = string.Format(helpFileUrlFormat, baseSiteUrl);
            Info.OpenUrl(parent, helpFileUrl);
        }

        /// <summary>
        /// The simplify and inflate region.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="complexity">
        /// The complexity.
        /// </param>
        /// <param name="inflationAmount">
        /// The inflation amount.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] SimplifyAndInflateRegion(Rectangle[] rects, int complexity, int inflationAmount)
        {
            Rectangle[] simplified = SimplifyRegion(rects, complexity);

            for (int i = 0; i < simplified.Length; ++i)
            {
                simplified[i].Inflate(inflationAmount, inflationAmount);
            }

            return simplified;
        }

        /// <summary>
        /// The simplify and inflate region.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] SimplifyAndInflateRegion(Rectangle[] rects)
        {
            return SimplifyAndInflateRegion(rects, defaultSimplificationFactor, 1);
        }

        /// <summary>
        /// Simplifies a Region into N number of bounding boxes.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="complexity">
        /// The maximum number of bounding boxes to return, or 0 for however many are necessary (equivalent to using Region.GetRegionScans).
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] SimplifyRegion(Rectangle[] rects, int complexity)
        {
            if (complexity == 0 || rects.Length < complexity)
            {
                return (Rectangle[])rects.Clone();
            }

            Rectangle[] boxes = new Rectangle[complexity];

            for (int i = 0; i < complexity; ++i)
            {
                int startIndex = (i * rects.Length) / complexity;
                int length = Math.Min(rects.Length, ((i + 1) * rects.Length) / complexity) - startIndex;
                boxes[i] = GetRegionBounds(rects, startIndex, length);
            }

            return boxes;
        }

        /// <summary>
        /// The simplify trace.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <param name="complexity">
        /// The complexity.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF[]"/>.
        /// </returns>
        public static RectangleF[] SimplifyTrace(PointF[] pointsF, int complexity)
        {
            if (complexity == 0 || (pointsF.Length - 1) < complexity)
            {
                return PointsToRectangles(pointsF);
            }

            RectangleF[] boxes = new RectangleF[complexity];
            int parLength = pointsF.Length - 1; // "(points as Rectangles).Length"

            for (int i = 0; i < complexity; ++i)
            {
                int startIndex = (i * parLength) / complexity;
                int length = Math.Min(parLength, ((i + 1) * parLength) / complexity) - startIndex;
                boxes[i] = GetTraceBounds(pointsF, startIndex, length + 1);
            }

            return boxes;
        }

        // public static Rectangle[] SimplifyTrace(PdnGraphicsPath trace, int complexity)
        // {
        // return SimplifyRegion(TraceToRectangles(trace), complexity);
        // }

        // public static Rectangle[] SimplifyTrace(PdnGraphicsPath trace)
        // {
        // return SimplifyTrace(trace, DefaultSimplificationFactor);
        // }

        // public static Rectangle[] TraceToRectangles(PdnGraphicsPath trace, int complexity)
        // {
        // int pointCount = trace.PointCount;

        // if (pointCount == 0)
        // {
        // return new Rectangle[0];
        // }

        // PointF[] pathPoints = trace.PathPoints;
        // byte[] pathTypes = trace.PathTypes;
        // int figureStart = 0;

        // // first get count of rectangles we'll need
        // Rectangle[] rects = new Rectangle[pointCount];

        // for (int i = 0; i < pointCount; ++i)
        // {
        // byte type = pathTypes[i];

        // Point a = Point.Truncate(pathPoints[i]);
        // Point b;

        // if ((type & (byte)PathPointType.CloseSubpath) != 0)
        // {
        // b = Point.Truncate(pathPoints[figureStart]);
        // figureStart = i + 1;
        // }
        // else
        // {
        // b = Point.Truncate(pathPoints[i + 1]);
        // }

        // rects[i] = Utility.PointsToRectangle(a, b);
        // }

        // return rects;
        // }

        // public static Rectangle[] TraceToRectangles(PdnGraphicsPath trace)
        // {
        // return TraceToRectangles(trace, DefaultSimplificationFactor);
        // }

        /// <summary>
        /// The simplify trace.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF[]"/>.
        /// </returns>
        public static RectangleF[] SimplifyTrace(PointF[] pointsF)
        {
            return SimplifyTrace(pointsF, defaultSimplificationFactor);
        }

        /// <summary>
        /// The size string from bytes.
        /// </summary>
        /// <param name="bytes">
        /// The bytes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string SizeStringFromBytes(long bytes)
        {
            string returnMe;
            double bytesDouble = (double)bytes;

            if (bytesDouble > (1024 * 1024 * 1024))
            {
                // Gigs
                bytesDouble /= 1024 * 1024 * 1024;
                returnMe = bytesDouble.ToString("F1") + " GB";
            }
            else if (bytesDouble > (1024 * 1024))
            {
                // Megs
                bytesDouble /= 1024 * 1024;
                returnMe = bytesDouble.ToString("F1") + " MB";
            }
            else if (bytesDouble > 1024)
            {
                // K
                bytesDouble /= 1024;
                returnMe = bytesDouble.ToString("F1") + " KB";
            }
            else
            {
                // Bytes
                returnMe = bytesDouble.ToString("F0") + " Bytes";
            }

            return returnMe;
        }

        /// <summary>
        /// The split rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="rects">
        /// The rects.
        /// </param>
        public static void SplitRectangle(Rectangle rect, Rectangle[] rects)
        {
            int height = rect.Height;

            for (int i = 0; i < rects.Length; ++i)
            {
                Rectangle newRect = Rectangle.FromLTRB(
                    rect.Left, 
                    rect.Top + ((height * i) / rects.Length), 
                    rect.Right, 
                    rect.Top + ((height * (i + 1)) / rects.Length));

                rects[i] = newRect;
            }
        }

        /// <summary>
        /// The subtract vectors.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public static PointF SubtractVectors(PointF lhs, PointF rhs)
        {
            return new PointF(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        /// <summary>
        /// The sum.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Sum(int[][] array)
        {
            int sum = 0;

            for (int i = 0; i < array.Length; ++i)
            {
                int[] row = array[i];

                for (int j = 0; j < row.Length; ++j)
                {
                    sum += row[j];
                }
            }

            return sum;
        }

        /// <summary>
        /// The Sutherland-Hodgman clipping alrogithm.
        /// http://ezekiel.vancouver.wsu.edu/~cs442/lectures/clip/clip/index.html
        /// 
        /// # Clipping a convex polygon to a convex region (e.g., rectangle) will always produce a convex polygon (or no polygon if completely outside the clipping region).
        /// # Clipping a concave polygon to a rectangle may produce several polygons (see figure above) or, as the following algorithm does, produce a single, possibly degenerate, polygon.
        /// # Divide and conquer: Clip entire polygon against a single edge (i.e., half-plane). Repeat for each edge in the clipping region.
        /// 
        /// The input is a sequence of vertices: {v0, v1, ... vn} given as an array of Points
        /// the result is a sequence of vertices, given as an array of Points. This result may have
        /// less than, equal, more than, or 0 vertices.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<PointF> SutherlandHodgman(RectangleF bounds, List<PointF> v)
        {
            List<PointF> p1 = SutherlandHodgmanOneAxis(bounds, RectangleEdge.Left, v);
            List<PointF> p2 = SutherlandHodgmanOneAxis(bounds, RectangleEdge.Right, p1);
            List<PointF> p3 = SutherlandHodgmanOneAxis(bounds, RectangleEdge.Top, p2);
            List<PointF> p4 = SutherlandHodgmanOneAxis(bounds, RectangleEdge.Bottom, p3);

            return p4;
        }

        /// <summary>
        /// The swap.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        public static void Swap(ref int a, ref int b)
        {
            int t;

            t = a;
            a = b;
            b = t;
        }

        /// <summary>
        /// The swap.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Swap<T>(ref T a, ref T b)
        {
            T t;

            t = a;
            a = b;
            b = t;
        }

        /// <summary>
        /// The ticks to ms.
        /// </summary>
        /// <param name="ticks">
        /// The ticks.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long TicksToMs(long ticks)
        {
            return ticks / 10000;
        }

        /// <summary>
        /// The translate points in place.
        /// </summary>
        /// <param name="ptsF">
        /// The pts f.
        /// </param>
        /// <param name="dx">
        /// The dx.
        /// </param>
        /// <param name="dy">
        /// The dy.
        /// </param>
        public static void TranslatePointsInPlace(PointF[] ptsF, float dx, float dy)
        {
            for (int i = 0; i < ptsF.Length; ++i)
            {
                ptsF[i].X += dx;
                ptsF[i].Y += dy;
            }
        }

        /// <summary>
        /// The translate points in place.
        /// </summary>
        /// <param name="pts">
        /// The pts.
        /// </param>
        /// <param name="dx">
        /// The dx.
        /// </param>
        /// <param name="dy">
        /// The dy.
        /// </param>
        public static void TranslatePointsInPlace(Point[] pts, int dx, int dy)
        {
            for (int i = 0; i < pts.Length; ++i)
            {
                pts[i].X += dx;
                pts[i].Y += dy;
            }
        }

        /// <summary>
        /// The translate rectangles.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <returns>
        /// The <see cref="RectangleF[]"/>.
        /// </returns>
        public static RectangleF[] TranslateRectangles(RectangleF[] rectsF, PointF offset)
        {
            RectangleF[] retRectsF = new RectangleF[rectsF.Length];
            int i = 0;

            foreach (RectangleF rectF in rectsF)
            {
                retRectsF[i] = new RectangleF(rectF.X + offset.X, rectF.Y + offset.Y, rectF.Width, rectF.Height);
                ++i;
            }

            return retRectsF;
        }

        /// <summary>
        /// The translate rectangles.
        /// </summary>
        /// <param name="rects">
        /// The rects.
        /// </param>
        /// <param name="dx">
        /// The dx.
        /// </param>
        /// <param name="dy">
        /// The dy.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] TranslateRectangles(Rectangle[] rects, int dx, int dy)
        {
            Rectangle[] retRects = new Rectangle[rects.Length];

            for (int i = 0; i < rects.Length; ++i)
            {
                retRects[i] = new Rectangle(rects[i].X + dx, rects[i].Y + dy, rects[i].Width, rects[i].Height);
            }

            return retRects;
        }

        /// <summary>
        /// The truncate points.
        /// </summary>
        /// <param name="pointsF">
        /// The points f.
        /// </param>
        /// <returns>
        /// The <see cref="Point[]"/>.
        /// </returns>
        public static Point[] TruncatePoints(PointF[] pointsF)
        {
            Point[] points = new Point[pointsF.Length];

            for (int i = 0; i < pointsF.Length; ++i)
            {
                points[i] = Point.Truncate(pointsF[i]);
            }

            return points;
        }

        /// <summary>
        /// The truncate rectangles.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <returns>
        /// The <see cref="Rectangle[]"/>.
        /// </returns>
        public static Rectangle[] TruncateRectangles(RectangleF[] rectsF)
        {
            Rectangle[] rects = new Rectangle[rectsF.Length];

            for (int i = 0; i < rectsF.Length; ++i)
            {
                rects[i] = Rectangle.Truncate(rectsF[i]);
            }

            return rects;
        }

        /// <summary>
        /// The write u int 16.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="word">
        /// The word.
        /// </param>
        public static void WriteUInt16(Stream stream, ushort word)
        {
            stream.WriteByte((byte)(word & 0xff));
            stream.WriteByte((byte)(word >> 8));
        }

        /// <summary>
        /// The write u int 24.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="uint24">
        /// The uint 24.
        /// </param>
        public static void WriteUInt24(Stream stream, int uint24)
        {
            stream.WriteByte((byte)(uint24 & 0xff));
            stream.WriteByte((byte)((uint24 >> 8) & 0xff));
            stream.WriteByte((byte)((uint24 >> 16) & 0xff));
        }

        /// <summary>
        /// The write u int 32.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        /// <param name="uint32">
        /// The uint 32.
        /// </param>
        public static void WriteUInt32(Stream stream, uint uint32)
        {
            stream.WriteByte((byte)(uint32 & 0xff));
            stream.WriteByte((byte)((uint32 >> 8) & 0xff));
            stream.WriteByte((byte)((uint32 >> 16) & 0xff));
            stream.WriteByte((byte)((uint32 >> 24) & 0xff));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The distance squared.
        /// </summary>
        /// <param name="rectsF">
        /// The rects f.
        /// </param>
        /// <param name="indexA">
        /// The index a.
        /// </param>
        /// <param name="indexB">
        /// The index b.
        /// </param>
        /// <returns>
        /// The <see cref="float"/>.
        /// </returns>
        private static float DistanceSquared(RectangleF[] rectsF, int indexA, int indexB)
        {
            PointF centerA = new PointF(
                rectsF[indexA].Left + (rectsF[indexA].Width / 2), 
                rectsF[indexA].Top + (rectsF[indexA].Height / 2));
            PointF centerB = new PointF(
                rectsF[indexB].Left + (rectsF[indexB].Width / 2), 
                rectsF[indexB].Top + (rectsF[indexB].Height / 2));

            return ((centerA.X - centerB.X) * (centerA.X - centerB.X))
                   + ((centerA.Y - centerB.Y) * (centerA.Y - centerB.Y));
        }

        /// <summary>
        /// The download small file.
        /// </summary>
        /// <param name="uri">
        /// The uri.
        /// </param>
        /// <param name="proxy">
        /// The proxy.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        private static byte[] DownloadSmallFile(Uri uri, WebProxy proxy)
        {
            WebRequest request = WebRequest.Create(uri);

            if (proxy != null)
            {
                request.Proxy = proxy;
            }

            request.Timeout = 5000;
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            try
            {
                byte[] buffer = new byte[8192];
                int offset = 0;

                while (offset < buffer.Length)
                {
                    int bytesRead = stream.Read(buffer, offset, buffer.Length - offset);

                    if (bytesRead == 0)
                    {
                        byte[] smallerBuffer = new byte[offset + bytesRead];

                        for (int i = 0; i < offset + bytesRead; ++i)
                        {
                            smallerBuffer[i] = buffer[i];
                        }

                        buffer = smallerBuffer;
                    }

                    offset += bytesRead;
                }

                return buffer;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }

                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }

        /// <summary>
        /// The find focus.
        /// </summary>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <returns>
        /// The <see cref="Control"/>.
        /// </returns>
        private static Control FindFocus(Control c)
        {
            if (c.Focused)
            {
                return c;
            }

            foreach (Control child in c.Controls)
            {
                Control f = FindFocus(child);

                if (f != null)
                {
                    return f;
                }
            }

            return null;
        }

        /// <summary>
        /// The highest bit.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int HighestBit(int x)
        {
            if (x == 0)
            {
                return 0;
            }

            int b = 0;
            int hi = 0;

            while (b <= 30)
            {
                if ((x & (1 << b)) != 0)
                {
                    hi = b;
                }

                ++b;
            }

            return hi;
        }

        /// <summary>
        /// The is inside.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// </exception>
        private static bool IsInside(RectangleF bounds, RectangleEdge edge, PointF p)
        {
            switch (edge)
            {
                case RectangleEdge.Left:
                    return !(p.X < bounds.Left);

                case RectangleEdge.Right:
                    return !(p.X >= bounds.Right);

                case RectangleEdge.Top:
                    return !(p.Y < bounds.Top);

                case RectangleEdge.Bottom:
                    return !(p.Y >= bounds.Bottom);

                default:
                    throw new InvalidEnumArgumentException("edge");
            }
        }

        /// <summary>
        /// The line intercept.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="Point"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static Point LineIntercept(Rectangle bounds, RectangleEdge edge, Point a, Point b)
        {
            if (a == b)
            {
                return a;
            }

            switch (edge)
            {
                case RectangleEdge.Bottom:
                    if (b.Y == a.Y)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new Point(a.X + (((b.X - a.X) * (bounds.Bottom - a.Y)) / (b.Y - a.Y)), bounds.Bottom);

                case RectangleEdge.Left:
                    if (b.X == a.X)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new Point(bounds.Left, a.Y + (((b.Y - a.Y) * (bounds.Left - a.X)) / (b.X - a.X)));

                case RectangleEdge.Right:
                    if (b.X == a.X)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new Point(bounds.Right, a.Y + (((b.Y - a.Y) * (bounds.Right - a.X)) / (b.X - a.X)));

                case RectangleEdge.Top:
                    if (b.Y == a.Y)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new Point(a.X + (((b.X - a.X) * (bounds.Top - a.Y)) / (b.Y - a.Y)), bounds.Top);
            }

            throw new ArgumentException("no intercept found");
        }

        /// <summary>
        /// The line intercept.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static PointF LineIntercept(RectangleF bounds, RectangleEdge edge, PointF a, PointF b)
        {
            if (a == b)
            {
                return a;
            }

            switch (edge)
            {
                case RectangleEdge.Bottom:
                    if (b.Y == a.Y)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new PointF(a.X + (((b.X - a.X) * (bounds.Bottom - a.Y)) / (b.Y - a.Y)), bounds.Bottom);

                case RectangleEdge.Left:
                    if (b.X == a.X)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new PointF(bounds.Left, a.Y + (((b.Y - a.Y) * (bounds.Left - a.X)) / (b.X - a.X)));

                case RectangleEdge.Right:
                    if (b.X == a.X)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new PointF(bounds.Right, a.Y + (((b.Y - a.Y) * (bounds.Right - a.X)) / (b.X - a.X)));

                case RectangleEdge.Top:
                    if (b.Y == a.Y)
                    {
                        throw new ArgumentException("no intercept found");
                    }

                    return new PointF(a.X + (((b.X - a.X) * (bounds.Top - a.Y)) / (b.Y - a.Y)), bounds.Top);
            }

            throw new ArgumentException("no intercept found");
        }

        /// <summary>
        /// The sutherland hodgman one axis.
        /// </summary>
        /// <param name="bounds">
        /// The bounds.
        /// </param>
        /// <param name="edge">
        /// The edge.
        /// </param>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private static List<PointF> SutherlandHodgmanOneAxis(RectangleF bounds, RectangleEdge edge, List<PointF> v)
        {
            if (v.Count == 0)
            {
                return new List<PointF>();
            }

            List<PointF> polygon = new List<PointF>();

            PointF s = v[v.Count - 1];

            for (int i = 0; i < v.Count; ++i)
            {
                PointF p = v[i];
                bool pIn = IsInside(bounds, edge, p);
                bool sIn = IsInside(bounds, edge, s);

                if (sIn && pIn)
                {
                    // case 1: inside -> inside
                    polygon.Add(p);
                }
                else if (sIn && !pIn)
                {
                    // case 2: inside -> outside
                    polygon.Add(LineIntercept(bounds, edge, s, p));
                }
                else if (!sIn && !pIn)
                {
                    // case 3: outside -> outside
                    // emit nothing
                }
                else if (!sIn && pIn)
                {
                    // case 4: outside -> inside
                    polygon.Add(LineIntercept(bounds, edge, s, p));
                    polygon.Add(p);
                }

                s = p;
            }

            return polygon;
        }

        /// <summary>
        /// The count bits.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int CountBits(int x)
        {
            uint y = (uint)x;
            int count = 0;

            for (int bit = 0; bit < 32; ++bit)
            {
                if ((y & ((uint)1 << bit)) != 0)
                {
                    ++count;
                }
            }

            return count;
        }

        #endregion

        /// <summary>
        /// The edge.
        /// </summary>
        private struct Edge
        {
            #region Fields

            /// <summary>
            /// The dxdy.
            /// </summary>
            public int dxdy; // fixed point: 24.8

            /// <summary>
            /// The maxy.
            /// </summary>
            public int maxy; // int

            /// <summary>
            /// The miny.
            /// </summary>
            public int miny; // int

            /// <summary>
            /// The x.
            /// </summary>
            public int x; // fixed point: 24.8

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Edge"/> struct.
            /// </summary>
            /// <param name="miny">
            /// The miny.
            /// </param>
            /// <param name="maxy">
            /// The maxy.
            /// </param>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="dxdy">
            /// The dxdy.
            /// </param>
            public Edge(int miny, int maxy, int x, int dxdy)
            {
                this.miny = miny;
                this.maxy = maxy;
                this.x = x;
                this.dxdy = dxdy;
            }

            #endregion
        }
    }
}