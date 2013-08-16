using System;

namespace DevDefined.Common.Observable
{
  public class PeriodicObserver<T> : AbstractObserverDecorator<T>
  {
    readonly TimeSpan _period;
    DateTime? _last;

    public PeriodicObserver(IObserver<T> innerObserver, TimeSpan period) : base(innerObserver)
    {
      _period = period;
    }

    public override void OnNext(T item)
    {
      DateTime now = DateTime.Now;

      if ((_last == null) || now.Subtract(_last.Value) > _period)
      {
        _last = now;
        _innerObserver.OnNext(item);
      }
    }
  }
}