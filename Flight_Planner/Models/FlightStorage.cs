using Flight_Planner.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ModelState = System.Web.ModelBinding.ModelState;

namespace Flight_Planner.Models
{
    public class FlightStorage
    {
        //public static List<Flight> DbFlights = new List<Flight>();

        public static Flight GetFlight(int id)
        {
            using (var context = new FlightPlannerDBContext())
            {
                var flight = context.Flights.Include(a => a.From).Include(a => a.To).SingleOrDefault(f => f.Id == id);
                return flight;
            }
            //var flight = DbFlights.FirstOrDefault(x => x.Id == id);
        }

        private static Object thisLock = new Object();
        public static HttpResponseMessage AddFlight(HttpRequestMessage request, Flight flight)
        {
            lock (thisLock)
            {
                if (DateIsInvalid(flight))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                if (FromAirportSameTo(flight))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                using (var context = new FlightPlannerDBContext())
                {
                    if (IsSameFlight(context, flight))
                    {
                        return new HttpResponseMessage(HttpStatusCode.Conflict);
                    }
                    var flights = context.Flights.Add(flight);
                    context.SaveChanges();
                    var fakeFlight = new TestFlight();

                    fakeFlight.Carrier = flights.Carrier;
                    fakeFlight.From.AirportName = flights.From.AirportName;
                    fakeFlight.From.City = flights.From.City;
                    fakeFlight.From.Country = flights.From.Country;
                    fakeFlight.To.AirportName = flights.To.AirportName;
                    fakeFlight.To.City = flights.To.City;
                    fakeFlight.To.Country = flights.To.Country;
                    fakeFlight.ArrivalTime = flights.ArrivalTime;
                    fakeFlight.DepartureTime = flights.DepartureTime;
                    fakeFlight.Id = flights.Id;
                    return request.CreateResponse(HttpStatusCode.Created, fakeFlight);
                }
            }
            
        }

        public static HttpResponseMessage FindAirport(HttpRequestMessage request, string search)
        {
            var foundAirport = new TestAirport();
            
            using (var context = new FlightPlannerDBContext())
            {
                var airports  = context.Airports.Where(c => c.AirportName.ToLower().Contains(search.ToLower().Trim()) ||
                                                     c.City.ToLower().Contains(search.ToLower().Trim()) ||
                                                     c.Country.ToLower().Contains(search.ToLower().Trim())).ToList();
                var foundAirports = new TestAirport[airports.Count];
                for (int i = 0; i < airports.Count; i++)
                {
                    foundAirport.AirportName = airports[i].AirportName;
                    foundAirport.City = airports[i].City;
                    foundAirport.Country = airports[i].Country;
                    foundAirports[i] = foundAirport;
                }
                return request.CreateResponse(HttpStatusCode.OK, foundAirports);
            }
            
        }

        public static bool IsSameFlight(FlightPlannerDBContext context, Flight CompareFlight)
        {
            if (context.Flights.Include(c => c.From).Include(c => c.To).
                    Any(c => c.From.AirportName == CompareFlight.From.AirportName && c.From.City == CompareFlight.From.City
                        && c.From.Country == CompareFlight.From.Country && c.To.AirportName == CompareFlight.To.AirportName
                        && c.To.City == CompareFlight.To.City && c.To.Country == CompareFlight.To.Country
                        && c.Carrier == CompareFlight.Carrier && c.DepartureTime == CompareFlight.DepartureTime
                        && c.ArrivalTime == CompareFlight.ArrivalTime))
            {
                return true;
            }
            return false;
            //return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public static bool DateIsInvalid(Flight flight)
        {
            int result = DateTime.Compare(DateTime.Parse(flight.DepartureTime),
                DateTime.Parse(flight.ArrivalTime));
            if (result > 0 || result == 0)
            {
                return true;
            }
            return false;
        }

        public static bool FromAirportSameTo(Flight flight)
        {
            if (flight.From.AirportName.ToLower().Trim() == flight.To.AirportName.ToLower().Trim()
                && flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim()
                && flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim())
            {
                return true;
            }

            return false;
        }

        public static bool FlightIsDeleted(int id)
        {
            using (var context = new FlightPlannerDBContext())
            {
                var flight = context.Flights.Include(c => c.From).SingleOrDefault(f => f.Id == id);

                if (flight != null)
                {
                    context.Flights.Remove(flight);
                    context.SaveChanges();
                    return true;
                }

                return false;
            }
        }

        public static bool IdOutOfRangeOk(int id)
        {
            using (var context = new FlightPlannerDBContext())
            {
                if (context.Flights.Include(c => c.Id).Any(f => f.Id != id))
                {
                    return true;
                }
                return true;
            }
            //if (id > DbFlights.Count ||
            //    id < DbFlights.Count)
            //    return true;
            //return true;
        }

        public static HttpResponseMessage FindFlightById(HttpRequestMessage request, int id)
        {
            var flightById = new TestFlight();
            using (var context = new FlightPlannerDBContext())
            {
                var idExost = context.Flights.Any(f => f.Id == id);
                if (idExost)
                {
                    var flight = context.Flights.Include(a => a.From).Include(a => a.To).SingleOrDefault(f => f.Id == id);
                    flightById.From.AirportName = flight.From.AirportName;
                    flightById.From.City = flight.From.City;
                    flightById.From.Country = flight.From.Country;
                    flightById.To.AirportName = flight.To.AirportName;
                    flightById.To.City = flight.To.City;
                    flightById.To.Country = flight.To.Country;
                    flightById.Carrier = flight.Carrier;
                    flightById.DepartureTime = flight.DepartureTime;
                    flightById.ArrivalTime = flight.ArrivalTime;
                    flightById.Id = flight.Id;

                    return request.CreateResponse(HttpStatusCode.OK, flightById);
                }
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        public static PageResult PageResult(Search flight)
        {
            var result = new PageResult();

            using (var context = new FlightPlannerDBContext())
            {
                var dbFlight = context.Flights.Include(a => a.From).Include(a => a.To).
                    SingleOrDefault(f => f.From.AirportName == flight.From);

                if (dbFlight != null && dbFlight.From.AirportName == flight.From && dbFlight.To.AirportName == flight.To
                    && dbFlight.DepartureTime.Split(' ')[0] == flight.DepartureDate)
                {
                    result.page++;
                    result.totalItems++;
                }

                return result;
            }
        }
    }
}