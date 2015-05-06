using System;

namespace SIS.ScanModes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ScanModeAttribute : Attribute
    {
        private string m_sName = null;

        public ScanModeAttribute(string sName)
        {
            m_sName = sName;
        }

        public string Name
        {
            get
            {
                return m_sName;
            }
        }
    }
}
