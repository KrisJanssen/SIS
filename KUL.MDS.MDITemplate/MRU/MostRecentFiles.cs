// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MostRecentFiles.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Data structure to manage the Most Recently Used list of files.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate.MRU
{
    using System;
    using System.Collections;
    using System.Drawing;

    using SIS.MDITemplate.Settings;
    using SIS.Systemlayer;
    using SIS.Systemlayer.Settings;

    /// <summary>
    /// Data structure to manage the Most Recently Used list of files.
    /// </summary>
    internal class MostRecentFiles
    {
        #region Constants

        /// <summary>
        /// The icon size.
        /// </summary>
        private const int iconSize = 56;

        #endregion

        #region Fields

        /// <summary>
        /// The files.
        /// </summary>
        private Queue files; // contains MostRecentFile instances

        /// <summary>
        /// The loaded.
        /// </summary>
        private bool loaded = false;

        /// <summary>
        /// The max count.
        /// </summary>
        private int maxCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MostRecentFiles"/> class.
        /// </summary>
        /// <param name="maxCount">
        /// The max count.
        /// </param>
        public MostRecentFiles(int maxCount)
        {
            this.maxCount = maxCount;
            this.files = new Queue();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                if (!this.loaded)
                {
                    this.LoadMruList();
                }

                return this.files.Count;
            }
        }

        /// <summary>
        /// Gets the icon size.
        /// </summary>
        public int IconSize
        {
            get
            {
                return UI.ScaleWidth(iconSize);
            }
        }

        /// <summary>
        /// Gets a value indicating whether loaded.
        /// </summary>
        public bool Loaded
        {
            get
            {
                return this.loaded;
            }
        }

        /// <summary>
        /// Gets the max count.
        /// </summary>
        public int MaxCount
        {
            get
            {
                return this.maxCount;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="mrf">
        /// The mrf.
        /// </param>
        public void Add(MostRecentFile mrf)
        {
            if (!this.Loaded)
            {
                this.LoadMruList();
            }

            if (!this.Contains(mrf.FileName))
            {
                this.files.Enqueue(mrf);

                while (this.files.Count > this.maxCount)
                {
                    this.files.Dequeue();
                }
            }
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            if (!this.Loaded)
            {
                this.LoadMruList();
            }

            foreach (MostRecentFile mrf in this.GetFileList())
            {
                this.Remove(mrf.FileName);
            }
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Contains(string fileName)
        {
            if (!this.Loaded)
            {
                this.LoadMruList();
            }

            string lcFileName = fileName.ToLower();

            foreach (MostRecentFile mrf in this.files)
            {
                string lcMrf = mrf.FileName.ToLower();

                if (0 == string.Compare(lcMrf, lcFileName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The get file list.
        /// </summary>
        /// <returns>
        /// The <see cref="MostRecentFile[]"/>.
        /// </returns>
        public MostRecentFile[] GetFileList()
        {
            if (!this.Loaded)
            {
                this.LoadMruList();
            }

            object[] array = this.files.ToArray();
            MostRecentFile[] mrfArray = new MostRecentFile[array.Length];
            array.CopyTo(mrfArray, 0);
            return mrfArray;
        }

        /// <summary>
        /// The load mru list.
        /// </summary>
        public void LoadMruList()
        {
            try
            {
                this.loaded = true;

                this.Clear();

                for (int i = 0; i < this.MaxCount; ++i)
                {
                    try
                    {
                        string mruName = "MRU" + i.ToString();
                        string fileName = (string)Settings.CurrentUser.GetString(mruName);

                        if (fileName != null)
                        {
                            Image thumb = Settings.CurrentUser.GetImage(mruName + "Thumb");

                            if (fileName != null && thumb != null)
                            {
                                MostRecentFile mrf = new MostRecentFile(fileName, thumb);
                                this.Add(mrf);
                            }
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Tracing.Ping("Exception when loading MRU list: " + ex.ToString());
                this.Clear();
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void Remove(string fileName)
        {
            if (!this.Loaded)
            {
                this.LoadMruList();
            }

            if (!this.Contains(fileName))
            {
                return;
            }

            Queue newQueue = new Queue();

            foreach (MostRecentFile mrf in this.files)
            {
                if (0 != string.Compare(mrf.FileName, fileName, true))
                {
                    newQueue.Enqueue(mrf);
                }
            }

            this.files = newQueue;
        }

        /// <summary>
        /// The save mru list.
        /// </summary>
        public void SaveMruList()
        {
            if (this.Loaded)
            {
                Settings.CurrentUser.SetInt32(SettingNames.MruMax, this.MaxCount);
                MostRecentFile[] mrfArray = this.GetFileList();

                for (int i = 0; i < this.MaxCount; ++i)
                {
                    string mruName = "MRU" + i.ToString();
                    string mruThumbName = mruName + "Thumb";

                    if (i >= mrfArray.Length)
                    {
                        Settings.CurrentUser.Delete(mruName);
                        Settings.CurrentUser.Delete(mruThumbName);
                    }
                    else
                    {
                        MostRecentFile mrf = mrfArray[i];
                        Settings.CurrentUser.SetString(mruName, mrf.FileName);
                        Settings.CurrentUser.SetImage(mruThumbName, mrf.Thumb);
                    }
                }
            }
        }

        #endregion
    }
}