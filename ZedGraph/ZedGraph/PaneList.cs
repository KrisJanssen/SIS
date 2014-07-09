// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PaneList.cs">
//   
// </copyright>
// <summary>
//   A collection class containing a list of  objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// A collection class containing a list of <see cref="GraphPane"/> objects.
    /// </summary>
    /// 
    /// <author>John Champion</author>
    /// <version> $Revision: 3.6 $ $Date: 2006-06-24 20:26:43 $ </version>
    [Serializable]
    public class PaneList : List<GraphPane>, ICloneable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 10;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PaneList"/> class. 
        /// Default constructor for the collection class.
        /// </summary>
        public PaneList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaneList"/> class. 
        /// The Copy Constructor
        /// </summary>
        /// <param name="rhs">
        /// The <see cref="PaneList"/> object from which to copy
        /// </param>
        public PaneList(PaneList rhs)
        {
            foreach (GraphPane item in rhs)
            {
                this.Add(item.Clone());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaneList"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected PaneList(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");
        }

        #endregion

        /*		/// <summary>
		/// Indexer to access the specified <see cref="GraphPane"/> object by
		/// its ordinal position in the list.
		/// </summary>
		/// <param name="index">The ordinal position (zero-based) of the
		/// <see cref="GraphPane"/> object to be accessed.</param>
		/// <value>A <see cref="GraphPane"/> object reference.</value>
		public GraphPane this[ int index ]  
		{
			get { return( (GraphPane) List[index] ); }
			set { List[index] = value; }
		}
*/
        #region Public Indexers

        /// <summary>
        /// Indexer to access the specified <see cref="GraphPane"/> object by
        /// its <see cref="PaneBase.Title"/> string.
        /// </summary>
        /// <param name="title">
        /// The string title of the
        /// <see cref="GraphPane"/> object to be accessed.
        /// </param>
        /// <value>
        /// A <see cref="GraphPane"/> object reference.
        /// </value>
        /// <returns>
        /// The <see cref="GraphPane"/>.
        /// </returns>
        public GraphPane this[string title]
        {
            get
            {
                int index = this.IndexOf(title);
                if (index >= 0)
                {
                    return (GraphPane)this[index];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public PaneList Clone()
        {
            return new PaneList(this);
        }

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> instance with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("schema", schema);
        }

        /*
		/// <summary>
		/// Add a <see cref="GraphPane"/> object to the collection at the end of the list.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object to
		/// be added</param>
		/// <seealso cref="IList.Add"/>
		public void Add( GraphPane pane )
		{
			List.Add( pane );
		}

		/// <summary>
		/// Remove a <see cref="GraphPane"/> object from the collection based on an object reference.
		/// </summary>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is to be
		/// removed.</param>
		/// <seealso cref="IList.Remove"/>
		public void Remove( GraphPane pane )
		{
			List.Remove( pane );
		}

		/// <summary>
		/// Insert a <see cref="GraphPane"/> object into the collection at the specified
		/// zero-based index location.
		/// </summary>
		/// <param name="index">The zero-based index location for insertion.</param>
		/// <param name="pane">A reference to the <see cref="GraphPane"/> object that is to be
		/// inserted.</param>
		/// <seealso cref="IList.Insert"/>
		public void Insert( int index, GraphPane pane )
		{
			List.Insert( index, pane );
		}
*/

        /// <summary>
        /// Return the zero-based position index of the
        /// <see cref="GraphPane"/> with the specified <see cref="PaneBase.Title"/>.
        /// </summary>
        /// <remarks>
        /// The comparison of titles is not case sensitive, but it must include
        /// all characters including punctuation, spaces, etc.
        /// </remarks>
        /// <param name="title">
        /// The <see cref="String"/> label that is in the
        /// <see cref="PaneBase.Title"/> attribute of the item to be found.
        /// </param>
        /// <returns>
        /// The zero-based index of the specified <see cref="GraphPane"/>,
        /// or -1 if the <see cref="PaneBase.Title"/> was not found in the list
        /// </returns>
        /// <seealso cref="IndexOfTag"/>
        public int IndexOf(string title)
        {
            int index = 0;
            foreach (GraphPane pane in this)
            {
                if (string.Compare(pane.Title.Text, title, true) == 0)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Return the zero-based position index of the
        /// <see cref="GraphPane"/> with the specified <see cref="PaneBase.Tag"/>.
        /// </summary>
        /// <remarks>
        /// In order for this method to work, the <see cref="PaneBase.Tag"/>
        /// property must be of type <see cref="String"/>.
        /// </remarks>
        /// <param name="tagStr">
        /// The <see cref="String"/> tag that is in the
        /// <see cref="PaneBase.Tag"/> attribute of the item to be found.
        /// </param>
        /// <returns>
        /// The zero-based index of the specified <see cref="GraphPane"/>,
        /// or -1 if the <see cref="PaneBase.Tag"/> string is not in the list
        /// </returns>
        public int IndexOfTag(string tagStr)
        {
            int index = 0;
            foreach (GraphPane pane in this)
            {
                if (pane.Tag is string && string.Compare((string)pane.Tag, tagStr, true) == 0)
                {
                    return index;
                }

                index++;
            }

            return -1;
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
    }
}