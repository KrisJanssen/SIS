// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="ComSettings.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Persistent settings
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Hardware.ComPort
{
    using System.IO.Ports;
    using System.Text;

    /// <summary>
    /// Persistent settings
    /// </summary>
    public class CommSettings
    {
        #region Public Methods and Operators

        /// <summary>
        ///   Read the settings from disk. </summary>
        public static void Read()
        {
            // Port.PortName = ConfigurationManager.AppSettings["PortName"];

            // Port.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
            // Port.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["Databits"]);
            // Port.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"]);
            // Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"]);
            // Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ConfigurationManager.AppSettings["HandShake"]);

            // Port.Encoding = System.Text.Encoding.GetEncoding(ConfigurationManager.AppSettings["Encoding"]);

            // Option.AppendToSend = (Option.AppendType)Enum.Parse(typeof(Option.AppendType), ini.ReadValue("Option", "AppendToSend", Option.AppendToSend.ToString()));
            // Option.HexOutput = bool.Parse(ini.ReadValue("Option", "HexOutput", Option.HexOutput.ToString()));
            // Option.MonoFont = bool.Parse(ini.ReadValue("Option", "MonoFont", Option.MonoFont.ToString()));
        }

        /// <summary>
        /// The setup settings.
        /// </summary>
        /// <param name="__sPortName">
        /// The __s port name.
        /// </param>
        public static void SetupSettings(string __sPortName)
        {
            Port.PortName = __sPortName;

            // Port.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
            // Port.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["Databits"]);
            // Port.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"]);
            // Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"]);
            // Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ConfigurationManager.AppSettings["HandShake"]);

            // Port.Encoding = System.Text.Encoding.GetEncoding(ConfigurationManager.AppSettings["Encoding"]);

            // Option.AppendToSend = (Option.AppendType)Enum.Parse(typeof(Option.AppendType), ini.ReadValue("Option", "AppendToSend", Option.AppendToSend.ToString()));
            // Option.HexOutput = bool.Parse(ini.ReadValue("Option", "HexOutput", Option.HexOutput.ToString()));
            // Option.MonoFont = bool.Parse(ini.ReadValue("Option", "MonoFont", Option.MonoFont.ToString()));
        }

        #endregion

        /// <summary> Option settings. </summary>
        public class Option
        {
            #region Static Fields

            /// <summary>
            /// The append to send.
            /// </summary>
            public static AppendType AppendToSend = AppendType.AppendCR;

            /// <summary>
            /// The filter use case.
            /// </summary>
            public static bool FilterUseCase = false;

            /// <summary>
            /// The hex output.
            /// </summary>
            public static bool HexOutput = false;

            /// <summary>
            /// The local echo.
            /// </summary>
            public static bool LocalEcho = true;

            /// <summary>
            /// The log file name.
            /// </summary>
            public static string LogFileName = string.Empty;

            /// <summary>
            /// The mono font.
            /// </summary>
            public static bool MonoFont = true;

            /// <summary>
            /// The stay on top.
            /// </summary>
            public static bool StayOnTop = false;

            #endregion

            #region Enums

            /// <summary>
            /// The append type.
            /// </summary>
            public enum AppendType
            {
                /// <summary>
                /// The append nothing.
                /// </summary>
                AppendNothing, 

                /// <summary>
                /// The append cr.
                /// </summary>
                AppendCR, 

                /// <summary>
                /// The append lf.
                /// </summary>
                AppendLF, 

                /// <summary>
                /// The append crlf.
                /// </summary>
                AppendCRLF, 

                /// <summary>
                /// The append sc.
                /// </summary>
                AppendSC
            }

            #endregion
        }

        /// <summary> Port settings. </summary>
        public class Port
        {
            #region Static Fields

            /// <summary>
            /// The baud rate.
            /// </summary>
            public static int BaudRate = 57600;

            /// <summary>
            /// The data bits.
            /// </summary>
            public static int DataBits = 8;

            /// <summary>
            /// The encoding.
            /// </summary>
            public static Encoding Encoding = System.Text.Encoding.UTF8;

            /// <summary>
            /// The handshake.
            /// </summary>
            public static Handshake Handshake = System.IO.Ports.Handshake.None;

            /// <summary>
            /// The parity.
            /// </summary>
            public static Parity Parity = System.IO.Ports.Parity.None;

            /// <summary>
            /// The port name.
            /// </summary>
            public static string PortName = "COM1";

            /// <summary>
            /// The stop bits.
            /// </summary>
            public static StopBits StopBits = System.IO.Ports.StopBits.One;

            #endregion
        }
    }
}