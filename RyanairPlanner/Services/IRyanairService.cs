using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.Models;
using System.Collections.Generic;

namespace RyanairPlanner.Services
{
    public interface IRyanairService
    {
        List<AirportModel> getAirports();

        string getRoutesFromAirport(string iataCode);
    }
}
