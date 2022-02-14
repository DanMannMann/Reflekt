using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public sealed class DepthFirstContextualTreeEnumerator<Tvalue> : DepthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>>
    {
        private readonly TreeEnumerationContext<Tvalue> context = new TreeEnumerationContext<Tvalue>();
        public DepthFirstContextualTreeEnumerator(object rootObject,
                                                  TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.AllProperties,
                                                  Filter[] filters = null)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy, filters)
        {
        }

        private DepthFirstContextualTreeEnumerator(object rootObject,
                                                   int depth,
                                                   ObjectIDGenerator loopDetector,
                                                   TreeBranchingStrategy branchingStrategy,
                                                   Filter[] filters)
            : base(rootObject, depth, loopDetector, branchingStrategy, filters)
        {
            context.Depth = depth;
        }

        protected sealed override DepthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>> GetBranchEnumerator(object value)
        {
            return new DepthFirstContextualTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy, filters);
        }

        protected sealed override TreeEnumerationContext<Tvalue> MapValue(Tvalue currentValue)
        {
            context.Value = currentValue;
            context.Property = propertyMap[CurrentIndex].Property;
            return context;
        }
    }
}
