// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationCompareOperator.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   Operations that can be perform in Compare Validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation
{
    /// <summary>
    /// Operations that can be perform in Compare Validation.
    /// </summary>
    public enum ValidationCompareOperator
    {
        /// <summary>
        /// Default - Check component DataType.
        /// </summary>
        DataTypeCheck, 

        /// <summary>
        /// Compare is equal to ValueToCompare.
        /// </summary>
        Equal, 

        /// <summary>
        /// Compare is greater than ValueToCompare.
        /// </summary>
        GreaterThan, 

        /// <summary>
        /// Compare greater than or equal to ValueToCompare.
        /// </summary>
        GreaterThanEqual, 

        /// <summary>
        /// Compare is less than ValueToCompare.
        /// </summary>
        LessThan, 

        /// <summary>
        /// Compare is greater than ValueToCompare.
        /// </summary>
        LessThanEqual, 

        /// <summary>
        /// Compare is not equal to ValueToCompare.
        /// </summary>
        NotEqual
    }
}