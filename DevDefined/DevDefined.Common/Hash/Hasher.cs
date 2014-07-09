// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hasher.cs" company="">
//   
// </copyright>
// <summary>
//   The hasher.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Hash
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The hasher.
    /// </summary>
    public static class Hasher
    {
        #region Public Methods and Operators

        /// <summary>
        /// The add to hash.
        /// </summary>
        /// <param name="dict">
        /// The dict.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void AddToHash<T>(Dictionary<string, T> dict, params Func<string, T>[] args)
        {
            foreach (var func in args)
            {
                dict[func.Method.GetParameters()[0].Name] = func(null);
            }
        }

        /// <summary>
        /// The hash.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Dictionary"/>.
        /// </returns>
        public static Dictionary<string, T> Hash<T>(params Func<string, T>[] args)
        {
            var dict = new Dictionary<string, T>();
            AddToHash(dict, args);
            return dict;
        }

        #endregion
    }
}