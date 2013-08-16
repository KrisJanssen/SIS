using System;
using System.Collections.Generic;

namespace DevDefined.Common.Observable
{
  public abstract class AbstractObservable<T> : IObservable<T>
  {
    readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

    #region IObservable<T> Members

    public IDisposable Subscribe(IObserver<T> observer)
    {
      _observers.Add(observer);
      return new DisposableAction(() => _observers.Remove(observer));
    }

    #endregion

    protected virtual void OnNext(T item)
    {
      _observers.ForEach(o => o.OnNext(item));
    }

    protected virtual void OnDone()
    {
      _observers.ForEach(o => o.OnDone());
    }

    protected virtual void OnException(Exception exception)
    {
      _observers.ForEach(o => o.OnException(exception));
    }
  }
}