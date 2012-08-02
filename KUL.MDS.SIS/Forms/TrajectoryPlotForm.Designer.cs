namespace KUL.MDS.SIS.Forms
{
    partial class TrajectoryPlotForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrajectoryPlotForm));
            this.graphTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.XCoordGraph = new ZedGraph.ZedGraphControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.YCoordGraph = new ZedGraph.ZedGraphControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.XYCoordGraph = new ZedGraph.ZedGraphControl();
            this.graphTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphTabControl
            // 
            this.graphTabControl.Controls.Add(this.tabPage1);
            this.graphTabControl.Controls.Add(this.tabPage2);
            this.graphTabControl.Controls.Add(this.tabPage3);
            this.graphTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTabControl.HotTrack = true;
            this.graphTabControl.Location = new System.Drawing.Point(0, 0);
            this.graphTabControl.Name = "graphTabControl";
            this.graphTabControl.SelectedIndex = 0;
            this.graphTabControl.Size = new System.Drawing.Size(384, 362);
            this.graphTabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.XCoordGraph);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(376, 336);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // XCoordGraph
            // 
            this.XCoordGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XCoordGraph.Location = new System.Drawing.Point(3, 3);
            this.XCoordGraph.Name = "XCoordGraph";
            this.XCoordGraph.ScrollGrace = 0;
            this.XCoordGraph.ScrollMaxX = 0;
            this.XCoordGraph.ScrollMaxY = 0;
            this.XCoordGraph.ScrollMaxY2 = 0;
            this.XCoordGraph.ScrollMinX = 0;
            this.XCoordGraph.ScrollMinY = 0;
            this.XCoordGraph.ScrollMinY2 = 0;
            this.XCoordGraph.Size = new System.Drawing.Size(370, 330);
            this.XCoordGraph.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.YCoordGraph);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(376, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // YCoordGraph
            // 
            this.YCoordGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YCoordGraph.Location = new System.Drawing.Point(3, 3);
            this.YCoordGraph.Name = "YCoordGraph";
            this.YCoordGraph.ScrollGrace = 0;
            this.YCoordGraph.ScrollMaxX = 0;
            this.YCoordGraph.ScrollMaxY = 0;
            this.YCoordGraph.ScrollMaxY2 = 0;
            this.YCoordGraph.ScrollMinX = 0;
            this.YCoordGraph.ScrollMinY = 0;
            this.YCoordGraph.ScrollMinY2 = 0;
            this.YCoordGraph.Size = new System.Drawing.Size(370, 330);
            this.YCoordGraph.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.XYCoordGraph);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(376, 336);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // XYCoordGraph
            // 
            this.XYCoordGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.XYCoordGraph.Location = new System.Drawing.Point(3, 3);
            this.XYCoordGraph.Name = "XYCoordGraph";
            this.XYCoordGraph.ScrollGrace = 0;
            this.XYCoordGraph.ScrollMaxX = 0;
            this.XYCoordGraph.ScrollMaxY = 0;
            this.XYCoordGraph.ScrollMaxY2 = 0;
            this.XYCoordGraph.ScrollMinX = 0;
            this.XYCoordGraph.ScrollMinY = 0;
            this.XYCoordGraph.ScrollMinY2 = 0;
            this.XYCoordGraph.Size = new System.Drawing.Size(370, 330);
            this.XYCoordGraph.TabIndex = 0;
            // 
            // TrajectoryPlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.graphTabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrajectoryPlotForm";
            this.Text = "TrajectoryPlotForm";
            this.graphTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl graphTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private ZedGraph.ZedGraphControl XCoordGraph;
        private System.Windows.Forms.TabPage tabPage2;
        private ZedGraph.ZedGraphControl YCoordGraph;
        private System.Windows.Forms.TabPage tabPage3;
        private ZedGraph.ZedGraphControl XYCoordGraph;

    }
}