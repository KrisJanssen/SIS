using System;

namespace DevDefined.Common.Observable
{
  public abstract class AbstractObservableDecorator<T> : IObservable<T>
  {
    protected readonly IObservable<T> _innerObservable;

    protected AbstractObservableDecorator(IObservable<T> innerObservable)
    {
      if (innerObservable == null) throw new ArgumentNullException("innerObservable");
      _innerObservable = innerObservable;
    }

    #region IObservable<T> Members

    public virtual IDisposable Subscribe(IObserver<T> observer)
    {
      return _innerObservable.Subscribe(DecorateObserver(observer));
    }

    #endregion

    protected abstract IObserver<T> DecorateObserver(IObserver<T> observer);
  }
}