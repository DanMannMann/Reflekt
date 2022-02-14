using System;

namespace Marsman.Reflekt.Test
{
    public class NotNode : ITestInterface
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


		public static NotNode New(Random rand)
		{
			var i = rand.Next(0, 81);
			switch (i)
			{
				case 0: return new NotNode();
				case 1: return new NotNode1();
				case 2: return new NotNode2();
				case 3: return new NotNode3();
				case 4: return new NotNode4();
				case 5: return new NotNode5();
				case 6: return new NotNode6();
				case 7: return new NotNode7();
				case 8: return new NotNode8();
				case 9: return new NotNode9();
				case 10: return new NotNode10();
				case 11: return new NotNode11();
				case 12: return new NotNode12();
				case 13: return new NotNode13();
				case 14: return new NotNode14();
				case 15: return new NotNode15();
				case 16: return new NotNode16();
				case 17: return new NotNode17();
				case 18: return new NotNode18();
				case 19: return new NotNode19();
				case 20: return new NotNode20();
				case 21: return new NotNode21();
				case 22: return new NotNode22();
				case 23: return new NotNode23();
				case 24: return new NotNode24();
				case 25: return new NotNode25();
				case 26: return new NotNode26();
				case 27: return new NotNode27();
				case 28: return new NotNode28();
				case 29: return new NotNode29();
				case 30: return new NotNode30();
				case 31: return new NotNode31();
				case 32: return new NotNode32();
				case 33: return new NotNode33();
				case 34: return new NotNode34();
				case 35: return new NotNode35();
				case 36: return new NotNode36();
				case 37: return new NotNode37();
				case 38: return new NotNode38();
				case 39: return new NotNode39();
				case 40: return new NotNode40();
				case 41: return new NotNode41();
				case 42: return new NotNode42();
				case 43: return new NotNode43();
				case 44: return new NotNode44();
				case 45: return new NotNode45();
				case 46: return new NotNode46();
				case 47: return new NotNode47();
				case 48: return new NotNode48();
				case 49: return new NotNode49();
				case 50: return new NotNode50();
				case 51: return new NotNode51();
				case 52: return new NotNode52();
				case 53: return new NotNode53();
				case 54: return new NotNode54();
				case 55: return new NotNode55();
				case 56: return new NotNode56();
				case 57: return new NotNode57();
				case 58: return new NotNode58();
				case 59: return new NotNode59();
				case 60: return new NotNode60();
				case 61: return new NotNode61();
				case 62: return new NotNode62();
				case 63: return new NotNode63();
				case 64: return new NotNode64();
				case 65: return new NotNode65();
				case 66: return new NotNode66();
				case 67: return new NotNode67();
				case 68: return new NotNode68();
				case 69: return new NotNode69();
				case 70: return new NotNode70();
				case 71: return new NotNode71();
				case 72: return new NotNode72();
				case 73: return new NotNode73();
				case 74: return new NotNode74();
				case 75: return new NotNode75();
				case 76: return new NotNode76();
				case 77: return new NotNode77();
				case 78: return new NotNode78();
				case 79: return new NotNode79();
				case 80: return new NotNode80();
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}

	public class NotNode1 : NotNode { }
	public class NotNode2 : NotNode { }
	public class NotNode3 : NotNode { }
	public class NotNode4 : NotNode { }
	public class NotNode5 : NotNode { }
	public class NotNode6 : NotNode { }
	public class NotNode7 : NotNode { }
	public class NotNode8 : NotNode { }
	public class NotNode9 : NotNode { }
	public class NotNode10 : NotNode { }
	public class NotNode11 : NotNode { }
	public class NotNode12 : NotNode { }
	public class NotNode13 : NotNode { }
	public class NotNode14 : NotNode { }
	public class NotNode15 : NotNode { }
	public class NotNode16 : NotNode { }
	public class NotNode17 : NotNode { }
	public class NotNode18 : NotNode { }
	public class NotNode19 : NotNode { }
	public class NotNode20 : NotNode { }
	public class NotNode21 : NotNode { }
	public class NotNode22 : NotNode { }
	public class NotNode23 : NotNode { }
	public class NotNode24 : NotNode { }
	public class NotNode25 : NotNode { }
	public class NotNode26 : NotNode { }
	public class NotNode27 : NotNode { }
	public class NotNode28 : NotNode { }
	public class NotNode29 : NotNode { }
	public class NotNode30 : NotNode { }
	public class NotNode31 : NotNode { }
	public class NotNode32 : NotNode { }
	public class NotNode33 : NotNode { }
	public class NotNode34 : NotNode { }
	public class NotNode35 : NotNode { }
	public class NotNode36 : NotNode { }
	public class NotNode37 : NotNode { }
	public class NotNode38 : NotNode { }
	public class NotNode39 : NotNode { }
	public class NotNode40 : NotNode { }
	public class NotNode41 : NotNode { }
	public class NotNode42 : NotNode { }
	public class NotNode43 : NotNode { }
	public class NotNode44 : NotNode { }
	public class NotNode45 : NotNode { }
	public class NotNode46 : NotNode { }
	public class NotNode47 : NotNode { }
	public class NotNode48 : NotNode { }
	public class NotNode49 : NotNode { }
	public class NotNode50 : NotNode { }
	public class NotNode51 : NotNode { }
	public class NotNode52 : NotNode { }
	public class NotNode53 : NotNode { }
	public class NotNode54 : NotNode { }
	public class NotNode55 : NotNode { }
	public class NotNode56 : NotNode { }
	public class NotNode57 : NotNode { }
	public class NotNode58 : NotNode { }
	public class NotNode59 : NotNode { }
	public class NotNode60 : NotNode { }
	public class NotNode61 : NotNode { }
	public class NotNode62 : NotNode { }
	public class NotNode63 : NotNode { }
	public class NotNode64 : NotNode { }
	public class NotNode65 : NotNode { }
	public class NotNode66 : NotNode { }
	public class NotNode67 : NotNode { }
	public class NotNode68 : NotNode { }
	public class NotNode69 : NotNode { }
	public class NotNode70 : NotNode { }
	public class NotNode71 : NotNode { }
	public class NotNode72 : NotNode { }
	public class NotNode73 : NotNode { }
	public class NotNode74 : NotNode { }
	public class NotNode75 : NotNode { }
	public class NotNode76 : NotNode { }
	public class NotNode77 : NotNode { }
	public class NotNode78 : NotNode { }
	public class NotNode79 : NotNode { }
	public class NotNode80 : NotNode { }
}
