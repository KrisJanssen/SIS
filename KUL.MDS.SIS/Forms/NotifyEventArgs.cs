// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyEventArgs.cs" company="">
//   
// </copyright>
// <summary>
//   The notify event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Forms
{
    using SIS.Documents;

    /// <summary>
    /// The notify event args.
    /// </summary>
    public class NotifyEventArgs : System.EventArgs
    {
        #region Fields

        /// <summary>
        /// The settings.
        /// </summary>
        public readonly ScanSettings Settings;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyEventArgs"/> class.
        /// </summary>
        /// <param name="__scnstSettings">
        /// The __scnst settings.
        /// </param>
        public NotifyEventArgs(ScanSettings __scnstSettings)
        {
            this.Settings = __scnstSettings;
        }

        #endregion
    }
}