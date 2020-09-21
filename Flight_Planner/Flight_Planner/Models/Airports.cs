using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class Airports
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Airport { get; set; }

        protected bool Equals(Airports other)
        {
            return Country == other.Country && City == other.City && Airport == other.Airport;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Airports)obj);
        }

        public static List<Airports> FindAirport(string search)
        {
            List<Airports> airport = new List<Airports>();
            foreach (var flight in FlightStorage.FlightDb)
            {
                if (flight.From.Airport.ToLower().Contains(search.ToLower().Trim()) ||
                    flight.From.City.ToLower().Contains(search.ToLower().Trim()) ||
                    flight.From.Country.ToLower().Contains(search.ToLower().Trim()))
                    airport.Add(flight.From);
            }
            return airport;
        }
        public static bool IsTheSameAirport(Flight flight)
        {
            if (flight.From.Airport.ToLower().Trim() == flight.To.Airport.ToLower().Trim()
                && flight.From.Country.ToLower().Trim() == flight.To.Country.ToLower().Trim()
                && flight.From.City.ToLower().Trim() == flight.To.City.ToLower().Trim())
            {
                return true;
            }
            return false;
        }
    }
}
