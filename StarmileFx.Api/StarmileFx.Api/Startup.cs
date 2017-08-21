﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySQL.Data.EntityFrameworkCore.Extensions;
using NLog.Extensions.Logging;
using NLog.Web;
using StarmileFx.Api.Middleware;
using StarmileFx.Api.Server;
using StarmileFx.Api.Server.Data;
using StarmileFx.Models.Json;
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
            env.ConfigureNLog("nlog.config");
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
            services.AddResponseCompression();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            //add NLog.Web
            app.AddNLogWeb();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug((c, l) => l >= LogLevel.Error);

            //app.UseApplicationInsightsRequestTelemetry();

            //app.UseApplicationInsightsExceptionTelemetry();

            //注册输入格式
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            app.UseCors(builder => builder.WithOrigins("https://*").AllowAnyHeader());
            //自定义中间件
            app.UseCustomMddleware();
            app.UseResponseCompression();
            app.UseResponseCaching();
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