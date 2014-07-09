// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoUpdater.cs" company="">
//   
// </copyright>
// <summary>
//   The remind later format.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoUpdaterDotNET
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Reflection;
    using System.Threading;
    using System.Xml;

    using Microsoft.Win32;

    /// <summary>
    /// The remind later format.
    /// </summary>
    public enum RemindLaterFormat
    {
        /// <summary>
        /// The minutes.
        /// </summary>
        Minutes, 

        /// <summary>
        /// The hours.
        /// </summary>
        Hours, 

        /// <summary>
        /// The days.
        /// </summary>
        Days
    }

    /// <summary>
    /// Main class that lets you auto update applications by setting some static fields and executing its Start method.
    /// </summary>
    public static class AutoUpdater
    {
        #region Static Fields

        /// <summary>
        /// URL of the xml file that contains information about latest version of the application.
        /// </summary>
        /// 
        public static string AppCastURL;

        /// <summary>
        /// Sets the current culture of the auto update notification window. Set this value if your application supports functionalty to change the languge of the application.
        /// </summary>
        public static CultureInfo CurrentCulture;

        /// <summary>
        /// If this is true users see dialog where they can set remind later interval otherwise it will take the interval from RemindLaterAt and RemindLaterTimeSpan fields.
        /// </summary>
        public static bool LetUserSelectRemindLater = true;

        /// <summary>
        /// Opens the download url in default browser if true. Very usefull if you have portable application.
        /// </summary>
        public static bool OpenDownloadPage;

        /// <summary>
        /// Remind Later interval after user should be reminded of update.
        /// </summary>
        public static int RemindLaterAt = 2;

        /// <summary>
        /// Set if RemindLaterAt interval should be in Minutes, Hours or Days.
        /// </summary>
        public static RemindLaterFormat RemindLaterTimeSpan = RemindLaterFormat.Days;

        /// <summary>
        /// The app title.
        /// </summary>
        internal static string AppTitle;

        /// <summary>
        /// The change log url.
        /// </summary>
        internal static string ChangeLogURL;

        /// <summary>
        /// The current version.
        /// </summary>
        internal static Version CurrentVersion;

        /// <summary>
        /// The dialog title.
        /// </summary>
        internal static string DialogTitle;

        /// <summary>
        /// The download url.
        /// </summary>
        internal static string DownloadURL;

        /// <summary>
        /// The installed version.
        /// </summary>
        internal static Version InstalledVersion;

        /// <summary>
        /// The registry location.
        /// </summary>
        internal static string RegistryLocation;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Start checking for new version of application and display dialog to the user if update is available.
        /// </summary>
        public static void Start()
        {
            Start(AppCastURL);
        }

        /// <summary>
        /// Start checking for new version of application and display dialog to the user if update is available.
        /// </summary>
        /// <param name="appCast">
        /// URL of the xml file that contains information about latest version of the application.
        /// </param>
        public static void Start(string appCast)
        {
            AppCastURL = appCast;

            var backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += BackgroundWorkerDoWork;

            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The background worker do work.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            var mainAssembly = Assembly.GetEntryAssembly();
            var companyAttribute =
                (AssemblyCompanyAttribute)GetAttribute(mainAssembly, typeof(AssemblyCompanyAttribute));
            var titleAttribute = (AssemblyTitleAttribute)GetAttribute(mainAssembly, typeof(AssemblyTitleAttribute));
            AppTitle = titleAttribute != null ? titleAttribute.Title : mainAssembly.GetName().Name;
            var appCompany = companyAttribute != null ? companyAttribute.Company : string.Empty;

            RegistryLocation = !string.IsNullOrEmpty(appCompany)
                                   ? string.Format(@"Software\{0}\{1}\AutoUpdater", appCompany, AppTitle)
                                   : string.Format(@"Software\{0}\AutoUpdater", AppTitle);

            RegistryKey updateKey = Registry.CurrentUser.OpenSubKey(RegistryLocation);

            if (updateKey != null)
            {
                object remindLaterTime = updateKey.GetValue("remindlater");

                if (remindLaterTime != null)
                {
                    DateTime remindLater = Convert.ToDateTime(
                        remindLaterTime.ToString(), 
                        CultureInfo.CreateSpecificCulture("en-US"));

                    int compareResult = DateTime.Compare(DateTime.Now, remindLater);

                    if (compareResult < 0)
                    {
                        var updateForm = new UpdateForm(true);
                        updateForm.SetTimer(remindLater);
                        return;
                    }
                }
            }

            InstalledVersion = mainAssembly.GetName().Version;

            WebRequest webRequest = WebRequest.Create(AppCastURL);
            webRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);

            WebResponse webResponse;

            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception)
            {
                return;
            }

            Stream appCastStream = webResponse.GetResponseStream();

            var receivedAppCastDocument = new XmlDocument();

            if (appCastStream != null)
            {
                receivedAppCastDocument.Load(appCastStream);
            }
            else
            {
                return;
            }

            XmlNodeList appCastItems = receivedAppCastDocument.SelectNodes("item");

            if (appCastItems != null)
            {
                foreach (XmlNode item in appCastItems)
                {
                    XmlNode appCastVersion = item.SelectSingleNode("version");
                    if (appCastVersion != null)
                    {
                        string appVersion = appCastVersion.InnerText;
                        var version = new Version(appVersion);
                        if (version <= InstalledVersion)
                        {
                            continue;
                        }

                        CurrentVersion = version;
                    }
                    else
                    {
                        continue;
                    }

                    XmlNode appCastTitle = item.SelectSingleNode("title");

                    DialogTitle = appCastTitle != null ? appCastTitle.InnerText : string.Empty;

                    XmlNode appCastChangeLog = item.SelectSingleNode("changelog");

                    ChangeLogURL = appCastChangeLog != null ? appCastChangeLog.InnerText : string.Empty;

                    XmlNode appCastUrl = item.SelectSingleNode("url");

                    DownloadURL = appCastUrl != null ? appCastUrl.InnerText : string.Empty;
                }
            }

            if (updateKey != null)
            {
                object skip = updateKey.GetValue("skip");
                object applicationVersion = updateKey.GetValue("version");
                if (skip != null && applicationVersion != null)
                {
                    string skipValue = skip.ToString();
                    var skipVersion = new Version(applicationVersion.ToString());
                    if (skipValue.Equals("1") && CurrentVersion <= skipVersion)
                    {
                        return;
                    }

                    if (CurrentVersion > skipVersion)
                    {
                        RegistryKey updateKeyWrite = Registry.CurrentUser.CreateSubKey(RegistryLocation);
                        if (updateKeyWrite != null)
                        {
                            updateKeyWrite.SetValue("version", CurrentVersion.ToString());
                            updateKeyWrite.SetValue("skip", 0);
                        }
                    }
                }

                updateKey.Close();
            }

            if (CurrentVersion == null)
            {
                return;
            }

            if (CurrentVersion > InstalledVersion)
            {
                var thread = new Thread(ShowUI);
                thread.CurrentCulture =
                    thread.CurrentUICulture = CurrentCulture ?? System.Windows.Forms.Application.CurrentCulture;
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
        }

        /// <summary>
        /// The get attribute.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <param name="attributeType">
        /// The attribute type.
        /// </param>
        /// <returns>
        /// The <see cref="Attribute"/>.
        /// </returns>
        private static Attribute GetAttribute(Assembly assembly, Type attributeType)
        {
            var attributes = assembly.GetCustomAttributes(attributeType, false);
            if (attributes.Length == 0)
            {
                return null;
            }

            return (Attribute)attributes[0];
        }

        /// <summary>
        /// The show ui.
        /// </summary>
        private static void ShowUI()
        {
            var updateForm = new UpdateForm();

            updateForm.ShowDialog();
        }

        #endregion
    }
}