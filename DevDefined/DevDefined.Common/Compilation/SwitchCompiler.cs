using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DevDefined.Common.Compilation
{
    public static class SwitchCompiler
    {
        private static readonly MethodInfo StringIndex = typeof (string).GetMethod("get_Chars");
        private static readonly MethodInfo StringLength = typeof (string).GetMethod("get_Length");

        public static Func<string, TReturn> CreateSwitch<TReturn>(IDictionary<string, TReturn> caseDictionary)
        {
            var cases = new List<Case<TReturn>>(caseDictionary.Count);
            foreach (var kv in caseDictionary)
                cases.Add(new Case<TReturn> {C = kv.Key, R = kv.Value});
            ParameterExpression p = Expression.Parameter(typeof (string), "p");
            Expression<Func<string, TReturn>> expr = Expression.Lambda<Func<string, TReturn>>(
                BuildStringLength(p, cases.ToOrderedArray(s => s.C.Length), 0, cases.Count - 1),
                new[] {p}
                );
            Func<string, TReturn> del = expr.Compile();
            return del;
        }

        private static Expression BuildStringLength<TR>(ParameterExpression p, Case<TR>[] pairs, int lower, int upper)
        {
            int middle = MidPoint(lower, upper);
            if (pairs[lower].C.Length == pairs[middle].C.Length)
                return BuildStringChar(p, pairs.TakePage(lower, upper).ToOrderedArray(c => c.C), 0, 0, upper - lower);
            middle = pairs.FirstDifferentDown(middle, (c) => c.C.Length);
            return Expression.Condition(
                Expression.LessThan(Expression.Call(p, StringLength), Expression.Constant(pairs[middle + 1].C.Length)),
                BuildStringLength(p, pairs, lower, middle),
                BuildStringLength(p, pairs, middle + 1, upper)
                );
        }

        private static Expression BuildStringChar<TR>(ParameterExpression p, Case<TR>[] pairs, int index, int lower, int upper)
        {
            if (pairs.TakePage(lower, upper).All(c => c.R.Equals(pairs[lower].R)))
                return Expression.Constant(pairs[lower].R);
            int middle = MidPoint(lower, upper);
            if (pairs[middle].C[index] == pairs[lower].C[index])
                return BuildStringChar(p, pairs, index + 1, lower, upper);
            middle = pairs.FirstDifferentDown(middle, (c) => c.C[index]);
            return Expression.Condition(
                Expression.LessThan(Expression.Call(p, StringIndex, Expression.Constant(index)),
                                    Expression.Constant(pairs[middle + 1].C[index])),
                BuildStringChar(p, pairs, index, lower, middle),
                BuildStringChar(p, pairs, index, middle + 1, upper)
                );
        }

        private static int MidPoint(int lower, int upper)
        {
            return ((upper - lower + 1)/2) + lower;
        }

        #region Nested type: Case

        private struct Case<TR>
        {
            public string C { get; set; }
            public TR R { get; set; }

            public override string ToString()
            {
                return C + " " + R;
            }
        }

        #endregion
    }
}