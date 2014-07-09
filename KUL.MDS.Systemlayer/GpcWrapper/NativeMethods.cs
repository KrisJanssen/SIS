// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer.GpcWrapper
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// The x 64.
        /// </summary>
        private static class X64
        {
            #region Public Methods and Operators

            /// <summary>
            /// The gpc_free_polygon.
            /// </summary>
            /// <param name="polygon">
            /// The polygon.
            /// </param>
            [DllImport("ShellExtension_x64.dll")]
            public static extern void gpc_free_polygon([In] ref NativeStructs.gpc_polygon polygon);

            /// <summary>
            /// The gpc_polygon_clip.
            /// </summary>
            /// <param name="set_operation">
            /// The set_operation.
            /// </param>
            /// <param name="subject_polygon">
            /// The subject_polygon.
            /// </param>
            /// <param name="clip_polygon">
            /// The clip_polygon.
            /// </param>
            /// <param name="result_polygon">
            /// The result_polygon.
            /// </param>
            [DllImport("ShellExtension_x64.dll")]
            public static extern void gpc_polygon_clip(
                [In] NativeConstants.gpc_op set_operation, 
                [In] ref NativeStructs.gpc_polygon subject_polygon, 
                [In] ref NativeStructs.gpc_polygon clip_polygon, 
                [In] [Out] ref NativeStructs.gpc_polygon result_polygon);

            #endregion
        }

        /// <summary>
        /// The x 86.
        /// </summary>
        private static class X86
        {
            #region Public Methods and Operators

            /// <summary>
            /// The gpc_free_polygon.
            /// </summary>
            /// <param name="polygon">
            /// The polygon.
            /// </param>
            [DllImport("ShellExtension_x86.dll")]
            public static extern void gpc_free_polygon([In] ref NativeStructs.gpc_polygon polygon);

            /// <summary>
            /// The gpc_polygon_clip.
            /// </summary>
            /// <param name="set_operation">
            /// The set_operation.
            /// </param>
            /// <param name="subject_polygon">
            /// The subject_polygon.
            /// </param>
            /// <param name="clip_polygon">
            /// The clip_polygon.
            /// </param>
            /// <param name="result_polygon">
            /// The result_polygon.
            /// </param>
            [DllImport("ShellExtension_x86.dll")]
            public static extern void gpc_polygon_clip(
                [In] NativeConstants.gpc_op set_operation, 
                [In] ref NativeStructs.gpc_polygon subject_polygon, 
                [In] ref NativeStructs.gpc_polygon clip_polygon, 
                [In] [Out] ref NativeStructs.gpc_polygon result_polygon);

            #endregion
        }

        /// <summary>
        /// The gpc_polygon_clip.
        /// </summary>
        /// <param name="set_operation">
        /// The set_operation.
        /// </param>
        /// <param name="subject_polygon">
        /// The subject_polygon.
        /// </param>
        /// <param name="clip_polygon">
        /// The clip_polygon.
        /// </param>
        /// <param name="result_polygon">
        /// The result_polygon.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static void gpc_polygon_clip(
            [In] NativeConstants.gpc_op set_operation, 
            [In] ref NativeStructs.gpc_polygon subject_polygon, 
            [In] ref NativeStructs.gpc_polygon clip_polygon, 
            [In] [Out] ref NativeStructs.gpc_polygon result_polygon)
        {
            if (Processor.Architecture == ProcessorArchitecture.X64)
            {
                X64.gpc_polygon_clip(set_operation, ref subject_polygon, ref clip_polygon, ref result_polygon);
            }
            else if (Processor.Architecture == ProcessorArchitecture.X86)
            {
                X86.gpc_polygon_clip(set_operation, ref subject_polygon, ref clip_polygon, ref result_polygon);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// The gpc_free_polygon.
        /// </summary>
        /// <param name="polygon">
        /// The polygon.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public static void gpc_free_polygon([In] ref NativeStructs.gpc_polygon polygon)
        {
            if (Processor.Architecture == ProcessorArchitecture.X64)
            {
                X64.gpc_free_polygon(ref polygon);
            }
            else if (Processor.Architecture == ProcessorArchitecture.X86)
            {
                X86.gpc_free_polygon(ref polygon);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}