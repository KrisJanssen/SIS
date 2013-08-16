namespace DevDefined.Common.Pipeline
{
    public interface IOperation<T, TContext>
    {
        T Execute(T input, TContext context);
    }
}