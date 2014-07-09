// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ScrollRangeList.cs">
//   
// </copyright>
// <summary>
//   A collection class containing a list of <see cref="ScrollRange" /> objects.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection class containing a list of <see cref="ScrollRange"/> objects.
    /// </summary>
    /// 
    /// <author>John Champion</author>
    /// <version> $Revision: 3.3 $ $Date: 2006-06-24 20:26:43 $ </version>
    public class ScrollRangeList : List<ScrollRange>, ICloneable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollRangeList"/> class. 
        /// Default constructor for the collection class.
        /// </summary>
        public ScrollRangeList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollRangeList"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="ScrollRangeList"/> object from which to copy
        /// </param>
        public ScrollRangeList(ScrollRangeList rhs)
        {
            foreach (ScrollRange item in rhs)
            {
                this.Add(new ScrollRange(item));
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Indexer to access the specified <see cref="ScrollRange"/> object by
        /// its ordinal position in the list.
        /// </summary>
        /// <param name="index">
        /// The ordinal position (zero-based) of the
        /// <see cref="ScrollRange"/> object to be accessed.
        /// </param>
        /// <value>
        /// A <see cref="ScrollRange"/> object instance
        /// </value>
        /// <returns>
        /// The <see cref="ScrollRange"/>.
        /// </returns>
        public new ScrollRange this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                {
                    return new ScrollRange(false);
                }
                else
                {
                    return (ScrollRange)base[index];
                }
            }

            set
            {
                base[index] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public ScrollRangeList Clone()
        {
            return new ScrollRangeList(this);
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// Implement the <see cref="ICloneable" /> interface in a typesafe manner by just
        /// calling the typed version of <see cref="Clone" />
        /// </summary>
        /// <returns>A deep copy of this object</returns>
        object ICloneable.Clone()
        {
            return this.Clone();
        }

        #endregion

        /*		/// <summary>
				/// Add a <see cref="ScrollRange"/> object to the collection at the end of the list.
				/// </summary>
				/// <param name="item">The <see cref="ScrollRange"/> object to be added</param>
				/// <seealso cref="IList.Add"/>
				public int Add( ScrollRange item )
				{
					return List.Add( item );
				}
				/// <summary>
				/// Insert a <see cref="ScrollRange"/> object into the collection at the specified
				/// zero-based index location.
				/// </summary>
				/// <param name="index">The zero-based index location for insertion.</param>
				/// <param name="item">The <see cref="ScrollRange"/> object that is to be
				/// inserted.</param>
				/// <seealso cref="IList.Insert"/>
				public void Insert( int index, ScrollRange item )
				{
					List.Insert( index, item );
				}
		*/
    }
}