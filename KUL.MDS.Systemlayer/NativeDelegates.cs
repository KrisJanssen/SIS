// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeDelegates.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native delegates.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native delegates.
    /// </summary>
    internal static class NativeDelegates
    {
        #region Delegates

        /// <summary>
        /// The enum windows proc.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        #endregion
    }
}