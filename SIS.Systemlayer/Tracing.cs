// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tracing.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Methods for manual profiling and tracing.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using SIS.Base;

    /// <summary>
    /// Methods for manual profiling and tracing.
    /// </summary>
    /// <remarks>
    /// This class does not rely on any system-specific functionality, but is placed
    /// in the SystemLayer assembly (as opposed to Core) so that classes here may 
    /// also used it.
    /// </remarks>
    public static class Tracing
    {
        /// <summary>
        /// The write line.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        private static void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <summary>
        /// The features.
        /// </summary>
        private static Set<string> features = new Set<string>();

        /// <summary>
        /// The log feature.
        /// </summary>
        /// <param name="featureName">
        /// The feature name.
        /// </param>
        public static void LogFeature(string featureName)
        {
            if (string.IsNullOrEmpty(featureName))
            {
                return;
            }

            Ping("Logging feature: " + featureName);

            lock (features)
            {
                if (!features.Contains(featureName))
                {
                    features.Add(featureName);
                }
            }
        }

        /// <summary>
        /// The get logged features.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<string> GetLoggedFeatures()
        {
            lock (features)
            {
                return features.ToArray();
            }
        }

#if DEBUG

        /// <summary>
        /// The timing.
        /// </summary>
        private static Timing timing = new Timing();

        /// <summary>
        /// The trace points.
        /// </summary>
        private static Stack tracePoints = new Stack();

        /// <summary>
        /// The trace point.
        /// </summary>
        private class TracePoint
        {
            #region Fields

            /// <summary>
            /// The message.
            /// </summary>
            private string message;

            /// <summary>
            /// The timestamp.
            /// </summary>
            private ulong timestamp;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="TracePoint"/> class.
            /// </summary>
            /// <param name="message">
            /// The message.
            /// </param>
            /// <param name="timestamp">
            /// The timestamp.
            /// </param>
            public TracePoint(string message, ulong timestamp)
            {
                this.message = message;
                this.timestamp = timestamp;
            }

            #endregion

            #region Public Properties

            /// <summary>
            /// Gets the message.
            /// </summary>
            public string Message
            {
                get
                {
                    return this.message;
                }
            }

            /// <summary>
            /// Gets the timestamp.
            /// </summary>
            public ulong Timestamp
            {
                get
                {
                    return this.timestamp;
                }
            }

            #endregion
        }
#endif

        /// <summary>
        /// The enter.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Enter()
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            string msg = new string(' ', 4 * tracePoints.Count) + parentMethod.DeclaringType.Name + "."
                         + parentMethod.Name;
            WriteLine((now - timing.BirthTick).ToString() + ": " + msg);
            TracePoint tracePoint = new TracePoint(msg, now);
            tracePoints.Push(tracePoint);
#endif
        }

        /// <summary>
        /// The enter.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [Conditional("DEBUG")]
        public static void Enter(string message)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            string msg = new string(' ', 4 * tracePoints.Count) + parentMethod.DeclaringType.Name + "."
                         + parentMethod.Name + ": " + message;
            WriteLine((now - timing.BirthTick).ToString() + ": " + msg);
            TracePoint tracePoint = new TracePoint(msg, now);
            tracePoints.Push(tracePoint);
#endif
        }

        /// <summary>
        /// The leave.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Leave()
        {
#if DEBUG
            TracePoint tracePoint = (TracePoint)tracePoints.Pop();
            ulong now = timing.GetTickCount();
            WriteLine(
                (now - timing.BirthTick).ToString() + ": " + tracePoint.Message + " ("
                + (now - tracePoint.Timestamp).ToString() + "ms)");
#endif
        }

        /// <summary>
        /// The ping.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="callerCount">
        /// The caller count.
        /// </param>
        [Conditional("DEBUG")]
        public static void Ping(string message, int callerCount)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            string callerString = string.Empty;
            for (int i = 0; i < Math.Min(trace.FrameCount - 1, callerCount); ++i)
            {
                StackFrame frame = trace.GetFrame(1 + i);
                MethodBase method = frame.GetMethod();
                callerString += method.DeclaringType.Name + "." + method.Name;
                if (i != callerCount - 1)
                {
                    callerString += " <- ";
                }
            }

            ulong now = timing.GetTickCount();
            WriteLine(
                (now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count) + callerString
                + (message != null ? (": " + message) : string.Empty));
#endif
        }

        /// <summary>
        /// The ping.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        [Conditional("DEBUG")]
        public static void Ping(string message)
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            WriteLine(
                (now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count)
                + parentMethod.DeclaringType.Name + "." + parentMethod.Name
                + (message != null ? (": " + message) : string.Empty));
#endif
        }

        /// <summary>
        /// The ping.
        /// </summary>
        [Conditional("DEBUG")]
        public static void Ping()
        {
#if DEBUG
            StackTrace trace = new StackTrace();
            StackFrame parentFrame = trace.GetFrame(1);
            MethodBase parentMethod = parentFrame.GetMethod();
            ulong now = timing.GetTickCount();
            WriteLine(
                (now - timing.BirthTick).ToString() + ": " + new string(' ', 4 * tracePoints.Count)
                + parentMethod.DeclaringType.Name + "." + parentMethod.Name);
#endif
        }
    }
}