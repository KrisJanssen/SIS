using System;

namespace DevDefined.Common.Observable
{
  public abstract class AbstractObserverDecorator<T> : IObserver<T>
  {
    protected readonly IObserver<T> _innerObserver;

    protected AbstractObserverDecorator(IObserver<T> innerObserver)
    {
      if (innerObserver == null) throw new ArgumentNullException("innerObserver");
      _innerObserver = innerObserver;
    }

    #region IObserver<T> Members

    public void OnException(Exception ex)
    {
      _innerObserver.OnException(ex);
    }

    public void OnDone()
    {
      _innerObserver.OnDone();
    }

    public abstract void OnNext(T item);

    #endregion
  }
}