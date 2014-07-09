// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Timing.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Methods for keeping track of time in a high precision manner.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    /// <summary>
    /// Methods for keeping track of time in a high precision manner.
    /// </summary>
    /// <remarks>
    /// This class provides precision and accuracy of 1 millisecond.
    /// </remarks>
    public sealed class Timing
    {
        #region Fields

        /// <summary>
        /// The birth tick.
        /// </summary>
        private ulong birthTick;

        /// <summary>
        /// The counts per ms.
        /// </summary>
        private ulong countsPerMs;

        /// <summary>
        /// The counts per ms double.
        /// </summary>
        private double countsPerMsDouble;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Timing"/> class. 
        /// Constructs an instance of the Timing class.
        /// </summary>
        public Timing()
        {
            ulong frequency;

            if (!SafeNativeMethods.QueryPerformanceFrequency(out frequency))
            {
                NativeMethods.ThrowOnWin32Error("QueryPerformanceFrequency returned false");
            }

            this.countsPerMs = frequency / 1000;
            this.countsPerMsDouble = (double)frequency / 1000.0;
            this.birthTick = this.GetTickCount();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The number of milliseconds that elapsed between system startup
        /// and creation of this instance of Timing.
        /// </summary>
        public ulong BirthTick
        {
            get
            {
                return this.birthTick;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since
        /// system startup.
        /// </summary>
        /// <returns>
        /// The <see cref="ulong"/>.
        /// </returns>
        public ulong GetTickCount()
        {
            ulong tick;
            SafeNativeMethods.QueryPerformanceCounter(out tick);
            return tick / this.countsPerMs;
        }

        /// <summary>
        /// Returns the number of milliseconds that have elapsed since
        /// system startup.
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetTickCountDouble()
        {
            ulong tick;
            SafeNativeMethods.QueryPerformanceCounter(out tick);
            return (double)tick / this.countsPerMsDouble;
        }

        #endregion
    }
}