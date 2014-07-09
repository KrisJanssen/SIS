using System;
using System.Drawing;

namespace SIS.MDITemplate
{
    public sealed class MovingEventArgs
        : EventArgs
    {
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return this.rectangle;
            }

            set
            {
                this.rectangle = value;
            }
        }

        public MovingEventArgs(Rectangle rect)
        {
            this.rectangle = rect;
        }
    }
}
