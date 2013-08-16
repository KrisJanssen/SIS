using System;

namespace DevDefined.Common.Observable
{
  public class TimerElapsed
  {
    public TimerElapsed(DateTime time)
    {
      Time = time;
    }

    public DateTime Time { get; private set; }
  }
}