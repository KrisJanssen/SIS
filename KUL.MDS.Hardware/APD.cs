using System;
using System.Collections.Generic;
using System.Text;
using NationalInstruments.DAQmx;

namespace KUL.MDS.Hardware
{
    public class APD
    {
        // The Various NI-Daqmx tasks.
        private Task m_daqtskAPDCount;
        private Task m_daqtskTimingPulse;

        // Strings holding NI Channel names for Tasks.
        private string m_sBoardID;          /* "Dev2" for analog */
                                            /* "Dev2" for digital */
        private string m_sPulseGenCtr;      /* "Ctr1" for analog */
        private string m_sPulseGenTimeBase; /* "80MHzTimebase" for analog */
        private string m_sPulseGenTrigger;  /* "RTSI0" for analog */
                                            /* "PFI27" for digital */
        private string m_sAPDTTLCounter;    /* "Ctr0" for analog */
        private string m_sAPDInputLine;     /* "PFI39" for analog */
                                            /* "PFI39" for digital */
        // Count readers.
        private CounterReader m_rdrCountReader;

        // Counts read.
        private long m_dTotalCountsRead;

        public long TotalSamplesAcuired
        {
            get
            {
                return this.m_dTotalCountsRead;
            }
        }

        // Constructor
        /// <summary>
        /// An APD Hardware object that takes care of photon counting.
        /// </summary>
        /// <param name="__sBoardID">The ID of the NI counter board to be used. This board should have at least 2 counters and 2 PFI lines, eg. Dev2.</param>
        /// <param name="__sPulseGenCtr">The counter to take care of generating a block pulse that delimits bin time, eg. Ctr1.</param>
        /// <param name="__sPulseGenTimeBase">The timebase of the board. Preferably use a board that has 80MHzTimebase for optimal time resolution.</param>
        /// <param name="__sPulseGenTrigger">The trigger to fire one block pulse with bintime length. For analog this should be RTSI0, for digital this is a trigger line on a PFI.</param>
        /// <param name="__sAPDTTLCounter">The counter that counts the actual photons, eg. Ctr0.</param>
        /// <param name="__sAPDInputLine">The PFI line where TTL signals from the detector arrive for counting, eg PFI39.</param>
        public APD(
            string __sBoardID, 
            string __sPulseGenCtr, 
            string __sPulseGenTimeBase, 
            string __sPulseGenTrigger,
            string __sAPDTTLCounter,
            string __sAPDInputLine)
        {
            this.m_sBoardID = __sBoardID;
            this.m_sPulseGenCtr = __sPulseGenCtr;
            this.m_sPulseGenTimeBase = __sPulseGenTimeBase;
            this.m_sPulseGenTrigger = __sPulseGenTrigger;
            this.m_sAPDTTLCounter = __sAPDTTLCounter;
            this.m_sAPDInputLine = __sAPDInputLine;
            this.m_dTotalCountsRead = 0;
        }

        /// <summary>
        /// Prepares the APD hardware for a specific image acquisition.
        /// </summary>
        /// <param name="__dBinTimems">The photon counting bin time in miliseconds. This time is padded by 80 ticks of an 80MHz clock so be sure that the stage cycle time is bigger than this!</param>
        /// <param name="__iSteps">The total number of pixels to acquire.</param>
        public void SetupAPDCountAndTiming(double __dBinTimeMilisec, int __iSteps)
        {
            try
            {
                // Calculate how many ticks the photon counting should take.
                int _iBinTicks = Convert.ToInt32(__dBinTimeMilisec * 80000);

                // Create new task instances that will be passed to the private member tasks.
                Task _daqtskTiming = new Task();
                Task _daqtskAPD = new Task();

                // Setup a pulsechannel that will determine the bin time for photon counts.
                // This channel will create a single delayed edge upon triggering by the global sync pulsetrain or another source. 
                // High time of the pulse determines bin time.
                _daqtskTiming.COChannels.CreatePulseChannelTicks(
                    "/" + m_sBoardID + "/" + m_sPulseGenCtr, 
                    "TimedPulse", 
                    "/" + m_sBoardID + "/" + m_sPulseGenTimeBase, 
                    COPulseIdleState.Low, 
                    80, 
                    80, 
                    _iBinTicks);

                // We want to sync voltage out to Analog Piezo or  Digital Piezo with measurement without software intervention.
                // Therefore we tap into the global sync pulsetrain of another timing source which is available from the RTSI cable (analog)
                // or a PFI line (digital) to sync photon counting with movement.
                // For each pixel a single pulse with a high duration equal to the photon binning time will be generated.
                _daqtskTiming.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger(
                    "/" + m_sBoardID + "/" + m_sPulseGenTrigger, 
                    DigitalEdgeStartTriggerEdge.Rising);

                // This trigger will occur for every pixel so it should be retriggerable.
                _daqtskTiming.Triggers.StartTrigger.Retriggerable = true;
                _daqtskTiming.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);

                // Be sure to route the timing pulse to the RTSI line to make it available on all the installed DAQ boards of the system.
                // For syncing of other detection processess.
                DaqSystem.Local.ConnectTerminals("/Dev1/Ctr0InternalOutput", "/Dev1/RTSI0");

                _daqtskTiming.Control(TaskAction.Verify);
                _daqtskTiming.Control(TaskAction.Commit);

                // Setup countertask for the actual timed APD counting.
                // We will actually measure the width of the counting timing pulse in # of TTLs of the APD, thus effectively counting photons.
                _daqtskAPD.CIChannels.CreatePulseWidthChannel(
                    "/" + m_sBoardID + "/" + m_sAPDTTLCounter, 
                    "CountAPD", 
                    0.0, 
                    1000000, 
                    CIPulseWidthStartingEdge.Rising, 
                    CIPulseWidthUnits.Ticks);

                // On this terminal the timing pulse, that defines the bintime, will come in so that it's width can be counted.
                _daqtskAPD.CIChannels.All.PulseWidthTerminal = "/" + m_sBoardID + "/" + m_sPulseGenCtr + "InternalOutput";

                // On this line the TTLs from the APD will come in.
                _daqtskAPD.CIChannels.All.CounterTimebaseSource = "/" + m_sBoardID + "/" + m_sAPDInputLine;

                // TODO: Evaluate the behavior of this setting. It might be necessary to deal with zero counts correctly although this is not 
                // likeley because an APD probably has non-zero dark count!
                _daqtskAPD.CIChannels.All.DuplicateCountPrevention = true;

                // We only want to collect as many counts as there are pixels or "steps" in the image.
                // Every time we read from the buffer we will read all samples that are there at once.
                _daqtskAPD.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, __iSteps);
                _daqtskAPD.Stream.ReadAllAvailableSamples = true;

                // Commit before start to speed things up.
                _daqtskAPD.Control(TaskAction.Verify);
                _daqtskAPD.Control(TaskAction.Commit);

                // Finally pass the tasks.
                this.m_daqtskTimingPulse = _daqtskTiming;
                this.m_daqtskAPDCount = _daqtskAPD;
            }

            catch (DaqException ex)
            {
                this.m_daqtskTimingPulse = null;
                this.m_daqtskAPDCount = null;

                // Inform the user about the error.
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Prime the APD for counting. Every time it receives a trigger it will now count photons for a set bintime. 
        /// The count will be stored in a buffer and can be read at any time.
        /// </summary>
        public void StartAPDAcquisition()
        {
            // Reset the total amount of counts read.
            this.m_dTotalCountsRead = 0;

            // Create an instance of a reader to get counts from the HW buffer.
            m_rdrCountReader = new CounterReader(this.m_daqtskAPDCount.Stream);

            // Start the task that counts the TTL's coming from the APD first.
            m_daqtskAPDCount.Start();

            // Now start the pulse with duration of bintime. The length of this pulse will be measured in TTL ticks from the actual APD.
            m_daqtskTimingPulse.Start();
        }

        /// <summary>
        /// Returns all counts in buffer.
        /// </summary>
        /// <returns></returns>
        public UInt32[] Read()
        {
            UInt32[] _ui32Values = this.m_rdrCountReader.ReadMultiSampleUInt32(-1);
            this.m_dTotalCountsRead = m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;
            return _ui32Values;
        }

        /// <summary>
        /// Stops the running acquisition and releases all resources.
        /// </summary>
        public void StopAPDAcquisition()
        {
            // Stop the pulse whose width is measured in TTL's coming from the APD.
            m_daqtskTimingPulse.Stop();
            // Stop the task that counts the TTL's
            m_daqtskAPDCount.Stop();

            this.m_dTotalCountsRead = m_daqtskAPDCount.Stream.TotalSamplesAcquiredPerChannel;

            // Free the resources used.
            m_daqtskTimingPulse.Control(TaskAction.Unreserve);
            m_daqtskAPDCount.Control(TaskAction.Unreserve);

            // Dispose of the tasks.
            m_daqtskTimingPulse.Dispose();
            m_daqtskAPDCount.Dispose();
        }
    }
}
