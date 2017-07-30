﻿using YoungoFx.Web.Server.IServices;
using StarmileFx.Common.Redis;
using YoungoFx.Web.Server.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using YoungoFx.Web.Server.IService;
using YoungoFx.Web.Server.Service;

namespace YoungoFx.Web.Server
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
            services.AddTransient<IRedisServer, RedisManager>()
                .AddTransient<IBaseServer, BaseManager>()
                .AddTransient<IYoungoServer, YoungoManager>();
        }
    }
}
