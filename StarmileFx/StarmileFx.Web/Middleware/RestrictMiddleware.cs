using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarmileFx.Models.Json;
using System.Net;
using System.Threading.Tasks;

namespace StarmileFx.Web.Middleware
{
    public class RestrictMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IOptions<WebConfig> _WebConfig;

        public RestrictMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<WebConfig> WebConfig)
        {
            _next = next;
            _WebConfig = WebConfig;
            _logger = loggerFactory.CreateLogger<RestrictMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!_WebConfig.Value.WebState)
            {
                ContentResult cr = new ContentResult
                {
                    Content = "<div><script>top.location.href='/home/maintain';</script></div>",
                    StatusCode = (int)HttpStatusCode.ServiceUnavailable
                };
                context.Response.Redirect("/home/maintain");
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                //_logger.LogInformation("开启30分钟获取商品列表缓存系统线程");
            }
            await _next.Invoke(context);
        }
    }
}
