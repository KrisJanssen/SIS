//#ifndef TILLLSM_H_INCLUDED
//#define TILLLSM_H_INCLUDED
//
//#ifdef __cplusplus
//extern "C"
//{
//#endif
///** @mainpage TILL Photonics LSM device
//  *  
//  *  @htmlonly <div align="center"><table width="80%" border="0"><tr><td> @endhtmlonly
//  *  @par
//  *
//  *  This reference 
//  *  documentation describes the C Application Programming Interface (API) of the LSM device in detail.
//  *  @htmlonly </td></tr></table></div> @endhtmlonly
//  */
//
///** @defgroup lsm Laser Scanning Microscopy (LSM) device
// *
// *  This module holds functions to control the TILL Photonics LSM device.
// *
// *  To control a additional Polytrope, see the @ref LSM_SetGalvoRawPosition1.
// *
// *  @section error Error Codes
// *  All functions return an integer error code that can be interpreted as follows:
// * 
// *  <table border="1" width="50%">
// *  <tr><th>Error Code</th><th>Enumeration</th><th>Interpretation</th></tr>
// *  <tr><td>0</td><td>LSM_ErrorNone</td><td>no error</td></tr>
// *  <tr><td>1</td><td>LSM_ErrorUnknown</td><td>unknown error</td></tr>
// *  <tr><td>2</td><td>LSM_ErrorNotImplemented</td><td>method not implemented yet</td></tr>
// *  <tr><td>30</td><td>LSM_ErrorInvalidArg1</td><td>invalid first argument</td></tr>
// *  <tr><td>31</td><td>LSM_ErrorInvalidArg2</td><td>invalid second argument</td></tr>
// *  <tr><td>32</td><td>LSM_ErrorInvalidArg3</td><td>invalid third argument</td></tr>
// *  <tr><td>33</td><td>LSM_ErrorInvalidArg4</td><td>invalid fourth argument</td></tr>
// *  <tr><td>34</td><td>LSM_ErrorInvalidArg5</td><td>invalid fifth argument</td></tr>
// *  <tr><td>35</td><td>LSM_ErrorInvalidArg6</td><td>invalid sixth argument</td></tr>
// *  <tr><td>36</td><td>LSM_ErrorInvalidArg7</td><td>invalid seventh argument</td></tr>
// *  <tr><td>37</td><td>LSM_ErrorInvalidArg8</td><td>invalid eighth argument</td></tr>
// *  <tr><td>100</td><td>LSM_ErrorWrongHandle</td><td>invalid LSM handle</td></tr>
// *  <tr><td>102</td><td>LSM_ErrorOpenComPortFailed</td><td>error opening the COM port</td></tr>
// *  <tr><td>100000</td><td>LSM_ErrorNotCalibrated</td><td>the LSM is not calibrated</td></tr>
// *  <tr><td>100001</td><td>LSM_ErrorClearingProtocol</td><td>failed to clear the scan protocol from the LSM</td></tr>
// *  <tr><td>100002</td><td>LSM_ErrorSetProtocol</td><td>failed to set a new scan protocol</td></tr>
// *  <tr><td>100003</td><td>LSM_ErrorExecProtocol</td><td>failed to start the execution of the scan protocol</td></tr>
// *  <tr><td>100004</td><td>LSM_ErrorAbortProtocol</td><td>failed to abort the scan protocol</td></tr>
// *  <tr><td>100005</td><td>LSM_ErrorCurveCalcFailed</td><td>unable to determine the blanking time between lines</td></tr>
// *  <tr><td>100006</td><td>LSM_ErrorProtNotSupported</td><td>protocol is not supported</td></tr>
// *  <tr><td>100007</td><td>LSM_ErrorProtCalculation</td><td>error creating the protocol</td></tr>
// *  <tr><td>100008</td><td>LSM_ErrorProtGeometry</td><td>error calculating intersections between scan lines and region to scan</td></tr>
// *  <tr><td>100009</td><td>LSM_ErrorWrongCommandCount</td><td>number of created commands for a protocol is not as expected</td></tr>
// *  </table>
// *
// */
//
//
//    /**@name LSM Coordinate
//    *
//    */
//    ///@{ @ingroup lsm
//    ///@struct LSM_Coordinate 
//    /// Double precision coordinate
//    ///
//    struct LSM_Coordinate
//    {
//        double X;///< The X coordinate
//        double Y;///< The Y coordinate
//    };
//    ///@}
//
//    /**@name ProtocolDone Callback
//    *
//    */
//    ///@{ @ingroup lsm
//    ///@typedef ProtocolDoneCb 
//    /// Callback prototype to signal protocol end
//    ///
//    typedef void (ProtocolDoneCb) (void* context);
//    ///@}
//
///**@name LSM Control
//*
//*/
/////@{ @ingroup lsm
//
//    int LSM_Abort(void* handle);
//    int LSM_AddCalibrationPoint(void* handle, LSM_Coordinate pixCoordinate, LSM_Coordinate galvoCoordinate);
//    int LSM_AddEndCallback(void* handle, ProtocolDoneCb* pdCb, void* context);
//    int LSM_AddLine(void* handle, void** protH, LSM_Coordinate point1, LSM_Coordinate point2, bool bidirectional, double* tNet, double* tGross, unsigned int loops);
//    int LSM_AddParallelogram(void* handle, void** protH, 
//                            LSM_Coordinate start, 
//                            LSM_Coordinate row,
//                            LSM_Coordinate column, 
//                            unsigned int lines, double* tNet, double* tGross, unsigned int loops);
//
//    int LSM_AddPoint(void* handle, void** protH, LSM_Coordinate point, double* tNet, double* tGross, unsigned int loops, double loopCycle);
//    int LSM_AddPolygon(void* handle, void** protH, LSM_Coordinate* points, unsigned int nbPoints,
//                       double alpha,
//                       bool counterclockwise,
//                       unsigned int lines, double* tNet, double* tGross, unsigned int loops);
//
//    int LSM_Close(void* handle);
//    
//    int LSM_ConfigEndTrigger(void* handle, int device, int trigger, bool returnToRest);
//    int LSM_ConfigLaserTrigger(void* handle, int device, int trigger);
//    int LSM_ConfigTriggerIn(void* handle, int trigger);
//    int LSM_CreateProtocol(void* handle, void** protH);
//    int LSM_DeleteProtocol(void* handle, void* protH);
//
//    int LSM_Execute(void* handle);
//    int LSM_GetBoundingBox(double alpha, 
//                           bool counterclockwise, 
//                           LSM_Coordinate* points,
//                           int nbPoints,
//                           LSM_Coordinate* start,
//                           LSM_Coordinate* row,
//                           LSM_Coordinate* column);
//    int LSM_GetConfigEndTrigger(void* handle, int* device, int* trigger, bool* returnToRest);
//    int LSM_GetConfigLaserTrigger(void* handle, int* device, int* trigger);
//    int LSM_GetConfigTriggerIn(void* handle, int* trigger);
//    int LSM_GetRealShape(void* handle, void* protH, int shapeIndex, LSM_Coordinate* points, int* length);
//    int LSM_GetRestPosition(void* handle, LSM_Coordinate* galvoPos);
//    int LSM_IsCalibrated(void* handle, bool* calibrated);
//    int LSM_Load(void* handle, void* protH, int nbLoops);
//    int LSM_Open(const char* port, void** handle);
//    int LSM_Read(void* handle, void* protH, char* text, int* size);
//    int LSM_ResetCalibration(void* handle);
//    int LSM_SetGalvoRawPosition(void* handle, LSM_Coordinate galvoCoordinate);
//    int LSM_SetGalvoRawPosition1(void* handle, int index, double galvoPos);
//    int LSM_SetOffset(void* handle, LSM_Coordinate offset, bool withRestart);
//    int LSM_SetPoint(void* handle, LSM_Coordinate pixCoordinate);   
//    int LSM_SetRestPosition(void* handle, LSM_Coordinate galvoPos);
//    int LSM_ShootPoint(void* handle, LSM_Coordinate galvoPoint, double tNet, unsigned int count, double* cycleTime);
//    int LSM_GetCommandSetVersion(void* handle, int* commandSetVersion);
/////@}
//
//    ///@{ @ingroup lsm_intern
//    int LSM_HackSetSmartMove(double smart);
//    int LSM_LoadCommands(void* handle, char* text);
//    int LSM_ExtractProtocol(void* handle, void* protH, int* size, int* devices, double* vals, unsigned long* times);
//    int LSM_DirectSetOffset(void* handle, LSM_Coordinate offset);
//    ///@}
//
//    extern void init_callback( int id, char* str );
//
//
//#define    LSM_ErrorNone                 0
//#define    LSM_ErrorUnknown              1
//#define    LSM_ErrorNotImplemented       2
//
//#define    LSM_ErrorInvalidArg1         30
//#define    LSM_ErrorInvalidArg2         31
//#define    LSM_ErrorInvalidArg3         32
//#define    LSM_ErrorInvalidArg4         33
//#define    LSM_ErrorInvalidArg5         34
//#define    LSM_ErrorInvalidArg6         35
//#define    LSM_ErrorInvalidArg7         36
//#define    LSM_ErrorInvalidArg8         37
//
//#define    LSM_ErrorWrongHandle        100
//#define    LSM_ErrorNotInitialized     101
//#define    LSM_ErrorOpenComPortFailed  102
//#define    LSM_ErrorInitFailed         103
//#define    LSM_ErrorNoProperty         104
//
//#define    LSM_ErrorNotCalibrated     10000
//#define    LSM_ErrorClearingProtocol  10001
//#define    LSM_ErrorSetProtocol       10002
//#define    LSM_ErrorExecProtocol      10003
//#define    LSM_ErrorAbortProtocol     10004
//#define    LSM_ErrorCurveCalcFailed   10005
//#define    LSM_ErrorProtNotSupported  10006
//#define    LSM_ErrorProtCalculation   10007
//#define    LSM_ErrorProtGeometry      10008
//#define    LSM_ErrorWrongCommandCount 10009
//
//#ifdef __cplusplus
//}
//#endif
//
//
//#endif /*TILLLSM_H_INCLUDED*/
