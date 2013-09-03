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
        internal const int LSM_ErrorUnknown                 =       1;
        internal const int LSM_ErrorNotImplemented          =       2;

        internal const int LSM_ErrorInvalidArg1             =       30;
        internal const int LSM_ErrorInvalidArg2             =       31;
        internal const int LSM_ErrorInvalidArg3             =       32;
        internal const int LSM_ErrorInvalidArg4             =       33;
        internal const int LSM_ErrorInvalidArg5             =       34;
        internal const int LSM_ErrorInvalidArg6             =       35;
        internal const int LSM_ErrorInvalidArg7             =       36;
        internal const int LSM_ErrorInvalidArg8             =       37;

        internal const int LSM_ErrorWrongHandle             =       100;
        internal const int LSM_ErrorNotInitialized          =       101;
        internal const int LSM_ErrorOpenComPortFailed       =       102;
        internal const int LSM_ErrorInitFailed              =       103;
        internal const int LSM_ErrorNoProperty              =       104;

        internal const int LSM_ErrorNotCalibrated           =       10000;
        internal const int LSM_ErrorClearingProtocol        =       10001;
        internal const int LSM_ErrorSetProtocol             =       10002;
        internal const int LSM_ErrorExecProtocol            =       10003;
        internal const int LSM_ErrorAbortProtocol           =       10004;
        internal const int LSM_ErrorCurveCalcFailed         =       10005;
        internal const int LSM_ErrorProtNotSupported        =       10006;
        internal const int LSM_ErrorProtCalculation         =       10007;
        internal const int LSM_ErrorProtGeometry            =       10008;
        internal const int LSM_ErrorWrongCommandCount       =       10009;
        internal const int LSM_ErrorNone                    =       0;
        internal const int LSM_ErrorUnknown                 =       1;
        internal const int LSM_ErrorNotImplemented          =       2;

        internal const int LSM_ErrorInvalidArg1             =       30;
        internal const int LSM_ErrorInvalidArg2             =       31;
        internal const int LSM_ErrorInvalidArg3             =       32;
        internal const int LSM_ErrorInvalidArg4             =       33;
        internal const int LSM_ErrorInvalidArg5             =       34;
        internal const int LSM_ErrorInvalidArg6             =       35;
        internal const int LSM_ErrorInvalidArg7             =       36;
        internal const int LSM_ErrorInvalidArg8             =       37;

        internal const int LSM_ErrorWrongHandle             =       100;
        internal const int LSM_ErrorNotInitialized          =       101;
        internal const int LSM_ErrorOpenComPortFailed       =       102;
        internal const int LSM_ErrorInitFailed              =       103;
        internal const int LSM_ErrorNoProperty              =       104;
        
        internal const int LSM_ErrorNotCalibrated           =       10000;
        internal const int LSM_ErrorClearingProtocol        =       10001;
        internal const int LSM_ErrorSetProtocol             =       10002;
        internal const int LSM_ErrorExecProtocol            =       10003;
        internal const int LSM_ErrorAbortProtocol           =       10004;
        internal const int LSM_ErrorCurveCalcFailed         =       10005;
        internal const int LSM_ErrorProtNotSupported        =       10006;
        internal const int LSM_ErrorProtCalculation         =       10007;
        internal const int LSM_ErrorProtGeometry            =       10008;
        internal const int LSM_ErrorWrongCommandCount       =       10009;
       


    /**@name LSM Coordinate
    *
    */
    ///@{ @ingroup lsm
    ///@struct LSM_Coordinate 
    /// Double precision coordinate
    ///
    struct LSM_Coordinate
    {
        double X;///< The X coordinate
        double Y;///< The Y coordinate
    };
    ///@}

    /**@name ProtocolDone Callback
    *
    */
    ///@{ @ingroup lsm
    ///@typedef ProtocolDoneCb 
    /// Callback prototype to signal protocol end
    ///
    typedef void (ProtocolDoneCb) (void* context);
    ///@}

/**@name LSM Control
*
*/
///@{ @ingroup lsm

    [DllImport("tillimic.dll", EntryPoint = "LSM_Abort")]		internal static extern int	LSM_Abort(string sRegKeyName);
    int LSM_Abort(void* handle);
    int LSM_AddCalibrationPoint(void* handle, LSM_Coordinate pixCoordinate, LSM_Coordinate galvoCoordinate);
    int LSM_AddEndCallback(void* handle, ProtocolDoneCb* pdCb, void* context);
    int LSM_AddLine(void* handle, void** protH, LSM_Coordinate point1, LSM_Coordinate point2, bool bidirectional, double* tNet, double* tGross, unsigned int loops);
    int LSM_AddParallelogram(void* handle, void** protH, 
                            LSM_Coordinate start, 
                            LSM_Coordinate row,
                            LSM_Coordinate column, 
                            unsigned int lines, double* tNet, double* tGross, unsigned int loops);

    int LSM_AddPoint(void* handle, void** protH, LSM_Coordinate point, double* tNet, double* tGross, unsigned int loops, double loopCycle);
    int LSM_AddPolygon(void* handle, void** protH, LSM_Coordinate* points, unsigned int nbPoints,
                       double alpha,
                       bool counterclockwise,
                       unsigned int lines, double* tNet, double* tGross, unsigned int loops);

    int LSM_Close(void* handle);
    
    int LSM_ConfigEndTrigger(void* handle, int device, int trigger, bool returnToRest);
    int LSM_ConfigLaserTrigger(void* handle, int device, int trigger);
    int LSM_ConfigTriggerIn(void* handle, int trigger);
    int LSM_CreateProtocol(void* handle, void** protH);
    int LSM_DeleteProtocol(void* handle, void* protH);

    int LSM_Execute(void* handle);
    int LSM_GetBoundingBox(double alpha, 
                           bool counterclockwise, 
                           LSM_Coordinate* points,
                           int nbPoints,
                           LSM_Coordinate* start,
                           LSM_Coordinate* row,
                           LSM_Coordinate* column);
    int LSM_GetConfigEndTrigger(void* handle, int* device, int* trigger, bool* returnToRest);
    int LSM_GetConfigLaserTrigger(void* handle, int* device, int* trigger);
    int LSM_GetConfigTriggerIn(void* handle, int* trigger);
    int LSM_GetRealShape(void* handle, void* protH, int shapeIndex, LSM_Coordinate* points, int* length);
    int LSM_GetRestPosition(void* handle, LSM_Coordinate* galvoPos);
    int LSM_IsCalibrated(void* handle, bool* calibrated);
    int LSM_Load(void* handle, void* protH, int nbLoops);
    int LSM_Open(const char* port, void** handle);
    int LSM_Read(void* handle, void* protH, char* text, int* size);
    int LSM_ResetCalibration(void* handle);
    int LSM_SetGalvoRawPosition(void* handle, LSM_Coordinate galvoCoordinate);
    int LSM_SetGalvoRawPosition1(void* handle, int index, double galvoPos);
    int LSM_SetOffset(void* handle, LSM_Coordinate offset, bool withRestart);
    int LSM_SetPoint(void* handle, LSM_Coordinate pixCoordinate);   
    int LSM_SetRestPosition(void* handle, LSM_Coordinate galvoPos);
    int LSM_ShootPoint(void* handle, LSM_Coordinate galvoPoint, double tNet, unsigned int count, double* cycleTime);
    int LSM_GetCommandSetVersion(void* handle, int* commandSetVersion);
///@}

    ///@{ @ingroup lsm_intern
    int LSM_HackSetSmartMove(double smart);
    int LSM_LoadCommands(void* handle, char* text);
    int LSM_ExtractProtocol(void* handle, void* protH, int* size, int* devices, double* vals, unsigned long* times);
    int LSM_DirectSetOffset(void* handle, LSM_Coordinate offset);
    ///@}

    extern void init_callback( int id, char* str );




#ifdef __cplusplus
}
#endif


#endif /*TILLLSM_H_INCLUDED*/

    }
}
