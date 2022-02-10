using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ReflektReturnMethod<T, Tout> : ReflektReturnMethodBase<T, Tout>
    {
        internal ReflektReturnMethod(Func<Expression, MethodReflektor<T>> expressionVisitor) { _expressionVisitor = expressionVisitor; }

        public ReflektReturnDelegate<T, Tout> AsDelegate(params Type[] typeArguments)
        {
            return new ReflektReturnDelegate<T, Tout>(_expressionVisitor, typeArguments);
        }

        public ReflektReturnDelegateFactorySource<T, Tout> AsDelegateFactory()
        {
            return new ReflektReturnDelegateFactorySource<T, Tout>(_expressionVisitor);
        }

        public ReflektReturnMethodBase<T,Tout> WithTypeArguments(params Type[] types)
        {
            return new ReflektReturnMethod<T, Tout>(x => new GenericMethodReflektor<T>(x, types));
        }

        public ReflektReturnMethodBase<T, Tout> AsGenericDefinition()
        {
            return new ReflektReturnMethod<T, Tout>(x => new GenericMethodReflektor<T>(x, Type.EmptyTypes));
        }

        [Obsolete("use AsGenericDefinition()")]
        public ReflektReturnMethodBase<T, Tout> GenericDefinition() => AsGenericDefinition();
    }

    public abstract class ReflektReturnMethodBase<T,Tout>
    {
        internal Func<Expression, MethodReflektor<T>> _expressionVisitor;

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