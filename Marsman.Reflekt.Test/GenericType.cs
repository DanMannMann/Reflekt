namespace Marsman.Reflekt.Test
{
	class GenericType<T> where T : class
	{
		private string[] _ctrParams;

		public string Report()
		{
			return $"Type is {typeof(T).Name}, constructor used with parameters {string.Join(", ", _ctrParams)}";
		}

		public GenericType()
		{
			_ctrParams = new string[] { "none" };
		}

		public GenericType(int param1, string param2)
		{
			_ctrParams = new string[] { "int", "string" };
		}

		public GenericType(double param1, string param2)
		{
			_ctrParams = new string[] { "double", "string" };
		}

		public GenericType(T param1, string param2)
		{
			_ctrParams = new string[] { typeof(T).Name, "string" };
		}
	}
}
