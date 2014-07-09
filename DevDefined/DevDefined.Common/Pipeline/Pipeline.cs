// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Pipeline.cs" company="">
//   
// </copyright>
// <summary>
//   Implments a pipeline, which can be used to combine on or more filters together in sequence.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Pipeline
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implments a pipeline, which can be used to combine on or more filters together in sequence.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TContext">
    /// </typeparam>
    public class Pipeline<T, TContext> : IOperation<T, TContext>
    {
        #region Fields

        /// <summary>
        /// The _operations.
        /// </summary>
        private readonly List<IOperation<T, TContext>> _operations = new List<IOperation<T, TContext>>();

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
            Filter<T, TContext> composedFilter = null;

            foreach (var operation in this._operations)
            {
                if (composedFilter == null)
                {
                    composedFilter = operation.Execute;
                }
                else
                {
                    IOperation<T, TContext> outerOperation = operation;
                    Filter<T, TContext> oldFilter = composedFilter;
                    composedFilter = (i, c) => outerOperation.Execute(oldFilter(i, c), c);
                }
            }

            return composedFilter(input, context);
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Execute(TContext context)
        {
            this.Execute(default(T), context);
        }

        /// <summary>
        /// The execute.
        /// </summary>
        public void Execute()
        {
            this.Execute(default(TContext));
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public void Register(params Filter<T, TContext>[] filters)
        {
            if (filters != null)
            {
                IEnumerable<IOperation<T, TContext>> wrappedFilters =
                    filters.Select<Filter<T, TContext>, IOperation<T, TContext>>(
                        filter => new GenericOperation<T, TContext>(filter));
                this._operations.AddRange(wrappedFilters);
            }
        }

        /// <summary>
        /// The register.
        /// </summary>
        /// <param name="operations">
        /// The operations.
        /// </param>
        public void Register(params IOperation<T, TContext>[] operations)
        {
            if (operations != null)
            {
                this._operations.AddRange(operations);
            }
        }

        /// <summary>
        /// The set source.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        public void SetSource(T source)
        {
            this._operations.Insert(0, new GenericOperation<T, TContext>((T input) => source));
        }

        #endregion
    }
}