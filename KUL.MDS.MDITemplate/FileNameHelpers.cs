using System;

namespace SIS.MDITemplate
{
	public sealed class FileNameHelpers
	{
		static public string GetPath(string sFilePath)
		{
			int nLastSlashIndex = sFilePath.LastIndexOf('\\');

			if (nLastSlashIndex >= 0)
			{
				return sFilePath.Substring(0, nLastSlashIndex);
			}
			else
			{
				return "";
			}
		}

		static public string GetFileName(string sFilePath)
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

		static public string GetTitle(string sFilePath)
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

		static public string GetExtension(string sFilePath)
		{
			int nLastPeriodIndex = sFilePath.LastIndexOf('.');

			if (nLastPeriodIndex < 0)
			{
				return "";
			}
			else
			{
				return sFilePath.Substring(nLastPeriodIndex);
			}
		}
	}
}
