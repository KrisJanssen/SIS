using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUL.MDS.Hardware
{
    class APDSettings : AcquisitionSettings
    {
        APDSettings()
        {
            this.m_iDetectorType = (int)DetectorTypes.TimeHarp;
        }
    }
}
