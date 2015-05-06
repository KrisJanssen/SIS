using System;
using System.Collections;
using System.Text;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace SIS.MDITemplate
{
	public class RecentFilesList : StringCollection
	{
		private const string m_sValue = "RecentFiles";
		private const int m_nMaximumItems = 10;
		private static RecentFilesList m_theList = null;

		private RecentFilesList()
		{
			Load();
		}

		private void Load()
		{
			RegistryKey registryKey = ApplicationSettingsKey.Get(false);

			if (registryKey != null)
			{
				string sValue = registryKey.GetValue(m_sValue) as string;

				if (sValue != null)
				{
					string [] asFiles = sValue.Split(new char [] {'|'});

					foreach (string sFile in asFiles)
					{
						base.Add(sFile);
					}
				}

				registryKey.Close();
			}
		}
	
		private int FindFile(string sFile)
		{
			string sFileLower = sFile.ToLower();

			for (int nIndex = 0; nIndex < this.Count; nIndex++)
			{
				if (sFileLower == this[nIndex].ToLower())
				{
					return nIndex;
				}
			}

			return -1;
		}
		
		public static RecentFilesList Get()
		{
			if (m_theList == null)
			{
				m_theList = new RecentFilesList();
			}

			return m_theList;
		}

		public void Save()
		{
			RegistryKey registryKey = ApplicationSettingsKey.Get(true);

			if (registryKey != null)
			{
				StringBuilder stringBuilder = new StringBuilder();

				bool fFirst = true;

				foreach (string sFile in this)
				{
					if (fFirst)
					{
						fFirst = false;
					}
					else
					{
						stringBuilder.Append('|');
					}

					stringBuilder.Append(sFile);
				}

				registryKey.SetValue(m_sValue, stringBuilder.ToString());
				registryKey.Close();
			}				
		}

		public new void Add(string sFile)
		{
			int nFileIndex = FindFile(sFile);

			if (nFileIndex < 0)
			{
				base.Insert(0, sFile);

				while (this.Count > m_nMaximumItems)
				{
					this.RemoveAt(this.Count - 1);
				}
			}
			else
			{
				this.RemoveAt(nFileIndex);
				this.Insert(0, sFile);
			}

			Save();
		}
	}
}
