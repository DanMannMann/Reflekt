using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public sealed class BreadthFirstContextualTreeEnumerator<Tvalue> : BreadthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>>
    {
        private readonly TreeEnumerationContext<Tvalue> context = new TreeEnumerationContext<Tvalue>();
        public BreadthFirstContextualTreeEnumerator(object rootObject,
                                                  TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.AllProperties,
                                                  Filter[] filters = null)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy, filters)
        {
        }

        private BreadthFirstContextualTreeEnumerator(object rootObject,
                                                   int depth,
                                                   ObjectIDGenerator loopDetector,
                                                   TreeBranchingStrategy branchingStrategy,
                                                   Filter[] filters)
            : base(rootObject, depth, loopDetector, branchingStrategy, filters)
        {
            context.Depth = depth;
        }

        protected sealed override BreadthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>> GetBranchEnumerator(object value)
        {
            return new BreadthFirstContextualTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy, filters);
        }

        protected sealed override TreeEnumerationContext<Tvalue> MapValue(Tvalue currentValue)
        {
            context.Value = currentValue;
            context.Property = propertyMap[CurrentIndex].Property;
            return context;
        }
    }
}
