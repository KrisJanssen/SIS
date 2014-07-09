// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseDictionary.cs" company="">
//   
// </copyright>
// <summary>
//   Represents a dictionary mapping keys to values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a dictionary mapping keys to values.
    /// </summary>
    /// <typeparam name="TKey">
    /// </typeparam>
    /// <typeparam name="TValue">
    /// </typeparam>
    /// <remarks>
    /// Provides the plumbing for the portions of IDictionary&lt;TKey,TValue&gt; 
    /// which can reasonably be implemented without any dependency on the underlying 
    /// representation of the dictionary.
    /// </remarks>
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(PREFIX + "DictionaryDebugView`2" + SUFFIX)]
    public abstract class BaseDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Constants

        /// <summary>
        /// The prefix.
        /// </summary>
        private const string PREFIX = "System.Collections.Generic.Mscorlib_";

        /// <summary>
        /// The suffix.
        /// </summary>
        private const string SUFFIX = ",mscorlib,Version=2.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089";

        #endregion

        #region Fields

        /// <summary>
        /// The keys.
        /// </summary>
        private KeyCollection keys;

        /// <summary>
        /// The values.
        /// </summary>
        private ValueCollection values;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public abstract int Count { get; }

        /// <summary>
        /// Gets a value indicating whether is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                if (this.keys == null)
                {
                    this.keys = new KeyCollection(this);
                }

                return this.keys;
            }
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                if (this.values == null)
                {
                    this.values = new ValueCollection(this);
                }

                return this.values;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <exception cref="KeyNotFoundException">
        /// </exception>
        /// <returns>
        /// The <see cref="TValue"/>.
        /// </returns>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!this.TryGetValue(key, out value))
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }

            set
            {
                this.SetValue(key, value);
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
        public abstract void Add(TKey key, TValue value);

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue value;
            if (!this.TryGetValue(item.Key, out value))
            {
                return false;
            }

            return EqualityComparer<TValue>.Default.Equals(value, item.Value);
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
        public abstract bool ContainsKey(TKey key);

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Copy(this, array, arrayIndex);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public abstract IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public abstract bool Remove(TKey key);

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!this.Contains(item))
            {
                return false;
            }

            return Remove(item.Key);
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
        public abstract bool TryGetValue(TKey key, out TValue value);

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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
        protected abstract void SetValue(TKey key, TValue value);

        /// <summary>
        /// The copy.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static void Copy<T>(ICollection<T> source, T[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (arrayIndex < 0 || arrayIndex > array.Length)
            {
                throw new ArgumentOutOfRangeException("arrayIndex");
            }

            if ((array.Length - arrayIndex) < source.Count)
            {
                throw new ArgumentException("Destination array is not large enough. Check array.Length and arrayIndex.");
            }

            foreach (T item in source)
            {
                array[arrayIndex++] = item;
            }
        }

        #endregion

        /// <summary>
        /// The collection.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        private abstract class Collection<T> : ICollection<T>
        {
            #region Fields

            /// <summary>
            /// The dictionary.
            /// </summary>
            protected readonly IDictionary<TKey, TValue> dictionary;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="Collection{T}"/> class.
            /// </summary>
            /// <param name="dictionary">
            /// The dictionary.
            /// </param>
            protected Collection(IDictionary<TKey, TValue> dictionary)
            {
                this.dictionary = dictionary;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the count.
            /// </summary>
            public int Count
            {
                get
                {
                    return this.dictionary.Count;
                }
            }

            /// <summary>
            /// Gets a value indicating whether is read only.
            /// </summary>
            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The add.
            /// </summary>
            /// <param name="item">
            /// The item.
            /// </param>
            /// <exception cref="NotSupportedException">
            /// </exception>
            public void Add(T item)
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            /// <summary>
            /// The clear.
            /// </summary>
            /// <exception cref="NotSupportedException">
            /// </exception>
            public void Clear()
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            /// <summary>
            /// The contains.
            /// </summary>
            /// <param name="item">
            /// The item.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public virtual bool Contains(T item)
            {
                foreach (T element in this)
                {
                    if (EqualityComparer<T>.Default.Equals(element, item))
                    {
                        return true;
                    }
                }

                return false;
            }

            /// <summary>
            /// The copy to.
            /// </summary>
            /// <param name="array">
            /// The array.
            /// </param>
            /// <param name="arrayIndex">
            /// The array index.
            /// </param>
            public void CopyTo(T[] array, int arrayIndex)
            {
                Copy(this, array, arrayIndex);
            }

            /// <summary>
            /// The get enumerator.
            /// </summary>
            /// <returns>
            /// The <see cref="IEnumerator"/>.
            /// </returns>
            public IEnumerator<T> GetEnumerator()
            {
                foreach (var pair in this.dictionary)
                {
                    yield return this.GetItem(pair);
                }
            }

            /// <summary>
            /// The remove.
            /// </summary>
            /// <param name="item">
            /// The item.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            /// <exception cref="NotSupportedException">
            /// </exception>
            public bool Remove(T item)
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            #endregion

            #region Explicit Interface Methods

            /// <summary>
            /// The get enumerator.
            /// </summary>
            /// <returns>
            /// The <see cref="IEnumerator"/>.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region Methods

            /// <summary>
            /// The get item.
            /// </summary>
            /// <param name="pair">
            /// The pair.
            /// </param>
            /// <returns>
            /// The <see cref="T"/>.
            /// </returns>
            protected abstract T GetItem(KeyValuePair<TKey, TValue> pair);

            #endregion
        }

        /// <summary>
        /// The key collection.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(PREFIX + "DictionaryKeyCollectionDebugView`2" + SUFFIX)]
        private class KeyCollection : Collection<TKey>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyCollection"/> class.
            /// </summary>
            /// <param name="dictionary">
            /// The dictionary.
            /// </param>
            public KeyCollection(IDictionary<TKey, TValue> dictionary)
                : base(dictionary)
            {
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The contains.
            /// </summary>
            /// <param name="item">
            /// The item.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public override bool Contains(TKey item)
            {
                return this.dictionary.ContainsKey(item);
            }

            #endregion

            #region Methods

            /// <summary>
            /// The get item.
            /// </summary>
            /// <param name="pair">
            /// The pair.
            /// </param>
            /// <returns>
            /// The <see cref="TKey"/>.
            /// </returns>
            protected override TKey GetItem(KeyValuePair<TKey, TValue> pair)
            {
                return pair.Key;
            }

            #endregion
        }

        /// <summary>
        /// The value collection.
        /// </summary>
        [DebuggerDisplay("Count = {Count}")]
        [DebuggerTypeProxy(PREFIX + "DictionaryValueCollectionDebugView`2" + SUFFIX)]
        private class ValueCollection : Collection<TValue>
        {
            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueCollection"/> class.
            /// </summary>
            /// <param name="dictionary">
            /// The dictionary.
            /// </param>
            public ValueCollection(IDictionary<TKey, TValue> dictionary)
                : base(dictionary)
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// The get item.
            /// </summary>
            /// <param name="pair">
            /// The pair.
            /// </param>
            /// <returns>
            /// The <see cref="TValue"/>.
            /// </returns>
            protected override TValue GetItem(KeyValuePair<TKey, TValue> pair)
            {
                return pair.Value;
            }

            #endregion
        }
    }
}