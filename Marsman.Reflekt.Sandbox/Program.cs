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
            List<string> testInstance = new List<string>();
            PropertyInfo countProperty = testInstance.Reflekt().property(x => x.Count);

            var typeKnownAtRuntime = typeof(string);

            //On type            //Get the          //For this property
            //ExampleType        //prop name   
            string propertyName = Reflekt<ExampleType>.PropertyName(x => x.Property1);

            //On type            //Get the      //For this property
            //ExampleType        //info   
            PropertyInfo propertyInfo = Reflekt<ExampleType>.Property(x => x.Property2);


            //On type             //get method     //and with           //Select the member
            //List<string>        //with string    //1 param of
            //return type    //type int
            MethodInfo methodInfo = Reflekt<List<string>>.Method<string>().Parameters<int>(x => x.ElementAt);

            //On type           //get method     //with a generic type                 //and with         //Select the member
            //ExampleType       //with string    //argument known only                 //no parameters   
            //return type,   //at runtime
            MethodInfo genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);

            //On type GenericType<>  //get the ctr //for a concrete type       //Where the ctr has 2            //Select the
            //using the runtime type    //params, int and                //constructor
            //args                      //string
            ConstructorInfo genericTypeConstructorInfo = Reflekt<GenericType<T1>>.Constructor().WithTypeArguments(typeKnownAtRuntime).Parameters<int, string>((x, y) => new GenericType<T1>(x, y));

        }
    }

    class ExampleType
    {
        public string Property1{ get; set; }

        public string Property2 { get; set; }

        public string GenericMethod<T>() where T : class { return "Hello world. Type is:" + typeof(T).Name; }
    }

    class GenericType<T>
    {
        public GenericType(int param1, string param2) { /*...*/ }
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
