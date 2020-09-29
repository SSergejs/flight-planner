using Flight_Planner.Models;
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
        public FlightStorage Post([FromBody]string value)
        {
           FlightStorage.FlightDb.Clear();
           return new FlightStorage();          
        }
    }
}
