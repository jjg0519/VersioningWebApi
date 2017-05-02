using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Company.WebAPI.Controllers.Base;
using Company.WebAPI.Core.Module.Products;
using Company.WebAPI.Models.Common.Products;
using Company.WebAPI.Versioning;

namespace Company.WebAPI.Controllers
{
    public partial class ProductsController : BaseApiController
    {
        [EnableQuery]
        [ResponseType(typeof(IQueryable<Product>))]
        [VersionedRangeRoute("", VersionNumbers.v2_0, VersionNumbers.v3_0 )]
        // GET api/<controller>
        public IHttpActionResult GetV2()
        {
            var result = CurrentProductModule.GetAll().AsQueryable();

            return Ok(result);
        }
    }
}