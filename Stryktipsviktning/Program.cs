using System;
using System.Collections.Generic;
using Ardalis.SmartEnum;
using Weighted_Randomizer;

namespace Stryktipsviktning
{
    class Program
    {
        static void Main(string[] args)
        {
            var kupong = new Dictionary<int, DynamicWeightedRandomizer<string>>();
            var temp = 0;

            for (int i = 0; i < 13; i++)
            {
                kupong.Add(i, new DynamicWeightedRandomizer<string>());

                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine($"För match {i + 1}, andelsvikta {Tecken.FromValue(j + 1)}:");
                    temp = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    kupong[i].Add(Tecken.FromValue(j + 1).Name, temp);
                }
            }

            Console.Clear();
            Console.WriteLine("Slumpad rad:");
            foreach (var rad in kupong)
            {
                var tecken = rad.Value.NextWithReplacement();

                switch (tecken)
                {
                    case "1":
                        Console.WriteLine("1");
                        break;
                    case "X":
                        Console.WriteLine(" X");
                        break;
                    case "2":
                        Console.WriteLine("  2");
                        break;
                }
            }
        }

        public class Tecken : SmartEnum<Tecken>
        {
            public static readonly Tecken Ett = new Tecken(1, "1");
            public static readonly Tecken Kryss = new Tecken(2, "X");
            public static readonly Tecken Två = new Tecken(3, "2");

            private Tecken(int value, string displayName) : base(displayName, value) { }
        }

    }
}
