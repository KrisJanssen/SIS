// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOperation.cs" company="">
//   
// </copyright>
// <summary>
//   The Operation interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Pipeline
{
    /// <summary>
    /// The Operation interface.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TContext">
    /// </typeparam>
    public interface IOperation<T, TContext>
    {
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
        T Execute(T input, TContext context);

        #endregion
    }
}