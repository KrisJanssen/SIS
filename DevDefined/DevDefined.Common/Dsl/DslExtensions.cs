// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DslExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The dsl extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Dsl
{
    using System;
    using System.Collections.Generic;

    using DevDefined.Common.Extensions.Annotations;

    /// <summary>
    /// The dsl extensions.
    /// </summary>
    public static class DslExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The for each.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Batch[]"/>.
        /// </returns>
        public static Batch[] ForEach<T>(this IEnumerable<T> source, Func<T, Batch> predicate)
        {
            var batches = new List<Batch>();

            foreach (T item in source)
            {
                batches.Add(predicate(item));
            }

            return batches.ToArray();
        }

        /// <summary>
        /// The ignore.
        /// </summary>
        /// <param name="batch">
        /// The batch.
        /// </param>
        public static void Ignore(this Batch batch)
        {
            batch.Annotate(Ignore => true);
        }

        /// <summary>
        /// The is ignored.
        /// </summary>
        /// <param name="batch">
        /// The batch.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsIgnored(this Batch batch)
        {
            return batch.HasAnnotation("Ignore");
        }

        #endregion
    }
}