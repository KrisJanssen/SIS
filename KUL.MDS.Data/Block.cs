// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="">
//   
// </copyright>
// <summary>
//   The block.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The block.
    /// </summary>
    public class Block
    {
        // Members.
        #region Static Fields

        /// <summary>
        /// The col index column.
        /// </summary>
        public static Column colIndexColumn = new StepColumn(0, 1);

        #endregion

        #region Fields

        /// <summary>
        /// The m_l columns.
        /// </summary>
        private List<Column> m_lColumns;

        /// <summary>
        /// The m_mdta meta data.
        /// </summary>
        private MetaData m_mdtaMetaData;

        // Blocks can have a name (but usually it doesn't have)
        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Block"/> class.
        /// </summary>
        public Block()
        {
            this.m_mdtaMetaData = null;
            this.m_sName = string.Empty;
            this.m_lColumns = new List<Column>();
        }

        #endregion

        // Fields.
        #region Public Properties

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        public MetaData MetaData
        {
            get
            {
                return this.m_mdtaMetaData;
            }

            set
            {
                this.m_mdtaMetaData = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_sName;
            }

            set
            {
                this.m_sName = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add column.
        /// </summary>
        /// <param name="__colC">
        /// The __col c.
        /// </param>
        /// <param name="__sTitle">
        /// The __s title.
        /// </param>
        /// <param name="__blAppend">
        /// The __bl append.
        /// </param>
        public void AddColumn(Column __colC, string __sTitle, bool __blAppend)
        {
            if (__sTitle.Length != 0)
            {
                __colC.Name = __sTitle;
            }

            this.m_lColumns.Insert(__blAppend ? this.m_lColumns.Count : 0, __colC);
        }

        // Constructor.

        // number of real columns, not including 0-th pseudo-column
        // int get_column_count() const { return cols.size(); }
        // get column, 0-th column is index of point
        /// <summary>
        /// The get column.
        /// </summary>
        /// <param name="__iN">
        /// The __i n.
        /// </param>
        /// <returns>
        /// The <see cref="Column"/>.
        /// </returns>
        public Column GetColumn(int __iN)
        {
            if (__iN == 0)
            {
                return colIndexColumn;
            }

            int _iC = __iN < 0 ? __iN + this.m_lColumns.Count : __iN - 1;

            if (_iC < 0 || _iC >= this.m_lColumns.Count)
            {
                // throw RunTimeError("column index out of range: " + S(__iN));
            }

            return this.m_lColumns[_iC];
        }

        /// <summary>
        /// The get point count.
        /// </summary>
        /// return number of points or -1 for "unlimited" number of points
        /// each column should have the same number of points (or "unlimited"
        /// number if the column is a generator)
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetPointCount()
        {
            int _iMinN = -1;
            for (int _iI = 0; _iI < this.m_lColumns.Count; _iI++)
            {
                int _iN = this.m_lColumns[_iI].GetPointCount();
                if (_iMinN == -1 || (_iN != -1 && _iN < _iMinN))
                {
                    _iMinN = _iN;
                }
            }

            return _iMinN;
        }

        #endregion

        // add one column; for use in filetype implementations

        // split block if it has columns with different sizes
        #region Methods

        /// <summary>
        /// The split on column length.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        private List<Block> SplitOnColumnLength()
        {
            List<Block> _lResult = new List<Block>();

            if (this.m_lColumns.Count == 0)
            {
                return _lResult;
            }

            _lResult.Add(this);

            int _iN1 = this.m_lColumns[0].GetPointCount();

            for (int _iI = 1; _iI < this.m_lColumns.Count; /*nothing*/)
            {
                int _iN = this.m_lColumns[_iI].GetPointCount();

                // If two neighboring columns have the same size we can continue because the can stay in the same block.
                if (_iN == _iN1)
                {
                    _iI++;
                }
                else
                {
                    int _iNewBlockID = -1;
                    for (int _iJ = 1; _iJ < _lResult.Count; _iJ++)
                    {
                        if (_lResult[_iJ].GetPointCount() == _iN)
                        {
                            _iNewBlockID = _iJ;
                            break;
                        }
                    }

                    if (_iNewBlockID == -1)
                    {
                        _iNewBlockID = _lResult.Count;
                        Block _blckNewBlock = new Block();
                        _blckNewBlock.MetaData = this.MetaData;
                        _blckNewBlock.Name = this.Name + "_" + _iN.ToString();
                        _lResult.Add(_blckNewBlock);
                    }

                    _lResult[_iNewBlockID].AddColumn(this.m_lColumns[_iI], string.Empty, true);
                    this.m_lColumns.Remove(this.m_lColumns[0 + _iI]);
                }
            }

            return _lResult;
        }

        #endregion
    }
}