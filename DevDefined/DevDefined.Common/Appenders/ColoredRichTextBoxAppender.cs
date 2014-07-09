// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredRichTextBoxAppender.cs" company="">
//   
// </copyright>
// <summary>
//   The colored rich text box appender.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Appenders
{
    using System;
    using System.Text;
    using System.Windows.Forms;

    using DevDefined.Common.Collections;

    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    /// The colored rich text box appender.
    /// </summary>
    public class ColoredRichTextBoxAppender : AppenderSkeleton
    {
        #region Fields

        /// <summary>
        /// The _buffer.
        /// </summary>
        private readonly RingBuffer<ColoredItem> _buffer;

        /// <summary>
        /// The _buffered invoker.
        /// </summary>
        private readonly BufferedInvoker _bufferedInvoker;

        /// <summary>
        /// The _text box.
        /// </summary>
        private readonly RichTextBox _textBox;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ColoredRichTextBoxAppender"/> class.
        /// </summary>
        /// <param name="textBox">
        /// The text box.
        /// </param>
        /// <param name="bufferSize">
        /// The buffer size.
        /// </param>
        /// <param name="minTimeBetweenUpdatesInMillseconds">
        /// The min time between updates in millseconds.
        /// </param>
        public ColoredRichTextBoxAppender(
            RichTextBox textBox, 
            int bufferSize, 
            double minTimeBetweenUpdatesInMillseconds)
        {
            this._buffer = new RingBuffer<ColoredItem>(bufferSize);
            this._bufferedInvoker = new BufferedInvoker(this.UpdateControl, minTimeBetweenUpdatesInMillseconds);
            this._textBox = textBox;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="loggingEvent">
        /// The logging event.
        /// </param>
        protected override void Append(LoggingEvent loggingEvent)
        {
            string text = this.RenderLoggingEvent(loggingEvent);

            this._buffer.Add(new ColoredItem(GetIndexForLevel(loggingEvent.Level), text));

            this._bufferedInvoker.Invoke();
        }

        /// <summary>
        /// The get index for level.
        /// </summary>
        /// <param name="level">
        /// The level.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int GetIndexForLevel(Level level)
        {
            if (level == Level.Info)
            {
                return 2;
            }

            if (level == Level.Warn)
            {
                return 3;
            }

            if (level == Level.Error || level == Level.Fatal)
            {
                return 4;
            }

            return 1;
        }

        /// <summary>
        /// The build rtf document.
        /// </summary>
        /// <returns>
        /// The <see cref="StringBuilder"/>.
        /// </returns>
        private StringBuilder BuildRtfDocument()
        {
            var builder = new StringBuilder();

            builder.Append(@"{\rtf1\ansi\deff0
{\colortbl;\red0\green0\blue0;\red0\green0\blue50;\red255\green140\blue0;\red200\green0\blue0;}");

            foreach (ColoredItem line in this._buffer)
            {
                builder.Append(line);
            }

            builder.Append("\r\n}");
            return builder;
        }

        /// <summary>
        /// The update control.
        /// </summary>
        private void UpdateControl()
        {
            if (this._textBox != null)
            {
                if (this._textBox.InvokeRequired)
                {
                    this._textBox.Invoke((Action)this.UpdateControl);
                }
                else
                {
                    StringBuilder builder = this.BuildRtfDocument();

                    this._textBox.Rtf = builder.ToString();
                    this._textBox.SelectionStart = this._textBox.Text.Length;
                    this._textBox.ScrollToCaret();
                    this._textBox.Refresh();
                }
            }
        }

        #endregion
    }
}