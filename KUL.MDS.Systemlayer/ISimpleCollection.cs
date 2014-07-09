// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISimpleCollection.cs" company="">
//   
// </copyright>
// <summary>
//   The SimpleCollection interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer
{
    /// <summary>
    /// The SimpleCollection interface.
    /// </summary>
    /// <typeparam name="K">
    /// </typeparam>
    /// <typeparam name="V">
    /// </typeparam>
    public interface ISimpleCollection<K, V>
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="V"/>.
        /// </returns>
        V Get(K key);

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void Set(K key, V value);

        #endregion
    }
}