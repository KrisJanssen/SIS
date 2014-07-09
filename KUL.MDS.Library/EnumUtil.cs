// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumUtil.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The enum util.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The enum util.
    /// </summary>
    public static class EnumUtil
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get values.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        #endregion
    }
}