namespace SIS.Data
{
    using System.Collections.Generic;

    public class Block
    {
        // Members.
        private MetaData m_mdtaMetaData;

        // Blocks can have a name (but usually it doesn't have)
        private string m_sName;
        private List<Column> m_lColumns;

        // Fields.
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

        // Constructor.
        public Block()
        {
            this.m_mdtaMetaData = null;
            this.m_sName = string.Empty;
            this.m_lColumns = new List<Column>();
        }

        // handy pseudo-column that returns index of point as value
        public static Column colIndexColumn = new StepColumn(0, 1);

        // number of real columns, not including 0-th pseudo-column
        // int get_column_count() const { return cols.size(); }
        // get column, 0-th column is index of point
        public Column GetColumn(int __iN)
        {
            if (__iN == 0)
            {
                return colIndexColumn;
            }

            int _iC = (__iN < 0 ? __iN + this.m_lColumns.Count : __iN - 1);

            if (_iC < 0 || _iC >= this.m_lColumns.Count)
            {
                //throw RunTimeError("column index out of range: " + S(__iN));
            }

            return this.m_lColumns[_iC];
        }

        /// return number of points or -1 for "unlimited" number of points
        /// each column should have the same number of points (or "unlimited"
        /// number if the column is a generator)
        public int GetPointCount()
        {
            int _iMinN = -1;
            for (int _iI = 0; _iI < this.m_lColumns.Count; _iI++)
            {
                int _iN = this.m_lColumns[_iI].GetPointCount();
                if (_iMinN == -1 || (_iN != -1 && _iN < _iMinN))
                    _iMinN = _iN;
            }
            return _iMinN;
        }

        // add one column; for use in filetype implementations
        public void AddColumn(Column __colC, string __sTitle, bool __blAppend)
        {
            if (__sTitle.Length != 0)
                __colC.Name = __sTitle;
            this.m_lColumns.Insert((__blAppend ? this.m_lColumns.Count : 0), __colC);
        }

        // split block if it has columns with different sizes
        List<Block> SplitOnColumnLength()
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
                    _iI++;
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
    }
}
