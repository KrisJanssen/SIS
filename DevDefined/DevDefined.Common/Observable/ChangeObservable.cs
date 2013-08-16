using System;

namespace DevDefined.Common.Observable
{
  public class ChangeObservable<T> : AbstractObservableDecorator<T>
    where T : IEquatable<T>
  {
    public ChangeObservable(IObservable<T> innerObservable) : base(innerObservable)
    {
    }

    protected override IObserver<T> DecorateObserver(IObserver<T> observer)
    {
      return new ChangeObserver<T>(observer);
    }
  }
}