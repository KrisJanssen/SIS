// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassAnnotation.cs" company="">
//   
// </copyright>
// <summary>
//   The class annotation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DevDefined.Common.Extensions.Annotations
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// The class annotation.
    /// </summary>
    public class ClassAnnotation : IAnnotation
    {
        #region Fields

        /// <summary>
        /// The _class annotations.
        /// </summary>
        private readonly Dictionary<object, object> _classAnnotations = new Dictionary<object, object>();

        /// <summary>
        /// The _member annotations.
        /// </summary>
        private readonly Dictionary<MemberInfo, MemberAnnotation> _memberAnnotations =
            new Dictionary<MemberInfo, MemberAnnotation>();

        /// <summary>
        /// The _weak ref target.
        /// </summary>
        private readonly WeakReference _weakRefTarget;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassAnnotation"/> class.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        public ClassAnnotation(object target)
        {
            this._weakRefTarget = new WeakReference(target);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this._classAnnotations.Count;
            }
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        public IEnumerable<MemberAnnotation> Properties
        {
            get
            {
                return this._memberAnnotations.Values;
            }
        }

        /// <summary>
        /// Gets the target.
        /// </summary>
        public object Target
        {
            get
            {
                return this._weakRefTarget.Target;
            }
        }

        /// <summary>
        /// Gets the target type.
        /// </summary>
        public Type TargetType
        {
            get
            {
                return this.Target.GetType();
            }
        }

        /// <summary>
        /// Gets the target type name.
        /// </summary>
        public string TargetTypeName
        {
            get
            {
                return this.TargetType.Name;
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
                return this._classAnnotations[key];
            }

            set
            {
                this._classAnnotations[key] = value;
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
                this._classAnnotations[func.Method.GetParameters()[0].Name] = func(null);
            }
        }

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="memberExpression">
        /// The member expression.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberAnnotation"/>.
        /// </returns>
        public MemberAnnotation Annotate<T>(Expression<Funclet> memberExpression, params Func<string, T>[] args)
        {
            MemberAnnotation memberAnnotation = this.ForMember(memberExpression);
            memberAnnotation.Annotate(args);
            return memberAnnotation;
        }

        /// <summary>
        /// The annotate.
        /// </summary>
        /// <param name="memberExpression">
        /// The member expression.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MemberAnnotation"/>.
        /// </returns>
        public MemberAnnotation Annotate<T>(Expression<Proc> memberExpression, params Func<string, T>[] args)
        {
            MemberAnnotation memberAnnotation = this.ForMember(memberExpression);
            memberAnnotation.Annotate(args);
            return memberAnnotation;
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            this._classAnnotations.Clear();
            this._memberAnnotations.Clear();
        }

        /// <summary>
        /// The for member.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="MemberAnnotation"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        public MemberAnnotation ForMember(LambdaExpression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("expression");
            }

            MemberInfo info = this.GetMemberKey(expression);

            MemberAnnotation memberAnnotation;

            if (!this._memberAnnotations.TryGetValue(info, out memberAnnotation))
            {
                lock (this._memberAnnotations)
                {
                    if (!this._memberAnnotations.TryGetValue(info, out memberAnnotation))
                    {
                        memberAnnotation = new MemberAnnotation(info, this);

                        this._memberAnnotations[info] = memberAnnotation;
                    }
                }
            }

            return memberAnnotation;
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
            return this._classAnnotations.ContainsKey(key);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void Remove(object key)
        {
            this._classAnnotations.Remove(key);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get member key.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="MemberInfo"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private MemberInfo GetMemberKey(LambdaExpression expression)
        {
            MemberInfo info;

            var member = expression.Body as MemberExpression;
            var methodCall = expression.Body as MethodCallExpression;

            if (member != null)
            {
                info = member.Member;
            }
            else if (methodCall != null)
            {
                info = methodCall.Method;
            }
            else
            {
                throw new ArgumentException(string.Format("could extract MemberInfo from expression: {0}", expression));
            }

            if (this.Target.GetType() != info.DeclaringType)
            {
                throw new ArgumentException(
                    string.Format(
                        "The selected member does not belong to the declaring type \"{0}\"", 
                        info.DeclaringType));
            }

            return info;
        }

        #endregion
    }
}