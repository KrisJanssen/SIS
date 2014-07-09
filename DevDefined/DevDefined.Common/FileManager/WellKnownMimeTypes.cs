// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WellKnownMimeTypes.cs" company="">
//   
// </copyright>
// <summary>
//   A collection of well known types, and some methods which can be used to check
//   a mime type for it's base type i.e. Text, Image, Video or Audio.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.FileManager
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A collection of well known types, and some methods which can be used to check
    /// a mime type for it's base type i.e. Text, Image, Video or Audio.
    /// </summary>
    public static class WellKnownMimeTypes
    {
        #region Constants

        /// <summary>
        /// The application.
        /// </summary>
        public const string Application = "application";

        /// <summary>
        /// The application octet stream.
        /// </summary>
        public const string ApplicationOctetStream = "application/octet-stream";

        /// <summary>
        /// The audio.
        /// </summary>
        public const string Audio = "audio";

        /// <summary>
        /// The image.
        /// </summary>
        public const string Image = "image";

        /// <summary>
        /// The image bitmap.
        /// </summary>
        public const string ImageBitmap = "image/bitmap";

        /// <summary>
        /// The image gif.
        /// </summary>
        public const string ImageGif = "image/gif";

        /// <summary>
        /// The image jpeg.
        /// </summary>
        public const string ImageJpeg = "image/jpeg";

        /// <summary>
        /// The image png.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Png")]
        public const string ImagePng = "image/png";

        /// <summary>
        /// The text.
        /// </summary>
        public const string Text = "text";

        /// <summary>
        /// The text plain.
        /// </summary>
        public const string TextPlain = "text/plain";

        /// <summary>
        /// The video.
        /// </summary>
        public const string Video = "video";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The is application.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsApplication(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Application);
        }

        /// <summary>
        /// The is audio.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAudio(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Audio);
        }

        /// <summary>
        /// The is image.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsImage(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Image);
        }

        /// <summary>
        /// The is text.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsText(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Text);
        }

        /// <summary>
        /// The is video.
        /// </summary>
        /// <param name="mimeType">
        /// The mime type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsVideo(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Video);
        }

        #endregion
    }
}