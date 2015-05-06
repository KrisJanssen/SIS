﻿//using PaintDotNet.SystemLayer;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SIS.SystemLayer;
using System.Threading;

namespace SIS.Library
{
    public sealed class Ruler
        : UserControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private MeasurementUnit measurementUnit = MeasurementUnit.Inch;
        public MeasurementUnit MeasurementUnit
        {
            get
            {
                return measurementUnit;
            }

            set
            {
                if (value != measurementUnit)
                {
                    measurementUnit = value;
                    Invalidate();
                }
            }
        }

        private Orientation orientation = Orientation.Horizontal;

        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get
            {
                return orientation;
            }

            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    Invalidate();
                }
            }
        }

        private double dpu = 96;

        [DefaultValue(96.0)]
        public double Dpu
        {
            get
            {
                return dpu;
            }

            set
            {
                if (value != dpu)
                {
                    dpu = value;
                    Invalidate();
                }
            }
        }

        private ScaleFactor scaleFactor = ScaleFactor.OneToOne;

        [Browsable(false)]
        public ScaleFactor ScaleFactor
        {
            get
            {
                return scaleFactor;
            }

            set
            {
                if (scaleFactor != value)
                {
                    scaleFactor = value;
                    Invalidate();
                }
            }
        }

        private float offset = 0;

        [DefaultValue(0)]
        public float Offset
        {
            get
            {
                return offset;
            }

            set
            {
                if (offset != value)
                {
                    offset = value;
                    Invalidate();
                }
            }
        }

        private float rulerValue = 0.0f;

        [DefaultValue(0)]
        public float Value
        {
            get
            {
                return rulerValue;
            }

            set
            {
                if (this.rulerValue != value)
                {
                    float oldStart = this.scaleFactor.ScaleScalar(this.rulerValue - offset) - 1;
                    float oldEnd = this.scaleFactor.ScaleScalar(this.rulerValue + 1 - offset) + 1;
                    RectangleF oldRect;

                    if (this.orientation == Orientation.Horizontal)
                    {
                        oldRect = new RectangleF(oldStart, this.ClientRectangle.Top, oldEnd - oldStart, this.ClientRectangle.Height);
                    }
                    else // if (this.orientation == Orientation.Vertical)
                    {
                        oldRect = new RectangleF(this.ClientRectangle.Left, oldStart, this.ClientRectangle.Width, oldEnd - oldStart);
                    }

                    float newStart = this.scaleFactor.ScaleScalar(value - offset);
                    float newEnd = this.scaleFactor.ScaleScalar(value + 1 - offset);
                    RectangleF newRect;

                    if (this.orientation == Orientation.Horizontal)
                    {
                        newRect = new RectangleF(newStart, this.ClientRectangle.Top, newEnd - newStart, this.ClientRectangle.Height);
                    }
                    else // if (this.orientation == Orientation.Vertical)
                    {
                        newRect = new RectangleF(this.ClientRectangle.Left, newStart, this.ClientRectangle.Width, newEnd - newStart);
                    }

                    this.rulerValue = value;

                    Invalidate(Utility.RoundRectangle(oldRect));
                    Invalidate(Utility.RoundRectangle(newRect));
                }
            }
        }

        private float highlightStart = 0.0f;
        public float HighlightStart
        {
            get
            {
                return this.highlightStart;
            }

            set
            {
                if (this.highlightStart != value)
                {
                    this.highlightStart = value;
                    Invalidate();
                }
            }
        }

        private float highlightLength = 0.0f;
        public float HighlightLength
        {
            get
            {
                return this.highlightLength;
            }

            set
            {
                if (this.highlightLength != value)
                {
                    this.highlightLength = value;
                    Invalidate();
                }
            }
        }

        private bool highlightEnabled = false;
        public bool HighlightEnabled
        {
            get
            {
                return this.highlightEnabled;
            }

            set
            {
                if (this.highlightEnabled != value)
                {
                    this.highlightEnabled = value;
                    Invalidate();
                }
            }
        }

        public Ruler()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float valueStart = this.scaleFactor.ScaleScalar(this.rulerValue - offset);
            float valueEnd = this.scaleFactor.ScaleScalar(this.rulerValue + 1.0f - offset);
            float highlightStartPx = this.scaleFactor.ScaleScalar(this.highlightStart - offset);
            float highlightEndPx = this.scaleFactor.ScaleScalar(this.highlightStart + this.highlightLength - offset);

            RectangleF highlightRect;
            RectangleF valueRect;

            if (this.orientation == Orientation.Horizontal)
            {
                valueRect = new RectangleF(valueStart, this.ClientRectangle.Top, valueEnd - valueStart, this.ClientRectangle.Height);
                highlightRect = new RectangleF(highlightStartPx, this.ClientRectangle.Top, highlightEndPx - highlightStartPx, this.ClientRectangle.Height);
            }
            else // if (this.orientation == Orientation.Vertical)
            {
                valueRect = new RectangleF(this.ClientRectangle.Left, valueStart, this.ClientRectangle.Width, valueEnd - valueStart);
                highlightRect = new RectangleF(this.ClientRectangle.Left, highlightStartPx, this.ClientRectangle.Width, highlightEndPx - highlightStartPx);
            }

            if (!this.highlightEnabled)
            {
                highlightRect = RectangleF.Empty;
            }

            if (this.orientation == Orientation.Horizontal)
            {
                // This could be used in the future for making a DPI aware app. Needs a specific system layer though...
                //e.Graphics.DrawLine(
                //    SystemPens.WindowText,
                //    UI.ScaleWidth(15),
                //    ClientRectangle.Top,
                //    UI.ScaleWidth(15),
                //    ClientRectangle.Bottom);
                e.Graphics.DrawLine(
                    SystemPens.WindowText,
                    15,
                    ClientRectangle.Top,
                    15,
                    ClientRectangle.Bottom);

                //string abbStringName = "MeasurementUnit." + this.MeasurementUnit.ToString() + ".Abbreviation";
                //string abbString = SISResources.GetString(abbStringName);
                //e.Graphics.DrawString("Unit", Font, SystemBrushes.WindowText, UI.ScaleWidth(-2), 0);
                e.Graphics.DrawString("nm", Font, SystemBrushes.WindowText, -2, 0);
            }

            Region clipRegion = new Region(highlightRect);
            clipRegion.Xor(valueRect);

            if (this.orientation == Orientation.Horizontal)
            {
                //clipRegion.Exclude(new Rectangle(0, 0, UI.ScaleWidth(16), ClientRectangle.Height));
                clipRegion.Exclude(new Rectangle(0, 0, 16, ClientRectangle.Height));
            }

            e.Graphics.SetClip(clipRegion, CombineMode.Replace);
            DrawRuler(e, true);

            clipRegion.Xor(this.ClientRectangle);

            if (this.orientation == Orientation.Horizontal)
            {
                //clipRegion.Exclude(new Rectangle(0, 0, UI.ScaleWidth(16), ClientRectangle.Height - 1));
                clipRegion.Exclude(new Rectangle(0, 0, 16, ClientRectangle.Height - 1));
            }

            e.Graphics.SetClip(clipRegion, CombineMode.Replace);
            DrawRuler(e, false);
        }

        private static readonly float[] majorDivisors =
            new float[] 
            {
                2.0f, 
                2.5f, 
                2.0f
            };

        private int[] GetSubdivs(MeasurementUnit unit)
        {
            switch (unit)
            {
                case MeasurementUnit.Centimeter:
                    {
                        return new int[] { 2, 5 };
                    }

                case MeasurementUnit.Inch:
                    {
                        return new int[] { 2 };
                    }

                default:
                    {
                        return null;
                    }
            }
        }

        void SubdivideX(
            Graphics g,
            Pen pen,
            float x,
            float delta,
            int index,
            float y,
            float height,
            int[] subdivs)
        {
            g.DrawLine(pen, x, y, x, y + height);

            if (index > 10)
            {
                return;
            }

            float div;

            if (subdivs != null && index >= 0)
            {
                div = subdivs[index % subdivs.Length];
            }
            else if (index < 0)
            {
                div = majorDivisors[(-index - 1) % majorDivisors.Length];
            }
            else
            {
                return;
            }

            for (int i = 0; i < div; i++)
            {
                if ((delta / div) > 3.5)
                {
                    SubdivideX(g, pen, x + delta * i / div, delta / div, index + 1, y, height / div + 0.5f, subdivs);
                }
            }
        }

        void SubdivideY(
            Graphics g,
            Pen pen,
            float y,
            float delta,
            int index,
            float x,
            float width,
            int[] subdivs)
        {
            g.DrawLine(pen, x, y, x + width, y);

            if (index > 10)
            {
                return;
            }

            float div;

            if (subdivs != null && index >= 0)
            {
                div = subdivs[index % subdivs.Length];
            }
            else if (index < 0)
            {
                div = majorDivisors[(-index - 1) % majorDivisors.Length];
            }
            else
            {
                return;
            }

            for (int i = 0; i < div; i++)
            {
                if ((delta / div) > 3.5)
                {
                    SubdivideY(g, pen, y + delta * i / div, delta / div, index + 1, x, width / div + 0.5f, subdivs);
                }
            }
        }

        private void DrawRuler(PaintEventArgs e, bool highlighted)
        {
            Pen pen;
            Brush cursorBrush;
            Brush textBrush;
            StringFormat textFormat = new StringFormat();
            int maxPixel;
            Color cursorColor;

            if (highlighted)
            {
                e.Graphics.Clear(SystemColors.Highlight);
                pen = SystemPens.HighlightText;
                textBrush = SystemBrushes.HighlightText;
                cursorColor = SystemColors.Window;
            }
            else
            {
                e.Graphics.Clear(SystemColors.Window);
                pen = SystemPens.WindowText;
                textBrush = SystemBrushes.WindowText;
                cursorColor = SystemColors.Highlight;
            }

            cursorColor = Color.FromArgb(128, cursorColor);
            cursorBrush = new SolidBrush(cursorColor);

            if (orientation == Orientation.Horizontal)
            {
                maxPixel = ScaleFactor.UnscaleScalar(ClientRectangle.Width);
                textFormat.Alignment = StringAlignment.Near;
                textFormat.LineAlignment = StringAlignment.Far;
            }
            else // if (orientation == Orientation.Vertical)
            {
                // maxPixel holds the exact number of pixels that is available in the client rectangle to display the ruler.
                maxPixel = ScaleFactor.UnscaleScalar(ClientRectangle.Height);

                // These lines govern the way the text of the ruler is rendered.
                textFormat.Alignment = StringAlignment.Near;
                textFormat.LineAlignment = StringAlignment.Near;
                textFormat.FormatFlags |= StringFormatFlags.DirectionVertical;

                //Matrix mx = new Matrix(1, 0, 0, -1, 0, this.ClientSize.Height);

                //e.Graphics.Transform = mx;
            }

            // Not sure what these do yet...
            float majorSkip = 1;
            int majorSkipPower = 0;

            // majorDivisionLength gives the amount of pixels that represent a unit of length (a nm, cm, ...).
            float majorDivisionLength = (float)dpu;
            // If need be, majorDivisionLength can be modified in a dpi aware environment.
            float majorDivisionPixels = (float)ScaleFactor.ScaleScalar(majorDivisionLength);
            // Gets the number of subdivisions that are made for a certain measurement unit (for cm this is 2 and 5 and for inch this is 2, all others 0).
            // Still have to figure out how this works exactly...
            int[] subdivs = GetSubdivs(measurementUnit);
            // Self-explanatory. The amount of pixels for the offset.
            float offsetPixels = ScaleFactor.ScaleScalar((float)offset);
            // Calculates the indicated start value of the ruler in the physical unit, based on the pixel value...
            int startMajor = (int)(offset / majorDivisionLength) - 1;
            // Calculates the indicated end value of the ruler in the physical unit, based on the pixel value...
            int endMajor = (int)((offset + maxPixel) / majorDivisionLength) + 1;

            if (orientation == Orientation.Horizontal)
            {
                // draw Value
                if (!highlighted)
                {
                    PointF pt = scaleFactor.ScalePointJustX(new PointF(ClientRectangle.Left + Value - Offset, ClientRectangle.Top));
                    SizeF size = new SizeF(Math.Max(1, scaleFactor.ScaleScalar(1.0f)), ClientRectangle.Height);

                    pt.X -= 0.5f;

                    CompositingMode oldCM = e.Graphics.CompositingMode;
                    e.Graphics.CompositingMode = CompositingMode.SourceOver;
                    e.Graphics.FillRectangle(cursorBrush, new RectangleF(pt, size));
                    e.Graphics.CompositingMode = oldCM;
                }

                // draw border
                e.Graphics.DrawLine(SystemPens.WindowText, new Point(ClientRectangle.Left, ClientRectangle.Bottom - 1),
                    new Point(ClientRectangle.Right - 1, ClientRectangle.Bottom - 1));
            }
            else if (orientation == Orientation.Vertical)
            {
                // draw Value
                if (!highlighted)
                {
                    PointF pt = scaleFactor.ScalePointJustY(new PointF(ClientRectangle.Left, ClientRectangle.Top + Value - Offset));
                    SizeF size = new SizeF(ClientRectangle.Width, Math.Max(1, scaleFactor.ScaleScalar(1.0f)));

                    pt.Y -= 0.5f;
                    CompositingMode oldCM = e.Graphics.CompositingMode;
                    e.Graphics.CompositingMode = CompositingMode.SourceOver;
                    e.Graphics.FillRectangle(cursorBrush, new RectangleF(pt, size));
                    e.Graphics.CompositingMode = oldCM;
                }

                // draw border
                e.Graphics.DrawLine(SystemPens.WindowText, new Point(ClientRectangle.Right - 1, ClientRectangle.Top),
                    new Point(ClientRectangle.Right - 1, ClientRectangle.Bottom - 1));
            }

            while (majorDivisionPixels * majorSkip < 60)
            {
                // majorDivisors is float{2.0 2.5 2.0}
                majorSkip *= majorDivisors[majorSkipPower % majorDivisors.Length];
                ++majorSkipPower;
            }

            startMajor = (int)(majorSkip * Math.Floor(startMajor / (double)majorSkip));

            for (int major = startMajor; major <= endMajor; major += (int)majorSkip)
            {
                float majorMarkPos = (major * majorDivisionPixels) - offsetPixels;
                string majorText = (major).ToString();

                if (orientation == Orientation.Horizontal)
                {
                    SubdivideX(e.Graphics, pen, ClientRectangle.Left + majorMarkPos, majorDivisionPixels * majorSkip, -majorSkipPower, ClientRectangle.Top, ClientRectangle.Height, subdivs);
                    e.Graphics.DrawString(majorText, Font, textBrush, new PointF(ClientRectangle.Left + majorMarkPos, ClientRectangle.Bottom), textFormat);
                }
                else // if (orientation == Orientation.Vertical)
                {
                    SubdivideY(e.Graphics, pen, ClientRectangle.Top + majorMarkPos, majorDivisionPixels * majorSkip, -majorSkipPower, ClientRectangle.Left, ClientRectangle.Width, subdivs);
                    e.Graphics.DrawString(majorText, Font, textBrush, new PointF(ClientRectangle.Left, ClientRectangle.Top + majorMarkPos), textFormat);
                    //e.Graphics.DrawString(majorText, Font, textBrush, new PointF(ClientRectangle.Left, ClientRectangle.Bottom - majorMarkPos), textFormat);
                }
            }

            textFormat.Dispose();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
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
            components = new System.ComponentModel.Container();
        }
        #endregion
    }
}
