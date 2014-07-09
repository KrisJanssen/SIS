// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Filter.cs" company="">
//   
// </copyright>
// <summary>
//   The filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Pipeline
{
    /// <summary>
    /// The filter.
    /// </summary>
    /// <param name="input">
    /// The input.
    /// </param>
    /// <param name="context">
    /// The context.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TContext">
    /// </typeparam>
    public delegate T Filter<T, TContext>(T input, TContext context);
}