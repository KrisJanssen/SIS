//// --------------------------------------------------------------------------------------------------------------------
//// <copyright file="PhotoDiode.cs" company="Kris Janssen">
////   Copyright (c) 2014 Kris Janssen
//// </copyright>
//// <summary>
////   The photo diode.
//// </summary>
//// --------------------------------------------------------------------------------------------------------------------

//namespace SIS.Hardware
//{
//    using System;

//    using NationalInstruments.DAQmx;

//    using SIS.SystemLayer;

//    /// <summary>
//    /// The photo diode.
//    /// </summary>
//    public class PhotoDiode
//    {
//        // The Various NI-Daqmx tasks.

//        // Strings holding NI Channel names for Tasks.
//        #region Fields

//        /// <summary>
//        /// The m_s apdttl counter.
//        /// </summary>
//        private readonly string m_sAPDTTLCounter; /* "Ctr0" for analog */

//        /// <summary>
//        /// The m_s board id.
//        /// </summary>
//        private readonly string m_sBoardID; /* "Dev2" for analog */

//        /* "Dev2" for digital */

//        /// <summary>
//        /// The m_s pulse gen ctr.
//        /// </summary>
//        private readonly string m_sPulseGenCtr; /* "Ctr1" for analog */

//        /// <summary>
//        /// The m_s pulse gen time base.
//        /// </summary>
//        private readonly string m_sPulseGenTimeBase; /* "80MHzTimebase" for analog */

//        /// <summary>
//        /// The m_s pulse gen trigger.
//        /// </summary>
//        private readonly string m_sPulseGenTrigger; /* "RTSI0" for analog */

//        /* "PFI27" for digital */

//        // Counts read.
//        /// <summary>
//        /// The m_d total counts read.
//        /// </summary>
//        private long m_dTotalCountsRead;

//        /// <summary>
//        /// The m_daqtsk apd count.
//        /// </summary>
//        private Task m_daqtskAPDCount;

//        /// <summary>
//        /// The m_daqtsk timing pulse.
//        /// </summary>
//        private Task m_daqtskTimingPulse;

//        /// <summary>
//        /// The m_rdr count reader.
//        /// </summary>
//        private AnalogSingleChannelReader m_rdrCountReader;

//        /// <summary>
//        /// The m_s apd input line.
//        /// </summary>
//        private string m_sAPDInputLine; /* "PFI39" for analog */

//        #endregion

//        // Constructor
//        #region Constructors and Destructors

//        /// <summary>
//        /// Initializes a new instance of the <see cref="PhotoDiode"/> class. 
//        /// An APD Hardware object that takes care of photon counting.
//        /// </summary>
//        /// <param name="__sBoardID">
//        /// The ID of the NI counter board to be used. This board should have at least 2 counters and 2
//        ///     PFI lines, eg. Dev2.
//        /// </param>
//        /// <param name="__sPulseGenCtr">
//        /// The counter to take care of generating a block pulse that delimits bin time, eg. Ctr1.
//        /// </param>
//        /// <param name="__sPulseGenTimeBase">
//        /// The timebase of the board. Preferably use a board that has 80MHzTimebase for optimal
//        ///     time resolution.
//        /// </param>
//        /// <param name="__sPulseGenTrigger">
//        /// The trigger to fire one block pulse with bintime length. For analog this should be
//        ///     RTSI0, for digital this is a trigger line on a PFI.
//        /// </param>
//        /// <param name="__sAPDTTLCounter">
//        /// The counter that counts the actual photons, eg. Ctr0.
//        /// </param>
//        public PhotoDiode(
//            string __sBoardID, 
//            string __sPulseGenCtr, 
//            string __sPulseGenTimeBase, 
//            string __sPulseGenTrigger, 
//            string __sAPDTTLCounter)
//        {
//            this.m_sBoardID = __sBoardID;
//            this.m_sPulseGenCtr = __sPulseGenCtr;
//            this.m_sPulseGenTimeBase = __sPulseGenTimeBase;
//            this.m_sPulseGenTrigger = __sPulseGenTrigger;
//            this.m_sAPDTTLCounter = __sAPDTTLCounter;
//            this.m_dTotalCountsRead = 0;
//        }

//        #endregion

//        #region Public Properties

//        /// <summary>
//        /// Gets the total samples acuired.
//        /// </summary>
//        public long TotalSamplesAcuired
//        {
//            get
//            {
//                return this.m_dTotalCountsRead;
//            }
//        }

//        #endregion

//        #region Public Methods and Operators

//        /// <summary>
//        /// Returns all counts in buffer.
//        /// </summary>
//        /// <returns>
//        /// The <see cref="double[]"/>.
//        /// </returns>
//        public double[] Read()
//        {
//            double[] _dValues = this.m_rdrCountReader.ReadMultiSample(-1);
//            this.m_dTotalCountsRead = this.m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;
//            return _dValues;
//        }

//        /// <summary>
//        /// Prepares the APD hardware for a specific image acquisition.
//        /// </summary>
//        /// <param name="__dBinTimeMilisec">
//        /// The __d Bin Time Milisec.
//        /// </param>
//        /// <param name="__iSteps">
//        /// The total number of pixels to acquire.
//        /// </param>
//        public void SetupAPDCountAndTiming(double __dBinTimeMilisec, int __iSteps)
//        {
//            try
//            {
//                // Calculate how many ticks the photon counting should take.
//                int _iBinTicks = Convert.ToInt32(__dBinTimeMilisec * 80000);

//                // Create new task instances that will be passed to the private member tasks.
//                var _daqtskTiming = new Task();
//                var _daqtskAPD = new Task();

//                // Setup a pulsechannel that will determine the bin time for photon counts.
//                // This channel will create a single delayed edge upon triggering by the global sync pulsetrain or another source. 
//                // This edge can be repeated the required number of times, each time triggering a single AI event.
//                // High time of the pulse determines bin time.
//                _daqtskTiming.COChannels.CreatePulseChannelTicks(
//                    "/" + this.m_sBoardID + "/" + this.m_sPulseGenCtr, 
//                    "TimedPulse", 
//                    "/" + this.m_sBoardID + "/" + this.m_sPulseGenTimeBase, 
//                    COPulseIdleState.Low, 
//                    (_iBinTicks / 500) * 200, 
//                    _iBinTicks / 500, 
//                    _iBinTicks / 500);

//                // We want to sync voltage out to Analog Piezo or  Digital Piezo with measurement without software intervention.
//                // Therefore we tap into the global sync pulsetrain of another timing source which is available from the RTSI cable (analog)
//                // or a PFI line (digital) to sync photon counting with movement.
//                // For each pixel a single pulse with a high duration equal to the photon binning time will be generated.
//                _daqtskTiming.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(
//                    "/" + this.m_sBoardID + "/" + this.m_sPulseGenTrigger, 
//                    DigitalEdgeStartTriggerEdge.Rising);

//                // This trigger will occur for every pixel so it should be retriggerable.
//                _daqtskTiming.Triggers.StartTrigger.Retriggerable = true;
//                _daqtskTiming.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
//                _daqtskTiming.Control(TaskAction.Verify);
//                _daqtskTiming.Control(TaskAction.Commit);

//                // Setup AItask for the actual timed Voltage Sampling.
//                // We will actually measure the width of the counting timing pulse in # of TTLs of the APD, thus effectively counting photons.
//                _daqtskAPD.AIChannels.CreateVoltageChannel(
//                    "/" + this.m_sBoardID + "/" + this.m_sAPDTTLCounter, 
//                    "PhotoDiode", 
//                    AITerminalConfiguration.Nrse, 
//                    -10.0d, 
//                    10.0d, 
//                    AIVoltageUnits.Volts);

//                // _daqtskAPD.Timing.ConfigureSampleClock(
//                // "/" + m_sBoardID + "/" + m_sPulseGenCtr + "InternalOutput", 
//                // 1000.0d, 
//                // SampleClockActiveEdge.Rising, 
//                // SampleQuantityMode.FiniteSamples, 
//                // 1000);
//                _daqtskAPD.Timing.ConfigureSampleClock(
//                    "/" + this.m_sBoardID + "/RTSI0", 
//                    1000.0d, 
//                    SampleClockActiveEdge.Rising, 
//                    SampleQuantityMode.FiniteSamples, 
//                    1000);

//                // We only want to collect as many counts as there are pixels or "steps" in the image.
//                // Every time we read from the buffer we will read all samples that are there at once.
//                // _daqtskAPD.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);
//                _daqtskAPD.Stream.ReadAllAvailableSamples = true;

//                // Commit before start to speed things up.
//                _daqtskAPD.Control(TaskAction.Verify);
//                _daqtskAPD.Control(TaskAction.Commit);

//                // Finally pass the tasks.
//                this.m_daqtskTimingPulse = _daqtskTiming;
//                this.m_daqtskAPDCount = _daqtskAPD;
//            }
//            catch (DaqException ex)
//            {
//                this.m_daqtskTimingPulse = null;
//                this.m_daqtskAPDCount = null;

//                // Inform the user about the error.
//                Tracing.Ping(ex.Message);
//            }
//        }

//        /// <summary>
//        ///     Prime the APD for counting. Every time it receives a trigger it will now count photons for a set bintime.
//        ///     The count will be stored in a buffer and can be read at any time.
//        /// </summary>
//        public void StartAPDAcquisition()
//        {
//            // Reset the total amount of counts read.
//            this.m_dTotalCountsRead = 0;

//            // Create an instance of a reader to get counts from the HW buffer.
//            this.m_rdrCountReader = new AnalogSingleChannelReader(this.m_daqtskAPDCount.Stream);

//            // Start the task that counts the TTL's coming from the APD first.
//            this.m_daqtskAPDCount.Start();

//            // Now start the pulse with duration of bintime. The length of this pulse will be measured in TTL ticks from the actual APD.
//            this.m_daqtskTimingPulse.Start();
//        }

//        /// <summary>
//        ///     Stops the running acquisition and releases all resources.
//        /// </summary>
//        public void StopAPDAcquisition()
//        {
//            // Stop the pulse whose width is measured in TTL's coming from the APD.
//            this.m_daqtskTimingPulse.Stop();

//            // Stop the task that counts the TTL's
//            this.m_daqtskAPDCount.Stop();

//            this.m_dTotalCountsRead = this.m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;

//            // Free the resources used.
//            this.m_daqtskTimingPulse.Control(TaskAction.Unreserve);
//            this.m_daqtskAPDCount.Control(TaskAction.Unreserve);

//            // Dispose of the tasks.
//            this.m_daqtskTimingPulse.Dispose();
//            this.m_daqtskAPDCount.Dispose();
//        }

//        #endregion
//    }
//}