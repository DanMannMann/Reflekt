using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.Reflekt.Visitors
{
    internal class PropertyVisitor : ReflektVisitor<PropertyInfo>
    {
        private PropertyVisitor() { }

        public static PropertyInfo GetPropertyInfo(Expression ex)
        {
            var vis = new PropertyVisitor();
            vis.Visit(ex);
            return vis.Result;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Result = node.Member as PropertyInfo;
            return node;
        }
    }

}
