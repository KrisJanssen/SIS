// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="CollectionPlus.cs">
//   
// </copyright>
// <summary>
//   A collection base class containing basic extra functionality to be inherited
//   by , ,
//   .
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Collections;

    /// <summary>
    /// A collection base class containing basic extra functionality to be inherited
    /// by <see cref="CurveList"/>, <see cref="IPointList"/>,
    /// <see cref="GraphObjList"/>.
    /// </summary>
    /// <remarks>The methods in this collection operate on basic
    /// <see cref="object"/> types.  Therefore, in order to make sure that
    /// the derived classes remain strongly-typed, there are no Add() or
    /// Insert() methods here, and no methods that return an object.
    /// Only Remove(), Move(), IndexOf(), etc. methods are included.</remarks>
    /// 
    /// <author> John Champion</author>
    /// <version> $Revision: 3.8 $ $Date: 2006-06-24 20:26:43 $ </version>
    [Serializable]
    public class CollectionPlus : CollectionBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionPlus"/> class. 
        /// Default Constructor
        /// </summary>
        public CollectionPlus()
            : base()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Return the zero-based position index of the specified object
        /// in the collection.
        /// </summary>
        /// <param name="item">
        /// A reference to the object that is to be found.
        /// </param>
        /// <returns>
        /// The zero-based index of the specified object, or -1 if the
        /// object is not in the list
        /// </returns>
        /// <seealso cref="IList.IndexOf"/>
        public int IndexOf(object item)
        {
            return this.List.IndexOf(item);
        }

        /// <summary>
        /// Move the position of the object at the specified index
        /// to the new relative position in the list.
        /// </summary>
        /// <remarks>
        /// For Graphic type objects, this method controls the
        /// Z-Order of the items.  Objects at the beginning of the list
        /// appear in front of objects at the end of the list.
        /// </remarks>
        /// <param name="index">
        /// The zero-based index of the object
        /// to be moved.
        /// </param>
        /// <param name="relativePos">
        /// The relative number of positions to move
        /// the object.  A value of -1 will move the
        /// object one position earlier in the list, a value
        /// of 1 will move it one position later.  To move an item to the
        /// beginning of the list, use a large negative value (such as -999).
        /// To move it to the end of the list, use a large positive value.
        /// </param>
        /// <returns>
        /// The new position for the object, or -1 if the object
        /// was not found.
        /// </returns>
        public int Move(int index, int relativePos)
        {
            if (index < 0 || index >= this.List.Count)
            {
                return -1;
            }

            object obj = this.List[index];
            this.List.RemoveAt(index);
            index += relativePos;
            if (index < 0)
            {
                index = 0;
            }

            if (index > this.List.Count)
            {
                index = this.List.Count;
            }

            this.List.Insert(index, obj);
            return index;
        }

        /// <summary>
        /// Remove an object from the collection at the specified ordinal location.
        /// </summary>
        /// <param name="index">
        /// An ordinal position in the list at which the object to be removed 
        /// is located.
        /// </param>
        /// <seealso cref="IList.Remove"/>
        public void Remove(int index)
        {
            if (index >= 0 && index < this.List.Count)
            {
                this.List.RemoveAt(index);
            }
        }

        /// <summary>
        /// Remove an object from the collection based on an object reference.
        /// </summary>
        /// <param name="item">
        /// A reference to the object that is to be
        /// removed.
        /// </param>
        /// <seealso cref="IList.Remove"/>
        public void Remove(object item)
        {
            this.List.Remove(item);
        }

        #endregion

        /*	
	#region Serialization
		/// <summary>
		/// Current schema value that defines the version of the serialized file
		/// </summary>
		public const int schema = 1;

		/// <summary>
		/// Constructor for deserializing objects
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data
		/// </param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data
		/// </param>
		protected CollectionPlus( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			// The schema value is just a file version parameter.  You can use it to make future versions
			// backwards compatible as new member variables are added to classes
			int sch = info.GetInt32( "schema" );

		}
		/// <summary>
		/// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">A <see cref="SerializationInfo"/> instance that defines the serialized data</param>
		/// <param name="context">A <see cref="StreamingContext"/> instance that contains the serialized data</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			base.GetObjectData( info, context );

			info.AddValue( "schema", schema );
		}
	#endregion
*/
    }
}