// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativeUrlResolver.cs" company="">
//   
// </copyright>
// <summary>
//   A fairly specific class which can be used to resolve the relative urls in a CSS file to their absolute path.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.IO
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A fairly specific class which can be used to resolve the relative urls in a CSS file to their absolute path.
    /// </summary>
    public class CssRelativeUrlResolver
    {
        #region Static Fields

        /// <summary>
        /// The css urls.
        /// </summary>
        private static readonly Regex CssUrls = new Regex(
            @"(.*?)(url)\((?!http|/)(?<Url>.*?)\)(.*?)", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Fields

        /// <summary>
        /// The physical root.
        /// </summary>
        private readonly string physicalRoot;

        /// <summary>
        /// The virtual root.
        /// </summary>
        private readonly Uri virtualRoot;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CssRelativeUrlResolver"/> class.
        /// </summary>
        /// <param name="physicalRoot">
        /// The physical root (file path)
        /// </param>
        /// <param name="virtualRoot">
        /// The virtual root (the path of the application)
        /// </param>
        public CssRelativeUrlResolver(string physicalRoot, Uri virtualRoot)
        {
            this.physicalRoot = Path.GetFullPath(physicalRoot);
            this.virtualRoot = virtualRoot;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The resolve.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Resolve(string file, string input)
        {
            return CssUrls.Replace(input, match => this.RootPathInFileForMatch(file, match));
        }

        #endregion

        #region Methods

        /// <summary>
        /// The is relative.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsRelative(string url)
        {
            return
                !(Uri.IsWellFormedUriString(url, UriKind.Absolute)
                  || (Uri.IsWellFormedUriString(url, UriKind.Relative) && !url.StartsWith("/")));
        }

        /// <summary>
        /// The make relative.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="root">
        /// The root.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string MakeRelative(string path, string root)
        {
            string[] partParts = path.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
            string[] rootParts = root.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

            int start = 0;

            for (int i = 0; i < partParts.Length; i++)
            {
                if ((rootParts.Length > i)
                    && partParts[i].Equals(rootParts[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    start = i;
                }
                else
                {
                    break;
                }
            }

            return string.Join(
                Path.DirectorySeparatorChar.ToString(), 
                partParts.Where((part, index) => index > start).ToArray());
        }

        /// <summary>
        /// The root path in file for match.
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <param name="match">
        /// The match.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string RootPathInFileForMatch(string file, Match match)
        {
            if (IsRelative(match.Value))
            {
                Group urlGroup = match.Groups["Url"];
                string relativePhysicalFilePath = MakeRelative(Path.GetDirectoryName(file), this.physicalRoot);
                var startPath = new Uri(this.virtualRoot, relativePhysicalFilePath);
                var rootedUrl = new Uri(startPath, urlGroup.Value);
                int relativeGroupIndex = urlGroup.Index - match.Index;
                return match.Value.Substring(0, relativeGroupIndex) + rootedUrl
                       + match.Value.Substring(relativeGroupIndex + urlGroup.Length);
            }

            return match.Value;
        }

        #endregion
    }
}