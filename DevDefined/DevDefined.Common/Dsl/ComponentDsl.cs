// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentDsl.cs" company="">
//   
// </copyright>
// <summary>
//   The component dsl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    using System;
    using System.Collections.Generic;

    using DevDefined.Common.Hash;

    /// <summary>
    /// The component dsl.
    /// </summary>
    public class ComponentDsl : StandardDsl
    {
        #region Public Methods and Operators

        /// <summary>
        /// The component.
        /// </summary>
        /// <param name="batches">
        /// The batches.
        /// </param>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public Batch[] Component(params Batch[] batches)
        {
            Batch asBatch = delegate
                {
                    var componentNode = new ComponentNode { Name = this.NodeWriter.ExtractName };

                    this.NodeWriter.WriteStartNode(componentNode);
                    this.ExecuteBatches(batches);
                    this.NodeWriter.WriteEndNode();

                    return null;
                };

            asBatch.Ignore();

            return new[] { asBatch };
        }

        /// <summary>
        /// The item.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public Batch[] Item<T>(Func<T, string> func)
        {
            return new Batch[] { delegate
                {
                    this.NodeWriter.WriteNode(new ItemNode<T>(func));
                    return null;
                } };
        }

        /// <summary>
        /// The parameters.
        /// </summary>
        /// <param name="hash">
        /// The hash.
        /// </param>
        /// <returns>
        /// The <see cref="Batch"/>.
        /// </returns>
        public Batch Parameters(params Func<string, object>[] hash)
        {
            return this.Parameters(Hasher.Hash(hash));
        }

        /// <summary>
        /// The parameters.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="Batch"/>.
        /// </returns>
        public Batch Parameters(Dictionary<string, object> parameters)
        {
            return delegate
                {
                    this.FindNode<ComponentNode>().Parameters = parameters;

                    return null;
                };
        }

        /// <summary>
        /// The section.
        /// </summary>
        /// <param name="batches">
        /// The batches.
        /// </param>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public Batch[] Section(params Batch[] batches)
        {
            return new Batch[] { delegate
                {
                    string name = this.NodeWriter.ExtractName;

                    var sectionNode = new SectionNode();
                    this.FindNode<ComponentNode>().Sections.Add(name, sectionNode);

                    var nodeWriter = new NodeWriter();

                    using (new DslEvaluationScope(nodeWriter))
                    {
                        this.ExecuteBatches(batches);
                    }

                    sectionNode.Nodes.AddRange(nodeWriter.Nodes);

                    return null;
                } };
        }

        #endregion

        #region Methods

        /// <summary>
        /// The find node.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private T FindNode<T>() where T : class, INode
        {
            INode currentNode = this.NodeWriter.CurrentNode;

            while (currentNode != null)
            {
                var typedNode = currentNode as T;
                if (typedNode != null)
                {
                    return typedNode;
                }

                currentNode = currentNode.Parent;
            }

            return null;
        }

        #endregion
    }
}