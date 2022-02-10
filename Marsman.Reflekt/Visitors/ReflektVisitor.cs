using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt.Visitors
{
    public class ReflektVisitor<T> : ExpressionVisitor where T : MemberInfo
	{
		public T Result { get; protected set; }
		public override Expression Visit(Expression node)
		{
			if (Result != null) return node;
			return base.Visit(node);
		}
	}

}