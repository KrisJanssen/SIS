// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressBarForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The progress bar form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Forms
{
    using System.Windows.Forms;

    /// <summary>
    /// The progress bar form.
    /// </summary>
    public partial class ProgressBarForm : Form
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBarForm"/> class.
        /// </summary>
        public ProgressBarForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Sets the progress.
        /// </summary>
        public int Progress
        {
            set
            {
                this.pbarProgress.Value = value;
                this.pbarProgress.Update();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The update progress.
        /// </summary>
        public void UpdateProgress()
        {
            this.pbarProgress.Invalidate();
        }

        #endregion
    }
}