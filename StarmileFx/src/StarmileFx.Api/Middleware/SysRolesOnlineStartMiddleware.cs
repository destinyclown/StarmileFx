using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarmileFx.Api.Services;
using StarmileFx.Models.Json;

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

        public SysRolesOnlineStartMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<EmailModel> EmailModel)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<SysRolesOnlineStartMiddleware>();
            _EmailModel = EmailModel;
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
                Email email = new Email();
                email.Message = string.Format("用户于{0}启动StarmileFx.Api系统，请持续跟踪系统邮件！", DateTime.Now);
                email.Subject = "启动StarmileFx.Api系统";
                email.type = StarmileFx.Models.Enum.BaseEnum.EmailTypeEnum.Error;
                EmailService.Add(email);
                _logger.LogInformation("开启Eamil系统线程");
            }
            await _next.Invoke(context);
        }
    }
}
