// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Do.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The do.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Base
{
    using System;

    /// <summary>
    /// The do.
    /// </summary>
    public static class Do
    {
        #region Public Methods and Operators

        /// <summary>
        /// The generate test.
        /// </summary>
        /// <param name="generate">
        /// The generate.
        /// </param>
        /// <param name="test">
        /// The test.
        /// </param>
        /// <param name="ifTrue">
        /// The if true.
        /// </param>
        /// <param name="ifFalse">
        /// The if false.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void GenerateTest<T>(
            Function<T> generate, 
            Function<bool, T> test, 
            Procedure<T> ifTrue, 
            Procedure<T> ifFalse)
        {
            Test(generate(), test, ifTrue, ifFalse);
        }

        /// <summary>
        /// The test.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="testFn">
        /// The test fn.
        /// </param>
        /// <param name="ifTrueFn">
        /// The if true fn.
        /// </param>
        /// <param name="ifFalseFn">
        /// The if false fn.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Test<T>(T value, Function<bool, T> testFn, Procedure<T> ifTrueFn, Procedure<T> ifFalseFn)
        {
            (testFn(value) ? ifTrueFn : ifFalseFn)(value);
        }

        /// <summary>
        /// The try bool.
        /// </summary>
        /// <param name="actionProcedure">
        /// The action procedure.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool TryBool(Procedure actionProcedure)
        {
            try
            {
                actionProcedure();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The try catch.
        /// </summary>
        /// <param name="actionFunction">
        /// The action function.
        /// </param>
        /// <param name="catchClause">
        /// The catch clause.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T TryCatch<T>(Function<T> actionFunction, Function<T, Exception> catchClause)
        {
            T returnVal;

            try
            {
                returnVal = actionFunction();
            }
            catch (Exception ex)
            {
                returnVal = catchClause(ex);
            }

            return returnVal;
        }

        #endregion
    }
}