using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.EFCore;
using RyanairPlanner.Models;
using RyanairPlanner.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RyanairPlanner.Controllers
{
    [Route("api/ryanair")]
    public class RyanairDataController : Controller
    {
        private IRyanairService _ryanairService;
        private RyanairRepository _ryanairRepository;
        private readonly CustomConverter _converter;

        public RyanairDataController(IRyanairService ryanairService, DatabaseContext ctx)
        {
            _ryanairService = ryanairService;
            _ryanairRepository = new RyanairRepository(ctx);
            _converter = new CustomConverter();
        }

        // GET: api/ryanair/routes/update - !IN USE!
        [HttpGet]
        [Route("routes/update")]
        public async Task<IActionResult> UpdateRoutes()
        {
            var routes = _ryanairService.getRoutes();
            var response = await _ryanairRepository.UpdateRoutes(routes);                      

            return Ok(response);
        }

        // GET: api/ryanair/airports/update - !IN USE!
        [HttpGet]
        [Route("airports/update")]
        public async Task<IActionResult> UpdateAirports()
        {
            var airports = _ryanairService.getAirports();
            var response = await _ryanairRepository.UpdateAirports(airports);

            return Ok(response);
        }

        // GET: api/ryanair/airports/VNO - !IN USE!
        [HttpGet]
        [Route("airports/{fromAirport}")]
        public async Task<IActionResult> GetDirectAirports(string fromAirport)
        {
            var airports = _ryanairRepository.getDirectAirports(fromAirport);

            return Ok(airports);
        }

        // GET: api/ryanair/routes - !IN USE!
        [HttpGet]
        [Route("routes/{fromAirport}/{toAirport}/{fromDate}/{toDate}")]
        public IActionResult GetDirectRoutes(string fromAirport, string toAirport, DateTime? fromDate, DateTime? toDate)
        {
            var route =  _ryanairRepository.getDirectRoutes(fromAirport, toAirport);

            MonthScheduleModel monthSchedule = null;

            if (route!= null) { 
                //get schedule days/departure time for month
                monthSchedule = _ryanairService.getScheduleMonth(fromAirport, toAirport, fromDate?.Year.ToString(), fromDate?.Month.ToString());

                monthSchedule.AirportFrom = route.AirportFrom;
                monthSchedule.AirportFromName = route.AirportFromName;
                monthSchedule.AirportTo = route.AirportTo;
                monthSchedule.AirportToName = route.AirportToName;

                if (monthSchedule.Flights != null)
                {
                    // get fares for given route for month
                    var pricesJson = _ryanairService.getCheapestPerDay(fromAirport, toAirport, monthSchedule, fromDate);

                    _converter.GetPriceModel(pricesJson, monthSchedule);
                }
                else
                {
                    return BadRequest("No flights found for the given route");
                }
            }
            else
            {
                return BadRequest("No direct routes found");
            }

            return Ok(monthSchedule);
        }

        // GET: api/ryanair/routes
        [HttpGet]
        [Route("routes/{fromAirport}/{toAirport}/{oneStop}")]
        public async Task<IActionResult> GetRoutes(string fromAirport, string toAirport, int oneStop)
        {
            //var data = _ryanairService.getRoutes();

            var response = _ryanairRepository.getRoutes(fromAirport, toAirport, 1);

            return Ok(response);
        }

        // GET: api/ryanair/airports
        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirports()
        {
            var response = _ryanairRepository.getAirports();

            return Ok(response);
        }

        // GET: api/ryanair/updateddate
        [HttpGet]
        [Route("updateddate")]
        public IActionResult GetUpdatedDate()
        {
            var response = _ryanairRepository.getUpdatedDate();

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

    }
}
