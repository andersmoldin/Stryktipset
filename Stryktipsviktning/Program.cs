using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Ardalis.SmartEnum;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using StryktipsetCore.Stryket;
using Weighted_Randomizer;

namespace Stryktipsviktning
{
    class Program
    {
        static string html;
        private static readonly HttpClient client = new HttpClient();
        private static readonly string correctCoupon = "2X111X1X1122X";
        private static Week[] weeks;

        static async Task Main(string[] args)
        {
            Initialize();

            Console.WriteLine(string.Join(Environment.NewLine, Faktorisera(144).Select(x => $"{x.Key}: {x.Value}")));
            Console.WriteLine();

            var oddsfavoritskap = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap).Result;
            var spelvärde = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Spelvärde).Result;
            var komboslump = Komboslump(oddsfavoritskap, spelvärde);
            var slump = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde().Result;

            SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);
            SkrivUtKupong(spelvärde, Värde.Spelvärde);
            SkrivUtKupong(komboslump, Värde.Komboslump);
            SkrivUtKupong(slump, null);

            //KollaHurMångaLooparSomBehövs();

            //SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);

            Console.WriteLine();

            //var spelvärde = await MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Spelvärde);
            //SkrivUtKupong(spelvärde, Värde.Spelvärde);

            //var oddsfavoritskap = HämtaOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap);
            //SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);

            //Console.WriteLine();

            //var spelvärde = HämtaOddsfavoritskapOchSpelvärde(Värde.Spelvärde);
            //SkrivUtKupong(spelvärde, Värde.Spelvärde);
        }

        private static void Initialize()
        {
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            weeks = JsonConvert.DeserializeObject<Week[]>(File.ReadAllText("tempStringTask.json"), new JsonSerializerSettings { Culture = new CultureInfo("sv-SE") });
            Console.Clear();
        }

        private static void KollaHurMångaLooparSomBehövs()
        {
            var stats = new Dictionary<int, int>();
            for (int i = 0; i <= 13; i++)
            {
                stats.Add(i, 0);
            }

            var oddsfavoritskap = "";
            var komboslump = "";
            var spelvärde = "";
            var slump = "";
            var loops = 0;
            var antalRätt = 0;

            do
            {
                oddsfavoritskap = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap).Result;
                spelvärde = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Spelvärde).Result;
                komboslump = Komboslump(oddsfavoritskap, spelvärde);
                slump = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde().Result;
                loops++;

                antalRätt = AntalRätt(oddsfavoritskap);
                stats[antalRätt]++;
                antalRätt = AntalRätt(spelvärde);
                stats[antalRätt]++;
                antalRätt = AntalRätt(komboslump);
                stats[antalRätt]++;
                antalRätt = AntalRätt(slump);
                stats[antalRätt]++;

                if (loops % 10000 == 0)
                {
                    Console.Clear();
                    Console.WriteLine($"{loops * 4:N0}:");
                    foreach (var rad in stats)
                    {
                        Console.WriteLine($"{rad.Key,2}: {rad.Value}");
                    }
                    Console.WriteLine($"Totalt: {stats.Sum(s => s.Value):N0}");
                }
            } while (!((oddsfavoritskap == correctCoupon) || (spelvärde == correctCoupon) || (komboslump == correctCoupon) || (slump == correctCoupon)));

            Console.WriteLine($"{Environment.NewLine}" +
                $"Antal loopar: {loops * 4:N0}" +
                $"{Environment.NewLine}" +
                $"Rätt rad:        {correctCoupon}" +
                $"{Environment.NewLine}" +
                $"Oddsfavoritskap: {oddsfavoritskap}" +
                $"{Environment.NewLine}" +
                $"Spelvärde:       {spelvärde}" +
                $"{Environment.NewLine}" +
                $"Komboslump:      {komboslump}" +
                $"{Environment.NewLine}" +
                $"Slump:           {slump}");
        }

        private static string Komboslump(string oddsfavoritskap, string spelvärde)
        {
            Random random = new Random();

            for (int i = 0; i < oddsfavoritskap.Length; i++)
            {
                if (random.Next() % 2 == 0)
                {
                    oddsfavoritskap = oddsfavoritskap.Remove(i, 1).Insert(i, spelvärde[i].ToString());
                }
            }

            return oddsfavoritskap;
        }

        private static int AntalRätt(string input)
        {
            int count = 0;

            for (int i = 0; i < correctCoupon.Length; i++)
            {
                if (correctCoupon[i] == input[i])
                {
                    count++;
                }
            }

            return count;
        }

        private static async Task<string> MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde? värde = null)
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();
            var response = await HämtaFrånStryketApi();
            var currentWeek = response.OrderByDescending(r => r.CloseTime).First().Events;

            for (int i = 0; i < currentWeek.Count; i++)
            {
                viktning.Add(i, new DynamicWeightedRandomizer<string>());

                switch (värde)
                {
                    case Värde.Oddsfavoritskap:
                        if (currentWeek[i].Odds != null)
                        {
                            viktning[i].Add(Tecken.Ett.Name, decimal.ToInt32(1 / currentWeek[i].Odds.Home * 100));
                            viktning[i].Add(Tecken.Kryss.Name, decimal.ToInt32(1 / currentWeek[i].Odds.Draw * 100));
                            viktning[i].Add(Tecken.Två.Name, decimal.ToInt32(1 / currentWeek[i].Odds.Away * 100));
                        }
                        else
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                viktning[i].Add(Tecken.FromValue(j + 1).Name, 1);
                            }
                        }
                        break;

                    case Värde.Spelvärde:
                        if (currentWeek[i].Odds != null && currentWeek[i].Distribution != null)
                        {
                            viktning[i].Add(Tecken.Ett.Name, decimal.ToInt32((1 / currentWeek[i].Odds.Home * 100) / decimal.Parse(currentWeek[i].Distribution.Home) * 100));
                            viktning[i].Add(Tecken.Kryss.Name, decimal.ToInt32((1 / currentWeek[i].Odds.Draw * 100) / decimal.Parse(currentWeek[i].Distribution.Draw) * 100));
                            viktning[i].Add(Tecken.Två.Name, decimal.ToInt32((1 / currentWeek[i].Odds.Away * 100) / decimal.Parse(currentWeek[i].Distribution.Away) * 100));
                        }
                        else
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                viktning[i].Add(Tecken.FromValue(j + 1).Name, 1);
                            }
                        }
                        break;

                    case null:
                        for (int j = 0; j < 3; j++)
                        {
                            viktning[i].Add(Tecken.FromValue(j + 1).Name, 1);
                        }
                        break;
                }

            }

            return GenereraTipsRad(viktning);
        }

        private static string GenereraTipsRad(Dictionary<int, DynamicWeightedRandomizer<string>> viktning)
        {
            var temp = "";

            foreach (var rad in viktning)
            {
                temp += rad.Value.NextWithReplacement();
            }

            return temp;
        }

        private static async Task<Week[]> HämtaFrånStryketApi(bool debug = false)
        {
            if (debug)
            {
                return weeks;
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://stryket.se/api/draws/stryktipset");
            return JsonConvert.DeserializeObject<Week[]>(await stringTask, new JsonSerializerSettings { Culture = new CultureInfo("sv-SE") });
        }

        private static Dictionary<int, DynamicWeightedRandomizer<string>> HämtaOddsfavoritskapOchSpelvärde(Värde värde)
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(GetHtml("https://stryket.se/stryktipset"));

            var rows = htmlDoc.DocumentNode.SelectNodes("//table//tbody//tr");

            for (int i = 0; i < rows.Count; i++)
            {
                var row = rows[i].ChildNodes.Where(r => r.Name == "td").ToList();

                viktning.Add(i, new DynamicWeightedRandomizer<string>());

                for (int j = 0; j < 3; j++)
                {
                    viktning[i].Add(Tecken.FromValue(j + 1).Name, int.Parse(row[j + (int)värde].InnerText.Replace(".", "")));
                }
            }

            return viktning;
        }

        private static string GetHtml(string url)
        {
            if (string.IsNullOrEmpty(html))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    var driver = new SafariDriver();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(url);
                    var table = driver.FindElement(By.XPath("/html/body/app-root/body/div/app-draw-value/table"));
                    html = driver.PageSource;
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var options = new ChromeOptions
                    {
                    };
                    var driver = new ChromeDriver();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    driver.Navigate().GoToUrl(url);
                    var table = driver.FindElement(By.XPath("/html/body/app-root/body/div/app-draw-value/table"));
                    html = driver.PageSource;
                }
            }

            return html;
        }

        private static void TaEmotInputOchSkapaEnViktadKupong()
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();

            for (int i = 0; i < 13; i++)
            {
                viktning.Add(i, new DynamicWeightedRandomizer<string>());

                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine($"För match {i + 1}, andelsvikta {Tecken.FromValue(j + 1)}:");
                    int temp = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();

                    viktning[i].Add(Tecken.FromValue(j + 1).Name, temp);
                }
            }
        }

        private static Dictionary<int, int> Faktorisera(int input)
        {
            int b;
            Dictionary<int, int> faktorer = new Dictionary<int, int>();

            for (b = 2; input > 1; b++)
                if (input % b == 0)
                {
                    int x = 0;
                    while (input % b == 0)
                    {
                        input /= b;
                        x++;
                    }
                    faktorer.Add(b, x);
                }

            return faktorer;
        }

        private static void SkrivUtKupong(Dictionary<int, DynamicWeightedRandomizer<string>> viktning, Värde värde)
        {
            Console.WriteLine($"Slumpad {värde.ToString().ToLower()}srad:");
            foreach (var rad in viktning)
            {
                var tecken = rad.Value.NextWithReplacement();

                var hej = rad.Value.Skip(0).Take(1);

                switch (tecken)
                {
                    case "1":
                        Console.WriteLine($"{"1".PadRight(3)} ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})");
                        break;
                    case "X":
                        Console.WriteLine($"{"X".PadLeft(2).PadRight(3)} ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})".PadLeft((1)));
                        break;
                    case "2":
                        Console.WriteLine($"{"2".PadLeft(3)} ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})");
                        break;
                }
            }
        }

        private static void SkrivUtKupong(string kupong, Värde? värde)
        {
            Console.WriteLine($"Slumpad {(värde == null ? "rad" : värde.ToString().ToLower())}srad:");
            foreach (var rad in kupong)
            {
                Print(rad);
            }
        }

        private static void Print(char input)
        {
            switch (input)
            {
                case '1':
                    Console.WriteLine($"{"1",1}");
                    break;
                case 'X':
                    Console.WriteLine($"{"X",2}");
                    break;
                case '2':
                    Console.WriteLine($"{"2",3}");
                    break;
            }
        }

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
            Komboslump
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
