using Newtonsoft.Json;
using RyanairPlanner.Services;


namespace RyanairPlanner.Models
{
    [JsonConverter(typeof(CustomJsonConverter))]
    public class AirportModel
    {
        public int Id { get; set; }

        [JsonProperty("iataCode")]
        public string IataCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("seoName")]
        public string SeoName { get; set; }

        [JsonProperty("coordinates.latitude")]
        public string Latitude { get; set; }

        [JsonProperty("coordinates.longitude")]
        public string Longitude { get; set; }

        [JsonProperty("base")]
        public bool Base { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("regionCode")]
        public string RegionCode { get; set; }

        [JsonProperty("cityCode")]
        public string CityCode { get; set; }

        [JsonProperty("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }
    }
}
