// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeErrors.cs" company="">
//   
// </copyright>
// <summary>
//   The native errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Systemlayer
{
    // Copied from System.IO.MonoIOError.cs, (C) 2002 Dan Lewis

    // System.IO.MonoIOError.cs: Win32 error codes. Yuck.
    // Author:
    // Dan Lewis (dihlewis@yahoo.co.uk)
    // (C) 2002

    // Permission is hereby granted, free of charge, to any person obtaining
    // a copy of this software and associated documentation files (the
    // "Software"), to deal in the Software without restriction, including
    // without limitation the rights to use, copy, modify, merge, publish,
    // distribute, sublicense, and/or sell copies of the Software, and to
    // permit persons to whom the Software is furnished to do so, subject to
    // the following conditions:
    // The above copyright notice and this permission notice shall be
    // included in all copies or substantial portions of the Software.
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    // MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    // LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    // OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    // WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

    /// <summary>
    /// The native errors.
    /// </summary>
    internal enum NativeErrors
    {
        /// <summary>
        /// The erro r_ success.
        /// </summary>
        ERROR_SUCCESS = 0, 

        /// <summary>
        /// The erro r_ invali d_ function.
        /// </summary>
        ERROR_INVALID_FUNCTION = 1, 

        /// <summary>
        /// The erro r_ fil e_ no t_ found.
        /// </summary>
        ERROR_FILE_NOT_FOUND = 2, 

        /// <summary>
        /// The erro r_ pat h_ no t_ found.
        /// </summary>
        ERROR_PATH_NOT_FOUND = 3, 

        /// <summary>
        /// The erro r_ to o_ man y_ ope n_ files.
        /// </summary>
        ERROR_TOO_MANY_OPEN_FILES = 4, 

        /// <summary>
        /// The erro r_ acces s_ denied.
        /// </summary>
        ERROR_ACCESS_DENIED = 5, 

        /// <summary>
        /// The erro r_ invali d_ handle.
        /// </summary>
        ERROR_INVALID_HANDLE = 6, 

        /// <summary>
        /// The erro r_ aren a_ trashed.
        /// </summary>
        ERROR_ARENA_TRASHED = 7, 

        /// <summary>
        /// The erro r_ no t_ enoug h_ memory.
        /// </summary>
        ERROR_NOT_ENOUGH_MEMORY = 8, 

        /// <summary>
        /// The erro r_ invali d_ block.
        /// </summary>
        ERROR_INVALID_BLOCK = 9, 

        /// <summary>
        /// The erro r_ ba d_ environment.
        /// </summary>
        ERROR_BAD_ENVIRONMENT = 10, 

        /// <summary>
        /// The erro r_ ba d_ format.
        /// </summary>
        ERROR_BAD_FORMAT = 11, 

        /// <summary>
        /// The erro r_ invali d_ access.
        /// </summary>
        ERROR_INVALID_ACCESS = 12, 

        /// <summary>
        /// The erro r_ invali d_ data.
        /// </summary>
        ERROR_INVALID_DATA = 13, 

        /// <summary>
        /// The erro r_ outofmemory.
        /// </summary>
        ERROR_OUTOFMEMORY = 14, 

        /// <summary>
        /// The erro r_ invali d_ drive.
        /// </summary>
        ERROR_INVALID_DRIVE = 15, 

        /// <summary>
        /// The erro r_ curren t_ directory.
        /// </summary>
        ERROR_CURRENT_DIRECTORY = 16, 

        /// <summary>
        /// The erro r_ no t_ sam e_ device.
        /// </summary>
        ERROR_NOT_SAME_DEVICE = 17, 

        /// <summary>
        /// The erro r_ n o_ mor e_ files.
        /// </summary>
        ERROR_NO_MORE_FILES = 18, 

        /// <summary>
        /// The erro r_ writ e_ protect.
        /// </summary>
        ERROR_WRITE_PROTECT = 19, 

        /// <summary>
        /// The erro r_ ba d_ unit.
        /// </summary>
        ERROR_BAD_UNIT = 20, 

        /// <summary>
        /// The erro r_ no t_ ready.
        /// </summary>
        ERROR_NOT_READY = 21, 

        /// <summary>
        /// The erro r_ ba d_ command.
        /// </summary>
        ERROR_BAD_COMMAND = 22, 

        /// <summary>
        /// The erro r_ crc.
        /// </summary>
        ERROR_CRC = 23, 

        /// <summary>
        /// The erro r_ ba d_ length.
        /// </summary>
        ERROR_BAD_LENGTH = 24, 

        /// <summary>
        /// The erro r_ seek.
        /// </summary>
        ERROR_SEEK = 25, 

        /// <summary>
        /// The erro r_ no t_ do s_ disk.
        /// </summary>
        ERROR_NOT_DOS_DISK = 26, 

        /// <summary>
        /// The erro r_ secto r_ no t_ found.
        /// </summary>
        ERROR_SECTOR_NOT_FOUND = 27, 

        /// <summary>
        /// The erro r_ ou t_ o f_ paper.
        /// </summary>
        ERROR_OUT_OF_PAPER = 28, 

        /// <summary>
        /// The erro r_ writ e_ fault.
        /// </summary>
        ERROR_WRITE_FAULT = 29, 

        /// <summary>
        /// The erro r_ rea d_ fault.
        /// </summary>
        ERROR_READ_FAULT = 30, 

        /// <summary>
        /// The erro r_ ge n_ failure.
        /// </summary>
        ERROR_GEN_FAILURE = 31, 

        /// <summary>
        /// The erro r_ sharin g_ violation.
        /// </summary>
        ERROR_SHARING_VIOLATION = 32, 

        /// <summary>
        /// The erro r_ loc k_ violation.
        /// </summary>
        ERROR_LOCK_VIOLATION = 33, 

        /// <summary>
        /// The erro r_ wron g_ disk.
        /// </summary>
        ERROR_WRONG_DISK = 34, 

        /// <summary>
        /// The erro r_ sharin g_ buffe r_ exceeded.
        /// </summary>
        ERROR_SHARING_BUFFER_EXCEEDED = 36, 

        /// <summary>
        /// The erro r_ handl e_ eof.
        /// </summary>
        ERROR_HANDLE_EOF = 38, 

        /// <summary>
        /// The erro r_ handl e_ dis k_ full.
        /// </summary>
        ERROR_HANDLE_DISK_FULL = 39, 

        /// <summary>
        /// The erro r_ no t_ supported.
        /// </summary>
        ERROR_NOT_SUPPORTED = 50, 

        /// <summary>
        /// The erro r_ re m_ no t_ list.
        /// </summary>
        ERROR_REM_NOT_LIST = 51, 

        /// <summary>
        /// The erro r_ du p_ name.
        /// </summary>
        ERROR_DUP_NAME = 52, 

        /// <summary>
        /// The erro r_ ba d_ netpath.
        /// </summary>
        ERROR_BAD_NETPATH = 53, 

        /// <summary>
        /// The erro r_ networ k_ busy.
        /// </summary>
        ERROR_NETWORK_BUSY = 54, 

        /// <summary>
        /// The erro r_ de v_ no t_ exist.
        /// </summary>
        ERROR_DEV_NOT_EXIST = 55, 

        /// <summary>
        /// The erro r_ to o_ man y_ cmds.
        /// </summary>
        ERROR_TOO_MANY_CMDS = 56, 

        /// <summary>
        /// The erro r_ ada p_ hd w_ err.
        /// </summary>
        ERROR_ADAP_HDW_ERR = 57, 

        /// <summary>
        /// The erro r_ ba d_ ne t_ resp.
        /// </summary>
        ERROR_BAD_NET_RESP = 58, 

        /// <summary>
        /// The erro r_ unex p_ ne t_ err.
        /// </summary>
        ERROR_UNEXP_NET_ERR = 59, 

        /// <summary>
        /// The erro r_ ba d_ re m_ adap.
        /// </summary>
        ERROR_BAD_REM_ADAP = 60, 

        /// <summary>
        /// The erro r_ print q_ full.
        /// </summary>
        ERROR_PRINTQ_FULL = 61, 

        /// <summary>
        /// The erro r_ n o_ spoo l_ space.
        /// </summary>
        ERROR_NO_SPOOL_SPACE = 62, 

        /// <summary>
        /// The erro r_ prin t_ cancelled.
        /// </summary>
        ERROR_PRINT_CANCELLED = 63, 

        /// <summary>
        /// The erro r_ netnam e_ deleted.
        /// </summary>
        ERROR_NETNAME_DELETED = 64, 

        /// <summary>
        /// The erro r_ networ k_ acces s_ denied.
        /// </summary>
        ERROR_NETWORK_ACCESS_DENIED = 65, 

        /// <summary>
        /// The erro r_ ba d_ de v_ type.
        /// </summary>
        ERROR_BAD_DEV_TYPE = 66, 

        /// <summary>
        /// The erro r_ ba d_ ne t_ name.
        /// </summary>
        ERROR_BAD_NET_NAME = 67, 

        /// <summary>
        /// The erro r_ to o_ man y_ names.
        /// </summary>
        ERROR_TOO_MANY_NAMES = 68, 

        /// <summary>
        /// The erro r_ to o_ man y_ sess.
        /// </summary>
        ERROR_TOO_MANY_SESS = 69, 

        /// <summary>
        /// The erro r_ sharin g_ paused.
        /// </summary>
        ERROR_SHARING_PAUSED = 70, 

        /// <summary>
        /// The erro r_ re q_ no t_ accep.
        /// </summary>
        ERROR_REQ_NOT_ACCEP = 71, 

        /// <summary>
        /// The erro r_ redi r_ paused.
        /// </summary>
        ERROR_REDIR_PAUSED = 72, 

        /// <summary>
        /// The erro r_ fil e_ exists.
        /// </summary>
        ERROR_FILE_EXISTS = 80, 

        /// <summary>
        /// The erro r_ canno t_ make.
        /// </summary>
        ERROR_CANNOT_MAKE = 82, 

        /// <summary>
        /// The erro r_ fai l_ i 24.
        /// </summary>
        ERROR_FAIL_I24 = 83, 

        /// <summary>
        /// The erro r_ ou t_ o f_ structures.
        /// </summary>
        ERROR_OUT_OF_STRUCTURES = 84, 

        /// <summary>
        /// The erro r_ alread y_ assigned.
        /// </summary>
        ERROR_ALREADY_ASSIGNED = 85, 

        /// <summary>
        /// The erro r_ invali d_ password.
        /// </summary>
        ERROR_INVALID_PASSWORD = 86, 

        /// <summary>
        /// The erro r_ invali d_ parameter.
        /// </summary>
        ERROR_INVALID_PARAMETER = 87, 

        /// <summary>
        /// The erro r_ ne t_ writ e_ fault.
        /// </summary>
        ERROR_NET_WRITE_FAULT = 88, 

        /// <summary>
        /// The erro r_ n o_ pro c_ slots.
        /// </summary>
        ERROR_NO_PROC_SLOTS = 89, 

        /// <summary>
        /// The erro r_ to o_ man y_ semaphores.
        /// </summary>
        ERROR_TOO_MANY_SEMAPHORES = 100, 

        /// <summary>
        /// The erro r_ exc l_ se m_ alread y_ owned.
        /// </summary>
        ERROR_EXCL_SEM_ALREADY_OWNED = 101, 

        /// <summary>
        /// The erro r_ se m_ i s_ set.
        /// </summary>
        ERROR_SEM_IS_SET = 102, 

        /// <summary>
        /// The erro r_ to o_ man y_ se m_ requests.
        /// </summary>
        ERROR_TOO_MANY_SEM_REQUESTS = 103, 

        /// <summary>
        /// The erro r_ invali d_ a t_ interrup t_ time.
        /// </summary>
        ERROR_INVALID_AT_INTERRUPT_TIME = 104, 

        /// <summary>
        /// The erro r_ se m_ owne r_ died.
        /// </summary>
        ERROR_SEM_OWNER_DIED = 105, 

        /// <summary>
        /// The erro r_ se m_ use r_ limit.
        /// </summary>
        ERROR_SEM_USER_LIMIT = 106, 

        /// <summary>
        /// The erro r_ dis k_ change.
        /// </summary>
        ERROR_DISK_CHANGE = 107, 

        /// <summary>
        /// The erro r_ driv e_ locked.
        /// </summary>
        ERROR_DRIVE_LOCKED = 108, 

        /// <summary>
        /// The erro r_ broke n_ pipe.
        /// </summary>
        ERROR_BROKEN_PIPE = 109, 

        /// <summary>
        /// The erro r_ ope n_ failed.
        /// </summary>
        ERROR_OPEN_FAILED = 110, 

        /// <summary>
        /// The erro r_ buffe r_ overflow.
        /// </summary>
        ERROR_BUFFER_OVERFLOW = 111, 

        /// <summary>
        /// The erro r_ dis k_ full.
        /// </summary>
        ERROR_DISK_FULL = 112, 

        /// <summary>
        /// The erro r_ n o_ mor e_ searc h_ handles.
        /// </summary>
        ERROR_NO_MORE_SEARCH_HANDLES = 113, 

        /// <summary>
        /// The erro r_ invali d_ targe t_ handle.
        /// </summary>
        ERROR_INVALID_TARGET_HANDLE = 114, 

        /// <summary>
        /// The erro r_ invali d_ category.
        /// </summary>
        ERROR_INVALID_CATEGORY = 117, 

        /// <summary>
        /// The erro r_ invali d_ verif y_ switch.
        /// </summary>
        ERROR_INVALID_VERIFY_SWITCH = 118, 

        /// <summary>
        /// The erro r_ ba d_ drive r_ level.
        /// </summary>
        ERROR_BAD_DRIVER_LEVEL = 119, 

        /// <summary>
        /// The erro r_ cal l_ no t_ implemented.
        /// </summary>
        ERROR_CALL_NOT_IMPLEMENTED = 120, 

        /// <summary>
        /// The erro r_ se m_ timeout.
        /// </summary>
        ERROR_SEM_TIMEOUT = 121, 

        /// <summary>
        /// The erro r_ insufficien t_ buffer.
        /// </summary>
        ERROR_INSUFFICIENT_BUFFER = 122, 

        /// <summary>
        /// The erro r_ invali d_ name.
        /// </summary>
        ERROR_INVALID_NAME = 123, 

        /// <summary>
        /// The erro r_ invali d_ level.
        /// </summary>
        ERROR_INVALID_LEVEL = 124, 

        /// <summary>
        /// The erro r_ n o_ volum e_ label.
        /// </summary>
        ERROR_NO_VOLUME_LABEL = 125, 

        /// <summary>
        /// The erro r_ mo d_ no t_ found.
        /// </summary>
        ERROR_MOD_NOT_FOUND = 126, 

        /// <summary>
        /// The erro r_ pro c_ no t_ found.
        /// </summary>
        ERROR_PROC_NOT_FOUND = 127, 

        /// <summary>
        /// The erro r_ wai t_ n o_ children.
        /// </summary>
        ERROR_WAIT_NO_CHILDREN = 128, 

        /// <summary>
        /// The erro r_ chil d_ no t_ complete.
        /// </summary>
        ERROR_CHILD_NOT_COMPLETE = 129, 

        /// <summary>
        /// The erro r_ direc t_ acces s_ handle.
        /// </summary>
        ERROR_DIRECT_ACCESS_HANDLE = 130, 

        /// <summary>
        /// The erro r_ negativ e_ seek.
        /// </summary>
        ERROR_NEGATIVE_SEEK = 131, 

        /// <summary>
        /// The erro r_ see k_ o n_ device.
        /// </summary>
        ERROR_SEEK_ON_DEVICE = 132, 

        /// <summary>
        /// The erro r_ i s_ joi n_ target.
        /// </summary>
        ERROR_IS_JOIN_TARGET = 133, 

        /// <summary>
        /// The erro r_ i s_ joined.
        /// </summary>
        ERROR_IS_JOINED = 134, 

        /// <summary>
        /// The erro r_ i s_ substed.
        /// </summary>
        ERROR_IS_SUBSTED = 135, 

        /// <summary>
        /// The erro r_ no t_ joined.
        /// </summary>
        ERROR_NOT_JOINED = 136, 

        /// <summary>
        /// The erro r_ no t_ substed.
        /// </summary>
        ERROR_NOT_SUBSTED = 137, 

        /// <summary>
        /// The erro r_ joi n_ t o_ join.
        /// </summary>
        ERROR_JOIN_TO_JOIN = 138, 

        /// <summary>
        /// The erro r_ subs t_ t o_ subst.
        /// </summary>
        ERROR_SUBST_TO_SUBST = 139, 

        /// <summary>
        /// The erro r_ joi n_ t o_ subst.
        /// </summary>
        ERROR_JOIN_TO_SUBST = 140, 

        /// <summary>
        /// The erro r_ subs t_ t o_ join.
        /// </summary>
        ERROR_SUBST_TO_JOIN = 141, 

        /// <summary>
        /// The erro r_ bus y_ drive.
        /// </summary>
        ERROR_BUSY_DRIVE = 142, 

        /// <summary>
        /// The erro r_ sam e_ drive.
        /// </summary>
        ERROR_SAME_DRIVE = 143, 

        /// <summary>
        /// The erro r_ di r_ no t_ root.
        /// </summary>
        ERROR_DIR_NOT_ROOT = 144, 

        /// <summary>
        /// The erro r_ di r_ no t_ empty.
        /// </summary>
        ERROR_DIR_NOT_EMPTY = 145, 

        /// <summary>
        /// The erro r_ i s_ subs t_ path.
        /// </summary>
        ERROR_IS_SUBST_PATH = 146, 

        /// <summary>
        /// The erro r_ i s_ joi n_ path.
        /// </summary>
        ERROR_IS_JOIN_PATH = 147, 

        /// <summary>
        /// The erro r_ pat h_ busy.
        /// </summary>
        ERROR_PATH_BUSY = 148, 

        /// <summary>
        /// The erro r_ i s_ subs t_ target.
        /// </summary>
        ERROR_IS_SUBST_TARGET = 149, 

        /// <summary>
        /// The erro r_ syste m_ trace.
        /// </summary>
        ERROR_SYSTEM_TRACE = 150, 

        /// <summary>
        /// The erro r_ invali d_ even t_ count.
        /// </summary>
        ERROR_INVALID_EVENT_COUNT = 151, 

        /// <summary>
        /// The erro r_ to o_ man y_ muxwaiters.
        /// </summary>
        ERROR_TOO_MANY_MUXWAITERS = 152, 

        /// <summary>
        /// The erro r_ invali d_ lis t_ format.
        /// </summary>
        ERROR_INVALID_LIST_FORMAT = 153, 

        /// <summary>
        /// The erro r_ labe l_ to o_ long.
        /// </summary>
        ERROR_LABEL_TOO_LONG = 154, 

        /// <summary>
        /// The erro r_ to o_ man y_ tcbs.
        /// </summary>
        ERROR_TOO_MANY_TCBS = 155, 

        /// <summary>
        /// The erro r_ signa l_ refused.
        /// </summary>
        ERROR_SIGNAL_REFUSED = 156, 

        /// <summary>
        /// The erro r_ discarded.
        /// </summary>
        ERROR_DISCARDED = 157, 

        /// <summary>
        /// The erro r_ no t_ locked.
        /// </summary>
        ERROR_NOT_LOCKED = 158, 

        /// <summary>
        /// The erro r_ ba d_ threadi d_ addr.
        /// </summary>
        ERROR_BAD_THREADID_ADDR = 159, 

        /// <summary>
        /// The erro r_ ba d_ arguments.
        /// </summary>
        ERROR_BAD_ARGUMENTS = 160, 

        /// <summary>
        /// The erro r_ ba d_ pathname.
        /// </summary>
        ERROR_BAD_PATHNAME = 161, 

        /// <summary>
        /// The erro r_ signa l_ pending.
        /// </summary>
        ERROR_SIGNAL_PENDING = 162, 

        /// <summary>
        /// The erro r_ ma x_ thrd s_ reached.
        /// </summary>
        ERROR_MAX_THRDS_REACHED = 164, 

        /// <summary>
        /// The erro r_ loc k_ failed.
        /// </summary>
        ERROR_LOCK_FAILED = 167, 

        /// <summary>
        /// The erro r_ busy.
        /// </summary>
        ERROR_BUSY = 170, 

        /// <summary>
        /// The erro r_ cance l_ violation.
        /// </summary>
        ERROR_CANCEL_VIOLATION = 173, 

        /// <summary>
        /// The erro r_ atomi c_ lock s_ no t_ supported.
        /// </summary>
        ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174, 

        /// <summary>
        /// The erro r_ invali d_ segmen t_ number.
        /// </summary>
        ERROR_INVALID_SEGMENT_NUMBER = 180, 

        /// <summary>
        /// The erro r_ invali d_ ordinal.
        /// </summary>
        ERROR_INVALID_ORDINAL = 182, 

        /// <summary>
        /// The erro r_ alread y_ exists.
        /// </summary>
        ERROR_ALREADY_EXISTS = 183, 

        /// <summary>
        /// The erro r_ invali d_ fla g_ number.
        /// </summary>
        ERROR_INVALID_FLAG_NUMBER = 186, 

        /// <summary>
        /// The erro r_ se m_ no t_ found.
        /// </summary>
        ERROR_SEM_NOT_FOUND = 187, 

        /// <summary>
        /// The erro r_ invali d_ startin g_ codeseg.
        /// </summary>
        ERROR_INVALID_STARTING_CODESEG = 188, 

        /// <summary>
        /// The erro r_ invali d_ stackseg.
        /// </summary>
        ERROR_INVALID_STACKSEG = 189, 

        /// <summary>
        /// The erro r_ invali d_ moduletype.
        /// </summary>
        ERROR_INVALID_MODULETYPE = 190, 

        /// <summary>
        /// The erro r_ invali d_ ex e_ signature.
        /// </summary>
        ERROR_INVALID_EXE_SIGNATURE = 191, 

        /// <summary>
        /// The erro r_ ex e_ marke d_ invalid.
        /// </summary>
        ERROR_EXE_MARKED_INVALID = 192, 

        /// <summary>
        /// The erro r_ ba d_ ex e_ format.
        /// </summary>
        ERROR_BAD_EXE_FORMAT = 193, 

        /// <summary>
        /// The erro r_ iterate d_ dat a_ exceed s_64 k.
        /// </summary>
        ERROR_ITERATED_DATA_EXCEEDS_64k = 194, 

        /// <summary>
        /// The erro r_ invali d_ minallocsize.
        /// </summary>
        ERROR_INVALID_MINALLOCSIZE = 195, 

        /// <summary>
        /// The erro r_ dynlin k_ fro m_ invali d_ ring.
        /// </summary>
        ERROR_DYNLINK_FROM_INVALID_RING = 196, 

        /// <summary>
        /// The erro r_ iop l_ no t_ enabled.
        /// </summary>
        ERROR_IOPL_NOT_ENABLED = 197, 

        /// <summary>
        /// The erro r_ invali d_ segdpl.
        /// </summary>
        ERROR_INVALID_SEGDPL = 198, 

        /// <summary>
        /// The erro r_ autodatase g_ exceed s_64 k.
        /// </summary>
        ERROR_AUTODATASEG_EXCEEDS_64k = 199, 

        /// <summary>
        /// The erro r_ rin g 2 se g_ mus t_ b e_ movable.
        /// </summary>
        ERROR_RING2SEG_MUST_BE_MOVABLE = 200, 

        /// <summary>
        /// The erro r_ relo c_ chai n_ xeed s_ seglim.
        /// </summary>
        ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201, 

        /// <summary>
        /// The erro r_ infloo p_ i n_ relo c_ chain.
        /// </summary>
        ERROR_INFLOOP_IN_RELOC_CHAIN = 202, 

        /// <summary>
        /// The erro r_ envva r_ no t_ found.
        /// </summary>
        ERROR_ENVVAR_NOT_FOUND = 203, 

        /// <summary>
        /// The erro r_ n o_ signa l_ sent.
        /// </summary>
        ERROR_NO_SIGNAL_SENT = 205, 

        /// <summary>
        /// The erro r_ filenam e_ exce d_ range.
        /// </summary>
        ERROR_FILENAME_EXCED_RANGE = 206, 

        /// <summary>
        /// The erro r_ rin g 2_ stac k_ i n_ use.
        /// </summary>
        ERROR_RING2_STACK_IN_USE = 207, 

        /// <summary>
        /// The erro r_ met a_ expansio n_ to o_ long.
        /// </summary>
        ERROR_META_EXPANSION_TOO_LONG = 208, 

        /// <summary>
        /// The erro r_ invali d_ signa l_ number.
        /// </summary>
        ERROR_INVALID_SIGNAL_NUMBER = 209, 

        /// <summary>
        /// The erro r_ threa d_1_ inactive.
        /// </summary>
        ERROR_THREAD_1_INACTIVE = 210, 

        /// <summary>
        /// The erro r_ locked.
        /// </summary>
        ERROR_LOCKED = 212, 

        /// <summary>
        /// The erro r_ to o_ man y_ modules.
        /// </summary>
        ERROR_TOO_MANY_MODULES = 214, 

        /// <summary>
        /// The erro r_ nestin g_ no t_ allowed.
        /// </summary>
        ERROR_NESTING_NOT_ALLOWED = 215, 

        /// <summary>
        /// The erro r_ ex e_ machin e_ typ e_ mismatch.
        /// </summary>
        ERROR_EXE_MACHINE_TYPE_MISMATCH = 216, 

        /// <summary>
        /// The erro r_ ba d_ pipe.
        /// </summary>
        ERROR_BAD_PIPE = 230, 

        /// <summary>
        /// The erro r_ pip e_ busy.
        /// </summary>
        ERROR_PIPE_BUSY = 231, 

        /// <summary>
        /// The erro r_ n o_ data.
        /// </summary>
        ERROR_NO_DATA = 232, 

        /// <summary>
        /// The erro r_ pip e_ no t_ connected.
        /// </summary>
        ERROR_PIPE_NOT_CONNECTED = 233, 

        /// <summary>
        /// The erro r_ mor e_ data.
        /// </summary>
        ERROR_MORE_DATA = 234, 

        /// <summary>
        /// The erro r_ v c_ disconnected.
        /// </summary>
        ERROR_VC_DISCONNECTED = 240, 

        /// <summary>
        /// The erro r_ invali d_ e a_ name.
        /// </summary>
        ERROR_INVALID_EA_NAME = 254, 

        /// <summary>
        /// The erro r_ e a_ lis t_ inconsistent.
        /// </summary>
        ERROR_EA_LIST_INCONSISTENT = 255, 

        /// <summary>
        /// The wai t_ timeout.
        /// </summary>
        WAIT_TIMEOUT = 258, 

        /// <summary>
        /// The erro r_ n o_ mor e_ items.
        /// </summary>
        ERROR_NO_MORE_ITEMS = 259, 

        /// <summary>
        /// The erro r_ canno t_ copy.
        /// </summary>
        ERROR_CANNOT_COPY = 266, 

        /// <summary>
        /// The erro r_ directory.
        /// </summary>
        ERROR_DIRECTORY = 267, 

        /// <summary>
        /// The erro r_ ea s_ didn t_ fit.
        /// </summary>
        ERROR_EAS_DIDNT_FIT = 275, 

        /// <summary>
        /// The erro r_ e a_ fil e_ corrupt.
        /// </summary>
        ERROR_EA_FILE_CORRUPT = 276, 

        /// <summary>
        /// The erro r_ e a_ tabl e_ full.
        /// </summary>
        ERROR_EA_TABLE_FULL = 277, 

        /// <summary>
        /// The erro r_ invali d_ e a_ handle.
        /// </summary>
        ERROR_INVALID_EA_HANDLE = 278, 

        /// <summary>
        /// The erro r_ ea s_ no t_ supported.
        /// </summary>
        ERROR_EAS_NOT_SUPPORTED = 282, 

        /// <summary>
        /// The erro r_ no t_ owner.
        /// </summary>
        ERROR_NOT_OWNER = 288, 

        /// <summary>
        /// The erro r_ to o_ man y_ posts.
        /// </summary>
        ERROR_TOO_MANY_POSTS = 298, 

        /// <summary>
        /// The erro r_ partia l_ copy.
        /// </summary>
        ERROR_PARTIAL_COPY = 299, 

        /// <summary>
        /// The erro r_ oploc k_ no t_ granted.
        /// </summary>
        ERROR_OPLOCK_NOT_GRANTED = 300, 

        /// <summary>
        /// The erro r_ invali d_ oploc k_ protocol.
        /// </summary>
        ERROR_INVALID_OPLOCK_PROTOCOL = 301, 

        /// <summary>
        /// The erro r_ dis k_ to o_ fragmented.
        /// </summary>
        ERROR_DISK_TOO_FRAGMENTED = 302, 

        /// <summary>
        /// The erro r_ delet e_ pending.
        /// </summary>
        ERROR_DELETE_PENDING = 303, 

        /// <summary>
        /// The erro r_ m r_ mi d_ no t_ found.
        /// </summary>
        ERROR_MR_MID_NOT_FOUND = 317, 

        /// <summary>
        /// The erro r_ invali d_ address.
        /// </summary>
        ERROR_INVALID_ADDRESS = 487, 

        /// <summary>
        /// The erro r_ arithmeti c_ overflow.
        /// </summary>
        ERROR_ARITHMETIC_OVERFLOW = 534, 

        /// <summary>
        /// The erro r_ pip e_ connected.
        /// </summary>
        ERROR_PIPE_CONNECTED = 535, 

        /// <summary>
        /// The erro r_ pip e_ listening.
        /// </summary>
        ERROR_PIPE_LISTENING = 536, 

        /// <summary>
        /// The erro r_ e a_ acces s_ denied.
        /// </summary>
        ERROR_EA_ACCESS_DENIED = 994, 

        /// <summary>
        /// The erro r_ operatio n_ aborted.
        /// </summary>
        ERROR_OPERATION_ABORTED = 995, 

        /// <summary>
        /// The erro r_ i o_ incomplete.
        /// </summary>
        ERROR_IO_INCOMPLETE = 996, 

        /// <summary>
        /// The erro r_ i o_ pending.
        /// </summary>
        ERROR_IO_PENDING = 997, 

        /// <summary>
        /// The erro r_ noaccess.
        /// </summary>
        ERROR_NOACCESS = 998, 

        /// <summary>
        /// The erro r_ swaperror.
        /// </summary>
        ERROR_SWAPERROR = 999, 

        /// <summary>
        /// The erro r_ stac k_ overflow.
        /// </summary>
        ERROR_STACK_OVERFLOW = 1001, 

        /// <summary>
        /// The erro r_ invali d_ message.
        /// </summary>
        ERROR_INVALID_MESSAGE = 1002, 

        /// <summary>
        /// The erro r_ ca n_ no t_ complete.
        /// </summary>
        ERROR_CAN_NOT_COMPLETE = 1003, 

        /// <summary>
        /// The erro r_ invali d_ flags.
        /// </summary>
        ERROR_INVALID_FLAGS = 1004, 

        /// <summary>
        /// The erro r_ unrecognize d_ volume.
        /// </summary>
        ERROR_UNRECOGNIZED_VOLUME = 1005, 

        /// <summary>
        /// The erro r_ fil e_ invalid.
        /// </summary>
        ERROR_FILE_INVALID = 1006, 

        /// <summary>
        /// The erro r_ fullscree n_ mode.
        /// </summary>
        ERROR_FULLSCREEN_MODE = 1007, 

        /// <summary>
        /// The erro r_ n o_ token.
        /// </summary>
        ERROR_NO_TOKEN = 1008, 

        /// <summary>
        /// The erro r_ baddb.
        /// </summary>
        ERROR_BADDB = 1009, 

        /// <summary>
        /// The erro r_ badkey.
        /// </summary>
        ERROR_BADKEY = 1010, 

        /// <summary>
        /// The erro r_ cantopen.
        /// </summary>
        ERROR_CANTOPEN = 1011, 

        /// <summary>
        /// The erro r_ cantread.
        /// </summary>
        ERROR_CANTREAD = 1012, 

        /// <summary>
        /// The erro r_ cantwrite.
        /// </summary>
        ERROR_CANTWRITE = 1013, 

        /// <summary>
        /// The erro r_ registr y_ recovered.
        /// </summary>
        ERROR_REGISTRY_RECOVERED = 1014, 

        /// <summary>
        /// The erro r_ registr y_ corrupt.
        /// </summary>
        ERROR_REGISTRY_CORRUPT = 1015, 

        /// <summary>
        /// The erro r_ registr y_ i o_ failed.
        /// </summary>
        ERROR_REGISTRY_IO_FAILED = 1016, 

        /// <summary>
        /// The erro r_ no t_ registr y_ file.
        /// </summary>
        ERROR_NOT_REGISTRY_FILE = 1017, 

        /// <summary>
        /// The erro r_ ke y_ deleted.
        /// </summary>
        ERROR_KEY_DELETED = 1018, 

        /// <summary>
        /// The erro r_ n o_ lo g_ space.
        /// </summary>
        ERROR_NO_LOG_SPACE = 1019, 

        /// <summary>
        /// The erro r_ ke y_ ha s_ children.
        /// </summary>
        ERROR_KEY_HAS_CHILDREN = 1020, 

        /// <summary>
        /// The erro r_ chil d_ mus t_ b e_ volatile.
        /// </summary>
        ERROR_CHILD_MUST_BE_VOLATILE = 1021, 

        /// <summary>
        /// The erro r_ notif y_ enu m_ dir.
        /// </summary>
        ERROR_NOTIFY_ENUM_DIR = 1022, 

        /// <summary>
        /// The erro r_ dependen t_ service s_ running.
        /// </summary>
        ERROR_DEPENDENT_SERVICES_RUNNING = 1051, 

        /// <summary>
        /// The erro r_ invali d_ servic e_ control.
        /// </summary>
        ERROR_INVALID_SERVICE_CONTROL = 1052, 

        /// <summary>
        /// The erro r_ servic e_ reques t_ timeout.
        /// </summary>
        ERROR_SERVICE_REQUEST_TIMEOUT = 1053, 

        /// <summary>
        /// The erro r_ servic e_ n o_ thread.
        /// </summary>
        ERROR_SERVICE_NO_THREAD = 1054, 

        /// <summary>
        /// The erro r_ servic e_ databas e_ locked.
        /// </summary>
        ERROR_SERVICE_DATABASE_LOCKED = 1055, 

        /// <summary>
        /// The erro r_ servic e_ alread y_ running.
        /// </summary>
        ERROR_SERVICE_ALREADY_RUNNING = 1056, 

        /// <summary>
        /// The erro r_ invali d_ servic e_ account.
        /// </summary>
        ERROR_INVALID_SERVICE_ACCOUNT = 1057, 

        /// <summary>
        /// The erro r_ servic e_ disabled.
        /// </summary>
        ERROR_SERVICE_DISABLED = 1058, 

        /// <summary>
        /// The erro r_ circula r_ dependency.
        /// </summary>
        ERROR_CIRCULAR_DEPENDENCY = 1059, 

        /// <summary>
        /// The erro r_ servic e_ doe s_ no t_ exist.
        /// </summary>
        ERROR_SERVICE_DOES_NOT_EXIST = 1060, 

        /// <summary>
        /// The erro r_ servic e_ canno t_ accep t_ ctrl.
        /// </summary>
        ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 1061, 

        /// <summary>
        /// The erro r_ servic e_ no t_ active.
        /// </summary>
        ERROR_SERVICE_NOT_ACTIVE = 1062, 

        /// <summary>
        /// The erro r_ faile d_ servic e_ controlle r_ connect.
        /// </summary>
        ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 1063, 

        /// <summary>
        /// The erro r_ exceptio n_ i n_ service.
        /// </summary>
        ERROR_EXCEPTION_IN_SERVICE = 1064, 

        /// <summary>
        /// The erro r_ databas e_ doe s_ no t_ exist.
        /// </summary>
        ERROR_DATABASE_DOES_NOT_EXIST = 1065, 

        /// <summary>
        /// The erro r_ servic e_ specifi c_ error.
        /// </summary>
        ERROR_SERVICE_SPECIFIC_ERROR = 1066, 

        /// <summary>
        /// The erro r_ proces s_ aborted.
        /// </summary>
        ERROR_PROCESS_ABORTED = 1067, 

        /// <summary>
        /// The erro r_ servic e_ dependenc y_ fail.
        /// </summary>
        ERROR_SERVICE_DEPENDENCY_FAIL = 1068, 

        /// <summary>
        /// The erro r_ servic e_ logo n_ failed.
        /// </summary>
        ERROR_SERVICE_LOGON_FAILED = 1069, 

        /// <summary>
        /// The erro r_ servic e_ star t_ hang.
        /// </summary>
        ERROR_SERVICE_START_HANG = 1070, 

        /// <summary>
        /// The erro r_ invali d_ servic e_ lock.
        /// </summary>
        ERROR_INVALID_SERVICE_LOCK = 1071, 

        /// <summary>
        /// The erro r_ servic e_ marke d_ fo r_ delete.
        /// </summary>
        ERROR_SERVICE_MARKED_FOR_DELETE = 1072, 

        /// <summary>
        /// The erro r_ servic e_ exists.
        /// </summary>
        ERROR_SERVICE_EXISTS = 1073, 

        /// <summary>
        /// The erro r_ alread y_ runnin g_ lkg.
        /// </summary>
        ERROR_ALREADY_RUNNING_LKG = 1074, 

        /// <summary>
        /// The erro r_ servic e_ dependenc y_ deleted.
        /// </summary>
        ERROR_SERVICE_DEPENDENCY_DELETED = 1075, 

        /// <summary>
        /// The erro r_ boo t_ alread y_ accepted.
        /// </summary>
        ERROR_BOOT_ALREADY_ACCEPTED = 1076, 

        /// <summary>
        /// The erro r_ servic e_ neve r_ started.
        /// </summary>
        ERROR_SERVICE_NEVER_STARTED = 1077, 

        /// <summary>
        /// The erro r_ duplicat e_ servic e_ name.
        /// </summary>
        ERROR_DUPLICATE_SERVICE_NAME = 1078, 

        /// <summary>
        /// The erro r_ differen t_ servic e_ account.
        /// </summary>
        ERROR_DIFFERENT_SERVICE_ACCOUNT = 1079, 

        /// <summary>
        /// The erro r_ canno t_ detec t_ drive r_ failure.
        /// </summary>
        ERROR_CANNOT_DETECT_DRIVER_FAILURE = 1080, 

        /// <summary>
        /// The erro r_ canno t_ detec t_ proces s_ abort.
        /// </summary>
        ERROR_CANNOT_DETECT_PROCESS_ABORT = 1081, 

        /// <summary>
        /// The erro r_ n o_ recover y_ program.
        /// </summary>
        ERROR_NO_RECOVERY_PROGRAM = 1082, 

        /// <summary>
        /// The erro r_ servic e_ no t_ i n_ exe.
        /// </summary>
        ERROR_SERVICE_NOT_IN_EXE = 1083, 

        /// <summary>
        /// The erro r_ no t_ safeboo t_ service.
        /// </summary>
        ERROR_NOT_SAFEBOOT_SERVICE = 1084, 

        /// <summary>
        /// The erro r_ en d_ o f_ media.
        /// </summary>
        ERROR_END_OF_MEDIA = 1100, 

        /// <summary>
        /// The erro r_ filemar k_ detected.
        /// </summary>
        ERROR_FILEMARK_DETECTED = 1101, 

        /// <summary>
        /// The erro r_ beginnin g_ o f_ media.
        /// </summary>
        ERROR_BEGINNING_OF_MEDIA = 1102, 

        /// <summary>
        /// The erro r_ setmar k_ detected.
        /// </summary>
        ERROR_SETMARK_DETECTED = 1103, 

        /// <summary>
        /// The erro r_ n o_ dat a_ detected.
        /// </summary>
        ERROR_NO_DATA_DETECTED = 1104, 

        /// <summary>
        /// The erro r_ partitio n_ failure.
        /// </summary>
        ERROR_PARTITION_FAILURE = 1105, 

        /// <summary>
        /// The erro r_ invali d_ bloc k_ length.
        /// </summary>
        ERROR_INVALID_BLOCK_LENGTH = 1106, 

        /// <summary>
        /// The erro r_ devic e_ no t_ partitioned.
        /// </summary>
        ERROR_DEVICE_NOT_PARTITIONED = 1107, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ loc k_ media.
        /// </summary>
        ERROR_UNABLE_TO_LOCK_MEDIA = 1108, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ unloa d_ media.
        /// </summary>
        ERROR_UNABLE_TO_UNLOAD_MEDIA = 1109, 

        /// <summary>
        /// The erro r_ medi a_ changed.
        /// </summary>
        ERROR_MEDIA_CHANGED = 1110, 

        /// <summary>
        /// The erro r_ bu s_ reset.
        /// </summary>
        ERROR_BUS_RESET = 1111, 

        /// <summary>
        /// The erro r_ n o_ medi a_ i n_ drive.
        /// </summary>
        ERROR_NO_MEDIA_IN_DRIVE = 1112, 

        /// <summary>
        /// The erro r_ n o_ unicod e_ translation.
        /// </summary>
        ERROR_NO_UNICODE_TRANSLATION = 1113, 

        /// <summary>
        /// The erro r_ dl l_ ini t_ failed.
        /// </summary>
        ERROR_DLL_INIT_FAILED = 1114, 

        /// <summary>
        /// The erro r_ shutdow n_ i n_ progress.
        /// </summary>
        ERROR_SHUTDOWN_IN_PROGRESS = 1115, 

        /// <summary>
        /// The erro r_ n o_ shutdow n_ i n_ progress.
        /// </summary>
        ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116, 

        /// <summary>
        /// The erro r_ i o_ device.
        /// </summary>
        ERROR_IO_DEVICE = 1117, 

        /// <summary>
        /// The erro r_ seria l_ n o_ device.
        /// </summary>
        ERROR_SERIAL_NO_DEVICE = 1118, 

        /// <summary>
        /// The erro r_ ir q_ busy.
        /// </summary>
        ERROR_IRQ_BUSY = 1119, 

        /// <summary>
        /// The erro r_ mor e_ writes.
        /// </summary>
        ERROR_MORE_WRITES = 1120, 

        /// <summary>
        /// The erro r_ counte r_ timeout.
        /// </summary>
        ERROR_COUNTER_TIMEOUT = 1121, 

        /// <summary>
        /// The erro r_ flopp y_ i d_ mar k_ no t_ found.
        /// </summary>
        ERROR_FLOPPY_ID_MARK_NOT_FOUND = 1122, 

        /// <summary>
        /// The erro r_ flopp y_ wron g_ cylinder.
        /// </summary>
        ERROR_FLOPPY_WRONG_CYLINDER = 1123, 

        /// <summary>
        /// The erro r_ flopp y_ unknow n_ error.
        /// </summary>
        ERROR_FLOPPY_UNKNOWN_ERROR = 1124, 

        /// <summary>
        /// The erro r_ flopp y_ ba d_ registers.
        /// </summary>
        ERROR_FLOPPY_BAD_REGISTERS = 1125, 

        /// <summary>
        /// The erro r_ dis k_ recalibrat e_ failed.
        /// </summary>
        ERROR_DISK_RECALIBRATE_FAILED = 1126, 

        /// <summary>
        /// The erro r_ dis k_ operatio n_ failed.
        /// </summary>
        ERROR_DISK_OPERATION_FAILED = 1127, 

        /// <summary>
        /// The erro r_ dis k_ rese t_ failed.
        /// </summary>
        ERROR_DISK_RESET_FAILED = 1128, 

        /// <summary>
        /// The erro r_ eo m_ overflow.
        /// </summary>
        ERROR_EOM_OVERFLOW = 1129, 

        /// <summary>
        /// The erro r_ no t_ enoug h_ serve r_ memory.
        /// </summary>
        ERROR_NOT_ENOUGH_SERVER_MEMORY = 1130, 

        /// <summary>
        /// The erro r_ possibl e_ deadlock.
        /// </summary>
        ERROR_POSSIBLE_DEADLOCK = 1131, 

        /// <summary>
        /// The erro r_ mappe d_ alignment.
        /// </summary>
        ERROR_MAPPED_ALIGNMENT = 1132, 

        /// <summary>
        /// The erro r_ se t_ powe r_ stat e_ vetoed.
        /// </summary>
        ERROR_SET_POWER_STATE_VETOED = 1140, 

        /// <summary>
        /// The erro r_ se t_ powe r_ stat e_ failed.
        /// </summary>
        ERROR_SET_POWER_STATE_FAILED = 1141, 

        /// <summary>
        /// The erro r_ to o_ man y_ links.
        /// </summary>
        ERROR_TOO_MANY_LINKS = 1142, 

        /// <summary>
        /// The erro r_ ol d_ wi n_ version.
        /// </summary>
        ERROR_OLD_WIN_VERSION = 1150, 

        /// <summary>
        /// The erro r_ ap p_ wron g_ os.
        /// </summary>
        ERROR_APP_WRONG_OS = 1151, 

        /// <summary>
        /// The erro r_ singl e_ instanc e_ app.
        /// </summary>
        ERROR_SINGLE_INSTANCE_APP = 1152, 

        /// <summary>
        /// The erro r_ rmod e_ app.
        /// </summary>
        ERROR_RMODE_APP = 1153, 

        /// <summary>
        /// The erro r_ invali d_ dll.
        /// </summary>
        ERROR_INVALID_DLL = 1154, 

        /// <summary>
        /// The erro r_ n o_ association.
        /// </summary>
        ERROR_NO_ASSOCIATION = 1155, 

        /// <summary>
        /// The erro r_ dd e_ fail.
        /// </summary>
        ERROR_DDE_FAIL = 1156, 

        /// <summary>
        /// The erro r_ dl l_ no t_ found.
        /// </summary>
        ERROR_DLL_NOT_FOUND = 1157, 

        /// <summary>
        /// The erro r_ n o_ mor e_ use r_ handles.
        /// </summary>
        ERROR_NO_MORE_USER_HANDLES = 1158, 

        /// <summary>
        /// The erro r_ messag e_ syn c_ only.
        /// </summary>
        ERROR_MESSAGE_SYNC_ONLY = 1159, 

        /// <summary>
        /// The erro r_ sourc e_ elemen t_ empty.
        /// </summary>
        ERROR_SOURCE_ELEMENT_EMPTY = 1160, 

        /// <summary>
        /// The erro r_ destinatio n_ elemen t_ full.
        /// </summary>
        ERROR_DESTINATION_ELEMENT_FULL = 1161, 

        /// <summary>
        /// The erro r_ illega l_ elemen t_ address.
        /// </summary>
        ERROR_ILLEGAL_ELEMENT_ADDRESS = 1162, 

        /// <summary>
        /// The erro r_ magazin e_ no t_ present.
        /// </summary>
        ERROR_MAGAZINE_NOT_PRESENT = 1163, 

        /// <summary>
        /// The erro r_ devic e_ reinitializatio n_ needed.
        /// </summary>
        ERROR_DEVICE_REINITIALIZATION_NEEDED = 1164, 

        /// <summary>
        /// The erro r_ devic e_ require s_ cleaning.
        /// </summary>
        ERROR_DEVICE_REQUIRES_CLEANING = 1165, 

        /// <summary>
        /// The erro r_ devic e_ doo r_ open.
        /// </summary>
        ERROR_DEVICE_DOOR_OPEN = 1166, 

        /// <summary>
        /// The erro r_ devic e_ no t_ connected.
        /// </summary>
        ERROR_DEVICE_NOT_CONNECTED = 1167, 

        /// <summary>
        /// The erro r_ no t_ found.
        /// </summary>
        ERROR_NOT_FOUND = 1168, 

        /// <summary>
        /// The erro r_ n o_ match.
        /// </summary>
        ERROR_NO_MATCH = 1169, 

        /// <summary>
        /// The erro r_ se t_ no t_ found.
        /// </summary>
        ERROR_SET_NOT_FOUND = 1170, 

        /// <summary>
        /// The erro r_ poin t_ no t_ found.
        /// </summary>
        ERROR_POINT_NOT_FOUND = 1171, 

        /// <summary>
        /// The erro r_ n o_ trackin g_ service.
        /// </summary>
        ERROR_NO_TRACKING_SERVICE = 1172, 

        /// <summary>
        /// The erro r_ n o_ volum e_ id.
        /// </summary>
        ERROR_NO_VOLUME_ID = 1173, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ remov e_ replaced.
        /// </summary>
        ERROR_UNABLE_TO_REMOVE_REPLACED = 1175, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ mov e_ replacement.
        /// </summary>
        ERROR_UNABLE_TO_MOVE_REPLACEMENT = 1176, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ mov e_ replacemen t_2.
        /// </summary>
        ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 = 1177, 

        /// <summary>
        /// The erro r_ journa l_ delet e_ i n_ progress.
        /// </summary>
        ERROR_JOURNAL_DELETE_IN_PROGRESS = 1178, 

        /// <summary>
        /// The erro r_ journa l_ no t_ active.
        /// </summary>
        ERROR_JOURNAL_NOT_ACTIVE = 1179, 

        /// <summary>
        /// The erro r_ potentia l_ fil e_ found.
        /// </summary>
        ERROR_POTENTIAL_FILE_FOUND = 1180, 

        /// <summary>
        /// The erro r_ journa l_ entr y_ deleted.
        /// </summary>
        ERROR_JOURNAL_ENTRY_DELETED = 1181, 

        /// <summary>
        /// The erro r_ ba d_ device.
        /// </summary>
        ERROR_BAD_DEVICE = 1200, 

        /// <summary>
        /// The erro r_ connectio n_ unavail.
        /// </summary>
        ERROR_CONNECTION_UNAVAIL = 1201, 

        /// <summary>
        /// The erro r_ devic e_ alread y_ remembered.
        /// </summary>
        ERROR_DEVICE_ALREADY_REMEMBERED = 1202, 

        /// <summary>
        /// The erro r_ n o_ ne t_ o r_ ba d_ path.
        /// </summary>
        ERROR_NO_NET_OR_BAD_PATH = 1203, 

        /// <summary>
        /// The erro r_ ba d_ provider.
        /// </summary>
        ERROR_BAD_PROVIDER = 1204, 

        /// <summary>
        /// The erro r_ canno t_ ope n_ profile.
        /// </summary>
        ERROR_CANNOT_OPEN_PROFILE = 1205, 

        /// <summary>
        /// The erro r_ ba d_ profile.
        /// </summary>
        ERROR_BAD_PROFILE = 1206, 

        /// <summary>
        /// The erro r_ no t_ container.
        /// </summary>
        ERROR_NOT_CONTAINER = 1207, 

        /// <summary>
        /// The erro r_ extende d_ error.
        /// </summary>
        ERROR_EXTENDED_ERROR = 1208, 

        /// <summary>
        /// The erro r_ invali d_ groupname.
        /// </summary>
        ERROR_INVALID_GROUPNAME = 1209, 

        /// <summary>
        /// The erro r_ invali d_ computername.
        /// </summary>
        ERROR_INVALID_COMPUTERNAME = 1210, 

        /// <summary>
        /// The erro r_ invali d_ eventname.
        /// </summary>
        ERROR_INVALID_EVENTNAME = 1211, 

        /// <summary>
        /// The erro r_ invali d_ domainname.
        /// </summary>
        ERROR_INVALID_DOMAINNAME = 1212, 

        /// <summary>
        /// The erro r_ invali d_ servicename.
        /// </summary>
        ERROR_INVALID_SERVICENAME = 1213, 

        /// <summary>
        /// The erro r_ invali d_ netname.
        /// </summary>
        ERROR_INVALID_NETNAME = 1214, 

        /// <summary>
        /// The erro r_ invali d_ sharename.
        /// </summary>
        ERROR_INVALID_SHARENAME = 1215, 

        /// <summary>
        /// The erro r_ invali d_ passwordname.
        /// </summary>
        ERROR_INVALID_PASSWORDNAME = 1216, 

        /// <summary>
        /// The erro r_ invali d_ messagename.
        /// </summary>
        ERROR_INVALID_MESSAGENAME = 1217, 

        /// <summary>
        /// The erro r_ invali d_ messagedest.
        /// </summary>
        ERROR_INVALID_MESSAGEDEST = 1218, 

        /// <summary>
        /// The erro r_ sessio n_ credentia l_ conflict.
        /// </summary>
        ERROR_SESSION_CREDENTIAL_CONFLICT = 1219, 

        /// <summary>
        /// The erro r_ remot e_ sessio n_ limi t_ exceeded.
        /// </summary>
        ERROR_REMOTE_SESSION_LIMIT_EXCEEDED = 1220, 

        /// <summary>
        /// The erro r_ du p_ domainname.
        /// </summary>
        ERROR_DUP_DOMAINNAME = 1221, 

        /// <summary>
        /// The erro r_ n o_ network.
        /// </summary>
        ERROR_NO_NETWORK = 1222, 

        /// <summary>
        /// The erro r_ cancelled.
        /// </summary>
        ERROR_CANCELLED = 1223, 

        /// <summary>
        /// The erro r_ use r_ mappe d_ file.
        /// </summary>
        ERROR_USER_MAPPED_FILE = 1224, 

        /// <summary>
        /// The erro r_ connectio n_ refused.
        /// </summary>
        ERROR_CONNECTION_REFUSED = 1225, 

        /// <summary>
        /// The erro r_ gracefu l_ disconnect.
        /// </summary>
        ERROR_GRACEFUL_DISCONNECT = 1226, 

        /// <summary>
        /// The erro r_ addres s_ alread y_ associated.
        /// </summary>
        ERROR_ADDRESS_ALREADY_ASSOCIATED = 1227, 

        /// <summary>
        /// The erro r_ addres s_ no t_ associated.
        /// </summary>
        ERROR_ADDRESS_NOT_ASSOCIATED = 1228, 

        /// <summary>
        /// The erro r_ connectio n_ invalid.
        /// </summary>
        ERROR_CONNECTION_INVALID = 1229, 

        /// <summary>
        /// The erro r_ connectio n_ active.
        /// </summary>
        ERROR_CONNECTION_ACTIVE = 1230, 

        /// <summary>
        /// The erro r_ networ k_ unreachable.
        /// </summary>
        ERROR_NETWORK_UNREACHABLE = 1231, 

        /// <summary>
        /// The erro r_ hos t_ unreachable.
        /// </summary>
        ERROR_HOST_UNREACHABLE = 1232, 

        /// <summary>
        /// The erro r_ protoco l_ unreachable.
        /// </summary>
        ERROR_PROTOCOL_UNREACHABLE = 1233, 

        /// <summary>
        /// The erro r_ por t_ unreachable.
        /// </summary>
        ERROR_PORT_UNREACHABLE = 1234, 

        /// <summary>
        /// The erro r_ reques t_ aborted.
        /// </summary>
        ERROR_REQUEST_ABORTED = 1235, 

        /// <summary>
        /// The erro r_ connectio n_ aborted.
        /// </summary>
        ERROR_CONNECTION_ABORTED = 1236, 

        /// <summary>
        /// The erro r_ retry.
        /// </summary>
        ERROR_RETRY = 1237, 

        /// <summary>
        /// The erro r_ connectio n_ coun t_ limit.
        /// </summary>
        ERROR_CONNECTION_COUNT_LIMIT = 1238, 

        /// <summary>
        /// The erro r_ logi n_ tim e_ restriction.
        /// </summary>
        ERROR_LOGIN_TIME_RESTRICTION = 1239, 

        /// <summary>
        /// The erro r_ logi n_ wkst a_ restriction.
        /// </summary>
        ERROR_LOGIN_WKSTA_RESTRICTION = 1240, 

        /// <summary>
        /// The erro r_ incorrec t_ address.
        /// </summary>
        ERROR_INCORRECT_ADDRESS = 1241, 

        /// <summary>
        /// The erro r_ alread y_ registered.
        /// </summary>
        ERROR_ALREADY_REGISTERED = 1242, 

        /// <summary>
        /// The erro r_ servic e_ no t_ found.
        /// </summary>
        ERROR_SERVICE_NOT_FOUND = 1243, 

        /// <summary>
        /// The erro r_ no t_ authenticated.
        /// </summary>
        ERROR_NOT_AUTHENTICATED = 1244, 

        /// <summary>
        /// The erro r_ no t_ logge d_ on.
        /// </summary>
        ERROR_NOT_LOGGED_ON = 1245, 

        /// <summary>
        /// The erro r_ continue.
        /// </summary>
        ERROR_CONTINUE = 1246, 

        /// <summary>
        /// The erro r_ alread y_ initialized.
        /// </summary>
        ERROR_ALREADY_INITIALIZED = 1247, 

        /// <summary>
        /// The erro r_ n o_ mor e_ devices.
        /// </summary>
        ERROR_NO_MORE_DEVICES = 1248, 

        /// <summary>
        /// The erro r_ n o_ suc h_ site.
        /// </summary>
        ERROR_NO_SUCH_SITE = 1249, 

        /// <summary>
        /// The erro r_ domai n_ controlle r_ exists.
        /// </summary>
        ERROR_DOMAIN_CONTROLLER_EXISTS = 1250, 

        /// <summary>
        /// The erro r_ onl y_ i f_ connected.
        /// </summary>
        ERROR_ONLY_IF_CONNECTED = 1251, 

        /// <summary>
        /// The erro r_ overrid e_ nochanges.
        /// </summary>
        ERROR_OVERRIDE_NOCHANGES = 1252, 

        /// <summary>
        /// The erro r_ ba d_ use r_ profile.
        /// </summary>
        ERROR_BAD_USER_PROFILE = 1253, 

        /// <summary>
        /// The erro r_ no t_ supporte d_ o n_ sbs.
        /// </summary>
        ERROR_NOT_SUPPORTED_ON_SBS = 1254, 

        /// <summary>
        /// The erro r_ serve r_ shutdow n_ i n_ progress.
        /// </summary>
        ERROR_SERVER_SHUTDOWN_IN_PROGRESS = 1255, 

        /// <summary>
        /// The erro r_ hos t_ down.
        /// </summary>
        ERROR_HOST_DOWN = 1256, 

        /// <summary>
        /// The erro r_ no n_ accoun t_ sid.
        /// </summary>
        ERROR_NON_ACCOUNT_SID = 1257, 

        /// <summary>
        /// The erro r_ no n_ domai n_ sid.
        /// </summary>
        ERROR_NON_DOMAIN_SID = 1258, 

        /// <summary>
        /// The erro r_ apphel p_ block.
        /// </summary>
        ERROR_APPHELP_BLOCK = 1259, 

        /// <summary>
        /// The erro r_ acces s_ disable d_ b y_ policy.
        /// </summary>
        ERROR_ACCESS_DISABLED_BY_POLICY = 1260, 

        /// <summary>
        /// The erro r_ re g_ na t_ consumption.
        /// </summary>
        ERROR_REG_NAT_CONSUMPTION = 1261, 

        /// <summary>
        /// The erro r_ cscshar e_ offline.
        /// </summary>
        ERROR_CSCSHARE_OFFLINE = 1262, 

        /// <summary>
        /// The erro r_ pkini t_ failure.
        /// </summary>
        ERROR_PKINIT_FAILURE = 1263, 

        /// <summary>
        /// The erro r_ smartcar d_ subsyste m_ failure.
        /// </summary>
        ERROR_SMARTCARD_SUBSYSTEM_FAILURE = 1264, 

        /// <summary>
        /// The erro r_ downgrad e_ detected.
        /// </summary>
        ERROR_DOWNGRADE_DETECTED = 1265, 

        /// <summary>
        /// The se c_ e_ smartcar d_ cer t_ revoked.
        /// </summary>
        SEC_E_SMARTCARD_CERT_REVOKED = 1266, 

        /// <summary>
        /// The se c_ e_ issuin g_ c a_ untrusted.
        /// </summary>
        SEC_E_ISSUING_CA_UNTRUSTED = 1267, 

        /// <summary>
        /// The se c_ e_ revocatio n_ offlin e_ c.
        /// </summary>
        SEC_E_REVOCATION_OFFLINE_C = 1268, 

        /// <summary>
        /// The se c_ e_ pkini t_ clien t_ failur.
        /// </summary>
        SEC_E_PKINIT_CLIENT_FAILUR = 1269, 

        /// <summary>
        /// The se c_ e_ smartcar d_ cer t_ expired.
        /// </summary>
        SEC_E_SMARTCARD_CERT_EXPIRED = 1270, 

        /// <summary>
        /// The erro r_ machin e_ locked.
        /// </summary>
        ERROR_MACHINE_LOCKED = 1271, 

        /// <summary>
        /// The erro r_ callbac k_ supplie d_ invali d_ data.
        /// </summary>
        ERROR_CALLBACK_SUPPLIED_INVALID_DATA = 1273, 

        /// <summary>
        /// The erro r_ syn c_ foregroun d_ refres h_ required.
        /// </summary>
        ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED = 1274, 

        /// <summary>
        /// The erro r_ drive r_ blocked.
        /// </summary>
        ERROR_DRIVER_BLOCKED = 1275, 

        /// <summary>
        /// The erro r_ invali d_ impor t_ o f_ no n_ dll.
        /// </summary>
        ERROR_INVALID_IMPORT_OF_NON_DLL = 1276, 

        /// <summary>
        /// The erro r_ no t_ al l_ assigned.
        /// </summary>
        ERROR_NOT_ALL_ASSIGNED = 1300, 

        /// <summary>
        /// The erro r_ som e_ no t_ mapped.
        /// </summary>
        ERROR_SOME_NOT_MAPPED = 1301, 

        /// <summary>
        /// The erro r_ n o_ quota s_ fo r_ account.
        /// </summary>
        ERROR_NO_QUOTAS_FOR_ACCOUNT = 1302, 

        /// <summary>
        /// The erro r_ loca l_ use r_ sessio n_ key.
        /// </summary>
        ERROR_LOCAL_USER_SESSION_KEY = 1303, 

        /// <summary>
        /// The erro r_ nul l_ l m_ password.
        /// </summary>
        ERROR_NULL_LM_PASSWORD = 1304, 

        /// <summary>
        /// The erro r_ unknow n_ revision.
        /// </summary>
        ERROR_UNKNOWN_REVISION = 1305, 

        /// <summary>
        /// The erro r_ revisio n_ mismatch.
        /// </summary>
        ERROR_REVISION_MISMATCH = 1306, 

        /// <summary>
        /// The erro r_ invali d_ owner.
        /// </summary>
        ERROR_INVALID_OWNER = 1307, 

        /// <summary>
        /// The erro r_ invali d_ primar y_ group.
        /// </summary>
        ERROR_INVALID_PRIMARY_GROUP = 1308, 

        /// <summary>
        /// The erro r_ n o_ impersonatio n_ token.
        /// </summary>
        ERROR_NO_IMPERSONATION_TOKEN = 1309, 

        /// <summary>
        /// The erro r_ can t_ disabl e_ mandatory.
        /// </summary>
        ERROR_CANT_DISABLE_MANDATORY = 1310, 

        /// <summary>
        /// The erro r_ n o_ logo n_ servers.
        /// </summary>
        ERROR_NO_LOGON_SERVERS = 1311, 

        /// <summary>
        /// The erro r_ n o_ suc h_ logo n_ session.
        /// </summary>
        ERROR_NO_SUCH_LOGON_SESSION = 1312, 

        /// <summary>
        /// The erro r_ n o_ suc h_ privilege.
        /// </summary>
        ERROR_NO_SUCH_PRIVILEGE = 1313, 

        /// <summary>
        /// The erro r_ privileg e_ no t_ held.
        /// </summary>
        ERROR_PRIVILEGE_NOT_HELD = 1314, 

        /// <summary>
        /// The erro r_ invali d_ accoun t_ name.
        /// </summary>
        ERROR_INVALID_ACCOUNT_NAME = 1315, 

        /// <summary>
        /// The erro r_ use r_ exists.
        /// </summary>
        ERROR_USER_EXISTS = 1316, 

        /// <summary>
        /// The erro r_ n o_ suc h_ user.
        /// </summary>
        ERROR_NO_SUCH_USER = 1317, 

        /// <summary>
        /// The erro r_ grou p_ exists.
        /// </summary>
        ERROR_GROUP_EXISTS = 1318, 

        /// <summary>
        /// The erro r_ n o_ suc h_ group.
        /// </summary>
        ERROR_NO_SUCH_GROUP = 1319, 

        /// <summary>
        /// The erro r_ membe r_ i n_ group.
        /// </summary>
        ERROR_MEMBER_IN_GROUP = 1320, 

        /// <summary>
        /// The erro r_ membe r_ no t_ i n_ group.
        /// </summary>
        ERROR_MEMBER_NOT_IN_GROUP = 1321, 

        /// <summary>
        /// The erro r_ las t_ admin.
        /// </summary>
        ERROR_LAST_ADMIN = 1322, 

        /// <summary>
        /// The erro r_ wron g_ password.
        /// </summary>
        ERROR_WRONG_PASSWORD = 1323, 

        /// <summary>
        /// The erro r_ il l_ forme d_ password.
        /// </summary>
        ERROR_ILL_FORMED_PASSWORD = 1324, 

        /// <summary>
        /// The erro r_ passwor d_ restriction.
        /// </summary>
        ERROR_PASSWORD_RESTRICTION = 1325, 

        /// <summary>
        /// The erro r_ logo n_ failure.
        /// </summary>
        ERROR_LOGON_FAILURE = 1326, 

        /// <summary>
        /// The erro r_ accoun t_ restriction.
        /// </summary>
        ERROR_ACCOUNT_RESTRICTION = 1327, 

        /// <summary>
        /// The erro r_ invali d_ logo n_ hours.
        /// </summary>
        ERROR_INVALID_LOGON_HOURS = 1328, 

        /// <summary>
        /// The erro r_ invali d_ workstation.
        /// </summary>
        ERROR_INVALID_WORKSTATION = 1329, 

        /// <summary>
        /// The erro r_ passwor d_ expired.
        /// </summary>
        ERROR_PASSWORD_EXPIRED = 1330, 

        /// <summary>
        /// The erro r_ accoun t_ disabled.
        /// </summary>
        ERROR_ACCOUNT_DISABLED = 1331, 

        /// <summary>
        /// The erro r_ non e_ mapped.
        /// </summary>
        ERROR_NONE_MAPPED = 1332, 

        /// <summary>
        /// The erro r_ to o_ man y_ luid s_ requested.
        /// </summary>
        ERROR_TOO_MANY_LUIDS_REQUESTED = 1333, 

        /// <summary>
        /// The erro r_ luid s_ exhausted.
        /// </summary>
        ERROR_LUIDS_EXHAUSTED = 1334, 

        /// <summary>
        /// The erro r_ invali d_ su b_ authority.
        /// </summary>
        ERROR_INVALID_SUB_AUTHORITY = 1335, 

        /// <summary>
        /// The erro r_ invali d_ acl.
        /// </summary>
        ERROR_INVALID_ACL = 1336, 

        /// <summary>
        /// The erro r_ invali d_ sid.
        /// </summary>
        ERROR_INVALID_SID = 1337, 

        /// <summary>
        /// The erro r_ invali d_ securit y_ descr.
        /// </summary>
        ERROR_INVALID_SECURITY_DESCR = 1338, 

        /// <summary>
        /// The erro r_ ba d_ inheritanc e_ acl.
        /// </summary>
        ERROR_BAD_INHERITANCE_ACL = 1340, 

        /// <summary>
        /// The erro r_ serve r_ disabled.
        /// </summary>
        ERROR_SERVER_DISABLED = 1341, 

        /// <summary>
        /// The erro r_ serve r_ no t_ disabled.
        /// </summary>
        ERROR_SERVER_NOT_DISABLED = 1342, 

        /// <summary>
        /// The erro r_ invali d_ i d_ authority.
        /// </summary>
        ERROR_INVALID_ID_AUTHORITY = 1343, 

        /// <summary>
        /// The erro r_ allotte d_ spac e_ exceeded.
        /// </summary>
        ERROR_ALLOTTED_SPACE_EXCEEDED = 1344, 

        /// <summary>
        /// The erro r_ invali d_ grou p_ attributes.
        /// </summary>
        ERROR_INVALID_GROUP_ATTRIBUTES = 1345, 

        /// <summary>
        /// The erro r_ ba d_ impersonatio n_ level.
        /// </summary>
        ERROR_BAD_IMPERSONATION_LEVEL = 1346, 

        /// <summary>
        /// The erro r_ can t_ ope n_ anonymous.
        /// </summary>
        ERROR_CANT_OPEN_ANONYMOUS = 1347, 

        /// <summary>
        /// The erro r_ ba d_ validatio n_ class.
        /// </summary>
        ERROR_BAD_VALIDATION_CLASS = 1348, 

        /// <summary>
        /// The erro r_ ba d_ toke n_ type.
        /// </summary>
        ERROR_BAD_TOKEN_TYPE = 1349, 

        /// <summary>
        /// The erro r_ n o_ securit y_ o n_ object.
        /// </summary>
        ERROR_NO_SECURITY_ON_OBJECT = 1350, 

        /// <summary>
        /// The erro r_ can t_ acces s_ domai n_ info.
        /// </summary>
        ERROR_CANT_ACCESS_DOMAIN_INFO = 1351, 

        /// <summary>
        /// The erro r_ invali d_ serve r_ state.
        /// </summary>
        ERROR_INVALID_SERVER_STATE = 1352, 

        /// <summary>
        /// The erro r_ invali d_ domai n_ state.
        /// </summary>
        ERROR_INVALID_DOMAIN_STATE = 1353, 

        /// <summary>
        /// The erro r_ invali d_ domai n_ role.
        /// </summary>
        ERROR_INVALID_DOMAIN_ROLE = 1354, 

        /// <summary>
        /// The erro r_ n o_ suc h_ domain.
        /// </summary>
        ERROR_NO_SUCH_DOMAIN = 1355, 

        /// <summary>
        /// The erro r_ domai n_ exists.
        /// </summary>
        ERROR_DOMAIN_EXISTS = 1356, 

        /// <summary>
        /// The erro r_ domai n_ limi t_ exceeded.
        /// </summary>
        ERROR_DOMAIN_LIMIT_EXCEEDED = 1357, 

        /// <summary>
        /// The erro r_ interna l_ d b_ corruption.
        /// </summary>
        ERROR_INTERNAL_DB_CORRUPTION = 1358, 

        /// <summary>
        /// The erro r_ interna l_ error.
        /// </summary>
        ERROR_INTERNAL_ERROR = 1359, 

        /// <summary>
        /// The erro r_ generi c_ no t_ mapped.
        /// </summary>
        ERROR_GENERIC_NOT_MAPPED = 1360, 

        /// <summary>
        /// The erro r_ ba d_ descripto r_ format.
        /// </summary>
        ERROR_BAD_DESCRIPTOR_FORMAT = 1361, 

        /// <summary>
        /// The erro r_ no t_ logo n_ process.
        /// </summary>
        ERROR_NOT_LOGON_PROCESS = 1362, 

        /// <summary>
        /// The erro r_ logo n_ sessio n_ exists.
        /// </summary>
        ERROR_LOGON_SESSION_EXISTS = 1363, 

        /// <summary>
        /// The erro r_ n o_ suc h_ package.
        /// </summary>
        ERROR_NO_SUCH_PACKAGE = 1364, 

        /// <summary>
        /// The erro r_ ba d_ logo n_ sessio n_ state.
        /// </summary>
        ERROR_BAD_LOGON_SESSION_STATE = 1365, 

        /// <summary>
        /// The erro r_ logo n_ sessio n_ collision.
        /// </summary>
        ERROR_LOGON_SESSION_COLLISION = 1366, 

        /// <summary>
        /// The erro r_ invali d_ logo n_ type.
        /// </summary>
        ERROR_INVALID_LOGON_TYPE = 1367, 

        /// <summary>
        /// The erro r_ canno t_ impersonate.
        /// </summary>
        ERROR_CANNOT_IMPERSONATE = 1368, 

        /// <summary>
        /// The erro r_ rxac t_ invali d_ state.
        /// </summary>
        ERROR_RXACT_INVALID_STATE = 1369, 

        /// <summary>
        /// The erro r_ rxac t_ commi t_ failure.
        /// </summary>
        ERROR_RXACT_COMMIT_FAILURE = 1370, 

        /// <summary>
        /// The erro r_ specia l_ account.
        /// </summary>
        ERROR_SPECIAL_ACCOUNT = 1371, 

        /// <summary>
        /// The erro r_ specia l_ group.
        /// </summary>
        ERROR_SPECIAL_GROUP = 1372, 

        /// <summary>
        /// The erro r_ specia l_ user.
        /// </summary>
        ERROR_SPECIAL_USER = 1373, 

        /// <summary>
        /// The erro r_ member s_ primar y_ group.
        /// </summary>
        ERROR_MEMBERS_PRIMARY_GROUP = 1374, 

        /// <summary>
        /// The erro r_ toke n_ alread y_ i n_ use.
        /// </summary>
        ERROR_TOKEN_ALREADY_IN_USE = 1375, 

        /// <summary>
        /// The erro r_ n o_ suc h_ alias.
        /// </summary>
        ERROR_NO_SUCH_ALIAS = 1376, 

        /// <summary>
        /// The erro r_ membe r_ no t_ i n_ alias.
        /// </summary>
        ERROR_MEMBER_NOT_IN_ALIAS = 1377, 

        /// <summary>
        /// The erro r_ membe r_ i n_ alias.
        /// </summary>
        ERROR_MEMBER_IN_ALIAS = 1378, 

        /// <summary>
        /// The erro r_ alia s_ exists.
        /// </summary>
        ERROR_ALIAS_EXISTS = 1379, 

        /// <summary>
        /// The erro r_ logo n_ no t_ granted.
        /// </summary>
        ERROR_LOGON_NOT_GRANTED = 1380, 

        /// <summary>
        /// The erro r_ to o_ man y_ secrets.
        /// </summary>
        ERROR_TOO_MANY_SECRETS = 1381, 

        /// <summary>
        /// The erro r_ secre t_ to o_ long.
        /// </summary>
        ERROR_SECRET_TOO_LONG = 1382, 

        /// <summary>
        /// The erro r_ interna l_ d b_ error.
        /// </summary>
        ERROR_INTERNAL_DB_ERROR = 1383, 

        /// <summary>
        /// The erro r_ to o_ man y_ contex t_ ids.
        /// </summary>
        ERROR_TOO_MANY_CONTEXT_IDS = 1384, 

        /// <summary>
        /// The erro r_ logo n_ typ e_ no t_ granted.
        /// </summary>
        ERROR_LOGON_TYPE_NOT_GRANTED = 1385, 

        /// <summary>
        /// The erro r_ n t_ cros s_ encryptio n_ required.
        /// </summary>
        ERROR_NT_CROSS_ENCRYPTION_REQUIRED = 1386, 

        /// <summary>
        /// The erro r_ n o_ suc h_ member.
        /// </summary>
        ERROR_NO_SUCH_MEMBER = 1387, 

        /// <summary>
        /// The erro r_ invali d_ member.
        /// </summary>
        ERROR_INVALID_MEMBER = 1388, 

        /// <summary>
        /// The erro r_ to o_ man y_ sids.
        /// </summary>
        ERROR_TOO_MANY_SIDS = 1389, 

        /// <summary>
        /// The erro r_ l m_ cros s_ encryptio n_ required.
        /// </summary>
        ERROR_LM_CROSS_ENCRYPTION_REQUIRED = 1390, 

        /// <summary>
        /// The erro r_ n o_ inheritance.
        /// </summary>
        ERROR_NO_INHERITANCE = 1391, 

        /// <summary>
        /// The erro r_ fil e_ corrupt.
        /// </summary>
        ERROR_FILE_CORRUPT = 1392, 

        /// <summary>
        /// The erro r_ dis k_ corrupt.
        /// </summary>
        ERROR_DISK_CORRUPT = 1393, 

        /// <summary>
        /// The erro r_ n o_ use r_ sessio n_ key.
        /// </summary>
        ERROR_NO_USER_SESSION_KEY = 1394, 

        /// <summary>
        /// The erro r_ licens e_ quot a_ exceeded.
        /// </summary>
        ERROR_LICENSE_QUOTA_EXCEEDED = 1395, 

        /// <summary>
        /// The erro r_ wron g_ targe t_ name.
        /// </summary>
        ERROR_WRONG_TARGET_NAME = 1396, 

        /// <summary>
        /// The erro r_ mutua l_ aut h_ failed.
        /// </summary>
        ERROR_MUTUAL_AUTH_FAILED = 1397, 

        /// <summary>
        /// The erro r_ tim e_ skew.
        /// </summary>
        ERROR_TIME_SKEW = 1398, 

        /// <summary>
        /// The erro r_ curren t_ domai n_ no t_ allowed.
        /// </summary>
        ERROR_CURRENT_DOMAIN_NOT_ALLOWED = 1399, 

        /// <summary>
        /// The erro r_ invali d_ windo w_ handle.
        /// </summary>
        ERROR_INVALID_WINDOW_HANDLE = 1400, 

        /// <summary>
        /// The erro r_ invali d_ men u_ handle.
        /// </summary>
        ERROR_INVALID_MENU_HANDLE = 1401, 

        /// <summary>
        /// The erro r_ invali d_ curso r_ handle.
        /// </summary>
        ERROR_INVALID_CURSOR_HANDLE = 1402, 

        /// <summary>
        /// The erro r_ invali d_ acce l_ handle.
        /// </summary>
        ERROR_INVALID_ACCEL_HANDLE = 1403, 

        /// <summary>
        /// The erro r_ invali d_ hoo k_ handle.
        /// </summary>
        ERROR_INVALID_HOOK_HANDLE = 1404, 

        /// <summary>
        /// The erro r_ invali d_ dw p_ handle.
        /// </summary>
        ERROR_INVALID_DWP_HANDLE = 1405, 

        /// <summary>
        /// The erro r_ tl w_ wit h_ wschild.
        /// </summary>
        ERROR_TLW_WITH_WSCHILD = 1406, 

        /// <summary>
        /// The erro r_ canno t_ fin d_ wn d_ class.
        /// </summary>
        ERROR_CANNOT_FIND_WND_CLASS = 1407, 

        /// <summary>
        /// The erro r_ windo w_ o f_ othe r_ thread.
        /// </summary>
        ERROR_WINDOW_OF_OTHER_THREAD = 1408, 

        /// <summary>
        /// The erro r_ hotke y_ alread y_ registered.
        /// </summary>
        ERROR_HOTKEY_ALREADY_REGISTERED = 1409, 

        /// <summary>
        /// The erro r_ clas s_ alread y_ exists.
        /// </summary>
        ERROR_CLASS_ALREADY_EXISTS = 1410, 

        /// <summary>
        /// The erro r_ clas s_ doe s_ no t_ exist.
        /// </summary>
        ERROR_CLASS_DOES_NOT_EXIST = 1411, 

        /// <summary>
        /// The erro r_ clas s_ ha s_ windows.
        /// </summary>
        ERROR_CLASS_HAS_WINDOWS = 1412, 

        /// <summary>
        /// The erro r_ invali d_ index.
        /// </summary>
        ERROR_INVALID_INDEX = 1413, 

        /// <summary>
        /// The erro r_ invali d_ ico n_ handle.
        /// </summary>
        ERROR_INVALID_ICON_HANDLE = 1414, 

        /// <summary>
        /// The erro r_ privat e_ dialo g_ index.
        /// </summary>
        ERROR_PRIVATE_DIALOG_INDEX = 1415, 

        /// <summary>
        /// The erro r_ listbo x_ i d_ no t_ found.
        /// </summary>
        ERROR_LISTBOX_ID_NOT_FOUND = 1416, 

        /// <summary>
        /// The erro r_ n o_ wildcar d_ characters.
        /// </summary>
        ERROR_NO_WILDCARD_CHARACTERS = 1417, 

        /// <summary>
        /// The erro r_ clipboar d_ no t_ open.
        /// </summary>
        ERROR_CLIPBOARD_NOT_OPEN = 1418, 

        /// <summary>
        /// The erro r_ hotke y_ no t_ registered.
        /// </summary>
        ERROR_HOTKEY_NOT_REGISTERED = 1419, 

        /// <summary>
        /// The erro r_ windo w_ no t_ dialog.
        /// </summary>
        ERROR_WINDOW_NOT_DIALOG = 1420, 

        /// <summary>
        /// The erro r_ contro l_ i d_ no t_ found.
        /// </summary>
        ERROR_CONTROL_ID_NOT_FOUND = 1421, 

        /// <summary>
        /// The erro r_ invali d_ combobo x_ message.
        /// </summary>
        ERROR_INVALID_COMBOBOX_MESSAGE = 1422, 

        /// <summary>
        /// The erro r_ windo w_ no t_ combobox.
        /// </summary>
        ERROR_WINDOW_NOT_COMBOBOX = 1423, 

        /// <summary>
        /// The erro r_ invali d_ edi t_ height.
        /// </summary>
        ERROR_INVALID_EDIT_HEIGHT = 1424, 

        /// <summary>
        /// The erro r_ d c_ no t_ found.
        /// </summary>
        ERROR_DC_NOT_FOUND = 1425, 

        /// <summary>
        /// The erro r_ invali d_ hoo k_ filter.
        /// </summary>
        ERROR_INVALID_HOOK_FILTER = 1426, 

        /// <summary>
        /// The erro r_ invali d_ filte r_ proc.
        /// </summary>
        ERROR_INVALID_FILTER_PROC = 1427, 

        /// <summary>
        /// The erro r_ hoo k_ need s_ hmod.
        /// </summary>
        ERROR_HOOK_NEEDS_HMOD = 1428, 

        /// <summary>
        /// The erro r_ globa l_ onl y_ hook.
        /// </summary>
        ERROR_GLOBAL_ONLY_HOOK = 1429, 

        /// <summary>
        /// The erro r_ journa l_ hoo k_ set.
        /// </summary>
        ERROR_JOURNAL_HOOK_SET = 1430, 

        /// <summary>
        /// The erro r_ hoo k_ no t_ installed.
        /// </summary>
        ERROR_HOOK_NOT_INSTALLED = 1431, 

        /// <summary>
        /// The erro r_ invali d_ l b_ message.
        /// </summary>
        ERROR_INVALID_LB_MESSAGE = 1432, 

        /// <summary>
        /// The erro r_ setcoun t_ o n_ ba d_ lb.
        /// </summary>
        ERROR_SETCOUNT_ON_BAD_LB = 1433, 

        /// <summary>
        /// The erro r_ l b_ withou t_ tabstops.
        /// </summary>
        ERROR_LB_WITHOUT_TABSTOPS = 1434, 

        /// <summary>
        /// The erro r_ destro y_ objec t_ o f_ othe r_ thread.
        /// </summary>
        ERROR_DESTROY_OBJECT_OF_OTHER_THREAD = 1435, 

        /// <summary>
        /// The erro r_ chil d_ windo w_ menu.
        /// </summary>
        ERROR_CHILD_WINDOW_MENU = 1436, 

        /// <summary>
        /// The erro r_ n o_ syste m_ menu.
        /// </summary>
        ERROR_NO_SYSTEM_MENU = 1437, 

        /// <summary>
        /// The erro r_ invali d_ msgbo x_ style.
        /// </summary>
        ERROR_INVALID_MSGBOX_STYLE = 1438, 

        /// <summary>
        /// The erro r_ invali d_ sp i_ value.
        /// </summary>
        ERROR_INVALID_SPI_VALUE = 1439, 

        /// <summary>
        /// The erro r_ scree n_ alread y_ locked.
        /// </summary>
        ERROR_SCREEN_ALREADY_LOCKED = 1440, 

        /// <summary>
        /// The erro r_ hwnd s_ hav e_ dif f_ parent.
        /// </summary>
        ERROR_HWNDS_HAVE_DIFF_PARENT = 1441, 

        /// <summary>
        /// The erro r_ no t_ chil d_ window.
        /// </summary>
        ERROR_NOT_CHILD_WINDOW = 1442, 

        /// <summary>
        /// The erro r_ invali d_ g w_ command.
        /// </summary>
        ERROR_INVALID_GW_COMMAND = 1443, 

        /// <summary>
        /// The erro r_ invali d_ threa d_ id.
        /// </summary>
        ERROR_INVALID_THREAD_ID = 1444, 

        /// <summary>
        /// The erro r_ no n_ mdichil d_ window.
        /// </summary>
        ERROR_NON_MDICHILD_WINDOW = 1445, 

        /// <summary>
        /// The erro r_ popu p_ alread y_ active.
        /// </summary>
        ERROR_POPUP_ALREADY_ACTIVE = 1446, 

        /// <summary>
        /// The erro r_ n o_ scrollbars.
        /// </summary>
        ERROR_NO_SCROLLBARS = 1447, 

        /// <summary>
        /// The erro r_ invali d_ scrollba r_ range.
        /// </summary>
        ERROR_INVALID_SCROLLBAR_RANGE = 1448, 

        /// <summary>
        /// The erro r_ invali d_ showwi n_ command.
        /// </summary>
        ERROR_INVALID_SHOWWIN_COMMAND = 1449, 

        /// <summary>
        /// The erro r_ n o_ syste m_ resources.
        /// </summary>
        ERROR_NO_SYSTEM_RESOURCES = 1450, 

        /// <summary>
        /// The erro r_ nonpage d_ syste m_ resources.
        /// </summary>
        ERROR_NONPAGED_SYSTEM_RESOURCES = 1451, 

        /// <summary>
        /// The erro r_ page d_ syste m_ resources.
        /// </summary>
        ERROR_PAGED_SYSTEM_RESOURCES = 1452, 

        /// <summary>
        /// The erro r_ workin g_ se t_ quota.
        /// </summary>
        ERROR_WORKING_SET_QUOTA = 1453, 

        /// <summary>
        /// The erro r_ pagefil e_ quota.
        /// </summary>
        ERROR_PAGEFILE_QUOTA = 1454, 

        /// <summary>
        /// The erro r_ commitmen t_ limit.
        /// </summary>
        ERROR_COMMITMENT_LIMIT = 1455, 

        /// <summary>
        /// The erro r_ men u_ ite m_ no t_ found.
        /// </summary>
        ERROR_MENU_ITEM_NOT_FOUND = 1456, 

        /// <summary>
        /// The erro r_ invali d_ keyboar d_ handle.
        /// </summary>
        ERROR_INVALID_KEYBOARD_HANDLE = 1457, 

        /// <summary>
        /// The erro r_ hoo k_ typ e_ no t_ allowed.
        /// </summary>
        ERROR_HOOK_TYPE_NOT_ALLOWED = 1458, 

        /// <summary>
        /// The erro r_ require s_ interactiv e_ windowstation.
        /// </summary>
        ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION = 1459, 

        /// <summary>
        /// The erro r_ timeout.
        /// </summary>
        ERROR_TIMEOUT = 1460, 

        /// <summary>
        /// The erro r_ invali d_ monito r_ handle.
        /// </summary>
        ERROR_INVALID_MONITOR_HANDLE = 1461, 

        /// <summary>
        /// The erro r_ eventlo g_ fil e_ corrupt.
        /// </summary>
        ERROR_EVENTLOG_FILE_CORRUPT = 1500, 

        /// <summary>
        /// The erro r_ eventlo g_ can t_ start.
        /// </summary>
        ERROR_EVENTLOG_CANT_START = 1501, 

        /// <summary>
        /// The erro r_ lo g_ fil e_ full.
        /// </summary>
        ERROR_LOG_FILE_FULL = 1502, 

        /// <summary>
        /// The erro r_ eventlo g_ fil e_ changed.
        /// </summary>
        ERROR_EVENTLOG_FILE_CHANGED = 1503, 

        /// <summary>
        /// The erro r_ instal l_ servic e_ failure.
        /// </summary>
        ERROR_INSTALL_SERVICE_FAILURE = 1601, 

        /// <summary>
        /// The erro r_ instal l_ userexit.
        /// </summary>
        ERROR_INSTALL_USEREXIT = 1602, 

        /// <summary>
        /// The erro r_ instal l_ failure.
        /// </summary>
        ERROR_INSTALL_FAILURE = 1603, 

        /// <summary>
        /// The erro r_ instal l_ suspend.
        /// </summary>
        ERROR_INSTALL_SUSPEND = 1604, 

        /// <summary>
        /// The erro r_ unknow n_ product.
        /// </summary>
        ERROR_UNKNOWN_PRODUCT = 1605, 

        /// <summary>
        /// The erro r_ unknow n_ feature.
        /// </summary>
        ERROR_UNKNOWN_FEATURE = 1606, 

        /// <summary>
        /// The erro r_ unknow n_ component.
        /// </summary>
        ERROR_UNKNOWN_COMPONENT = 1607, 

        /// <summary>
        /// The erro r_ unknow n_ property.
        /// </summary>
        ERROR_UNKNOWN_PROPERTY = 1608, 

        /// <summary>
        /// The erro r_ invali d_ handl e_ state.
        /// </summary>
        ERROR_INVALID_HANDLE_STATE = 1609, 

        /// <summary>
        /// The erro r_ ba d_ configuration.
        /// </summary>
        ERROR_BAD_CONFIGURATION = 1610, 

        /// <summary>
        /// The erro r_ inde x_ absent.
        /// </summary>
        ERROR_INDEX_ABSENT = 1611, 

        /// <summary>
        /// The erro r_ instal l_ sourc e_ absent.
        /// </summary>
        ERROR_INSTALL_SOURCE_ABSENT = 1612, 

        /// <summary>
        /// The erro r_ instal l_ packag e_ version.
        /// </summary>
        ERROR_INSTALL_PACKAGE_VERSION = 1613, 

        /// <summary>
        /// The erro r_ produc t_ uninstalled.
        /// </summary>
        ERROR_PRODUCT_UNINSTALLED = 1614, 

        /// <summary>
        /// The erro r_ ba d_ quer y_ syntax.
        /// </summary>
        ERROR_BAD_QUERY_SYNTAX = 1615, 

        /// <summary>
        /// The erro r_ invali d_ field.
        /// </summary>
        ERROR_INVALID_FIELD = 1616, 

        /// <summary>
        /// The erro r_ devic e_ removed.
        /// </summary>
        ERROR_DEVICE_REMOVED = 1617, 

        /// <summary>
        /// The erro r_ instal l_ alread y_ running.
        /// </summary>
        ERROR_INSTALL_ALREADY_RUNNING = 1618, 

        /// <summary>
        /// The erro r_ instal l_ packag e_ ope n_ failed.
        /// </summary>
        ERROR_INSTALL_PACKAGE_OPEN_FAILED = 1619, 

        /// <summary>
        /// The erro r_ instal l_ packag e_ invalid.
        /// </summary>
        ERROR_INSTALL_PACKAGE_INVALID = 1620, 

        /// <summary>
        /// The erro r_ instal l_ u i_ failure.
        /// </summary>
        ERROR_INSTALL_UI_FAILURE = 1621, 

        /// <summary>
        /// The erro r_ instal l_ lo g_ failure.
        /// </summary>
        ERROR_INSTALL_LOG_FAILURE = 1622, 

        /// <summary>
        /// The erro r_ instal l_ languag e_ unsupported.
        /// </summary>
        ERROR_INSTALL_LANGUAGE_UNSUPPORTED = 1623, 

        /// <summary>
        /// The erro r_ instal l_ transfor m_ failure.
        /// </summary>
        ERROR_INSTALL_TRANSFORM_FAILURE = 1624, 

        /// <summary>
        /// The erro r_ instal l_ packag e_ rejected.
        /// </summary>
        ERROR_INSTALL_PACKAGE_REJECTED = 1625, 

        /// <summary>
        /// The erro r_ functio n_ no t_ called.
        /// </summary>
        ERROR_FUNCTION_NOT_CALLED = 1626, 

        /// <summary>
        /// The erro r_ functio n_ failed.
        /// </summary>
        ERROR_FUNCTION_FAILED = 1627, 

        /// <summary>
        /// The erro r_ invali d_ table.
        /// </summary>
        ERROR_INVALID_TABLE = 1628, 

        /// <summary>
        /// The erro r_ datatyp e_ mismatch.
        /// </summary>
        ERROR_DATATYPE_MISMATCH = 1629, 

        /// <summary>
        /// The erro r_ unsupporte d_ type.
        /// </summary>
        ERROR_UNSUPPORTED_TYPE = 1630, 

        /// <summary>
        /// The erro r_ creat e_ failed.
        /// </summary>
        ERROR_CREATE_FAILED = 1631, 

        /// <summary>
        /// The erro r_ instal l_ tem p_ unwritable.
        /// </summary>
        ERROR_INSTALL_TEMP_UNWRITABLE = 1632, 

        /// <summary>
        /// The erro r_ instal l_ platfor m_ unsupported.
        /// </summary>
        ERROR_INSTALL_PLATFORM_UNSUPPORTED = 1633, 

        /// <summary>
        /// The erro r_ instal l_ notused.
        /// </summary>
        ERROR_INSTALL_NOTUSED = 1634, 

        /// <summary>
        /// The erro r_ patc h_ packag e_ ope n_ failed.
        /// </summary>
        ERROR_PATCH_PACKAGE_OPEN_FAILED = 1635, 

        /// <summary>
        /// The erro r_ patc h_ packag e_ invalid.
        /// </summary>
        ERROR_PATCH_PACKAGE_INVALID = 1636, 

        /// <summary>
        /// The erro r_ patc h_ packag e_ unsupported.
        /// </summary>
        ERROR_PATCH_PACKAGE_UNSUPPORTED = 1637, 

        /// <summary>
        /// The erro r_ produc t_ version.
        /// </summary>
        ERROR_PRODUCT_VERSION = 1638, 

        /// <summary>
        /// The erro r_ invali d_ comman d_ line.
        /// </summary>
        ERROR_INVALID_COMMAND_LINE = 1639, 

        /// <summary>
        /// The erro r_ instal l_ remot e_ disallowed.
        /// </summary>
        ERROR_INSTALL_REMOTE_DISALLOWED = 1640, 

        /// <summary>
        /// The erro r_ succes s_ reboo t_ initiated.
        /// </summary>
        ERROR_SUCCESS_REBOOT_INITIATED = 1641, 

        /// <summary>
        /// The erro r_ patc h_ targe t_ no t_ found.
        /// </summary>
        ERROR_PATCH_TARGET_NOT_FOUND = 1642, 

        /// <summary>
        /// The erro r_ patc h_ packag e_ rejected.
        /// </summary>
        ERROR_PATCH_PACKAGE_REJECTED = 1643, 

        /// <summary>
        /// The erro r_ instal l_ transfor m_ rejected.
        /// </summary>
        ERROR_INSTALL_TRANSFORM_REJECTED = 1644, 

        /// <summary>
        /// The rp c_ s_ invali d_ strin g_ binding.
        /// </summary>
        RPC_S_INVALID_STRING_BINDING = 1700, 

        /// <summary>
        /// The rp c_ s_ wron g_ kin d_ o f_ binding.
        /// </summary>
        RPC_S_WRONG_KIND_OF_BINDING = 1701, 

        /// <summary>
        /// The rp c_ s_ invali d_ binding.
        /// </summary>
        RPC_S_INVALID_BINDING = 1702, 

        /// <summary>
        /// The rp c_ s_ protse q_ no t_ supported.
        /// </summary>
        RPC_S_PROTSEQ_NOT_SUPPORTED = 1703, 

        /// <summary>
        /// The rp c_ s_ invali d_ rp c_ protseq.
        /// </summary>
        RPC_S_INVALID_RPC_PROTSEQ = 1704, 

        /// <summary>
        /// The rp c_ s_ invali d_ strin g_ uuid.
        /// </summary>
        RPC_S_INVALID_STRING_UUID = 1705, 

        /// <summary>
        /// The rp c_ s_ invali d_ endpoin t_ format.
        /// </summary>
        RPC_S_INVALID_ENDPOINT_FORMAT = 1706, 

        /// <summary>
        /// The rp c_ s_ invali d_ ne t_ addr.
        /// </summary>
        RPC_S_INVALID_NET_ADDR = 1707, 

        /// <summary>
        /// The rp c_ s_ n o_ endpoin t_ found.
        /// </summary>
        RPC_S_NO_ENDPOINT_FOUND = 1708, 

        /// <summary>
        /// The rp c_ s_ invali d_ timeout.
        /// </summary>
        RPC_S_INVALID_TIMEOUT = 1709, 

        /// <summary>
        /// The rp c_ s_ objec t_ no t_ found.
        /// </summary>
        RPC_S_OBJECT_NOT_FOUND = 1710, 

        /// <summary>
        /// The rp c_ s_ alread y_ registered.
        /// </summary>
        RPC_S_ALREADY_REGISTERED = 1711, 

        /// <summary>
        /// The rp c_ s_ typ e_ alread y_ registered.
        /// </summary>
        RPC_S_TYPE_ALREADY_REGISTERED = 1712, 

        /// <summary>
        /// The rp c_ s_ alread y_ listening.
        /// </summary>
        RPC_S_ALREADY_LISTENING = 1713, 

        /// <summary>
        /// The rp c_ s_ n o_ protseq s_ registered.
        /// </summary>
        RPC_S_NO_PROTSEQS_REGISTERED = 1714, 

        /// <summary>
        /// The rp c_ s_ no t_ listening.
        /// </summary>
        RPC_S_NOT_LISTENING = 1715, 

        /// <summary>
        /// The rp c_ s_ unknow n_ mg r_ type.
        /// </summary>
        RPC_S_UNKNOWN_MGR_TYPE = 1716, 

        /// <summary>
        /// The rp c_ s_ unknow n_ if.
        /// </summary>
        RPC_S_UNKNOWN_IF = 1717, 

        /// <summary>
        /// The rp c_ s_ n o_ bindings.
        /// </summary>
        RPC_S_NO_BINDINGS = 1718, 

        /// <summary>
        /// The rp c_ s_ n o_ protseqs.
        /// </summary>
        RPC_S_NO_PROTSEQS = 1719, 

        /// <summary>
        /// The rp c_ s_ can t_ creat e_ endpoint.
        /// </summary>
        RPC_S_CANT_CREATE_ENDPOINT = 1720, 

        /// <summary>
        /// The rp c_ s_ ou t_ o f_ resources.
        /// </summary>
        RPC_S_OUT_OF_RESOURCES = 1721, 

        /// <summary>
        /// The rp c_ s_ serve r_ unavailable.
        /// </summary>
        RPC_S_SERVER_UNAVAILABLE = 1722, 

        /// <summary>
        /// The rp c_ s_ serve r_ to o_ busy.
        /// </summary>
        RPC_S_SERVER_TOO_BUSY = 1723, 

        /// <summary>
        /// The rp c_ s_ invali d_ networ k_ options.
        /// </summary>
        RPC_S_INVALID_NETWORK_OPTIONS = 1724, 

        /// <summary>
        /// The rp c_ s_ n o_ cal l_ active.
        /// </summary>
        RPC_S_NO_CALL_ACTIVE = 1725, 

        /// <summary>
        /// The rp c_ s_ cal l_ failed.
        /// </summary>
        RPC_S_CALL_FAILED = 1726, 

        /// <summary>
        /// The rp c_ s_ cal l_ faile d_ dne.
        /// </summary>
        RPC_S_CALL_FAILED_DNE = 1727, 

        /// <summary>
        /// The rp c_ s_ protoco l_ error.
        /// </summary>
        RPC_S_PROTOCOL_ERROR = 1728, 

        /// <summary>
        /// The rp c_ s_ unsupporte d_ tran s_ syn.
        /// </summary>
        RPC_S_UNSUPPORTED_TRANS_SYN = 1730, 

        /// <summary>
        /// The rp c_ s_ unsupporte d_ type.
        /// </summary>
        RPC_S_UNSUPPORTED_TYPE = 1732, 

        /// <summary>
        /// The rp c_ s_ invali d_ tag.
        /// </summary>
        RPC_S_INVALID_TAG = 1733, 

        /// <summary>
        /// The rp c_ s_ invali d_ bound.
        /// </summary>
        RPC_S_INVALID_BOUND = 1734, 

        /// <summary>
        /// The rp c_ s_ n o_ entr y_ name.
        /// </summary>
        RPC_S_NO_ENTRY_NAME = 1735, 

        /// <summary>
        /// The rp c_ s_ invali d_ nam e_ syntax.
        /// </summary>
        RPC_S_INVALID_NAME_SYNTAX = 1736, 

        /// <summary>
        /// The rp c_ s_ unsupporte d_ nam e_ syntax.
        /// </summary>
        RPC_S_UNSUPPORTED_NAME_SYNTAX = 1737, 

        /// <summary>
        /// The rp c_ s_ uui d_ n o_ address.
        /// </summary>
        RPC_S_UUID_NO_ADDRESS = 1739, 

        /// <summary>
        /// The rp c_ s_ duplicat e_ endpoint.
        /// </summary>
        RPC_S_DUPLICATE_ENDPOINT = 1740, 

        /// <summary>
        /// The rp c_ s_ unknow n_ auth n_ type.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_TYPE = 1741, 

        /// <summary>
        /// The rp c_ s_ ma x_ call s_ to o_ small.
        /// </summary>
        RPC_S_MAX_CALLS_TOO_SMALL = 1742, 

        /// <summary>
        /// The rp c_ s_ strin g_ to o_ long.
        /// </summary>
        RPC_S_STRING_TOO_LONG = 1743, 

        /// <summary>
        /// The rp c_ s_ protse q_ no t_ found.
        /// </summary>
        RPC_S_PROTSEQ_NOT_FOUND = 1744, 

        /// <summary>
        /// The rp c_ s_ procnu m_ ou t_ o f_ range.
        /// </summary>
        RPC_S_PROCNUM_OUT_OF_RANGE = 1745, 

        /// <summary>
        /// The rp c_ s_ bindin g_ ha s_ n o_ auth.
        /// </summary>
        RPC_S_BINDING_HAS_NO_AUTH = 1746, 

        /// <summary>
        /// The rp c_ s_ unknow n_ auth n_ service.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_SERVICE = 1747, 

        /// <summary>
        /// The rp c_ s_ unknow n_ auth n_ level.
        /// </summary>
        RPC_S_UNKNOWN_AUTHN_LEVEL = 1748, 

        /// <summary>
        /// The rp c_ s_ invali d_ aut h_ identity.
        /// </summary>
        RPC_S_INVALID_AUTH_IDENTITY = 1749, 

        /// <summary>
        /// The rp c_ s_ unknow n_ auth z_ service.
        /// </summary>
        RPC_S_UNKNOWN_AUTHZ_SERVICE = 1750, 

        /// <summary>
        /// The ep t_ s_ invali d_ entry.
        /// </summary>
        EPT_S_INVALID_ENTRY = 1751, 

        /// <summary>
        /// The ep t_ s_ can t_ perfor m_ op.
        /// </summary>
        EPT_S_CANT_PERFORM_OP = 1752, 

        /// <summary>
        /// The ep t_ s_ no t_ registered.
        /// </summary>
        EPT_S_NOT_REGISTERED = 1753, 

        /// <summary>
        /// The rp c_ s_ nothin g_ t o_ export.
        /// </summary>
        RPC_S_NOTHING_TO_EXPORT = 1754, 

        /// <summary>
        /// The rp c_ s_ incomplet e_ name.
        /// </summary>
        RPC_S_INCOMPLETE_NAME = 1755, 

        /// <summary>
        /// The rp c_ s_ invali d_ ver s_ option.
        /// </summary>
        RPC_S_INVALID_VERS_OPTION = 1756, 

        /// <summary>
        /// The rp c_ s_ n o_ mor e_ members.
        /// </summary>
        RPC_S_NO_MORE_MEMBERS = 1757, 

        /// <summary>
        /// The rp c_ s_ no t_ al l_ obj s_ unexported.
        /// </summary>
        RPC_S_NOT_ALL_OBJS_UNEXPORTED = 1758, 

        /// <summary>
        /// The rp c_ s_ interfac e_ no t_ found.
        /// </summary>
        RPC_S_INTERFACE_NOT_FOUND = 1759, 

        /// <summary>
        /// The rp c_ s_ entr y_ alread y_ exists.
        /// </summary>
        RPC_S_ENTRY_ALREADY_EXISTS = 1760, 

        /// <summary>
        /// The rp c_ s_ entr y_ no t_ found.
        /// </summary>
        RPC_S_ENTRY_NOT_FOUND = 1761, 

        /// <summary>
        /// The rp c_ s_ nam e_ servic e_ unavailable.
        /// </summary>
        RPC_S_NAME_SERVICE_UNAVAILABLE = 1762, 

        /// <summary>
        /// The rp c_ s_ invali d_ na f_ id.
        /// </summary>
        RPC_S_INVALID_NAF_ID = 1763, 

        /// <summary>
        /// The rp c_ s_ canno t_ support.
        /// </summary>
        RPC_S_CANNOT_SUPPORT = 1764, 

        /// <summary>
        /// The rp c_ s_ n o_ contex t_ available.
        /// </summary>
        RPC_S_NO_CONTEXT_AVAILABLE = 1765, 

        /// <summary>
        /// The rp c_ s_ interna l_ error.
        /// </summary>
        RPC_S_INTERNAL_ERROR = 1766, 

        /// <summary>
        /// The rp c_ s_ zer o_ divide.
        /// </summary>
        RPC_S_ZERO_DIVIDE = 1767, 

        /// <summary>
        /// The rp c_ s_ addres s_ error.
        /// </summary>
        RPC_S_ADDRESS_ERROR = 1768, 

        /// <summary>
        /// The rp c_ s_ f p_ di v_ zero.
        /// </summary>
        RPC_S_FP_DIV_ZERO = 1769, 

        /// <summary>
        /// The rp c_ s_ f p_ underflow.
        /// </summary>
        RPC_S_FP_UNDERFLOW = 1770, 

        /// <summary>
        /// The rp c_ s_ f p_ overflow.
        /// </summary>
        RPC_S_FP_OVERFLOW = 1771, 

        /// <summary>
        /// The rp c_ x_ n o_ mor e_ entries.
        /// </summary>
        RPC_X_NO_MORE_ENTRIES = 1772, 

        /// <summary>
        /// The rp c_ x_ s s_ cha r_ tran s_ ope n_ fail.
        /// </summary>
        RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 1773, 

        /// <summary>
        /// The rp c_ x_ s s_ cha r_ tran s_ shor t_ file.
        /// </summary>
        RPC_X_SS_CHAR_TRANS_SHORT_FILE = 1774, 

        /// <summary>
        /// The rp c_ x_ s s_ i n_ nul l_ context.
        /// </summary>
        RPC_X_SS_IN_NULL_CONTEXT = 1775, 

        /// <summary>
        /// The rp c_ x_ s s_ contex t_ damaged.
        /// </summary>
        RPC_X_SS_CONTEXT_DAMAGED = 1777, 

        /// <summary>
        /// The rp c_ x_ s s_ handle s_ mismatch.
        /// </summary>
        RPC_X_SS_HANDLES_MISMATCH = 1778, 

        /// <summary>
        /// The rp c_ x_ s s_ canno t_ ge t_ cal l_ handle.
        /// </summary>
        RPC_X_SS_CANNOT_GET_CALL_HANDLE = 1779, 

        /// <summary>
        /// The rp c_ x_ nul l_ re f_ pointer.
        /// </summary>
        RPC_X_NULL_REF_POINTER = 1780, 

        /// <summary>
        /// The rp c_ x_ enu m_ valu e_ ou t_ o f_ range.
        /// </summary>
        RPC_X_ENUM_VALUE_OUT_OF_RANGE = 1781, 

        /// <summary>
        /// The rp c_ x_ byt e_ coun t_ to o_ small.
        /// </summary>
        RPC_X_BYTE_COUNT_TOO_SMALL = 1782, 

        /// <summary>
        /// The rp c_ x_ ba d_ stu b_ data.
        /// </summary>
        RPC_X_BAD_STUB_DATA = 1783, 

        /// <summary>
        /// The erro r_ invali d_ use r_ buffer.
        /// </summary>
        ERROR_INVALID_USER_BUFFER = 1784, 

        /// <summary>
        /// The erro r_ unrecognize d_ media.
        /// </summary>
        ERROR_UNRECOGNIZED_MEDIA = 1785, 

        /// <summary>
        /// The erro r_ n o_ trus t_ ls a_ secret.
        /// </summary>
        ERROR_NO_TRUST_LSA_SECRET = 1786, 

        /// <summary>
        /// The erro r_ n o_ trus t_ sa m_ account.
        /// </summary>
        ERROR_NO_TRUST_SAM_ACCOUNT = 1787, 

        /// <summary>
        /// The erro r_ truste d_ domai n_ failure.
        /// </summary>
        ERROR_TRUSTED_DOMAIN_FAILURE = 1788, 

        /// <summary>
        /// The erro r_ truste d_ relationshi p_ failure.
        /// </summary>
        ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789, 

        /// <summary>
        /// The erro r_ trus t_ failure.
        /// </summary>
        ERROR_TRUST_FAILURE = 1790, 

        /// <summary>
        /// The rp c_ s_ cal l_ i n_ progress.
        /// </summary>
        RPC_S_CALL_IN_PROGRESS = 1791, 

        /// <summary>
        /// The erro r_ netlogo n_ no t_ started.
        /// </summary>
        ERROR_NETLOGON_NOT_STARTED = 1792, 

        /// <summary>
        /// The erro r_ accoun t_ expired.
        /// </summary>
        ERROR_ACCOUNT_EXPIRED = 1793, 

        /// <summary>
        /// The erro r_ redirecto r_ ha s_ ope n_ handles.
        /// </summary>
        ERROR_REDIRECTOR_HAS_OPEN_HANDLES = 1794, 

        /// <summary>
        /// The erro r_ printe r_ drive r_ alread y_ installed.
        /// </summary>
        ERROR_PRINTER_DRIVER_ALREADY_INSTALLED = 1795, 

        /// <summary>
        /// The erro r_ unknow n_ port.
        /// </summary>
        ERROR_UNKNOWN_PORT = 1796, 

        /// <summary>
        /// The erro r_ unknow n_ printe r_ driver.
        /// </summary>
        ERROR_UNKNOWN_PRINTER_DRIVER = 1797, 

        /// <summary>
        /// The erro r_ unknow n_ printprocessor.
        /// </summary>
        ERROR_UNKNOWN_PRINTPROCESSOR = 1798, 

        /// <summary>
        /// The erro r_ invali d_ separato r_ file.
        /// </summary>
        ERROR_INVALID_SEPARATOR_FILE = 1799, 

        /// <summary>
        /// The erro r_ invali d_ priority.
        /// </summary>
        ERROR_INVALID_PRIORITY = 1800, 

        /// <summary>
        /// The erro r_ invali d_ printe r_ name.
        /// </summary>
        ERROR_INVALID_PRINTER_NAME = 1801, 

        /// <summary>
        /// The erro r_ printe r_ alread y_ exists.
        /// </summary>
        ERROR_PRINTER_ALREADY_EXISTS = 1802, 

        /// <summary>
        /// The erro r_ invali d_ printe r_ command.
        /// </summary>
        ERROR_INVALID_PRINTER_COMMAND = 1803, 

        /// <summary>
        /// The erro r_ invali d_ datatype.
        /// </summary>
        ERROR_INVALID_DATATYPE = 1804, 

        /// <summary>
        /// The erro r_ invali d_ environment.
        /// </summary>
        ERROR_INVALID_ENVIRONMENT = 1805, 

        /// <summary>
        /// The rp c_ s_ n o_ mor e_ bindings.
        /// </summary>
        RPC_S_NO_MORE_BINDINGS = 1806, 

        /// <summary>
        /// The erro r_ nologo n_ interdomai n_ trus t_ account.
        /// </summary>
        ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = 1807, 

        /// <summary>
        /// The erro r_ nologo n_ workstatio n_ trus t_ account.
        /// </summary>
        ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT = 1808, 

        /// <summary>
        /// The erro r_ nologo n_ serve r_ trus t_ account.
        /// </summary>
        ERROR_NOLOGON_SERVER_TRUST_ACCOUNT = 1809, 

        /// <summary>
        /// The erro r_ domai n_ trus t_ inconsistent.
        /// </summary>
        ERROR_DOMAIN_TRUST_INCONSISTENT = 1810, 

        /// <summary>
        /// The erro r_ serve r_ ha s_ ope n_ handles.
        /// </summary>
        ERROR_SERVER_HAS_OPEN_HANDLES = 1811, 

        /// <summary>
        /// The erro r_ resourc e_ dat a_ no t_ found.
        /// </summary>
        ERROR_RESOURCE_DATA_NOT_FOUND = 1812, 

        /// <summary>
        /// The erro r_ resourc e_ typ e_ no t_ found.
        /// </summary>
        ERROR_RESOURCE_TYPE_NOT_FOUND = 1813, 

        /// <summary>
        /// The erro r_ resourc e_ nam e_ no t_ found.
        /// </summary>
        ERROR_RESOURCE_NAME_NOT_FOUND = 1814, 

        /// <summary>
        /// The erro r_ resourc e_ lan g_ no t_ found.
        /// </summary>
        ERROR_RESOURCE_LANG_NOT_FOUND = 1815, 

        /// <summary>
        /// The erro r_ no t_ enoug h_ quota.
        /// </summary>
        ERROR_NOT_ENOUGH_QUOTA = 1816, 

        /// <summary>
        /// The rp c_ s_ n o_ interfaces.
        /// </summary>
        RPC_S_NO_INTERFACES = 1817, 

        /// <summary>
        /// The rp c_ s_ cal l_ cancelled.
        /// </summary>
        RPC_S_CALL_CANCELLED = 1818, 

        /// <summary>
        /// The rp c_ s_ bindin g_ incomplete.
        /// </summary>
        RPC_S_BINDING_INCOMPLETE = 1819, 

        /// <summary>
        /// The rp c_ s_ com m_ failure.
        /// </summary>
        RPC_S_COMM_FAILURE = 1820, 

        /// <summary>
        /// The rp c_ s_ unsupporte d_ auth n_ level.
        /// </summary>
        RPC_S_UNSUPPORTED_AUTHN_LEVEL = 1821, 

        /// <summary>
        /// The rp c_ s_ n o_ prin c_ name.
        /// </summary>
        RPC_S_NO_PRINC_NAME = 1822, 

        /// <summary>
        /// The rp c_ s_ no t_ rp c_ error.
        /// </summary>
        RPC_S_NOT_RPC_ERROR = 1823, 

        /// <summary>
        /// The rp c_ s_ uui d_ loca l_ only.
        /// </summary>
        RPC_S_UUID_LOCAL_ONLY = 1824, 

        /// <summary>
        /// The rp c_ s_ se c_ pk g_ error.
        /// </summary>
        RPC_S_SEC_PKG_ERROR = 1825, 

        /// <summary>
        /// The rp c_ s_ no t_ cancelled.
        /// </summary>
        RPC_S_NOT_CANCELLED = 1826, 

        /// <summary>
        /// The rp c_ x_ invali d_ e s_ action.
        /// </summary>
        RPC_X_INVALID_ES_ACTION = 1827, 

        /// <summary>
        /// The rp c_ x_ wron g_ e s_ version.
        /// </summary>
        RPC_X_WRONG_ES_VERSION = 1828, 

        /// <summary>
        /// The rp c_ x_ wron g_ stu b_ version.
        /// </summary>
        RPC_X_WRONG_STUB_VERSION = 1829, 

        /// <summary>
        /// The rp c_ x_ invali d_ pip e_ object.
        /// </summary>
        RPC_X_INVALID_PIPE_OBJECT = 1830, 

        /// <summary>
        /// The rp c_ x_ wron g_ pip e_ order.
        /// </summary>
        RPC_X_WRONG_PIPE_ORDER = 1831, 

        /// <summary>
        /// The rp c_ x_ wron g_ pip e_ version.
        /// </summary>
        RPC_X_WRONG_PIPE_VERSION = 1832, 

        /// <summary>
        /// The rp c_ s_ grou p_ membe r_ no t_ found.
        /// </summary>
        RPC_S_GROUP_MEMBER_NOT_FOUND = 1898, 

        /// <summary>
        /// The ep t_ s_ can t_ create.
        /// </summary>
        EPT_S_CANT_CREATE = 1899, 

        /// <summary>
        /// The rp c_ s_ invali d_ object.
        /// </summary>
        RPC_S_INVALID_OBJECT = 1900, 

        /// <summary>
        /// The erro r_ invali d_ time.
        /// </summary>
        ERROR_INVALID_TIME = 1901, 

        /// <summary>
        /// The erro r_ invali d_ for m_ name.
        /// </summary>
        ERROR_INVALID_FORM_NAME = 1902, 

        /// <summary>
        /// The erro r_ invali d_ for m_ size.
        /// </summary>
        ERROR_INVALID_FORM_SIZE = 1903, 

        /// <summary>
        /// The erro r_ alread y_ waiting.
        /// </summary>
        ERROR_ALREADY_WAITING = 1904, 

        /// <summary>
        /// The erro r_ printe r_ deleted.
        /// </summary>
        ERROR_PRINTER_DELETED = 1905, 

        /// <summary>
        /// The erro r_ invali d_ printe r_ state.
        /// </summary>
        ERROR_INVALID_PRINTER_STATE = 1906, 

        /// <summary>
        /// The erro r_ passwor d_ mus t_ change.
        /// </summary>
        ERROR_PASSWORD_MUST_CHANGE = 1907, 

        /// <summary>
        /// The erro r_ domai n_ controlle r_ no t_ found.
        /// </summary>
        ERROR_DOMAIN_CONTROLLER_NOT_FOUND = 1908, 

        /// <summary>
        /// The erro r_ accoun t_ locke d_ out.
        /// </summary>
        ERROR_ACCOUNT_LOCKED_OUT = 1909, 

        /// <summary>
        /// The o r_ invali d_ oxid.
        /// </summary>
        OR_INVALID_OXID = 1910, 

        /// <summary>
        /// The o r_ invali d_ oid.
        /// </summary>
        OR_INVALID_OID = 1911, 

        /// <summary>
        /// The o r_ invali d_ set.
        /// </summary>
        OR_INVALID_SET = 1912, 

        /// <summary>
        /// The rp c_ s_ sen d_ incomplete.
        /// </summary>
        RPC_S_SEND_INCOMPLETE = 1913, 

        /// <summary>
        /// The rp c_ s_ invali d_ asyn c_ handle.
        /// </summary>
        RPC_S_INVALID_ASYNC_HANDLE = 1914, 

        /// <summary>
        /// The rp c_ s_ invali d_ asyn c_ call.
        /// </summary>
        RPC_S_INVALID_ASYNC_CALL = 1915, 

        /// <summary>
        /// The rp c_ x_ pip e_ closed.
        /// </summary>
        RPC_X_PIPE_CLOSED = 1916, 

        /// <summary>
        /// The rp c_ x_ pip e_ disciplin e_ error.
        /// </summary>
        RPC_X_PIPE_DISCIPLINE_ERROR = 1917, 

        /// <summary>
        /// The rp c_ x_ pip e_ empty.
        /// </summary>
        RPC_X_PIPE_EMPTY = 1918, 

        /// <summary>
        /// The erro r_ n o_ sitename.
        /// </summary>
        ERROR_NO_SITENAME = 1919, 

        /// <summary>
        /// The erro r_ can t_ acces s_ file.
        /// </summary>
        ERROR_CANT_ACCESS_FILE = 1920, 

        /// <summary>
        /// The erro r_ can t_ resolv e_ filename.
        /// </summary>
        ERROR_CANT_RESOLVE_FILENAME = 1921, 

        /// <summary>
        /// The rp c_ s_ entr y_ typ e_ mismatch.
        /// </summary>
        RPC_S_ENTRY_TYPE_MISMATCH = 1922, 

        /// <summary>
        /// The rp c_ s_ no t_ al l_ obj s_ exported.
        /// </summary>
        RPC_S_NOT_ALL_OBJS_EXPORTED = 1923, 

        /// <summary>
        /// The rp c_ s_ interfac e_ no t_ exported.
        /// </summary>
        RPC_S_INTERFACE_NOT_EXPORTED = 1924, 

        /// <summary>
        /// The rp c_ s_ profil e_ no t_ added.
        /// </summary>
        RPC_S_PROFILE_NOT_ADDED = 1925, 

        /// <summary>
        /// The rp c_ s_ pr f_ el t_ no t_ added.
        /// </summary>
        RPC_S_PRF_ELT_NOT_ADDED = 1926, 

        /// <summary>
        /// The rp c_ s_ pr f_ el t_ no t_ removed.
        /// </summary>
        RPC_S_PRF_ELT_NOT_REMOVED = 1927, 

        /// <summary>
        /// The rp c_ s_ gr p_ el t_ no t_ added.
        /// </summary>
        RPC_S_GRP_ELT_NOT_ADDED = 1928, 

        /// <summary>
        /// The rp c_ s_ gr p_ el t_ no t_ removed.
        /// </summary>
        RPC_S_GRP_ELT_NOT_REMOVED = 1929, 

        /// <summary>
        /// The erro r_ k m_ drive r_ blocked.
        /// </summary>
        ERROR_KM_DRIVER_BLOCKED = 1930, 

        /// <summary>
        /// The erro r_ contex t_ expired.
        /// </summary>
        ERROR_CONTEXT_EXPIRED = 1931, 

        /// <summary>
        /// The erro r_ invali d_ pixe l_ format.
        /// </summary>
        ERROR_INVALID_PIXEL_FORMAT = 2000, 

        /// <summary>
        /// The erro r_ ba d_ driver.
        /// </summary>
        ERROR_BAD_DRIVER = 2001, 

        /// <summary>
        /// The erro r_ invali d_ windo w_ style.
        /// </summary>
        ERROR_INVALID_WINDOW_STYLE = 2002, 

        /// <summary>
        /// The erro r_ metafil e_ no t_ supported.
        /// </summary>
        ERROR_METAFILE_NOT_SUPPORTED = 2003, 

        /// <summary>
        /// The erro r_ transfor m_ no t_ supported.
        /// </summary>
        ERROR_TRANSFORM_NOT_SUPPORTED = 2004, 

        /// <summary>
        /// The erro r_ clippin g_ no t_ supported.
        /// </summary>
        ERROR_CLIPPING_NOT_SUPPORTED = 2005, 

        /// <summary>
        /// The erro r_ invali d_ cmm.
        /// </summary>
        ERROR_INVALID_CMM = 2010, 

        /// <summary>
        /// The erro r_ invali d_ profile.
        /// </summary>
        ERROR_INVALID_PROFILE = 2011, 

        /// <summary>
        /// The erro r_ ta g_ no t_ found.
        /// </summary>
        ERROR_TAG_NOT_FOUND = 2012, 

        /// <summary>
        /// The erro r_ ta g_ no t_ present.
        /// </summary>
        ERROR_TAG_NOT_PRESENT = 2013, 

        /// <summary>
        /// The erro r_ duplicat e_ tag.
        /// </summary>
        ERROR_DUPLICATE_TAG = 2014, 

        /// <summary>
        /// The erro r_ profil e_ no t_ associate d_ wit h_ device.
        /// </summary>
        ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE = 2015, 

        /// <summary>
        /// The erro r_ profil e_ no t_ found.
        /// </summary>
        ERROR_PROFILE_NOT_FOUND = 2016, 

        /// <summary>
        /// The erro r_ invali d_ colorspace.
        /// </summary>
        ERROR_INVALID_COLORSPACE = 2017, 

        /// <summary>
        /// The erro r_ ic m_ no t_ enabled.
        /// </summary>
        ERROR_ICM_NOT_ENABLED = 2018, 

        /// <summary>
        /// The erro r_ deletin g_ ic m_ xform.
        /// </summary>
        ERROR_DELETING_ICM_XFORM = 2019, 

        /// <summary>
        /// The erro r_ invali d_ transform.
        /// </summary>
        ERROR_INVALID_TRANSFORM = 2020, 

        /// <summary>
        /// The erro r_ colorspac e_ mismatch.
        /// </summary>
        ERROR_COLORSPACE_MISMATCH = 2021, 

        /// <summary>
        /// The erro r_ invali d_ colorindex.
        /// </summary>
        ERROR_INVALID_COLORINDEX = 2022, 

        /// <summary>
        /// The erro r_ connecte d_ othe r_ password.
        /// </summary>
        ERROR_CONNECTED_OTHER_PASSWORD = 2108, 

        /// <summary>
        /// The erro r_ connecte d_ othe r_ passwor d_ default.
        /// </summary>
        ERROR_CONNECTED_OTHER_PASSWORD_DEFAULT = 2109, 

        /// <summary>
        /// The erro r_ ba d_ username.
        /// </summary>
        ERROR_BAD_USERNAME = 2202, 

        /// <summary>
        /// The erro r_ no t_ connected.
        /// </summary>
        ERROR_NOT_CONNECTED = 2250, 

        /// <summary>
        /// The erro r_ ope n_ files.
        /// </summary>
        ERROR_OPEN_FILES = 2401, 

        /// <summary>
        /// The erro r_ activ e_ connections.
        /// </summary>
        ERROR_ACTIVE_CONNECTIONS = 2402, 

        /// <summary>
        /// The erro r_ devic e_ i n_ use.
        /// </summary>
        ERROR_DEVICE_IN_USE = 2404, 

        /// <summary>
        /// The erro r_ unknow n_ prin t_ monitor.
        /// </summary>
        ERROR_UNKNOWN_PRINT_MONITOR = 3000, 

        /// <summary>
        /// The erro r_ printe r_ drive r_ i n_ use.
        /// </summary>
        ERROR_PRINTER_DRIVER_IN_USE = 3001, 

        /// <summary>
        /// The erro r_ spoo l_ fil e_ no t_ found.
        /// </summary>
        ERROR_SPOOL_FILE_NOT_FOUND = 3002, 

        /// <summary>
        /// The erro r_ sp l_ n o_ startdoc.
        /// </summary>
        ERROR_SPL_NO_STARTDOC = 3003, 

        /// <summary>
        /// The erro r_ sp l_ n o_ addjob.
        /// </summary>
        ERROR_SPL_NO_ADDJOB = 3004, 

        /// <summary>
        /// The erro r_ prin t_ processo r_ alread y_ installed.
        /// </summary>
        ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 3005, 

        /// <summary>
        /// The erro r_ prin t_ monito r_ alread y_ installed.
        /// </summary>
        ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 3006, 

        /// <summary>
        /// The erro r_ invali d_ prin t_ monitor.
        /// </summary>
        ERROR_INVALID_PRINT_MONITOR = 3007, 

        /// <summary>
        /// The erro r_ prin t_ monito r_ i n_ use.
        /// </summary>
        ERROR_PRINT_MONITOR_IN_USE = 3008, 

        /// <summary>
        /// The erro r_ printe r_ ha s_ job s_ queued.
        /// </summary>
        ERROR_PRINTER_HAS_JOBS_QUEUED = 3009, 

        /// <summary>
        /// The erro r_ succes s_ reboo t_ required.
        /// </summary>
        ERROR_SUCCESS_REBOOT_REQUIRED = 3010, 

        /// <summary>
        /// The erro r_ succes s_ restar t_ required.
        /// </summary>
        ERROR_SUCCESS_RESTART_REQUIRED = 3011, 

        /// <summary>
        /// The erro r_ printe r_ no t_ found.
        /// </summary>
        ERROR_PRINTER_NOT_FOUND = 3012, 

        /// <summary>
        /// The erro r_ printe r_ drive r_ warned.
        /// </summary>
        ERROR_PRINTER_DRIVER_WARNED = 3013, 

        /// <summary>
        /// The erro r_ printe r_ drive r_ blocked.
        /// </summary>
        ERROR_PRINTER_DRIVER_BLOCKED = 3014, 

        /// <summary>
        /// The erro r_ win s_ internal.
        /// </summary>
        ERROR_WINS_INTERNAL = 4000, 

        /// <summary>
        /// The erro r_ ca n_ no t_ de l_ loca l_ wins.
        /// </summary>
        ERROR_CAN_NOT_DEL_LOCAL_WINS = 4001, 

        /// <summary>
        /// The erro r_ stati c_ init.
        /// </summary>
        ERROR_STATIC_INIT = 4002, 

        /// <summary>
        /// The erro r_ in c_ backup.
        /// </summary>
        ERROR_INC_BACKUP = 4003, 

        /// <summary>
        /// The erro r_ ful l_ backup.
        /// </summary>
        ERROR_FULL_BACKUP = 4004, 

        /// <summary>
        /// The erro r_ re c_ no n_ existent.
        /// </summary>
        ERROR_REC_NON_EXISTENT = 4005, 

        /// <summary>
        /// The erro r_ rp l_ no t_ allowed.
        /// </summary>
        ERROR_RPL_NOT_ALLOWED = 4006, 

        /// <summary>
        /// The erro r_ dhc p_ addres s_ conflict.
        /// </summary>
        ERROR_DHCP_ADDRESS_CONFLICT = 4100, 

        /// <summary>
        /// The erro r_ wm i_ gui d_ no t_ found.
        /// </summary>
        ERROR_WMI_GUID_NOT_FOUND = 4200, 

        /// <summary>
        /// The erro r_ wm i_ instanc e_ no t_ found.
        /// </summary>
        ERROR_WMI_INSTANCE_NOT_FOUND = 4201, 

        /// <summary>
        /// The erro r_ wm i_ itemi d_ no t_ found.
        /// </summary>
        ERROR_WMI_ITEMID_NOT_FOUND = 4202, 

        /// <summary>
        /// The erro r_ wm i_ tr y_ again.
        /// </summary>
        ERROR_WMI_TRY_AGAIN = 4203, 

        /// <summary>
        /// The erro r_ wm i_ d p_ no t_ found.
        /// </summary>
        ERROR_WMI_DP_NOT_FOUND = 4204, 

        /// <summary>
        /// The erro r_ wm i_ unresolve d_ instanc e_ ref.
        /// </summary>
        ERROR_WMI_UNRESOLVED_INSTANCE_REF = 4205, 

        /// <summary>
        /// The erro r_ wm i_ alread y_ enabled.
        /// </summary>
        ERROR_WMI_ALREADY_ENABLED = 4206, 

        /// <summary>
        /// The erro r_ wm i_ gui d_ disconnected.
        /// </summary>
        ERROR_WMI_GUID_DISCONNECTED = 4207, 

        /// <summary>
        /// The erro r_ wm i_ serve r_ unavailable.
        /// </summary>
        ERROR_WMI_SERVER_UNAVAILABLE = 4208, 

        /// <summary>
        /// The erro r_ wm i_ d p_ failed.
        /// </summary>
        ERROR_WMI_DP_FAILED = 4209, 

        /// <summary>
        /// The erro r_ wm i_ invali d_ mof.
        /// </summary>
        ERROR_WMI_INVALID_MOF = 4210, 

        /// <summary>
        /// The erro r_ wm i_ invali d_ reginfo.
        /// </summary>
        ERROR_WMI_INVALID_REGINFO = 4211, 

        /// <summary>
        /// The erro r_ wm i_ alread y_ disabled.
        /// </summary>
        ERROR_WMI_ALREADY_DISABLED = 4212, 

        /// <summary>
        /// The erro r_ wm i_ rea d_ only.
        /// </summary>
        ERROR_WMI_READ_ONLY = 4213, 

        /// <summary>
        /// The erro r_ wm i_ se t_ failure.
        /// </summary>
        ERROR_WMI_SET_FAILURE = 4214, 

        /// <summary>
        /// The erro r_ invali d_ media.
        /// </summary>
        ERROR_INVALID_MEDIA = 4300, 

        /// <summary>
        /// The erro r_ invali d_ library.
        /// </summary>
        ERROR_INVALID_LIBRARY = 4301, 

        /// <summary>
        /// The erro r_ invali d_ medi a_ pool.
        /// </summary>
        ERROR_INVALID_MEDIA_POOL = 4302, 

        /// <summary>
        /// The erro r_ driv e_ medi a_ mismatch.
        /// </summary>
        ERROR_DRIVE_MEDIA_MISMATCH = 4303, 

        /// <summary>
        /// The erro r_ medi a_ offline.
        /// </summary>
        ERROR_MEDIA_OFFLINE = 4304, 

        /// <summary>
        /// The erro r_ librar y_ offline.
        /// </summary>
        ERROR_LIBRARY_OFFLINE = 4305, 

        /// <summary>
        /// The erro r_ empty.
        /// </summary>
        ERROR_EMPTY = 4306, 

        /// <summary>
        /// The erro r_ no t_ empty.
        /// </summary>
        ERROR_NOT_EMPTY = 4307, 

        /// <summary>
        /// The erro r_ medi a_ unavailable.
        /// </summary>
        ERROR_MEDIA_UNAVAILABLE = 4308, 

        /// <summary>
        /// The erro r_ resourc e_ disabled.
        /// </summary>
        ERROR_RESOURCE_DISABLED = 4309, 

        /// <summary>
        /// The erro r_ invali d_ cleaner.
        /// </summary>
        ERROR_INVALID_CLEANER = 4310, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ clean.
        /// </summary>
        ERROR_UNABLE_TO_CLEAN = 4311, 

        /// <summary>
        /// The erro r_ objec t_ no t_ found.
        /// </summary>
        ERROR_OBJECT_NOT_FOUND = 4312, 

        /// <summary>
        /// The erro r_ databas e_ failure.
        /// </summary>
        ERROR_DATABASE_FAILURE = 4313, 

        /// <summary>
        /// The erro r_ databas e_ full.
        /// </summary>
        ERROR_DATABASE_FULL = 4314, 

        /// <summary>
        /// The erro r_ medi a_ incompatible.
        /// </summary>
        ERROR_MEDIA_INCOMPATIBLE = 4315, 

        /// <summary>
        /// The erro r_ resourc e_ no t_ present.
        /// </summary>
        ERROR_RESOURCE_NOT_PRESENT = 4316, 

        /// <summary>
        /// The erro r_ invali d_ operation.
        /// </summary>
        ERROR_INVALID_OPERATION = 4317, 

        /// <summary>
        /// The erro r_ medi a_ no t_ available.
        /// </summary>
        ERROR_MEDIA_NOT_AVAILABLE = 4318, 

        /// <summary>
        /// The erro r_ devic e_ no t_ available.
        /// </summary>
        ERROR_DEVICE_NOT_AVAILABLE = 4319, 

        /// <summary>
        /// The erro r_ reques t_ refused.
        /// </summary>
        ERROR_REQUEST_REFUSED = 4320, 

        /// <summary>
        /// The erro r_ invali d_ driv e_ object.
        /// </summary>
        ERROR_INVALID_DRIVE_OBJECT = 4321, 

        /// <summary>
        /// The erro r_ librar y_ full.
        /// </summary>
        ERROR_LIBRARY_FULL = 4322, 

        /// <summary>
        /// The erro r_ mediu m_ no t_ accessible.
        /// </summary>
        ERROR_MEDIUM_NOT_ACCESSIBLE = 4323, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ loa d_ medium.
        /// </summary>
        ERROR_UNABLE_TO_LOAD_MEDIUM = 4324, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ inventor y_ drive.
        /// </summary>
        ERROR_UNABLE_TO_INVENTORY_DRIVE = 4325, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ inventor y_ slot.
        /// </summary>
        ERROR_UNABLE_TO_INVENTORY_SLOT = 4326, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ inventor y_ transport.
        /// </summary>
        ERROR_UNABLE_TO_INVENTORY_TRANSPORT = 4327, 

        /// <summary>
        /// The erro r_ transpor t_ full.
        /// </summary>
        ERROR_TRANSPORT_FULL = 4328, 

        /// <summary>
        /// The erro r_ controllin g_ ieport.
        /// </summary>
        ERROR_CONTROLLING_IEPORT = 4329, 

        /// <summary>
        /// The erro r_ unabl e_ t o_ ejec t_ mounte d_ media.
        /// </summary>
        ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA = 4330, 

        /// <summary>
        /// The erro r_ cleane r_ slo t_ set.
        /// </summary>
        ERROR_CLEANER_SLOT_SET = 4331, 

        /// <summary>
        /// The erro r_ cleane r_ slo t_ no t_ set.
        /// </summary>
        ERROR_CLEANER_SLOT_NOT_SET = 4332, 

        /// <summary>
        /// The erro r_ cleane r_ cartridg e_ spent.
        /// </summary>
        ERROR_CLEANER_CARTRIDGE_SPENT = 4333, 

        /// <summary>
        /// The erro r_ unexpecte d_ omid.
        /// </summary>
        ERROR_UNEXPECTED_OMID = 4334, 

        /// <summary>
        /// The erro r_ can t_ delet e_ las t_ item.
        /// </summary>
        ERROR_CANT_DELETE_LAST_ITEM = 4335, 

        /// <summary>
        /// The erro r_ messag e_ exceed s_ ma x_ size.
        /// </summary>
        ERROR_MESSAGE_EXCEEDS_MAX_SIZE = 4336, 

        /// <summary>
        /// The erro r_ volum e_ contain s_ sy s_ files.
        /// </summary>
        ERROR_VOLUME_CONTAINS_SYS_FILES = 4337, 

        /// <summary>
        /// The erro r_ indigenou s_ type.
        /// </summary>
        ERROR_INDIGENOUS_TYPE = 4338, 

        /// <summary>
        /// The erro r_ n o_ supportin g_ drives.
        /// </summary>
        ERROR_NO_SUPPORTING_DRIVES = 4339, 

        /// <summary>
        /// The erro r_ cleane r_ cartridg e_ installed.
        /// </summary>
        ERROR_CLEANER_CARTRIDGE_INSTALLED = 4340, 

        /// <summary>
        /// The erro r_ fil e_ offline.
        /// </summary>
        ERROR_FILE_OFFLINE = 4350, 

        /// <summary>
        /// The erro r_ remot e_ storag e_ no t_ active.
        /// </summary>
        ERROR_REMOTE_STORAGE_NOT_ACTIVE = 4351, 

        /// <summary>
        /// The erro r_ remot e_ storag e_ medi a_ error.
        /// </summary>
        ERROR_REMOTE_STORAGE_MEDIA_ERROR = 4352, 

        /// <summary>
        /// The erro r_ no t_ a_ repars e_ point.
        /// </summary>
        ERROR_NOT_A_REPARSE_POINT = 4390, 

        /// <summary>
        /// The erro r_ repars e_ attribut e_ conflict.
        /// </summary>
        ERROR_REPARSE_ATTRIBUTE_CONFLICT = 4391, 

        /// <summary>
        /// The erro r_ invali d_ repars e_ data.
        /// </summary>
        ERROR_INVALID_REPARSE_DATA = 4392, 

        /// <summary>
        /// The erro r_ repars e_ ta g_ invalid.
        /// </summary>
        ERROR_REPARSE_TAG_INVALID = 4393, 

        /// <summary>
        /// The erro r_ repars e_ ta g_ mismatch.
        /// </summary>
        ERROR_REPARSE_TAG_MISMATCH = 4394, 

        /// <summary>
        /// The erro r_ volum e_ no t_ si s_ enabled.
        /// </summary>
        ERROR_VOLUME_NOT_SIS_ENABLED = 4500, 

        /// <summary>
        /// The erro r_ dependen t_ resourc e_ exists.
        /// </summary>
        ERROR_DEPENDENT_RESOURCE_EXISTS = 5001, 

        /// <summary>
        /// The erro r_ dependenc y_ no t_ found.
        /// </summary>
        ERROR_DEPENDENCY_NOT_FOUND = 5002, 

        /// <summary>
        /// The erro r_ dependenc y_ alread y_ exists.
        /// </summary>
        ERROR_DEPENDENCY_ALREADY_EXISTS = 5003, 

        /// <summary>
        /// The erro r_ resourc e_ no t_ online.
        /// </summary>
        ERROR_RESOURCE_NOT_ONLINE = 5004, 

        /// <summary>
        /// The erro r_ hos t_ nod e_ no t_ available.
        /// </summary>
        ERROR_HOST_NODE_NOT_AVAILABLE = 5005, 

        /// <summary>
        /// The erro r_ resourc e_ no t_ available.
        /// </summary>
        ERROR_RESOURCE_NOT_AVAILABLE = 5006, 

        /// <summary>
        /// The erro r_ resourc e_ no t_ found.
        /// </summary>
        ERROR_RESOURCE_NOT_FOUND = 5007, 

        /// <summary>
        /// The erro r_ shutdow n_ cluster.
        /// </summary>
        ERROR_SHUTDOWN_CLUSTER = 5008, 

        /// <summary>
        /// The erro r_ can t_ evic t_ activ e_ node.
        /// </summary>
        ERROR_CANT_EVICT_ACTIVE_NODE = 5009, 

        /// <summary>
        /// The erro r_ objec t_ alread y_ exists.
        /// </summary>
        ERROR_OBJECT_ALREADY_EXISTS = 5010, 

        /// <summary>
        /// The erro r_ objec t_ i n_ list.
        /// </summary>
        ERROR_OBJECT_IN_LIST = 5011, 

        /// <summary>
        /// The erro r_ grou p_ no t_ available.
        /// </summary>
        ERROR_GROUP_NOT_AVAILABLE = 5012, 

        /// <summary>
        /// The erro r_ grou p_ no t_ found.
        /// </summary>
        ERROR_GROUP_NOT_FOUND = 5013, 

        /// <summary>
        /// The erro r_ grou p_ no t_ online.
        /// </summary>
        ERROR_GROUP_NOT_ONLINE = 5014, 

        /// <summary>
        /// The erro r_ hos t_ nod e_ no t_ resourc e_ owner.
        /// </summary>
        ERROR_HOST_NODE_NOT_RESOURCE_OWNER = 5015, 

        /// <summary>
        /// The erro r_ hos t_ nod e_ no t_ grou p_ owner.
        /// </summary>
        ERROR_HOST_NODE_NOT_GROUP_OWNER = 5016, 

        /// <summary>
        /// The erro r_ resmo n_ creat e_ failed.
        /// </summary>
        ERROR_RESMON_CREATE_FAILED = 5017, 

        /// <summary>
        /// The erro r_ resmo n_ onlin e_ failed.
        /// </summary>
        ERROR_RESMON_ONLINE_FAILED = 5018, 

        /// <summary>
        /// The erro r_ resourc e_ online.
        /// </summary>
        ERROR_RESOURCE_ONLINE = 5019, 

        /// <summary>
        /// The erro r_ quoru m_ resource.
        /// </summary>
        ERROR_QUORUM_RESOURCE = 5020, 

        /// <summary>
        /// The erro r_ no t_ quoru m_ capable.
        /// </summary>
        ERROR_NOT_QUORUM_CAPABLE = 5021, 

        /// <summary>
        /// The erro r_ cluste r_ shuttin g_ down.
        /// </summary>
        ERROR_CLUSTER_SHUTTING_DOWN = 5022, 

        /// <summary>
        /// The erro r_ invali d_ state.
        /// </summary>
        ERROR_INVALID_STATE = 5023, 

        /// <summary>
        /// The erro r_ resourc e_ propertie s_ stored.
        /// </summary>
        ERROR_RESOURCE_PROPERTIES_STORED = 5024, 

        /// <summary>
        /// The erro r_ no t_ quoru m_ class.
        /// </summary>
        ERROR_NOT_QUORUM_CLASS = 5025, 

        /// <summary>
        /// The erro r_ cor e_ resource.
        /// </summary>
        ERROR_CORE_RESOURCE = 5026, 

        /// <summary>
        /// The erro r_ quoru m_ resourc e_ onlin e_ failed.
        /// </summary>
        ERROR_QUORUM_RESOURCE_ONLINE_FAILED = 5027, 

        /// <summary>
        /// The erro r_ quorumlo g_ ope n_ failed.
        /// </summary>
        ERROR_QUORUMLOG_OPEN_FAILED = 5028, 

        /// <summary>
        /// The erro r_ clusterlo g_ corrupt.
        /// </summary>
        ERROR_CLUSTERLOG_CORRUPT = 5029, 

        /// <summary>
        /// The erro r_ clusterlo g_ recor d_ exceed s_ maxsize.
        /// </summary>
        ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE = 5030, 

        /// <summary>
        /// The erro r_ clusterlo g_ exceed s_ maxsize.
        /// </summary>
        ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE = 5031, 

        /// <summary>
        /// The erro r_ clusterlo g_ chkpoin t_ no t_ found.
        /// </summary>
        ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND = 5032, 

        /// <summary>
        /// The erro r_ clusterlo g_ no t_ enoug h_ space.
        /// </summary>
        ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE = 5033, 

        /// <summary>
        /// The erro r_ quoru m_ owne r_ alive.
        /// </summary>
        ERROR_QUORUM_OWNER_ALIVE = 5034, 

        /// <summary>
        /// The erro r_ networ k_ no t_ available.
        /// </summary>
        ERROR_NETWORK_NOT_AVAILABLE = 5035, 

        /// <summary>
        /// The erro r_ nod e_ no t_ available.
        /// </summary>
        ERROR_NODE_NOT_AVAILABLE = 5036, 

        /// <summary>
        /// The erro r_ al l_ node s_ no t_ available.
        /// </summary>
        ERROR_ALL_NODES_NOT_AVAILABLE = 5037, 

        /// <summary>
        /// The erro r_ resourc e_ failed.
        /// </summary>
        ERROR_RESOURCE_FAILED = 5038, 

        /// <summary>
        /// The erro r_ cluste r_ invali d_ node.
        /// </summary>
        ERROR_CLUSTER_INVALID_NODE = 5039, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ exists.
        /// </summary>
        ERROR_CLUSTER_NODE_EXISTS = 5040, 

        /// <summary>
        /// The erro r_ cluste r_ joi n_ i n_ progress.
        /// </summary>
        ERROR_CLUSTER_JOIN_IN_PROGRESS = 5041, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_NODE_NOT_FOUND = 5042, 

        /// <summary>
        /// The erro r_ cluste r_ loca l_ nod e_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND = 5043, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ exists.
        /// </summary>
        ERROR_CLUSTER_NETWORK_EXISTS = 5044, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_NETWORK_NOT_FOUND = 5045, 

        /// <summary>
        /// The erro r_ cluste r_ netinterfac e_ exists.
        /// </summary>
        ERROR_CLUSTER_NETINTERFACE_EXISTS = 5046, 

        /// <summary>
        /// The erro r_ cluste r_ netinterfac e_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_NETINTERFACE_NOT_FOUND = 5047, 

        /// <summary>
        /// The erro r_ cluste r_ invali d_ request.
        /// </summary>
        ERROR_CLUSTER_INVALID_REQUEST = 5048, 

        /// <summary>
        /// The erro r_ cluste r_ invali d_ networ k_ provider.
        /// </summary>
        ERROR_CLUSTER_INVALID_NETWORK_PROVIDER = 5049, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ down.
        /// </summary>
        ERROR_CLUSTER_NODE_DOWN = 5050, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ unreachable.
        /// </summary>
        ERROR_CLUSTER_NODE_UNREACHABLE = 5051, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ no t_ member.
        /// </summary>
        ERROR_CLUSTER_NODE_NOT_MEMBER = 5052, 

        /// <summary>
        /// The erro r_ cluste r_ joi n_ no t_ i n_ progress.
        /// </summary>
        ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS = 5053, 

        /// <summary>
        /// The erro r_ cluste r_ invali d_ network.
        /// </summary>
        ERROR_CLUSTER_INVALID_NETWORK = 5054, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ up.
        /// </summary>
        ERROR_CLUSTER_NODE_UP = 5056, 

        /// <summary>
        /// The erro r_ cluste r_ ipadd r_ i n_ use.
        /// </summary>
        ERROR_CLUSTER_IPADDR_IN_USE = 5057, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ no t_ paused.
        /// </summary>
        ERROR_CLUSTER_NODE_NOT_PAUSED = 5058, 

        /// <summary>
        /// The erro r_ cluste r_ n o_ securit y_ context.
        /// </summary>
        ERROR_CLUSTER_NO_SECURITY_CONTEXT = 5059, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ no t_ internal.
        /// </summary>
        ERROR_CLUSTER_NETWORK_NOT_INTERNAL = 5060, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ alread y_ up.
        /// </summary>
        ERROR_CLUSTER_NODE_ALREADY_UP = 5061, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ alread y_ down.
        /// </summary>
        ERROR_CLUSTER_NODE_ALREADY_DOWN = 5062, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ alread y_ online.
        /// </summary>
        ERROR_CLUSTER_NETWORK_ALREADY_ONLINE = 5063, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ alread y_ offline.
        /// </summary>
        ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE = 5064, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ alread y_ member.
        /// </summary>
        ERROR_CLUSTER_NODE_ALREADY_MEMBER = 5065, 

        /// <summary>
        /// The erro r_ cluste r_ las t_ interna l_ network.
        /// </summary>
        ERROR_CLUSTER_LAST_INTERNAL_NETWORK = 5066, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ ha s_ dependents.
        /// </summary>
        ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS = 5067, 

        /// <summary>
        /// The erro r_ invali d_ operatio n_ o n_ quorum.
        /// </summary>
        ERROR_INVALID_OPERATION_ON_QUORUM = 5068, 

        /// <summary>
        /// The erro r_ dependenc y_ no t_ allowed.
        /// </summary>
        ERROR_DEPENDENCY_NOT_ALLOWED = 5069, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ paused.
        /// </summary>
        ERROR_CLUSTER_NODE_PAUSED = 5070, 

        /// <summary>
        /// The erro r_ nod e_ can t_ hos t_ resource.
        /// </summary>
        ERROR_NODE_CANT_HOST_RESOURCE = 5071, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ no t_ ready.
        /// </summary>
        ERROR_CLUSTER_NODE_NOT_READY = 5072, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ shuttin g_ down.
        /// </summary>
        ERROR_CLUSTER_NODE_SHUTTING_DOWN = 5073, 

        /// <summary>
        /// The erro r_ cluste r_ joi n_ aborted.
        /// </summary>
        ERROR_CLUSTER_JOIN_ABORTED = 5074, 

        /// <summary>
        /// The erro r_ cluste r_ incompatibl e_ versions.
        /// </summary>
        ERROR_CLUSTER_INCOMPATIBLE_VERSIONS = 5075, 

        /// <summary>
        /// The erro r_ cluste r_ maxnu m_ o f_ resource s_ exceeded.
        /// </summary>
        ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED = 5076, 

        /// <summary>
        /// The erro r_ cluste r_ syste m_ confi g_ changed.
        /// </summary>
        ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED = 5077, 

        /// <summary>
        /// The erro r_ cluste r_ resourc e_ typ e_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND = 5078, 

        /// <summary>
        /// The erro r_ cluste r_ restyp e_ no t_ supported.
        /// </summary>
        ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED = 5079, 

        /// <summary>
        /// The erro r_ cluste r_ resnam e_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_RESNAME_NOT_FOUND = 5080, 

        /// <summary>
        /// The erro r_ cluste r_ n o_ rp c_ package s_ registered.
        /// </summary>
        ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED = 5081, 

        /// <summary>
        /// The erro r_ cluste r_ owne r_ no t_ i n_ preflist.
        /// </summary>
        ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST = 5082, 

        /// <summary>
        /// The erro r_ cluste r_ databas e_ seqmismatch.
        /// </summary>
        ERROR_CLUSTER_DATABASE_SEQMISMATCH = 5083, 

        /// <summary>
        /// The erro r_ resmo n_ invali d_ state.
        /// </summary>
        ERROR_RESMON_INVALID_STATE = 5084, 

        /// <summary>
        /// The erro r_ cluste r_ gu m_ no t_ locker.
        /// </summary>
        ERROR_CLUSTER_GUM_NOT_LOCKER = 5085, 

        /// <summary>
        /// The erro r_ quoru m_ dis k_ no t_ found.
        /// </summary>
        ERROR_QUORUM_DISK_NOT_FOUND = 5086, 

        /// <summary>
        /// The erro r_ databas e_ backu p_ corrupt.
        /// </summary>
        ERROR_DATABASE_BACKUP_CORRUPT = 5087, 

        /// <summary>
        /// The erro r_ cluste r_ nod e_ alread y_ ha s_ df s_ root.
        /// </summary>
        ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT = 5088, 

        /// <summary>
        /// The erro r_ resourc e_ propert y_ unchangeable.
        /// </summary>
        ERROR_RESOURCE_PROPERTY_UNCHANGEABLE = 5089, 

        /// <summary>
        /// The erro r_ cluste r_ membershi p_ invali d_ state.
        /// </summary>
        ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE = 5890, 

        /// <summary>
        /// The erro r_ cluste r_ quorumlo g_ no t_ found.
        /// </summary>
        ERROR_CLUSTER_QUORUMLOG_NOT_FOUND = 5891, 

        /// <summary>
        /// The erro r_ cluste r_ membershi p_ halt.
        /// </summary>
        ERROR_CLUSTER_MEMBERSHIP_HALT = 5892, 

        /// <summary>
        /// The erro r_ cluste r_ instanc e_ i d_ mismatch.
        /// </summary>
        ERROR_CLUSTER_INSTANCE_ID_MISMATCH = 5893, 

        /// <summary>
        /// The erro r_ cluste r_ networ k_ no t_ foun d_ fo r_ ip.
        /// </summary>
        ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP = 5894, 

        /// <summary>
        /// The erro r_ cluste r_ propert y_ dat a_ typ e_ mismatch.
        /// </summary>
        ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH = 5895, 

        /// <summary>
        /// The erro r_ cluste r_ evic t_ withou t_ cleanup.
        /// </summary>
        ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP = 5896, 

        /// <summary>
        /// The erro r_ cluste r_ paramete r_ mismatch.
        /// </summary>
        ERROR_CLUSTER_PARAMETER_MISMATCH = 5897, 

        /// <summary>
        /// The erro r_ nod e_ canno t_ b e_ clustered.
        /// </summary>
        ERROR_NODE_CANNOT_BE_CLUSTERED = 5898, 

        /// <summary>
        /// The erro r_ cluste r_ wron g_ o s_ version.
        /// </summary>
        ERROR_CLUSTER_WRONG_OS_VERSION = 5899, 

        /// <summary>
        /// The erro r_ cluste r_ can t_ creat e_ du p_ cluste r_ name.
        /// </summary>
        ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME = 5900, 

        /// <summary>
        /// The erro r_ encryptio n_ failed.
        /// </summary>
        ERROR_ENCRYPTION_FAILED = 6000, 

        /// <summary>
        /// The erro r_ decryptio n_ failed.
        /// </summary>
        ERROR_DECRYPTION_FAILED = 6001, 

        /// <summary>
        /// The erro r_ fil e_ encrypted.
        /// </summary>
        ERROR_FILE_ENCRYPTED = 6002, 

        /// <summary>
        /// The erro r_ n o_ recover y_ policy.
        /// </summary>
        ERROR_NO_RECOVERY_POLICY = 6003, 

        /// <summary>
        /// The erro r_ n o_ efs.
        /// </summary>
        ERROR_NO_EFS = 6004, 

        /// <summary>
        /// The erro r_ wron g_ efs.
        /// </summary>
        ERROR_WRONG_EFS = 6005, 

        /// <summary>
        /// The erro r_ n o_ use r_ keys.
        /// </summary>
        ERROR_NO_USER_KEYS = 6006, 

        /// <summary>
        /// The erro r_ fil e_ no t_ encrypted.
        /// </summary>
        ERROR_FILE_NOT_ENCRYPTED = 6007, 

        /// <summary>
        /// The erro r_ no t_ expor t_ format.
        /// </summary>
        ERROR_NOT_EXPORT_FORMAT = 6008, 

        /// <summary>
        /// The erro r_ fil e_ rea d_ only.
        /// </summary>
        ERROR_FILE_READ_ONLY = 6009, 

        /// <summary>
        /// The erro r_ di r_ ef s_ disallowed.
        /// </summary>
        ERROR_DIR_EFS_DISALLOWED = 6010, 

        /// <summary>
        /// The erro r_ ef s_ serve r_ no t_ trusted.
        /// </summary>
        ERROR_EFS_SERVER_NOT_TRUSTED = 6011, 

        /// <summary>
        /// The erro r_ ba d_ recover y_ policy.
        /// </summary>
        ERROR_BAD_RECOVERY_POLICY = 6012, 

        /// <summary>
        /// The erro r_ ef s_ al g_ blo b_ to o_ big.
        /// </summary>
        ERROR_EFS_ALG_BLOB_TOO_BIG = 6013, 

        /// <summary>
        /// The erro r_ volum e_ no t_ suppor t_ efs.
        /// </summary>
        ERROR_VOLUME_NOT_SUPPORT_EFS = 6014, 

        /// <summary>
        /// The erro r_ ef s_ disabled.
        /// </summary>
        ERROR_EFS_DISABLED = 6015, 

        /// <summary>
        /// The erro r_ ef s_ versio n_ no t_ support.
        /// </summary>
        ERROR_EFS_VERSION_NOT_SUPPORT = 6016, 

        /// <summary>
        /// The erro r_ n o_ browse r_ server s_ found.
        /// </summary>
        ERROR_NO_BROWSER_SERVERS_FOUND = 6118, 

        /// <summary>
        /// The sche d_ e_ servic e_ no t_ localsystem.
        /// </summary>
        SCHED_E_SERVICE_NOT_LOCALSYSTEM = 6200, 

        /// <summary>
        /// The erro r_ ct x_ winstatio n_ nam e_ invalid.
        /// </summary>
        ERROR_CTX_WINSTATION_NAME_INVALID = 7001, 

        /// <summary>
        /// The erro r_ ct x_ invali d_ pd.
        /// </summary>
        ERROR_CTX_INVALID_PD = 7002, 

        /// <summary>
        /// The erro r_ ct x_ p d_ no t_ found.
        /// </summary>
        ERROR_CTX_PD_NOT_FOUND = 7003, 

        /// <summary>
        /// The erro r_ ct x_ w d_ no t_ found.
        /// </summary>
        ERROR_CTX_WD_NOT_FOUND = 7004, 

        /// <summary>
        /// The erro r_ ct x_ canno t_ mak e_ eventlo g_ entry.
        /// </summary>
        ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY = 7005, 

        /// <summary>
        /// The erro r_ ct x_ servic e_ nam e_ collision.
        /// </summary>
        ERROR_CTX_SERVICE_NAME_COLLISION = 7006, 

        /// <summary>
        /// The erro r_ ct x_ clos e_ pending.
        /// </summary>
        ERROR_CTX_CLOSE_PENDING = 7007, 

        /// <summary>
        /// The erro r_ ct x_ n o_ outbuf.
        /// </summary>
        ERROR_CTX_NO_OUTBUF = 7008, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ in f_ no t_ found.
        /// </summary>
        ERROR_CTX_MODEM_INF_NOT_FOUND = 7009, 

        /// <summary>
        /// The erro r_ ct x_ invali d_ modemname.
        /// </summary>
        ERROR_CTX_INVALID_MODEMNAME = 7010, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ error.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_ERROR = 7011, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ timeout.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_TIMEOUT = 7012, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ n o_ carrier.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_NO_CARRIER = 7013, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ n o_ dialtone.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE = 7014, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ busy.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_BUSY = 7015, 

        /// <summary>
        /// The erro r_ ct x_ mode m_ respons e_ voice.
        /// </summary>
        ERROR_CTX_MODEM_RESPONSE_VOICE = 7016, 

        /// <summary>
        /// The erro r_ ct x_ t d_ error.
        /// </summary>
        ERROR_CTX_TD_ERROR = 7017, 

        /// <summary>
        /// The erro r_ ct x_ winstatio n_ no t_ found.
        /// </summary>
        ERROR_CTX_WINSTATION_NOT_FOUND = 7022, 

        /// <summary>
        /// The erro r_ ct x_ winstatio n_ alread y_ exists.
        /// </summary>
        ERROR_CTX_WINSTATION_ALREADY_EXISTS = 7023, 

        /// <summary>
        /// The erro r_ ct x_ winstatio n_ busy.
        /// </summary>
        ERROR_CTX_WINSTATION_BUSY = 7024, 

        /// <summary>
        /// The erro r_ ct x_ ba d_ vide o_ mode.
        /// </summary>
        ERROR_CTX_BAD_VIDEO_MODE = 7025, 

        /// <summary>
        /// The erro r_ ct x_ graphic s_ invalid.
        /// </summary>
        ERROR_CTX_GRAPHICS_INVALID = 7035, 

        /// <summary>
        /// The erro r_ ct x_ logo n_ disabled.
        /// </summary>
        ERROR_CTX_LOGON_DISABLED = 7037, 

        /// <summary>
        /// The erro r_ ct x_ no t_ console.
        /// </summary>
        ERROR_CTX_NOT_CONSOLE = 7038, 

        /// <summary>
        /// The erro r_ ct x_ clien t_ quer y_ timeout.
        /// </summary>
        ERROR_CTX_CLIENT_QUERY_TIMEOUT = 7040, 

        /// <summary>
        /// The erro r_ ct x_ consol e_ disconnect.
        /// </summary>
        ERROR_CTX_CONSOLE_DISCONNECT = 7041, 

        /// <summary>
        /// The erro r_ ct x_ consol e_ connect.
        /// </summary>
        ERROR_CTX_CONSOLE_CONNECT = 7042, 

        /// <summary>
        /// The erro r_ ct x_ shado w_ denied.
        /// </summary>
        ERROR_CTX_SHADOW_DENIED = 7044, 

        /// <summary>
        /// The erro r_ ct x_ winstatio n_ acces s_ denied.
        /// </summary>
        ERROR_CTX_WINSTATION_ACCESS_DENIED = 7045, 

        /// <summary>
        /// The erro r_ ct x_ invali d_ wd.
        /// </summary>
        ERROR_CTX_INVALID_WD = 7049, 

        /// <summary>
        /// The erro r_ ct x_ shado w_ invalid.
        /// </summary>
        ERROR_CTX_SHADOW_INVALID = 7050, 

        /// <summary>
        /// The erro r_ ct x_ shado w_ disabled.
        /// </summary>
        ERROR_CTX_SHADOW_DISABLED = 7051, 

        /// <summary>
        /// The erro r_ ct x_ clien t_ licens e_ i n_ use.
        /// </summary>
        ERROR_CTX_CLIENT_LICENSE_IN_USE = 7052, 

        /// <summary>
        /// The erro r_ ct x_ clien t_ licens e_ no t_ set.
        /// </summary>
        ERROR_CTX_CLIENT_LICENSE_NOT_SET = 7053, 

        /// <summary>
        /// The erro r_ ct x_ licens e_ no t_ available.
        /// </summary>
        ERROR_CTX_LICENSE_NOT_AVAILABLE = 7054, 

        /// <summary>
        /// The erro r_ ct x_ licens e_ clien t_ invalid.
        /// </summary>
        ERROR_CTX_LICENSE_CLIENT_INVALID = 7055, 

        /// <summary>
        /// The erro r_ ct x_ licens e_ expired.
        /// </summary>
        ERROR_CTX_LICENSE_EXPIRED = 7056, 

        /// <summary>
        /// The erro r_ ct x_ shado w_ no t_ running.
        /// </summary>
        ERROR_CTX_SHADOW_NOT_RUNNING = 7057, 

        /// <summary>
        /// The erro r_ ct x_ shado w_ ende d_ b y_ mod e_ change.
        /// </summary>
        ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE = 7058, 

        /// <summary>
        /// The fr s_ er r_ invali d_ ap i_ sequence.
        /// </summary>
        FRS_ERR_INVALID_API_SEQUENCE = 8001, 

        /// <summary>
        /// The fr s_ er r_ startin g_ service.
        /// </summary>
        FRS_ERR_STARTING_SERVICE = 8002, 

        /// <summary>
        /// The fr s_ er r_ stoppin g_ service.
        /// </summary>
        FRS_ERR_STOPPING_SERVICE = 8003, 

        /// <summary>
        /// The fr s_ er r_ interna l_ api.
        /// </summary>
        FRS_ERR_INTERNAL_API = 8004, 

        /// <summary>
        /// The fr s_ er r_ internal.
        /// </summary>
        FRS_ERR_INTERNAL = 8005, 

        /// <summary>
        /// The fr s_ er r_ servic e_ comm.
        /// </summary>
        FRS_ERR_SERVICE_COMM = 8006, 

        /// <summary>
        /// The fr s_ er r_ insufficien t_ priv.
        /// </summary>
        FRS_ERR_INSUFFICIENT_PRIV = 8007, 

        /// <summary>
        /// The fr s_ er r_ authentication.
        /// </summary>
        FRS_ERR_AUTHENTICATION = 8008, 

        /// <summary>
        /// The fr s_ er r_ paren t_ insufficien t_ priv.
        /// </summary>
        FRS_ERR_PARENT_INSUFFICIENT_PRIV = 8009, 

        /// <summary>
        /// The fr s_ er r_ paren t_ authentication.
        /// </summary>
        FRS_ERR_PARENT_AUTHENTICATION = 8010, 

        /// <summary>
        /// The fr s_ er r_ chil d_ t o_ paren t_ comm.
        /// </summary>
        FRS_ERR_CHILD_TO_PARENT_COMM = 8011, 

        /// <summary>
        /// The fr s_ er r_ paren t_ t o_ chil d_ comm.
        /// </summary>
        FRS_ERR_PARENT_TO_CHILD_COMM = 8012, 

        /// <summary>
        /// The fr s_ er r_ sysvo l_ populate.
        /// </summary>
        FRS_ERR_SYSVOL_POPULATE = 8013, 

        /// <summary>
        /// The fr s_ er r_ sysvo l_ populat e_ timeout.
        /// </summary>
        FRS_ERR_SYSVOL_POPULATE_TIMEOUT = 8014, 

        /// <summary>
        /// The fr s_ er r_ sysvo l_ i s_ busy.
        /// </summary>
        FRS_ERR_SYSVOL_IS_BUSY = 8015, 

        /// <summary>
        /// The fr s_ er r_ sysvo l_ demote.
        /// </summary>
        FRS_ERR_SYSVOL_DEMOTE = 8016, 

        /// <summary>
        /// The fr s_ er r_ invali d_ servic e_ parameter.
        /// </summary>
        FRS_ERR_INVALID_SERVICE_PARAMETER = 8017, 

        /// <summary>
        /// The erro r_ d s_ no t_ installed.
        /// </summary>
        ERROR_DS_NOT_INSTALLED = 8200, 

        /// <summary>
        /// The erro r_ d s_ membershi p_ evaluate d_ locally.
        /// </summary>
        ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY = 8201, 

        /// <summary>
        /// The erro r_ d s_ n o_ attribut e_ o r_ value.
        /// </summary>
        ERROR_DS_NO_ATTRIBUTE_OR_VALUE = 8202, 

        /// <summary>
        /// The erro r_ d s_ invali d_ attribut e_ syntax.
        /// </summary>
        ERROR_DS_INVALID_ATTRIBUTE_SYNTAX = 8203, 

        /// <summary>
        /// The erro r_ d s_ attribut e_ typ e_ undefined.
        /// </summary>
        ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED = 8204, 

        /// <summary>
        /// The erro r_ d s_ attribut e_ o r_ valu e_ exists.
        /// </summary>
        ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS = 8205, 

        /// <summary>
        /// The erro r_ d s_ busy.
        /// </summary>
        ERROR_DS_BUSY = 8206, 

        /// <summary>
        /// The erro r_ d s_ unavailable.
        /// </summary>
        ERROR_DS_UNAVAILABLE = 8207, 

        /// <summary>
        /// The erro r_ d s_ n o_ rid s_ allocated.
        /// </summary>
        ERROR_DS_NO_RIDS_ALLOCATED = 8208, 

        /// <summary>
        /// The erro r_ d s_ n o_ mor e_ rids.
        /// </summary>
        ERROR_DS_NO_MORE_RIDS = 8209, 

        /// <summary>
        /// The erro r_ d s_ incorrec t_ rol e_ owner.
        /// </summary>
        ERROR_DS_INCORRECT_ROLE_OWNER = 8210, 

        /// <summary>
        /// The erro r_ d s_ ridmg r_ ini t_ error.
        /// </summary>
        ERROR_DS_RIDMGR_INIT_ERROR = 8211, 

        /// <summary>
        /// The erro r_ d s_ ob j_ clas s_ violation.
        /// </summary>
        ERROR_DS_OBJ_CLASS_VIOLATION = 8212, 

        /// <summary>
        /// The erro r_ d s_ can t_ o n_ no n_ leaf.
        /// </summary>
        ERROR_DS_CANT_ON_NON_LEAF = 8213, 

        /// <summary>
        /// The erro r_ d s_ can t_ o n_ rdn.
        /// </summary>
        ERROR_DS_CANT_ON_RDN = 8214, 

        /// <summary>
        /// The erro r_ d s_ can t_ mo d_ ob j_ class.
        /// </summary>
        ERROR_DS_CANT_MOD_OBJ_CLASS = 8215, 

        /// <summary>
        /// The erro r_ d s_ cros s_ do m_ mov e_ error.
        /// </summary>
        ERROR_DS_CROSS_DOM_MOVE_ERROR = 8216, 

        /// <summary>
        /// The erro r_ d s_ g c_ no t_ available.
        /// </summary>
        ERROR_DS_GC_NOT_AVAILABLE = 8217, 

        /// <summary>
        /// The erro r_ share d_ policy.
        /// </summary>
        ERROR_SHARED_POLICY = 8218, 

        /// <summary>
        /// The erro r_ polic y_ objec t_ no t_ found.
        /// </summary>
        ERROR_POLICY_OBJECT_NOT_FOUND = 8219, 

        /// <summary>
        /// The erro r_ polic y_ onl y_ i n_ ds.
        /// </summary>
        ERROR_POLICY_ONLY_IN_DS = 8220, 

        /// <summary>
        /// The erro r_ promotio n_ active.
        /// </summary>
        ERROR_PROMOTION_ACTIVE = 8221, 

        /// <summary>
        /// The erro r_ n o_ promotio n_ active.
        /// </summary>
        ERROR_NO_PROMOTION_ACTIVE = 8222, 

        /// <summary>
        /// The erro r_ d s_ operation s_ error.
        /// </summary>
        ERROR_DS_OPERATIONS_ERROR = 8224, 

        /// <summary>
        /// The erro r_ d s_ protoco l_ error.
        /// </summary>
        ERROR_DS_PROTOCOL_ERROR = 8225, 

        /// <summary>
        /// The erro r_ d s_ timelimi t_ exceeded.
        /// </summary>
        ERROR_DS_TIMELIMIT_EXCEEDED = 8226, 

        /// <summary>
        /// The erro r_ d s_ sizelimi t_ exceeded.
        /// </summary>
        ERROR_DS_SIZELIMIT_EXCEEDED = 8227, 

        /// <summary>
        /// The erro r_ d s_ admi n_ limi t_ exceeded.
        /// </summary>
        ERROR_DS_ADMIN_LIMIT_EXCEEDED = 8228, 

        /// <summary>
        /// The erro r_ d s_ compar e_ false.
        /// </summary>
        ERROR_DS_COMPARE_FALSE = 8229, 

        /// <summary>
        /// The erro r_ d s_ compar e_ true.
        /// </summary>
        ERROR_DS_COMPARE_TRUE = 8230, 

        /// <summary>
        /// The erro r_ d s_ aut h_ metho d_ no t_ supported.
        /// </summary>
        ERROR_DS_AUTH_METHOD_NOT_SUPPORTED = 8231, 

        /// <summary>
        /// The erro r_ d s_ stron g_ aut h_ required.
        /// </summary>
        ERROR_DS_STRONG_AUTH_REQUIRED = 8232, 

        /// <summary>
        /// The erro r_ d s_ inappropriat e_ auth.
        /// </summary>
        ERROR_DS_INAPPROPRIATE_AUTH = 8233, 

        /// <summary>
        /// The erro r_ d s_ aut h_ unknown.
        /// </summary>
        ERROR_DS_AUTH_UNKNOWN = 8234, 

        /// <summary>
        /// The erro r_ d s_ referral.
        /// </summary>
        ERROR_DS_REFERRAL = 8235, 

        /// <summary>
        /// The erro r_ d s_ unavailabl e_ cri t_ extension.
        /// </summary>
        ERROR_DS_UNAVAILABLE_CRIT_EXTENSION = 8236, 

        /// <summary>
        /// The erro r_ d s_ confidentialit y_ required.
        /// </summary>
        ERROR_DS_CONFIDENTIALITY_REQUIRED = 8237, 

        /// <summary>
        /// The erro r_ d s_ inappropriat e_ matching.
        /// </summary>
        ERROR_DS_INAPPROPRIATE_MATCHING = 8238, 

        /// <summary>
        /// The erro r_ d s_ constrain t_ violation.
        /// </summary>
        ERROR_DS_CONSTRAINT_VIOLATION = 8239, 

        /// <summary>
        /// The erro r_ d s_ n o_ suc h_ object.
        /// </summary>
        ERROR_DS_NO_SUCH_OBJECT = 8240, 

        /// <summary>
        /// The erro r_ d s_ alia s_ problem.
        /// </summary>
        ERROR_DS_ALIAS_PROBLEM = 8241, 

        /// <summary>
        /// The erro r_ d s_ invali d_ d n_ syntax.
        /// </summary>
        ERROR_DS_INVALID_DN_SYNTAX = 8242, 

        /// <summary>
        /// The erro r_ d s_ i s_ leaf.
        /// </summary>
        ERROR_DS_IS_LEAF = 8243, 

        /// <summary>
        /// The erro r_ d s_ alia s_ dere f_ problem.
        /// </summary>
        ERROR_DS_ALIAS_DEREF_PROBLEM = 8244, 

        /// <summary>
        /// The erro r_ d s_ unwillin g_ t o_ perform.
        /// </summary>
        ERROR_DS_UNWILLING_TO_PERFORM = 8245, 

        /// <summary>
        /// The erro r_ d s_ loo p_ detect.
        /// </summary>
        ERROR_DS_LOOP_DETECT = 8246, 

        /// <summary>
        /// The erro r_ d s_ namin g_ violation.
        /// </summary>
        ERROR_DS_NAMING_VIOLATION = 8247, 

        /// <summary>
        /// The erro r_ d s_ objec t_ result s_ to o_ large.
        /// </summary>
        ERROR_DS_OBJECT_RESULTS_TOO_LARGE = 8248, 

        /// <summary>
        /// The erro r_ d s_ affect s_ multipl e_ dsas.
        /// </summary>
        ERROR_DS_AFFECTS_MULTIPLE_DSAS = 8249, 

        /// <summary>
        /// The erro r_ d s_ serve r_ down.
        /// </summary>
        ERROR_DS_SERVER_DOWN = 8250, 

        /// <summary>
        /// The erro r_ d s_ loca l_ error.
        /// </summary>
        ERROR_DS_LOCAL_ERROR = 8251, 

        /// <summary>
        /// The erro r_ d s_ encodin g_ error.
        /// </summary>
        ERROR_DS_ENCODING_ERROR = 8252, 

        /// <summary>
        /// The erro r_ d s_ decodin g_ error.
        /// </summary>
        ERROR_DS_DECODING_ERROR = 8253, 

        /// <summary>
        /// The erro r_ d s_ filte r_ unknown.
        /// </summary>
        ERROR_DS_FILTER_UNKNOWN = 8254, 

        /// <summary>
        /// The erro r_ d s_ para m_ error.
        /// </summary>
        ERROR_DS_PARAM_ERROR = 8255, 

        /// <summary>
        /// The erro r_ d s_ no t_ supported.
        /// </summary>
        ERROR_DS_NOT_SUPPORTED = 8256, 

        /// <summary>
        /// The erro r_ d s_ n o_ result s_ returned.
        /// </summary>
        ERROR_DS_NO_RESULTS_RETURNED = 8257, 

        /// <summary>
        /// The erro r_ d s_ contro l_ no t_ found.
        /// </summary>
        ERROR_DS_CONTROL_NOT_FOUND = 8258, 

        /// <summary>
        /// The erro r_ d s_ clien t_ loop.
        /// </summary>
        ERROR_DS_CLIENT_LOOP = 8259, 

        /// <summary>
        /// The erro r_ d s_ referra l_ limi t_ exceeded.
        /// </summary>
        ERROR_DS_REFERRAL_LIMIT_EXCEEDED = 8260, 

        /// <summary>
        /// The erro r_ d s_ sor t_ contro l_ missing.
        /// </summary>
        ERROR_DS_SORT_CONTROL_MISSING = 8261, 

        /// <summary>
        /// The erro r_ d s_ offse t_ rang e_ error.
        /// </summary>
        ERROR_DS_OFFSET_RANGE_ERROR = 8262, 

        /// <summary>
        /// The erro r_ d s_ roo t_ mus t_ b e_ nc.
        /// </summary>
        ERROR_DS_ROOT_MUST_BE_NC = 8301, 

        /// <summary>
        /// The erro r_ d s_ ad d_ replic a_ inhibited.
        /// </summary>
        ERROR_DS_ADD_REPLICA_INHIBITED = 8302, 

        /// <summary>
        /// The erro r_ d s_ at t_ no t_ de f_ i n_ schema.
        /// </summary>
        ERROR_DS_ATT_NOT_DEF_IN_SCHEMA = 8303, 

        /// <summary>
        /// The erro r_ d s_ ma x_ ob j_ siz e_ exceeded.
        /// </summary>
        ERROR_DS_MAX_OBJ_SIZE_EXCEEDED = 8304, 

        /// <summary>
        /// The erro r_ d s_ ob j_ strin g_ nam e_ exists.
        /// </summary>
        ERROR_DS_OBJ_STRING_NAME_EXISTS = 8305, 

        /// <summary>
        /// The erro r_ d s_ n o_ rd n_ define d_ i n_ schema.
        /// </summary>
        ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA = 8306, 

        /// <summary>
        /// The erro r_ d s_ rd n_ doesn t_ matc h_ schema.
        /// </summary>
        ERROR_DS_RDN_DOESNT_MATCH_SCHEMA = 8307, 

        /// <summary>
        /// The erro r_ d s_ n o_ requeste d_ att s_ found.
        /// </summary>
        ERROR_DS_NO_REQUESTED_ATTS_FOUND = 8308, 

        /// <summary>
        /// The erro r_ d s_ use r_ buffe r_ t o_ small.
        /// </summary>
        ERROR_DS_USER_BUFFER_TO_SMALL = 8309, 

        /// <summary>
        /// The erro r_ d s_ at t_ i s_ no t_ o n_ obj.
        /// </summary>
        ERROR_DS_ATT_IS_NOT_ON_OBJ = 8310, 

        /// <summary>
        /// The erro r_ d s_ illega l_ mo d_ operation.
        /// </summary>
        ERROR_DS_ILLEGAL_MOD_OPERATION = 8311, 

        /// <summary>
        /// The erro r_ d s_ ob j_ to o_ large.
        /// </summary>
        ERROR_DS_OBJ_TOO_LARGE = 8312, 

        /// <summary>
        /// The erro r_ d s_ ba d_ instanc e_ type.
        /// </summary>
        ERROR_DS_BAD_INSTANCE_TYPE = 8313, 

        /// <summary>
        /// The erro r_ d s_ masterds a_ required.
        /// </summary>
        ERROR_DS_MASTERDSA_REQUIRED = 8314, 

        /// <summary>
        /// The erro r_ d s_ objec t_ clas s_ required.
        /// </summary>
        ERROR_DS_OBJECT_CLASS_REQUIRED = 8315, 

        /// <summary>
        /// The erro r_ d s_ missin g_ require d_ att.
        /// </summary>
        ERROR_DS_MISSING_REQUIRED_ATT = 8316, 

        /// <summary>
        /// The erro r_ d s_ at t_ no t_ de f_ fo r_ class.
        /// </summary>
        ERROR_DS_ATT_NOT_DEF_FOR_CLASS = 8317, 

        /// <summary>
        /// The erro r_ d s_ at t_ alread y_ exists.
        /// </summary>
        ERROR_DS_ATT_ALREADY_EXISTS = 8318, 

        /// <summary>
        /// The erro r_ d s_ can t_ ad d_ at t_ values.
        /// </summary>
        ERROR_DS_CANT_ADD_ATT_VALUES = 8320, 

        /// <summary>
        /// The erro r_ d s_ singl e_ valu e_ constraint.
        /// </summary>
        ERROR_DS_SINGLE_VALUE_CONSTRAINT = 8321, 

        /// <summary>
        /// The erro r_ d s_ rang e_ constraint.
        /// </summary>
        ERROR_DS_RANGE_CONSTRAINT = 8322, 

        /// <summary>
        /// The erro r_ d s_ at t_ va l_ alread y_ exists.
        /// </summary>
        ERROR_DS_ATT_VAL_ALREADY_EXISTS = 8323, 

        /// <summary>
        /// The erro r_ d s_ can t_ re m_ missin g_ att.
        /// </summary>
        ERROR_DS_CANT_REM_MISSING_ATT = 8324, 

        /// <summary>
        /// The erro r_ d s_ can t_ re m_ missin g_ at t_ val.
        /// </summary>
        ERROR_DS_CANT_REM_MISSING_ATT_VAL = 8325, 

        /// <summary>
        /// The erro r_ d s_ roo t_ can t_ b e_ subref.
        /// </summary>
        ERROR_DS_ROOT_CANT_BE_SUBREF = 8326, 

        /// <summary>
        /// The erro r_ d s_ n o_ chaining.
        /// </summary>
        ERROR_DS_NO_CHAINING = 8327, 

        /// <summary>
        /// The erro r_ d s_ n o_ chaine d_ eval.
        /// </summary>
        ERROR_DS_NO_CHAINED_EVAL = 8328, 

        /// <summary>
        /// The erro r_ d s_ n o_ paren t_ object.
        /// </summary>
        ERROR_DS_NO_PARENT_OBJECT = 8329, 

        /// <summary>
        /// The erro r_ d s_ paren t_ i s_ a n_ alias.
        /// </summary>
        ERROR_DS_PARENT_IS_AN_ALIAS = 8330, 

        /// <summary>
        /// The erro r_ d s_ can t_ mi x_ maste r_ an d_ reps.
        /// </summary>
        ERROR_DS_CANT_MIX_MASTER_AND_REPS = 8331, 

        /// <summary>
        /// The erro r_ d s_ childre n_ exist.
        /// </summary>
        ERROR_DS_CHILDREN_EXIST = 8332, 

        /// <summary>
        /// The erro r_ d s_ ob j_ no t_ found.
        /// </summary>
        ERROR_DS_OBJ_NOT_FOUND = 8333, 

        /// <summary>
        /// The erro r_ d s_ aliase d_ ob j_ missing.
        /// </summary>
        ERROR_DS_ALIASED_OBJ_MISSING = 8334, 

        /// <summary>
        /// The erro r_ d s_ ba d_ nam e_ syntax.
        /// </summary>
        ERROR_DS_BAD_NAME_SYNTAX = 8335, 

        /// <summary>
        /// The erro r_ d s_ alia s_ point s_ t o_ alias.
        /// </summary>
        ERROR_DS_ALIAS_POINTS_TO_ALIAS = 8336, 

        /// <summary>
        /// The erro r_ d s_ can t_ dere f_ alias.
        /// </summary>
        ERROR_DS_CANT_DEREF_ALIAS = 8337, 

        /// <summary>
        /// The erro r_ d s_ ou t_ o f_ scope.
        /// </summary>
        ERROR_DS_OUT_OF_SCOPE = 8338, 

        /// <summary>
        /// The erro r_ d s_ objec t_ bein g_ removed.
        /// </summary>
        ERROR_DS_OBJECT_BEING_REMOVED = 8339, 

        /// <summary>
        /// The erro r_ d s_ can t_ delet e_ ds a_ obj.
        /// </summary>
        ERROR_DS_CANT_DELETE_DSA_OBJ = 8340, 

        /// <summary>
        /// The erro r_ d s_ generi c_ error.
        /// </summary>
        ERROR_DS_GENERIC_ERROR = 8341, 

        /// <summary>
        /// The erro r_ d s_ ds a_ mus t_ b e_ in t_ master.
        /// </summary>
        ERROR_DS_DSA_MUST_BE_INT_MASTER = 8342, 

        /// <summary>
        /// The erro r_ d s_ clas s_ no t_ dsa.
        /// </summary>
        ERROR_DS_CLASS_NOT_DSA = 8343, 

        /// <summary>
        /// The erro r_ d s_ insuf f_ acces s_ rights.
        /// </summary>
        ERROR_DS_INSUFF_ACCESS_RIGHTS = 8344, 

        /// <summary>
        /// The erro r_ d s_ illega l_ superior.
        /// </summary>
        ERROR_DS_ILLEGAL_SUPERIOR = 8345, 

        /// <summary>
        /// The erro r_ d s_ attribut e_ owne d_ b y_ sam.
        /// </summary>
        ERROR_DS_ATTRIBUTE_OWNED_BY_SAM = 8346, 

        /// <summary>
        /// The erro r_ d s_ nam e_ to o_ man y_ parts.
        /// </summary>
        ERROR_DS_NAME_TOO_MANY_PARTS = 8347, 

        /// <summary>
        /// The erro r_ d s_ nam e_ to o_ long.
        /// </summary>
        ERROR_DS_NAME_TOO_LONG = 8348, 

        /// <summary>
        /// The erro r_ d s_ nam e_ valu e_ to o_ long.
        /// </summary>
        ERROR_DS_NAME_VALUE_TOO_LONG = 8349, 

        /// <summary>
        /// The erro r_ d s_ nam e_ unparseable.
        /// </summary>
        ERROR_DS_NAME_UNPARSEABLE = 8350, 

        /// <summary>
        /// The erro r_ d s_ nam e_ typ e_ unknown.
        /// </summary>
        ERROR_DS_NAME_TYPE_UNKNOWN = 8351, 

        /// <summary>
        /// The erro r_ d s_ no t_ a n_ object.
        /// </summary>
        ERROR_DS_NOT_AN_OBJECT = 8352, 

        /// <summary>
        /// The erro r_ d s_ se c_ des c_ to o_ short.
        /// </summary>
        ERROR_DS_SEC_DESC_TOO_SHORT = 8353, 

        /// <summary>
        /// The erro r_ d s_ se c_ des c_ invalid.
        /// </summary>
        ERROR_DS_SEC_DESC_INVALID = 8354, 

        /// <summary>
        /// The erro r_ d s_ n o_ delete d_ name.
        /// </summary>
        ERROR_DS_NO_DELETED_NAME = 8355, 

        /// <summary>
        /// The erro r_ d s_ subre f_ mus t_ hav e_ parent.
        /// </summary>
        ERROR_DS_SUBREF_MUST_HAVE_PARENT = 8356, 

        /// <summary>
        /// The erro r_ d s_ ncnam e_ mus t_ b e_ nc.
        /// </summary>
        ERROR_DS_NCNAME_MUST_BE_NC = 8357, 

        /// <summary>
        /// The erro r_ d s_ can t_ ad d_ syste m_ only.
        /// </summary>
        ERROR_DS_CANT_ADD_SYSTEM_ONLY = 8358, 

        /// <summary>
        /// The erro r_ d s_ clas s_ mus t_ b e_ concrete.
        /// </summary>
        ERROR_DS_CLASS_MUST_BE_CONCRETE = 8359, 

        /// <summary>
        /// The erro r_ d s_ invali d_ dmd.
        /// </summary>
        ERROR_DS_INVALID_DMD = 8360, 

        /// <summary>
        /// The erro r_ d s_ ob j_ gui d_ exists.
        /// </summary>
        ERROR_DS_OBJ_GUID_EXISTS = 8361, 

        /// <summary>
        /// The erro r_ d s_ no t_ o n_ backlink.
        /// </summary>
        ERROR_DS_NOT_ON_BACKLINK = 8362, 

        /// <summary>
        /// The erro r_ d s_ n o_ crossre f_ fo r_ nc.
        /// </summary>
        ERROR_DS_NO_CROSSREF_FOR_NC = 8363, 

        /// <summary>
        /// The erro r_ d s_ shuttin g_ down.
        /// </summary>
        ERROR_DS_SHUTTING_DOWN = 8364, 

        /// <summary>
        /// The erro r_ d s_ unknow n_ operation.
        /// </summary>
        ERROR_DS_UNKNOWN_OPERATION = 8365, 

        /// <summary>
        /// The erro r_ d s_ invali d_ rol e_ owner.
        /// </summary>
        ERROR_DS_INVALID_ROLE_OWNER = 8366, 

        /// <summary>
        /// The erro r_ d s_ couldn t_ contac t_ fsmo.
        /// </summary>
        ERROR_DS_COULDNT_CONTACT_FSMO = 8367, 

        /// <summary>
        /// The erro r_ d s_ cros s_ n c_ d n_ rename.
        /// </summary>
        ERROR_DS_CROSS_NC_DN_RENAME = 8368, 

        /// <summary>
        /// The erro r_ d s_ can t_ mo d_ syste m_ only.
        /// </summary>
        ERROR_DS_CANT_MOD_SYSTEM_ONLY = 8369, 

        /// <summary>
        /// The erro r_ d s_ replicato r_ only.
        /// </summary>
        ERROR_DS_REPLICATOR_ONLY = 8370, 

        /// <summary>
        /// The erro r_ d s_ ob j_ clas s_ no t_ defined.
        /// </summary>
        ERROR_DS_OBJ_CLASS_NOT_DEFINED = 8371, 

        /// <summary>
        /// The erro r_ d s_ ob j_ clas s_ no t_ subclass.
        /// </summary>
        ERROR_DS_OBJ_CLASS_NOT_SUBCLASS = 8372, 

        /// <summary>
        /// The erro r_ d s_ nam e_ referenc e_ invalid.
        /// </summary>
        ERROR_DS_NAME_REFERENCE_INVALID = 8373, 

        /// <summary>
        /// The erro r_ d s_ cros s_ re f_ exists.
        /// </summary>
        ERROR_DS_CROSS_REF_EXISTS = 8374, 

        /// <summary>
        /// The erro r_ d s_ can t_ de l_ maste r_ crossref.
        /// </summary>
        ERROR_DS_CANT_DEL_MASTER_CROSSREF = 8375, 

        /// <summary>
        /// The erro r_ d s_ subtre e_ notif y_ no t_ n c_ head.
        /// </summary>
        ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD = 8376, 

        /// <summary>
        /// The erro r_ d s_ notif y_ filte r_ to o_ complex.
        /// </summary>
        ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX = 8377, 

        /// <summary>
        /// The erro r_ d s_ du p_ rdn.
        /// </summary>
        ERROR_DS_DUP_RDN = 8378, 

        /// <summary>
        /// The erro r_ d s_ du p_ oid.
        /// </summary>
        ERROR_DS_DUP_OID = 8379, 

        /// <summary>
        /// The erro r_ d s_ du p_ map i_ id.
        /// </summary>
        ERROR_DS_DUP_MAPI_ID = 8380, 

        /// <summary>
        /// The erro r_ d s_ du p_ schem a_ i d_ guid.
        /// </summary>
        ERROR_DS_DUP_SCHEMA_ID_GUID = 8381, 

        /// <summary>
        /// The erro r_ d s_ du p_ lda p_ displa y_ name.
        /// </summary>
        ERROR_DS_DUP_LDAP_DISPLAY_NAME = 8382, 

        /// <summary>
        /// The erro r_ d s_ semanti c_ at t_ test.
        /// </summary>
        ERROR_DS_SEMANTIC_ATT_TEST = 8383, 

        /// <summary>
        /// The erro r_ d s_ synta x_ mismatch.
        /// </summary>
        ERROR_DS_SYNTAX_MISMATCH = 8384, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ mus t_ have.
        /// </summary>
        ERROR_DS_EXISTS_IN_MUST_HAVE = 8385, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ ma y_ have.
        /// </summary>
        ERROR_DS_EXISTS_IN_MAY_HAVE = 8386, 

        /// <summary>
        /// The erro r_ d s_ nonexisten t_ ma y_ have.
        /// </summary>
        ERROR_DS_NONEXISTENT_MAY_HAVE = 8387, 

        /// <summary>
        /// The erro r_ d s_ nonexisten t_ mus t_ have.
        /// </summary>
        ERROR_DS_NONEXISTENT_MUST_HAVE = 8388, 

        /// <summary>
        /// The erro r_ d s_ au x_ cl s_ tes t_ fail.
        /// </summary>
        ERROR_DS_AUX_CLS_TEST_FAIL = 8389, 

        /// <summary>
        /// The erro r_ d s_ nonexisten t_ pos s_ sup.
        /// </summary>
        ERROR_DS_NONEXISTENT_POSS_SUP = 8390, 

        /// <summary>
        /// The erro r_ d s_ su b_ cl s_ tes t_ fail.
        /// </summary>
        ERROR_DS_SUB_CLS_TEST_FAIL = 8391, 

        /// <summary>
        /// The erro r_ d s_ ba d_ rd n_ at t_ i d_ syntax.
        /// </summary>
        ERROR_DS_BAD_RDN_ATT_ID_SYNTAX = 8392, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ au x_ cls.
        /// </summary>
        ERROR_DS_EXISTS_IN_AUX_CLS = 8393, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ su b_ cls.
        /// </summary>
        ERROR_DS_EXISTS_IN_SUB_CLS = 8394, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ pos s_ sup.
        /// </summary>
        ERROR_DS_EXISTS_IN_POSS_SUP = 8395, 

        /// <summary>
        /// The erro r_ d s_ recalcschem a_ failed.
        /// </summary>
        ERROR_DS_RECALCSCHEMA_FAILED = 8396, 

        /// <summary>
        /// The erro r_ d s_ tre e_ delet e_ no t_ finished.
        /// </summary>
        ERROR_DS_TREE_DELETE_NOT_FINISHED = 8397, 

        /// <summary>
        /// The erro r_ d s_ can t_ delete.
        /// </summary>
        ERROR_DS_CANT_DELETE = 8398, 

        /// <summary>
        /// The erro r_ d s_ at t_ schem a_ re q_ id.
        /// </summary>
        ERROR_DS_ATT_SCHEMA_REQ_ID = 8399, 

        /// <summary>
        /// The erro r_ d s_ ba d_ at t_ schem a_ syntax.
        /// </summary>
        ERROR_DS_BAD_ATT_SCHEMA_SYNTAX = 8400, 

        /// <summary>
        /// The erro r_ d s_ can t_ cach e_ att.
        /// </summary>
        ERROR_DS_CANT_CACHE_ATT = 8401, 

        /// <summary>
        /// The erro r_ d s_ can t_ cach e_ class.
        /// </summary>
        ERROR_DS_CANT_CACHE_CLASS = 8402, 

        /// <summary>
        /// The erro r_ d s_ can t_ remov e_ at t_ cache.
        /// </summary>
        ERROR_DS_CANT_REMOVE_ATT_CACHE = 8403, 

        /// <summary>
        /// The erro r_ d s_ can t_ remov e_ clas s_ cache.
        /// </summary>
        ERROR_DS_CANT_REMOVE_CLASS_CACHE = 8404, 

        /// <summary>
        /// The erro r_ d s_ can t_ retriev e_ dn.
        /// </summary>
        ERROR_DS_CANT_RETRIEVE_DN = 8405, 

        /// <summary>
        /// The erro r_ d s_ missin g_ supref.
        /// </summary>
        ERROR_DS_MISSING_SUPREF = 8406, 

        /// <summary>
        /// The erro r_ d s_ can t_ retriev e_ instance.
        /// </summary>
        ERROR_DS_CANT_RETRIEVE_INSTANCE = 8407, 

        /// <summary>
        /// The erro r_ d s_ cod e_ inconsistency.
        /// </summary>
        ERROR_DS_CODE_INCONSISTENCY = 8408, 

        /// <summary>
        /// The erro r_ d s_ databas e_ error.
        /// </summary>
        ERROR_DS_DATABASE_ERROR = 8409, 

        /// <summary>
        /// The erro r_ d s_ governsi d_ missing.
        /// </summary>
        ERROR_DS_GOVERNSID_MISSING = 8410, 

        /// <summary>
        /// The erro r_ d s_ missin g_ expecte d_ att.
        /// </summary>
        ERROR_DS_MISSING_EXPECTED_ATT = 8411, 

        /// <summary>
        /// The erro r_ d s_ ncnam e_ missin g_ c r_ ref.
        /// </summary>
        ERROR_DS_NCNAME_MISSING_CR_REF = 8412, 

        /// <summary>
        /// The erro r_ d s_ securit y_ checkin g_ error.
        /// </summary>
        ERROR_DS_SECURITY_CHECKING_ERROR = 8413, 

        /// <summary>
        /// The erro r_ d s_ schem a_ no t_ loaded.
        /// </summary>
        ERROR_DS_SCHEMA_NOT_LOADED = 8414, 

        /// <summary>
        /// The erro r_ d s_ schem a_ allo c_ failed.
        /// </summary>
        ERROR_DS_SCHEMA_ALLOC_FAILED = 8415, 

        /// <summary>
        /// The erro r_ d s_ at t_ schem a_ re q_ syntax.
        /// </summary>
        ERROR_DS_ATT_SCHEMA_REQ_SYNTAX = 8416, 

        /// <summary>
        /// The erro r_ d s_ gcverif y_ error.
        /// </summary>
        ERROR_DS_GCVERIFY_ERROR = 8417, 

        /// <summary>
        /// The erro r_ d s_ dr a_ schem a_ mismatch.
        /// </summary>
        ERROR_DS_DRA_SCHEMA_MISMATCH = 8418, 

        /// <summary>
        /// The erro r_ d s_ can t_ fin d_ ds a_ obj.
        /// </summary>
        ERROR_DS_CANT_FIND_DSA_OBJ = 8419, 

        /// <summary>
        /// The erro r_ d s_ can t_ fin d_ expecte d_ nc.
        /// </summary>
        ERROR_DS_CANT_FIND_EXPECTED_NC = 8420, 

        /// <summary>
        /// The erro r_ d s_ can t_ fin d_ n c_ i n_ cache.
        /// </summary>
        ERROR_DS_CANT_FIND_NC_IN_CACHE = 8421, 

        /// <summary>
        /// The erro r_ d s_ can t_ retriev e_ child.
        /// </summary>
        ERROR_DS_CANT_RETRIEVE_CHILD = 8422, 

        /// <summary>
        /// The erro r_ d s_ securit y_ illega l_ modify.
        /// </summary>
        ERROR_DS_SECURITY_ILLEGAL_MODIFY = 8423, 

        /// <summary>
        /// The erro r_ d s_ can t_ replac e_ hidde n_ rec.
        /// </summary>
        ERROR_DS_CANT_REPLACE_HIDDEN_REC = 8424, 

        /// <summary>
        /// The erro r_ d s_ ba d_ hierarch y_ file.
        /// </summary>
        ERROR_DS_BAD_HIERARCHY_FILE = 8425, 

        /// <summary>
        /// The erro r_ d s_ buil d_ hierarch y_ tabl e_ failed.
        /// </summary>
        ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED = 8426, 

        /// <summary>
        /// The erro r_ d s_ confi g_ para m_ missing.
        /// </summary>
        ERROR_DS_CONFIG_PARAM_MISSING = 8427, 

        /// <summary>
        /// The erro r_ d s_ countin g_ a b_ indice s_ failed.
        /// </summary>
        ERROR_DS_COUNTING_AB_INDICES_FAILED = 8428, 

        /// <summary>
        /// The erro r_ d s_ hierarch y_ tabl e_ mallo c_ failed.
        /// </summary>
        ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED = 8429, 

        /// <summary>
        /// The erro r_ d s_ interna l_ failure.
        /// </summary>
        ERROR_DS_INTERNAL_FAILURE = 8430, 

        /// <summary>
        /// The erro r_ d s_ unknow n_ error.
        /// </summary>
        ERROR_DS_UNKNOWN_ERROR = 8431, 

        /// <summary>
        /// The erro r_ d s_ roo t_ require s_ clas s_ top.
        /// </summary>
        ERROR_DS_ROOT_REQUIRES_CLASS_TOP = 8432, 

        /// <summary>
        /// The erro r_ d s_ refusin g_ fsm o_ roles.
        /// </summary>
        ERROR_DS_REFUSING_FSMO_ROLES = 8433, 

        /// <summary>
        /// The erro r_ d s_ missin g_ fsm o_ settings.
        /// </summary>
        ERROR_DS_MISSING_FSMO_SETTINGS = 8434, 

        /// <summary>
        /// The erro r_ d s_ unabl e_ t o_ surrende r_ roles.
        /// </summary>
        ERROR_DS_UNABLE_TO_SURRENDER_ROLES = 8435, 

        /// <summary>
        /// The erro r_ d s_ dr a_ generic.
        /// </summary>
        ERROR_DS_DRA_GENERIC = 8436, 

        /// <summary>
        /// The erro r_ d s_ dr a_ invali d_ parameter.
        /// </summary>
        ERROR_DS_DRA_INVALID_PARAMETER = 8437, 

        /// <summary>
        /// The erro r_ d s_ dr a_ busy.
        /// </summary>
        ERROR_DS_DRA_BUSY = 8438, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ba d_ dn.
        /// </summary>
        ERROR_DS_DRA_BAD_DN = 8439, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ba d_ nc.
        /// </summary>
        ERROR_DS_DRA_BAD_NC = 8440, 

        /// <summary>
        /// The erro r_ d s_ dr a_ d n_ exists.
        /// </summary>
        ERROR_DS_DRA_DN_EXISTS = 8441, 

        /// <summary>
        /// The erro r_ d s_ dr a_ interna l_ error.
        /// </summary>
        ERROR_DS_DRA_INTERNAL_ERROR = 8442, 

        /// <summary>
        /// The erro r_ d s_ dr a_ inconsisten t_ dit.
        /// </summary>
        ERROR_DS_DRA_INCONSISTENT_DIT = 8443, 

        /// <summary>
        /// The erro r_ d s_ dr a_ connectio n_ failed.
        /// </summary>
        ERROR_DS_DRA_CONNECTION_FAILED = 8444, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ba d_ instanc e_ type.
        /// </summary>
        ERROR_DS_DRA_BAD_INSTANCE_TYPE = 8445, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ou t_ o f_ mem.
        /// </summary>
        ERROR_DS_DRA_OUT_OF_MEM = 8446, 

        /// <summary>
        /// The erro r_ d s_ dr a_ mai l_ problem.
        /// </summary>
        ERROR_DS_DRA_MAIL_PROBLEM = 8447, 

        /// <summary>
        /// The erro r_ d s_ dr a_ re f_ alread y_ exists.
        /// </summary>
        ERROR_DS_DRA_REF_ALREADY_EXISTS = 8448, 

        /// <summary>
        /// The erro r_ d s_ dr a_ re f_ no t_ found.
        /// </summary>
        ERROR_DS_DRA_REF_NOT_FOUND = 8449, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ob j_ i s_ re p_ source.
        /// </summary>
        ERROR_DS_DRA_OBJ_IS_REP_SOURCE = 8450, 

        /// <summary>
        /// The erro r_ d s_ dr a_ d b_ error.
        /// </summary>
        ERROR_DS_DRA_DB_ERROR = 8451, 

        /// <summary>
        /// The erro r_ d s_ dr a_ n o_ replica.
        /// </summary>
        ERROR_DS_DRA_NO_REPLICA = 8452, 

        /// <summary>
        /// The erro r_ d s_ dr a_ acces s_ denied.
        /// </summary>
        ERROR_DS_DRA_ACCESS_DENIED = 8453, 

        /// <summary>
        /// The erro r_ d s_ dr a_ no t_ supported.
        /// </summary>
        ERROR_DS_DRA_NOT_SUPPORTED = 8454, 

        /// <summary>
        /// The erro r_ d s_ dr a_ rp c_ cancelled.
        /// </summary>
        ERROR_DS_DRA_RPC_CANCELLED = 8455, 

        /// <summary>
        /// The erro r_ d s_ dr a_ sourc e_ disabled.
        /// </summary>
        ERROR_DS_DRA_SOURCE_DISABLED = 8456, 

        /// <summary>
        /// The erro r_ d s_ dr a_ sin k_ disabled.
        /// </summary>
        ERROR_DS_DRA_SINK_DISABLED = 8457, 

        /// <summary>
        /// The erro r_ d s_ dr a_ nam e_ collision.
        /// </summary>
        ERROR_DS_DRA_NAME_COLLISION = 8458, 

        /// <summary>
        /// The erro r_ d s_ dr a_ sourc e_ reinstalled.
        /// </summary>
        ERROR_DS_DRA_SOURCE_REINSTALLED = 8459, 

        /// <summary>
        /// The erro r_ d s_ dr a_ missin g_ parent.
        /// </summary>
        ERROR_DS_DRA_MISSING_PARENT = 8460, 

        /// <summary>
        /// The erro r_ d s_ dr a_ preempted.
        /// </summary>
        ERROR_DS_DRA_PREEMPTED = 8461, 

        /// <summary>
        /// The erro r_ d s_ dr a_ abando n_ sync.
        /// </summary>
        ERROR_DS_DRA_ABANDON_SYNC = 8462, 

        /// <summary>
        /// The erro r_ d s_ dr a_ shutdown.
        /// </summary>
        ERROR_DS_DRA_SHUTDOWN = 8463, 

        /// <summary>
        /// The erro r_ d s_ dr a_ incompatibl e_ partia l_ set.
        /// </summary>
        ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET = 8464, 

        /// <summary>
        /// The erro r_ d s_ dr a_ sourc e_ i s_ partia l_ replica.
        /// </summary>
        ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA = 8465, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ext n_ connectio n_ failed.
        /// </summary>
        ERROR_DS_DRA_EXTN_CONNECTION_FAILED = 8466, 

        /// <summary>
        /// The erro r_ d s_ instal l_ schem a_ mismatch.
        /// </summary>
        ERROR_DS_INSTALL_SCHEMA_MISMATCH = 8467, 

        /// <summary>
        /// The erro r_ d s_ du p_ lin k_ id.
        /// </summary>
        ERROR_DS_DUP_LINK_ID = 8468, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ resolving.
        /// </summary>
        ERROR_DS_NAME_ERROR_RESOLVING = 8469, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ no t_ found.
        /// </summary>
        ERROR_DS_NAME_ERROR_NOT_FOUND = 8470, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ no t_ unique.
        /// </summary>
        ERROR_DS_NAME_ERROR_NOT_UNIQUE = 8471, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ n o_ mapping.
        /// </summary>
        ERROR_DS_NAME_ERROR_NO_MAPPING = 8472, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ domai n_ only.
        /// </summary>
        ERROR_DS_NAME_ERROR_DOMAIN_ONLY = 8473, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ n o_ syntactica l_ mapping.
        /// </summary>
        ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING = 8474, 

        /// <summary>
        /// The erro r_ d s_ constructe d_ at t_ mod.
        /// </summary>
        ERROR_DS_CONSTRUCTED_ATT_MOD = 8475, 

        /// <summary>
        /// The erro r_ d s_ wron g_ o m_ ob j_ class.
        /// </summary>
        ERROR_DS_WRONG_OM_OBJ_CLASS = 8476, 

        /// <summary>
        /// The erro r_ d s_ dr a_ rep l_ pending.
        /// </summary>
        ERROR_DS_DRA_REPL_PENDING = 8477, 

        /// <summary>
        /// The erro r_ d s_ d s_ required.
        /// </summary>
        ERROR_DS_DS_REQUIRED = 8478, 

        /// <summary>
        /// The erro r_ d s_ invali d_ lda p_ displa y_ name.
        /// </summary>
        ERROR_DS_INVALID_LDAP_DISPLAY_NAME = 8479, 

        /// <summary>
        /// The erro r_ d s_ no n_ bas e_ search.
        /// </summary>
        ERROR_DS_NON_BASE_SEARCH = 8480, 

        /// <summary>
        /// The erro r_ d s_ can t_ retriev e_ atts.
        /// </summary>
        ERROR_DS_CANT_RETRIEVE_ATTS = 8481, 

        /// <summary>
        /// The erro r_ d s_ backlin k_ withou t_ link.
        /// </summary>
        ERROR_DS_BACKLINK_WITHOUT_LINK = 8482, 

        /// <summary>
        /// The erro r_ d s_ epoc h_ mismatch.
        /// </summary>
        ERROR_DS_EPOCH_MISMATCH = 8483, 

        /// <summary>
        /// The erro r_ d s_ sr c_ nam e_ mismatch.
        /// </summary>
        ERROR_DS_SRC_NAME_MISMATCH = 8484, 

        /// <summary>
        /// The erro r_ d s_ sr c_ an d_ ds t_ n c_ identical.
        /// </summary>
        ERROR_DS_SRC_AND_DST_NC_IDENTICAL = 8485, 

        /// <summary>
        /// The erro r_ d s_ ds t_ n c_ mismatch.
        /// </summary>
        ERROR_DS_DST_NC_MISMATCH = 8486, 

        /// <summary>
        /// The erro r_ d s_ no t_ authoritiv e_ fo r_ ds t_ nc.
        /// </summary>
        ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC = 8487, 

        /// <summary>
        /// The erro r_ d s_ sr c_ gui d_ mismatch.
        /// </summary>
        ERROR_DS_SRC_GUID_MISMATCH = 8488, 

        /// <summary>
        /// The erro r_ d s_ can t_ mov e_ delete d_ object.
        /// </summary>
        ERROR_DS_CANT_MOVE_DELETED_OBJECT = 8489, 

        /// <summary>
        /// The erro r_ d s_ pd c_ operatio n_ i n_ progress.
        /// </summary>
        ERROR_DS_PDC_OPERATION_IN_PROGRESS = 8490, 

        /// <summary>
        /// The erro r_ d s_ cros s_ domai n_ cleanu p_ reqd.
        /// </summary>
        ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD = 8491, 

        /// <summary>
        /// The erro r_ d s_ illega l_ xdo m_ mov e_ operation.
        /// </summary>
        ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION = 8492, 

        /// <summary>
        /// The erro r_ d s_ can t_ wit h_ acc t_ grou p_ membershps.
        /// </summary>
        ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS = 8493, 

        /// <summary>
        /// The erro r_ d s_ n c_ mus t_ hav e_ n c_ parent.
        /// </summary>
        ERROR_DS_NC_MUST_HAVE_NC_PARENT = 8494, 

        /// <summary>
        /// The erro r_ d s_ ds t_ domai n_ no t_ native.
        /// </summary>
        ERROR_DS_DST_DOMAIN_NOT_NATIVE = 8496, 

        /// <summary>
        /// The erro r_ d s_ missin g_ infrastructur e_ container.
        /// </summary>
        ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER = 8497, 

        /// <summary>
        /// The erro r_ d s_ can t_ mov e_ accoun t_ group.
        /// </summary>
        ERROR_DS_CANT_MOVE_ACCOUNT_GROUP = 8498, 

        /// <summary>
        /// The erro r_ d s_ can t_ mov e_ resourc e_ group.
        /// </summary>
        ERROR_DS_CANT_MOVE_RESOURCE_GROUP = 8499, 

        /// <summary>
        /// The erro r_ d s_ invali d_ searc h_ flag.
        /// </summary>
        ERROR_DS_INVALID_SEARCH_FLAG = 8500, 

        /// <summary>
        /// The erro r_ d s_ n o_ tre e_ delet e_ abov e_ nc.
        /// </summary>
        ERROR_DS_NO_TREE_DELETE_ABOVE_NC = 8501, 

        /// <summary>
        /// The erro r_ d s_ couldn t_ loc k_ tre e_ fo r_ delete.
        /// </summary>
        ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE = 8502, 

        /// <summary>
        /// The erro r_ d s_ couldn t_ identif y_ object s_ fo r_ tre e_ delete.
        /// </summary>
        ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE = 8503, 

        /// <summary>
        /// The erro r_ d s_ sa m_ ini t_ failure.
        /// </summary>
        ERROR_DS_SAM_INIT_FAILURE = 8504, 

        /// <summary>
        /// The erro r_ d s_ sensitiv e_ grou p_ violation.
        /// </summary>
        ERROR_DS_SENSITIVE_GROUP_VIOLATION = 8505, 

        /// <summary>
        /// The erro r_ d s_ can t_ mo d_ primarygroupid.
        /// </summary>
        ERROR_DS_CANT_MOD_PRIMARYGROUPID = 8506, 

        /// <summary>
        /// The erro r_ d s_ illega l_ bas e_ schem a_ mod.
        /// </summary>
        ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD = 8507, 

        /// <summary>
        /// The erro r_ d s_ nonsaf e_ schem a_ change.
        /// </summary>
        ERROR_DS_NONSAFE_SCHEMA_CHANGE = 8508, 

        /// <summary>
        /// The erro r_ d s_ schem a_ updat e_ disallowed.
        /// </summary>
        ERROR_DS_SCHEMA_UPDATE_DISALLOWED = 8509, 

        /// <summary>
        /// The erro r_ d s_ can t_ creat e_ unde r_ schema.
        /// </summary>
        ERROR_DS_CANT_CREATE_UNDER_SCHEMA = 8510, 

        /// <summary>
        /// The erro r_ d s_ instal l_ n o_ sr c_ sc h_ version.
        /// </summary>
        ERROR_DS_INSTALL_NO_SRC_SCH_VERSION = 8511, 

        /// <summary>
        /// The erro r_ d s_ instal l_ n o_ sc h_ versio n_ i n_ inifile.
        /// </summary>
        ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE = 8512, 

        /// <summary>
        /// The erro r_ d s_ invali d_ grou p_ type.
        /// </summary>
        ERROR_DS_INVALID_GROUP_TYPE = 8513, 

        /// <summary>
        /// The erro r_ d s_ n o_ nes t_ globalgrou p_ i n_ mixeddomain.
        /// </summary>
        ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = 8514, 

        /// <summary>
        /// The erro r_ d s_ n o_ nes t_ localgrou p_ i n_ mixeddomain.
        /// </summary>
        ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = 8515, 

        /// <summary>
        /// The erro r_ d s_ globa l_ can t_ hav e_ loca l_ member.
        /// </summary>
        ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER = 8516, 

        /// <summary>
        /// The erro r_ d s_ globa l_ can t_ hav e_ universa l_ member.
        /// </summary>
        ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER = 8517, 

        /// <summary>
        /// The erro r_ d s_ universa l_ can t_ hav e_ loca l_ member.
        /// </summary>
        ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER = 8518, 

        /// <summary>
        /// The erro r_ d s_ globa l_ can t_ hav e_ crossdomai n_ member.
        /// </summary>
        ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER = 8519, 

        /// <summary>
        /// The erro r_ d s_ loca l_ can t_ hav e_ crossdomai n_ loca l_ member.
        /// </summary>
        ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER = 8520, 

        /// <summary>
        /// The erro r_ d s_ hav e_ primar y_ members.
        /// </summary>
        ERROR_DS_HAVE_PRIMARY_MEMBERS = 8521, 

        /// <summary>
        /// The erro r_ d s_ strin g_ s d_ conversio n_ failed.
        /// </summary>
        ERROR_DS_STRING_SD_CONVERSION_FAILED = 8522, 

        /// <summary>
        /// The erro r_ d s_ namin g_ maste r_ gc.
        /// </summary>
        ERROR_DS_NAMING_MASTER_GC = 8523, 

        /// <summary>
        /// The erro r_ d s_ looku p_ failure.
        /// </summary>
        ERROR_DS_LOOKUP_FAILURE = 8524, 

        /// <summary>
        /// The erro r_ d s_ couldn t_ updat e_ spns.
        /// </summary>
        ERROR_DS_COULDNT_UPDATE_SPNS = 8525, 

        /// <summary>
        /// The erro r_ d s_ can t_ retriev e_ sd.
        /// </summary>
        ERROR_DS_CANT_RETRIEVE_SD = 8526, 

        /// <summary>
        /// The erro r_ d s_ ke y_ no t_ unique.
        /// </summary>
        ERROR_DS_KEY_NOT_UNIQUE = 8527, 

        /// <summary>
        /// The erro r_ d s_ wron g_ linke d_ at t_ syntax.
        /// </summary>
        ERROR_DS_WRONG_LINKED_ATT_SYNTAX = 8528, 

        /// <summary>
        /// The erro r_ d s_ sa m_ nee d_ bootke y_ password.
        /// </summary>
        ERROR_DS_SAM_NEED_BOOTKEY_PASSWORD = 8529, 

        /// <summary>
        /// The erro r_ d s_ sa m_ nee d_ bootke y_ floppy.
        /// </summary>
        ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY = 8530, 

        /// <summary>
        /// The erro r_ d s_ can t_ start.
        /// </summary>
        ERROR_DS_CANT_START = 8531, 

        /// <summary>
        /// The erro r_ d s_ ini t_ failure.
        /// </summary>
        ERROR_DS_INIT_FAILURE = 8532, 

        /// <summary>
        /// The erro r_ d s_ n o_ pk t_ privac y_ o n_ connection.
        /// </summary>
        ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION = 8533, 

        /// <summary>
        /// The erro r_ d s_ sourc e_ domai n_ i n_ forest.
        /// </summary>
        ERROR_DS_SOURCE_DOMAIN_IN_FOREST = 8534, 

        /// <summary>
        /// The erro r_ d s_ destinatio n_ domai n_ no t_ i n_ forest.
        /// </summary>
        ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST = 8535, 

        /// <summary>
        /// The erro r_ d s_ destinatio n_ auditin g_ no t_ enabled.
        /// </summary>
        ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED = 8536, 

        /// <summary>
        /// The erro r_ d s_ can t_ fin d_ d c_ fo r_ sr c_ domain.
        /// </summary>
        ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN = 8537, 

        /// <summary>
        /// The erro r_ d s_ sr c_ ob j_ no t_ grou p_ o r_ user.
        /// </summary>
        ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER = 8538, 

        /// <summary>
        /// The erro r_ d s_ sr c_ si d_ exist s_ i n_ forest.
        /// </summary>
        ERROR_DS_SRC_SID_EXISTS_IN_FOREST = 8539, 

        /// <summary>
        /// The erro r_ d s_ sr c_ an d_ ds t_ objec t_ clas s_ mismatch.
        /// </summary>
        ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH = 8540, 

        /// <summary>
        /// The erro r_ sa m_ ini t_ failure.
        /// </summary>
        ERROR_SAM_INIT_FAILURE = 8541, 

        /// <summary>
        /// The erro r_ d s_ dr a_ schem a_ inf o_ ship.
        /// </summary>
        ERROR_DS_DRA_SCHEMA_INFO_SHIP = 8542, 

        /// <summary>
        /// The erro r_ d s_ dr a_ schem a_ conflict.
        /// </summary>
        ERROR_DS_DRA_SCHEMA_CONFLICT = 8543, 

        /// <summary>
        /// The erro r_ d s_ dr a_ earlie r_ schem a_ conlict.
        /// </summary>
        ERROR_DS_DRA_EARLIER_SCHEMA_CONLICT = 8544, 

        /// <summary>
        /// The erro r_ d s_ dr a_ ob j_ n c_ mismatch.
        /// </summary>
        ERROR_DS_DRA_OBJ_NC_MISMATCH = 8545, 

        /// <summary>
        /// The erro r_ d s_ n c_ stil l_ ha s_ dsas.
        /// </summary>
        ERROR_DS_NC_STILL_HAS_DSAS = 8546, 

        /// <summary>
        /// The erro r_ d s_ g c_ required.
        /// </summary>
        ERROR_DS_GC_REQUIRED = 8547, 

        /// <summary>
        /// The erro r_ d s_ loca l_ membe r_ o f_ loca l_ only.
        /// </summary>
        ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY = 8548, 

        /// <summary>
        /// The erro r_ d s_ n o_ fp o_ i n_ universa l_ groups.
        /// </summary>
        ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS = 8549, 

        /// <summary>
        /// The erro r_ d s_ can t_ ad d_ t o_ gc.
        /// </summary>
        ERROR_DS_CANT_ADD_TO_GC = 8550, 

        /// <summary>
        /// The erro r_ d s_ n o_ checkpoin t_ wit h_ pdc.
        /// </summary>
        ERROR_DS_NO_CHECKPOINT_WITH_PDC = 8551, 

        /// <summary>
        /// The erro r_ d s_ sourc e_ auditin g_ no t_ enabled.
        /// </summary>
        ERROR_DS_SOURCE_AUDITING_NOT_ENABLED = 8552, 

        /// <summary>
        /// The erro r_ d s_ can t_ creat e_ i n_ nondomai n_ nc.
        /// </summary>
        ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC = 8553, 

        /// <summary>
        /// The erro r_ d s_ invali d_ nam e_ fo r_ spn.
        /// </summary>
        ERROR_DS_INVALID_NAME_FOR_SPN = 8554, 

        /// <summary>
        /// The erro r_ d s_ filte r_ use s_ contructe d_ attrs.
        /// </summary>
        ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS = 8555, 

        /// <summary>
        /// The erro r_ d s_ unicodepw d_ no t_ i n_ quotes.
        /// </summary>
        ERROR_DS_UNICODEPWD_NOT_IN_QUOTES = 8556, 

        /// <summary>
        /// The erro r_ d s_ machin e_ accoun t_ quot a_ exceeded.
        /// </summary>
        ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED = 8557, 

        /// <summary>
        /// The erro r_ d s_ mus t_ b e_ ru n_ o n_ ds t_ dc.
        /// </summary>
        ERROR_DS_MUST_BE_RUN_ON_DST_DC = 8558, 

        /// <summary>
        /// The erro r_ d s_ sr c_ d c_ mus t_ b e_ s p 4_ o r_ greater.
        /// </summary>
        ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER = 8559, 

        /// <summary>
        /// The erro r_ d s_ can t_ tre e_ delet e_ critica l_ obj.
        /// </summary>
        ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ = 8560, 

        /// <summary>
        /// The erro r_ d s_ ini t_ failur e_ console.
        /// </summary>
        ERROR_DS_INIT_FAILURE_CONSOLE = 8561, 

        /// <summary>
        /// The erro r_ d s_ sa m_ ini t_ failur e_ console.
        /// </summary>
        ERROR_DS_SAM_INIT_FAILURE_CONSOLE = 8562, 

        /// <summary>
        /// The erro r_ d s_ fores t_ versio n_ to o_ high.
        /// </summary>
        ERROR_DS_FOREST_VERSION_TOO_HIGH = 8563, 

        /// <summary>
        /// The erro r_ d s_ domai n_ versio n_ to o_ high.
        /// </summary>
        ERROR_DS_DOMAIN_VERSION_TOO_HIGH = 8564, 

        /// <summary>
        /// The erro r_ d s_ fores t_ versio n_ to o_ low.
        /// </summary>
        ERROR_DS_FOREST_VERSION_TOO_LOW = 8565, 

        /// <summary>
        /// The erro r_ d s_ domai n_ versio n_ to o_ low.
        /// </summary>
        ERROR_DS_DOMAIN_VERSION_TOO_LOW = 8566, 

        /// <summary>
        /// The erro r_ d s_ incompatibl e_ version.
        /// </summary>
        ERROR_DS_INCOMPATIBLE_VERSION = 8567, 

        /// <summary>
        /// The erro r_ d s_ lo w_ ds a_ version.
        /// </summary>
        ERROR_DS_LOW_DSA_VERSION = 8568, 

        /// <summary>
        /// The erro r_ d s_ n o_ behavio r_ versio n_ i n_ mixeddomain.
        /// </summary>
        ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN = 8569, 

        /// <summary>
        /// The erro r_ d s_ no t_ supporte d_ sor t_ order.
        /// </summary>
        ERROR_DS_NOT_SUPPORTED_SORT_ORDER = 8570, 

        /// <summary>
        /// The erro r_ d s_ nam e_ no t_ unique.
        /// </summary>
        ERROR_DS_NAME_NOT_UNIQUE = 8571, 

        /// <summary>
        /// The erro r_ d s_ machin e_ accoun t_ create d_ pren t 4.
        /// </summary>
        ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 = 8572, 

        /// <summary>
        /// The erro r_ d s_ ou t_ o f_ versio n_ store.
        /// </summary>
        ERROR_DS_OUT_OF_VERSION_STORE = 8573, 

        /// <summary>
        /// The erro r_ d s_ incompatibl e_ control s_ used.
        /// </summary>
        ERROR_DS_INCOMPATIBLE_CONTROLS_USED = 8574, 

        /// <summary>
        /// The erro r_ d s_ n o_ re f_ domain.
        /// </summary>
        ERROR_DS_NO_REF_DOMAIN = 8575, 

        /// <summary>
        /// The erro r_ d s_ reserve d_ lin k_ id.
        /// </summary>
        ERROR_DS_RESERVED_LINK_ID = 8576, 

        /// <summary>
        /// The erro r_ d s_ lin k_ i d_ no t_ available.
        /// </summary>
        ERROR_DS_LINK_ID_NOT_AVAILABLE = 8577, 

        /// <summary>
        /// The erro r_ d s_ a g_ can t_ hav e_ universa l_ member.
        /// </summary>
        ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER = 8578, 

        /// <summary>
        /// The erro r_ d s_ modifyd n_ disallowe d_ b y_ instanc e_ type.
        /// </summary>
        ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE = 8579, 

        /// <summary>
        /// The erro r_ d s_ n o_ objec t_ mov e_ i n_ schem a_ nc.
        /// </summary>
        ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC = 8580, 

        /// <summary>
        /// The erro r_ d s_ modifyd n_ disallowe d_ b y_ flag.
        /// </summary>
        ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG = 8581, 

        /// <summary>
        /// The erro r_ d s_ modifyd n_ wron g_ grandparent.
        /// </summary>
        ERROR_DS_MODIFYDN_WRONG_GRANDPARENT = 8582, 

        /// <summary>
        /// The erro r_ d s_ nam e_ erro r_ trus t_ referral.
        /// </summary>
        ERROR_DS_NAME_ERROR_TRUST_REFERRAL = 8583, 

        /// <summary>
        /// The erro r_ no t_ supporte d_ o n_ standar d_ server.
        /// </summary>
        ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER = 8584, 

        /// <summary>
        /// The erro r_ d s_ can t_ acces s_ remot e_ par t_ o f_ ad.
        /// </summary>
        ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD = 8585, 

        /// <summary>
        /// The erro r_ d s_ c r_ impossibl e_ t o_ validate.
        /// </summary>
        ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE = 8586, 

        /// <summary>
        /// The erro r_ d s_ threa d_ limi t_ exceeded.
        /// </summary>
        ERROR_DS_THREAD_LIMIT_EXCEEDED = 8587, 

        /// <summary>
        /// The erro r_ d s_ no t_ closest.
        /// </summary>
        ERROR_DS_NOT_CLOSEST = 8588, 

        /// <summary>
        /// The erro r_ d s_ can t_ deriv e_ sp n_ withou t_ serve r_ ref.
        /// </summary>
        ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF = 8589, 

        /// <summary>
        /// The erro r_ d s_ singl e_ use r_ mod e_ failed.
        /// </summary>
        ERROR_DS_SINGLE_USER_MODE_FAILED = 8590, 

        /// <summary>
        /// The erro r_ d s_ ntdscrip t_ synta x_ error.
        /// </summary>
        ERROR_DS_NTDSCRIPT_SYNTAX_ERROR = 8591, 

        /// <summary>
        /// The erro r_ d s_ ntdscrip t_ proces s_ error.
        /// </summary>
        ERROR_DS_NTDSCRIPT_PROCESS_ERROR = 8592, 

        /// <summary>
        /// The erro r_ d s_ differen t_ rep l_ epochs.
        /// </summary>
        ERROR_DS_DIFFERENT_REPL_EPOCHS = 8593, 

        /// <summary>
        /// The erro r_ d s_ dr s_ extension s_ changed.
        /// </summary>
        ERROR_DS_DRS_EXTENSIONS_CHANGED = 8594, 

        /// <summary>
        /// The erro r_ d s_ replic a_ se t_ chang e_ no t_ allowe d_ o n_ disable d_ cr.
        /// </summary>
        ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR = 8595, 

        /// <summary>
        /// The erro r_ d s_ n o_ msd s_ intid.
        /// </summary>
        ERROR_DS_NO_MSDS_INTID = 8596, 

        /// <summary>
        /// The erro r_ d s_ du p_ msd s_ intid.
        /// </summary>
        ERROR_DS_DUP_MSDS_INTID = 8597, 

        /// <summary>
        /// The erro r_ d s_ exist s_ i n_ rdnattid.
        /// </summary>
        ERROR_DS_EXISTS_IN_RDNATTID = 8598, 

        /// <summary>
        /// The erro r_ d s_ authorizatio n_ failed.
        /// </summary>
        ERROR_DS_AUTHORIZATION_FAILED = 8599, 

        /// <summary>
        /// The erro r_ d s_ invali d_ script.
        /// </summary>
        ERROR_DS_INVALID_SCRIPT = 8600, 

        /// <summary>
        /// The erro r_ d s_ remot e_ crossre f_ o p_ failed.
        /// </summary>
        ERROR_DS_REMOTE_CROSSREF_OP_FAILED = 8601, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ forma t_ error.
        /// </summary>
        DNS_ERROR_RCODE_FORMAT_ERROR = 9001, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ serve r_ failure.
        /// </summary>
        DNS_ERROR_RCODE_SERVER_FAILURE = 9002, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ nam e_ error.
        /// </summary>
        DNS_ERROR_RCODE_NAME_ERROR = 9003, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ no t_ implemented.
        /// </summary>
        DNS_ERROR_RCODE_NOT_IMPLEMENTED = 9004, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ refused.
        /// </summary>
        DNS_ERROR_RCODE_REFUSED = 9005, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ yxdomain.
        /// </summary>
        DNS_ERROR_RCODE_YXDOMAIN = 9006, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ yxrrset.
        /// </summary>
        DNS_ERROR_RCODE_YXRRSET = 9007, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ nxrrset.
        /// </summary>
        DNS_ERROR_RCODE_NXRRSET = 9008, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ notauth.
        /// </summary>
        DNS_ERROR_RCODE_NOTAUTH = 9009, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ notzone.
        /// </summary>
        DNS_ERROR_RCODE_NOTZONE = 9010, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ badsig.
        /// </summary>
        DNS_ERROR_RCODE_BADSIG = 9016, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ badkey.
        /// </summary>
        DNS_ERROR_RCODE_BADKEY = 9017, 

        /// <summary>
        /// The dn s_ erro r_ rcod e_ badtime.
        /// </summary>
        DNS_ERROR_RCODE_BADTIME = 9018, 

        /// <summary>
        /// The dn s_ inf o_ n o_ records.
        /// </summary>
        DNS_INFO_NO_RECORDS = 9501, 

        /// <summary>
        /// The dn s_ erro r_ ba d_ packet.
        /// </summary>
        DNS_ERROR_BAD_PACKET = 9502, 

        /// <summary>
        /// The dn s_ erro r_ n o_ packet.
        /// </summary>
        DNS_ERROR_NO_PACKET = 9503, 

        /// <summary>
        /// The dn s_ erro r_ rcode.
        /// </summary>
        DNS_ERROR_RCODE = 9504, 

        /// <summary>
        /// The dn s_ erro r_ unsecur e_ packet.
        /// </summary>
        DNS_ERROR_UNSECURE_PACKET = 9505, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ type.
        /// </summary>
        DNS_ERROR_INVALID_TYPE = 9551, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ i p_ address.
        /// </summary>
        DNS_ERROR_INVALID_IP_ADDRESS = 9552, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ property.
        /// </summary>
        DNS_ERROR_INVALID_PROPERTY = 9553, 

        /// <summary>
        /// The dn s_ erro r_ tr y_ agai n_ later.
        /// </summary>
        DNS_ERROR_TRY_AGAIN_LATER = 9554, 

        /// <summary>
        /// The dn s_ erro r_ no t_ unique.
        /// </summary>
        DNS_ERROR_NOT_UNIQUE = 9555, 

        /// <summary>
        /// The dn s_ erro r_ no n_ rf c_ name.
        /// </summary>
        DNS_ERROR_NON_RFC_NAME = 9556, 

        /// <summary>
        /// The dn s_ statu s_ fqdn.
        /// </summary>
        DNS_STATUS_FQDN = 9557, 

        /// <summary>
        /// The dn s_ statu s_ dotte d_ name.
        /// </summary>
        DNS_STATUS_DOTTED_NAME = 9558, 

        /// <summary>
        /// The dn s_ statu s_ singl e_ par t_ name.
        /// </summary>
        DNS_STATUS_SINGLE_PART_NAME = 9559, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ nam e_ char.
        /// </summary>
        DNS_ERROR_INVALID_NAME_CHAR = 9560, 

        /// <summary>
        /// The dn s_ erro r_ numeri c_ name.
        /// </summary>
        DNS_ERROR_NUMERIC_NAME = 9561, 

        /// <summary>
        /// The dn s_ erro r_ no t_ allowe d_ o n_ roo t_ server.
        /// </summary>
        DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER = 9562, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ doe s_ no t_ exist.
        /// </summary>
        DNS_ERROR_ZONE_DOES_NOT_EXIST = 9601, 

        /// <summary>
        /// The dn s_ erro r_ n o_ zon e_ info.
        /// </summary>
        DNS_ERROR_NO_ZONE_INFO = 9602, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ zon e_ operation.
        /// </summary>
        DNS_ERROR_INVALID_ZONE_OPERATION = 9603, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ configuratio n_ error.
        /// </summary>
        DNS_ERROR_ZONE_CONFIGURATION_ERROR = 9604, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ ha s_ n o_ so a_ record.
        /// </summary>
        DNS_ERROR_ZONE_HAS_NO_SOA_RECORD = 9605, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ ha s_ n o_ n s_ records.
        /// </summary>
        DNS_ERROR_ZONE_HAS_NO_NS_RECORDS = 9606, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ locked.
        /// </summary>
        DNS_ERROR_ZONE_LOCKED = 9607, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ creatio n_ failed.
        /// </summary>
        DNS_ERROR_ZONE_CREATION_FAILED = 9608, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ alread y_ exists.
        /// </summary>
        DNS_ERROR_ZONE_ALREADY_EXISTS = 9609, 

        /// <summary>
        /// The dn s_ erro r_ autozon e_ alread y_ exists.
        /// </summary>
        DNS_ERROR_AUTOZONE_ALREADY_EXISTS = 9610, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ zon e_ type.
        /// </summary>
        DNS_ERROR_INVALID_ZONE_TYPE = 9611, 

        /// <summary>
        /// The dn s_ erro r_ secondar y_ require s_ maste r_ ip.
        /// </summary>
        DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP = 9612, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ no t_ secondary.
        /// </summary>
        DNS_ERROR_ZONE_NOT_SECONDARY = 9613, 

        /// <summary>
        /// The dn s_ erro r_ nee d_ secondar y_ addresses.
        /// </summary>
        DNS_ERROR_NEED_SECONDARY_ADDRESSES = 9614, 

        /// <summary>
        /// The dn s_ erro r_ win s_ ini t_ failed.
        /// </summary>
        DNS_ERROR_WINS_INIT_FAILED = 9615, 

        /// <summary>
        /// The dn s_ erro r_ nee d_ win s_ servers.
        /// </summary>
        DNS_ERROR_NEED_WINS_SERVERS = 9616, 

        /// <summary>
        /// The dn s_ erro r_ nbsta t_ ini t_ failed.
        /// </summary>
        DNS_ERROR_NBSTAT_INIT_FAILED = 9617, 

        /// <summary>
        /// The dn s_ erro r_ so a_ delet e_ invalid.
        /// </summary>
        DNS_ERROR_SOA_DELETE_INVALID = 9618, 

        /// <summary>
        /// The dn s_ erro r_ forwarde r_ alread y_ exists.
        /// </summary>
        DNS_ERROR_FORWARDER_ALREADY_EXISTS = 9619, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ require s_ maste r_ ip.
        /// </summary>
        DNS_ERROR_ZONE_REQUIRES_MASTER_IP = 9620, 

        /// <summary>
        /// The dn s_ erro r_ zon e_ i s_ shutdown.
        /// </summary>
        DNS_ERROR_ZONE_IS_SHUTDOWN = 9621, 

        /// <summary>
        /// The dn s_ erro r_ primar y_ require s_ datafile.
        /// </summary>
        DNS_ERROR_PRIMARY_REQUIRES_DATAFILE = 9651, 

        /// <summary>
        /// The dn s_ erro r_ invali d_ datafil e_ name.
        /// </summary>
        DNS_ERROR_INVALID_DATAFILE_NAME = 9652, 

        /// <summary>
        /// The dn s_ erro r_ datafil e_ ope n_ failure.
        /// </summary>
        DNS_ERROR_DATAFILE_OPEN_FAILURE = 9653, 

        /// <summary>
        /// The dn s_ erro r_ fil e_ writebac k_ failed.
        /// </summary>
        DNS_ERROR_FILE_WRITEBACK_FAILED = 9654, 

        /// <summary>
        /// The dn s_ erro r_ datafil e_ parsing.
        /// </summary>
        DNS_ERROR_DATAFILE_PARSING = 9655, 

        /// <summary>
        /// The dn s_ erro r_ recor d_ doe s_ no t_ exist.
        /// </summary>
        DNS_ERROR_RECORD_DOES_NOT_EXIST = 9701, 

        /// <summary>
        /// The dn s_ erro r_ recor d_ format.
        /// </summary>
        DNS_ERROR_RECORD_FORMAT = 9702, 

        /// <summary>
        /// The dn s_ erro r_ nod e_ creatio n_ failed.
        /// </summary>
        DNS_ERROR_NODE_CREATION_FAILED = 9703, 

        /// <summary>
        /// The dn s_ erro r_ unknow n_ recor d_ type.
        /// </summary>
        DNS_ERROR_UNKNOWN_RECORD_TYPE = 9704, 

        /// <summary>
        /// The dn s_ erro r_ recor d_ time d_ out.
        /// </summary>
        DNS_ERROR_RECORD_TIMED_OUT = 9705, 

        /// <summary>
        /// The dn s_ erro r_ nam e_ no t_ i n_ zone.
        /// </summary>
        DNS_ERROR_NAME_NOT_IN_ZONE = 9706, 

        /// <summary>
        /// The dn s_ erro r_ cnam e_ loop.
        /// </summary>
        DNS_ERROR_CNAME_LOOP = 9707, 

        /// <summary>
        /// The dn s_ erro r_ nod e_ i s_ cname.
        /// </summary>
        DNS_ERROR_NODE_IS_CNAME = 9708, 

        /// <summary>
        /// The dn s_ erro r_ cnam e_ collision.
        /// </summary>
        DNS_ERROR_CNAME_COLLISION = 9709, 

        /// <summary>
        /// The dn s_ erro r_ recor d_ onl y_ a t_ zon e_ root.
        /// </summary>
        DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT = 9710, 

        /// <summary>
        /// The dn s_ erro r_ recor d_ alread y_ exists.
        /// </summary>
        DNS_ERROR_RECORD_ALREADY_EXISTS = 9711, 

        /// <summary>
        /// The dn s_ erro r_ secondar y_ data.
        /// </summary>
        DNS_ERROR_SECONDARY_DATA = 9712, 

        /// <summary>
        /// The dn s_ erro r_ n o_ creat e_ cach e_ data.
        /// </summary>
        DNS_ERROR_NO_CREATE_CACHE_DATA = 9713, 

        /// <summary>
        /// The dn s_ erro r_ nam e_ doe s_ no t_ exist.
        /// </summary>
        DNS_ERROR_NAME_DOES_NOT_EXIST = 9714, 

        /// <summary>
        /// The dn s_ warnin g_ pt r_ creat e_ failed.
        /// </summary>
        DNS_WARNING_PTR_CREATE_FAILED = 9715, 

        /// <summary>
        /// The dn s_ warnin g_ domai n_ undeleted.
        /// </summary>
        DNS_WARNING_DOMAIN_UNDELETED = 9716, 

        /// <summary>
        /// The dn s_ erro r_ d s_ unavailable.
        /// </summary>
        DNS_ERROR_DS_UNAVAILABLE = 9717, 

        /// <summary>
        /// The dn s_ erro r_ d s_ zon e_ alread y_ exists.
        /// </summary>
        DNS_ERROR_DS_ZONE_ALREADY_EXISTS = 9718, 

        /// <summary>
        /// The dn s_ erro r_ n o_ bootfil e_ i f_ d s_ zone.
        /// </summary>
        DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE = 9719, 

        /// <summary>
        /// The dn s_ inf o_ axf r_ complete.
        /// </summary>
        DNS_INFO_AXFR_COMPLETE = 9751, 

        /// <summary>
        /// The dn s_ erro r_ axfr.
        /// </summary>
        DNS_ERROR_AXFR = 9752, 

        /// <summary>
        /// The dn s_ inf o_ adde d_ loca l_ wins.
        /// </summary>
        DNS_INFO_ADDED_LOCAL_WINS = 9753, 

        /// <summary>
        /// The dn s_ statu s_ continu e_ needed.
        /// </summary>
        DNS_STATUS_CONTINUE_NEEDED = 9801, 

        /// <summary>
        /// The dn s_ erro r_ n o_ tcpip.
        /// </summary>
        DNS_ERROR_NO_TCPIP = 9851, 

        /// <summary>
        /// The dn s_ erro r_ n o_ dn s_ servers.
        /// </summary>
        DNS_ERROR_NO_DNS_SERVERS = 9852, 

        /// <summary>
        /// The dn s_ erro r_ d p_ doe s_ no t_ exist.
        /// </summary>
        DNS_ERROR_DP_DOES_NOT_EXIST = 9901, 

        /// <summary>
        /// The dn s_ erro r_ d p_ alread y_ exists.
        /// </summary>
        DNS_ERROR_DP_ALREADY_EXISTS = 9902, 

        /// <summary>
        /// The dn s_ erro r_ d p_ no t_ enlisted.
        /// </summary>
        DNS_ERROR_DP_NOT_ENLISTED = 9903, 

        /// <summary>
        /// The dn s_ erro r_ d p_ alread y_ enlisted.
        /// </summary>
        DNS_ERROR_DP_ALREADY_ENLISTED = 9904, 

        /// <summary>
        /// The wsaeintr.
        /// </summary>
        WSAEINTR = 10004, 

        /// <summary>
        /// The wsaebadf.
        /// </summary>
        WSAEBADF = 10009, 

        /// <summary>
        /// The wsaeacces.
        /// </summary>
        WSAEACCES = 10013, 

        /// <summary>
        /// The wsaefault.
        /// </summary>
        WSAEFAULT = 10014, 

        /// <summary>
        /// The wsaeinval.
        /// </summary>
        WSAEINVAL = 10022, 

        /// <summary>
        /// The wsaemfile.
        /// </summary>
        WSAEMFILE = 10024, 

        /// <summary>
        /// The wsaewouldblock.
        /// </summary>
        WSAEWOULDBLOCK = 10035, 

        /// <summary>
        /// The wsaeinprogress.
        /// </summary>
        WSAEINPROGRESS = 10036, 

        /// <summary>
        /// The wsaealready.
        /// </summary>
        WSAEALREADY = 10037, 

        /// <summary>
        /// The wsaenotsock.
        /// </summary>
        WSAENOTSOCK = 10038, 

        /// <summary>
        /// The wsaedestaddrreq.
        /// </summary>
        WSAEDESTADDRREQ = 10039, 

        /// <summary>
        /// The wsaemsgsize.
        /// </summary>
        WSAEMSGSIZE = 10040, 

        /// <summary>
        /// The wsaeprototype.
        /// </summary>
        WSAEPROTOTYPE = 10041, 

        /// <summary>
        /// The wsaenoprotoopt.
        /// </summary>
        WSAENOPROTOOPT = 10042, 

        /// <summary>
        /// The wsaeprotonosupport.
        /// </summary>
        WSAEPROTONOSUPPORT = 10043, 

        /// <summary>
        /// The wsaesocktnosupport.
        /// </summary>
        WSAESOCKTNOSUPPORT = 10044, 

        /// <summary>
        /// The wsaeopnotsupp.
        /// </summary>
        WSAEOPNOTSUPP = 10045, 

        /// <summary>
        /// The wsaepfnosupport.
        /// </summary>
        WSAEPFNOSUPPORT = 10046, 

        /// <summary>
        /// The wsaeafnosupport.
        /// </summary>
        WSAEAFNOSUPPORT = 10047, 

        /// <summary>
        /// The wsaeaddrinuse.
        /// </summary>
        WSAEADDRINUSE = 10048, 

        /// <summary>
        /// The wsaeaddrnotavail.
        /// </summary>
        WSAEADDRNOTAVAIL = 10049, 

        /// <summary>
        /// The wsaenetdown.
        /// </summary>
        WSAENETDOWN = 10050, 

        /// <summary>
        /// The wsaenetunreach.
        /// </summary>
        WSAENETUNREACH = 10051, 

        /// <summary>
        /// The wsaenetreset.
        /// </summary>
        WSAENETRESET = 10052, 

        /// <summary>
        /// The wsaeconnaborted.
        /// </summary>
        WSAECONNABORTED = 10053, 

        /// <summary>
        /// The wsaeconnreset.
        /// </summary>
        WSAECONNRESET = 10054, 

        /// <summary>
        /// The wsaenobufs.
        /// </summary>
        WSAENOBUFS = 10055, 

        /// <summary>
        /// The wsaeisconn.
        /// </summary>
        WSAEISCONN = 10056, 

        /// <summary>
        /// The wsaenotconn.
        /// </summary>
        WSAENOTCONN = 10057, 

        /// <summary>
        /// The wsaeshutdown.
        /// </summary>
        WSAESHUTDOWN = 10058, 

        /// <summary>
        /// The wsaetoomanyrefs.
        /// </summary>
        WSAETOOMANYREFS = 10059, 

        /// <summary>
        /// The wsaetimedout.
        /// </summary>
        WSAETIMEDOUT = 10060, 

        /// <summary>
        /// The wsaeconnrefused.
        /// </summary>
        WSAECONNREFUSED = 10061, 

        /// <summary>
        /// The wsaeloop.
        /// </summary>
        WSAELOOP = 10062, 

        /// <summary>
        /// The wsaenametoolong.
        /// </summary>
        WSAENAMETOOLONG = 10063, 

        /// <summary>
        /// The wsaehostdown.
        /// </summary>
        WSAEHOSTDOWN = 10064, 

        /// <summary>
        /// The wsaehostunreach.
        /// </summary>
        WSAEHOSTUNREACH = 10065, 

        /// <summary>
        /// The wsaenotempty.
        /// </summary>
        WSAENOTEMPTY = 10066, 

        /// <summary>
        /// The wsaeproclim.
        /// </summary>
        WSAEPROCLIM = 10067, 

        /// <summary>
        /// The wsaeusers.
        /// </summary>
        WSAEUSERS = 10068, 

        /// <summary>
        /// The wsaedquot.
        /// </summary>
        WSAEDQUOT = 10069, 

        /// <summary>
        /// The wsaestale.
        /// </summary>
        WSAESTALE = 10070, 

        /// <summary>
        /// The wsaeremote.
        /// </summary>
        WSAEREMOTE = 10071, 

        /// <summary>
        /// The wsasysnotready.
        /// </summary>
        WSASYSNOTREADY = 10091, 

        /// <summary>
        /// The wsavernotsupported.
        /// </summary>
        WSAVERNOTSUPPORTED = 10092, 

        /// <summary>
        /// The wsanotinitialised.
        /// </summary>
        WSANOTINITIALISED = 10093, 

        /// <summary>
        /// The wsaediscon.
        /// </summary>
        WSAEDISCON = 10101, 

        /// <summary>
        /// The wsaenomore.
        /// </summary>
        WSAENOMORE = 10102, 

        /// <summary>
        /// The wsaecancelled.
        /// </summary>
        WSAECANCELLED = 10103, 

        /// <summary>
        /// The wsaeinvalidproctable.
        /// </summary>
        WSAEINVALIDPROCTABLE = 10104, 

        /// <summary>
        /// The wsaeinvalidprovider.
        /// </summary>
        WSAEINVALIDPROVIDER = 10105, 

        /// <summary>
        /// The wsaeproviderfailedinit.
        /// </summary>
        WSAEPROVIDERFAILEDINIT = 10106, 

        /// <summary>
        /// The wsasyscallfailure.
        /// </summary>
        WSASYSCALLFAILURE = 10107, 

        /// <summary>
        /// The wsaservic e_ no t_ found.
        /// </summary>
        WSASERVICE_NOT_FOUND = 10108, 

        /// <summary>
        /// The wsatyp e_ no t_ found.
        /// </summary>
        WSATYPE_NOT_FOUND = 10109, 

        /// <summary>
        /// The ws a_ e_ n o_ more.
        /// </summary>
        WSA_E_NO_MORE = 10110, 

        /// <summary>
        /// The ws a_ e_ cancelled.
        /// </summary>
        WSA_E_CANCELLED = 10111, 

        /// <summary>
        /// The wsaerefused.
        /// </summary>
        WSAEREFUSED = 10112, 

        /// <summary>
        /// The wsahos t_ no t_ found.
        /// </summary>
        WSAHOST_NOT_FOUND = 11001, 

        /// <summary>
        /// The wsatr y_ again.
        /// </summary>
        WSATRY_AGAIN = 11002, 

        /// <summary>
        /// The wsan o_ recovery.
        /// </summary>
        WSANO_RECOVERY = 11003, 

        /// <summary>
        /// The wsan o_ data.
        /// </summary>
        WSANO_DATA = 11004, 

        /// <summary>
        /// The ws a_ qo s_ receivers.
        /// </summary>
        WSA_QOS_RECEIVERS = 11005, 

        /// <summary>
        /// The ws a_ qo s_ senders.
        /// </summary>
        WSA_QOS_SENDERS = 11006, 

        /// <summary>
        /// The ws a_ qo s_ n o_ senders.
        /// </summary>
        WSA_QOS_NO_SENDERS = 11007, 

        /// <summary>
        /// The ws a_ qo s_ n o_ receivers.
        /// </summary>
        WSA_QOS_NO_RECEIVERS = 11008, 

        /// <summary>
        /// The ws a_ qo s_ reques t_ confirmed.
        /// </summary>
        WSA_QOS_REQUEST_CONFIRMED = 11009, 

        /// <summary>
        /// The ws a_ qo s_ admissio n_ failure.
        /// </summary>
        WSA_QOS_ADMISSION_FAILURE = 11010, 

        /// <summary>
        /// The ws a_ qo s_ polic y_ failure.
        /// </summary>
        WSA_QOS_POLICY_FAILURE = 11011, 

        /// <summary>
        /// The ws a_ qo s_ ba d_ style.
        /// </summary>
        WSA_QOS_BAD_STYLE = 11012, 

        /// <summary>
        /// The ws a_ qo s_ ba d_ object.
        /// </summary>
        WSA_QOS_BAD_OBJECT = 11013, 

        /// <summary>
        /// The ws a_ qo s_ traffi c_ ctr l_ error.
        /// </summary>
        WSA_QOS_TRAFFIC_CTRL_ERROR = 11014, 

        /// <summary>
        /// The ws a_ qo s_ generi c_ error.
        /// </summary>
        WSA_QOS_GENERIC_ERROR = 11015, 

        /// <summary>
        /// The ws a_ qo s_ eservicetype.
        /// </summary>
        WSA_QOS_ESERVICETYPE = 11016, 

        /// <summary>
        /// The ws a_ qo s_ eflowspec.
        /// </summary>
        WSA_QOS_EFLOWSPEC = 11017, 

        /// <summary>
        /// The ws a_ qo s_ eprovspecbuf.
        /// </summary>
        WSA_QOS_EPROVSPECBUF = 11018, 

        /// <summary>
        /// The ws a_ qo s_ efilterstyle.
        /// </summary>
        WSA_QOS_EFILTERSTYLE = 11019, 

        /// <summary>
        /// The ws a_ qo s_ efiltertype.
        /// </summary>
        WSA_QOS_EFILTERTYPE = 11020, 

        /// <summary>
        /// The ws a_ qo s_ efiltercount.
        /// </summary>
        WSA_QOS_EFILTERCOUNT = 11021, 

        /// <summary>
        /// The ws a_ qo s_ eobjlength.
        /// </summary>
        WSA_QOS_EOBJLENGTH = 11022, 

        /// <summary>
        /// The ws a_ qo s_ eflowcount.
        /// </summary>
        WSA_QOS_EFLOWCOUNT = 11023, 

        /// <summary>
        /// The ws a_ qo s_ eunknownpsobj.
        /// </summary>
        WSA_QOS_EUNKNOWNPSOBJ = 11024, 

        /// <summary>
        /// The ws a_ qo s_ epolicyobj.
        /// </summary>
        WSA_QOS_EPOLICYOBJ = 11025, 

        /// <summary>
        /// The ws a_ qo s_ eflowdesc.
        /// </summary>
        WSA_QOS_EFLOWDESC = 11026, 

        /// <summary>
        /// The ws a_ qo s_ epsflowspec.
        /// </summary>
        WSA_QOS_EPSFLOWSPEC = 11027, 

        /// <summary>
        /// The ws a_ qo s_ epsfilterspec.
        /// </summary>
        WSA_QOS_EPSFILTERSPEC = 11028, 

        /// <summary>
        /// The ws a_ qo s_ esdmodeobj.
        /// </summary>
        WSA_QOS_ESDMODEOBJ = 11029, 

        /// <summary>
        /// The ws a_ qo s_ eshaperateobj.
        /// </summary>
        WSA_QOS_ESHAPERATEOBJ = 11030, 

        /// <summary>
        /// The ws a_ qo s_ reserve d_ petype.
        /// </summary>
        WSA_QOS_RESERVED_PETYPE = 11031, 

        /// <summary>
        /// The erro r_ ipse c_ q m_ polic y_ exists.
        /// </summary>
        ERROR_IPSEC_QM_POLICY_EXISTS = 13000, 

        /// <summary>
        /// The erro r_ ipse c_ q m_ polic y_ no t_ found.
        /// </summary>
        ERROR_IPSEC_QM_POLICY_NOT_FOUND = 13001, 

        /// <summary>
        /// The erro r_ ipse c_ q m_ polic y_ i n_ use.
        /// </summary>
        ERROR_IPSEC_QM_POLICY_IN_USE = 13002, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ polic y_ exists.
        /// </summary>
        ERROR_IPSEC_MM_POLICY_EXISTS = 13003, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ polic y_ no t_ found.
        /// </summary>
        ERROR_IPSEC_MM_POLICY_NOT_FOUND = 13004, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ polic y_ i n_ use.
        /// </summary>
        ERROR_IPSEC_MM_POLICY_IN_USE = 13005, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ filte r_ exists.
        /// </summary>
        ERROR_IPSEC_MM_FILTER_EXISTS = 13006, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ filte r_ no t_ found.
        /// </summary>
        ERROR_IPSEC_MM_FILTER_NOT_FOUND = 13007, 

        /// <summary>
        /// The erro r_ ipse c_ transpor t_ filte r_ exists.
        /// </summary>
        ERROR_IPSEC_TRANSPORT_FILTER_EXISTS = 13008, 

        /// <summary>
        /// The erro r_ ipse c_ transpor t_ filte r_ no t_ found.
        /// </summary>
        ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND = 13009, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ aut h_ exists.
        /// </summary>
        ERROR_IPSEC_MM_AUTH_EXISTS = 13010, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ aut h_ no t_ found.
        /// </summary>
        ERROR_IPSEC_MM_AUTH_NOT_FOUND = 13011, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ aut h_ i n_ use.
        /// </summary>
        ERROR_IPSEC_MM_AUTH_IN_USE = 13012, 

        /// <summary>
        /// The erro r_ ipse c_ defaul t_ m m_ polic y_ no t_ found.
        /// </summary>
        ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND = 13013, 

        /// <summary>
        /// The erro r_ ipse c_ defaul t_ m m_ aut h_ no t_ found.
        /// </summary>
        ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND = 13014, 

        /// <summary>
        /// The erro r_ ipse c_ defaul t_ q m_ polic y_ no t_ found.
        /// </summary>
        ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND = 13015, 

        /// <summary>
        /// The erro r_ ipse c_ tunne l_ filte r_ exists.
        /// </summary>
        ERROR_IPSEC_TUNNEL_FILTER_EXISTS = 13016, 

        /// <summary>
        /// The erro r_ ipse c_ tunne l_ filte r_ no t_ found.
        /// </summary>
        ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND = 13017, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ filte r_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_MM_FILTER_PENDING_DELETION = 13018, 

        /// <summary>
        /// The erro r_ ipse c_ transpor t_ filte r_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION = 13019, 

        /// <summary>
        /// The erro r_ ipse c_ tunne l_ filte r_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION = 13020, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ polic y_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_MM_POLICY_PENDING_DELETION = 13021, 

        /// <summary>
        /// The erro r_ ipse c_ m m_ aut h_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_MM_AUTH_PENDING_DELETION = 13022, 

        /// <summary>
        /// The erro r_ ipse c_ q m_ polic y_ pendin g_ deletion.
        /// </summary>
        ERROR_IPSEC_QM_POLICY_PENDING_DELETION = 13023, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ aut h_ fail.
        /// </summary>
        ERROR_IPSEC_IKE_AUTH_FAIL = 13801, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ attri b_ fail.
        /// </summary>
        ERROR_IPSEC_IKE_ATTRIB_FAIL = 13802, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ negotiatio n_ pending.
        /// </summary>
        ERROR_IPSEC_IKE_NEGOTIATION_PENDING = 13803, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ genera l_ processin g_ error.
        /// </summary>
        ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR = 13804, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ time d_ out.
        /// </summary>
        ERROR_IPSEC_IKE_TIMED_OUT = 13805, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ cert.
        /// </summary>
        ERROR_IPSEC_IKE_NO_CERT = 13806, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ s a_ deleted.
        /// </summary>
        ERROR_IPSEC_IKE_SA_DELETED = 13807, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ s a_ reaped.
        /// </summary>
        ERROR_IPSEC_IKE_SA_REAPED = 13808, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ m m_ acquir e_ drop.
        /// </summary>
        ERROR_IPSEC_IKE_MM_ACQUIRE_DROP = 13809, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ q m_ acquir e_ drop.
        /// </summary>
        ERROR_IPSEC_IKE_QM_ACQUIRE_DROP = 13810, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ queu e_ dro p_ mm.
        /// </summary>
        ERROR_IPSEC_IKE_QUEUE_DROP_MM = 13811, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ queu e_ dro p_ n o_ mm.
        /// </summary>
        ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM = 13812, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ dro p_ n o_ response.
        /// </summary>
        ERROR_IPSEC_IKE_DROP_NO_RESPONSE = 13813, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ m m_ dela y_ drop.
        /// </summary>
        ERROR_IPSEC_IKE_MM_DELAY_DROP = 13814, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ q m_ dela y_ drop.
        /// </summary>
        ERROR_IPSEC_IKE_QM_DELAY_DROP = 13815, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ error.
        /// </summary>
        ERROR_IPSEC_IKE_ERROR = 13816, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ cr l_ failed.
        /// </summary>
        ERROR_IPSEC_IKE_CRL_FAILED = 13817, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ ke y_ usage.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_KEY_USAGE = 13818, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ cer t_ type.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_CERT_TYPE = 13819, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ privat e_ key.
        /// </summary>
        ERROR_IPSEC_IKE_NO_PRIVATE_KEY = 13820, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ d h_ fail.
        /// </summary>
        ERROR_IPSEC_IKE_DH_FAIL = 13822, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ header.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_HEADER = 13824, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ policy.
        /// </summary>
        ERROR_IPSEC_IKE_NO_POLICY = 13825, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ signature.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_SIGNATURE = 13826, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ kerbero s_ error.
        /// </summary>
        ERROR_IPSEC_IKE_KERBEROS_ERROR = 13827, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ publi c_ key.
        /// </summary>
        ERROR_IPSEC_IKE_NO_PUBLIC_KEY = 13828, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ err.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR = 13829, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ sa.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_SA = 13830, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ prop.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_PROP = 13831, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ trans.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_TRANS = 13832, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ ke.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_KE = 13833, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ id.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_ID = 13834, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ cert.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_CERT = 13835, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ cer t_ req.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ = 13836, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ hash.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_HASH = 13837, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ sig.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_SIG = 13838, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ nonce.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_NONCE = 13839, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ notify.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY = 13840, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ delete.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_DELETE = 13841, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ proces s_ er r_ vendor.
        /// </summary>
        ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR = 13842, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ payload.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_PAYLOAD = 13843, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ loa d_ sof t_ sa.
        /// </summary>
        ERROR_IPSEC_IKE_LOAD_SOFT_SA = 13844, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ sof t_ s a_ tor n_ down.
        /// </summary>
        ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN = 13845, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ cookie.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_COOKIE = 13846, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ pee r_ cert.
        /// </summary>
        ERROR_IPSEC_IKE_NO_PEER_CERT = 13847, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ pee r_ cr l_ failed.
        /// </summary>
        ERROR_IPSEC_IKE_PEER_CRL_FAILED = 13848, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ polic y_ change.
        /// </summary>
        ERROR_IPSEC_IKE_POLICY_CHANGE = 13849, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ n o_ m m_ policy.
        /// </summary>
        ERROR_IPSEC_IKE_NO_MM_POLICY = 13850, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ notcbpriv.
        /// </summary>
        ERROR_IPSEC_IKE_NOTCBPRIV = 13851, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ secloadfail.
        /// </summary>
        ERROR_IPSEC_IKE_SECLOADFAIL = 13852, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ failsspinit.
        /// </summary>
        ERROR_IPSEC_IKE_FAILSSPINIT = 13853, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ failqueryssp.
        /// </summary>
        ERROR_IPSEC_IKE_FAILQUERYSSP = 13854, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ srvacqfail.
        /// </summary>
        ERROR_IPSEC_IKE_SRVACQFAIL = 13855, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ srvquerycred.
        /// </summary>
        ERROR_IPSEC_IKE_SRVQUERYCRED = 13856, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ getspifail.
        /// </summary>
        ERROR_IPSEC_IKE_GETSPIFAIL = 13857, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ filter.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_FILTER = 13858, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ ou t_ o f_ memory.
        /// </summary>
        ERROR_IPSEC_IKE_OUT_OF_MEMORY = 13859, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ ad d_ updat e_ ke y_ failed.
        /// </summary>
        ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED = 13860, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ policy.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_POLICY = 13861, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ unknow n_ doi.
        /// </summary>
        ERROR_IPSEC_IKE_UNKNOWN_DOI = 13862, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ situation.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_SITUATION = 13863, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ d h_ failure.
        /// </summary>
        ERROR_IPSEC_IKE_DH_FAILURE = 13864, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ group.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_GROUP = 13865, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ encrypt.
        /// </summary>
        ERROR_IPSEC_IKE_ENCRYPT = 13866, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ decrypt.
        /// </summary>
        ERROR_IPSEC_IKE_DECRYPT = 13867, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ polic y_ match.
        /// </summary>
        ERROR_IPSEC_IKE_POLICY_MATCH = 13868, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ unsupporte d_ id.
        /// </summary>
        ERROR_IPSEC_IKE_UNSUPPORTED_ID = 13869, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ hash.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_HASH = 13870, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ has h_ alg.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_HASH_ALG = 13871, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ has h_ size.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_HASH_SIZE = 13872, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ encryp t_ alg.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG = 13873, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ aut h_ alg.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_AUTH_ALG = 13874, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ sig.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_SIG = 13875, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ loa d_ failed.
        /// </summary>
        ERROR_IPSEC_IKE_LOAD_FAILED = 13876, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ rp c_ delete.
        /// </summary>
        ERROR_IPSEC_IKE_RPC_DELETE = 13877, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ benig n_ reinit.
        /// </summary>
        ERROR_IPSEC_IKE_BENIGN_REINIT = 13878, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ responde r_ lifetim e_ notify.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY = 13879, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ invali d_ cer t_ keylen.
        /// </summary>
        ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN = 13881, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ m m_ limit.
        /// </summary>
        ERROR_IPSEC_IKE_MM_LIMIT = 13882, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ negotiatio n_ disabled.
        /// </summary>
        ERROR_IPSEC_IKE_NEGOTIATION_DISABLED = 13883, 

        /// <summary>
        /// The erro r_ ipse c_ ik e_ ne g_ statu s_ end.
        /// </summary>
        ERROR_IPSEC_IKE_NEG_STATUS_END = 13884, 

        /// <summary>
        /// The erro r_ sx s_ sectio n_ no t_ found.
        /// </summary>
        ERROR_SXS_SECTION_NOT_FOUND = 14000, 

        /// <summary>
        /// The erro r_ sx s_ can t_ ge n_ actctx.
        /// </summary>
        ERROR_SXS_CANT_GEN_ACTCTX = 14001, 

        /// <summary>
        /// The erro r_ sx s_ invali d_ actctxdat a_ format.
        /// </summary>
        ERROR_SXS_INVALID_ACTCTXDATA_FORMAT = 14002, 

        /// <summary>
        /// The erro r_ sx s_ assembl y_ no t_ found.
        /// </summary>
        ERROR_SXS_ASSEMBLY_NOT_FOUND = 14003, 

        /// <summary>
        /// The erro r_ sx s_ manifes t_ forma t_ error.
        /// </summary>
        ERROR_SXS_MANIFEST_FORMAT_ERROR = 14004, 

        /// <summary>
        /// The erro r_ sx s_ manifes t_ pars e_ error.
        /// </summary>
        ERROR_SXS_MANIFEST_PARSE_ERROR = 14005, 

        /// <summary>
        /// The erro r_ sx s_ activatio n_ contex t_ disabled.
        /// </summary>
        ERROR_SXS_ACTIVATION_CONTEXT_DISABLED = 14006, 

        /// <summary>
        /// The erro r_ sx s_ ke y_ no t_ found.
        /// </summary>
        ERROR_SXS_KEY_NOT_FOUND = 14007, 

        /// <summary>
        /// The erro r_ sx s_ versio n_ conflict.
        /// </summary>
        ERROR_SXS_VERSION_CONFLICT = 14008, 

        /// <summary>
        /// The erro r_ sx s_ wron g_ sectio n_ type.
        /// </summary>
        ERROR_SXS_WRONG_SECTION_TYPE = 14009, 

        /// <summary>
        /// The erro r_ sx s_ threa d_ querie s_ disabled.
        /// </summary>
        ERROR_SXS_THREAD_QUERIES_DISABLED = 14010, 

        /// <summary>
        /// The erro r_ sx s_ proces s_ defaul t_ alread y_ set.
        /// </summary>
        ERROR_SXS_PROCESS_DEFAULT_ALREADY_SET = 14011, 

        /// <summary>
        /// The erro r_ sx s_ unknow n_ encodin g_ group.
        /// </summary>
        ERROR_SXS_UNKNOWN_ENCODING_GROUP = 14012, 

        /// <summary>
        /// The erro r_ sx s_ unknow n_ encoding.
        /// </summary>
        ERROR_SXS_UNKNOWN_ENCODING = 14013, 

        /// <summary>
        /// The erro r_ sx s_ invali d_ xm l_ namespac e_ uri.
        /// </summary>
        ERROR_SXS_INVALID_XML_NAMESPACE_URI = 14014, 

        /// <summary>
        /// The erro r_ sx s_ roo t_ manifes t_ dependenc y_ no t_ installed.
        /// </summary>
        ERROR_SXS_ROOT_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14015, 

        /// <summary>
        /// The erro r_ sx s_ lea f_ manifes t_ dependenc y_ no t_ installed.
        /// </summary>
        ERROR_SXS_LEAF_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14016, 

        /// <summary>
        /// The erro r_ sx s_ invali d_ assembl y_ identit y_ attribute.
        /// </summary>
        ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE = 14017, 

        /// <summary>
        /// The erro r_ sx s_ manifes t_ missin g_ require d_ defaul t_ namespace.
        /// </summary>
        ERROR_SXS_MANIFEST_MISSING_REQUIRED_DEFAULT_NAMESPACE = 14018, 

        /// <summary>
        /// The erro r_ sx s_ manifes t_ invali d_ require d_ defaul t_ namespace.
        /// </summary>
        ERROR_SXS_MANIFEST_INVALID_REQUIRED_DEFAULT_NAMESPACE = 14019, 

        /// <summary>
        /// The erro r_ sx s_ privat e_ manifes t_ cros s_ pat h_ wit h_ repars e_ point.
        /// </summary>
        ERROR_SXS_PRIVATE_MANIFEST_CROSS_PATH_WITH_REPARSE_POINT = 14020, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ dl l_ name.
        /// </summary>
        ERROR_SXS_DUPLICATE_DLL_NAME = 14021, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ windowclas s_ name.
        /// </summary>
        ERROR_SXS_DUPLICATE_WINDOWCLASS_NAME = 14022, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ clsid.
        /// </summary>
        ERROR_SXS_DUPLICATE_CLSID = 14023, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ iid.
        /// </summary>
        ERROR_SXS_DUPLICATE_IID = 14024, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ tlbid.
        /// </summary>
        ERROR_SXS_DUPLICATE_TLBID = 14025, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ progid.
        /// </summary>
        ERROR_SXS_DUPLICATE_PROGID = 14026, 

        /// <summary>
        /// The erro r_ sx s_ duplicat e_ assembl y_ name.
        /// </summary>
        ERROR_SXS_DUPLICATE_ASSEMBLY_NAME = 14027, 

        /// <summary>
        /// The erro r_ sx s_ fil e_ has h_ mismatch.
        /// </summary>
        ERROR_SXS_FILE_HASH_MISMATCH = 14028, 

        /// <summary>
        /// The erro r_ sx s_ polic y_ pars e_ error.
        /// </summary>
        ERROR_SXS_POLICY_PARSE_ERROR = 14029, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missingquote.
        /// </summary>
        ERROR_SXS_XML_E_MISSINGQUOTE = 14030, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ commentsyntax.
        /// </summary>
        ERROR_SXS_XML_E_COMMENTSYNTAX = 14031, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badstartnamechar.
        /// </summary>
        ERROR_SXS_XML_E_BADSTARTNAMECHAR = 14032, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badnamechar.
        /// </summary>
        ERROR_SXS_XML_E_BADNAMECHAR = 14033, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badcharinstring.
        /// </summary>
        ERROR_SXS_XML_E_BADCHARINSTRING = 14034, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ xmldeclsyntax.
        /// </summary>
        ERROR_SXS_XML_E_XMLDECLSYNTAX = 14035, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badchardata.
        /// </summary>
        ERROR_SXS_XML_E_BADCHARDATA = 14036, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missingwhitespace.
        /// </summary>
        ERROR_SXS_XML_E_MISSINGWHITESPACE = 14037, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ expectingtagend.
        /// </summary>
        ERROR_SXS_XML_E_EXPECTINGTAGEND = 14038, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missingsemicolon.
        /// </summary>
        ERROR_SXS_XML_E_MISSINGSEMICOLON = 14039, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unbalancedparen.
        /// </summary>
        ERROR_SXS_XML_E_UNBALANCEDPAREN = 14040, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ internalerror.
        /// </summary>
        ERROR_SXS_XML_E_INTERNALERROR = 14041, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unexpecte d_ whitespace.
        /// </summary>
        ERROR_SXS_XML_E_UNEXPECTED_WHITESPACE = 14042, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ incomplet e_ encoding.
        /// </summary>
        ERROR_SXS_XML_E_INCOMPLETE_ENCODING = 14043, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missin g_ paren.
        /// </summary>
        ERROR_SXS_XML_E_MISSING_PAREN = 14044, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ expectingclosequote.
        /// </summary>
        ERROR_SXS_XML_E_EXPECTINGCLOSEQUOTE = 14045, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ multipl e_ colons.
        /// </summary>
        ERROR_SXS_XML_E_MULTIPLE_COLONS = 14046, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invali d_ decimal.
        /// </summary>
        ERROR_SXS_XML_E_INVALID_DECIMAL = 14047, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invali d_ hexidecimal.
        /// </summary>
        ERROR_SXS_XML_E_INVALID_HEXIDECIMAL = 14048, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invali d_ unicode.
        /// </summary>
        ERROR_SXS_XML_E_INVALID_UNICODE = 14049, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ whitespaceorquestionmark.
        /// </summary>
        ERROR_SXS_XML_E_WHITESPACEORQUESTIONMARK = 14050, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unexpectedendtag.
        /// </summary>
        ERROR_SXS_XML_E_UNEXPECTEDENDTAG = 14051, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedtag.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDTAG = 14052, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ duplicateattribute.
        /// </summary>
        ERROR_SXS_XML_E_DUPLICATEATTRIBUTE = 14053, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ multipleroots.
        /// </summary>
        ERROR_SXS_XML_E_MULTIPLEROOTS = 14054, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invalidatrootlevel.
        /// </summary>
        ERROR_SXS_XML_E_INVALIDATROOTLEVEL = 14055, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badxmldecl.
        /// </summary>
        ERROR_SXS_XML_E_BADXMLDECL = 14056, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missingroot.
        /// </summary>
        ERROR_SXS_XML_E_MISSINGROOT = 14057, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unexpectedeof.
        /// </summary>
        ERROR_SXS_XML_E_UNEXPECTEDEOF = 14058, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badperefinsubset.
        /// </summary>
        ERROR_SXS_XML_E_BADPEREFINSUBSET = 14059, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedstarttag.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDSTARTTAG = 14060, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedendtag.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDENDTAG = 14061, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedstring.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDSTRING = 14062, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedcomment.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDCOMMENT = 14063, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ uncloseddecl.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDDECL = 14064, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unclosedcdata.
        /// </summary>
        ERROR_SXS_XML_E_UNCLOSEDCDATA = 14065, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ reservednamespace.
        /// </summary>
        ERROR_SXS_XML_E_RESERVEDNAMESPACE = 14066, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invalidencoding.
        /// </summary>
        ERROR_SXS_XML_E_INVALIDENCODING = 14067, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invalidswitch.
        /// </summary>
        ERROR_SXS_XML_E_INVALIDSWITCH = 14068, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ badxmlcase.
        /// </summary>
        ERROR_SXS_XML_E_BADXMLCASE = 14069, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invali d_ standalone.
        /// </summary>
        ERROR_SXS_XML_E_INVALID_STANDALONE = 14070, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ unexpecte d_ standalone.
        /// </summary>
        ERROR_SXS_XML_E_UNEXPECTED_STANDALONE = 14071, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ invali d_ version.
        /// </summary>
        ERROR_SXS_XML_E_INVALID_VERSION = 14072, 

        /// <summary>
        /// The erro r_ sx s_ xm l_ e_ missingequals.
        /// </summary>
        ERROR_SXS_XML_E_MISSINGEQUALS = 14073, 

        /// <summary>
        /// The erro r_ sx s_ protectio n_ recover y_ failed.
        /// </summary>
        ERROR_SXS_PROTECTION_RECOVERY_FAILED = 14074, 

        /// <summary>
        /// The erro r_ sx s_ protectio n_ publi c_ ke y_ to o_ short.
        /// </summary>
        ERROR_SXS_PROTECTION_PUBLIC_KEY_TOO_SHORT = 14075, 

        /// <summary>
        /// The erro r_ sx s_ protectio n_ catalo g_ no t_ valid.
        /// </summary>
        ERROR_SXS_PROTECTION_CATALOG_NOT_VALID = 14076, 

        /// <summary>
        /// The erro r_ sx s_ untranslatabl e_ hresult.
        /// </summary>
        ERROR_SXS_UNTRANSLATABLE_HRESULT = 14077, 

        /// <summary>
        /// The erro r_ sx s_ protectio n_ catalo g_ fil e_ missing.
        /// </summary>
        ERROR_SXS_PROTECTION_CATALOG_FILE_MISSING = 14078, 

        /// <summary>
        /// The erro r_ sx s_ missin g_ assembl y_ identit y_ attribute.
        /// </summary>
        ERROR_SXS_MISSING_ASSEMBLY_IDENTITY_ATTRIBUTE = 14079, 

        /// <summary>
        /// The erro r_ sx s_ invali d_ assembl y_ identit y_ attribut e_ name.
        /// </summary>
        ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE_NAME = 14080
    }
}