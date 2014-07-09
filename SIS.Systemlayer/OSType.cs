// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OSType.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The os type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    /// <summary>
    /// The os type.
    /// </summary>
    public enum OSType
    {
        /// <summary>
        /// The unknown.
        /// </summary>
        Unknown = 0, 

        /// <summary>
        /// The workstation.
        /// </summary>
        Workstation = (int)NativeConstants.VER_NT_WORKSTATION, 

        /// <summary>
        /// The domain controller.
        /// </summary>
        DomainController = (int)NativeConstants.VER_NT_DOMAIN_CONTROLLER, 

        /// <summary>
        /// The server.
        /// </summary>
        Server = (int)NativeConstants.VER_NT_SERVER, 
    }
}