using System.Collections;
using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public class TreeEnumerable<Tvalue> : IEnumerable<Tvalue>
    {
        private readonly object rootObject;
        private readonly TreeBranchingStrategy branchStrategy;
        private readonly TreeEnumerationStrategy enumStrategy;

        public TreeEnumerable(object rootObject, TreeBranchingStrategy branchStrategy, TreeEnumerationStrategy enumStrategy)
        {
            this.rootObject = rootObject;
            this.branchStrategy = branchStrategy;
            this.enumStrategy = enumStrategy;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public IEnumerator<Tvalue> GetEnumerator()
        {
            return enumStrategy switch
            {
                TreeEnumerationStrategy.BreadthFirst => new BreadthFirstTreeEnumerator<Tvalue>(rootObject, branchStrategy),
                _ => new DepthFirstTreeEnumerator<Tvalue>(rootObject, branchStrategy),
            };
        }
    }
}
