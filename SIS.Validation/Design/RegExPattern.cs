// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegExPattern.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   RegExItem.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation.Design
{
    using System;

    /// <summary>
    /// RegExItem.
    /// </summary>
    [Serializable()]
    public class RegExPattern
    {
        #region Fields

        /// <summary>
        /// The _ error message.
        /// </summary>
        private string _ErrorMessage = string.Empty;

        /// <summary>
        /// The _ pattern.
        /// </summary>
        private string _Pattern = string.Empty;

        /// <summary>
        /// The _ pattern name.
        /// </summary>
        private string _PatternName = string.Empty;

        /// <summary>
        /// The _ test value.
        /// </summary>
        private string _TestValue = string.Empty;

        #endregion

        #region Public Properties

        /// <summary>
        /// Suggested error message when pattern validation fail.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this._ErrorMessage;
            }

            set
            {
                this._ErrorMessage = value;
            }
        }

        /// <summary>
        /// Regular expression pattern string.
        /// </summary>
        public string Pattern
        {
            get
            {
                return this._Pattern;
            }

            set
            {
                this._Pattern = value;
            }
        }

        /// <summary>
        /// This make pattern easier to remember.
        /// </summary>
        public string PatternName
        {
            get
            {
                return this._PatternName;
            }

            set
            {
                this._PatternName = value;
            }
        }

        /// <summary>
        /// Sample value to give user an idea about the pattern.
        /// </summary>
        public string TestValue
        {
            get
            {
                return this._TestValue;
            }

            set
            {
                this._TestValue = value;
            }
        }

        #endregion
    }
}