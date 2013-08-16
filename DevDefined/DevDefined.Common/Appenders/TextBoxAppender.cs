using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevDefined.Common.Collections;
using log4net.Appender;
using log4net.Core;

namespace DevDefined.Common.Appenders
{
  public class TextBoxAppender : AppenderSkeleton
  {
    readonly RingBuffer<string> _buffer;
    readonly TextBox _textBox;
    readonly BufferedInvoker _bufferedInvoker;

    public TextBoxAppender(TextBox textBox, int bufferSize, double minTimeBetweenUpdatesInMillseconds)
    {
      _buffer = new RingBuffer<string>(bufferSize);
      _bufferedInvoker = new BufferedInvoker(UpdateControl, minTimeBetweenUpdatesInMillseconds);
      _textBox = textBox;
    }

    protected override void Append(LoggingEvent loggingEvent)
    {
      var stringWriter = new StringWriter(CultureInfo.CurrentCulture);
      Layout.Format(stringWriter, loggingEvent);
      string text = stringWriter.ToString();
      if (text.EndsWith("\r\n")) text = text.Substring(0, text.Length - 2);
      _buffer.Add(text);
      _bufferedInvoker.Invoke();
    }

    void UpdateControl()
    {
      if (_textBox != null)
      {
        if (_textBox.InvokeRequired)
        {
          _textBox.Invoke((Action) UpdateControl);
        }
        else
        {
          _textBox.Lines = _buffer.ToArray();
          _textBox.SelectionStart = _textBox.Text.Length;
          _textBox.ScrollToCaret();
          _textBox.Refresh();
        }
      }
    }
  }
}