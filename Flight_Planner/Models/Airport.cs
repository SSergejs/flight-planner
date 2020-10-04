using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Flight_Planner.Models
{
    public class Airport
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [JsonProperty("airport")] 
        [Required]
        public string AirportName { get; set; }

        //protected bool Equals(Airport other)
        //{
        //    return Country == other.Country && City == other.City && AirportName == other.AirportName;
        //}
        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;
        //    if (obj.GetType() != this.GetType()) return false;
        //    return Equals((Airport)obj);
        //}
    }
}
