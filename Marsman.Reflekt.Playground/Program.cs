using System;

namespace Marsman.Reflekt.Playground
{
    class Program
	{
		private static readonly Type typeKnownAtRuntime = typeof(string);
		static void Main(string[] args)
		{
			var factory = Reflekt<ExampleType>.Method<T1>().AsDelegateFactory().Parameters<T1>(x => x.GenericMethodEx);
			var stringDelegate = factory.CreateWithTypeArguments(typeof(string));
			var intDelegate = factory.CreateWithTypeArguments(typeof(int));
			var uriDelegate = factory.CreateWithTypeArguments(typeof(Uri));

			var method = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethod);
			var methodWithGenericReturnType = Reflekt<ExampleType>.Method<T1>().WithTypeArguments(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethodEx);
			var @delegate = Reflekt<ExampleType>.Method<string>().AsDelegate(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethod);
			var delegateWithGenericReturnType = Reflekt<ExampleType>.Method<T1>().AsDelegate(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethodEx);
			var instance = new ExampleType();
			var sw = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var methodResult = instance.GenericMethodEx("string");
			}
			sw.Stop();
			Console.WriteLine($"Normal call: 2m runs in {sw.ElapsedMilliseconds}ms"); 
			sw = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var methodResult = instance.GenericMethod("string");
			}
			sw.Stop();
			Console.WriteLine($"Normal call: 2m runs in {sw.ElapsedMilliseconds}ms");
			var sw1 = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var methodResult = method.Invoke(instance, new object[] { "test input" });
			}
			sw1.Stop();
			Console.WriteLine($"MethodInfo.Invoke: 2m runs in {sw1.ElapsedMilliseconds}ms");
			var sw2 = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var methodResult = methodWithGenericReturnType.Invoke(instance, new object[] { "test input" });
			}
			sw2.Stop();
			Console.WriteLine($"MethodInfo.Invoke: 2m runs in {sw2.ElapsedMilliseconds}ms");
			var sw3 = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var delegateResult = @delegate.Invoke(instance, "test input");
			}
			sw3.Stop();
			Console.WriteLine($"Delegate.Invoke: 2m runs in {sw3.ElapsedMilliseconds}ms");
			var sw4 = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var delegateResult = delegateWithGenericReturnType.Invoke(instance, "test input");
			}
			sw4.Stop(); 
			Console.WriteLine($"Delegate.Invoke: 2m runs in {sw4.ElapsedMilliseconds}ms");
			Console.ReadLine();
		}
	}

	class ExampleType
	{
		public string Property1 { get; set; }

		public string Property2 { get; set; }

		public double Method1() { return 12d; }

		public double Method2(float input) { return 16d; }

		public void Method3() { }

		public string GenericMethod<T>() where T : class { var g = Guid.NewGuid(); var i = g.ToByteArray(); long f = i[5] * i[10]; return "a const string"; }

		public string GenericMethod<T>(T input) where T : class { var g = Guid.NewGuid(); var i = g.ToByteArray(); long f = i[5] * i[10]; return "a const string"; }

		public T GenericMethodEx<T>(T input) where T : class { var g = Guid.NewGuid(); var i = g.ToByteArray(); long f = i[5] * i[10]; return input; }
	}
}
