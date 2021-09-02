using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.SmartEnum;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using Weighted_Randomizer;

namespace Stryktipsviktning
{
    class Program
    {
        static string html;

        static void Main(string[] args)
        {
            Console.Clear();

            var oddsfavoritskap = HämtaOddsfavoritskapOchSpelvärde(Värde.Oddsfavoritskap);
            SkrivUtKupong(oddsfavoritskap, Värde.Oddsfavoritskap);

            Console.WriteLine();

            var spelvärde = HämtaOddsfavoritskapOchSpelvärde(Värde.Spelvärde);
            SkrivUtKupong(spelvärde, Värde.Spelvärde);
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
                var driver = new SafariDriver();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl(url);
                var table = driver.FindElement(By.XPath("/html/body/app-root/body/div/app-draw-value/table"));
                html = driver.PageSource;
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

        public enum Värde
        {
            Oddsfavoritskap = 8,
            Spelvärde = 11
        }
    }
}
