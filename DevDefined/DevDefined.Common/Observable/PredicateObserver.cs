using System;

namespace DevDefined.Common.Observable
{
  public class PredicateObserver<T> : AbstractObserverDecorator<T>
  {
    readonly Predicate<T> _match;

    public PredicateObserver(IObserver<T> innerObserver, Predicate<T> match)
      : base(innerObserver)
    {
      if (match == null) throw new ArgumentNullException("match");
      _match = match;
    }

    public override void OnNext(T item)
    {
      if (_match(item)) _innerObserver.OnNext(item);
    }
  }
}