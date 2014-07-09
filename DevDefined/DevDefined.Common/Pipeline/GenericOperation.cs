// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericOperation.cs" company="">
//   
// </copyright>
// <summary>
//   The generic operation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Pipeline
{
    using System;

    /// <summary>
    /// The generic operation.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TContext">
    /// </typeparam>
    public class GenericOperation<T, TContext> : IOperation<T, TContext>
    {
        #region Fields

        /// <summary>
        /// The _filter.
        /// </summary>
        private readonly Filter<T, TContext> _filter;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericOperation{T,TContext}"/> class.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        public GenericOperation(Filter<T, TContext> filter)
        {
            this._filter = filter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericOperation{T,TContext}"/> class.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        public GenericOperation(Func<T, T> filter)
        {
            this._filter = (input, context) => filter(input);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Execute(T input, TContext context)
        {
            return this._filter(input, context);
        }

        #endregion
    }
}