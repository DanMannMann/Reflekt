using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt.Visitors
{
    public class ConstructorBuilderVisitor : ExpressionVisitor
    {
        private Type[] _newGenericArgs;
        private ConstructorInfo result;

        public ConstructorBuilderVisitor(Type[] types)
        {
            _newGenericArgs = types;
        }

        public static ConstructorInfo GetConstructorInfo(Expression ex, Type[] types)
        {
            var vis = new ConstructorBuilderVisitor(types);
            try
            {
                vis.Visit(ex);
            }
            catch (VisitStoppedException) { }
            return vis.result;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var originalConstructorParameters = node.Arguments.Select(x => x.Type).ToList();
            var genericType = node.Constructor.DeclaringType.GetGenericTypeDefinition();
			var constructorIndex = node.Constructor.DeclaringType.GetTypeInfo().DeclaredConstructors.ToList().IndexOf(node.Constructor);

			var genericTypeParameters = genericType.GetTypeInfo().GenericTypeParameters.ToList();
            var originalGenericArgs = node.Constructor.DeclaringType.GetTypeInfo().GenericTypeArguments.ToList();

            if (genericTypeParameters.Count != _newGenericArgs.Length)
            {
                throw new InvalidOperationException("Wrong number of generic argument values specified. This type requires " + genericTypeParameters.Count + " generic type arguments");
            }

            result = genericType.MakeGenericType(_newGenericArgs.ToArray()).GetTypeInfo().DeclaredConstructors.ToList()[constructorIndex];
            throw new VisitStoppedException();
        }
    }
}