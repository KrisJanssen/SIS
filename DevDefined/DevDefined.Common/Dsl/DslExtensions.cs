using System;
using System.Collections.Generic;
using DevDefined.Common.Extensions.Annotations;

namespace DevDefined.Common.Dsl
{
    public static class DslExtensions
    {
        public static Batch[] ForEach<T>(this IEnumerable<T> source, Func<T, Batch> predicate)
        {
            var batches = new List<Batch>();

            foreach (T item in source)
            {
                batches.Add(predicate(item));
            }

            return batches.ToArray();
        }

        public static void Ignore(this Batch batch)
        {
            batch.Annotate(Ignore => true);
        }

        public static bool IsIgnored(this Batch batch)
        {
            return batch.HasAnnotation("Ignore");
        }
    }
}