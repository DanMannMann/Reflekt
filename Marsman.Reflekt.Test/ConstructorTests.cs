using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marsman.Reflekt.Test
{
	[TestClass]
	public class ConstructorTests
	{
		private readonly Type typeKnownAtRuntime = typeof(string);

		[TestMethod]
		public void GetGenericConstructorWithNoParameters()
		{
			SuccessfulTest(new Type[] { typeKnownAtRuntime }, 
						   x => x.Parameterless(() => new GenericType<T1>()), 
						   "none");
		}

		[TestMethod]
		public void GetGenericConstructorWithIntAndStringParameters()
		{
			SuccessfulTest(new Type[] { typeKnownAtRuntime }, 
						   t => t.Parameters<int, string>((x, y) => new GenericType<T1>(x, y)), 
						   "int, string", 
						   new object[] { 1, "test" });
		}

		[TestMethod]
		public void GetGenericConstructorWithDoubleAndStringParameters()
		{
			SuccessfulTest(new Type[] { typeKnownAtRuntime }, 
						   t => t.Parameters<double, string>((x, y) => new GenericType<T1>(x, y)), 
						   "double, string", 
						   new object[] { 1d, "test" });
		}

		[TestMethod]
		public void GetGenericConstructorWithGenericAndStringParameters()
		{
			SuccessfulTest(new Type[] { typeKnownAtRuntime }, 
						   t => t.Parameters<T1, string>((x, y) => new GenericType<T1>(x, y)), 
						   "String, string", 
						   new object[] { "1", "test" });
		}

		[TestMethod]
		public void AttemptToGetGenericConstructorWithTheWrongNumberOfTypeParameters()
		{
			FailureTest<InvalidOperationException>(typeKnownAtRuntime, typeof(Guid));
		}

		[TestMethod]
		public void AttemptToGetGenericConstructorWithNoTypeParameters()
		{
			FailureTest<InvalidOperationException>();
		}

		[TestMethod]
		public void AttemptToGetGenericConstructorWhichViolatesATypeConstraint()
		{
			FailureTest<ArgumentException>(typeof(int));
		}

		private void SuccessfulTest(Type[] typeArgs, Func<ConstructorSelectorReflekt<GenericType<T1>>, ConstructorInfo> operation, string expectedParameterReport, object[] parameters = null)
		{
			var genericTypeConstructorInfo = operation(Reflekt<GenericType<T1>>.Constructor().WithTypeArguments(typeArgs ?? new Type[0]));

			var instance = (GenericType<string>)genericTypeConstructorInfo.Invoke(parameters);
			Assert.AreEqual(".ctor", genericTypeConstructorInfo.Name);
			Assert.AreEqual($"Type is String, constructor used with parameters {expectedParameterReport}", instance.Report());
		}

		private void FailureTest<T>(params Type[] typeArgs) where T : Exception
		{
			Assert.ThrowsException<T>(() =>
			{
				var genericTypeConstructorInfo = Reflekt<GenericType<T1>>.Constructor()
																		 .WithTypeArguments(typeArgs)
																		 .Parameterless(() => new GenericType<T1>());
			});
		}
	}
}
