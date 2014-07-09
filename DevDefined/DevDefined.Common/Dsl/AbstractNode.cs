// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractNode.cs" company="">
//   
// </copyright>
// <summary>
//   The abstract node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    using System.Collections.Generic;

    /// <summary>
    /// The abstract node.
    /// </summary>
    public abstract class AbstractNode : INode
    {
        #region Fields

        /// <summary>
        /// The _nodes.
        /// </summary>
        private readonly List<INode> _nodes = new List<INode>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        public List<INode> Nodes
        {
            get
            {
                return this._nodes;
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public INode Parent { get; set; }

        #endregion
    }
}