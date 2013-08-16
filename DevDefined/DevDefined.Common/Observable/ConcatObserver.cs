using System;

namespace DevDefined.Common.Observable
{
  public class ConcatenateObservable<T> : IObservable<T>
  {
    readonly IObservable<T> _first;
    readonly IObservable<T> _second;

    public ConcatenateObservable(IObservable<T> first, IObservable<T> second)
    {
      _first = first;
      _second = second;
    }

    #region IObservable<T> Members

    public IDisposable Subscribe(IObserver<T> observer)
    {
      IDisposable firstDisposable = _first.Subscribe(observer);
      IDisposable secondDisposable = _second.Subscribe(observer);
      return new DisposableAction(() =>
        {
          firstDisposable.Dispose();
          secondDisposable.Dispose();
        });
    }

    #endregion
  }
}