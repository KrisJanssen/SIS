namespace SIS.Data
{
    using System;

    class UnexpectedFormatException : Exception
    {
        // The constructor that takes msg as a parameter
        public UnexpectedFormatException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
