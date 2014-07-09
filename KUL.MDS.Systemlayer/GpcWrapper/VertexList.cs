/////////////////////////////////////////////////////////////////////////////////
// SIS                                                                   //
// Copyright (C) dotPDN LLC, Rick Brewster, Tom Jackson, and contributors.     //
// Portions Copyright (C) Microsoft Corporation. All Rights Reserved.          //
// See src/Resources/Files/License.txt for full licensing and attribution      //
// details.                                                                    //
// .                                                                           //
/////////////////////////////////////////////////////////////////////////////////

namespace SIS.Systemlayer.GpcWrapper
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal sealed class VertexList
    {
        public int NofVertices;
        public Vertex[] Vertex;

        public VertexList()
        {
        }

        public VertexList(Vertex[] v)
            : this(v, false)
        {
        }

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

        public VertexList(PointF[] p)
        {
            this.NofVertices = p.Length;
            this.Vertex = new Vertex[this.NofVertices];
            for (int i = 0; i < p.Length; i++)
                this.Vertex[i] = new Vertex((double)p[i].X, (double)p[i].Y);
        }

        public GraphicsPath ToGraphicsPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddLines(this.ToPoints());
            return graphicsPath;
        }

        public PointF[] ToPoints()
        {
            PointF[] vertexArray = new PointF[this.NofVertices];
            for (int i = 0; i < this.NofVertices; i++)
            {
                vertexArray[i] = new PointF((float)this.Vertex[i].X, (float)this.Vertex[i].Y);
            }
            return vertexArray;
        }

        public GraphicsPath TristripToGraphicsPath()
        {
            GraphicsPath graphicsPath = new GraphicsPath();

            for (int i = 0; i < this.NofVertices - 2; i++)
            {
                graphicsPath.AddPolygon(new PointF[3]{ new PointF( (float)this.Vertex[i].X,   (float)this.Vertex[i].Y ),
				                                           new PointF( (float)this.Vertex[i+1].X, (float)this.Vertex[i+1].Y ),
				                                           new PointF( (float)this.Vertex[i+2].X, (float)this.Vertex[i+2].Y )  });
            }

            return graphicsPath;
        }

        public override string ToString()
        {
            string s = "Polygon with " + this.NofVertices + " vertices: ";

            for (int i = 0; i < this.NofVertices; i++)
            {
                s += this.Vertex[i].ToString();
                if (i != this.NofVertices - 1)
                    s += ",";
            }
            return s;
        }
    }
}
