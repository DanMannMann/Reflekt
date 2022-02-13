using System.Collections;
using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public class ContextualTreeEnumerable<Tvalue> : IEnumerable<TreeEnumerationContext<Tvalue>>
    {
        private readonly object rootObject;
        private readonly TreeBranchingStrategy branchStrategy;
        private readonly TreeEnumerationStrategy enumStrategy;

        public ContextualTreeEnumerable(object rootObject, TreeBranchingStrategy branchStrategy, TreeEnumerationStrategy enumStrategy)
        {
            this.rootObject = rootObject;
            this.branchStrategy = branchStrategy;
            this.enumStrategy = enumStrategy;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<TreeEnumerationContext<Tvalue>> GetEnumerator()
        {
            return enumStrategy switch
            {
                TreeEnumerationStrategy.BreadthFirst => new BreadthFirstContextualTreeEnumerator<Tvalue>(rootObject, branchStrategy),
                _ => new DepthFirstContextualTreeEnumerator<Tvalue>(rootObject, branchStrategy),
            };
        }
    }
}
