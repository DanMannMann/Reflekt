using System.Collections;
using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public class TreeEnumerable<Tvalue> : IEnumerable<Tvalue>
    {
        private readonly object rootObject;
        private readonly TreeBranchingStrategy branchingStrategy;
        private readonly TreeEnumerationStrategy enumerationStrategy;
        private readonly int maxDepth;
        private readonly Filter[] filters;

        public TreeEnumerable(object rootObject,
                              TreeBranchingStrategy branchingStrategy,
                              TreeEnumerationStrategy enumerationStrategy,
                              int maxDepth = int.MaxValue,
                              params Filter[] filters)
        {
            this.rootObject = rootObject;
            this.branchingStrategy = branchingStrategy;
            this.enumerationStrategy = enumerationStrategy;
            this.maxDepth = maxDepth;
            this.filters = filters;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<Tvalue> GetEnumerator()
        {
            return enumerationStrategy switch
            {
                TreeEnumerationStrategy.BreadthFirst => new BreadthFirstTreeEnumerator<Tvalue>(rootObject, branchingStrategy, maxDepth, filters),
                _ => new DepthFirstTreeEnumerator<Tvalue>(rootObject, branchingStrategy, maxDepth, filters),
            };
        }
    }
}
