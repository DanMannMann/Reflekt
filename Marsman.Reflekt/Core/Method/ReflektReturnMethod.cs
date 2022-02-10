using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ReflektReturnMethod<T, Tout>
    {
        internal Func<Expression, MethodReflektor<T>> _expressionVisitor;

        internal ReflektReturnMethod(Func<Expression, MethodReflektor<T>> expressionVisitor) { _expressionVisitor = expressionVisitor; }

        public ReflektGenericReturnMethod<T, Tout> WithTypeArguments(params Type[] types)
        {
            return new ReflektGenericReturnMethod<T, Tout>(x => new GenericMethodReflektor<T>(x, types), types);
        }

        public ReflektGenericReturnMethod<T, Tout> GenericDefinition()
        {
            return new ReflektGenericReturnMethod<T, Tout>(x => new GenericMethodReflektor<T>(x, new Type[] { }), null);
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