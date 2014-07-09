using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIS.Forms
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm()
        {
            InitializeComponent();
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
