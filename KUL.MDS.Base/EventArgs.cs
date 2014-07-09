using System;

namespace SIS.Base
{
    public class EventArgs<T>
        : EventArgs
    {
        private T data;
        public T Data
        {
            get
            {
                return data;
            }
        }

        public EventArgs(T data)
        {
            this.data = data;
        }
    }
}
