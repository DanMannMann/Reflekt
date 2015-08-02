using Marsman.Ember.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
{
    public class MethodReflekt<T> : MemberReflekt<T, MethodInfo>
    {
        internal MethodReflekt(Expression ex) : base(ex) { }

        public override MethodInfo Value
        {
            get { return MethodVisitor.GetMethodInfo(Selector); }
        }
    }

}