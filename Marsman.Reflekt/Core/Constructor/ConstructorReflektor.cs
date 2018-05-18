using Marsman.Reflekt.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    internal class ConstructorReflektor<T> : MemberReflektor<T, ConstructorInfo>
    {
        internal ConstructorReflektor(Expression ex) : base(ex) { }

        public override ConstructorInfo Value
        {
            get
            {
                return ConstructorVisitor.GetConstructorInfo(Selector);
            }
        }
    }
}