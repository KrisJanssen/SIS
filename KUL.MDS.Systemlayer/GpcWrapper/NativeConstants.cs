// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeConstants.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The native constants.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer.GpcWrapper
{
    /// <summary>
    /// The native constants.
    /// </summary>
    internal static class NativeConstants
    {
        #region Enums

        /// <summary>
        /// The gpc_op.
        /// </summary>
        public enum gpc_op
        {
            /// <summary>
            /// The gp c_ diff.
            /// </summary>
            GPC_DIFF = 0, 

            /// <summary>
            /// The gp c_ int.
            /// </summary>
            GPC_INT = 1, 

            /// <summary>
            /// The gp c_ xor.
            /// </summary>
            GPC_XOR = 2, 

            /// <summary>
            /// The gp c_ union.
            /// </summary>
            GPC_UNION = 3
        }

        #endregion
    }
}