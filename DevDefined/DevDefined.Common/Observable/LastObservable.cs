using System;

namespace DevDefined.Common.Observable
{
  public class LastObservable<T> : IObservable<T>
  {
    readonly IObservable<T> _innerObservable;

    public LastObservable(IObservable<T> innerObservable)
    {
      if (innerObservable == null) throw new ArgumentNullException("innerObservable");
      _innerObservable = innerObservable;
    }

    #region IObservable<T> Members

    public IDisposable Subscribe(IObserver<T> observer)
    {
      return _innerObservable.Subscribe(new LastObserver<T>(observer));
    }

    #endregion
  }
}