// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeStructs.cs" company="">
//   
// </copyright>
// <summary>
//   The native structs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer
{
    using System;
    using System.Runtime.InteropServices;

    using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

    /// <summary>
    /// The native structs.
    /// </summary>
    internal static class NativeStructs
    {
        /// <summary>
        /// The bitmapinfo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BITMAPINFO
        {
            /// <summary>
            /// The bmi header.
            /// </summary>
            internal BITMAPINFOHEADER bmiHeader;

            /// <summary>
            /// The bmi colors.
            /// </summary>
            internal RGBQUAD bmiColors;
        }

        /// <summary>
        /// The bitmapinfoheader.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BITMAPINFOHEADER
        {
            /// <summary>
            /// The bi size.
            /// </summary>
            internal uint biSize;

            /// <summary>
            /// The bi width.
            /// </summary>
            internal int biWidth;

            /// <summary>
            /// The bi height.
            /// </summary>
            internal int biHeight;

            /// <summary>
            /// The bi planes.
            /// </summary>
            internal ushort biPlanes;

            /// <summary>
            /// The bi bit count.
            /// </summary>
            internal ushort biBitCount;

            /// <summary>
            /// The bi compression.
            /// </summary>
            internal uint biCompression;

            /// <summary>
            /// The bi size image.
            /// </summary>
            internal uint biSizeImage;

            /// <summary>
            /// The bi x pels per meter.
            /// </summary>
            internal int biXPelsPerMeter;

            /// <summary>
            /// The bi y pels per meter.
            /// </summary>
            internal int biYPelsPerMeter;

            /// <summary>
            /// The bi clr used.
            /// </summary>
            internal uint biClrUsed;

            /// <summary>
            /// The bi clr important.
            /// </summary>
            internal uint biClrImportant;
        }

        /// <summary>
        /// The comdl g_ filterspec.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        internal struct COMDLG_FILTERSPEC
        {
            /// <summary>
            /// The psz name.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszName;

            /// <summary>
            /// The psz spec.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSpec;
        }

        /// <summary>
        /// The copydatastruct.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct COPYDATASTRUCT
        {
            /// <summary>
            /// The dw data.
            /// </summary>
            internal UIntPtr dwData;

            /// <summary>
            /// The cb data.
            /// </summary>
            internal uint cbData;

            /// <summary>
            /// The lp data.
            /// </summary>
            internal IntPtr lpData;
        }

        /// <summary>
        /// The knownfolde r_ definition.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        internal struct KNOWNFOLDER_DEFINITION
        {
            /// <summary>
            /// The category.
            /// </summary>
            public NativeConstants.KF_CATEGORY category;

            /// <summary>
            /// The psz name.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszName;

            /// <summary>
            /// The psz creator.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCreator;

            /// <summary>
            /// The psz description.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszDescription;

            /// <summary>
            /// The fid parent.
            /// </summary>
            public Guid fidParent;

            /// <summary>
            /// The psz relative path.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszRelativePath;

            /// <summary>
            /// The psz parsing name.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszParsingName;

            /// <summary>
            /// The psz tool tip.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszToolTip;

            /// <summary>
            /// The psz localized name.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszLocalizedName;

            /// <summary>
            /// The psz icon.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszIcon;

            /// <summary>
            /// The psz security.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSecurity;

            /// <summary>
            /// The dw attributes.
            /// </summary>
            public uint dwAttributes;

            /// <summary>
            /// The kfd flags.
            /// </summary>
            public NativeConstants.KF_DEFINITION_FLAGS kfdFlags;

            /// <summary>
            /// The ftid type.
            /// </summary>
            public Guid ftidType;
        }

        /// <summary>
        /// The logbrush.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct LOGBRUSH
        {
            /// <summary>
            /// The lb style.
            /// </summary>
            internal uint lbStyle;

            /// <summary>
            /// The lb color.
            /// </summary>
            internal uint lbColor;

            /// <summary>
            /// The lb hatch.
            /// </summary>
            internal int lbHatch;
        };

        /// <summary>
        /// The memorystatusex.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORYSTATUSEX
        {
            /// <summary>
            /// The dw length.
            /// </summary>
            internal uint dwLength;

            /// <summary>
            /// The dw memory load.
            /// </summary>
            internal uint dwMemoryLoad;

            /// <summary>
            /// The ull total phys.
            /// </summary>
            internal ulong ullTotalPhys;

            /// <summary>
            /// The ull avail phys.
            /// </summary>
            internal ulong ullAvailPhys;

            /// <summary>
            /// The ull total page file.
            /// </summary>
            internal ulong ullTotalPageFile;

            /// <summary>
            /// The ull avail page file.
            /// </summary>
            internal ulong ullAvailPageFile;

            /// <summary>
            /// The ull total virtual.
            /// </summary>
            internal ulong ullTotalVirtual;

            /// <summary>
            /// The ull avail virtual.
            /// </summary>
            internal ulong ullAvailVirtual;

            /// <summary>
            /// The ull avail extended virtual.
            /// </summary>
            internal ulong ullAvailExtendedVirtual;
        }

        /// <summary>
        /// The memor y_ basi c_ information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct MEMORY_BASIC_INFORMATION
        {
            /// <summary>
            /// The base address.
            /// </summary>
            internal void* BaseAddress;

            /// <summary>
            /// The allocation base.
            /// </summary>
            internal void* AllocationBase;

            /// <summary>
            /// The allocation protect.
            /// </summary>
            internal uint AllocationProtect;

            /// <summary>
            /// The region size.
            /// </summary>
            internal UIntPtr RegionSize;

            /// <summary>
            /// The state.
            /// </summary>
            internal uint State;

            /// <summary>
            /// The protect.
            /// </summary>
            internal uint Protect;

            /// <summary>
            /// The type.
            /// </summary>
            internal uint Type;
        };

        /// <summary>
        /// The osversioninfoex.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct OSVERSIONINFOEX
        {
            /// <summary>
            /// Gets the size of.
            /// </summary>
            public static int SizeOf
            {
                get
                {
                    return Marshal.SizeOf(typeof(OSVERSIONINFOEX));
                }
            }

            /// <summary>
            /// The dw os version info size.
            /// </summary>
            public uint dwOSVersionInfoSize;

            /// <summary>
            /// The dw major version.
            /// </summary>
            public uint dwMajorVersion;

            /// <summary>
            /// The dw minor version.
            /// </summary>
            public uint dwMinorVersion;

            /// <summary>
            /// The dw build number.
            /// </summary>
            public uint dwBuildNumber;

            /// <summary>
            /// The dw platform id.
            /// </summary>
            public uint dwPlatformId;

            /// <summary>
            /// The sz csd version.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;

            /// <summary>
            /// The w service pack major.
            /// </summary>
            public ushort wServicePackMajor;

            /// <summary>
            /// The w service pack minor.
            /// </summary>
            public ushort wServicePackMinor;

            /// <summary>
            /// The w suite mask.
            /// </summary>
            public ushort wSuiteMask;

            /// <summary>
            /// The w product type.
            /// </summary>
            public byte wProductType;

            /// <summary>
            /// The w reserved.
            /// </summary>
            public byte wReserved;
        }

        /// <summary>
        /// The overlapped.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct OVERLAPPED
        {
            /// <summary>
            /// The internal.
            /// </summary>
            internal UIntPtr Internal;

            /// <summary>
            /// The internal high.
            /// </summary>
            internal UIntPtr InternalHigh;

            /// <summary>
            /// The offset.
            /// </summary>
            internal uint Offset;

            /// <summary>
            /// The offset high.
            /// </summary>
            internal uint OffsetHigh;

            /// <summary>
            /// The h event.
            /// </summary>
            internal IntPtr hEvent;
        }

        /// <summary>
        /// The point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            /// <summary>
            /// The x.
            /// </summary>
            internal int x;

            /// <summary>
            /// The y.
            /// </summary>
            internal int y;
        }

        /// <summary>
        /// The proces s_ information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            /// <summary>
            /// The h process.
            /// </summary>
            public IntPtr hProcess;

            /// <summary>
            /// The h thread.
            /// </summary>
            public IntPtr hThread;

            /// <summary>
            /// The dw process id.
            /// </summary>
            public uint dwProcessId;

            /// <summary>
            /// The dw thread id.
            /// </summary>
            public uint dwThreadId;
        }

        /// <summary>
        /// The propertykey.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct PROPERTYKEY
        {
            /// <summary>
            /// The fmtid.
            /// </summary>
            public Guid fmtid;

            /// <summary>
            /// The pid.
            /// </summary>
            public uint pid;
        }

        /// <summary>
        /// The property item.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct PropertyItem
        {
            /// <summary>
            /// The id.
            /// </summary>
            internal int id;

            /// <summary>
            /// The length.
            /// </summary>
            internal uint length;

            /// <summary>
            /// The type.
            /// </summary>
            internal short type;

            /// <summary>
            /// The value.
            /// </summary>
            internal void* value;
        }

        /// <summary>
        /// The rect.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            /// <summary>
            /// The left.
            /// </summary>
            internal int left;

            /// <summary>
            /// The top.
            /// </summary>
            internal int top;

            /// <summary>
            /// The right.
            /// </summary>
            internal int right;

            /// <summary>
            /// The bottom.
            /// </summary>
            internal int bottom;
        }

        /// <summary>
        /// The rgbquad.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RGBQUAD
        {
            /// <summary>
            /// The rgb blue.
            /// </summary>
            internal byte rgbBlue;

            /// <summary>
            /// The rgb green.
            /// </summary>
            internal byte rgbGreen;

            /// <summary>
            /// The rgb red.
            /// </summary>
            internal byte rgbRed;

            /// <summary>
            /// The rgb reserved.
            /// </summary>
            internal byte rgbReserved;
        }

        /// <summary>
        /// The rgndata.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RGNDATA
        {
            /// <summary>
            /// The rdh.
            /// </summary>
            internal RGNDATAHEADER rdh;

            /// <summary>
            /// The get rects pointer.
            /// </summary>
            /// <param name="me">
            /// The me.
            /// </param>
            /// <returns>
            /// The <see cref="RECT*"/>.
            /// </returns>
            internal static unsafe RECT* GetRectsPointer(RGNDATA* me)
            {
                return (RECT*)((byte*)me + sizeof(RGNDATAHEADER));
            }
        }

        /// <summary>
        /// The rgndataheader.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RGNDATAHEADER
        {
            /// <summary>
            /// The dw size.
            /// </summary>
            internal uint dwSize;

            /// <summary>
            /// The i type.
            /// </summary>
            internal uint iType;

            /// <summary>
            /// The n count.
            /// </summary>
            internal uint nCount;

            /// <summary>
            /// The n rgn size.
            /// </summary>
            internal uint nRgnSize;

            /// <summary>
            /// The rc bound.
            /// </summary>
            internal RECT rcBound;
        };

        /// <summary>
        /// The shellexecuteinfo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SHELLEXECUTEINFO
        {
            /// <summary>
            /// The cb size.
            /// </summary>
            internal uint cbSize;

            /// <summary>
            /// The f mask.
            /// </summary>
            internal uint fMask;

            /// <summary>
            /// The hwnd.
            /// </summary>
            internal IntPtr hwnd;

            /// <summary>
            /// The lp verb.
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            internal string lpVerb;

            /// <summary>
            /// The lp file.
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            internal string lpFile;

            /// <summary>
            /// The lp parameters.
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            internal string lpParameters;

            /// <summary>
            /// The lp directory.
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            internal string lpDirectory;

            /// <summary>
            /// The n show.
            /// </summary>
            internal int nShow;

            /// <summary>
            /// The h inst app.
            /// </summary>
            internal IntPtr hInstApp;

            /// <summary>
            /// The lp id list.
            /// </summary>
            internal IntPtr lpIDList;

            /// <summary>
            /// The lp class.
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
            internal string lpClass;

            /// <summary>
            /// The hkey class.
            /// </summary>
            internal IntPtr hkeyClass;

            /// <summary>
            /// The dw hot key.
            /// </summary>
            internal uint dwHotKey;

            /// <summary>
            /// The h icon_or_h monitor.
            /// </summary>
            internal IntPtr hIcon_or_hMonitor;

            /// <summary>
            /// The h process.
            /// </summary>
            internal IntPtr hProcess;
        }

        /// <summary>
        /// The s p_ devinf o_ data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SP_DEVINFO_DATA
        {
            /// <summary>
            /// The cb size.
            /// </summary>
            public uint cbSize;

            /// <summary>
            /// The class guid.
            /// </summary>
            public Guid ClassGuid;

            /// <summary>
            /// The dev inst.
            /// </summary>
            public uint DevInst;

            /// <summary>
            /// The reserved.
            /// </summary>
            public UIntPtr Reserved;
        }

        /// <summary>
        /// The statstg.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct STATSTG
        {
            /// <summary>
            /// The pwcs name.
            /// </summary>
            public IntPtr pwcsName;

            /// <summary>
            /// The type.
            /// </summary>
            public NativeConstants.STGTY type;

            /// <summary>
            /// The cb size.
            /// </summary>
            public ulong cbSize;

            /// <summary>
            /// The mtime.
            /// </summary>
            public FILETIME mtime;

            /// <summary>
            /// The ctime.
            /// </summary>
            public FILETIME ctime;

            /// <summary>
            /// The atime.
            /// </summary>
            public FILETIME atime;

            /// <summary>
            /// The grf mode.
            /// </summary>
            public uint grfMode;

            /// <summary>
            /// The grf locks supported.
            /// </summary>
            public uint grfLocksSupported;

            /// <summary>
            /// The clsid.
            /// </summary>
            public Guid clsid;

            /// <summary>
            /// The grf state bits.
            /// </summary>
            public uint grfStateBits;

            /// <summary>
            /// The reserved.
            /// </summary>
            public uint reserved;
        }

        /// <summary>
        /// The syste m_ info.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            /// <summary>
            /// The w processor architecture.
            /// </summary>
            public ushort wProcessorArchitecture;

            /// <summary>
            /// The w reserved.
            /// </summary>
            public ushort wReserved;

            /// <summary>
            /// The dw page size.
            /// </summary>
            public uint dwPageSize;

            /// <summary>
            /// The lp minimum application address.
            /// </summary>
            public IntPtr lpMinimumApplicationAddress;

            /// <summary>
            /// The lp maximum application address.
            /// </summary>
            public IntPtr lpMaximumApplicationAddress;

            /// <summary>
            /// The dw active processor mask.
            /// </summary>
            public UIntPtr dwActiveProcessorMask;

            /// <summary>
            /// The dw number of processors.
            /// </summary>
            public uint dwNumberOfProcessors;

            /// <summary>
            /// The dw processor type.
            /// </summary>
            public uint dwProcessorType;

            /// <summary>
            /// The dw allocation granularity.
            /// </summary>
            public uint dwAllocationGranularity;

            /// <summary>
            /// The w processor level.
            /// </summary>
            public ushort wProcessorLevel;

            /// <summary>
            /// The w processor revision.
            /// </summary>
            public ushort wProcessorRevision;
        }

        /// <summary>
        /// The winhtt p_ curren t_ use r_ i e_ prox y_ config.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct WINHTTP_CURRENT_USER_IE_PROXY_CONFIG
        {
            /// <summary>
            /// The f auto detect.
            /// </summary>
            internal bool fAutoDetect;

            /// <summary>
            /// The lpsz auto config url.
            /// </summary>
            internal IntPtr lpszAutoConfigUrl;

            /// <summary>
            /// The lpsz proxy.
            /// </summary>
            internal IntPtr lpszProxy;

            /// <summary>
            /// The lpsz proxy bypass.
            /// </summary>
            internal IntPtr lpszProxyBypass;
        };

        /// <summary>
        /// The wintrus t_ data.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct WINTRUST_DATA
        {
            /// <summary>
            /// The cb struct.
            /// </summary>
            internal uint cbStruct;

            /// <summary>
            /// The p policy callback data.
            /// </summary>
            internal IntPtr pPolicyCallbackData;

            /// <summary>
            /// The p sip client data.
            /// </summary>
            internal IntPtr pSIPClientData;

            /// <summary>
            /// The dw ui choice.
            /// </summary>
            internal uint dwUIChoice;

            /// <summary>
            /// The fdw revocation checks.
            /// </summary>
            internal uint fdwRevocationChecks;

            /// <summary>
            /// The dw union choice.
            /// </summary>
            internal uint dwUnionChoice;

            /// <summary>
            /// The p info.
            /// </summary>
            internal void* pInfo; // pFile, pCatalog, pBlob, pSgnr, or pCert

            /// <summary>
            /// The dw state action.
            /// </summary>
            internal uint dwStateAction;

            /// <summary>
            /// The h wvt state data.
            /// </summary>
            internal IntPtr hWVTStateData;

            /// <summary>
            /// The pwsz url reference.
            /// </summary>
            internal IntPtr pwszURLReference;

            /// <summary>
            /// The dw prov flags.
            /// </summary>
            internal uint dwProvFlags;

            /// <summary>
            /// The dw ui context.
            /// </summary>
            internal uint dwUIContext;
        }

        /// <summary>
        /// The wintrus t_ fil e_ info.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal unsafe struct WINTRUST_FILE_INFO
        {
            /// <summary>
            /// The cb struct.
            /// </summary>
            internal uint cbStruct;

            /// <summary>
            /// The pcwsz file path.
            /// </summary>
            internal char* pcwszFilePath;

            /// <summary>
            /// The h file.
            /// </summary>
            internal IntPtr hFile;
        }

        /// <summary>
        /// The logfont.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal class LOGFONT
        {
            /// <summary>
            /// The lf height.
            /// </summary>
            internal int lfHeight = 0;

            /// <summary>
            /// The lf width.
            /// </summary>
            internal int lfWidth = 0;

            /// <summary>
            /// The lf escapement.
            /// </summary>
            internal int lfEscapement = 0;

            /// <summary>
            /// The lf orientation.
            /// </summary>
            internal int lfOrientation = 0;

            /// <summary>
            /// The lf weight.
            /// </summary>
            internal int lfWeight = 0;

            /// <summary>
            /// The lf italic.
            /// </summary>
            internal byte lfItalic = 0;

            /// <summary>
            /// The lf underline.
            /// </summary>
            internal byte lfUnderline = 0;

            /// <summary>
            /// The lf strike out.
            /// </summary>
            internal byte lfStrikeOut = 0;

            /// <summary>
            /// The lf char set.
            /// </summary>
            internal byte lfCharSet = 0;

            /// <summary>
            /// The lf out precision.
            /// </summary>
            internal byte lfOutPrecision = 0;

            /// <summary>
            /// The lf clip precision.
            /// </summary>
            internal byte lfClipPrecision = 0;

            /// <summary>
            /// The lf quality.
            /// </summary>
            internal byte lfQuality = 0;

            /// <summary>
            /// The lf pitch and family.
            /// </summary>
            internal byte lfPitchAndFamily = 0;

            /// <summary>
            /// The lf face name.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            internal string lfFaceName = string.Empty;
        }
    }
}