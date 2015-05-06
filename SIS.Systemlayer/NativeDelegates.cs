using System;
using System.Runtime.InteropServices;

namespace SIS.SystemLayer
{
    internal static class NativeDelegates
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
    }
}
