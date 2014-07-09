//using System.Windows.Forms; // for Application.StartupPath

namespace SIS.Hardware.ComPort
{
    /// <summary>
    /// Persistent settings
    /// </summary>
    public class CommSettings
    {
        /// <summary> Port settings. </summary>
        public class Port
        {
            public static string PortName = "COM1";
            public static int BaudRate = 57600;
            public static int DataBits = 8;
            public static System.IO.Ports.Parity Parity = System.IO.Ports.Parity.None;
            public static System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.One;
            public static System.IO.Ports.Handshake Handshake = System.IO.Ports.Handshake.None;
            public static System.Text.Encoding Encoding = System.Text.Encoding.UTF8;
        }

        /// <summary> Option settings. </summary>
        public class Option
        {
            public enum AppendType
            {
                AppendNothing,
                AppendCR,
                AppendLF,
                AppendCRLF,
                AppendSC
            }

            public static AppendType AppendToSend = AppendType.AppendCR;
            public static bool HexOutput = false;
            public static bool MonoFont = true;
            public static bool LocalEcho = true;
            public static bool StayOnTop = false;
			public static bool FilterUseCase = false;
			public static string LogFileName = "";
		}

        /// <summary>
        ///   Read the settings from disk. </summary>
        public static void Read()
        {
            //Port.PortName = ConfigurationManager.AppSettings["PortName"];

            //Port.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
            //Port.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["Databits"]);
            //Port.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"]);
            //Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"]);
            //Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ConfigurationManager.AppSettings["HandShake"]);

            //Port.Encoding = System.Text.Encoding.GetEncoding(ConfigurationManager.AppSettings["Encoding"]);

            //Option.AppendToSend = (Option.AppendType)Enum.Parse(typeof(Option.AppendType), ini.ReadValue("Option", "AppendToSend", Option.AppendToSend.ToString()));
            //Option.HexOutput = bool.Parse(ini.ReadValue("Option", "HexOutput", Option.HexOutput.ToString()));
            //Option.MonoFont = bool.Parse(ini.ReadValue("Option", "MonoFont", Option.MonoFont.ToString()));
		}

        ///   Setup the COM settings. </summary>
        public static void SetupSettings(string __sPortName)
        {
            Port.PortName = __sPortName;

            //Port.BaudRate = Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]);
            //Port.DataBits = Convert.ToInt32(ConfigurationManager.AppSettings["Databits"]);
            //Port.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["Parity"]);
            //Port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ConfigurationManager.AppSettings["StopBits"]);
            //Port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ConfigurationManager.AppSettings["HandShake"]);

            //Port.Encoding = System.Text.Encoding.GetEncoding(ConfigurationManager.AppSettings["Encoding"]);

            //Option.AppendToSend = (Option.AppendType)Enum.Parse(typeof(Option.AppendType), ini.ReadValue("Option", "AppendToSend", Option.AppendToSend.ToString()));
            //Option.HexOutput = bool.Parse(ini.ReadValue("Option", "HexOutput", Option.HexOutput.ToString()));
            //Option.MonoFont = bool.Parse(ini.ReadValue("Option", "MonoFont", Option.MonoFont.ToString()));
        }
	}
}
