// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnnotation.cs" company="">
//   
// </copyright>
// <summary>
//   The Annotation interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Extensions.Annotations
{
    using System;

    /// <summary>
    /// The Annotation interface.
    /// </summary>
    public interface IAnnotation
    {
        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object this[object key] { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        void Annotate<T>(params Func<string, T>[] args);

        /// <summary>
        /// The clear.
        /// </summary>
        void Clear();

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void Remove(object key);

        #endregion
    }
}