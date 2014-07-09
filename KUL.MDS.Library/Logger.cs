// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logger.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The log type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library
{
    using System;
    using System.IO;

    // Logger class 

    /// <summary>
    /// The log type.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// The info.
        /// </summary>
        Info = 1, 

        /// <summary>
        /// The warning.
        /// </summary>
        Warning = 2, 

        /// <summary>
        /// The error.
        /// </summary>
        Error = 3
    }

    /// <summary>
    /// The logger.
    /// </summary>
    public class Logger
    {
        // Privates
        #region Fields

        /// <summary>
        /// The is ready.
        /// </summary>
        private bool isReady = false;

        /// <summary>
        /// The str log file.
        /// </summary>
        private string strLogFile;

        /// <summary>
        /// The sw log.
        /// </summary>
        private StreamWriter swLog;

        #endregion

        // Constructors
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="LogFileName">
        /// The log file name.
        /// </param>
        public Logger(string LogFileName)
        {
            this.strLogFile = LogFileName;
            this.openFile();
            this._writelog(string.Empty);
            this.closeFile();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get new log filename.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNewLogFilename()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                   + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
        }

        /// <summary>
        /// The get new log filename.
        /// </summary>
        /// <param name="__sFilename">
        /// The __s filename.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetNewLogFilename(string __sFilename)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + __sFilename + " "
                   + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
        }

        /// <summary>
        /// The write line.
        /// </summary>
        /// <param name="logtype">
        /// The logtype.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
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

        #endregion

        #region Methods

        /// <summary>
        /// The _writelog.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
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

        /// <summary>
        /// The close file.
        /// </summary>
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

        /// <summary>
        /// The open file.
        /// </summary>
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

        #endregion
    }
}