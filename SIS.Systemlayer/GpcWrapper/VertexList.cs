// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VertexList.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The vertex list.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer.GpcWrapper
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// The vertex list.
    /// </summary>
    internal sealed class VertexList
    {
        #region Fields

        /// <summary>
        /// The nof vertices.
        /// </summary>
        public int NofVertices;

        /// <summary>
        /// The vertex.
        /// </summary>
        public Vertex[] Vertex;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexList"/> class.
        /// </summary>
        public VertexList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexList"/> class.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        public VertexList(Vertex[] v)
            : this(v, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexList"/> class.
        /// </summary>
        /// <param name="v">
        /// The v.
        /// </param>
        /// <param name="takeOwnership">
        /// The take ownership.
        /// </param>
        public VertexList(Vertex[] v, bool takeOwnership)
        {
            if (takeOwnership)
            {
                this.Vertex = v;
                this.NofVertices = v.Length;
            }
            else
            {
                this.Vertex = (Vertex[])v.Clone();
                this.NofVertices = v.Length;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexList"/> class.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        public VertexList(PointF[] p)
        {
            this.NofVertices = p.Length;
            this.Vertex = new Vertex[this.NofVertices];
            for (int i = 0; i < p.Length; i++)
            {
                this.Vertex[i] = new Vertex((double)p[i].X, (double)p[i].Y);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The to graphics path.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphicsPath"/>.
        /// </returns>
        public GraphicsPath ToGraphicsPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLines(this.ToPoints());
            return graphicsPath;
        }

        /// <summary>
        /// The to points.
        /// </summary>
        /// <returns>
        /// The <see cref="PointF[]"/>.
        /// </returns>
        public PointF[] ToPoints()
        {
            PointF[] vertexArray = new PointF[this.NofVertices];
            for (int i = 0; i < this.NofVertices; i++)
            {
                vertexArray[i] = new PointF((float)this.Vertex[i].X, (float)this.Vertex[i].Y);
            }

            return vertexArray;
        }

        /// <summary>
        /// The to string.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            string s = "Polygon with " + this.NofVertices + " vertices: ";

            for (int i = 0; i < this.NofVertices; i++)
            {
                s += this.Vertex[i].ToString();
                if (i != this.NofVertices - 1)
                {
                    s += ",";
                }
            }

            return s;
        }

        /// <summary>
        /// The tristrip to graphics path.
        /// </summary>
        /// <returns>
        /// The <see cref="GraphicsPath"/>.
        /// </returns>
        public GraphicsPath TristripToGraphicsPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            for (int i = 0; i < this.NofVertices - 2; i++)
            {
                graphicsPath.AddPolygon(
                    new[]
                        {
                            new PointF((float)this.Vertex[i].X, (float)this.Vertex[i].Y), 
                            new PointF((float)this.Vertex[i + 1].X, (float)this.Vertex[i + 1].Y), 
                            new PointF((float)this.Vertex[i + 2].X, (float)this.Vertex[i + 2].Y)
                        });
            }

            return graphicsPath;
        }

        #endregion
    }
}