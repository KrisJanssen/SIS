// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IniFile.cs" company="">
//   
// </copyright>
// <summary>
//   The ini file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

/// Read/Write values to an ini file
/// </summary>

namespace SIS.SerialTerminal
{
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// The ini file.
    /// </summary>
    public class IniFile
    {
        #region Fields

        /// <summary>
        /// The path.
        /// </summary>
        public string path;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IniFile"/> class.
        /// </summary>
        /// <param name="INIPath">
        /// The ini path.
        /// </param>
        public IniFile(string INIPath)
        {
            this.path = INIPath;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The read value.
        /// </summary>
        /// <param name="Section">
        /// The section.
        /// </param>
        /// <param name="Key">
        /// The key.
        /// </param>
        /// <param name="Default">
        /// The default.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ReadValue(string Section, string Key, string Default)
        {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default, buffer, 255, this.path);

            return buffer.ToString();
        }

        /// <summary>
        /// The read value.
        /// </summary>
        /// <param name="Section">
        /// The section.
        /// </param>
        /// <param name="Key">
        /// The key.
        /// </param>
        /// <param name="Default">
        /// The default.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int ReadValue(string Section, string Key, int Default)
        {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default.ToString(), buffer, 255, this.path);

            return int.Parse(buffer.ToString());
        }

        /// <summary>
        /// The write value.
        /// </summary>
        /// <param name="Section">
        /// The section.
        /// </param>
        /// <param name="Key">
        /// The key.
        /// </param>
        /// <param name="Value">
        /// The value.
        /// </param>
        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// The write value.
        /// </summary>
        /// <param name="Section">
        /// The section.
        /// </param>
        /// <param name="Key">
        /// The key.
        /// </param>
        /// <param name="Value">
        /// The value.
        /// </param>
        public void WriteValue(string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), this.path);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get private profile string.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="def">
        /// The def.
        /// </param>
        /// <param name="retVal">
        /// The ret val.
        /// </param>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section, 
            string key, 
            string def, 
            StringBuilder retVal, 
            int size, 
            string filePath);

        /// <summary>
        /// The write private profile string.
        /// </summary>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="val">
        /// The val.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        #endregion
    }
}