// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileNameHelpers.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The file name helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    /// <summary>
    /// The file name helpers.
    /// </summary>
    public sealed class FileNameHelpers
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get extension.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetExtension(string sFilePath)
        {
            int nLastPeriodIndex = sFilePath.LastIndexOf('.');

            if (nLastPeriodIndex < 0)
            {
                return string.Empty;
            }
            else
            {
                return sFilePath.Substring(nLastPeriodIndex);
            }
        }

        /// <summary>
        /// The get file name.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFileName(string sFilePath)
        {
            int nLastSlashIndex = sFilePath.LastIndexOf('\\');

            if (nLastSlashIndex >= 0)
            {
                return sFilePath.Substring(nLastSlashIndex + 1);
            }
            else
            {
                return sFilePath;
            }
        }

        /// <summary>
        /// The get path.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetPath(string sFilePath)
        {
            int nLastSlashIndex = sFilePath.LastIndexOf('\\');

            if (nLastSlashIndex >= 0)
            {
                return sFilePath.Substring(0, nLastSlashIndex);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The get title.
        /// </summary>
        /// <param name="sFilePath">
        /// The s file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetTitle(string sFilePath)
        {
            int nLastSlashIndex = sFilePath.LastIndexOf('\\');

            if (nLastSlashIndex < 0)
            {
                nLastSlashIndex = 0;
            }
            else
            {
                nLastSlashIndex ++;
            }

            int nDotIndex = sFilePath.LastIndexOf('.');

            if (nDotIndex < 0)
            {
                nDotIndex = sFilePath.Length;
            }

            return sFilePath.Substring(nLastSlashIndex, nDotIndex - nLastSlashIndex);
        }

        #endregion
    }
}