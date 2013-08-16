namespace DevDefined.Common.Observable
{
  public class TakeObserver<T> : AbstractObserverDecorator<T>
  {
    readonly int _max;
    int _count;

    public TakeObserver(IObserver<T> innerObserver, int max) : base(innerObserver)
    {
      _max = max;
    }

    public override void OnNext(T item)
    {
      if (_count < _max)
      {
        _innerObserver.OnNext(item);
        _count++;
      }
    }
  }
}