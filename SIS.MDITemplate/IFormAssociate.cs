using System;
using System.Windows.Forms;

namespace SIS.MDITemplate
{
    /// <summary>
    /// Used by classes to indicate they are associated with a certain Form, even if
    /// they are not contained within the Form. To this end, they are an Associate of
    /// the Form.
    /// </summary>
    public interface IFormAssociate
    {
        /// <summary>
        /// Gets the Form that this object is associated with, or null if there is
        /// no association.
        /// </summary>
        Form AssociatedForm
        {
            get;
        }
    }
}
