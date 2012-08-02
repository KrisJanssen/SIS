using System;
using System.Collections.Generic;
using System.Text;

namespace KUL.MDS.Data
{
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
