using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class RouteModel
    {
        public int Id { get; set; }

        public string AirportFrom { get; set; }

        public string AirportTo { get; set; }

        public string ConnectingAirport { get; set; }

        public bool NewRoute { get; set; }

        public bool SeasonalRoute { get; set; }

        public string Operator { get; set; }

        public string Group { get; set; }

        public string CarrierCode { get; set; }
    }
}
