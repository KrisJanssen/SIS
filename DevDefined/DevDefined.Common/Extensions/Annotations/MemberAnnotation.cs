// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberAnnotation.cs" company="">
//   
// </copyright>
// <summary>
//   The member annotation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Extensions.Annotations
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The member annotation.
    /// </summary>
    public class MemberAnnotation : IAnnotation
    {
        #region Fields

        /// <summary>
        /// The _class annotation.
        /// </summary>
        private readonly ClassAnnotation _classAnnotation;

        /// <summary>
        /// The _member.
        /// </summary>
        private readonly MemberInfo _member;

        /// <summary>
        /// The _property annotations.
        /// </summary>
        private readonly Dictionary<object, object> _propertyAnnotations = new Dictionary<object, object>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberAnnotation"/> class.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <param name="classAnnotation">
        /// The class annotation.
        /// </param>
        public MemberAnnotation(MemberInfo member, ClassAnnotation classAnnotation)
        {
            this._member = member;
            this._classAnnotation = classAnnotation;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the class annotation.
        /// </summary>
        public ClassAnnotation ClassAnnotation
        {
            get
            {
                return this._classAnnotation;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this._propertyAnnotations.Count;
            }
        }

        /// <summary>
        /// Gets the member.
        /// </summary>
        public MemberInfo Member
        {
            get
            {
                return this._member;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object this[object key]
        {
            get
            {
                return this._propertyAnnotations[key];
            }

            set
            {
                this._propertyAnnotations[key] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void Annotate<T>(params Func<string, T>[] args)
        {
            foreach (var func in args)
            {
                this._propertyAnnotations[func.Method.GetParameters()[0].Name] = func(null);
            }
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            this._propertyAnnotations.Clear();
        }

        /// <summary>
        /// The has key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HasKey(object key)
        {
            return this._propertyAnnotations.ContainsKey(key);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void Remove(object key)
        {
            this._propertyAnnotations.Remove(key);
        }

        #endregion
    }
}