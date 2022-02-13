using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public class BreadthFirstContextualTreeEnumerator<Tvalue> : BreadthFirstTreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>>
    {
        private readonly TreeEnumerationContext<Tvalue> context = new TreeEnumerationContext<Tvalue>();
        public BreadthFirstContextualTreeEnumerator(object rootObject,
                                                  TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.PropertyValueIsValueType)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy)
        {
        }

        private BreadthFirstContextualTreeEnumerator(object rootObject,
                                                   int depth,
                                                   ObjectIDGenerator loopDetector,
                                                   TreeBranchingStrategy branchingStrategy)
            : base(rootObject, depth, loopDetector, branchingStrategy)
        {
            context.Depth = depth;
        }

        protected override TreeEnumeratorBase<Tvalue, TreeEnumerationContext<Tvalue>> GetBranchEnumerator(object value)
        {
            return new BreadthFirstContextualTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy);
        }

        protected override TreeEnumerationContext<Tvalue> MapValue(Tvalue currentValue)
        {
            context.Value = currentValue;
            context.Property = propertyMap[CurrentIndex].Property;
            return context;
        }
    }
}
