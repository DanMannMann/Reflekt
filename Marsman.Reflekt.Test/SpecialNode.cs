using System;

namespace Marsman.Reflekt.Test
{
    public class SpecialNode : Node, INodeWithSpecialThing
    {
		public string SpecialThing { get; set; } = "special 1";

		public new static SpecialNode New(Random rand)
		{
			var i = rand.Next(0, 81);
			switch (i)
			{
				case 0: return new SpecialNode();
				case 1: return new SpecialNode1();
				case 2: return new SpecialNode2();
				case 3: return new SpecialNode3();
				case 4: return new SpecialNode4();
				case 5: return new SpecialNode5();
				case 6: return new SpecialNode6();
				case 7: return new SpecialNode7();
				case 8: return new SpecialNode8();
				case 9: return new SpecialNode9();
				case 10: return new SpecialNode10();
				case 11: return new SpecialNode11();
				case 12: return new SpecialNode12();
				case 13: return new SpecialNode13();
				case 14: return new SpecialNode14();
				case 15: return new SpecialNode15();
				case 16: return new SpecialNode16();
				case 17: return new SpecialNode17();
				case 18: return new SpecialNode18();
				case 19: return new SpecialNode19();
				case 20: return new SpecialNode20();
				case 21: return new SpecialNode21();
				case 22: return new SpecialNode22();
				case 23: return new SpecialNode23();
				case 24: return new SpecialNode24();
				case 25: return new SpecialNode25();
				case 26: return new SpecialNode26();
				case 27: return new SpecialNode27();
				case 28: return new SpecialNode28();
				case 29: return new SpecialNode29();
				case 30: return new SpecialNode30();
				case 31: return new SpecialNode31();
				case 32: return new SpecialNode32();
				case 33: return new SpecialNode33();
				case 34: return new SpecialNode34();
				case 35: return new SpecialNode35();
				case 36: return new SpecialNode36();
				case 37: return new SpecialNode37();
				case 38: return new SpecialNode38();
				case 39: return new SpecialNode39();
				case 40: return new SpecialNode40();
				case 41: return new SpecialNode41();
				case 42: return new SpecialNode42();
				case 43: return new SpecialNode43();
				case 44: return new SpecialNode44();
				case 45: return new SpecialNode45();
				case 46: return new SpecialNode46();
				case 47: return new SpecialNode47();
				case 48: return new SpecialNode48();
				case 49: return new SpecialNode49();
				case 50: return new SpecialNode50();
				case 51: return new SpecialNode51();
				case 52: return new SpecialNode52();
				case 53: return new SpecialNode53();
				case 54: return new SpecialNode54();
				case 55: return new SpecialNode55();
				case 56: return new SpecialNode56();
				case 57: return new SpecialNode57();
				case 58: return new SpecialNode58();
				case 59: return new SpecialNode59();
				case 60: return new SpecialNode60();
				case 61: return new SpecialNode61();
				case 62: return new SpecialNode62();
				case 63: return new SpecialNode63();
				case 64: return new SpecialNode64();
				case 65: return new SpecialNode65();
				case 66: return new SpecialNode66();
				case 67: return new SpecialNode67();
				case 68: return new SpecialNode68();
				case 69: return new SpecialNode69();
				case 70: return new SpecialNode70();
				case 71: return new SpecialNode71();
				case 72: return new SpecialNode72();
				case 73: return new SpecialNode73();
				case 74: return new SpecialNode74();
				case 75: return new SpecialNode75();
				case 76: return new SpecialNode76();
				case 77: return new SpecialNode77();
				case 78: return new SpecialNode78();
				case 79: return new SpecialNode79();
				case 80: return new SpecialNode80();
				default: throw new ArgumentOutOfRangeException();
			}
		}
	}

	public class SpecialNode1 : SpecialNode { }
	public class SpecialNode2 : SpecialNode { }
	public class SpecialNode3 : SpecialNode { }
	public class SpecialNode4 : SpecialNode { }
	public class SpecialNode5 : SpecialNode { }
	public class SpecialNode6 : SpecialNode { }
	public class SpecialNode7 : SpecialNode { }
	public class SpecialNode8 : SpecialNode { }
	public class SpecialNode9 : SpecialNode { }
	public class SpecialNode10 : SpecialNode { }
	public class SpecialNode11 : SpecialNode { }
	public class SpecialNode12 : SpecialNode { }
	public class SpecialNode13 : SpecialNode { }
	public class SpecialNode14 : SpecialNode { }
	public class SpecialNode15 : SpecialNode { }
	public class SpecialNode16 : SpecialNode { }
	public class SpecialNode17 : SpecialNode { }
	public class SpecialNode18 : SpecialNode { }
	public class SpecialNode19 : SpecialNode { }
	public class SpecialNode20 : SpecialNode { }
	public class SpecialNode21 : SpecialNode { }
	public class SpecialNode22 : SpecialNode { }
	public class SpecialNode23 : SpecialNode { }
	public class SpecialNode24 : SpecialNode { }
	public class SpecialNode25 : SpecialNode { }
	public class SpecialNode26 : SpecialNode { }
	public class SpecialNode27 : SpecialNode { }
	public class SpecialNode28 : SpecialNode { }
	public class SpecialNode29 : SpecialNode { }
	public class SpecialNode30 : SpecialNode { }
	public class SpecialNode31 : SpecialNode { }
	public class SpecialNode32 : SpecialNode { }
	public class SpecialNode33 : SpecialNode { }
	public class SpecialNode34 : SpecialNode { }
	public class SpecialNode35 : SpecialNode { }
	public class SpecialNode36 : SpecialNode { }
	public class SpecialNode37 : SpecialNode { }
	public class SpecialNode38 : SpecialNode { }
	public class SpecialNode39 : SpecialNode { }
	public class SpecialNode40 : SpecialNode { }
	public class SpecialNode41 : SpecialNode { }
	public class SpecialNode42 : SpecialNode { }
	public class SpecialNode43 : SpecialNode { }
	public class SpecialNode44 : SpecialNode { }
	public class SpecialNode45 : SpecialNode { }
	public class SpecialNode46 : SpecialNode { }
	public class SpecialNode47 : SpecialNode { }
	public class SpecialNode48 : SpecialNode { }
	public class SpecialNode49 : SpecialNode { }
	public class SpecialNode50 : SpecialNode { }
	public class SpecialNode51 : SpecialNode { }
	public class SpecialNode52 : SpecialNode { }
	public class SpecialNode53 : SpecialNode { }
	public class SpecialNode54 : SpecialNode { }
	public class SpecialNode55 : SpecialNode { }
	public class SpecialNode56 : SpecialNode { }
	public class SpecialNode57 : SpecialNode { }
	public class SpecialNode58 : SpecialNode { }
	public class SpecialNode59 : SpecialNode { }
	public class SpecialNode60 : SpecialNode { }
	public class SpecialNode61 : SpecialNode { }
	public class SpecialNode62 : SpecialNode { }
	public class SpecialNode63 : SpecialNode { }
	public class SpecialNode64 : SpecialNode { }
	public class SpecialNode65 : SpecialNode { }
	public class SpecialNode66 : SpecialNode { }
	public class SpecialNode67 : SpecialNode { }
	public class SpecialNode68 : SpecialNode { }
	public class SpecialNode69 : SpecialNode { }
	public class SpecialNode70 : SpecialNode { }
	public class SpecialNode71 : SpecialNode { }
	public class SpecialNode72 : SpecialNode { }
	public class SpecialNode73 : SpecialNode { }
	public class SpecialNode74 : SpecialNode { }
	public class SpecialNode75 : SpecialNode { }
	public class SpecialNode76 : SpecialNode { }
	public class SpecialNode77 : SpecialNode { }
	public class SpecialNode78 : SpecialNode { }
	public class SpecialNode79 : SpecialNode { }
	public class SpecialNode80 : SpecialNode { }
}
