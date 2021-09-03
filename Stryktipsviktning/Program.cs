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

        static async Task Main(string[] args)
        {
            Console.Clear();

            var oddsfavoritskap = await MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap);
            SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);
            
            Console.WriteLine();

            var spelvärde = await MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde.Spelvärde);
            SkrivUtKupong(spelvärde, Värde.Spelvärde);

            //var oddsfavoritskap = HämtaOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap);
            //SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);

            //Console.WriteLine();

            //var spelvärde = HämtaOddsfavoritskapOchSpelvärde(Värde.Spelvärde);
            //SkrivUtKupong(spelvärde, Värde.Spelvärde);
        }

        private static async Task<Dictionary<int, DynamicWeightedRandomizer<string>>> MasseraApiOchRäknaUtOddsfavoritskapOchSpelvärde(Värde värde)
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
                }

            }

            return viktning;
        }

        private static async Task<Week[]> HämtaFrånStryketApi()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://stryket.se/api/draws/stryktipset");

            //var stringTask = File.ReadAllText("tempStringTask.txt");

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
                        Console.WriteLine($"1   ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})");
                        break;
                    case "X":
                        Console.WriteLine($" X  ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})");
                        break;
                    case "2":
                        Console.WriteLine($"  2 ({rad.Value["1"]} {rad.Value["X"]} {rad.Value["2"]})");
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

        public enum Värde
        {
            Oddsfavoritskap = 8,
            Spelvärde = 11
        }
    }
}
