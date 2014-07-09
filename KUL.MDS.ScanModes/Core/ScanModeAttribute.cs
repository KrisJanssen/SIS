// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScanModeAttribute.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The scan mode attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.ScanModes.Core
{
    using System;

    /// <summary>
    /// The scan mode attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ScanModeAttribute : Attribute
    {
        #region Fields

        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanModeAttribute"/> class.
        /// </summary>
        /// <param name="sName">
        /// The s name.
        /// </param>
        public ScanModeAttribute(string sName)
        {
            this.m_sName = sName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.m_sName;
            }
        }

        #endregion
    }
}