// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakNullReference_T.cs" company="">
//   
// </copyright>
// <summary>
//   Provides a weak reference to a null target object, which, unlike
//   other weak references, is always considered to be alive. This
//   facilitates handling null dictionary values, which are perfectly
//   legal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.WeakRef
{
    /// <summary>
    /// Provides a weak reference to a null target object, which, unlike
    /// other weak references, is always considered to be alive. This
    /// facilitates handling null dictionary values, which are perfectly
    /// legal.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class WeakNullReference<T> : WeakReference<T>
        where T : class
    {
        #region Static Fields

        /// <summary>
        /// The singleton.
        /// </summary>
        public static readonly WeakNullReference<T> Singleton = new WeakNullReference<T>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="WeakNullReference"/> class from being created.
        /// </summary>
        private WeakNullReference()
            : base(null)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether is alive.
        /// </summary>
        public override bool IsAlive
        {
            get
            {
                return true;
            }
        }

        #endregion
    }
}