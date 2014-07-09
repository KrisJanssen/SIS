// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// The succeeded.
        /// </summary>
        /// <param name="hr">
        /// The hr.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal static bool SUCCEEDED(int hr)
        {
            return hr >= 0;
        }

        /// <summary>
        /// The sh get folder path w.
        /// </summary>
        /// <param name="hwndOwner">
        /// The hwnd owner.
        /// </param>
        /// <param name="nFolder">
        /// The n folder.
        /// </param>
        /// <param name="hToken">
        /// The h token.
        /// </param>
        /// <param name="dwFlags">
        /// The dw flags.
        /// </param>
        /// <param name="lpszPath">
        /// The lpsz path.
        /// </param>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void SHGetFolderPathW(
            IntPtr hwndOwner, 
            int nFolder, 
            IntPtr hToken, 
            uint dwFlags, 
            StringBuilder lpszPath);

        /// <summary>
        /// The delete file w.
        /// </summary>
        /// <param name="lpFileName">
        /// The lp file name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFileW([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        /// <summary>
        /// The remove directory w.
        /// </summary>
        /// <param name="lpPathName">
        /// The lp path name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RemoveDirectoryW([MarshalAs(UnmanagedType.LPWStr)] string lpPathName);

        /// <summary>
        /// The wait for input idle.
        /// </summary>
        /// <param name="hProcess">
        /// The h process.
        /// </param>
        /// <param name="dwMilliseconds">
        /// The dw milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint WaitForInputIdle(IntPtr hProcess, uint dwMilliseconds);

        /// <summary>
        /// The enum windows.
        /// </summary>
        /// <param name="lpEnumFunc">
        /// The lp enum func.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(
            [MarshalAs(UnmanagedType.FunctionPtr)] NativeDelegates.EnumWindowsProc lpEnumFunc, 
            IntPtr lParam);

        /// <summary>
        /// The open process.
        /// </summary>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="bInheritHandle">
        /// The b inherit handle.
        /// </param>
        /// <param name="dwProcessId">
        /// The dw process id.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            uint dwDesiredAccess, 
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, 
            uint dwProcessId);

        /// <summary>
        /// The open process token.
        /// </summary>
        /// <param name="ProcessHandle">
        /// The process handle.
        /// </param>
        /// <param name="DesiredAccess">
        /// The desired access.
        /// </param>
        /// <param name="TokenHandle">
        /// The token handle.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        /// <summary>
        /// The duplicate token ex.
        /// </summary>
        /// <param name="hExistingToken">
        /// The h existing token.
        /// </param>
        /// <param name="dwDesiredAccess">
        /// The dw desired access.
        /// </param>
        /// <param name="lpTokenAttributes">
        /// The lp token attributes.
        /// </param>
        /// <param name="ImpersonationLevel">
        /// The impersonation level.
        /// </param>
        /// <param name="TokenType">
        /// The token type.
        /// </param>
        /// <param name="phNewToken">
        /// The ph new token.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateTokenEx(
            IntPtr hExistingToken, 
            uint dwDesiredAccess, 
            IntPtr lpTokenAttributes, 
            NativeConstants.SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, 
            NativeConstants.TOKEN_TYPE TokenType, 
            out IntPtr phNewToken);

        /// <summary>
        /// The create process with token w.
        /// </summary>
        /// <param name="hToken">
        /// The h token.
        /// </param>
        /// <param name="dwLogonFlags">
        /// The dw logon flags.
        /// </param>
        /// <param name="lpApplicationName">
        /// The lp application name.
        /// </param>
        /// <param name="lpCommandLine">
        /// The lp command line.
        /// </param>
        /// <param name="dwCreationFlags">
        /// The dw creation flags.
        /// </param>
        /// <param name="lpEnvironment">
        /// The lp environment.
        /// </param>
        /// <param name="lpCurrentDirectory">
        /// The lp current directory.
        /// </param>
        /// <param name="lpStartupInfo">
        /// The lp startup info.
        /// </param>
        /// <param name="lpProcessInfo">
        /// The lp process info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateProcessWithTokenW(
            IntPtr hToken, 
            uint dwLogonFlags, 
            IntPtr lpApplicationName, 
            IntPtr lpCommandLine, 
            uint dwCreationFlags, 
            IntPtr lpEnvironment, 
            IntPtr lpCurrentDirectory, 
            IntPtr lpStartupInfo, 
            out NativeStructs.PROCESS_INFORMATION lpProcessInfo);

        /// <summary>
        /// The open clipboard.
        /// </summary>
        /// <param name="hWndNewOwner">
        /// The h wnd new owner.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        /// <summary>
        /// The close clipboard.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseClipboard();

        /// <summary>
        /// The get clipboard data.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetClipboardData(uint format);

        /// <summary>
        /// The is clipboard format available.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsClipboardFormatAvailable(uint format);

        /// <summary>
        /// The sh create item from parsing name.
        /// </summary>
        /// <param name="pszPath">
        /// The psz path.
        /// </param>
        /// <param name="pbc">
        /// The pbc.
        /// </param>
        /// <param name="riid">
        /// The riid.
        /// </param>
        /// <param name="ppv">
        /// The ppv.
        /// </param>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void SHCreateItemFromParsingName(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPath, 
            IntPtr pbc, 
            ref Guid riid, 
            out IntPtr ppv);

        /// <summary>
        /// The verify version info.
        /// </summary>
        /// <param name="lpVersionInfo">
        /// The lp version info.
        /// </param>
        /// <param name="dwTypeMask">
        /// The dw type mask.
        /// </param>
        /// <param name="dwlConditionMask">
        /// The dwl condition mask.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VerifyVersionInfo(
            ref NativeStructs.OSVERSIONINFOEX lpVersionInfo, 
            uint dwTypeMask, 
            ulong dwlConditionMask);

        /// <summary>
        /// The ver set condition mask.
        /// </summary>
        /// <param name="dwlConditionMask">
        /// The dwl condition mask.
        /// </param>
        /// <param name="dwTypeBitMask">
        /// The dw type bit mask.
        /// </param>
        /// <param name="dwConditionMask">
        /// The dw condition mask.
        /// </param>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        [DllImport("kernel32.dll")]
        internal static extern ulong VerSetConditionMask(
            ulong dwlConditionMask, 
            uint dwTypeBitMask, 
            byte dwConditionMask);

        /// <summary>
        /// The device io control.
        /// </summary>
        /// <param name="hDevice">
        /// The h device.
        /// </param>
        /// <param name="dwIoControlCode">
        /// The dw io control code.
        /// </param>
        /// <param name="lpInBuffer">
        /// The lp in buffer.
        /// </param>
        /// <param name="nInBufferSize">
        /// The n in buffer size.
        /// </param>
        /// <param name="lpOutBuffer">
        /// The lp out buffer.
        /// </param>
        /// <param name="nOutBufferSize">
        /// The n out buffer size.
        /// </param>
        /// <param name="lpBytesReturned">
        /// The lp bytes returned.
        /// </param>
        /// <param name="lpOverlapped">
        /// The lp overlapped.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(
            IntPtr hDevice, 
            uint dwIoControlCode, 
            IntPtr lpInBuffer, 
            uint nInBufferSize, 
            IntPtr lpOutBuffer, 
            uint nOutBufferSize, 
            ref uint lpBytesReturned, 
            IntPtr lpOverlapped);

        /// <summary>
        /// The shell execute ex w.
        /// </summary>
        /// <param name="lpExecInfo">
        /// The lp exec info.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShellExecuteExW(ref NativeStructs.SHELLEXECUTEINFO lpExecInfo);

        /// <summary>
        /// The global memory status ex.
        /// </summary>
        /// <param name="lpBuffer">
        /// The lp buffer.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GlobalMemoryStatusEx(ref NativeStructs.MEMORYSTATUSEX lpBuffer);

        /// <summary>
        /// The sh add to recent docs.
        /// </summary>
        /// <param name="uFlags">
        /// The u flags.
        /// </param>
        /// <param name="pv">
        /// The pv.
        /// </param>
        [DllImport("shell32.dll", SetLastError = false)]
        internal static extern void SHAddToRecentDocs(uint uFlags, IntPtr pv);

        /// <summary>
        /// The get system info.
        /// </summary>
        /// <param name="lpSystemInfo">
        /// The lp system info.
        /// </param>
        [DllImport("kernel32.dll", SetLastError = false)]
        internal static extern void GetSystemInfo(ref NativeStructs.SYSTEM_INFO lpSystemInfo);

        /// <summary>
        /// The get native system info.
        /// </summary>
        /// <param name="lpSystemInfo">
        /// The lp system info.
        /// </param>
        [DllImport("kernel32.dll", SetLastError = false)]
        internal static extern void GetNativeSystemInfo(ref NativeStructs.SYSTEM_INFO lpSystemInfo);

        /// <summary>
        /// The win verify trust.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <param name="pgActionID">
        /// The pg action id.
        /// </param>
        /// <param name="pWinTrustData">
        /// The p win trust data.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("Wintrust.dll", PreserveSig = true, SetLastError = false)]
        internal static extern unsafe int WinVerifyTrust(
            IntPtr hWnd, 
            ref Guid pgActionID, 
            ref NativeStructs.WINTRUST_DATA pWinTrustData);

        /// <summary>
        /// The setup di get class devs w.
        /// </summary>
        /// <param name="ClassGuid">
        /// The class guid.
        /// </param>
        /// <param name="Enumerator">
        /// The enumerator.
        /// </param>
        /// <param name="hwndParent">
        /// The hwnd parent.
        /// </param>
        /// <param name="Flags">
        /// The flags.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr SetupDiGetClassDevsW(
            ref Guid ClassGuid, 
            [MarshalAs(UnmanagedType.LPWStr)] string Enumerator, 
            IntPtr hwndParent, 
            uint Flags);

        /// <summary>
        /// The setup di destroy device info list.
        /// </summary>
        /// <param name="DeviceInfoSet">
        /// The device info set.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        /// <summary>
        /// The setup di enum device info.
        /// </summary>
        /// <param name="DeviceInfoSet">
        /// The device info set.
        /// </param>
        /// <param name="MemberIndex">
        /// The member index.
        /// </param>
        /// <param name="DeviceInfoData">
        /// The device info data.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet, 
            uint MemberIndex, 
            ref NativeStructs.SP_DEVINFO_DATA DeviceInfoData);

        /// <summary>
        /// The setup di get device instance id w.
        /// </summary>
        /// <param name="DeviceInfoSet">
        /// The device info set.
        /// </param>
        /// <param name="DeviceInfoData">
        /// The device info data.
        /// </param>
        /// <param name="DeviceInstanceId">
        /// The device instance id.
        /// </param>
        /// <param name="DeviceInstanceIdSize">
        /// The device instance id size.
        /// </param>
        /// <param name="RequiredSize">
        /// The required size.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("SetupApi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceInstanceIdW(
            IntPtr DeviceInfoSet, 
            ref NativeStructs.SP_DEVINFO_DATA DeviceInfoData, 
            IntPtr DeviceInstanceId, 
            uint DeviceInstanceIdSize, 
            out uint RequiredSize);

        /// <summary>
        /// The throw on win 32 error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal static void ThrowOnWin32Error(string message)
        {
            int lastWin32Error = Marshal.GetLastWin32Error();
            ThrowOnWin32Error(message, lastWin32Error);
        }

        /// <summary>
        /// The throw on win 32 error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="lastWin32Error">
        /// The last win 32 error.
        /// </param>
        internal static void ThrowOnWin32Error(string message, NativeErrors lastWin32Error)
        {
            ThrowOnWin32Error(message, (int)lastWin32Error);
        }

        /// <summary>
        /// The throw on win 32 error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="lastWin32Error">
        /// The last win 32 error.
        /// </param>
        /// <exception cref="Win32Exception">
        /// </exception>
        internal static void ThrowOnWin32Error(string message, int lastWin32Error)
        {
            if (lastWin32Error != NativeConstants.ERROR_SUCCESS)
            {
                string exMessageFormat = "{0} ({1}, {2})";
                string exMessage = string.Format(
                    exMessageFormat, 
                    message, 
                    lastWin32Error, 
                    ((NativeErrors)lastWin32Error).ToString());

                throw new Win32Exception(lastWin32Error, exMessage);
            }
        }
    }
}