using System;
using System.Collections.Generic;
using System.Linq;

namespace Marsman.Reflekt.Test
{
    public class HugeRandomTreeFactory
    {
		private NotNode root;
		private List<(ITestInterface, int)> objects;
		private int maxDepth;
		private int numIterations;

		public List<(ITestInterface Object, int Depth)> Objects => objects;
		public object Tree => root;
		public int MaxDepth => maxDepth;
		public int NumberOfIterations => numIterations;
		public IEnumerable<Type> TypesUsed => objects.Select(x => x.Item1.GetType()).Distinct().ToList();

		public HugeRandomTreeFactory(double sizeMultipler = 1d)
		{
			var rand = new Random();
			objects = new List<(ITestInterface, int)>();
			maxDepth = rand.Next(50, 151);
			numIterations = (int)((double)rand.Next(50000, 150000) * sizeMultipler);
			root = new NotNode();

			var currentDepth = 0;
			ITestInterface currentNode = root;

			for (var i = 0; i < numIterations; i++)
			{
				if (currentNode == null)
				{
					var randNode = objects[rand.Next(0, objects.Count)];
					currentNode = randNode.Item1;
					currentDepth = randNode.Item2;
				}
				var newNode = SetRandomProperty(rand, currentNode);
				if (newNode != null)
				{
					objects.Add((newNode, currentDepth + 1));
					if (rand.Next(0, 1) == 0)
					{
						currentNode = newNode;
						currentDepth++;
						if (currentDepth > maxDepth) currentNode = null;
					}
				}
				if (rand.Next(0, 10) == 0) currentNode = null;
			}
		}

		public Node GetRandomClassNode(Random rand)
		{
			var type = rand.Next(0, 3);
			switch (type)
			{
				case 0:
				case 1:
					return Node.New(rand);
				case 2:
					return SpecialNode.New(rand);
				default:
					throw new InvalidOperationException();
			}
		}

		public object GetRandomNonNode(Random rand)
		{
			var type = rand.Next(0, 6);
			switch (type)
			{
				case 0:
					return Guid.NewGuid().ToString();
				case 1:
					return rand.Next(10, 100000);
				case 2:
					return new Uri($"https://www.{Guid.NewGuid()}.com/ass-hats");
				case 3:
					return new List<string>
					{
						"Item", "Item 2", "Item 3"
					};
				default: // two thirds NotNode
					return NotNode.New(rand);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>newly created node, or null if a scalar value was created</returns>
		public ITestInterface SetRandomProperty(Random rand, ITestInterface target)
		{
			if (target.ClassNode != null && target.ClassNode2 != null && target.ClassNode3 != null &&
				target.InterfaceNode != null && target.InterfaceNode2 != null && target.InterfaceNode3 != null &&
				target.ObjectNode != null && target.ObjectNode2 != null && target.ObjectNode3 != null)
            {
				// object is saturated, just return null
				return null;
            }

			var prop = rand.Next(0, 9);
			while (true) // until it hits a null property
            {
				switch (prop)
				{
					case 0 when (target.ClassNode == null):
						return target.ClassNode = GetRandomClassNode(rand);
					case 1 when (target.ClassNode2 == null):
						return target.ClassNode2 = GetRandomClassNode(rand);
					case 2 when (target.ClassNode3 == null):
						return target.ClassNode3 = GetRandomClassNode(rand);
					case 3 when (target.InterfaceNode == null):
						return target.InterfaceNode = GetRandomClassNode(rand);
					case 4 when (target.InterfaceNode2 == null):
						return target.InterfaceNode2 = GetRandomClassNode(rand);
					case 5 when (target.InterfaceNode3 == null):
						return target.InterfaceNode3 = GetRandomClassNode(rand);
					case 6 when (target.ObjectNode == null):
						target.ObjectNode = rand.Next(0, 3) == 0 ? GetRandomClassNode(rand) : GetRandomNonNode(rand);
						return target.ObjectNode is ITestInterface t ? t : null;
					case 7 when (target.ObjectNode2 == null):
						target.ObjectNode2 = rand.Next(0, 3) == 0 ? GetRandomClassNode(rand) : GetRandomNonNode(rand);
						return target.ObjectNode2 is ITestInterface t2 ? t2 : null;
					case 8 when (target.ObjectNode3 == null):
						target.ObjectNode3 = rand.Next(0, 3) == 0 ? GetRandomClassNode(rand) : GetRandomNonNode(rand);
						return target.ObjectNode3 is ITestInterface t3 ? t3 : null;
				}
				prop = rand.Next(0, 9);
			}

		}
	}
}
