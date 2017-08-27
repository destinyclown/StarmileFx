using StarmileFx.Api.Server.IServices;
using StarmileFx.Api.Server.Services;
using Microsoft.Extensions.DependencyInjection;

namespace StarmileFx.Api.Server
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IBaseServer, BaseManager>()
                .AddScoped<IYoungoServer, YoungoManager>();
        }
    }
}
