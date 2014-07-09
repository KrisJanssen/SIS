// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INode.cs" company="">
//   
// </copyright>
// <summary>
//   The Node interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    using System.Collections.Generic;

    /// <summary>
    /// The Node interface.
    /// </summary>
    public interface INode
    {
        #region Public Properties

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        List<INode> Nodes { get; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        INode Parent { get; set; }

        #endregion
    }
}