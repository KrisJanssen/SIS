using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections;

namespace SIS.MDITemplate
{
	public class RecentFilesMenuItem : MdiMenuItem
	{
		private Hashtable m_mapMenuItemToFile = null;
		
		public delegate void OpenFileHandler(string sFile);
		public event OpenFileHandler OpenFile = null;

		public RecentFilesMenuItem()
		{
			m_mapMenuItemToFile = new Hashtable();
			this.Text = "Recent files";
			
			Load();
		}

		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}

			set
			{
			}
		} 

		public void Load()
		{
			this.MenuItems.Clear();
			m_mapMenuItemToFile.Clear();

			foreach (string sFile in RecentFilesList.Get())
			{
				MenuItem itemFile = new MenuItem(FileNameHelpers.GetFileName(sFile));
				this.MenuItems.Add(itemFile);
				itemFile.Click += new EventHandler(MenuItem_Click);
				m_mapMenuItemToFile.Add(itemFile, sFile);
			}

			base.Enabled = (m_mapMenuItemToFile.Count > 0);
		}

		private void MenuItem_Click(object sender, EventArgs e)
		{
			string sFile = m_mapMenuItemToFile[sender] as string;

			if (sFile != null)
			{
				if (OpenFile != null)
				{
					OpenFile(sFile);
				}
			}

			StatusBarMessenger.SetMessage(StatusBarMessenger.DefaultMessage);
		}

		protected override void OnTestEnabled()
		{
			Load();
		}

	}
}
