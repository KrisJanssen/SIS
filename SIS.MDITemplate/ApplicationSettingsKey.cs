// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationSettingsKey.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The application settings key.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.Windows.Forms;

    using Microsoft.Win32;

    /// <summary>
    /// The application settings key.
    /// </summary>
    public sealed class ApplicationSettingsKey
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="fWrite">
        /// The f write.
        /// </param>
        /// <returns>
        /// The <see cref="RegistryKey"/>.
        /// </returns>
        public static RegistryKey Get(bool fWrite)
        {
            string sAppPath = string.Format(@"Software\{0}\{1}", Application.CompanyName, Application.ProductName);

            RegistryKey key = Registry.CurrentUser.OpenSubKey(sAppPath, fWrite);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(sAppPath);
            }

            return key;
        }

        #endregion
    }
}