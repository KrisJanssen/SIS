// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The polygon.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer.GpcWrapper
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The polygon.
    /// </summary>
    internal sealed class Polygon
    {
        #region Fields

        /// <summary>
        /// The contour.
        /// </summary>
        public VertexList[] Contour;

        /// <summary>
        /// The contour is hole.
        /// </summary>
        public bool[] ContourIsHole;

        /// <summary>
        /// The nof contours.
        /// </summary>
        public int NofContours;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon()
        {
        }

        // path should contain only polylines ( use Flatten )
        // furthermore the constructor assumes that all Subpathes of path except the first one are holes
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public Polygon(GraphicsPath path)
        {
            byte[] pathTypes = path.PathTypes;

            this.NofContours = 0;
            foreach (byte b in pathTypes)
            {
                if ((b & ((byte)PathPointType.CloseSubpath)) != 0)
                {
                    this.NofContours++;
                }
            }

            this.ContourIsHole = new bool[this.NofContours];
            this.Contour = new VertexList[this.NofContours];

            for (int i = 0; i < this.NofContours; i++)
            {
                this.ContourIsHole[i] = i == 0;
            }

            int contourNr = 0;

            List<PointF> contour = new List<PointF>();

            PointF[] pathPoints = path.PathPoints;

            for (int i = 0; i < pathPoints.Length; i++)
            {
                contour.Add(pathPoints[i]);

                if ((pathTypes[i] & ((byte)PathPointType.CloseSubpath)) != 0)
                {
                    PointF[] pointArray = contour.ToArray();
                    VertexList vl = new VertexList(pointArray);
                    this.Contour[contourNr++] = vl;
                    contour.Clear();
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The check.
        /// </summary>
        /// <param name="combineMode">
        /// The combine mode.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Check(CombineMode combineMode)
        {
            switch (combineMode)
            {
                case CombineMode.Exclude:
                case CombineMode.Intersect:
                case CombineMode.Union:
                case CombineMode.Xor:
                    return true;

                case CombineMode.Complement:
                case CombineMode.Replace:
                default:
                    return false;
            }
        }

        /// <summary>
        /// The clip.
        /// </summary>
        /// <param name="clipMode">
        /// The clip mode.
        /// </param>
        /// <param name="subject_polygon">
        /// The subject_polygon.
        /// </param>
        /// <param name="clip_polygon">
        /// The clip_polygon.
        /// </param>
        /// <returns>
        /// The <see cref="Polygon"/>.
        /// </returns>
        public static Polygon Clip(CombineMode clipMode, Polygon subject_polygon, Polygon clip_polygon)
        {
            Validate(clipMode);

            NativeConstants.gpc_op gpcOp = Convert(clipMode);

            NativeStructs.gpc_polygon gpc_polygon = new NativeStructs.gpc_polygon();
            NativeStructs.gpc_polygon gpc_subject_polygon = PolygonTo_gpc_polygon(subject_polygon);
            NativeStructs.gpc_polygon gpc_clip_polygon = PolygonTo_gpc_polygon(clip_polygon);

            NativeMethods.gpc_polygon_clip(gpcOp, ref gpc_subject_polygon, ref gpc_clip_polygon, ref gpc_polygon);

            Polygon polygon = gpc_polygon_ToPolygon(gpc_polygon);

            Free_gpc_polygon(gpc_subject_polygon);
            Free_gpc_polygon(gpc_clip_polygon);
            NativeMethods.gpc_free_polygon(ref gpc_polygon);

            return polygon;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="combineMode">
        /// The combine mode.
        /// </param>
        /// <exception cref="InvalidEnumArgumentException">
        /// </exception>
        public static void Validate(CombineMode combineMode)
        {
            if (Check(combineMode))
            {
                return;
            }
            else
            {
                throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// The clip.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <param name="polygon">
        /// The polygon.
        /// </param>
        /// <returns>
        /// The <see cref="Polygon"/>.
        /// </returns>
        public Polygon Clip(CombineMode operation, Polygon polygon)
        {
            return Clip(operation, this, polygon);
        }

        /// <summary>
        /// The to graphics path.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphicsPath"/>.
        /// </returns>
        public GraphicsPath ToGraphicsPath()
        {
            GraphicsPath path = new GraphicsPath();

            for (int i = 0; i < this.NofContours; i++)
            {
                PointF[] points = this.Contour[i].ToPoints();

                if (this.ContourIsHole[i])
                {
                    Array.Reverse(points);
                }

                path.AddPolygon(points);
            }

            return path;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="combineMode">
        /// The combine mode.
        /// </param>
        /// <returns>
        /// The <see cref="gpc_op"/>.
        /// </returns>
        /// <exception cref="InvalidEnumArgumentException">
        /// </exception>
        private static NativeConstants.gpc_op Convert(CombineMode combineMode)
        {
            switch (combineMode)
            {
                case CombineMode.Exclude:
                    return NativeConstants.gpc_op.GPC_DIFF;

                case CombineMode.Intersect:
                    return NativeConstants.gpc_op.GPC_INT;

                case CombineMode.Union:
                    return NativeConstants.gpc_op.GPC_UNION;

                case CombineMode.Xor:
                    return NativeConstants.gpc_op.GPC_XOR;

                case CombineMode.Complement:
                case CombineMode.Replace:
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        /// <summary>
        /// The free_gpc_polygon.
        /// </summary>
        /// <param name="gpc_pol">
        /// The gpc_pol.
        /// </param>
        private static void Free_gpc_polygon(NativeStructs.gpc_polygon gpc_pol)
        {
            Marshal.FreeCoTaskMem(gpc_pol.hole);
            IntPtr ptr = gpc_pol.contour;
            for (int i = 0; i < gpc_pol.num_contours; i++)
            {
                NativeStructs.gpc_vertex_list gpc_vtx_list =
                    (NativeStructs.gpc_vertex_list)Marshal.PtrToStructure(ptr, typeof(NativeStructs.gpc_vertex_list));
                Marshal.FreeCoTaskMem(gpc_vtx_list.vertex);
                ptr = (IntPtr)(((int)ptr) + Marshal.SizeOf(gpc_vtx_list));
            }
        }

        /// <summary>
        /// The polygon to_gpc_polygon.
        /// </summary>
        /// <param name="polygon">
        /// The polygon.
        /// </param>
        /// <returns>
        /// The <see cref="gpc_polygon"/>.
        /// </returns>
        private static NativeStructs.gpc_polygon PolygonTo_gpc_polygon(Polygon polygon)
        {
            NativeStructs.gpc_polygon gpc_pol = new NativeStructs.gpc_polygon();
            gpc_pol.num_contours = polygon.NofContours;

            int[] hole = new int[polygon.NofContours];

            for (int i = 0; i < polygon.NofContours; i++)
            {
                hole[i] = polygon.ContourIsHole[i] ? 1 : 0;
            }

            gpc_pol.hole = Marshal.AllocCoTaskMem(polygon.NofContours * Marshal.SizeOf(typeof(int) /*hole[0]*/));

            if (polygon.NofContours > 0)
            {
                Marshal.Copy(hole, 0, gpc_pol.hole, polygon.NofContours);
            }

            gpc_pol.contour =
                Marshal.AllocCoTaskMem(polygon.NofContours * Marshal.SizeOf(typeof(NativeStructs.gpc_vertex_list)));
            IntPtr ptr = gpc_pol.contour;
            for (int i = 0; i < polygon.NofContours; i++)
            {
                NativeStructs.gpc_vertex_list gpc_vtx_list = new NativeStructs.gpc_vertex_list();
                gpc_vtx_list.num_vertices = polygon.Contour[i].NofVertices;
                gpc_vtx_list.vertex =
                    Marshal.AllocCoTaskMem(
                        polygon.Contour[i].NofVertices * Marshal.SizeOf(typeof(NativeStructs.gpc_vertex)));
                IntPtr ptr2 = gpc_vtx_list.vertex;
                for (int j = 0; j < polygon.Contour[i].NofVertices; j++)
                {
                    NativeStructs.gpc_vertex gpc_vtx = new NativeStructs.gpc_vertex();
                    gpc_vtx.x = polygon.Contour[i].Vertex[j].X;
                    gpc_vtx.y = polygon.Contour[i].Vertex[j].Y;
                    Marshal.StructureToPtr(gpc_vtx, ptr2, false);
                    ptr2 = (IntPtr)(((int)ptr2) + Marshal.SizeOf(gpc_vtx));
                }

                Marshal.StructureToPtr(gpc_vtx_list, ptr, false);
                ptr = (IntPtr)(((int)ptr) + Marshal.SizeOf(gpc_vtx_list));
            }

            return gpc_pol;
        }

        /// <summary>
        /// The gpc_polygon_ to polygon.
        /// </summary>
        /// <param name="gpc_polygon">
        /// The gpc_polygon.
        /// </param>
        /// <returns>
        /// The <see cref="Polygon"/>.
        /// </returns>
        private static Polygon gpc_polygon_ToPolygon(NativeStructs.gpc_polygon gpc_polygon)
        {
            Polygon polygon = new Polygon();

            polygon.NofContours = gpc_polygon.num_contours;
            polygon.ContourIsHole = new bool[polygon.NofContours];
            polygon.Contour = new VertexList[polygon.NofContours];
            short[] holeShort = new short[polygon.NofContours];
            IntPtr ptr = gpc_polygon.hole;

            if (polygon.NofContours > 0)
            {
                Marshal.Copy(gpc_polygon.hole, holeShort, 0, polygon.NofContours);
            }

            for (int i = 0; i < polygon.NofContours; i++)
            {
                polygon.ContourIsHole[i] = holeShort[i] != 0;
            }

            ptr = gpc_polygon.contour;
            for (int i = 0; i < polygon.NofContours; i++)
            {
                NativeStructs.gpc_vertex_list gpc_vtx_list =
                    (NativeStructs.gpc_vertex_list)Marshal.PtrToStructure(ptr, typeof(NativeStructs.gpc_vertex_list));
                polygon.Contour[i] = new VertexList();
                polygon.Contour[i].NofVertices = gpc_vtx_list.num_vertices;
                polygon.Contour[i].Vertex = new Vertex[polygon.Contour[i].NofVertices];
                IntPtr ptr2 = gpc_vtx_list.vertex;
                for (int j = 0; j < polygon.Contour[i].NofVertices; j++)
                {
                    NativeStructs.gpc_vertex gpc_vtx =
                        (NativeStructs.gpc_vertex)Marshal.PtrToStructure(ptr2, typeof(NativeStructs.gpc_vertex));
                    polygon.Contour[i].Vertex[j].X = gpc_vtx.x;
                    polygon.Contour[i].Vertex[j].Y = gpc_vtx.y;

                    ptr2 = (IntPtr)(((int)ptr2) + Marshal.SizeOf(gpc_vtx));
                }

                ptr = (IntPtr)(((int)ptr) + Marshal.SizeOf(gpc_vtx_list));
            }

            return polygon;
        }

        #endregion
    }
}