using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public sealed class DepthFirstTreeEnumerator<Tvalue> : DepthFirstTreeEnumeratorBase<Tvalue, Tvalue>
    {
        public DepthFirstTreeEnumerator(object rootObject,
                              TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.AllProperties,
                              int maxDepth = int.MaxValue,
                              Filter[] filters = null)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy, filters, maxDepth)
        {
        }

        private DepthFirstTreeEnumerator(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy,
                               Filter[] filters,
                               int maxDepth)
            : base(rootObject, depth, loopDetector, branchingStrategy, maxDepth, filters)
        {
        }

        protected sealed override DepthFirstTreeEnumeratorBase<Tvalue, Tvalue> GetBranchEnumerator(object value)
        {
            return new DepthFirstTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy, filters, MaxDepth);
        }

        protected sealed override Tvalue MapValue(Tvalue currentValue)
        {
            return currentValue;
        }
    }
}
