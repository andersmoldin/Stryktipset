using System;
using CsvHelper.Configuration.Attributes;

namespace StryktipsetCore.Contract
{
    public class Match
    {
        public Match()
        {
        }

        public int Id { get; set; }
        public string Vecka { get; set; }
        public int MatchNummer { get; set; }
        public string HemmaLag { get; set; }
        public string BortaLag { get; set; }
        public int HemmaMål { get; set; }
        public int BortaMål { get; set; }
        public DateTime MatchStart { get; set; }
        public char Utfall { get; set; }
        public string LottadText { get; set; }
        public bool Lottad { get { return LottadText == "Lottad"; } }
        public double SvenskaFolketEttProcent { get; set; }
        public double SvenskaFolketKryssProcent { get; set; }
        public double SvenskaFolketTvåProcent { get; set; }
        public double TioTidningarEtt { get; set; }
        public double TioTidningarKryss{ get; set; }
        public double TioTidningarTvå { get; set; }
    }
}
