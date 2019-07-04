using Newtonsoft.Json;
using RyanairPlanner.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class OneWaySchedule : DirectRouteModel
    {        
        public List<OneWayFareModel> Fares { get; set; }
    }

    public class OneWayFareModel
    {        
        public DateTime DepartureDate { get; set; }
        
        public DateTime ArrivalDate { get; set; }        

        public PriceModel Price { get; set; }
    }
}
