// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeWriter.cs" company="">
//   
// </copyright>
// <summary>
//   The node writer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The node writer.
    /// </summary>
    public class NodeWriter
    {
        #region Fields

        /// <summary>
        /// The _root nodes.
        /// </summary>
        private readonly List<INode> _rootNodes = new List<INode>();

        /// <summary>
        /// The _current node.
        /// </summary>
        private INode _currentNode;

        /// <summary>
        /// The _ignore write end count.
        /// </summary>
        private int _ignoreWriteEndCount;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current node.
        /// </summary>
        public INode CurrentNode
        {
            get
            {
                return this._currentNode;
            }
        }

        /// <summary>
        /// Gets the extract name.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public string ExtractName
        {
            get
            {
                var namedNode = this._currentNode as NamedNode;

                if (namedNode != null)
                {
                    this._ignoreWriteEndCount++;
                    this._currentNode = this._currentNode.Parent;
                    this._currentNode.Nodes.Remove(namedNode);
                    return namedNode.Name;
                }

                throw new InvalidOperationException(
                    "The CurrentNode is not of type NamedNode, and can not be popped to fetch the name");
            }
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        public IEnumerable<INode> Nodes
        {
            get
            {
                return this._rootNodes;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The write end node.
        /// </summary>
        /// <returns>
        /// The <see cref="NodeWriter"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public NodeWriter WriteEndNode()
        {
            if (this._ignoreWriteEndCount > 0)
            {
                this._ignoreWriteEndCount--;
            }
            else
            {
                if (this._currentNode == null)
                {
                    throw new InvalidOperationException(
                        "WriteEndNode called does not match any oustanding call to WriteStartNode");
                }

                this._currentNode = this._currentNode.Parent;
            }

            return this;
        }

        /// <summary>
        /// The write node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="NodeWriter"/>.
        /// </returns>
        public NodeWriter WriteNode(INode node)
        {
            if (this._currentNode != null)
            {
                this._currentNode.Nodes.Add(node);
            }
            else
            {
                this._rootNodes.Add(node);
            }

            return this;
        }

        /// <summary>
        /// The write start node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="NodeWriter"/>.
        /// </returns>
        public NodeWriter WriteStartNode(INode node)
        {
            if (this._currentNode != null)
            {
                this._currentNode.Nodes.Add(node);
                node.Parent = this._currentNode;
            }
            else
            {
                this._rootNodes.Add(node);
            }

            this._currentNode = node;

            return this;
        }

        #endregion
    }
}