using Marsman.Reflekt.Visitors;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
	internal class MethodReflektor<T> : MemberReflektor<T, MethodInfo>
    {
        internal MethodReflektor(Expression ex) : base(ex) { }

        public override MethodInfo Value
        {
            get { return MethodVisitor.GetMethodInfo(Selector); }
        }
    }

}