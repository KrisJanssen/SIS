// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pair.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The pair.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    using System;

    /// <summary>
    /// The pair.
    /// </summary>
    public static class Pair
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="U">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Pair"/>.
        /// </returns>
        public static Pair<T, U> Create<T, U>(T first, U second)
        {
            return new Pair<T, U>(first, second);
        }

        #endregion
    }

    /// <summary>
    /// The pair.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="U">
    /// </typeparam>
    [Serializable]
    public struct Pair<T, U>
    {
        #region Fields

        /// <summary>
        /// The first.
        /// </summary>
        private readonly T first;

        /// <summary>
        /// The second.
        /// </summary>
        private readonly U second;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair{T,U}"/> struct.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        public Pair(T first, U second)
        {
            this.first = first;
            this.second = second;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the first.
        /// </summary>
        public T First
        {
            get
            {
                return this.first;
            }
        }

        /// <summary>
        /// Gets the second.
        /// </summary>
        public U Second
        {
            get
            {
                return this.second;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(Pair<T, U> lhs, Pair<T, U> rhs)
        {
            bool firstEqual;
            bool secondEqual;

            if (ReferenceEquals(lhs.First, null) && ReferenceEquals(rhs.First, null))
            {
                firstEqual = true;
            }
            else if (ReferenceEquals(lhs.First, null) || ReferenceEquals(rhs.First, null))
            {
                firstEqual = false;
            }
            else
            {
                firstEqual = lhs.First.Equals(rhs.First);
            }

            if (ReferenceEquals(lhs.Second, null) && ReferenceEquals(rhs.Second, null))
            {
                secondEqual = true;
            }
            else if (ReferenceEquals(lhs.Second, null) || ReferenceEquals(rhs.Second, null))
            {
                secondEqual = false;
            }
            else
            {
                secondEqual = lhs.Second.Equals(rhs.Second);
            }

            return firstEqual && secondEqual;
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(Pair<T, U> lhs, Pair<T, U> rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (obj != null) && (obj is Pair<T, U>) && (this == (Pair<T, U>)obj);
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            int firstHash;
            int secondHash;

            if (ReferenceEquals(this.first, null))
            {
                firstHash = 0;
            }
            else
            {
                firstHash = this.first.GetHashCode();
            }

            if (ReferenceEquals(this.second, null))
            {
                secondHash = 0;
            }
            else
            {
                secondHash = this.second.GetHashCode();
            }

            return firstHash ^ secondHash;
        }

        #endregion
    }
}