using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;

namespace Company.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //停用MVC回應標頭
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);


            #region Serializer
            // 設定 WebAPI 使用 Newtonsoft.Json 套件進行全站序列化
            var serializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            var contractResolver = serializerSettings.ContractResolver as DefaultContractResolver;

            contractResolver.IgnoreSerializableAttribute = true; 
            #endregion

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig.RegisterAutofac();
            
        }

        /// <summary>
        /// 每次需求後回傳的標頭處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            var application = sender as HttpApplication;

            if (application != null && application.Context != null)
            {
                application.Context.Response.Headers.Remove("Server");
                application.Context.Response.Headers.Remove("X-AspNet-Version");
            }
        }
    }
}
