// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnnotationStore.cs" company="">
//   
// </copyright>
// <summary>
//   The annotation store.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace DevDefined.Common.Extensions.Annotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using DevDefined.Common.WeakRef;

    /// <summary>
    /// The annotation store.
    /// </summary>
    public static class AnnotationStore
    {
        #region Static Fields

        /// <summary>
        /// The _annotations.
        /// </summary>
        private static readonly WeakDictionary<object, ClassAnnotation> _annotations =
            new WeakDictionary<object, ClassAnnotation>();

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the classes.
        /// </summary>
        public static IEnumerable<ClassAnnotation> Classes
        {
            get
            {
                _annotations.RemoveCollectedEntries();
                return _annotations.Values;
            }
        }

        /// <summary>
        /// Gets the members.
        /// </summary>
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Annotate<T>(this object target, object key, T value)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation[key] = value;
        }

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberAnnotation"/>.
        /// </returns>
        public static MemberAnnotation Annotate<T>(
            this object target, 
            Expression<Funclet> expression, 
            params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return annotation.Annotate(expression, args);
        }

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberAnnotation"/>.
        /// </returns>
        public static MemberAnnotation Annotate<T>(
            this object target, 
            Expression<Proc> expression, 
            params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return annotation.Annotate(expression, args);
        }

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="ClassAnnotation"/>.
        /// </returns>
        public static ClassAnnotation Annotate<T>(this object target, params Func<string, T>[] args)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Annotate(args);
            return annotation;
        }

        /// <summary>
        /// The annotation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Annotation<T>(this object target, object key)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            return (T)annotation[key];
        }

        /// <summary>
        /// The annotation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Annotation<T>(this object target, Expression<Funclet> expression, object key)
        {
            ClassAnnotation classAnnotation = GetOrCreateAnnotation(target);
            MemberAnnotation memberAnnotation = classAnnotation.ForMember(expression);
            return (T)memberAnnotation[key];
        }

        /// <summary>
        /// The annotation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="ClassAnnotation"/>.
        /// </returns>
        public static ClassAnnotation Annotation(this object target)
        {
            return GetOrCreateAnnotation(target);
        }

        /// <summary>
        /// The clear annotation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        public static void ClearAnnotation(this object target, object key)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Remove(key);
        }

        /// <summary>
        /// The clear annotations.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        public static void ClearAnnotations(this object target)
        {
            ClassAnnotation annotation = GetOrCreateAnnotation(target);
            annotation.Clear();
        }

        /// <summary>
        /// The has annotation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool HasAnnotation(this object target, string name)
        {
            ClassAnnotation annotation = GetAnnotation(target);
            if (annotation == null)
            {
                return false;
            }

            return annotation.HasKey(name);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get annotation.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ClassAnnotation"/>.
        /// </returns>
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

        /// <summary>
        /// The get or create annotation.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="ClassAnnotation"/>.
        /// </returns>
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

        #endregion
    }
}