using System;

namespace DevDefined.Common.Observable
{
  public class PeriodicObserveable<T> : AbstractObservableDecorator<T>
  {
    readonly TimeSpan _period;

    public PeriodicObserveable(IObservable<T> innerObservable, TimeSpan period)
      : base(innerObservable)
    {
      _period = period;
    }

    protected override IObserver<T> DecorateObserver(IObserver<T> observer)
    {
      return new PeriodicObserver<T>(observer, _period);
    }
  }
}