using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt.Visitors
{
    public class MethodBuilderVisitor : ExpressionVisitor
    {
        private MethodInfo result;
        private Type[] _newGenericArgs;

        private MethodBuilderVisitor(Type[] newGenericArgs) { _newGenericArgs = newGenericArgs; }

        public static MethodInfo GetMethodInfo(Expression ex, Type[] types)
        {
            var vis = new MethodBuilderVisitor(types);
            try
            {
                vis.Visit(ex);
            }
            catch (VisitStoppedException) { }
            return vis.result;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var concrete = (MethodInfo)(node.Object as ConstantExpression).Value;
            var genericDef = concrete.GetGenericMethodDefinition();
            var genericTypeParameters = genericDef.GetGenericArguments().ToList();

            if (_newGenericArgs.Length == 0)
            {
                result = genericDef;
                throw new VisitStoppedException();
            }
            else if (genericTypeParameters.Count != _newGenericArgs.Length)
            {
                throw new InvalidOperationException("Wrong number of generic argument values specified. This method requires " + genericTypeParameters.Count + " generic type arguments");
            }
            else
            {
                result = genericDef.MakeGenericMethod(_newGenericArgs);
                throw new VisitStoppedException();
            }
        }
    }
}