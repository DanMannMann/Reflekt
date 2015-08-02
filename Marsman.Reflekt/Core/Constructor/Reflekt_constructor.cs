using Marsman.Ember.Visitors;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Marsman.Ember
{
    public partial class Reflekt<T>
    {
        public static ConstructorSelectorReflekt<T> Constructor()
        {
            return new ConstructorSelectorReflekt<T>(x => new ConstructorReflekt<T>(x));
        }

        public ConstructorSelectorReflekt<T> constructor()
        {
            return new ConstructorSelectorReflekt<T>(x => new ConstructorReflekt<T>(x));
        }
    }
}