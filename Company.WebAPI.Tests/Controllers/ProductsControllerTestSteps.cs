using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Autofac;
using Autofac.Features.Indexed;
using Company.WebAPI.Controllers;
using Company.WebAPI.Core.Factory;
using Company.WebAPI.Core.Module.Products;
using Company.WebAPI.Models.Common.Products;
using Company.WebAPI.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using TechTalk.SpecFlow;

namespace Company.WebAPI.Tests.Controllers
{
    [Binding]
    public class ProductsControllerTestSteps
    {
        private Mock<ProductsController> _mockProductController;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProductFactory>().As<IFactory<IProductModule>>();

            builder.RegisterType<ProductV2Module>().As<IProductModule>();
            builder.RegisterType<ProductModule>().Keyed<IProductModule>(VersionNumbers.v1_0);
            builder.RegisterType<ProductV2Module>().Keyed<IProductModule>(VersionNumbers.v2_0);

            var _container = builder.Build();

            var factory = _container.Resolve<IFactory<IProductModule>>();

            _mockProductController = new Mock<ProductsController>(factory);

            _mockProductController.CallBase = true;
            _mockProductController.Object.Request = new HttpRequestMessage();
            _mockProductController.Object.Configuration = new HttpConfiguration();
        }

        [Given(@"設定商品控制器的版本號為 (.*)")]
        public void Given設定商品控制器的版本號為(string version)
        {
            _mockProductController.Protected().Setup<string>("CurrentVersion").Returns(version);
        }
        
        [When(@"取得V1版本的商品清單")]
        public void When取得V1版本的商品清單()
        {
            var actual = _mockProductController.Object.Get();

            ScenarioContext.Current.Set(actual, "result");

        }

        [When(@"取得V2版本的商品清單")]
        public void When取得V2版本的商品清單()
        {
            var actual = _mockProductController.Object.GetV2();

            ScenarioContext.Current.Set(actual, "result");
        }


        [Then(@"驗證商品清單總共有 (.*) 品")]
        public void Then驗證商品清單總共有品(int expected)
        {
            var actual = ScenarioContext.Current.Get<IHttpActionResult>("result");

            Assert.IsInstanceOfType(actual, typeof(OkNegotiatedContentResult<IQueryable<Product>>));

            var okResult = actual as OkNegotiatedContentResult<IQueryable<Product>>;

            Assert.AreEqual(expected, okResult.Content.Count());
        }
    }
}
