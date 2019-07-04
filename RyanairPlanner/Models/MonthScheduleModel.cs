using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class MonthScheduleModel : DirectRouteModel
    {
        public int Month { get; set; }

        public decimal LowestFare { get; set; }

        public List<Flight> Flights { get; set; }
    }

    public class Flight
    {
        public DateTime Date { get; set; }

        public string CarrierCode { get; set; }

        public string Number { get; set; }

        public string DepartureTime { get; set; }

        public string ArrivalTime { get; set; }        

        public PriceModel Price { get; set; }

        public Boolean SoldOut { get; set; }
    }
    
}
