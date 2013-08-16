using System;
using System.Threading;

namespace DevDefined.Common.Observable
{
  public class ObservableTimer : AbstractObservable<TimerElapsed>
  {
    bool _stopped;
    Timer _timer;

    public void Dispose()
    {
      Stop();
    }

    public void Start(int periodInMilliseconds)
    {
      if (_timer != null)
      {
        throw new InvalidOperationException("Observable timer has already been started");
      }
      _timer = new Timer(Elapsed, null, 0, periodInMilliseconds);
    }

    public void Stop()
    {
      if (!_stopped && _timer != null)
      {
        _timer.Dispose();
        OnDone();
        _stopped = true;
      }
    }

    void Elapsed(object state)
    {
      var elapsed = new TimerElapsed(DateTime.Now);
      OnNext(elapsed);
    }
  }
}