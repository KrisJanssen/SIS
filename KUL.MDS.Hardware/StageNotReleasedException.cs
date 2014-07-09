namespace SIS.Hardware
{
    /// <summary>
    /// This Exception will get thrown when the stage is not released properly when it should have been.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class StageNotReleasedException : System.Exception
    {
        // The constructor that takes msg as a parameter
        public StageNotReleasedException(string msg) : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
