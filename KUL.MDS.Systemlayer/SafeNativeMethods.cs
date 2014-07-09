// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SafeNativeMethods.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The safe native methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// The safe native methods.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        /// <summary>
        /// The is processor feature present.
        /// </summary>
        /// <param name="ProcessorFeature">
        /// The processor feature.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsProcessorFeaturePresent(uint ProcessorFeature);

        /// <summary>
        /// The draw menu bar.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DrawMenuBar(IntPtr hWnd);

        /// <summary>
        /// The get system menu.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="bRevert">
        /// The b revert.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        /// <summary>
        /// The enable menu item.
        /// </summary>
        /// <param name="hMenu">
        /// The h menu.
        /// </param>
        /// <param name="uIDEnableItem">
        /// The u id enable item.
        /// </param>
        /// <param name="uEnable">
        /// The u enable.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        internal static extern int EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        /// <summary>
        /// The flash window.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="bInvert">
        /// The b invert.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FlashWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bInvert);

        /// <summary>
        /// The dwm get window attribute.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="dwAttribute">
        /// The dw attribute.
        /// </param>
        /// <param name="pvAttribute">
        /// The pv attribute.
        /// </param>
        /// <param name="cbAttribute">
        /// The cb attribute.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("dwmapi.dll")]
        internal static extern unsafe int DwmGetWindowAttribute(
            IntPtr hwnd, 
            uint dwAttribute, 
            void* pvAttribute, 
            uint cbAttribute);

        /// <summary>
        /// The get current thread.
        /// </summary>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = false)]
        internal static extern IntPtr GetCurrentThread();

        /// <summary>
        /// The set thread priority.
        /// </summary>
        /// <param name="hThread">
        /// The h thread.
        /// </param>
        /// <param name="nPriority">
        /// The n priority.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetThreadPriority(IntPtr hThread, int nPriority);

        /// <summary>
        /// The create file mapping w.
        /// </summary>
        /// <param name="hFile">
        /// The h file.
        /// </param>
        /// <param name="lpFileMappingAttributes">
        /// The lp file mapping attributes.
        /// </param>
        /// <param name="flProtect">
        /// The fl protect.
        /// </param>
        /// <param name="dwMaximumSizeHigh">
        /// The dw maximum size high.
        /// </param>
        /// <param name="dwMaximumSizeLow">
        /// The dw maximum size low.
        /// </param>
        /// <param name="lpName">
        /// The lp name.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFileMappingW(
            IntPtr hFile, 
            IntPtr lpFileMappingAttributes, 
            uint flProtect, 
            uint dwMaximumSizeHigh, 
            uint dwMaximumSizeLow, 
            [MarshalAs(UnmanagedType.LPTStr)] string lpName);

        /// <summary>
        /// The map view of file.
        /// </summary>
        /// <param name="hFileMappingObject">
        /// The h file mapping object.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="dwFileOffsetHigh">
        /// The dw file offset high.
        /// </param>
        /// <param name="dwFileOffsetLow">
        /// The dw file offset low.
        /// </param>
        /// <param name="dwNumberOfBytesToMap">
        /// The dw number of bytes to map.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject, 
            uint dwDesiredAccess, 
            uint dwFileOffsetHigh, 
            uint dwFileOffsetLow, 
            UIntPtr dwNumberOfBytesToMap);

        /// <summary>
        /// The unmap view of file.
        /// </summary>
        /// <param name="lpBaseAddress">
        /// The lp base address.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        /// <summary>
        /// The show scroll bar.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="wBar">
        /// The w bar.
        /// </param>
        /// <param name="bShow">
        /// The b show.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowScrollBar(IntPtr hWnd, int wBar, [MarshalAs(UnmanagedType.Bool)] bool bShow);

        /// <summary>
        /// The get version ex.
        /// </summary>
        /// <param name="lpVersionInfo">
        /// The lp version info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetVersionEx(ref NativeStructs.OSVERSIONINFOEX lpVersionInfo);

        /// <summary>
        /// The get layered window attributes.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="pcrKey">
        /// The pcr key.
        /// </param>
        /// <param name="pbAlpha">
        /// The pb alpha.
        /// </param>
        /// <param name="pdwFlags">
        /// The pdw flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetLayeredWindowAttributes(
            IntPtr hwnd, 
            out uint pcrKey, 
            out byte pbAlpha, 
            out uint pdwFlags);

        /// <summary>
        /// The set layered window attributes.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="crKey">
        /// The cr key.
        /// </param>
        /// <param name="bAlpha">
        /// The b alpha.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", 
            "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "2")]
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        /// <summary>
        /// The create font w.
        /// </summary>
        /// <param name="nHeight">
        /// The n height.
        /// </param>
        /// <param name="nWidth">
        /// The n width.
        /// </param>
        /// <param name="nEscapement">
        /// The n escapement.
        /// </param>
        /// <param name="nOrientation">
        /// The n orientation.
        /// </param>
        /// <param name="fnWeight">
        /// The fn weight.
        /// </param>
        /// <param name="fdwItalic">
        /// The fdw italic.
        /// </param>
        /// <param name="fdwUnderline">
        /// The fdw underline.
        /// </param>
        /// <param name="fdwStrikeOut">
        /// The fdw strike out.
        /// </param>
        /// <param name="fdwCharSet">
        /// The fdw char set.
        /// </param>
        /// <param name="fdwOutputPrecision">
        /// The fdw output precision.
        /// </param>
        /// <param name="fdwClipPrecision">
        /// The fdw clip precision.
        /// </param>
        /// <param name="fdwQuality">
        /// The fdw quality.
        /// </param>
        /// <param name="fdwPitchAndFamily">
        /// The fdw pitch and family.
        /// </param>
        /// <param name="lpszFace">
        /// The lpsz face.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFontW(
            int nHeight, 
            int nWidth, 
            int nEscapement, 
            int nOrientation, 
            int fnWeight, 
            uint fdwItalic, 
            uint fdwUnderline, 
            uint fdwStrikeOut, 
            uint fdwCharSet, 
            uint fdwOutputPrecision, 
            uint fdwClipPrecision, 
            uint fdwQuality, 
            uint fdwPitchAndFamily, 
            string lpszFace);

        /// <summary>
        /// The draw text w.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <param name="lpString">
        /// The lp string.
        /// </param>
        /// <param name="nCount">
        /// The n count.
        /// </param>
        /// <param name="lpRect">
        /// The lp rect.
        /// </param>
        /// <param name="uFormat">
        /// The u format.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int DrawTextW(
            IntPtr hdc, 
            string lpString, 
            int nCount, 
            ref NativeStructs.RECT lpRect, 
            uint uFormat);

        /// <summary>
        /// The create dib section.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <param name="pbmi">
        /// The pbmi.
        /// </param>
        /// <param name="iUsage">
        /// The i usage.
        /// </param>
        /// <param name="ppvBits">
        /// The ppv bits.
        /// </param>
        /// <param name="hSection">
        /// The h section.
        /// </param>
        /// <param name="dwOffset">
        /// The dw offset.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("gdi32.dll", SetLastError = true)]
        internal static extern IntPtr CreateDIBSection(
            IntPtr hdc, 
            ref NativeStructs.BITMAPINFO pbmi, 
            uint iUsage, 
            out IntPtr ppvBits, 
            IntPtr hSection, 
            uint dwOffset);

        /// <summary>
        /// The create file w.
        /// </summary>
        /// <param name="lpFileName">
        /// The lp file name.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="dwShareMode">
        /// The dw share mode.
        /// </param>
        /// <param name="lpSecurityAttributes">
        /// The lp security attributes.
        /// </param>
        /// <param name="dwCreationDisposition">
        /// The dw creation disposition.
        /// </param>
        /// <param name="dwFlagsAndAttributes">
        /// The dw flags and attributes.
        /// </param>
        /// <param name="hTemplateFile">
        /// The h template file.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CreateFileW(
            string lpFileName, 
            uint dwDesiredAccess, 
            uint dwShareMode, 
            IntPtr lpSecurityAttributes, 
            uint dwCreationDisposition, 
            uint dwFlagsAndAttributes, 
            IntPtr hTemplateFile);

        /// <summary>
        /// The write file.
        /// </summary>
        /// <param name="hFile">
        /// The h file.
        /// </param>
        /// <param name="lpBuffer">
        /// The lp buffer.
        /// </param>
        /// <param name="nNumberOfBytesToWrite">
        /// The n number of bytes to write.
        /// </param>
        /// <param name="lpNumberOfBytesWritten">
        /// The lp number of bytes written.
        /// </param>
        /// <param name="lpOverlapped">
        /// The lp overlapped.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool WriteFile(
            IntPtr hFile, 
            void* lpBuffer, 
            uint nNumberOfBytesToWrite, 
            out uint lpNumberOfBytesWritten, 
            IntPtr lpOverlapped);

        /// <summary>
        /// The read file.
        /// </summary>
        /// <param name="sfhFile">
        /// The sfh file.
        /// </param>
        /// <param name="lpBuffer">
        /// The lp buffer.
        /// </param>
        /// <param name="nNumberOfBytesToRead">
        /// The n number of bytes to read.
        /// </param>
        /// <param name="lpNumberOfBytesRead">
        /// The lp number of bytes read.
        /// </param>
        /// <param name="lpOverlapped">
        /// The lp overlapped.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern unsafe bool ReadFile(
            SafeFileHandle sfhFile, 
            void* lpBuffer, 
            uint nNumberOfBytesToRead, 
            out uint lpNumberOfBytesRead, 
            IntPtr lpOverlapped);

        /// <summary>
        /// The close handle.
        /// </summary>
        /// <param name="hObject">
        /// The h object.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// The set handle information.
        /// </summary>
        /// <param name="hObject">
        /// The h object.
        /// </param>
        /// <param name="dwMask">
        /// The dw mask.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetHandleInformation(IntPtr hObject, uint dwMask, uint dwFlags);

        /// <summary>
        /// The get update rgn.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="hRgn">
        /// The h rgn.
        /// </param>
        /// <param name="bErase">
        /// The b erase.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        internal static extern int GetUpdateRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bErase);

        /// <summary>
        /// The get window thread process id.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="lpdwProcessId">
        /// The lpdw process id.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// The find window w.
        /// </summary>
        /// <param name="lpClassName">
        /// The lp class name.
        /// </param>
        /// <param name="lpWindowName">
        /// The lp window name.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindWindowW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, 
            [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName);

        /// <summary>
        /// The find window ex w.
        /// </summary>
        /// <param name="hwndParent">
        /// The hwnd parent.
        /// </param>
        /// <param name="hwndChildAfter">
        /// The hwnd child after.
        /// </param>
        /// <param name="lpszClass">
        /// The lpsz class.
        /// </param>
        /// <param name="lpszWindow">
        /// The lpsz window.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr FindWindowExW(
            IntPtr hwndParent, 
            IntPtr hwndChildAfter, 
            [MarshalAs(UnmanagedType.LPWStr)] string lpszClass, 
            [MarshalAs(UnmanagedType.LPWStr)] string lpszWindow);

        /// <summary>
        /// The send message w.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="wParam">
        /// The w param.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        internal static extern IntPtr SendMessageW(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The post message w.
        /// </summary>
        /// <param name="handle">
        /// The handle.
        /// </param>
        /// <param name="msg">
        /// The msg.
        /// </param>
        /// <param name="wParam">
        /// The w param.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PostMessageW(IntPtr handle, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The get window long w.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="nIndex">
        /// The n index.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint GetWindowLongW(IntPtr hWnd, int nIndex);

        /// <summary>
        /// The set window long w.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="nIndex">
        /// The n index.
        /// </param>
        /// <param name="dwNewLong">
        /// The dw new long.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern uint SetWindowLongW(IntPtr hWnd, int nIndex, uint dwNewLong);

        /// <summary>
        /// The query performance counter.
        /// </summary>
        /// <param name="lpPerformanceCount">
        /// The lp performance count.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryPerformanceCounter(out ulong lpPerformanceCount);

        /// <summary>
        /// The query performance frequency.
        /// </summary>
        /// <param name="lpFrequency">
        /// The lp frequency.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool QueryPerformanceFrequency(out ulong lpFrequency);

        /// <summary>
        /// The memcpy.
        /// </summary>
        /// <param name="dst">
        /// The dst.
        /// </param>
        /// <param name="src">
        /// The src.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memcpy(void* dst, void* src, UIntPtr length);

        /// <summary>
        /// The memset.
        /// </summary>
        /// <param name="dst">
        /// The dst.
        /// </param>
        /// <param name="c">
        /// The c.
        /// </param>
        /// <param name="length">
        /// The length.
        /// </param>
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        internal static extern unsafe void memset(void* dst, int c, UIntPtr length);

        /// <summary>
        /// The get system metrics.
        /// </summary>
        /// <param name="nIndex">
        /// The n index.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("User32.dll", SetLastError = false)]
        internal static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// The wait for single object.
        /// </summary>
        /// <param name="hHandle">
        /// The h handle.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The dw milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

        /// <summary>
        /// The wait for multiple objects.
        /// </summary>
        /// <param name="nCount">
        /// The n count.
        /// </param>
        /// <param name="lpHandles">
        /// The lp handles.
        /// </param>
        /// <param name="bWaitAll">
        /// The b wait all.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The dw milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint WaitForMultipleObjects(
            uint nCount, 
            IntPtr[] lpHandles, 
            [MarshalAs(UnmanagedType.Bool)] bool bWaitAll, 
            uint dwMilliseconds);

        /// <summary>
        /// The wait for multiple objects.
        /// </summary>
        /// <param name="lpHandles">
        /// The lp handles.
        /// </param>
        /// <param name="bWaitAll">
        /// The b wait all.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The dw milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        internal static uint WaitForMultipleObjects(IntPtr[] lpHandles, bool bWaitAll, uint dwMilliseconds)
        {
            return WaitForMultipleObjects((uint)lpHandles.Length, lpHandles, bWaitAll, dwMilliseconds);
        }

        /// <summary>
        /// The wts register session notification.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern uint WTSRegisterSessionNotification(IntPtr hWnd, uint dwFlags);

        /// <summary>
        /// The wts un register session notification.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("wtsapi32.dll", SetLastError = true)]
        internal static extern uint WTSUnRegisterSessionNotification(IntPtr hWnd);

        /// <summary>
        /// The get region data.
        /// </summary>
        /// <param name="hRgn">
        /// The h rgn.
        /// </param>
        /// <param name="dwCount">
        /// The dw count.
        /// </param>
        /// <param name="lpRgnData">
        /// The lp rgn data.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern unsafe uint GetRegionData(IntPtr hRgn, uint dwCount, NativeStructs.RGNDATA* lpRgnData);

        /// <summary>
        /// The create rect rgn.
        /// </summary>
        /// <param name="nLeftRect">
        /// The n left rect.
        /// </param>
        /// <param name="nTopRect">
        /// The n top rect.
        /// </param>
        /// <param name="nRightRect">
        /// The n right rect.
        /// </param>
        /// <param name="nBottomRect">
        /// The n bottom rect.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern unsafe IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        /// <summary>
        /// The move to ex.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <param name="X">
        /// The x.
        /// </param>
        /// <param name="Y">
        /// The y.
        /// </param>
        /// <param name="lpPoint">
        /// The lp point.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MoveToEx(IntPtr hdc, int X, int Y, out NativeStructs.POINT lpPoint);

        /// <summary>
        /// The line to.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <param name="nXEnd">
        /// The n x end.
        /// </param>
        /// <param name="nYEnd">
        /// The n y end.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);

        /// <summary>
        /// The fill rect.
        /// </summary>
        /// <param name="hDC">
        /// The h dc.
        /// </param>
        /// <param name="lprc">
        /// The lprc.
        /// </param>
        /// <param name="hbr">
        /// The hbr.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("User32.dll", SetLastError = true)]
        internal static extern int FillRect(IntPtr hDC, ref NativeStructs.RECT lprc, IntPtr hbr);

        /// <summary>
        /// The create pen.
        /// </summary>
        /// <param name="fnPenStyle">
        /// The fn pen style.
        /// </param>
        /// <param name="nWidth">
        /// The n width.
        /// </param>
        /// <param name="crColor">
        /// The cr color.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern IntPtr CreatePen(int fnPenStyle, int nWidth, uint crColor);

        /// <summary>
        /// The create solid brush.
        /// </summary>
        /// <param name="crColor">
        /// The cr color.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern IntPtr CreateSolidBrush(uint crColor);

        /// <summary>
        /// The select object.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <param name="hgdiobj">
        /// The hgdiobj.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = false)]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        /// The delete object.
        /// </summary>
        /// <param name="hObject">
        /// The h object.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// The delete dc.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("Gdi32.dll", SetLastError = true)]
        internal static extern uint DeleteDC(IntPtr hdc);

        /// <summary>
        /// The create compatible dc.
        /// </summary>
        /// <param name="hdc">
        /// The hdc.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Gdi32.Dll", SetLastError = true)]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// The bit blt.
        /// </summary>
        /// <param name="hdcDest">
        /// The hdc dest.
        /// </param>
        /// <param name="nXDest">
        /// The n x dest.
        /// </param>
        /// <param name="nYDest">
        /// The n y dest.
        /// </param>
        /// <param name="nWidth">
        /// The n width.
        /// </param>
        /// <param name="nHeight">
        /// The n height.
        /// </param>
        /// <param name="hdcSrc">
        /// The hdc src.
        /// </param>
        /// <param name="nXSrc">
        /// The n x src.
        /// </param>
        /// <param name="nYSrc">
        /// The n y src.
        /// </param>
        /// <param name="dwRop">
        /// The dw rop.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("Gdi32.Dll", SetLastError = true)]
        internal static extern uint BitBlt(
            IntPtr hdcDest, 
            int nXDest, 
            int nYDest, 
            int nWidth, 
            int nHeight, 
            IntPtr hdcSrc, 
            int nXSrc, 
            int nYSrc, 
            uint dwRop);

        /// <summary>
        /// The virtual alloc.
        /// </summary>
        /// <param name="lpAddress">
        /// The lp address.
        /// </param>
        /// <param name="dwSize">
        /// The dw size.
        /// </param>
        /// <param name="flAllocationType">
        /// The fl allocation type.
        /// </param>
        /// <param name="flProtect">
        /// The fl protect.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr VirtualAlloc(
            IntPtr lpAddress, 
            UIntPtr dwSize, 
            uint flAllocationType, 
            uint flProtect);

        /// <summary>
        /// The virtual free.
        /// </summary>
        /// <param name="lpAddress">
        /// The lp address.
        /// </param>
        /// <param name="dwSize">
        /// The dw size.
        /// </param>
        /// <param name="dwFreeType">
        /// The dw free type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VirtualFree(IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        /// <summary>
        /// The virtual protect.
        /// </summary>
        /// <param name="lpAddress">
        /// The lp address.
        /// </param>
        /// <param name="dwSize">
        /// The dw size.
        /// </param>
        /// <param name="flNewProtect">
        /// The fl new protect.
        /// </param>
        /// <param name="lpflOldProtect">
        /// The lpfl old protect.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VirtualProtect(
            IntPtr lpAddress, 
            UIntPtr dwSize, 
            uint flNewProtect, 
            out uint lpflOldProtect);

        /// <summary>
        /// The heap alloc.
        /// </summary>
        /// <param name="hHeap">
        /// The h heap.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <param name="dwBytes">
        /// The dw bytes.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = false)]
        internal static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, UIntPtr dwBytes);

        /// <summary>
        /// The heap free.
        /// </summary>
        /// <param name="hHeap">
        /// The h heap.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <param name="lpMem">
        /// The lp mem.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        /// <summary>
        /// The heap size.
        /// </summary>
        /// <param name="hHeap">
        /// The h heap.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <param name="lpMem">
        /// The lp mem.
        /// </param>
        /// <returns>
        /// The <see cref="UIntPtr"/>.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = false)]
        internal static extern UIntPtr HeapSize(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        /// <summary>
        /// The heap create.
        /// </summary>
        /// <param name="flOptions">
        /// The fl options.
        /// </param>
        /// <param name="dwInitialSize">
        /// The dw initial size.
        /// </param>
        /// <param name="dwMaximumSize">
        /// The dw maximum size.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern IntPtr HeapCreate(
            uint flOptions, 
            [MarshalAs(UnmanagedType.SysUInt)] IntPtr dwInitialSize, 
            [MarshalAs(UnmanagedType.SysUInt)] IntPtr dwMaximumSize);

        /// <summary>
        /// The heap destroy.
        /// </summary>
        /// <param name="hHeap">
        /// The h heap.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("Kernel32.dll", SetLastError = true)]
        internal static extern uint HeapDestroy(IntPtr hHeap);

        /// <summary>
        /// The heap set information.
        /// </summary>
        /// <param name="HeapHandle">
        /// The heap handle.
        /// </param>
        /// <param name="HeapInformationClass">
        /// The heap information class.
        /// </param>
        /// <param name="HeapInformation">
        /// The heap information.
        /// </param>
        /// <param name="HeapInformationLength">
        /// The heap information length.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("Kernel32.Dll", SetLastError = true)]
        internal static extern unsafe uint HeapSetInformation(
            IntPtr HeapHandle, 
            int HeapInformationClass, 
            void* HeapInformation, 
            uint HeapInformationLength);

        /// <summary>
        /// The win http get ie proxy config for current user.
        /// </summary>
        /// <param name="pProxyConfig">
        /// The p proxy config.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("winhttp.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool WinHttpGetIEProxyConfigForCurrentUser(
            ref NativeStructs.WINHTTP_CURRENT_USER_IE_PROXY_CONFIG pProxyConfig);

        /// <summary>
        /// The global free.
        /// </summary>
        /// <param name="hMem">
        /// The h mem.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GlobalFree(IntPtr hMem);

        /// <summary>
        /// The set foreground window.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The is iconic.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// The show window.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="nCmdShow">
        /// The n cmd show.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}