// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutePrivelige.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The execute privilege.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    /// <summary>
    /// The execute privilege.
    /// </summary>
    public enum ExecutePrivilege
    {
        /// <summary>
        /// The process is started with default permissions: either the same as the invoker,
        /// or those required by the executable's manifest.
        /// </summary>
        AsInvokerOrAsManifest, 

        /// <summary>
        /// The process is required to run with administrator privilege. If the user does not
        /// have administrator privilege, nor has the ability to obtain it, then the operation
        /// will fail.
        /// </summary>
        RequireAdmin, 

        /// <summary>
        /// The process is required to run with normal privilege. On some systems this may
        /// not be possible, and as such this will have the same effect as AsInvokerOrAsManifest.
        /// </summary>
        /// <remarks>
        /// This flag only has an effect in Windows Vista from a process that already has
        /// administrator privilege.
        /// </remarks>
        RequireNonAdminIfPossible
    }
}