using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace KUL.MDS.Hardware
{
    internal static class TillLSMDevice
    {
        internal const int LSM_ErrorNone = 0;
        internal const int LSM_ErrorUnknown = 1;
        internal const int LSM_ErrorNotImplemented = 2;

        internal const int LSM_ErrorInvalidArg1 = 30;
        internal const int LSM_ErrorInvalidArg2 = 31;
        internal const int LSM_ErrorInvalidArg3 = 32;
        internal const int LSM_ErrorInvalidArg4 = 33;
        internal const int LSM_ErrorInvalidArg5 = 34;
        internal const int LSM_ErrorInvalidArg6 = 35;
        internal const int LSM_ErrorInvalidArg7 = 36;
        internal const int LSM_ErrorInvalidArg8 = 37;

        internal const int LSM_ErrorWrongHandle = 100;
        internal const int LSM_ErrorNotInitialized = 101;
        internal const int LSM_ErrorOpenComPortFailed = 102;
        internal const int LSM_ErrorInitFailed = 103;
        internal const int LSM_ErrorNoProperty = 104;

        internal const int LSM_ErrorNotCalibrated = 10000;
        internal const int LSM_ErrorClearingProtocol = 10001;
        internal const int LSM_ErrorSetProtocol = 10002;
        internal const int LSM_ErrorExecProtocol = 10003;
        internal const int LSM_ErrorAbortProtocol = 10004;
        internal const int LSM_ErrorCurveCalcFailed = 10005;
        internal const int LSM_ErrorProtNotSupported = 10006;
        internal const int LSM_ErrorProtCalculation = 10007;
        internal const int LSM_ErrorProtGeometry = 10008;
        internal const int LSM_ErrorWrongCommandCount = 10009;

        [StructLayout(LayoutKind.Sequential)]
        public struct LSM_Coordinate
        {
            double X;///< The X coordinate
            double Y;///< The Y coordinate
        };

        public delegate void ProtocolDoneCb(System.IntPtr context);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Abort")]
        internal static extern int LSM_Abort(string sRegKeyName);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Abort")]
        internal static extern int LSM_Abort(IntPtr handle);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddCalibrationPoint")]
        internal static extern int LSM_AddCalibrationPoint(IntPtr handle, LSM_Coordinate pixCoordinate, LSM_Coordinate galvoCoordinate);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddEndCallback")]
        internal static extern int LSM_AddEndCallback(IntPtr handle, ref ProtocolDoneCb pdCb, IntPtr context);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddLine")]
        internal static extern int LSM_AddLine(IntPtr handle, ref IntPtr protH, LSM_Coordinate point1, LSM_Coordinate point2, bool bidirectional, ref double tNet, ref double tGross, uint loops);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddParallelogram")]
        internal static extern int LSM_AddParallelogram(
            IntPtr handle, 
            ref IntPtr protH, 
            LSM_Coordinate start, 
            LSM_Coordinate row, 
            LSM_Coordinate column, 
            uint lines, 
            ref double tNet, 
            ref double tGross, 
            uint loops);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddPoint")]
        internal static extern int LSM_AddPoint(IntPtr handle, ref IntPtr protH, LSM_Coordinate point, ref double tNet, ref double tGross, uint loops, double loopCycle);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddPolygon")]
        internal static extern int LSM_AddPolygon(IntPtr handle, ref IntPtr protH, ref LSM_Coordinate points, uint nbPoints,
            double alpha,
            bool counterclockwise,
            uint lines, ref double tNet, ref double tGross, uint loops);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Close")]
        internal static extern int LSM_Close(IntPtr handle);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigEndTrigger")]
        internal static extern int LSM_ConfigEndTrigger(IntPtr handle, int device, int trigger, bool returnToRest);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigLaserTrigger")]
        internal static extern int LSM_ConfigLaserTrigger(IntPtr handle, int device, int trigger);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigTriggerIn")]
        internal static extern int LSM_ConfigTriggerIn(IntPtr handle, int trigger);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_CreateProtocol")]
        internal static extern int LSM_CreateProtocol(IntPtr handle, ref IntPtr protH);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_DeleteProtocol")]
        internal static extern int LSM_DeleteProtocol(IntPtr handle, IntPtr protH);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Execute")]
        internal static extern int LSM_Execute(IntPtr handle);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetBoundingBox")]
        internal static extern int LSM_GetBoundingBox(double alpha,
            bool counterclockwise,
            ref LSM_Coordinate points,
            int nbPoints,
            ref LSM_Coordinate start,
            ref LSM_Coordinate row,
            ref LSM_Coordinate column);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigEndTrigger")]
        internal static extern int LSM_GetConfigEndTrigger(IntPtr handle, ref int device, ref int trigger, ref bool returnToRest);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigLaserTrigger")]
        internal static extern int LSM_GetConfigLaserTrigger(IntPtr handle, ref int device, ref int trigger);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigTriggerIn")]
        internal static extern int LSM_GetConfigTriggerIn(IntPtr handle, ref int trigger);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetRealShape")]
        internal static extern int LSM_GetRealShape(IntPtr handle, IntPtr protH, int shapeIndex, ref LSM_Coordinate points, ref int length);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetRestPosition")]
        internal static extern int LSM_GetRestPosition(IntPtr handle, ref LSM_Coordinate galvoPos);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_IsCalibrated")]
        internal static extern int LSM_IsCalibrated(IntPtr handle, ref bool calibrated);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Load")]
        internal static extern int LSM_Load(IntPtr handle, IntPtr protH, int nbLoops);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Open")]
        internal static extern int LSM_Open([System.Runtime.InteropServices.InAttribute()] string port, ref IntPtr handle);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Read")]
        internal static extern int LSM_Read(IntPtr handle, IntPtr protH, ref char text, ref int size);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ResetCalibration")]
        internal static extern int LSM_ResetCalibration(IntPtr handle);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetGalvoRawPosition")]
        internal static extern int LSM_SetGalvoRawPosition(IntPtr handle, LSM_Coordinate galvoCoordinate);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetGalvoRawPosition1")]
        internal static extern int LSM_SetGalvoRawPosition1(IntPtr handle, int index, double galvoPos);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetOffset")]
        internal static extern int LSM_SetOffset(IntPtr handle, LSM_Coordinate offset, bool withRestart);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetPoint")]
        internal static extern int LSM_SetPoint(IntPtr handle, LSM_Coordinate pixCoordinate);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetRestPosition")]
        internal static extern int LSM_SetRestPosition(IntPtr handle, LSM_Coordinate galvoPos);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Shootpoint")]
        internal static extern int LSM_ShootPoint(IntPtr handle, LSM_Coordinate galvoPoint, double tNet, uint count, ref double cycleTime);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetCommandSetVersion")]
        internal static extern int LSM_GetCommandSetVersion(IntPtr handle, ref int commandSetVersion);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_HackSetSmartMove")]
        internal static extern int LSM_HackSetSmartMove(double smart);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_LoadCommands")]
        internal static extern int LSM_LoadCommands(IntPtr handle, ref char text);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ExtractProtocol")]
        internal static extern int LSM_ExtractProtocol(IntPtr handle, IntPtr protH, ref int size, ref int devices, ref double vals, ref ulong times);
        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_DirectSetOffset")]
        internal static extern int LSM_DirectSetOffset(IntPtr handle, LSM_Coordinate offset);

        [DllImport("tillimic.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "init_callback")]
        public static extern void init_callback(int id, IntPtr str);

    }
}
