using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Marsman.Reflekt
{

    public abstract class BreadthFirstTreeEnumeratorBase<Tvalue, Tcurrent> : TreeEnumeratorBase<Tvalue, Tcurrent>, IEnumerator<Tcurrent>, IMoveNextWithinDepth
    {
        private List<TreeEnumeratorBase<Tvalue, Tcurrent>> currentDepthBranches = new List<TreeEnumeratorBase<Tvalue, Tcurrent>>();
        private List<TreeEnumeratorBase<Tvalue, Tcurrent>> nonExhaustedBranches = new List<TreeEnumeratorBase<Tvalue, Tcurrent>>();
        private int currentBranchIndex = -1;

        protected BreadthFirstTreeEnumeratorBase(object rootObject,
                               int depth,
                               ObjectIDGenerator loopDetector,
                               TreeBranchingStrategy branchingStrategy)
            : base(rootObject, depth, loopDetector, branchingStrategy)
        {
        }

        public override void Reset()
        {
            base.Reset();
            currentBranchIndex = -1;
            nonExhaustedBranches.Clear();
            currentDepthBranches.Clear();
        }

        public virtual EnumerationState MoveNextWithinDepth()
        {
            if (++CurrentIndex < propertyMap.Count)
            {
                var value = propertyMap[CurrentIndex].Accessor(rootObject);
                if (value is Tvalue t)
                {
                    current = MapValue(t);
                    return EnumerationState.Moved;
                }
                return MoveNextWithinDepth(); // try the next local value
            }
            if (CurrentIndex == propertyMap.Count) return EnumerationState.EndOfDepth;

            if (currentBranchEnumerator != null)
            {
                var result = (currentBranchEnumerator as BreadthFirstTreeEnumeratorBase<Tvalue, Tcurrent>).MoveNextWithinDepth();
                switch (result)
                {
                    case EnumerationState.Moved:
                        return EnumerationState.Moved;

                    case EnumerationState.EndOfDepth:
                        currentDepthBranches.Remove(currentBranchEnumerator); // done with this one until next depth
                        currentBranchEnumerator = null; // move to the next one on next call
                        return MoveNextWithinDepth();

                    case EnumerationState.EndOfItems:
                        currentDepthBranches.Remove(currentBranchEnumerator);
                        nonExhaustedBranches.Remove(currentBranchEnumerator); // don't look in this one any more
                        currentBranchEnumerator = null; // move to the next one on next call
                        return MoveNextWithinDepth();

                    default:
                        throw new InvalidOperationException("Unexpected fall through");
                }
            }
            if (currentBranchIndex == propertyMap.Count - 1) // end of depth for the first pass
            {
                currentBranchIndex++; // stop us coming back in here
                return EnumerationState.EndOfDepth;
            }
            if (currentBranchIndex == propertyMap.Count && nonExhaustedBranches.Count == 0) // actual end
            {
                return EnumerationState.EndOfItems;
            }
            if (currentBranchIndex == propertyMap.Count && currentDepthBranches.Count == 0) // end of depth for passes n > 0
            {
                currentDepthBranches.AddRange(nonExhaustedBranches);
                return EnumerationState.EndOfDepth;
            }

            if (currentBranchIndex < propertyMap.Count - 1)
            {
                currentBranchIndex++;
                var value = propertyMap[currentBranchIndex].Accessor(rootObject);
                var branch = branchingStrategy == TreeBranchingStrategy.PropertyTypeIsValueType && propertyMap[currentBranchIndex].PropertyTypeIsValueType;
                branch |= branchingStrategy == TreeBranchingStrategy.PropertyValueIsValueType && value is Tvalue;
                branch |= branchingStrategy == TreeBranchingStrategy.AllProperties;
                branch &= value != null;
                if (branch)
                {
                    LoopDetector.GetId(value, out var isNew);
                    if (isNew) // only branch into this object if it isn't already known
                    {
                        currentDepthBranches.Add(currentBranchEnumerator = GetBranchEnumerator(value));
                        nonExhaustedBranches.Add(currentBranchEnumerator);
                    }
                }
                return MoveNextWithinDepth();
            }

            if (currentDepthBranches.Any())
            {
                currentBranchEnumerator = currentDepthBranches.First();
                return MoveNextWithinDepth();
            }
            throw new InvalidOperationException("Unexpected fall through");
        }

        public override bool MoveNext()
        {
            if (++CurrentIndex < propertyMap.Count)
            {
                var value = propertyMap[CurrentIndex].Accessor(rootObject);
                if (value is Tvalue t)
                {
                    current = MapValue(t);
                    return true;
                }
                return MoveNext();
            }

            if (currentBranchEnumerator != null)
            {
                var result = (currentBranchEnumerator as IMoveNextWithinDepth).MoveNextWithinDepth(); switch (result)
                {
                    case EnumerationState.Moved:
                        return true;

                    case EnumerationState.EndOfDepth:
                        currentDepthBranches.Remove(currentBranchEnumerator); // done with this one until next depth
                        currentBranchEnumerator = null; // move to the next one on next call
                        return MoveNext();

                    case EnumerationState.EndOfItems:
                        currentDepthBranches.Remove(currentBranchEnumerator);
                        nonExhaustedBranches.Remove(currentBranchEnumerator); // don't look in this one any more
                        currentBranchEnumerator = null; // move to the next one on next call
                        return MoveNext();

                    default:
                        throw new InvalidOperationException("Unexpected fall through");
                }
            }
            if (currentBranchIndex == propertyMap.Count -1 && nonExhaustedBranches.Count == 0) // actual end
            {
                return false;
            }
            if (currentBranchIndex == propertyMap.Count -1 && currentDepthBranches.Count == 0) // end of depth for passes n > 0
            {
                currentDepthBranches.AddRange(nonExhaustedBranches);
                return MoveNext();
            }

            if (currentBranchIndex < propertyMap.Count - 1)
            {
                currentBranchIndex++;
                var value = propertyMap[currentBranchIndex].Accessor(rootObject);
                var branch = branchingStrategy == TreeBranchingStrategy.PropertyTypeIsValueType && propertyMap[currentBranchIndex].PropertyTypeIsValueType;
                branch |= branchingStrategy == TreeBranchingStrategy.PropertyValueIsValueType && value is Tvalue;
                branch |= branchingStrategy == TreeBranchingStrategy.AllProperties;
                branch &= value != null;
                if (branch)
                {
                    LoopDetector.GetId(value, out var isNew);
                    if (isNew) // only branch into this object if it isn't already known
                    {
                        currentDepthBranches.Add(currentBranchEnumerator = GetBranchEnumerator(value));
                        nonExhaustedBranches.Add(currentBranchEnumerator);
                    }
                }
                return MoveNext();
            }

            if (currentDepthBranches.Any())
            {
                currentBranchEnumerator = currentDepthBranches.First();
                return MoveNext();
            }
            throw new InvalidOperationException("Unexpected fall through");
        }
    }
}
