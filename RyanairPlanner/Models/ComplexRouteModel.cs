using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.Models
{
    public class ComplexRouteModel:DirectRouteModel
    {
        public RouteType RouteStops { get; set; }

        public List<DirectRouteModel> RoutePart {get; set;}
    }

    public enum RouteType
    {
        Direct,
        OneStop,
        TwoStops,
        ThreeStops
    }
}
