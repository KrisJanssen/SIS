// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StylusAsyncPlugin.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The stylus async plugin.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System.Drawing;
    using System.Windows.Forms;

    using Microsoft.StylusInput;
    using Microsoft.StylusInput.PluginData;

    /// <summary>
    /// The stylus async plugin.
    /// </summary>
    internal sealed class StylusAsyncPlugin : IStylusAsyncPlugin
    {
        #region Fields

        /// <summary>
        /// The attached control.
        /// </summary>
        private Control attachedControl;

        /// <summary>
        /// The lastbutton.
        /// </summary>
        private MouseButtons lastbutton = MouseButtons.None;

        /// <summary>
        /// The ratio.
        /// </summary>
        private PointF ratio = PointF.Empty;

        /// <summary>
        /// The subject.
        /// </summary>
        private IStylusReaderHooks subject;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StylusAsyncPlugin"/> class.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="attached">
        /// The attached.
        /// </param>
        internal StylusAsyncPlugin(IStylusReaderHooks subject, Control attached)
        {
            Graphics g = subject.CreateGraphics();
            this.attachedControl = attached;
            this.ratio = new PointF(g.DpiX / 2540.0f, g.DpiY / 2540.0f);
            this.subject = subject;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the data interest.
        /// </summary>
        public DataInterestMask DataInterest
        {
            get
            {
                return DataInterestMask.AllStylusData;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The custom stylus data added.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void CustomStylusDataAdded(RealTimeStylus sender, CustomStylusData data)
        {
        }

        /// <summary>
        /// The error.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void Error(RealTimeStylus sender, ErrorData data)
        {
        }

        /// <summary>
        /// The in air packets.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void InAirPackets(RealTimeStylus sender, InAirPacketsData data)
        {
            for (int i = 0; i < data.Count; i += data.PacketPropertyCount)
            {
                this.Interpret(data, i);
            }
        }

        /// <summary>
        /// The packets.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void Packets(RealTimeStylus sender, PacketsData data)
        {
            for (int i = 0; i < data.Count; i += data.PacketPropertyCount)
            {
                this.Interpret(data, i);
            }
        }

        /// <summary>
        /// The real time stylus disabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void RealTimeStylusDisabled(RealTimeStylus sender, RealTimeStylusDisabledData data)
        {
        }

        /// <summary>
        /// The real time stylus enabled.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void RealTimeStylusEnabled(RealTimeStylus sender, RealTimeStylusEnabledData data)
        {
        }

        /// <summary>
        /// The stylus button down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusButtonDown(RealTimeStylus sender, StylusButtonDownData data)
        {
        }

        /// <summary>
        /// The stylus button up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusButtonUp(RealTimeStylus sender, StylusButtonUpData data)
        {
        }

        /// <summary>
        /// The stylus down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusDown(RealTimeStylus sender, StylusDownData data)
        {
            this.Interpret(data, 0);
        }

        /// <summary>
        /// The stylus in range.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusInRange(RealTimeStylus sender, StylusInRangeData data)
        {
        }

        /// <summary>
        /// The stylus out of range.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusOutOfRange(RealTimeStylus sender, StylusOutOfRangeData data)
        {
        }

        /// <summary>
        /// The stylus up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void StylusUp(RealTimeStylus sender, StylusUpData data)
        {
            this.Interpret(data, 0);
        }

        /// <summary>
        /// The system gesture.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void SystemGesture(RealTimeStylus sender, SystemGestureData data)
        {
        }

        /// <summary>
        /// The tablet added.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void TabletAdded(RealTimeStylus sender, TabletAddedData data)
        {
        }

        /// <summary>
        /// The tablet removed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public void TabletRemoved(RealTimeStylus sender, TabletRemovedData data)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The himetric to point f.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        private PointF HimetricToPointF(int x, int y)
        {
            return new PointF(x * this.ratio.X, y * this.ratio.Y);
        }

        /// <summary>
        /// The interpret.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        private void Interpret(StylusDataBase data, int index)
        {
            Point offset = this.attachedControl.PointToScreen(new Point(0, 0));
            PointF relativePosition = this.HimetricToPointF(data[index], data[index + 1]);
            PointF position =
                this.subject.ScreenToDocument(new PointF(relativePosition.X + offset.X, relativePosition.Y + offset.Y));
            float pressure = (data.PacketPropertyCount > 3 ? data[index + 2] : 255.0f) / 255.0f;
            int status = data[index + data.PacketPropertyCount - 1];
            MouseButtons button = this.StatusToMouseButtons(status);

            bool didMouseMove = false;

            if (this.lastbutton != button)
            {
                // if a button was previously down, MouseUp it.
                if (this.lastbutton != MouseButtons.None)
                {
                    this.subject.PerformDocumentMouseMove(button, 1, position.X, position.Y, 0, pressure);
                    this.subject.PerformDocumentMouseUp(this.lastbutton, 1, position.X, position.Y, 0, pressure);
                    didMouseMove = true;
                }

                // if a new button was pushed, MouseDown it.
                if (button != MouseButtons.None)
                {
                    this.subject.PerformDocumentMouseDown(button, 1, position.X, position.Y, 0, pressure);
                    this.subject.PerformDocumentMouseMove(button, 1, position.X, position.Y, 0, pressure);
                    didMouseMove = true;
                }
            }

            if (!didMouseMove)
            {
                // regardless of the button states, send a new MouseMove
                this.subject.PerformDocumentMouseMove(button, 1, position.X, position.Y, 0, pressure);
            }

            this.lastbutton = button;
        }

        /// <summary>
        /// The status to mouse buttons.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <returns>
        /// The <see cref="MouseButtons"/>.
        /// </returns>
        private MouseButtons StatusToMouseButtons(int status)
        {
            if ((status & 0x1) != 0)
            {
                if ((status & 0x8) != 0)
                {
                    return MouseButtons.Right;
                }
                else if ((status & 0x2) != 0)
                {
                    return MouseButtons.Middle;
                }
                else
                {
                    return MouseButtons.Left;
                }
            }
            else
            {
                return MouseButtons.None;
            }
        }

        #endregion
    }
}