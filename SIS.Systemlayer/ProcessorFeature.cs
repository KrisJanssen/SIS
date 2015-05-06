using System;

namespace SIS.SystemLayer
{
    [Flags]
    public enum ProcessorFeature
    {
        DEP = 1,
        SSE = 2,
        SSE2 = 4,
        SSE3 = 8,
    }
}
