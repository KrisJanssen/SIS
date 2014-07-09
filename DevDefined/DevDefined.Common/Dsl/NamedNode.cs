// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamedNode.cs" company="">
//   
// </copyright>
// <summary>
//   The named node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    /// <summary>
    /// The named node.
    /// </summary>
    public class NamedNode : AbstractNode
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedNode"/> class.
        /// </summary>
        public NamedNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedNode"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public NamedNode(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}