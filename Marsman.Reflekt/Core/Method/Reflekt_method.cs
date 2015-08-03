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
        public static VoidMethodReflekt<T> Method()
        {
            return new VoidMethodReflekt<T>(x => new MethodReflekt<T>(x));
        }

        public static ReturnMethodReflekt<T, Treturn> Method<Treturn>()
        {
            return new ReturnMethodReflekt<T, Treturn>(x => new MethodReflekt<T>(x));
        }

        public VoidMethodReflekt<T> method()
        {
            return new VoidMethodReflekt<T>(x => new MethodReflekt<T>(x));
        }

        public ReturnMethodReflekt<T, Treturn> method<Treturn>()
        {
            return new ReturnMethodReflekt<T, Treturn>(x => new MethodReflekt<T>(x));
        }
    }
}
