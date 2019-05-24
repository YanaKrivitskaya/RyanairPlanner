using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class OneWayFareModel
    {
        public DateTime DepartureDate { get; set; }

        public DateTime ArrivalDate { get; set; }

        public BasicAirportModel DepartureAirport { get; set; }

        public BasicAirportModel ArrivalAirport { get; set; }

        public PriceModel Price { get; set; }
    }
}
