using YoungoFx.Web.Server.IServices;
using YoungoFx.Web.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using YoungoFx.Web.Server.IService;
using YoungoFx.Web.Server.Service;
using StarmileFx.Models.MongoDB;
using Microsoft.Extensions.Configuration;
using StarmileFx.Common.MongoDB;
using StarmileFx.Common.Redis;
using Microsoft.Extensions.Caching.Distributed;

namespace YoungoFx.Web.Server
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            //Session存于redis中
            services.AddScoped<IDistributedCache>(
                serviceProvider =>
                    new RedisCache(new RedisCacheOptions
                    {
                        Configuration = "127.0.0.1",
                        InstanceName = "Session:"
                    }));

            //依赖服务
            services.AddTransient<IRedisServer, RedisManager>()
                .AddTransient<IBaseServer, BaseManager>()
                .AddTransient<IYoungoServer, YoungoManager>();
        }
        public static IServiceCollection UserMongoLog(this IServiceCollection services,
            IConfigurationSection configurationSection)
        {
            services.Configure<MongoDBSetting>(configurationSection);
            services.AddSingleton<LogsContext>();
            return services;
        }
    }
}
