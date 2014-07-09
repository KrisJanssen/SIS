// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredItem.cs" company="">
//   
// </copyright>
// <summary>
//   The colored item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Appenders
{
    /// <summary>
    /// The colored item.
    /// </summary>
    public sealed class ColoredItem
    {
        #region Fields

        /// <summary>
        /// The _rtf.
        /// </summary>
        private readonly string _rtf;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredItem"/> class.
        /// </summary>
        /// <param name="colorIndex">
        /// The color index.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        public ColoredItem(int colorIndex, string text)
        {
            this._rtf = "\\cf" + colorIndex + "\r\n" + text.Replace("\r\n", "\r\n\\line ");
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return this._rtf;
        }

        #endregion
    }
}