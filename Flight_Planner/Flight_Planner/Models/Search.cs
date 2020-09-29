using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class Search
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DepartureDate { get; set; }
      
        public static bool SearchIsValid(Search searchFlight)
        {          
            if (searchFlight == null)
                return false;
            if (searchFlight.From == searchFlight.To)
                return false;
            if (searchFlight.From == null || searchFlight.To == null || searchFlight.DepartureDate == null)
                return false;
            return true;
        }
    }
}