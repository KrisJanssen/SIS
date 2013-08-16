using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DevDefined.Common.WeakRef;

namespace DevDefined.Common.Extensions.Annotations
{
    public static class AnnotationStore
    {
        private static readonly WeakDictionary<object, ClassAnnotation> _annotations
            = new WeakDictionary<object, ClassAnnotation>();

        public static IEnumerable<ClassAnnotation> Classes
        {
            get
            {
                _annotations.RemoveCollectedEntries();
                return _annotations.Values;
            }
        }

        public static IEnumerable<MemberAnnotation> Members
        {
            get
            {
                foreach (ClassAnnotation classAnnotation in Classes)
                {
                    foreach (MemberAnnotation memberAnnotation in classAnnotation.Properties)
                    {
                        yield return memberAnnotation;
                    }
                }
            }
        }

        public static T Annotation<T>(this object target, object key)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return (T) annotation[key];
        }

        public static T Annotation<T>(this object target, Expression<Funclet> expression, object key)
        {
            ClassAnnotation classAnnotation = GetOrCreateAnnotation(target);
            MemberAnnotation memberAnnotation = classAnnotation.ForMember(expression);
            return (T) memberAnnotation[key];
        }

        public static void ClearAnnotation(this object target, object key)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Remove(key);
        }

        public static void ClearAnnotations(this object target)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Clear();
        }

        public static ClassAnnotation Annotation(this object target)
        {
            return GetOrCreateAnnotation(target);
        }

        public static void Annotate<T>(this object target, object key, T value)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation[key] = value;
        }

        public static MemberAnnotation Annotate<T>(this object target, Expression<Funclet> expression, params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return annotation.Annotate(expression, args);
        }

        public static MemberAnnotation Annotate<T>(this object target, Expression<Proc> expression, params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return annotation.Annotate(expression, args);
        }

        public static ClassAnnotation Annotate<T>(this object target, params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Annotate(args);
            return annotation;
        }

        public static bool HasAnnotation(this object target, string name)
        {
            ClassAnnotation annotation = GetAnnotation(target);
            if (annotation == null) return false;
            return annotation.HasKey(name);
        }

        private static ClassAnnotation GetOrCreateAnnotation(object key)
        {
            _annotations.RemoveCollectedEntries();

            ClassAnnotation annotation;

            if (!_annotations.TryGetValue(key, out annotation))
            {
                lock (_annotations)
                {
                    if (!_annotations.TryGetValue(key, out annotation))
                    {
                        annotation = new ClassAnnotation(key);
                        _annotations.Add(key, annotation);
                    }
                }
            }

            return annotation;
        }

        private static ClassAnnotation GetAnnotation(object key)
        {
            _annotations.RemoveCollectedEntries();

            ClassAnnotation annotation;

            if (_annotations.TryGetValue(key, out annotation))
            {
                return annotation;
            }

            return null;
        }
    }
}