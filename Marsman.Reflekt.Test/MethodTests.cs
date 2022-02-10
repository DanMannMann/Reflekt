using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marsman.Reflekt.Test
{
	[TestClass]
	public class MethodTests
	{
		private readonly Type typeKnownAtRuntime = typeof(string);

		[TestMethod]
		public void GetDelegate()
		{
			var method = Reflekt<ExampleType>.Method<T1>().WithTypeArguments(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethodEx);
			var @delegate = Reflekt<ExampleType>.Method<T1>().WithTypeArguments(typeKnownAtRuntime).AsDelegate().Parameters<T1>(x => x.GenericMethodEx);
			var instance = new ExampleType();
			var sw1 = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < 20000000; i++)
			{
				var methodResult = method.Invoke(instance, new object[] { "test input" });
			}
			sw1.Stop();
			Console.WriteLine(sw1);
			var sw2 = System.Diagnostics.Stopwatch.StartNew();
			for (var i = 0; i < 20000000; i++)
			{
				var delegateResult = @delegate.Invoke(instance, "test input");
			}
			sw2.Stop();
			Console.WriteLine(sw2);
			Console.ReadLine();
		}

		[TestMethod]
		public void GetMethodInfo()
		{
			var method = Reflekt<ExampleType>.Method<double>().Parameterless(x => x.Method1);
			var instance = new ExampleType();
			Assert.AreEqual("Method1", method.Name);
			Assert.AreEqual(12d, method.Invoke(instance, null));
		}

		[TestMethod]
		public void GetMethodInfoWithParameters()
		{
			var method = Reflekt<ExampleType>.Method<double>().Parameters<float>(x => x.Method2);
			var instance = new ExampleType();
			Assert.AreEqual("Method2", method.Name);
			Assert.AreEqual(16d, method.Invoke(instance, new object[] { 4f }));
		}

		[TestMethod]
		public void GetMethodInfoWithParametersUsingShorthand()
		{
			var method = Reflekt<ExampleType>.MethodInfo<float, double>(x => x.Method2);
			var instance = new ExampleType();
			Assert.AreEqual("Method2", method.Name);
			Assert.AreEqual(16d, method.Invoke(instance, new object[] { 4f }));
		}

		[TestMethod]
		public void GetMethodInfoWithNoReturnType()
		{
			var method = Reflekt<ExampleType>.Method().Parameterless(x => x.Method3);
			var instance = new ExampleType();
			Assert.AreEqual("Method3", method.Name);
		}

		[TestMethod]
		public void GetConstructedGenericMethodInfo()
		{
			var genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);
			var instance = new ExampleType();
			Assert.AreEqual("GenericMethod", genericInfo.Name);
			Assert.AreEqual("Hello world. Type is:String", genericInfo.Invoke(instance, null));
		}

		[TestMethod]
		public void GetUnconstructedGenericMethodInfo()
		{
			var genericInfo = Reflekt<ExampleType>.Method<string>().GenericDefinition().Parameterless(x => x.GenericMethod<T1>);
			var instance = new ExampleType();
			Assert.AreEqual("GenericMethod", genericInfo.Name);
			Assert.ThrowsException<InvalidOperationException>(() => genericInfo.Invoke(instance, null));
			var constructed = genericInfo.MakeGenericMethod(typeKnownAtRuntime);
			Assert.AreEqual("Hello world. Type is:String", constructed.Invoke(instance, null));
		}

		[TestMethod]
		public void GetGenericMethodInfo()
		{
			var genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameterless(x => x.GenericMethod<T1>);
			var instance = new ExampleType();
			Assert.AreEqual("GenericMethod", genericInfo.Name);
			Assert.AreEqual("Hello world. Type is:String", genericInfo.Invoke(instance, null));
		}

		[TestMethod]
		public void GetGenericMethodOverloadInfo()
		{
			var genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime).Parameters<T1>(x => x.GenericMethod<T1>);
			var instance = new ExampleType();
			Assert.AreEqual("GenericMethod", genericInfo.Name);
			Assert.AreEqual("Hello world from the overload. Type is:String", genericInfo.Invoke(instance, new object[] { "test" }));
		}

		[TestMethod]
		public void AttemptToGetGenericMethodWithTheWrongNumberOfTypeParameters()
		{
			Assert.ThrowsException<InvalidOperationException>(() =>
			{
				var genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeKnownAtRuntime, typeof(int)).Parameters<T1>(x => x.GenericMethod<T1>);
			});
		}

		[TestMethod]
		public void AttemptToGetGenericMethodWhichViolatesATypeConstraint()
		{
			Assert.ThrowsException<ArgumentException>(() =>
			{
				var genericInfo = Reflekt<ExampleType>.Method<string>().WithTypeArguments(typeof(int)).Parameters<T1>(x => x.GenericMethod<T1>);
			});
		}
	}
}
