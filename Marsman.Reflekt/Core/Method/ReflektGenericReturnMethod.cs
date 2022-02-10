using System;
using System.Linq.Expressions;

namespace Marsman.Reflekt
{
    public class ReflektGenericReturnMethod<T, Tout> : ReflektReturnMethod<T, Tout>
    {
        private readonly Type[] types;

        internal ReflektGenericReturnMethod(Func<Expression, MethodReflektor<T>> expressionVisitor, Type[] types) : base(expressionVisitor)
        {
            this.types = (types?.Length ?? 0) < 1 ? null : types;
        }

        public ReflektReturnDelegate<T, Tout> AsDelegate()
        {
            return new ReflektReturnDelegate<T, Tout>(_expressionVisitor, types);
        }
    }
}