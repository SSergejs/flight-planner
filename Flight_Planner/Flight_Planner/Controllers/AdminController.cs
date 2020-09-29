using Flight_Planner.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Flight_Planner.Models;
using System.Runtime.Remoting.Messaging;
using Microsoft.Ajax.Utilities;
using System.Web.Http.Routing.Constraints;
using System.Threading;

namespace Flight_Planner.Controllers
{
    [Route("admin-api")]
    [BasicAutentication]
    public class AdminController : System.Web.Http.ApiController
    {
        [HttpGet, Route("admin-api/flights/{id}")]
        public IEnumerable<string> GetFlights()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet, Route("admin-api/flights/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage message, int id)
        {
            var flight = FlightStorage.FlightDb.FirstOrDefault(x => x.Id == id);
            if (flight == null)
            {
                return message.CreateResponse(HttpStatusCode.NotFound);
            }
            return message.CreateResponse(HttpStatusCode.OK, flight);
        }

        public static object locker = new object();

        [HttpPut, Route("admin-api/flights")]
        public HttpResponseMessage AddFlight(HttpRequestMessage request, Flight flight)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (Airports.IsTheSameAirport(flight) == true)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            int result = DateTime.Compare(DateTime.Parse(flight.DepartureTime),
                DateTime.Parse(flight.ArrivalTime));

            if (result > 0 || result == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            lock (locker)
            {
                if (FlightStorage.IsSameFlight(flight))
                {
                    return request.CreateResponse(HttpStatusCode.Conflict);
                }
                else { FlightStorage.Add(flight); }
            }
            return request.CreateResponse(HttpStatusCode.Created, flight);
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public HttpResponseMessage DeleteFlight(HttpRequestMessage message, int id)
        {
            lock (locker)
            {
                if (FlightStorage.DeleteFlight(id) == true)
                {
                    return message.CreateResponse(HttpStatusCode.OK);
                }

                else if (id > FlightStorage.FlightDb.Count ||
                    id < FlightStorage.FlightDb.Count)
                {
                    return message.CreateResponse(HttpStatusCode.OK);
                }
                return message.CreateResponse(HttpStatusCode.OK);
            }
        }

    }

}
