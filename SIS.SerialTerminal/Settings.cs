// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Kris Janssen" file="Settings.cs">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Persistent settings
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.SerialTerminal
{
    using System;
    using System.IO.Ports;
    using System.Windows.Forms;

    /// <summary>
    /// Persistent settings
    /// </summary>
    public class Settings
    {
        #region Public Methods and Operators

        /// <summary>
        ///   Read the settings from disk. </summary>
        public static void Read()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\SerialTerminal.ini");
            Port.PortName = ini.ReadValue("Port", "PortName", Port.PortName);
            Port.BaudRate = ini.ReadValue("Port", "BaudRate", Port.BaudRate);
            Port.DataBits = ini.ReadValue("Port", "DataBits", Port.DataBits);
            Port.Parity = (Parity)Enum.Parse(typeof(Parity), ini.ReadValue("Port", "Parity", Port.Parity.ToString()));
            Port.StopBits =
                (StopBits)Enum.Parse(typeof(StopBits), ini.ReadValue("Port", "StopBits", Port.StopBits.ToString()));
            Port.Handshake =
                (Handshake)Enum.Parse(typeof(Handshake), ini.ReadValue("Port", "Handshake", Port.Handshake.ToString()));

            Option.AppendToSend =
                (Option.AppendType)
                Enum.Parse(
                    typeof(Option.AppendType), 
                    ini.ReadValue("Option", "AppendToSend", Option.AppendToSend.ToString()));
            Option.HexOutput = bool.Parse(ini.ReadValue("Option", "HexOutput", Option.HexOutput.ToString()));
            Option.MonoFont = bool.Parse(ini.ReadValue("Option", "MonoFont", Option.MonoFont.ToString()));
            Option.LocalEcho = bool.Parse(ini.ReadValue("Option", "LocalEcho", Option.LocalEcho.ToString()));
            Option.StayOnTop = bool.Parse(ini.ReadValue("Option", "StayOnTop", Option.StayOnTop.ToString()));
            Option.FilterUseCase = bool.Parse(ini.ReadValue("Option", "FilterUseCase", Option.FilterUseCase.ToString()));
        }

        /// <summary>
        ///   Write the settings to disk. </summary>
        public static void Write()
        {
            IniFile ini = new IniFile(Application.StartupPath + "\\SerialTerminal.ini");
            ini.WriteValue("Port", "PortName", Port.PortName);
            ini.WriteValue("Port", "BaudRate", Port.BaudRate);
            ini.WriteValue("Port", "DataBits", Port.DataBits);
            ini.WriteValue("Port", "Parity", Port.Parity.ToString());
            ini.WriteValue("Port", "StopBits", Port.StopBits.ToString());
            ini.WriteValue("Port", "Handshake", Port.Handshake.ToString());

            ini.WriteValue("Option", "AppendToSend", Option.AppendToSend.ToString());
            ini.WriteValue("Option", "HexOutput", Option.HexOutput.ToString());
            ini.WriteValue("Option", "MonoFont", Option.MonoFont.ToString());
            ini.WriteValue("Option", "LocalEcho", Option.LocalEcho.ToString());
            ini.WriteValue("Option", "StayOnTop", Option.StayOnTop.ToString());
            ini.WriteValue("Option", "FilterUseCase", Option.FilterUseCase.ToString());
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
                AppendCRLF
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
            public static int BaudRate = 115200;

            /// <summary>
            /// The data bits.
            /// </summary>
            public static int DataBits = 8;

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