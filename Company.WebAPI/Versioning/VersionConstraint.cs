using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using Company.WebAPI.Core.Common;

namespace Company.WebAPI.Versioning
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private readonly string[] _allowedVersions;
        private readonly string[] _rangeVersions;
        
        /// <summary>
        /// RouteConfig 自定義可支援的版本號，若無設定則讀取預設值
        /// </summary>
        /// <param name="allowedVersions">自定義可支援的版本號</param>
        public VersionConstraint(string allowedVersions = null)
        {
            // If no allowed versions are specified, the allowed versions are the supported versions
            _allowedVersions = !string.IsNullOrWhiteSpace(allowedVersions) ?
                allowedVersions.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray() :
                SupportedVersions;
        }

        /// <summary>
        /// RouteConfig 自定義可支援的版本號區間
        /// </summary>
        /// <param name="rangeVersions">版本號區間，若僅設定一組版本號則從此版號至最新版號</param>
        public VersionConstraint(string[] rangeVersions)
        {
            _rangeVersions = rangeVersions;
        }

        /// <summary>
        /// 取得預設限制的版本號清單
        /// </summary>
        protected virtual string[] SupportedVersions
        {
            get { return AppSettingConfig.SYS_SupportedVersions; }
        }

        /// <summary>
        /// 檢查目前HttpRequest是否符合版本號
        /// </summary>
        /// <param name="request">目前的 HttpRequest</param>
        /// <param name="route">目前路由路徑定義</param>
        /// <param name="parameterName">目前參數名稱</param>
        /// <param name="values">目前參數值</param>
        /// <param name="routeDirection">指定路由轉導位置</param>
        /// <returns></returns>
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection != HttpRouteDirection.UriResolution) return true;
            if (!values.ContainsKey(parameterName)) return false;

            var version = ((string)values[parameterName]).ToLower().TrimStart('v');
            
            return MatchVersion(version, SupportedVersions)
                && MatchVersion(version, _allowedVersions)
                && MatchRangeVersions(version, _rangeVersions);

        }

        /// <summary>
        /// 檢查版本號是否可使用
        /// </summary>
        /// <param name="version">目前的版本號</param>
        /// <param name="allowedVersions">可支援的版本號</param>
        /// <returns></returns>
        private static bool MatchVersion(string version, string[] allowedVersions)
        {
            if (allowedVersions == null) return true;

            return (allowedVersions.Contains(version, StringComparer.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 檢查版本號是否可在版本區間內
        /// </summary>
        /// <param name="version">目前的版本號</param>
        /// <param name="rangeVersions">版本區間</param>
        /// <returns></returns>
        private static bool MatchRangeVersions(string version, string[] rangeVersions)
        {
            if (rangeVersions == null) return true;

            var minVersion = rangeVersions.Length >= 1 ? rangeVersions[0] : version;
            var maxVersion = rangeVersions.Length >= 2 ? rangeVersions[1] : version; 

            return CompareVersionNumbers(version, minVersion) >= 0
                && CompareVersionNumbers(version, maxVersion) <= 0;
        }

        /// <summary>
        /// 比對版本號
        /// </summary>
        /// <param name="versionA">版本號A</param>
        /// <param name="versionB">版本號B</param>
        /// <returns></returns>
        private static int CompareVersionNumbers(string versionA, string versionB)
        {
            if (versionA == versionB)
                return 0;

            var a = versionA.Split('.');
            var b = versionB.Split('.');

            var size = new int[] { a.Length, b.Length }.Max();
            
            for (var i = 0; i < size; i++)
            {
                var numberA = a.Length > i ? int.Parse(a[i]) : 0;
                var numberB = b.Length > i ? int.Parse(b[i]) : 0;

                if (numberA > numberB)
                    return 1;
                else if (numberB > numberA)
                    return -1;
            }

            return 0;
        }
        
    }
}