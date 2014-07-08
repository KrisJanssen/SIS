﻿using System;
using KUL.MDS.ScanModes;

namespace KUL.MDS.Hardware
{
    /// <summary>
    /// This interface provides a template to define any piezo stage hardware.
    /// </summary>
    public interface IPiezoStage
    {
        #region Properties.

        /// <summary>
        /// This string holds the last error generated by the hardware. "No Error" will be returned if no error occurred.
        /// </summary>
        string CurrentError
        {
            get;
        }

        /// <summary>
        /// Returns the current X position of the stage in nm.
        /// </summary>
        double XPosition
        {
            get;
        }

        /// <summary>
        /// Returns the current Y position of the stage in nm.
        /// </summary>
        double YPosition
        {
            get;
        }

        /// <summary>
        /// Returns the current Z position of the stage in nm.
        /// </summary>
        double ZPosition
        {
            get;
        }

        /// <summary>
        /// Returns the total number of single moves already performed during a scan.
        /// </summary>
        int SamplesWritten
        {
            get;
        }

        /// <summary>
        /// True if the stage hardware is initialized and ready for use. False otherwise.
        /// </summary>
        bool IsInitialized
        {
            get;
        }

        /// <summary>
        /// True is the stage is moving.
        /// </summary>
        bool IsMoving
        {
            get;
        }

        #endregion

        #region Events.

        /// <summary>
        /// Event thrown whenever the stage changed position.
        /// </summary>
        event EventHandler PositionChanged;

        /// <summary>
        /// Event thrown whenever the hardware generated an error.
        /// </summary>
        event EventHandler ErrorOccurred;

        /// <summary>
        /// Event thrown whenever the stage is switched on, or off.
        /// </summary>
        event EventHandler EngagedChanged;

        #endregion

        #region Methods.

        /// <summary>
        /// Initialize the stage hardware to prepare it for move and scan operations.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Configure the stage to perform scans at a set rate.
        /// </summary>
        /// <param name="__dCycleTimeMilisec">The amount of time in ms between subsequent position updates.</param>
        /// <param name="__iSteps">The amount of pixels in a scan.</param>
        void Configure(double __dCycleTimeMilisec, int __iSteps);

        /// <summary>
        /// Setup stage - pass few variables to stage prior to starting the scanning
        /// <param name="__iTypeOfScan">The type of scan (0 - unidirectional, 1 - bidirectional, 2 - line scan, 3 - point scan)</param>
        /// <param name="__iFrameMarker">The frame synchronization marker that the galvo rises upon a beginning of a frame</param>
        /// <param name="__iLineMarker">The line synchronization marker that the galvo rises upon a beginning of a line</param>
        /// </summary>
        void Setup(int __iTypeOfScan, int __iFrameMarker, int __iLineMarker);        

        /// <summary>
        /// Release the stage hardware and free up all resources. Leaves the stage in a safe state.
        /// </summary>
        void Release();

        /// <summary>
        /// Move the stage to its home position.
        /// </summary>
        void Home();

        /// <summary>
        /// Perform an absolute move of the stage.
        /// </summary>
        /// <param name="__dXPosNm">Desired X coordinate in nm.</param>
        /// <param name="__dYPosNm">Desired Y coordinate in nm.</param>
        /// <param name="__dZPosNm">Desired Z coordinate in nm.</param>
        void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm);

        /// <summary>
        /// Perform a relative move of the stage.
        /// </summary>
        /// <param name="__dXPosNm">Desired relative X coordinate in nm</param>
        /// <param name="__dYPosNm">Desired relative Y coordinate in nm</param>
        /// <param name="__dZPosNm">Desired relative Z coordinate in nm</param>
        void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm);

        /// <summary>
        /// Perform a scan.
        /// </summary>
        /// <param name="__scmScanMode">Scanmode that holds all spatial information for a scan and defines it completely.</param>
        void Scan(Scanmode __scmScanMode, bool __bResend);

        /// <summary>
        /// Stop a scan in progress. Leaves the stage in a recoverable state.
        /// </summary>
        void Stop();

        #endregion
    }
}
