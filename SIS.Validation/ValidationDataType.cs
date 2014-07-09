// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationDataType.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Date Type of the component.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    /// <summary>
    /// Date Type of the component.
    /// </summary>
    public enum ValidationDataType
    {
        /// <summary>
        /// Monetary data type.
        /// </summary>
        Currency, 

        /// <summary>
        /// DateTime data type.
        /// </summary>
        Date, 

        /// <summary>
        /// Double data type.
        /// </summary>
        Double, 

        /// <summary>
        /// Integer data type.
        /// </summary>
        Integer, 

        /// <summary>
        /// Default - string data type.
        /// </summary>
        String
    }
}