using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;
using StarmileFx.Api.Middleware;
using StarmileFx.Api.Server;
using StarmileFx.Api.Server.Data;
using StarmileFx.Models.Json;

namespace StarmileFx.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("sysmenus.json", optional: true, reloadOnChange: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddDbContext<BaseContext>(options => options.UseMySQL(Configuration.GetConnectionString("BaseConnection"), builder => builder.MigrationsAssembly("StarmileFx.Api")));
            services.AddDbContext<YoungoContext>(options => options.UseMySQL(Configuration.GetConnectionString("YoungoConnection"), builder => builder.MigrationsAssembly("StarmileFx.Api")));
            services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));
            services.AddMvc();

            // 添加应用程序服务。
            services.AddCoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug((c, l) => l >= LogLevel.Warning);

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            //注册输入格式
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //自定义中间件
            app.UseCustomMddleware();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=api}/{action=createsystem}/{id?}");
            });
        }
    }
}
