using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public sealed class BreadthFirstContextualTreeEnumerator<Tvalue> : BreadthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>>
    {
        private readonly TreeEnumerationContext<Tvalue> context = new TreeEnumerationContext<Tvalue>();
        public BreadthFirstContextualTreeEnumerator(object rootObject,
                                                  TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.AllProperties,
                                                   int maxDepth = int.MaxValue,
                                                  Filter[] filters = null)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy, filters, maxDepth)
        {
        }

        private BreadthFirstContextualTreeEnumerator(object rootObject,
                                                   int depth,
                                                   ObjectIDGenerator loopDetector,
                                                   TreeBranchingStrategy branchingStrategy,
                                                   Filter[] filters,
                                                   int maxDepth)
            : base(rootObject, depth, loopDetector, branchingStrategy, maxDepth, filters)
        {
            context.Depth = depth;
        }

        protected sealed override BreadthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>> GetBranchEnumerator(object value)
        {
            return new BreadthFirstContextualTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy, filters, MaxDepth);
        }

        protected sealed override TreeEnumerationContext<Tvalue> MapValue(Tvalue currentValue)
        {
            context.Value = currentValue;
            context.Property = propertyMap[CurrentIndex].Property;
            return context;
        }
    }
}
