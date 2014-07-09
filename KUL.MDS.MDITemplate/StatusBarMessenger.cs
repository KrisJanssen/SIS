// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StatusBarMessenger.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The StatusBarMessage interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    /// <summary>
    /// The StatusBarMessage interface.
    /// </summary>
    public interface IStatusBarMessage
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        string StatusMessage { get; set; }

        #endregion
    };

    /// <summary>
    /// The status bar messenger.
    /// </summary>
    public abstract class StatusBarMessenger
    {
        #region Constants

        /// <summary>
        /// The m_s default message.
        /// </summary>
        private const string m_sDefaultMessage = "Ready";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="StatusBarMessenger"/> class from being created.
        /// </summary>
        private StatusBarMessenger()
        {
        }

        #endregion

        #region Delegates

        /// <summary>
        /// The message handler.
        /// </summary>
        /// <param name="sMessage">
        /// The s message.
        /// </param>
        public delegate void MessageHandler(string sMessage);

        #endregion

        #region Public Events

        /// <summary>
        /// The message.
        /// </summary>
        public static event MessageHandler Message = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the default message.
        /// </summary>
        public static string DefaultMessage
        {
            get
            {
                return m_sDefaultMessage;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set message.
        /// </summary>
        /// <param name="sMessage">
        /// The s message.
        /// </param>
        public static void SetMessage(string sMessage)
        {
            if (Message != null)
            {
                Message(sMessage);
            }
        }

        #endregion
    }
}