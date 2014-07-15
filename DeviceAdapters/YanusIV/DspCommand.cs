// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DspCommand.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The DSP command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// The DSP command.
    /// </summary>
    internal enum DspCommand
    {
        /// <summary>
        /// Do nothing but keeps the interface busy and signals. 
        /// Break of protocol execution; can also serve as comment/marker.
        /// No arguments.
        /// </summary>
        DSP_CMD_DO_NOTHING = '#', 

        /// <summary>
        /// Return info tells DSP to return null terminated information string (about YanusIV).
        /// No arguments.
        /// </summary>
        DSP_CMD_RETURN_INFO = 'R', 

        /// <summary>
        /// Clear the scan control protocol.
        /// No arguments.
        /// Returns 0 on succes.
        /// </summary>
        DSP_CMD_CLEAR_PROT = 'C', 

        /// <summary>
        /// Add a command to the control protocol.
        /// Arguments:
        /// char representing the scanner control command.
        /// cycles_t representing the cycle as long.
        /// int representing the channel.
        /// int representing the value.
        /// All arguments must be present. Even if they do not apply to the scan command.
        /// Returns 0 on success or an error value.
        /// </summary>
        DSP_CMD_ADD_PROT_CMD = 'A', 

        /// <summary>
        /// Initialize protocol: sort commands within blocks ... currently not functional.
        /// </summary>
        DSP_CMD_INIT_PROT = 'I', 

        /// <summary>
        /// Execute the current protocol.
        /// No arguments.
        /// Returns 0 on success or a numerical error code.
        /// </summary>
        DSP_CMD_EXECUTE_PROT = 'X', 

        /// <summary>
        /// List loaded protocol.
        /// No arguments.
        /// </summary>
        DSP_CMD_LIST_PROT = 'L', 

        /// <summary>
        /// Show the buffer for scan channel "channel".
        /// Argument:
        /// int representing the channel.
        /// </summary>
        DSP_CMD_SHOW_BUFFER = 'B', 

        /// <summary>
        /// Set the value of a scan channel directly.
        /// Like the "V" command, but the current protocol remains unchanged and the value is set immediately after parsing the line.
        /// Arguments:
        /// int representing the channel
        /// long representing the value
        /// Returns 0 on success or a numerical error code.
        /// </summary>
        DSP_CMD_SET_VALUE = 'V'
    }
}