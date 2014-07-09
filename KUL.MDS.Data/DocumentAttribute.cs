// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentAttribute.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The document attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Data
{
    using System;

    /// <summary>
    /// The document attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class DocumentAttribute : Attribute
    {
        #region Fields

        /// <summary>
        /// The m_s extension.
        /// </summary>
        private string m_sExtension = null;

        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentAttribute"/> class.
        /// </summary>
        /// <param name="sName">
        /// The s name.
        /// </param>
        /// <param name="sExtension">
        /// The s extension.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public DocumentAttribute(string sName, string sExtension)
        {
            this.m_sName = sName;

            if (sExtension.Length == 0)
            {
                throw new Exception("You must specify and extension for the document");
            }

            if (sExtension[0] != '.')
            {
                throw new Exception("The extension for the document must begin with a period ('.')");
            }

            this.m_sExtension = sExtension;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the extension.
        /// </summary>
        public string Extension
        {
            get
            {
                return this.m_sExtension;
            }
        }

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