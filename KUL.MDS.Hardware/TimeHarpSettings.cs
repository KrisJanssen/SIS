using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Hardware
{
    class TimeHarpSettings : AcquisitionSettings
    {
        TimeHarpSettings()
        {
            this.m_iDetectorType = (int)DetectorTypes.TimeHarp;
        }
    }
}
