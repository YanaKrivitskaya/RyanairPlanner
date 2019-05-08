﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RyanairPlanner.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RyanairPlanner.Controllers
{
    [Route("api/ryanair")]
    public class RyanairDataController : Controller
    {
        private IRyanairService _ryanairService;

        public RyanairDataController(IRyanairService ryanairService)
        {
            _ryanairService = ryanairService;
        }

        // GET: api/ryanair/airports
        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirports()
        {
            var data = _ryanairService.getAirports();
            var res = Json(data).Value; 

            return Ok(res);
        }

        // GET api/ryanair/routes/VNO
        [HttpGet]
        [Route("routes/{iataCode}")]
        public IActionResult Get(string iataCode)
        {
            var data = _ryanairService.getRoutesFromAirport(iataCode);

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
