using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;
using StarmileFx.Api.Middleware;
using StarmileFx.Api.Server;
using StarmileFx.Api.Server.Data;
using StarmileFx.Models.Json;
using System.IO;
using System.Text;

namespace StarmileFx.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<BaseContext>(options => options.UseMySQL(Configuration.GetConnectionString("BaseConnection"), builder => builder.MigrationsAssembly("StarmileFx.Api")));
            services.AddDbContext<YoungoContext>(options => options.UseMySQL(Configuration.GetConnectionString("YoungoConnection"), builder => builder.MigrationsAssembly("StarmileFx.Api")));
            //读取配置
            services.Configure<EmailModel>(Configuration.GetSection("EmailConfig"));
            services.Configure<SysMenusModel>(Configuration.GetSection("SysMenus"));
            services.AddMvc();

            // 添加应用程序服务。
            services.AddCoreServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug((c, l) => l >= LogLevel.Error);

            //app.UseApplicationInsightsRequestTelemetry();

            //app.UseApplicationInsightsExceptionTelemetry();

            //注册输入格式
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //自定义中间件
            app.UseCustomMddleware();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=api}/{action=Index}/{id?}");
            });
        }
    }
}
