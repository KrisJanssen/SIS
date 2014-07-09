﻿/////////////////////////////////////////////////////////////////////////////////
// Paint.NET                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

namespace SIS.Systemlayer.Settings
{
    using System;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    using Microsoft.Win32;

    /// <summary>
    /// Stores non-volatile name/value settings. These persist between sessions of the application.
    /// </summary>
    /// <remarks>
    /// On Windows, this class uses the registry.
    /// </remarks>
    public sealed class Settings
        : ISimpleCollection<string, string>
    {
        private const string hkcuKey = @"SOFTWARE\SIS";

        public static readonly Settings SystemWide = new Settings(Microsoft.Win32.Registry.LocalMachine);
        public static readonly Settings CurrentUser = new Settings(Microsoft.Win32.Registry.CurrentUser);

        private const string pointXSuffix = ".X";
        private const string pointYSuffix = ".Y";

        private RegistryKey rootKey;

        private Settings(RegistryKey rootKey)
        {
            this.rootKey = rootKey;
        }

        private RegistryKey CreateSettingsKey(bool writable)
        {
            RegistryKey softwareKey = null;

            try
            {
                softwareKey = this.rootKey.OpenSubKey(hkcuKey, writable);
            }

            catch (Exception)
            {
                softwareKey = null;
            }

            if (softwareKey == null)
            {
                try
                {
                    softwareKey = this.rootKey.CreateSubKey(hkcuKey);
                }

                catch (Exception)
                {
                    throw;
                }
            }

            return softwareKey;
        }

        public bool TryDelete(string key)
        {
            try
            {
                this.Delete(key);
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a settings key.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        public void Delete(string key)
        {
            using (RegistryKey SISKey = this.CreateSettingsKey(true))
            {
                SISKey.DeleteValue(key, false);
            }
        }

        /// <summary>
        /// Deletes several settings keys.
        /// </summary>
        /// <param name="keys">The keys to delete.</param>
        public void Delete(string[] keys)
        {
            using (RegistryKey SISKey = this.CreateSettingsKey(true))
            {
                foreach (string key in keys)
                {
                    SISKey.DeleteValue(key, false);
                }
            }
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public object GetObject(string key)
        {
            using (RegistryKey SISKey = this.CreateSettingsKey(false))
            {
                return SISKey.GetValue(key);
            }
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public object GetObject(string key, object defaultValue)
        {
            try
            {
                using (RegistryKey SISKey = this.CreateSettingsKey(false))
                {
                    return SISKey.GetValue(key, defaultValue);
                }
            }

            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetObject(string key, object value)
        {
            using (RegistryKey SISKey = this.CreateSettingsKey(true))
            {
                SISKey.SetValue(key, value);
            }
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public string GetString(string key)
        {
            return (string)this.GetObject(key);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public string GetString(string key, string defaultValue)
        {
            return (string)this.GetObject(key, defaultValue);
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetString(string key, string value)
        {
            this.SetObject(key, value);
        }

        /// <summary>
        /// Saves the given strings.
        /// </summary>
        public void SetStrings(NameValueCollection nvc)
        {
            foreach (string key in nvc.Keys)
            {
                string value = nvc[key];
                this.SetString("Test\\" + key, value);
            }
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public bool GetBoolean(string key)
        {
            return bool.Parse(this.GetString(key));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public bool GetBoolean(string key, bool defaultValue)
        {
            return bool.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetBoolean(string key, bool value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public Point GetPoint(string key)
        {
            int x = this.GetInt32(key + pointXSuffix);
            int y = this.GetInt32(key + pointYSuffix);
            return new Point(x, y);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public Point GetPoint(string key, Point defaultValue)
        {
            int x = this.GetInt32(key + pointXSuffix, defaultValue.X);
            int y = this.GetInt32(key + pointYSuffix, defaultValue.Y);
            return new Point(x, y);
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetPoint(string key, Point value)
        {
            this.SetInt32(key + pointXSuffix, value.X);
            this.SetInt32(key + pointYSuffix, value.Y);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public Int32 GetInt32(string key)
        {
            return Int32.Parse(this.GetString(key));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public Int32 GetInt32(string key, Int32 defaultValue)
        {
            return Int32.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <param name="defaultValue">The default value to use if the key doesn't exist.</param>
        /// <returns>The value of the key, or defaultValue if it didn't exist.</returns>
        public float GetSingle(string key, float defaultValue)
        {
            return Single.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        public float GetSingle(string key)
        {
            return Single.Parse(this.GetString(key));
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetInt32(string key, int value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        public void SetSingle(string key, float value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Gets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to retrieve.</param>
        /// <returns>The value of the key.</returns>
        /// <remarks>This method treats the key value as a stream of base64 encoded bytes that represent a PNG image.</remarks>
        public Image GetImage(string key)
        {
            string imageB64 = this.GetString(key);
            byte[] pngBytes = Convert.FromBase64String(imageB64);
            MemoryStream ms = new MemoryStream(pngBytes);
            Image image = Image.FromStream(ms);
            ms.Close();
            return image;
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">The name of the key to set.</param>
        /// <param name="value">The new value of the key.</param>
        /// <remarks>This method saves the key value as a stream of base64 encoded bytes that represent a PNG image.</remarks>
        public void SetImage(string key, Image value)
        {
            MemoryStream ms = new MemoryStream();
            value.Save(ms, ImageFormat.Png);
            byte[] buffer = ms.GetBuffer();
            string base64 = Convert.ToBase64String(buffer);
            this.SetString(key, base64);
            ms.Close();
        }

        public string Get(string key)
        {
            return this.GetString(key);
        }

        public void Set(string key, string value)
        {
            this.SetString(key, value);
        }
    }
}
