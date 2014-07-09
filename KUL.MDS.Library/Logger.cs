namespace SIS.Library
{

    using System;
    using System.Drawing;
    using System.Data;
    using System.Reflection;
    using System.Windows.Forms;
    using System.IO;

    // Logger class 

    public enum LogType
    {

        Info = 1,
        Warning = 2,
        Error = 3
    }

    public class Logger
    {

        // Privates
        private bool isReady = false;
        private StreamWriter swLog;
        private string strLogFile;

        // Constructors
        public Logger(string LogFileName)
        {
            this.strLogFile = LogFileName;
            openFile();
            _writelog("");
            closeFile();
        }


        private void openFile()
        {
            try
            {
                swLog = File.AppendText(strLogFile);
                isReady = true;
            }
            catch
            {
                isReady = false;
            }
        }

        private void closeFile()
        {

            if (isReady)
            {
                try
                {
                    swLog.Close();
                }
                catch
                {

                }
            }
        }

        public static string GetNewLogFilename()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
        }

        public static string GetNewLogFilename(string __sFilename)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + __sFilename + " " + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
        }

        public void WriteLine(LogType logtype, string message)
        {

            string stub = DateTime.Now.ToString("dd-MM-yyyy @ HH:mm:ss  ");
            switch (logtype)
            {
                case LogType.Info:
                    stub += "Informational , ";
                    break;
                case LogType.Warning:
                    stub += "Warning       , ";
                    break;
                case LogType.Error:
                    stub += "Fatal error   , ";
                    break;
            }
            stub += message;
            openFile();
            _writelog(stub);
            closeFile();
            Console.WriteLine(stub);
        }

        private void _writelog(string msg)
        {
            if (isReady)
            {
                swLog.WriteLine(msg);
            }
            else
            {
                Console.WriteLine("Error Cannot write to log file.");
            }
        }
    }
}
