using System;
using System.Collections;
using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public class ContextualTreeEnumerable<Tvalue> : IEnumerable<TreeEnumerationContext<Tvalue>>
    {
        private readonly object rootObject;
        private readonly TreeBranchingStrategy branchingStrategy;
        private readonly TreeEnumerationStrategy enumerationStrategy;
        private readonly Filter[] filters;

        public ContextualTreeEnumerable(object rootObject,
                                        TreeBranchingStrategy branchingStrategy,
                                        TreeEnumerationStrategy enumerationStrategy,
                                        params Filter[] filters)
        {
            this.rootObject = rootObject;
            this.branchingStrategy = branchingStrategy;
            this.enumerationStrategy = enumerationStrategy;
            this.filters = filters;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<TreeEnumerationContext<Tvalue>> GetEnumerator()
        {
            return enumerationStrategy switch
            {
                TreeEnumerationStrategy.BreadthFirst => new BreadthFirstContextualTreeEnumerator<Tvalue>(rootObject, branchingStrategy, filters),
                _ => new DepthFirstContextualTreeEnumerator<Tvalue>(rootObject, branchingStrategy, filters),
            };
        }
    }

    public enum FilterMode
    {
        IncludeBranch,
        ExcludeBranch,
        IncludeValues,
        ExcludeValues,
        ExcludeBoth,
        IncludeBoth,
    }
    public class Filter
    {
        private Filter() { }
        public Type Type { get; private set; }
        public FilterMode Mode { get; private set; }
        public static Filter ExcludeBranches<T>()
        {
            return new Filter
            {
                Mode = FilterMode.ExcludeBranch,
                Type = typeof(T)
            };
        }
        public static Filter IncludeBranches<T>()
        {
            return new Filter
            {
                Mode = FilterMode.IncludeBranch,
                Type = typeof(T)
            };
        }
        public static Filter ExcludeValues<T>()
        {
            return new Filter
            {
                Mode = FilterMode.ExcludeValues,
                Type = typeof(T)
            };
        }
        public static Filter IncludeValues<T>()
        {
            return new Filter
            {
                Mode = FilterMode.IncludeValues,
                Type = typeof(T)
            };
        }
        public static Filter ExcludeBranchesAndValues<T>()
        {
            return new Filter
            {
                Mode = FilterMode.ExcludeBoth,
                Type = typeof(T)
            };
        }
        public static Filter IncludeBranchesAndValues<T>()
        {
            return new Filter
            {
                Mode = FilterMode.IncludeBoth,
                Type = typeof(T)
            };
        }
    }
}
