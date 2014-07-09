// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemNode.cs" company="">
//   
// </copyright>
// <summary>
//   The item node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Dsl
{
    using System;

    /// <summary>
    /// The item node.
    /// </summary>
    public abstract class ItemNode : AbstractNode
    {
        #region Public Methods and Operators

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public abstract string Evaluate(object source);

        #endregion
    }

    /// <summary>
    /// The item node.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ItemNode<T> : ItemNode
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNode{T}"/> class.
        /// </summary>
        public ItemNode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNode{T}"/> class.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        public ItemNode(Func<T, string> func)
        {
            this.Func = func;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the func.
        /// </summary>
        public Func<T, string> Func { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The evaluate.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string Evaluate(object source)
        {
            return this.Func((T)source);
        }

        #endregion
    }
}