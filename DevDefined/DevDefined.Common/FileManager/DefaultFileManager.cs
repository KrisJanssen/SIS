// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultFileManager.cs" company="">
//   
// </copyright>
// <summary>
//   Default implementation of a file manager, provides access to a path, and all the folders and files
//   within it, but does not allow access to any resources outside of that path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.FileManager
{
    using System;
    using System.IO;

    using Castle.Core.Logging;

    using DevDefined.Common.IO;

    /// <summary>
    /// Default implementation of a file manager, provides access to a path, and all the folders and files
    /// within it, but does not allow access to any resources outside of that path.
    /// </summary>
    public class DefaultFileManager : IFileManager
    {
        #region Fields

        /// <summary>
        /// The _path.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// The _logger.
        /// </summary>
        private ILogger _logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFileManager"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public DefaultFileManager(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            this._path = Path.GetFullPath(path);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger Logger
        {
            get
            {
                if (this._logger == null)
                {
                    this._logger = NullLogger.Instance;
                }

                return this._logger;
            }

            set
            {
                this._logger = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The delete file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public void DeleteFile(string path)
        {
            string absolutePath = this.Absoloute(path);
            File.Delete(absolutePath);
            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Deleted file: {0}", absolutePath);
            }
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Exists(string path)
        {
            return File.Exists(this.Absoloute(path));
        }

        /// <summary>
        /// The get file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="Stream"/>.
        /// </returns>
        public Stream GetFile(string path)
        {
            string absolutePath = this.Absoloute(path);

            try
            {
                return File.OpenRead(absolutePath);
            }
            finally
            {
                if (this.Logger.IsDebugEnabled)
                {
                    this.Logger.Debug("Get file: {0}", absolutePath);
                }
            }
        }

        /// <summary>
        /// The post file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="contents">
        /// The contents.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void PostFile(string path, Stream contents)
        {
            string absoloutePath = this.Absoloute(path);

            if (File.Exists(absoloutePath))
            {
                throw new ArgumentException(string.Format("file \"{0}\" exists, can not create", absoloutePath), "path");
            }

            PreparePath(absoloutePath);

            using (FileStream stream = File.Open(absoloutePath, FileMode.CreateNew, FileAccess.Write))
            {
                contents.Pipe(stream);
            }

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Posted file: {0}", absoloutePath);
            }
        }

        /// <summary>
        /// The put file.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="contents">
        /// The contents.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void PutFile(string path, Stream contents)
        {
            string absoloutePath = this.Absoloute(path);

            if (!File.Exists(absoloutePath))
            {
                throw new ArgumentException(
                    string.Format("file \"{0}\" does not exist, can not update", absoloutePath), 
                    "path");
            }

            using (FileStream stream = File.Open(absoloutePath, FileMode.Truncate))
            {
                contents.Pipe(stream);
            }

            if (this.Logger.IsDebugEnabled)
            {
                this.Logger.Debug("Put file: {0}", absoloutePath);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The prepare path.
        /// </summary>
        /// <param name="absolutePath">
        /// The absolute path.
        /// </param>
        private static void PreparePath(string absolutePath)
        {
            string directory = Path.GetDirectoryName(absolutePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// The absoloute.
        /// </summary>
        /// <param name="relativePath">
        /// The relative path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private string Absoloute(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
            {
                throw new ArgumentException(string.Format("path \"{0}\" is not relative", relativePath), "relativePath");
            }

            string absoloutePath = Path.GetFullPath(Path.Combine(this._path, relativePath));

            if (!absoloutePath.StartsWith(this._path))
            {
                throw new ArgumentException(
                    string.Format(
                        "path must be within the root path \"{0}\" of this file manager, but resolved to: \"{1}\"", 
                        this._path, 
                        absoloutePath), 
                    "relativePath");
            }

            return absoloutePath;
        }

        #endregion
    }
}