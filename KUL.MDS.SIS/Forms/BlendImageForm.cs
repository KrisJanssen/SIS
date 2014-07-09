namespace SIS.Forms
{
    using System.Drawing;
    using System.Windows.Forms;

    public partial class BlendImageForm : Form
    {
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
        public BlendImageForm()
        {
            this.InitializeComponent();
        }
    }
}
