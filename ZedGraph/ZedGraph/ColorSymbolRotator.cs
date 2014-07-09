// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ColorSymbolRotator.cs">
//   
// </copyright>
// <summary>
//   Class used to get the next color/symbol for GraphPane.AddCurve methods.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System.Drawing;

    /// <summary>
    /// Class used to get the next color/symbol for GraphPane.AddCurve methods.
    /// </summary>
    /// 
    /// <author> Jerry Vos modified by John Champion </author>
    /// <version> $Revision: 3.4 $ $Date: 2006-06-24 20:26:43 $ </version>
    public class ColorSymbolRotator
    {
        #region Static Fields

        /// <summary>
        /// The <see cref="Color"/>s <see cref="ColorSymbolRotator"/> 
        /// rotates through.
        /// </summary>
        public static readonly Color[] COLORS = new[]
                                                    {
                                                        Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Cyan, 
                                                        Color.Pink, Color.LightBlue, Color.PaleVioletRed, Color.SeaGreen, 
                                                        Color.Yellow
                                                    };

        /// <summary>
        /// The <see cref="SymbolType"/>s <see cref="ColorSymbolRotator"/> 
        /// rotates through.
        /// </summary>
        public static readonly SymbolType[] SYMBOLS = new[]
                                                          {
                                                              SymbolType.Circle, SymbolType.Diamond, SymbolType.Plus, 
                                                              SymbolType.Square, SymbolType.Star, SymbolType.Triangle, 
                                                              SymbolType.TriangleDown, SymbolType.XCross, SymbolType.HDash, 
                                                              SymbolType.VDash
                                                          };

        /// <summary>
        /// The _static instance.
        /// </summary>
        private static ColorSymbolRotator _staticInstance;

        #endregion

        #region Fields

        /// <summary>
        /// The index of the next color to be used. Note: may be 
        /// > COLORS.Length, it is reset to 0 on the next call if it is.
        /// </summary>
        protected int colorIndex = 0;

        /// <summary>
        /// The index of the next symbol to be used. Note: may be 
        /// > SYMBOLS.Length, it is reset to 0 on the next call if it is.
        /// </summary>
        protected int symbolIndex = 0;

        #endregion

        #region Public Properties

        /// <summary>
        /// Retrieves the <see cref="ColorSymbolRotator"/> instance used by the
        /// static methods.
        /// <seealso cref="StaticNextColor"/>
        /// <seealso cref="StaticNextSymbol"/>
        /// </summary>
        public static ColorSymbolRotator StaticInstance
        {
            get
            {
                if (_staticInstance == null)
                {
                    _staticInstance = new ColorSymbolRotator();
                }

                return _staticInstance;
            }
        }

        /// <summary>
        /// Retrieves the next color from this class's static 
        /// <see cref="ColorSymbolRotator"/> instance
        /// <seealso cref="StaticInstance"/>
        /// <seealso cref="StaticNextSymbol"/>
        /// </summary>
        public static Color StaticNextColor
        {
            get
            {
                return StaticInstance.NextColor;
            }
        }

        /// <summary>
        /// Retrieves the next symbol type from this class's static 
        /// <see cref="ColorSymbolRotator"/> instance
        /// <seealso cref="StaticInstance"/>
        /// <seealso cref="StaticNextColor"/>
        /// </summary>
        public static SymbolType StaticNextSymbol
        {
            get
            {
                return StaticInstance.NextSymbol;
            }
        }

        /// <summary>
        /// Retrieves the next color in the rotation  Calling this
        /// method has the side effect of incrementing the color index.
        /// <seealso cref="NextSymbol"/>
        /// <seealso cref="NextColorIndex"/>
        /// </summary>
        public Color NextColor
        {
            get
            {
                return COLORS[this.NextColorIndex];
            }
        }

        /// <summary>
        /// Retrieves the index of the next color to be used.  Calling this
        /// method has the side effect of incrementing the color index.
        /// </summary>
        public int NextColorIndex
        {
            get
            {
                if (this.colorIndex >= COLORS.Length)
                {
                    this.colorIndex = 0;
                }

                return this.colorIndex++;
            }

            set
            {
                this.colorIndex = value;
            }
        }

        /// <summary>
        /// Retrieves the next color in the rotation.  Calling this
        /// method has the side effect of incrementing the symbol index.
        /// <seealso cref="NextColor"/>
        /// <seealso cref="NextSymbolIndex"/>
        /// </summary>
        public SymbolType NextSymbol
        {
            get
            {
                return SYMBOLS[this.NextSymbolIndex];
            }
        }

        /// <summary>
        /// Retrieves the index of the next symbol to be used.  Calling this
        /// method has the side effect of incrementing the symbol index.
        /// </summary>
        public int NextSymbolIndex
        {
            get
            {
                if (this.symbolIndex >= SYMBOLS.Length)
                {
                    this.symbolIndex = 0;
                }

                return this.symbolIndex++;
            }

            set
            {
                this.symbolIndex = value;
            }
        }

        #endregion
    }
}