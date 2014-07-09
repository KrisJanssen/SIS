// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Branding.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Provides methods that control branding aspects of SIS. For instance,
//   the URL we ping for update manifests, and the e-mail address to send
//   feedback to.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    /// <summary>
    /// Provides methods that control branding aspects of SIS. For instance,
    /// the URL we ping for update manifests, and the e-mail address to send
    /// feedback to.
    /// </summary>
    public static class Branding
    {
        #region Constants

        /// <summary>
        /// The feedback email.
        /// </summary>
        public const string FeedbackEmail = "kris.janssen@gmail.com";

        /// <summary>
        /// The website url.
        /// </summary>
        public const string WebsiteUrl = "http://www.biosensors.be";

        #endregion
    }
}