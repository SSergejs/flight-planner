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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet, Route("api/airports/")]
        public HttpResponseMessage GetAirport(HttpRequestMessage message, string search)
        {
            Airports[] airport = new Airports[Airports.FindAirport(search).Count];
            for (int i = 0; i < Airports.FindAirport(search).Count; i++)
            {
                airport[i] = Airports.FindAirport(search).ElementAt(i);
            }
            return message.CreateResponse(HttpStatusCode.OK, airport);
        }

        [HttpGet, Route("api/flights/{id}")]
        public HttpResponseMessage GetFlightById(HttpRequestMessage message, int id)
        {
            var flightById = new Flight();

            flightById = FlightStorage.FindFlightById(id);
            if (flightById.Id == id)
            {
                return message.CreateResponse(HttpStatusCode.OK, flightById);
            }
            return message.CreateResponse(HttpStatusCode.NotFound);
        }

        [HttpPost, Route("api/flights/search/")]
        public HttpResponseMessage PostFlightSearch(HttpRequestMessage message, Search flight)
        {
            if (!Search.SearchIsValid(flight))
            {
                return message.CreateResponse(HttpStatusCode.BadRequest);
            }
            else
            {
                var result = FlightStorage.PageResult(flight);
                return message.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}
