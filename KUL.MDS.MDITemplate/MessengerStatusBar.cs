using System;
using System.Windows.Forms;

namespace KUL.MDS.MDITemplate
{
	public class MessengerStatusBar : StatusBar
	{
		public MessengerStatusBar()
		{
			StatusBarMessenger.Message += new KUL.MDS.MDITemplate.StatusBarMessenger.MessageHandler(StatusBarMessenger_Message);
		}

		private void StatusBarMessenger_Message(string sMessage)
		{
			this.Text = sMessage;
		}
	}
}
