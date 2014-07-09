// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateForm.cs" company="">
//   
// </copyright>
// <summary>
//   The update form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoUpdaterDotNET
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Timers;
    using System.Windows.Forms;

    using Microsoft.Win32;

    using Timer = System.Timers.Timer;

    /// <summary>
    /// The update form.
    /// </summary>
    internal partial class UpdateForm : Form
    {
        #region Fields

        /// <summary>
        /// The _timer.
        /// </summary>
        private Timer _timer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateForm"/> class.
        /// </summary>
        /// <param name="remindLater">
        /// The remind later.
        /// </param>
        public UpdateForm(bool remindLater = false)
        {
            if (!remindLater)
            {
                this.InitializeComponent();
                var resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
                this.Text = AutoUpdater.DialogTitle;
                this.labelUpdate.Text =
                    string.Format(
                        resources.GetString("labelUpdate.Text", CultureInfo.CurrentCulture), 
                        AutoUpdater.AppTitle);
                this.labelDescription.Text =
                    string.Format(
                        resources.GetString("labelDescription.Text", CultureInfo.CurrentCulture), 
                        AutoUpdater.AppTitle, 
                        AutoUpdater.CurrentVersion, 
                        AutoUpdater.InstalledVersion);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public override sealed string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The set timer.
        /// </summary>
        /// <param name="remindLater">
        /// The remind later.
        /// </param>
        public void SetTimer(DateTime remindLater)
        {
            TimeSpan timeSpan = remindLater - DateTime.Now;
            this._timer = new System.Timers.Timer { Interval = (int)timeSpan.TotalMilliseconds };
            this._timer.Elapsed += this.TimerElapsed;
            this._timer.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The button remind later click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ButtonRemindLaterClick(object sender, EventArgs e)
        {
            if (AutoUpdater.LetUserSelectRemindLater)
            {
                var remindLaterForm = new RemindLaterForm();

                var dialogResult = remindLaterForm.ShowDialog();

                if (dialogResult.Equals(DialogResult.OK))
                {
                    AutoUpdater.RemindLaterTimeSpan = remindLaterForm.RemindLaterFormat;
                    AutoUpdater.RemindLaterAt = remindLaterForm.RemindLaterAt;
                }
                else if (dialogResult.Equals(DialogResult.Abort))
                {
                    var downloadDialog = new DownloadUpdateDialog(AutoUpdater.DownloadURL);

                    try
                    {
                        downloadDialog.ShowDialog();
                    }
                    catch (System.Reflection.TargetInvocationException)
                    {
                        return;
                    }

                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }

            RegistryKey updateKey = Registry.CurrentUser.CreateSubKey(AutoUpdater.RegistryLocation);
            if (updateKey != null)
            {
                updateKey.SetValue("version", AutoUpdater.CurrentVersion);
                updateKey.SetValue("skip", 0);
                DateTime remindLaterDateTime = DateTime.Now;
                switch (AutoUpdater.RemindLaterTimeSpan)
                {
                    case RemindLaterFormat.Days:
                        remindLaterDateTime = DateTime.Now + TimeSpan.FromDays(AutoUpdater.RemindLaterAt);
                        break;
                    case RemindLaterFormat.Hours:
                        remindLaterDateTime = DateTime.Now + TimeSpan.FromHours(AutoUpdater.RemindLaterAt);
                        break;
                    case RemindLaterFormat.Minutes:
                        remindLaterDateTime = DateTime.Now + TimeSpan.FromMinutes(AutoUpdater.RemindLaterAt);
                        break;
                }

                updateKey.SetValue(
                    "remindlater", 
                    remindLaterDateTime.ToString(CultureInfo.CreateSpecificCulture("en-US")));
                this.SetTimer(remindLaterDateTime);
                updateKey.Close();
            }
        }

        /// <summary>
        /// The button skip click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ButtonSkipClick(object sender, EventArgs e)
        {
            RegistryKey updateKey = Registry.CurrentUser.CreateSubKey(AutoUpdater.RegistryLocation);
            if (updateKey != null)
            {
                updateKey.SetValue("version", AutoUpdater.CurrentVersion.ToString());
                updateKey.SetValue("skip", 1);
                updateKey.Close();
            }
        }

        /// <summary>
        /// The button update click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ButtonUpdateClick(object sender, EventArgs e)
        {
            if (AutoUpdater.OpenDownloadPage)
            {
                var processStartInfo = new ProcessStartInfo(AutoUpdater.DownloadURL);

                Process.Start(processStartInfo);
            }
            else
            {
                var downloadDialog = new DownloadUpdateDialog(AutoUpdater.DownloadURL);

                try
                {
                    downloadDialog.ShowDialog();
                }
                catch (System.Reflection.TargetInvocationException)
                {
                }
            }
        }

        /// <summary>
        /// The timer elapsed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this._timer.Stop();
            AutoUpdater.Start();
        }

        /// <summary>
        /// The update form load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UpdateFormLoad(object sender, EventArgs e)
        {
            this.webBrowser.Navigate(AutoUpdater.ChangeLogURL);
        }

        #endregion
    }
}