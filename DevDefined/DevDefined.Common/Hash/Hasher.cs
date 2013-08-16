using System;
using System.Collections.Generic;

namespace DevDefined.Common.Hash
{
    public static class Hasher
    {
        public static Dictionary<string, T> Hash<T>(params Func<string, T>[] args)
        {
            var dict = new Dictionary<string, T>();
            AddToHash(dict, args);
            return dict;
        }

        public static void AddToHash<T>(Dictionary<string, T> dict, params Func<string, T>[] args)
        {
            foreach (var func in args)
            {
                dict[func.Method.GetParameters()[0].Name] = func(null);
            }
        }
    }
}