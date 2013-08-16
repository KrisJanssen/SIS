using System;
using System.Timers;

namespace DevDefined.Common.Appenders
{
  public class BufferedInvoker
  {
    readonly Action _action;
    readonly double _maxRateInMillseconds;
    readonly Timer _timer;
    DateTime? _lastInvocation;

    public BufferedInvoker(Action action, double maxRateInMillseconds)
    {
      _action = action;
      _maxRateInMillseconds = maxRateInMillseconds;
      _timer = new Timer(maxRateInMillseconds) {Enabled = false};
      _timer.Elapsed += TimerElapsed;
    }

    void TimerElapsed(object sender, ElapsedEventArgs e)
    {
      _timer.Enabled = false;
      _lastInvocation = DateTime.Now;
      _action();
    }

    void FireInvoke()
    {
      _lastInvocation = DateTime.Now;
      _timer.Enabled = false;
      _action();
    }

    public void Invoke()
    {
      if (!_timer.Enabled)
      {
        if (_lastInvocation.HasValue)
        {
          if (DateTime.Now.Subtract(_lastInvocation.Value).TotalMilliseconds > _maxRateInMillseconds)
          {
            FireInvoke();
          }
          else
          {
            _timer.Start();
          }
        }
        else
        {
          FireInvoke();
        }
      }
    }
  }
}