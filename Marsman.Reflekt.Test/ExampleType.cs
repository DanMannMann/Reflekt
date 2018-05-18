namespace Marsman.Reflekt.Test
{
	class ExampleType
	{
		public string Property1 { get; set; }

		public string Property2 { get; set; }

		public double Method1() { return 12d; }

		public double Method2(float input) { return 16d; }

		public void Method3() { }

		public string GenericMethod<T>() where T : class { return "Hello world. Type is:" + typeof(T).Name; }

		public string GenericMethod<T>(T input) where T : class { return "Hello world from the overload. Type is:" + typeof(T).Name; }
	}
}
