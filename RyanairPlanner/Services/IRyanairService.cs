using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.Models;
using System.Collections.Generic;

namespace RyanairPlanner.Services
{
    public interface IRyanairService
    {
        List<AirportModel> getAirports();

        string getRoutesFromAirport(string iataCode);

        List<string> getRoutesIataCodesFromAirport(string iataCode);

        List<RouteModel> getRoutes();

        string getScheduleAvailability(string depIata, string arrivIata);

        string getScheduleGlobalPeriod();

        string getSchedulePeriodReturn(string depIata, string arrivIata);

        string getScheduleMonth(string depIata, string arrivIata, string year, string month);

        string getSchedulePeriodOneWay(string depIata);
    }
}
