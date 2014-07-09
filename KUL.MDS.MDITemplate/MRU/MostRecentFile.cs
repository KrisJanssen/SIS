/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

namespace SIS.MDITemplate.MRU
{
    using System.Drawing;

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
                return this.fileName;
            }
        }

        public Image Thumb
        {
            get
            {
                return this.thumb;
            }
        }

        public MostRecentFile(string fileName, Image thumb)
        {
            this.fileName = fileName;
            this.thumb = thumb;
        }
    }
}