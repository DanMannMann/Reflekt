using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ReturnMethodReflekt<T, Tout>
    {
        private Func<Expression, MethodReflekt<T>> _expressionVisitor;

        internal ReturnMethodReflekt(Func<Expression, MethodReflekt<T>> expressionVisitor) { _expressionVisitor = expressionVisitor; }

        public ReturnMethodReflekt<T, Tout> WithTypeArguments(params Type[] types)
        {
            return new ReturnMethodReflekt<T, Tout>(x => new GenericMethodReflekt<T>(x, types));
        }

        public ReturnMethodReflekt<T, Tout> GenericDefinition()
        {
            return new ReturnMethodReflekt<T, Tout>(x => new GenericMethodReflekt<T>(x, new Type[] { }));
        }

        public MethodInfo Parameterless(Expression<Func<T, Func<Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1>(Expression<Func<T, Func<T1, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2>(Expression<Func<T, Func<T1, T2, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3>(Expression<Func<T, Func<T1, T2, T3, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4>(Expression<Func<T, Func<T1, T2, T3, T4, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5>(Expression<Func<T, Func<T1, T2, T3, T4, T5, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public MethodInfo Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, Tout>>> selector)
        {
            return _expressionVisitor(selector).Value;
        }
    }
}