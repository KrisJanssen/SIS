using System;

namespace SIS.SystemLayer.GpcWrapper
{
    internal static class NativeConstants
    {
        public enum gpc_op                                 
        {
            GPC_DIFF = 0,
            GPC_INT = 1,
            GPC_XOR = 2,
            GPC_UNION = 3
        }
    }
}
