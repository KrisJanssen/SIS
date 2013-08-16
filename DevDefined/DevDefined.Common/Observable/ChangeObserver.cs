using System;

namespace DevDefined.Common.Observable
{
  public class ChangeObserver<T> : AbstractObserverDecorator<T>
    where T : IEquatable<T>
  {
    bool _hasValue;
    T _last;

    public ChangeObserver(IObserver<T> innerObserver) : base(innerObserver)
    {
    }

    public override void OnNext(T item)
    {
      if (_hasValue && _last.Equals(item)) return;
      _last = item;
      _hasValue = true;
      _innerObserver.OnNext(item);
    }
  }
}