namespace SIS.Forms
{
    using System.Windows.Forms;

    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm()
        {
            this.InitializeComponent();
        }

        public int Progress
        {
            set
            {
                this.pbarProgress.Value = value;
                this.pbarProgress.Update();
            }
        }

        public void UpdateProgress()
        {
            this.pbarProgress.Invalidate();
        }

    }
}
