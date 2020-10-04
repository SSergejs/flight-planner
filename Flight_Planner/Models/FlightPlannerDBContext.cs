using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Flight_Planner.Models
{
    public class FlightPlannerDBContext : DbContext
    { 
        public FlightPlannerDBContext() : base ("flight-planner")
        {
            Database.SetInitializer<FlightPlannerDBContext>(null);
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}