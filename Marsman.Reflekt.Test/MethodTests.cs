using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marsman.Reflekt.Test
{
	[TestClass]
	public class MethodTests
	{
		private readonly Type typeKnownAtRuntime = typeof(string);

		[TestMethod]
		public void GetMethodInfo()
		{
			var method = Reflekt<ExampleType>.Method<double>().Parameterless(x => x.Method1);
			var instance = new ExampleType();
			Assert.AreEqual("Method1", method.Name);
			Assert.AreEqual(12d, method.Invoke(instance, null));
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
