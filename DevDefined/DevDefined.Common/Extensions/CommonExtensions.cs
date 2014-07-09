// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The common extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Extensions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The common extensions.
    /// </summary>
    public static class CommonExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The for each.
        /// </summary>
        /// <param name="sequence">
        /// The sequence.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (T item in sequence)
            {
                action(item);
            }
        }

        /// <summary>
        /// The loop to.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="end">
        /// The end.
        /// </param>
        /// <returns>
        /// The <see cref="Action"/>.
        /// </returns>
        public static Action<Action<int>> LoopTo(this int start, int end)
        {
            return action => To(start, end).ForEach(i => action(i));
        }

        /// <summary>
        /// The print line.
        /// </summary>
        /// <param name="o">
        /// The o.
        /// </param>
        public static void PrintLine(this object o)
        {
            Console.WriteLine(o);
        }

        /// <summary>
        /// The to.
        /// </summary>
        /// <param name="start">
        /// The start.
        /// </param>
        /// <param name="end">
        /// The end.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<int> To(this int start, int end)
        {
            if (end < start)
            {
                for (int i = start; i > end - 1; i--)
                {
                    yield return i;
                }
            }
            else
            {
                for (int i = start; i < end + 1; i++)
                {
                    yield return i;
                }
            }
        }

        /// <summary>
        /// The to projected dictionary of lists.
        /// </summary>
        /// <param name="that">
        /// The that.
        /// </param>
        /// <param name="keyFunc">
        /// The key func.
        /// </param>
        /// <param name="projectedItemFunc">
        /// The projected item func.
        /// </param>
        /// <typeparam name="ItemType">
        /// </typeparam>
        /// <typeparam name="KeyType">
        /// </typeparam>
        /// <typeparam name="ProjectedType">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDictionary"/>.
        /// </returns>
        public static IDictionary<KeyType, IList<ProjectedType>> ToProjectedDictionaryOfLists
            <ItemType, KeyType, ProjectedType>(
            this IEnumerable<ItemType> that, 
            Func<ItemType, KeyType> keyFunc, 
            Func<ItemType, ProjectedType> projectedItemFunc)
        {
            var dictionaryOfLists = new Dictionary<KeyType, IList<ProjectedType>>();
            foreach (ItemType item in that)
            {
                KeyType key = keyFunc(item);
                ProjectedType projectedItem = projectedItemFunc(item);
                IList<ProjectedType> list;
                if (dictionaryOfLists.TryGetValue(key, out list))
                {
                    list.Add(projectedItem);
                }
                else
                {
                    list = new List<ProjectedType>();
                    list.Add(projectedItem);
                    dictionaryOfLists.Add(key, list);
                }
            }

            return dictionaryOfLists;
        }

        #endregion
    }
}