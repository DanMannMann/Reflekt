using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public abstract class DepthFirstTreeEnumeratorBase<Tvalue,Tcurrent> : TreeEnumeratorBase<Tvalue,Tcurrent>, IEnumerator<Tcurrent>
    {
        protected TreeEnumeratorBase<Tvalue, Tcurrent> nextBranchEnumerator;

        protected DepthFirstTreeEnumeratorBase(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy)
            : base(rootObject, depth, loopDetector, branchingStrategy)
        {
        }

        public override void Reset()
        {
            base.Reset();
            nextBranchEnumerator = null;
        }

        public override bool MoveNext()
        {
            if (nextBranchEnumerator != null && currentBranchEnumerator == null)
            {
                currentBranchEnumerator = nextBranchEnumerator;
                nextBranchEnumerator = null;
            }

            if (currentBranchEnumerator != null)
            {
                if (currentBranchEnumerator.MoveNext())
                {
                    return true;
                }
                currentBranchEnumerator = null; // MoveNext = false, branch exhausted
            }

            if (++CurrentIndex < propertyMap.Count)
            {
                var value = propertyMap[CurrentIndex].Accessor(rootObject);

                if (value == null)
                {
                    return MoveNext(); // try the next property
                }

                if (value is Tvalue t)
                {
                    current = MapValue(t);
                }

                var branch = branchingStrategy == TreeBranchingStrategy.PropertyTypeIsValueType && propertyMap[CurrentIndex].PropertyTypeIsValueType;
                branch |= branchingStrategy == TreeBranchingStrategy.PropertyValueIsValueType && value is Tvalue;
                branch |= branchingStrategy == TreeBranchingStrategy.AllProperties;
                branch &= value != null;
                if (branch)
                {
                    LoopDetector.GetId(value, out var isNew);
                    if (isNew) // only branch into this object if it isn't already known
                    {
                        nextBranchEnumerator = GetBranchEnumerator(value);
                    }
                }

                return value is Tvalue || MoveNext();
            }
            return false;
        }
    }
}
