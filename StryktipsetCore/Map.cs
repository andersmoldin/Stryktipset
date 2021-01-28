using System;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using StryktipsetCore.Contract;

namespace StryktipsetCore
{
    public sealed class OmgångMap : ClassMap<Omgång>
    {
        public OmgångMap()
        {
            Map(m => m.AntalLottadeMatcher).Name("randomResults");
            Map(m => m.AntalMed10Rätt).Name("ant10");
            Map(m => m.AntalMed11Rätt).Name("ant11");
            Map(m => m.AntalMed12Rätt).Name("ant12");
            Map(m => m.AntalMed13Rätt).Name("ant13");
            Map(m => m.Ettor).Name("count1");
            Map(m => m.Id).Name("id");
            Map(m => m.Kryss).Name("countX");
            Map(m => m.Omsättning).Name("turnover");
            Map(m => m.RättRad).Name("correctRow");
            Map(m => m.Tvåor).Name("count2");
            Map(m => m.Utdelning10).Name("utd10");
            Map(m => m.Utdelning11).Name("utd11");
            Map(m => m.Utdelning12).Name("utd12");
            Map(m => m.Utdelning13).Name("utd13");
            Map(m => m.Vecka).Name("omg");
        }
    }

    public sealed class MatchMap : ClassMap<Match>
    {
        public MatchMap()
        {
            Map(m => m.BortaLag).Name("bortalag");
            Map(m => m.BortaMål).Name("bortaresultat");
            Map(m => m.HemmaLag).Name("hemmalag");
            Map(m => m.HemmaMål).Name("hemmaresultat");
            Map(m => m.Id).Name("svspelinfo_id");
            Map(m => m.LottadText).Name("matchstatus");
            Map(m => m.MatchNummer).Name("matchnummer");
            Map(m => m.MatchStart).Name("matchstart");
            Map(m => m.SvenskaFolketEttProcent).Name("svenskaFolket1");
            Map(m => m.SvenskaFolketKryssProcent).Name("svenskaFolketX");
            Map(m => m.SvenskaFolketTvåProcent).Name("svenskaFolket2");
            Map(m => m.TioTidningarEtt).Name("tioTidningar1");
            Map(m => m.TioTidningarKryss).Name("tioTidningarX");
            Map(m => m.TioTidningarTvå).Name("tioTidningar2");
            Map(m => m.Utfall).Name("utfall");
            Map(m => m.Vecka).Name("omg");
        }
    }
}
