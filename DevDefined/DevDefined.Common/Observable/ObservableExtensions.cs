// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObservableExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   The observable extensions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Observable
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The observable extensions.
    /// </summary>
    public static class ObservableExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The all.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<IEnumerable<T>> All<T>(this IObservable<T> source)
        {
            return new AllObservable<T>(source);
        }

        /// <summary>
        /// The changes only.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> ChangesOnly<T>(this IObservable<T> source) where T : IEquatable<T>
        {
            return new ChangeObservable<T>(source);
        }

        /// <summary>
        /// The concat.
        /// </summary>
        /// <param name="first">
        /// The first.
        /// </param>
        /// <param name="second">
        /// The second.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> Concat<T>(this IObservable<T> first, IObservable<T> second)
        {
            return new ConcatenateObservable<T>(first, second);
        }

        /// <summary>
        /// The last.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> Last<T>(this IObservable<T> source)
        {
            return new LastObservable<T>(source);
        }

        /// <summary>
        /// The limit rate.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="minimumPeriodBetweenItems">
        /// The minimum period between items.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> LimitRate<T>(this IObservable<T> source, TimeSpan minimumPeriodBetweenItems)
        {
            return new PeriodicObserveable<T>(source, minimumPeriodBetweenItems);
        }

        /// <summary>
        /// The select.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="TInput">
        /// </typeparam>
        /// <typeparam name="TOutput">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<TOutput> Select<TInput, TOutput>(
            this IObservable<TInput> source, 
            Func<TInput, TOutput> selector)
        {
            return new SelectObservable<TInput, TOutput>(source, selector);
        }

        /// <summary>
        /// The skip.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="numberToSkip">
        /// The number to skip.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> Skip<T>(this IObservable<T> source, int numberToSkip)
        {
            return new SkipObservable<T>(source, numberToSkip);
        }

        /// <summary>
        /// The subscribe.
        /// </summary>
        /// <param name="observable">
        /// The observable.
        /// </param>
        /// <param name="onNext">
        /// The on next.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext)
        {
            return observable.Subscribe(new ActionObserver<T>(onNext));
        }

        /// <summary>
        /// The take.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> Take<T>(this IObservable<T> source, int max)
        {
            return new TakeObservable<T>(source, max);
        }

        /// <summary>
        /// The where.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="match">
        /// The match.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        public static IObservable<T> Where<T>(this IObservable<T> source, Predicate<T> match)
        {
            return new PredicateObservable<T>(source, match);
        }

        #endregion
    }

    /// <summary>
    /// The observe.
    /// </summary>
    public static class Observe
    {
        #region Public Methods and Operators

        /// <summary>
        /// The event.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="accessor">
        /// The accessor.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TItem">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IObservable"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public static IObservable<TItem> Event<TSource, TItem>(
            TSource source, 
            Expression<Func<TSource, Delegate>> accessor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}