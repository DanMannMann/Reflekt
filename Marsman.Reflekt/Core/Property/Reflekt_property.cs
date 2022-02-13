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

        public static Action<T, Tvalue> PropertySetterDelegate<Tvalue>(Expression<Func<T, Tvalue>> selector)
        {
            return Delegate.CreateDelegate(typeof(Action<T, Tvalue>), PropertyVisitor.GetPropertyInfo(selector).GetSetMethod()) as Action<T, Tvalue>;
        }

        public static Func<T, Tvalue> PropertyGetterDelegate<Tvalue>(Expression<Func<T, Tvalue>> selector)
        {
            return Delegate.CreateDelegate(typeof(Func<T, Tvalue>), PropertyVisitor.GetPropertyInfo(selector).GetGetMethod()) as Func<T, Tvalue>;
        }

        public static string PropertyName(Expression<Func<T, object>> selector)
        {
            return PropertyVisitor.GetPropertyInfo(selector).Name;
        }

        public PropertyInfo property(Expression<Func<T, object>> selector)
        {
			return Property(selector);
		}

        public string propertyName(Expression<Func<T, object>> selector)
        {
			return PropertyName(selector);
		}
    }
}