// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MimeTypeRegister.cs" company="">
//   
// </copyright>
// <summary>
//   Encapsulates functionality for retrieving a registered mime type's details in the system.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.FileManager
{
    using System.Collections.Generic;

    using Microsoft.Win32;

    /// <summary>
    /// Encapsulates functionality for retrieving a registered mime type's details in the system.
    /// </summary>
    public static class MimeTypeRegister
    {
        #region Constants

        /// <summary>
        /// The key in the registry for looking up content types.
        /// </summary>
        public const string ContentTypeKey = "Content Type";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Get the extensions for the supplied mime type, the reverse operation of the <see cref="GetMimeType"/> method.
        /// </summary>
        /// <param name="mimeType">
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetExtensions(string mimeType)
        {
            var extensions = new List<string>();

            foreach (string className in Registry.ClassesRoot.GetSubKeyNames())
            {
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(className);
                if (key != null)
                {
                    object contentType = key.GetValue(ContentTypeKey);
                    if ((contentType != null) && (contentType.ToString().ToLower() == mimeType.ToLower()))
                    {
                        extensions.Add(className);
                    }
                }
            }

            return extensions;
        }

        /// <summary>
        /// Get the content type for file extension, or return null if no mime type is registered for the file extension.
        /// </summary>
        /// <param name="extension">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMimeType(string extension)
        {
            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            extension = extension.Trim();

            RegistryKey key = Registry.ClassesRoot.OpenSubKey(extension);

            if (key != null)
            {
                object value = key.GetValue(ContentTypeKey);
                if (value != null)
                {
                    return value.ToString();
                }
            }

            return null;
        }

        #endregion
    }
}