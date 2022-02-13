using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public abstract class TreeEnumeratorBase<Tvalue,Tcurrent> : IEnumerator<Tcurrent>
    {
        protected TreeBranchingStrategy branchingStrategy;
        protected readonly List<(PropertyInfo Property, Func<object, object> Accessor, bool PropertyTypeIsValueType)> propertyMap;
        protected Tcurrent current;
        protected readonly object rootObject;
        protected TreeEnumeratorBase<Tvalue, Tcurrent> currentBranchEnumerator;

        protected int Depth { get; private set; } = 0;
        protected ObjectIDGenerator LoopDetector { get; private set; }
        protected int CurrentIndex { get; set; } = -1;

        private static ConcurrentDictionary<Type, List<(PropertyInfo, Func<object, object>, bool)>> factoryCache =
            new ConcurrentDictionary<Type, List<(PropertyInfo, Func<object, object>, bool)>>();

        private static List<(PropertyInfo property, Func<object, object> accessor, bool propertyTypeIsValueType)> MapType(Type type)
        {
            var properties = factoryCache.GetOrAdd(type,
                                                   t =>
                                                   {
                                                       var factory = new ReflektPropertyGetterDelegateFactory(t);
                                                       return t.GetProperties()
                                                         .Where(x => typeof(Tvalue).IsAssignableFrom(x.PropertyType) || // anything that might itself contain a Tvalue...
                                                                     x.PropertyType.IsAssignableFrom(typeof(Tvalue)) ||
                                                                     !x.PropertyType.IsValueType) // ...plus *all* non-value-types to continue navigating through
                                                         .Select(x => (x, factory.Create(x.GetMethod), typeof(Tvalue).IsAssignableFrom(x.PropertyType)))
                                                         .ToList();
                                                   });
            return properties;
        }

        protected TreeEnumeratorBase(object rootObject,
                                               int depth,
                                               ObjectIDGenerator loopDetector,
                                               TreeBranchingStrategy branchingStrategy)
        {
            propertyMap = MapType(rootObject.GetType());
            this.rootObject = rootObject;
            this.Depth = depth;
            this.LoopDetector = loopDetector ?? new ObjectIDGenerator();
            this.branchingStrategy = branchingStrategy;
        }

        public Tcurrent Current => currentBranchEnumerator == null ? current : currentBranchEnumerator.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // no-op
        }

        public abstract bool MoveNext();

        protected abstract TreeEnumeratorBase<Tvalue, Tcurrent> GetBranchEnumerator(object value);

        protected abstract Tcurrent MapValue(Tvalue currentValue);

        public virtual void Reset()
        {
            CurrentIndex = -1;
            currentBranchEnumerator = null;
            current = default;
        }
    }
}
