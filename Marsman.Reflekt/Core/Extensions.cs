using System;

namespace Marsman.Reflekt
{
    public static class Extensions
    {
        public static Reflekt<T> Reflekt<T>(this T target)
        {
            return new Reflekt<T>();
        }

        public static Reflekt<T> Reflekt<T>(this Type type)
        {
            if (typeof(T) != type)
            {
                throw new InvalidOperationException("Type parameter does not match the type object");
            }
            return new Reflekt<T>();
        }
    }
}