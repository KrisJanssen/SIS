namespace KUL.MDS.SIS.Forms
{
    partial class BlendImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.imageControl1 = new KUL.MDS.Library.ImageControl();
            this.SuspendLayout();
            // 
            // imageControl1
            // 
            this.imageControl1.BackColor = System.Drawing.SystemColors.Control;
            this.imageControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageControl1.Image = null;
            this.imageControl1.ImageText = null;
            this.imageControl1.Location = new System.Drawing.Point(0, 0);
            this.imageControl1.Margin = new System.Windows.Forms.Padding(2);
            this.imageControl1.Name = "imageControl1";
            this.imageControl1.Origin = new System.Drawing.Point(0, 0);
            this.imageControl1.PanButton = System.Windows.Forms.MouseButtons.Left;
            this.imageControl1.PanMode = true;
            this.imageControl1.ScrollbarsVisible = true;
            this.imageControl1.Size = new System.Drawing.Size(491, 502);
            this.imageControl1.StretchImageToFit = false;
            this.imageControl1.TabIndex = 0;
            this.imageControl1.ZoomFactor = 1;
            this.imageControl1.ZoomOnMouseWheel = true;
            // 
            // BlendImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 502);
            this.Controls.Add(this.imageControl1);
            this.Name = "BlendImageForm";
            this.Text = "BlendImageForm";
            this.ResumeLayout(false);

        }

        #endregion

        private KUL.MDS.Library.ImageControl imageControl1;
    }
}