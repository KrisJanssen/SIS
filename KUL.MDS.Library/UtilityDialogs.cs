// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="UtilityDialogs.cs">
//   
// </copyright>
// <summary>
//   Defines miscellaneous constants and static functions.
// </summary>
// 
// --------------------------------------------------------------------------------------------------------------------
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
        #region Public Methods and Operators

        /// <summary>
        /// The ask ok cancel.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="question">
        /// The question.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public static DialogResult AskOKCancel(IWin32Window parent, string question)
        {
            return MessageBox.Show(
                parent, 
                question, 
                Info.GetBareProductName(), 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Question);
        }

        /// <summary>
        /// The ask yes no.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="question">
        /// The question.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public static DialogResult AskYesNo(IWin32Window parent, string question)
        {
            return MessageBox.Show(
                parent, 
                question, 
                Info.GetBareProductName(), 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
        }

        /// <summary>
        /// The ask yes no cancel.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="question">
        /// The question.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public static DialogResult AskYesNoCancel(IWin32Window parent, string question)
        {
            return MessageBox.Show(
                parent, 
                question, 
                Info.GetBareProductName(), 
                MessageBoxButtons.YesNoCancel, 
                MessageBoxIcon.Question);
        }

        /// <summary>
        /// The error box.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void ErrorBox(IWin32Window parent, string message)
        {
            MessageBox.Show(parent, message, Info.GetBareProductName(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// The error box ok cancel.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public static DialogResult ErrorBoxOKCancel(IWin32Window parent, string message)
        {
            return MessageBox.Show(
                parent, 
                message, 
                Info.GetBareProductName(), 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// The image to icon.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon ImageToIcon(Image image)
        {
            return ImageToIcon(image, Utility.TransparentKey);
        }

        /// <summary>
        /// The image to icon.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="disposeImage">
        /// The dispose image.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon ImageToIcon(Image image, bool disposeImage)
        {
            return ImageToIcon(image, Utility.TransparentKey, disposeImage);
        }

        /// <summary>
        /// The image to icon.
        /// </summary>
        /// <param name="image">
        /// The image.
        /// </param>
        /// <param name="seeThru">
        /// The see thru.
        /// </param>
        /// <returns>
        /// The <see cref="Icon"/>.
        /// </returns>
        public static Icon ImageToIcon(Image image, Color seeThru)
        {
            return ImageToIcon(image, seeThru, false);
        }

        /// <summary>
        /// The info box.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void InfoBox(IWin32Window parent, string message)
        {
            MessageBox.Show(
                parent, 
                message, 
                Info.GetBareProductName(), 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// The info box ok cancel.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="DialogResult"/>.
        /// </returns>
        public static DialogResult InfoBoxOKCancel(IWin32Window parent, string message)
        {
            return MessageBox.Show(
                parent, 
                message, 
                Info.GetBareProductName(), 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Information);
        }

        /// <summary>
        /// The show non admin error box.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public static void ShowNonAdminErrorBox(IWin32Window parent)
        {
            // ErrorBox(parent, SISResources.GetString("NonAdminErrorBox.Message"));
        }

        #endregion
    }
}