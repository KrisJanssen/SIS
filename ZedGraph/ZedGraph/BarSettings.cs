// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="BarSettings.cs">
//   
// </copyright>
// <summary>
//   Class that handles the global settings for bar charts
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Class that handles the global settings for bar charts
    /// </summary>
    /// 
    /// <author> John Champion </author>
    /// <version> $Revision: 3.6 $ $Date: 2007-12-30 23:27:39 $ </version>
    [Serializable]
    public class BarSettings : ISerializable
    {
        #region Constants

        /// <summary>
        /// Current schema value that defines the version of the serialized file
        /// </summary>
        public const int schema = 10;

        #endregion

        #region Fields

        /// <summary>Private field that determines the width of a bar cluster (for bar charts)
        /// in user scale units.  Normally, this value is 1.0 because bar charts are typically
        /// <see cref="AxisType.Ordinal"/> or <see cref="AxisType.Text"/>, and the bars are
        /// defined at ordinal values (1.0 scale units apart).  For <see cref="AxisType.Linear"/>
        /// or other scale types, you can use this value to scale the bars to an arbitrary
        /// user scale. Use the public property <see cref="ClusterScaleWidth"/> to access this
        /// value. </summary>
        internal double _clusterScaleWidth;

        /// <summary>
        /// Private field that determines if the <see cref="ClusterScaleWidth" /> will be
        /// calculated automatically.  Use the public property <see cref="ClusterScaleWidthAuto" />
        /// to access this value.
        /// </summary>
        internal bool _clusterScaleWidthAuto;

        /// <summary>
        /// private field that stores the owner GraphPane that contains this BarSettings instance.
        /// </summary>
        internal GraphPane _ownerPane;

        /// <summary>Private field that determines the base axis from which <see cref="Bar"/>
        /// graphs will be displayed.  The base axis is the axis from which the bars grow with
        /// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
        /// To access this value, use the public property <see cref="Base"/>.
        /// </summary>
        /// <seealso cref="Default.Base"/>
        private BarBase _base;

        /// <summary>Private field that determines the size of the gap between individual bars
        /// within a bar cluster for bar charts.  This gap is expressed as a fraction of the
        /// bar size (1.0 means leave a 1-barwidth gap between each bar).
        /// Use the public property <see cref="MinBarGap"/> to access this value. </summary>
        private float _minBarGap;

        /// <summary>Private field that determines the size of the gap between bar clusters
        /// for bar charts.  This gap is expressed as a fraction of the bar size (1.0 means
        /// leave a 1-barwidth gap between clusters).
        /// Use the public property <see cref="MinClusterGap"/> to access this value. </summary>
        private float _minClusterGap;

        /// <summary>Private field that determines how the <see cref="BarItem"/>
        /// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
        /// for the individual types available.
        /// To access this value, use the public property <see cref="Type"/>.
        /// </summary>
        /// <seealso cref="Default.Type"/>
        private BarType _type;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarSettings"/> class. 
        /// Constructor to build a <see cref="BarSettings"/> instance from the defaults.
        /// </summary>
        /// <param name="parentPane">
        /// The parent Pane.
        /// </param>
        public BarSettings(GraphPane parentPane)
        {
            this._minClusterGap = Default.MinClusterGap;
            this._minBarGap = Default.MinBarGap;
            this._clusterScaleWidth = Default.ClusterScaleWidth;
            this._clusterScaleWidthAuto = Default.ClusterScaleWidthAuto;
            this._base = Default.Base;
            this._type = Default.Type;

            this._ownerPane = parentPane;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarSettings"/> class. 
        /// Copy constructor
        /// </summary>
        /// <param name="rhs">
        /// the <see cref="BarSettings"/> instance to be copied.
        /// </param>
        /// <param name="parentPane">
        /// The <see cref="GraphPane"/> that will be the
        /// parent of this new BarSettings object.
        /// </param>
        public BarSettings(BarSettings rhs, GraphPane parentPane)
        {
            this._minClusterGap = rhs._minClusterGap;
            this._minBarGap = rhs._minBarGap;
            this._clusterScaleWidth = rhs._clusterScaleWidth;
            this._clusterScaleWidthAuto = rhs._clusterScaleWidthAuto;
            this._base = rhs._base;
            this._type = rhs._type;

            this._ownerPane = parentPane;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarSettings"/> class. 
        /// Constructor for deserializing objects
        /// </summary>
        /// <remarks>
        /// You MUST set the _ownerPane property after deserializing a BarSettings object.
        /// </remarks>
        /// <param name="info">
        /// A <see cref="SerializationInfo"/> instance that defines the
        /// serialized data
        /// </param>
        /// <param name="context">
        /// A <see cref="StreamingContext"/> instance that contains
        /// the serialized data
        /// </param>
        internal BarSettings(SerializationInfo info, StreamingContext context)
        {
            // The schema value is just a file version parameter.  You can use it to make future versions
            // backwards compatible as new member variables are added to classes
            int sch = info.GetInt32("schema");

            this._minClusterGap = info.GetSingle("minClusterGap");
            this._minBarGap = info.GetSingle("minBarGap");
            this._clusterScaleWidth = info.GetDouble("clusterScaleWidth");
            this._clusterScaleWidthAuto = info.GetBoolean("clusterScaleWidthAuto");
            this._base = (BarBase)info.GetValue("base", typeof(BarBase));
            this._type = (BarType)info.GetValue("type", typeof(BarType));
        }

        #endregion

        #region Public Properties

        /// <summary>Determines the base axis from which <see cref="Bar"/>
        /// graphs will be displayed.
        /// </summary>
        /// <remarks>The base axis is the axis from which the bars grow with
        /// increasing value. The value is of the enumeration type <see cref="ZedGraph.BarBase"/>.
        /// </remarks>
        /// <seealso cref="Default.Base"/>
        public BarBase Base
        {
            get
            {
                return this._base;
            }

            set
            {
                this._base = value;
            }
        }

        /// <summary>
        /// The width of an individual bar cluster on a <see cref="Bar"/> graph.
        /// This value only applies to bar graphs plotted on non-ordinal X axis
        /// types (<see cref="AxisType.Linear"/>, <see cref="AxisType.Log"/>, and
        /// <see cref="AxisType.Date"/>.
        /// </summary>
        /// <remarks>
        /// This value can be calculated automatically if <see cref="ClusterScaleWidthAuto" />
        /// is set to true.  In this case, ClusterScaleWidth will be calculated if
        /// <see cref="Base" /> refers to an <see cref="Axis" /> of a non-ordinal type
        /// (<see cref="Scale.IsAnyOrdinal" /> is false).  The ClusterScaleWidth is calculated
        /// from the minimum difference found between any two points on the <see cref="Base" />
        /// <see cref="Axis" /> for any <see cref="BarItem" /> in the
        /// <see cref="GraphPane.CurveList" />.  The ClusterScaleWidth is set automatically
        /// each time <see cref="GraphPane.AxisChange()" /> is called.  Calculations are
        /// done by the <see cref="BarSettings.CalcClusterScaleWidth" /> method.
        /// </remarks>
        /// <seealso cref="Default.ClusterScaleWidth"/>
        /// <seealso cref="ClusterScaleWidthAuto"/>
        /// <seealso cref="MinBarGap"/>
        /// <seealso cref="MinClusterGap"/>
        public double ClusterScaleWidth
        {
            get
            {
                return this._clusterScaleWidth;
            }

            set
            {
                this._clusterScaleWidth = value;
                this._clusterScaleWidthAuto = false;
            }
        }

        /// <summary>
        /// Gets or sets a property that determines if the <see cref="ClusterScaleWidth" /> will be
        /// calculated automatically.
        /// </summary>
        /// <remarks>true for the <see cref="ClusterScaleWidth" /> to be calculated
        /// automatically based on the available data, false otherwise.  This value will
        /// be set to false automatically if the <see cref="ClusterScaleWidth" /> value
        /// is changed by the user.
        /// </remarks>
        /// <seealso cref="Default.ClusterScaleWidthAuto"/>
        /// <seealso cref="ClusterScaleWidth"/>
        public bool ClusterScaleWidthAuto
        {
            get
            {
                return this._clusterScaleWidthAuto;
            }

            set
            {
                this._clusterScaleWidthAuto = value;
            }
        }

        /// <summary>
        /// The minimum space between individual <see cref="Bar">Bars</see>
        /// within a cluster, expressed as a
        /// fraction of the bar size.
        /// </summary>
        /// <seealso cref="Default.MinBarGap"/>
        /// <seealso cref="MinClusterGap"/>
        /// <seealso cref="ClusterScaleWidth"/>
        public float MinBarGap
        {
            get
            {
                return this._minBarGap;
            }

            set
            {
                this._minBarGap = value;
            }
        }

        /// <summary>
        /// The minimum space between <see cref="Bar"/> clusters, expressed as a
        /// fraction of the bar size.
        /// </summary>
        /// <seealso cref="Default.MinClusterGap"/>
        /// <seealso cref="MinBarGap"/>
        /// <seealso cref="ClusterScaleWidth"/>
        public float MinClusterGap
        {
            get
            {
                return this._minClusterGap;
            }

            set
            {
                this._minClusterGap = value;
            }
        }

        /// <summary>Determines how the <see cref="BarItem"/>
        /// graphs will be displayed. See the <see cref="ZedGraph.BarType"/> enum
        /// for the individual types available.
        /// </summary>
        /// <seealso cref="Default.Type"/>
        public BarType Type
        {
            get
            {
                return this._type;
            }

            set
            {
                this._type = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determine the <see cref="Axis"/> from which the <see cref="Bar"/> charts are based.
        /// </summary>
        /// <seealso cref="ZedGraph.BarBase"/>
        /// <seealso cref="BarSettings"/>
        /// <seealso cref="ZedGraph.BarSettings.Base"/>
        /// <seealso cref="Scale.GetClusterWidth(GraphPane)"/>
        /// <returns>The <see cref="Axis"/> class for the axis from which the bars are based</returns>
        public Axis BarBaseAxis()
        {
            Axis barAxis;
            if (this._base == BarBase.Y)
            {
                barAxis = this._ownerPane.YAxis;
            }
            else if (this._base == BarBase.Y2)
            {
                barAxis = this._ownerPane.Y2Axis;
            }
            else if (this._base == BarBase.X2)
            {
                barAxis = this._ownerPane.X2Axis;
            }
            else
            {
                barAxis = this._ownerPane.XAxis;
            }

            return barAxis;
        }

        /// <summary>
        /// Calculate the width of an individual bar cluster on a <see cref="BarItem"/> graph.
        /// This value only applies to bar graphs plotted on non-ordinal X axis
        /// types (<see cref="Scale.IsAnyOrdinal" /> is false).
        /// </summary>
        /// <remarks>
        /// This value can be calculated automatically if <see cref="ClusterScaleWidthAuto" />
        /// is set to true.  In this case, ClusterScaleWidth will be calculated if
        /// <see cref="Base" /> refers to an <see cref="Axis" /> of a non-ordinal type
        /// (<see cref="Scale.IsAnyOrdinal" /> is false).  The ClusterScaleWidth is calculated
        /// from the minimum difference found between any two points on the <see cref="Base" />
        /// <see cref="Axis" /> for any <see cref="BarItem" /> in the
        /// <see cref="GraphPane.CurveList" />.  The ClusterScaleWidth is set automatically
        /// each time <see cref="GraphPane.AxisChange()" /> is called.
        /// </remarks>
        /// <seealso cref="Default.ClusterScaleWidth"/>
        /// <seealso cref="ClusterScaleWidthAuto"/>
        /// <seealso cref="MinBarGap"/>
        /// <seealso cref="MinClusterGap"/>
        public void CalcClusterScaleWidth()
        {
            Axis baseAxis = this.BarBaseAxis();

            // First, calculate the clusterScaleWidth for BarItem objects
            if (this._clusterScaleWidthAuto && !baseAxis.Scale.IsAnyOrdinal)
            {
                double minStep = double.MaxValue;

                foreach (CurveItem curve in this._ownerPane.CurveList)
                {
                    IPointList list = curve.Points;

                    if (curve is BarItem)
                    {
                        double step = GetMinStepSize(curve.Points, baseAxis);
                        minStep = step < minStep ? step : minStep;
                    }
                }

                if (minStep == double.MaxValue)
                {
                    minStep = 1.0;
                }

                this._clusterScaleWidth = minStep;
            }

            // Second, calculate the sizes of any HiLowBarItem and JapaneseCandleStickItem objects
            foreach (CurveItem curve in this._ownerPane.CurveList)
            {
                IPointList list = curve.Points;

                // 				if ( curve is HiLowBarItem &&
                // 						(curve as HiLowBarItem).Bar.IsAutoSize )
                // 				{
                // 					( curve as HiLowBarItem ).Bar._userScaleSize =
                // 								GetMinStepSize( list, baseAxis );
                // 				}
                // 				else if ( curve is JapaneseCandleStickItem &&
                if (curve is JapaneseCandleStickItem && (curve as JapaneseCandleStickItem).Stick.IsAutoSize)
                {
                    (curve as JapaneseCandleStickItem).Stick._userScaleSize = GetMinStepSize(list, baseAxis);
                }
            }
        }

        /// <summary>
        /// Determine the width, in screen pixel units, of each bar cluster including
        /// the cluster gaps and bar gaps.
        /// </summary>
        /// <remarks>This method calls the <see cref="Scale.GetClusterWidth(GraphPane)"/>
        /// method for the base <see cref="Axis"/> for <see cref="Bar"/> graphs
        /// (the base <see cref="Axis"/> is assigned by the <see cref="ZedGraph.BarSettings.Base"/>
        /// property).
        /// </remarks>
        /// <seealso cref="ZedGraph.BarBase"/>
        /// <seealso cref="ZedGraph.BarSettings"/>
        /// <seealso cref="Scale.GetClusterWidth(GraphPane)"/>
        /// <seealso cref="ZedGraph.BarSettings.Type"/>
        /// <returns>The width of each bar cluster, in pixel units</returns>
        public float GetClusterWidth()
        {
            return this.BarBaseAxis()._scale.GetClusterWidth(this._ownerPane);
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

            info.AddValue("minClusterGap", this._minClusterGap);
            info.AddValue("minBarGap", this._minBarGap);
            info.AddValue("clusterScaleWidth", this._clusterScaleWidth);
            info.AddValue("clusterScaleWidthAuto", this._clusterScaleWidthAuto);
            info.AddValue("base", this._base);
            info.AddValue("type", this._type);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determine the minimum increment between individual points to be used for
        /// calculating a bar size that fits without overlapping
        /// </summary>
        /// <param name="list">
        /// The <see cref="IPointList"/> list of points for the bar
        /// of interest
        /// </param>
        /// <param name="baseAxis">
        /// The base axis for the bar
        /// </param>
        /// <returns>
        /// The minimum increment between bars along the base axis
        /// </returns>
        internal static double GetMinStepSize(IPointList list, Axis baseAxis)
        {
            double minStep = double.MaxValue;

            if (list.Count <= 0 || baseAxis._scale.IsAnyOrdinal)
            {
                return 1.0;
            }

            PointPair lastPt = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                PointPair pt = list[i];
                if (!pt.IsInvalid || !lastPt.IsInvalid)
                {
                    double step;
                    if (baseAxis is XAxis || baseAxis is X2Axis)
                    {
                        step = pt.X - lastPt.X;
                    }
                    else
                    {
                        step = pt.Y - lastPt.Y;
                    }

                    if (step > 0 && step < minStep)
                    {
                        minStep = step;
                    }
                }

                lastPt = pt;
            }

            double range = baseAxis.Scale._maxLinearized - baseAxis.Scale._minLinearized;
            if (range <= 0)
            {
                minStep = 1.0;
            }
                
                // 			else if ( minStep <= 0 || minStep < 0.001 * range || minStep > range )
            else if (minStep <= 0 || minStep > range)
            {
                minStep = 0.1 * range;
            }

            return minStep;
        }

        #endregion

        /// <summary>
        /// A simple struct that defines the
        /// default property values for the <see cref="BarSettings"/> class.
        /// </summary>
        public struct Default
        {
            #region Static Fields

            /// <summary>The default value for the <see cref="BarSettings.Base"/>, which determines the base
            /// <see cref="Axis"/> from which the <see cref="Bar"/> graphs will be displayed.
            /// </summary>
            /// <seealso cref="BarSettings.Base"/>
            public static BarBase Base = BarBase.X;

            /// <summary>
            /// The default width of a bar cluster 
            /// on a <see cref="Bar"/> graph.  This value only applies to
            /// <see cref="Bar"/> graphs, and only when the
            /// <see cref="Axis.Type"/> is <see cref="AxisType.Linear"/>,
            /// <see cref="AxisType.Log"/> or <see cref="AxisType.Date"/>.
            /// This dimension is expressed in terms of X scale user units.
            /// </summary>
            /// <seealso cref="Default.MinClusterGap"/>
            /// <seealso cref="BarSettings.MinBarGap"/>
            public static double ClusterScaleWidth = 1.0;

            /// <summary>
            /// The default value for <see cref="BarSettings.ClusterScaleWidthAuto" />.
            /// </summary>
            public static bool ClusterScaleWidthAuto = true;

            /// <summary>
            /// The default dimension gap between each individual bar within a bar cluster
            /// on a <see cref="Bar"/> graph.
            /// This dimension is expressed in terms of the normal bar width.
            /// </summary>
            /// <seealso cref="Default.MinClusterGap"/>
            /// <seealso cref="BarSettings.MinBarGap"/>
            public static float MinBarGap = 0.2F;

            /// <summary>
            /// The default dimension gap between clusters of bars on a
            /// <see cref="Bar"/> graph.
            /// This dimension is expressed in terms of the normal bar width.
            /// </summary>
            /// <seealso cref="Default.MinBarGap"/>
            /// <seealso cref="BarSettings.MinClusterGap"/>
            public static float MinClusterGap = 1.0F;

            /// <summary>The default value for the <see cref="BarSettings.Type"/> property, which
            /// determines if the bars are drawn overlapping eachother in a "stacked" format,
            /// or side-by-side in a "cluster" format.  See the <see cref="ZedGraph.BarType"/>
            /// for more information.
            /// </summary>
            /// <seealso cref="BarSettings.Type"/>
            public static BarType Type = BarType.Cluster;

            #endregion
        }
    }
}