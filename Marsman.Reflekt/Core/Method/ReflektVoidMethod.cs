using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ReflektVoidMethod<T> : ReflektVoidMethodBase<T>
    {
        internal ReflektVoidMethod(Func<Expression, MethodReflektor<T>> expressionVisitor) { _expressionVisitor = expressionVisitor; }

        public ReflektVoidDelegate<T> AsDelegate(params Type[] typeArguments)
        {
            return new ReflektVoidDelegate<T>(_expressionVisitor, typeArguments);
        }

        public ReflektVoidDelegateFactorySource<T> AsDelegateFactory()
        {
            return new ReflektVoidDelegateFactorySource<T>(_expressionVisitor);
        }

        public ReflektVoidMethodBase<T> WithTypeArguments(params Type[] types)
        {
            return new ReflektVoidMethod<T>(x => new GenericMethodReflektor<T>(x, types));
        }

        public ReflektVoidMethodBase<T> AsGenericDefinition()
        {
            return new ReflektVoidMethod<T>(x => new GenericMethodReflektor<T>(x, Type.EmptyTypes));
        }

        [Obsolete("use AsGenericDefinition()")]
        public ReflektVoidMethod<T> GenericDefinition() => AsGenericDefinition() as ReflektVoidMethod<T>;
    }

    public abstract class ReflektVoidMethodBase<T>
    {
        internal Func<Expression, MethodReflektor<T>> _expressionVisitor;

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