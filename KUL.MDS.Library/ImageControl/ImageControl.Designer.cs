namespace SIS.Library.ImageControl
{
   partial class ImageControl
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
         if (disposing && (this.components != null))
         {
            this.components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Component Designer generated code

      /// <summary> 
      /// Required method for Designer support - do not modify 
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
          this.m_scrlbrVScrollBar = new System.Windows.Forms.VScrollBar();
          this.m_scrlbrHScrollBar = new System.Windows.Forms.HScrollBar();
          this.m_rlrVRuler = new Ruler();
          this.m_rlrHRuler = new Ruler();
          this.m_drwcnvDrawCanvas = new DrawCanvas();
          this.SuspendLayout();
          // 
          // m_scrlbrVScrollBar
          // 
          this.m_scrlbrVScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.m_scrlbrVScrollBar.Location = new System.Drawing.Point(528, 16);
          this.m_scrlbrVScrollBar.Name = "m_scrlbrVScrollBar";
          this.m_scrlbrVScrollBar.Size = new System.Drawing.Size(20, 512);
          this.m_scrlbrVScrollBar.TabIndex = 1;
          this.m_scrlbrVScrollBar.ValueChanged += new System.EventHandler(this.scrollBar_ValueChanged);
          // 
          // m_scrlbrHScrollBar
          // 
          this.m_scrlbrHScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.m_scrlbrHScrollBar.Location = new System.Drawing.Point(16, 528);
          this.m_scrlbrHScrollBar.Name = "m_scrlbrHScrollBar";
          this.m_scrlbrHScrollBar.Size = new System.Drawing.Size(512, 20);
          this.m_scrlbrHScrollBar.TabIndex = 2;
          this.m_scrlbrHScrollBar.ValueChanged += new System.EventHandler(this.scrollBar_ValueChanged);
          // 
          // m_rlrVRuler
          // 
          this.m_rlrVRuler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.m_rlrVRuler.Dpu = 1;
          this.m_rlrVRuler.HighlightEnabled = false;
          this.m_rlrVRuler.HighlightLength = 0F;
          this.m_rlrVRuler.HighlightStart = 0F;
          this.m_rlrVRuler.Location = new System.Drawing.Point(0, 16);
          this.m_rlrVRuler.MeasurementUnit = MeasurementUnit.Pixel;
          this.m_rlrVRuler.Name = "m_rlrVRuler";
          this.m_rlrVRuler.Offset = 0F;
          this.m_rlrVRuler.Orientation = System.Windows.Forms.Orientation.Vertical;
          this.m_rlrVRuler.Size = new System.Drawing.Size(16, 532);
          this.m_rlrVRuler.TabIndex = 4;
          this.m_rlrVRuler.Value = 0F;
          // 
          // m_rlrHRuler
          // 
          this.m_rlrHRuler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.m_rlrHRuler.Dpu = 1;
          this.m_rlrHRuler.HighlightEnabled = false;
          this.m_rlrHRuler.HighlightLength = 0F;
          this.m_rlrHRuler.HighlightStart = 0F;
          this.m_rlrHRuler.Location = new System.Drawing.Point(0, 0);
          this.m_rlrHRuler.MeasurementUnit = MeasurementUnit.Pixel;
          this.m_rlrHRuler.Name = "m_rlrHRuler";
          this.m_rlrHRuler.Offset = -16F;
          this.m_rlrHRuler.Size = new System.Drawing.Size(548, 16);
          this.m_rlrHRuler.TabIndex = 3;
          this.m_rlrHRuler.Value = 0F;
          // 
          // m_drwcnvDrawCanvas
          // 
          this.m_drwcnvDrawCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.m_drwcnvDrawCanvas.Image = null;
          this.m_drwcnvDrawCanvas.ImageText = null;
          this.m_drwcnvDrawCanvas.Location = new System.Drawing.Point(16, 16);
          this.m_drwcnvDrawCanvas.Margin = new System.Windows.Forms.Padding(2);
          this.m_drwcnvDrawCanvas.Name = "m_drwcnvDrawCanvas";
          this.m_drwcnvDrawCanvas.Origin = new System.Drawing.Point(0, 0);
          this.m_drwcnvDrawCanvas.PanButton = System.Windows.Forms.MouseButtons.Left;
          this.m_drwcnvDrawCanvas.PanMode = true;
          this.m_drwcnvDrawCanvas.Size = new System.Drawing.Size(512, 512);
          this.m_drwcnvDrawCanvas.StretchImageToFit = false;
          this.m_drwcnvDrawCanvas.TabIndex = 0;
          this.m_drwcnvDrawCanvas.ZoomFactor = 1;
          this.m_drwcnvDrawCanvas.ZoomOnMouseWheel = true;
          this.m_drwcnvDrawCanvas.OnMousePositionChanged += new System.EventHandler(this.m_drwcnvDrawCanvas_OnMousePositionChanged);
          this.m_drwcnvDrawCanvas.OnPositionSelected += new System.EventHandler(this.m_drwcnvDrawCanvas_OnPositionSelected);
          this.m_drwcnvDrawCanvas.OnScrollPositionsChanged += new System.EventHandler(this.m_drwcnvDrawCanvas_OnSetScrollPositions);
          // 
          // ImageControl
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.SystemColors.Control;
          this.Controls.Add(this.m_rlrVRuler);
          this.Controls.Add(this.m_rlrHRuler);
          this.Controls.Add(this.m_scrlbrHScrollBar);
          this.Controls.Add(this.m_scrlbrVScrollBar);
          this.Controls.Add(this.m_drwcnvDrawCanvas);
          this.Margin = new System.Windows.Forms.Padding(2);
          this.Name = "ImageControl";
          this.Size = new System.Drawing.Size(548, 548);
          this.ResumeLayout(false);

      }

      #endregion

      private DrawCanvas m_drwcnvDrawCanvas;
      private System.Windows.Forms.VScrollBar m_scrlbrVScrollBar;
      private System.Windows.Forms.HScrollBar m_scrlbrHScrollBar;
      private Ruler m_rlrHRuler;
      private Ruler m_rlrVRuler;
   }
}

