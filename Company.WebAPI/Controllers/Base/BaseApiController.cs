using System.Net.Http;
using System.Web.Http;
using Company.WebAPI.Core.Common;

namespace Company.WebAPI.Controllers.Base
{
    public class BaseApiController : ApiController
    {
        protected virtual string CurrentVersion {
            get
            {
                var routeData = Request.GetRouteData();

                if (routeData.Values.ContainsKey("version"))
                    return routeData.Values["version"].ToString().ToLower().TrimStart('v');

                return AppSettingConfig.SYS_DefaultVersion;
            }
        }

        
    }
}