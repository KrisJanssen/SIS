using System;
using System.Windows.Forms;
using System.Collections;

namespace SIS.MDITemplate
{
	public class FileDragDropHandler
	{
		private Hashtable m_mapAllowedExtensions = null;

		public delegate void FileDropHandler(string sFileName);
		public event FileDropHandler FileDropped = null;

		public FileDragDropHandler(Form form, string [] asExtensions)
		{
			InitialiseEvents(form);
			InitialiseExtensionMap(asExtensions);
		}

		private void InitialiseEvents(Form form)
		{
			form.AllowDrop = true;
			form.DragEnter += new DragEventHandler(form_DragEnter);
			form.DragLeave += new EventHandler(form_DragLeave);
			form.DragDrop += new DragEventHandler(form_DragDrop);
		}

		private void InitialiseExtensionMap(string [] asExtensions)
		{
			m_mapAllowedExtensions = new Hashtable();

			if (asExtensions != null)
			{
				foreach (string sExtension in asExtensions)
				{
					m_mapAllowedExtensions.Add(sExtension, null);
				}
			}
		}

		private void form_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				if (TestExtensions(e))
				{
					e.Effect = DragDropEffects.Copy;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}
			else
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private bool TestExtensions(DragEventArgs e)
		{
			string [] asFiles = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (asFiles == null)
			{
				return false;
			}
			else
			{
				foreach (string sFileName in asFiles)
				{
					string sExtension = FileNameHelpers.GetExtension(sFileName);

					if (!m_mapAllowedExtensions.Contains(sExtension))
					{
						return false;
					}
				}

				return true;
			}
		}

		private void form_DragLeave(object sender, EventArgs e)
		{
		}

		private void form_DragDrop(object sender, DragEventArgs e)
		{
			string [] asFiles = e.Data.GetData(DataFormats.FileDrop) as string[];

			if (asFiles != null && FileDropped != null)
			{
				foreach (string sFileName in asFiles)
				{
					FileDropped(sFileName);
				}
			}
		}
	}
}
