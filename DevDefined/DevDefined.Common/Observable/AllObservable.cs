using System;
using System.Collections.Generic;

namespace DevDefined.Common.Observable
{
  public class AllObservable<T> : IObservable<IEnumerable<T>>
  {
    readonly IObservable<T> _innerObservable;

    public AllObservable(IObservable<T> innerObservable)
    {
      _innerObservable = innerObservable;
    }

    #region IObservable<IEnumerable<T>> Members

    public IDisposable Subscribe(IObserver<IEnumerable<T>> observer)
    {
      return _innerObservable.Subscribe(new AllObserver<T>(observer));
    }

    #endregion
  }
}