/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SIS.SystemLayer
{
    /// <summary>
    /// Provides a way to manage and communicate between instances of an application
    /// in the same user session.
    /// </summary>
    public sealed class SingleInstanceManager
        : IDisposable
    {
        private const int m_iMappingSize = 8; // sizeof(int64)
        private string m_sMappingName;
        private Form m_frmWindow = null;
        private IntPtr m_iptrHWnd = IntPtr.Zero;
        private IntPtr m_iptrhFileMapping;
        private List<string> m_lPendingInstanceMessages = new List<string>();
        private bool m_bIsFirstInstance;

        public bool IsFirstInstance
        {
            get
            {
                return this.m_bIsFirstInstance;
            }
        }

        public bool AreMessagesPending
        {
            get
            {
                lock (this.m_lPendingInstanceMessages)
                {
                    return (this.m_lPendingInstanceMessages.Count > 0);
                }
            }
        }

        public void SetWindow(Form __frmNewWindow)
        {
            if (this.m_frmWindow != null)
            {
                UnregisterWindow();
            }

            RegisterWindow(__frmNewWindow);
        }

        private void UnregisterWindow()
        {
            if (this.m_frmWindow != null)
            {
                this.m_frmWindow.HandleCreated -= new EventHandler(Window_HandleCreated);
                this.m_frmWindow.HandleDestroyed -= new EventHandler(Window_HandleDestroyed);
                this.m_frmWindow.Disposed -= new EventHandler(Window_Disposed);
                WriteHandleValueToMappedFile(IntPtr.Zero);
                this.m_iptrHWnd = IntPtr.Zero;
                this.m_frmWindow = null;
            }
        }

        private void RegisterWindow(Form __frmNewWindow)
        {
            this.m_frmWindow = __frmNewWindow;

            if (this.m_frmWindow != null)
            {
                this.m_frmWindow.HandleCreated += new EventHandler(Window_HandleCreated);
                this.m_frmWindow.HandleDestroyed += new EventHandler(Window_HandleDestroyed);
                this.m_frmWindow.Disposed += new EventHandler(Window_Disposed);

                if (this.m_frmWindow.IsHandleCreated)
                {
                    this.m_iptrHWnd = this.m_frmWindow.Handle;
                    WriteHandleValueToMappedFile(this.m_iptrHWnd);
                }
            }

            GC.KeepAlive(__frmNewWindow);
        }

        private void Window_Disposed(object __oSender, EventArgs _evargsE)
        {
            UnregisterWindow();
        }

        private void Window_HandleDestroyed(object __oSender, EventArgs _evargsE)
        {
            UnregisterWindow();
        }

        /// <summary>
        /// Handles the HandleCreated event.
        /// </summary>
        /// <param name="_oSender"></param>
        /// <param name="_evargsE"></param>
        private void Window_HandleCreated(object _oSender, EventArgs _evargsE)
        {
            // Once we have a handle to the application window, we write it to the mapped file
            // to make it globally accessible for inter process communication.
            this.m_iptrHWnd = this.m_frmWindow.Handle;
            WriteHandleValueToMappedFile(this.m_iptrHWnd);
            GC.KeepAlive(this.m_frmWindow);
        }

        public string[] GetPendingInstanceMessages()
        {
            string[] _sMessages;

            lock (this.m_lPendingInstanceMessages)
            {
                _sMessages = this.m_lPendingInstanceMessages.ToArray();
                this.m_lPendingInstanceMessages.Clear();
            }

            return _sMessages;
        }

        public event EventHandler InstanceMessageReceived;
        private void OnInstanceMessageReceived()
        {
            if (InstanceMessageReceived != null)
            {
                InstanceMessageReceived(this, EventArgs.Empty);
            }
        }

        public void SendInstanceMessage(string __sText)
        {
            SendInstanceMessage(__sText, 1);
        }

        public void SendInstanceMessage(string __sText, int __iTimeoutSeconds)
        {
            IntPtr _iptrOurHwnd = IntPtr.Zero;
            DateTime _dtNow = DateTime.Now;
            DateTime _dtTimeoutTime = DateTime.Now + new TimeSpan(0, 0, 0, __iTimeoutSeconds);

            while (_iptrOurHwnd == IntPtr.Zero && _dtNow < _dtTimeoutTime)
            {
                _iptrOurHwnd = ReadHandleFromFromMappedFile();
                _dtNow = DateTime.Now;

                if (_iptrOurHwnd == IntPtr.Zero)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }

            if (_iptrOurHwnd != IntPtr.Zero)
            {
                NativeStructs.COPYDATASTRUCT copyDataStruct = new NativeStructs.COPYDATASTRUCT();
                IntPtr _iptrSZText = IntPtr.Zero;

                try
                {
                    unsafe
                    {
                        _iptrSZText = Marshal.StringToCoTaskMemUni(__sText);
                        copyDataStruct.dwData = UIntPtr.Zero;
                        copyDataStruct.lpData = _iptrSZText;
                        copyDataStruct.cbData = (uint)(2 * (1 + __sText.Length));
                        IntPtr _iptrLParam = new IntPtr((void*)&copyDataStruct);

                        SafeNativeMethods.SendMessageW(_iptrOurHwnd, NativeConstants.WM_COPYDATA, this.m_iptrHWnd, _iptrLParam);
                    }
                }

                finally
                {
                    if (_iptrSZText != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(_iptrSZText);
                        _iptrSZText = IntPtr.Zero;
                    }
                }
            }
        }

        public void FocusFirstInstance()
        {
            IntPtr _iptrOurHwnd = this.ReadHandleFromFromMappedFile();

            if (_iptrOurHwnd != IntPtr.Zero)
            {
                if (SafeNativeMethods.IsIconic(_iptrOurHwnd))
                {
                    SafeNativeMethods.ShowWindow(_iptrOurHwnd, NativeConstants.SW_RESTORE);
                }

                SafeNativeMethods.SetForegroundWindow(_iptrOurHwnd);
            }
        }

        public void FilterMessage(ref Message __msgM)
        {
            if (__msgM.Msg == NativeConstants.WM_COPYDATA)
            {
                unsafe
                {
                    NativeStructs.COPYDATASTRUCT* pCopyDataStruct = (NativeStructs.COPYDATASTRUCT*)__msgM.LParam.ToPointer();
                    string _sMessage = Marshal.PtrToStringUni(pCopyDataStruct->lpData);

                    lock (this.m_lPendingInstanceMessages)
                    {
                        this.m_lPendingInstanceMessages.Add(_sMessage);
                    }

                    OnInstanceMessageReceived();
                }
            }
        }

        public SingleInstanceManager(string __sMoniker)
        {
            int error = NativeConstants.ERROR_SUCCESS;

            if (__sMoniker.IndexOf('\\') != -1)
            {
                throw new ArgumentException("moniker must not have a backslash character");
            }

            this.m_sMappingName = "Local\\" + __sMoniker;

            this.m_iptrhFileMapping = SafeNativeMethods.CreateFileMappingW(
                NativeConstants.INVALID_HANDLE_VALUE,
                IntPtr.Zero,
                NativeConstants.PAGE_READWRITE | NativeConstants.SEC_COMMIT,
                0,
                m_iMappingSize,
                m_sMappingName);

            error = Marshal.GetLastWin32Error();

            if (this.m_iptrhFileMapping == IntPtr.Zero)
            {
                //throw new Win32Exception(error, "CreateFileMappingW() returned NULL (" + error.ToString() + ")");
            }

            this.m_bIsFirstInstance = (error != NativeConstants.ERROR_ALREADY_EXISTS);
        }

        private void WriteHandleValueToMappedFile(IntPtr __iptrHValue)
        {
            int error = NativeConstants.ERROR_SUCCESS;
            bool bResult = true;

            IntPtr lpData = SafeNativeMethods.MapViewOfFile(
                this.m_iptrhFileMapping,
                NativeConstants.FILE_MAP_WRITE,
                0,
                0,
                new UIntPtr((uint)m_iMappingSize));

            error = Marshal.GetLastWin32Error();

            if (lpData == IntPtr.Zero)
            {
                throw new Win32Exception(error, "MapViewOfFile() returned NULL (" + error + ")");
            }

            long int64 = __iptrHValue.ToInt64();
            byte[] int64Bytes = new byte[(int)m_iMappingSize];

            for (int i = 0; i < m_iMappingSize; ++i)
            {
                int64Bytes[i] = (byte)((int64 >> (i * 8)) & 0xff);
            }

            Marshal.Copy(int64Bytes, 0, lpData, m_iMappingSize);

            bResult = SafeNativeMethods.UnmapViewOfFile(lpData);
            error = Marshal.GetLastWin32Error();

            if (!bResult)
            {
                throw new Win32Exception(error, "UnmapViewOfFile() returned FALSE (" + error + ")");
            }
        }

        private IntPtr ReadHandleFromFromMappedFile()
        {
            int error = NativeConstants.ERROR_SUCCESS;

            IntPtr lpData = SafeNativeMethods.MapViewOfFile(
                this.m_iptrhFileMapping,
                NativeConstants.FILE_MAP_READ,
                0,
                0,
                new UIntPtr((uint)m_iMappingSize));

            error = Marshal.GetLastWin32Error();

            if (lpData == IntPtr.Zero)
            {
                throw new Win32Exception(error, "MapViewOfFile() returned NULL (" + error + ")");
            }

            byte[] int64Bytes = new byte[(int)m_iMappingSize];
            Marshal.Copy(lpData, int64Bytes, 0, m_iMappingSize);

            long int64 = 0;
            for (int i = 0; i < m_iMappingSize; ++i)
            {
                int64 += (long)(int64Bytes[i] << (i * 8));
            }

            bool bResult = SafeNativeMethods.UnmapViewOfFile(lpData);
            error = Marshal.GetLastWin32Error();

            if (!bResult)
            {
                throw new Win32Exception(error, "UnmapViewOfFile() returned FALSE (" + error + ")");
            }

            IntPtr hValue = new IntPtr(int64);
            return hValue;
        }

        ~SingleInstanceManager()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterWindow();
            }

            if (this.m_iptrhFileMapping != IntPtr.Zero)
            {
                SafeNativeMethods.CloseHandle(this.m_iptrhFileMapping);
                this.m_iptrhFileMapping = IntPtr.Zero;
            }
        }
    }
}