// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BlendImageForm.cs" company="">
//   
// </copyright>
// <summary>
//   The blend image form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Forms
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The blend image form.
    /// </summary>
    public partial class BlendImageForm : Form
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BlendImageForm"/> class.
        /// </summary>
        public BlendImageForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the imgage.
        /// </summary>
        public Image Imgage
        {
            get
            {
                return this.imageControl1.Image;
            }

            set
            {
                this.imageControl1.Image = value;
                this.imageControl1.FitToScreen();
            }
        }

        #endregion
    }
}