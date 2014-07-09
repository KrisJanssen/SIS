// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakKeyComparer_T.cs" company="">
//   
// </copyright>
// <summary>
//   The weak key comparer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.WeakRef
{
    using System.Collections.Generic;

    /// <summary>
    /// The weak key comparer.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public sealed class WeakKeyComparer<T> : IEqualityComparer<object>
        where T : class
    {
        #region Fields

        /// <summary>
        /// The comparer.
        /// </summary>
        private readonly IEqualityComparer<T> comparer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyComparer{T}"/> class.
        /// </summary>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        public WeakKeyComparer(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            this.comparer = comparer;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Note: There are actually 9 cases to handle here.
        /// 
        ///  Let Wa = Alive Weak Reference
        ///  Let Wd = Dead Weak Reference
        ///  Let S  = Strong Reference
        ///  
        ///  x  | y  | Equals(x,y)
        /// -------------------------------------------------
        ///  Wa | Wa | comparer.Equals(x.Target, y.Target)
        ///  Wa | Wd | false
        ///  Wa | S  | comparer.Equals(x.Target, y)
        ///  Wd | Wa | false
        ///  Wd | Wd | x == y
        ///  Wd | S  | false
        ///  S  | Wa | comparer.Equals(x, y.Target)
        ///  S  | Wd | false
        ///  S  | S  | comparer.Equals(x, y)
        /// -------------------------------------------------
        /// </summary>
        /// <param name="x">
        /// </param>
        /// <param name="y">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public new bool Equals(object x, object y)
        {
            bool xIsDead, yIsDead;
            T first = GetTarget(x, out xIsDead);
            T second = GetTarget(y, out yIsDead);

            if (xIsDead)
            {
                return yIsDead ? x == y : false;
            }

            if (yIsDead)
            {
                return false;
            }

            return this.comparer.Equals(first, second);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetHashCode(object obj)
        {
            var weakKey = obj as WeakKeyReference<T>;
            if (weakKey != null)
            {
                return weakKey.HashCode;
            }

            return this.comparer.GetHashCode((T)obj);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get target.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="isDead">
        /// The is dead.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        private static T GetTarget(object obj, out bool isDead)
        {
            var wref = obj as WeakKeyReference<T>;

            T target;

            if (wref != null)
            {
                target = wref.Target;
                isDead = !wref.IsAlive;
            }
            else
            {
                target = (T)obj;
                isDead = false;
            }

            return target;
        }

        #endregion
    }
}