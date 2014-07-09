// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WndProcDelegate.cs" company="">
//   
// </copyright>
// <summary>
//   The window proc delegate.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Tools.Win32
{
    using System;

    /// <summary>
    /// The window proc delegate.
    /// </summary>
    /// <param name="hw">
    /// The hw.
    /// </param>
    /// <param name="uMsg">
    /// The u msg.
    /// </param>
    /// <param name="wParam">
    /// The w param.
    /// </param>
    /// <param name="lParam">
    /// The l param.
    /// </param>
    public delegate int WindowProcDelegate(IntPtr hw, IntPtr uMsg, IntPtr wParam, IntPtr lParam);
}