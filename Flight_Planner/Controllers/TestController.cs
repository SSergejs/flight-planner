using Flight_Planner.Models;
using Microsoft.Build.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flight_Planner.Controllers
{
    [Route("testing-api")]  
    public class TestController : System.Web.Http.ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost, Route("testing-api/clear")]
        public HttpResponseMessage Clear(HttpRequestMessage message)
        {
            using (var context = new FlightPlannerDBContext())
            {
                context.Flights.RemoveRange(context.Flights);
                context.Airports.RemoveRange(context.Airports);
                context.SaveChanges();
                return message.CreateResponse(HttpStatusCode.OK);
            }
            
        }
    }
}
