using System;

namespace DevDefined.Common.Observable
{
  public interface IObservable<T>
  {
    IDisposable Subscribe(IObserver<T> observer);
  }
}