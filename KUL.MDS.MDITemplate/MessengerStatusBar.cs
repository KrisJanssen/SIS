// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessengerStatusBar.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The messenger status bar.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.Windows.Forms;

    /// <summary>
    /// The messenger status bar.
    /// </summary>
    public class MessengerStatusBar : StatusBar
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MessengerStatusBar"/> class.
        /// </summary>
        public MessengerStatusBar()
        {
            StatusBarMessenger.Message += new StatusBarMessenger.MessageHandler(this.StatusBarMessenger_Message);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The status bar messenger_ message.
        /// </summary>
        /// <param name="sMessage">
        /// The s message.
        /// </param>
        private void StatusBarMessenger_Message(string sMessage)
        {
            this.Text = sMessage;
        }

        #endregion
    }
}