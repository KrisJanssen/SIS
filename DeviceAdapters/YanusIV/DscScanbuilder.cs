//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="DscScanbuilder.cs" company="Kris Janssen">
////   Copyright (c) 2014 Kris Janssen
//// </copyright>
//// <summary>
////   The dsc sanbuilder.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------
//namespace SIS.Hardware
//{
//    using System;

//    /// <summary>
//    /// The DSC sanbuilder.
//    /// </summary>
//    internal static class DscScanbuilder
//    {
//        #region Public Methods and Operators

//        /// <summary>
//        /// Add/Create a rectangle in the format suitable to send as DSP scanning protocol that scans a rectangular area.
//        /// </summary>
//        /// <param name="yanusIv">
//        /// </param>
//        /// <param name="protocol">
//        /// The protocol.
//        /// </param>
//        /// <param name="start">
//        /// The start.
//        /// </param>
//        /// <param name="column">
//        /// The column.
//        /// </param>
//        /// <param name="row">
//        /// The row.
//        /// </param>
//        /// <param name="timePerPixel">
//        /// The time Per Pixel.
//        /// </param>
//        public static void AddRectangle(
//            YanusIV yanusIv, 
//            DscProtocol protocol, 
//            CoordinateD start, 
//            CoordinateD column, 
//            CoordinateD row, 
//            double timePerPixel)
//        {
//            // Set and check the scanning range in terms of angles values - starting angle position and angle scan range.
//            long _int64XScanAngleInt = this.ConvertMeterToAngle(__dXScanSizeNm);

//            // the width of the image in terms of an integer (angle)
//            long _int64YScanAngleInt = this.ConvertMeterToAngle(__dYScanSizeNm);

//            // the height of the image in terms of an integer (angle)
//            long _int64StartXAngleInt = yanusIv.ConvertMeterToAngle(__dInitialXnm) - _int64XScanAngleInt / 2L;

//            // X offset of the scanning range in terms of an integer (angle) - note that the initial angle is divided by two because (X=0,Y=0) is the middle of the scan area
//            long _int64StartYAngleInt = yanusIv.ConvertMeterToAngle(__dInitialYnm) + _int64YScanAngleInt / 2L;

//            // Y offset of the scanning range in terms of an integer (angle) - note that the initial angle is divided by two because (X=0,Y=0) is the middle of the scan area

//            // Check if we do not exceed the max galvo range along X
//            if (_int64StartXAngleInt < -MAX_SETUP_GALVO_RANGE_ANGLE
//                || _int64StartXAngleInt > MAX_SETUP_GALVO_RANGE_ANGLE)
//            {
//                _int64StartXAngleInt = -_int64XScanAngleInt / 2L;

//                // if we exceed the max galvo range set a default value (in this case no offset applied)
//            }

//            // Check if we do not exceed the max galvo range along Y
//            if (_int64StartYAngleInt < -MAX_SETUP_GALVO_RANGE_ANGLE
//                || _int64StartYAngleInt > MAX_SETUP_GALVO_RANGE_ANGLE)
//            {
//                _int64StartYAngleInt = +_int64YScanAngleInt / 2L;

//                // if we exceed the max galvo range set a default value (in this case no offset applied)
//            }

//            // Get frame/line markers
//            long _int64FrameMarker = (Int64)yanusIv.m_iFrameMarker;

//            // the value of the frame marker to be raised by YanusIV in the beginning of each frame
//            long _int64LineMarker = (Int64)yanusIv.m_iLineMarker;

//            // the value of the line marker to be raised by YanusIV in the beginning of each line            

//            // Calc time per pixel in terms of cycles (one cycle equals 10.0us)
//            double _dTimePPixelUs = __dTimePPixel * 1000.0; // time per pixel in [us]
//            double _dTimePPixelCycles = _dTimePPixelUs / SINGLE_CYCLE_LENGTH_us;

//            // time per pixel in terms of YanusIV controller cycles

//            // Increment/decrement and angles in terms of cycles
//            ulong _ui64XAngleCycles = (UInt64)(__intImageWidthPx * _dTimePPixelCycles);

//            // the image width in terms of cycles of the YanusIV controller
//            ulong _ui64YAngleCycles = (UInt64)__intImageHeightPx;

//            // the image height in terms of cycles of the YanusIV controller. Note that it differs than the image width in terms of cycles, because here we directly go to the new line.
//            long _int64XAngleIntPerCycle = _int64XScanAngleInt / (Int64)_ui64XAngleCycles;

//            // the angle per cycle in order to cover the width of the image
//            long _int64YAngleIntPerCycle = _int64YScanAngleInt / (Int64)_ui64YAngleCycles;

//            // the angle per cycle in order to cover the height of the image

//            // Create the DSP rectangle
//            switch (__iTypeOfScan)
//            {
//                case 0:
//                    {
//                        // The scan type is unidirectional scan
//                        // Start DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                0UL, 
//                                DscChannel.None, 
//                                __int64ScanCommandLoopCount)); // add start DSP loop

//                        // Go to top initial position from where we start to scan the rectangle at cycle 0
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.X, _int64StartXAngleInt));

//                        // move X to the left end of the rectangle
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.Y, _int64StartYAngleInt));

//                        // move Y to the top end of the rectangle

//                        // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                0UL, 
//                                DscChannel.X, 
//                                _int64XAngleIntPerCycle)); // set the increment along X so that first we scan along X

//                        // set the increment along Y to zero (first we scan a line along X)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DscChannel.Y, 0L));

//                        // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV). Note that we need a frame marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.DO, _int64FrameMarker));

//                        // raise the frame marker
//                        yanusIv.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 1UL, DscChannel.DO, 0L));

//                        // set down the frame marker (it is enough to raise the marker for one cycle only)

//                        // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                __intImageHeightPx));

//                        // add start DSP loop - the number of loops equals the number of lines to scan, i.e. the height of the image

//                        // Go to top initial X position from where we start to scan a new line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                _int64StartXAngleInt)); // move X to the left end of the rectangle

//                        // Set the Y axis decrement so that we go to the next line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                -_int64YAngleIntPerCycle));

//                        // set the decrement along Y so that we go to the next line along Y (top down scanning)

//                        // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                _ui64XAngleCycles, 
//                                DscChannel.DO, 
//                                _int64LineMarker)); // raise the line marker
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DscChannel.DO, 0L));

//                        // set down the line marker (it is enough to raise the marker for one cycle only)

//                        // Set the Y axis decrement to zero so that we stay on the new line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                _ui64XAngleCycles + 1UL, 
//                                DscChannel.Y, 
//                                0L));

//                        // End DSP loop 2
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_loop_end, 2L * _ui64XAngleCycles, DscChannel.None, 0L));

//                        // add end DSP loop - note that we multiply by factor of 2 because the first line was already scanned before entering the second loop.

//                        // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                0L));

//                        // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                0L));

//                        // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

//                        // End DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_end, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                0L)); // add end DSP loop

//                        break;
//                    }

//                case 1:
//                    {
//                        // The scan type is bidirectional scan
//                        // The scan type is bidirectional scan

//                        // Start DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                0UL, 
//                                DscChannel.None, 
//                                __int64ScanCommandLoopCount)); // add start DSP loop

//                        // Go forward direction (left to right)

//                        // Go to top initial position from where we start to scan the rectangle
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.X, _int64StartXAngleInt));

//                        // move X to the left end of the rectangle
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.Y, _int64StartYAngleInt));

//                        // move Y to the top end of the rectangle

//                        // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                0UL, 
//                                DscChannel.X, 
//                                _int64XAngleIntPerCycle)); // set the increment along X so that first we scan along X
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DscChannel.Y, 0L));

//                        // set the increment along Y to zero (first we scan a line along X)

//                        // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV). Note that we need a frame marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.DO, _int64FrameMarker));

//                        // raise the frame marker
//                        yanusIv.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 1UL, DscChannel.DO, 0L));

//                        // set down the frame marker (it is enough to raise the marker for one cycle only)

//                        // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                __intImageHeightPx / 2));

//                        // add start DSP loop - the number of loops equals the number of lines to scan over 2, i.e. the height of the image over 2 (because it is a bidirectional scan)

//                        // Go backward direction (right to left)

//                        // Set the X axis decrement so that galvo scan X axis in backwards direction (because the scan is bidirectional)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                -_int64XAngleIntPerCycle));

//                        // set the decrement along X so that we go to backwards direction along X (bidirectional scanning)

//                        // Set the Y axis decrement so that we go to the next line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                -_int64YAngleIntPerCycle));

//                        // set the decrement along Y so that we go to the next line along Y (top down scanning)

//                        // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                _ui64XAngleCycles, 
//                                DscChannel.DO, 
//                                _int64LineMarker)); // raise the line marker
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DscChannel.DO, 0L));

//                        // set down the line marker (it is enough to raise the marker for one cycle only)

//                        // Set the Y axis decrement to zero so that we stay on the new line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                _ui64XAngleCycles + 1UL, 
//                                DscChannel.Y, 
//                                0L));

//                        // Go forward direction (left to right)

//                        // Set the X axis increment so that galvo scan X axis in forward direction (note that now we reached the beginning of the line, so we have to tell the galvo to scan in forward direction, i.e. left to right)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                2UL * _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                _int64XAngleIntPerCycle));

//                        // set the increment along X so that we go to forward direction along X

//                        // Set the Y axis decrement so that we go to the next line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                2UL * _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                -_int64YAngleIntPerCycle));

//                        // set the decrement along Y so that we go to the next line along Y (top down scanning)

//                        // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                2UL * _ui64XAngleCycles, 
//                                DscChannel.DO, 
//                                _int64LineMarker)); // raise the line marker
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                2UL * _ui64XAngleCycles + 1UL, 
//                                DscChannel.DO, 
//                                0L)); // set down the line marker (it is enough to raise the marker for one cycle only)

//                        // Set the Y axis decrement to zero so that we stay on the new line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                2UL * _ui64XAngleCycles + 1UL, 
//                                DscChannel.Y, 
//                                0L));

//                        // End DSP loop 2
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_loop_end, 3UL * _ui64XAngleCycles, DscChannel.None, 0L));

//                        // add end DSP loop - note that we multiply by factor of 3 because the first line was already scanned before entering the second loop (and inside this loop we scan two lines per loop iteration).

//                        // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                0L));

//                        // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                0L));

//                        // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

//                        // End DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_end, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                0L)); // add end DSP loop

//                        break;
//                    }

//                case 2:
//                    {
//                        // The scan type is unidirectional scan
//                        // Start DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                0UL, 
//                                DscChannel.None, 
//                                __int64ScanCommandLoopCount)); // add start DSP loop

//                        // Go to top initial position from where we start to scan the rectangle at cycle 0
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.X, _int64StartXAngleInt));

//                        // move X to the left end of the rectangle
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.Y, _int64StartYAngleInt));

//                        // move Y to the top end of the rectangle

//                        // Set the axes increments so that we cover the whole scan range (increments are applied on every 10us cycle) - controls the scanning along X and Y
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                0UL, 
//                                DscChannel.X, 
//                                _int64XAngleIntPerCycle)); // set the increment along X so that first we scan along X

//                        // set the increment along Y to zero (first we scan a line along X)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_increment_1, 0UL, DscChannel.Y, 0L));

//                        // Raise a frame marker - marks the beginning of a frame (the el. signal appears on the digital outputs port of YanusIV). Note that we need a frame marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, 0UL, DscChannel.DO, _int64FrameMarker));

//                        // raise the frame marker
//                        yanusIv.m_lCmdList.Add(new DscCommand(ScanCommand.scan_cmd_set_value, 1UL, DscChannel.DO, 0L));

//                        // set down the frame marker (it is enough to raise the marker for one cycle only)

//                        // Start DSP loop 2 - this loop scans the rest of the frame + one more line (this line marks the end of the frame)
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_start, 
//                                _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                __intImageHeightPx));

//                        // add start DSP loop - the number of loops equals the number of lines to scan, i.e. the height of the image

//                        // Go to top initial X position from where we start to scan a new line
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                _int64StartXAngleInt)); // move X to the left end of the rectangle

//                        // Set the Y axis decrement so that we go to the next line
//                        // this.m_lCmdList.Add(
//                        // new DSCCommand(
//                        // ScanCommand.scan_cmd_set_increment_1,
//                        // _ui64XAngleCycles,
//                        // DSCChannel.Y,
//                        // -_int64YAngleIntPerCycle));

//                        // set the decrement along Y so that we go to the next line along Y (top down scanning)

//                        // Raise a line marker - marks the beginning of a line (the el. signal appears on the digital outputs port of YanusIV). Note that we need a line marker to extract an image from the raw Time Harp data stream.
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_value, 
//                                _ui64XAngleCycles, 
//                                DscChannel.DO, 
//                                _int64LineMarker)); // raise the line marker
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_set_value, _ui64XAngleCycles + 1UL, DscChannel.DO, 0L));

//                        // set down the line marker (it is enough to raise the marker for one cycle only)

//                        // Set the Y axis decrement to zero so that we stay on the new line
//                        // this.m_lCmdList.Add(
//                        // new DSCCommand(
//                        // ScanCommand.scan_cmd_set_increment_1,
//                        // _ui64XAngleCycles + 1UL,
//                        // DSCChannel.Y,
//                        // 0L));

//                        // End DSP loop 2
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(ScanCommand.scan_cmd_loop_end, 2L * _ui64XAngleCycles, DscChannel.None, 0L));

//                        // add end DSP loop - note that we multiply by factor of 2 because the first line was already scanned before entering the second loop.

//                        // Set the axes increments to zero so that we stop the scanning - the frame is done, so no need to move the galvo axes (if we do not stop the increment of the axes they may go out of range, which is dangerous).
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.X, 
//                                0L));

//                        // set the increment along X to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_set_increment_1, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.Y, 
//                                0L));

//                        // set the increment along Y to zero (scanning the current frame finished) - note that we scanned one more line than the image height, therefore we need to take into account when calculating the end cycle value

//                        // End DSP loop 1
//                        yanusIv.m_lCmdList.Add(
//                            new DscCommand(
//                                ScanCommand.scan_cmd_loop_end, 
//                                ((UInt64)__intImageHeightPx) * _ui64XAngleCycles + _ui64XAngleCycles, 
//                                DscChannel.None, 
//                                0L)); // add end DSP loop

//                        break;
//                    }

//                default:
//                    {
//                        break;
//                    }
//            }
//        }

//        /// <summary>
//        /// The delta coordinate.
//        /// </summary>
//        /// <param name="firsCoordinate">
//        /// The firs coordinate.
//        /// </param>
//        /// <param name="secondCoordinate">
//        /// The second coordinate.
//        /// </param>
//        /// <returns>
//        /// The calculated X and Y distance between both input <see cref="CoordinateD"/>s.
//        /// </returns>
//        public static CoordinateD DeltaCoordinate(CoordinateD firsCoordinate, CoordinateD secondCoordinate)
//        {
//            var deltaCoordinate = new CoordinateD
//                                      {
//                                          X = secondCoordinate.X - firsCoordinate.X, 
//                                          Y = secondCoordinate.Y - firsCoordinate.X
//                                      };

//            return deltaCoordinate;
//        }

//        /// <summary>
//        /// Rotate a given coordinate around a center by a given angle.
//        /// </summary>
//        /// <param name="inCoordinate">
//        /// The coordinate to be rotated.
//        /// </param>
//        /// <param name="centerCoordinate">
//        /// The center coordinate.
//        /// </param>
//        /// <param name="angle">
//        /// The angle or rotation.
//        /// </param>
//        /// <returns>
//        /// The rotated <see cref="CoordinateD"/>.
//        /// </returns>
//        public static CoordinateD RotateCoordinate(CoordinateD inCoordinate, CoordinateD centerCoordinate, double angle)
//        {
//            var rotCoordinate = new CoordinateD
//                                    {
//                                        X =
//                                            Math.Cos(angle) * (inCoordinate.X - centerCoordinate.X)
//                                            - Math.Sin(angle) * (inCoordinate.Y - centerCoordinate.Y)
//                                            + centerCoordinate.X, 
//                                        Y =
//                                            Math.Sin(angle) * (inCoordinate.X - centerCoordinate.X)
//                                            + Math.Cos(angle) * (inCoordinate.Y - centerCoordinate.Y)
//                                            + centerCoordinate.Y
//                                    };

//            return rotCoordinate;
//        }

//        #endregion
//    }
//}

