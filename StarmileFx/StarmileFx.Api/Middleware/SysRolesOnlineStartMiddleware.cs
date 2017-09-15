using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarmileFx.Api.Services;
using StarmileFx.Models.Json;
using StarmileFx.Api.Server.IServices;

namespace StarmileFx.Api.Middleware
{
    /// <summary>
    /// 开启系统线程
    /// </summary>
    public class SysRolesOnlineStartMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IOptions<EmailModel> _EmailModel;
        private readonly IBaseServer _BaseServer;

        public SysRolesOnlineStartMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<EmailModel> EmailModel, IBaseServer IBaseServer)
        {
            _next = next;
            _EmailModel = EmailModel;
            _BaseServer = IBaseServer;
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
            if (!EmailService.IsStarted)
            {
                EmailService.Start(_EmailModel.Value);
                _logger.LogInformation("开启Eamil系统线程");
            }
            if (!LogService.IsStarted)
            {
                LogService cs = new LogService(_BaseServer);
                cs.Start();
                _logger.LogInformation("开启日志系统线程");
            }
            await _next.Invoke(context);
        }
    }
}
