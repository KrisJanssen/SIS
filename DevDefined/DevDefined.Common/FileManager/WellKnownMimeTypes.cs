using System.Diagnostics.CodeAnalysis;

namespace DevDefined.Common.FileManager
{
    /// <summary>
    /// A collection of well known types, and some methods which can be used to check
    /// a mime type for it's base type i.e. Text, Image, Video or Audio.
    /// </summary>
    public static class WellKnownMimeTypes
    {
        public const string Application = "application";
        public const string ApplicationOctetStream = "application/octet-stream";
        public const string Audio = "audio";
        public const string Image = "image";
        public const string ImageBitmap = "image/bitmap";
        public const string ImageGif = "image/gif";
        public const string ImageJpeg = "image/jpeg";
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Png")] public const string ImagePng = "image/png";
        public const string Text = "text";
        public const string TextPlain = "text/plain";
        public const string Video = "video";

        public static bool IsApplication(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Application);
        }

        public static bool IsText(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Text);
        }

        public static bool IsImage(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Image);
        }

        public static bool IsVideo(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Video);
        }

        public static bool IsAudio(string mimeType)
        {
            return mimeType.ToLower().StartsWith(Audio);
        }
    }
}