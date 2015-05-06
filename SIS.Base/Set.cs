// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Set.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Represents an enumerable collection of items. Each item can only be present
//   in the collection once. An item's identity is determined by a combination
//   of the return values from its GetHashCode and Equals methods.
//   This class is analagous to C++'s std::set template class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    ///     Represents an enumerable collection of items. Each item can only be present
    ///     in the collection once. An item's identity is determined by a combination
    ///     of the return values from its GetHashCode and Equals methods.
    ///     This class is analagous to C++'s std::set template class.
    /// </summary>
    [Serializable]
    public class Set : IEnumerable, ICloneable, ICollection
    {
        #region Fields

        /// <summary>
        /// The hashtable.
        /// </summary>
        private readonly Hashtable hashtable;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class. 
        ///     Constructs an empty Set.
        /// </summary>
        public Set()
        {
            this.hashtable = new Hashtable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class. 
        /// Constructs a Set with data copied from the given list.
        /// </summary>
        /// <param name="cloneMe">
        /// </param>
        public Set(IEnumerable cloneMe)
        {
            this.hashtable = new Hashtable();

            foreach (object theObject in cloneMe)
            {
                this.Add(theObject);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set"/> class. 
        /// Constructs a copy of a Set.
        /// </summary>
        /// <param name="copyMe">
        /// The Set to copy from.
        /// </param>
        private Set(Set copyMe)
        {
            this.hashtable = (Hashtable)copyMe.Clone();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating how many elements are contained within the Set.
        /// </summary>
        public int Count
        {
            get
            {
                return this.hashtable.Count;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not the Set is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the Set.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public static Set<T> Create<T>(params T[] items)
        {
            return new Set<T>(items);
        }

        /// <summary>
        /// Adds an element to the set.
        /// </summary>
        /// <param name="item">
        /// The object reference to be included in the set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference
        /// </exception>
        /// <exception cref="ArgumentException">
        /// item is already in the Set
        /// </exception>
        public void Add(object item)
        {
            try
            {
                this.hashtable.Add(item, null);
            }
            catch (ArgumentNullException e1)
            {
                throw e1;
            }
            catch (ArgumentException e2)
            {
                throw e2;
            }
        }

        /// <summary>
        /// Returns a copy of the Set. The elements in the Set are copied by-reference only.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Clone()
        {
            return new Set(this);
        }

        /// <summary>
        /// Determines whether the Set includes a specific element.
        /// </summary>
        /// <param name="item">
        /// The object reference to check for.
        /// </param>
        /// <returns>
        /// true if the Set includes item, false if it doesn't.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference.
        /// </exception>
        public bool Contains(object item)
        {
            try
            {
                return this.hashtable.ContainsKey(item);
            }
            catch (ArgumentNullException e1)
            {
                throw e1;
            }
        }

        /// <summary>
        /// Copies the Set elements to a one-dimensional Array instance at a specified index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional Array that is the destination of the objects copied from the Set. The Array
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// array is a null reference.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The array is not one-dimensional, or the array could not contain the objects copied
        ///     to it.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// The Array does not have enough space, starting from the given offset, to
        ///     contain all the Set's objects.
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            int i = index;

            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            foreach (object o in this)
            {
                try
                {
                    array.SetValue(o, i);
                }
                catch (ArgumentException e1)
                {
                    throw e1;
                }
                catch (IndexOutOfRangeException e2)
                {
                    throw e2;
                }

                ++i;
            }
        }

        /// <summary>
        ///     Returns an IEnumerator that can be used to enumerate through the items in the Set.
        /// </summary>
        /// <returns>An IEnumerator for the Set.</returns>
        public IEnumerator GetEnumerator()
        {
            return this.hashtable.Keys.GetEnumerator();
        }

        /// <summary>
        /// Removes an element from the set.
        /// </summary>
        /// <param name="item">
        /// The object reference to be excluded from the set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference
        /// </exception>
        public void Remove(object item)
        {
            try
            {
                this.hashtable.Remove(item);
            }
            catch (ArgumentNullException e1)
            {
                throw e1;
            }
        }

        /// <summary>
        ///     Copies the elements of the Set to a new generic array.
        /// </summary>
        /// <returns>An array of object references.</returns>
        public object[] ToArray()
        {
            var array = new object[this.Count];
            int index = 0;

            foreach (object o in this)
            {
                array[index] = o;
                ++index;
            }

            return array;
        }

        /// <summary>
        /// Copies the elements of the Set to a new array of the requested type.
        /// </summary>
        /// <param name="type">
        /// The Type of array to create and copy elements to.
        /// </param>
        /// <returns>
        /// An array of objects of the requested type.
        /// </returns>
        public Array ToArray(Type type)
        {
            Array array = Array.CreateInstance(type, this.Count);
            int index = 0;

            foreach (object o in this)
            {
                array.SetValue(o, index);
                ++index;
            }

            return array;
        }

        #endregion
    }

    /// <summary>
    /// Represents an enumerable collection of items. Each item can only be present
    ///     in the collection once. An item's identity is determined by a combination
    ///     of the return values from its GetHashCode and Equals methods.
    ///     This class is analagous to C++'s std::set template class.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [Serializable]
    public class Set<T> : ICloneable, ICollection<T>
    {
        #region Fields

        /// <summary>
        /// The dictionary.
        /// </summary>
        private Dictionary<T, object> dictionary;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class. 
        ///     Constructs an empty Set.
        /// </summary>
        public Set()
        {
            this.dictionary = new Dictionary<T, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class. 
        /// Constructs a Set with data copied from the given list.
        /// </summary>
        /// <param name="cloneMe">
        /// </param>
        public Set(IEnumerable<T> cloneMe)
        {
            this.dictionary = new Dictionary<T, object>();

            foreach (T theObject in cloneMe)
            {
                this.Add(theObject);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public Set(params T[] items)
            : this((IEnumerable<T>)items)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set{T}"/> class. 
        /// Constructs a copy of a Set.
        /// </summary>
        /// <param name="copyMe">
        /// The Set to copy from.
        /// </param>
        private Set(Set<T> copyMe)
        {
            this.dictionary = new Dictionary<T, object>(copyMe.dictionary);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a value indicating how many elements are contained within the Set.
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
                return false;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether or not the Set is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Gets an object that can be used to synchronize access to the Set.
        /// </summary>
        public object SyncRoot
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The are equal.
        /// </summary>
        /// <param name="set1">
        /// The set 1.
        /// </param>
        /// <param name="set2">
        /// The set 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool AreEqual(Set<T> set1, Set<T> set2)
        {
            if (set1.Count != set2.Count)
            {
                // Can't be equal if sizes are different
                return false;
            }

            if (set1.Count == 0)
            {
                // Empty sets are equal to each other. 
                // We know that set1.Count=set2.Count, so no need to check set2.Count for 0 as well.
                return true;
            }

            // At this point we know that either everything in set1 is in set2, or
            // that there is something in set1 which is not in set2.
            foreach (T item in set1)
            {
                if (!set2.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// The intersect.
        /// </summary>
        /// <param name="set1">
        /// The set 1.
        /// </param>
        /// <param name="set2">
        /// The set 2.
        /// </param>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public static Set<T> Intersect(Set<T> set1, Set<T> set2)
        {
            var intersection = new Set<T>();

            foreach (T item in set1)
            {
                if (set2.Contains(item))
                {
                    intersection.Add(item);
                }
            }

            return intersection;
        }

        /// <summary>
        /// The union.
        /// </summary>
        /// <param name="set1">
        /// The set 1.
        /// </param>
        /// <param name="set2">
        /// The set 2.
        /// </param>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public static Set<T> Union(Set<T> set1, Set<T> set2)
        {
            var union = new Set<T>(set1);

            foreach (T item in set2)
            {
                if (!union.Contains(item))
                {
                    union.Add(item);
                }
            }

            return union;
        }

        /// <summary>
        /// The without.
        /// </summary>
        /// <param name="withUs">
        /// The with us.
        /// </param>
        /// <param name="withoutUs">
        /// The without us.
        /// </param>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public static Set<T> Without(Set<T> withUs, Set<T> withoutUs)
        {
            var result = new Set<T>();

            foreach (T item in withUs)
            {
                if (!withoutUs.Contains(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Adds an element to the set.
        /// </summary>
        /// <param name="item">
        /// The object reference to be included in the set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference
        /// </exception>
        /// <exception cref="ArgumentException">
        /// item is already in the Set
        /// </exception>
        public void Add(T item)
        {
            try
            {
                this.dictionary.Add(item, null);
            }
            catch (ArgumentNullException e1)
            {
                throw e1;
            }
            catch (ArgumentException e2)
            {
                throw e2;
            }
        }

        /// <summary>
        /// The add range.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// The add range.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        public void AddRange(params T[] items)
        {
            this.AddRange((IEnumerable<T>)items);
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            this.dictionary = new Dictionary<T, object>();
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public Set<T> Clone()
        {
            return new Set<T>(this);
        }

        /// <summary>
        /// Determines whether the Set includes a specific element.
        /// </summary>
        /// <param name="item">
        /// The object reference to check for.
        /// </param>
        /// <returns>
        /// true if the Set includes item, false if it doesn't.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference.
        /// </exception>
        public bool Contains(T item)
        {
            try
            {
                return this.dictionary.ContainsKey(item);
            }
            catch (ArgumentNullException e1)
            {
                throw e1;
            }
        }

        /// <summary>
        /// Copies the Set elements to a one-dimensional Array instance at a specified index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional Array that is the destination of the objects copied from the Set. The Array
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// array is a null reference.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The array is not one-dimensional, or the array could not contain the objects copied
        ///     to it.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// The Array does not have enough space, starting from the given offset, to
        ///     contain all the Set's objects.
        /// </exception>
        public void CopyTo(T[] array, int index)
        {
            int i = index;

            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            foreach (T o in this)
            {
                try
                {
                    array.SetValue(o, i);
                }
                catch (ArgumentException e1)
                {
                    throw e1;
                }
                catch (IndexOutOfRangeException e2)
                {
                    throw e2;
                }

                ++i;
            }
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.dictionary.Keys.GetEnumerator();
        }

        /// <summary>
        /// The is equal to.
        /// </summary>
        /// <param name="set2">
        /// The set 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEqualTo(Set<T> set2)
        {
            return AreEqual(this, set2);
        }

        /// <summary>
        /// The is subset of.
        /// </summary>
        /// <param name="set2">
        /// The set 2.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSubsetOf(Set<T> set2)
        {
            foreach (T item in this)
            {
                if (!set2.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes an element from the set.
        /// </summary>
        /// <param name="item">
        /// The object reference to be excluded from the set.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// item is a null reference
        /// </exception>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Remove(T item)
        {
            try
            {
                this.dictionary.Remove(item);
                return true;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        /// <summary>
        ///     Copies the elements of the Set to a new generic array.
        /// </summary>
        /// <returns>An array of object references.</returns>
        public T[] ToArray()
        {
            var array = new T[this.Count];
            int index = 0;

            foreach (T o in this)
            {
                array[index] = o;
                ++index;
            }

            return array;
        }

        /// <summary>
        /// The without.
        /// </summary>
        /// <param name="withoutUs">
        /// The without us.
        /// </param>
        /// <returns>
        /// The <see cref="Set"/>.
        /// </returns>
        public Set<T> Without(Set<T> withoutUs)
        {
            return Without(this, withoutUs);
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Returns a copy of the Set. The elements in the Set are copied by-reference only.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        /// <summary>
        ///     Returns an IEnumerator that can be used to enumerate through the items in the Set.
        /// </summary>
        /// <returns>An IEnumerator for the Set.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dictionary.Keys.GetEnumerator();
        }

        #endregion
    }
}