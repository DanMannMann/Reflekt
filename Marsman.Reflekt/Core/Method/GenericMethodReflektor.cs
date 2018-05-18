using Marsman.Reflekt.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Reflekt
{
    internal class GenericMethodReflektor<T> : MethodReflektor<T>
    {
        private Type[] _newTypes;

        internal GenericMethodReflektor(Expression ex, Type[] newTypes) : base(ex) { _newTypes = newTypes; }

        public override MethodInfo Value
        {
            get { return MethodVisitor.GetMethodInfo(Selector, _newTypes); }
        }
    }
}