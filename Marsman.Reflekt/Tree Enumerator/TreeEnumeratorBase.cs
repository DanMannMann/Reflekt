using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public abstract class TreeEnumeratorBase<Tvalue,Tcurrent,TbranchEnumerator> : IEnumerator<Tcurrent> 
        where TbranchEnumerator : TreeEnumeratorBase<Tvalue, Tcurrent, TbranchEnumerator>
    {
        protected TreeBranchingStrategy branchingStrategy;
        protected readonly Filter[] filters;
        protected readonly List<(PropertyInfo Property, Func<object, object> Accessor, bool PropertyTypeIsTvalue)> propertyMap;
        protected readonly Dictionary<Type, bool> passesBranchFilter = new Dictionary<Type, bool>();
        protected readonly Dictionary<Type, bool> passesValueFilter = new Dictionary<Type, bool>();
        protected Tcurrent current;
        protected readonly object rootObject;
        protected TbranchEnumerator currentBranchEnumerator;
        private readonly bool explicitFilter;

        protected int Depth { get; private set; } = 0;
        protected ObjectIDGenerator LoopDetector { get; private set; }
        protected int CurrentIndex { get; set; } = -1;

        private static ConcurrentDictionary<Type, List<(PropertyInfo, Func<object, object>, bool)>> factoryCache =
            new ConcurrentDictionary<Type, List<(PropertyInfo, Func<object, object>, bool)>>();

        private static List<(PropertyInfo property, Func<object, object> accessor, bool PropertyTypeIsTvalue)> MapType(Type type)
        {
            var properties = factoryCache.GetOrAdd(type,
                                                   t =>
                                                   {
                                                       var factory = new ReflektPropertyGetterDelegateFactory(t);
                                                       return t.GetProperties()
                                                         .Where(x => !x.GetIndexParameters().Any()) // ignore indexer properties - we don't explore into collections' items
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
                                               TreeBranchingStrategy branchingStrategy,
                                               Filter[] filters)
        {
            propertyMap = MapType(rootObject.GetType());
            this.rootObject = rootObject;
            this.Depth = depth;
            this.LoopDetector = loopDetector ?? new ObjectIDGenerator();
            this.branchingStrategy = branchingStrategy;
            this.filters = filters;
            this.explicitFilter = filters?.Any(x => x.Mode == FilterMode.IncludeBranch) ?? false;
        }

        public Tcurrent Current => currentBranchEnumerator == null ? current : currentBranchEnumerator.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // no-op
        }

        public abstract bool MoveNext();

        protected abstract TbranchEnumerator GetBranchEnumerator(object value);

        protected abstract Tcurrent MapValue(Tvalue currentValue);

        public virtual void Reset()
        {
            CurrentIndex = -1;
            currentBranchEnumerator = null;
            current = default;
        }

        protected bool ShouldBranch(object value, (PropertyInfo Property, Func<object, object> Accessor, bool PropertyTypeIsTvalue) propertyRecord)
        {
            if (value == null) return false;
            var branch = false;
            switch (branchingStrategy)
            {
                case TreeBranchingStrategy.PropertyTypeIsTvalue:
                    branch = propertyRecord.PropertyTypeIsTvalue && GetOrAddBranchCheck(propertyRecord.Property.PropertyType);
                    break;

                case TreeBranchingStrategy.PropertyValueIsTvalue:
                    branch = value is Tvalue && GetOrAddBranchCheck(value.GetType());
                    break;

                case TreeBranchingStrategy.AllProperties:
                    branch = GetOrAddBranchCheck(value.GetType());
                    break;
            }
            return branch;
        }

        protected bool GetOrAddValueCheck(Type type)
        {
            if (!passesValueFilter.ContainsKey(type))
            {
                passesValueFilter[type] = !filters.Any(x => (x.Mode == FilterMode.ExcludeValues || x.Mode == FilterMode.ExcludeBoth) && x.Type.IsAssignableFrom(type)) &&
                                          (!explicitFilter || filters.Any(x => (x.Mode == FilterMode.IncludeValues || x.Mode == FilterMode.IncludeBoth) && x.Type.IsAssignableFrom(type)));
            }
            return passesValueFilter[type];
        }

        protected bool GetOrAddBranchCheck(Type type)
        {
            if (!passesBranchFilter.ContainsKey(type))
            {
                passesBranchFilter[type] = !filters.Any(x => (x.Mode == FilterMode.ExcludeBranch || x.Mode == FilterMode.ExcludeBoth) && x.Type.IsAssignableFrom(type)) &&
                                           (!explicitFilter || filters.Any(x => (x.Mode == FilterMode.IncludeBranch || x.Mode == FilterMode.IncludeBoth) && x.Type.IsAssignableFrom(type)));
            }
            return passesBranchFilter[type];
        }
    }
}
