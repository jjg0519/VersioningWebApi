using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using Autofac.Features.Indexed;
using Company.WebAPI.Controllers.Base;
using Company.WebAPI.Core.Factory;
using Company.WebAPI.Core.Module.Products;
using Company.WebAPI.Models.Common.Products;
using Company.WebAPI.Versioning;
using Swashbuckle.Swagger.Annotations;

namespace Company.WebAPI.Controllers
{
    [RoutePrefix("api/{version}/Proudcts")]
    public partial class ProductsController : BaseApiController
    {

        private IFactory<IProductModule> _factory;

        public ProductsController(IFactory<IProductModule> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// 取得目前的ProductModule，若未設定該版本號則讀取預設模組
        /// </summary>
        protected IProductModule CurrentProductModule
        {
            get { return _factory.Instance(CurrentVersion); }
        }
        

        [EnableQuery]
        [ResponseType(typeof(IQueryable<Product>))]
        [VersionedRoute("", VersionNumbers.v1_0)]

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var source = CurrentProductModule.GetAll();
            var result = source.AsQueryable();
            
            return Ok(result);
        }

        /// <summary>
        /// 取得指定代碼的單一商品
        /// </summary>
        /// <param name="id">指定代碼</param>
        /// <returns>HttpActionResult</returns>
        [ResponseType(typeof(Product))]
        [SwaggerResponse(404, "Product is empty")]
        public IHttpActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Ok(CurrentProductModule.Get(id));
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult Post(Product product)
        {
            var result = CurrentProductModule.Get(1);

            return this.Ok(result);
        }
    }
}