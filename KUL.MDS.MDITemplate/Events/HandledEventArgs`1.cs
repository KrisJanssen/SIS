using System;
using System.ComponentModel;

namespace SIS.MDITemplate
{
    public class HandledEventArgs<T>
        : HandledEventArgs
    {
        private T data;
        public T Data
        {
            get
            {
                return this.data;
            }
        }

        public HandledEventArgs(bool handled, T data)
            : base(handled)
        {
            this.data = data;
        }
    }
}
