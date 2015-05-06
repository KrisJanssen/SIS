using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SIS.MDITemplate
{
	//public class MdiViewForm : System.Windows.Forms.Form
    public class MdiViewForm : SIS.MDITemplate.BaseForm
	{
		private MdiDocument m_document = null;

		private System.ComponentModel.Container components = null;

		public MdiViewForm()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // MdiViewForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.DoubleBuffered = true;
            this.Name = "MdiViewForm";
            this.ShowInTaskbar = false;
            this.Text = "MdiViewForm";
            this.ResumeLayout(false);

		}
		#endregion

		public MdiDocument Document
		{
			get
			{
				return m_document;
			}

			set
			{
				m_document = value;
			}
		}

		public void InitialUpdate()
		{
			this.Text = Document.FileName;
			OnInitialUpdate();
		}

		public void UpdateDocument()
		{
			OnUpdateDocument();
		}

		public void UpdateViewTitle()
		{
			if (Document.Modified)
			{
				this.Text = string.Format("{0}*", Document.FileName);
			}
			else
			{
				this.Text = Document.FileName;
			}
		}

		public void UpdateView(object update)
		{
			OnUpdateView(update);
		}

		protected virtual void OnUpdateView(object update)
		{
		}

		protected virtual void OnUpdateDocument()
		{
		}

		protected virtual void OnInitialUpdate()
		{
		}

		protected override void OnActivated(EventArgs e)
		{
			MdiDocument.ActiveDocument = this.Document;

			base.OnActivated (e);
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);

			if (MdiDocument.ActiveDocument == this.Document)
			{
				MdiDocument.ActiveDocument = null;
			}
		}

	}
}
