using System.Collections;
using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public class TreeEnumerable<Tvalue> : IEnumerable<Tvalue>
    {
        private readonly object rootObject;
        private readonly TreeBranchingStrategy branchingStrategy;
        private readonly TreeEnumerationStrategy enumerationStrategy;
        private readonly Filter[] filters;

        public TreeEnumerable(object rootObject,
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

        public IEnumerator<Tvalue> GetEnumerator()
        {
            return enumerationStrategy switch
            {
                TreeEnumerationStrategy.BreadthFirst => new BreadthFirstTreeEnumerator<Tvalue>(rootObject, branchingStrategy, filters),
                _ => new DepthFirstTreeEnumerator<Tvalue>(rootObject, branchingStrategy, filters),
            };
        }
    }
}
