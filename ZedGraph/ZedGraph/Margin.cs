// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Margin.cs">
//   
// </copyright>
// <summary>
//   Class that handles that stores the margin properties for the GraphPane
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Class that handles that stores the margin properties for the GraphPane
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.1 $ $Date: 2006-06-24 20:26:44 $ </version>
    [Serializable]
    public class Margin : ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 10;

        #endregion

        #region Fields

        /// <summary>
        /// Private fields that store the size of the margin around the edge of the pane which will be
        /// kept blank.  Use the public properties <see cref="Margin.Left"/>, <see cref="Margin.Right"/>,
        /// <see cref="Margin.Top"/>, <see cref="Margin.Bottom"/> to access these values.
        /// </summary>
        /// <value>Units are points (1/72 inch)</value>
        protected float _bottom;

        /// <summary>
        /// Private fields that store the size of the margin around the edge of the pane which will be
        /// kept blank.  Use the public properties <see cref="Margin.Left"/>, <see cref="Margin.Right"/>,
        /// <see cref="Margin.Top"/>, <see cref="Margin.Bottom"/> to access these values.
        /// </summary>
        /// <value>Units are points (1/72 inch)</value>
        protected float _left;

        /// <summary>
        /// Private fields that store the size of the margin around the edge of the pane which will be
        /// kept blank.  Use the public properties <see cref="Margin.Left"/>, <see cref="Margin.Right"/>,
        /// <see cref="Margin.Top"/>, <see cref="Margin.Bottom"/> to access these values.
        /// </summary>
        /// <value>Units are points (1/72 inch)</value>
        protected float _right;

        /// <summary>
        /// Private fields that store the size of the margin around the edge of the pane which will be
        /// kept blank.  Use the public properties <see cref="Margin.Left"/>, <see cref="Margin.Right"/>,
        /// <see cref="Margin.Top"/>, <see cref="Margin.Bottom"/> to access these values.
        /// </summary>
        /// <value>Units are points (1/72 inch)</value>
        protected float _top;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Margin"/> class. 
        /// Constructor to build a <see cref="Margin"/> from the default values.
        /// </summary>
        public Margin()
        {
            this._left = Default.Left;
            this._right = Default.Right;
            this._top = Default.Top;
            this._bottom = Default.Bottom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Margin"/> class. 
        /// Copy constructor
        /// </summary>
        /// <param name="rhs">
        /// the <see cref="Margin"/> instance to be copied.
        /// </param>
        public Margin(Margin rhs)
        {
            this._left = rhs._left;
            this._right = rhs._right;
            this._top = rhs._top;
            this._bottom = rhs._bottom;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Margin"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected Margin(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._left = info.GetSingle("left");
            this._right = info.GetSingle("right");
            this._top = info.GetSingle("top");
            this._bottom = info.GetSingle("bottom");
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Concurrently sets all outer margin values to a single value.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Bottom"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        public float All
        {
            set
            {
                this._bottom = value;
                this._top = value;
                this._left = value;
                this._right = value;
            }
        }

        /// <summary>
        /// Gets or sets a float value that determines the margin area between the bottom edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Bottom"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        public float Bottom
        {
            get
            {
                return this._bottom;
            }

            set
            {
                this._bottom = value;
            }
        }

        /// <summary>
        /// Gets or sets a float value that determines the margin area between the left edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Left"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Top"/>
        /// <seealso cref="Bottom"/>
        public float Left
        {
            get
            {
                return this._left;
            }

            set
            {
                this._left = value;
            }
        }

        /// <summary>
        /// Gets or sets a float value that determines the margin area between the right edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Right"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Top"/>
        /// <seealso cref="Bottom"/>
        public float Right
        {
            get
            {
                return this._right;
            }

            set
            {
                this._right = value;
            }
        }

        /// <summary>
        /// Gets or sets a float value that determines the margin area between the top edge of the
        /// <see cref="PaneBase.Rect"/> rectangle and the features of the graph.
        /// </summary>
        /// <value>This value is in units of points (1/72 inch), and is scaled
        /// linearly with the graph size.</value>
        /// <seealso cref="Default.Top"/>
        /// <seealso cref="PaneBase.IsFontsScaled"/>
        /// <seealso cref="Left"/>
        /// <seealso cref="Right"/>
        /// <seealso cref="Bottom"/>
        public float Top
        {
            get
            {
                return this._top;
            }

            set
            {
                this._top = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public Margin Clone()
        {
            return new Margin(this);
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

            info.AddValue("left", this._left);
            info.AddValue("right", this._right);
            info.AddValue("top", this._top);
            info.AddValue("bottom", this._bottom);
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

        /// <summary>
        /// A simple struct that defines the default property values for the <see cref="Margin"/> class.
        /// </summary>
        public class Default
        {
            #region Static Fields

            /// <summary>
            /// The default value for the <see cref="Margin.Bottom"/> property, which is
            /// the size of the space on the bottom side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Bottom = 10.0F;

            /// <summary>
            /// The default value for the <see cref="Margin.Left"/> property, which is
            /// the size of the space on the left side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Left = 10.0F;

            /// <summary>
            /// The default value for the <see cref="Margin.Right"/> property, which is
            /// the size of the space on the right side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Right = 10.0F;

            /// <summary>
            /// The default value for the <see cref="Margin.Top"/> property, which is
            /// the size of the space on the top side of the <see cref="PaneBase.Rect"/>.
            /// </summary>
            /// <value>Units are points (1/72 inch)</value>
            public static float Top = 10.0F;

            #endregion
        }
    }
}