namespace SIS.MDITemplate
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    using SIS.Base;
    using SIS.Library;
    using SIS.Resources;
    using SIS.Systemlayer;

    //public class MdiParentForm : System.Windows.Forms.Form
    public class MdiParentForm : BaseForm
    {
        protected System.Windows.Forms.MainMenu m_menuMain = null;
        protected MdiMenuItem m_menuItemFile = null;
        protected MdiMenuItem m_menuItemFileOpen = null;
        protected MdiMenuItem m_menuItemFileClose = null;
        protected MdiMenuItem m_menuItemFileSave = null;
        protected MdiMenuItem m_menuItemFileExit = null;
        protected MdiMenuItem m_menuItemWindow = null;
        protected MdiMenuItem m_menuItemHelp = null;
        protected MdiMenuItem m_menuItemHelpAbout = null;
        protected MdiMenuItem m_menuItemFileSeparator = null;
        protected MessengerStatusBar m_statusBar = null;
        protected MdiMenuItem m_menuItemFileSaveAs = null;
        protected System.Windows.Forms.MenuItem m_menuItemFileSeparator2 = null;
        protected MdiMenuItem m_menuItemFileNew = null;
        private RecentFilesMenuItem m_menuItemRecentFiles = null;
        private System.Windows.Forms.MenuItem m_menuItemFileSeperator2 = null;

        private DocumentTypes m_documentTypes = null;
        private IContainer components;
        private System.Drawing.Image m_imageApplication = null;
        private FormPositionSerializer m_positionSerializer = null;
        private FileDragDropHandler m_dragDropHandler = null;
        static private MdiParentForm m_formMain = null;
        private SplashForm splashForm = null;

        #region RECENT

        private List<string> queuedInstanceMessages = new List<string>();

        //Recently added;
        private SingleInstanceManager singleInstanceManager = null;

        public SplashForm SplashForm
        {
            get
            {
                return this.splashForm;
            }
            set
            {
                this.splashForm = value;
            }
        }



        public SingleInstanceManager SingleInstanceManager
        {
            get
            {
                return this.singleInstanceManager;
            }

            set
            {
                if (this.singleInstanceManager != null)
                {
                    this.singleInstanceManager.InstanceMessageReceived -= new EventHandler(this.SingleInstanceManager_InstanceMessageReceived);
                    this.singleInstanceManager.SetWindow(null);
                }

                this.singleInstanceManager = value;

                if (this.singleInstanceManager != null)
                {
                    this.singleInstanceManager.SetWindow(this);
                    this.singleInstanceManager.InstanceMessageReceived += new EventHandler(this.SingleInstanceManager_InstanceMessageReceived);
                }
            }
        }

        private void SingleInstanceManager_InstanceMessageReceived(object sender, EventArgs e)
        {
            this.BeginInvoke(new Procedure(this.ProcessQueuedInstanceMessages), null);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            Thread.Sleep(1000);

            if (this.splashForm != null)
            {
                this.splashForm.Close();
                this.splashForm.Dispose();
                this.splashForm = null;
            }
        }


        private void ProcessQueuedInstanceMessages()
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (this.IsHandleCreated &&
                !Info.IsExpired &&
                this.singleInstanceManager != null)
            {
                string[] messages1 = this.singleInstanceManager.GetPendingInstanceMessages();
                string[] messages2 = this.queuedInstanceMessages.ToArray();
                this.queuedInstanceMessages.Clear();

                string[] messages = new string[messages1.Length + messages2.Length];
                for (int i = 0; i < messages1.Length; ++i)
                {
                    messages[i] = messages1[i];
                }

                for (int i = 0; i < messages2.Length; ++i)
                {
                    messages[i + messages1.Length] = messages2[i];
                }

                foreach (string message in messages)
                {
                    bool result = this.ProcessMessage(message);

                    if (!result)
                    {
                        break;
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (this.singleInstanceManager != null)
            {
                this.singleInstanceManager.FilterMessage(ref m);
            }

            base.WndProc(ref m);
        }

        private enum ArgumentAction
        {
            Open,
            OpenUntitled,
            Print,
            NoOp
        }

        private bool SplitMessage(string message, out ArgumentAction action, out string actionParm)
        {
            if (message.Length == 0)
            {
                action = ArgumentAction.NoOp;
                actionParm = null;
                return false;
            }

            const string printPrefix = "print:";

            if (message.IndexOf(printPrefix) == 0)
            {
                action = ArgumentAction.Print;
                actionParm = message.Substring(printPrefix.Length);
                return true;
            }

            const string untitledPrefix = "untitled:";

            if (message.IndexOf(untitledPrefix) == 0)
            {
                action = ArgumentAction.OpenUntitled;
                actionParm = message.Substring(untitledPrefix.Length);
                return true;
            }

            action = ArgumentAction.Open;
            actionParm = message;
            return true;
        }

        private bool ProcessMessage(string message)
        {
            if (this.IsDisposed)
            {
                return false;
            }

            ArgumentAction action;
            string actionParm;
            bool result;

            result = this.SplitMessage(message, out action, out actionParm);

            if (!result)
            {
                return true;
            }

            switch (action)
            {
                case ArgumentAction.NoOp:
                    result = true;
                    break;

                case ArgumentAction.Open:
                    this.Activate();

                    if (this.IsCurrentModalForm && this.Enabled)
                    {
                        //result = this.appWorkspace.OpenFileInNewWorkspace(actionParm);
                        this.OpenDocument(actionParm);
                        result = true;
                    }

                    break;

                case ArgumentAction.OpenUntitled:
                    this.Activate();

                    if (!string.IsNullOrEmpty(actionParm) && this.IsCurrentModalForm && this.Enabled)
                    {
                        //result = this.appWorkspace.OpenFileInNewWorkspace(actionParm, false);
                        this.OpenDocument(actionParm);
                        result = true;

                        if (result)
                        {
                            //this.appWorkspace.ActiveDocumentWorkspace.SetDocumentSaveOptions(null, null, null);
                            //this.appWorkspace.ActiveDocumentWorkspace.Document.Dirty = true;
                        }
                    }

                    break;

                //case ArgumentAction.Print:
                //    Activate();

                //    if (!string.IsNullOrEmpty(actionParm) && IsCurrentModalForm && Enabled)
                //    {
                //        result = this.appWorkspace.OpenFileInNewWorkspace(actionParm);

                //        if (result)
                //        {
                //            DocumentWorkspace dw = this.appWorkspace.ActiveDocumentWorkspace;
                //            PrintAction pa = new PrintAction();
                //            dw.PerformAction(pa);
                //            CloseWorkspaceAction cwa = new CloseWorkspaceAction(dw);
                //            this.appWorkspace.PerformAction(cwa);

                //            if (this.appWorkspace.DocumentWorkspaces.Length == 0)
                //            {
                //                Startup.CloseApplication();
                //            }
                //        }
                //    }
                //    break;

                default:
                    throw new InvalidEnumArgumentException();
            }

            return result;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (!this.IsDisposed &&
                (this.queuedInstanceMessages.Count > 0 || (this.singleInstanceManager != null && this.singleInstanceManager.AreMessagesPending)))
            {
                this.ProcessQueuedInstanceMessages();
            }
        }

        #endregion

        #region Debug

        // In Debug we might want to learn which control is currently in focus...
#if DEBUG

        // A static constructor does not take access modifiers or have parameters.
        // A static constructor is called automatically to initialize the class before the first instance is created or any 
        // static members are referenced.
        // A static constructor cannot be called directly.
        // The user has no control on when the static constructor is executed in the program.
        // A typical use of static constructors is when the class is using a log file and the constructor is used to write 
        // entries to this file.
        // Static constructors are also useful when creating wrapper classes for unmanaged code, when the constructor can 
        // call the LoadLibrary method.
        // If a static constructor throws an exception, the runtime will not invoke it a second time, and the type will 
        // remain uninitialized for the lifetime of the application domain in which your program is running.
        static MdiParentForm()
        {
            //new Thread(FocusPrintThread).Start();
        }

        private static string GetControlName(Control control)
        {
            if (control == null)
            {
                return "null";
            }

            string name = control.Name + "(" + control.GetType().Name + ")";

            if (control.Parent != null)
            {
                name += " <- " + GetControlName(control.Parent);
            }

            return name;
        }

        private static void PrintFocus()
        {
            Control c = Utility.FindFocus();
            Tracing.Ping("Focused: " + GetControlName(c));
        }

        private static void FocusPrintThread()
        {
            Thread.CurrentThread.IsBackground = true;

            while (true)
            {
                try
                {
                    FormCollection forms = Application.OpenForms;
                    Form form;
                    if (forms.Count > 0)
                    {
                        form = forms[0];
                        form.BeginInvoke(new Procedure(PrintFocus));
                    }
                }

                catch
                {
                }

                Thread.Sleep(1000);
            }
        }

#endif

        #endregion

        public MdiParentForm()
            : this(new string[0])
        {
        }

        public MdiParentForm(string[] __sArgs)
        {
            m_formMain = this;

            this.StartPosition = FormStartPosition.WindowsDefaultLocation;

            bool splash = false;
            List<string> _lFileNames = new List<string>();

            // Parse command line arguments
            foreach (string _sArgument in __sArgs)
            {
                if (0 == string.Compare(_sArgument, "/dontForceGC"))
                {
                    //Utility.AllowGCFullCollect = false;
                }
                else if (0 == string.Compare(_sArgument, "/splash", true))
                {
                    splash = true;
                }
                else if (0 == string.Compare(_sArgument, "/test", true))
                {
                    // This lets us use an alternate update manifest on the web server so that
                    // we can test manifests on a small scale before "deploying" them to everybody
                    Info.IsTestMode = true;
                }
                else if (0 == string.Compare(_sArgument, "/profileStartupTimed", true))
                {
                    // profileStartupTimed and profileStartupWorkingSet compete, which
                    // ever is last in the args list wins.
                    Info.StartupTest = StartupTestType.Timed;
                }
                else if (0 == string.Compare(_sArgument, "/profileStartupWorkingSet", true))
                {
                    // profileStartupTimed and profileStartupWorkingSet compete, which
                    // ever is last in the args list wins.
                    Info.StartupTest = StartupTestType.WorkingSet;
                }
                else if (_sArgument.Length > 0 && _sArgument[0] != '/')
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(_sArgument);
                        _lFileNames.Add(fullPath);
                    }

                    catch (Exception)
                    {
                        _lFileNames.Add(_sArgument);
                        //canSetCurrentDir = false;
                    }

                    splash = true;
                }
            }


            this.InitializeComponent();

            if (!this.DesignMode)
            {
                this.m_documentTypes = new DocumentTypes();

                this.Text = Application.ProductName;

                this.m_menuItemHelpAbout.Text = string.Format("About {0}...", Application.ProductName);
                this.m_menuItemRecentFiles.OpenFile += new RecentFilesMenuItem.OpenFileHandler(this.m_menuItemRecentFiles_OpenFile);

                this.m_dragDropHandler = new FileDragDropHandler(this, this.m_documentTypes.Extensions);
                this.m_dragDropHandler.FileDropped += new FileDragDropHandler.FileDropHandler(this.m_dragDropHandler_FileDropped);
            }

            foreach (string _sFileName in _lFileNames)
            {
                this.queuedInstanceMessages.Add(_sFileName);
            }

            // no file specified? create a blank image
            if (_lFileNames.Count == 0)
            {
                //MeasurementUnit units = Document.DefaultDpuUnit;
                //double dpu = Document.GetDefaultDpu(units);
                //Size newSize = this.appWorkspace.GetNewDocumentSize();
                //this.appWorkspace.CreateBlankDocumentInNewWorkspace(newSize, units, dpu, true);
                //this.appWorkspace.ActiveDocumentWorkspace.IncrementJustPaintWhite();
                //this.appWorkspace.ActiveDocumentWorkspace.Document.Dirty = false;
            }

            Application.Idle += new EventHandler(this.Application_Idle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.m_menuMain = new System.Windows.Forms.MainMenu(this.components);
            this.m_menuItemFile = new MdiMenuItem();
            this.m_menuItemFileNew = new MdiMenuItem();
            this.m_menuItemFileOpen = new MdiMenuItem();
            this.m_menuItemFileClose = new MdiMenuItem();
            this.m_menuItemFileSeparator2 = new System.Windows.Forms.MenuItem();
            this.m_menuItemFileSave = new MdiMenuItem();
            this.m_menuItemFileSaveAs = new MdiMenuItem();
            this.m_menuItemFileSeperator2 = new System.Windows.Forms.MenuItem();
            this.m_menuItemRecentFiles = new RecentFilesMenuItem();
            this.m_menuItemFileSeparator = new MdiMenuItem();
            this.m_menuItemFileExit = new MdiMenuItem();
            this.m_menuItemWindow = new MdiMenuItem();
            this.m_menuItemHelp = new MdiMenuItem();
            this.m_menuItemHelpAbout = new MdiMenuItem();
            this.m_statusBar = new MessengerStatusBar();
            this.SuspendLayout();
            // 
            // m_menuMain
            // 
            this.m_menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemFile,
            this.m_menuItemWindow,
            this.m_menuItemHelp});
            // 
            // m_menuItemFile
            // 
            this.m_menuItemFile.Index = 0;
            this.m_menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemFileNew,
            this.m_menuItemFileOpen,
            this.m_menuItemFileClose,
            this.m_menuItemFileSeparator2,
            this.m_menuItemFileSave,
            this.m_menuItemFileSaveAs,
            this.m_menuItemFileSeperator2,
            this.m_menuItemRecentFiles,
            this.m_menuItemFileSeparator,
            this.m_menuItemFileExit});
            this.m_menuItemFile.NeedsDocument = false;
            this.m_menuItemFile.StatusMessage = "Ready";
            this.m_menuItemFile.Text = "&File";
            // 
            // m_menuItemFileNew
            // 
            this.m_menuItemFileNew.Index = 0;
            this.m_menuItemFileNew.NeedsDocument = false;
            this.m_menuItemFileNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.m_menuItemFileNew.StatusMessage = "Create a new document";
            this.m_menuItemFileNew.Text = "&New...";
            this.m_menuItemFileNew.Click += new System.EventHandler(this.m_menuItemFileNew_Click);
            // 
            // m_menuItemFileOpen
            // 
            this.m_menuItemFileOpen.Index = 1;
            this.m_menuItemFileOpen.NeedsDocument = false;
            this.m_menuItemFileOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.m_menuItemFileOpen.StatusMessage = "Open a document file...";
            this.m_menuItemFileOpen.Text = "&Open...";
            this.m_menuItemFileOpen.Click += new System.EventHandler(this.m_menuFileOpen_Click);
            // 
            // m_menuItemFileClose
            // 
            this.m_menuItemFileClose.Index = 2;
            this.m_menuItemFileClose.NeedsDocument = true;
            this.m_menuItemFileClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
            this.m_menuItemFileClose.StatusMessage = "Close the current document";
            this.m_menuItemFileClose.Text = "&Close";
            this.m_menuItemFileClose.Click += new System.EventHandler(this.m_menuItemFileClose_Click);
            // 
            // m_menuItemFileSeparator2
            // 
            this.m_menuItemFileSeparator2.Index = 3;
            this.m_menuItemFileSeparator2.Text = "-";
            // 
            // m_menuItemFileSave
            // 
            this.m_menuItemFileSave.Index = 4;
            this.m_menuItemFileSave.NeedsDocument = true;
            this.m_menuItemFileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.m_menuItemFileSave.StatusMessage = "Save a document file...";
            this.m_menuItemFileSave.Text = "&Save";
            this.m_menuItemFileSave.Click += new System.EventHandler(this.m_menuItemFileSave_Click);
            // 
            // m_menuItemFileSaveAs
            // 
            this.m_menuItemFileSaveAs.Index = 5;
            this.m_menuItemFileSaveAs.NeedsDocument = true;
            this.m_menuItemFileSaveAs.StatusMessage = "Save the document to a different file...";
            this.m_menuItemFileSaveAs.Text = "Save &As...";
            this.m_menuItemFileSaveAs.Click += new System.EventHandler(this.m_menuItemFileSaveAs_Click);
            // 
            // m_menuItemFileSeperator2
            // 
            this.m_menuItemFileSeperator2.Index = 6;
            this.m_menuItemFileSeperator2.Text = "-";
            // 
            // m_menuItemRecentFiles
            // 
            this.m_menuItemRecentFiles.Enabled = false;
            this.m_menuItemRecentFiles.Index = 7;
            this.m_menuItemRecentFiles.NeedsDocument = false;
            this.m_menuItemRecentFiles.StatusMessage = "Open a file which has been previously opened...";
            this.m_menuItemRecentFiles.Text = "&Recent files";
            // 
            // m_menuItemFileSeparator
            // 
            this.m_menuItemFileSeparator.Index = 8;
            this.m_menuItemFileSeparator.NeedsDocument = false;
            this.m_menuItemFileSeparator.StatusMessage = "Ready";
            this.m_menuItemFileSeparator.Text = "-";
            // 
            // m_menuItemFileExit
            // 
            this.m_menuItemFileExit.Index = 9;
            this.m_menuItemFileExit.NeedsDocument = false;
            this.m_menuItemFileExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.m_menuItemFileExit.StatusMessage = "Exit the application";
            this.m_menuItemFileExit.Text = "E&xit";
            this.m_menuItemFileExit.Click += new System.EventHandler(this.m_menuItemFileExit_Click);
            // 
            // m_menuItemWindow
            // 
            this.m_menuItemWindow.Index = 1;
            this.m_menuItemWindow.MdiList = true;
            this.m_menuItemWindow.NeedsDocument = true;
            this.m_menuItemWindow.StatusMessage = "Ready";
            this.m_menuItemWindow.Text = "&Window";
            // 
            // m_menuItemHelp
            // 
            this.m_menuItemHelp.Index = 2;
            this.m_menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.m_menuItemHelpAbout});
            this.m_menuItemHelp.NeedsDocument = false;
            this.m_menuItemHelp.StatusMessage = "Ready";
            this.m_menuItemHelp.Text = "&Help";
            // 
            // m_menuItemHelpAbout
            // 
            this.m_menuItemHelpAbout.Index = 0;
            this.m_menuItemHelpAbout.NeedsDocument = false;
            this.m_menuItemHelpAbout.Shortcut = System.Windows.Forms.Shortcut.AltF12;
            this.m_menuItemHelpAbout.StatusMessage = "Display information about the application";
            this.m_menuItemHelpAbout.Text = "&About...";
            this.m_menuItemHelpAbout.Click += new System.EventHandler(this.m_menuItemHelpAbout_Click);
            // 
            // m_statusBar
            // 
            this.m_statusBar.Location = new System.Drawing.Point(0, 403);
            this.m_statusBar.Name = "m_statusBar";
            this.m_statusBar.Size = new System.Drawing.Size(672, 22);
            this.m_statusBar.TabIndex = 1;
            this.m_statusBar.Text = "Ready";
            // 
            // MdiParentForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(672, 425);
            this.Controls.Add(this.m_statusBar);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Menu = this.m_menuMain;
            this.Name = "MdiParentForm";
            this.Text = "MDIParentForm";
            this.ResumeLayout(false);

        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                this.m_positionSerializer = new FormPositionSerializer(this, "MainForm");
            }
        }

        public MdiViewForm ActiveView
        {
            get
            {
                return this.ActiveMdiChild as MdiViewForm;
            }

            set
            {
                value.Focus();
            }
        }

        public Image ApplicationIcon
        {
            get
            {
                return this.m_imageApplication;
            }

            set
            {
                this.m_imageApplication = value;
            }
        }

        static public MdiParentForm MainForm
        {
            get
            {
                return m_formMain;
            }
        }

        private void m_menuItemFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void m_menuFileOpen_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = this.m_documentTypes.OpenDialogFilter;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.OpenDocument(dialog.FileName);
            }
        }

        private void OpenDocument(string sFile)
        {
            string sFileLower = sFile.ToLower();

            foreach (MdiDocument documentExisting in MdiDocument.Documents)
            {
                if (documentExisting.FilePath.ToLower() == sFileLower)
                {
                    MdiViewForm view = documentExisting.Views[0];
                    view.Activate();
                    RecentFilesList.Get().Add(documentExisting.FilePath);
                    return;
                }
            }

            // Replaced sFile by sFileLower here.
            MdiDocument document = this.m_documentTypes.OpenDocument(sFileLower);

            if (document == null)
            {
                MessageBox.Show("Couldn't open document",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                this.CreateView(document);
            }

            this.UpdateMenuItems();
        }

        private void m_menuItemFileSave_Click(object sender, System.EventArgs e)
        {
            MdiViewForm view = this.ActiveView;

            if (view != null)
            {
                if (view.Document.FilePath.Length == 0)
                {
                    this.m_menuItemFileSaveAs_Click(sender, e);
                }
                else
                {
                    foreach (MdiViewForm viewUpdate in view.Document.Views)
                    {
                        viewUpdate.UpdateDocument();
                    }

                    view.Document.SaveDocument(view.Document.FilePath);
                }
            }
        }

        private void m_menuItemFileSaveAs_Click(object sender, System.EventArgs e)
        {
            MdiViewForm view = this.ActiveView;

            if (view != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = this.m_documentTypes.GetSaveFilter(view);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!view.Document.SaveDocument(dialog.FileName))
                    {
                        MessageBox.Show("Couldn't save document",
                            Application.ProductName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void m_menuItemFileClose_Click(object sender, System.EventArgs e)
        {
            MdiViewForm view = this.ActiveView;

            if (view != null)
            {
                view.Close();
            }
        }

        private void m_menuItemFileNew_Click(object sender, System.EventArgs e)
        {
            if (this.m_documentTypes.Count == 0)
            {
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = this.m_documentTypes.CreateFilter;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            MdiDocument document = this.m_documentTypes.CreateDocument(dialog.FileName);

            if (document == null)
            {
                MessageBox.Show("Couldn't create document",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.CreateView(document))
                {
                    RecentFilesList.Get().Add(dialog.FileName);
                }
            }
        }

        private bool CreateView(MdiDocument document)
        {
            MdiViewForm view = document.CreateView();

            if (view == null)
            {
                MessageBox.Show("Couldn't create view",
                    Application.ProductName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                view.Document = document;
                view.MdiParent = this;
                view.InitialUpdate();
                view.Show();
                view.Closed += new EventHandler(this.view_Closed);
                view.Activated += new EventHandler(this.view_Activated);
                return true;
            }
        }

        private void m_menuItemHelpAbout_Click(object sender, System.EventArgs e)
        {
            //AboutForm form = new AboutForm();
            AboutDialog form = new AboutDialog();
            //form.ApplicationIcon = m_imageApplication;
            form.ShowDialog();
        }

        private void m_menuItemRecentFiles_OpenFile(string sFile)
        {
            this.OpenDocument(sFile);
        }

        private void m_dragDropHandler_FileDropped(string sFileName)
        {
            this.OpenDocument(sFileName);
        }

        private void UpdateMenuItems()
        {
            foreach (MdiMenuItem item in this.m_menuMain.MenuItems)
            {
                if (item != null)
                {
                    item.UpdateMenuItem();
                }
            }
        }

        private void view_Closed(object sender, EventArgs e)
        {
            this.UpdateMenuItems();
        }

        private void view_Activated(object sender, EventArgs e)
        {
            this.UpdateMenuItems();
        }
    }
}
