using System;
using System.Collections.Generic;

namespace DevDefined.Common.WeakRef
{
    /// <summary>
    /// A generic dictionary, which allows both its keys and values
    /// to be garbage collected if there are no other references
    /// to them than from the dictionary itself.
    /// </summary>    
    /// <remarks>
    /// If either the key or value of a particular entry in the dictionary
    /// has been collected, then both the key and value become effectively
    /// unreachable. However, left-over WeakReference objects for the key
    /// and value will physically remain in the dictionary until
    /// RemoveCollectedEntries is called. This will lead to a discrepancy
    /// between the Count property and the number of iterations required
    /// to visit all of the elements of the dictionary using its
    /// enumerator or those of the Keys and Values collections. Similarly,
    /// CopyTo will copy fewer than Count elements in this situation.
    /// </remarks>
    public sealed class WeakDictionary<TKey, TValue> : BaseDictionary<TKey, TValue>
        where TKey : class
        where TValue : class
    {
        private readonly WeakKeyComparer<TKey> comparer;
        private readonly Dictionary<object, WeakReference<TValue>> dictionary;

        public WeakDictionary()
            : this(0, null)
        {
        }

        public WeakDictionary(int capacity)
            : this(capacity, null)
        {
        }

        public WeakDictionary(IEqualityComparer<TKey> comparer)
            : this(0, comparer)
        {
        }

        public WeakDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.comparer = new WeakKeyComparer<TKey>(comparer);
            dictionary = new Dictionary<object, WeakReference<TValue>>(capacity, this.comparer);
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
            WeakReference<TValue> weakValue = WeakReference<TValue>.Create(value);
            dictionary.Add(weakKey, weakValue);
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
            WeakReference<TValue> weakValue;
            if (dictionary.TryGetValue(key, out weakValue))
            {
                value = weakValue.Target;
                return weakValue.IsAlive;
            }
            value = null;
            return false;
        }

        protected override void SetValue(TKey key, TValue value)
        {
            WeakReference<TKey> weakKey = new WeakKeyReference<TKey>(key, comparer);
            dictionary[weakKey] = WeakReference<TValue>.Create(value);
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
                WeakReference<TValue> weakValue = kvp.Value;
                TKey key = weakKey.Target;
                TValue value = weakValue.Target;
                if (weakKey.IsAlive && weakValue.IsAlive)
                    yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        /// <summary>
        /// Removes the left-over weak references for entries in the dictionary
        /// whose key or value has already been reclaimed by the garbage
        /// collector. This will reduce the dictionary's Count by the number
        /// of dead key-value pairs that were eliminated.       
        /// </summary>
        public void RemoveCollectedEntries()
        {
            List<object> toRemove = null;
            foreach (var pair in dictionary)
            {
                var weakKey = (WeakReference<TKey>) (pair.Key);
                WeakReference<TValue> weakValue = pair.Value;

                if (!weakKey.IsAlive || !weakValue.IsAlive)
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