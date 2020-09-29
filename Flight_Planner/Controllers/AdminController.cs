using Flight_Planner.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Flight_Planner.Models;


namespace Flight_Planner.Controllers
{
    [Route("admin-api")]
    [BasicAutentication]
    public class AdminController : ApiController
    {

        [HttpGet, Route("admin-api/flights/{id}")]
        public IEnumerable<string> GetFlight()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet, Route("admin-api/flights/{id}")]
        public HttpResponseMessage Get(HttpRequestMessage message, int id)
        {
            var flight = FlightStorage.GetFlight(id);
            if (flight == null)
            {
                return message.CreateResponse(HttpStatusCode.NotFound);
            }
            return message.CreateResponse(HttpStatusCode.OK, flight.Id);
        }

        [HttpPut, Route("admin-api/flights")]
        public HttpResponseMessage AddFlight(HttpRequestMessage request, Flight flight)
        {
            if (ModelState.IsValid)
            {
                return FlightStorage.AddFlight(request, flight);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete, Route("admin-api/flights/{id}")]
        public HttpResponseMessage DeleteFlight(HttpRequestMessage message, int id)
        {
            if (FlightStorage.FlightIsDeleted(id))
            {
                return message.CreateResponse(HttpStatusCode.OK);
            }
            if (FlightStorage.IdOutOfRangeOk(id))
            {
                return message.CreateResponse(HttpStatusCode.OK);
            }
            return message.CreateResponse(HttpStatusCode.OK);
        }
    }

}
