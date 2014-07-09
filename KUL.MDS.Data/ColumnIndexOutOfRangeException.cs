using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Data
{
    class ColumnIndexOutOfRangeException : System.Exception 
    {
         // The constructor that takes msg as a parameter
        public ColumnIndexOutOfRangeException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
