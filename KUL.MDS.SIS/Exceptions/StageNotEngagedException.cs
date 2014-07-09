namespace SIS.Exceptions
{
    /// <summary>
    /// This Exception will get thrown when the stage is not engaged properly when it should have been.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class StageNotEngagedException : System.Exception
    {
        // The constructor that takes msg as a parameter
        public StageNotEngagedException(string msg) : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }
}
