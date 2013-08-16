using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevDefined.Common.Observable
{
  public static class ObservableExtensions
  {
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
    {
      return observable.Subscribe(new ActionObserver<T>(onNext));
    }

    public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> match)
    {
      return new PredicateObservable<T>(source, match);
    }

    public static IObservable<T> Take<T>(this IObservable<T> source, int max)
    {
      return new TakeObservable<T>(source, max);
    }

    public static IObservable<T> Skip<T>(this IObservable<T> source, int numberToSkip)
    {
      return new SkipObservable<T>(source, numberToSkip);
    }

    public static IObservable<T> LimitRate<T>(this IObservable<T> source, TimeSpan minimumPeriodBetweenItems)
    {
      return new PeriodicObserveable<T>(source, minimumPeriodBetweenItems);
    }

    public static IObservable<TOutput> Select<TInput, TOutput>(this IObservable<TInput> source, Func<TInput, TOutput> selector)
    {
      return new SelectObservable<TInput, TOutput>(source, selector);
    }

    public static IObservable<T> Last<T>(this IObservable<T> source)
    {
      return new LastObservable<T>(source);
    }

    public static IObservable<T> ChangesOnly<T>(this IObservable<T> source)
      where T : IEquatable<T>
    {
      return new ChangeObservable<T>(source);
    }

    public static IObservable<IEnumerable<T>> All<T>(this IObservable<T> source)
    {
      return new AllObservable<T>(source);
    }

    public static IObservable<T> Concat<T>(this IObservable<T> first, IObservable<T> second)
    {
      return new ConcatenateObservable<T>(first, second);
    }
  }

  public static class Observe
  {
    public static IObservable<TItem> Event<TSource, TItem>(TSource source, Expression<Func<TSource, Delegate>> accessor)
    {
      throw new NotImplementedException();
    }
  }
}