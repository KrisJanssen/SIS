// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeConstants.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;

    /// <summary>
    /// The native constants.
    /// </summary>
    internal static class NativeConstants
    {
        #region Constants

        /// <summary>
        /// The ans i_ charset.
        /// </summary>
        public const uint ANSI_CHARSET = 0;

        /// <summary>
        /// The antialiase d_ quality.
        /// </summary>
        public const uint ANTIALIASED_QUALITY = 4;

        /// <summary>
        /// The arabi c_ charset.
        /// </summary>
        public const uint ARABIC_CHARSET = 178;

        /// <summary>
        /// The balti c_ charset.
        /// </summary>
        public const uint BALTIC_CHARSET = 186;

        /// <summary>
        /// The bc m_ first.
        /// </summary>
        public const int BCM_FIRST = 0x1600;

        /// <summary>
        /// The bc m_ setshield.
        /// </summary>
        public const int BCM_SETSHIELD = BCM_FIRST + 0x000C;

        /// <summary>
        /// The b i_ bitfields.
        /// </summary>
        public const uint BI_BITFIELDS = 3;

        /// <summary>
        /// The b i_ jpeg.
        /// </summary>
        public const uint BI_JPEG = 4;

        /// <summary>
        /// The b i_ png.
        /// </summary>
        public const uint BI_PNG = 5;

        /// <summary>
        /// The b i_ rgb.
        /// </summary>
        public const uint BI_RGB = 0;

        /// <summary>
        /// The b i_ rl e 4.
        /// </summary>
        public const uint BI_RLE4 = 2;

        /// <summary>
        /// The b i_ rl e 8.
        /// </summary>
        public const uint BI_RLE8 = 1;

        /// <summary>
        /// The blackness.
        /// </summary>
        public const uint BLACKNESS = 0x00000042; /* dest = BLACK  */

        /// <summary>
        /// The blackonwhite.
        /// </summary>
        public const int BLACKONWHITE = 1;

        /// <summary>
        /// The b p_ commandlink.
        /// </summary>
        public const int BP_COMMANDLINK = 6;

        /// <summary>
        /// The b p_ pushbutton.
        /// </summary>
        public const int BP_PUSHBUTTON = 1;

        /// <summary>
        /// The b s_ dibpattern.
        /// </summary>
        public const int BS_DIBPATTERN = 5;

        /// <summary>
        /// The b s_ dibpatter n 8 x 8.
        /// </summary>
        public const int BS_DIBPATTERN8X8 = 8;

        /// <summary>
        /// The b s_ dibpatternpt.
        /// </summary>
        public const int BS_DIBPATTERNPT = 6;

        /// <summary>
        /// The b s_ hatched.
        /// </summary>
        public const int BS_HATCHED = 2;

        /// <summary>
        /// The b s_ hollow.
        /// </summary>
        public const int BS_HOLLOW = BS_NULL;

        /// <summary>
        /// The b s_ indexed.
        /// </summary>
        public const int BS_INDEXED = 4;

        /// <summary>
        /// The b s_ monopattern.
        /// </summary>
        public const int BS_MONOPATTERN = 9;

        /// <summary>
        /// The b s_ multiline.
        /// </summary>
        public const uint BS_MULTILINE = 0x00002000;

        /// <summary>
        /// The b s_ null.
        /// </summary>
        public const int BS_NULL = 1;

        /// <summary>
        /// The b s_ pattern.
        /// </summary>
        public const int BS_PATTERN = 3;

        /// <summary>
        /// The b s_ patter n 8 x 8.
        /// </summary>
        public const int BS_PATTERN8X8 = 7;

        /// <summary>
        /// The b s_ solid.
        /// </summary>
        public const int BS_SOLID = 0;

        /// <summary>
        /// The captureblt.
        /// </summary>
        public const uint CAPTUREBLT = 0x40000000; /* Include layered windows */

        /// <summary>
        /// The c b_ showdropdown.
        /// </summary>
        public const int CB_SHOWDROPDOWN = 0x014f;

        /// <summary>
        /// The c f_ enhmetafile.
        /// </summary>
        public const uint CF_ENHMETAFILE = 14;

        /// <summary>
        /// The chinesebi g 5_ charset.
        /// </summary>
        public const uint CHINESEBIG5_CHARSET = 136;

        /// <summary>
        /// The cleartyp e_ natura l_ quality.
        /// </summary>
        public const uint CLEARTYPE_NATURAL_QUALITY = 6;

        /// <summary>
        /// The cleartyp e_ quality.
        /// </summary>
        public const uint CLEARTYPE_QUALITY = 5;

        /// <summary>
        /// The cli p_ characte r_ precis.
        /// </summary>
        public const uint CLIP_CHARACTER_PRECIS = 1;

        /// <summary>
        /// The cli p_ defaul t_ precis.
        /// </summary>
        public const uint CLIP_DEFAULT_PRECIS = 0;

        /// <summary>
        /// The cli p_ embedded.
        /// </summary>
        public const uint CLIP_EMBEDDED = 8 << 4;

        /// <summary>
        /// The cli p_ l h_ angles.
        /// </summary>
        public const uint CLIP_LH_ANGLES = 1 << 4;

        /// <summary>
        /// The cli p_ mask.
        /// </summary>
        public const uint CLIP_MASK = 0xf;

        /// <summary>
        /// The cli p_ strok e_ precis.
        /// </summary>
        public const uint CLIP_STROKE_PRECIS = 2;

        /// <summary>
        /// The cli p_ t t_ always.
        /// </summary>
        public const uint CLIP_TT_ALWAYS = 2 << 4;

        /// <summary>
        /// The clsi d_ file open dialog.
        /// </summary>
        public const string CLSID_FileOpenDialog = "DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7";

        /// <summary>
        /// The clsi d_ file operation.
        /// </summary>
        public const string CLSID_FileOperation = "3ad05575-8857-4850-9277-11b85bdb8e09";

        /// <summary>
        /// The clsi d_ file save dialog.
        /// </summary>
        public const string CLSID_FileSaveDialog = "C0B4E2F3-BA21-4773-8DBA-335EC946EB8B";

        /// <summary>
        /// The clsi d_ known folder manager.
        /// </summary>
        public const string CLSID_KnownFolderManager = "4df0c730-df9d-4ae3-9153-aa6b82e9795a";

        /// <summary>
        /// The cmdl s_ defaulted.
        /// </summary>
        public const int CMDLS_DEFAULTED = 5;

        /// <summary>
        /// The cmdl s_ defaulte d_ animating.
        /// </summary>
        public const int CMDLS_DEFAULTED_ANIMATING = 6;

        /// <summary>
        /// The cmdl s_ disabled.
        /// </summary>
        public const int CMDLS_DISABLED = 4;

        /// <summary>
        /// The cmdl s_ hot.
        /// </summary>
        public const int CMDLS_HOT = 2;

        /// <summary>
        /// The cmdl s_ normal.
        /// </summary>
        public const int CMDLS_NORMAL = 1;

        /// <summary>
        /// The cmdl s_ pressed.
        /// </summary>
        public const int CMDLS_PRESSED = 3;

        /// <summary>
        /// The coloroncolor.
        /// </summary>
        public const int COLORONCOLOR = 3;

        /// <summary>
        /// The creat e_ always.
        /// </summary>
        public const uint CREATE_ALWAYS = 2;

        /// <summary>
        /// The creat e_ new.
        /// </summary>
        public const uint CREATE_NEW = 1;

        /// <summary>
        /// The csid l_ appdata.
        /// </summary>
        public const int CSIDL_APPDATA = 0x001a; // C:\Users\[user]\AppData\Roaming\

        /// <summary>
        /// The csid l_ commo n_ desktopdirectory.
        /// </summary>
        public const int CSIDL_COMMON_DESKTOPDIRECTORY = 0x0019; // C:\Users\All Users\Desktop

        /// <summary>
        /// The csid l_ deskto p_ directory.
        /// </summary>
        public const int CSIDL_DESKTOP_DIRECTORY = 0x0010; // C:\Users\[user]\Desktop\

        /// <summary>
        /// The csid l_ fla g_ create.
        /// </summary>
        public const int CSIDL_FLAG_CREATE = 0x8000; // new for Win2K, or this in to force creation of folder

        /// <summary>
        /// The csid l_ loca l_ appdata.
        /// </summary>
        public const int CSIDL_LOCAL_APPDATA = 0x001c; // C:\Users\[user]\AppData\Local\

        /// <summary>
        /// The csid l_ mypictures.
        /// </summary>
        public const int CSIDL_MYPICTURES = 0x0027;

        /// <summary>
        /// The csid l_ personal.
        /// </summary>
        public const int CSIDL_PERSONAL = 0x0005;

        /// <summary>
        /// The csid l_ progra m_ files.
        /// </summary>
        public const int CSIDL_PROGRAM_FILES = 0x0026; // C:\Program Files\

        /// <summary>
        /// The defaul t_ charset.
        /// </summary>
        public const uint DEFAULT_CHARSET = 1;

        /// <summary>
        /// The defaul t_ pitch.
        /// </summary>
        public const uint DEFAULT_PITCH = 0;

        /// <summary>
        /// The defaul t_ quality.
        /// </summary>
        public const uint DEFAULT_QUALITY = 0;

        /// <summary>
        /// The di b_ pa l_ colors.
        /// </summary>
        public const uint DIB_PAL_COLORS = 1; /* color table in palette indices */

        /// <summary>
        /// The di b_ rg b_ colors.
        /// </summary>
        public const uint DIB_RGB_COLORS = 0; /* color table in RGBs */

        /// <summary>
        /// The digc f_ present.
        /// </summary>
        public const uint DIGCF_PRESENT = 2;

        /// <summary>
        /// The draf t_ quality.
        /// </summary>
        public const uint DRAFT_QUALITY = 1;

        /// <summary>
        /// The dropeffec t_ copy.
        /// </summary>
        public const uint DROPEFFECT_COPY = 1;

        /// <summary>
        /// The dropeffec t_ link.
        /// </summary>
        public const uint DROPEFFECT_LINK = 4;

        /// <summary>
        /// The dropeffec t_ move.
        /// </summary>
        public const uint DROPEFFECT_MOVE = 2;

        /// <summary>
        /// The dstinvert.
        /// </summary>
        public const uint DSTINVERT = 0x00550009; /* dest = (NOT dest) */

        /// <summary>
        /// The d t_ bottom.
        /// </summary>
        public const uint DT_BOTTOM = 0x00000008;

        /// <summary>
        /// The d t_ calcrect.
        /// </summary>
        public const uint DT_CALCRECT = 0x00000400;

        /// <summary>
        /// The d t_ center.
        /// </summary>
        public const uint DT_CENTER = 0x00000001;

        /// <summary>
        /// The d t_ editcontrol.
        /// </summary>
        public const uint DT_EDITCONTROL = 0x00002000;

        /// <summary>
        /// The d t_ en d_ ellipsis.
        /// </summary>
        public const uint DT_END_ELLIPSIS = 0x00008000;

        /// <summary>
        /// The d t_ expandtabs.
        /// </summary>
        public const uint DT_EXPANDTABS = 0x00000040;

        /// <summary>
        /// The d t_ externalleading.
        /// </summary>
        public const uint DT_EXTERNALLEADING = 0x00000200;

        /// <summary>
        /// The d t_ hideprefix.
        /// </summary>
        public const uint DT_HIDEPREFIX = 0x00100000;

        /// <summary>
        /// The d t_ left.
        /// </summary>
        public const uint DT_LEFT = 0x00000000;

        /// <summary>
        /// The d t_ modifystring.
        /// </summary>
        public const uint DT_MODIFYSTRING = 0x00010000;

        /// <summary>
        /// The d t_ noclip.
        /// </summary>
        public const uint DT_NOCLIP = 0x00000100;

        /// <summary>
        /// The d t_ nofullwidthcharbreak.
        /// </summary>
        public const uint DT_NOFULLWIDTHCHARBREAK = 0x00080000;

        /// <summary>
        /// The d t_ noprefix.
        /// </summary>
        public const uint DT_NOPREFIX = 0x00000800;

        /// <summary>
        /// The d t_ pat h_ ellipsis.
        /// </summary>
        public const uint DT_PATH_ELLIPSIS = 0x00004000;

        /// <summary>
        /// The d t_ prefixonly.
        /// </summary>
        public const uint DT_PREFIXONLY = 0x00200000;

        /// <summary>
        /// The d t_ right.
        /// </summary>
        public const uint DT_RIGHT = 0x00000002;

        /// <summary>
        /// The d t_ rtlreading.
        /// </summary>
        public const uint DT_RTLREADING = 0x00020000;

        /// <summary>
        /// The d t_ singleline.
        /// </summary>
        public const uint DT_SINGLELINE = 0x00000020;

        /// <summary>
        /// The d t_ tabstop.
        /// </summary>
        public const uint DT_TABSTOP = 0x00000080;

        /// <summary>
        /// The d t_ top.
        /// </summary>
        public const uint DT_TOP = 0x00000000;

        /// <summary>
        /// The d t_ vcenter.
        /// </summary>
        public const uint DT_VCENTER = 0x00000004;

        /// <summary>
        /// The d t_ wordbreak.
        /// </summary>
        public const uint DT_WORDBREAK = 0x00000010;

        /// <summary>
        /// The d t_ wor d_ ellipsis.
        /// </summary>
        public const uint DT_WORD_ELLIPSIS = 0x00040000;

        /// <summary>
        /// The d t_public.
        /// </summary>
        public const uint DT_public = 0x00001000;

        /// <summary>
        /// The dwmncr p_ disabled.
        /// </summary>
        public const uint DWMNCRP_DISABLED = 1;

        /// <summary>
        /// The dwmncr p_ enabled.
        /// </summary>
        public const uint DWMNCRP_ENABLED = 2;

        /// <summary>
        /// The dwmncr p_ last.
        /// </summary>
        public const uint DWMNCRP_LAST = 3;

        /// <summary>
        /// The dwmncr p_ usewindowstyle.
        /// </summary>
        public const uint DWMNCRP_USEWINDOWSTYLE = 0;

        /// <summary>
        /// The dwmw a_ allo w_ ncpaint.
        /// </summary>
        public const uint DWMWA_ALLOW_NCPAINT = 4;

        // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.

        /// <summary>
        /// The dwmw a_ captio n_ butto n_ bounds.
        /// </summary>
        public const uint DWMWA_CAPTION_BUTTON_BOUNDS = 5;

        // [get] Bounds of the caption button area in window-relative space.

        /// <summary>
        /// The dwmw a_ extende d_ fram e_ bounds.
        /// </summary>
        public const uint DWMWA_EXTENDED_FRAME_BOUNDS = 9;

        // [get] Gets the extended frame bounds rectangle in screen space

        /// <summary>
        /// The dwmw a_ fli p 3 d_ policy.
        /// </summary>
        public const uint DWMWA_FLIP3D_POLICY = 8; // [set] Designates how Flip3D will treat the window.

        /// <summary>
        /// The dwmw a_ forc e_ iconi c_ representation.
        /// </summary>
        public const uint DWMWA_FORCE_ICONIC_REPRESENTATION = 7;

        // [set] Force this window to display iconic thumbnails.

        /// <summary>
        /// The dwmw a_ last.
        /// </summary>
        public const uint DWMWA_LAST = 10;

        /// <summary>
        /// The dwmw a_ ncrenderin g_ enabled.
        /// </summary>
        public const uint DWMWA_NCRENDERING_ENABLED = 1; // [get] Is non-client rendering enabled/disabled

        /// <summary>
        /// The dwmw a_ ncrenderin g_ policy.
        /// </summary>
        public const uint DWMWA_NCRENDERING_POLICY = 2; // [set] Non-client rendering policy

        /// <summary>
        /// The dwmw a_ nonclien t_ rt l_ layout.
        /// </summary>
        public const uint DWMWA_NONCLIENT_RTL_LAYOUT = 6; // [set] Is non-client content RTL mirrored

        /// <summary>
        /// The dwmw a_ transition s_ forcedisabled.
        /// </summary>
        public const uint DWMWA_TRANSITIONS_FORCEDISABLED = 3; // [set] Potentially enable/forcibly disable transitions

        /// <summary>
        /// The easteurop e_ charset.
        /// </summary>
        public const uint EASTEUROPE_CHARSET = 238;

        /// <summary>
        /// The erro r_ alread y_ exists.
        /// </summary>
        public const int ERROR_ALREADY_EXISTS = 183;

        /// <summary>
        /// The erro r_ cancelled.
        /// </summary>
        public const int ERROR_CANCELLED = 1223;

        /// <summary>
        /// The erro r_ i o_ pending.
        /// </summary>
        public const int ERROR_IO_PENDING = 0x3e5;

        /// <summary>
        /// The erro r_ n o_ mor e_ items.
        /// </summary>
        public const int ERROR_NO_MORE_ITEMS = 259;

        /// <summary>
        /// The erro r_ success.
        /// </summary>
        public const int ERROR_SUCCESS = 0;

        /// <summary>
        /// The erro r_ timeout.
        /// </summary>
        public const int ERROR_TIMEOUT = 1460;

        /// <summary>
        /// The e_ notimpl.
        /// </summary>
        public const int E_NOTIMPL = unchecked((int)0x80004001);

        /// <summary>
        /// The f f_ decorative.
        /// </summary>
        public const uint FF_DECORATIVE = 5 << 4;

        /// <summary>
        /// The f f_ dontcare.
        /// </summary>
        public const uint FF_DONTCARE = 0 << 4;

        /// <summary>
        /// The f f_ modern.
        /// </summary>
        public const uint FF_MODERN = 3 << 4;

        /// <summary>
        /// The f f_ roman.
        /// </summary>
        public const uint FF_ROMAN = 1 << 4;

        /// <summary>
        /// The f f_ script.
        /// </summary>
        public const uint FF_SCRIPT = 4 << 4;

        /// <summary>
        /// The f f_ swiss.
        /// </summary>
        public const uint FF_SWISS = 2 << 4;

        /// <summary>
        /// The fil e_ ad d_ file.
        /// </summary>
        public const uint FILE_ADD_FILE = 0x0002;

        /// <summary>
        /// The fil e_ ad d_ subdirectory.
        /// </summary>
        public const uint FILE_ADD_SUBDIRECTORY = 0x0004;

        /// <summary>
        /// The fil e_ al l_ access.
        /// </summary>
        public const uint FILE_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x1FF;

        /// <summary>
        /// The fil e_ appen d_ data.
        /// </summary>
        public const uint FILE_APPEND_DATA = 0x0004;

        /// <summary>
        /// The fil e_ attribut e_ archive.
        /// </summary>
        public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;

        /// <summary>
        /// The fil e_ attribut e_ compressed.
        /// </summary>
        public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;

        /// <summary>
        /// The fil e_ attribut e_ device.
        /// </summary>
        public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;

        /// <summary>
        /// The fil e_ attribut e_ directory.
        /// </summary>
        public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;

        /// <summary>
        /// The fil e_ attribut e_ encrypted.
        /// </summary>
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;

        /// <summary>
        /// The fil e_ attribut e_ hidden.
        /// </summary>
        public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;

        /// <summary>
        /// The fil e_ attribut e_ normal.
        /// </summary>
        public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        /// <summary>
        /// The fil e_ attribut e_ no t_ conten t_ indexed.
        /// </summary>
        public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;

        /// <summary>
        /// The fil e_ attribut e_ offline.
        /// </summary>
        public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;

        /// <summary>
        /// The fil e_ attribut e_ readonly.
        /// </summary>
        public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;

        /// <summary>
        /// The fil e_ attribut e_ repars e_ point.
        /// </summary>
        public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;

        /// <summary>
        /// The fil e_ attribut e_ spars e_ file.
        /// </summary>
        public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;

        /// <summary>
        /// The fil e_ attribut e_ system.
        /// </summary>
        public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;

        /// <summary>
        /// The fil e_ attribut e_ temporary.
        /// </summary>
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;

        /// <summary>
        /// The fil e_ begin.
        /// </summary>
        public const uint FILE_BEGIN = 0;

        /// <summary>
        /// The fil e_ creat e_ pip e_ instance.
        /// </summary>
        public const uint FILE_CREATE_PIPE_INSTANCE = 0x0004;

        /// <summary>
        /// The fil e_ current.
        /// </summary>
        public const uint FILE_CURRENT = 1;

        /// <summary>
        /// The fil e_ delet e_ child.
        /// </summary>
        public const uint FILE_DELETE_CHILD = 0x0040;

        /// <summary>
        /// The fil e_ devic e_ fil e_ system.
        /// </summary>
        public const uint FILE_DEVICE_FILE_SYSTEM = 0x00000009;

        /// <summary>
        /// The fil e_ end.
        /// </summary>
        public const uint FILE_END = 2;

        /// <summary>
        /// The fil e_ execute.
        /// </summary>
        public const uint FILE_EXECUTE = 0x0020;

        /// <summary>
        /// The fil e_ fla g_ backu p_ semantics.
        /// </summary>
        public const uint FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;

        /// <summary>
        /// The fil e_ fla g_ delet e_ o n_ close.
        /// </summary>
        public const uint FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;

        /// <summary>
        /// The fil e_ fla g_ firs t_ pip e_ instance.
        /// </summary>
        public const uint FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000;

        /// <summary>
        /// The fil e_ fla g_ n o_ buffering.
        /// </summary>
        public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;

        /// <summary>
        /// The fil e_ fla g_ ope n_ n o_ recall.
        /// </summary>
        public const uint FILE_FLAG_OPEN_NO_RECALL = 0x00100000;

        /// <summary>
        /// The fil e_ fla g_ ope n_ repars e_ point.
        /// </summary>
        public const uint FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;

        /// <summary>
        /// The fil e_ fla g_ overlapped.
        /// </summary>
        public const uint FILE_FLAG_OVERLAPPED = 0x40000000;

        /// <summary>
        /// The fil e_ fla g_ posi x_ semantics.
        /// </summary>
        public const uint FILE_FLAG_POSIX_SEMANTICS = 0x01000000;

        /// <summary>
        /// The fil e_ fla g_ rando m_ access.
        /// </summary>
        public const uint FILE_FLAG_RANDOM_ACCESS = 0x10000000;

        /// <summary>
        /// The fil e_ fla g_ sequentia l_ scan.
        /// </summary>
        public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;

        /// <summary>
        /// The fil e_ fla g_ writ e_ through.
        /// </summary>
        public const uint FILE_FLAG_WRITE_THROUGH = 0x80000000;

        /// <summary>
        /// The fil e_ generi c_ execute.
        /// </summary>
        public const uint FILE_GENERIC_EXECUTE =
            STANDARD_RIGHTS_EXECUTE | FILE_READ_ATTRIBUTES | FILE_EXECUTE | SYNCHRONIZE;

        /// <summary>
        /// The fil e_ generi c_ read.
        /// </summary>
        public const uint FILE_GENERIC_READ =
            STANDARD_RIGHTS_READ | FILE_READ_DATA | FILE_READ_ATTRIBUTES | FILE_READ_EA | SYNCHRONIZE;

        /// <summary>
        /// The fil e_ generi c_ write.
        /// </summary>
        public const uint FILE_GENERIC_WRITE =
            STANDARD_RIGHTS_WRITE | FILE_WRITE_DATA | FILE_WRITE_ATTRIBUTES | FILE_WRITE_EA | FILE_APPEND_DATA
            | SYNCHRONIZE;

        /// <summary>
        /// The fil e_ lis t_ directory.
        /// </summary>
        public const uint FILE_LIST_DIRECTORY = 0x0001;

        /// <summary>
        /// The fil e_ ma p_ copy.
        /// </summary>
        public const uint FILE_MAP_COPY = SECTION_QUERY;

        /// <summary>
        /// The fil e_ ma p_ execute.
        /// </summary>
        public const uint FILE_MAP_EXECUTE = SECTION_MAP_EXECUTE_EXPLICIT;

        /// <summary>
        /// The fil e_ ma p_ read.
        /// </summary>
        public const uint FILE_MAP_READ = SECTION_MAP_READ;

        /// <summary>
        /// The fil e_ ma p_ write.
        /// </summary>
        public const uint FILE_MAP_WRITE = SECTION_MAP_WRITE;

        /// <summary>
        /// The fil e_ rea d_ attributes.
        /// </summary>
        public const uint FILE_READ_ATTRIBUTES = 0x0080;

        /// <summary>
        /// The fil e_ rea d_ data.
        /// </summary>
        public const uint FILE_READ_DATA = 0x0001;

        /// <summary>
        /// The fil e_ rea d_ ea.
        /// </summary>
        public const uint FILE_READ_EA = 0x0008;

        /// <summary>
        /// The fil e_ shar e_ delete.
        /// </summary>
        public const uint FILE_SHARE_DELETE = 0x00000004;

        /// <summary>
        /// The fil e_ shar e_ read.
        /// </summary>
        public const uint FILE_SHARE_READ = 0x00000001;

        /// <summary>
        /// The fil e_ shar e_ write.
        /// </summary>
        public const uint FILE_SHARE_WRITE = 0x00000002;

        /// <summary>
        /// The fil e_ traverse.
        /// </summary>
        public const uint FILE_TRAVERSE = 0x0020;

        /// <summary>
        /// The fil e_ writ e_ attributes.
        /// </summary>
        public const uint FILE_WRITE_ATTRIBUTES = 0x0100;

        /// <summary>
        /// The fil e_ writ e_ data.
        /// </summary>
        public const uint FILE_WRITE_DATA = 0x0002;

        /// <summary>
        /// The fil e_ writ e_ ea.
        /// </summary>
        public const uint FILE_WRITE_EA = 0x0010;

        /// <summary>
        /// The fixe d_ pitch.
        /// </summary>
        public const uint FIXED_PITCH = 1;

        /// <summary>
        /// The f w_ bold.
        /// </summary>
        public const uint FW_BOLD = 700;

        /// <summary>
        /// The f w_ dontcare.
        /// </summary>
        public const uint FW_DONTCARE = 0;

        /// <summary>
        /// The f w_ extrabold.
        /// </summary>
        public const uint FW_EXTRABOLD = 800;

        /// <summary>
        /// The f w_ extralight.
        /// </summary>
        public const uint FW_EXTRALIGHT = 200;

        /// <summary>
        /// The f w_ heavy.
        /// </summary>
        public const uint FW_HEAVY = 900;

        /// <summary>
        /// The f w_ light.
        /// </summary>
        public const uint FW_LIGHT = 300;

        /// <summary>
        /// The f w_ medium.
        /// </summary>
        public const uint FW_MEDIUM = 500;

        /// <summary>
        /// The f w_ normal.
        /// </summary>
        public const uint FW_NORMAL = 400;

        /// <summary>
        /// The f w_ semibold.
        /// </summary>
        public const uint FW_SEMIBOLD = 600;

        /// <summary>
        /// The f w_ thin.
        /// </summary>
        public const uint FW_THIN = 100;

        /// <summary>
        /// The g b 2312_ charset.
        /// </summary>
        public const uint GB2312_CHARSET = 134;

        /// <summary>
        /// The generi c_ execute.
        /// </summary>
        public const uint GENERIC_EXECUTE = 0x20000000;

        /// <summary>
        /// The generi c_ read.
        /// </summary>
        public const uint GENERIC_READ = 0x80000000;

        /// <summary>
        /// The generi c_ write.
        /// </summary>
        public const uint GENERIC_WRITE = 0x40000000;

        /// <summary>
        /// The ghnd.
        /// </summary>
        public const uint GHND = 0x0042;

        /// <summary>
        /// The gme m_ fixed.
        /// </summary>
        public const uint GMEM_FIXED = 0x0000;

        /// <summary>
        /// The gme m_ moveable.
        /// </summary>
        public const uint GMEM_MOVEABLE = 0x0002;

        /// <summary>
        /// The gme m_ zeroinit.
        /// </summary>
        public const uint GMEM_ZEROINIT = 0x0040;

        /// <summary>
        /// The gptr.
        /// </summary>
        public const uint GPTR = 0x0040;

        /// <summary>
        /// The gree k_ charset.
        /// </summary>
        public const uint GREEK_CHARSET = 161;

        /// <summary>
        /// The gwl p_ hinstance.
        /// </summary>
        public const int GWLP_HINSTANCE = -6;

        /// <summary>
        /// The gwl p_ hwndparent.
        /// </summary>
        public const int GWLP_HWNDPARENT = -8;

        /// <summary>
        /// The gwl p_ id.
        /// </summary>
        public const int GWLP_ID = -12;

        /// <summary>
        /// The gwl p_ userdata.
        /// </summary>
        public const int GWLP_USERDATA = -21;

        /// <summary>
        /// The gwl p_ wndproc.
        /// </summary>
        public const int GWLP_WNDPROC = -4;

        /// <summary>
        /// The gw l_ exstyle.
        /// </summary>
        public const int GWL_EXSTYLE = -20;

        /// <summary>
        /// The gw l_ style.
        /// </summary>
        public const int GWL_STYLE = -16;

        /// <summary>
        /// The halftone.
        /// </summary>
        public const int HALFTONE = 4;

        /// <summary>
        /// The handl e_ fla g_ inherit.
        /// </summary>
        public const uint HANDLE_FLAG_INHERIT = 0x1;

        /// <summary>
        /// The handl e_ fla g_ protec t_ fro m_ close.
        /// </summary>
        public const uint HANDLE_FLAG_PROTECT_FROM_CLOSE = 0x2;

        /// <summary>
        /// The hangeu l_ charset.
        /// </summary>
        public const uint HANGEUL_CHARSET = 129;

        /// <summary>
        /// The hangu l_ charset.
        /// </summary>
        public const uint HANGUL_CHARSET = 129;

        /// <summary>
        /// The hea p_ creat e_ alig n_16.
        /// </summary>
        public const uint HEAP_CREATE_ALIGN_16 = 0x00010000;

        /// <summary>
        /// The hea p_ creat e_ enabl e_ tracing.
        /// </summary>
        public const uint HEAP_CREATE_ENABLE_TRACING = 0x00020000;

        /// <summary>
        /// The hea p_ disabl e_ coalesc e_ o n_ free.
        /// </summary>
        public const uint HEAP_DISABLE_COALESCE_ON_FREE = 0x00000080;

        /// <summary>
        /// The hea p_ fre e_ checkin g_ enabled.
        /// </summary>
        public const uint HEAP_FREE_CHECKING_ENABLED = 0x00000040;

        /// <summary>
        /// The hea p_ generat e_ exceptions.
        /// </summary>
        public const uint HEAP_GENERATE_EXCEPTIONS = 0x00000004;

        /// <summary>
        /// The hea p_ growable.
        /// </summary>
        public const uint HEAP_GROWABLE = 0x00000002;

        /// <summary>
        /// The hea p_ maximu m_ tag.
        /// </summary>
        public const uint HEAP_MAXIMUM_TAG = 0x0FFF;

        /// <summary>
        /// The hea p_ n o_ serialize.
        /// </summary>
        public const uint HEAP_NO_SERIALIZE = 0x00000001;

        /// <summary>
        /// The hea p_ pseud o_ ta g_ flag.
        /// </summary>
        public const uint HEAP_PSEUDO_TAG_FLAG = 0x8000;

        /// <summary>
        /// The hea p_ reallo c_ i n_ plac e_ only.
        /// </summary>
        public const uint HEAP_REALLOC_IN_PLACE_ONLY = 0x00000010;

        /// <summary>
        /// The hea p_ ta g_ shift.
        /// </summary>
        public const uint HEAP_TAG_SHIFT = 18;

        /// <summary>
        /// The hea p_ tai l_ checkin g_ enabled.
        /// </summary>
        public const uint HEAP_TAIL_CHECKING_ENABLED = 0x00000020;

        /// <summary>
        /// The hea p_ zer o_ memory.
        /// </summary>
        public const uint HEAP_ZERO_MEMORY = 0x00000008;

        /// <summary>
        /// The hebre w_ charset.
        /// </summary>
        public const uint HEBREW_CHARSET = 177;

        /// <summary>
        /// The heap compatibility information.
        /// </summary>
        public const int HeapCompatibilityInformation = 0;

        /// <summary>
        /// The id i_ application.
        /// </summary>
        public const uint IDI_APPLICATION = 32512;

        /// <summary>
        /// The ii d_ i file dialog.
        /// </summary>
        public const string IID_IFileDialog = "42f85136-db7e-439c-85f1-e4075d135fc8";

        /// <summary>
        /// The ii d_ i file dialog control events.
        /// </summary>
        public const string IID_IFileDialogControlEvents = "36116642-D713-4b97-9B83-7484A9D00433";

        /// <summary>
        /// The ii d_ i file dialog customize.
        /// </summary>
        public const string IID_IFileDialogCustomize = "8016b7b3-3d49-4504-a0aa-2a37494e606f";

        /// <summary>
        /// The ii d_ i file dialog events.
        /// </summary>
        public const string IID_IFileDialogEvents = "973510DB-7D7F-452B-8975-74A85828D354";

        /// <summary>
        /// The ii d_ i file open dialog.
        /// </summary>
        public const string IID_IFileOpenDialog = "d57c7288-d4ad-4768-be02-9d969532d960";

        /// <summary>
        /// The ii d_ i file operation.
        /// </summary>
        public const string IID_IFileOperation = "947aab5f-0a5c-4c13-b4d6-4bf7836fc9f8";

        /// <summary>
        /// The ii d_ i file operation progress sink.
        /// </summary>
        public const string IID_IFileOperationProgressSink = "04b0f1a7-9490-44bc-96e1-4296a31252e2";

        /// <summary>
        /// The ii d_ i file save dialog.
        /// </summary>
        public const string IID_IFileSaveDialog = "84bccd23-5fde-4cdb-aea4-af64b83d78ab";

        /// <summary>
        /// The ii d_ i known folder.
        /// </summary>
        public const string IID_IKnownFolder = "38521333-6A87-46A7-AE10-0F16706816C3";

        /// <summary>
        /// The ii d_ i known folder manager.
        /// </summary>
        public const string IID_IKnownFolderManager = "44BEAAEC-24F4-4E90-B3F0-23D258FBB146";

        /// <summary>
        /// The ii d_ i modal window.
        /// </summary>
        public const string IID_IModalWindow = "b4db1657-70d7-485e-8e3e-6fcb5a5c1802";

        /// <summary>
        /// The ii d_ i ole window.
        /// </summary>
        public const string IID_IOleWindow = "00000114-0000-0000-C000-000000000046";

        /// <summary>
        /// The ii d_ i property store.
        /// </summary>
        public const string IID_IPropertyStore = "886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99";

        /// <summary>
        /// The ii d_ i sequential stream.
        /// </summary>
        public const string IID_ISequentialStream = "0c733a30-2a1c-11ce-ade5-00aa0044773d";

        /// <summary>
        /// The ii d_ i shell item.
        /// </summary>
        public const string IID_IShellItem = "43826D1E-E718-42EE-BC55-A1E261C37BFE";

        /// <summary>
        /// The ii d_ i shell item array.
        /// </summary>
        public const string IID_IShellItemArray = "B63EA76D-1F85-456F-A19C-48159EFA858B";

        /// <summary>
        /// The ii d_ i stream.
        /// </summary>
        public const string IID_IStream = "0000000C-0000-0000-C000-000000000046";

        /// <summary>
        /// The infinite.
        /// </summary>
        public const uint INFINITE = 0xffffffff;

        /// <summary>
        /// The joha b_ charset.
        /// </summary>
        public const uint JOHAB_CHARSET = 130;

        /// <summary>
        /// The lw a_ alpha.
        /// </summary>
        public const uint LWA_ALPHA = 0x00000002;

        /// <summary>
        /// The lw a_ colorkey.
        /// </summary>
        public const uint LWA_COLORKEY = 0x00000001;

        /// <summary>
        /// The ma c_ charset.
        /// </summary>
        public const uint MAC_CHARSET = 77;

        /// <summary>
        /// The maximu m_ allowed.
        /// </summary>
        public const uint MAXIMUM_ALLOWED = 0x02000000;

        /// <summary>
        /// The maxstretchbltmode.
        /// </summary>
        public const int MAXSTRETCHBLTMODE = 4;

        /// <summary>
        /// The ma x_ path.
        /// </summary>
        public const int MAX_PATH = 260;

        /// <summary>
        /// The m a_ activate.
        /// </summary>
        public const uint MA_ACTIVATE = 1;

        /// <summary>
        /// The m a_ activateandeat.
        /// </summary>
        public const uint MA_ACTIVATEANDEAT = 2;

        /// <summary>
        /// The m a_ noactivate.
        /// </summary>
        public const uint MA_NOACTIVATE = 3;

        /// <summary>
        /// The m a_ noactivateandeat.
        /// </summary>
        public const uint MA_NOACTIVATEANDEAT = 4;

        /// <summary>
        /// The me m_ commit.
        /// </summary>
        public const uint MEM_COMMIT = 0x1000;

        /// <summary>
        /// The me m_ decommit.
        /// </summary>
        public const uint MEM_DECOMMIT = 0x4000;

        /// <summary>
        /// The me m_ physical.
        /// </summary>
        public const uint MEM_PHYSICAL = 0x400000;

        /// <summary>
        /// The me m_ release.
        /// </summary>
        public const uint MEM_RELEASE = 0x8000;

        /// <summary>
        /// The me m_ reserve.
        /// </summary>
        public const uint MEM_RESERVE = 0x2000;

        /// <summary>
        /// The me m_ reset.
        /// </summary>
        public const uint MEM_RESET = 0x80000;

        /// <summary>
        /// The me m_ to p_ down.
        /// </summary>
        public const uint MEM_TOP_DOWN = 0x100000;

        /// <summary>
        /// The mergecopy.
        /// </summary>
        public const uint MERGECOPY = 0x00C000CA; /* dest = (source AND pattern) */

        /// <summary>
        /// The mergepaint.
        /// </summary>
        public const uint MERGEPAINT = 0x00BB0226; /* dest = (NOT source) OR dest */

        /// <summary>
        /// The metho d_ buffered.
        /// </summary>
        public const uint METHOD_BUFFERED = 0;

        /// <summary>
        /// The m f_ bycommand.
        /// </summary>
        public const uint MF_BYCOMMAND = 0;

        /// <summary>
        /// The m f_ disabled.
        /// </summary>
        public const uint MF_DISABLED = 2;

        /// <summary>
        /// The m f_ grayed.
        /// </summary>
        public const uint MF_GRAYED = 1;

        /// <summary>
        /// The monito r_ defaulttonearest.
        /// </summary>
        public const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

        /// <summary>
        /// The monito r_ defaulttonull.
        /// </summary>
        public const uint MONITOR_DEFAULTTONULL = 0x00000000;

        /// <summary>
        /// The monito r_ defaulttoprimary.
        /// </summary>
        public const uint MONITOR_DEFAULTTOPRIMARY = 0x00000001;

        /// <summary>
        /// The mon o_ font.
        /// </summary>
        public const uint MONO_FONT = 8;

        /// <summary>
        /// The nomirrorbitmap.
        /// </summary>
        public const uint NOMIRRORBITMAP = 0x80000000; /* Do not Mirror the bitmap in this call */

        /// <summary>
        /// The nonantialiase d_ quality.
        /// </summary>
        public const uint NONANTIALIASED_QUALITY = 3;

        /// <summary>
        /// The notif y_ fo r_ al l_ sessions.
        /// </summary>
        public const uint NOTIFY_FOR_ALL_SESSIONS = 1;

        /// <summary>
        /// The notif y_ fo r_ thi s_ session.
        /// </summary>
        public const uint NOTIFY_FOR_THIS_SESSION = 0;

        /// <summary>
        /// The notsrccopy.
        /// </summary>
        public const uint NOTSRCCOPY = 0x00330008; /* dest = (NOT source) */

        /// <summary>
        /// The notsrcerase.
        /// </summary>
        public const uint NOTSRCERASE = 0x001100A6; /* dest = (NOT src) AND (NOT dest) */

        /// <summary>
        /// The oe m_ charset.
        /// </summary>
        public const uint OEM_CHARSET = 255;

        /// <summary>
        /// The ope n_ always.
        /// </summary>
        public const uint OPEN_ALWAYS = 4;

        /// <summary>
        /// The ope n_ existing.
        /// </summary>
        public const uint OPEN_EXISTING = 3;

        /// <summary>
        /// The ou t_ characte r_ precis.
        /// </summary>
        public const uint OUT_CHARACTER_PRECIS = 2;

        /// <summary>
        /// The ou t_ defaul t_ precis.
        /// </summary>
        public const uint OUT_DEFAULT_PRECIS = 0;

        /// <summary>
        /// The ou t_ devic e_ precis.
        /// </summary>
        public const uint OUT_DEVICE_PRECIS = 5;

        /// <summary>
        /// The ou t_ outlin e_ precis.
        /// </summary>
        public const uint OUT_OUTLINE_PRECIS = 8;

        /// <summary>
        /// The ou t_ p s_ onl y_ precis.
        /// </summary>
        public const uint OUT_PS_ONLY_PRECIS = 10;

        /// <summary>
        /// The ou t_ raste r_ precis.
        /// </summary>
        public const uint OUT_RASTER_PRECIS = 6;

        /// <summary>
        /// The ou t_ scree n_ outlin e_ precis.
        /// </summary>
        public const uint OUT_SCREEN_OUTLINE_PRECIS = 9;

        /// <summary>
        /// The ou t_ strin g_ precis.
        /// </summary>
        public const uint OUT_STRING_PRECIS = 1;

        /// <summary>
        /// The ou t_ strok e_ precis.
        /// </summary>
        public const uint OUT_STROKE_PRECIS = 3;

        /// <summary>
        /// The ou t_ t t_ onl y_ precis.
        /// </summary>
        public const uint OUT_TT_ONLY_PRECIS = 7;

        /// <summary>
        /// The ou t_ t t_ precis.
        /// </summary>
        public const uint OUT_TT_PRECIS = 4;

        /// <summary>
        /// The pag e_ execute.
        /// </summary>
        public const uint PAGE_EXECUTE = 0x10;

        /// <summary>
        /// The pag e_ execut e_ read.
        /// </summary>
        public const uint PAGE_EXECUTE_READ = 0x20;

        /// <summary>
        /// The pag e_ execut e_ readwrite.
        /// </summary>
        public const uint PAGE_EXECUTE_READWRITE = 0x40;

        /// <summary>
        /// The pag e_ execut e_ writecopy.
        /// </summary>
        public const uint PAGE_EXECUTE_WRITECOPY = 0x80;

        /// <summary>
        /// The pag e_ guard.
        /// </summary>
        public const uint PAGE_GUARD = 0x100;

        /// <summary>
        /// The pag e_ noaccess.
        /// </summary>
        public const uint PAGE_NOACCESS = 0x01;

        /// <summary>
        /// The pag e_ nocache.
        /// </summary>
        public const uint PAGE_NOCACHE = 0x200;

        /// <summary>
        /// The pag e_ readonly.
        /// </summary>
        public const uint PAGE_READONLY = 0x02;

        /// <summary>
        /// The pag e_ readwrite.
        /// </summary>
        public const uint PAGE_READWRITE = 0x04;

        /// <summary>
        /// The pag e_ writecombine.
        /// </summary>
        public const uint PAGE_WRITECOMBINE = 0x400;

        /// <summary>
        /// The pag e_ writecopy.
        /// </summary>
        public const uint PAGE_WRITECOPY = 0x08;

        /// <summary>
        /// The patcopy.
        /// </summary>
        public const uint PATCOPY = 0x00F00021; /* dest = pattern  */

        /// <summary>
        /// The patinvert.
        /// </summary>
        public const uint PATINVERT = 0x005A0049; /* dest = pattern XOR dest */

        /// <summary>
        /// The patpaint.
        /// </summary>
        public const uint PATPAINT = 0x00FB0A09; /* dest = DPSnoo  */

        /// <summary>
        /// The pb m_ setmarquee.
        /// </summary>
        public const int PBM_SETMARQUEE = WM_USER + 10;

        /// <summary>
        /// The pb s_ defaulted.
        /// </summary>
        public const int PBS_DEFAULTED = 5;

        /// <summary>
        /// The pb s_ disabled.
        /// </summary>
        public const int PBS_DISABLED = 4;

        /// <summary>
        /// The pb s_ hot.
        /// </summary>
        public const int PBS_HOT = 2;

        /// <summary>
        /// The pb s_ marquee.
        /// </summary>
        public const uint PBS_MARQUEE = 0x08;

        /// <summary>
        /// The pb s_ normal.
        /// </summary>
        public const int PBS_NORMAL = 1;

        /// <summary>
        /// The pb s_ pressed.
        /// </summary>
        public const int PBS_PRESSED = 3;

        /// <summary>
        /// The pb s_ smooth.
        /// </summary>
        public const uint PBS_SMOOTH = 0x01;

        /// <summary>
        /// The p f_ n x_ enabled.
        /// </summary>
        public const uint PF_NX_ENABLED = 12;

        /// <summary>
        /// The p f_ ss e 3_ instruction s_ available.
        /// </summary>
        public const uint PF_SSE3_INSTRUCTIONS_AVAILABLE = 13;

        /// <summary>
        /// The p f_ xmm i 64_ instruction s_ available.
        /// </summary>
        public const uint PF_XMMI64_INSTRUCTIONS_AVAILABLE = 10;

        /// <summary>
        /// The p f_ xmm i_ instruction s_ available.
        /// </summary>
        public const uint PF_XMMI_INSTRUCTIONS_AVAILABLE = 6;

        /// <summary>
        /// The processo r_ architectur e_ am d 64.
        /// </summary>
        public const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;

        /// <summary>
        /// The processo r_ architectur e_ i a 64.
        /// </summary>
        public const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;

        /// <summary>
        /// The processo r_ architectur e_ intel.
        /// </summary>
        public const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;

        /// <summary>
        /// The processo r_ architectur e_ unknown.
        /// </summary>
        public const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        /// <summary>
        /// The proces s_ al l_ access.
        /// </summary>
        public const uint PROCESS_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0xFFFF;

        /// <summary>
        /// The proces s_ creat e_ process.
        /// </summary>
        public const uint PROCESS_CREATE_PROCESS = 0x0080;

        /// <summary>
        /// The proces s_ creat e_ thread.
        /// </summary>
        public const uint PROCESS_CREATE_THREAD = 0x0002;

        /// <summary>
        /// The proces s_ du p_ handle.
        /// </summary>
        public const uint PROCESS_DUP_HANDLE = 0x0040;

        /// <summary>
        /// The proces s_ quer y_ information.
        /// </summary>
        public const uint PROCESS_QUERY_INFORMATION = 0x0400;

        /// <summary>
        /// The proces s_ quer y_ limite d_ information.
        /// </summary>
        public const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;

        /// <summary>
        /// The proces s_ se t_ information.
        /// </summary>
        public const uint PROCESS_SET_INFORMATION = 0x0200;

        /// <summary>
        /// The proces s_ se t_ quota.
        /// </summary>
        public const uint PROCESS_SET_QUOTA = 0x0100;

        /// <summary>
        /// The proces s_ se t_ sessionid.
        /// </summary>
        public const uint PROCESS_SET_SESSIONID = 0x0004;

        /// <summary>
        /// The proces s_ suspen d_ resume.
        /// </summary>
        public const uint PROCESS_SUSPEND_RESUME = 0x0800;

        /// <summary>
        /// The proces s_ terminate.
        /// </summary>
        public const uint PROCESS_TERMINATE = 0x0001;

        /// <summary>
        /// The proces s_ v m_ operation.
        /// </summary>
        public const uint PROCESS_VM_OPERATION = 0x0008;

        /// <summary>
        /// The proces s_ v m_ read.
        /// </summary>
        public const uint PROCESS_VM_READ = 0x0010;

        /// <summary>
        /// The proces s_ v m_ write.
        /// </summary>
        public const uint PROCESS_VM_WRITE = 0x0020;

        /// <summary>
        /// The proo f_ quality.
        /// </summary>
        public const uint PROOF_QUALITY = 2;

        /// <summary>
        /// The p s_ alternate.
        /// </summary>
        public const int PS_ALTERNATE = 8;

        /// <summary>
        /// The p s_ cosmetic.
        /// </summary>
        public const int PS_COSMETIC = 0x00000000;

        /// <summary>
        /// The p s_ dash.
        /// </summary>
        public const int PS_DASH = 1; /* -------  */

        /// <summary>
        /// The p s_ dashdot.
        /// </summary>
        public const int PS_DASHDOT = 3; /* _._._._  */

        /// <summary>
        /// The p s_ dashdotdot.
        /// </summary>
        public const int PS_DASHDOTDOT = 4; /* _.._.._  */

        /// <summary>
        /// The p s_ dot.
        /// </summary>
        public const int PS_DOT = 2; /* .......  */

        /// <summary>
        /// The p s_ endca p_ flat.
        /// </summary>
        public const int PS_ENDCAP_FLAT = 0x00000200;

        /// <summary>
        /// The p s_ endca p_ mask.
        /// </summary>
        public const int PS_ENDCAP_MASK = 0x00000F00;

        /// <summary>
        /// The p s_ endca p_ round.
        /// </summary>
        public const int PS_ENDCAP_ROUND = 0x00000000;

        /// <summary>
        /// The p s_ endca p_ square.
        /// </summary>
        public const int PS_ENDCAP_SQUARE = 0x00000100;

        /// <summary>
        /// The p s_ geometric.
        /// </summary>
        public const int PS_GEOMETRIC = 0x00010000;

        /// <summary>
        /// The p s_ insideframe.
        /// </summary>
        public const int PS_INSIDEFRAME = 6;

        /// <summary>
        /// The p s_ joi n_ bevel.
        /// </summary>
        public const int PS_JOIN_BEVEL = 0x00001000;

        /// <summary>
        /// The p s_ joi n_ mask.
        /// </summary>
        public const int PS_JOIN_MASK = 0x0000F000;

        /// <summary>
        /// The p s_ joi n_ miter.
        /// </summary>
        public const int PS_JOIN_MITER = 0x00002000;

        /// <summary>
        /// The p s_ joi n_ round.
        /// </summary>
        public const int PS_JOIN_ROUND = 0x00000000;

        /// <summary>
        /// The p s_ null.
        /// </summary>
        public const int PS_NULL = 5;

        /// <summary>
        /// The p s_ solid.
        /// </summary>
        public const int PS_SOLID = 0;

        /// <summary>
        /// The p s_ typ e_ mask.
        /// </summary>
        public const int PS_TYPE_MASK = 0x000F0000;

        /// <summary>
        /// The p s_ userstyle.
        /// </summary>
        public const int PS_USERSTYLE = 7;

        /// <summary>
        /// The rea d_ control.
        /// </summary>
        public const uint READ_CONTROL = 0x00020000;

        /// <summary>
        /// The russia n_ charset.
        /// </summary>
        public const uint RUSSIAN_CHARSET = 204;

        /// <summary>
        /// The sb m_ setpos.
        /// </summary>
        public const int SBM_SETPOS = 0x00E0;

        /// <summary>
        /// The sb m_ setrange.
        /// </summary>
        public const int SBM_SETRANGE = 0x00E2;

        /// <summary>
        /// The sb m_ setrangeredraw.
        /// </summary>
        public const int SBM_SETRANGEREDRAW = 0x00E6;

        /// <summary>
        /// The sb m_ setscrollinfo.
        /// </summary>
        public const int SBM_SETSCROLLINFO = 0x00E9;

        /// <summary>
        /// The s b_ horz.
        /// </summary>
        public const int SB_HORZ = 0;

        /// <summary>
        /// The s c_ close.
        /// </summary>
        public const uint SC_CLOSE = 0xf060;

        /// <summary>
        /// The sectio n_ ma p_ execut e_ explicit.
        /// </summary>
        public const uint SECTION_MAP_EXECUTE_EXPLICIT = 0x0020;

        /// <summary>
        /// The sectio n_ ma p_ read.
        /// </summary>
        public const uint SECTION_MAP_READ = 0x0004;

        /// <summary>
        /// The sectio n_ ma p_ write.
        /// </summary>
        public const uint SECTION_MAP_WRITE = 0x0002;

        /// <summary>
        /// The sectio n_ query.
        /// </summary>
        public const uint SECTION_QUERY = 0x0001;

        /// <summary>
        /// The se c_ commit.
        /// </summary>
        public const uint SEC_COMMIT = 0x8000000;

        /// <summary>
        /// The se c_ image.
        /// </summary>
        public const uint SEC_IMAGE = 0x1000000;

        /// <summary>
        /// The se c_ nocache.
        /// </summary>
        public const uint SEC_NOCACHE = 0x10000000;

        /// <summary>
        /// The se c_ reserve.
        /// </summary>
        public const uint SEC_RESERVE = 0x4000000;

        /// <summary>
        /// The se e_ mas k_ asyncok.
        /// </summary>
        public const uint SEE_MASK_ASYNCOK = 0x00100000;

        /// <summary>
        /// The se e_ mas k_ classkey.
        /// </summary>
        public const uint SEE_MASK_CLASSKEY = 0x00000003;

        /// <summary>
        /// The se e_ mas k_ classname.
        /// </summary>
        public const uint SEE_MASK_CLASSNAME = 0x00000001;

        /// <summary>
        /// The se e_ mas k_ connectnetdrv.
        /// </summary>
        public const uint SEE_MASK_CONNECTNETDRV = 0x00000080;

        /// <summary>
        /// The se e_ mas k_ doenvsubst.
        /// </summary>
        public const uint SEE_MASK_DOENVSUBST = 0x00000200;

        /// <summary>
        /// The se e_ mas k_ fla g_ ddewait.
        /// </summary>
        public const uint SEE_MASK_FLAG_DDEWAIT = 0x00000100;

        /// <summary>
        /// The se e_ mas k_ fla g_ lo g_ usage.
        /// </summary>
        public const uint SEE_MASK_FLAG_LOG_USAGE = 0x04000000;

        /// <summary>
        /// The se e_ mas k_ fla g_ n o_ ui.
        /// </summary>
        public const uint SEE_MASK_FLAG_NO_UI = 0x00000400;

        /// <summary>
        /// The se e_ mas k_ hmonitor.
        /// </summary>
        public const uint SEE_MASK_HMONITOR = 0x00200000;

        /// <summary>
        /// The se e_ mas k_ hotkey.
        /// </summary>
        public const uint SEE_MASK_HOTKEY = 0x00000020;

        /// <summary>
        /// The se e_ mas k_ icon.
        /// </summary>
        public const uint SEE_MASK_ICON = 0x00000010;

        /// <summary>
        /// The se e_ mas k_ idlist.
        /// </summary>
        public const uint SEE_MASK_IDLIST = 0x00000004;

        /// <summary>
        /// The se e_ mas k_ invokeidlist.
        /// </summary>
        public const uint SEE_MASK_INVOKEIDLIST = 0x0000000c;

        /// <summary>
        /// The se e_ mas k_ nocloseprocess.
        /// </summary>
        public const uint SEE_MASK_NOCLOSEPROCESS = 0x00000040;

        /// <summary>
        /// The se e_ mas k_ noqueryclassstore.
        /// </summary>
        public const uint SEE_MASK_NOQUERYCLASSSTORE = 0x01000000;

        /// <summary>
        /// The se e_ mas k_ nozonechecks.
        /// </summary>
        public const uint SEE_MASK_NOZONECHECKS = 0x00800000;

        /// <summary>
        /// The se e_ mas k_ n o_ console.
        /// </summary>
        public const uint SEE_MASK_NO_CONSOLE = 0x00008000;

        /// <summary>
        /// The se e_ mas k_ unicode.
        /// </summary>
        public const uint SEE_MASK_UNICODE = 0x00004000;

        /// <summary>
        /// The se e_ mas k_ waitforinputidle.
        /// </summary>
        public const uint SEE_MASK_WAITFORINPUTIDLE = 0x02000000;

        /// <summary>
        /// The shar d_ patha.
        /// </summary>
        public const uint SHARD_PATHA = 0x00000002;

        /// <summary>
        /// The shar d_ pathw.
        /// </summary>
        public const uint SHARD_PATHW = 0x00000003;

        /// <summary>
        /// The shar d_ pidl.
        /// </summary>
        public const uint SHARD_PIDL = 0x00000001;

        /// <summary>
        /// The shgf p_ typ e_ current.
        /// </summary>
        public const uint SHGFP_TYPE_CURRENT = 0;

        /// <summary>
        /// The shgf p_ typ e_ default.
        /// </summary>
        public const uint SHGFP_TYPE_DEFAULT = 1;

        /// <summary>
        /// The shiftji s_ charset.
        /// </summary>
        public const uint SHIFTJIS_CHARSET = 128;

        /// <summary>
        /// The shvie w_ thumbnail.
        /// </summary>
        public const uint SHVIEW_THUMBNAIL = 0x702d;

        /// <summary>
        /// The smt o_ abortifhung.
        /// </summary>
        public const uint SMTO_ABORTIFHUNG = 0x0002;

        /// <summary>
        /// The smt o_ block.
        /// </summary>
        public const uint SMTO_BLOCK = 0x0001;

        /// <summary>
        /// The smt o_ normal.
        /// </summary>
        public const uint SMTO_NORMAL = 0x0000;

        /// <summary>
        /// The smt o_ notimeoutifnothung.
        /// </summary>
        public const uint SMTO_NOTIMEOUTIFNOTHUNG = 0x0008;

        /// <summary>
        /// The s m_ remotesession.
        /// </summary>
        public const int SM_REMOTESESSION = 0x1000;

        /// <summary>
        /// The s m_ tabletpc.
        /// </summary>
        public const int SM_TABLETPC = 86;

        /// <summary>
        /// The sp i_ getaccesstimeout.
        /// </summary>
        public const uint SPI_GETACCESSTIMEOUT = 0x003C;

        /// <summary>
        /// The sp i_ getactivewindowtracking.
        /// </summary>
        public const uint SPI_GETACTIVEWINDOWTRACKING = 0x1000;

        /// <summary>
        /// The sp i_ getactivewndtrktimeout.
        /// </summary>
        public const uint SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002;

        /// <summary>
        /// The sp i_ getactivewndtrkzorder.
        /// </summary>
        public const uint SPI_GETACTIVEWNDTRKZORDER = 0x100C;

        /// <summary>
        /// The sp i_ getanimation.
        /// </summary>
        public const uint SPI_GETANIMATION = 0x0048;

        /// <summary>
        /// The sp i_ getbeep.
        /// </summary>
        public const uint SPI_GETBEEP = 0x0001;

        /// <summary>
        /// The sp i_ getblocksendinputresets.
        /// </summary>
        public const uint SPI_GETBLOCKSENDINPUTRESETS = 0x1026;

        /// <summary>
        /// The sp i_ getborder.
        /// </summary>
        public const uint SPI_GETBORDER = 0x0005;

        /// <summary>
        /// The sp i_ getcaretwidth.
        /// </summary>
        public const uint SPI_GETCARETWIDTH = 0x2006;

        /// <summary>
        /// The sp i_ getcomboboxanimation.
        /// </summary>
        public const uint SPI_GETCOMBOBOXANIMATION = 0x1004;

        /// <summary>
        /// The sp i_ getcursorshadow.
        /// </summary>
        public const uint SPI_GETCURSORSHADOW = 0x101A;

        /// <summary>
        /// The sp i_ getdefaultinputlang.
        /// </summary>
        public const uint SPI_GETDEFAULTINPUTLANG = 0x0059;

        /// <summary>
        /// The sp i_ getdeskwallpaper.
        /// </summary>
        public const uint SPI_GETDESKWALLPAPER = 0x0073;

        /// <summary>
        /// The sp i_ getdragfullwindows.
        /// </summary>
        public const uint SPI_GETDRAGFULLWINDOWS = 0x0026;

        /// <summary>
        /// The sp i_ getdropshadow.
        /// </summary>
        public const uint SPI_GETDROPSHADOW = 0x1024;

        /// <summary>
        /// The sp i_ getfasttaskswitch.
        /// </summary>
        public const uint SPI_GETFASTTASKSWITCH = 0x0023;

        /// <summary>
        /// The sp i_ getfilterkeys.
        /// </summary>
        public const uint SPI_GETFILTERKEYS = 0x0032;

        /// <summary>
        /// The sp i_ getflatmenu.
        /// </summary>
        public const uint SPI_GETFLATMENU = 0x1022;

        /// <summary>
        /// The sp i_ getfocusborderheight.
        /// </summary>
        public const uint SPI_GETFOCUSBORDERHEIGHT = 0x2010;

        /// <summary>
        /// The sp i_ getfocusborderwidth.
        /// </summary>
        public const uint SPI_GETFOCUSBORDERWIDTH = 0x200E;

        /// <summary>
        /// The sp i_ getfontsmoothing.
        /// </summary>
        public const uint SPI_GETFONTSMOOTHING = 0x004A;

        /// <summary>
        /// The sp i_ getfontsmoothingcontrast.
        /// </summary>
        public const uint SPI_GETFONTSMOOTHINGCONTRAST = 0x200C;

        /// <summary>
        /// The sp i_ getfontsmoothingorientation.
        /// </summary>
        public const uint SPI_GETFONTSMOOTHINGORIENTATION = 0x2012;

        /// <summary>
        /// The sp i_ getfontsmoothingtype.
        /// </summary>
        public const uint SPI_GETFONTSMOOTHINGTYPE = 0x200A;

        /// <summary>
        /// The sp i_ getforegroundflashcount.
        /// </summary>
        public const uint SPI_GETFOREGROUNDFLASHCOUNT = 0x2004;

        /// <summary>
        /// The sp i_ getforegroundlocktimeout.
        /// </summary>
        public const uint SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000;

        /// <summary>
        /// The sp i_ getgradientcaptions.
        /// </summary>
        public const uint SPI_GETGRADIENTCAPTIONS = 0x1008;

        /// <summary>
        /// The sp i_ getgridgranularity.
        /// </summary>
        public const uint SPI_GETGRIDGRANULARITY = 0x0012;

        /// <summary>
        /// The sp i_ gethighcontrast.
        /// </summary>
        public const uint SPI_GETHIGHCONTRAST = 0x0042;

        /// <summary>
        /// The sp i_ gethottracking.
        /// </summary>
        public const uint SPI_GETHOTTRACKING = 0x100E;

        /// <summary>
        /// The sp i_ geticonmetrics.
        /// </summary>
        public const uint SPI_GETICONMETRICS = 0x002D;

        /// <summary>
        /// The sp i_ geticontitlelogfont.
        /// </summary>
        public const uint SPI_GETICONTITLELOGFONT = 0x001F;

        /// <summary>
        /// The sp i_ geticontitlewrap.
        /// </summary>
        public const uint SPI_GETICONTITLEWRAP = 0x0019;

        /// <summary>
        /// The sp i_ getkeyboardcues.
        /// </summary>
        public const uint SPI_GETKEYBOARDCUES = 0x100A;

        /// <summary>
        /// The sp i_ getkeyboarddelay.
        /// </summary>
        public const uint SPI_GETKEYBOARDDELAY = 0x0016;

        /// <summary>
        /// The sp i_ getkeyboardpref.
        /// </summary>
        public const uint SPI_GETKEYBOARDPREF = 0x0044;

        /// <summary>
        /// The sp i_ getkeyboardspeed.
        /// </summary>
        public const uint SPI_GETKEYBOARDSPEED = 0x000A;

        /// <summary>
        /// The sp i_ getlistboxsmoothscrolling.
        /// </summary>
        public const uint SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006;

        /// <summary>
        /// The sp i_ getlowpoweractive.
        /// </summary>
        public const uint SPI_GETLOWPOWERACTIVE = 0x0053;

        /// <summary>
        /// The sp i_ getlowpowertimeout.
        /// </summary>
        public const uint SPI_GETLOWPOWERTIMEOUT = 0x004F;

        /// <summary>
        /// The sp i_ getmenuanimation.
        /// </summary>
        public const uint SPI_GETMENUANIMATION = 0x1002;

        /// <summary>
        /// The sp i_ getmenudropalignment.
        /// </summary>
        public const uint SPI_GETMENUDROPALIGNMENT = 0x001B;

        /// <summary>
        /// The sp i_ getmenufade.
        /// </summary>
        public const uint SPI_GETMENUFADE = 0x1012;

        /// <summary>
        /// The sp i_ getmenushowdelay.
        /// </summary>
        public const uint SPI_GETMENUSHOWDELAY = 0x006A;

        /// <summary>
        /// The sp i_ getmenuunderlines.
        /// </summary>
        public const uint SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES;

        /// <summary>
        /// The sp i_ getminimizedmetrics.
        /// </summary>
        public const uint SPI_GETMINIMIZEDMETRICS = 0x002B;

        /// <summary>
        /// The sp i_ getmouse.
        /// </summary>
        public const uint SPI_GETMOUSE = 0x0003;

        /// <summary>
        /// The sp i_ getmouseclicklock.
        /// </summary>
        public const uint SPI_GETMOUSECLICKLOCK = 0x101E;

        /// <summary>
        /// The sp i_ getmouseclicklocktime.
        /// </summary>
        public const uint SPI_GETMOUSECLICKLOCKTIME = 0x2008;

        /// <summary>
        /// The sp i_ getmousehoverheight.
        /// </summary>
        public const uint SPI_GETMOUSEHOVERHEIGHT = 0x0064;

        /// <summary>
        /// The sp i_ getmousehovertime.
        /// </summary>
        public const uint SPI_GETMOUSEHOVERTIME = 0x0066;

        /// <summary>
        /// The sp i_ getmousehoverwidth.
        /// </summary>
        public const uint SPI_GETMOUSEHOVERWIDTH = 0x0062;

        /// <summary>
        /// The sp i_ getmousekeys.
        /// </summary>
        public const uint SPI_GETMOUSEKEYS = 0x0036;

        /// <summary>
        /// The sp i_ getmousesonar.
        /// </summary>
        public const uint SPI_GETMOUSESONAR = 0x101C;

        /// <summary>
        /// The sp i_ getmousespeed.
        /// </summary>
        public const uint SPI_GETMOUSESPEED = 0x0070;

        /// <summary>
        /// The sp i_ getmousetrails.
        /// </summary>
        public const uint SPI_GETMOUSETRAILS = 0x005E;

        /// <summary>
        /// The sp i_ getmousevanish.
        /// </summary>
        public const uint SPI_GETMOUSEVANISH = 0x1020;

        /// <summary>
        /// The sp i_ getnonclientmetrics.
        /// </summary>
        public const uint SPI_GETNONCLIENTMETRICS = 0x0029;

        /// <summary>
        /// The sp i_ getpoweroffactive.
        /// </summary>
        public const uint SPI_GETPOWEROFFACTIVE = 0x0054;

        /// <summary>
        /// The sp i_ getpowerofftimeout.
        /// </summary>
        public const uint SPI_GETPOWEROFFTIMEOUT = 0x0050;

        /// <summary>
        /// The sp i_ getscreenreader.
        /// </summary>
        public const uint SPI_GETSCREENREADER = 0x0046;

        /// <summary>
        /// The sp i_ getscreensaveactive.
        /// </summary>
        public const uint SPI_GETSCREENSAVEACTIVE = 0x0010;

        /// <summary>
        /// The sp i_ getscreensaverrunning.
        /// </summary>
        public const uint SPI_GETSCREENSAVERRUNNING = 0x0072;

        /// <summary>
        /// The sp i_ getscreensavetimeout.
        /// </summary>
        public const uint SPI_GETSCREENSAVETIMEOUT = 0x000E;

        /// <summary>
        /// The sp i_ getselectionfade.
        /// </summary>
        public const uint SPI_GETSELECTIONFADE = 0x1014;

        /// <summary>
        /// The sp i_ getserialkeys.
        /// </summary>
        public const uint SPI_GETSERIALKEYS = 0x003E;

        /// <summary>
        /// The sp i_ getshowimeui.
        /// </summary>
        public const uint SPI_GETSHOWIMEUI = 0x006E;

        /// <summary>
        /// The sp i_ getshowsounds.
        /// </summary>
        public const uint SPI_GETSHOWSOUNDS = 0x0038;

        /// <summary>
        /// The sp i_ getsnaptodefbutton.
        /// </summary>
        public const uint SPI_GETSNAPTODEFBUTTON = 0x005F;

        /// <summary>
        /// The sp i_ getsoundsentry.
        /// </summary>
        public const uint SPI_GETSOUNDSENTRY = 0x0040;

        /// <summary>
        /// The sp i_ getstickykeys.
        /// </summary>
        public const uint SPI_GETSTICKYKEYS = 0x003A;

        /// <summary>
        /// The sp i_ gettogglekeys.
        /// </summary>
        public const uint SPI_GETTOGGLEKEYS = 0x0034;

        /// <summary>
        /// The sp i_ gettooltipanimation.
        /// </summary>
        public const uint SPI_GETTOOLTIPANIMATION = 0x1016;

        /// <summary>
        /// The sp i_ gettooltipfade.
        /// </summary>
        public const uint SPI_GETTOOLTIPFADE = 0x1018;

        /// <summary>
        /// The sp i_ getuieffects.
        /// </summary>
        public const uint SPI_GETUIEFFECTS = 0x103E;

        /// <summary>
        /// The sp i_ getwheelscrolllines.
        /// </summary>
        public const uint SPI_GETWHEELSCROLLLINES = 0x0068;

        /// <summary>
        /// The sp i_ getwindowsextension.
        /// </summary>
        public const uint SPI_GETWINDOWSEXTENSION = 0x005C;

        /// <summary>
        /// The sp i_ getworkarea.
        /// </summary>
        public const uint SPI_GETWORKAREA = 0x0030;

        /// <summary>
        /// The sp i_ iconhorizontalspacing.
        /// </summary>
        public const uint SPI_ICONHORIZONTALSPACING = 0x000D;

        /// <summary>
        /// The sp i_ iconverticalspacing.
        /// </summary>
        public const uint SPI_ICONVERTICALSPACING = 0x0018;

        /// <summary>
        /// The sp i_ langdriver.
        /// </summary>
        public const uint SPI_LANGDRIVER = 0x000C;

        /// <summary>
        /// The sp i_ screensaverrunning.
        /// </summary>
        public const uint SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING;

        /// <summary>
        /// The sp i_ setaccesstimeout.
        /// </summary>
        public const uint SPI_SETACCESSTIMEOUT = 0x003D;

        /// <summary>
        /// The sp i_ setactivewindowtracking.
        /// </summary>
        public const uint SPI_SETACTIVEWINDOWTRACKING = 0x1001;

        /// <summary>
        /// The sp i_ setactivewndtrktimeout.
        /// </summary>
        public const uint SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003;

        /// <summary>
        /// The sp i_ setactivewndtrkzorder.
        /// </summary>
        public const uint SPI_SETACTIVEWNDTRKZORDER = 0x100D;

        /// <summary>
        /// The sp i_ setanimation.
        /// </summary>
        public const uint SPI_SETANIMATION = 0x0049;

        /// <summary>
        /// The sp i_ setbeep.
        /// </summary>
        public const uint SPI_SETBEEP = 0x0002;

        /// <summary>
        /// The sp i_ setblocksendinputresets.
        /// </summary>
        public const uint SPI_SETBLOCKSENDINPUTRESETS = 0x1027;

        /// <summary>
        /// The sp i_ setborder.
        /// </summary>
        public const uint SPI_SETBORDER = 0x0006;

        /// <summary>
        /// The sp i_ setcaretwidth.
        /// </summary>
        public const uint SPI_SETCARETWIDTH = 0x2007;

        /// <summary>
        /// The sp i_ setcomboboxanimation.
        /// </summary>
        public const uint SPI_SETCOMBOBOXANIMATION = 0x1005;

        /// <summary>
        /// The sp i_ setcursors.
        /// </summary>
        public const uint SPI_SETCURSORS = 0x0057;

        /// <summary>
        /// The sp i_ setcursorshadow.
        /// </summary>
        public const uint SPI_SETCURSORSHADOW = 0x101B;

        /// <summary>
        /// The sp i_ setdefaultinputlang.
        /// </summary>
        public const uint SPI_SETDEFAULTINPUTLANG = 0x005A;

        /// <summary>
        /// The sp i_ setdeskpattern.
        /// </summary>
        public const uint SPI_SETDESKPATTERN = 0x0015;

        /// <summary>
        /// The sp i_ setdeskwallpaper.
        /// </summary>
        public const uint SPI_SETDESKWALLPAPER = 0x0014;

        /// <summary>
        /// The sp i_ setdoubleclicktime.
        /// </summary>
        public const uint SPI_SETDOUBLECLICKTIME = 0x0020;

        /// <summary>
        /// The sp i_ setdoubleclkheight.
        /// </summary>
        public const uint SPI_SETDOUBLECLKHEIGHT = 0x001E;

        /// <summary>
        /// The sp i_ setdoubleclkwidth.
        /// </summary>
        public const uint SPI_SETDOUBLECLKWIDTH = 0x001D;

        /// <summary>
        /// The sp i_ setdragfullwindows.
        /// </summary>
        public const uint SPI_SETDRAGFULLWINDOWS = 0x0025;

        /// <summary>
        /// The sp i_ setdragheight.
        /// </summary>
        public const uint SPI_SETDRAGHEIGHT = 0x004D;

        /// <summary>
        /// The sp i_ setdragwidth.
        /// </summary>
        public const uint SPI_SETDRAGWIDTH = 0x004C;

        /// <summary>
        /// The sp i_ setdropshadow.
        /// </summary>
        public const uint SPI_SETDROPSHADOW = 0x1025;

        /// <summary>
        /// The sp i_ setfasttaskswitch.
        /// </summary>
        public const uint SPI_SETFASTTASKSWITCH = 0x0024;

        /// <summary>
        /// The sp i_ setfilterkeys.
        /// </summary>
        public const uint SPI_SETFILTERKEYS = 0x0033;

        /// <summary>
        /// The sp i_ setflatmenu.
        /// </summary>
        public const uint SPI_SETFLATMENU = 0x1023;

        /// <summary>
        /// The sp i_ setfocusborderheight.
        /// </summary>
        public const uint SPI_SETFOCUSBORDERHEIGHT = 0x2011;

        /// <summary>
        /// The sp i_ setfocusborderwidth.
        /// </summary>
        public const uint SPI_SETFOCUSBORDERWIDTH = 0x200F;

        /// <summary>
        /// The sp i_ setfontsmoothing.
        /// </summary>
        public const uint SPI_SETFONTSMOOTHING = 0x004B;

        /// <summary>
        /// The sp i_ setfontsmoothingcontrast.
        /// </summary>
        public const uint SPI_SETFONTSMOOTHINGCONTRAST = 0x200D;

        /// <summary>
        /// The sp i_ setfontsmoothingorientation.
        /// </summary>
        public const uint SPI_SETFONTSMOOTHINGORIENTATION = 0x2013;

        /// <summary>
        /// The sp i_ setfontsmoothingtype.
        /// </summary>
        public const uint SPI_SETFONTSMOOTHINGTYPE = 0x200B;

        /// <summary>
        /// The sp i_ setforegroundflashcount.
        /// </summary>
        public const uint SPI_SETFOREGROUNDFLASHCOUNT = 0x2005;

        /// <summary>
        /// The sp i_ setforegroundlocktimeout.
        /// </summary>
        public const uint SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001;

        /// <summary>
        /// The sp i_ setgradientcaptions.
        /// </summary>
        public const uint SPI_SETGRADIENTCAPTIONS = 0x1009;

        /// <summary>
        /// The sp i_ setgridgranularity.
        /// </summary>
        public const uint SPI_SETGRIDGRANULARITY = 0x0013;

        /// <summary>
        /// The sp i_ sethandheld.
        /// </summary>
        public const uint SPI_SETHANDHELD = 0x004E;

        /// <summary>
        /// The sp i_ sethighcontrast.
        /// </summary>
        public const uint SPI_SETHIGHCONTRAST = 0x0043;

        /// <summary>
        /// The sp i_ sethottracking.
        /// </summary>
        public const uint SPI_SETHOTTRACKING = 0x100F;

        /// <summary>
        /// The sp i_ seticonmetrics.
        /// </summary>
        public const uint SPI_SETICONMETRICS = 0x002E;

        /// <summary>
        /// The sp i_ seticons.
        /// </summary>
        public const uint SPI_SETICONS = 0x0058;

        /// <summary>
        /// The sp i_ seticontitlelogfont.
        /// </summary>
        public const uint SPI_SETICONTITLELOGFONT = 0x0022;

        /// <summary>
        /// The sp i_ seticontitlewrap.
        /// </summary>
        public const uint SPI_SETICONTITLEWRAP = 0x001A;

        /// <summary>
        /// The sp i_ setkeyboardcues.
        /// </summary>
        public const uint SPI_SETKEYBOARDCUES = 0x100B;

        /// <summary>
        /// The sp i_ setkeyboarddelay.
        /// </summary>
        public const uint SPI_SETKEYBOARDDELAY = 0x0017;

        /// <summary>
        /// The sp i_ setkeyboardpref.
        /// </summary>
        public const uint SPI_SETKEYBOARDPREF = 0x0045;

        /// <summary>
        /// The sp i_ setkeyboardspeed.
        /// </summary>
        public const uint SPI_SETKEYBOARDSPEED = 0x000B;

        /// <summary>
        /// The sp i_ setlangtoggle.
        /// </summary>
        public const uint SPI_SETLANGTOGGLE = 0x005B;

        /// <summary>
        /// The sp i_ setlistboxsmoothscrolling.
        /// </summary>
        public const uint SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007;

        /// <summary>
        /// The sp i_ setlowpoweractive.
        /// </summary>
        public const uint SPI_SETLOWPOWERACTIVE = 0x0055;

        /// <summary>
        /// The sp i_ setlowpowertimeout.
        /// </summary>
        public const uint SPI_SETLOWPOWERTIMEOUT = 0x0051;

        /// <summary>
        /// The sp i_ setmenuanimation.
        /// </summary>
        public const uint SPI_SETMENUANIMATION = 0x1003;

        /// <summary>
        /// The sp i_ setmenudropalignment.
        /// </summary>
        public const uint SPI_SETMENUDROPALIGNMENT = 0x001C;

        /// <summary>
        /// The sp i_ setmenufade.
        /// </summary>
        public const uint SPI_SETMENUFADE = 0x1013;

        /// <summary>
        /// The sp i_ setmenushowdelay.
        /// </summary>
        public const uint SPI_SETMENUSHOWDELAY = 0x006B;

        /// <summary>
        /// The sp i_ setmenuunderlines.
        /// </summary>
        public const uint SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES;

        /// <summary>
        /// The sp i_ setminimizedmetrics.
        /// </summary>
        public const uint SPI_SETMINIMIZEDMETRICS = 0x002C;

        /// <summary>
        /// The sp i_ setmouse.
        /// </summary>
        public const uint SPI_SETMOUSE = 0x0004;

        /// <summary>
        /// The sp i_ setmousebuttonswap.
        /// </summary>
        public const uint SPI_SETMOUSEBUTTONSWAP = 0x0021;

        /// <summary>
        /// The sp i_ setmouseclicklock.
        /// </summary>
        public const uint SPI_SETMOUSECLICKLOCK = 0x101F;

        /// <summary>
        /// The sp i_ setmouseclicklocktime.
        /// </summary>
        public const uint SPI_SETMOUSECLICKLOCKTIME = 0x2009;

        /// <summary>
        /// The sp i_ setmousehoverheight.
        /// </summary>
        public const uint SPI_SETMOUSEHOVERHEIGHT = 0x0065;

        /// <summary>
        /// The sp i_ setmousehovertime.
        /// </summary>
        public const uint SPI_SETMOUSEHOVERTIME = 0x0067;

        /// <summary>
        /// The sp i_ setmousehoverwidth.
        /// </summary>
        public const uint SPI_SETMOUSEHOVERWIDTH = 0x0063;

        /// <summary>
        /// The sp i_ setmousekeys.
        /// </summary>
        public const uint SPI_SETMOUSEKEYS = 0x0037;

        /// <summary>
        /// The sp i_ setmousesonar.
        /// </summary>
        public const uint SPI_SETMOUSESONAR = 0x101D;

        /// <summary>
        /// The sp i_ setmousespeed.
        /// </summary>
        public const uint SPI_SETMOUSESPEED = 0x0071;

        /// <summary>
        /// The sp i_ setmousetrails.
        /// </summary>
        public const uint SPI_SETMOUSETRAILS = 0x005D;

        /// <summary>
        /// The sp i_ setmousevanish.
        /// </summary>
        public const uint SPI_SETMOUSEVANISH = 0x1021;

        /// <summary>
        /// The sp i_ setnonclientmetrics.
        /// </summary>
        public const uint SPI_SETNONCLIENTMETRICS = 0x002A;

        /// <summary>
        /// The sp i_ setpenwindows.
        /// </summary>
        public const uint SPI_SETPENWINDOWS = 0x0031;

        /// <summary>
        /// The sp i_ setpoweroffactive.
        /// </summary>
        public const uint SPI_SETPOWEROFFACTIVE = 0x0056;

        /// <summary>
        /// The sp i_ setpowerofftimeout.
        /// </summary>
        public const uint SPI_SETPOWEROFFTIMEOUT = 0x0052;

        /// <summary>
        /// The sp i_ setscreenreader.
        /// </summary>
        public const uint SPI_SETSCREENREADER = 0x0047;

        /// <summary>
        /// The sp i_ setscreensaveactive.
        /// </summary>
        public const uint SPI_SETSCREENSAVEACTIVE = 0x0011;

        /// <summary>
        /// The sp i_ setscreensaverrunning.
        /// </summary>
        public const uint SPI_SETSCREENSAVERRUNNING = 0x0061;

        /// <summary>
        /// The sp i_ setscreensavetimeout.
        /// </summary>
        public const uint SPI_SETSCREENSAVETIMEOUT = 0x000F;

        /// <summary>
        /// The sp i_ setselectionfade.
        /// </summary>
        public const uint SPI_SETSELECTIONFADE = 0x1015;

        /// <summary>
        /// The sp i_ setserialkeys.
        /// </summary>
        public const uint SPI_SETSERIALKEYS = 0x003F;

        /// <summary>
        /// The sp i_ setshowimeui.
        /// </summary>
        public const uint SPI_SETSHOWIMEUI = 0x006F;

        /// <summary>
        /// The sp i_ setshowsounds.
        /// </summary>
        public const uint SPI_SETSHOWSOUNDS = 0x0039;

        /// <summary>
        /// The sp i_ setsnaptodefbutton.
        /// </summary>
        public const uint SPI_SETSNAPTODEFBUTTON = 0x0060;

        /// <summary>
        /// The sp i_ setsoundsentry.
        /// </summary>
        public const uint SPI_SETSOUNDSENTRY = 0x0041;

        /// <summary>
        /// The sp i_ setstickykeys.
        /// </summary>
        public const uint SPI_SETSTICKYKEYS = 0x003B;

        /// <summary>
        /// The sp i_ settogglekeys.
        /// </summary>
        public const uint SPI_SETTOGGLEKEYS = 0x0035;

        /// <summary>
        /// The sp i_ settooltipanimation.
        /// </summary>
        public const uint SPI_SETTOOLTIPANIMATION = 0x1017;

        /// <summary>
        /// The sp i_ settooltipfade.
        /// </summary>
        public const uint SPI_SETTOOLTIPFADE = 0x1019;

        /// <summary>
        /// The sp i_ setuieffects.
        /// </summary>
        public const uint SPI_SETUIEFFECTS = 0x103F;

        /// <summary>
        /// The sp i_ setwheelscrolllines.
        /// </summary>
        public const uint SPI_SETWHEELSCROLLLINES = 0x0069;

        /// <summary>
        /// The sp i_ setworkarea.
        /// </summary>
        public const uint SPI_SETWORKAREA = 0x002F;

        /// <summary>
        /// The srcand.
        /// </summary>
        public const uint SRCAND = 0x008800C6; /* dest = source AND dest */

        /// <summary>
        /// The srccopy.
        /// </summary>
        public const uint SRCCOPY = 0x00CC0020; /* dest = source  */

        /// <summary>
        /// The srcerase.
        /// </summary>
        public const uint SRCERASE = 0x00440328; /* dest = source AND (NOT dest ) */

        /// <summary>
        /// The srcinvert.
        /// </summary>
        public const uint SRCINVERT = 0x00660046; /* dest = source XOR dest */

        /// <summary>
        /// The srcpaint.
        /// </summary>
        public const uint SRCPAINT = 0x00EE0086; /* dest = source OR dest */

        /// <summary>
        /// The standar d_ right s_ execute.
        /// </summary>
        public const uint STANDARD_RIGHTS_EXECUTE = READ_CONTROL;

        /// <summary>
        /// The standar d_ right s_ read.
        /// </summary>
        public const uint STANDARD_RIGHTS_READ = READ_CONTROL;

        /// <summary>
        /// The standar d_ right s_ required.
        /// </summary>
        public const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;

        /// <summary>
        /// The standar d_ right s_ write.
        /// </summary>
        public const uint STANDARD_RIGHTS_WRITE = READ_CONTROL;

        /// <summary>
        /// The statu s_ abandone d_ wai t_0.
        /// </summary>
        public const uint STATUS_ABANDONED_WAIT_0 = 0x80;

        /// <summary>
        /// The statu s_ use r_ apc.
        /// </summary>
        public const uint STATUS_USER_APC = 0x000000C0;

        /// <summary>
        /// The statu s_ wai t_0.
        /// </summary>
        public const uint STATUS_WAIT_0 = 0;

        /// <summary>
        /// The s w_ forceminimize.
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;

        /// <summary>
        /// The s w_ hide.
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// The s w_ max.
        /// </summary>
        public const int SW_MAX = 11;

        /// <summary>
        /// The s w_ maximize.
        /// </summary>
        public const int SW_MAXIMIZE = 3;

        /// <summary>
        /// The s w_ minimize.
        /// </summary>
        public const int SW_MINIMIZE = 6;

        /// <summary>
        /// The s w_ normal.
        /// </summary>
        public const int SW_NORMAL = 1;

        /// <summary>
        /// The s w_ restore.
        /// </summary>
        public const int SW_RESTORE = 9;

        /// <summary>
        /// The s w_ show.
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// The s w_ showdefault.
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// The s w_ showmaximized.
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// The s w_ showminimized.
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// The s w_ showminnoactive.
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// The s w_ showna.
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// The s w_ shownoactivate.
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// The s w_ shownormal.
        /// </summary>
        public const int SW_SHOWNORMAL = 1;

        /// <summary>
        /// The symbo l_ charset.
        /// </summary>
        public const uint SYMBOL_CHARSET = 2;

        /// <summary>
        /// The synchronize.
        /// </summary>
        public const uint SYNCHRONIZE = 0x00100000;

        /// <summary>
        /// The s_ false.
        /// </summary>
        public const int S_FALSE = 1;

        /// <summary>
        /// The s_ ok.
        /// </summary>
        public const int S_OK = 0;

        /// <summary>
        /// The tha i_ charset.
        /// </summary>
        public const uint THAI_CHARSET = 222;

        /// <summary>
        /// The threa d_ mod e_ backgroun d_ begin.
        /// </summary>
        public const int THREAD_MODE_BACKGROUND_BEGIN = 0x10000;

        /// <summary>
        /// The threa d_ mod e_ backgroun d_ end.
        /// </summary>
        public const int THREAD_MODE_BACKGROUND_END = 0x20000;

        /// <summary>
        /// The toke n_ adjus t_ default.
        /// </summary>
        public const uint TOKEN_ADJUST_DEFAULT = 0x0080;

        /// <summary>
        /// The toke n_ adjus t_ groups.
        /// </summary>
        public const uint TOKEN_ADJUST_GROUPS = 0x0040;

        /// <summary>
        /// The toke n_ adjus t_ privileges.
        /// </summary>
        public const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;

        /// <summary>
        /// The toke n_ adjus t_ sessionid.
        /// </summary>
        public const uint TOKEN_ADJUST_SESSIONID = 0x0100;

        /// <summary>
        /// The toke n_ al l_ access.
        /// </summary>
        public const uint TOKEN_ALL_ACCESS = TOKEN_ALL_ACCESS_P | TOKEN_ADJUST_SESSIONID;

        /// <summary>
        /// The toke n_ al l_ acces s_ p.
        /// </summary>
        public const uint TOKEN_ALL_ACCESS_P =
            STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY | TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY
            | TOKEN_QUERY_SOURCE | TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT;

        /// <summary>
        /// The toke n_ assig n_ primary.
        /// </summary>
        public const uint TOKEN_ASSIGN_PRIMARY = 0x0001;

        /// <summary>
        /// The toke n_ duplicate.
        /// </summary>
        public const uint TOKEN_DUPLICATE = 0x0002;

        /// <summary>
        /// The toke n_ execute.
        /// </summary>
        public const uint TOKEN_EXECUTE = STANDARD_RIGHTS_EXECUTE;

        /// <summary>
        /// The toke n_ impersonate.
        /// </summary>
        public const uint TOKEN_IMPERSONATE = 0x0004;

        /// <summary>
        /// The toke n_ query.
        /// </summary>
        public const uint TOKEN_QUERY = 0x0008;

        /// <summary>
        /// The toke n_ quer y_ source.
        /// </summary>
        public const uint TOKEN_QUERY_SOURCE = 0x0010;

        /// <summary>
        /// The toke n_ read.
        /// </summary>
        public const uint TOKEN_READ = STANDARD_RIGHTS_READ | TOKEN_QUERY;

        /// <summary>
        /// The toke n_ write.
        /// </summary>
        public const uint TOKEN_WRITE =
            STANDARD_RIGHTS_WRITE | TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT;

        /// <summary>
        /// The truncat e_ existing.
        /// </summary>
        public const uint TRUNCATE_EXISTING = 5;

        /// <summary>
        /// The turkis h_ charset.
        /// </summary>
        public const uint TURKISH_CHARSET = 162;

        /// <summary>
        /// The variabl e_ pitch.
        /// </summary>
        public const uint VARIABLE_PITCH = 2;

        /// <summary>
        /// The ve r_ and.
        /// </summary>
        public const byte VER_AND = 6;

        /// <summary>
        /// The ve r_ buildnumber.
        /// </summary>
        public const uint VER_BUILDNUMBER = 0x0000004;

        /// <summary>
        /// The ve r_ conditio n_ mask.
        /// </summary>
        public const uint VER_CONDITION_MASK = 7;

        /// <summary>
        /// The ve r_ equal.
        /// </summary>
        public const byte VER_EQUAL = 1;

        /// <summary>
        /// The ve r_ greater.
        /// </summary>
        public const byte VER_GREATER = 2;

        /// <summary>
        /// The ve r_ greate r_ equal.
        /// </summary>
        public const byte VER_GREATER_EQUAL = 3;

        /// <summary>
        /// The ve r_ less.
        /// </summary>
        public const byte VER_LESS = 4;

        /// <summary>
        /// The ve r_ les s_ equal.
        /// </summary>
        public const byte VER_LESS_EQUAL = 5;

        /// <summary>
        /// The ve r_ majorversion.
        /// </summary>
        public const uint VER_MAJORVERSION = 0x0000002;

        /// <summary>
        /// The ve r_ minorversion.
        /// </summary>
        public const uint VER_MINORVERSION = 0x0000001;

        /// <summary>
        /// The ve r_ n t_ domai n_ controller.
        /// </summary>
        public const uint VER_NT_DOMAIN_CONTROLLER = 0x0000002;

        /// <summary>
        /// The ve r_ n t_ server.
        /// </summary>
        public const uint VER_NT_SERVER = 0x0000003;

        /// <summary>
        /// The ve r_ n t_ workstation.
        /// </summary>
        public const uint VER_NT_WORKSTATION = 0x0000001;

        /// <summary>
        /// The ve r_ nu m_ bit s_ pe r_ conditio n_ mask.
        /// </summary>
        public const uint VER_NUM_BITS_PER_CONDITION_MASK = 3;

        /// <summary>
        /// The ve r_ or.
        /// </summary>
        public const byte VER_OR = 7;

        /// <summary>
        /// The ve r_ platformid.
        /// </summary>
        public const uint VER_PLATFORMID = 0x0000008;

        /// <summary>
        /// The ve r_ platfor m_ wi n 32_ nt.
        /// </summary>
        public const uint VER_PLATFORM_WIN32_NT = 2;

        /// <summary>
        /// The ve r_ platfor m_ wi n 32_ windows.
        /// </summary>
        public const uint VER_PLATFORM_WIN32_WINDOWS = 1;

        /// <summary>
        /// The ve r_ platfor m_ wi n 32 s.
        /// </summary>
        public const uint VER_PLATFORM_WIN32s = 0;

        /// <summary>
        /// The ve r_ produc t_ type.
        /// </summary>
        public const uint VER_PRODUCT_TYPE = 0x0000080;

        /// <summary>
        /// The ve r_ servicepackmajor.
        /// </summary>
        public const uint VER_SERVICEPACKMAJOR = 0x0000020;

        /// <summary>
        /// The ve r_ servicepackminor.
        /// </summary>
        public const uint VER_SERVICEPACKMINOR = 0x0000010;

        /// <summary>
        /// The ve r_ suitename.
        /// </summary>
        public const uint VER_SUITENAME = 0x0000040;

        /// <summary>
        /// The vietnames e_ charset.
        /// </summary>
        public const uint VIETNAMESE_CHARSET = 163;

        /// <summary>
        /// The wai t_ abandoned.
        /// </summary>
        public const uint WAIT_ABANDONED = STATUS_ABANDONED_WAIT_0 + 0;

        /// <summary>
        /// The wai t_ abandone d_0.
        /// </summary>
        public const uint WAIT_ABANDONED_0 = STATUS_ABANDONED_WAIT_0 + 0;

        /// <summary>
        /// The wai t_ failed.
        /// </summary>
        public const uint WAIT_FAILED = 0xffffffff;

        /// <summary>
        /// The wai t_ i o_ completion.
        /// </summary>
        public const uint WAIT_IO_COMPLETION = STATUS_USER_APC;

        /// <summary>
        /// The wai t_ objec t_0.
        /// </summary>
        public const uint WAIT_OBJECT_0 = STATUS_WAIT_0 + 0;

        /// <summary>
        /// The wai t_ timeout.
        /// </summary>
        public const uint WAIT_TIMEOUT = 258;

        /// <summary>
        /// The whiteness.
        /// </summary>
        public const uint WHITENESS = 0x00FF0062; /* dest = WHITE  */

        /// <summary>
        /// The whiteonblack.
        /// </summary>
        public const int WHITEONBLACK = 2;

        /// <summary>
        /// The w m_ activate.
        /// </summary>
        public const int WM_ACTIVATE = 0x006;

        /// <summary>
        /// The w m_ activateapp.
        /// </summary>
        public const int WM_ACTIVATEAPP = 0x01C;

        /// <summary>
        /// The w m_ command.
        /// </summary>
        public const uint WM_COMMAND = 0x111;

        /// <summary>
        /// The w m_ copydata.
        /// </summary>
        public const uint WM_COPYDATA = 0x004a;

        /// <summary>
        /// The w m_ hscroll.
        /// </summary>
        public const int WM_HSCROLL = 0x114;

        /// <summary>
        /// The w m_ mouseactivate.
        /// </summary>
        public const uint WM_MOUSEACTIVATE = 0x21;

        /// <summary>
        /// The w m_ moving.
        /// </summary>
        public const int WM_MOVING = 0x0216;

        /// <summary>
        /// The w m_ ncactivate.
        /// </summary>
        public const int WM_NCACTIVATE = 0x086;

        /// <summary>
        /// The w m_ ncpaint.
        /// </summary>
        public const int WM_NCPAINT = 0x0085;

        /// <summary>
        /// The w m_ paint.
        /// </summary>
        public const int WM_PAINT = 0x000f;

        /// <summary>
        /// The w m_ queryendsession.
        /// </summary>
        public const int WM_QUERYENDSESSION = 0x0011;

        /// <summary>
        /// The w m_ setfocus.
        /// </summary>
        public const int WM_SETFOCUS = 7;

        /// <summary>
        /// The w m_ setredraw.
        /// </summary>
        public const int WM_SETREDRAW = 0x000B;

        /// <summary>
        /// The w m_ user.
        /// </summary>
        public const int WM_USER = 0x400;

        /// <summary>
        /// The w m_ vscroll.
        /// </summary>
        public const int WM_VSCROLL = 0x115;

        /// <summary>
        /// The w m_ wtssessio n_ change.
        /// </summary>
        public const int WM_WTSSESSION_CHANGE = 0x2b1;

        /// <summary>
        /// The w s_ e x_ layered.
        /// </summary>
        public const uint WS_EX_LAYERED = 0x00080000;

        /// <summary>
        /// The w s_ hscroll.
        /// </summary>
        public const uint WS_HSCROLL = 0x00100000;

        /// <summary>
        /// The w s_ vscroll.
        /// </summary>
        public const uint WS_VSCROLL = 0x00200000;

        /// <summary>
        /// The wt d_ cach e_ onl y_ ur l_ retrieval.
        /// </summary>
        public const uint WTD_CACHE_ONLY_URL_RETRIEVAL = 0x00001000;

        /// <summary>
        /// The wt d_ choic e_ blob.
        /// </summary>
        public const uint WTD_CHOICE_BLOB = 3;

        /// <summary>
        /// The wt d_ choic e_ catalog.
        /// </summary>
        public const uint WTD_CHOICE_CATALOG = 2;

        /// <summary>
        /// The wt d_ choic e_ cert.
        /// </summary>
        public const uint WTD_CHOICE_CERT = 5;

        /// <summary>
        /// The wt d_ choic e_ file.
        /// </summary>
        public const uint WTD_CHOICE_FILE = 1;

        /// <summary>
        /// The wt d_ choic e_ signer.
        /// </summary>
        public const uint WTD_CHOICE_SIGNER = 4;

        /// <summary>
        /// The wt d_ has h_ onl y_ flag.
        /// </summary>
        public const uint WTD_HASH_ONLY_FLAG = 0x00000200;

        /// <summary>
        /// The wt d_ lifetim e_ signin g_ flag.
        /// </summary>
        public const uint WTD_LIFETIME_SIGNING_FLAG = 0x00000800;

        /// <summary>
        /// The wt d_ n o_ i e 4_ chai n_ flag.
        /// </summary>
        public const uint WTD_NO_IE4_CHAIN_FLAG = 0x00000002;

        /// <summary>
        /// The wt d_ n o_ polic y_ usag e_ flag.
        /// </summary>
        public const uint WTD_NO_POLICY_USAGE_FLAG = 0x00000004;

        /// <summary>
        /// The wt d_ pro v_ flag s_ mask.
        /// </summary>
        public const uint WTD_PROV_FLAGS_MASK = 0x0000FFFF;

        /// <summary>
        /// The wt d_ revocatio n_ chec k_ chain.
        /// </summary>
        public const uint WTD_REVOCATION_CHECK_CHAIN = 0x00000040;

        /// <summary>
        /// The wt d_ revocatio n_ chec k_ chai n_ exclud e_ root.
        /// </summary>
        public const uint WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 0x00000080;

        /// <summary>
        /// The wt d_ revocatio n_ chec k_ en d_ cert.
        /// </summary>
        public const uint WTD_REVOCATION_CHECK_END_CERT = 0x00000020;

        /// <summary>
        /// The wt d_ revocatio n_ chec k_ none.
        /// </summary>
        public const uint WTD_REVOCATION_CHECK_NONE = 0x00000010;

        /// <summary>
        /// The wt d_ revok e_ none.
        /// </summary>
        public const uint WTD_REVOKE_NONE = 0;

        /// <summary>
        /// The wt d_ revok e_ wholechain.
        /// </summary>
        public const uint WTD_REVOKE_WHOLECHAIN = 1;

        /// <summary>
        /// The wt d_ safe r_ flag.
        /// </summary>
        public const uint WTD_SAFER_FLAG = 0x00000100;

        /// <summary>
        /// The wt d_ stateactio n_ aut o_ cache.
        /// </summary>
        public const uint WTD_STATEACTION_AUTO_CACHE = 3;

        /// <summary>
        /// The wt d_ stateactio n_ aut o_ cach e_ flush.
        /// </summary>
        public const uint WTD_STATEACTION_AUTO_CACHE_FLUSH = 4;

        /// <summary>
        /// The wt d_ stateactio n_ close.
        /// </summary>
        public const uint WTD_STATEACTION_CLOSE = 2;

        /// <summary>
        /// The wt d_ stateactio n_ ignore.
        /// </summary>
        public const uint WTD_STATEACTION_IGNORE = 0;

        /// <summary>
        /// The wt d_ stateactio n_ verify.
        /// </summary>
        public const uint WTD_STATEACTION_VERIFY = 1;

        /// <summary>
        /// The wt d_ u i_ all.
        /// </summary>
        public const uint WTD_UI_ALL = 1;

        /// <summary>
        /// The wt d_ u i_ nobad.
        /// </summary>
        public const uint WTD_UI_NOBAD = 3;

        /// <summary>
        /// The wt d_ u i_ nogood.
        /// </summary>
        public const uint WTD_UI_NOGOOD = 4;

        /// <summary>
        /// The wt d_ u i_ none.
        /// </summary>
        public const uint WTD_UI_NONE = 2;

        /// <summary>
        /// The wt d_ us e_ defaul t_ osve r_ check.
        /// </summary>
        public const uint WTD_USE_DEFAULT_OSVER_CHECK = 0x00000400;

        /// <summary>
        /// The wt d_ us e_ i e 4_ trus t_ flag.
        /// </summary>
        public const uint WTD_USE_IE4_TRUST_FLAG = 0x00000001;

        #endregion

        #region Static Fields

        /// <summary>
        /// The fsct l_ se t_ compression.
        /// </summary>
        public static readonly uint FSCTL_SET_COMPRESSION = CTL_CODE(
            FILE_DEVICE_FILE_SYSTEM, 
            16, 
            METHOD_BUFFERED, 
            FILE_READ_DATA | FILE_WRITE_DATA);

        /// <summary>
        /// The invali d_ handl e_ value.
        /// </summary>
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        /// <summary>
        /// The compressio n_ forma t_ default.
        /// </summary>
        public static ushort COMPRESSION_FORMAT_DEFAULT = 1;

        #endregion

        #region Enums

        /// <summary>
        /// The cdcontrolstate.
        /// </summary>
        public enum CDCONTROLSTATE
        {
            /// <summary>
            /// The cdc s_ inactive.
            /// </summary>
            CDCS_INACTIVE = 0x00000000, 

            /// <summary>
            /// The cdc s_ enabled.
            /// </summary>
            CDCS_ENABLED = 0x00000001, 

            /// <summary>
            /// The cdc s_ visible.
            /// </summary>
            CDCS_VISIBLE = 0x00000002
        }

        /// <summary>
        /// The fdap.
        /// </summary>
        public enum FDAP
        {
            /// <summary>
            /// The fda p_ bottom.
            /// </summary>
            FDAP_BOTTOM = 0x00000000, 

            /// <summary>
            /// The fda p_ top.
            /// </summary>
            FDAP_TOP = 0x00000001, 
        }

        /// <summary>
        /// The fd e_ overwrit e_ response.
        /// </summary>
        public enum FDE_OVERWRITE_RESPONSE
        {
            /// <summary>
            /// The fdeo r_ default.
            /// </summary>
            FDEOR_DEFAULT = 0x00000000, 

            /// <summary>
            /// The fdeo r_ accept.
            /// </summary>
            FDEOR_ACCEPT = 0x00000001, 

            /// <summary>
            /// The fdeo r_ refuse.
            /// </summary>
            FDEOR_REFUSE = 0x00000002
        }

        /// <summary>
        /// The fd e_ shareviolatio n_ response.
        /// </summary>
        public enum FDE_SHAREVIOLATION_RESPONSE
        {
            /// <summary>
            /// The fdesv r_ default.
            /// </summary>
            FDESVR_DEFAULT = 0x00000000, 

            /// <summary>
            /// The fdesv r_ accept.
            /// </summary>
            FDESVR_ACCEPT = 0x00000001, 

            /// <summary>
            /// The fdesv r_ refuse.
            /// </summary>
            FDESVR_REFUSE = 0x00000002
        }

        /// <summary>
        /// The fff p_ mode.
        /// </summary>
        public enum FFFP_MODE
        {
            /// <summary>
            /// The fff p_ exactmatch.
            /// </summary>
            FFFP_EXACTMATCH, 

            /// <summary>
            /// The fff p_ nearestparentmatch.
            /// </summary>
            FFFP_NEARESTPARENTMATCH
        }

        /// <summary>
        /// The fof.
        /// </summary>
        public enum FOF : uint
        {
            /// <summary>
            /// The fo f_ multidestfiles.
            /// </summary>
            FOF_MULTIDESTFILES = 0x0001, 

            /// <summary>
            /// The fo f_ confirmmouse.
            /// </summary>
            FOF_CONFIRMMOUSE = 0x0002, 

            /// <summary>
            /// The fo f_ silent.
            /// </summary>
            FOF_SILENT = 0x0004, // don't display progress UI (confirm prompts may be displayed still)

            /// <summary>
            /// The fo f_ renameoncollision.
            /// </summary>
            FOF_RENAMEONCOLLISION = 0x0008, // automatically rename the source files to avoid the collisions

            /// <summary>
            /// The fo f_ noconfirmation.
            /// </summary>
            FOF_NOCONFIRMATION = 0x0010, 

            // don't display confirmation UI, assume "yes" for cases that can be bypassed, "no" for those that can not

            /// <summary>
            /// The fo f_ wantmappinghandle.
            /// </summary>
            FOF_WANTMAPPINGHANDLE = 0x0020, // Fill in SHFILEOPSTRUCT.hNameMappings
            // Must be freed using SHFreeNameMappings
            /// <summary>
            /// The fo f_ allowundo.
            /// </summary>
            FOF_ALLOWUNDO = 0x0040, // enable undo including Recycle behavior for IFileOperation::Delete()

            /// <summary>
            /// The fo f_ filesonly.
            /// </summary>
            FOF_FILESONLY = 0x0080, 

            // only operate on the files (non folders), both files and folders are assumed without this

            /// <summary>
            /// The fo f_ simpleprogress.
            /// </summary>
            FOF_SIMPLEPROGRESS = 0x0100, // means don't show names of files

            /// <summary>
            /// The fo f_ noconfirmmkdir.
            /// </summary>
            FOF_NOCONFIRMMKDIR = 0x0200, 

            // don't dispplay confirmatino UI before making any needed directories, assume "Yes" in these cases

            /// <summary>
            /// The fo f_ noerrorui.
            /// </summary>
            FOF_NOERRORUI = 0x0400, // don't put up error UI, other UI may be displayed, progress, confirmations

            /// <summary>
            /// The fo f_ nocopysecurityattribs.
            /// </summary>
            FOF_NOCOPYSECURITYATTRIBS = 0x0800, // dont copy file security attributes (ACLs)

            /// <summary>
            /// The fo f_ norecursion.
            /// </summary>
            FOF_NORECURSION = 0x1000, // don't recurse into directories for operations that would recurse

            /// <summary>
            /// The fo f_ n o_ connecte d_ elements.
            /// </summary>
            FOF_NO_CONNECTED_ELEMENTS = 0x2000, 

            // don't operate on connected elements ("xxx_files" folders that go with .htm files)

            /// <summary>
            /// The fo f_ wantnukewarning.
            /// </summary>
            FOF_WANTNUKEWARNING = 0x4000, 

            // during delete operation, warn if nuking instead of recycling (partially overrides FOF_NOCONFIRMATION)

            /// <summary>
            /// The fo f_ norecursereparse.
            /// </summary>
            FOF_NORECURSEREPARSE = 0x8000, 

            // deprecated; the operations engine always does the right thing on FolderLink objects (symlinks, reparse points, folder shortcuts)

            /// <summary>
            /// The fo f_ n o_ ui.
            /// </summary>
            FOF_NO_UI = FOF_SILENT | FOF_NOCONFIRMATION | FOF_NOERRORUI | FOF_NOCONFIRMMKDIR, 

            // don't display any UI at all

            /// <summary>
            /// The fof x_ noskipjunctions.
            /// </summary>
            FOFX_NOSKIPJUNCTIONS = 0x00010000, // Don't avoid binding to junctions (like Task folder, Recycle-Bin)

            /// <summary>
            /// The fof x_ preferhardlink.
            /// </summary>
            FOFX_PREFERHARDLINK = 0x00020000, // Create hard link if possible

            /// <summary>
            /// The fof x_ showelevationprompt.
            /// </summary>
            FOFX_SHOWELEVATIONPROMPT = 0x00040000, 

            // Show elevation prompts when error UI is disabled (use with FOF_NOERRORUI)

            /// <summary>
            /// The fof x_ earlyfailure.
            /// </summary>
            FOFX_EARLYFAILURE = 0x00100000, 

            // Fail operation as soon as a single error occurs rather than trying to process other items (applies only when using FOF_NOERRORUI)

            /// <summary>
            /// The fof x_ preservefileextensions.
            /// </summary>
            FOFX_PRESERVEFILEEXTENSIONS = 0x00200000, 

            // Rename collisions preserve file extns (use with FOF_RENAMEONCOLLISION)

            /// <summary>
            /// The fof x_ keepnewerfile.
            /// </summary>
            FOFX_KEEPNEWERFILE = 0x00400000, // Keep newer file on naming conflicts

            /// <summary>
            /// The fof x_ nocopyhooks.
            /// </summary>
            FOFX_NOCOPYHOOKS = 0x00800000, // Don't use copy hooks

            /// <summary>
            /// The fof x_ nominimizebox.
            /// </summary>
            FOFX_NOMINIMIZEBOX = 0x01000000, // Don't allow minimizing the progress dialog

            /// <summary>
            /// The fof x_ moveaclsacrossvolumes.
            /// </summary>
            FOFX_MOVEACLSACROSSVOLUMES = 0x02000000, 

            // Copy security information when performing a cross-volume move operation

            /// <summary>
            /// The fof x_ dontdisplaysourcepath.
            /// </summary>
            FOFX_DONTDISPLAYSOURCEPATH = 0x04000000, // Don't display the path of source file in progress dialog

            /// <summary>
            /// The fof x_ dontdisplaydestpath.
            /// </summary>
            FOFX_DONTDISPLAYDESTPATH = 0x08000000, // Don't display the path of destination file in progress dialog
        }

        /// <summary>
        /// The fos.
        /// </summary>
        [Flags]
        public enum FOS : uint
        {
            /// <summary>
            /// The fo s_ overwriteprompt.
            /// </summary>
            FOS_OVERWRITEPROMPT = 0x00000002, 

            /// <summary>
            /// The fo s_ strictfiletypes.
            /// </summary>
            FOS_STRICTFILETYPES = 0x00000004, 

            /// <summary>
            /// The fo s_ nochangedir.
            /// </summary>
            FOS_NOCHANGEDIR = 0x00000008, 

            /// <summary>
            /// The fo s_ pickfolders.
            /// </summary>
            FOS_PICKFOLDERS = 0x00000020, 

            /// <summary>
            /// The fo s_ forcefilesystem.
            /// </summary>
            FOS_FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.

            /// <summary>
            /// The fo s_ allnonstorageitems.
            /// </summary>
            FOS_ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.

            /// <summary>
            /// The fo s_ novalidate.
            /// </summary>
            FOS_NOVALIDATE = 0x00000100, 

            /// <summary>
            /// The fo s_ allowmultiselect.
            /// </summary>
            FOS_ALLOWMULTISELECT = 0x00000200, 

            /// <summary>
            /// The fo s_ pathmustexist.
            /// </summary>
            FOS_PATHMUSTEXIST = 0x00000800, 

            /// <summary>
            /// The fo s_ filemustexist.
            /// </summary>
            FOS_FILEMUSTEXIST = 0x00001000, 

            /// <summary>
            /// The fo s_ createprompt.
            /// </summary>
            FOS_CREATEPROMPT = 0x00002000, 

            /// <summary>
            /// The fo s_ shareaware.
            /// </summary>
            FOS_SHAREAWARE = 0x00004000, 

            /// <summary>
            /// The fo s_ noreadonlyreturn.
            /// </summary>
            FOS_NOREADONLYRETURN = 0x00008000, 

            /// <summary>
            /// The fo s_ notestfilecreate.
            /// </summary>
            FOS_NOTESTFILECREATE = 0x00010000, 

            /// <summary>
            /// The fo s_ hidemruplaces.
            /// </summary>
            FOS_HIDEMRUPLACES = 0x00020000, 

            /// <summary>
            /// The fo s_ hidepinnedplaces.
            /// </summary>
            FOS_HIDEPINNEDPLACES = 0x00040000, 

            /// <summary>
            /// The fo s_ nodereferencelinks.
            /// </summary>
            FOS_NODEREFERENCELINKS = 0x00100000, 

            /// <summary>
            /// The fo s_ dontaddtorecent.
            /// </summary>
            FOS_DONTADDTORECENT = 0x02000000, 

            /// <summary>
            /// The fo s_ forceshowhidden.
            /// </summary>
            FOS_FORCESHOWHIDDEN = 0x10000000, 

            /// <summary>
            /// The fo s_ defaultnominimode.
            /// </summary>
            FOS_DEFAULTNOMINIMODE = 0x20000000
        }

        /// <summary>
        /// The k f_ category.
        /// </summary>
        public enum KF_CATEGORY
        {
            /// <summary>
            /// The k f_ categor y_ virtual.
            /// </summary>
            KF_CATEGORY_VIRTUAL = 0x00000001, 

            /// <summary>
            /// The k f_ categor y_ fixed.
            /// </summary>
            KF_CATEGORY_FIXED = 0x00000002, 

            /// <summary>
            /// The k f_ categor y_ common.
            /// </summary>
            KF_CATEGORY_COMMON = 0x00000003, 

            /// <summary>
            /// The k f_ categor y_ peruser.
            /// </summary>
            KF_CATEGORY_PERUSER = 0x00000004
        }

        /// <summary>
        /// The k f_ definitio n_ flags.
        /// </summary>
        [Flags]
        public enum KF_DEFINITION_FLAGS
        {
            /// <summary>
            /// The kfd f_ personalize.
            /// </summary>
            KFDF_PERSONALIZE = 0x00000001, 

            /// <summary>
            /// The kfd f_ loca l_ redirec t_ only.
            /// </summary>
            KFDF_LOCAL_REDIRECT_ONLY = 0x00000002, 

            /// <summary>
            /// The kfd f_ roamable.
            /// </summary>
            KFDF_ROAMABLE = 0x00000004, 
        }

        /// <summary>
        /// The securit y_ impersonatio n_ level.
        /// </summary>
        public enum SECURITY_IMPERSONATION_LEVEL
        {
            /// <summary>
            /// The security anonymous.
            /// </summary>
            SecurityAnonymous = 0, 

            /// <summary>
            /// The security identification.
            /// </summary>
            SecurityIdentification = 1, 

            /// <summary>
            /// The security impersonation.
            /// </summary>
            SecurityImpersonation = 2, 

            /// <summary>
            /// The security delegation.
            /// </summary>
            SecurityDelegation = 3
        }

        /// <summary>
        /// The sfgao.
        /// </summary>
        [Flags]
        public enum SFGAO : uint
        {
            /// <summary>
            /// The sfga o_ cancopy.
            /// </summary>
            SFGAO_CANCOPY = DROPEFFECT_COPY, // Objects can be copied (0x1)

            /// <summary>
            /// The sfga o_ canmove.
            /// </summary>
            SFGAO_CANMOVE = DROPEFFECT_MOVE, // Objects can be moved (0x2)

            /// <summary>
            /// The sfga o_ canlink.
            /// </summary>
            SFGAO_CANLINK = DROPEFFECT_LINK, // Objects can be linked (0x4)

            /// <summary>
            /// The sfga o_ storage.
            /// </summary>
            SFGAO_STORAGE = 0x00000008, // supports BindToObject(IID_IStorage)

            /// <summary>
            /// The sfga o_ canrename.
            /// </summary>
            SFGAO_CANRENAME = 0x00000010, // Objects can be renamed

            /// <summary>
            /// The sfga o_ candelete.
            /// </summary>
            SFGAO_CANDELETE = 0x00000020, // Objects can be deleted

            /// <summary>
            /// The sfga o_ haspropsheet.
            /// </summary>
            SFGAO_HASPROPSHEET = 0x00000040, // Objects have property sheets

            /// <summary>
            /// The sfga o_ droptarget.
            /// </summary>
            SFGAO_DROPTARGET = 0x00000100, // Objects are drop target

            /// <summary>
            /// The sfga o_ capabilitymask.
            /// </summary>
            SFGAO_CAPABILITYMASK = 0x00000177, 

            /// <summary>
            /// The sfga o_ encrypted.
            /// </summary>
            SFGAO_ENCRYPTED = 0x00002000, // Object is encrypted (use alt color)

            /// <summary>
            /// The sfga o_ isslow.
            /// </summary>
            SFGAO_ISSLOW = 0x00004000, // 'Slow' object

            /// <summary>
            /// The sfga o_ ghosted.
            /// </summary>
            SFGAO_GHOSTED = 0x00008000, // Ghosted icon

            /// <summary>
            /// The sfga o_ link.
            /// </summary>
            SFGAO_LINK = 0x00010000, // Shortcut (link)

            /// <summary>
            /// The sfga o_ share.
            /// </summary>
            SFGAO_SHARE = 0x00020000, // Shared

            /// <summary>
            /// The sfga o_ readonly.
            /// </summary>
            SFGAO_READONLY = 0x00040000, // Read-only

            /// <summary>
            /// The sfga o_ hidden.
            /// </summary>
            SFGAO_HIDDEN = 0x00080000, // Hidden object

            /// <summary>
            /// The sfga o_ displayattrmask.
            /// </summary>
            SFGAO_DISPLAYATTRMASK = 0x000FC000, 

            /// <summary>
            /// The sfga o_ filesysancestor.
            /// </summary>
            SFGAO_FILESYSANCESTOR = 0x10000000, // May contain children with SFGAO_FILESYSTEM

            /// <summary>
            /// The sfga o_ folder.
            /// </summary>
            SFGAO_FOLDER = 0x20000000, // Support BindToObject(IID_IShellFolder)

            /// <summary>
            /// The sfga o_ filesystem.
            /// </summary>
            SFGAO_FILESYSTEM = 0x40000000, // Is a win32 file system object (file/folder/root)

            /// <summary>
            /// The sfga o_ hassubfolder.
            /// </summary>
            SFGAO_HASSUBFOLDER = 0x80000000, // May contain children with SFGAO_FOLDER (may be slow)

            /// <summary>
            /// The sfga o_ contentsmask.
            /// </summary>
            SFGAO_CONTENTSMASK = 0x80000000, 

            /// <summary>
            /// The sfga o_ validate.
            /// </summary>
            SFGAO_VALIDATE = 0x01000000, // Invalidate cached information (may be slow)

            /// <summary>
            /// The sfga o_ removable.
            /// </summary>
            SFGAO_REMOVABLE = 0x02000000, // Is this removeable media?

            /// <summary>
            /// The sfga o_ compressed.
            /// </summary>
            SFGAO_COMPRESSED = 0x04000000, // Object is compressed (use alt color)

            /// <summary>
            /// The sfga o_ browsable.
            /// </summary>
            SFGAO_BROWSABLE = 0x08000000, 

            // Supports IShellFolder, but only implements CreateViewObject() (non-folder view)

            /// <summary>
            /// The sfga o_ nonenumerated.
            /// </summary>
            SFGAO_NONENUMERATED = 0x00100000, // Is a non-enumerated object (should be hidden)

            /// <summary>
            /// The sfga o_ newcontent.
            /// </summary>
            SFGAO_NEWCONTENT = 0x00200000, // Should show bold in explorer tree

            /// <summary>
            /// The sfga o_ stream.
            /// </summary>
            SFGAO_STREAM = 0x00400000, // Supports BindToObject(IID_IStream)

            /// <summary>
            /// The sfga o_ canmoniker.
            /// </summary>
            SFGAO_CANMONIKER = 0x00400000, // Obsolete

            /// <summary>
            /// The sfga o_ hasstorage.
            /// </summary>
            SFGAO_HASSTORAGE = 0x00400000, // Obsolete

            /// <summary>
            /// The sfga o_ storageancestor.
            /// </summary>
            SFGAO_STORAGEANCESTOR = 0x00800000, // May contain children with SFGAO_STORAGE or SFGAO_STREAM

            /// <summary>
            /// The sfga o_ storagecapmask.
            /// </summary>
            SFGAO_STORAGECAPMASK = 0x70C50008, // For determining storage capabilities, ie for open/save semantics

            /// <summary>
            /// The sfga o_ pkeysfgaomask.
            /// </summary>
            SFGAO_PKEYSFGAOMASK = 0x81044010

            // Attributes that are masked out for PKEY_SFGAOFlags because they are considered to cause slow calculations or lack context (SFGAO_VALIDATE | SFGAO_ISSLOW | SFGAO_HASSUBFOLDER and others)
        }

        /// <summary>
        /// The siattribflags.
        /// </summary>
        public enum SIATTRIBFLAGS
        {
            /// <summary>
            /// The siattribflag s_ and.
            /// </summary>
            SIATTRIBFLAGS_AND = 0x00000001, // if multiple items and the attirbutes together.

            /// <summary>
            /// The siattribflag s_ or.
            /// </summary>
            SIATTRIBFLAGS_OR = 0x00000002, // if multiple items or the attributes together.

            /// <summary>
            /// The siattribflag s_ appcompat.
            /// </summary>
            SIATTRIBFLAGS_APPCOMPAT = 0x00000003, 

            // Call GetAttributes directly on the ShellFolder for multiple attributes
        }

        /// <summary>
        /// The sigdn.
        /// </summary>
        public enum SIGDN : uint
        {
            /// <summary>
            /// The sigd n_ normaldisplay.
            /// </summary>
            SIGDN_NORMALDISPLAY = 0x00000000, // SHGDN_NORMAL

            /// <summary>
            /// The sigd n_ parentrelativeparsing.
            /// </summary>
            SIGDN_PARENTRELATIVEPARSING = 0x80018001, // SHGDN_INFOLDER | SHGDN_FORPARSING

            /// <summary>
            /// The sigd n_ desktopabsoluteparsing.
            /// </summary>
            SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000, // SHGDN_FORPARSING

            /// <summary>
            /// The sigd n_ parentrelativeediting.
            /// </summary>
            SIGDN_PARENTRELATIVEEDITING = 0x80031001, // SHGDN_INFOLDER | SHGDN_FOREDITING

            /// <summary>
            /// The sigd n_ desktopabsoluteediting.
            /// </summary>
            SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000, // SHGDN_FORPARSING | SHGDN_FORADDRESSBAR

            /// <summary>
            /// The sigd n_ filesyspath.
            /// </summary>
            SIGDN_FILESYSPATH = 0x80058000, // SHGDN_FORPARSING

            /// <summary>
            /// The sigd n_ url.
            /// </summary>
            SIGDN_URL = 0x80068000, // SHGDN_FORPARSING

            /// <summary>
            /// The sigd n_ parentrelativeforaddressbar.
            /// </summary>
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001, // SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR

            /// <summary>
            /// The sigd n_ parentrelative.
            /// </summary>
            SIGDN_PARENTRELATIVE = 0x80080001 // SHGDN_INFOLDER
        }

        /// <summary>
        /// The statflag.
        /// </summary>
        public enum STATFLAG : uint
        {
            /// <summary>
            /// The statfla g_ default.
            /// </summary>
            STATFLAG_DEFAULT = 0, 

            /// <summary>
            /// The statfla g_ noname.
            /// </summary>
            STATFLAG_NONAME = 1, 

            /// <summary>
            /// The statfla g_ noopen.
            /// </summary>
            STATFLAG_NOOPEN = 2
        }

        /// <summary>
        /// The stgc.
        /// </summary>
        [Flags]
        public enum STGC : uint
        {
            /// <summary>
            /// The stg c_ default.
            /// </summary>
            STGC_DEFAULT = 0, 

            /// <summary>
            /// The stg c_ overwrite.
            /// </summary>
            STGC_OVERWRITE = 1, 

            /// <summary>
            /// The stg c_ onlyifcurrent.
            /// </summary>
            STGC_ONLYIFCURRENT = 2, 

            /// <summary>
            /// The stg c_ dangerouslycommitmerelytodiskcache.
            /// </summary>
            STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4, 

            /// <summary>
            /// The stg c_ consolidate.
            /// </summary>
            STGC_CONSOLIDATE = 8
        }

        /// <summary>
        /// The stgty.
        /// </summary>
        public enum STGTY : uint
        {
            /// <summary>
            /// The stgt y_ storage.
            /// </summary>
            STGTY_STORAGE = 1, 

            /// <summary>
            /// The stgt y_ stream.
            /// </summary>
            STGTY_STREAM = 2, 

            /// <summary>
            /// The stgt y_ lockbytes.
            /// </summary>
            STGTY_LOCKBYTES = 3, 

            /// <summary>
            /// The stgt y_ property.
            /// </summary>
            STGTY_PROPERTY = 4
        }

        /// <summary>
        /// The toke n_ type.
        /// </summary>
        public enum TOKEN_TYPE
        {
            /// <summary>
            /// The token primary.
            /// </summary>
            TokenPrimary = 1, 

            /// <summary>
            /// The token impersonation.
            /// </summary>
            TokenImpersonation = 2
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the bhi d_ stream.
        /// </summary>
        public static Guid BHID_Stream
        {
            get
            {
                return new Guid(0x1cebb3ab, 0x7c10, 0x499a, 0xa4, 0x17, 0x92, 0xca, 0x16, 0xc4, 0xcb, 0x83);
            }
        }

        /// <summary>
        /// Gets the wintrus t_ actio n_ generi c_ verif y_ v 2.
        /// </summary>
        public static Guid WINTRUST_ACTION_GENERIC_VERIFY_V2
        {
            get
            {
                return new Guid(0xaac56b, 0xcd44, 0x11d0, 0x8c, 0xc2, 0x0, 0xc0, 0x4f, 0xc2, 0x95, 0xee);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The ct l_ code.
        /// </summary>
        /// <param name="deviceType">
        /// The device type.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="access">
        /// The access.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        private static uint CTL_CODE(uint deviceType, uint function, uint method, uint access)
        {
            return (deviceType << 16) | (access << 14) | (function << 2) | method;
        }

        #endregion
    }
}