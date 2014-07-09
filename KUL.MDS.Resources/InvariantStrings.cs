using System;

namespace SIS
{
    /// <summary>
    /// Contains strings that must be the same no matter what locale the UI is running with.
    /// </summary>
    public static class InvariantStrings
    {
        // {0} is "All Rights Reserved"
        // Legal has advised that's the only part of this string that should be localizable.
        public const string CopyrightFormat =
            "Copyright © 2009 Kris Janssen.\r\n" +
            "Portions Copyright © 2009 Rick Brewster and dotPDN LLC. {0}";

        public const string FeedbackEmail =
              "kris.janssen@biw.kuleuven.be"; //<-- You must specify an e-mail address for users to send feedback to.;

        public const string CrashlogEmail =
              "kris.janssen@biw.kuleuven.be"; //<-- You must specify a contact e-mail address to be placed in the crash log.;

        public const string WebsiteUrl =
              "http://www.biosensors.be"; //<-- You must specify a URL for the application's website.;

        public const string WebsitePageHelpMenu = "/redirect/main_hm.html";

        public const string ForumPageHelpPage = "/redirect/forum_hm.html";

        public const string PluginsPageHelpPage = "/redirect/plugins_hm.html";

        public const string TutorialsPageHelpPage = "/redirect/tutorials_hm.html";

        public const string DonatePageHelpMenu = "/redirect/donate_hm.html";

        public const string SearchEngineHelpMenu = "/redirect/search_hm.html";

        public const string DonateUrlSetup =
            ""; //<-- You must specify a destination URL for the donate button in the setup wizard.;        

        public const string ExpiredPage = "redirect/pdnexpired.html";

        public const string EffectsSubDir = "Scanmodes";

        public const string FileTypesSubDir = "FileTypes";

        public const string DllExtension = ".dll";

        // Fallback strings are used in case the resources file is unavailable.
        public const string CrashLogHeaderTextFormatFallback =
            @"This text file was created because SIS crashed." + 
            "Please e-mail this file to {0} so we can diagnose and fix the problem.";

        public const string StartupUnhandledErrorFormatFallback =
            "There was an unhandled error, and SIS must be closed." + 
            "Refer to the file '{0}', which has been placed on your desktop, for more information.";

        public const string SingleInstanceMonikerName =
            "SISInstance"; // <-- You must specify a moniker name (only letters, no symbols, no spaces);
    }
}
