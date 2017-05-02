using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using Company.WebAPI.Controllers;
using Company.WebAPI.Core.Factory;
using Company.WebAPI.Core.Module.Products;
using Company.WebAPI.Versioning;

namespace Company.WebAPI
{
    public class AutofacConfig
    {

        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            //單一指定模組
            //RegisterProductSingle(builder);

            RegisterProductModule(builder);
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        private static void RegisterProductModule(ContainerBuilder builder)
        {
            builder.RegisterType<ProductFactory>().As<IFactory<IProductModule>>();

            builder.RegisterType<ProductV2Module>().As<IProductModule>();
            builder.RegisterType<ProductModule>().Keyed<IProductModule>(VersionNumbers.v1_0);
            builder.RegisterType<ProductV2Module>().Keyed<IProductModule>(VersionNumbers.v2_0);
        }
    }
}