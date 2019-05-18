using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.EFCore;
using RyanairPlanner.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RyanairPlanner.Controllers
{
    [Route("api/ryanair")]
    public class RyanairDataController : Controller
    {
        private IRyanairService _ryanairService;
        private RyanairRepository _ryanairRepository;

        public RyanairDataController(IRyanairService ryanairService, DatabaseContext ctx)
        {
            _ryanairService = ryanairService;
            _ryanairRepository = new RyanairRepository(ctx);
        }

        // GET: api/ryanair/routes/update
        [HttpGet]
        [Route("routes/update")]
        public async Task<IActionResult> UpdateRoutes()
        {
            var routes = _ryanairService.getRoutes();
            var response = await _ryanairRepository.UpdateRoutes(routes);                      

            return Ok(response);
        }

        // GET: api/ryanair/airports/update
        [HttpGet]
        [Route("airports/update")]
        public async Task<IActionResult> UpdateAirports()
        {
            var airports = _ryanairService.getAirports();
            var response = await _ryanairRepository.UpdateAirports(airports);

            return Ok(response);
        }

        // GET: api/ryanair/routes
        [HttpGet]
        [Route("routes/{fromAirport}/{toAirport}")]
        public async Task<IActionResult> GetRoutes(string fromAirport, string toAirport)
        {
            //var data = _ryanairService.getRoutes();

            var response =  _ryanairRepository.getRoutes(fromAirport, toAirport);
            
            return Ok(response);
        }

        // GET: api/ryanair/airports
        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> GetAirports()
        {
            //var airports = _ryanairService.getAirports();

            //var response = await _ryanairRepository.UpdateAirports(airports);

            var response = _ryanairRepository.getAirports();

            return Ok(response);
        }

        // GET api/ryanair/routes/VNO
        [HttpGet]
        [Route("routes/{iataCode}")]
        public IActionResult Get(string iataCode)
        {
            var data = _ryanairService.getRoutesFromAirport(iataCode);

            var data1 = _ryanairService.getRoutes();

            var data2 = _ryanairService.getRoutesIataCodesFromAirport(iataCode);

            var data3 = _ryanairService.getScheduleAvailability(iataCode, "MAD");

            var data4 = _ryanairService.getScheduleGlobalPeriod();

            var data5 = _ryanairService.getScheduleMonth(iataCode, "MAD", "2019", "10");

            var data6 = _ryanairService.getSchedulePeriodOneWay(iataCode);

            var data7 = _ryanairService.getSchedulePeriodReturn(iataCode, "MAD");            

            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class UpdateResponseModel
        {
            public int AirportsAffected { get; set; }

            public int RoutesAffected { get; set; }

            public string ResponseMessage { get; set; }            
        }
    }
}
