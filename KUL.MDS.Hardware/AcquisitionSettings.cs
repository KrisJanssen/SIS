using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KUL.MDS.Hardware
{
    public enum DetectorTypes { APD, TimeHarp };

    public abstract class AcquisitionSettings
    {
        protected int m_iDetectorType;

        public int DetectorType
        {
            get
            {
                return this.m_iDetectorType;
            }
        }

    }
}
