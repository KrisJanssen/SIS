// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleInstanceManager.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Provides a way to manage and communicate between instances of an application
//   in the same user session.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Provides a way to manage and communicate between instances of an application
    /// in the same user session.
    /// </summary>
    public sealed class SingleInstanceManager : IDisposable
    {
        #region Constants

        /// <summary>
        /// The m_i mapping size.
        /// </summary>
        private const int m_iMappingSize = 8; // sizeof(int64)

        #endregion

        #region Fields

        /// <summary>
        /// The m_b is first instance.
        /// </summary>
        private bool m_bIsFirstInstance;

        /// <summary>
        /// The m_frm window.
        /// </summary>
        private Form m_frmWindow = null;

        /// <summary>
        /// The m_iptr h wnd.
        /// </summary>
        private IntPtr m_iptrHWnd = IntPtr.Zero;

        /// <summary>
        /// The m_iptrh file mapping.
        /// </summary>
        private IntPtr m_iptrhFileMapping;

        /// <summary>
        /// The m_l pending instance messages.
        /// </summary>
        private List<string> m_lPendingInstanceMessages = new List<string>();

        /// <summary>
        /// The m_s mapping name.
        /// </summary>
        private string m_sMappingName;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleInstanceManager"/> class.
        /// </summary>
        /// <param name="__sMoniker">
        /// The __s moniker.
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
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
                this.m_sMappingName);

            error = Marshal.GetLastWin32Error();

            if (this.m_iptrhFileMapping == IntPtr.Zero)
            {
                // throw new Win32Exception(error, "CreateFileMappingW() returned NULL (" + error.ToString() + ")");
            }

            this.m_bIsFirstInstance = error != NativeConstants.ERROR_ALREADY_EXISTS;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SingleInstanceManager"/> class. 
        /// </summary>
        ~SingleInstanceManager()
        {
            this.Dispose(false);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The instance message received.
        /// </summary>
        public event EventHandler InstanceMessageReceived;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether are messages pending.
        /// </summary>
        public bool AreMessagesPending
        {
            get
            {
                lock (this.m_lPendingInstanceMessages)
                {
                    return this.m_lPendingInstanceMessages.Count > 0;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether is first instance.
        /// </summary>
        public bool IsFirstInstance
        {
            get
            {
                return this.m_bIsFirstInstance;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The filter message.
        /// </summary>
        /// <param name="__msgM">
        /// The __msg m.
        /// </param>
        public void FilterMessage(ref Message __msgM)
        {
            if (__msgM.Msg == NativeConstants.WM_COPYDATA)
            {
                unsafe
                {
                    NativeStructs.COPYDATASTRUCT* pCopyDataStruct =
                        (NativeStructs.COPYDATASTRUCT*)__msgM.LParam.ToPointer();
                    string _sMessage = Marshal.PtrToStringUni(pCopyDataStruct->lpData);

                    lock (this.m_lPendingInstanceMessages)
                    {
                        this.m_lPendingInstanceMessages.Add(_sMessage);
                    }

                    this.OnInstanceMessageReceived();
                }
            }
        }

        /// <summary>
        /// The focus first instance.
        /// </summary>
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

        /// <summary>
        /// The get pending instance messages.
        /// </summary>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
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

        /// <summary>
        /// The send instance message.
        /// </summary>
        /// <param name="__sText">
        /// The __s text.
        /// </param>
        public void SendInstanceMessage(string __sText)
        {
            this.SendInstanceMessage(__sText, 1);
        }

        /// <summary>
        /// The send instance message.
        /// </summary>
        /// <param name="__sText">
        /// The __s text.
        /// </param>
        /// <param name="__iTimeoutSeconds">
        /// The __i timeout seconds.
        /// </param>
        public void SendInstanceMessage(string __sText, int __iTimeoutSeconds)
        {
            IntPtr _iptrOurHwnd = IntPtr.Zero;
            DateTime _dtNow = DateTime.Now;
            DateTime _dtTimeoutTime = DateTime.Now + new TimeSpan(0, 0, 0, __iTimeoutSeconds);

            while (_iptrOurHwnd == IntPtr.Zero && _dtNow < _dtTimeoutTime)
            {
                _iptrOurHwnd = this.ReadHandleFromFromMappedFile();
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

                        SafeNativeMethods.SendMessageW(
                            _iptrOurHwnd, 
                            NativeConstants.WM_COPYDATA, 
                            this.m_iptrHWnd, 
                            _iptrLParam);
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

        /// <summary>
        /// The set window.
        /// </summary>
        /// <param name="__frmNewWindow">
        /// The __frm new window.
        /// </param>
        public void SetWindow(Form __frmNewWindow)
        {
            if (this.m_frmWindow != null)
            {
                this.UnregisterWindow();
            }

            this.RegisterWindow(__frmNewWindow);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UnregisterWindow();
            }

            if (this.m_iptrhFileMapping != IntPtr.Zero)
            {
                SafeNativeMethods.CloseHandle(this.m_iptrhFileMapping);
                this.m_iptrhFileMapping = IntPtr.Zero;
            }
        }

        /// <summary>
        /// The on instance message received.
        /// </summary>
        private void OnInstanceMessageReceived()
        {
            if (this.InstanceMessageReceived != null)
            {
                this.InstanceMessageReceived(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The read handle from from mapped file.
        /// </summary>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        /// <exception cref="Win32Exception">
        /// </exception>
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

        /// <summary>
        /// The register window.
        /// </summary>
        /// <param name="__frmNewWindow">
        /// The __frm new window.
        /// </param>
        private void RegisterWindow(Form __frmNewWindow)
        {
            this.m_frmWindow = __frmNewWindow;

            if (this.m_frmWindow != null)
            {
                this.m_frmWindow.HandleCreated += new EventHandler(this.Window_HandleCreated);
                this.m_frmWindow.HandleDestroyed += new EventHandler(this.Window_HandleDestroyed);
                this.m_frmWindow.Disposed += new EventHandler(this.Window_Disposed);

                if (this.m_frmWindow.IsHandleCreated)
                {
                    this.m_iptrHWnd = this.m_frmWindow.Handle;
                    this.WriteHandleValueToMappedFile(this.m_iptrHWnd);
                }
            }

            GC.KeepAlive(__frmNewWindow);
        }

        /// <summary>
        /// The unregister window.
        /// </summary>
        private void UnregisterWindow()
        {
            if (this.m_frmWindow != null)
            {
                this.m_frmWindow.HandleCreated -= new EventHandler(this.Window_HandleCreated);
                this.m_frmWindow.HandleDestroyed -= new EventHandler(this.Window_HandleDestroyed);
                this.m_frmWindow.Disposed -= new EventHandler(this.Window_Disposed);
                this.WriteHandleValueToMappedFile(IntPtr.Zero);
                this.m_iptrHWnd = IntPtr.Zero;
                this.m_frmWindow = null;
            }
        }

        /// <summary>
        /// The window_ disposed.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="_evargsE">
        /// The _evargs e.
        /// </param>
        private void Window_Disposed(object __oSender, EventArgs _evargsE)
        {
            this.UnregisterWindow();
        }

        /// <summary>
        /// Handles the HandleCreated event.
        /// </summary>
        /// <param name="_oSender">
        /// </param>
        /// <param name="_evargsE">
        /// </param>
        private void Window_HandleCreated(object _oSender, EventArgs _evargsE)
        {
            // Once we have a handle to the application window, we write it to the mapped file
            // to make it globally accessible for inter process communication.
            this.m_iptrHWnd = this.m_frmWindow.Handle;
            this.WriteHandleValueToMappedFile(this.m_iptrHWnd);
            GC.KeepAlive(this.m_frmWindow);
        }

        /// <summary>
        /// The window_ handle destroyed.
        /// </summary>
        /// <param name="__oSender">
        /// The __o sender.
        /// </param>
        /// <param name="_evargsE">
        /// The _evargs e.
        /// </param>
        private void Window_HandleDestroyed(object __oSender, EventArgs _evargsE)
        {
            this.UnregisterWindow();
        }

        /// <summary>
        /// The write handle value to mapped file.
        /// </summary>
        /// <param name="__iptrHValue">
        /// The __iptr h value.
        /// </param>
        /// <exception cref="Win32Exception">
        /// </exception>
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

        #endregion
    }
}