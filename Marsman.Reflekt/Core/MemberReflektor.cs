using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
	internal abstract class MemberReflektor<T, Tmember> where Tmember : MemberInfo
    {
        protected Expression Selector { get; private set; }

        protected MemberReflektor(Expression selector)
        {
            Selector = selector;
        }

        public abstract Tmember Value { get; }
    }
}