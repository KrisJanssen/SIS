using System;
using System.Text;
using System.Windows.Forms;
using DevDefined.Common.Collections;
using log4net.Appender;
using log4net.Core;

namespace DevDefined.Common.Appenders
{
  public class ColoredRichTextBoxAppender : AppenderSkeleton
  {
    readonly RingBuffer<ColoredItem> _buffer;
    readonly RichTextBox _textBox;
    readonly BufferedInvoker _bufferedInvoker;

    public ColoredRichTextBoxAppender(RichTextBox textBox, int bufferSize, double minTimeBetweenUpdatesInMillseconds)
    {
      _buffer = new RingBuffer<ColoredItem>(bufferSize);
      _bufferedInvoker = new BufferedInvoker(UpdateControl, minTimeBetweenUpdatesInMillseconds);
      _textBox = textBox;
    }

    protected override void Append(LoggingEvent loggingEvent)
    {
      string text = RenderLoggingEvent(loggingEvent);

      _buffer.Add(new ColoredItem(GetIndexForLevel(loggingEvent.Level), text));

      _bufferedInvoker.Invoke();      
    }

    static int GetIndexForLevel(Level level)
    {
      if (level == Level.Info) return 2;
      if (level == Level.Warn) return 3;
      if (level == Level.Error || level == Level.Fatal) return 4;
      return 1;
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
          StringBuilder builder = BuildRtfDocument();

          _textBox.Rtf = builder.ToString();
          _textBox.SelectionStart = _textBox.Text.Length;
          _textBox.ScrollToCaret();
          _textBox.Refresh();
        }
      }
    }

    StringBuilder BuildRtfDocument()
    {
      var builder = new StringBuilder();

      builder.Append(@"{\rtf1\ansi\deff0
{\colortbl;\red0\green0\blue0;\red0\green0\blue50;\red255\green140\blue0;\red200\green0\blue0;}");

      foreach (ColoredItem line in _buffer) builder.Append(line);

      builder.Append("\r\n}");
      return builder;
    }
  }
}