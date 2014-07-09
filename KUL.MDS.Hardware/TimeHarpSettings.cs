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
