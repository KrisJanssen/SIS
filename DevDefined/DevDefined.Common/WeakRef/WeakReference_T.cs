// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakReference_T.cs" company="">
//   
// </copyright>
// <summary>
//   Adds strong typing to WeakReference.Target using generics. Also,
//   the Create factory method is used in place of a constructor
//   to handle the case where target is null, but we want the
//   reference to still appear to be alive.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.WeakRef
{
    using System;

    /// <summary>
    /// Adds strong typing to WeakReference.Target using generics. Also,
    /// the Create factory method is used in place of a constructor
    /// to handle the case where target is null, but we want the
    /// reference to still appear to be alive.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class WeakReference<T> : WeakReference
        where T : class
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakReference{T}"/> class.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        protected WeakReference(T target)
            : base(target, false)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the target.
        /// </summary>
        public new T Target
        {
            get
            {
                return (T)base.Target;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="WeakReference"/>.
        /// </returns>
        public static WeakReference<T> Create(T target)
        {
            if (target == null)
            {
                return WeakNullReference<T>.Singleton;
            }

            return new WeakReference<T>(target);
        }

        #endregion
    }
}