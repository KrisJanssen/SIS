namespace DevDefined.Common.Observable
{
  public class TakeObservable<T> : AbstractObservableDecorator<T>
  {
    readonly int _max;

    public TakeObservable(IObservable<T> innerObservable, int max)
      : base(innerObservable)
    {
      _max = max;
    }

    protected override IObserver<T> DecorateObserver(IObserver<T> observer)
    {
      return new TakeObserver<T>(observer, _max);
    }
  }
}