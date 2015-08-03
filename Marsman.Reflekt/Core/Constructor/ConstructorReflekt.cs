using Marsman.Reflekt.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public class ConstructorReflekt<T> : MemberReflekt<T, ConstructorInfo>
    {
        internal ConstructorReflekt(Expression ex) : base(ex) { }

        public override ConstructorInfo Value
        {
            get
            {
                return ConstructorVisitor.GetConstructorInfo(Selector);
            }
        }
    }
}