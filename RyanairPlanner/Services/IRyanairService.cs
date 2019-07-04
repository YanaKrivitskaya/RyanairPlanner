using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.Models;
using System;
using System.Collections.Generic;

namespace RyanairPlanner.Services
{
    public interface IRyanairService
    {
        List<AirportModel> getAirports();

        string getRoutesFromAirport(string iataCode);

        List<string> getRoutesIataCodesFromAirport(string iataCode);

        List<RouteModel> getRoutes();

        List<DateTime> getScheduleAvailability(string depIata, string arrivIata);

        string getScheduleGlobalPeriod();

        string getSchedulePeriodReturn(string depIata, string arrivIata);

        MonthScheduleModel getScheduleMonth(string depIata, string arrivIata, string year, string month);

        string getSchedulePeriodOneWay(string depIata);

        OneWayFareModel getCheapest(string depIata, string arrivIata, DateTime? depDate1, DateTime? depDate2);

        string getCheapestPerDay(string depIata, string arrivIata, MonthScheduleModel monthSchedule, DateTime? depDate1);
    }
}
