using System;

namespace DevDefined.Common.Observable
{
  public class ActionObserver<T> : IObserver<T>
  {
    readonly Action _onDone;
    readonly Action<Exception> _onException;
    readonly Action<T> _onNext;

    public ActionObserver(Action<T> onNext)
      : this(onNext, DefaultExceptionHandler, DefaultDoneHandler)
    {
    }

    public ActionObserver(Action<T> onNext, Action<Exception> onException, Action onDone)
    {
      if (onNext == null) throw new ArgumentNullException("onNext");
      if (onException == null) throw new ArgumentNullException("onException");
      if (onDone == null) throw new ArgumentNullException("onDone");
      _onNext = onNext;
      _onException = onException;
      _onDone = onDone;
    }

    #region IObserver<T> Members

    public void OnException(Exception ex)
    {
      _onException(ex);
    }

    public void OnDone()
    {
      _onDone();
    }

    public void OnNext(T item)
    {
      _onNext(item);
    }

    #endregion

    static void DefaultDoneHandler()
    {
    }

    static void DefaultExceptionHandler(Exception ex)
    {
      throw new Exception("Exception was thrown by Observable", ex);
    }
  }
}