namespace SIS.Forms
{
    using SIS.Documents;

    public class NotifyEventArgs : System.EventArgs
    {
        public readonly ScanSettings Settings;

        public NotifyEventArgs(ScanSettings __scnstSettings)
        {
            this.Settings = __scnstSettings;
        }
    }
}
