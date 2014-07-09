/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

namespace SIS.Systemlayer
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeDelegates
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);
    }
}
