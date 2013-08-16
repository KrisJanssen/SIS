using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevDefined.Common.IO
{
  /// <summary>
  /// A fairly specific class which can be used to resolve the relative urls in a CSS file to their absolute path.
  /// </summary>
  public class CssRelativeUrlResolver
  {
    private static readonly Regex CssUrls = new Regex(@"(.*?)(url)\((?!http|/)(?<Url>.*?)\)(.*?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private readonly string physicalRoot;
    private readonly Uri virtualRoot;

    /// <summary>
    /// Initializes a new instance of the <see cref="CssRelativeUrlResolver"/> class.
    /// </summary>
    /// <param name="physicalRoot">The physical root (file path)</param>
    /// <param name="virtualRoot">The virtual root (the path of the application)</param>
    public CssRelativeUrlResolver(string physicalRoot, Uri virtualRoot)
    {
      this.physicalRoot = Path.GetFullPath(physicalRoot);
      this.virtualRoot = virtualRoot;
    }

    public string Resolve(string file, string input)
    {
      return CssUrls.Replace(input, match => RootPathInFileForMatch(file, match));
    }

    private string RootPathInFileForMatch(string file, Match match)
    {
      if (IsRelative(match.Value))
      {
        Group urlGroup = match.Groups["Url"];
        string relativePhysicalFilePath = MakeRelative(Path.GetDirectoryName(file), physicalRoot);
        var startPath = new Uri(virtualRoot, relativePhysicalFilePath);
        var rootedUrl = new Uri(startPath, urlGroup.Value);
        int relativeGroupIndex = urlGroup.Index - match.Index;
        return match.Value.Substring(0, relativeGroupIndex) + rootedUrl + match.Value.Substring(relativeGroupIndex + urlGroup.Length);
      }

      return match.Value;
    }

    private static string MakeRelative(string path, string root)
    {
      string[] partParts = path.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
      string[] rootParts = root.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

      int start = 0;

      for (int i = 0; i < partParts.Length; i++)
      {
        if ((rootParts.Length > i) && partParts[i].Equals(rootParts[i], StringComparison.InvariantCultureIgnoreCase)) start = i;
        else break;
      }

      return string.Join(Path.DirectorySeparatorChar.ToString(), partParts.Where((part, index) => index > start).ToArray());
    }

    private static bool IsRelative(string url)
    {
      return !(Uri.IsWellFormedUriString(url, UriKind.Absolute) || (Uri.IsWellFormedUriString(url, UriKind.Relative) && !url.StartsWith("/")));
    }
  }
}