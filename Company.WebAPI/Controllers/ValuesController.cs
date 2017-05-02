﻿using System.Collections.Generic;
using System.Web.Http;
using Company.WebAPI.Versioning;

namespace Company.WebAPI.Controllers
{
    [RoutePrefix("api/{version}/Values")]
    public partial class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [VersionedRoute("{id}", VersionNumbers.v1_0)]
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
