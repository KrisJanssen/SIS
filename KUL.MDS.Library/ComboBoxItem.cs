using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Library
{
    /// <summary>
    /// Generic class to store objects of type T in a ComboBox whilst also providing a human readable name for the object.
    /// </summary>
    /// <typeparam name="T">The type of the object to store in the ComboBox</typeparam>
    public class ComboBoxItem<T>
    {
        private string m_sName;
        private T m_tValue;

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

        /// <summary>
        /// ComboBoxItem constructor.
        /// </summary>
        /// <param name="name">A name for the object to store.</param>
        /// <param name="value">The object to store.</param>
        public ComboBoxItem(string name, T value)
        {
            this.m_sName = name; this.m_tValue = value;
        }

        /// <summary>
        /// Generates the text actually shown in the combo box.
        /// </summary>
        /// <returns>String representation of the object to store.</returns>
        public override string ToString()
        {
            return this.m_sName;
        }
    }
}
