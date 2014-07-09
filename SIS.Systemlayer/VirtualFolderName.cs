// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VirtualFolderName.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The virtual folder name.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Systemlayer
{
    /// <summary>
    /// The virtual folder name.
    /// </summary>
    public enum VirtualFolderName
    {
        /// <summary>
        /// The user desktop.
        /// </summary>
        UserDesktop, 

        /// <summary>
        /// The user documents.
        /// </summary>
        UserDocuments, 

        /// <summary>
        /// The user pictures.
        /// </summary>
        UserPictures, 

        /// <summary>
        /// The user local app data.
        /// </summary>
        UserLocalAppData, 

        /// <summary>
        /// The user roaming app data.
        /// </summary>
        UserRoamingAppData, 

        /// <summary>
        /// The system program files.
        /// </summary>
        SystemProgramFiles, 
    }
}