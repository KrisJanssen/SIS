using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;

namespace SIS.MDITemplate
{
	public class FormPositionSerializer
	{
		private string m_sName = null;
		private const string m_sKey = @"Settings\WindowPositions";

		public FormPositionSerializer(Form form, string sName)
		{
			m_sName = sName;

			form.Closing += new System.ComponentModel.CancelEventHandler(form_Closing);
			RegistryKey registryKey = ApplicationSettingsKey.Get(false);

			if (registryKey != null)
			{
				RegistryKey subKey = registryKey.OpenSubKey(m_sKey, false);

				if (subKey != null)
				{
					string sPosition = subKey.GetValue(m_sName) as string;

					if (sPosition != null && sPosition.Length > 0)
					{
						System.Drawing.RectangleConverter converter = new RectangleConverter();
						System.Drawing.Rectangle rectWindow = (System.Drawing.Rectangle)converter.ConvertFromString(sPosition);

						form.Bounds = rectWindow;
						form.StartPosition = FormStartPosition.Manual;
					}

					subKey.Close();
				}

				registryKey.Close();
			}
		}
		
		private void form_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

					subKey.SetValue(m_sName, sRectangle);
					subKey.Close();

					registryKey.Close();
				}
			}	
		}
	}
}