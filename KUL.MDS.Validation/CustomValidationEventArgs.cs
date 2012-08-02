using System;

namespace KUL.MDS.Validation
{
	/// <summary>
	/// Delegate for custom validation methods.
	/// </summary>
	public delegate	void CustomValidationEventHandler(object sender, CustomValidationEventArgs e);

	/// <summary>
	/// Arguments of validation event.
	/// </summary>
	public class CustomValidationEventArgs : EventArgs
	{
		private object			_Value = null;
		private ValidationRule	_ValidationRule = null;

		/// <summary>
		/// Default Ctor.
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="vr"></param>
		public CustomValidationEventArgs(object Value, ValidationRule vr)
		{
			this._Value = Value;
			this._ValidationRule = vr;
		}

		/// <summary>
		/// Value to validate.
		/// </summary>
		public object Value
		{
			get { return _Value; }
		}

		/// <summary>
		/// Get or set validity of attached validation rule.
		/// </summary>
		public bool IsValid
		{
			get { return this._ValidationRule.IsValid; }
			set { this._ValidationRule.IsValid = value; }
		}

		/// <summary>
		/// Get or set error message to display when validation fail.
		/// </summary>
		public string ErrorMessage
		{
			get { return this._ValidationRule.ErrorMessage; }
			set { this._ValidationRule.ErrorMessage = value; }
		}

		/// <summary>
		/// Allow custom validation class to set IsValid and ErrorMessage
		/// value.
		/// </summary>
		public ValidationRule ValidationRule
		{
			get { return this._ValidationRule;}
		}
	}
}
