// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataSet.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The data set.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// The data set.
    /// </summary>
    public abstract class DataSet
    {
        #region Fields

        /// <summary>
        /// The m_blcklst blocks.
        /// </summary>
        protected List<Block> m_blcklstBlocks;

        /// <summary>
        /// The m_mdta meta data.
        /// </summary>
        protected MetaData m_mdtaMetaData;

        /// <summary>
        /// The m_strlst options.
        /// </summary>
        protected List<string> m_strlstOptions;

        /// <summary>
        /// The m_struct format info.
        /// </summary>
        protected FormatInfo m_structFormatInfo;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSet"/> class.
        /// </summary>
        protected DataSet()
        {
            this.m_strlstOptions = new List<string>();
            this.m_mdtaMetaData = null;
            this.m_blcklstBlocks = new List<Block>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the block count.
        /// </summary>
        public int BlockCount
        {
            get
            {
                return this.m_blcklstBlocks.Count;
            }
        }

        /// <summary>
        /// Gets or sets the format info.
        /// </summary>
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
        /// Gets or sets the options.
        /// </summary>
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

        #endregion

        // public List<Block> Blocks
        // {
        // get
        // {
        // return this.m_blcklstBlocks;
        // }
        // set
        // {
        // this.m_blcklstBlocks = value;
        // }
        // }
        #region Public Methods and Operators

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            this.m_blcklstBlocks.Clear();
            this.m_mdtaMetaData.Clear();
        }

        /// <summary>
        /// The format assert.
        /// </summary>
        /// <param name="__bCondition">
        /// The __b condition.
        /// </param>
        /// <param name="__sComment">
        /// The __s comment.
        /// </param>
        public void FormatAssert(bool __bCondition, string __sComment)
        {
            // if (!__bCondition)
            // throw new UnexpectedFormatException("Unexpected format for filetype: " + fi->name
            // + (__sComment.Length != 0 ? __sComment : "; " + __sComment));
        }

        /// <summary>
        /// The get block.
        /// </summary>
        /// <param name="__iN">
        /// The __i n.
        /// </param>
        /// <returns>
        /// The <see cref="Block"/>.
        /// </returns>
        /// <exception cref="BlockDoesNotExistException">
        /// </exception>
        public Block GetBlock(int __iN)
        {
            try
            {
                if (__iN < 0 || (int)__iN >= this.m_blcklstBlocks.Count)
                {
                    throw new BlockDoesNotExistException("no block #" + __iN.ToString() + " in this file!");
                }

                return this.m_blcklstBlocks[__iN];
            }
            catch (BlockDoesNotExistException ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// The is correct format.
        /// </summary>
        /// <param name="__fstrmFile">
        /// The __fstrm file.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public abstract bool IsCorrectFormat(FileStream __fstrmFile);

        /// <summary>
        /// The load data.
        /// </summary>
        /// <param name="__fstrmFile">
        /// The __fstrm file.
        /// </param>
        public abstract void LoadData(FileStream __fstrmFile);

        #endregion
    }
}