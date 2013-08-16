using System;
using System.IO;
using Castle.Core.Logging;
using DevDefined.Common.IO;

namespace DevDefined.Common.FileManager
{
    /// <summary>
    /// Default implementation of a file manager, provides access to a path, and all the folders and files
    /// within it, but does not allow access to any resources outside of that path.
    /// </summary>
    public class DefaultFileManager : IFileManager
    {
        private readonly string _path;
        private ILogger _logger;

        public DefaultFileManager(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("path");
            _path = Path.GetFullPath(path);
        }

        public ILogger Logger
        {
            get
            {
                if (_logger == null) _logger = NullLogger.Instance;
                return _logger;
            }
            set { _logger = value; }
        }

        #region IFileManager Members

        public void DeleteFile(string path)
        {
            string absolutePath = Absoloute(path);
            File.Delete(absolutePath);
            if (Logger.IsDebugEnabled) Logger.Debug("Deleted file: {0}", absolutePath);
        }

        public void PutFile(string path, Stream contents)
        {
            string absoloutePath = Absoloute(path);

            if (!File.Exists(absoloutePath))
            {
                throw new ArgumentException(string.Format("file \"{0}\" does not exist, can not update", absoloutePath), "path");
            }

            using (FileStream stream = File.Open(absoloutePath, FileMode.Truncate))
            {
                contents.Pipe(stream);
            }

            if (Logger.IsDebugEnabled) Logger.Debug("Put file: {0}", absoloutePath);
        }

        public void PostFile(string path, Stream contents)
        {
            string absoloutePath = Absoloute(path);

            if (File.Exists(absoloutePath))
            {
                throw new ArgumentException(string.Format("file \"{0}\" exists, can not create", absoloutePath), "path");
            }

            PreparePath(absoloutePath);

            using (FileStream stream = File.Open(absoloutePath, FileMode.CreateNew, FileAccess.Write))
            {
                contents.Pipe(stream);
            }

            if (Logger.IsDebugEnabled) Logger.Debug("Posted file: {0}", absoloutePath);
        }

        public Stream GetFile(string path)
        {
            string absolutePath = Absoloute(path);

            try
            {
                return File.OpenRead(absolutePath);
            }
            finally
            {
                if (Logger.IsDebugEnabled) Logger.Debug("Get file: {0}", absolutePath);
            }
        }

        public bool Exists(string path)
        {
            return File.Exists(Absoloute(path));
        }

        #endregion

        private static void PreparePath(string absolutePath)
        {
            string directory = Path.GetDirectoryName(absolutePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private string Absoloute(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
            {
                throw new ArgumentException(string.Format("path \"{0}\" is not relative", relativePath), "relativePath");
            }

            string absoloutePath = Path.GetFullPath(Path.Combine(_path, relativePath));

            if (!absoloutePath.StartsWith(_path))
            {
                throw new ArgumentException(
                    string.Format("path must be within the root path \"{0}\" of this file manager, but resolved to: \"{1}\"", _path, absoloutePath),
                    "relativePath");
            }

            return absoloutePath;
        }
    }
}