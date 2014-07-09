// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Ink.cs">
//   
// </copyright>
// <summary>
//   The ink.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The ink.
    /// </summary>
    public static class Ink
    {
        #region Static Fields

        /// <summary>
        /// The is ink available.
        /// </summary>
        private static bool isInkAvailable = false;

        /// <summary>
        /// The is ink available init.
        /// </summary>
        private static bool isInkAvailableInit = false;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Hooks Ink support in to a control.
        /// </summary>
        /// <param name="subject">
        /// </param>
        /// <param name="control">
        /// </param>
        /// <exception cref="NotSupportedException">
        /// IsAvailable() returned false
        /// </exception>
        /// <remarks>
        /// Ink support will be automatically unhooked when the control's Disposed event is raised.
        /// </remarks>
        public static void HookInk(IInkHooks subject, Control control)
        {
            if (!IsAvailable())
            {
                throw new NotSupportedException("Ink is not available");
            }

            HookInkImpl(subject, control);
        }

        /// <summary>
        /// Gets a value indicating whether Ink is available.
        /// </summary>
        /// <returns>true if Ink is available, or false if it is not</returns>
        /// <remarks>
        /// If ink is not available, then the other static methods or properties of this class will not
        /// be usable and will throw NotSupportedException.
        /// </remarks>
        public static bool IsAvailable()
        {
            if (!isInkAvailableInit)
            {
                // For debug builds we try to load the assembly. This enables us to work with ink
                // if we have the Tablet PC SDK installed. 
                // For retail builds we only enable ink on true blue Tablet PC's. Calling GetSystemMetrics
                // is much faster than attempting to load an assembly.
#if NOINK
                isInkAvailable = false;
#elif DEBUG
#if ENABLE_INK_IN_DEBUG_BUILDS
                try
                {
                    Assembly inkAssembly = Assembly.Load("Microsoft.Ink, Version=1.7.2600.2180, Culture=\"\", PublicKeyToken=31bf3856ad364e35");
                    isInkAvailable = true;
                }
                catch (Exception)
                {
                    isInkAvailable = false;
                }
#else
                isInkAvailable = false;
#endif
#else
                if (SafeNativeMethods.GetSystemMetrics(NativeConstants.SM_TABLETPC) != 0)
                {
                    // Only enable ink if the system states it is a Tablet PC.
                    // In other words, don't incur the performance penalty and a few other
                    // weird things for regular PC's that just happen to have the SDK
                    // installed, or that have something like Vista Ultimate.
                    try
                    {
                        Assembly inkAssembly = Assembly.Load("Microsoft.Ink, Version=1.7.2600.2180, Culture=\"\", PublicKeyToken=31bf3856ad364e35");
                        isInkAvailable = true;
                    }
                    catch (Exception)
                    {
                        isInkAvailable = false;
                    }
                }
                else
                {
                    isInkAvailable = false;
                }
#endif

                isInkAvailableInit = true;
            }

            return isInkAvailable;
        }

        /// <summary>
        /// Unhooks Ink support from a control.
        /// </summary>
        /// <param name="control">
        /// </param>
        /// <exception cref="NotSupportedException">
        /// IsAvailable() returned false
        /// </exception>
        public static void UnhookInk(Control control)
        {
            if (!IsAvailable())
            {
                throw new NotSupportedException("Ink is not available");
            }

            UnhookInkImpl(control);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The hook ink impl.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="control">
        /// The control.
        /// </param>
        private static void HookInkImpl(IInkHooks subject, Control control)
        {
            HookAdapter adapter = new HookAdapter(subject);
            control.CreateControl();
            StylusReader.HookStylus(adapter, control);
        }

        /// <summary>
        /// The unhook ink impl.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        private static void UnhookInkImpl(Control control)
        {
            StylusReader.UnhookStylus(control);
        }

        #endregion

        /// <summary>
        /// Adapts an IInkHook instance to work with the IStylusReaderHooks interface.
        /// </summary>
        private sealed class HookAdapter : IStylusReaderHooks
        {
            #region Fields

            /// <summary>
            /// The subject.
            /// </summary>
            private IInkHooks subject;

            #endregion

            #region Constructors and Destructors

            /// <summary>
            /// Initializes a new instance of the <see cref="HookAdapter"/> class.
            /// </summary>
            /// <param name="subject">
            /// The subject.
            /// </param>
            public HookAdapter(IInkHooks subject)
            {
                this.subject = subject;
            }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The create graphics.
            /// </summary>
            /// <returns>
            /// The <see cref="Graphics"/>.
            /// </returns>
            public Graphics CreateGraphics()
            {
                return this.subject.CreateGraphics();
            }

            /// <summary>
            /// The perform document mouse down.
            /// </summary>
            /// <param name="button">
            /// The button.
            /// </param>
            /// <param name="clicks">
            /// The clicks.
            /// </param>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="y">
            /// The y.
            /// </param>
            /// <param name="delta">
            /// The delta.
            /// </param>
            /// <param name="pressure">
            /// The pressure.
            /// </param>
            public void PerformDocumentMouseDown(
                MouseButtons button, 
                int clicks, 
                float x, 
                float y, 
                int delta, 
                float pressure)
            {
                this.subject.PerformDocumentMouseDown(button, clicks, x, y, delta, pressure);
            }

            /// <summary>
            /// The perform document mouse move.
            /// </summary>
            /// <param name="button">
            /// The button.
            /// </param>
            /// <param name="clicks">
            /// The clicks.
            /// </param>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="y">
            /// The y.
            /// </param>
            /// <param name="delta">
            /// The delta.
            /// </param>
            /// <param name="pressure">
            /// The pressure.
            /// </param>
            public void PerformDocumentMouseMove(
                MouseButtons button, 
                int clicks, 
                float x, 
                float y, 
                int delta, 
                float pressure)
            {
                this.subject.PerformDocumentMouseMove(button, clicks, x, y, delta, pressure);
            }

            /// <summary>
            /// The perform document mouse up.
            /// </summary>
            /// <param name="button">
            /// The button.
            /// </param>
            /// <param name="clicks">
            /// The clicks.
            /// </param>
            /// <param name="x">
            /// The x.
            /// </param>
            /// <param name="y">
            /// The y.
            /// </param>
            /// <param name="delta">
            /// The delta.
            /// </param>
            /// <param name="pressure">
            /// The pressure.
            /// </param>
            public void PerformDocumentMouseUp(
                MouseButtons button, 
                int clicks, 
                float x, 
                float y, 
                int delta, 
                float pressure)
            {
                this.subject.PerformDocumentMouseUp(button, clicks, x, y, delta, pressure);
            }

            /// <summary>
            /// The screen to document.
            /// </summary>
            /// <param name="pointF">
            /// The point f.
            /// </param>
            /// <returns>
            /// The <see cref="PointF"/>.
            /// </returns>
            public PointF ScreenToDocument(PointF pointF)
            {
                return this.subject.ScreenToDocument(pointF);
            }

            #endregion
        }
    }
}