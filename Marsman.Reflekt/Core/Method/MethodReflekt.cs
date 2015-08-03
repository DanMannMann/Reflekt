using Marsman.Reflekt.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
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