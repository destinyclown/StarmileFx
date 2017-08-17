using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarmileFx.Models.Json
{
    /// <summary>
    /// 网站配置文件
    /// </summary>
    public class WebConfig
    {
        /// <summary>
        /// 网站状态
        /// </summary>
        public bool WebState { get; set; }
        /// <summary>
        /// 是否测试
        /// </summary>
        public bool IsTest { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string WebName { get; set; }
        /// <summary>
        /// 测试API接口
        /// </summary>
        public string TestApiHost { get; set; }
        /// <summary>
        /// API接口
        /// </summary>
        public string ApiHost { get; set; }
        /// <summary>
        /// 测试Redis地址
        /// </summary>
        public string TestRedisHost { get; set; }
        /// <summary>
        /// Redis地址
        /// </summary>
        public string RedisHost { get; set; }
    }
}
