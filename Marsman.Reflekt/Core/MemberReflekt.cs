using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public abstract class MemberReflekt<T, Tmember> where Tmember : MemberInfo
    {
        protected Expression Selector { get; private set; }

        protected MemberReflekt(Expression selector)
        {
            Selector = selector;
        }

        public abstract Tmember Value { get; }
    }
}