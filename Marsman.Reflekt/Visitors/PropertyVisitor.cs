using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.Reflekt.Visitors
{
    public class PropertyVisitor : ExpressionVisitor
    {
        private PropertyInfo result;

        private PropertyVisitor() { }

        public static PropertyInfo GetPropertyInfo(Expression ex)
        {
            var vis = new PropertyVisitor();
            try
            {
                vis.Visit(ex);
            }
            catch (VisitStoppedException) { }
            return vis.result;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            result = node.Member as PropertyInfo;
            throw new VisitStoppedException();
        }
    }

}
