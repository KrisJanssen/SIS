// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStylusReaderHooks.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The StylusReaderHooks interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// The StylusReaderHooks interface.
    /// </summary>
    public interface IStylusReaderHooks
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create graphics.
        /// </summary>
        /// <returns>
        /// The <see cref="Graphics"/>.
        /// </returns>
        Graphics CreateGraphics();

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
        void PerformDocumentMouseDown(MouseButtons button, int clicks, float x, float y, int delta, float pressure);

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
        void PerformDocumentMouseMove(MouseButtons button, int clicks, float x, float y, int delta, float pressure);

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
        void PerformDocumentMouseUp(MouseButtons button, int clicks, float x, float y, int delta, float pressure);

        /// <summary>
        /// The screen to document.
        /// </summary>
        /// <param name="pointF">
        /// The point f.
        /// </param>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        PointF ScreenToDocument(PointF pointF);

        #endregion
    }
}