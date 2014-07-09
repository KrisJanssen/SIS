namespace SIS.MDITemplate
{
    using System.Windows.Forms;

    public class MessengerStatusBar : StatusBar
	{
		public MessengerStatusBar()
		{
			StatusBarMessenger.Message += new StatusBarMessenger.MessageHandler(this.StatusBarMessenger_Message);
		}

		private void StatusBarMessenger_Message(string sMessage)
		{
			this.Text = sMessage;
		}
	}
}
