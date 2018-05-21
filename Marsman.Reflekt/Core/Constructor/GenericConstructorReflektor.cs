using Marsman.Reflekt.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    internal class GenericConstructorReflektor<T> : ConstructorReflektor<T>
    {
        private Type[] _types;

        internal GenericConstructorReflektor(Expression ex, Type[] types) : base(ex) { _types = types; }

        public override ConstructorInfo Value
        {
            get
            {
                return ConstructorVisitor.GetConstructorInfo(Selector, _types);
            }
        }
    }
}