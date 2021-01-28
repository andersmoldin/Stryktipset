using System;
using CsvHelper.Configuration.Attributes;

namespace StryktipsetCore.Contract
{
    public class Omgång
    {
        public string ProduktNamn { get; set; }
        public int Id { get; set; }
        public string Vecka { get; set; }
        public string RättRad { get; set; }
        public ulong Omsättning { get; set; }
        public ulong Utdelning13 { get; set; }
        //public ulong Utdelning12 { get; set; }

        //[Name("utd11")]
        //public ulong Utdelning11 { get; set; }

        //[Name("utd10")]
        //public ulong Utdelning10 { get; set; }

        //[Ignore]
        //public int AntalMed13Rätt { get; set; }

        //[Ignore]
        //public int AntalMed12Rätt { get; set; }

        //[Ignore]
        //public int AntalMed11Rätt { get; set; }

        //[Ignore]
        //public int AntalMed10Rätt { get; set; }

        //[Ignore]
        //public int Ettor { get; set; }

        //[Ignore]
        //public int Kryss { get; set; }

        //[Ignore]
        //public int Tvåor { get; set; }

        //[Ignore]
        //public int AntalLottadeMatcher { get; set; }

        //[Ignore]
        //public Match[] Matcher { get; set; }
    }
}
