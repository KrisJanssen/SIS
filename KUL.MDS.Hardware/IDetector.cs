using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUL.MDS.Hardware
{
    interface IDetector
    {
        long TotalSamplesAcquired
        {
            get;
        }

        void SetupAcquisition(AcquisitionSettings __AcqSettings);

        void StartAcquisition();

        void StopAcquisition();

        void Read();

    }
}
