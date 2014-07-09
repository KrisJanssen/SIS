// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextNode.cs" company="">
//   
// </copyright>
// <summary>
//   The text node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    /// <summary>
    /// The text node.
    /// </summary>
    public class TextNode : AbstractNode
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextNode"/> class.
        /// </summary>
        public TextNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextNode"/> class.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        public TextNode(string text)
        {
            this.Text = text;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        #endregion
    }
}