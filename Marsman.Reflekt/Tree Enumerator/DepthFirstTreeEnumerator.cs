using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public class DepthFirstTreeEnumerator<Tvalue> : DepthFirstTreeEnumeratorBase<Tvalue, Tvalue>
    {
        public DepthFirstTreeEnumerator(object rootObject,
                              TreeBranchingStrategy branchingStrategy = TreeBranchingStrategy.PropertyValueIsValueType)
            : this(rootObject, 0, new ObjectIDGenerator(), branchingStrategy)
        {
        }

        private DepthFirstTreeEnumerator(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy)
            : base(rootObject, depth, loopDetector, branchingStrategy)
        {
        }

        protected override TreeEnumeratorBase<Tvalue, Tvalue> GetBranchEnumerator(object value)
        {
            return new DepthFirstTreeEnumerator<Tvalue>(value, Depth + 1, LoopDetector, branchingStrategy);
        }

        protected override Tvalue MapValue(Tvalue currentValue)
        {
            return currentValue;
        }
    }
}
