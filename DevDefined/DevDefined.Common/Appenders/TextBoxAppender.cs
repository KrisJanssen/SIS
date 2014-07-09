// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxAppender.cs" company="">
//   
// </copyright>
// <summary>
//   The text box appender.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Appenders
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using DevDefined.Common.Collections;

    using log4net.Appender;
    using log4net.Core;

    /// <summary>
    /// The text box appender.
    /// </summary>
    public class TextBoxAppender : AppenderSkeleton
    {
        #region Fields

        /// <summary>
        /// The _buffer.
        /// </summary>
        private readonly RingBuffer<string> _buffer;

        /// <summary>
        /// The _buffered invoker.
        /// </summary>
        private readonly BufferedInvoker _bufferedInvoker;

        /// <summary>
        /// The _text box.
        /// </summary>
        private readonly TextBox _textBox;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxAppender"/> class.
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
        public TextBoxAppender(TextBox textBox, int bufferSize, double minTimeBetweenUpdatesInMillseconds)
        {
            this._buffer = new RingBuffer<string>(bufferSize);
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
            var stringWriter = new StringWriter(CultureInfo.CurrentCulture);
            this.Layout.Format(stringWriter, loggingEvent);
            string text = stringWriter.ToString();
            if (text.EndsWith("\r\n"))
            {
                text = text.Substring(0, text.Length - 2);
            }

            this._buffer.Add(text);
            this._bufferedInvoker.Invoke();
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
                    this._textBox.Lines = this._buffer.ToArray();
                    this._textBox.SelectionStart = this._textBox.Text.Length;
                    this._textBox.ScrollToCaret();
                    this._textBox.Refresh();
                }
            }
        }

        #endregion
    }
}