using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.WebAPI.Core.Common
{
    public partial class AppSettingConfig
    {
        /// <summary>
        /// 預設的版本號
        /// </summary>
        public static string SYS_DefaultVersion
        {
            get
            {
                return GetAppSettings("SYS_DefaultVersion", "1.0");
            }
        }

        /// <summary>
        /// 可支援的版本列表
        /// </summary>
        public static string[] SYS_SupportedVersions
        {
            get
            {
                return GetStringsAppSettings("SYS_SupportedVersions", ';', 
                    new List<string> { SYS_DefaultVersion }).ToArray();
            }
        }

    }
}
