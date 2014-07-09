// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakKeyReference_T.cs" company="">
//   
// </copyright>
// <summary>
//   Provides a weak reference to an object of the given type to be used in
//   a WeakDictionary along with the given comparer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.WeakRef
{
    /// <summary>
    /// Provides a weak reference to an object of the given type to be used in
    /// a WeakDictionary along with the given comparer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public sealed class WeakKeyReference<T> : WeakReference<T>
        where T : class
    {
        #region Fields

        /// <summary>
        /// The hash code.
        /// </summary>
        public readonly int HashCode;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyReference{T}"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        public WeakKeyReference(T key, WeakKeyComparer<T> comparer)
            : base(key)
        {
            // retain the object's hash code immediately so that even
            // if the target is GC'ed we will be able to find and
            // remove the dead weak reference.
            this.HashCode = comparer.GetHashCode(key);
        }

        #endregion
    }
}