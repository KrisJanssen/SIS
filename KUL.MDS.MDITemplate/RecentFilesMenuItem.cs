namespace SIS.MDITemplate
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class RecentFilesMenuItem : MdiMenuItem
	{
		private Hashtable m_mapMenuItemToFile = null;
		
		public delegate void OpenFileHandler(string sFile);
		public event OpenFileHandler OpenFile = null;

		public RecentFilesMenuItem()
		{
			this.m_mapMenuItemToFile = new Hashtable();
			this.Text = "Recent files";
			
			this.Load();
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
			this.m_mapMenuItemToFile.Clear();

			foreach (string sFile in RecentFilesList.Get())
			{
				MenuItem itemFile = new MenuItem(FileNameHelpers.GetFileName(sFile));
				this.MenuItems.Add(itemFile);
				itemFile.Click += new EventHandler(this.MenuItem_Click);
				this.m_mapMenuItemToFile.Add(itemFile, sFile);
			}

			base.Enabled = (this.m_mapMenuItemToFile.Count > 0);
		}

		private void MenuItem_Click(object sender, EventArgs e)
		{
			string sFile = this.m_mapMenuItemToFile[sender] as string;

			if (sFile != null)
			{
				if (this.OpenFile != null)
				{
					this.OpenFile(sFile);
				}
			}

			StatusBarMessenger.SetMessage(StatusBarMessenger.DefaultMessage);
		}

		protected override void OnTestEnabled()
		{
			this.Load();
		}

	}
}
