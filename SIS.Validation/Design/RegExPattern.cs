using System;

namespace SIS.Validation.Design
{
	/// <summary>
	/// RegExItem.
	/// </summary>
	[Serializable()]
	public class RegExPattern
	{
		private string _PatternName	= string.Empty;
		/// <summary>
		/// This make pattern easier to remember.
		/// </summary>
		public string PatternName
		{
			get { return _PatternName; }
			set { _PatternName = value; }
		}

		private string _ErrorMessage = string.Empty;
		/// <summary>
		/// Suggested error message when pattern validation fail.
		/// </summary>
		public string ErrorMessage
		{
			get { return _ErrorMessage; }
			set { _ErrorMessage = value; }
		}

		private string _Pattern = string.Empty;
		/// <summary>
		/// Regular expression pattern string.
		/// </summary>
		public string Pattern
		{
			get { return _Pattern; }
			set { _Pattern = value; }
		}

		private string _TestValue = string.Empty;
		/// <summary>
		/// Sample value to give user an idea about the pattern.
		/// </summary>
		public string TestValue
		{
			get { return _TestValue; }
			set { _TestValue = value; }
		}
	}
}
