using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StryktipsetCore.Stryket
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Odds
    {
        [JsonProperty("home")]
        public decimal Home { get; set; }

        [JsonProperty("draw")]
        public decimal Draw { get; set; }

        [JsonProperty("away")]
        public decimal Away { get; set; }

        public decimal Total => Home + Draw + Away;

        public int HomePercent => Convert.ToInt32(Total / Home * 100);
    }

    public class Distribution
    {
        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("draw")]
        public string Draw { get; set; }

        [JsonProperty("away")]
        public string Away { get; set; }
    }

    public class NewspaperAdvice
    {
        [JsonProperty("home")]
        public string Home { get; set; }

        [JsonProperty("draw")]
        public string Draw { get; set; }

        [JsonProperty("away")]
        public string Away { get; set; }
    }

    public class Season
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Country
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class League
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("season")]
        public Season Season { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }
    }

    public class Participant
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Event
    {
        [JsonProperty("eventNumber")]
        public int EventNumber { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("extraInfo")]
        public object ExtraInfo { get; set; }

        [JsonProperty("eventTypeDescription")]
        public string EventTypeDescription { get; set; }

        [JsonProperty("participantType")]
        public string ParticipantType { get; set; }

        [JsonProperty("outcomes")]
        public object Outcomes { get; set; }

        [JsonProperty("odds")]
        public Odds Odds { get; set; }

        [JsonProperty("distribution")]
        public Distribution Distribution { get; set; }

        [JsonProperty("newspaperAdvice")]
        public NewspaperAdvice NewspaperAdvice { get; set; }

        [JsonProperty("league")]
        public League League { get; set; }

        [JsonProperty("participants")]
        public List<Participant> Participants { get; set; }

        [JsonProperty("sportEventId")]
        public int SportEventId { get; set; }

        [JsonProperty("sportEventStart")]
        public DateTime SportEventStart { get; set; }
    }

    public class Week
    {
        [JsonProperty("drawComment")]
        public string DrawComment { get; set; }

        [JsonProperty("extraInfo")]
        public object ExtraInfo { get; set; }

        [JsonProperty("fund")]
        public object Fund { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }

        [JsonProperty("drawNumber")]
        public int DrawNumber { get; set; }

        [JsonProperty("openTime")]
        public DateTime OpenTime { get; set; }

        [JsonProperty("closeTime")]
        public DateTime CloseTime { get; set; }

        [JsonProperty("turnover")]
        public string Turnover { get; set; }

        [JsonProperty("sport")]
        public string Sport { get; set; }

        [JsonProperty("sportId")]
        public int SportId { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }
    }


}
