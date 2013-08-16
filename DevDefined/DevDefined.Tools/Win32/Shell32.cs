using System;
using System.Runtime.InteropServices;

namespace DevDefined.Tools.Win32
{
    /// <summary>
    /// API Declarations for the Windows Shell32 library
    /// </summary>
    public class Shell32
    {
        public const int MAX_PATH = 260;

        #region Nested type: structSHFILEINFO

        [StructLayout(LayoutKind.Sequential)]
        protected struct structSHFILEINFO
        {
            /// <summary>
            /// Array of values that indicates the attributes 
            /// of the file object. For information about these 
            /// values, see the IShellFolder::GetAttributesOf method. 
            /// </summary>
            public int dwAttributes;

            /// <summary>
            /// Handle to the icon that represents the file. 
            /// You are responsible for destroying this 
            /// handle with DestroyIcon when you no longer need it. 
            /// </summary>
            public IntPtr hIcon;

            /// <summary>
            /// Index of the icon image within the system image list. 
            /// </summary>
            public Int16 iIcon;

            /// <summary>
            /// String that contains the name of the file 
            /// as it appears in the Microsoft® Windows® Shell, 
            /// or the path and file name of the file that 
            /// contains the icon representing the file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)] public string szDisplayName;

            /// <summary>
            /// String that describes the type of file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
        }

        #endregion
    }
}