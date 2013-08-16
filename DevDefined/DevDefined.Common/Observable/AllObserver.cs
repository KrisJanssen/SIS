using System;
using System.Collections.Generic;

namespace DevDefined.Common.Observable
{
  public class AllObserver<T> : IObserver<T>
  {
    readonly IObserver<IEnumerable<T>> _innerObserver;
    readonly List<T> _items = new List<T>();

    public AllObserver(IObserver<IEnumerable<T>> innerObserver)
    {
      _innerObserver = innerObserver;
    }

    #region IObserver<T> Members

    public void OnException(Exception ex)
    {
      _innerObserver.OnException(ex);
    }

    public void OnDone()
    {
      _innerObserver.OnNext(_items);
      _innerObserver.OnDone();
    }

    public void OnNext(T item)
    {
      _items.Add(item);
    }

    #endregion
  }
}