using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class AirportModel
    {
        public string Id { get; set; }
        public string IataCode { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Base { get; set; }
        public string CountryCode { get; set; }
        public string RegionCode { get; set; }
        public string CityCode { get; set; }
        public string CurrencyCode { get; set; }
        public string Priority { get; set; }
    }
}
