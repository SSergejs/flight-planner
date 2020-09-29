using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Flight_Planner.Controllers
{
    public class ApiController : System.Web.Http.ApiController
    {
        // GET: api/Apii
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Apii/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Apii
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Apii/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Apii/5
        public void Delete(int id)
        {
        }
    }
}
