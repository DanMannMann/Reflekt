using Marsman.Reflekt.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    public partial class Reflekt<T>
    {
        public static ReflektConstructor<T> Constructor()
        {
            return new ReflektConstructor<T>(x => new ConstructorReflektor<T>(x));
        }

        public ReflektConstructor<T> constructor()
        {
			return Constructor();
        }
    }
}