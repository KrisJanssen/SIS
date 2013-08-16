using System.IO;

namespace DevDefined.Common.FileManager
{
    /// <summary>
    /// Interface implemented by a class which can manage files
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Deletes a file, given it's path
        /// </summary>
        /// <param name="path"></param>
        void DeleteFile(string path);

        /// <summary>
        /// Updates a file, given it's path (the file must already exist for this operation
        /// to succeed)
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>        
        void PutFile(string path, Stream contents);

        /// <summary>
        /// Creates a file, given it's path (the file must not exist for this operation to succeed).
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        void PostFile(string path, Stream contents);

        /// <summary>
        /// Gets a file, given it's path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Stream GetFile(string path);

        /// <summary>
        /// Returns true if a file exists matching the supplied path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Exists(string path);
    }
}