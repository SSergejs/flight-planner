using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Flight_Planner.Models
{
    public class Flight 
    {
        public int Id { get; set; }
        
        [Required]
        public Airports From { get; set; }
        
        [Required]
        public Airports To { get; set; }
        
        [Required]
        public string Carrier { get; set; }
        
        [Required]
        public string DepartureTime { get; set; }
        
        [Required]
        public string ArrivalTime { get; set; }

        protected bool Equals(Flight other)
        {
            return Equals(From, other.From) && Equals(To, other.To) &&
                   Carrier == other.Carrier && DepartureTime == other.DepartureTime && ArrivalTime == other.ArrivalTime;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Flight)obj);
        }
    }
}
