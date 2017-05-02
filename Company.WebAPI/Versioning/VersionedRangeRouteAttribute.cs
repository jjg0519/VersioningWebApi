using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;

namespace Company.WebAPI.Versioning
{
    public class VersionedRangeRouteAttribute : RouteFactoryAttribute
    {
        private readonly string[] _rangeVersions;

        /// <summary>
        /// 限制路由及最小運行版本號
        /// </summary>
        /// <param name="template">路由類型</param>
        /// <param name="minVersion">最小運行的版本</param>
        public VersionedRangeRouteAttribute(string template, string minVersion) : base(template)
        {
            _rangeVersions = new [] { minVersion };
        }

        /// <summary>
        /// 限制路由及運行版本號區間
        /// </summary>
        /// <param name="template">路由類型</param>
        /// <param name="minVersion">最小運行的版本</param>
        /// <param name="minVersion">最大運行的版本</param>
        public VersionedRangeRouteAttribute(string template, string minVersion, string maxVersion) : base(template)
        {
            _rangeVersions = new[] { minVersion, maxVersion };
        }

        /// <summary>
        /// 取得目前的路由限制條件
        /// </summary>
        public override IDictionary<string, object> Constraints
        {
            get
            {


                var constraints = new HttpRouteValueDictionary { {
                        "version", new VersionConstraint(_rangeVersions) } };

                return constraints;
            }
        }
    }
}