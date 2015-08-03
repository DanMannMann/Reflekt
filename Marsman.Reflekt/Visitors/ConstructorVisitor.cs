using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Marsman.Reflekt.Visitors
{
    public class ConstructorVisitor : ExpressionVisitor
    {
        private ConstructorInfo result;

        private ConstructorVisitor() { }

        public static ConstructorInfo GetConstructorInfo(Expression ex)
        {
            var vis = new ConstructorVisitor();
            try
            {
                vis.Visit(ex);
            }
            catch (VisitStoppedException) { }
            return vis.result;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            result = node.Constructor;
            throw new VisitStoppedException();
        }
    }

}