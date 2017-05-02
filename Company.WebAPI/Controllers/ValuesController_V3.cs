using System.Web.Http;
using Company.WebAPI.Versioning;

namespace Company.WebAPI.Controllers
{
    public partial class ValuesController : ApiController
    {
        [VersionedRangeRoute("{id}", VersionNumbers.v3_0)]
        //[Route("{id}")]
        // GET api/<controller>/5
        public string GetV3(int id)
        {
            return "value3";
        }

    }
}