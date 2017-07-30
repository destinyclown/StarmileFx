using Microsoft.AspNetCore.Builder;

namespace StarmileFx.Api.Middleware
{
    /// <summary>
    /// 自定义中间件
    /// </summary>
    public static class CustomMddlewareExtensions
    {
        /// <summary>
        /// 自定义中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomMddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SysRolesOnlineStartMiddleware>();
        }
    }
}
