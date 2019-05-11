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
            

        public async Task<ResponseModel>UpdateAirports(List<AirportModel> airports)
        {
            response.RowsAffected = 0;
            response.ResponseMessage = "Ok";

            var existAirports = _db.Airports.ToList();

            if(existAirports.Count() > 0)
            {
                try
                {
                    _db.Airports.RemoveRange(existAirports);
                }
                catch (Exception ex){
                    response.RowsAffected = -1;
                    response.ResponseMessage = "Error during removing historical airports data. " + ex.Message;
                    return response;
                }                
            }                    

            try
            {
                response.RowsAffected = airports.Count();
                _db.Airports.AddRange(airports);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.RowsAffected = -1;
                response.ResponseMessage = "Error during updating airports data. " + ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel> UpdateRoutes(List<RouteModel> routes)
        {
            response.RowsAffected = 0;
            response.ResponseMessage = "Ok";

            var existRoutes = _db.Routes.ToList();
            

            if(existRoutes.Count() > 0)
            {
                try
                {
                    _db.Routes.RemoveRange(existRoutes);
                }
                catch (Exception ex)
                {
                    response.RowsAffected = -1;
                    response.ResponseMessage = "Error during updating Routes. " + ex.Message;
                }

            }  

            try
            {
                response.RowsAffected = routes.Count();
                _db.Routes.AddRange(routes);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.RowsAffected = -1;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public List<AirportModel> getAirports(){

            var airports = _db.Airports.ToList();
            return airports;
        }

        public class ResponseModel
        {
            public int RowsAffected { get; set; }

            public string ResponseMessage { get; set; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
