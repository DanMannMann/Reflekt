using System.Reflection;

namespace Marsman.Reflekt
{
    public class TreeEnumerationContext<Tvalue>
    {
        public PropertyInfo Property { get; internal set; }
        public Tvalue Value { get; internal set; }
        public int Depth { get; internal set; }

        public override string ToString()
        {
            return $"Depth: {Depth}, Property: {Property.Name}, Value Type: {Value?.GetType()}";
        }
    }
}
