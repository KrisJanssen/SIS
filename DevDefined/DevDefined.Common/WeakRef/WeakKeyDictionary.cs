using System;
using System.Collections.Generic;

namespace DevDefined.Common.WeakRef
{
    /// <summary>
    /// Dictionary where only the Key is a weak reference.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class WeakKeyDictionary<TKey, TValue> : BaseDictionary<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly WeakKeyComparer<TKey> comparer;
        private readonly Dictionary<object, TValue> dictionary;

        public WeakKeyDictionary()
            : this(0, null)
        {
        }

        public WeakKeyDictionary(int capacity)
            : this(capacity, null)
        {
        }

        public WeakKeyDictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
        }

        public WeakKeyDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.comparer = new WeakKeyComparer<TKey>(comparer);
            dictionary = new Dictionary<object, TValue>(capacity, this.comparer);
        }

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
            get { return dictionary.Count; }
        }

        public override void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key");
            WeakReference<TKey> weakKey = new WeakKeyReference<TKey>(key, comparer);
            dictionary.Add(weakKey, value);
        }

        public override bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public override bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        protected override void SetValue(TKey key, TValue value)
        {
            WeakReference<TKey> weakKey = new WeakKeyReference<TKey>(key, comparer);
            dictionary[weakKey] = value;
        }

        public override void Clear()
        {
            dictionary.Clear();
        }

        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var kvp in dictionary)
            {
                var weakKey = (WeakReference<TKey>) (kvp.Key);
                TKey key = weakKey.Target;
                TValue value = kvp.Value;

                if (weakKey.IsAlive)
                    yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        // Removes the left-over weak references for entries in the dictionary
        // whose key or value has already been reclaimed by the garbage
        // collector. This will reduce the dictionary's Count by the number
        // of dead key-value pairs that were eliminated.
        public void RemoveCollectedEntries()
        {
            List<object> toRemove = null;

            foreach (var pair in dictionary)
            {
                var weakKey = (WeakReference<TKey>) (pair.Key);

                if (!weakKey.IsAlive)
                {
                    if (toRemove == null)
                        toRemove = new List<object>();

                    toRemove.Add(weakKey);
                }
            }

            if (toRemove != null)
            {
                foreach (object key in toRemove)
                    dictionary.Remove(key);
            }
        }
    }
}