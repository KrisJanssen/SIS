using System;

namespace SIS.Base
{
    public interface IDisposedEvent
    {
        event EventHandler Disposed;
    }
}
