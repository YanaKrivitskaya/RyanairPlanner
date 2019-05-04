using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.Extensions.Configuration;

namespace RyanairPlanner.Services
{
    public class RyanairService : IRyanairService
    {
        private readonly IConfiguration _config;

        public RyanairService(IConfiguration config)
        {
            _config = config;
        }

        public string getAirports()
        {
            string apikey = _config.GetValue<String>("apikey");

            

            var client = new RestClient("http://apigateway.ryanair.com/pub/v1/core/3/airports");

            var request = new RestRequest(Method.GET);
            request.AddParameter("apikey", apikey);

            IRestResponse response = client.Execute(request);

            return response.ToString();
        }
    }
}
