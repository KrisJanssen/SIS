using KUL.MDS.SIS.Documents;

namespace KUL.MDS.SIS.Forms
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
