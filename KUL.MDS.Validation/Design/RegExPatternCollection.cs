// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegExPatternCollection.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   RegExItemCollection allow RegExPattern to be bindable.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation.Design
{
    using System.Collections;

    /// <summary>
    /// RegExItemCollection allow RegExPattern to be bindable.
    /// </summary>
    public class RegExPatternCollection : CollectionBase
    {
        #region Public Indexers

        /// <summary>
        /// Get or set RegExPattern.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="RegExPattern"/>.
        /// </returns>
        public RegExPattern this[int index]
        {
            get
            {
                return (RegExPattern)this.List[index];
            }

            set
            {
                this.List[index] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Add new RegExPattern to collection.
        /// </summary>
        /// <param name="rePattern">
        /// </param>
        public void Add(RegExPattern rePattern)
        {
            this.List.Add(rePattern);
        }

        #endregion
    }
}