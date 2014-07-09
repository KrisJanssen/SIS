namespace SIS.Hardware
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
