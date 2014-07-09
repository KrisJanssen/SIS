// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormAssociate.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Used by classes to indicate they are associated with a certain Form, even if
//   they are not contained within the Form. To this end, they are an Associate of
//   the Form.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System.Windows.Forms;

    /// <summary>
    /// Used by classes to indicate they are associated with a certain Form, even if
    /// they are not contained within the Form. To this end, they are an Associate of
    /// the Form.
    /// </summary>
    public interface IFormAssociate
    {
        #region Public Properties

        /// <summary>
        /// Gets the Form that this object is associated with, or null if there is
        /// no association.
        /// </summary>
        Form AssociatedForm { get; }

        #endregion
    }
}