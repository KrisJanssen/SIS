namespace SIS.Hardware.YanusIV
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// This class provides the DLL functions for interacting with YanusIV galvo scanner.
    /// </summary>
    internal static class TillLSMDevice
    {
        ////////////////////////////////////////////////////////////
        // Till Photonics LSM device DLL functions and constants. //
        ////////////////////////////////////////////////////////////

        const string TillLibName = "tillimic.dll";  //the DLL library

        /// <summary>
        /// Error conditions returned by tillimic.dll
        /// </summary>
        public enum LSM_Error
        {
            None = 0,
            Unknown = 1,
            NotImplemented = 2,

            InvalidArg1 = 30,
            InvalidArg2 = 31,
            InvalidArg3 = 32,
            InvalidArg4 = 33,
            InvalidArg5 = 34,
            InvalidArg6 = 35,
            InvalidArg7 = 36,
            InvalidArg8 = 37,

            WrongHandle = 100,
            NotInitialized = 101,
            OpenComPortFailed = 102,
            InitFailed = 103,
            NoProperty = 104,

            NotCalibrated = 10000,
            ClearingProtocol = 10001,
            SetProtocol = 10002,
            ExecProtocol = 10003,
            AbortProtocol = 10004,
            CurveCalcFailed = 10005,
            ProtNotSupported = 10006,
            ProtCalculation = 10007,
            ProtGeometry = 10008,
            WrongCommandCount = 10009,
        }


        /// <summary>
        /// The following structures are used to hold the X and Y coordinates as used from YanusIV.
        /// </summary> 
        [StructLayout(LayoutKind.Sequential)]
        public struct LSM_Coordinate
        {
            public LSM_Coordinate(double __dX, double __dY)
            {
                this.X = __dX;
                this.Y = __dY;
            }

            public double X;///< The X coordinate
            public double Y;///< The Y coordinate
        };





        #region Till Photonics LSM device DLL functions

        /// <summary>
        /// DLL initialization and common functions:
        /// DLL initialization and common functions:
        /// DLL initialization and common functions:
        /// </summary>


        /// <summary>
        /// A delegate (function pointer) - a callback prototype to signal protocol end.
        /// </summary>
        /// <param name="context">A pointer to type void.</param>  
        public delegate void ProtocolDoneCb(IntPtr context);


        /// <summary>
        /// Aborts the current scanning protocol execution.
        /// </summary>
        /// <param name="sRegKeyName">A string to... .</param>        
        /// <returns>int: An integer error code.</returns>   
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Abort")]
        internal static extern int LSM_Abort(string sRegKeyName);


        /// <summary>
        /// Aborts the current scanning protocol execution.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Abort")]
        internal static extern int LSM_Abort(IntPtr handle);


        /// <summary>
        /// Adds a calibration point.
        /// 
        /// The calibration calculates the needed transformation between pixel positions
        /// and galvos positions. The calibration needs at least three point pairs and these
        /// points have to be independent (they should not be aligned). It is possible to give 
        /// as many points as needed. With each new point the precision of the calibration is 
        /// increased. Without calibration, LSM_AddPolygon(), LSM_AddParallelogram(), LSM_SetPoint() 
        /// and LSM_GetRealShape() are disabled.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="pixCoordinate">The pixel coordinates.</param> 
        /// <param name="galvoCoordinate">The galvos position (in counts).</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddCalibrationPoint")]
        internal static extern int LSM_AddCalibrationPoint(IntPtr handle, LSM_Coordinate pixCoordinate, LSM_Coordinate galvoCoordinate);


        /// <summary>
        /// Allows to define a callback for the protocol end.
        /// 
        /// If a callback is set, the given function will be called after each protocol successfully
        /// ended in the FRAPPA. The context parameter allows to give a pointer to anything that 
        /// will be then send back in the callback.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>  
        /// <param name="pdCb">Function pointer for the callback.</param>
        /// <param name="context">A user defined pointer that will be returned in the callback.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddEndCallback")]
        internal static extern int LSM_AddEndCallback(IntPtr handle, ref ProtocolDoneCb pdCb, IntPtr context);


        /// <summary>
        /// Add a line to a scanning protocol.
        /// 
        /// A line is defined by the pixel coordinates of its extremities. The laser moves along the given line at constant 
        /// speed for the given tNet time. The scanning of the line can be repeated loops time. The line can be scanned 
        /// unidirectionally (the scan always begins at the starting point) or bidirectionally (alternatively scanning from 
        /// origin to end and from end to origin). If protH points to NULL, a new protocol is created and returned in protH. 
        /// In this case, protH can then be used in all methods using a protocol handle. If protH points to an existing protocol, 
        /// the desired point scan is added at the end of the the protocol. During the execution, the shapes are scanned according 
        /// to their order in the protocol. To be executed a protocol must be loaded in the LSM device through LSM_Load() and then started 
        /// through LSM_Execute().
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protH">Pointer to a protocol handle. If points to NULL, a new protocol is created.</param> 
        /// <param name="point1">Pixel Coordinates of the origin of the line to scan.</param> 
        /// <param name="point2">Pixel Coordinates of the end of the line to scan.</param> 
        /// <param name="bidirectional">Indicates if the line should be scanned bidirectionally.</param> 
        /// <param name="tNet">Bleaching time in seconds.</param> 
        /// <param name="tGross">Scan execution time in seconds (not used yet).</param> 
        /// <param name="loops">Number of repeats of the scan on this line.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddLine")]
        internal static extern int LSM_AddLine(IntPtr handle, ref IntPtr protH, LSM_Coordinate point1, LSM_Coordinate point2, bool bidirectional, ref double tNet, ref double tGross, uint loops);


        /// <summary>
        /// Add a parallelogram to a scanning protocol.
        /// 
        /// The area is defined through the start point, the point at the end of the first line, the point at the beginning of the last line,
        /// the number of lines, the desired illumination time and the number of times the shape should be scanned. Due to the change of direction
        /// for the galvos at the end of each line, the total execution is longer than the illumination and is returned as the tGross parameter. If 
        /// the given illumination time is too small to execute a given protocol, then the minimum time is calculated and returned in tNet. If protH 
        /// points to NULL, a new protocol is created and returned in protH. In this case, protH can then be used in all methods using a protocol 
        /// handle. If protH points to an existing protocol, the desired parallelogram scan is added at the end of the the protocol. During the execution,
        /// the shapes are scanned according to their order in the protocol. To be executed a protocol must be loaded in the LSM device through LSM_Load()
        /// and then started through LSM_Execute().
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protH">Pointer to a protocol handle. If points to NULL, a new protocol is created.</param> 
        /// <param name="start">Coordinates of the starting point (in pixel coordinates).</param> 
        /// <param name="row">Coordinates of the point at the end of the first scanned line (in pixel coordinates).</param> 
        /// <param name="column">Coordinates of the point at the beginning of the last scanned line (in pixel coordinates).</param> 
        /// <param name="lines">Number of lines to scan.</param> 
        /// <param name="tNet">Bleaching time in seconds.</param> 
        /// <param name="tGross">Scan execution time in seconds.</param> 
        /// <param name="loops">Number of repeats of the scan on this parallelogram.</param>      
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddParallelogram")]
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


        /// <summary>
        /// Add a point to a scanning protocol.
        /// 
        /// The point will be bleached for the given tNet time. If the scan is repeated (loops > 1) the time between two bleaching is given by 
        /// loopCycle. If protH points to NULL, a new protocol is created and returned in protH. In this case, protH can then be used in all 
        /// methods using a protocol handle. If protH points to an existing protocol, the desired point scan is added at the end of the the protocol.
        /// During the execution, the shapes are scanned according to their order in the protocol. To be executed a protocol must be loaded in the LSM
        /// device through LSM_Load() and then started through LSM_Execute().
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protH">Pointer to a protocol handle. If points to NULL, a new protocol is created.</param> 
        /// <param name="point">Pixel Coordinates of the point to scan.</param> 
        /// <param name="row">Coordinates of the point at the end of the first scanned line (in pixel coordinates).</param> 
        /// <param name="column">Coordinates of the point at the beginning of the last scanned line (in pixel coordinates).</param> 
        /// <param name="tNet">Bleaching time in seconds.</param> 
        /// <param name="tGross">Scan execution time in seconds (not used yet).</param> 
        /// <param name="loops">Number of repeats of the scan on this point.</param>      
        /// <param name="loopCycle">Time in seconds between two consecutive bleaching of the point.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddPoint")]
        internal static extern int LSM_AddPoint(IntPtr handle, ref IntPtr protH, LSM_Coordinate point, ref double tNet, ref double tGross, uint loops, double loopCycle);


        /// <summary>
        /// Add an arbitrary shape to a scanning protocol.
        /// 
        /// The area is defined through a list of points coordinates, the angle between the abscissa axis and the lines of the scan, a boolean indicating
        /// the direction of the jumps between lines, the number of lines, the desired illumination time and the number of times the shape should be scanned.
        /// Due to the change of direction for the galvos at the end of each line, the total execution is longer than the illumination and is returned as the 
        /// tGross parameter. If the given illumination time is too small to execute a given protocol, then the minimum time is calculated and returned in tNet.
        /// The boolean counterclockwise indicates if the direction of the line jumps is counterclockwise to the direction of the first scan line. If protH points 
        /// to NULL, a new protocol is created and returned in protH. In this case, protH can then be used in all methods using a protocol handle. If protH points 
        /// to an existing protocol, the desired polygon scan is added at the end of the the protocol. During the execution, the shapes are scanned according to 
        /// their order in the protocol. To be executed a protocol must be loaded in the LSM device through LSM_Load() and then started through LSM_Execute().
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protH">Pointer to a protocol handle. If points to NULL, a new protocol is created.</param> 
        /// <param name="points">The list of the coordinates of the points defining the desired shape (in pixel coordinates).</param> 
        /// <param name="nbPoints">Number of points in the list.</param> 
        /// <param name="alpha">Angle in degrees between the abscissa axis and the lines of the scan.</param> 
        /// <param name="counterclockwise">Boolean indicating if the line jumps direction are made counterclockwise to the first scan line direction.</param> 
        /// <param name="lines">Number of lines to scan.</param>
        /// <param name="tNet">Bleaching time in seconds.</param> 
        /// <param name="tGross">Scan execution time in seconds.</param> 
        /// <param name="loops">Number of repeats of the scan on this shape.</param>      
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_AddPolygon")]
        internal static extern int LSM_AddPolygon(IntPtr handle, ref IntPtr protH, ref LSM_Coordinate points, uint nbPoints,
            double alpha,
            bool counterclockwise,
            uint lines, ref double tNet, ref double tGross, uint loops);


        /// <summary>
        /// Disconnecting from a LSM device.
        /// 
        /// On disconnection the LSM device handle will be invalidated and all previously made settings will be lost.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Close")]
        internal static extern int LSM_Close(IntPtr handle);


        /// <summary>
        /// Indicates if the LSM protocol sends a trigger out when its protocol is done.
        /// 
        /// The trigger can be rising or falling. The LSM device can then return in the rest position (the acquisition position).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="device">Trigger exit (1001 for T-OUT, 1002 for D-OUT).</param>    
        /// <param name="trigger">Indicates the form of the end trigger : 1 for rising, -1 for falling, 0 for none.</param>    
        /// <param name="returnToRest">Indicates if the galvos should return to the rest position at the end of the protocol.</param>    
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigEndTrigger")]
        internal static extern int LSM_ConfigEndTrigger(IntPtr handle, int device, int trigger, bool returnToRest);


        /// <summary>
        /// Determines the state of the blanking trigger (high for laser on, down for laser on or none).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="device">Trigger exit (1001 for T-OUT, 1002 for D-OUT).</param>
        /// <param name="trigger">Indicates the level of the laser when on : 1 for high, -1 for down, 0 for none.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigLaserTrigger")]
        internal static extern int LSM_ConfigLaserTrigger(IntPtr handle, int device, int trigger);


        /// <summary>
        /// Determines if the LSM protocol waits for a trigger in to start.
        /// 
        /// The trigger can be rising or falling.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="trigger">Indicates the form of the start trigger : 1 for rising, -1 for falling, 0 for none.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ConfigTriggerIn")]
        internal static extern int LSM_ConfigTriggerIn(IntPtr handle, int trigger);


        /// <summary>
        /// Create a new LSM protocol.
        /// 
        /// Using LSM_AddPolygon() or LSM_AddParallelogram() with a null pointer as protocol handle, also creates a new LSM protocol.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="protoH">Pointer to a protocol handle.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_CreateProtocol")]
        internal static extern int LSM_CreateProtocol(IntPtr handle, ref IntPtr protH);


        /// <summary>
        /// Delete the given protocol.
        /// 
        /// Free the associated resources.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>  
        /// <param name="protoH">Pointer to a protocol to delete.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_DeleteProtocol")]
        internal static extern int LSM_DeleteProtocol(IntPtr handle, IntPtr protH);


        /// <summary>
        /// Executes the loaded scanning protocol.
        /// 
        /// A protocol must first be loaded with LSM_Load() before being executed.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Execute")]
        internal static extern int LSM_Execute(IntPtr handle);


        /// <summary>
        /// Determines the bounding box of a given shape.
        /// 
        /// In addition to the points list, the angle between the first line of the scan and the abscissa axis and a boolean indicating 
        /// the direction of the jumps between lines must be given. The boolean counterclockwise indicates if the direction of the line
        /// jumps is counterclockwise to the direction of the first scan line. The method calculates the origin point of the bounding 
        /// box (the starting point of the scan), the coordinate of the point at the end of the first scan line and the coordinates of 
        /// the point at the beginning of the last line of the scan. It is then easy to deduce the point at the end of the last line and 
        /// such to obtain the rectangle used to scan the given form.
        /// </summary>
        /// <param name="alpha">Angle in degrees between the abscissa axis and the lines of the scan.</param>
        /// <param name="counterclockwise">Boolean indicating if the line jumps direction are made counterclockwise to the first scan line direction.</param>
        /// <param name="points">List of coordinates of the points defining the shape (in pixel coordinates).</param> 
        /// <param name="nbPoints">Number of points in the list.</param> 
        /// <param name="start">Coordinates of the starting point (in pixel coordinates).</param> 
        /// <param name="row">Coordinates of the point at the end of the first scanned line (in pixel coordinates).</param> 
        /// <param name="column">Coordinates of the point at the beginning of the last scanned line (in pixel coordinates).</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetBoundingBox")]
        internal static extern int LSM_GetBoundingBox(double alpha,
            bool counterclockwise,
            ref LSM_Coordinate points,
            int nbPoints,
            ref LSM_Coordinate start,
            ref LSM_Coordinate row,
            ref LSM_Coordinate column);


        /// <summary>
        /// Retrieve the LSM protocol end trigger shape, the exit it comes from and indicates if the galvos return to their resting position.
        /// 
        /// The trigger can be rising or falling (1 for rising, -1 for falling, 0 for none).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="device">Trigger exit (1001 for T-OUT, 1002 for D-OUT).</param> 
        /// <param name="trigger">Indicates the form of the end trigger : 1 for rising, -1 for falling, 0 for none.</param> 
        /// <param name="returnToRest">Indicates if the galvo return to their resting position.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigEndTrigger")]
        internal static extern int LSM_GetConfigEndTrigger(IntPtr handle, ref int device, ref int trigger, ref bool returnToRest);


        /// <summary>
        /// Retrieve the LSM protocol laser blanking shape and the exit it comes from.
        /// 
        /// The trigger can be on with high or down level (1 for high, -1 for down, 0 for none).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="device">Trigger exit (1001 for T-OUT, 1002 for D-OUT).</param>
        /// <param name="trigger">Indicates the level of the laser when on : 1 for high, -1 for down, 0 for none.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigLaserTrigger")]
        internal static extern int LSM_GetConfigLaserTrigger(IntPtr handle, ref int device, ref int trigger);


        /// <summary>
        /// Retrieve the LSM protocol start trigger shape.
        /// 
        /// The trigger can be rising or falling (1 for rising, -1 for falling, 0 for none).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="trigger">Indicates the form of the start trigger : 1 for rising, -1 for falling, 0 for none.</param>  
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetConfigTriggerIn")]
        internal static extern int LSM_GetConfigTriggerIn(IntPtr handle, ref int trigger);


        /// <summary>
        /// Retrieve the shape really scanned by the given part of the defined protocol.
        /// 
        /// A scanning protocol contains a list of shapes that are to be scanned. For each of these shapes, the real shape can be 
        /// retrieved (specified by its index in the protocol). Due to the 10us raster of the commands in the LSM device, the exact
        /// shape defined in the protocol can not be exactly obtained. With this method the real scan shape can be obtained and compared
        /// with the desired one. If the pointers to receive the shape points are not yet initialized (they are NULL), the method returns
        /// the number of points defining the real scanned shape. Else the method, returns the list of coordinates of the points defining
        /// the real scanned shape.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protoH">The protocol handle.</param>
        /// <param name="shapeIndex">The index of the desired shape in the protocol.</param>
        /// <param name="points">List of pixel coordinates of the points defining the shape really scanned (if NULL, only the number of points is returned in the next parameter).</param>
        /// <param name="length">Number of points defining the real scanned shape (Ignored if the pointsX or pointsY arguments are NULL).</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetRealShape")]
        internal static extern int LSM_GetRealShape(IntPtr handle, IntPtr protH, int shapeIndex, LSM_Coordinate[] points, ref int length);


        /// <summary>
        /// Gets the resting position of the galvos.
        /// 
        /// (see LSM_ConfigEndTrigger()).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="galvoPos">The resting position in galvo counts.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetRestPosition")]
        internal static extern int LSM_GetRestPosition(IntPtr handle, ref LSM_Coordinate galvoPos);


        /// <summary>
        /// Checks if the selected LSM is calibrated.
        /// 
        /// At least three points must be added by LSM_AddCalibrationPoint() for a calibration to be successful.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="calibrated">TRUE if the selected LSM device is calibrated, FALSE otherwise.</param>    
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_IsCalibrated")]
        internal static extern int LSM_IsCalibrated(IntPtr handle, ref bool calibrated);


        /// <summary>
        /// Load the given protocol in the LSM_Device.
        /// 
        /// The protocol handle is created through the first call of LSM_AddPolygon. The whole scanning protocol is repeated
        /// as many times as specified in nbLoops. If nbLoops is 0, the scanning protocol will be done only once during execution.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protoH">The protocol handle.</param>
        /// <param name="nbLoops">Number of times the whole scan protocol should be repeated.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Load")]
        internal static extern int LSM_Load(IntPtr handle, IntPtr protH, int nbLoops);


        /// <summary>
        /// Connecting to a specific LSM device.
        /// 
        /// The LSM is selected by the com port. On successful connection a valid LSM device handle is assigned by the function.
        /// </summary>
        /// <param name="port">The com port (e.g. "COM1") of an existing LSM device.</param>        
        /// <param name="handle">The LSM device handle for the given com port.</param>  
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Open")]
        internal static extern int LSM_Open([In()] string port, ref IntPtr handle);


        /// <summary>
        /// Determine the commands sent to the LSM device when loading the specified protocol.
        /// 
        /// Each command ends with a semicolon "\n" If text is NULL, only the number of characters in the command list 
        /// is returned. LSM_Read should be used in two steps: first text is NULL, and the number of characters is retrieved,
        /// then allocate a char buffer with the retrieved length +1 and use it in a second call of LSM_Read().
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="protH">The protocol handle.</param>
        /// <param name="text">Buffer to receive the list of commands (if NULL, only the number of characters is returned in the next parameter).</param>
        /// <param name="size">Number of characters in the commands list (ignored if text is not NULL).</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Read")]
        internal static extern int LSM_Read(IntPtr handle, IntPtr protH, char[] text, ref int size);


        /// <summary>
        /// Reset the calibration by removing all previously set calibration points.
        /// 
        /// The LSM device is no longer calibrated and new calibration points have to be added.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ResetCalibration")]
        internal static extern int LSM_ResetCalibration(IntPtr handle);


        /// <summary>
        /// Move both scanning galvos absolute compared to their central position (uncalibrated).
        /// 
        /// The coordinates are given in counts between -2^35 and 2^35. This method can be used for the calibration to
        /// determine the pixel positions corresponding to the given galvo positions.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="galvoCoordinate">The new positions for both scanning galvos (in counts).</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetGalvoRawPosition")]
        internal static extern int LSM_SetGalvoRawPosition(IntPtr handle, LSM_Coordinate galvoCoordinate);


        /// <summary>
        /// Move a scanning galvo absolute compared to its central position (uncalibrated).
        /// 
        /// The coordinates are given in counts between -2^35 and 2^35. This method can be used for the calibration to 
        /// determine the pixel positions corresponding to the given galvo positions. If a polytrope is built in, it can 
        /// be moved using the index 3. This method can only be called when no scanning protocol is running. NB: The Polytrope
        /// can be controlled with index 3 and with the galvoPos given between -2^35 and 2^35. it is the only method allowing to
        /// control the Polytrope.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="index">Zero based index of the galvo.</param>
        /// <param name="galvoPos">The new position for the galvo (in counts).</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetGalvoRawPosition1")]
        internal static extern int LSM_SetGalvoRawPosition1(IntPtr handle, int index, double galvoPos);


        /// <summary>
        /// Set an offset for the current protocol execution.
        /// 
        /// When the offset is set, the protocol stops, the offset is sent to the frapper and the protocol is restarted.
        /// It means that the Frapper will wait of an eventual trigger if LSM_ConfigTriggerIn was used with trigger != 0. 
        /// The loop counter is also reset: for example, if the protocol was loaded to do 10 loops, independently of the 
        /// time the offset was set, the protocol will do 10 loops after the offset was set. The Offset is not reset for 
        /// new protocols, so it should be manually reset to 0,0 to start a new protocol with withRestart set to false. The 
        /// Offset is not cumulative, it always indicates the offset to the regions as they were when LSM_Load was called.
        /// The Offset applies for every region of the same protocol. You can not set an offset for each region of your protocol.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="offset">The relative move to add to the regions in pixel coordinates.</param>
        /// <param name="withRestart">Indicates if the last loaded protocol should be restarted after the offset is set.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetOffset")]
        internal static extern int LSM_SetOffset(IntPtr handle, LSM_Coordinate offset, bool withRestart);


        /// <summary>
        /// Move the scanning galvos to the corresponding pixel position.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="pixCoordinate">The desired position in pixel coordinates.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetPoint")]
        internal static extern int LSM_SetPoint(IntPtr handle, LSM_Coordinate pixCoordinate);


        /// <summary>
        /// Sets the resting position of the galvos.
        /// 
        /// It can be used to jump to a specific position at the end of a protocol (see LSM_ConfigEndTrigger()).
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="galvoPos">The desired resting position in galvo counts.</param>
        /// <returns>int: An integer error code.</returns>
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_SetRestPosition")]
        internal static extern int LSM_SetRestPosition(IntPtr handle, LSM_Coordinate galvoPos);


        /// <summary>
        /// Create a protocol where a single galvo position is bleached.
        /// 
        /// This protocol can be used to "shoot" a point on a phosphorescence substance, and then acquire a picture to determine
        /// the relation galvo/camera position (see LSM_AddCalibrationPoint). To acquire the picture short after the point was "shot",
        /// the returnToRest parameter of the LSM_ConfigEndTrigger method should be set to true. The tNet parameter allows to define 
        /// the duration of the laser bleach. The protocol can be repeated count times with the given cycle time. If the cycle time 
        /// is too small, the smallest possible cycle time is returned.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="galvoPoint">The desired point to "shoot" in galvo counts.</param>
        /// <param name="tNet">Bleaching time in seconds.</param>
        /// <param name="count">Number of times the shoot protocol is repeated.</param>
        /// <param name="cycleTime">Cycle time of the protocol repeat.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_Shootpoint")]
        internal static extern int LSM_ShootPoint(IntPtr handle, LSM_Coordinate galvoPoint, double tNet, uint count, ref double cycleTime);


        /// <summary>
        /// Get command set version.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="commandSetVersion">Command set version.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_GetCommandSetVersion")]
        internal static extern int LSM_GetCommandSetVersion(IntPtr handle, ref int commandSetVersion);


        /// <summary>
        /// Hack set smart move.
        /// </summary>
        /// <param name="smart">???.</param>        
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_HackSetSmartMove")]
        internal static extern int LSM_HackSetSmartMove(double smart);


        /// <summary>
        /// Load commands.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>
        /// <param name="text">???.</param> 
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_LoadCommands")]
        internal static extern int LSM_LoadCommands(IntPtr handle, ref char text);


        /// <summary>
        /// Extract protocol.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="protH">Protocol handle.</param>
        /// <param name="size">???.</param>
        /// <param name="devices">???.</param>
        /// <param name="vals">???.</param>
        /// <param name="times">???.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_ExtractProtocol")]
        internal static extern int LSM_ExtractProtocol(IntPtr handle, IntPtr protH, ref int size, ref int devices, ref double vals, ref ulong times);


        /// <summary>
        /// Direct set offset.
        /// </summary>
        /// <param name="handle">The LSM device handle.</param>        
        /// <param name="offset">???.</param>
        /// <returns>int: An integer error code.</returns>  
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "LSM_DirectSetOffset")]
        internal static extern int LSM_DirectSetOffset(IntPtr handle, LSM_Coordinate offset);


        /// <summary>
        /// Initialization callback.
        /// </summary>
        /// <param name="id">???.</param>
        /// <param name="str">???.</param>         
        [DllImport(TillLibName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "init_callback")]
        public static extern void init_callback(int id, IntPtr str);


        #endregion Till Photonics LSM device DLL functions

    }
}
