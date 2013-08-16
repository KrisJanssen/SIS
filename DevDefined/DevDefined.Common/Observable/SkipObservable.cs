namespace DevDefined.Common.Observable
{
  public class SkipObservable<T> : AbstractObservableDecorator<T>
  {
    readonly int _numberToSkip;

    public SkipObservable(IObservable<T> innerObservable, int numberToSkip) : base(innerObservable)
    {
      _numberToSkip = numberToSkip;
    }

    protected override IObserver<T> DecorateObserver(IObserver<T> observer)
    {
      return new SkipObserver<T>(observer, _numberToSkip);
    }
  }
}