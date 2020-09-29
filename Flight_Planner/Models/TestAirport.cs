using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class TestAirport
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [JsonProperty("airport")]
        [Required]
        public string AirportName { get; set; }
    }
}