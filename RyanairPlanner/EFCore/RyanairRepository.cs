using Microsoft.EntityFrameworkCore;
using RyanairPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanairPlanner.EFCore
{
    public class RyanairRepository : IDisposable
    {
        private DatabaseContext _db;
        private ResponseModel response;

        public RyanairRepository(DatabaseContext context)
        {
            _db = context;
            response = new ResponseModel();
        }


        public async Task<ResponseModel> UpdateAirports(List<AirportModel> airports)
        {
            response.ResponseCode = 0;
            response.ResponseMessage = "";

            var existAirports = _db.Airports.ToList();

            if (existAirports.Count() > 0)
            {
                try
                {
                    _db.Airports.RemoveRange(existAirports);
                }
                catch (Exception ex) {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "Error during updating Airports. " + ex.Message;
                }
            }

            try
            {
                _db.Airports.AddRange(airports);
                await _db.SaveChangesAsync();
                await HistoryUpdate("Airports", airports.Count());
                response.ResponseMessage = "Airports updated. Rows affected: " + airports.Count();
            }
            catch (Exception ex)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "Error during updating Airports. " + ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel> UpdateRoutes(List<RouteModel> routes)
        {
            response.ResponseCode = 0;
            response.ResponseMessage = "";

            var existRoutes = _db.Routes.ToList();

            if (existRoutes.Count() > 0)
            {
                try
                {
                    _db.Routes.RemoveRange(existRoutes);
                }
                catch (Exception ex)
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "Error during updating Routes. " + ex.Message;
                }
            }
            try
            {
                _db.Routes.AddRange(routes);
                await _db.SaveChangesAsync();
                await HistoryUpdate("Routes", routes.Count());
                response.ResponseMessage = "Routes updated. Rows affected: " + routes.Count();
            }
            catch (Exception ex)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = "Error during updating Routes. " + ex.Message;
            }

            return response;
        }

        public async Task HistoryUpdate(string name, int rows)
        {
            UpdatesHistoryModel entry = new UpdatesHistoryModel()
            {
                Name = name,
                RowsUpdated = rows,
                UpdatedDate = DateTime.Now
            };

            _db.UpdatesHistory.Add(entry);
            await _db.SaveChangesAsync();
        }

        public List<AirportModel> getAirports(){

            var airports = _db.Airports.ToList();
            return airports;
        }

        public List<RouteModel> getRoutes(string fromAirport, string toAirport)
        {
            var routes = _db.Routes.Where(r => r.AirportFrom == fromAirport).ToList(); ;

            var res = routes.Where(r => r.AirportTo == toAirport).ToList();

            return res;
        }

        public class ResponseModel
        {
            public int ResponseCode { get; set; }

            public string ResponseMessage { get; set; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
