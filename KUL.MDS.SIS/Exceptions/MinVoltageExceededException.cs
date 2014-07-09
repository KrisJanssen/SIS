namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the requested voltage exceeds the global minimum.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class MinVoltageExceededException : System.Exception
    {
        // The constructor that takes msg as a parameter
        public MinVoltageExceededException(string msg) : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
