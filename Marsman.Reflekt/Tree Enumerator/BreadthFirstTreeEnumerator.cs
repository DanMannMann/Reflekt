using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public sealed class BreadthFirstTreeEnumerator<Tvalue> : BreadthFirstTreeEnumeratorBase<Tvalue, Tvalue>
    {
        public BreadthFirstTreeEnumerator(object rootObject,
                              TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.AllProperties,
                              int maxDepth = int.MaxValue,
                              Filter[] filters = null)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy, filters, maxDepth)
        {
        }

        private BreadthFirstTreeEnumerator(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy,
                               Filter[] filters,
                               int maxDepth)
            : base(rootObject, depth, loopDetector, branchingStrategy, maxDepth, filters)
        {
        }

        protected sealed override BreadthFirstTreeEnumeratorBase<Tvalue, Tvalue> GetBranchEnumerator(object value)
        {
            return new BreadthFirstTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy, filters, MaxDepth);
        }

        protected sealed override Tvalue MapValue(Tvalue currentValue)
        {
            return currentValue;
        }
    }
}
