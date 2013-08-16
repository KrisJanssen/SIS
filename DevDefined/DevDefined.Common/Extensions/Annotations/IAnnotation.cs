using System;

namespace DevDefined.Common.Extensions.Annotations
{
    public interface IAnnotation
    {
        object this[object key] { get; set; }
        int Count { get; }
        void Clear();
        void Remove(object key);
        void Annotate<T>(params Func<string, T>[] args);
    }
}