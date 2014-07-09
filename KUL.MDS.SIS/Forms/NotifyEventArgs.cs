using SIS.Documents;

namespace SIS.Forms
{
    public class NotifyEventArgs : System.EventArgs
    {
        public readonly ScanSettings Settings;

        public NotifyEventArgs(ScanSettings __scnstSettings)
        {
            Settings = __scnstSettings;
        }
    }
}
