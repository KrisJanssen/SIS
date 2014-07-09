// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Procedure.cs" company="">
//   
// </copyright>
// <summary>
//   The procedure.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    /// <summary>
    /// The procedure.
    /// </summary>
    public delegate void Procedure();

    /// <summary>
    /// The procedure.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public delegate void Procedure<T>(T t);

    /// <summary>
    /// The procedure.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <param name="u">
    /// The u.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="U">
    /// </typeparam>
    public delegate void Procedure<T, U>(T t, U u);

    /// <summary>
    /// The procedure.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <param name="u">
    /// The u.
    /// </param>
    /// <param name="v">
    /// The v.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="U">
    /// </typeparam>
    /// <typeparam name="V">
    /// </typeparam>
    public delegate void Procedure<T, U, V>(T t, U u, V v);

    /// <summary>
    /// The procedure.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <param name="u">
    /// The u.
    /// </param>
    /// <param name="v">
    /// The v.
    /// </param>
    /// <param name="w">
    /// The w.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="U">
    /// </typeparam>
    /// <typeparam name="V">
    /// </typeparam>
    /// <typeparam name="W">
    /// </typeparam>
    public delegate void Procedure<T, U, V, W>(T t, U u, V v, W w);
}