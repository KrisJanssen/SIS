// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Info.cs" company="">
//   
// </copyright>
// <summary>
//   A few utility functions specific to KUL.MDS.SIS.exe
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Resources
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// A few utility functions specific to KUL.MDS.SIS.exe
    /// </summary>
    public static class Info
    {
        #region Constants

        /// <summary>
        /// The beta expire time days.
        /// </summary>
        public const int BetaExpireTimeDays = 30;

        #endregion

        #region Static Fields

        /// <summary>
        /// The is final build.
        /// </summary>
        private static readonly bool isFinalBuild = GetIsFinalBuild();

        /// <summary>
        /// The app icon.
        /// </summary>
        private static Icon appIcon;

        /// <summary>
        /// The copyright string.
        /// </summary>
        private static string copyrightString = null;

        /// <summary>
        /// The is test mode.
        /// </summary>
        private static bool isTestMode = false;

        /// <summary>
        /// The startup test.
        /// </summary>
        private static StartupTestType startupTest = StartupTestType.None;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the app icon.
        /// </summary>
        public static Icon AppIcon
        {
            get
            {
                if (appIcon == null)
                {
                    Stream stream = Resources.GetResourceStream("Icons.App.ico");
                    appIcon = new Icon(stream);
                    stream.Close();
                }

                return appIcon;
            }
        }

        /// <summary>
        /// Gets the build time.
        /// </summary>
        public static DateTime BuildTime
        {
            get
            {
                Version version = GetVersion();

                DateTime time = new DateTime(2000, 1, 1, 0, 0, 0);
                time = time.AddDays(version.Build);
                time = time.AddSeconds(version.Revision * 2);

                return time;
            }
        }

        /// <summary>
        /// Gets the expiration date.
        /// </summary>
        public static DateTime ExpirationDate
        {
            get
            {
                if (Info.IsFinalBuild && !IsDebugBuild)
                {
                    return DateTime.MaxValue;
                }
                else
                {
                    return Info.BuildTime + new TimeSpan(BetaExpireTimeDays, 0, 0, 0);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether is debug build.
        /// </summary>
        public static bool IsDebugBuild
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Gets a value indicating whether is expired.
        /// </summary>
        public static bool IsExpired
        {
            get
            {
                // if (!SISInfo.IsFinalBuild || SISInfo.IsDebugBuild)
                // {
                // if (DateTime.Now > SISInfo.ExpirationDate)
                // {
                // return true;
                // }
                // }
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is final build.
        /// </summary>
        public static bool IsFinalBuild
        {
            get
            {
                return isFinalBuild;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether is test mode.
        /// </summary>
        public static bool IsTestMode
        {
            get
            {
                return isTestMode;
            }

            set
            {
                isTestMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the startup test.
        /// </summary>
        public static StartupTestType StartupTest
        {
            get
            {
                return startupTest;
            }

            set
            {
                startupTest = value;
            }
        }

        /// <summary>
        /// Gets the full path to where user customization files should be stored.
        /// </summary>
        /// <returns>
        /// User data files should include settings or customizations that don't go into data files such as *.PDN.
        /// An example of a user data file is a color palette.
        /// </returns>
        public static string UserDataPath
        {
            get
            {
                string myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string userDataDirName = Resources.GetString("SystemLayer.UserDataDirName");
                string userDataPath = Path.Combine(myDocsPath, userDataDirName);
                return userDataPath;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// For final builds, this returns SISInfo.GetProductName() (i.e., "SIS v2.2")
        /// For non-final builds, this returns GetFullAppName()
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAppName()
        {
            if (Info.IsFinalBuild && !Info.IsDebugBuild)
            {
                return Info.GetProductName(false);
            }
            else
            {
                return GetFullAppName();
            }
        }

        /// <summary>
        /// The get application dir.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetApplicationDir()
        {
            string appPath = Application.StartupPath;
            return appPath;
        }

        /// <summary>
        /// Returns the bare product name, e.g. "SIS"
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetBareProductName()
        {
            return Resources.GetString("Application.ProductName.Bare");
        }

        /// <summary>
        /// The get copyright string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCopyrightString()
        {
            if (copyrightString == null)
            {
                string format = InvariantStrings.CopyrightFormat;
                string allRightsReserved = Resources.GetString("Application.Copyright.AllRightsReserved");
                copyrightString = string.Format(CultureInfo.CurrentCulture, format, allRightsReserved);
            }

            return copyrightString;
        }

        /// <summary>
        /// Returns a version string that is presentable without the SIS name. example: "version 2.5 Beta 5"
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFriendlyVersionString()
        {
            Version version = Info.GetVersion();
            string versionFormat = Resources.GetString("SISInfo.FriendlyVersionString.Format");
            string configFormat = Resources.GetString("SISInfo.FriendlyVersionString.ConfigWithSpace.Format");
            string config = string.Format(configFormat, GetConfigurationString());
            string configText;

            if (Info.IsFinalBuild)
            {
                configText = string.Empty;
            }
            else
            {
                configText = config;
            }

            string versionText = string.Format(versionFormat, GetVersionNumberString(version, 2), configText);
            return versionText;
        }

        /// <summary>
        /// Returns the application name, with the version string. i.e., "SIS v2.5 (Beta 2 Debug build 1.0.*.*)"
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFullAppName()
        {
            string fullAppNameFormat = Resources.GetString("SISInfo.FullAppName.Format");
            string fullAppName = string.Format(fullAppNameFormat, Info.GetProductName(false), GetVersionString());
            return fullAppName;
        }

        /// <summary>
        /// The get ngen path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNgenPath()
        {
            return GetNgenPath(false);
        }

        /// <summary>
        /// The get ngen path.
        /// </summary>
        /// <param name="force32bit">
        /// The force 32 bit.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNgenPath(bool force32bit)
        {
            string fxDir;

            if (UIntPtr.Size == 8 && !force32bit)
            {
                fxDir = "Framework64";
            }
            else
            {
                fxDir = "Framework";
            }

            string fxPathBase = @"%WINDIR%\Microsoft.NET\" + fxDir + @"\v";
            string fxPath = fxPathBase + Environment.Version.ToString(3) + @"\";
            string fxPathExp = System.Environment.ExpandEnvironmentVariables(fxPath);
            string ngenExe = Path.Combine(fxPathExp, "ngen.exe");

            return ngenExe;
        }

        /// <summary>
        /// For final builds, returns a string such as "SIS v2.6"
        /// For non-final builds, returns a string such as "SIS v2.6 Beta 2"
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetProductName()
        {
            return GetProductName(!IsFinalBuild);
        }

        /// <summary>
        /// The get product name.
        /// </summary>
        /// <param name="withTag">
        /// The with tag.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetProductName(bool withTag)
        {
            string bareProductName = GetBareProductName();
            string productNameFormat = Resources.GetString("Application.ProductName.Format");
            string tag;

            if (withTag)
            {
                string tagFormat = Resources.GetString("Application.ProductName.Tag.Format");
                tag = string.Format(tagFormat, GetAppConfig());
            }
            else
            {
                tag = string.Empty;
            }

            string version = GetVersionNumberString(GetVersion(), 2);

            string productName = string.Format(productNameFormat, bareProductName, version, tag);
            return productName;
        }

        /// <summary>
        /// The get version.
        /// </summary>
        /// <returns>
        /// The <see cref="Version"/>.
        /// </returns>
        public static Version GetVersion()
        {
            return new Version(Application.ProductVersion);
        }

        /// <summary>
        /// Returns a string for just the version number, i.e. "3.01"
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="fieldCount">
        /// The field Count.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetVersionNumberString(Version version, int fieldCount)
        {
            if (fieldCount < 1 || fieldCount > 4)
            {
                throw new ArgumentOutOfRangeException("fieldCount", "must be in the range [1, 4]");
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(version.Major.ToString());

            if (fieldCount >= 2)
            {
                sb.AppendFormat(".{0}", version.Minor.ToString("D2"));
            }

            if (fieldCount >= 3)
            {
                sb.AppendFormat(".{0}", version.Build.ToString());
            }

            if (fieldCount == 4)
            {
                sb.AppendFormat(".{0}", version.Revision.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a full version string of the form: ApplicationConfiguration + BuildType + BuildVersion
        /// i.e.: "Beta 2 Debug build 1.0.*.*"
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetVersionString()
        {
            string buildType =
#if DEBUG
                "Debug";
#else
                "Release";
#endif

            string versionFormat = Resources.GetString("SISInfo.VersionString.Format");

            string versionText = string.Format(
                versionFormat, 
                GetConfigurationString(), 
                buildType, 
                GetVersionNumberString(GetVersion(), 4));
            return versionText;
        }

        /// <summary>
        /// Checks if the build is expired, and displays a dialog box that takes the user to
        /// the SIS website if necessary.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <returns>
        /// true if the user should be allowed to continue, false if the build has expired
        /// </returns>
        public static bool HandleExpiration(IWin32Window owner)
        {
            if (IsExpired)
            {
                string expiredMessage = Resources.GetString("ExpiredDialog.Message");

                DialogResult result = MessageBox.Show(
                    expiredMessage, 
                    Info.GetProductName(true), 
                    MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    // string expiredRedirect = InvariantStrings.ExpiredPage;
                    // SISInfo.LaunchWebSite(owner, expiredRedirect);
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// The launch web site.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        public static void LaunchWebSite(IWin32Window owner)
        {
            LaunchWebSite(owner, null);
        }

        /// <summary>
        /// The launch web site.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        public static void LaunchWebSite(IWin32Window owner, string page)
        {
            // string webSite = SystemLayer.Branding.WebsiteUrl;

            // Uri baseUri = new Uri(webSite);
            // Uri uri;

            // if (page == null)
            // {
            // uri = baseUri;
            // }
            // else
            // {
            // uri = new Uri(baseUri, page);
            // }

            // string url = uri.ToString();

            // if (url.IndexOf("@") == -1)
            // {
            // OpenUrl(owner, url);
            // }
        }

        /// <summary>
        /// The open url.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool OpenUrl(IWin32Window owner, string url)
        {
            // bool result = SystemLayer.Shell.LaunchUrl(owner, url);

            // if (!result)
            // {
            // string message = SISResources.GetString("LaunchLink.Error");
            // MessageBox.Show(owner, message, SISInfo.GetBareProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }

            // return result;
            return true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get app config.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetAppConfig()
        {
            object[] attributes = typeof(Info).Assembly.GetCustomAttributes(
                typeof(AssemblyConfigurationAttribute), 
                false);
            AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)attributes[0];
            return aca.Configuration;
        }

        /// <summary>
        /// The get configuration string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetConfigurationString()
        {
            object[] attributes =
                Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
            AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)attributes[0];
            return aca.Configuration;
        }

        /// <summary>
        /// The get is final build.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool GetIsFinalBuild()
        {
            return !(GetAppConfig().IndexOf("Final") == -1);
        }

        #endregion
    }
}