using Marsman.Reflekt.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.Reflekt
{
    public partial class Reflekt<T>
    {
		public static ReflektVoidMethod<T> Method()
        {
            return new ReflektVoidMethod<T>(x => new MethodReflektor<T>(x));
        }

        public static ReflektReturnMethod<T, Treturn> Method<Treturn>()
        {
            return new ReflektReturnMethod<T, Treturn>(x => new MethodReflektor<T>(x));
        }

        public ReflektVoidMethod<T> method()
        {
			return Method();

		}

        public ReflektReturnMethod<T, Treturn> method<Treturn>()
        {
			return Method<Treturn>();
        }

		public static MethodInfo MethodInfo(Expression<Func<T, Action>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, T4, T5, T6, T7, T8, Treturn>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, T8, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, T4, T5, T6, T7, Treturn>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, T7, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, T4, T5, T6, Treturn>(Expression<Func<T, Func<T1, T2, T3, T4, T5, T6, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, T4, T5, Treturn>(Expression<Func<T, Func<T1, T2, T3, T4, T5, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, T4, Treturn>(Expression<Func<T, Func<T1, T2, T3, T4, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, T3, Treturn>(Expression<Func<T, Func<T1, T2, T3, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, T2, Treturn>(Expression<Func<T, Func<T1, T2, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<T1, Treturn>(Expression<Func<T, Func<T1, Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
		public static MethodInfo MethodInfo<Treturn>(Expression<Func<T, Func<Treturn>>> selector)
		{
			return new MethodReflektor<object>(selector).Value;
		}
	}
}
