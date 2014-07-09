namespace SIS.Library
{
    using System;
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
            this.openFile();
            this._writelog("");
            this.closeFile();
        }


        private void openFile()
        {
            try
            {
                this.swLog = File.AppendText(this.strLogFile);
                this.isReady = true;
            }
            catch
            {
                this.isReady = false;
            }
        }

        private void closeFile()
        {

            if (this.isReady)
            {
                try
                {
                    this.swLog.Close();
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
            this.openFile();
            this._writelog(stub);
            this.closeFile();
            Console.WriteLine(stub);
        }

        private void _writelog(string msg)
        {
            if (this.isReady)
            {
                this.swLog.WriteLine(msg);
            }
            else
            {
                Console.WriteLine("Error Cannot write to log file.");
            }
        }
    }
}
