// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomValidationEventArgs.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Delegate for custom validation methods.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    using System;

    /// <summary>
    /// Delegate for custom validation methods.
    /// </summary>
    public delegate void CustomValidationEventHandler(object sender, CustomValidationEventArgs e);

    /// <summary>
    /// Arguments of validation event.
    /// </summary>
    public class CustomValidationEventArgs : EventArgs
    {
        #region Fields

        /// <summary>
        /// The _ validation rule.
        /// </summary>
        private ValidationRule _ValidationRule = null;

        /// <summary>
        /// The _ value.
        /// </summary>
        private object _Value = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidationEventArgs"/> class. 
        /// Default Ctor.
        /// </summary>
        /// <param name="Value">
        /// </param>
        /// <param name="vr">
        /// </param>
        public CustomValidationEventArgs(object Value, ValidationRule vr)
        {
            this._Value = Value;
            this._ValidationRule = vr;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Get or set error message to display when validation fail.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this._ValidationRule.ErrorMessage;
            }

            set
            {
                this._ValidationRule.ErrorMessage = value;
            }
        }

        /// <summary>
        /// Get or set validity of attached validation rule.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this._ValidationRule.IsValid;
            }

            set
            {
                this._ValidationRule.IsValid = value;
            }
        }

        /// <summary>
        /// Allow custom validation class to set IsValid and ErrorMessage
        /// value.
        /// </summary>
        public ValidationRule ValidationRule
        {
            get
            {
                return this._ValidationRule;
            }
        }

        /// <summary>
        /// Value to validate.
        /// </summary>
        public object Value
        {
            get
            {
                return this._Value;
            }
        }

        #endregion
    }
}