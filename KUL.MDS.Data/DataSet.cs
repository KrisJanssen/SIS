using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace KUL.MDS.Data
{
    public abstract class DataSet
    {
        protected FormatInfo m_structFormatInfo;

        protected List<string> m_strlstOptions;

        protected MetaData m_mdtaMetaData;

        protected List<Block> m_blcklstBlocks;

        public FormatInfo FormatInfo
        {
            get
            {
                return this.m_structFormatInfo;
            }
            set
            {
                this.m_structFormatInfo = value;
            }
        }

        public List<string> Options
        {
            get
            {
                return this.m_strlstOptions;
            }
            set
            {
                this.m_strlstOptions = value;
            }
        }

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

        //public List<Block> Blocks
        //{
        //    get
        //    {
        //        return this.m_blcklstBlocks;
        //    }
        //    set
        //    {
        //        this.m_blcklstBlocks = value;
        //    }
        //}

        public int BlockCount
        {
            get
            {
                return this.m_blcklstBlocks.Count;
            }
        }

        // Constructor. This is overridable without an abstract or virtual modifier.
        protected DataSet()
        {
            m_strlstOptions = new List<string>();
            m_mdtaMetaData = null;
            m_blcklstBlocks = new List<Block>();
        }

        public abstract void LoadData(System.IO.FileStream __fstrmFile);

        public abstract bool IsCorrectFormat(System.IO.FileStream __fstrmFile);

        public Block GetBlock(int __iN) 
        {
            try
            {
                if (__iN < 0 || (int)__iN >= m_blcklstBlocks.Count)
                    throw new BlockDoesNotExistException("no block #" + __iN.ToString() + " in this file!");
                return this.m_blcklstBlocks[__iN];
            }
            catch (BlockDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void FormatAssert(bool __bCondition, string __sComment) 
        {
        //if (!__bCondition)
         //   throw new UnexpectedFormatException("Unexpected format for filetype: " + fi->name
         //                     + (__sComment.Length != 0 ? __sComment : "; " + __sComment));
        }

        public void Clear()
        {
            this.m_blcklstBlocks.Clear();
            this.m_mdtaMetaData.Clear();
        }   
    }
}
