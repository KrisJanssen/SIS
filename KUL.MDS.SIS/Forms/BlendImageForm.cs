using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIS.Forms
{
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
            InitializeComponent();
        }
    }
}
