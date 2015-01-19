﻿using System;
using System.Text;
using System.Linq;
using System.Threading;
using KUL.MDS.ScanModes;
using KUL.MDS.SystemLayer;

using NationalInstruments.DAQmx;

namespace KUL.MDS.Hardware
{
    using System.Net.Mime;
    using System.Xml.Serialization;

    public class PIAnalogStage : IPiezoStage
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        # region Essential Member Objects.

        // The NI Task object that will handle actual stage control.
        // LineTrigger handles a multi bit 'bus' that can be programmed 
        // on a per-pixel basis. This can be used to modulate the main 
        // pulse train for external HW.
        // MasterClock handles the global synchronizing pulse train.
        // MoveStage handles analog output.
        private Task m_daqtskLineTrigger;
        private Task m_daqtskMasterClock;
        private Task m_daqtskMoveStage;

        // Current array of coordinates for timed stage motion
        private double[,] m_dGeneratorCoordinates;

        #endregion

        #region Constant Stage Parameters.

        // Constant properties of the stage. These will be used in input validation and safe speed calculation for stage movement.
        private const double m_dNmPVolt = 10000.0;
        private const double m_dMaxPosition = 90000.0;

        // Set global range for the Voltage outputs as an additional safety.
        private const double m_dVoltageMax = 10.0;
        private const double m_dVoltageMin = 0.0;

        #endregion

        #region Members.

        // Create variables to keep track of the currently set voltage to the Piezo stage.
        private double m_dCurrentVoltageX;
        private double m_dCurrentVoltageY;

        // The UI will display data on the acquisition progress during the scan.
        // More specifically, total samples currently sent to stage and total samples taken from APD.
        private int m_iSamplesToStageCurrent;

        // Status of the stage
        private bool m_bIsInitialized;

        #endregion

        #region Properties.

        /// <summary>
        /// This string holds the last error generated by the hardware. "No Error" will be returned if no error occurred.
        /// </summary>
        public string CurrentError
        {
            get
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Returns the current X position of the stage in nm.
        /// </summary>
        public double XPosition
        {
            get
            {
                if (this.m_bIsInitialized)
                {
                    return m_dCurrentVoltageX;
                    //return this.m_iSamplesToStageCurrent;
                    //double[] _dPositions = new double[3];
                    //this.IsError(E7XXController.qPOS(this.m_iControllerID, "123", _dPositions));
                    //return _dPositions[0] * 1000;
                }
                else
                {
                    return -1.0;
                }
            }
        }

        /// <summary>
        /// Returns the current Y position of the stage in nm.
        /// </summary>
        public double YPosition
        {
            get
            {
                if (this.m_bIsInitialized)
                {
                    return m_dCurrentVoltageY;
                    //return this.m_iSamplesToStageCurrent;
                    //double[] _dPositions = new double[3];
                    //this.IsError(E7XXController.qPOS(this.m_iControllerID, "123", _dPositions));
                    //return _dPositions[1] * 1000;
                }
                else
                {
                    return -1.0;
                }
            }
        }

        /// <summary>
        /// Returns the current Y position of the stage in nm.
        /// </summary>
        public double ZPosition
        {
            get
            {
                if (this.m_bIsInitialized)
                {
                    return 0.0;
                    //return this.m_iSamplesToStageCurrent;
                    //double[] _dPositions = new double[3];
                    //this.IsError(E7XXController.qPOS(this.m_iControllerID, "123", _dPositions));
                    //return _dPositions[2] * 1000;
                }
                else
                {
                    return -1.0;
                }
            }
        }

        /// <summary>
        /// Returns the total number of moves already performed during a scan.
        /// </summary>
        public int SamplesWritten
        {
            get
            {
                return this.m_iSamplesToStageCurrent;
            }
        }

        /// <summary>
        /// True if the stage hardware is initialized and ready for use. False otherwise.
        /// </summary>
        public bool IsInitialized
        {
            get
            {
                return this.m_bIsInitialized;
            }
        }

        /// <summary>
        /// True is the stage is moving.
        /// </summary>
        public bool IsMoving
        {
            get
            {
                return true;
                //if (this.Moving() || this.GeneratorRunning())
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
        }

        #endregion

        #region Events.

        /// <summary>
        /// Event thrown whenever the stage changed position.
        /// </summary>
        public event EventHandler PositionChanged;

        /// <summary>
        /// Event thrown whenever the hardware generated an error.
        /// </summary>
        public event EventHandler ErrorOccurred;

        /// <summary>
        /// Event thrown whenever the stage is switched on, or off.
        /// </summary>
        public event EventHandler EngagedChanged;

        #endregion

        #region Delegates.

        //private delegate void UIUpdateDelegate();
        //private delegate void ProgressUpdate(int _iProgress);

        #endregion

        #region Singleton Pattern.

        // This class operates according to a singleton pattern. 
        // Having multiple PIAnalogStage could be dangerous because 
        // the hardware could be left in an unknown state.
        private static volatile PIAnalogStage m_instance;
        private static object m_syncRoot = new object();

        public static PIAnalogStage Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new PIAnalogStage();
                        }
                    }
                }

                return m_instance;
            }
        }
        #endregion

        #region Methods.

        // The constructor obviously needs to be private to prevent normal instantiation.
        private PIAnalogStage()
        {
            // The PIAnalogStage object should be instantiated in an uninitialized state.
            this.m_bIsInitialized = false;
        }

        public void Initialize()
        {
            _logger.Info("Initializing analog Piezo....");

            // Setup an Analog Out task to move the Piezo stage along X and Y.
            Task _daqtskTask = new Task();

            try
            {
                // Add AO channels.
                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao0", "aoChannelX", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao1", "aoChannelY", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao2", "aoChannelZ", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);

                // checked IFilteredTypeDescriptor everything is OK.
                _daqtskTask.Control(TaskAction.Verify);

                // Assign the task.
                this.m_daqtskMoveStage = _daqtskTask;

                // Return a status indication for the stage.
                this.m_bIsInitialized = true;
            }

            catch (DaqException exception)
            {
                if (_daqtskTask != null)
                {
                    _daqtskTask.Dispose();
                }

                this.m_bIsInitialized = false;

                _logger.Error("Unable to connect set up AO channels for Move task!");
            }

            // If everything went well, tell everyone :)
            if (EngagedChanged != null)
            {
                EngagedChanged(this, new EventArgs());
            }
        }

        public void Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            _logger.Info("Configuring stage timing....");

            if (this.m_daqtskMasterClock != null)
            {
                if (this.m_daqtskMasterClock.IsDone != true)
                {
                    this.m_daqtskMasterClock.Stop();
                }

                this.m_daqtskMasterClock.Control(TaskAction.Unreserve);
            }

            if (this.m_daqtskLineTrigger != null)
            {
                if (this.m_daqtskLineTrigger.IsDone != true)
                {
                    this.m_daqtskLineTrigger.Stop();
                }

                this.m_daqtskLineTrigger.Control(TaskAction.Unreserve);
            }

            Task _timingTask = new Task();
            Task _lineTask = new Task();

            try
            {
                double dCycleDuration = __dCycleTimeMilisec * 100000 / 2;

                if (this.m_daqtskMoveStage == null)
                {
                    this.Initialize();
                }

                _timingTask.COChannels.CreatePulseChannelTicks(
                    "/Dev1/Ctr2",
                    "MasterClk",
                    "/Dev1/100MHzTimebase",
                    COPulseIdleState.Low,
                    2,
                    (int)dCycleDuration - 2,
                    (int)dCycleDuration);

                _timingTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, __iSteps);

                _timingTask.Control(TaskAction.Verify);
                _timingTask.Control(TaskAction.Commit);

                //this.m_daqtskMoveStage.Timing.SampleClockRate = 1000 / __dCycleTimeMilisec;

                this.m_daqtskMoveStage.Timing.ConfigureSampleClock(
                    "/Dev1/Ctr2InternalOutput",
                    1000 / __dCycleTimeMilisec,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    __iSteps);

                this.m_daqtskMoveStage.Timing.SampleTimingType = SampleTimingType.SampleClock;

                _lineTask.DOChannels.CreateChannel(
                    "Dev1/port0",
                    "test",
                    ChannelLineGrouping.OneChannelForAllLines);

                _lineTask.Timing.ConfigureSampleClock(
                    "/Dev1/Ctr2InternalOutput",
                    1000 / __dCycleTimeMilisec,
                    SampleClockActiveEdge.Rising,
                    SampleQuantityMode.FiniteSamples,
                    __iSteps);

                _lineTask.Timing.SampleTimingType = SampleTimingType.SampleClock;

                _lineTask.Control(TaskAction.Verify);
                _lineTask.Control(TaskAction.Commit);
            }

            catch (DaqException ex)
            {
                if (_timingTask != null)
                {
                    _timingTask.Dispose();
                }
                if (_lineTask != null)
                {
                    _lineTask.Dispose();
                }

                _logger.Error("Error while setting timing!", ex);
            }

            if (_timingTask != null)
            {
                this.m_daqtskMasterClock = _timingTask;
            }

            if (_lineTask != null)
            {
                this.m_daqtskLineTrigger = _lineTask;
            }
        }

        public void Release()
        {
            this.MoveAbs(0.0, 0.0, 0.0);

            try
            {
                if (this.m_daqtskMasterClock != null)
                {
                    this.m_daqtskMasterClock.Stop();
                    this.m_daqtskMasterClock.Control(TaskAction.Unreserve);
                    this.m_daqtskMasterClock.Dispose();
                    this.m_daqtskMasterClock = null;
                }

                // Properly dispose of the AO tasks that control the piezo stage.
                if (this.m_daqtskMoveStage != null)
                {
                    this.m_daqtskMoveStage.Stop();
                    this.m_daqtskMoveStage.Control(TaskAction.Unreserve);
                    this.m_daqtskMoveStage.Dispose();
                    this.m_daqtskMoveStage = null;
                }

                if (this.m_daqtskLineTrigger != null)
                {
                    this.m_daqtskLineTrigger.Stop();
                    this.m_daqtskLineTrigger.Control(TaskAction.Unreserve);
                    this.m_daqtskLineTrigger.Dispose();
                    this.m_daqtskLineTrigger = null;
                }

                // Return a status indication for the stage.
                this.m_bIsInitialized = false;
            }

            catch (DaqException ex)
            {
                //MessageBox.Show(ex.Message);
                this.m_bIsInitialized = false;
            }

            // If everything went well, tell everyone :)
            if (EngagedChanged != null)
            {
                EngagedChanged(this, new EventArgs());
            }
        }

        public void Home()
        {
            this.MoveAbs(0.0, 0.0, 0.0);
        }

        // Calculates voltages for a direct move to a set of XY coordinates.
        private void CalculateMove(double __dInitVoltageX, double __dInitVoltageY, double __dFinVoltageX, double __dFinVoltageY, int __iSteps)
        {
            // Init some variables.
            double _dCurrentVoltageX = __dInitVoltageX;
            double _dCurrentVoltageY = __dInitVoltageY;

            // Calculate the voltage resolution.
            double _dVoltageResX = (__dFinVoltageX - __dInitVoltageX) / __iSteps;
            double _dVoltageResY = (__dFinVoltageY - __dInitVoltageY) / __iSteps;

            // Array to store the voltages for the entire move operation.
            double[,] _dMovement = new double[3, __iSteps];

            // Calculate the actual voltages for the intended movement on X.
            // Movement will be one axis at a time.
            for (int _iI = 0; _iI < __iSteps; _iI++)
            {
                // Increment voltage. 
                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
                _dCurrentVoltageX = Math.Round((__dInitVoltageX + _dVoltageResX * (_iI + 1)), 4);

                // Increment voltage. 
                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
                _dCurrentVoltageY = Math.Round((__dInitVoltageY + _dVoltageResY * (_iI + 1)), 4);

                // Write voltage for X.
                _dMovement[0, _iI] = _dCurrentVoltageX;

                // Write voltage for Y.
                _dMovement[1, _iI] = _dCurrentVoltageY;
            }


            this.m_dGeneratorCoordinates = _dMovement;
        }

        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            // Calculate the voltages that make up the full scan.
            this.CalculateMove(
                m_dCurrentVoltageX,
                m_dCurrentVoltageY,
                this.NmToVoltage(__dXPosNm),
                this.NmToVoltage(__dYPosNm),
                1000);

            int[] levels = Enumerable.Repeat(0, this.m_dGeneratorCoordinates.GetLength(1)).ToArray();

            this.TimedMove(1.0, this.m_dGeneratorCoordinates, levels);

            while (this.m_daqtskMoveStage.IsDone != true)
            {
                Thread.Sleep(100);
            }

            this.Stop();
        }

        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
        }

        private double NmToVoltage(double __dNmCoordinate)
        {
            double _dVoltage = __dNmCoordinate / m_dNmPVolt;
            return _dVoltage;
        }

        public void Scan(ScanModes.Scanmode __scmScanMode, double __dPixelTime, bool __bResend)
        {
            int size = __scmScanMode.NMScanCoordinates.GetLength(1);
            double delta = __scmScanMode.NMScanCoordinates[1, size - 1] - __scmScanMode.NMScanCoordinates[1, 0];

            double[] linevolts = new double[size];
            int[] levels = new int[size];

            for (int i = __scmScanMode.Trig1Start; i < __scmScanMode.Trig1End + 1; i++)
            {
                linevolts[i] = 3;
                levels[i] = 1;

                // The start of line trigger.
                if (i == __scmScanMode.Trig1Start)
                {
                    levels[i] = 3;
                }
            }

            // Allocate space for the full image
            double[,] coordinates =
                new double[3, size * __scmScanMode.RepeatNumber];

            int[] longlevels =
                new int[size * __scmScanMode.RepeatNumber];

            for (int i = 0; i < __scmScanMode.RepeatNumber; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    coordinates[0, j + (i * size)] = this.m_dCurrentVoltageX + this.NmToVoltage(__scmScanMode.NMScanCoordinates[0, j]);
                    coordinates[1, j + (i * size)] = this.m_dCurrentVoltageY + this.NmToVoltage(__scmScanMode.NMScanCoordinates[1, j] + i * delta);
                    coordinates[2, j + (i * size)] = 0.0;
                    longlevels[j + (i * size)] = levels[j];
                }
            }

            // Set the levels to achieve start of frame trigger.
            longlevels[0] = 7;

            this.m_dGeneratorCoordinates = coordinates;

            // Perform the actual scan as a timed move.
            this.TimedMove(__dPixelTime, this.m_dGeneratorCoordinates, longlevels);
        }

        public void Stop()
        {
            this.m_daqtskLineTrigger.Stop();
            this.m_daqtskMasterClock.Stop();
            this.m_daqtskMoveStage.Stop();

            this.m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;

            if (this.m_iSamplesToStageCurrent > 0)
            {
                m_dCurrentVoltageX = m_dGeneratorCoordinates[0, m_iSamplesToStageCurrent - 1];
                m_dCurrentVoltageY = m_dGeneratorCoordinates[1, m_iSamplesToStageCurrent - 1];
            }

            _logger.Info("written : " + m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel.ToString());

            if (PositionChanged != null)
            {
                PositionChanged(this, new EventArgs());
            }
        }

        private void TimedMove(double __dCycleTime, double[,] __dCoordinates, int[] __iLevels)
        {
            int _iSamplesPerChannel = __dCoordinates.Length / 3;

            // Prepare the stage control task for writing as many samples as necessary to complete Move.
            this.Configure(__dCycleTime, _iSamplesPerChannel);

            // Keep track of the progress on the output task.
            double _dProgress = 0.0;

            AnalogMultiChannelWriter writerA = new AnalogMultiChannelWriter(this.m_daqtskMoveStage.Stream);
            DigitalSingleChannelWriter writerD = new DigitalSingleChannelWriter(this.m_daqtskLineTrigger.Stream);

            try
            {
                this.m_daqtskMoveStage.Stream.Buffer.OutputBufferSize = __dCoordinates.GetLength(1);
                this.m_daqtskLineTrigger.Stream.Buffer.OutputBufferSize = __iLevels.Length;

                _logger.Debug("Buffer size " +
                    "Dev1 : " +
                    m_daqtskMoveStage.Stream.Buffer.OutputBufferSize.ToString() +
                    " samples and requested sample count = " +
                    __dCoordinates.GetLength(1).ToString());


                // Perform the actual AO write.
                writerA.WriteMultiSample(false, __dCoordinates);
                writerD.WriteMultiSamplePort(false, __iLevels);

                // Start all four tasks in the correct order. Global sync should be last.
                this.m_daqtskMoveStage.Start();
                this.m_daqtskLineTrigger.Start();
                this.m_daqtskMasterClock.Start();

                //// While moving, we indirectly poll position by checking how many samples are written.
                //while (this.m_daqtskMoveStage.IsDone != true)
                //{
                //    // Update the voltages.
                //    m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;

                //    if (m_iSamplesToStageCurrent > 0)
                //    {
                //        m_dCurrentVoltageX = m_dGeneratorCoordinates[0, m_iSamplesToStageCurrent - 1];
                //        m_dCurrentVoltageY = m_dGeneratorCoordinates[1, m_iSamplesToStageCurrent - 1];
                //    }

                //    _dProgress = ((double)m_iSamplesToStageCurrent / (_iSamplesPerChannel)) * 100;
                //    _dProgress = Math.Round(_dProgress);

                //    _logger.Info("Move Pct. done: " + _dProgress.ToString());

                //    if (PositionChanged != null)
                //    {
                //        PositionChanged(this, new EventArgs());
                //    }

                //    // Update the UI every 0.1 seconds, more than fast enough.
                //    Thread.Sleep(500);
                //}

                // Update the voltages one last time.
                if (m_iSamplesToStageCurrent > 0)
                {
                    this.m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
                    m_dCurrentVoltageX = m_dGeneratorCoordinates[0, m_iSamplesToStageCurrent - 1];
                    m_dCurrentVoltageY = m_dGeneratorCoordinates[1, m_iSamplesToStageCurrent - 1];
                }

                // Update Progress.
                _dProgress = ((double)m_iSamplesToStageCurrent / (_iSamplesPerChannel)) * 100;

                if (PositionChanged != null)
                {
                    PositionChanged(this, new EventArgs());
                }

                _logger.Info("Move Pct. done: " + _dProgress.ToString());
            }

            catch (Exception ex)
            {
                _logger.Error("Something went wrong! : \r\n", ex);
                m_daqtskMasterClock.Stop();
                m_daqtskMoveStage.Stop();
                m_daqtskLineTrigger.Stop();
            }

            //finally
            //{
            //    m_daqtskMasterClock.Stop();
            //    m_daqtskMoveStage.Stop();
            //    m_daqtskLineTrigger.Stop();
            //}
        }

        #endregion

    }
}
