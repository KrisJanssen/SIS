using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DevDefined.Common.Extensions
{
  public static class PathExtensions
  {
    public static string ToRelative(this string mainDirPath, string absoluteFilePath)
    {
      string[] firstPathParts = mainDirPath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
      string[] secondPathParts = absoluteFilePath.Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

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

      string newPath = String.Empty;
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
  }
}