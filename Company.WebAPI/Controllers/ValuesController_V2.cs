using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Company.WebAPI.Versioning;

namespace Company.WebAPI.Controllers
{
    public partial class ValuesController : ApiController
    {
        [VersionedRangeRoute("{id}", VersionNumbers.v2_0, VersionNumbers.v2_1)]
        public string GetV2(int id)
        {
            return "value2";
        }
        
    }
}