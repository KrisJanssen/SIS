namespace DevDefined.Common.Pipeline
{
    public delegate T Filter<T, TContext>(T input, TContext context);
}