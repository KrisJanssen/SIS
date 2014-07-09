// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MinorGrid.cs">
//   
// </copyright>
// <summary>
//   Class that holds the specific properties for the minor grid.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Class that holds the specific properties for the minor grid.
    /// </summary>
    /// <author> John Champion </author>
    /// <version> $Revision: 3.1 $ $Date: 2006-06-24 20:26:44 $ </version>
    [Serializable]
    public class MinorGrid : ICloneable, ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 10;

        #endregion

        #region Fields

        /// <summary>
        /// The _color.
        /// </summary>
        internal Color _color;

        /// <summary>
        /// The _dash off.
        /// </summary>
        internal float _dashOff;

        /// <summary>
        /// The _dash on.
        /// </summary>
        internal float _dashOn;

        /// <summary>
        /// The _is visible.
        /// </summary>
        internal bool _isVisible;

        /// <summary>
        /// The _pen width.
        /// </summary>
        internal float _penWidth;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MinorGrid"/> class. 
        /// Default constructor
        /// </summary>
        public MinorGrid()
        {
            this._dashOn = Default.DashOn;
            this._dashOff = Default.DashOff;
            this._penWidth = Default.PenWidth;
            this._isVisible = Default.IsVisible;
            this._color = Default.Color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinorGrid"/> class. 
        /// Copy constructor
        /// </summary>
        /// <param name="rhs">
        /// The source <see cref="MinorGrid"/> to be copied.
        /// </param>
        public MinorGrid(MinorGrid rhs)
        {
            this._dashOn = rhs._dashOn;
            this._dashOff = rhs._dashOff;
            this._penWidth = rhs._penWidth;

            this._isVisible = rhs._isVisible;

            this._color = rhs._color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinorGrid"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains the serialized data
        /// </param>
        protected MinorGrid(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._isVisible = info.GetBoolean("isVisible");

            this._dashOn = info.GetSingle("dashOn");
            this._dashOff = info.GetSingle("dashOff");
            this._penWidth = info.GetSingle("penWidth");

            this._color = (Color)info.GetValue("color", typeof(Color));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The color to use for drawing this <see cref="Axis"/> grid.
        /// </summary>
        /// <value> The color is defined using the
        /// <see cref="System.Drawing.Color"/> class</value>
        /// <seealso cref="Default.Color"/>.
        /// <seealso cref="PenWidth"/>
        public Color Color
        {
            get
            {
                return this._color;
            }

            set
            {
                this._color = value;
            }
        }

        /// <summary>
        /// The "Dash Off" mode for drawing the grid.
        /// </summary>
        /// <remarks>
        /// This is the distance,
        /// in points (1/72 inch), of the spaces between the dash segments that make up
        /// the dashed grid lines.
        /// </remarks>
        /// <value>The dash off length is defined in points (1/72 inch)</value>
        /// <seealso cref="DashOn"/>
        /// <seealso cref="IsVisible"/>
        /// <seealso cref="Default.DashOff"/>.
        public float DashOff
        {
            get
            {
                return this._dashOff;
            }

            set
            {
                this._dashOff = value;
            }
        }

        /// <summary>
        /// The "Dash On" mode for drawing the grid.
        /// </summary>
        /// <remarks>
        /// This is the distance,
        /// in points (1/72 inch), of the dash segments that make up the dashed grid lines.
        /// </remarks>
        /// <value>The dash on length is defined in points (1/72 inch)</value>
        /// <seealso cref="DashOff"/>
        /// <seealso cref="IsVisible"/>
        /// <seealso cref="Default.DashOn"/>.
        public float DashOn
        {
            get
            {
                return this._dashOn;
            }

            set
            {
                this._dashOn = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines if the major <see cref="Axis"/> gridlines
        /// (at each labeled value) will be visible
        /// </summary>
        /// <value>true to show the gridlines, false otherwise</value>
        /// <seealso cref="Default.IsVisible">Default.IsShowGrid</seealso>.
        /// <seealso cref="Color"/>
        /// <seealso cref="PenWidth"/>
        /// <seealso cref="DashOn"/>
        /// <seealso cref="DashOff"/>
        /// <seealso cref="IsVisible"/>
        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }

            set
            {
                this._isVisible = value;
            }
        }

        /// <summary>
        /// The pen width used for drawing the grid lines.
        /// </summary>
        /// <value>The grid pen width is defined in points (1/72 inch)</value>
        /// <seealso cref="IsVisible"/>
        /// <seealso cref="Default.PenWidth"/>.
        /// <seealso cref="Color"/>
        public float PenWidth
        {
            get
            {
                return this._penWidth;
            }

            set
            {
                this._penWidth = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Typesafe, deep-copy clone method.
        /// </summary>
        /// <returns>A new, independent copy of this class</returns>
        public MinorGrid Clone()
        {
            return new MinorGrid(this);
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

            info.AddValue("isVisible", this._isVisible);

            info.AddValue("dashOn", this._dashOn);
            info.AddValue("dashOff", this._dashOff);
            info.AddValue("penWidth", this._penWidth);

            info.AddValue("color", this._color);
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

        #region Methods

        /// <summary>
        /// The draw.
        /// </summary>
        /// <param name="g">
        /// The g.
        /// </param>
        /// <param name="pen">
        /// The pen.
        /// </param>
        /// <param name="pixVal">
        /// The pix val.
        /// </param>
        /// <param name="topPix">
        /// The top pix.
        /// </param>
        internal void Draw(Graphics g, Pen pen, float pixVal, float topPix)
        {
            // draw the minor grid
            if (this._isVisible)
            {
                g.DrawLine(pen, pixVal, 0.0F, pixVal, topPix);
            }
        }

        /// <summary>
        /// The get pen.
        /// </summary>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="scaleFactor">
        /// The scale factor.
        /// </param>
        /// <returns>
        /// The <see cref="Pen"/>.
        /// </returns>
        internal Pen GetPen(GraphPane pane, float scaleFactor)
        {
            Pen pen = new Pen(this._color, pane.ScaledPenWidth(this._penWidth, scaleFactor));

            if (this._dashOff > 1e-10 && this._dashOn > 1e-10)
            {
                pen.DashStyle = DashStyle.Custom;
                float[] pattern = new float[2];
                pattern[0] = this._dashOn;
                pattern[1] = this._dashOff;
                pen.DashPattern = pattern;
            }

            return pen;
        }

        #endregion

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="MinorGrid"/> class.
        /// </summary>
        public struct Default
        {
            #region Static Fields

            /// <summary>
            /// The default color for the <see cref="Axis"/> minor grid lines
            /// (<see cref="MinorGrid.Color"/> property).  This color only affects the
            /// minor grid lines.
            /// </summary>
            public static Color Color = Color.Gray;

            /// <summary>
            /// The default "dash off" size for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="MinorGrid.DashOff"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float DashOff = 10.0F;

            /// <summary>
            /// The default "dash on" size for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="MinorGrid.DashOn"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float DashOn = 1.0F;

            /// <summary>
            /// The default display mode for the <see cref="Axis"/> minor grid lines
            /// (<see cref="MinorGrid.IsVisible"/> property). true
            /// to show the minor grid lines, false to hide them.
            /// </summary>
            public static bool IsVisible = false;

            /// <summary>
            /// The default pen width for drawing the <see cref="Axis"/> minor grid
            /// (<see cref="MinorGrid.PenWidth"/> property). Units are in points (1/72 inch).
            /// </summary>
            public static float PenWidth = 1.0F;

            #endregion
        }
    }
}