using Flight_Planner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flight_Planner.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        [HttpGet, Route("api/airports/")]
        public HttpResponseMessage GetAirport(HttpRequestMessage message, string search)
        {
            return FlightStorage.FindAirport(message,search);
        }

        [HttpGet, Route("api/flights/{id}")]
        public HttpResponseMessage GetFlightById(HttpRequestMessage message, int id)
        {
            return FlightStorage.FindFlightById(message, id);
        }


        // POST: api/Test
        [HttpPost, Route("api/flights/search/")]
        public HttpResponseMessage PostFlightSearch(HttpRequestMessage message, Search flight)
        {
            if (!Search.SearchIsValid(flight))
            {
                return message.CreateResponse(HttpStatusCode.BadRequest);
            }

            var result = FlightStorage.PageResult(flight);
            return message.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
