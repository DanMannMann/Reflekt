using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Marsman.Reflekt.Test
{
	[TestClass]
	public class PropertyTests
	{
		[TestMethod]
		public void GetPropertyName()
		{
			var propertyName = Reflekt<ExampleType>.PropertyName(x => x.Property1);
			Assert.AreEqual("Property1", propertyName);
		}

		[TestMethod]
		public void GetPropertyInfo()
		{
			var property = Reflekt<ExampleType>.Property(x => x.Property1);
			var instance = new ExampleType();
			instance.Property1 = "test value";
			Assert.AreEqual("Property1", property.Name);
			Assert.AreEqual("test value", property.GetValue(instance));
		}
	}
}
