using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marsman.Reflekt;
using System.Reflection;

namespace Marsman.Reflekt.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Test();
            object fghgh;
            var arg = "";
            var cons2 = Reflekt<Test<T1>>.Constructor().Generic(arg.GetType()).Parameters<T1, int>((x, y) => new Test<T1>(x, y));
            var inst = cons2.Invoke(new object[] { arg, 5 });
            fghgh = t.Ember().method().Parameterless(x => x.GenericTest<string>);
            fghgh = t.Ember().method().Parameterless(x => x.GenericTest);
            fghgh = t.Ember().method().Parameterless(x => x.VoidVoid);
            fghgh = t.Ember().method<string>().Parameters<string, string>(x => x.String2);
            fghgh = t.Ember().method().Parameters<string, DateTime>(x => x.Void2);
            fghgh = t.Ember().property(x => x.Property);
            fghgh = Reflekt<Test>.Method<string>().Parameterless(x => x.StringVoid);
            var methodName = Reflekt<List<T1>>.PropertyName(x => x.Capacity);
            fghgh = t.Ember().method<T2>().WithTypeArguments(typeof(string),typeof(int)).Parameterless(x => x.GenericTest<T1, T2>);
            var cons = t.Ember().constructor().Parameterless(() => new Test());        }
    }

    class Test<X> { public Test(X firstArg, int secondArg) { } }

    class Test
    {
        public Test() { }

        public Test(string inp) { }

        public void GenericTest() { }

        public void GenericTest<T>() { }

        public K GenericTest<T, K>() { return default(K); }

        public string Property { get; set; }

        public void VoidVoid()
        {

        }

        public void Void1(string p1)
        {

        }

        public void Void2(string p1, string p2)
        {

        }

        public void Void2(string p1, DateTime p2)
        {

        }

        public void Void2(string p1, int p2)
        {

        }

        public void Void3(string p1, string p2, string p3)
        {

        }

        public string StringVoid()
        {
            return string.Empty;
        }

        public string String1(string p1)
        {
            return string.Empty;
        }

        public string String2(string p1, string p2)
        {
            return string.Empty;
        }

        public string String3(string p1, string p2, string p3)
        {
            return string.Empty;
        }
    }
}
