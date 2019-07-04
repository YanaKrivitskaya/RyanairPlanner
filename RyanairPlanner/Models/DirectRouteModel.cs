using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class DirectRouteModel
    {
        public int Id { get; set; }

        public string AirportFrom { get; set; }

        public string AirportFromName { get; set; }

        public string AirportTo { get; set; }

        public string AirportToName { get; set; }
    }
}
