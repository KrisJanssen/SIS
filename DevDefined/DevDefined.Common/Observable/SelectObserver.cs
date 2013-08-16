using System;

namespace DevDefined.Common.Observable
{
  public class SelectObserver<TInput, TOutput> : IObserver<TInput>
  {
    readonly IObserver<TOutput> _innerObserver;
    readonly Func<TInput, TOutput> _selector;

    public SelectObserver(IObserver<TOutput> innerObserver, Func<TInput, TOutput> selector)
    {
      _innerObserver = innerObserver;
      _selector = selector;
    }

    #region IObserver<TInput> Members

    public void OnException(Exception ex)
    {
      _innerObserver.OnException(ex);
    }

    public void OnDone()
    {
      _innerObserver.OnDone();
    }

    public void OnNext(TInput item)
    {
      _innerObserver.OnNext(_selector(item));
    }

    #endregion
  }
}