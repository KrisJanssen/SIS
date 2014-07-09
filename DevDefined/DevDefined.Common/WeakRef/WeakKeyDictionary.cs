// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeakKeyDictionary.cs" company="">
//   
// </copyright>
// <summary>
//   Dictionary where only the Key is a weak reference.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.WeakRef
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Dictionary where only the Key is a weak reference.
    /// </summary>
    /// <typeparam name="TKey">
    /// </typeparam>
    /// <typeparam name="TValue">
    /// </typeparam>
    public sealed class WeakKeyDictionary<TKey, TValue> : BaseDictionary<TKey, TValue>
        where TKey : class where TValue : class
    {
        #region Fields

        /// <summary>
        /// The comparer.
        /// </summary>
        private readonly WeakKeyComparer<TKey> comparer;

        /// <summary>
        /// The dictionary.
        /// </summary>
        private readonly Dictionary<object, TValue> dictionary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyDictionary{TKey,TValue}"/> class.
        /// </summary>
        public WeakKeyDictionary()
            : this(0, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyDictionary{TKey,TValue}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        public WeakKeyDictionary(int capacity)
            : this(capacity, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyDictionary{TKey,TValue}"/> class.
        /// </summary>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        public WeakKeyDictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakKeyDictionary{TKey,TValue}"/> class.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        public WeakKeyDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.comparer = new WeakKeyComparer<TKey>(comparer);
            this.dictionary = new Dictionary<object, TValue>(capacity, this.comparer);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns the count of items in the dictionary.
        /// <remarks>
        /// </remarks>
        /// WARNING: The count returned here may include entries for which
        /// either the key or value objects have already been garbage
        /// collected. Call RemoveCollectedEntries to weed out collected
        /// entries and update the count accordingly.
        /// </summary>
        public override int Count
        {
            get
            {
                return this.dictionary.Count;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public override void Add(TKey key, TValue value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            WeakReference<TKey> weakKey = new WeakKeyReference<TKey>(key, this.comparer);
            this.dictionary.Add(weakKey, value);
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public override void Clear()
        {
            this.dictionary.Clear();
        }

        /// <summary>
        /// The contains key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool ContainsKey(TKey key)
        {
            return this.dictionary.ContainsKey(key);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var kvp in this.dictionary)
            {
                var weakKey = (WeakReference<TKey>)kvp.Key;
                TKey key = weakKey.Target;
                TValue value = kvp.Value;

                if (weakKey.IsAlive)
                {
                    yield return new KeyValuePair<TKey, TValue>(key, value);
                }
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Remove(TKey key)
        {
            return this.dictionary.Remove(key);
        }

        /// <summary>
        /// The remove collected entries.
        /// </summary>
        public void RemoveCollectedEntries()
        {
            List<object> toRemove = null;

            foreach (var pair in this.dictionary)
            {
                var weakKey = (WeakReference<TKey>)pair.Key;

                if (!weakKey.IsAlive)
                {
                    if (toRemove == null)
                    {
                        toRemove = new List<object>();
                    }

                    toRemove.Add(weakKey);
                }
            }

            if (toRemove != null)
            {
                foreach (object key in toRemove)
                {
                    this.dictionary.Remove(key);
                }
            }
        }

        /// <summary>
        /// The try get value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The set value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        protected override void SetValue(TKey key, TValue value)
        {
            WeakReference<TKey> weakKey = new WeakKeyReference<TKey>(key, this.comparer);
            this.dictionary[weakKey] = value;
        }

        #endregion
    }
}