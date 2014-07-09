﻿/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) Rick Brewster, Tom Jackson, and past contributors.            //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

//using PaintDotNet.SystemLayer;

namespace SIS.Library.ImageControl
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

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
                return this.measurementUnit;
            }

            set
            {
                if (value != this.measurementUnit)
                {
                    this.measurementUnit = value;
                    this.Invalidate();
                }
            }
        }

        private Orientation orientation = Orientation.Horizontal;

        [DefaultValue(Orientation.Horizontal)]
        public Orientation Orientation
        {
            get
            {
                return this.orientation;
            }

            set
            {
                if (this.orientation != value)
                {
                    this.orientation = value;
                    this.Invalidate();
                }
            }
        }

        private double dpu = 96;

        [DefaultValue(96.0)]
        public double Dpu
        {
            get
            {
                return this.dpu;
            }

            set
            {
                if (value != this.dpu)
                {
                    this.dpu = value;
                    this.Invalidate();
                }
            }
        }

        private ScaleFactor scaleFactor = ScaleFactor.OneToOne;

        [Browsable(false)]
        public ScaleFactor ScaleFactor
        {
            get
            {
                return this.scaleFactor;
            }

            set
            {
                if (this.scaleFactor != value)
                {
                    this.scaleFactor = value;
                    this.Invalidate();
                }
            }
        }

        private float offset = 0;

        [DefaultValue(0)]
        public float Offset
        {
            get
            {
                return this.offset;
            }

            set
            {
                if (this.offset != value)
                {
                    this.offset = value;
                    this.Invalidate();
                }
            }
        }

        private float rulerValue = 0.0f;

        [DefaultValue(0)]
        public float Value
        {
            get
            {
                return this.rulerValue;
            }

            set
            {
                if (this.rulerValue != value)
                {
                    float oldStart = this.scaleFactor.ScaleScalar(this.rulerValue - this.offset) - 1;
                    float oldEnd = this.scaleFactor.ScaleScalar(this.rulerValue + 1 - this.offset) + 1;
                    RectangleF oldRect;

                    if (this.orientation == Orientation.Horizontal)
                    {
                        oldRect = new RectangleF(oldStart, this.ClientRectangle.Top, oldEnd - oldStart, this.ClientRectangle.Height);
                    }
                    else // if (this.orientation == Orientation.Vertical)
                    {
                        oldRect = new RectangleF(this.ClientRectangle.Left, oldStart, this.ClientRectangle.Width, oldEnd - oldStart);
                    }

                    float newStart = this.scaleFactor.ScaleScalar(value - this.offset);
                    float newEnd = this.scaleFactor.ScaleScalar(value + 1 - this.offset);
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

                    this.Invalidate(Utility.RoundRectangle(oldRect));
                    this.Invalidate(Utility.RoundRectangle(newRect));
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
                    this.Invalidate();
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
                    this.Invalidate();
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
                    this.Invalidate();
                }
            }
        }

        public Ruler()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            // This call is required by the Windows.Forms Form Designer.
            this.InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float valueStart = this.scaleFactor.ScaleScalar(this.rulerValue - this.offset);
            float valueEnd = this.scaleFactor.ScaleScalar(this.rulerValue + 1.0f - this.offset);
            float highlightStartPx = this.scaleFactor.ScaleScalar(this.highlightStart - this.offset);
            float highlightEndPx = this.scaleFactor.ScaleScalar(this.highlightStart + this.highlightLength - this.offset);

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
                    this.ClientRectangle.Top,
                    15,
                    this.ClientRectangle.Bottom);

                //string abbStringName = "MeasurementUnit." + this.MeasurementUnit.ToString() + ".Abbreviation";
                //string abbString = SISResources.GetString(abbStringName);
                //e.Graphics.DrawString("Unit", Font, SystemBrushes.WindowText, UI.ScaleWidth(-2), 0);
                e.Graphics.DrawString("nm", this.Font, SystemBrushes.WindowText, -2, 0);
            }

            Region clipRegion = new Region(highlightRect);
            clipRegion.Xor(valueRect);

            if (this.orientation == Orientation.Horizontal)
            {
                //clipRegion.Exclude(new Rectangle(0, 0, UI.ScaleWidth(16), ClientRectangle.Height));
                clipRegion.Exclude(new Rectangle(0, 0, 16, this.ClientRectangle.Height));
            }

            e.Graphics.SetClip(clipRegion, CombineMode.Replace);
            this.DrawRuler(e, true);

            clipRegion.Xor(this.ClientRectangle);

            if (this.orientation == Orientation.Horizontal)
            {
                //clipRegion.Exclude(new Rectangle(0, 0, UI.ScaleWidth(16), ClientRectangle.Height - 1));
                clipRegion.Exclude(new Rectangle(0, 0, 16, this.ClientRectangle.Height - 1));
            }

            e.Graphics.SetClip(clipRegion, CombineMode.Replace);
            this.DrawRuler(e, false);
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
                    this.SubdivideX(g, pen, x + delta * i / div, delta / div, index + 1, y, height / div + 0.5f, subdivs);
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
                    this.SubdivideY(g, pen, y + delta * i / div, delta / div, index + 1, x, width / div + 0.5f, subdivs);
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

            if (this.orientation == Orientation.Horizontal)
            {
                maxPixel = this.ScaleFactor.UnscaleScalar(this.ClientRectangle.Width);
                textFormat.Alignment = StringAlignment.Near;
                textFormat.LineAlignment = StringAlignment.Far;
            }
            else // if (orientation == Orientation.Vertical)
            {
                // maxPixel holds the exact number of pixels that is available in the client rectangle to display the ruler.
                maxPixel = this.ScaleFactor.UnscaleScalar(this.ClientRectangle.Height);

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
            float majorDivisionLength = (float)this.dpu;
            // If need be, majorDivisionLength can be modified in a dpi aware environment.
            float majorDivisionPixels = (float)this.ScaleFactor.ScaleScalar(majorDivisionLength);
            // Gets the number of subdivisions that are made for a certain measurement unit (for cm this is 2 and 5 and for inch this is 2, all others 0).
            // Still have to figure out how this works exactly...
            int[] subdivs = this.GetSubdivs(this.measurementUnit);
            // Self-explanatory. The amount of pixels for the offset.
            float offsetPixels = this.ScaleFactor.ScaleScalar((float)this.offset);
            // Calculates the indicated start value of the ruler in the physical unit, based on the pixel value...
            int startMajor = (int)(this.offset / majorDivisionLength) - 1;
            // Calculates the indicated end value of the ruler in the physical unit, based on the pixel value...
            int endMajor = (int)((this.offset + maxPixel) / majorDivisionLength) + 1;

            if (this.orientation == Orientation.Horizontal)
            {
                // draw Value
                if (!highlighted)
                {
                    PointF pt = this.scaleFactor.ScalePointJustX(new PointF(this.ClientRectangle.Left + this.Value - this.Offset, this.ClientRectangle.Top));
                    SizeF size = new SizeF(Math.Max(1, this.scaleFactor.ScaleScalar(1.0f)), this.ClientRectangle.Height);

                    pt.X -= 0.5f;

                    CompositingMode oldCM = e.Graphics.CompositingMode;
                    e.Graphics.CompositingMode = CompositingMode.SourceOver;
                    e.Graphics.FillRectangle(cursorBrush, new RectangleF(pt, size));
                    e.Graphics.CompositingMode = oldCM;
                }

                // draw border
                e.Graphics.DrawLine(SystemPens.WindowText, new Point(this.ClientRectangle.Left, this.ClientRectangle.Bottom - 1),
                    new Point(this.ClientRectangle.Right - 1, this.ClientRectangle.Bottom - 1));
            }
            else if (this.orientation == Orientation.Vertical)
            {
                // draw Value
                if (!highlighted)
                {
                    PointF pt = this.scaleFactor.ScalePointJustY(new PointF(this.ClientRectangle.Left, this.ClientRectangle.Top + this.Value - this.Offset));
                    SizeF size = new SizeF(this.ClientRectangle.Width, Math.Max(1, this.scaleFactor.ScaleScalar(1.0f)));

                    pt.Y -= 0.5f;
                    CompositingMode oldCM = e.Graphics.CompositingMode;
                    e.Graphics.CompositingMode = CompositingMode.SourceOver;
                    e.Graphics.FillRectangle(cursorBrush, new RectangleF(pt, size));
                    e.Graphics.CompositingMode = oldCM;
                }

                // draw border
                e.Graphics.DrawLine(SystemPens.WindowText, new Point(this.ClientRectangle.Right - 1, this.ClientRectangle.Top),
                    new Point(this.ClientRectangle.Right - 1, this.ClientRectangle.Bottom - 1));
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

                if (this.orientation == Orientation.Horizontal)
                {
                    this.SubdivideX(e.Graphics, pen, this.ClientRectangle.Left + majorMarkPos, majorDivisionPixels * majorSkip, -majorSkipPower, this.ClientRectangle.Top, this.ClientRectangle.Height, subdivs);
                    e.Graphics.DrawString(majorText, this.Font, textBrush, new PointF(this.ClientRectangle.Left + majorMarkPos, this.ClientRectangle.Bottom), textFormat);
                }
                else // if (orientation == Orientation.Vertical)
                {
                    this.SubdivideY(e.Graphics, pen, this.ClientRectangle.Top + majorMarkPos, majorDivisionPixels * majorSkip, -majorSkipPower, this.ClientRectangle.Left, this.ClientRectangle.Width, subdivs);
                    e.Graphics.DrawString(majorText, this.Font, textBrush, new PointF(this.ClientRectangle.Left, this.ClientRectangle.Top + majorMarkPos), textFormat);
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
                if (this.components != null)
                {
                    this.components.Dispose();
                    this.components = null;
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
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
}
