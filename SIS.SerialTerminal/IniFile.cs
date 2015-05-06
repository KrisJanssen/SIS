using System;
using System.Text;
using System.Runtime.InteropServices;

/// <summary>
/// Read/Write values to an ini file
/// </summary>
namespace SIS.SerialTerminal
{
    public class IniFile 
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key,string val,string filePath);
        
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
            string key,string def, StringBuilder retVal, int size,string filePath);

        public IniFile(string INIPath){
            path = INIPath;
        }

        public void WriteValue(string Section, string Key, string Value) {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }
        
        public string ReadValue(string Section, string Key, string Default) {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default, buffer, 255, this.path);

            return buffer.ToString();
        }

        public void WriteValue(string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), this.path);
        }

        public int ReadValue(string Section, string Key, int Default)
        {
            StringBuilder buffer = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default.ToString(), buffer, 255, this.path);

            return int.Parse(buffer.ToString());
        }
    }
}
