using System.Collections.Generic;

namespace DevDefined.Common.WeakRef
{
    /// <summary>
    /// Compares objects of the given type or WeakKeyReferences to them
    /// for equality based on the given comparer. Note that we can only
    /// implement IEqualityComparer<T> for T = object as there is no
    /// other common base between T and WeakKeyReference<T>. We need a
    /// single comparer to handle both types because we don't want to
    /// allocate a new weak reference for every lookup.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class WeakKeyComparer<T> : IEqualityComparer<object>
        where T : class
    {
        private readonly IEqualityComparer<T> comparer;

        public WeakKeyComparer(IEqualityComparer<T> comparer)
        {
            if (comparer == null)
                comparer = EqualityComparer<T>.Default;

            this.comparer = comparer;
        }

        #region IEqualityComparer<object> Members

        public int GetHashCode(object obj)
        {
            var weakKey = obj as WeakKeyReference<T>;
            if (weakKey != null) return weakKey.HashCode;
            return comparer.GetHashCode((T) obj);
        }

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
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public new bool Equals(object x, object y)
        {
            bool xIsDead, yIsDead;
            T first = GetTarget(x, out xIsDead);
            T second = GetTarget(y, out yIsDead);

            if (xIsDead)
                return yIsDead ? x == y : false;

            if (yIsDead)
                return false;

            return comparer.Equals(first, second);
        }

        #endregion

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
                target = (T) obj;
                isDead = false;
            }

            return target;
        }
    }
}