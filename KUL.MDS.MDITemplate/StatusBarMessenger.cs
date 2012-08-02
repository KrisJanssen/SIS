using System;

namespace KUL.MDS.MDITemplate
{
	public interface IStatusBarMessage
	{
		string StatusMessage { get; set; }
	} ;

	public abstract class StatusBarMessenger
	{
		private const string m_sDefaultMessage = "Ready";

		public delegate void MessageHandler(string sMessage);

		static public event MessageHandler Message = null;

		private StatusBarMessenger()
		{
		}

		static public void SetMessage(string sMessage)
		{
			if (Message != null)
			{
				Message(sMessage);
			}
		}
		
		static public string DefaultMessage
		{
			get
			{
				return m_sDefaultMessage;
			}
		}
	}
}
