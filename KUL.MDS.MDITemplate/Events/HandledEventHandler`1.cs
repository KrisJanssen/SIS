// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandledEventHandler`1.cs" company="">
//   
// </copyright>
// <summary>
//   The handled event handler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.MDITemplate.Events
{
    /// <summary>
    /// The handled event handler.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    public delegate void HandledEventHandler<T>(object sender, HandledEventArgs<T> e);
}