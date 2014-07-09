// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboBoxItem.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Generic class to store objects of type T in a ComboBox whilst also providing a human readable name for the object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.Library
{
    /// <summary>
    /// Generic class to store objects of type T in a ComboBox whilst also providing a human readable name for the object.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object to store in the ComboBox
    /// </typeparam>
    public class ComboBoxItem<T>
    {
        #region Fields

        /// <summary>
        /// The m_s name.
        /// </summary>
        private string m_sName;

        /// <summary>
        /// The m_t value.
        /// </summary>
        private T m_tValue;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBoxItem{T}"/> class. 
        /// ComboBoxItem constructor.
        /// </summary>
        /// <param name="name">
        /// A name for the object to store.
        /// </param>
        /// <param name="value">
        /// The object to store.
        /// </param>
        public ComboBoxItem(string name, T value)
        {
            this.m_sName = name;
            this.m_tValue = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The object stored in a ComboBoxItem.
        /// </summary>
        public T Value
        {
            get
            {
                return this.m_tValue;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Generates the text actually shown in the combo box.
        /// </summary>
        /// <returns>String representation of the object to store.</returns>
        public override string ToString()
        {
            return this.m_sName;
        }

        #endregion
    }
}