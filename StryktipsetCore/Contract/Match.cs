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
        public Lag HemmaLag { get; set; }
        public Lag BortaLag { get; set; }
        public int HemmaMål { get; set; }
        public int BortaMål { get; set; }
        public DateTime MatchStart { get; set; }
        [BooleanTrueValues("Lottad")]
        public char Utfall { get; set; }
        public bool Lottad { get; set; }
        public double SvenskaFolketEttProcent { get; set; }
        public double SvenskaFolketKryssProcent { get; set; }
        public double SvenskaFolketTvåProcent { get; set; }
        public double TioTidningarEtt { get; set; }
        public double TioTidningarKryss{ get; set; }
        public double TioTidningarTvå { get; set; }
    }
}
