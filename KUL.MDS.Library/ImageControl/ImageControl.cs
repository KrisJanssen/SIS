using System;
using System.Drawing;
using System.Windows.Forms;
using SIS.SystemLayer;

namespace SIS.Library
{
    [System.Runtime.InteropServices.ComVisible(false)]
    public partial class ImageControl : UserControl
    {
        #region Member Variables.

        private bool m_bScrollVisible = true;
        private double m_dPhysImageHeight;
        private double m_dPhysImageWidth;
        private double m_dOriginalZoomFactor;
        private double m_dXPositionSelected;
        private double m_dYPositionSelected;

        #endregion

        # region Properties.

        public double XPositionSelected
        {
            get
            {
                return this.m_dXPositionSelected;
            }
        }

        public double YPositionSelected
        {
            get
            {
                return this.m_dYPositionSelected;
            }
        }

        public Size ApparentImageSize
        {
            get
            {
                return this.m_drwcnvDrawCanvas.ApparentImageSize;
            }
        }

        public double XDpu
        {
            set
            {
                this.m_rlrHRuler.Dpu = value;
            }
        }

        public double YDpu
        {
            set
            {
                this.m_rlrVRuler.Dpu = value;
            }
        }

        public double ImageWidth
        {
            set
            {
                this.m_dPhysImageWidth = value;
            }
        }

        public double ImageHeight
        {
            set
            {
                this.m_dPhysImageHeight = value;
            }
        }

        public Image Image
        {
            get
            {
                return this.m_drwcnvDrawCanvas.Image;
            }
            set
            {
                this.m_drwcnvDrawCanvas.Image = value;
                if (value != null)
                {
                    this.m_dOriginalZoomFactor = (double)this.m_drwcnvDrawCanvas.Width / (double)this.m_drwcnvDrawCanvas.Image.Width;
                }
                if ((value == null))
                {
                    m_scrlbrHScrollBar.Enabled = false;
                    m_scrlbrVScrollBar.Enabled = false;
                }
            }
        }

        public string ImageText
        {
            get
            {
                return m_drwcnvDrawCanvas.ImageText;
            }
            set
            {
                m_drwcnvDrawCanvas.ImageText = value;
            }
        }

        public MouseButtons PanButton
        {
            get
            {
                return this.m_drwcnvDrawCanvas.PanButton;
            }
            set
            {
                this.m_drwcnvDrawCanvas.PanButton = value;
            }
        }

        public Point Origin
        {
            get
            {
                return this.m_drwcnvDrawCanvas.Origin;
            }
            set
            {
                this.m_drwcnvDrawCanvas.Origin = value;
            }
        }

        public bool PanMode
        {
            get
            {
                return this.m_drwcnvDrawCanvas.PanMode;
            }
            set
            {
                this.m_drwcnvDrawCanvas.PanMode = value;
            }
        }

        public bool ScrollbarsVisible
        {
            get
            {
                return this.m_bScrollVisible;
            }
            set
            {
                this.m_bScrollVisible = value;
                this.m_scrlbrHScrollBar.Visible = value;
                this.m_scrlbrVScrollBar.Visible = value;
                if ((value == false))
                {
                    this.m_drwcnvDrawCanvas.Dock = DockStyle.Fill;
                }
                // TODO: Clean this up to make scrollbars/rulers visible or invisible in a correct way.
                else
                {
                    //this.drawingBoard1.Dock = DockStyle.None;
                    //this.drawingBoard1.Location = new Point(16, 16);
                    //this.drawingBoard1.Width = (ClientSize.Width - vScrollBar1.Width - 16);
                    //this.drawingBoard1.Height = (ClientSize.Height - hScrollBar1.Height - 16);
                }
            }
        }

        public bool StretchImageToFit
        {
            get
            {
                return this.m_drwcnvDrawCanvas.StretchImageToFit;
            }
            set
            {
                this.m_drwcnvDrawCanvas.StretchImageToFit = value;
            }
        }

        public double ZoomFactor
        {
            get
            {
                return this.m_drwcnvDrawCanvas.ZoomFactor;
            }
            set
            {
                this.m_drwcnvDrawCanvas.ZoomFactor = value;
            }
        }

        public bool ZoomOnMouseWheel
        {
            get
            {
                return this.m_drwcnvDrawCanvas.ZoomOnMouseWheel;
            }
            set
            {
                this.m_drwcnvDrawCanvas.ZoomOnMouseWheel = value;
            }
        }

        #endregion

        #region Events.

        public event EventHandler OnPositionSelected;

        #endregion

        #region Methods.

        public ImageControl()
        {
            //  This call is required by the Windows Form Designer.
            InitializeComponent();
        }

        public void FitToScreen()
        {
            this.m_drwcnvDrawCanvas.FitToScreen();
        }

        public void RotateFlip(RotateFlipType __rftpRotateFlipType)
        {
            m_drwcnvDrawCanvas.RotateFlip(__rftpRotateFlipType);
        }

        public void ZoomIn()
        {
            this.m_drwcnvDrawCanvas.ZoomIn();
        }

        public void ZoomOut()
        {
            this.m_drwcnvDrawCanvas.ZoomOut();
        }

        private void SetScrollBars()
        {
            int DrawingWidth = this.m_drwcnvDrawCanvas.Image.Width;
            int DrawingHeight = this.m_drwcnvDrawCanvas.Image.Height;
            int OriginX = this.m_drwcnvDrawCanvas.Origin.X;
            int OriginY = this.m_drwcnvDrawCanvas.Origin.Y;
            int FactoredCtrlWidth = (int)(this.m_drwcnvDrawCanvas.Width / this.m_drwcnvDrawCanvas.ZoomFactor);
            int FactoredCtrlHeight = (int)(this.m_drwcnvDrawCanvas.Height / this.m_drwcnvDrawCanvas.ZoomFactor);

            // If we don't substract one here we scrol ZoomFactor * 1 pixels too far!
            // TODO: Figure this out.
            this.m_scrlbrHScrollBar.Maximum = this.m_drwcnvDrawCanvas.Image.Width - 1;
            this.m_scrlbrVScrollBar.Maximum = this.m_drwcnvDrawCanvas.Image.Height - 1;

            if (((FactoredCtrlWidth >= this.m_drwcnvDrawCanvas.Image.Width) || this.m_drwcnvDrawCanvas.StretchImageToFit))
            {
                this.m_scrlbrHScrollBar.Enabled = false;
                this.m_scrlbrHScrollBar.Value = 0;
            }
            else
            {
                this.m_scrlbrHScrollBar.LargeChange = FactoredCtrlWidth;
                this.m_scrlbrHScrollBar.Enabled = true;
                this.m_scrlbrHScrollBar.Value = OriginX;
            }
            if (((FactoredCtrlHeight >= this.m_drwcnvDrawCanvas.Image.Height) || this.m_drwcnvDrawCanvas.StretchImageToFit))
            {
                this.m_scrlbrVScrollBar.Enabled = false;
                this.m_scrlbrVScrollBar.Value = 0;
            }
            else
            {
                this.m_scrlbrVScrollBar.Enabled = true;
                this.m_scrlbrVScrollBar.LargeChange = FactoredCtrlHeight;
                this.m_scrlbrVScrollBar.Value = OriginY;
            }

            UpdateRulers();
        }

        private void scrollBar_ValueChanged(Object sender, EventArgs e)
        {
            // Get the new origin for the drawcanvas.
            this.m_drwcnvDrawCanvas.Origin = new Point(
                this.m_scrlbrHScrollBar.Value, 
                this.m_scrlbrVScrollBar.Value);
        }

        private void m_drwcnvDrawCanvas_OnMousePositionChanged(object sender, EventArgs e)
        {
            UpdateRulers();
        }

        private void UpdateRulers()
        {
            // The interpolation mode on the graphics will introduce 'rounding errors':
            // When ZoomFactor = 25 and the Drawingboard is 512 px wide there should be 20.48 pixels of the image in view 
            // for an original 512 px image. However, the interpolation will cause 20 px to be displayed. This makes the
            // real ZoomFactor to be 25.6 and therefore this value should be fed to the rulers.
            // This is done by doing the calculation 512 / Math.Round(this.drawingBoard1.Width / this.drawingBoard1.ZoomFactor, 0)
            //this.m_rlrHRuler.ScaleFactor = ScaleFactor.FromDouble((double)this.m_drwcnvDrawCanvas.Width / Math.Round((double)this.m_drwcnvDrawCanvas.Width / this.m_drwcnvDrawCanvas.ZoomFactor, 0, MidpointRounding.ToEven));
            this.m_rlrHRuler.ScaleFactor = ScaleFactor.FromDouble(this.m_drwcnvDrawCanvas.ZoomFactor);

            // It is VERY important to use a FLOAT value for the offset, otherwise you will introduce rounding erros in ruler alignment!
            this.m_rlrHRuler.Offset = this.m_drwcnvDrawCanvas.Origin.X + this.m_rlrHRuler.ScaleFactor.UnscaleScalar(-16.0f);

            //this.m_rlrVRuler.ScaleFactor = ScaleFactor.FromDouble((double)this.m_drwcnvDrawCanvas.Width / Math.Round((double)this.m_drwcnvDrawCanvas.Width / this.m_drwcnvDrawCanvas.ZoomFactor, 0, MidpointRounding.ToEven));
            this.m_rlrVRuler.ScaleFactor = ScaleFactor.FromDouble(this.m_drwcnvDrawCanvas.ZoomFactor);
            this.m_rlrVRuler.Offset = this.m_drwcnvDrawCanvas.Origin.Y;

            // Calculate the amount of pixels currently visible on the Canvas.
            // This amount can be calculated as follows:
            // Take the ZoomFactor necessary to make the image fit exactly in the control and compare it to the current zoomfactor.
            // This calculation gives the percentage of total pixels visible! This should be rounded because pixels cannot be split.
            float _fXVisblePixels = (float)Math.Round(
                (
                (double)this.m_drwcnvDrawCanvas.Image.Width *
                ((float)this.m_dOriginalZoomFactor / (float)this.m_drwcnvDrawCanvas.ZoomFactor)
                ), 0);

            // Calculate the percentage position of the cursor along the image dimension.
            float _fXPercentagePosition = (float)this.m_drwcnvDrawCanvas.MouseXPos / (float)this.m_drwcnvDrawCanvas.Width;

            // Current pixel position of the cursor in relation to the image is Offset pixels + Pixels visible * Percentage.
            this.m_rlrHRuler.Value = (float)this.m_drwcnvDrawCanvas.Origin.X + _fXVisblePixels * _fXPercentagePosition;
            //this.m_rlrHRuler.Update();

            // Same for Y.
            float _fYVisblePixels = (float)Math.Round(
                (
                (double)this.m_drwcnvDrawCanvas.Image.Height *
                ((float)this.m_dOriginalZoomFactor / (float)this.m_drwcnvDrawCanvas.ZoomFactor)
                ), 0);

            // Calculate the percentage position of the cursor along the image dimension.
            float _fYPercentagePosition = (float)this.m_drwcnvDrawCanvas.MouseYPos / (float)this.m_drwcnvDrawCanvas.Height;

            // Current pixel position of the cursor in relation to the image is Offset pixels + Pixels visible * Percentage.
            this.m_rlrVRuler.Value = (float)this.m_drwcnvDrawCanvas.Origin.Y + _fYVisblePixels * _fYPercentagePosition;
            //this.m_rlrVRuler.Update();

            this.ImageText =
                "px X: " + this.m_drwcnvDrawCanvas.MouseXPos.ToString() +
                "\r\n" +
                "px Y: " + this.m_drwcnvDrawCanvas.MouseYPos.ToString();
        }

        #endregion

        private void m_drwcnvDrawCanvas_OnSetScrollPositions(object sender, EventArgs e)
        {
            this.SetScrollBars();
        }

        private void m_drwcnvDrawCanvas_OnPositionSelected(object sender, EventArgs e)
        {
            //this.m_dXPositionSelected = Math.Round(((double)this.m_rlrHRuler.Value / (double)this.m_drwcnvDrawCanvas.Image.Width) * (double)this.m_dPhysImageWidth, 0);
            //this.m_dYPositionSelected = Math.Round(((double)this.m_rlrVRuler.Value / (double)this.m_drwcnvDrawCanvas.Image.Height) * (double)this.m_dPhysImageHeight, 0);
            this.m_dXPositionSelected = Math.Round(this.m_rlrHRuler.Value, 0);
            this.m_dYPositionSelected = Math.Round(this.m_rlrVRuler.Value, 0);
            if (this.OnPositionSelected != null)
            {
                this.OnPositionSelected(this, new EventArgs());
                Tracing.Ping("OnPositionSelected raised here! X: " + this.m_dXPositionSelected.ToString() + " Y: " + this.m_dYPositionSelected.ToString());
            }
        }
    }

}