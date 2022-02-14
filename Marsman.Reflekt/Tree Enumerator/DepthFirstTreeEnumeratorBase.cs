using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{
    public abstract class DepthFirstTreeEnumeratorBase<Tvalue,Tcurrent> : TreeEnumeratorBase<Tvalue,Tcurrent, DepthFirstTreeEnumeratorBase<Tvalue, Tcurrent>>, IEnumerator<Tcurrent>
    {
        protected DepthFirstTreeEnumeratorBase<Tvalue, Tcurrent> nextBranchEnumerator;

        protected DepthFirstTreeEnumeratorBase(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy,
                               params Filter[] filters)
            : base(rootObject, depth, loopDetector, branchingStrategy, filters)
        {
        }

        public sealed override void Reset()
        {
            base.Reset();
            nextBranchEnumerator = null;
        }

        public sealed override bool MoveNext()
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

                bool match = false;
                if (value is Tvalue t && GetOrAddValueCheck(t.GetType()))
                {
                    match = true;
                    current = MapValue(t);
                }
                var branch = ShouldBranch(value, propertyMap[CurrentIndex]);
                if (branch)
                {
                    LoopDetector.GetId(value, out var isNew);
                    if (isNew) // only branch into this object if it isn't already known
                    {
                        nextBranchEnumerator = GetBranchEnumerator(value);
                    }
                }

                return match || MoveNext();
            }
            return false;
        }
    }
}
