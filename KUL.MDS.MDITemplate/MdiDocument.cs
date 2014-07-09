namespace SIS.MDITemplate
{
    using System.Collections;
    using System.Windows.Forms;

    public abstract class MdiDocument
	{
		static private ArrayList m_aDocuments = null;
		static private MdiDocument m_documentActive = null;
		private ArrayList m_aViews = null;
		private string m_sFilePath = null;
		bool m_fModified = false;

		public MdiDocument()
		{
			this.m_aViews = new ArrayList();

			if (m_aDocuments == null)
			{
				m_aDocuments = new ArrayList();
			}

			m_aDocuments.Add(this);
		}

		public void AddView(MdiViewForm view)
		{
			this.m_aViews.Add(view);

			view.Closing += new System.ComponentModel.CancelEventHandler(this.view_Closing);
		}

		public MdiViewForm[] Views
		{
			get
			{
				return this.m_aViews.ToArray(typeof(MdiViewForm)) as MdiViewForm[];
			}
		}

		static public MdiDocument ActiveDocument
		{
			get
			{
				return m_documentActive;
			}

			set
			{
				m_documentActive = value;
			}
		}

		static public MdiDocument[] Documents
		{
			get
			{
				if (m_aDocuments == null)
				{
					return new MdiDocument[0];
				}
				else
				{
					return m_aDocuments.ToArray(typeof(MdiDocument)) as MdiDocument[];
				}
			}
		}

		public string FileName
		{
			get
			{
				if (this.m_sFilePath == null)
				{
					return "";
				}
				else
				{
					return FileNameHelpers.GetFileName(this.m_sFilePath);
				}
			}
		}

		public string FilePath
		{
			get
			{
				if (this.m_sFilePath == null)
				{
					return "";
				}
				else
				{
					return this.m_sFilePath;
				}
			}
		}

		public void UpdateAllViews(MdiViewForm formFrom, object update)
		{
			foreach (MdiViewForm view in this.m_aViews)
			{
				if (view != formFrom)
				{
					view.UpdateView(update);
				}
			}
		}

		public bool Modified
		{
			get
			{
				return this.m_fModified;
			}

			set
			{
				this.m_fModified = value;
				this.UpdateViewTitles();
			}
		}

		private void UpdateViewTitles()
		{
			foreach (MdiViewForm view in this.m_aViews)
			{
				view.UpdateViewTitle();
			}
		}

		private void view_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (this.m_aViews.Count == 1 && this.m_fModified)
			{
				switch (MessageBox.Show("This document has been modified. Do you want to save ?", this.FileName, 
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						this.SaveDocument(this.m_sFilePath);
						m_aDocuments.Remove(this);
						break;
					case DialogResult.No:
						m_aDocuments.Remove(this);
						break;
					case DialogResult.Cancel:
						e.Cancel = true;
						break;
				}
			}
			else
			{
				m_aDocuments.Remove(this);
			}
		}

		public bool LoadDocument(string sFilePath, bool fAddToRecentFiles)
		{
			this.m_sFilePath = sFilePath;

			if (fAddToRecentFiles)
			{
				RecentFilesList.Get().Add(sFilePath);
			}

			return this.OnLoadDocument(sFilePath);
		}

		public bool SaveDocument(string sFilePath)
		{
			this.UpdateDocument();
			this.Modified = false;
			if (this.OnSaveDocument(sFilePath))
			{
				this.m_sFilePath = sFilePath;
				this.UpdateViewTitles();

				RecentFilesList.Get().Add(sFilePath);

				return true;
			}
			else
			{
				return false;
			}
		}

		public void UpdateDocument()
		{
			foreach (MdiViewForm view in this.m_aViews)
			{
				view.UpdateDocument();
			}
		}

		public MdiViewForm CreateView()
		{
			MdiViewForm view = this.OnCreateView();

			if (view == null)
			{
				return null;
			}
			else
			{
				this.AddView(view);
				return view;
			}
		}

		protected abstract bool OnLoadDocument(string sFilePath);
		protected abstract MdiViewForm OnCreateView();
		protected abstract bool OnSaveDocument(string sFilePath);
	}
}
