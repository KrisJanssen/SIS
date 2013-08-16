using System;

namespace DevDefined.Common.Observable
{
  public class LastObserver<T> : IObserver<T>
  {
    readonly IObserver<T> _innerObserver;
    T _last;
    bool _set;

    public LastObserver(IObserver<T> innerObserver)
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
      if (_set) _innerObserver.OnNext(_last);
      _innerObserver.OnDone();
    }

    public void OnNext(T item)
    {
      _last = item;
      _set = true;
    }

    #endregion
  }
}