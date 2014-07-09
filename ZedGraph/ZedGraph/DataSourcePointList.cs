// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="DataSourcePointList.cs">
//   
// </copyright>
// <summary>
//   Defines the DataSourcePointList type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.Data;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary> 
    ///  
    /// </summary> 
    /// <seealso cref="IPointList" /> 
    /// <seealso cref="IPointListEdit" /> 
    ///  
    /// <author>John Champion</author> 
    /// <version> $Revision: 3.7 $ $Date: 2007-11-05 04:33:26 $ </version> 
    [Serializable]
    public class DataSourcePointList : IPointList
    {
        #region Fields

        /// <summary>
        /// The _binding source.
        /// </summary>
        private BindingSource _bindingSource;

        /// <summary>
        /// The _tag data member.
        /// </summary>
        private string _tagDataMember = null;

        // private object _dataSource = null; 
        /// <summary>
        /// The _x data member.
        /// </summary>
        private string _xDataMember = null;

        /// <summary>
        /// The _y data member.
        /// </summary>
        private string _yDataMember = null;

        /// <summary>
        /// The _z data member.
        /// </summary>
        private string _zDataMember = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourcePointList"/> class.  
        /// Default Constructor 
        /// </summary>
        public DataSourcePointList()
        {
            this._bindingSource = new BindingSource();
            this._xDataMember = string.Empty;
            this._yDataMember = string.Empty;
            this._zDataMember = string.Empty;
            this._tagDataMember = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourcePointList"/> class. 
        /// 
        /// Constructor to initialize the DataSourcePointList from an 
        /// existing <see cref="DataSourcePointList"/> 
        /// </summary>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        public DataSourcePointList(DataSourcePointList rhs)
            : this()
        {
            this._bindingSource.DataSource = rhs._bindingSource.DataSource;
            if (rhs._xDataMember != null)
            {
                this._xDataMember = (string)rhs._xDataMember.Clone();
            }

            if (rhs._yDataMember != null)
            {
                this._yDataMember = (string)rhs._yDataMember.Clone();
            }

            if (rhs._zDataMember != null)
            {
                this._zDataMember = (string)rhs._zDataMember.Clone();
            }

            if (rhs._tagDataMember != null)
            {
                this._tagDataMember = (string)rhs._tagDataMember.Clone();
            }
        }

        #endregion

        #region Public Properties

        /// <summary> 
        /// The <see cref="BindingSource" /> object from which to get the bound data 
        /// </summary> 
        /// <remarks> 
        /// Typically, you set the <see cref="System.Windows.Forms.BindingSource.DataSource" /> 
        /// property to a reference to your database, table or list object. The 
        /// <see cref="System.Windows.Forms.BindingSource.DataMember" /> property would be set 
        /// to the name of the datatable within the 
        /// <see cref="System.Windows.Forms.BindingSource.DataSource" />, 
        /// if applicable.</remarks> 
        public BindingSource BindingSource
        {
            get
            {
                return this._bindingSource;
            }
        }

        /// <summary> 
        /// gets the number of points available in the list 
        /// </summary> 
        public int Count
        {
            get
            {
                if (this._bindingSource != null)
                {
                    return this._bindingSource.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary> 
        /// The table or list object from which to extract the data values. 
        /// </summary> 
        /// <remarks> 
        /// This property is just an alias for 
        /// <see cref="System.Windows.Forms.BindingSource.DataSource" />. 
        /// </remarks> 
        public object DataSource
        {
            get
            {
                return this._bindingSource.DataSource;
            }

            set
            {
                this._bindingSource.DataSource = value;
            }
        }

        /// <summary> 
        /// The <see cref="string" /> name of the property or column from which to obtain the 
        /// tag values for the chart. 
        /// </summary> 
        /// <remarks>Set this to null leave the tag values set to null. If this references string 
        /// data, then the tags may be used as tooltips using the 
        /// <see cref="ZedGraphControl.IsShowPointValues" /> option. 
        /// </remarks> 
        public string TagDataMember
        {
            get
            {
                return this._tagDataMember;
            }

            set
            {
                this._tagDataMember = value;
            }
        }

        /// <summary> 
        /// The <see cref="string" /> name of the property or column from which to obtain the 
        /// X data values for the chart. 
        /// </summary> 
        /// <remarks>Set this to null leave the X data values set to <see cref="PointPairBase.Missing" /> 
        /// </remarks> 
        public string XDataMember
        {
            get
            {
                return this._xDataMember;
            }

            set
            {
                this._xDataMember = value;
            }
        }

        /// <summary> 
        /// The <see cref="string" /> name of the property or column from which to obtain the 
        /// Y data values for the chart. 
        /// </summary> 
        /// <remarks>Set this to null leave the Y data values set to <see cref="PointPairBase.Missing" /> 
        /// </remarks> 
        public string YDataMember
        {
            get
            {
                return this._yDataMember;
            }

            set
            {
                this._yDataMember = value;
            }
        }

        /// <summary> 
        /// The <see cref="string" /> name of the property or column from which to obtain the 
        /// Z data values for the chart. 
        /// </summary> 
        /// <remarks>Set this to null leave the Z data values set to <see cref="PointPairBase.Missing" /> 
        /// </remarks> 
        public string ZDataMember
        {
            get
            {
                return this._zDataMember;
            }

            set
            {
                this._zDataMember = value;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// 
        /// Indexer to access the specified <see cref="PointPair"/> object by 
        /// its ordinal position in the list. 
        /// </summary>
        /// <param name="index">
        /// The ordinal position (zero-based) of the 
        /// <see cref="PointPair"/> object to be accessed.
        /// </param>
        /// <value>
        /// A <see cref="PointPair"/> object reference.
        /// </value>
        /// <returns>
        /// The <see cref="PointPair"/>.
        /// </returns>
        public PointPair this[int index]
        {
            get
            {
                if (index < 0 || index >= this._bindingSource.Count)
                {
                    throw new System.ArgumentOutOfRangeException("Error: Index out of range");
                }

                object row = this._bindingSource[index];

                double x = this.GetDouble(row, this._xDataMember, index);
                double y = this.GetDouble(row, this._yDataMember, index);
                double z = this.GetDouble(row, this._zDataMember, index);
                object tag = this.GetObject(row, this._tagDataMember);

                PointPair pt = new PointPair(x, y, z);
                pt.Tag = tag;
                return pt;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary> 
        /// Typesafe, deep-copy clone method. 
        /// </summary> 
        /// <returns>A new, independent copy of this class</returns> 
        public DataSourcePointList Clone()
        {
            return new DataSourcePointList(this);
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
        /// 
        /// Extract a double value from the specified table row or data object with the 
        /// specified column name. 
        /// </summary>
        /// <param name="row">
        /// The data object from which to extract the value
        /// </param>
        /// <param name="dataMember">
        /// The property name or column name of the value 
        /// to be extracted
        /// </param>
        /// <param name="index">
        /// The zero-based index of the point to be extracted. 
        /// </param>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        private double GetDouble(object row, string dataMember, int index)
        {
            if (dataMember == null || dataMember == string.Empty)
            {
                return index + 1;
            }

            // Type myType = row.GetType();
            DataRowView drv = row as DataRowView;
            PropertyInfo pInfo = null;
            if (drv == null)
            {
                pInfo = row.GetType().GetProperty(dataMember);
            }

            object val = null;

            if (pInfo != null)
            {
                val = pInfo.GetValue(row, null);
            }
            else if (drv != null)
            {
                val = drv[dataMember];
            }
            else if (pInfo == null)
            {
                throw new System.Exception("Can't find DataMember '" + dataMember + "' in DataSource");
            }

            // if ( val == null ) 
            // throw new System.Exception( "Can't find DataMember '" + dataMember + "' in DataSource" ); 
            double x;
            if (val == null || val == DBNull.Value)
            {
                x = PointPair.Missing;
            }
            else if (val.GetType() == typeof(DateTime))
            {
                x = ((DateTime)val).ToOADate();
            }
            else if (val.GetType() == typeof(string))
            {
                x = index + 1;
            }
            else
            {
                x = Convert.ToDouble(val);
            }

            return x;
        }

        /// <summary>
        /// 
        /// Extract an object from the specified table row or data object with the 
        /// specified column name. 
        /// </summary>
        /// <param name="row">
        /// The data object from which to extract the object
        /// </param>
        /// <param name="dataMember">
        /// The property name or column name of the object 
        /// to be extracted
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object GetObject(object row, string dataMember)
        {
            if (dataMember == null || dataMember == string.Empty)
            {
                return null;
            }

            PropertyInfo pInfo = row.GetType().GetProperty(dataMember);
            DataRowView drv = row as DataRowView;

            object val = null;

            if (pInfo != null)
            {
                val = pInfo.GetValue(row, null);
            }
            else if (drv != null)
            {
                val = drv[dataMember];
            }

            if (val == null)
            {
                throw new System.Exception("Can't find DataMember '" + dataMember + "' in DataSource");
            }

            return val;
        }

        #endregion
    }
}