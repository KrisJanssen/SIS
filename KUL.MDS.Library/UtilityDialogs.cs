using Microsoft.Win32;
//using PaintDotNet.Threading;
using SIS.SystemLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SIS;
using SIS.AppResources;

namespace SIS.Library
{
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
