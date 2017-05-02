using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.WebAPI.Core.Common
{
    public partial class AppSettingConfig
    {
        /// <summary>
        /// 取得 app.config 設定值
        /// </summary>
        /// <param name="key">鍵值</param>
        /// <returns></returns>
        private static string GetAppSettings(string key)
        {
            return GetAppSettings(key, string.Empty);
        }
        /// <summary>
        /// 取得 app.config 設定值
        /// </summary>
        /// <param name="key">鍵值</param>
        /// <param name="defaultValue">預設值</param>
        /// <returns></returns>
        private static string GetAppSettings(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        /// <summary>
        /// 取得 app.config 設定列表
        /// </summary>
        /// <param name="key">鍵值</param>
        /// <param name="splitChar">分割字元</param>
        /// <returns></returns>
        private static IEnumerable<string> GetAppSettings(string key, char splitChar = ';')
        {
            return GetStringsAppSettings(key, splitChar, null);
        }

        /// <summary>
        /// 取得 app.config 設定列表
        /// </summary>
        /// <param name="key">鍵值</param>
        /// <param name="splitChar">分割字元</param>
        /// <param name="defaultValues">預設列表值</param>
        /// <returns></returns>
        private static IEnumerable<string> GetStringsAppSettings(string key, char splitChar, IEnumerable<string> defaultValues)
        {
            var values = GetAppSettings(key);

            return (!string.IsNullOrWhiteSpace(values))
                ? values.Split(new[] { splitChar }, StringSplitOptions.RemoveEmptyEntries)
                : defaultValues;
        }
    }
}
