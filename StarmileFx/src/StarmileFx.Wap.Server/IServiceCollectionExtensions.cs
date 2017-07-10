using StarmileFx.Wap.Server.IServices;
using StarmileFx.Common.Redis;
using StarmileFx.Wap.Server.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace StarmileFx.Wap.Server
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            ////Session存于redis中
            //services.AddScoped<IDistributedCache>(
            //    serviceProvider =>
            //        new RedisCache(new RedisCacheOptions
            //        {
            //            Configuration = "127.0.0.1",
            //            InstanceName = "Session:"
            //        }));

            //依赖服务
            services.AddTransient<IRedisServer, RedisManager>();
            services.AddTransient<IBaseServer, BaseManager>();
        }
    }
}
