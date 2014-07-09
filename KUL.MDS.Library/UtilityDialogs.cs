//using PaintDotNet.Threading;

namespace SIS.Library
{
    using System.Drawing;
    using System.Windows.Forms;

    using SIS.Resources;

    /// <summary>
    /// Defines miscellaneous constants and static functions.
    /// </summary>
    /// // TODO: refactor into mini static classes
    public sealed partial class Utility
    {
        public static void ShowNonAdminErrorBox(IWin32Window parent)
        {
            //ErrorBox(parent, SISResources.GetString("NonAdminErrorBox.Message"));
        }

        public static void ErrorBox(IWin32Window parent, string message)
        {
            MessageBox.Show(parent, message, Info.GetBareProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ErrorBoxOKCancel(IWin32Window parent, string message)
        {
            return MessageBox.Show(parent, message, Info.GetBareProductName(), MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
        }

        public static void InfoBox(IWin32Window parent, string message)
        {
            MessageBox.Show(parent, message, Info.GetBareProductName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult InfoBoxOKCancel(IWin32Window parent, string message)
        {
            return MessageBox.Show(parent, message, Info.GetBareProductName(), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        public static DialogResult AskOKCancel(IWin32Window parent, string question)
        {
            return MessageBox.Show(parent, question, Info.GetBareProductName(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        public static DialogResult AskYesNo(IWin32Window parent, string question)
        {
            return MessageBox.Show(parent, question, Info.GetBareProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult AskYesNoCancel(IWin32Window parent, string question)
        {
            return MessageBox.Show(parent, question, Info.GetBareProductName(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }

        public static Icon ImageToIcon(Image image)
        {
            return ImageToIcon(image, Utility.TransparentKey);
        }

        public static Icon ImageToIcon(Image image, bool disposeImage)
        {
            return ImageToIcon(image, Utility.TransparentKey, disposeImage);
        }

        public static Icon ImageToIcon(Image image, Color seeThru)
        {
            return ImageToIcon(image, seeThru, false);
        }
    }
}
