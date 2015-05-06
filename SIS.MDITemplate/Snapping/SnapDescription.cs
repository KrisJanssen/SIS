using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace SIS.MDITemplate
{
    public sealed class SnapDescription
    {
        private SnapObstacle snappedTo;
        private HorizontalSnapEdge horizontalEdge;
        private VerticalSnapEdge verticalEdge;
        private int xOffset;
        private int yOffset;

        public SnapObstacle SnappedTo
        {
            get
            {
                return this.snappedTo;
            }
        }

        public HorizontalSnapEdge HorizontalEdge
        {
            get
            {
                return this.horizontalEdge;
            }

            set
            {
                this.horizontalEdge = value;
            }
        }

        public VerticalSnapEdge VerticalEdge
        {
            get
            {
                return this.verticalEdge;
            }

            set
            {
                this.verticalEdge = value;
            }
        }

        public int XOffset
        {
            get
            {
                return this.xOffset;
            }

            set
            {
                this.xOffset = value;
            }
        }

        public int YOffset
        {
            get
            {
                return this.yOffset;
            }

            set
            {
                this.yOffset = value;
            }
        }

        public SnapDescription(
            SnapObstacle snappedTo,
            HorizontalSnapEdge horizontalEdge,
            VerticalSnapEdge verticalEdge,
            int xOffset,
            int yOffset)
        {
            if (snappedTo == null)
            {
                throw new ArgumentNullException("snappedTo");
            }

            this.snappedTo = snappedTo;
            this.horizontalEdge = horizontalEdge;
            this.verticalEdge = verticalEdge;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }
    }
}
