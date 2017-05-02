using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Company.WebAPI.Versioning
{
    /// <summary>
    /// 版本控管路徑設定
    /// </summary>
    public class VersionedRouteAttribute : RouteFactoryAttribute
    {
        private readonly string _allowedVersions;

        /// <summary>
        /// 限制路由及版本號
        /// </summary>
        /// <param name="template">路由類型</param>
        /// <param name="allowedVersions">目前運行的版本</param>
        public VersionedRouteAttribute(string template, string allowedVersions = null) : base(template)
        {
            _allowedVersions = allowedVersions;
        }
        

        /// <summary>
        /// 取得目前的路由限制條件
        /// </summary>
        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary { { "version", new VersionConstraint(_allowedVersions) } };
                return constraints;
            }
        }

    }
}