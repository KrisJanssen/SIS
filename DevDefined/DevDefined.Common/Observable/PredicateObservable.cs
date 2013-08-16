using System;

namespace DevDefined.Common.Observable
{
  public class PredicateObservable<T> : AbstractObservableDecorator<T>
  {
    readonly Predicate<T> _match;

    public PredicateObservable(IObservable<T> innerObservable, Predicate<T> match)
      : base(innerObservable)
    {
      if (match == null) throw new ArgumentNullException("match");
      _match = match;
    }

    protected override IObserver<T> DecorateObserver(IObserver<T> observer)
    {
      return new PredicateObserver<T>(observer, _match);
    }
  }
}