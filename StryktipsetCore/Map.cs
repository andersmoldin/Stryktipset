using System;
using CsvHelper.Configuration;
using StryktipsetCore.Contract;

namespace StryktipsetCore
{
    public sealed class OmgångMap : ClassMap<Omgång>
    {
        public OmgångMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.Vecka).Name("omg");
            Map(m => m.ProduktNamn).Name("produktnamn").Validate(field => field != "Stryktipset");
        }
    }
}
