using System;
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
        public static Omgång[] GetOmgångs()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using (var reader = new StreamReader("/Users/andersmoldin/Dropbox/Stryktipset/TipsXtra.se_Statistik_Summering_2021-01-28.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<OmgångMap>();

                var records = csv.GetRecords<Omgång>();

                return records.ToArray();
            }
        }


    }
}

