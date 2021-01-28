using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using StryktipsetCore.Contract;

namespace StryktipsetCore
{
    public class Read
    {
        private static Omgång[] Omgångar()
        {
            //using (var reader = new StreamReader("/Users/andersmoldin/Dropbox/Stryktipset/TipsXtra.se_Statistik_Summering_2021-01-28.csv"))
            using (var reader = new StreamReader(@"C:\Users\andersmoldin\Downloads\TipsXtra.se_Statistik_Summering_2021-01-28.csv"))

            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", TrimOptions = TrimOptions.Trim }))
            {
                var records = new List<Omgång>();
                csv.Context.RegisterClassMap<OmgångMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if (csv.GetField("produktnamn").Trim() == Constants.Stryktipset)
                    {
                        records.Add(csv.GetRecord<Omgång>());
                    }
                }

                return records.ToArray();
            }
        }

        private static Match[] Matcher()
        {
            using (var reader = new StreamReader(@"C:\Users\andersmoldin\Downloads\TipsXtra.se_Statistik_Detaljer_2021-01-28.csv"))

            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", TrimOptions = TrimOptions.Trim }))
            {
                var records = new List<Match>();
                csv.Context.RegisterClassMap<MatchMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if (csv.GetField("produktnamn").Trim() == Constants.Stryktipset)
                    {
                        records.Add(csv.GetRecord<Match>());
                    }
                }

                return records.ToArray();
            }
        }

        private static Omgång[] Intertwine(Omgång[] omgångar, Match[] matcher)
        {
            foreach (var omgång in omgångar)
            {
                omgång.Matcher = matcher.Where(m => m.Id == omgång.Id).ToArray();
            }

            return omgångar;
        }

        public static Omgång[] OmgångarMedMatcher()
        {
            return Intertwine(Omgångar(), Matcher());
        }
    }
}
