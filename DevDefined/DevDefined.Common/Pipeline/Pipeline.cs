using System.Collections.Generic;
using System.Linq;

namespace DevDefined.Common.Pipeline
{
    /// <summary>
    /// Implments a pipeline, which can be used to combine on or more filters together in sequence.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class Pipeline<T, TContext> : IOperation<T, TContext>
    {
        private readonly List<IOperation<T, TContext>> _operations = new List<IOperation<T, TContext>>();

        #region IOperation<T,TContext> Members

        public T Execute(T input, TContext context)
        {
            Filter<T, TContext> composedFilter = null;

            foreach (var operation in _operations)
            {
                if (composedFilter == null) composedFilter = operation.Execute;
                else
                {
                    IOperation<T, TContext> outerOperation = operation;
                    Filter<T, TContext> oldFilter = composedFilter;
                    composedFilter = (i, c) => outerOperation.Execute(oldFilter(i, c), c);
                }
            }

            return composedFilter(input, context);
        }

        #endregion

        public void SetSource(T source)
        {
            _operations.Insert(0, new GenericOperation<T, TContext>((T input) => source));
        }

        public void Register(params Filter<T, TContext>[] filters)
        {
            if (filters != null)
            {
                IEnumerable<IOperation<T, TContext>> wrappedFilters =
                    filters.Select<Filter<T, TContext>, IOperation<T, TContext>>(filter => new GenericOperation<T, TContext>(filter));
                _operations.AddRange(wrappedFilters);
            }
        }

        public void Register(params IOperation<T, TContext>[] operations)
        {
            if (operations != null) _operations.AddRange(operations);
        }

        public void Execute(TContext context)
        {
            Execute(default(T), context);
        }

        public void Execute()
        {
            Execute(default(TContext));
        }
    }
}