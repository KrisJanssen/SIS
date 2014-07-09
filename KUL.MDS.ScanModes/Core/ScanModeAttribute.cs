namespace SIS.ScanModes.Core
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ScanModeAttribute : Attribute
    {
        private string m_sName = null;

        public ScanModeAttribute(string sName)
        {
            this.m_sName = sName;
        }

        public string Name
        {
            get
            {
                return this.m_sName;
            }
        }
    }
}
