using System;
using System.Collections.Generic;
using RestSharp;
using Microsoft.Extensions.Configuration;
using RyanairPlanner.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace RyanairPlanner.Services
{
    public class RyanairService : IRyanairService
    {
        private readonly IConfiguration _config;
        private readonly string apikey;
        private readonly RestRequest request;

        public RyanairService(IConfiguration config)
        {
            _config = config;
            apikey = _config.GetValue<String>("apikey");
            request = new RestRequest(Method.GET);
            request.AddParameter("apikey", apikey);
        }

        #region CoreAPI

        //Returns list of all active airports
        public List<AirportModel> getAirports()
        {      
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/airports");

            IRestResponse response = client.Execute(request);

            var res = JsonConvert.DeserializeObject<List<AirportModel>>(response.Content);

            return res;
        }

        //Returns list of all available countries
        public string getCountries()
        {
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/countries");

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return "OK";
        }

        //Returns list of all active routes from the airport given by its three-letter IATA code
        public string getRoutesFromAirport(string iataCode)
        {
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/routes/" + iataCode);

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }


        //Returns list of three-letter IATA codes for the arrival airports on all active routes from the airport given by its three-letter IATA code        
        public List<string> getRoutesIataCodesFromAirport (string iataCode)
        {
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/routes/" + iataCode + "/iataCodes");

            IRestResponse response = client.Execute(request);

            //var res = response.Content;

            var res = JsonConvert.DeserializeObject<List<string>>(response.Content);

            return res;
        }

        //Returns list of all active routes
        public List<RouteModel> getRoutes()
        {
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/routes");

            IRestResponse response = client.Execute(request);

            var res = JsonConvert.DeserializeObject<List<RouteModel>>(response.Content);                     

            return res;
        }

        #endregion

        #region Timetable API

        //Returns list of days with available flights for given route
        public string getScheduleAvailability(string depIata, string arrivIata)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/timtbl/3/schedules/{0}/{1}/availability", depIata, arrivIata));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        //Returns global schedule period
        public string getScheduleGlobalPeriod()
        {
            var client = new RestClient("https://services-api.ryanair.com/timtbl/3/schedules/period");

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        //Returns schedule period for given route
        public string getSchedulePeriodReturn(string depIata, string arrivIata)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/timtbl/3/schedules/{0}/{1}/period", depIata, arrivIata));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        //Returns list of flights scheduled for given route, year, month
        public string getScheduleMonth(string depIata, string arrivIata, string year, string month)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/timtbl/3/schedules/{0}/{1}/years/{2}/months/{3}", depIata, arrivIata, year, month));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        //Returns schedule period for given departure airport
        public string getSchedulePeriodOneWay(string depIata)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/timtbl/3/schedules/{0}/periods", depIata));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        #endregion

        #region Farefinder API

        //Returns sorted list (ascending) of one way fares for given filter parameters
        public string getCheapest(string depIata, string arrivIata, DateTime depDate1, DateTime depDate2)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/farfnd/3/oneWayFares", depIata, arrivIata));

            request.AddParameter("departureAirportIataCode", depIata);
            request.AddParameter("arrivalAirportIataCode", arrivIata);
            request.AddParameter("outboundDepartureDateFrom", depDate1.ToString("yyyy-MM-dd"));
            request.AddParameter("outboundDepartureDateTo", depDate2.ToString("yyyy-MM-dd"));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        //Returns sorted list (ascending) of one way fares for given filter parameters per day.
        public string getCheapestPerDay(string depIata, string arrivIata, DateTime depDate1, DateTime depDate2)
        {
            var client = new RestClient(String.Format("https://services-api.ryanair.com/farfnd/3/oneWayFares/{0}/{1}/cheapestPerDay", depIata, arrivIata));

            //request.AddParameter("outboundWeekOfDate", depDate1.ToString("yyyy-MM-dd"));
            request.AddParameter("outboundMonthOfDate", depDate1.ToString("yyyy-MM-dd"));

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

        #endregion

    }
}
