using System;
using System.Collections.Generic;
using DevDefined.Common.Extensions;

namespace DevDefined.Common.Observable
{
  public class ObservableSink<T> : AbstractObservable<T>, IDisposable
  {
    #region IDisposable Members

    public void Dispose()
    {
      OnDone();
    }

    #endregion

    public ObservableSink<T> Pump(IEnumerable<T> source)
    {
      source.ForEach(OnNext);
      return this;
    }

    public ObservableSink<T> Pump(params T[] source)
    {
      source.ForEach(OnNext);
      return this;
    }

    public ObservableSink<T> Pump(T item)
    {
      OnNext(item);
      return this;
    }
  }
}