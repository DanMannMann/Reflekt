using Marsman.Ember.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
{
    public class GenericMethodReflekt<T> : MethodReflekt<T>
    {
        private Type[] _newTypes;

        internal GenericMethodReflekt(Expression ex, Type[] newTypes) : base(ex) { _newTypes = newTypes; }

        public override MethodInfo Value
        {
            get { return MethodBuilderVisitor.GetMethodInfo(Selector, _newTypes); }
        }
    }
}