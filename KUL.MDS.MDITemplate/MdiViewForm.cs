namespace SIS.MDITemplate
{
    using System;

    //public class MdiViewForm : System.Windows.Forms.Form
    public class MdiViewForm : BaseForm
	{
		private MdiDocument m_document = null;

		private System.ComponentModel.Container components = null;

		public MdiViewForm()
		{
			this.InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(this.components != null)
				{
					this.components.Dispose();
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
				return this.m_document;
			}

			set
			{
				this.m_document = value;
			}
		}

		public void InitialUpdate()
		{
			this.Text = this.Document.FileName;
			this.OnInitialUpdate();
		}

		public void UpdateDocument()
		{
			this.OnUpdateDocument();
		}

		public void UpdateViewTitle()
		{
			if (this.Document.Modified)
			{
				this.Text = string.Format("{0}*", this.Document.FileName);
			}
			else
			{
				this.Text = this.Document.FileName;
			}
		}

		public void UpdateView(object update)
		{
			this.OnUpdateView(update);
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
