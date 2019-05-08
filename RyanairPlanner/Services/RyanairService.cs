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

        public List<AirportModel> getAirports()
        {      
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/airports");

            IRestResponse response = client.Execute(request);

            var res = JsonConvert.DeserializeObject<List<AirportModel>>(response.Content);

            return res;
        }      

        public string getRoutesFromAirport(string iataCode)
        {
            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/routes/" + iataCode);

            IRestResponse response = client.Execute(request);

            var res = response.Content;

            return res;
        }

    }
}
