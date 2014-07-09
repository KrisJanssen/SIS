// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationProvider.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Provider validation properties to controls that can be validated.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Provider validation properties to controls that can be validated.
    /// </summary>
    [ProvideProperty("ValidationRule", typeof(Control))]
    [Designer(typeof(KUL.MDS.Validation.Design.ValidationProviderDesigner))]
    [ToolboxBitmap(typeof(KUL.MDS.Validation.ValidationProvider), "Validation.ico")]
    public class ValidationProvider : System.ComponentModel.Component, IExtenderProvider
    {
        #region Fields

        /// <summary>
        /// The _ default validation rule.
        /// </summary>
        private ValidationRule _DefaultValidationRule = new ValidationRule();

        /// <summary>
        /// The _ error provider.
        /// </summary>
        private ErrorProvider _ErrorProvider = new ErrorProvider();

        /// <summary>
        /// The _ validation rules.
        /// </summary>
        private Hashtable _ValidationRules = new Hashtable();

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationProvider"/> class. 
        /// Designer Ctor.
        /// </summary>
        /// <param name="container">
        /// </param>
        public ValidationProvider(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationProvider"/> class. 
        /// Default Ctor.
        /// </summary>
        public ValidationProvider()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// BlinkRate of ErrorIcon.
        /// </summary>
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("BlinkRate of ErrorIcon.")]
        [Category("Behavior")]
        [DefaultValue(250)]
        public int BlinkRate
        {
            get
            {
                return this._ErrorProvider.BlinkRate;
            }

            set
            {
                this._ErrorProvider.BlinkRate = value;
            }
        }

        /// <summary>
        /// Get or set Blink Behavior.
        /// </summary>
        [DefaultValue(0)]
        [Category("Behavior")]
        [Description("Blink Behavior.")]
        public ErrorBlinkStyle BlinkStyle
        {
            get
            {
                return this._ErrorProvider.BlinkStyle;
            }

            set
            {
                this._ErrorProvider.BlinkStyle = value;
            }
        }

        /// <summary>
        /// Icon display when validation failed.
        /// </summary>
        [Category("Appearance")]
        [Description("Icon display when validation failed.")]
        [Localizable(true)]
        public Icon Icon
        {
            get
            {
                return this._ErrorProvider.Icon;
            }

            set
            {
                this._ErrorProvider.Icon = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determine if ValidationProvider support a component.
        /// </summary>
        /// <param name="extendee">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool CanExtend(object extendee)
        {
            if ((extendee is TextBox) || (extendee is ComboBox))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get Error Icon alignment.
        /// </summary>
        /// <param name="control">
        /// </param>
        /// <returns>
        /// The <see cref="ErrorIconAlignment"/>.
        /// </returns>
        [DefaultValue(3)]
        [Category("Appearance")]
        [Localizable(true)]
        [Description("Get Error Icon alignment.")]
        public ErrorIconAlignment GetIconAlignment(Control control)
        {
            return this._ErrorProvider.GetIconAlignment(control);
        }

        /// <summary>
        /// Get Error Icon padding.
        /// </summary>
        /// <param name="control">
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [Description("Get Error Icon padding.")]
        [DefaultValue(0)]
        [Localizable(true)]
        [Category("Appearance")]
        public int GetIconPadding(Control control)
        {
            return this._ErrorProvider.GetIconPadding(control);
        }

        /// <summary>
        /// Get validation rule.
        /// </summary>
        /// <param name="inputComponent">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        [DefaultValue(null)]
        [Category("Data")]
        [Editor(typeof(KUL.MDS.Validation.Design.ValidationRuleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public ValidationRule GetValidationRule(object inputComponent)
        {
            return this._ValidationRules[inputComponent] as ValidationRule;
        }

        /// <summary>
        /// Set Error Icon alignment.
        /// </summary>
        /// <param name="control">
        /// </param>
        /// <param name="value">
        /// </param>
        public void SetIconAlignment(Control control, ErrorIconAlignment value)
        {
            this._ErrorProvider.SetIconAlignment(control, value);
        }

        /// <summary>
        /// Set Error Icon padding.
        /// </summary>
        /// <param name="control">
        /// </param>
        /// <param name="padding">
        /// </param>
        public void SetIconPadding(Control control, int padding)
        {
            this._ErrorProvider.SetIconPadding(control, padding);
        }

        /// <summary>
        /// Set validation rule.
        /// </summary>
        /// <param name="inputComponent">
        /// </param>
        /// <param name="vr">
        /// </param>
        public void SetValidationRule(object inputComponent, ValidationRule vr)
        {
            if (inputComponent != null)
            {
                // only throw error in DesignMode
                if (base.DesignMode)
                {
                    if (!this.CanExtend(inputComponent))
                    {
                        throw new InvalidOperationException(
                            inputComponent.GetType().ToString() + " is not supported by the validation provider.");
                    }

                    if (!this.IsDefaultRange(vr)
                        && ValidationRule.Compare(
                            vr.MinimumValue, 
                            vr.MaximumValue, 
                            ValidationCompareOperator.GreaterThanEqual, 
                            vr))
                    {
                        throw new ArgumentException("MinimumValue must not be greater than or equal to MaximumValue.");
                    }
                }

                ValidationRule vrOld = this._ValidationRules[inputComponent] as ValidationRule;

                // if new rule is valid and in not DesignMode, clone rule
                if ((vr != null) && !base.DesignMode)
                {
                    vr = vr.Clone() as ValidationRule;
                }

                if (vr == null)
                {
                    // if new is null, no more validation
                    this._ValidationRules.Remove(inputComponent);
                }
                else if (vrOld == null)
                {
                    this._ValidationRules.Add(inputComponent, vr);
                }
                else if ((vr != null) && (vrOld != null))
                {
                    this._ValidationRules[inputComponent] = vr;
                }
            }
        }

        /// <summary>
        /// Perform validation on all controls.
        /// </summary>
        /// <returns>False if any control contains invalid data.</returns>
        public bool Validate()
        {
            bool bIsValid = true;
            ValidationRule vr = null;
            foreach (Control ctrl in this._ValidationRules.Keys)
            {
                this.Validate(ctrl);

                vr = this.GetValidationRule(ctrl);
                if (vr != null && vr.IsValid == false)
                {
                    bIsValid = false;
                }
            }

            return bIsValid;
        }

        /// <summary>
        /// Get validation error messages.
        /// </summary>
        /// <param name="showErrorIcon">
        /// The show Error Icon.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ValidationMessages(bool showErrorIcon)
        {
            StringBuilder sb = new StringBuilder();
            ValidationRule vr = null;
            foreach (Control ctrl in this._ValidationRules.Keys)
            {
                vr = this.GetValidationRule(ctrl);
                if (vr != null)
                {
                    if (vr.IsValid == false)
                    {
                        vr.ResultErrorMessage += vr.ErrorMessage.Replace("%ControlName%", ctrl.Name);
                        sb.Append(vr.ResultErrorMessage);
                        sb.Append(Environment.NewLine);
                    }

                    if (showErrorIcon)
                    {
                        this._ErrorProvider.SetError(ctrl, vr.ResultErrorMessage);
                    }
                    else
                    {
                        this._ErrorProvider.SetError(ctrl, null);
                    }
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Perform CompareValidate on a specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// true if control has no validation rule.
        /// </returns>
        private ValidationRule CompareValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null)
            {
                if (this._DefaultValidationRule.ValueToCompare.Equals(vr.ValueToCompare)
                    && this._DefaultValidationRule.Operator.Equals(vr.Operator))
                {
                    return vr;
                }

                vr.IsValid = ValidationRule.Compare(ctrl.Text, vr.ValueToCompare, vr.Operator, vr);
            }

            return vr;
        }

        /// <summary>
        /// Perform Custom Validation on specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        private ValidationRule CustomValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null)
            {
                CustomValidationEventArgs e = new CustomValidationEventArgs(ctrl.Text, vr);
                vr.OnCustomValidationMethod(e);
            }

            return vr;
        }

        /// <summary>
        /// Validate Data Type.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        private ValidationRule DataTypeValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null && vr.Operator.Equals(ValidationCompareOperator.DataTypeCheck))
            {
                if (vr.DataType.Equals(this._DefaultValidationRule.DataType))
                {
                    return vr;
                }

                System.Web.UI.WebControls.ValidationDataType vdt =
                    (System.Web.UI.WebControls.ValidationDataType)
                    Enum.Parse(typeof(System.Web.UI.WebControls.ValidationDataType), vr.DataType.ToString());

                vr.IsValid = ValidationUtil.CanConvert(ctrl.Text, vdt);
            }

            return vr;
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        /// <summary>
        /// Check if validation rule range is default.
        /// </summary>
        /// <param name="vr">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool IsDefaultRange(ValidationRule vr)
        {
            return this._DefaultValidationRule.MinimumValue.Equals(vr.MinimumValue)
                   && this._DefaultValidationRule.MaximumValue.Equals(vr.MaximumValue);
        }

        /// <summary>
        /// Perform Range Validation on specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        private ValidationRule RangeValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null)
            {
                if (this.IsDefaultRange(vr))
                {
                    return vr;
                }

                vr.IsValid = ValidationRule.Compare(
                    ctrl.Text, 
                    vr.MinimumValue, 
                    ValidationCompareOperator.GreaterThanEqual, 
                    vr);

                if (vr.IsValid)
                {
                    vr.IsValid = ValidationRule.Compare(
                        ctrl.Text, 
                        vr.MaximumValue, 
                        ValidationCompareOperator.LessThanEqual, 
                        vr);
                }
            }

            return vr;
        }

        /// <summary>
        /// Perform Regular Expression Validation on a specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        private ValidationRule RegularExpressionValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null)
            {
                try
                {
                    if (this._DefaultValidationRule.RegExPattern.Equals(vr.RegExPattern))
                    {
                        return vr;
                    }

                    vr.IsValid = ValidationUtil.ValidateRegEx(ctrl.Text, vr.RegExPattern);
                }
                catch (Exception ex)
                {
                    vr.ResultErrorMessage = "RegEx Validation Exception: " + ex.Message + Environment.NewLine;
                    vr.IsValid = false;
                }
            }

            return vr;
        }

        /// <summary>
        /// Perform RequiredField Validation on a specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="ValidationRule"/>.
        /// </returns>
        private ValidationRule RequiredFieldValidate(Control ctrl)
        {
            ValidationRule vr = this._ValidationRules[ctrl] as ValidationRule;

            if (vr != null && vr.IsRequired)
            {
                ValidationRule vr2 = new ValidationRule();
                vr.IsValid = !ValidationRule.Compare(ctrl.Text, vr.InitialValue, ValidationCompareOperator.Equal, vr);
            }

            return vr;
        }

        /// <summary>
        /// Perform validation on specific control.
        /// </summary>
        /// <param name="ctrl">
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Validate(Control ctrl)
        {
            ValidationRule vr = this.GetValidationRule(ctrl);

            if (vr != null)
            {
                vr.ResultErrorMessage = string.Empty;
                vr.IsValid = true;
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.DataTypeValidate(ctrl);
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.CompareValidate(ctrl);
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.CustomValidate(ctrl);
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.RangeValidate(ctrl);
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.RegularExpressionValidate(ctrl);
            }

            if (vr == null || vr.IsValid)
            {
                vr = this.RequiredFieldValidate(ctrl);
            }

            return (vr == null) ? true : vr.IsValid;
        }

        #endregion
    }
}