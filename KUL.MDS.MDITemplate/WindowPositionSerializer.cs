// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowPositionSerializer.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The form position serializer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using Microsoft.Win32;

    /// <summary>
    /// The form position serializer.
    /// </summary>
    public class FormPositionSerializer
    {
        #region Constants

        /// <summary>
        /// The m_s key.
        /// </summary>
        private const string m_sKey = @"Settings\WindowPositions";

        #endregion

        #region Fields

        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormPositionSerializer"/> class.
        /// </summary>
        /// <param name="form">
        /// The form.
        /// </param>
        /// <param name="sName">
        /// The s name.
        /// </param>
        public FormPositionSerializer(Form form, string sName)
        {
            this.m_sName = sName;

            form.Closing += new System.ComponentModel.CancelEventHandler(this.form_Closing);
            RegistryKey registryKey = ApplicationSettingsKey.Get(false);

            if (registryKey != null)
            {
                RegistryKey subKey = registryKey.OpenSubKey(m_sKey, false);

                if (subKey != null)
                {
                    string sPosition = subKey.GetValue(this.m_sName) as string;

                    if (sPosition != null && sPosition.Length > 0)
                    {
                        RectangleConverter converter = new RectangleConverter();
                        Rectangle rectWindow = (System.Drawing.Rectangle)converter.ConvertFromString(sPosition);

                        form.Bounds = rectWindow;
                        form.StartPosition = FormStartPosition.Manual;
                    }

                    subKey.Close();
                }

                registryKey.Close();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The form_ closing.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void form_Closing(object sender, CancelEventArgs e)
        {
            Form form = sender as Form;

            if (form.Visible && form.WindowState != FormWindowState.Minimized)
            {
                RegistryKey registryKey = ApplicationSettingsKey.Get(true);

                if (registryKey != null)
                {
                    RegistryKey subKey = registryKey.OpenSubKey(m_sKey, true);

                    if (subKey == null)
                    {
                        subKey = registryKey.CreateSubKey(m_sKey);

                        if (subKey == null)
                        {
                            return;
                        }
                    }

                    RectangleConverter converter = new RectangleConverter();
                    string sRectangle = converter.ConvertToString(form.Bounds);

                    subKey.SetValue(this.m_sName, sRectangle);
                    subKey.Close();

                    registryKey.Close();
                }
            }
        }

        #endregion
    }
}