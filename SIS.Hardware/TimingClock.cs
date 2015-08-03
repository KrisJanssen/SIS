namespace SIS.Hardware
{
    using NationalInstruments.DAQmx;

    /// <summary>
    /// GlobalSync provides a standardized way to coordinate multipe DIO and/or AO/AI operations.
    /// </summary>
    public class SampleClock
    {
        /// <summary>
        /// The Daq counter channel.
        /// </summary>
        private string m_counter;

        /// <summary>
        /// The Daq device.
        /// </summary>
        private string m_device;

        /// <summary>
        /// The main Daq Task.
        /// </summary>
        private Task m_sampleClock;

        /// <summary>
        /// Logging.
        /// </summary>
        private static readonly log4net.ILog m_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public SampleClock(string device, string counter)
        {
            this.m_device = device;
            this.m_counter = counter;
        }

        /// <summary>
        /// Start.
        /// </summary>
        public void Start(double period)
        {
            // Create a new task instance that will be passed to the globally available task.
            var _daqtskTask = new Task();

            try
            {
                // Setup Global Sync Clock (GSC).
                // Commit before start to speed things up later on when the task needs to be started.
                _daqtskTask.COChannels.CreatePulseChannelTime(
                    this.m_device + "/" + this.m_counter,
                    "SampleClock",
                    COPulseTimeUnits.Seconds,
                    COPulseIdleState.Low,
                    0.0,
                    period * 0.01,
                    period);

                _daqtskTask.Timing.SampleQuantityMode = SampleQuantityMode.ContinuousSamples;

                _daqtskTask.Control(TaskAction.Verify);
                _daqtskTask.Control(TaskAction.Commit);

                // Finally pass the task.
                this.m_sampleClock = _daqtskTask;
            }
            catch (DaqException ex)
            {
                _daqtskTask.Dispose();
                this.m_sampleClock = null;

                m_logger.Error("Problem creating SYNC!" + ex.Message);
            }

            try
            {
                this.m_sampleClock.Start();
            }
            catch(DaqException ex)
            {
                m_logger.Error("Error starting SYNC: " + ex.Message);
            }
        }

        /// <summary>
        /// top.
        /// </summary>
        public void Stop()
        {
            try
            {
                if (!this.m_sampleClock.IsDone)
                {
                    this.m_sampleClock.Stop();
                }

                this.m_sampleClock.Dispose();
                this.m_sampleClock = null;
            }
            catch(DaqException ex)
            {
                m_logger.Error("Error stopping SYNC: " + ex.Message);
            }
        }
    }
}