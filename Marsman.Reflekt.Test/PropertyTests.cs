using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marsman.Reflekt.Test
{
    [TestClass]
	public class PropertyTests
	{
		[TestMethod]
		public void GetPropertySetterDelegate()
		{
			var instance = new ExampleType();
			instance.Property1 = "old value";
			var propertyDelegate = Reflekt<ExampleType>.PropertySetterDelegate(x => x.Property1);
			propertyDelegate(instance, "new value");
			Assert.AreEqual("new value", instance.Property1);
		}

		[TestMethod]
		public void GetPropertyGetterDelegate()
		{
			var instance = new ExampleType();
			instance.Property1 = "old value";
			var propertyDelegate = Reflekt<ExampleType>.PropertyGetterDelegate(x => x.Property1);
			var result = propertyDelegate(instance);
			Assert.AreEqual("old value", result);
		}

		[TestMethod]
		public void GetPropertyName()
		{
			var propertyName = Reflekt<ExampleType>.PropertyName(x => x.Property1);
			Assert.AreEqual("Property1", propertyName);
		}

		[TestMethod]
		public void GetPropertyInfo()
		{
			Expression<Func<ExampleType, string>> expression = x => x.Property1;
			var property = Reflekt<ExampleType>.Property(x => x.Property1);
			var instance = new ExampleType();
			instance.Property1 = "test value";
			Assert.AreEqual("Property1", property.Name);
			Assert.AreEqual("test value", property.GetValue(instance));
		}

		[TestMethod]
		public void GetPropertyInfoTyped()
		{
			Expression<Func<ExampleType, string>> expression = x => x.Property1;
			var property = Reflekt<ExampleType>.Property(expression);
			var instance = new ExampleType();
			instance.Property1 = "test value";
			Assert.AreEqual("Property1", property.Name);
			Assert.AreEqual("test value", property.GetValue(instance));
		}

		[TestMethod]
		public void GetPropertyInfoUntyped()
		{
			Expression<Func<ExampleType, object>> expression = x => x.Property1;
			var property = Reflekt<ExampleType>.Property(expression);
			var instance = new ExampleType();
			instance.Property1 = "test value";
			Assert.AreEqual("Property1", property.Name);
			Assert.AreEqual("test value", property.GetValue(instance));
		}
	}
}
