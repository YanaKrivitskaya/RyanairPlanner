using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class PriceModel
    {
        public decimal Value { get; set; }

        public int ValueMainUnit { get; set; }

        public int ValueFractionalUnit { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySymbol { get; set; }        
    }
}
