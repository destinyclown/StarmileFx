using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarmileFx.Models.Json;
using StarmileFx.Wap.Services;
using YoungoFx.Web.Server.IService;

namespace StarmileFx.Wap.Middleware
{
    /// <summary>
    /// 开启系统线程
    /// </summary>
    public class SysRolesOnlineStartMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IYoungoServer _IYoungoServer;

        public SysRolesOnlineStartMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IYoungoServer IYoungoServer)
        {
            _next = next;
            _IYoungoServer = IYoungoServer;
            _logger = loggerFactory.CreateLogger<SysRolesOnlineStartMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!CacheService.IsStarted)
            {
                CacheService cs = new CacheService(_IYoungoServer);
                cs.Start();
                _logger.LogInformation("开启30分钟获取商品列表缓存系统线程");
            }
            await _next.Invoke(context);
        }
    }
}
