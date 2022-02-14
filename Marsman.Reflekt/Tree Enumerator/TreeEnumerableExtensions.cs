using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public static class TreeEnumerableExtensions
    {
        public static IEnumerable<Tvalue> AsTreeEnumerable<Tvalue>(this object rootObject, 
                                                                   TreeEnumerationStrategy enumerationStrategy,
                                                                   TreeBranchingStrategy branchingStrategy,
                                                                   params Filter[] filters)
        {
            return new TreeEnumerable<Tvalue>(rootObject, branchingStrategy, enumerationStrategy, filters);
        }
        public static IEnumerable<Tvalue> AsTreeEnumerable<Tvalue>(this object rootObject,
            params Filter[] filters) =>
                AsTreeEnumerable<Tvalue>(rootObject,
                                         TreeEnumerationStrategy.DepthFirst,
                                         TreeBranchingStrategy.AllProperties,
                                         filters);
        public static IEnumerable<Tvalue> AsTreeEnumerable<Tvalue>(this object rootObject,
            TreeEnumerationStrategy enumerationStrategy,
            params Filter[] filters) =>
                AsTreeEnumerable<Tvalue>(rootObject,
                                         enumerationStrategy,
                                         TreeBranchingStrategy.AllProperties,
                                         filters);
        public static IEnumerable<Tvalue> AsTreeEnumerable<Tvalue>(this object rootObject,
            TreeBranchingStrategy branchingStrategy,
            params Filter[] filters) =>
                AsTreeEnumerable<Tvalue>(rootObject,
                                         TreeEnumerationStrategy.DepthFirst,
                                         branchingStrategy,
                                         filters);

        public static IEnumerable<TreeEnumerationContext<Tvalue>> AsTreeEnumerableWithContext<Tvalue>(this object rootObject,
                                                                   TreeEnumerationStrategy enumerationStrategy,
                                                                   TreeBranchingStrategy branchingStrategy,
                                                                   params Filter[] filters)
        {
            return new ContextualTreeEnumerable<Tvalue>(rootObject, branchingStrategy, enumerationStrategy, filters);
        }
        public static IEnumerable<TreeEnumerationContext<Tvalue>> AsTreeEnumerableWithContext<Tvalue>(this object rootObject,
            params Filter[] filters) =>
                AsTreeEnumerableWithContext<Tvalue>(rootObject,
                                                   TreeEnumerationStrategy.DepthFirst,
                                                   TreeBranchingStrategy.AllProperties,
                                                   filters);
        public static IEnumerable<TreeEnumerationContext<Tvalue>> AsTreeEnumerableWithContext<Tvalue>(this object rootObject,
            TreeEnumerationStrategy enumerationStrategy,
            params Filter[] filters) =>
                AsTreeEnumerableWithContext<Tvalue>(rootObject,
                                                   enumerationStrategy,
                                                   TreeBranchingStrategy.AllProperties,
                                                   filters);
        public static IEnumerable<TreeEnumerationContext<Tvalue>> AsTreeEnumerableWithContext<Tvalue>(this object rootObject,
            TreeBranchingStrategy branchingStrategy,
            params Filter[] filters) =>
                AsTreeEnumerableWithContext<Tvalue>(rootObject,
                                                   TreeEnumerationStrategy.DepthFirst,
                                                   branchingStrategy,
                                                   filters);
    }
}
