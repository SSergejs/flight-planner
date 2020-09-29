using Flight_Planner.Controllers;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI.WebControls;

namespace Flight_Planner.Models
{
    public class FlightStorage 
    {
        public static List<Flight> FlightDb = new List<Flight>();

        public static PageResult PageResult(Search flight)
        {
            var result = new PageResult();

            foreach (var flights in FlightDb)
            {
                if(flights.From.Airport == flight.From &&
                    flights.To.Airport == flight.To &&
                    flights.DepartureTime.Split(' ')[0] == flight.DepartureDate)
                {
                    result.page++;
                    result.totalItems++;                  
                }
            }
           return result;
        }

        public static bool DeleteFlight(int id)
        {
            lock (AdminController.locker)
            {
                foreach (var flight in FlightDb)
                {
                    if (flight.Id == id)
                    {
                        FlightDb.Remove(flight);
                        return true;
                    }
                }
                return false;
            }            
        }

        public static void Add (Flight flight)
        {
            lock (AdminController.locker)
            {
                flight.Id = FlightDb.Count + 1;
                FlightDb.Add(flight);
            }
        }

        public static Flight FindFlightById(int id)
        {
            var flightById = new Flight();
            foreach (var flight in FlightDb)
            {
                if (flight.Id == id)
                {
                    flightById = flight;
                }              
            }
            return flightById;
        }
        
        public static bool IsSameFlight(Flight otherFlight)
        {
            if(FlightStorage.FlightDb.Count > 0)
            {
                foreach (var flight in FlightDb)
                {
                    if (flight.Equals(otherFlight))
                        return true;
                }
            }
            return false;
        }

    }
}