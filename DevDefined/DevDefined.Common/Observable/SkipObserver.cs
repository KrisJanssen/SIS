namespace DevDefined.Common.Observable
{
  public class SkipObserver<T> : AbstractObserverDecorator<T>
  {
    readonly int _numberToSkip;
    int _skippedSoFar;

    public SkipObserver(IObserver<T> innerObserver, int numberToSkip) : base(innerObserver)
    {
      _numberToSkip = numberToSkip;
    }


    public override void OnNext(T item)
    {
      if (_skippedSoFar < _numberToSkip)
      {
        _skippedSoFar++;
      }
      else
      {
        _innerObserver.OnNext(item);
      }
    }
  }
}