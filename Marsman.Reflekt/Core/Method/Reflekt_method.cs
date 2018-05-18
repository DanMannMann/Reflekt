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
    }
}
