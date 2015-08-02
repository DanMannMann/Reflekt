using Marsman.Ember.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
{
    public class GenericConstructorReflekt<T> : ConstructorReflekt<T>
    {
        private Type[] _types;

        internal GenericConstructorReflekt(Expression ex, Type[] types) : base(ex) { _types = types; }

        public override ConstructorInfo Value
        {
            get
            {
                return ConstructorBuilderVisitor.GetConstructorInfo(Selector, _types);
            }
        }
    }
}