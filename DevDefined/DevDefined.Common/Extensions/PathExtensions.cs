// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PathExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The path extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Extensions
{
    using System;
    using System.IO;

    /// <summary>
    /// The path extensions.
    /// </summary>
    public static class PathExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The to relative.
        /// </summary>
        /// <param name="mainDirPath">
        /// The main dir path.
        /// </param>
        /// <param name="absoluteFilePath">
        /// The absolute file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToRelative(this string mainDirPath, string absoluteFilePath)
        {
            string[] firstPathParts = mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] secondPathParts =
                absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int sameCounter = 0;
            for (int i = 0; i < Math.Min(firstPathParts.Length, secondPathParts.Length); i++)
            {
                if (!firstPathParts[i].ToLower().Equals(secondPathParts[i].ToLower()))
                {
                    break;
                }

                sameCounter++;
            }

            if (sameCounter == 0)
            {
                return absoluteFilePath;
            }

            string newPath = string.Empty;
            for (int i = sameCounter; i < firstPathParts.Length; i++)
            {
                if (i > sameCounter)
                {
                    newPath += Path.DirectorySeparatorChar;
                }

                newPath += "..";
            }

            if (newPath.Length == 0)
            {
                newPath = ".";
            }

            for (int i = sameCounter; i < secondPathParts.Length; i++)
            {
                newPath += Path.DirectorySeparatorChar;
                newPath += secondPathParts[i];
            }

            return newPath;
        }

        #endregion
    }
}