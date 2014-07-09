// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemindLaterForm.cs" company="">
//   
// </copyright>
// <summary>
//   The remind later form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AutoUpdaterDotNET
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The remind later form.
    /// </summary>
    internal partial class RemindLaterForm : Form
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemindLaterForm"/> class.
        /// </summary>
        public RemindLaterForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the remind later at.
        /// </summary>
        public int RemindLaterAt { get; private set; }

        /// <summary>
        /// Gets the remind later format.
        /// </summary>
        public RemindLaterFormat RemindLaterFormat { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// The button ok click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ButtonOkClick(object sender, EventArgs e)
        {
            if (this.radioButtonYes.Checked)
            {
                switch (this.comboBoxRemindLater.SelectedIndex)
                {
                    case 0:
                        this.RemindLaterFormat = RemindLaterFormat.Minutes;
                        this.RemindLaterAt = 30;
                        break;
                    case 1:
                        this.RemindLaterFormat = RemindLaterFormat.Hours;
                        this.RemindLaterAt = 12;
                        break;
                    case 2:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 1;
                        break;
                    case 3:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 2;
                        break;
                    case 4:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 4;
                        break;
                    case 5:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 8;
                        break;
                    case 6:
                        this.RemindLaterFormat = RemindLaterFormat.Days;
                        this.RemindLaterAt = 10;
                        break;
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }
        }

        /// <summary>
        /// The radio button yes checked changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RadioButtonYesCheckedChanged(object sender, EventArgs e)
        {
            this.comboBoxRemindLater.Enabled = this.radioButtonYes.Checked;
        }

        /// <summary>
        /// The remind later form load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RemindLaterFormLoad(object sender, EventArgs e)
        {
            this.comboBoxRemindLater.SelectedIndex = 0;
            this.radioButtonYes.Checked = true;
        }

        #endregion
    }
}