using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SIS.MDITemplate
{
    public interface ISnapManagerHost
    {
        SnapManager SnapManager
        {
            get;
        }
    }
}
