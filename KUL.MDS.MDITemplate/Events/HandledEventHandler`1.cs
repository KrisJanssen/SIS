using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MDITemplate
{
    public delegate void HandledEventHandler<T>(object sender, HandledEventArgs<T> e);
}
