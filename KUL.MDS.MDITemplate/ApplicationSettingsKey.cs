namespace SIS.MDITemplate
{
    using System.Windows.Forms;

    using Microsoft.Win32;

    public sealed class ApplicationSettingsKey
	{
		static public RegistryKey Get(bool fWrite)
		{
			string sAppPath = string.Format(@"Software\{0}\{1}", Application.CompanyName, Application.ProductName);
			
			RegistryKey key = Registry.CurrentUser.OpenSubKey(sAppPath, fWrite);
			
			if (key == null)
			{
				key = Registry.CurrentUser.CreateSubKey(sAppPath);
			}

			return key;
		}		
	}
}
