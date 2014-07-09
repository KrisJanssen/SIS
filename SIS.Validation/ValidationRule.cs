// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRule.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   ValidationRule is designed to be a simple as possible to
//   reduce overhead in run-time.  It's because each validation
//   rule can be attach to a control, so we can have a many
//   instances of this class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// ValidationRule is designed to be a simple as possible to
    /// reduce overhead in run-time.  It's because each validation
    /// rule can be attach to a control, so we can have a many
    /// instances of this class.
    /// </summary>
    [TypeConverter(typeof(KUL.MDS.Validation.Design.ValidationRuleConverter))]
    public class ValidationRule : ICloneable
    {
        #region Fields

        /// <summary>
        /// The result error message.
        /// </summary>
        internal string ResultErrorMessage = string.Empty;

        /// <summary>
        /// The _ data type.
        /// </summary>
        private ValidationDataType _DataType = ValidationDataType.String;

        /// <summary>
        /// The _ error message.
        /// </summary>
        private string _ErrorMessage = "%ControlName% is invalid.";

        /// <summary>
        /// The _ initial value.
        /// </summary>
        private string _InitialValue = string.Empty;

        /// <summary>
        /// The _ is case sensitive.
        /// </summary>
        private bool _IsCaseSensitive = true;

        /// <summary>
        /// The _ is required.
        /// </summary>
        private bool _IsRequired = false;

        /// <summary>
        /// The _ is valid.
        /// </summary>
        private bool _IsValid = true;

        /// <summary>
        /// The _ maximum value.
        /// </summary>
        private string _MaximumValue = string.Empty;

        /// <summary>
        /// The _ minimum value.
        /// </summary>
        private string _MinimumValue = string.Empty;

        /// <summary>
        /// The _ operator.
        /// </summary>
        private ValidationCompareOperator _Operator = ValidationCompareOperator.DataTypeCheck;

        /// <summary>
        /// The _ reg ex pattern.
        /// </summary>
        private string _RegExPattern = string.Empty;

        /// <summary>
        /// The _ value to compare.
        /// </summary>
        private string _ValueToCompare = string.Empty;

        #endregion

        #region Public Events

        /// <summary>
        /// Allow for attachment of custom validation method.
        /// </summary>
        public event CustomValidationEventHandler CustomValidationMethod;

        #endregion

        #region Public Properties

        /// <summary>
        /// Data Type of the validation.
        /// </summary>
        [DefaultValue(ValidationDataType.String)]
        [Category("Basic Settings")]
        [Description("DataType of control value.")]
        public ValidationDataType DataType
        {
            get
            {
                return this._DataType;
            }

            set
            {
                this._DataType = value;
            }
        }

        /// <summary>
        /// ErrorMessage result of validation.
        /// </summary>
        [DefaultValue("%ControlName% is invalid.")]
        [Category("Basic Settings")]
        [Description("Message to display/return iwhen validation failed.")]
        public string ErrorMessage
        {
            get
            {
                return this._ErrorMessage;
            }

            set
            {
                this._ErrorMessage = (value == null) ? string.Empty : value;
            }
        }

        /// <summary>
        /// A mandatory value does not necessarily mean a value other than "". 
        /// In some cases, a default control value might be used as a prompt 
        /// e.g. using "[Choose a value]" in a DropDownList control. In this case, 
        /// the required value must be different than the initial value of 
        /// "[Choose a value]". InitialValue supports this requirement. The 
        /// default is "".
        /// </summary>
        [DefaultValue("")]
        [Category("Basic Settings")]
        [Description(
            "Initial value doesn't necessarily be empty string.  It might be like [Choose a value] as in a a DropDown list."
            )]
        public string InitialValue
        {
            get
            {
                return this._InitialValue;
            }

            set
            {
                this._InitialValue = (value == null) ? string.Empty : value;
            }
        }

        /// <summary>
        /// Set validation case sensitivity.
        /// </summary>
        [DefaultValue(true)]
        [Category("Basic Settings")]
        [Description("Case sensitivity validation works best with String DataType.")]
        public bool IsCaseSensitive
        {
            get
            {
                return this._IsCaseSensitive;
            }

            set
            {
                this._IsCaseSensitive = value;
            }
        }

        /// <summary>
        /// Cause validation to check if field is required.
        /// </summary>
        [DefaultValue(false)]
        [Category("Basic Settings")]
        [Description("Make it so control require a value.  Validate to false if control value matches InitialValue.")]
        public bool IsRequired
        {
            get
            {
                return this._IsRequired;
            }

            set
            {
                this._IsRequired = value;
            }
        }

        /// <summary>
        /// Get validity of control value after Validate method is called.
        /// </summary>
        [DefaultValue(true)]
        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }

            set
            {
                this._IsValid = value;
            }
        }

        /// <summary>
        /// RangeValidator MaximumValue Value.
        /// </summary>
        [DefaultValue("")]
        [Category("Range Validation")]
        [Description("Lowest value the control can have.  Remember to set DataType when range is not of type String.")]
        public string MaximumValue
        {
            get
            {
                return this._MaximumValue;
            }

            set
            {
                this._MaximumValue = (value == null) ? string.Empty : value;
            }
        }

        /// <summary>
        /// RangeValidator Minimum Value.
        /// </summary>
        [DefaultValue("")]
        [Category("Range Validation")]
        [Description("Highest value the control can have.  Remember to set DataType when range is not of type String.")]
        public string MinimumValue
        {
            get
            {
                return this._MinimumValue;
            }

            set
            {
                this._MinimumValue = (value == null) ? string.Empty : value;
            }
        }

        /// <summary>
        /// Get or set operator to use to compare.
        /// </summary>
        [DefaultValue(ValidationCompareOperator.DataTypeCheck)]
        [Category("Compare Validation")]
        [Description(
            "Type of comparison to perform with ValueToCompare.  Default is data type checking if DataType is not String."
            )]
        public ValidationCompareOperator Operator
        {
            get
            {
                return this._Operator;
            }

            set
            {
                this._Operator = value;
            }
        }

        /// <summary>
        /// Regular Expression Pattern to use for validation.
        /// </summary>
        [DefaultValue("")]
        [Category("Regular Expression Validation")]
        [Description("Regular Expression Pattern to use for validation.  See accompanied regular expression bank...")]
        public string RegExPattern
        {
            get
            {
                return this._RegExPattern;
            }

            set
            {
                this._RegExPattern = (value == null) ? string.Empty : value;
            }
        }

        /// <summary>
        /// Get or set value use to compare with the control value.
        /// </summary>
        [DefaultValue("")]
        [Category("Compare Validation")]
        [Description("This is use in combination with Operator.")]
        public string ValueToCompare
        {
            get
            {
                return this._ValueToCompare;
            }

            set
            {
                this._ValueToCompare = (value == null) ? string.Empty : value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Compare two values.
        /// </summary>
        /// <param name="leftText">
        /// </param>
        /// <param name="rightText">
        /// </param>
        /// <param name="op">
        /// </param>
        /// <param name="vr">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Compare(string leftText, string rightText, ValidationCompareOperator op, ValidationRule vr)
        {
            if (false == vr.IsCaseSensitive && vr.DataType == ValidationDataType.String)
            {
                leftText = leftText.ToLower();
                rightText = rightText.ToLower();
            }

            return ValidationUtil.CompareValues(leftText, rightText, op, vr.DataType);
        }

        /// <summary>
        /// ValidationRule is memberwised cloneable.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delegate invoking of validation method.
        /// </summary>
        /// <param name="e">
        /// </param>
        protected internal virtual void OnCustomValidationMethod(CustomValidationEventArgs e)
        {
            if (this.CustomValidationMethod != null)
            {
                this.CustomValidationMethod(this, e);
            }
        }

        #endregion
    }
}