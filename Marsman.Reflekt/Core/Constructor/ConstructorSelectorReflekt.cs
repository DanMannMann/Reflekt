using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ConstructorSelectorReflekt<T>
    {
        Func<Expression, ConstructorReflekt<T>> _expressionVisitor;

        public ConstructorSelectorReflekt<T> WithTypeArguments(params Type[] types)
        {
            return new ConstructorSelectorReflekt<T>(x => new GenericConstructorReflekt<T>(x, types));
        }

        public ConstructorSelectorReflekt(Func<Expression, ConstructorReflekt<T>> expressionVisitor)
        {
            _expressionVisitor = expressionVisitor;
        }

        public ConstructorInfo Parameterless(Expression<Func<T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1>(Expression<Func<T1, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2>(Expression<Func<T1, T2, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3>(Expression<Func<T1, T2, T3, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }

        public ConstructorInfo Parameters<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T>> selector)
        {
            return _expressionVisitor(selector).Value;
        }
    }
}