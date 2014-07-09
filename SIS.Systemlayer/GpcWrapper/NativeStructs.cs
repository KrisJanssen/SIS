// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeStructs.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native structs.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer.GpcWrapper
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The native structs.
    /// </summary>
    internal static class NativeStructs
    {
        /// <summary>
        /// The gpc_polygon.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct gpc_polygon /* Polygon set structure             */
        {
            /// <summary>
            /// The num_contours.
            /// </summary>
            public int num_contours; /* Number of contours in polygon     */

            /// <summary>
            /// The hole.
            /// </summary>
            public IntPtr hole; /* Hole / external contour flags     */

            /// <summary>
            /// The contour.
            /// </summary>
            public IntPtr contour; /* Contour array pointer             */
        }

        /// <summary>
        /// The gpc_vertex.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct gpc_vertex /* Polygon vertex structure          */
        {
            /// <summary>
            /// The x.
            /// </summary>
            public double x; /* Vertex x component                */

            /// <summary>
            /// The y.
            /// </summary>
            public double y; /* vertex y component                */
        }

        /// <summary>
        /// The gpc_vertex_list.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct gpc_vertex_list /* Vertex list structure             */
        {
            /// <summary>
            /// The num_vertices.
            /// </summary>
            public int num_vertices; /* Number of vertices in list        */

            /// <summary>
            /// The vertex.
            /// </summary>
            public IntPtr vertex; /* Vertex array pointer              */
        }
    }
}