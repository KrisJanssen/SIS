namespace SIS.Systemlayer
{
    using System.Drawing;
    using System.Windows.Forms;

    public interface IStylusReaderHooks
    {
        void PerformDocumentMouseMove(MouseButtons button, int clicks, float x, float y, int delta, float pressure);
        void PerformDocumentMouseUp(MouseButtons button, int clicks, float x, float y, int delta, float pressure);
        void PerformDocumentMouseDown(MouseButtons button, int clicks, float x, float y, int delta, float pressure);
        System.Drawing.Graphics CreateGraphics();
        PointF ScreenToDocument(PointF pointF);
    }
}
