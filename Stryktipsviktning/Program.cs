using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;
using StryktipsetCore;
using StryktipsetCore.Stryket;
using Weighted_Randomizer;
using static StryktipsetCore.Contract.Enums;

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
            Console.WriteLine($"Rad anno {DateTime.Now:HH:mm}:");
            SkrivUtKupong(MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärdeFörMSystem(RSystem.R4_4_144, Värde.Spelvärde).Result, Värde.Spelvärde);
            Console.WriteLine();
            SkrivUtKupong(MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärdeFörMSystem(RSystem.R4_4_144, Värde.Komboslump).Result, Värde.Komboslump);
            Console.WriteLine();

            /*var oddsfavoritskap = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap).Result;
            var spelvärde = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Spelvärde).Result;
            var komboslump = Komboslump(oddsfavoritskap, spelvärde);
            var slump = MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde().Result;

            SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);
            SkrivUtKupong(spelvärde, Värde.Spelvärde);
            SkrivUtKupong(komboslump, Värde.Komboslump);
            SkrivUtKupong(slump, null);*/

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
            var response = await HämtaFrånStryketApiAsync();
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

        private static async Task<List<string>> MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärdeFörMSystem(ISystem system, Värde? värde = null, Dictionary<int, DynamicWeightedRandomizer<string>> oddsfavoritskap = null, Dictionary<int, DynamicWeightedRandomizer<string>> spelvärde = null)
        {
            var viktning = new Dictionary<int, DynamicWeightedRandomizer<string>>();
            var response = HämtaFrånStryketApi();
            var currentWeek = response.OrderByDescending(r => r.CloseTime).First().Events;
            var slumpadeGarderingar = SlumpaGarderingarFörMSystem(system);

            for (int i = 0; i < currentWeek.Count; i++)
            {
                viktning.Add(i, new DynamicWeightedRandomizer<string>());

                if (värde == Värde.Komboslump)
                {
                    Random random = new Random();

                    Vikta(random.Next() % 2 == 0 ? Värde.Oddsfavoritskap : Värde.Spelvärde, i);
                }
                else if (värde == null)
                {
                    Vikta(null, i);
                }
                else
                {
                    Vikta(värde, i);
                }
            }
            
            return GenereraTipsRad(viktning, slumpadeGarderingar, currentWeek);

            void Vikta(Värde? värde, int i)
            {
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

        private static async Task<Week[]> HämtaFrånStryketApiAsync(bool debug = false)
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

        private static Week[] HämtaFrånStryketApi(bool debug = false)
        {
            if (debug)
            {
                return weeks;
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://stryket.se/api/draws/stryktipset").Result;
            return JsonConvert.DeserializeObject<Week[]>(stringTask, new JsonSerializerSettings { Culture = new CultureInfo("sv-SE") });
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

        public static Dictionary<int, int> Faktorisera(int input)
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

        private static Dictionary<int, int> SlumpaGarderingarFörMSystem(ISystem system)
        {
            var randomizer = new Random();
            var rad = new Dictionary<int, int>();
            for (int i = 1; i <= 13; i++)
            {
                rad.Add(i, 1);
            }

            Dictionary<int, int> faktorer;
            switch (system)
            {
                case MSystem mSystem:
                    faktorer = Utilities.Faktorisera(mSystem);
                    break;
                case RSystem rSystem:
                    faktorer = new Dictionary<int, int>();

                    var rSystemValues = rSystem.GetRSystemValues(rSystem);

                    if (rSystemValues.Halvgarderingar > 0)
                    {
                        faktorer.Add(2, rSystemValues.Halvgarderingar);
                    }

                    if (rSystemValues.Helgarderingar > 0)
                    {
                        faktorer.Add(3, rSystemValues.Helgarderingar);
                    }
                    break;
                default:
                    throw new Exception($"system ({system.GetType()}) går inte att processa.");
            }

            for (int i = 2; i <= 3; i++)
            {
                if (faktorer.ContainsKey(i))
                {
                    for (int j = 0; j < faktorer.Single(x => x.Key == i).Value; j++)
                    {
                        var ids = rad.Where(x => x.Value == 1).Select(x => x.Key).ToArray();
                        var randomId = ids[randomizer.Next(ids.Length)];
                        rad[randomId] = i;
                    }
                }
            }

            return rad;
        }

        private static List<string> GenereraTipsRad(Dictionary<int, DynamicWeightedRandomizer<string>> viktning, Dictionary<int, int> slumpadeGarderingar, List<Event> currentWeek)
        {
            var rad = new List<string>();

            for (int i = 0; i < 13; i++)
            {
                if (slumpadeGarderingar.ElementAt(i).Value == 3)
                {
                    rad.Add("1X2");
                }
                else
                {
                    string temp = "";

                    do
                    {
                        temp = viktning[i].NextWithReplacement();

                        if (rad.ElementAtOrDefault(i) == null)
                        {
                            rad.Add(temp);
                        }
                        else if (!rad[i].Contains(temp))
                        {
                            rad[i] += temp;
                        }
                    } while (rad.ElementAt(i).Length != slumpadeGarderingar.ElementAt(i).Value);
                }
            }

            //Prettify
            for (int i = 0; i < 13; i++)
            {
                rad[i] = $"{(i + 1).ToString(), 2}. {Prettify(rad[i]).PadRight(3)} ({currentWeek[i].Participants[0].Name} - {currentWeek[i].Participants[1].Name})";
            }

            return rad;
        }

        private static void SkrivUtKupong(string kupong, Värde? värde)
        {
            Console.WriteLine($"Slumpad {(värde == null ? "rad" : värde.ToString().ToLower())}srad:");
            foreach (var rad in kupong)
            {
                Print(rad);
            }
        }

        private static void SkrivUtKupong(List<string> kupong, Värde? värde)
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

        private static void Print(string input)
        {
            Console.WriteLine(input);
        }

        private static string Prettify(string input)
        {
            switch (input.Length)
            {
                case 1:
                    switch (input)
                    {
                        case "1":
                            return $"{"1",1}";
                        case "X":
                            return $"{"X",2}";
                        case "2":
                            return $"{"2",3}";
                    }
                    break;
                case 2:
                    switch (input)
                    {
                        case "1X":
                        case "X1":
                            return $"{"1X",2}";
                        case "X2":
                        case "2X":
                            return $"{"X2",3}";
                        case "12":
                        case "21":
                            return "1 2";
                    }
                    break;
                default:
                    return "1X2";
            }

            throw new Exception("Hit ska vi verkligen inte komma.");
        }
    }
}
