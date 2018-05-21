using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Marsman.Reflekt.Visitors
{
    public class MethodVisitor : ExpressionVisitor
    {
        private MethodInfo result;

        protected MethodVisitor() { }

        public static MethodInfo GetMethodInfo(Expression ex)
        {
            var vis = new MethodVisitor();
            try
            {
                vis.Visit(ex);
            }
            catch (VisitStoppedException) { }
            return vis.result;
        }

		public static MethodInfo GetMethodInfo(Expression ex, Type[] types)
		{
			return ReplaceTypeParameters(GetMethodInfo(ex), types);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            result = (MethodInfo)(node.Object as ConstantExpression).Value;
            throw new VisitStoppedException();
		}

		private static MethodInfo ReplaceTypeParameters(MethodInfo concrete, Type[] types)
		{
			var genericDef = concrete.GetGenericMethodDefinition();
			var genericTypeParameters = genericDef.GetGenericArguments().ToList();

			if (types.Length == 0)
			{
				return genericDef;
			}
			else if (genericTypeParameters.Count != types.Length)
			{
				throw new InvalidOperationException("Wrong number of generic argument values specified. This method requires " + genericTypeParameters.Count + " generic type arguments");
			}
			else
			{
				return genericDef.MakeGenericMethod(types);
			}
		}
	}

    public class VisitStoppedException : Exception { }
}