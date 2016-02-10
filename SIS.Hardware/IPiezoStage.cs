﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPiezoStage.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   This interface provides a template to define any piezo stage hardware.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware
{
    using System;

    using SIS.ScanModes;

    /// <summary>
    ///     This interface provides a template to define any piezo stage hardware.
    /// </summary>
    public interface IPiezoStage
    {
        #region Public Events

        /// <summary>
        ///     Event thrown whenever the stage is switched on, or off.
        /// </summary>
        event EventHandler EngagedChanged;

        /// <summary>
        ///     Event thrown whenever the hardware generated an error.
        /// </summary>
        event EventHandler ErrorOccurred;

        /// <summary>
        ///     Event thrown whenever the stage changed position.
        /// </summary>
        event EventHandler PositionChanged;

        #endregion

        #region Public Properties

        int BufferSize { get; }

        /// <summary>
        ///     This string holds the last error generated by the hardware. "No Error" will be returned if no error occurred.
        /// </summary>
        string CurrentError { get; }

        /// <summary>
        ///     True if the stage hardware is initialized and ready for use. False otherwise.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        ///     True is the stage is moving.
        /// </summary>
        bool IsMoving { get; }

        /// <summary>
        ///     Returns the total number of single moves already performed during a scan.
        /// </summary>
        int SamplesWritten { get; }

        /// <summary>
        ///     Returns the current X position of the stage in nm.
        /// </summary>
        double XPosition { get; }

        /// <summary>
        ///     Returns the current Y position of the stage in nm.
        /// </summary>
        double YPosition { get; }

        /// <summary>
        ///     Returns the current Z position of the stage in nm.
        /// </summary>
        double ZPosition { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Configure the stage to perform scans at a set rate.
        /// </summary>
        /// <param name="__dCycleTimeMilisec">
        /// The amount of time in ms between subsequent position updates.
        /// </param>
        /// <param name="__iSteps">
        /// The amount of pixels in a scan.
        /// </param>
        void Configure(double __dCycleTimeMilisec, int __iSteps);

        /// <summary>
        ///     Move the stage to its home position.
        /// </summary>
        void Home();
        void Reset();
        /// <summary>
        ///     Initialize the stage hardware to prepare it for move and scan operations.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Perform an absolute move of the stage.
        /// </summary>
        /// <param name="__dXPosNm">
        /// Desired X coordinate in nm.
        /// </param>
        /// <param name="__dYPosNm">
        /// Desired Y coordinate in nm.
        /// </param>
        /// <param name="__dZPosNm">
        /// Desired Z coordinate in nm.
        /// </param>
        void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm, bool __bFast);

        /// <summary>
        /// Perform a relative move of the stage.
        /// </summary>
        /// <param name="__dXPosNm">
        /// Desired relative X coordinate in nm
        /// </param>
        /// <param name="__dYPosNm">
        /// Desired relative Y coordinate in nm
        /// </param>
        /// <param name="__dZPosNm">
        /// Desired relative Z coordinate in nm
        /// </param>
        void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm);

        /// <summary>
        ///     Release the stage hardware and free up all resources. Leaves the stage in a safe state.
        /// </summary>
        void Release();

        /// <summary>
        /// Perform a scan.
        /// </summary>
        /// <param name="__scmScanMode">Scanmode that holds all spatial information for a scan and defines it completely.</param>
        void Scan(Scanmode __scmScanMode, double __dPixelTime, bool __bResend, bool master, double __dRotation, int delay, bool wobble, double wobbleAmplitude, bool flip);

        /// <summary>
        ///     Stop a scan in progress. Leaves the stage in a recoverable state.
        /// </summary>
        void Stop();

        #endregion
    }
}
