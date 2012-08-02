//using System;
//using System.Collections.Generic;
//using System.Text;
//using NationalInstruments.DAQmx;
//using System.ComponentModel;
//using KUL.MDS.Library;
//using System.Threading;

//namespace KUL.MDS.Hardware
//{
//    public class PIAnalogStage : IPiezoStage
//    {
//        # region Essential Member Objects.

//        // The NI Task object that will handle actual stage control.
//        private Task m_daqtskMoveStage;

//        // Writer to write voltages to the stage controller.
//        private AnalogMultiChannelWriter m_wrtrVoltageWriter;

//        // We need a timing source to run the move at the proper speed.
//        private TimingClock m_TimingClock;

//        #endregion

//        #region Constant Stage Parameters.

//        // Constant properties of the stage. These will be used in input validation and safe speed calculation for stage movement.
//        private const double m_dNmPVolt = 10000.0;
//        private const double m_dMaxPosition = 90000.0;

//        // Set global range for the Voltage outputs as an additional safety.
//        private const double m_dVoltageMax = 10.0;
//        private const double m_dVoltageMin = 0.0;

//        #endregion

//        #region Members.

//        // Create variables to keep track of the currently set voltage to the Piezo stage.
//        private double m_dVoltageXCurrent;
//        private double m_dVoltageYCurrent;

//        // The UI will display data on the acquisition progress during the scan.
//        // More specifically, total samples currently sent to stage and total samples taken from APD.
//        private int m_iSamplesToStageCurrent;
//        private int m_iSamplesFromAPD;
//        private int m_iSampleDelta;

//        // Status of the stage
//        private bool m_bIsInitialized;

//        // The variable that holds the voltages to write to the stage.
//        private double[,] m_dVoltages;

//        #endregion

//        #region Properties.

//        public double XPosition
//        {
//            get
//            {
//                return this.m_dVoltageXCurrent;
//            }
//        }

//        public double YPosition
//        {
//            get
//            {
//                return this.m_dVoltageYCurrent;
//            }
//        }

//        public int SamplesWritten
//        {
//            get
//            {
//                return this.m_iSamplesToStageCurrent;
//            }
//        }

//        public bool IsInitialized
//        {
//            get
//            {
//                return this.m_bIsInitialized;
//            }
//            //set
//            //{
//            //    this.m_bIsInitialized = value;
//            //}
//        }

//        #endregion

//        #region Events.

//        public event EventHandler PositionChanged;

//        #endregion

//        #region Delegates.

        
//        private delegate void UIUpdateDelegate();
//        private delegate void ProgressUpdate(int _iProgress);

//        #endregion

//        #region Singleton Pattern.

//        // This class operates according to a singleton pattern. Having multiple PIAnalogStage could be dangerous because the hardware
//        // could be left in an unknown state.
//        private static volatile PIAnalogStage m_instance;
//        private static object m_syncRoot = new object();

//        public static PIAnalogStage Instance
//        {
//            get
//            {
//                if (m_instance == null)
//                {
//                    lock (m_syncRoot)
//                    {
//                        if (m_instance == null)
//                        {
//                            m_instance = new PIAnalogStage();
//                        }
//                    }
//                }

//                return m_instance;
//            }
//        }
//        #endregion

//        #region Methods.

//        // The constructor obviously needs to be private to prevent normal instantiation.
//        private PIAnalogStage()
//        {
//            // The PIAnalogStage object should be instantiated in an uninitialized state.
//            this.m_bIsInitialized = false;
//        }

//        public void Initialize()
//        {            
//            // Create a new Task object.
//            Task _daqtskTask = new Task();

//            try
//            {
//                // Setup an Analog Out task to move the Piezo stage along X and Y.
//                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao0", "aoChannelX", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
//                _daqtskTask.AOChannels.CreateVoltageChannel("/Dev1/ao1", "aoChannelY", m_dVoltageMin, m_dVoltageMax, AOVoltageUnits.Volts);
//                _daqtskTask.Control(TaskAction.Verify);

//                // Assign the task.
//                this.m_daqtskMoveStage = _daqtskTask;

//                // Return a status indication for the stage.
//                this.m_bIsInitialized = true;
//            }

//            catch (DaqException exception)
//            {
//                _daqtskTask.Dispose();
//                //MessageBox.Show(exception.Message);
//                this.m_bIsInitialized = false;
//            }
//        }

//        public void Configure(double __dFreq, int __iSteps)
//        {
//            try
//            {
//                this.m_daqtskMoveStage.Timing.ConfigureSampleClock(
//                    "/Dev1/RTSI0", 
//                    __dFreq, 
//                    SampleClockActiveEdge.Rising, 
//                    SampleQuantityMode.FiniteSamples, 
//                    __iSteps);
//                this.m_daqtskMoveStage.Control(TaskAction.Verify);
//                this.m_daqtskMoveStage.Control(TaskAction.Commit);
//            }

//            catch (DaqException ex)
//            {
//                //MessageBox.Show(ex.Message);
//            }
//        }

//        public void Release()
//        {
//            //this.MoveStage(0, 0);

//            try
//            {
//                // Properly dispose of the AO tasks that control the piezo stage.
//                this.m_daqtskMoveStage.Stop();
//                this.m_daqtskMoveStage.Dispose();
//                this.m_daqtskMoveStage = null;

//                // Return a status indication for the stage.
//                this.m_bIsInitialized = false;
//            }

//            catch (DaqException ex)
//            {
//                //MessageBox.Show(ex.Message);
//                this.m_bIsInitialized = false;
//            }
//        }

//        // Calculates voltages for a direct move to a set of XY coordinates.
//        private void CalculateMove(double __dInitVoltageX, double __dInitVoltageY, double __dFinVoltageX, double __dFinVoltageY, int __iSteps)
//        {
//            // Init some variables.
//            double _dCurrentVoltageX = __dInitVoltageX;
//            double _dCurrentVoltageY = __dInitVoltageY;

//            // Calculate the voltage resolution.
//            double _dVoltageResX = (__dFinVoltageX - __dInitVoltageX) / __iSteps;
//            double _dVoltageResY = (__dFinVoltageY - __dInitVoltageY) / __iSteps;

//            // Array to store the voltages for the entire move operation.
//            double[,] _dMovement = new double[2, __iSteps * 2];

//            // Calculate the actual voltages for the intended movement on X.
//            // Movement will be one axis at a time.
//            for (int _iI = 0; _iI < __iSteps; _iI++)
//            {
//                // Increment voltage. 
//                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
//                _dCurrentVoltageX = Math.Round((__dInitVoltageX + _dVoltageResX * (_iI + 1)), 4);

//                // Write voltage for X.
//                _dMovement[0, _iI] = _dCurrentVoltageX;

//                // Write voltage for Y.
//                _dMovement[1, _iI] = _dCurrentVoltageY;
//            }

//            // Calculate the actual voltages for the intended movement on X.
//            // Movement will be one axis at a time.
//            for (int _iI = 0; _iI < __iSteps; _iI++)
//            {
//                // Increment voltage. 
//                // Rounding to 4 digits is done since the voltage resolution of the DAQ board is 305 microvolts.
//                _dCurrentVoltageY = Math.Round((__dInitVoltageY + _dVoltageResY * (_iI + 1)), 4);

//                // Write voltage for X.
//                _dMovement[0, _iI + __iSteps] = _dCurrentVoltageX;

//                // Write voltage for Y.
//                _dMovement[1, _iI + __iSteps] = _dCurrentVoltageY;
//            }

//            this.m_dVoltages = _dMovement;
//        }

//        public void MoveAbs(double __dXPosNm, double __dYPosNm, double __dZPosNm)
//        {
//            try
//            {
//                // Calculate the voltages that make up the full scan.
//                this.CalculateMove(m_dVoltageXCurrent, m_dVoltageYCurrent, this.NmToVoltage(__dXPosNm), this.NmToVoltage(__dYPosNm), 1000);

//                // Check the generated voltages.
//                double _dMinV = KUL.MDS.Library.Helper.FindMin(m_dVoltages, 2, m_dVoltages.Length / 2);
//                double _dMaxV = KUL.MDS.Library.Helper.FindMax(m_dVoltages, 2, m_dVoltages.Length / 2);

//                if (_dMinV < m_dVoltageMin)
//                {
//                    throw new MinVoltageExceededException("The move you wish to execute will cause a voltage too LOW condition!" +
//                        "\r\n\r\nVoltage Value: " + _dMinV.ToString());
//                }

//                if (_dMaxV > m_dVoltageMax)
//                {
//                    throw new MinVoltageExceededException("The move you wish to execute will cause a voltage too HIGH condition!" +
//                        "\r\n\r\nVoltage Value: " + _dMaxV.ToString());
//                }

//                if (this.m_daqtskMoveStage != null)
//                {
//                    // Setup the timing source.
//                    this.m_TimingClock = new TimingClock();
//                    this.m_TimingClock.SetupClock(500.0);

//                    // Prepare the stage control task for writing as many samples as necessary to complete the scan.
//                    this.Configure(500.0, m_dVoltages.Length / 2);

//                    double _dProgress = 0.0;

//                    // Objects to perform reads and writes on our DAQ tasks.
//                    this.m_wrtrVoltageWriter = new AnalogMultiChannelWriter(this.m_daqtskMoveStage.Stream);

//                    // Perform the actual AO write.
//                    this.m_wrtrVoltageWriter.WriteMultiSample(false, m_dVoltages);

//                    // Start all four tasks in the correct order. Global sync should be last.
//                    this.m_daqtskMoveStage.Start();
//                    //m_daqtskGlobalSync.Start();
//                    this.m_TimingClock.Start();

//                    while (this.m_daqtskMoveStage.IsDone != true)
//                    {
//                        // Update the UI every 0.1 seconds, more than fast enough.
//                        Thread.Sleep(100);

//                        // Update the voltages.
//                        m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//                        m_dVoltageXCurrent = m_dVoltages[0, m_iSamplesToStageCurrent - 1];
//                        m_dVoltageYCurrent = m_dVoltages[1, m_iSamplesToStageCurrent - 1];

//                        _dProgress = ((double)m_iSamplesToStageCurrent / (m_dVoltages.Length / 2)) * 100;

//                        if (PositionChanged != null)
//                        {
//                            PositionChanged(this, new EventArgs());
//                        }
//                    }

//                    // Stop the Timing Clock.
//                    this.m_TimingClock.Stop();

//                    // Update the voltages.
//                    this.m_iSamplesToStageCurrent = (int)m_daqtskMoveStage.Stream.TotalSamplesGeneratedPerChannel;
//                    this.m_dVoltageXCurrent = m_dVoltages[0, m_iSamplesToStageCurrent - 1];
//                    this.m_dVoltageYCurrent = m_dVoltages[1, m_iSamplesToStageCurrent - 1];

//                    // Update Progress.
//                    _dProgress = ((double)m_iSamplesToStageCurrent / (m_dVoltages.Length / 2)) * 100;

//                    // Stop the move task for the stage.
//                    m_daqtskMoveStage.Stop();

//                    // Dispose the writer and reader.
//                    m_wrtrVoltageWriter = null; 
//                }
//                else
//                {
//                    //EnableCtrls();
//                    throw new StageNotEngagedException("The Piezo stage was not engaged properly, please use stage control to engage it!");
//                }
//            }

//            catch (MinVoltageExceededException ex)
//            {
//                // Needs implementation.
//            }
//        }

//        public void MoveRel(double __dXPosNm, double __dYPosNm, double __dZPosNm)
//        {
//        }

//        private double NmToVoltage(double __dNmCoordinate)
//        {
//            double _dVoltage = __dNmCoordinate / m_dNmPVolt;
//            return _dVoltage;
//        }

//        public void Scan(ScanModes.Scanmode __scmScanMode)
//        {
//        }

//        public void Stop()
//        {
//        }

//        #endregion
//    }
//}
