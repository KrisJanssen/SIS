// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MostRecentFile.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Encapsulates a filename and a thumbnail.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.MRU
{
    using System.Drawing;

    /// <summary>
    /// Encapsulates a filename and a thumbnail.
    /// </summary>
    internal class MostRecentFile
    {
        #region Fields

        /// <summary>
        /// The file name.
        /// </summary>
        private string fileName;

        /// <summary>
        /// The thumb.
        /// </summary>
        private Image thumb;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MostRecentFile"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="thumb">
        /// The thumb.
        /// </param>
        public MostRecentFile(string fileName, Image thumb)
        {
            this.fileName = fileName;
            this.thumb = thumb;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Gets the thumb.
        /// </summary>
        public Image Thumb
        {
            get
            {
                return this.thumb;
            }
        }

        #endregion
    }
}