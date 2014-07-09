// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The startup.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    using log4net;

    using SIS.Forms;
    using SIS.Library;
    using SIS.MDITemplate.Settings;
    using SIS.Resources;
    using SIS.Systemlayer;
    using SIS.Systemlayer.Settings;

    /// <summary>
    /// The startup.
    /// </summary>
    internal sealed class Startup
    {
        #region Static Fields

        /// <summary>
        /// The _logger.
        /// </summary>
        private static readonly ILog _logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The m_dt startup time.
        /// </summary>
        private static DateTime m_dtStartupTime;

        /// <summary>
        /// The m_strt instance.
        /// </summary>
        private static Startup m_strtInstance;

        #endregion

        #region Fields

        /// <summary>
        /// The m_mf main form.
        /// </summary>
        private MainForm m_mfMainForm;

        /// <summary>
        /// The m_s args.
        /// </summary>
        private string[] m_sArgs;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="__sArgs">
        /// The __s args.
        /// </param>
        private Startup(string[] __sArgs)
        {
            this.m_sArgs = __sArgs;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The close application.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool CloseApplication()
        {
            bool _bReturnVal = true;

            List<Form> _lAllFormsButMainForm = new List<Form>();

            // Get a list of all Modal Forms except the main form.
            foreach (Form form in Application.OpenForms)
            {
                if (form.Modal && !object.ReferenceEquals(form, m_strtInstance.m_mfMainForm))
                {
                    _lAllFormsButMainForm.Add(form);
                }
            }

            if (_lAllFormsButMainForm.Count > 0)
            {
                // Cannot close application if there are modal dialogs
                return false;
            }

            // If there are no Modal Forms, close the Main Form.
            _bReturnVal = CloseForm(m_strtInstance.m_mfMainForm);

            // Return true.
            return _bReturnVal;
        }

        /// <summary>
        /// The get crash log header.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCrashLogHeader()
        {
            StringBuilder headerSB = new StringBuilder();
            StringWriter headerSW = new StringWriter(headerSB);
            WriteCrashLog(null, headerSW);
            return headerSB.ToString();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [STAThread]
        public static int Main(string[] args)
        {
            _logger.Info("Entering Main ...");

            m_dtStartupTime = DateTime.Now;

#if !DEBUG
            try
            {
#endif

            _logger.Info("Running Startup, stage 1 ...");
            m_strtInstance = new Startup(args);
            m_strtInstance.StartStage1();

#if !DEBUG
            }
            catch (Exception ex)
            {
                try
                {
                    UnhandledException(ex);
                    Process.GetCurrentProcess().Kill();
                }
                catch (Exception)
                {
                    MessageBox.Show(ex.ToString());
                    Process.GetCurrentProcess().Kill();
                }
            }
#endif

            return 0;
        }

        /// <summary>
        /// Starts a new instance of SIS with the given arguments.
        /// </summary>
        /// <param name="__iw32wndParent">
        /// </param>
        /// <param name="__bRequireAdmin">
        /// </param>
        /// <param name="__sArgs">
        /// </param>
        public static void StartNewInstance(IWin32Window __iw32wndParent, bool __bRequireAdmin, string[] __sArgs)
        {
            // Clean up the list of arguments a bit.
            StringBuilder _sbAllArgs = new StringBuilder();

            foreach (string _sArg in __sArgs)
            {
                _sbAllArgs.Append(' ');

                if (_sArg.IndexOf(' ') != -1)
                {
                    _sbAllArgs.Append('"');
                }

                _sbAllArgs.Append(_sArg);

                if (_sArg.IndexOf(' ') != -1)
                {
                    _sbAllArgs.Append('"');
                }
            }

            // This string will hold arguments stuck together.
            string _sAllArgs;

            if (_sbAllArgs.Length > 0)
            {
                _sAllArgs = _sbAllArgs.ToString(1, _sbAllArgs.Length - 1);
            }
            else
            {
                _sAllArgs = null;
            }

            // Run the app.
            Shell.Execute(
                __iw32wndParent, 
                Application.ExecutablePath, 
                _sAllArgs, 
                __bRequireAdmin ? ExecutePrivilege.RequireAdmin : ExecutePrivilege.AsInvokerOrAsManifest, 
                ExecuteWaitType.ReturnImmediately);
        }

        /// <summary>
        /// The start new instance.
        /// </summary>
        /// <param name="__iw32wndParent">
        /// The __iw 32 wnd parent.
        /// </param>
        /// <param name="__sFileName">
        /// The __s file name.
        /// </param>
        public static void StartNewInstance(IWin32Window __iw32wndParent, string __sFileName)
        {
            string _sArg;

            if (__sFileName != null && __sFileName.Length != 0)
            {
                _sArg = "\"" + __sFileName + "\"";
            }
            else
            {
                _sArg = string.Empty;
            }

            StartNewInstance(__iw32wndParent, false, new[] { _sArg });
        }

        /// <summary>
        /// The start stage 1.
        /// </summary>
        public void StartStage1()
        {
            // Set up unhandled exception handlers.
#if DEBUG

            // In debug builds we'd prefer to have it dump us into the debugger.
#else
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
#endif

            // Initialize some misc. Windows Forms settings.
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();

            // If any files are missing, try to repair.
            // However, support /skipRepairAttempt for when developing in the IDE 
            // so that we don't needlessly try to repair in that case.
            if (this.m_sArgs.Length > 0
                && string.Compare(this.m_sArgs[0], "/skipRepairAttempt", StringComparison.InvariantCultureIgnoreCase)
                == 0)
            {
                // do nothing: we need this so that we can run from IDE/debugger
                // without it trying to repair itself all the time
            }
            else
            {
                if (this.CheckForImportantFiles())
                {
                    Startup.StartNewInstance(null, false, this.m_sArgs);
                    return;
                }
            }

            // The rest of the code is put in a separate method so that certain DLL's
            // won't get delay loaded until after we try to do repairs.
            _logger.Info("Running Startup, stage 2 ...");
            this.StartStage2();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The application_ thread exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            UnhandledException(e.Exception);
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// The close form.
        /// </summary>
        /// <param name="__frmForm">
        /// The __frm form.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CloseForm(Form __frmForm)
        {
            ArrayList openForms = new ArrayList(Application.OpenForms);

            if (openForms.IndexOf(__frmForm) == -1)
            {
                return false;
            }

            __frmForm.Close();

            ArrayList openForms2 = new ArrayList(Application.OpenForms);

            if (openForms2.IndexOf(__frmForm) == -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The current domain_ unhandled exception.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            UnhandledException((Exception)e.ExceptionObject);
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// The unhandled exception.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        private static void UnhandledException(Exception ex)
        {
            string dir = Shell.GetVirtualPath(VirtualFolderName.UserDesktop, true);
            const string fileName = "SIScrash.log";
            string fullName = Path.Combine(dir, fileName);

            using (StreamWriter stream = new System.IO.StreamWriter(fullName, true))
            {
                stream.AutoFlush = true;
                WriteCrashLog(ex, stream);
            }

            string errorFormat;
            string errorText;

            try
            {
                errorFormat = Resources.Resources.GetString("Startup.UnhandledError.Format");
            }
            catch (Exception)
            {
                errorFormat = InvariantStrings.StartupUnhandledErrorFormatFallback;
            }

            errorText = string.Format(errorFormat, fileName);
            Utility.ErrorBox(null, errorText);
        }

        /// <summary>
        /// The write crash log.
        /// </summary>
        /// <param name="ex">
        /// The ex.
        /// </param>
        /// <param name="stream">
        /// The stream.
        /// </param>
        private static void WriteCrashLog(Exception ex, TextWriter stream)
        {
            string headerFormat;

            try
            {
                headerFormat = Resources.Resources.GetString("CrashLog.HeaderText.Format");
            }
            catch (Exception ex13)
            {
                headerFormat = InvariantStrings.CrashLogHeaderTextFormatFallback
                               + ", --- Exception while calling Resources.GetString(\"CrashLog.HeaderText.Format\"): "
                               + ex13.ToString() + Environment.NewLine;
            }

            string header;

            try
            {
                header = string.Format(headerFormat, InvariantStrings.CrashlogEmail);
            }
            catch
            {
                header = string.Empty;
            }

            stream.WriteLine(header);

            const string noInfoString = "err";

            string fullAppName = noInfoString;
            string timeOfCrash = noInfoString;
            string appUptime = noInfoString;
            string osVersion = noInfoString;
            string osRevision = noInfoString;
            string osType = noInfoString;
            string processorNativeArchitecture = noInfoString;
            string clrVersion = noInfoString;
            string fxInventory = noInfoString;
            string processorArchitecture = noInfoString;
            string cpuName = noInfoString;
            string cpuCount = noInfoString;
            string cpuSpeed = noInfoString;
            string cpuFeatures = noInfoString;
            string totalPhysicalBytes = noInfoString;
            string dpiInfo = noInfoString;
            string localeName = noInfoString;
            string inkInfo = noInfoString;
            string updaterInfo = noInfoString;
            string featuresInfo = noInfoString;
            string assembliesInfo = noInfoString;

            try
            {
                try
                {
                    fullAppName = Info.GetFullAppName();
                }
                catch (Exception ex1)
                {
                    fullAppName = Application.ProductVersion
                                  + ", --- Exception while calling SISInfo.GetFullAppName(): " + ex1.ToString()
                                  + Environment.NewLine;
                }

                try
                {
                    timeOfCrash = DateTime.Now.ToString();
                }
                catch (Exception ex2)
                {
                    timeOfCrash = "--- Exception while populating timeOfCrash: " + ex2.ToString() + Environment.NewLine;
                }

                try
                {
                    appUptime = (DateTime.Now - m_dtStartupTime).ToString();
                }
                catch (Exception ex13)
                {
                    appUptime = "--- Exception while populating appUptime: " + ex13.ToString() + Environment.NewLine;
                }

                try
                {
                    osVersion = System.Environment.OSVersion.Version.ToString();
                }
                catch (Exception ex3)
                {
                    osVersion = "--- Exception while populating osVersion: " + ex3.ToString() + Environment.NewLine;
                }

                try
                {
                    osRevision = OS.Revision;
                }
                catch (Exception ex4)
                {
                    osRevision = "--- Exception while populating osRevision: " + ex4.ToString() + Environment.NewLine;
                }

                try
                {
                    osType = OS.Type.ToString();
                }
                catch (Exception ex5)
                {
                    osType = "--- Exception while populating osType: " + ex5.ToString() + Environment.NewLine;
                }

                try
                {
                    processorNativeArchitecture = Processor.NativeArchitecture.ToString().ToLower();
                }
                catch (Exception ex6)
                {
                    processorNativeArchitecture = "--- Exception while populating processorNativeArchitecture: "
                                                  + ex6.ToString() + Environment.NewLine;
                }

                try
                {
                    clrVersion = System.Environment.Version.ToString();
                }
                catch (Exception ex7)
                {
                    clrVersion = "--- Exception while populating clrVersion: " + ex7.ToString() + Environment.NewLine;
                }

                try
                {
                    fxInventory = (OS.IsDotNetVersionInstalled(2, 0, 0, false) ? "2.0 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(2, 0, 1, false) ? "2.0_SP1 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(2, 0, 2, false) ? "2.0_SP2 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 0, 0, false) ? "3.0 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 0, 1, false) ? "3.0_SP1 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 0, 2, false) ? "3.0_SP2 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 5, 0, false) ? "3.5 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 5, 1, false) ? "3.5_SP1 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 5, 1, true) ? "3.5_SP1_Client " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(3, 5, 2, false) ? "3.5_SP2 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(4, 0, 0, false) ? "4.0 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(4, 0, 1, false) ? "4.0_SP1 " : string.Empty)
                                  + (OS.IsDotNetVersionInstalled(4, 0, 2, false) ? "4.0_SP2 " : string.Empty).Trim();
                }
                catch (Exception ex30)
                {
                    fxInventory = "--- Exception while populating fxInventory: " + ex30.ToString() + Environment.NewLine;
                }

                try
                {
                    processorArchitecture = Processor.Architecture.ToString().ToLower();
                }
                catch (Exception ex8)
                {
                    processorArchitecture = "--- Exception while populating processorArchitecture: " + ex8.ToString()
                                            + Environment.NewLine;
                }

                try
                {
                    cpuName = Processor.CpuName;
                }
                catch (Exception ex9)
                {
                    cpuName = "--- Exception while populating cpuName: " + ex9.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuCount = Processor.LogicalCpuCount.ToString() + "x";
                }
                catch (Exception ex10)
                {
                    cpuCount = "--- Exception while populating cpuCount: " + ex10.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuSpeed = "@ ~" + Processor.ApproximateSpeedMhz.ToString() + "MHz";
                }
                catch (Exception ex16)
                {
                    cpuSpeed = "--- Exception while populating cpuSpeed: " + ex16.ToString() + Environment.NewLine;
                }

                try
                {
                    cpuFeatures = string.Empty;
                    string[] featureNames = Enum.GetNames(typeof(ProcessorFeature));
                    bool firstFeature = true;

                    for (int i = 0; i < featureNames.Length; ++i)
                    {
                        string featureName = featureNames[i];
                        ProcessorFeature feature = (ProcessorFeature)Enum.Parse(typeof(ProcessorFeature), featureName);

                        if (Processor.IsFeaturePresent(feature))
                        {
                            if (firstFeature)
                            {
                                cpuFeatures = "(";
                                firstFeature = false;
                            }
                            else
                            {
                                cpuFeatures += ", ";
                            }

                            cpuFeatures += featureName;
                        }
                    }

                    if (cpuFeatures.Length > 0)
                    {
                        cpuFeatures += ")";
                    }
                }
                catch (Exception ex17)
                {
                    cpuFeatures = "--- Exception while populating cpuFeatures: " + ex17.ToString() + Environment.NewLine;
                }

                try
                {
                    totalPhysicalBytes = ((Memory.TotalPhysicalBytes / 1024) / 1024) + " MB";
                }
                catch (Exception ex11)
                {
                    totalPhysicalBytes = "--- Exception while populating totalPhysicalBytes: " + ex11.ToString()
                                         + Environment.NewLine;
                }

                try
                {
                    float xScale;

                    try
                    {
                        xScale = UI.GetXScaleFactor();
                    }
                    catch (Exception)
                    {
                        using (Control c = new Control())
                        {
                            UI.InitScaling(c);
                            xScale = UI.GetXScaleFactor();
                        }
                    }

                    dpiInfo = string.Format(
                        "{0} dpi ({1}x scale)", 
                        (96.0f * xScale).ToString("F2"), 
                        xScale.ToString("F2"));
                }
                catch (Exception ex19)
                {
                    dpiInfo = "--- Exception while populating dpiInfo: " + ex19.ToString() + Environment.NewLine;
                }

                try
                {
                    localeName = "pdnr.c: " + Resources.Resources.Culture.Name + ", hklm: "
                                 + Settings.SystemWide.GetString(SettingNames.LanguageName, "n/a") + ", hkcu: "
                                 + Settings.CurrentUser.GetString(SettingNames.LanguageName, "n/a") + ", cc: "
                                 + CultureInfo.CurrentCulture.Name + ", cuic: " + CultureInfo.CurrentUICulture.Name;
                }
                catch (Exception ex14)
                {
                    localeName = "--- Exception while populating localeName: " + ex14.ToString() + Environment.NewLine;
                }

                try
                {
                    inkInfo = Ink.IsAvailable() ? "yes" : "no";
                }
                catch (Exception ex15)
                {
                    inkInfo = "--- Exception while populating inkInfo: " + ex15.ToString() + Environment.NewLine;
                }

                try
                {
                    string autoCheckForUpdates = Settings.SystemWide.GetString(
                        SettingNames.AutoCheckForUpdates, 
                        noInfoString);

                    string lastUpdateCheckTimeInfo;

                    try
                    {
                        string lastUpdateCheckTimeString =
                            Settings.CurrentUser.Get(SettingNames.LastUpdateCheckTimeTicks);
                        long lastUpdateCheckTimeTicks = long.Parse(lastUpdateCheckTimeString);
                        DateTime lastUpdateCheckTime = new DateTime(lastUpdateCheckTimeTicks);
                        lastUpdateCheckTimeInfo = lastUpdateCheckTime.ToShortDateString();
                    }
                    catch (Exception)
                    {
                        lastUpdateCheckTimeInfo = noInfoString;
                    }

                    updaterInfo = string.Format(
                        "{0}, {1}", 
                        (autoCheckForUpdates == "1")
                            ? "true"
                            : (autoCheckForUpdates == "0" ? "false" : (autoCheckForUpdates ?? "null")), 
                        lastUpdateCheckTimeInfo);
                }
                catch (Exception ex17)
                {
                    updaterInfo = "--- Exception while populating updaterInfo: " + ex17.ToString() + Environment.NewLine;
                }

                try
                {
                    StringBuilder featureSB = new StringBuilder();

                    IEnumerable<string> featureList = Tracing.GetLoggedFeatures();

                    bool first = true;
                    foreach (string feature in featureList)
                    {
                        if (!first)
                        {
                            featureSB.Append(", ");
                        }

                        featureSB.Append(feature);

                        first = false;
                    }

                    featuresInfo = featureSB.ToString();
                }
                catch (Exception ex18)
                {
                    featuresInfo = "--- Exception while populating featuresInfo: " + ex18.ToString()
                                   + Environment.NewLine;
                }

                try
                {
                    StringBuilder assembliesInfoSB = new StringBuilder();

                    Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                    foreach (Assembly assembly in loadedAssemblies)
                    {
                        assembliesInfoSB.AppendFormat(
                            "{0}    {1} @ {2}", 
                            Environment.NewLine, 
                            assembly.FullName, 
                            assembly.Location);
                    }

                    assembliesInfo = assembliesInfoSB.ToString();
                }
                catch (Exception ex16)
                {
                    assembliesInfo = "--- Exception while populating assembliesInfo: " + ex16.ToString()
                                     + Environment.NewLine;
                }
            }
            catch (Exception ex12)
            {
                stream.WriteLine("Exception while gathering app and system info: " + ex12.ToString());
            }

            stream.WriteLine("Application version: " + fullAppName);
            stream.WriteLine("Time of crash: " + timeOfCrash);
            stream.WriteLine("Application uptime: " + appUptime);

            stream.WriteLine(
                "OS Version: " + osVersion + (string.IsNullOrEmpty(osRevision) ? string.Empty : (" " + osRevision))
                + " " + osType + " " + processorNativeArchitecture);
            stream.WriteLine(".NET version: CLR " + clrVersion + " " + processorArchitecture + ", FX " + fxInventory);
            stream.WriteLine("Processor: " + cpuCount + " \"" + cpuName + "\" " + cpuSpeed + " " + cpuFeatures);
            stream.WriteLine("Physical memory: " + totalPhysicalBytes);
            stream.WriteLine("UI DPI: " + dpiInfo);
            stream.WriteLine("Tablet PC: " + inkInfo);
            stream.WriteLine("Updates: " + updaterInfo);
            stream.WriteLine("Locale: " + localeName);
            stream.WriteLine("Features log: " + featuresInfo);
            stream.WriteLine("Loaded assemblies: " + assembliesInfo);
            stream.WriteLine();

            stream.WriteLine("Exception details:");

            if (ex == null)
            {
                stream.WriteLine("(null)");
            }
            else
            {
                stream.WriteLine(ex.ToString());

                // Determine if there is any 'secondary' exception to report
                Exception[] otherEx = null;

                if (ex is System.Reflection.ReflectionTypeLoadException)
                {
                    otherEx = ((System.Reflection.ReflectionTypeLoadException)ex).LoaderExceptions;
                }

                if (otherEx != null)
                {
                    for (int i = 0; i < otherEx.Length; ++i)
                    {
                        stream.WriteLine();
                        stream.WriteLine("Secondary exception details:");

                        if (otherEx[i] == null)
                        {
                            stream.WriteLine("(null)");
                        }
                        else
                        {
                            stream.WriteLine(otherEx[i].ToString());
                        }
                    }
                }
            }

            stream.WriteLine("------------------------------------------------------------------------------");
            stream.Flush();
        }

        /// <summary>
        /// Checks to make sure certain files are present, and tries to repair the problem.
        /// </summary>
        /// <returns>
        /// true if any repairs had to be made, at which point SIS must be restarted.
        /// false otherwise, if everything's okay.
        /// </returns>
        private bool CheckForImportantFiles()
        {
            string[] _sRequiredFiles = new string[]
                                           {
                                               // "SIS.Base.dll",
                                               // "SIS.Data.dll",
                                               // "SIS.Hardware.dll",
                                               // "SIS.Library.dll",
                                               // "SIS.MDITemplate.dll",
                                               // "SIS.Base.dll",
                                               // "SIS.AppResources.dll",
                                               // "SIS.ScanModes.dll",
                                               // "SIS.SerialTerminal.dll",
                                               // "SIS.SystemLayer.dll",
                                               // "SIS.Validation.dll",
                                               // "SIS.WPFControls.dll",
                                               // "ZedGraph.dll",
                                               // "AForge.dll",
                                               // "AForge.Imaging.dll",
                                               // "AForge.Math.dll",
                                           };

            string _sDirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<string> _lstMissingFiles = null;

            foreach (string _sRequiredFile in _sRequiredFiles)
            {
                bool _bMissing;

                try
                {
                    string _sPathName = Path.Combine(_sDirName, _sRequiredFile);
                    FileInfo _fnfoFileInfo = new FileInfo(_sPathName);
                    _bMissing = !_fnfoFileInfo.Exists;
                }
                catch (Exception)
                {
                    _bMissing = true;
                }

                if (_bMissing)
                {
                    if (_lstMissingFiles == null)
                    {
                        _lstMissingFiles = new List<string>();
                    }

                    _lstMissingFiles.Add(_sRequiredFile);
                }
            }

            if (_lstMissingFiles == null)
            {
                return false;
            }
            else
            {
                if (Shell.ReplaceMissingFiles(_lstMissingFiles.ToArray()))
                {
                    // Everything is repaired and happy.
                    return true;
                }
                else
                {
                    // Things didn't get fixed. Bail.
                    Process.GetCurrentProcess().Kill();
                    return false;
                }
            }
        }

        /// <summary>
        /// The start stage 2.
        /// </summary>
        private void StartStage2()
        {
            // Set up locale / resource details
            string locale = Settings.CurrentUser.GetString(SettingNames.LanguageName, null);

            if (locale == null)
            {
                locale = Settings.SystemWide.GetString(SettingNames.LanguageName, null);
            }

            if (locale != null)
            {
                try
                {
                    CultureInfo ci = new CultureInfo(locale, true);
                    Thread.CurrentThread.CurrentUICulture = ci;
                }
                catch (Exception)
                {
                    // Don't want bad culture name to crash us
                }
            }

            // Check system requirements
            if (!OS.CheckOSRequirement())
            {
                string message = Resources.Resources.GetString("Error.OSRequirement");
                Utility.ErrorBox(null, message);
                return;
            }

            // Parse command-line arguments
            // if (this.m_sArgs.Length == 1 &&
            // this.m_sArgs[0] == Updates.UpdatesOptionsDialog.CommandLineParameter)
            // {
            // Updates.UpdatesOptionsDialog.ShowUpdateOptionsDialog(null, false);
            // }
            // else
            // {
            SingleInstanceManager _simgrsingleInstanceManager =
                new SingleInstanceManager(InvariantStrings.SingleInstanceMonikerName);

            // If this is not the first instance of SIS.exe, then forward the command-line
            // parameters over to the first instance.
            if (!_simgrsingleInstanceManager.IsFirstInstance)
            {
                _simgrsingleInstanceManager.FocusFirstInstance();

                foreach (string arg in this.m_sArgs)
                {
                    _simgrsingleInstanceManager.SendInstanceMessage(arg, 30);
                }

                _simgrsingleInstanceManager.Dispose();
                _simgrsingleInstanceManager = null;

                return;
            }

            // Create main window
            this.m_mfMainForm = new MainForm(this.m_sArgs);

            // this.m_mfMainForm = new MainForm();
            this.m_mfMainForm.SingleInstanceManager = _simgrsingleInstanceManager;
            _simgrsingleInstanceManager = null; // mainForm owns it now

            // 3 2 1 go
            Application.Run(this.m_mfMainForm);

            try
            {
                this.m_mfMainForm.Dispose();
            }
            catch (Exception)
            {
            }

            this.m_mfMainForm = null;

            // }
        }

        #endregion
    }
}