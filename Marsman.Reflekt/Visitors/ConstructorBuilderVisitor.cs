using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember.Visitors
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
            var genericTypeParameters = genericType.GetGenericArguments().ToList();
            var originalGenericArgs = node.Constructor.DeclaringType.GetGenericArguments().ToList();

            if (genericTypeParameters.Count != _newGenericArgs.Length)
            {
                throw new InvalidOperationException("Wrong number of generic argument values specified. This type requires " + genericTypeParameters.Count + " generic type arguments");
            }
            for (int i = 0; i < _newGenericArgs.Length; i++)
            {
                var constraints = genericTypeParameters[i].GetGenericParameterConstraints().ToList();
                foreach (var constraint in constraints)
                {
                    if (!constraint.IsAssignableFrom(_newGenericArgs[i]))
                    {
                        throw new InvalidOperationException("Generic type parameter " + i + " is constrained to type " + constraint + ". The supplied type " + _newGenericArgs[i] + " does not meet this constraint");
                    }
                }
            }

            result = genericType.MakeGenericType(_newGenericArgs.ToArray()).GetConstructors().Where(x => x.MetadataToken == node.Constructor.MetadataToken).Single();
            throw new VisitStoppedException();
        }
    }
}