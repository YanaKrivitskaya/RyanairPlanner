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

        public List<AirportModel> getDirectAirports(string fromAirport)
        {
            var directRoutes = _db.Routes.Where(r => r.AirportFrom == fromAirport).ToList();

            var airports = new List<AirportModel>();

            foreach (var route in directRoutes)
            {
                airports.Add(_db.Airports.Where(a => a.IataCode == route.AirportTo).FirstOrDefault());
            }

            return airports;

        }

        public DirectRouteModel getDirectRoutes(string fromAirport, string toAirport)
        {
            var route = _db.Routes.Where(r => r.AirportFrom == fromAirport && r.AirportTo == toAirport).FirstOrDefault();      
            
            if (route != null)
            {
                var directRoute = ConvertToDirectRoute(route);
                return directRoute;
            }
            
            return null;
        }

        public List<ComplexRouteModel> getRoutes(string fromAirport, string toAirport, int stops)
        {
            var firstPartRoutes = _db.Routes.Where(r => r.AirportFrom == fromAirport).ToList();

            RouteModel secondPartRoute = null;
            
            var oneStopRoutes = new List<ComplexRouteModel>();

            foreach(var route in firstPartRoutes)
            {
                var directRoutes = new List<DirectRouteModel>();

                secondPartRoute = _db.Routes.Where(r => r.AirportFrom == route.AirportTo && r.AirportTo == toAirport).FirstOrDefault();

                if(secondPartRoute != null)
                {
                    directRoutes.Add(ConvertToDirectRoute(route));

                    directRoutes.Add(ConvertToDirectRoute(secondPartRoute));

                    oneStopRoutes.Add(new ComplexRouteModel
                    {
                        AirportFrom = route.AirportFrom,
                        AirportTo = secondPartRoute.AirportTo,
                        AirportFromName =  _db.Airports.Where(a => a.IataCode == route.AirportFrom).FirstOrDefault().Name,
                        AirportToName = _db.Airports.Where(a => a.IataCode == secondPartRoute.AirportTo).FirstOrDefault().Name,
                        RoutePart = directRoutes
                    });
                }
            }

            return oneStopRoutes;
        }



        public List<UpdatedDateModel> getUpdatedDate()
        {
            var dates = new List<UpdatesHistoryModel>();

            var response = new List<UpdatedDateModel>();

            var currentDate = DateTime.Now;
                
            dates.Add(_db.UpdatesHistory.Where(d => d.Name == "Airports").OrderByDescending(d => d.Id).FirstOrDefault());
            dates.Add(_db.UpdatesHistory.Where(d => d.Name == "Routes").OrderByDescending(d => d.Id).FirstOrDefault());

            foreach(var date in dates)
            {
                response.Add(new UpdatedDateModel
                {
                    Id = date.Id,
                    Name = date.Name,
                    RowsUpdated = date.RowsUpdated,
                    UpdatedDate = date.UpdatedDate,
                    DaysDiff = (date.UpdatedDate - currentDate).Days
                });
            }

            return response;
        }

        public DirectRouteModel ConvertToDirectRoute(RouteModel route)
        {
            var response = new DirectRouteModel
            {
                Id = route.Id,
                AirportFrom = route.AirportFrom,
                AirportFromName = _db.Airports.Where(a => a.IataCode == route.AirportFrom).FirstOrDefault().Name,
                AirportTo = route.AirportTo,
                AirportToName = _db.Airports.Where(a => a.IataCode == route.AirportTo).FirstOrDefault().Name
            };

            return response;
        }

        public class ResponseModel
        {
            public int ResponseCode { get; set; }

            public string ResponseMessage { get; set; }
        }

        public class UpdatedDateModel : UpdatesHistoryModel
        {
            public int DaysDiff { get; set; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
