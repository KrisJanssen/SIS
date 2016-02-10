﻿using System;
using System.Text;
using System.Linq;
using System.Threading;
using SIS.ScanModes;
using SIS.SystemLayer;

using NationalInstruments.DAQmx;

namespace SIS.Hardware
{
    using System.Diagnostics;
    using System.Net.Mime;
    using System.Xml.Serialization;

    public class NIAnalogStage : IPiezoStage
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constant Stage Parameters.

        // Constant properties of the stage. These will be used in input validation and safe speed calculation for stage movement.
        private const double m_dNmPVolt = 10000.0;
        private const double m_dMaxPosition = 90000.0;

        // Set global range for the Voltage outputs as an additional safety.
        private const double m_dVoltageMax = 10.0;
        private const double m_dVoltageMin = -10.0;

        #endregion

        #region Members.

        // The NI Task object that will handle actual stage control.
        // LineTrigger handles a multi bit 'bus' that can be programmed 
        // on a per-pixel basis. This can be used to modulate the main 
        // pulse train for external HW.
        // MasterClock handles the global synchronizing pulse train.
        // MoveStage handles analog output.
        private Task m_daqtskLineTrigger;
        private Task m_daqtskMoveStage;

        private NISampleClock m_sampleClock;
        private double m_samplePeriod;

        // Current array of coordinates for timed stage motion
        private double[,] m_dScanCoordinates;
        private double[,] m_dMoveGeneratorCoordinates;
        private int[] m_iLongLevels;

        // Create variables to keep track of the currently set voltage to the stage.
        private double m_dCurrentVoltageX;
        private double m_dCurrentVoltageY;
        private double m_dCurrentVoltageZ;

        private double m_startX;
        private double m_startY;
        private double m_startZ;

        // The UI will display data on the acquisition progress during the scan.
        // More specifically, total samples currently sent to stage and total samples taken from APD.
        private int m_iSamplesToStageCurrent;

        // Status of the stage
        private bool m_bIsInitialized;

        private bool m_bMaster;
        private string m_sTimingSource;
        private string m_sDevice;

        #endregion

        #region Properties.

        public int BufferSize
        {
            get
            {
                if (this.m_dScanCoordinates != null)
                {
                    return this.m_dScanCoordinates.GetLength(1);
                }
                else
                {
                    return 0;
                }
            }
        }

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
                    return this.VoltageToNm(m_dCurrentVoltageX);
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
                    return this.VoltageToNm(m_dCurrentVoltageY);
                }
                else
                {
                    return -1.0;
                }
            }
        }

        /// <summary>
        /// Returns the current Z position of the stage in nm.
        /// </summary>
        public double ZPosition
        {
            get
            {
                if (this.m_bIsInitialized)
                {
                    return this.VoltageToNm(m_dCurrentVoltageZ);
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

        #region Methods.

        // The constructor obviously needs to be private to prevent normal instantiation.
        public NIAnalogStage(string _sDevice, bool _bMaster, string _sTimingSource)
        {
            // The PIAnalogStage object should be instantiated in an uninitialized state.
            this.m_bIsInitialized = false;
            this.m_sDevice = _sDevice;
            this.m_bMaster = _bMaster;
            this.m_sTimingSource = _sTimingSource;
        }

        public void Initialize()
        {
            _logger.Info("Initializing analog Stage....");

            // Setup an Analog Out task to move the stage along X and Y.
            Task _daqtskTask = new Task();

            try
            {
                // Add AO channels.
                _daqtskTask.AOChannels.CreateVoltageChannel("/" + this.m_sDevice + "/ao0", "aoChannelX", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
                _daqtskTask.AOChannels.CreateVoltageChannel("/" + this.m_sDevice + "/ao1", "aoChannelY", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
                _daqtskTask.AOChannels.CreateVoltageChannel("/" + this.m_sDevice + "/ao2", "aoChannelZ", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);

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

                _logger.Error("Unable to connect set up AO channels for Move task!" + exception.Message);
            }

            // If everything went well, tell everyone.
            if (EngagedChanged != null)
            {
                EngagedChanged(this, new EventArgs());
            }

            _logger.Info("Init Stage Done!");
        }

        public void Configure(double __dCycleTimeMilisec, int __iSteps)
        {
            this.Configure(__dCycleTimeMilisec, __iSteps, true);
        }

        public void Configure(double __dCycleTimeMilisec, int __iSteps, bool continuous)
        {
            _logger.Info("Configuring stage timing....");

            this.m_samplePeriod = __dCycleTimeMilisec / 1000;

            if (this.m_bMaster && this.m_sampleClock == null)
            {
                this.m_sampleClock = new NISampleClock(this.m_sDevice, "Ctr2");
            }

            if (this.m_daqtskLineTrigger != null)
            {
                if (this.m_daqtskLineTrigger.IsDone != true)
                {
                    this.m_daqtskLineTrigger.Stop();
                }

                this.m_daqtskLineTrigger.Control(TaskAction.Unreserve);
            }

            Task _lineTask = new Task();

            try
            {

                if (this.m_daqtskMoveStage == null)
                {
                    this.Initialize();
                }

                this.m_daqtskMoveStage.Timing.SampleTimingType = SampleTimingType.SampleClock;

                this.m_daqtskMoveStage.Timing.ConfigureSampleClock(
                    this.m_bMaster ? this.m_sampleClock.Terminal : this.m_sTimingSource,
                    1000 / __dCycleTimeMilisec,
                    SampleClockActiveEdge.Rising,
                    continuous ? SampleQuantityMode.ContinuousSamples : SampleQuantityMode.FiniteSamples,
                    __iSteps);

                _lineTask.DOChannels.CreateChannel(
                    this.m_sDevice + "/port0",
                    "test",
                    ChannelLineGrouping.OneChannelForAllLines);

                _lineTask.Timing.SampleTimingType = SampleTimingType.SampleClock;

                _lineTask.Timing.ConfigureSampleClock(
                    this.m_bMaster ? this.m_sampleClock.Terminal : this.m_sTimingSource,
                    1000 / __dCycleTimeMilisec,
                    SampleClockActiveEdge.Rising,
                    continuous ? SampleQuantityMode.ContinuousSamples : SampleQuantityMode.FiniteSamples,
                    __iSteps);

                _lineTask.Control(TaskAction.Verify);
                _lineTask.Control(TaskAction.Commit);
            }

            catch (DaqException ex)
            {
                if (_lineTask != null)
                {
                    _lineTask.Dispose();
                }

                _logger.Error("Error while setting timing!", ex);
            }

            if (_lineTask != null)
            {
                this.m_daqtskLineTrigger = _lineTask;
            }
        }

        public void Release()
        {
            this.Home();

            try
            {
                // Properly dispose of the AO tasks that control the stage.
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

            if (this.m_bMaster && this.m_sampleClock != null)
            {
                this.m_sampleClock.Stop();
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
        private double[,] CalculateMove(
            double __dInitVoltageX, double __dInitVoltageY, double __dInitVoltageZ,
            double __dFinVoltageX, double __dFinVoltageY, double __dFinVoltageZ,
            int __iSteps)
        {
            // Init some variables.
            double _dCurrentVoltageX = __dInitVoltageX;
            double _dCurrentVoltageY = __dInitVoltageY;
            double _dCurrentVoltageZ = __dInitVoltageZ;

            // Calculate the voltage resolution.
            double _dVoltageResX = (__dFinVoltageX - __dInitVoltageX) / __iSteps;
            double _dVoltageResY = (__dFinVoltageY - __dInitVoltageY) / __iSteps;
            double _dVoltageResZ = (__dFinVoltageZ - __dInitVoltageZ) / __iSteps;

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

                // Increment voltage. 
                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
                _dCurrentVoltageZ = Math.Round((__dInitVoltageZ + _dVoltageResZ * (_iI + 1)), 4);

                // Write voltage for X.
                _dMovement[0, _iI] = _dCurrentVoltageX;

                // Write voltage for Y.
                _dMovement[1, _iI] = _dCurrentVoltageY;

                // Write voltage for Y.
                _dMovement[2, _iI] = _dCurrentVoltageZ;
            }


            return _dMovement;
        }

        public void Reset()
        {
            //this.MoveAbs(this.m_startX, this.m_startY, this.m_startZ);
        }

        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
            // Calculate the voltages that make up the full scan.
            this.m_dMoveGeneratorCoordinates = this.CalculateMove(
                m_dCurrentVoltageX,
                m_dCurrentVoltageY,
                m_dCurrentVoltageZ,
                this.NmToVoltage(__dXPosNm),
                this.NmToVoltage(__dYPosNm),
                this.NmToVoltage(__dZPosNm),
                1000);

            int[] levels = Enumerable.Repeat(0, this.m_dMoveGeneratorCoordinates.GetLength(1)).ToArray();

            this.TimedMove(1.0, this.m_dMoveGeneratorCoordinates, levels, false);

            while (this.m_daqtskMoveStage.IsDone != true)
            {
                Thread.Sleep(100);
            }

            this.Stop();
        }

        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
        {
        }

        private double VoltageToNm(double __dVoltage)
        {
            double _dNm = __dVoltage * m_dNmPVolt;
            return _dNm;
        }

        private double NmToVoltage(double __dNmCoordinate)
        {
            double _dVoltage = __dNmCoordinate / m_dNmPVolt;
            return _dVoltage;
        }

        public void Scan(ScanModes.Scanmode __scmScanMode, double __dPixelTime, bool __bResend, double __dRotation, int delay, bool wobble, double wobbleAmplitude, bool flip)
        {
            int returnlength = 1000;

            if (__bResend | this.m_dScanCoordinates == null)
            {
                // We need to figure out the size of the full coordinate buffer.
                // This is the size of 1 period buffer * the number of lines/repeats of that buffer.
                int linesize = __scmScanMode.ScanCoordinates.GetLength(1);
                int numlines = __scmScanMode.RepeatNumber;
                int framesize = linesize * numlines;


                // The position offset between consecutive scanlines.
                double delta = this.NmToVoltage(__scmScanMode.ScanCoordinates[1, linesize - 1] - __scmScanMode.ScanCoordinates[1, 0]);

                // Allocate space for the full image
                double[,] coordinates =
                    new double[3, framesize + returnlength];

                // We compose line triggers for a single line and later copy it to the frame buffer.
                int[] levels = new int[linesize];

                // The trigger buffer for the full frame.
                int[] longlevels =
                    new int[framesize + returnlength];

                foreach (Trigger t in __scmScanMode.Triggers)
                {
                    if (t.Active)
                    {
                        // Set pixel trigger to ensure data acq on the actual scanline only (and not the ramping period)
                        //for (int i = t.Start + delay; i < t.End + delay + 1; i++)
                        for (int i = t.Start; i < t.End + 1; i++)
                        {
                            levels[i] = 1;
                        }

                        // Additionally set the line start and end triggers.
                        //levels[t.Start + delay] = 3;
                        //levels[t.End + delay] = 3;
                        levels[t.Start] = 3;
                        levels[t.End] = 3;
                    }
                }

                // Final linebuffer
                double[,] linebuffer = new double[3, __scmScanMode.ScanCoordinates.Length / 2];

                int szdouble = sizeof(double);
                int szint = sizeof(int);

                for (int i = 0; i < linesize; i++)
                {
                    linebuffer[0, i] = this.NmToVoltage(__scmScanMode.ScanCoordinates[0, i]) + this.m_dCurrentVoltageX;
                    linebuffer[1, i] = this.NmToVoltage(__scmScanMode.ScanCoordinates[1, i]) + this.m_dCurrentVoltageY;
                    //linebuffer[2, i] = this.NmToVoltage(linebuffer[2, i]) + this.m_dCurrentVoltageZ;
                    linebuffer[2, i] = this.m_dCurrentVoltageZ;
                }

                if (!wobble)
                {
                    for (int i = 0; i < numlines; i++)
                    {
                        for (int j = 0; j < linesize; j++)
                        {
                            linebuffer[1, j] = linebuffer[1, j] + delta;
                        }

                        // Coordinates.
                        System.Buffer.BlockCopy(linebuffer, 0 * szdouble, coordinates, (i * linesize) * szdouble, linesize * szdouble);
                        System.Buffer.BlockCopy(linebuffer, linesize * szdouble, coordinates, ((i * linesize) + framesize + returnlength) * szdouble, linesize * szdouble);
                        System.Buffer.BlockCopy(linebuffer, 2 * linesize * szdouble, coordinates, ((i * linesize) + 2 * (framesize + returnlength)) * szdouble, linesize * szdouble);

                        // Triggers.
                        //System.Buffer.BlockCopy(levels, 0 * szint, longlevels, i * linesize * szint, (linesize - 1) * szint);
                        System.Buffer.BlockCopy(levels, 0 * szint, longlevels, (delay + (i * linesize)) * szint, (linesize - 1) * szint);

                        //for (int j = 0; j < linesize; j++)
                        //{
                        //    coordinates[0, j + (i * linesize)] = this.m_dCurrentVoltageX + this.NmToVoltage(__scmScanMode.ScanCoordinates[0, j]);
                        //    coordinates[1, j + (i * linesize)] = this.m_dCurrentVoltageY + this.NmToVoltage(__scmScanMode.ScanCoordinates[1, j] + i * delta);
                        //    coordinates[2, j + (i * linesize)] = this.m_dCurrentVoltageZ + 0.0;
                        //    longlevels[j + (i * linesize)] = levels[j];
                        //}
                    }
                }
                else
                {
                    // Generate the wobble waveform
                    double[] wobblewf = GenerateWobbleBuffer(this.NmToVoltage(wobbleAmplitude), framesize);

                    for (int i = 0; i < framesize; i++)
                    {
                        wobblewf[i] = wobblewf[i] + this.m_dCurrentVoltageZ;
                    }

                    for (int i = 0; i < numlines; i++)
                    {
                        for (int j = 0; j < linesize; j++)
                        {
                            linebuffer[1, j] = linebuffer[1, j] + delta;
                        }

                        System.Buffer.BlockCopy(linebuffer, 0 * szdouble, coordinates, (i * linesize) * szdouble, linesize * szdouble);
                        System.Buffer.BlockCopy(linebuffer, linesize * szdouble, coordinates, ((i * linesize) + framesize + returnlength) * szdouble, linesize * szdouble);
                        System.Buffer.BlockCopy(linebuffer, 2 * linesize * szdouble, coordinates, ((i * linesize) + 2 * (framesize + returnlength)) * szdouble, linesize * szdouble);

                        //System.Buffer.BlockCopy(levels, 0 * szint, longlevels, i * linesize * szint, (linesize - 1) * szint);
                        System.Buffer.BlockCopy(levels, 0 * szint, longlevels, (delay + (i * linesize)) * szint, (linesize - 1) * szint);
                    }

                    System.Buffer.BlockCopy(wobblewf, 0 * szdouble, coordinates, 2 * (framesize + returnlength) * szdouble, framesize * szdouble);

                    // We might need to flip channels.
                    if (flip)
                    {

                    }



                    //if (!wobble)
                    //{
                    //    for (int i = 0; i < __scmScanMode.RepeatNumber; i++)
                    //    {
                    //        for (int j = 0; j < linesize; j++)
                    //        {
                    //            coordinates[1, j + (i * linesize)] = this.m_dCurrentVoltageY + this.NmToVoltage(__scmScanMode.ScanCoordinates[0, j]);
                    //            coordinates[0, j + (i * linesize)] = this.m_dCurrentVoltageX + this.NmToVoltage(__scmScanMode.ScanCoordinates[1, j] + i * delta);
                    //            coordinates[2, j + (i * linesize)] = this.m_dCurrentVoltageZ + 0.0;
                    //            longlevels[j + (i * linesize)] = levels[j];
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    double[] sine = GenerateWobbleBuffer(this.NmToVoltage(wobbleAmplitude), linesize * __scmScanMode.RepeatNumber);

                    //    for (int i = 0; i < __scmScanMode.RepeatNumber; i++)
                    //    {
                    //        for (int j = 0; j < linesize; j++)
                    //        {
                    //            coordinates[1, j + (i * linesize)] = this.m_dCurrentVoltageY + this.NmToVoltage(__scmScanMode.ScanCoordinates[0, j]);
                    //            coordinates[0, j + (i * linesize)] = this.m_dCurrentVoltageX + this.NmToVoltage(__scmScanMode.ScanCoordinates[1, j] + i * delta);
                    //            coordinates[2, j + (i * linesize)] = this.m_dCurrentVoltageZ + sine[j + (i * linesize)];
                    //            longlevels[j + (i * linesize)] = levels[j];
                    //        }
                    //    }
                    //}
                }

                // TODO: Evaluate if rotation is still a desirable function to have.
                //double _dMidX = this.NmToVoltage(__scmScanMode.XScanSizeNm) / 2 + this.m_dCurrentVoltageX;
                //double _dMidY = this.m_dCurrentVoltageY;

                //for (int i = 0; i < coordinates.GetLength(1); i++)
                //{
                //    double xt = _dMidX + Math.Cos(__dRotation) * (coordinates[0, i] - _dMidX) - Math.Sin(__dRotation) * (coordinates[1, i] - _dMidY);
                //    double yt = _dMidY + Math.Sin(__dRotation) * (coordinates[0, i] - _dMidX) + Math.Cos(__dRotation) * (coordinates[1, i] - _dMidY);
                //    coordinates[0, i] = xt;
                //    coordinates[1, i] = yt;

                //}

                // Calculate a smooth return upon frame end. This is FlipXY independent.
                double[,] returnpath = this.CalculateMove(
                    coordinates[0, (linesize * __scmScanMode.RepeatNumber) - 1],
                    coordinates[1, (linesize * __scmScanMode.RepeatNumber) - 1],
                    coordinates[2, (linesize * __scmScanMode.RepeatNumber) - 1],
                    coordinates[0, 0],
                    coordinates[1, 0],
                    coordinates[2, 0],
                    returnlength);

                //for (int i = 0; i < returnlength; i++)
                //{
                //    coordinates[0, linesize * __scmScanMode.RepeatNumber + i] = returnpath[0, i];
                //    coordinates[1, linesize * __scmScanMode.RepeatNumber + i] = returnpath[1, i];
                //    coordinates[2, linesize * __scmScanMode.RepeatNumber + i] = returnpath[2, i];
                //}

                System.Buffer.BlockCopy(returnpath, 0 * szdouble, coordinates, framesize * szdouble, returnlength * szdouble);
                System.Buffer.BlockCopy(returnpath, returnlength * szdouble, coordinates, (2 * framesize + returnlength) * szdouble, returnlength * szdouble);
                System.Buffer.BlockCopy(returnpath, 2 * returnlength * szdouble, coordinates, (2 * (framesize + returnlength) + framesize) * szdouble, returnlength * szdouble);

                // Set the levels to achieve start of frame and end of frame trigger.
                longlevels[0 + delay] = 4;
                longlevels[longlevels.GetLength(0) + delay - returnlength] = 4;

                // Persist.
                this.m_iLongLevels = longlevels;
                this.m_dScanCoordinates = coordinates;
            }

            // Persist.
            this.m_dMoveGeneratorCoordinates = this.m_dScanCoordinates;

            this.m_startX = this.m_dScanCoordinates[0, 0];
            this.m_startY = this.m_dScanCoordinates[1, 0];
            this.m_startZ = this.m_dScanCoordinates[2, 0];

            // Perform the actual scan as a timed move.
            //this.TimedMove(__dPixelTime, this.m_dScanCoordinates, this.m_iLongLevels, false);
            this.TimedMove(__dPixelTime, this.m_dScanCoordinates, this.m_iLongLevels, true);
        }

        public void Stop()
        {
            this.m_daqtskLineTrigger.Stop();
            this.m_daqtskMoveStage.Stop();

            if (this.m_bMaster && this.m_sampleClock != null)
            {
                this.m_sampleClock.Stop();
            }

            this.m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel; //% m_dMoveGeneratorCoordinates.GetLength(1);

            if (this.m_iSamplesToStageCurrent > 0)
            {
                int temp = m_iSamplesToStageCurrent % this.m_dMoveGeneratorCoordinates.GetLength(1);
                if (temp == 0)
                {
                    temp = m_iSamplesToStageCurrent;
                }
                m_dCurrentVoltageX = m_dMoveGeneratorCoordinates[0, temp - 1];
                m_dCurrentVoltageY = m_dMoveGeneratorCoordinates[1, temp - 1];
                m_dCurrentVoltageZ = m_dMoveGeneratorCoordinates[2, temp - 1];
            }

            _logger.Info("written : " + m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel.ToString());

            if (PositionChanged != null)
            {
                PositionChanged(this, new EventArgs());
            }
        }

        private void TimedMove(double __dCycleTime, double[,] __dCoordinates, int[] __iLevels, bool continuous)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            _logger.Debug("Start:" + watch.ElapsedMilliseconds.ToString());
            int _iSamplesPerChannel = __dCoordinates.Length / 3;

            // Prepare the stage control task for writing as many samples as necessary to complete Move.
            this.Configure(__dCycleTime, _iSamplesPerChannel, continuous);

            AnalogMultiChannelWriter writerA = new AnalogMultiChannelWriter(this.m_daqtskMoveStage.Stream);
            DigitalSingleChannelWriter writerD = new DigitalSingleChannelWriter(this.m_daqtskLineTrigger.Stream);

            try
            {
                // Perform the actual AO write.
                writerA.WriteMultiSample(false, __dCoordinates);
                writerD.WriteMultiSamplePort(false, __iLevels);
                _logger.Debug("End write:" + watch.ElapsedMilliseconds.ToString());

                // Start all four tasks in the correct order. Global sync should be last.
                this.m_daqtskLineTrigger.Start();
                this.m_daqtskMoveStage.Start();
                if (this.m_bMaster && this.m_sampleClock != null)
                {
                    this.m_sampleClock.Start(this.m_samplePeriod);
                }
            }

            catch (Exception ex)
            {
                _logger.Error("Something went wrong! : \r\n", ex);
                m_daqtskMoveStage.Stop();
            }
        }

        #endregion

        public static double[] GenerateWobbleBuffer(
            double amplitude,
            double samplesPerBuffer)
        {
            int intSamplesPerBuffer = (int)samplesPerBuffer;

            int intRamp = Convert.ToInt32(samplesPerBuffer * 0.05);
            int intMiddle = intSamplesPerBuffer - 2 * intRamp;

            double rise = amplitude / intRamp;
            double middle = 2 * amplitude / intMiddle;

            double[] rVal = new double[intSamplesPerBuffer];

            for (int i = 0; i < intRamp; i++)
            {
                rVal[i] = i * rise;
            }

            for (int i = intRamp; i < intRamp + intMiddle; i++)
            {
                rVal[i] = amplitude - (i - intRamp) * middle;
            }

            for (int i = intRamp + intMiddle; i < 2 * intRamp + intMiddle; i++)
            {
                rVal[i] = -amplitude + (i - (intRamp + intMiddle)) * rise;
            }

            return rVal;
        }
    }
}
