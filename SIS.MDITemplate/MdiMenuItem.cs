using System;
using System.Windows.Forms;

namespace SIS.MDITemplate
{
	public class MdiMenuItem : MenuItem, IStatusBarMessage
	{
		private string m_sStatusMessage = StatusBarMessenger.DefaultMessage;
		private bool m_fNeedsDocument = false;

		public delegate void TestEnabledHelper(MdiMenuItem menuItem);
		public event TestEnabledHelper TestEnabled = null;

		public MdiMenuItem()
		{
		}

		public bool NeedsDocument
		{
			get
			{
				return m_fNeedsDocument;
			}

			set
			{
				m_fNeedsDocument = value;
			}
		}

		public void UpdateMenuItem()
		{
			if (this.IsParent)
			{
				foreach (MenuItem item in this.MenuItems)
				{
					if (item is MdiMenuItem)
					{
						MdiMenuItem itemStatusBar = item as MdiMenuItem;
						itemStatusBar.OnTestEnabled();
					}
				}
			}
		}

		protected override void OnSelect(EventArgs e)
		{
			base.OnSelect (e);

			StatusBarMessenger.SetMessage(m_sStatusMessage);
		}

		protected override void OnClick(EventArgs e)
		{
			base.OnClick (e);

			StatusBarMessenger.SetMessage(StatusBarMessenger.DefaultMessage);
		}

		protected override void OnInitMenuPopup(EventArgs e)
		{
			base.OnInitMenuPopup (e);

			UpdateMenuItem();
		}

		protected virtual void OnTestEnabled()
		{
			if (this.NeedsDocument)
			{
				this.Enabled = MdiDocument.ActiveDocument != null;
			}

			if (TestEnabled != null)
			{
				TestEnabled(this);
			}
		}

		#region IStatusBarMessage Members

		public string StatusMessage
		{
			get
			{
				return m_sStatusMessage;
			}
			set
			{
				m_sStatusMessage = value;
			}
		}

		#endregion
	}
}
