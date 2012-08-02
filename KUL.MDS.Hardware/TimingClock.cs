using System;
using System.Collections.Generic;
using System.Text;
using NationalInstruments.DAQmx;

namespace KUL.MDS.Hardware
{
    public class TimingClock
    {
        private Task m_daqtskGlobalSync;

        public TimingClock()
        {
        }

        /// <summary>
        /// Creates a global timing pulse train that will be made available on RTSI0. The Pulse train always has a duty cycle of .5
        /// </summary>
        /// <param name="__daqtskGlobalSync">The task object that will hold the task</param>
        /// <param name="__dFreq">The frequency in Hz at which all timed tasks will run</param>
        public void SetupClock(double __dFreq)
        {
            // Create a new task instance that will be passed to the globally available task.
            Task _daqtskTask = new Task();

            try
            {
                // Setup Global Sync Clock (GSC).
                // Commit before start to speed things up later on when the task needs to be started.
                _daqtskTask.COChannels.CreatePulseChannelFrequency("/Dev1/Ctr0", "Sync", COPulseFrequencyUnits.Hertz, COPulseIdleState.Low, 0.0, __dFreq, 0.1);
                _daqtskTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);
                _daqtskTask.Control(TaskAction.Verify);
                _daqtskTask.Control(TaskAction.Commit);

                // Be sure to route the timing pulse to the RTSI line to make it available on all the installed DAQ boards of the system.
                DaqSystem.Local.ConnectTerminals("/Dev1/Ctr0InternalOutput", "/Dev1/RTSI0");
                // Finally pass the task.
                this.m_daqtskGlobalSync = _daqtskTask;
            }

            catch (DaqException ex)
            {
                _daqtskTask.Dispose();
                this.m_daqtskGlobalSync = null;

                // Inform the user about the error.
                //MessageBox.Show(ex.Message, "Error creating the GSC!");
            }
        }

        /// <summary>
        /// Properly disposes of the global timing pulse train after measurements of moves
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
                //MessageBox.Show(ex.Message, "Error disposing the GSC!");
            }
        }

        /// <summary>
        /// Given a binning time for photon counting, a suitable frequency will be calculated to sync all tasks properly. 
        /// See Raman Imaging Timing Implementation.pptx in /Documentation/ folder for more details.
        /// </summary>
        /// <param name="__iBinTime">The photon binning time in ms</param>
        /// <returns></returns>
        public double Frequency(float __dBinTime, float __dPadTime)
        {
            // All operations in the program will be synchronized to one single clock.
            // An Edge will fire every x miliseconds. The edge timing depends on the time to scan every px plus a safety.
            // It is calculated here.
            double _dFreq = 1000 * (1 / (__dBinTime + __dPadTime));

            return _dFreq;
        }

        public void Start()
        {
            this.m_daqtskGlobalSync.Start();
        }

        public void Stop()
        {
            this.m_daqtskGlobalSync.Stop();
            this.m_daqtskGlobalSync.Dispose();
        }
    }
}
