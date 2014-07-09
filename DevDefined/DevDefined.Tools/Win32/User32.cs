// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User32.cs" company="">
//   
// </copyright>
// <summary>
//   Windows User32 dynamic link library declarations
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Tools.Win32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Windows User32 dynamic link library declarations
    /// </summary>
    public class User32
    {
        #region Public Methods and Operators

        /// <summary>
        /// The change clipboard chain.
        /// </summary>
        /// <param name="hWndRemove">
        /// The h wnd remove.
        /// </param>
        /// <param name="hWndNewNext">
        /// The h wnd new next.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(
            IntPtr hWndRemove, 
            // handle to window to remove
            IntPtr hWndNewNext // handle to next window
            );

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="hwnd">
        /// The hwnd.
        /// </param>
        /// <param name="wMsg">
        /// The w msg.
        /// </param>
        /// <param name="wParam">
        /// The w param.
        /// </param>
        /// <param name="lParam">
        /// The l param.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The set clipboard viewer.
        /// </summary>
        /// <param name="hWnd">
        /// The h wnd.
        /// </param>
        /// <returns>
        /// The <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWnd);

        #endregion
    }

    /// <summary>
    /// Windows Event Messages
    /// </summary>
    public enum Msgs
    {
        /// <summary>
        /// The w m_ null.
        /// </summary>
        WM_NULL = 0x0000, 

        /// <summary>
        /// The w m_ create.
        /// </summary>
        WM_CREATE = 0x0001, 

        /// <summary>
        /// The w m_ destroy.
        /// </summary>
        WM_DESTROY = 0x0002, 

        /// <summary>
        /// The w m_ move.
        /// </summary>
        WM_MOVE = 0x0003, 

        /// <summary>
        /// The w m_ size.
        /// </summary>
        WM_SIZE = 0x0005, 

        /// <summary>
        /// The w m_ activate.
        /// </summary>
        WM_ACTIVATE = 0x0006, 

        /// <summary>
        /// The w m_ setfocus.
        /// </summary>
        WM_SETFOCUS = 0x0007, 

        /// <summary>
        /// The w m_ killfocus.
        /// </summary>
        WM_KILLFOCUS = 0x0008, 

        /// <summary>
        /// The w m_ enable.
        /// </summary>
        WM_ENABLE = 0x000A, 

        /// <summary>
        /// The w m_ setredraw.
        /// </summary>
        WM_SETREDRAW = 0x000B, 

        /// <summary>
        /// The w m_ settext.
        /// </summary>
        WM_SETTEXT = 0x000C, 

        /// <summary>
        /// The w m_ gettext.
        /// </summary>
        WM_GETTEXT = 0x000D, 

        /// <summary>
        /// The w m_ gettextlength.
        /// </summary>
        WM_GETTEXTLENGTH = 0x000E, 

        /// <summary>
        /// The w m_ paint.
        /// </summary>
        WM_PAINT = 0x000F, 

        /// <summary>
        /// The w m_ close.
        /// </summary>
        WM_CLOSE = 0x0010, 

        /// <summary>
        /// The w m_ queryendsession.
        /// </summary>
        WM_QUERYENDSESSION = 0x0011, 

        /// <summary>
        /// The w m_ quit.
        /// </summary>
        WM_QUIT = 0x0012, 

        /// <summary>
        /// The w m_ queryopen.
        /// </summary>
        WM_QUERYOPEN = 0x0013, 

        /// <summary>
        /// The w m_ erasebkgnd.
        /// </summary>
        WM_ERASEBKGND = 0x0014, 

        /// <summary>
        /// The w m_ syscolorchange.
        /// </summary>
        WM_SYSCOLORCHANGE = 0x0015, 

        /// <summary>
        /// The w m_ endsession.
        /// </summary>
        WM_ENDSESSION = 0x0016, 

        /// <summary>
        /// The w m_ showwindow.
        /// </summary>
        WM_SHOWWINDOW = 0x0018, 

        /// <summary>
        /// The w m_ wininichange.
        /// </summary>
        WM_WININICHANGE = 0x001A, 

        /// <summary>
        /// The w m_ settingchange.
        /// </summary>
        WM_SETTINGCHANGE = 0x001A, 

        /// <summary>
        /// The w m_ devmodechange.
        /// </summary>
        WM_DEVMODECHANGE = 0x001B, 

        /// <summary>
        /// The w m_ activateapp.
        /// </summary>
        WM_ACTIVATEAPP = 0x001C, 

        /// <summary>
        /// The w m_ fontchange.
        /// </summary>
        WM_FONTCHANGE = 0x001D, 

        /// <summary>
        /// The w m_ timechange.
        /// </summary>
        WM_TIMECHANGE = 0x001E, 

        /// <summary>
        /// The w m_ cancelmode.
        /// </summary>
        WM_CANCELMODE = 0x001F, 

        /// <summary>
        /// The w m_ setcursor.
        /// </summary>
        WM_SETCURSOR = 0x0020, 

        /// <summary>
        /// The w m_ mouseactivate.
        /// </summary>
        WM_MOUSEACTIVATE = 0x0021, 

        /// <summary>
        /// The w m_ childactivate.
        /// </summary>
        WM_CHILDACTIVATE = 0x0022, 

        /// <summary>
        /// The w m_ queuesync.
        /// </summary>
        WM_QUEUESYNC = 0x0023, 

        /// <summary>
        /// The w m_ getminmaxinfo.
        /// </summary>
        WM_GETMINMAXINFO = 0x0024, 

        /// <summary>
        /// The w m_ painticon.
        /// </summary>
        WM_PAINTICON = 0x0026, 

        /// <summary>
        /// The w m_ iconerasebkgnd.
        /// </summary>
        WM_ICONERASEBKGND = 0x0027, 

        /// <summary>
        /// The w m_ nextdlgctl.
        /// </summary>
        WM_NEXTDLGCTL = 0x0028, 

        /// <summary>
        /// The w m_ spoolerstatus.
        /// </summary>
        WM_SPOOLERSTATUS = 0x002A, 

        /// <summary>
        /// The w m_ drawitem.
        /// </summary>
        WM_DRAWITEM = 0x002B, 

        /// <summary>
        /// The w m_ measureitem.
        /// </summary>
        WM_MEASUREITEM = 0x002C, 

        /// <summary>
        /// The w m_ deleteitem.
        /// </summary>
        WM_DELETEITEM = 0x002D, 

        /// <summary>
        /// The w m_ vkeytoitem.
        /// </summary>
        WM_VKEYTOITEM = 0x002E, 

        /// <summary>
        /// The w m_ chartoitem.
        /// </summary>
        WM_CHARTOITEM = 0x002F, 

        /// <summary>
        /// The w m_ setfont.
        /// </summary>
        WM_SETFONT = 0x0030, 

        /// <summary>
        /// The w m_ getfont.
        /// </summary>
        WM_GETFONT = 0x0031, 

        /// <summary>
        /// The w m_ sethotkey.
        /// </summary>
        WM_SETHOTKEY = 0x0032, 

        /// <summary>
        /// The w m_ gethotkey.
        /// </summary>
        WM_GETHOTKEY = 0x0033, 

        /// <summary>
        /// The w m_ querydragicon.
        /// </summary>
        WM_QUERYDRAGICON = 0x0037, 

        /// <summary>
        /// The w m_ compareitem.
        /// </summary>
        WM_COMPAREITEM = 0x0039, 

        /// <summary>
        /// The w m_ getobject.
        /// </summary>
        WM_GETOBJECT = 0x003D, 

        /// <summary>
        /// The w m_ compacting.
        /// </summary>
        WM_COMPACTING = 0x0041, 

        /// <summary>
        /// The w m_ commnotify.
        /// </summary>
        WM_COMMNOTIFY = 0x0044, 

        /// <summary>
        /// The w m_ windowposchanging.
        /// </summary>
        WM_WINDOWPOSCHANGING = 0x0046, 

        /// <summary>
        /// The w m_ windowposchanged.
        /// </summary>
        WM_WINDOWPOSCHANGED = 0x0047, 

        /// <summary>
        /// The w m_ power.
        /// </summary>
        WM_POWER = 0x0048, 

        /// <summary>
        /// The w m_ copydata.
        /// </summary>
        WM_COPYDATA = 0x004A, 

        /// <summary>
        /// The w m_ canceljournal.
        /// </summary>
        WM_CANCELJOURNAL = 0x004B, 

        /// <summary>
        /// The w m_ notify.
        /// </summary>
        WM_NOTIFY = 0x004E, 

        /// <summary>
        /// The w m_ inputlangchangerequest.
        /// </summary>
        WM_INPUTLANGCHANGEREQUEST = 0x0050, 

        /// <summary>
        /// The w m_ inputlangchange.
        /// </summary>
        WM_INPUTLANGCHANGE = 0x0051, 

        /// <summary>
        /// The w m_ tcard.
        /// </summary>
        WM_TCARD = 0x0052, 

        /// <summary>
        /// The w m_ help.
        /// </summary>
        WM_HELP = 0x0053, 

        /// <summary>
        /// The w m_ userchanged.
        /// </summary>
        WM_USERCHANGED = 0x0054, 

        /// <summary>
        /// The w m_ notifyformat.
        /// </summary>
        WM_NOTIFYFORMAT = 0x0055, 

        /// <summary>
        /// The w m_ contextmenu.
        /// </summary>
        WM_CONTEXTMENU = 0x007B, 

        /// <summary>
        /// The w m_ stylechanging.
        /// </summary>
        WM_STYLECHANGING = 0x007C, 

        /// <summary>
        /// The w m_ stylechanged.
        /// </summary>
        WM_STYLECHANGED = 0x007D, 

        /// <summary>
        /// The w m_ displaychange.
        /// </summary>
        WM_DISPLAYCHANGE = 0x007E, 

        /// <summary>
        /// The w m_ geticon.
        /// </summary>
        WM_GETICON = 0x007F, 

        /// <summary>
        /// The w m_ seticon.
        /// </summary>
        WM_SETICON = 0x0080, 

        /// <summary>
        /// The w m_ nccreate.
        /// </summary>
        WM_NCCREATE = 0x0081, 

        /// <summary>
        /// The w m_ ncdestroy.
        /// </summary>
        WM_NCDESTROY = 0x0082, 

        /// <summary>
        /// The w m_ nccalcsize.
        /// </summary>
        WM_NCCALCSIZE = 0x0083, 

        /// <summary>
        /// The w m_ nchittest.
        /// </summary>
        WM_NCHITTEST = 0x0084, 

        /// <summary>
        /// The w m_ ncpaint.
        /// </summary>
        WM_NCPAINT = 0x0085, 

        /// <summary>
        /// The w m_ ncactivate.
        /// </summary>
        WM_NCACTIVATE = 0x0086, 

        /// <summary>
        /// The w m_ getdlgcode.
        /// </summary>
        WM_GETDLGCODE = 0x0087, 

        /// <summary>
        /// The w m_ syncpaint.
        /// </summary>
        WM_SYNCPAINT = 0x0088, 

        /// <summary>
        /// The w m_ ncmousemove.
        /// </summary>
        WM_NCMOUSEMOVE = 0x00A0, 

        /// <summary>
        /// The w m_ nclbuttondown.
        /// </summary>
        WM_NCLBUTTONDOWN = 0x00A1, 

        /// <summary>
        /// The w m_ nclbuttonup.
        /// </summary>
        WM_NCLBUTTONUP = 0x00A2, 

        /// <summary>
        /// The w m_ nclbuttondblclk.
        /// </summary>
        WM_NCLBUTTONDBLCLK = 0x00A3, 

        /// <summary>
        /// The w m_ ncrbuttondown.
        /// </summary>
        WM_NCRBUTTONDOWN = 0x00A4, 

        /// <summary>
        /// The w m_ ncrbuttonup.
        /// </summary>
        WM_NCRBUTTONUP = 0x00A5, 

        /// <summary>
        /// The w m_ ncrbuttondblclk.
        /// </summary>
        WM_NCRBUTTONDBLCLK = 0x00A6, 

        /// <summary>
        /// The w m_ ncmbuttondown.
        /// </summary>
        WM_NCMBUTTONDOWN = 0x00A7, 

        /// <summary>
        /// The w m_ ncmbuttonup.
        /// </summary>
        WM_NCMBUTTONUP = 0x00A8, 

        /// <summary>
        /// The w m_ ncmbuttondblclk.
        /// </summary>
        WM_NCMBUTTONDBLCLK = 0x00A9, 

        /// <summary>
        /// The w m_ ncxbuttondown.
        /// </summary>
        WM_NCXBUTTONDOWN = 0x00AB, 

        /// <summary>
        /// The w m_ ncxbuttonup.
        /// </summary>
        WM_NCXBUTTONUP = 0x00AC, 

        /// <summary>
        /// The w m_ keydown.
        /// </summary>
        WM_KEYDOWN = 0x0100, 

        /// <summary>
        /// The w m_ keyup.
        /// </summary>
        WM_KEYUP = 0x0101, 

        /// <summary>
        /// The w m_ char.
        /// </summary>
        WM_CHAR = 0x0102, 

        /// <summary>
        /// The w m_ deadchar.
        /// </summary>
        WM_DEADCHAR = 0x0103, 

        /// <summary>
        /// The w m_ syskeydown.
        /// </summary>
        WM_SYSKEYDOWN = 0x0104, 

        /// <summary>
        /// The w m_ syskeyup.
        /// </summary>
        WM_SYSKEYUP = 0x0105, 

        /// <summary>
        /// The w m_ syschar.
        /// </summary>
        WM_SYSCHAR = 0x0106, 

        /// <summary>
        /// The w m_ sysdeadchar.
        /// </summary>
        WM_SYSDEADCHAR = 0x0107, 

        /// <summary>
        /// The w m_ keylast.
        /// </summary>
        WM_KEYLAST = 0x0108, 

        /// <summary>
        /// The w m_ im e_ startcomposition.
        /// </summary>
        WM_IME_STARTCOMPOSITION = 0x010D, 

        /// <summary>
        /// The w m_ im e_ endcomposition.
        /// </summary>
        WM_IME_ENDCOMPOSITION = 0x010E, 

        /// <summary>
        /// The w m_ im e_ composition.
        /// </summary>
        WM_IME_COMPOSITION = 0x010F, 

        /// <summary>
        /// The w m_ im e_ keylast.
        /// </summary>
        WM_IME_KEYLAST = 0x010F, 

        /// <summary>
        /// The w m_ initdialog.
        /// </summary>
        WM_INITDIALOG = 0x0110, 

        /// <summary>
        /// The w m_ command.
        /// </summary>
        WM_COMMAND = 0x0111, 

        /// <summary>
        /// The w m_ syscommand.
        /// </summary>
        WM_SYSCOMMAND = 0x0112, 

        /// <summary>
        /// The w m_ timer.
        /// </summary>
        WM_TIMER = 0x0113, 

        /// <summary>
        /// The w m_ hscroll.
        /// </summary>
        WM_HSCROLL = 0x0114, 

        /// <summary>
        /// The w m_ vscroll.
        /// </summary>
        WM_VSCROLL = 0x0115, 

        /// <summary>
        /// The w m_ initmenu.
        /// </summary>
        WM_INITMENU = 0x0116, 

        /// <summary>
        /// The w m_ initmenupopup.
        /// </summary>
        WM_INITMENUPOPUP = 0x0117, 

        /// <summary>
        /// The w m_ menuselect.
        /// </summary>
        WM_MENUSELECT = 0x011F, 

        /// <summary>
        /// The w m_ menuchar.
        /// </summary>
        WM_MENUCHAR = 0x0120, 

        /// <summary>
        /// The w m_ enteridle.
        /// </summary>
        WM_ENTERIDLE = 0x0121, 

        /// <summary>
        /// The w m_ menurbuttonup.
        /// </summary>
        WM_MENURBUTTONUP = 0x0122, 

        /// <summary>
        /// The w m_ menudrag.
        /// </summary>
        WM_MENUDRAG = 0x0123, 

        /// <summary>
        /// The w m_ menugetobject.
        /// </summary>
        WM_MENUGETOBJECT = 0x0124, 

        /// <summary>
        /// The w m_ uninitmenupopup.
        /// </summary>
        WM_UNINITMENUPOPUP = 0x0125, 

        /// <summary>
        /// The w m_ menucommand.
        /// </summary>
        WM_MENUCOMMAND = 0x0126, 

        /// <summary>
        /// The w m_ ctlcolormsgbox.
        /// </summary>
        WM_CTLCOLORMSGBOX = 0x0132, 

        /// <summary>
        /// The w m_ ctlcoloredit.
        /// </summary>
        WM_CTLCOLOREDIT = 0x0133, 

        /// <summary>
        /// The w m_ ctlcolorlistbox.
        /// </summary>
        WM_CTLCOLORLISTBOX = 0x0134, 

        /// <summary>
        /// The w m_ ctlcolorbtn.
        /// </summary>
        WM_CTLCOLORBTN = 0x0135, 

        /// <summary>
        /// The w m_ ctlcolordlg.
        /// </summary>
        WM_CTLCOLORDLG = 0x0136, 

        /// <summary>
        /// The w m_ ctlcolorscrollbar.
        /// </summary>
        WM_CTLCOLORSCROLLBAR = 0x0137, 

        /// <summary>
        /// The w m_ ctlcolorstatic.
        /// </summary>
        WM_CTLCOLORSTATIC = 0x0138, 

        /// <summary>
        /// The w m_ mousemove.
        /// </summary>
        WM_MOUSEMOVE = 0x0200, 

        /// <summary>
        /// The w m_ lbuttondown.
        /// </summary>
        WM_LBUTTONDOWN = 0x0201, 

        /// <summary>
        /// The w m_ lbuttonup.
        /// </summary>
        WM_LBUTTONUP = 0x0202, 

        /// <summary>
        /// The w m_ lbuttondblclk.
        /// </summary>
        WM_LBUTTONDBLCLK = 0x0203, 

        /// <summary>
        /// The w m_ rbuttondown.
        /// </summary>
        WM_RBUTTONDOWN = 0x0204, 

        /// <summary>
        /// The w m_ rbuttonup.
        /// </summary>
        WM_RBUTTONUP = 0x0205, 

        /// <summary>
        /// The w m_ rbuttondblclk.
        /// </summary>
        WM_RBUTTONDBLCLK = 0x0206, 

        /// <summary>
        /// The w m_ mbuttondown.
        /// </summary>
        WM_MBUTTONDOWN = 0x0207, 

        /// <summary>
        /// The w m_ mbuttonup.
        /// </summary>
        WM_MBUTTONUP = 0x0208, 

        /// <summary>
        /// The w m_ mbuttondblclk.
        /// </summary>
        WM_MBUTTONDBLCLK = 0x0209, 

        /// <summary>
        /// The w m_ mousewheel.
        /// </summary>
        WM_MOUSEWHEEL = 0x020A, 

        /// <summary>
        /// The w m_ xbuttondown.
        /// </summary>
        WM_XBUTTONDOWN = 0x020B, 

        /// <summary>
        /// The w m_ xbuttonup.
        /// </summary>
        WM_XBUTTONUP = 0x020C, 

        /// <summary>
        /// The w m_ xbuttondblclk.
        /// </summary>
        WM_XBUTTONDBLCLK = 0x020D, 

        /// <summary>
        /// The w m_ parentnotify.
        /// </summary>
        WM_PARENTNOTIFY = 0x0210, 

        /// <summary>
        /// The w m_ entermenuloop.
        /// </summary>
        WM_ENTERMENULOOP = 0x0211, 

        /// <summary>
        /// The w m_ exitmenuloop.
        /// </summary>
        WM_EXITMENULOOP = 0x0212, 

        /// <summary>
        /// The w m_ nextmenu.
        /// </summary>
        WM_NEXTMENU = 0x0213, 

        /// <summary>
        /// The w m_ sizing.
        /// </summary>
        WM_SIZING = 0x0214, 

        /// <summary>
        /// The w m_ capturechanged.
        /// </summary>
        WM_CAPTURECHANGED = 0x0215, 

        /// <summary>
        /// The w m_ moving.
        /// </summary>
        WM_MOVING = 0x0216, 

        /// <summary>
        /// The w m_ devicechange.
        /// </summary>
        WM_DEVICECHANGE = 0x0219, 

        /// <summary>
        /// The w m_ mdicreate.
        /// </summary>
        WM_MDICREATE = 0x0220, 

        /// <summary>
        /// The w m_ mdidestroy.
        /// </summary>
        WM_MDIDESTROY = 0x0221, 

        /// <summary>
        /// The w m_ mdiactivate.
        /// </summary>
        WM_MDIACTIVATE = 0x0222, 

        /// <summary>
        /// The w m_ mdirestore.
        /// </summary>
        WM_MDIRESTORE = 0x0223, 

        /// <summary>
        /// The w m_ mdinext.
        /// </summary>
        WM_MDINEXT = 0x0224, 

        /// <summary>
        /// The w m_ mdimaximize.
        /// </summary>
        WM_MDIMAXIMIZE = 0x0225, 

        /// <summary>
        /// The w m_ mditile.
        /// </summary>
        WM_MDITILE = 0x0226, 

        /// <summary>
        /// The w m_ mdicascade.
        /// </summary>
        WM_MDICASCADE = 0x0227, 

        /// <summary>
        /// The w m_ mdiiconarrange.
        /// </summary>
        WM_MDIICONARRANGE = 0x0228, 

        /// <summary>
        /// The w m_ mdigetactive.
        /// </summary>
        WM_MDIGETACTIVE = 0x0229, 

        /// <summary>
        /// The w m_ mdisetmenu.
        /// </summary>
        WM_MDISETMENU = 0x0230, 

        /// <summary>
        /// The w m_ entersizemove.
        /// </summary>
        WM_ENTERSIZEMOVE = 0x0231, 

        /// <summary>
        /// The w m_ exitsizemove.
        /// </summary>
        WM_EXITSIZEMOVE = 0x0232, 

        /// <summary>
        /// The w m_ dropfiles.
        /// </summary>
        WM_DROPFILES = 0x0233, 

        /// <summary>
        /// The w m_ mdirefreshmenu.
        /// </summary>
        WM_MDIREFRESHMENU = 0x0234, 

        /// <summary>
        /// The w m_ im e_ setcontext.
        /// </summary>
        WM_IME_SETCONTEXT = 0x0281, 

        /// <summary>
        /// The w m_ im e_ notify.
        /// </summary>
        WM_IME_NOTIFY = 0x0282, 

        /// <summary>
        /// The w m_ im e_ control.
        /// </summary>
        WM_IME_CONTROL = 0x0283, 

        /// <summary>
        /// The w m_ im e_ compositionfull.
        /// </summary>
        WM_IME_COMPOSITIONFULL = 0x0284, 

        /// <summary>
        /// The w m_ im e_ select.
        /// </summary>
        WM_IME_SELECT = 0x0285, 

        /// <summary>
        /// The w m_ im e_ char.
        /// </summary>
        WM_IME_CHAR = 0x0286, 

        /// <summary>
        /// The w m_ im e_ request.
        /// </summary>
        WM_IME_REQUEST = 0x0288, 

        /// <summary>
        /// The w m_ im e_ keydown.
        /// </summary>
        WM_IME_KEYDOWN = 0x0290, 

        /// <summary>
        /// The w m_ im e_ keyup.
        /// </summary>
        WM_IME_KEYUP = 0x0291, 

        /// <summary>
        /// The w m_ mousehover.
        /// </summary>
        WM_MOUSEHOVER = 0x02A1, 

        /// <summary>
        /// The w m_ mouseleave.
        /// </summary>
        WM_MOUSELEAVE = 0x02A3, 

        /// <summary>
        /// The w m_ cut.
        /// </summary>
        WM_CUT = 0x0300, 

        /// <summary>
        /// The w m_ copy.
        /// </summary>
        WM_COPY = 0x0301, 

        /// <summary>
        /// The w m_ paste.
        /// </summary>
        WM_PASTE = 0x0302, 

        /// <summary>
        /// The w m_ clear.
        /// </summary>
        WM_CLEAR = 0x0303, 

        /// <summary>
        /// The w m_ undo.
        /// </summary>
        WM_UNDO = 0x0304, 

        /// <summary>
        /// The w m_ renderformat.
        /// </summary>
        WM_RENDERFORMAT = 0x0305, 

        /// <summary>
        /// The w m_ renderallformats.
        /// </summary>
        WM_RENDERALLFORMATS = 0x0306, 

        /// <summary>
        /// The w m_ destroyclipboard.
        /// </summary>
        WM_DESTROYCLIPBOARD = 0x0307, 

        /// <summary>
        /// The w m_ drawclipboard.
        /// </summary>
        WM_DRAWCLIPBOARD = 0x0308, 

        /// <summary>
        /// The w m_ paintclipboard.
        /// </summary>
        WM_PAINTCLIPBOARD = 0x0309, 

        /// <summary>
        /// The w m_ vscrollclipboard.
        /// </summary>
        WM_VSCROLLCLIPBOARD = 0x030A, 

        /// <summary>
        /// The w m_ sizeclipboard.
        /// </summary>
        WM_SIZECLIPBOARD = 0x030B, 

        /// <summary>
        /// The w m_ askcbformatname.
        /// </summary>
        WM_ASKCBFORMATNAME = 0x030C, 

        /// <summary>
        /// The w m_ changecbchain.
        /// </summary>
        WM_CHANGECBCHAIN = 0x030D, 

        /// <summary>
        /// The w m_ hscrollclipboard.
        /// </summary>
        WM_HSCROLLCLIPBOARD = 0x030E, 

        /// <summary>
        /// The w m_ querynewpalette.
        /// </summary>
        WM_QUERYNEWPALETTE = 0x030F, 

        /// <summary>
        /// The w m_ paletteischanging.
        /// </summary>
        WM_PALETTEISCHANGING = 0x0310, 

        /// <summary>
        /// The w m_ palettechanged.
        /// </summary>
        WM_PALETTECHANGED = 0x0311, 

        /// <summary>
        /// The w m_ hotkey.
        /// </summary>
        WM_HOTKEY = 0x0312, 

        /// <summary>
        /// The w m_ print.
        /// </summary>
        WM_PRINT = 0x0317, 

        /// <summary>
        /// The w m_ printclient.
        /// </summary>
        WM_PRINTCLIENT = 0x0318, 

        /// <summary>
        /// The w m_ handheldfirst.
        /// </summary>
        WM_HANDHELDFIRST = 0x0358, 

        /// <summary>
        /// The w m_ handheldlast.
        /// </summary>
        WM_HANDHELDLAST = 0x035F, 

        /// <summary>
        /// The w m_ afxfirst.
        /// </summary>
        WM_AFXFIRST = 0x0360, 

        /// <summary>
        /// The w m_ afxlast.
        /// </summary>
        WM_AFXLAST = 0x037F, 

        /// <summary>
        /// The w m_ penwinfirst.
        /// </summary>
        WM_PENWINFIRST = 0x0380, 

        /// <summary>
        /// The w m_ penwinlast.
        /// </summary>
        WM_PENWINLAST = 0x038F, 

        /// <summary>
        /// The w m_ app.
        /// </summary>
        WM_APP = 0x8000, 

        /// <summary>
        /// The w m_ user.
        /// </summary>
        WM_USER = 0x0400, 

        // Windows XP Balloon messages from the System Notification Area
        /// <summary>
        /// The ni n_ balloonshow.
        /// </summary>
        NIN_BALLOONSHOW = 0x0402, 

        /// <summary>
        /// The ni n_ balloonhide.
        /// </summary>
        NIN_BALLOONHIDE = 0x0403, 

        /// <summary>
        /// The ni n_ balloontimeout.
        /// </summary>
        NIN_BALLOONTIMEOUT = 0x0404, 

        /// <summary>
        /// The ni n_ balloonuserclick.
        /// </summary>
        NIN_BALLOONUSERCLICK = 0x0405
    }
}