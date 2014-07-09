/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

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
        private Queue files; // contains MostRecentFile instances
        private int maxCount;
        private const int iconSize = 56;
        private bool loaded = false;

        public MostRecentFiles(int maxCount)
        {
            this.maxCount = maxCount;
            this.files = new Queue();
        }

        public bool Loaded
        {
            get
            {
                return this.loaded;
            }
        }

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

        public int MaxCount
        {
            get
            {
                return this.maxCount;
            }
        }

        public int IconSize
        {
            get
            {
                return UI.ScaleWidth(iconSize);
            }
        }

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

                if (0 == String.Compare(lcMrf, lcFileName))
                {
                    return true;
                }
            }

            return false;
        }

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

        public void LoadMruList()
        {
            try
            {
                this.loaded = true;

                //
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
    }
}
