// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Function.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The function.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    /// <summary>
    /// The function.
    /// </summary>
    /// <typeparam name="R">
    /// </typeparam>
    public delegate R Function<R>();

    /// <summary>
    /// The function.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <typeparam name="R">
    /// </typeparam>
    /// <typeparam name="T">
    /// </typeparam>
    public delegate R Function<R, T>(T t);

    /// <summary>
    /// The function.
    /// </summary>
    /// <param name="t">
    /// The t.
    /// </param>
    /// <param name="u">
    /// The u.
    /// </param>
    /// <typeparam name="R">
    /// </typeparam>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="U">
    /// </typeparam>
    public delegate R Function<R, T, U>(T t, U u);
}