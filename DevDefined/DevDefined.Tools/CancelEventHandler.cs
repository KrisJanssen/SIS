namespace DevDefined.Tools
{
    /// <summary>
    /// An event handler where the arguments have a cancel flag.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CancelEventHandler(object sender, CancelEventArgs e);

    /// <summary>
    /// An event handler where the arguments have a cancel flag.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CancelEventHandler<T>(object sender, CancelEventArgs<T> e);
}