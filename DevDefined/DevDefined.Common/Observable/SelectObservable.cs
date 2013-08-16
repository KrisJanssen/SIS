using System;

namespace DevDefined.Common.Observable
{
  public class SelectObservable<TInput, TOutput> : IObservable<TOutput>
  {
    readonly IObservable<TInput> _innerObservable;
    readonly Func<TInput, TOutput> _selector;

    public SelectObservable(IObservable<TInput> innerObservable, Func<TInput, TOutput> selector)
    {
      if (innerObservable == null) throw new ArgumentNullException("innerObservable");
      if (selector == null) throw new ArgumentNullException("selector");
      _innerObservable = innerObservable;
      _selector = selector;
    }

    #region IObservable<TOutput> Members

    public IDisposable Subscribe(IObserver<TOutput> observer)
    {
      return _innerObservable.Subscribe(new SelectObserver<TInput, TOutput>(observer, _selector));
    }

    #endregion
  }
}