using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace Marsman.Ember.Visitors
{
    public class MethodVisitor : ExpressionVisitor
    {
        private MethodInfo result;

        private MethodVisitor() { }

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

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            result = (MethodInfo)(node.Object as ConstantExpression).Value;
            throw new VisitStoppedException();
        }
    }

    public class VisitStoppedException : Exception { }
}