using StarmileFx.Api.Server.IServices;
using StarmileFx.Common.Redis;
using StarmileFx.Api.Server.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace StarmileFx.Api.Server
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IBaseServer, BaseManager>();
        }
    }
}
