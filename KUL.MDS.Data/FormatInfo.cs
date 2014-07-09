namespace SIS.Data
{
    using System.Collections.Generic;

    public struct FormatInfo
    {
        private string m_sName;               // short name, usually basename of .cpp/.h files  
        private string m_sDesc;               // full format name (reasonably short)
        private List<string> m_strlstExts;    // possible extensions
        private bool m_bIsBinary;             // true if it's binary file
        private bool m_bIsMultiBlock;         // true if filetype supports multiple blocks

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
    }
}
