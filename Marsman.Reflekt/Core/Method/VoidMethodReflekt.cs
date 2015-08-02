using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
{
    public class VoidMethodReflekt<T>
    {
        private Func<Expression, MethodReflekt<T>> _expressionVisitor;

        internal VoidMethodReflekt(Func<Expression, MethodReflekt<T>> expressionVisitor) { _expressionVisitor = expressionVisitor; }

        public VoidMethodReflekt<T> WithTypeArguments(params Type[] types)
        {
            return new VoidMethodReflekt<T>(x => new GenericMethodReflekt<T>(x, types));
        }

        public VoidMethodReflekt<T> GenericDefinition()
        {
            return new VoidMethodReflekt<T>(x => new GenericMethodReflekt<T>(x, new Type[] { }));
        }

        public MethodInfo Parameterless(Expression<Func<T, Action>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1>(Expression<Func<T, Action<T1>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2>(Expression<Func<T, Action<T1, T2>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3>(Expression<Func<T, Action<T2, T2, T3>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4>(Expression<Func<T, Action<T2, T2, T3, T4>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5>(Expression<Func<T, Action<T2, T2, T3, T4, T5>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<T, Action<T2, T2, T3, T4, T5, T6>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, Action<T2, T2, T3, T4, T5, T6, T7>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, Action<T2, T2, T3, T4, T5, T6, T7, T8>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }
    }
}