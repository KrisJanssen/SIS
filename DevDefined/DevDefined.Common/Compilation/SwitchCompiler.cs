// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwitchCompiler.cs" company="">
//   
// </copyright>
// <summary>
//   The switch compiler.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Compilation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The switch compiler.
    /// </summary>
    public static class SwitchCompiler
    {
        #region Static Fields

        /// <summary>
        /// The string index.
        /// </summary>
        private static readonly MethodInfo StringIndex = typeof(string).GetMethod("get_Chars");

        /// <summary>
        /// The string length.
        /// </summary>
        private static readonly MethodInfo StringLength = typeof(string).GetMethod("get_Length");

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The create switch.
        /// </summary>
        /// <param name="caseDictionary">
        /// The case dictionary.
        /// </param>
        /// <typeparam name="TReturn">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Func"/>.
        /// </returns>
        public static Func<string, TReturn> CreateSwitch<TReturn>(IDictionary<string, TReturn> caseDictionary)
        {
            var cases = new List<Case<TReturn>>(caseDictionary.Count);
            foreach (var kv in caseDictionary)
            {
                cases.Add(new Case<TReturn> { C = kv.Key, R = kv.Value });
            }

            ParameterExpression p = Expression.Parameter(typeof(string), "p");
            Expression<Func<string, TReturn>> expr =
                Expression.Lambda<Func<string, TReturn>>(
                    BuildStringLength(p, cases.ToOrderedArray(s => s.C.Length), 0, cases.Count - 1), 
                    new[] { p });
            Func<string, TReturn> del = expr.Compile();
            return del;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build string char.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <param name="pairs">
        /// The pairs.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="lower">
        /// The lower.
        /// </param>
        /// <param name="upper">
        /// The upper.
        /// </param>
        /// <typeparam name="TR">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        private static Expression BuildStringChar<TR>(
            ParameterExpression p, 
            Case<TR>[] pairs, 
            int index, 
            int lower, 
            int upper)
        {
            if (pairs.TakePage(lower, upper).All(c => c.R.Equals(pairs[lower].R)))
            {
                return Expression.Constant(pairs[lower].R);
            }

            int middle = MidPoint(lower, upper);
            if (pairs[middle].C[index] == pairs[lower].C[index])
            {
                return BuildStringChar(p, pairs, index + 1, lower, upper);
            }

            middle = pairs.FirstDifferentDown(middle, (c) => c.C[index]);
            return
                Expression.Condition(
                    Expression.LessThan(
                        Expression.Call(p, StringIndex, Expression.Constant(index)), 
                        Expression.Constant(pairs[middle + 1].C[index])), 
                    BuildStringChar(p, pairs, index, lower, middle), 
                    BuildStringChar(p, pairs, index, middle + 1, upper));
        }

        /// <summary>
        /// The build string length.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <param name="pairs">
        /// The pairs.
        /// </param>
        /// <param name="lower">
        /// The lower.
        /// </param>
        /// <param name="upper">
        /// The upper.
        /// </param>
        /// <typeparam name="TR">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        private static Expression BuildStringLength<TR>(ParameterExpression p, Case<TR>[] pairs, int lower, int upper)
        {
            int middle = MidPoint(lower, upper);
            if (pairs[lower].C.Length == pairs[middle].C.Length)
            {
                return BuildStringChar(p, pairs.TakePage(lower, upper).ToOrderedArray(c => c.C), 0, 0, upper - lower);
            }

            middle = pairs.FirstDifferentDown(middle, (c) => c.C.Length);
            return
                Expression.Condition(
                    Expression.LessThan(
                        Expression.Call(p, StringLength), 
                        Expression.Constant(pairs[middle + 1].C.Length)), 
                    BuildStringLength(p, pairs, lower, middle), 
                    BuildStringLength(p, pairs, middle + 1, upper));
        }

        /// <summary>
        /// The mid point.
        /// </summary>
        /// <param name="lower">
        /// The lower.
        /// </param>
        /// <param name="upper">
        /// The upper.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private static int MidPoint(int lower, int upper)
        {
            return ((upper - lower + 1) / 2) + lower;
        }

        #endregion

        /// <summary>
        /// The case.
        /// </summary>
        /// <typeparam name="TR">
        /// </typeparam>
        private struct Case<TR>
        {
            #region Public Properties

            /// <summary>
            /// Gets or sets the c.
            /// </summary>
            public string C { get; set; }

            /// <summary>
            /// Gets or sets the r.
            /// </summary>
            public TR R { get; set; }

            #endregion

            #region Public Methods and Operators

            /// <summary>
            /// The to string.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public override string ToString()
            {
                return this.C + " " + this.R;
            }

            #endregion
        }
    }
}