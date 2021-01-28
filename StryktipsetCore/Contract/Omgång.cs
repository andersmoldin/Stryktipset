using System;
using CsvHelper.Configuration.Attributes;

namespace StryktipsetCore.Contract
{
    public class Omgång
    {
        public int Id { get; set; }
        public string Vecka { get; set; }
        public string RättRad { get; set; }
        public ulong Omsättning { get; set; }
        public ulong Utdelning13 { get; set; }
        public ulong Utdelning12 { get; set; }
        public ulong Utdelning11 { get; set; }
        public ulong Utdelning10 { get; set; }
        public int AntalMed13Rätt { get; set; }
        public int AntalMed12Rätt { get; set; }
        public int AntalMed11Rätt { get; set; }
        public int AntalMed10Rätt { get; set; }
        public int Ettor { get; set; }
        public int Kryss { get; set; }
        public int Tvåor { get; set; }
        public int AntalLottadeMatcher { get; set; }
        public Match[] Matcher { get; set; }
    }
}
