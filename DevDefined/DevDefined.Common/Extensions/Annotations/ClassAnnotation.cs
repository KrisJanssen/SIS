using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace DevDefined.Common.Extensions.Annotations
{
    public class ClassAnnotation : IAnnotation
    {
        private readonly Dictionary<object, object> _classAnnotations = new Dictionary<object, object>();
        private readonly Dictionary<MemberInfo, MemberAnnotation> _memberAnnotations = new Dictionary<MemberInfo, MemberAnnotation>();
        private readonly WeakReference _weakRefTarget;

        public ClassAnnotation(object target)
        {
            _weakRefTarget = new WeakReference(target);
        }

        public string TargetTypeName
        {
            get { return TargetType.Name; }
        }

        public Type TargetType
        {
            get { return Target.GetType(); }
        }

        public object Target
        {
            get { return _weakRefTarget.Target; }
        }

        public IEnumerable<MemberAnnotation> Properties
        {
            get { return _memberAnnotations.Values; }
        }

        #region IAnnotation Members

        public object this[object key]
        {
            get { return _classAnnotations[key]; }
            set { _classAnnotations[key] = value; }
        }

        public void Clear()
        {
            _classAnnotations.Clear();
            _memberAnnotations.Clear();
        }

        public void Remove(object key)
        {
            _classAnnotations.Remove(key);
        }

        public int Count
        {
            get { return _classAnnotations.Count; }
        }

        public void Annotate<T>(params Func<string, T>[] args)
        {
            foreach (var func in args)
            {
                _classAnnotations[func.Method.GetParameters()[0].Name] = func(null);
            }
        }

        #endregion

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

            if (Target.GetType() != info.DeclaringType)
            {
                throw new ArgumentException(string.Format("The selected member does not belong to the declaring type \"{0}\"", info.DeclaringType));
            }

            return info;
        }

        public MemberAnnotation ForMember(LambdaExpression expression)
        {
            if (expression == null) throw new ArgumentException("expression");

            MemberInfo info = GetMemberKey(expression);

            MemberAnnotation memberAnnotation;

            if (!_memberAnnotations.TryGetValue(info, out memberAnnotation))
            {
                lock (_memberAnnotations)
                {
                    if (!_memberAnnotations.TryGetValue(info, out memberAnnotation))
                    {
                        memberAnnotation = new MemberAnnotation(info, this);

                        _memberAnnotations[info] = memberAnnotation;
                    }
                }
            }

            return memberAnnotation;
        }

        public MemberAnnotation Annotate<T>(Expression<Funclet> memberExpression, params Func<string, T>[] args)
        {
            MemberAnnotation memberAnnotation = ForMember(memberExpression);
            memberAnnotation.Annotate(args);
            return memberAnnotation;
        }

        public MemberAnnotation Annotate<T>(Expression<Proc> memberExpression, params Func<string, T>[] args)
        {
            MemberAnnotation memberAnnotation = ForMember(memberExpression);
            memberAnnotation.Annotate(args);
            return memberAnnotation;
        }

        public bool HasKey(object key)
        {
            return _classAnnotations.ContainsKey(key);
        }
    }
}