using Ardalis.SmartEnum;

namespace StryktipsetCore.Contract
{
    public class Enums
    {
        public class Tecken : SmartEnum<Tecken>
        {
            public static readonly Tecken Ett = new Tecken(1, "1");
            public static readonly Tecken Kryss = new Tecken(2, "X");
            public static readonly Tecken Två = new Tecken(3, "2");

            private Tecken(int value, string displayName) : base(displayName, value) { }
        }

        public enum Värde
        {
            Oddsfavoritskap = 8,
            Spelvärde = 11,
            Komboslump,
            Slump
        }

        public class MSystem : SmartEnum<MSystem>
        {
            public static readonly MSystem M1 = new MSystem(1, "M1");
            public static readonly MSystem M2 = new MSystem(2, "M2");
            public static readonly MSystem M4 = new MSystem(4, "M4");
            public static readonly MSystem M8 = new MSystem(8, "M8");
            public static readonly MSystem M16 = new MSystem(16, "M16");
            public static readonly MSystem M32 = new MSystem(32, "M32");
            public static readonly MSystem M64 = new MSystem(64, "M64");
            public static readonly MSystem M128 = new MSystem(128, "M128");
            public static readonly MSystem M256 = new MSystem(256, "M256");
            public static readonly MSystem M512 = new MSystem(512, "M512");
            public static readonly MSystem M1024 = new MSystem(1024, "M1024");
            public static readonly MSystem M2048 = new MSystem(2048, "M2048");
            public static readonly MSystem M4096 = new MSystem(4096, "M4096");
            public static readonly MSystem M8192 = new MSystem(8192, "M8192");
            public static readonly MSystem M3 = new MSystem(3, "M3");
            public static readonly MSystem M6 = new MSystem(6, "M6");
            public static readonly MSystem M12 = new MSystem(12, "M12");
            public static readonly MSystem M24 = new MSystem(24, "M24");
            public static readonly MSystem M48 = new MSystem(48, "M48");
            public static readonly MSystem M96 = new MSystem(96, "M96");
            public static readonly MSystem M192 = new MSystem(192, "M192");
            public static readonly MSystem M384 = new MSystem(384, "M384");
            public static readonly MSystem M768 = new MSystem(768, "M768");
            public static readonly MSystem M1536 = new MSystem(1536, "M1536");
            public static readonly MSystem M3072 = new MSystem(3072, "M3072");
            public static readonly MSystem M6144 = new MSystem(6144, "M6144");
            public static readonly MSystem M9 = new MSystem(9, "M9");
            public static readonly MSystem M18 = new MSystem(18, "M18");
            public static readonly MSystem M36 = new MSystem(36, "M36");
            public static readonly MSystem M72 = new MSystem(72, "M72");
            public static readonly MSystem M144 = new MSystem(144, "M144");
            public static readonly MSystem M288 = new MSystem(288, "M288");
            public static readonly MSystem M576 = new MSystem(576, "M576");
            public static readonly MSystem M1152 = new MSystem(1152, "M1152");
            public static readonly MSystem M2304 = new MSystem(2304, "M2304");
            public static readonly MSystem M4608 = new MSystem(4608, "M4608");
            public static readonly MSystem M9216 = new MSystem(9216, "M9216");
            public static readonly MSystem M27 = new MSystem(27, "M27");
            public static readonly MSystem M54 = new MSystem(54, "M54");
            public static readonly MSystem M108 = new MSystem(108, "M108");
            public static readonly MSystem M216 = new MSystem(216, "M216");
            public static readonly MSystem M432 = new MSystem(432, "M432");
            public static readonly MSystem M864 = new MSystem(864, "M864");
            public static readonly MSystem M1728 = new MSystem(1728, "M1728");
            public static readonly MSystem M3456 = new MSystem(3456, "M3456");
            public static readonly MSystem M6912 = new MSystem(6912, "M6912");
            public static readonly MSystem M81 = new MSystem(81, "M81");
            public static readonly MSystem M162 = new MSystem(162, "M162");
            public static readonly MSystem M324 = new MSystem(324, "M324");
            public static readonly MSystem M648 = new MSystem(648, "M648");
            public static readonly MSystem M1296 = new MSystem(1296, "M1296");
            public static readonly MSystem M2592 = new MSystem(2592, "M2592");
            public static readonly MSystem M5184 = new MSystem(5184, "M5184");
            public static readonly MSystem M243 = new MSystem(243, "M243");
            public static readonly MSystem M486 = new MSystem(486, "M486");
            public static readonly MSystem M972 = new MSystem(972, "M972");
            public static readonly MSystem M1944 = new MSystem(1944, "M1944");
            public static readonly MSystem M3888 = new MSystem(3888, "M3888");
            public static readonly MSystem M7776 = new MSystem(7776, "M7776");
            public static readonly MSystem M729 = new MSystem(729, "M729");
            public static readonly MSystem M1458 = new MSystem(1458, "M1458");
            public static readonly MSystem M2916 = new MSystem(2916, "M2916");
            public static readonly MSystem M5832 = new MSystem(5832, "M5832");
            public static readonly MSystem M2187 = new MSystem(2187, "M2187");
            public static readonly MSystem M4374 = new MSystem(4374, "M4374");
            public static readonly MSystem M8748 = new MSystem(8748, "M8748");
            public static readonly MSystem M6561 = new MSystem(6561, "M6561");

            private MSystem(int value, string displayName) : base(displayName, value) { }
        }

    }
}
