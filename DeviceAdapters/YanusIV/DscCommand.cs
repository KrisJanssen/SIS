// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DscCommand.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Structure that represents a DSC command line
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware
{
    /// <summary>
    /// Structure that represents a DSC command line
    /// </summary>
    internal struct DscCommand
    {
        #region Fields

        /// <summary>
        /// The channel on which the command will act.
        /// </summary>
        public DscChannel Channel;

        /// <summary>
        /// The cycle in which the command will be executed .
        /// </summary>
        public ulong Cycle;

        /// <summary>
        /// The scan command defines the type of action to be performed.
        /// </summary>
        public ScanCommand ScanCmd;

        /// <summary>
        /// The value that will be assigned to the channel.
        /// </summary>
        public long Value;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DscCommand"/> struct.
        /// </summary>
        /// <param name="scanCommand">
        /// The scan command.
        /// </param>
        /// <param name="cycleNumber">
        /// The DSC cycle when the command should run.
        /// </param>
        /// <param name="channel">
        /// The DSC channel on which the command will act.
        /// </param>
        /// <param name="channelValue">
        /// The value that will be assigned to the channel
        /// </param>
        public DscCommand(ScanCommand scanCommand, ulong cycleNumber, DscChannel channel, long channelValue)
        {
            this.ScanCmd = scanCommand;
            this.Cycle = cycleNumber;
            this.Channel = channel;
            this.Value = channelValue;
        }

        #endregion
    }
}