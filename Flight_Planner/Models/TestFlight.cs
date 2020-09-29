using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class TestFlight
    {
        public int Id { get; set; }
        [Required]
        public TestAirport From { get; set; }
        [Required]
        public TestAirport To { get; set; }
        [Required]
        public string Carrier { get; set; }
        [Required]
        public string DepartureTime { get; set; }
        [Required]
        public string ArrivalTime { get; set; }

        public TestFlight()
        {
            this.To = new TestAirport();
            this.From = new TestAirport();
        }
    }
}