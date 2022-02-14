using System;

namespace Marsman.Reflekt.Test
{
    public class Node : ITreeNode, ITestInterface
	{
		public ITreeNode InterfaceNode { get; set; }
		public Node ClassNode { get; set; }
		public object ObjectNode { get; set; }
		public ITreeNode InterfaceNode2 { get; set; }
		public Node ClassNode2 { get; set; }
		public object ObjectNode2 { get; set; }
		public ITreeNode InterfaceNode3 { get; set; }
		public Node ClassNode3 { get; set; }
		public object ObjectNode3 { get; set; }

		public static Node New(Random rand)
        {
			var i = rand.Next(0, 81);
            switch (i)
			{
				case 0: return new Node();
				case 1: return new Node1();
				case 2: return new Node2();
				case 3: return new Node3();
				case 4: return new Node4();
				case 5: return new Node5();
				case 6: return new Node6();
				case 7: return new Node7();
				case 8: return new Node8();
				case 9: return new Node9();
				case 10: return new Node10();
				case 11: return new Node11();
				case 12: return new Node12();
				case 13: return new Node13();
				case 14: return new Node14();
				case 15: return new Node15();
				case 16: return new Node16();
				case 17: return new Node17();
				case 18: return new Node18();
				case 19: return new Node19();
				case 20: return new Node20();
				case 21: return new Node21();
				case 22: return new Node22();
				case 23: return new Node23();
				case 24: return new Node24();
				case 25: return new Node25();
				case 26: return new Node26();
				case 27: return new Node27();
				case 28: return new Node28();
				case 29: return new Node29();
				case 30: return new Node30();
				case 31: return new Node31();
				case 32: return new Node32();
				case 33: return new Node33();
				case 34: return new Node34();
				case 35: return new Node35();
				case 36: return new Node36();
				case 37: return new Node37();
				case 38: return new Node38();
				case 39: return new Node39();
				case 40: return new Node40();
				case 41: return new Node41();
				case 42: return new Node42();
				case 43: return new Node43();
				case 44: return new Node44();
				case 45: return new Node45();
				case 46: return new Node46();
				case 47: return new Node47();
				case 48: return new Node48();
				case 49: return new Node49();
				case 50: return new Node50();
				case 51: return new Node51();
				case 52: return new Node52();
				case 53: return new Node53();
				case 54: return new Node54();
				case 55: return new Node55();
				case 56: return new Node56();
				case 57: return new Node57();
				case 58: return new Node58();
				case 59: return new Node59();
				case 60: return new Node60();
				case 61: return new Node61();
				case 62: return new Node62();
				case 63: return new Node63();
				case 64: return new Node64();
				case 65: return new Node65();
				case 66: return new Node66();
				case 67: return new Node67();
				case 68: return new Node68();
				case 69: return new Node69();
				case 70: return new Node70();
				case 71: return new Node71();
				case 72: return new Node72();
				case 73: return new Node73();
				case 74: return new Node74();
				case 75: return new Node75();
				case 76: return new Node76();
				case 77: return new Node77();
				case 78: return new Node78();
				case 79: return new Node79();
				case 80: return new Node80();
				default: throw new ArgumentOutOfRangeException();
			}
        }
	}

	public class Node1 : Node { }
	public class Node2 : Node { }
	public class Node3 : Node { }
	public class Node4 : Node { }
	public class Node5 : Node { }
	public class Node6 : Node { }
	public class Node7 : Node { }
	public class Node8 : Node { }
	public class Node9 : Node { }
	public class Node10 : Node { }
	public class Node11 : Node { }
	public class Node12 : Node { }
	public class Node13 : Node { }
	public class Node14 : Node { }
	public class Node15 : Node { }
	public class Node16 : Node { }
	public class Node17 : Node { }
	public class Node18 : Node { }
	public class Node19 : Node { }
	public class Node20 : Node { }
	public class Node21 : Node { }
	public class Node22 : Node { }
	public class Node23 : Node { }
	public class Node24 : Node { }
	public class Node25 : Node { }
	public class Node26 : Node { }
	public class Node27 : Node { }
	public class Node28 : Node { }
	public class Node29 : Node { }
	public class Node30 : Node { }
	public class Node31 : Node { }
	public class Node32 : Node { }
	public class Node33 : Node { }
	public class Node34 : Node { }
	public class Node35 : Node { }
	public class Node36 : Node { }
	public class Node37 : Node { }
	public class Node38 : Node { }
	public class Node39 : Node { }
	public class Node40 : Node { }
	public class Node41 : Node { }
	public class Node42 : Node { }
	public class Node43 : Node { }
	public class Node44 : Node { }
	public class Node45 : Node { }
	public class Node46 : Node { }
	public class Node47 : Node { }
	public class Node48 : Node { }
	public class Node49 : Node { }
	public class Node50 : Node { }
	public class Node51 : Node { }
	public class Node52 : Node { }
	public class Node53 : Node { }
	public class Node54 : Node { }
	public class Node55 : Node { }
	public class Node56 : Node { }
	public class Node57 : Node { }
	public class Node58 : Node { }
	public class Node59 : Node { }
	public class Node60 : Node { }
	public class Node61 : Node { }
	public class Node62 : Node { }
	public class Node63 : Node { }
	public class Node64 : Node { }
	public class Node65 : Node { }
	public class Node66 : Node { }
	public class Node67 : Node { }
	public class Node68 : Node { }
	public class Node69 : Node { }
	public class Node70 : Node { }
	public class Node71 : Node { }
	public class Node72 : Node { }
	public class Node73 : Node { }
	public class Node74 : Node { }
	public class Node75 : Node { }
	public class Node76 : Node { }
	public class Node77 : Node { }
	public class Node78 : Node { }
	public class Node79 : Node { }
	public class Node80 : Node { }
}
