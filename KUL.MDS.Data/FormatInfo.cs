// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatInfo.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The format info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The format info.
    /// </summary>
    public struct FormatInfo
    {
        #region Fields

        /// <summary>
        /// The m_b is binary.
        /// </summary>
        private bool m_bIsBinary; // true if it's binary file

        /// <summary>
        /// The m_b is multi block.
        /// </summary>
        private bool m_bIsMultiBlock; // true if filetype supports multiple blocks

        /// <summary>
        /// The m_s desc.
        /// </summary>
        private string m_sDesc; // full format name (reasonably short)

        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName; // short name, usually basename of .cpp/.h files  

        /// <summary>
        /// The m_strlst exts.
        /// </summary>
        private List<string> m_strlstExts; // possible extensions

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatInfo"/> struct.
        /// </summary>
        /// <param name="__sName">
        /// The __s name.
        /// </param>
        /// <param name="__sDesc">
        /// The __s desc.
        /// </param>
        /// <param name="__strlstExts">
        /// The __strlst exts.
        /// </param>
        /// <param name="__bIsBinary">
        /// The __b is binary.
        /// </param>
        /// <param name="__bIsMultiBlock">
        /// The __b is multi block.
        /// </param>
        public FormatInfo(
            string __sName, 
            string __sDesc, 
            List<string> __strlstExts, 
            bool __bIsBinary, 
            bool __bIsMultiBlock)
        {
            this.m_sName = __sName;
            this.m_sDesc = __sDesc;
            this.m_strlstExts = __strlstExts;
            this.m_bIsBinary = __bIsBinary;
            this.m_bIsMultiBlock = __bIsMultiBlock;
        }

        #endregion
    }
}