using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using StarmileFx.Api.Services;

namespace StarmileFx.Api.Middleware
{
    /// <summary>
    /// 开启系统线程
    /// </summary>
    public class SysRolesOnlineStartMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public SysRolesOnlineStartMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<SysRolesOnlineStartMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            //_logger.LogError("User IP: " + context.Connection.RemoteIpAddress.ToString());
            if (!BaseService.m_isStarted)
            {
                BaseService.Start();
                _logger.LogInformation("开启在线用户(Token临时令牌)系统线程");
            }
            await _next.Invoke(context);
        }
    }
}
