using System;

namespace DevDefined.Tools.Win32
{
    public delegate int WindowProcDelegate(IntPtr hw, IntPtr uMsg, IntPtr wParam, IntPtr lParam);
}