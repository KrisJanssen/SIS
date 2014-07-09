// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Stores non-volatile name/value settings. These persist between sessions of the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
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
    public sealed class Settings : ISimpleCollection<string, string>
    {
        #region Constants

        /// <summary>
        /// The hkcu key.
        /// </summary>
        private const string hkcuKey = @"SOFTWARE\SIS";

        /// <summary>
        /// The point x suffix.
        /// </summary>
        private const string pointXSuffix = ".X";

        /// <summary>
        /// The point y suffix.
        /// </summary>
        private const string pointYSuffix = ".Y";

        #endregion

        #region Static Fields

        /// <summary>
        /// The current user.
        /// </summary>
        public static readonly Settings CurrentUser = new Settings(Microsoft.Win32.Registry.CurrentUser);

        /// <summary>
        /// The system wide.
        /// </summary>
        public static readonly Settings SystemWide = new Settings(Microsoft.Win32.Registry.LocalMachine);

        #endregion

        #region Fields

        /// <summary>
        /// The root key.
        /// </summary>
        private RegistryKey rootKey;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="rootKey">
        /// The root key.
        /// </param>
        private Settings(RegistryKey rootKey)
        {
            this.rootKey = rootKey;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Deletes a settings key.
        /// </summary>
        /// <param name="key">
        /// The key to delete.
        /// </param>
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
        /// <param name="keys">
        /// The keys to delete.
        /// </param>
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
        /// The get.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Get(string key)
        {
            return this.GetString(key);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        public bool GetBoolean(string key)
        {
            return bool.Parse(this.GetString(key));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
        public bool GetBoolean(string key, bool defaultValue)
        {
            return bool.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Gets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        /// <remarks>
        /// This method treats the key value as a stream of base64 encoded bytes that represent a PNG image.
        /// </remarks>
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
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        public int GetInt32(string key)
        {
            return int.Parse(this.GetString(key));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
        public int GetInt32(string key, int defaultValue)
        {
            return int.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
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
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
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
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        public Point GetPoint(string key)
        {
            int x = this.GetInt32(key + pointXSuffix);
            int y = this.GetInt32(key + pointYSuffix);
            return new Point(x, y);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
        public Point GetPoint(string key, Point defaultValue)
        {
            int x = this.GetInt32(key + pointXSuffix, defaultValue.X);
            int y = this.GetInt32(key + pointYSuffix, defaultValue.Y);
            return new Point(x, y);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
        public float GetSingle(string key, float defaultValue)
        {
            return float.Parse(this.GetString(key, defaultValue.ToString()));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        public float GetSingle(string key)
        {
            return float.Parse(this.GetString(key));
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <returns>
        /// The value of the key.
        /// </returns>
        public string GetString(string key)
        {
            return (string)this.GetObject(key);
        }

        /// <summary>
        /// Retrieves the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to use if the key doesn't exist.
        /// </param>
        /// <returns>
        /// The value of the key, or defaultValue if it didn't exist.
        /// </returns>
        public string GetString(string key, string defaultValue)
        {
            return (string)this.GetObject(key, defaultValue);
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Set(string key, string value)
        {
            this.SetString(key, value);
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetBoolean(string key, bool value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        /// <remarks>
        /// This method saves the key value as a stream of base64 encoded bytes that represent a PNG image.
        /// </remarks>
        public void SetImage(string key, Image value)
        {
            MemoryStream ms = new MemoryStream();
            value.Save(ms, ImageFormat.Png);
            byte[] buffer = ms.GetBuffer();
            string base64 = Convert.ToBase64String(buffer);
            this.SetString(key, base64);
            ms.Close();
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetInt32(string key, int value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetObject(string key, object value)
        {
            using (RegistryKey SISKey = this.CreateSettingsKey(true))
            {
                SISKey.SetValue(key, value);
            }
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetPoint(string key, Point value)
        {
            this.SetInt32(key + pointXSuffix, value.X);
            this.SetInt32(key + pointYSuffix, value.Y);
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetSingle(string key, float value)
        {
            this.SetString(key, value.ToString());
        }

        /// <summary>
        /// Sets the value of a settings key.
        /// </summary>
        /// <param name="key">
        /// The name of the key to set.
        /// </param>
        /// <param name="value">
        /// The new value of the key.
        /// </param>
        public void SetString(string key, string value)
        {
            this.SetObject(key, value);
        }

        /// <summary>
        /// Saves the given strings.
        /// </summary>
        /// <param name="nvc">
        /// The nvc.
        /// </param>
        public void SetStrings(NameValueCollection nvc)
        {
            foreach (string key in nvc.Keys)
            {
                string value = nvc[key];
                this.SetString("Test\\" + key, value);
            }
        }

        /// <summary>
        /// The try delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
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

        #endregion

        #region Methods

        /// <summary>
        /// The create settings key.
        /// </summary>
        /// <param name="writable">
        /// The writable.
        /// </param>
        /// <returns>
        /// The <see cref="RegistryKey"/>.
        /// </returns>
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

        #endregion
    }
}