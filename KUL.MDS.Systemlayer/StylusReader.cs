// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StylusReader.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The stylus reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Microsoft.Ink;
    using Microsoft.StylusInput;

    /// <summary>
    /// The stylus reader.
    /// </summary>
    public sealed class StylusReader
    {
        // If we don't keep the styluses, they get garbagecollected.
        #region Static Fields

        /// <summary>
        /// The hooked controls.
        /// </summary>
        private static Hashtable hookedControls = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="StylusReader"/> class from being created.
        /// </summary>
        private StylusReader()
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The hook stylus.
        /// </summary>
        /// <param name="subject">
        /// The subject.
        /// </param>
        /// <param name="control">
        /// The control.
        /// </param>
        /// <exception cref="ApplicationException">
        /// </exception>
        public static void HookStylus(IStylusReaderHooks subject, Control control)
        {
            if (hookedControls.Contains(control))
            {
                throw new ApplicationException("control is already hooked");
            }

            RealTimeStylus stylus = new RealTimeStylus(control, true);
            StylusAsyncPlugin stylusReader = new StylusAsyncPlugin(subject, control);

            stylus.AsyncPluginCollection.Add(stylusReader);
            stylus.SetDesiredPacketDescription(
                new[] { PacketProperty.X, PacketProperty.Y, PacketProperty.NormalPressure, PacketProperty.PacketStatus });
            stylus.Enabled = true;

            control.Disposed += new EventHandler(control_Disposed);

            WeakReference weakRef = new WeakReference(control);
            hookedControls.Add(weakRef, stylus);
        }

        /// <summary>
        /// The unhook stylus.
        /// </summary>
        /// <param name="control">
        /// The control.
        /// </param>
        public static void UnhookStylus(Control control)
        {
            lock (hookedControls.SyncRoot)
            {
                List<WeakReference> deleteUs = new List<WeakReference>();

                foreach (WeakReference weakRef in hookedControls.Keys)
                {
                    object target = weakRef.Target;

                    if (target == null)
                    {
                        deleteUs.Add(weakRef);
                    }
                    else
                    {
                        Control control2 = (Control)target;

                        if (object.ReferenceEquals(control, control2))
                        {
                            deleteUs.Add(weakRef);
                        }
                    }
                }

                foreach (WeakReference weakRef in deleteUs)
                {
                    RealTimeStylus stylus = (RealTimeStylus)hookedControls[weakRef];
                    stylus.Enabled = false;
                    stylus.AsyncPluginCollection.Clear();
                    hookedControls.Remove(weakRef);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The control_ disposed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void control_Disposed(object sender, EventArgs e)
        {
            Control asControl = (Control)sender;
            asControl.Disposed -= new EventHandler(control_Disposed);
            UnhookStylus(asControl);
        }

        #endregion
    }
}