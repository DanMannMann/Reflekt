using Marsman.Reflekt.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public partial class Reflekt<T>
    {
        public static PropertyInfo Property(Expression<Func<T, object>> selector)
        {
            return PropertyVisitor.GetPropertyInfo(selector);
        }

        public static string PropertyName(Expression<Func<T, object>> selector)
        {
            return PropertyVisitor.GetPropertyInfo(selector).Name;
        }

        public PropertyInfo property(Expression<Func<T, object>> selector)
        {
            return PropertyVisitor.GetPropertyInfo(selector);
        }

        public string propertyName(Expression<Func<T, object>> selector)
        {
            return PropertyVisitor.GetPropertyInfo(selector).Name;
        }
    }
}