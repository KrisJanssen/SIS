namespace SIS.Hardware
{
    class APDSettings : AcquisitionSettings
    {
        APDSettings()
        {
            this.m_iDetectorType = (int)DetectorTypes.TimeHarp;
        }
    }
}
