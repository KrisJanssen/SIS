using System;
using System.Drawing;

namespace SIS.MDITemplate
{
    /// <summary>
    /// Encapsulates a filename and a thumbnail.
    /// </summary>
    internal class MostRecentFile
    {
        private string fileName;
        private Image thumb;

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public Image Thumb
        {
            get
            {
                return thumb;
            }
        }

        public MostRecentFile(string fileName, Image thumb)
        {
            this.fileName = fileName;
            this.thumb = thumb;
        }
    }
}