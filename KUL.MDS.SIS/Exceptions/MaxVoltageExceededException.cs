namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the requested voltage exceeds the global maximum.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class MaxVoltageExceededException : System.Exception
    {
        // The constructor that takes msg as a parameter
        public MaxVoltageExceededException(string msg) : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
