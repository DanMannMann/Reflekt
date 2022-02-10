using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Marsman.Reflekt.Visitors
{


    public class ConstructorVisitor : ReflektVisitor<ConstructorInfo>
	{
        private ConstructorVisitor() { }

        public static ConstructorInfo GetConstructorInfo(Expression ex)
        {
            var vis = new ConstructorVisitor();
			vis.Visit(ex);
			return vis.Result;
        }

		public static ConstructorInfo GetConstructorInfo(Expression ex, Type[] types)
		{
			return ReplaceTypeParameters(GetConstructorInfo(ex), types);
		}

		protected override Expression VisitNew(NewExpression node)
        {
			Result = node.Constructor;
			return node;
        }

		private static ConstructorInfo ReplaceTypeParameters(ConstructorInfo ctor, Type[] types)
		{
			var genericType = ctor.DeclaringType.GetGenericTypeDefinition();
			var constructorIndex = ctor.DeclaringType.GetTypeInfo().DeclaredConstructors.ToList().IndexOf(ctor);

			var genericTypeParameters = genericType.GetTypeInfo().GenericTypeParameters.ToList();
			var originalGenericArgs = ctor.DeclaringType.GetTypeInfo().GenericTypeArguments.ToList();

			if (genericTypeParameters.Count != types.Length)
			{
				throw new InvalidOperationException("Wrong number of generic argument values specified. This type requires " + genericTypeParameters.Count + " generic type arguments");
			}

			return genericType.MakeGenericType(types.ToArray()).GetTypeInfo().DeclaredConstructors.ToList()[constructorIndex];
		}
	}

}