// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimingClock.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The timing clock.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Hardware
{
    using NationalInstruments.DAQmx;

    /// <summary>
    /// The timing clock.
    /// </summary>
    public class TimingClock
    {
        #region Fields

        /// <summary>
        /// The m_daqtsk global sync.
        /// </summary>
        private Task m_daqtskGlobalSync;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Properly disposes of the global timing pulse train after measurements of moves
        /// </summary>
        /// <param name="__daqtskGlobalSync">The task object that will hold the task</param>
        public void DestroyClock()
        {
            try
            {
                this.m_daqtskGlobalSync.Stop();
                this.m_daqtskGlobalSync.Dispose();
                this.m_daqtskGlobalSync = null;
            }
            catch (DaqException ex)
            {
                this.m_daqtskGlobalSync = null;

                // Inform the user about the error.
                // MessageBox.Show(ex.Message, "Error disposing the GSC!");
            }
        }

        /// <summary>
        /// Creates a global timing pulse train that will be made available on RTSI0. The Pulse train always has a duty cycle
        ///     of .5
        /// </summary>
        /// <param name="__dFreq">
        /// The frequency in Hz at which all timed tasks will run
        /// </param>
        public void SetupClock(string __sCounter, double __dFreq)
        {
            // Create a new task instance that will be passed to the globally available task.
            var _daqtskTask = new Task();

            try
            {
                // Setup Global Sync Clock (GSC).
                // Commit before start to speed things up later on when the task needs to be started.
                _daqtskTask.COChannels.CreatePulseChannelFrequency(
                    "/Dev1/" + __sCounter, 
                    "Sync", 
                    COPulseFrequencyUnits.Hertz, 
                    COPulseIdleState.Low, 
                    0.0, 
                    __dFreq, 
                    0.5);
                _daqtskTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);
                _daqtskTask.Control(TaskAction.Verify);
                _daqtskTask.Control(TaskAction.Commit);

                // Be sure to route the timing pulse to the RTSI line to make it available on all the installed DAQ boards of the system.
                //DaqSystem.Local.ConnectTerminals("/Dev1/Ctr0InternalOutput", "/Dev1/RTSI0");

                // Finally pass the task.
                this.m_daqtskGlobalSync = _daqtskTask;
            }
            catch (DaqException ex)
            {
                _daqtskTask.Dispose();
                this.m_daqtskGlobalSync = null;

                // Inform the user about the error.
                // MessageBox.Show(ex.Message, "Error creating the GSC!");
            }
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void Start()
        {
            this.m_daqtskGlobalSync.Start();
        }

        /// <summary>
        /// The stop.
        /// </summary>
        public void Stop()
        {
            this.m_daqtskGlobalSync.Stop();
            this.m_daqtskGlobalSync.Dispose();
        }

        #endregion
    }
}