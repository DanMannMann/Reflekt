using System;
using System.Linq.Expressions;

namespace Marsman.Reflekt
{
    public class ReflektGenericVoidMethod<T> : ReflektVoidMethod<T>
    {
        private readonly Type[] types;

        internal ReflektGenericVoidMethod(Func<Expression, MethodReflektor<T>> expressionVisitor, Type[] types) : base(expressionVisitor)
        {
            this.types = (types?.Length ?? 0) < 1 ? null : types;
        }

        public ReflektVoidDelegate<T> AsDelegate()
        {
            return new ReflektVoidDelegate<T>(_expressionVisitor, types);
        }
    }
}