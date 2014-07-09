namespace SIS.Data
{
    /// <summary>
    /// This Exception will get thrown when the requested block does not exist.
    /// TODO: Make this implementation fully compliant with MSDN directives.
    /// </summary>
    public class BlockDoesNotExistException : System.Exception
    {
        // The constructor that takes msg as a parameter
        public BlockDoesNotExistException(string msg)
            : base(msg)
        {
            // Nothing Special should be done for now.
        }
    }

}
