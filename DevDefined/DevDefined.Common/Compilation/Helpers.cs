// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Helpers.cs" company="">
//   
// </copyright>
// <summary>
//   The helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Compilation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The helpers.
    /// </summary>
    public static class Helpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// The first different down.
        /// </summary>
        /// <param name="arr">
        /// The arr.
        /// </param>
        /// <param name="searchFrom">
        /// The search from.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public static int FirstDifferentDown<T, TV>(this T[] arr, int searchFrom, Func<T, TV> selector)
            where TV : IComparable<TV>
        {
            TV tvfrom = selector(arr[searchFrom]);
            for (int i = searchFrom - 1; i >= 0; --i)
            {
                if (tvfrom.CompareTo(selector(arr[i])) != 0)
                {
                    return i;
                }
            }

            // TODO: review just what exception should be thrown here.
            throw new Exception("No match");
        }

        /// <summary>
        /// The take page.
        /// </summary>
        /// <param name="seq">
        /// The seq.
        /// </param>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="last">
        /// The last.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> TakePage<T>(this IEnumerable<T> seq, int first, int last)
        {
            return seq.Skip(first).Take(last - first + 1);
        }

        /// <summary>
        /// The to ordered array.
        /// </summary>
        /// <param name="seq">
        /// The seq.
        /// </param>
        /// <param name="keySelector">
        /// The key selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T[]"/>.
        /// </returns>
        public static T[] ToOrderedArray<T, TKey>(this IEnumerable<T> seq, Func<T, TKey> keySelector)
        {
            return seq.OrderBy(keySelector).ToArray();
        }

        #endregion
    }
}