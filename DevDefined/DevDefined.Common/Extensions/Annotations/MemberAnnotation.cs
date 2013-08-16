using System;
using System.Collections.Generic;
using System.Reflection;

namespace DevDefined.Common.Extensions.Annotations
{
    public class MemberAnnotation : IAnnotation
    {
        private readonly ClassAnnotation _classAnnotation;
        private readonly MemberInfo _member;
        private readonly Dictionary<object, object> _propertyAnnotations = new Dictionary<object, object>();

        public MemberAnnotation(MemberInfo member, ClassAnnotation classAnnotation)
        {
            _member = member;
            _classAnnotation = classAnnotation;
        }

        public MemberInfo Member
        {
            get { return _member; }
        }

        public ClassAnnotation ClassAnnotation
        {
            get { return _classAnnotation; }
        }

        #region IAnnotation Members

        public object this[object key]
        {
            get { return _propertyAnnotations[key]; }
            set { _propertyAnnotations[key] = value; }
        }

        public void Clear()
        {
            _propertyAnnotations.Clear();
        }

        public void Remove(object key)
        {
            _propertyAnnotations.Remove(key);
        }

        public int Count
        {
            get { return _propertyAnnotations.Count; }
        }

        public void Annotate<T>(params Func<string, T>[] args)
        {
            foreach (var func in args)
            {
                _propertyAnnotations[func.Method.GetParameters()[0].Name] = func(null);
            }
        }

        #endregion

        public bool HasKey(object key)
        {
            return _propertyAnnotations.ContainsKey(key);
        }
    }
}