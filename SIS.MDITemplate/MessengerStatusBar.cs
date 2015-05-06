using System;
using System.Windows.Forms;

namespace SIS.MDITemplate
{
	public class MessengerStatusBar : StatusBar
	{
		public MessengerStatusBar()
		{
			StatusBarMessenger.Message += new SIS.MDITemplate.StatusBarMessenger.MessageHandler(StatusBarMessenger_Message);
		}

		private void StatusBarMessenger_Message(string sMessage)
		{
			this.Text = sMessage;
		}
	}
}
