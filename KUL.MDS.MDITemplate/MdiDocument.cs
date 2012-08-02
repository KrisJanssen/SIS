using System;
using System.Collections;
using System.Windows.Forms;

namespace KUL.MDS.MDITemplate
{
	public abstract class MdiDocument
	{
		static private ArrayList m_aDocuments = null;
		static private MdiDocument m_documentActive = null;
		private ArrayList m_aViews = null;
		private string m_sFilePath = null;
		bool m_fModified = false;

		public MdiDocument()
		{
			m_aViews = new ArrayList();

			if (m_aDocuments == null)
			{
				m_aDocuments = new ArrayList();
			}

			m_aDocuments.Add(this);
		}

		public void AddView(MdiViewForm view)
		{
			m_aViews.Add(view);

			view.Closing += new System.ComponentModel.CancelEventHandler(view_Closing);
		}

		public MdiViewForm[] Views
		{
			get
			{
				return m_aViews.ToArray(typeof(MdiViewForm)) as MdiViewForm[];
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
				if (m_sFilePath == null)
				{
					return "";
				}
				else
				{
					return FileNameHelpers.GetFileName(m_sFilePath);
				}
			}
		}

		public string FilePath
		{
			get
			{
				if (m_sFilePath == null)
				{
					return "";
				}
				else
				{
					return m_sFilePath;
				}
			}
		}

		public void UpdateAllViews(MdiViewForm formFrom, object update)
		{
			foreach (MdiViewForm view in m_aViews)
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
				return m_fModified;
			}

			set
			{
				m_fModified = value;
				UpdateViewTitles();
			}
		}

		private void UpdateViewTitles()
		{
			foreach (MdiViewForm view in m_aViews)
			{
				view.UpdateViewTitle();
			}
		}

		private void view_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (m_aViews.Count == 1 && m_fModified)
			{
				switch (MessageBox.Show("This document has been modified. Do you want to save ?", this.FileName, 
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
				{
					case DialogResult.Yes:
						SaveDocument(m_sFilePath);
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
			m_sFilePath = sFilePath;

			if (fAddToRecentFiles)
			{
				RecentFilesList.Get().Add(sFilePath);
			}

			return OnLoadDocument(sFilePath);
		}

		public bool SaveDocument(string sFilePath)
		{
			UpdateDocument();
			this.Modified = false;
			if (OnSaveDocument(sFilePath))
			{
				m_sFilePath = sFilePath;
				UpdateViewTitles();

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
			foreach (MdiViewForm view in m_aViews)
			{
				view.UpdateDocument();
			}
		}

		public MdiViewForm CreateView()
		{
			MdiViewForm view = OnCreateView();

			if (view == null)
			{
				return null;
			}
			else
			{
				AddView(view);
				return view;
			}
		}

		protected abstract bool OnLoadDocument(string sFilePath);
		protected abstract MdiViewForm OnCreateView();
		protected abstract bool OnSaveDocument(string sFilePath);
	}
}
