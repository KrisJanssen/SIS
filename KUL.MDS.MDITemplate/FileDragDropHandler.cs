namespace SIS.MDITemplate
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class FileDragDropHandler
	{
		private Hashtable m_mapAllowedExtensions = null;

		public delegate void FileDropHandler(string sFileName);
		public event FileDropHandler FileDropped = null;

		public FileDragDropHandler(Form form, string [] asExtensions)
		{
			this.InitialiseEvents(form);
			this.InitialiseExtensionMap(asExtensions);
		}

		private void InitialiseEvents(Form form)
		{
			form.AllowDrop = true;
			form.DragEnter += new DragEventHandler(this.form_DragEnter);
			form.DragLeave += new EventHandler(this.form_DragLeave);
			form.DragDrop += new DragEventHandler(this.form_DragDrop);
		}

		private void InitialiseExtensionMap(string [] asExtensions)
		{
			this.m_mapAllowedExtensions = new Hashtable();

			if (asExtensions != null)
			{
				foreach (string sExtension in asExtensions)
				{
					this.m_mapAllowedExtensions.Add(sExtension, null);
				}
			}
		}

		private void form_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				if (this.TestExtensions(e))
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

					if (!this.m_mapAllowedExtensions.Contains(sExtension))
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

			if (asFiles != null && this.FileDropped != null)
			{
				foreach (string sFileName in asFiles)
				{
					this.FileDropped(sFileName);
				}
			}
		}
	}
}
