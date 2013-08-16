using System;

namespace DevDefined.Common.Pipeline
{
    public class GenericOperation<T, TContext> : IOperation<T, TContext>
    {
        private readonly Filter<T, TContext> _filter;

        public GenericOperation(Filter<T, TContext> filter)
        {
            _filter = filter;
        }

        public GenericOperation(Func<T, T> filter)
        {
            _filter = (input, context) => filter(input);
        }

        #region IOperation<T,TContext> Members

        public T Execute(T input, TContext context)
        {
            return _filter(input, context);
        }

        #endregion
    }
}