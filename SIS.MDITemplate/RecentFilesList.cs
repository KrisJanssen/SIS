// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RecentFilesList.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The recent files list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.Collections.Specialized;
    using System.Text;

    using Microsoft.Win32;

    /// <summary>
    /// The recent files list.
    /// </summary>
    public class RecentFilesList : StringCollection
    {
        #region Constants

        /// <summary>
        /// The m_n maximum items.
        /// </summary>
        private const int m_nMaximumItems = 10;

        /// <summary>
        /// The m_s value.
        /// </summary>
        private const string m_sValue = "RecentFiles";

        #endregion

        #region Static Fields

        /// <summary>
        /// The m_the list.
        /// </summary>
        private static RecentFilesList m_theList = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="RecentFilesList"/> class from being created.
        /// </summary>
        private RecentFilesList()
        {
            this.Load();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="RecentFilesList"/>.
        /// </returns>
        public static RecentFilesList Get()
        {
            if (m_theList == null)
            {
                m_theList = new RecentFilesList();
            }

            return m_theList;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="sFile">
        /// The s file.
        /// </param>
        public new void Add(string sFile)
        {
            int nFileIndex = this.FindFile(sFile);

            if (nFileIndex < 0)
            {
                this.Insert(0, sFile);

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

            this.Save();
        }

        /// <summary>
        /// The save.
        /// </summary>
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

        #endregion

        #region Methods

        /// <summary>
        /// The find file.
        /// </summary>
        /// <param name="sFile">
        /// The s file.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
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

        /// <summary>
        /// The load.
        /// </summary>
        private void Load()
        {
            RegistryKey registryKey = ApplicationSettingsKey.Get(false);

            if (registryKey != null)
            {
                string sValue = registryKey.GetValue(m_sValue) as string;

                if (sValue != null)
                {
                    string[] asFiles = sValue.Split(new[] { '|' });

                    foreach (string sFile in asFiles)
                    {
                        base.Add(sFile);
                    }
                }

                registryKey.Close();
            }
        }

        #endregion
    }
}