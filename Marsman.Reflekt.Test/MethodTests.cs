using System;
using System.Linq;
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
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method<T1>().AsDelegate(typeof(string)).Parameters<T1>(x => x.GenericMethodEx);
			var delegateResult = @delegate.Invoke(instance, "test input");
			Assert.AreEqual("test input", delegateResult);
		}

		[TestMethod]
		public void GetDelegateNonGeneric()
		{
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method<double>().AsDelegate().Parameters<float>(x => x.Method2);
			var delegateResult = @delegate.Invoke(instance, 5f);
			Assert.AreEqual(16d, delegateResult);
		}

		[TestMethod]
		public void GetDelegateNestedGeneric()
		{
			var collector = new Collector<string>();
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method<Collector<T1>>().AsDelegate(typeof(string)).Parameters<T1, Collector<T1>>(x => x.NestedGenericMethod);
			var delegateResult = @delegate.Invoke(instance, "test input", collector);
			Assert.AreSame(collector, delegateResult);
		}

		[TestMethod]
		public void GetDelegateNestedGenericFactory()
		{
			var collector = new Collector<string>();
			var instance = new ExampleType();
			var delegateFactory = Reflekt<ExampleType>.Method<Collector<T1>>().AsDelegateFactory().Parameters<T1, Collector<T1>>(x => x.NestedGenericMethod);
			var @delegate = delegateFactory.CreateWithTypeArguments(typeof(string));
			var delegateResult = @delegate.Invoke(instance, "test input", collector);
			Assert.AreSame(collector, delegateResult);
			Assert.IsTrue(collector.List.Contains("test input"));

			var intCollector = new Collector<int>();
			@delegate = delegateFactory.CreateWithTypeArguments(typeof(int));
			delegateResult = @delegate.Invoke(instance, 69, intCollector);
			Assert.AreSame(intCollector, delegateResult);
			Assert.IsTrue(intCollector.List.Contains(69));

			var otherInstance = new ExampleType();
			var exCollector = new Collector<ExampleType>();
			@delegate = delegateFactory.CreateWithTypeArguments(typeof(ExampleType));
			delegateResult = @delegate.Invoke(instance, otherInstance, exCollector);
			Assert.AreSame(exCollector, delegateResult);
			Assert.IsTrue(exCollector.List.Contains(otherInstance));
		}

		[TestMethod]
		public void GetDelegateNonGenericFactory()
		{
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method<double>().AsDelegateFactory().Parameters<float>(x => x.Method2);
			var delegateResult = @delegate.CreateWithTypeArguments().Invoke(instance, 5f);
			Assert.AreEqual(16d, delegateResult);
		}

		[TestMethod]
		public void GetVoidDelegate()
		{
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method().AsDelegate(typeof(string)).Parameters<T1>(x => x.GenericVoidMethodEx);
			@delegate.Invoke(instance, "test input");
		}

		[TestMethod]
		public void GetVoidDelegateNestedGeneric()
		{
			var collector = new Collector<string>();
			var instance = new ExampleType();
			var @delegate = Reflekt<ExampleType>.Method().AsDelegate(typeof(string)).Parameters<T1,Collector<T1>>(x => x.GenericVoidMethod);
			@delegate.Invoke(instance, "test input", collector);
			Assert.AreEqual("test input", collector.List.Single());
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
			var genericInfo = Reflekt<ExampleType>.Method<string>().AsGenericDefinition().Parameterless(x => x.GenericMethod<T1>);
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
			Assert.AreEqual("Hello world with input test. Type is:String", genericInfo.Invoke(instance, new object[] { "test" }));
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
