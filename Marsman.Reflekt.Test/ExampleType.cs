﻿using System.Collections.Generic;

namespace Marsman.Reflekt.Test
{
	class ExampleType
	{
		public string Property1 { get; set; }

		public string Property2 { get; set; }
		public double PropertyNew { get; set; }

		public double Method1() { return 12d; }

		public double Method2(float input) { return 16d; }

		public void Method3() { }

		public string GenericMethod<T>() where T : class { return "Hello world. Type is:" + typeof(T).Name; }

		public string GenericMethod<T>(T input) where T : class { return "Hello world with input " + input + ". Type is:" + typeof(T).Name; }

		public T GenericMethodEx<T>(T input) where T : class { return input; }
		public void GenericVoidMethod<T>(T input, Collector<T> collector) where T : class { collector.List.Add(input); }
		public void GenericVoidMethodEx<T>(T input) where T : class {  }
		public Collector<T> NestedGenericMethod<T>(T input, Collector<T> collector) { collector.List.Add(input); return collector; }
	}

	public class Collector<T>
    {
		public List<T> List { get; } = new List<T>();
    }
}
