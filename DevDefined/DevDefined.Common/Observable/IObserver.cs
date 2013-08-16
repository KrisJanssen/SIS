using System;

namespace DevDefined.Common.Observable
{
  public interface IObserver<T>
  {
    void OnException(Exception ex);
    void OnDone();
    void OnNext(T item);
  }
}