// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvariantStrings.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Contains strings that must be the same no matter what locale the UI is running with.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Resources
{
    /// <summary>
    /// Contains strings that must be the same no matter what locale the UI is running with.
    /// </summary>
    public static class InvariantStrings
    {
        // {0} is "All Rights Reserved"
        // Legal has advised that's the only part of this string that should be localizable.
        #region Constants

        /// <summary>
        /// The copyright format.
        /// </summary>
        public const string CopyrightFormat =
            "Copyright © 2009 Kris Janssen.\r\n" + "Portions Copyright © 2009 Rick Brewster and dotPDN LLC. {0}";

        /// <summary>
        /// The crash log header text format fallback.
        /// </summary>
        public const string CrashLogHeaderTextFormatFallback =
            @"This text file was created because SIS crashed."
            + "Please e-mail this file to {0} so we can diagnose and fix the problem.";

        /// <summary>
        /// The crashlog email.
        /// </summary>
        public const string CrashlogEmail = "kris.janssen@biw.kuleuven.be";

        // <-- You must specify a contact e-mail address to be placed in the crash log.;

        /// <summary>
        /// The dll extension.
        /// </summary>
        public const string DllExtension = ".dll";

        /// <summary>
        /// The donate page help menu.
        /// </summary>
        public const string DonatePageHelpMenu = "/redirect/donate_hm.html";

        /// <summary>
        /// The donate url setup.
        /// </summary>
        public const string DonateUrlSetup = "";

        // <-- You must specify a destination URL for the donate button in the setup wizard.;        

        /// <summary>
        /// The effects sub dir.
        /// </summary>
        public const string EffectsSubDir = "Scanmodes";

        /// <summary>
        /// The expired page.
        /// </summary>
        public const string ExpiredPage = "redirect/pdnexpired.html";

        /// <summary>
        /// The feedback email.
        /// </summary>
        public const string FeedbackEmail = "kris.janssen@biw.kuleuven.be";

        // <-- You must specify an e-mail address for users to send feedback to.;

        /// <summary>
        /// The file types sub dir.
        /// </summary>
        public const string FileTypesSubDir = "FileTypes";

        /// <summary>
        /// The forum page help page.
        /// </summary>
        public const string ForumPageHelpPage = "/redirect/forum_hm.html";

        /// <summary>
        /// The plugins page help page.
        /// </summary>
        public const string PluginsPageHelpPage = "/redirect/plugins_hm.html";

        /// <summary>
        /// The search engine help menu.
        /// </summary>
        public const string SearchEngineHelpMenu = "/redirect/search_hm.html";

        /// <summary>
        /// The single instance moniker name.
        /// </summary>
        public const string SingleInstanceMonikerName = "SISInstance";

        // <-- You must specify a moniker name (only letters, no symbols, no spaces);

        /// <summary>
        /// The startup unhandled error format fallback.
        /// </summary>
        public const string StartupUnhandledErrorFormatFallback =
            "There was an unhandled error, and SIS must be closed."
            + "Refer to the file '{0}', which has been placed on your desktop, for more information.";

        /// <summary>
        /// The tutorials page help page.
        /// </summary>
        public const string TutorialsPageHelpPage = "/redirect/tutorials_hm.html";

        /// <summary>
        /// The website page help menu.
        /// </summary>
        public const string WebsitePageHelpMenu = "/redirect/main_hm.html";

        /// <summary>
        /// The website url.
        /// </summary>
        public const string WebsiteUrl = "http://www.biosensors.be";

        #endregion

        // <-- You must specify a URL for the application's website.;
    }
}