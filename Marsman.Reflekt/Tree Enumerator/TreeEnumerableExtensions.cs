using System.Collections.Generic;

namespace Marsman.Reflekt
{
    public static class TreeEnumerableExtensions
    {
        public static IEnumerable<Tvalue> AsTreeEnumerable<Tvalue>(this object rootObject, 
                                                                   TreeEnumerationStrategy enumStrategy = TreeEnumerationStrategy.DepthFirst,
                                                                   TreeBranchingStrategy branchStrategy = TreeBranchingStrategy.PropertyValueIsValueType)
        {
            return new TreeEnumerable<Tvalue>(rootObject, branchStrategy, enumStrategy);
        }
        public static IEnumerable<TreeEnumerationContext<Tvalue>> AsContextualTreeEnumerable<Tvalue>(this object rootObject,
                                                                   TreeEnumerationStrategy enumStrategy = TreeEnumerationStrategy.DepthFirst,
                                                                   TreeBranchingStrategy branchStrategy = TreeBranchingStrategy.PropertyValueIsValueType)
        {
            return new ContextualTreeEnumerable<Tvalue>(rootObject, branchStrategy, enumStrategy);
        }
    }
}
