// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DspError.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   An enumeration of all error codes that can be returned by the Yanus IV DSC.
//   Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware
{
    /// <summary>
    /// An enumeration of all error codes that can be returned by the Yanus IV DSC.
    /// Codes are returned by the DSC via RS232 as decimal numbers in ASCII representation.
    /// </summary>
    public enum DspError
    {
        /// <summary>
        /// No error.
        /// </summary>
        SCAN_CMD_NO_ERROR = 0, 

        /// <summary>
        /// Calculation timeout (calculation took > 10 us).
        /// </summary>
        SCAN_CMD_RUN_TIMEOUT = 1, 

        /// <summary>
        /// Scan aborted by sending character on RS232.
        /// </summary>
        SCAN_CMD_RUN_ABORTED = 2, 

        /// <summary>
        /// Command list empty.
        /// </summary>
        SCAN_CMD_LIST_EMPTY = 3, 

        /// <summary>
        /// Command list with unclosed loops.
        /// </summary>
        SCAN_CMD_LIST_NOT_CLOSED = 4, 

        /// <summary>
        /// Scan list exceeded maximum length.
        /// </summary>
        SCAN_CMD_LIST_OVERFLOW = 10, 

        /// <summary>
        /// Incorrect cycle time for command.
        /// </summary>
        SCAN_CMD_LIST_DISORDER = 11, 

        /// <summary>
        /// Invalid channel.
        /// </summary>
        SCAN_CMD_INVALID_CHANNEL = 12, 

        /// <summary>
        /// Too many nested loops.
        /// </summary>
        SCAN_CMD_LOOP_OVERFLOW = 13, 

        /// <summary>
        /// The sca n_ cm d_ invali d_ iterations.
        /// </summary>
        SCAN_CMD_INVALID_ITERATIONS = 14, 

        /// <summary>
        /// Loop is already closed.
        /// </summary>
        SCAN_CMD_LOOP_IS_CLOSED = 15, 

        /// <summary>
        /// Unknown command.
        /// </summary>
        SCAN_CMD_UNKONWN_COMMAND = 16, 

        /// <summary>
        /// No buffer available for command.
        /// </summary>
        SCAN_CMD_NO_BUFFER = 17, 

        /// <summary>
        /// Invalid syntax.
        /// Not enough parameters or too many parameters.
        /// </summary>
        SCAN_CMD_INVALID_SYNTAX = 18, 

        /// <summary>
        /// Com port closed and DSP commands cannot be sent..
        /// </summary>
        SCAN_CMD_COM_PORT_CLOSED = 19, 

        /// <summary>
        /// Galvo is off and DSP commands cannot be sent.
        /// </summary>
        SCAN_CMD_COM_GALVO_IS_OFF = 20
    }
}