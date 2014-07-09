// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RealParentWndProcDelegate.cs" company="">
//   
// </copyright>
// <summary>
//   The real parent wnd proc delegate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer
{
    using System.Windows.Forms;

    /// <summary>
    /// The real parent wnd proc delegate.
    /// </summary>
    /// <param name="m">
    /// The m.
    /// </param>
    public delegate void RealParentWndProcDelegate(ref Message m);
}