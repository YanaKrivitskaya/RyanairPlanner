using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RyanairPlanner.Models;
using System;
using System.Collections.Generic;

namespace RyanairPlanner.Services
{
    public class CustomConverter
    {       
        public OneWayFareModel GetFareModel(string jsonResponse)
        {
            dynamic json = JsonConvert.DeserializeObject(jsonResponse);

            OneWayFareModel fareModel;

            try
            {
                fareModel = new OneWayFareModel
                {
                    DepartureDate = json.fares[0].outbound.departureDate,
                    ArrivalDate = json.fares[0].outbound.arrivalDate,                   
                    Price = new PriceModel
                    {
                        Value = json.fares[0].outbound.price.value,
                        ValueMainUnit = json.fares[0].outbound.price.valueMainUnit,
                        ValueFractionalUnit = json.fares[0].outbound.price.valueFractionalUnit,
                        CurrencyCode = json.fares[0].outbound.price.currencyCode,
                        CurrencySymbol = json.fares[0].outbound.price.currencySymbol
                    }
                };
            } catch(Exception ex)
            {
                return new OneWayFareModel();
            }                     

            return fareModel;
        }   

        public MonthScheduleModel GetMonthScheduleModel(string jsonResponse, string year)
        {
            dynamic json = JsonConvert.DeserializeObject(jsonResponse);

            MonthScheduleModel monthSchedule;

            try
            {
                monthSchedule = new MonthScheduleModel
                {
                    Month = json.month,
                    Flights = new List<Flight>()
                };

                foreach (var day in json.days)
                {
                    foreach(var flight in day.flights)
                    {
                        monthSchedule.Flights.Add(new Flight()
                        {
                            Date = new DateTime(Convert.ToInt32(year), monthSchedule.Month, day.day.ToObject<int>()),
                            CarrierCode = flight.carrierCode,
                            Number = flight.number,
                            DepartureTime = flight.departureTime,
                            ArrivalTime = flight.arrivalTime
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return new MonthScheduleModel();
            }

            return monthSchedule;
        }

        public void GetPriceModel(string jsonResponse, MonthScheduleModel monthSchedule)
        {
            dynamic json = JsonConvert.DeserializeObject(jsonResponse);

            if (json.outbound.minFare != null)
            {
                monthSchedule.LowestFare = json.outbound.minFare.price.value;
            }            

            foreach (var fare in json.outbound.fares)
            {
                if (fare.unavailable == false)
                {
                    if (fare.price != null)
                    {
                        monthSchedule.Flights.Find(f => f.Date == fare.day.ToObject<DateTime>()).Price = new PriceModel()
                        {
                            Value = fare.price.value,
                            ValueMainUnit = fare.price.valueMainUnit,
                            ValueFractionalUnit = fare.price.valueFractionalUnit,
                            CurrencyCode = fare.price.currecyCode,
                            CurrencySymbol = fare.price.currencySymbol
                        };
                    }                    

                    monthSchedule.Flights.Find(f => f.Date == fare.day.ToObject<DateTime>()).SoldOut = fare.soldOut;
                }

                
            }
            
        }

    }
}
