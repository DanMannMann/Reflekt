using Marsman.Ember.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
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